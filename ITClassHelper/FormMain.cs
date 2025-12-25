using ITClassHelper.Common;
using Microsoft.Win32;
using Ookii.Dialogs.WinForms;
using Serilog;
using System.Diagnostics;
using Ujhhgtg.Library;
using Ujhhgtg.Library.ExtensionMethods;
using Ujhhgtg.Library.Windows;
using Ujhhgtg.Library.WinForms;
using Vanara.PInvoke;
using Keys = Ujhhgtg.Library.Keys;
using OpenFileDialog = Ookii.Dialogs.WinForms.VistaOpenFileDialog;
using TaskDialogIcon = Ookii.Dialogs.WinForms.TaskDialogIcon;
namespace ITClassHelper;

internal partial class FormMain : Form
{
    //public readonly Socket socket = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    //public bool socketBound = false;

    private readonly FormCastControl castControl;
    private readonly FormDeviceManage deviceManage;

    private string? classPath = null;
    private Process? classProcess = null;

    private bool firstTimeHide = true;
    private WithProcessTool killClassMethods = WithProcessTool.Native | WithProcessTool.Nt | WithProcessTool.Ntsd;
    private readonly HotKeyWin hotKey;

    public bool Running = true;

    public FormMain(string[] args)
    {
        // single instance check
        if (ProcessUtils.GetAll("ITClassHelper").Count > 1)
        {
            foreach (Process programProc in ProcessUtils.GetAll("ITClassHelper"))
            {
                int procId = programProc.Id;
                if (procId != Environment.ProcessId)
                {
                    UiUtils.ShowErrorDialog("程序已在运行！点击[确认]退出当前进程！");
                    Environment.Exit(1);
                }
            }
        }

        // resources
        Directory.CreateDirectory(Const.AppDataPath);

        if (AssemblyUtils.IsSingleFile)
        {
            if (Environment.ProcessPath!.AsPath().FullPath != Const.AppPath.FullPath)
            {
                File.Copy(Environment.ProcessPath!, Const.AppPath, true);
                ProcessUtils.Run(Const.AppPath, null, null, false);
                Environment.Exit(0);
            }
        }
        else
        {
            UiUtils.ShowWarningDialog("程序未以单文件模式编译，无法复制到本地目录运行！");
        }

        var rescNtsd = Properties.Resources.ntsd;
        using var ntsdFile = new FileStream(Const.NtsdPath, FileMode.OpenOrCreate);
        ntsdFile.Write(rescNtsd, 0, rescNtsd.Length);

        var rescCopyparty = Properties.Resources.copyparty;
        using var copypartyFile = new FileStream(Const.CopypartyPath, FileMode.OpenOrCreate);
        copypartyFile.Write(rescCopyparty, 0, rescCopyparty.Length);

        ProcessUtilsWin.NtsdPath = Const.NtsdPath;

        // ui 1
        InitializeComponent();

        // logging (must be after InitializeComponent() cuz we are writing to the textbox)
        Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Is(Serilog.Events.LogEventLevel.Verbose)
                    .WriteTo.Console()
                    .WriteTo.File(Const.AppDataPath / "log.txt", rollingInterval: RollingInterval.Day)
                    .WriteTo.RichTextBox(LogTextBox)
                    .CreateLogger();

        // ui 2
        ProgramAboutLabel.Text = ProgramAboutLabel.Text.Replace("X.Y.Z", Const.AppVersion);
        if (Const.AppVersion.Contains("alpha") || Const.AppVersion.Contains("beta"))
        {
            UiUtils.ShowWarningDialog("这是一个实验性版本，尚不稳定，请小心操作！");
        }
        castControl = new FormCastControl(this);
        castControl.Show();
        castControl.Hide();
        deviceManage = new FormDeviceManage(this);

        // hotkey
        hotKey = new HotKeyWin(Handle, 100, User32.HotKeyModifiers.MOD_ALT, Keys.H);
        hotKey.Register();

        //// messaging
        //if (NetworkUtils.IsPortInUse(Const.ServerPort) != true)
        //{
        //    try
        //    {
        //        if (socketBound == false)
        //        {
        //            socket.Bind(new IPEndPoint(IPAddress.Any, Const.ServerPort));
        //            socketBound = true;
        //        }
        //    }
        //    catch (SocketException ex)
        //    {
        //        UiUtils.ShowErrorDialog($"本机 IP 地址绑定失败！将无法使用[简易内网聊天]功能！\n错因：{ex.Message}");
        //        CommunicationButton.Enabled = false;
        //    }
        //}
        //else
        //{
        //    UiUtils.ShowErrorDialog($"本机聊天端口 {Const.ServerPort} 被占用！将无法使用[简易内网聊天]功能！");
        //    CommunicationButton.Enabled = false;
        //}
        //new Thread(ReceiveMessage) { IsBackground = true }.Start();

        // background thread
        new Thread(BackgroundThread) { IsBackground = true }.Start();

        // log info
        Log.Information("IsSingleFile: {Value}", AssemblyUtils.IsSingleFile);
        Log.Information("IsSelfContained: {Value}", AssemblyUtils.IsSelfContained);
    }

    private void BackgroundThread()
    {
        while (Running)
        {
            SetClassPath(true);
            var castWindow = ClassUtils.GetCastWindow();
            if (!castWindow.IsNull)
            {
                WindowUtilsWin.EnableChrome(castWindow);
                //RemoveHooks();
                if (Visible == true)
                {
                    TopMost = true;
                }
            }
            else
            {
                if (TopMost == true)
                {
                    TopMost = false;
                }
            }
            if (MousePosition == new Point(0, 0))
            {
                User32.SetWindowPos(castWindow, WindowPositionWin.NoTopMost, castControl.Size.Width, castControl.Size.Height, 1000, 500, User32.SetWindowPosFlags.SWP_SHOWWINDOW);
                castControl.Show();
            }

            var procs = ProcessUtils.GetAll("StudentMain");
            if (procs.Count > 0)
            {
                classProcess = procs[0];
            }
            else
            {
                procs = ProcessUtils.GetAll("REDAgent");
                if (procs.Count > 0)
                {
                    classProcess = procs[0];
                }
                else
                {
                    classProcess = null;
                }
            }

            if (classProcess != null)
            {
                ClassStatusLabel.Text = "正在运行";
                ClassStatusLabel.ForeColor = Color.Red;
            }
            else
            {
                ClassStatusLabel.Text = "未在运行";
                ClassStatusLabel.ForeColor = Color.Green;
            }
            Thread.Sleep(1000);
        }
    }

    private void SuspendRoomButton_Click(object sender, EventArgs e) => SuspendRoom();

    private void SuspendRoom()
    {
        if (classProcess != null)
        {
            ProcessUtilsWin.Suspend(classProcess);
        }
    }

    private void KillRoomButton_Click(object sender, EventArgs e) => KillRoom();

    private void KillRoom()
    {
        if (classProcess != null)
            ProcessUtilsWin.Terminate(classProcess, killClassMethods);
    }

    private void ResumeRoomButton_Click(object sender, EventArgs e) => ResumeRoom();

    private void ResumeRoom()
    {
        if (classProcess != null)
            ProcessUtilsWin.Resume(classProcess);
        else
        {
            if (classPath != null)
                ProcessUtils.Run(classPath, null, null, false);
            else
                UiUtils.ShowErrorDialog("教室程序路径未设置，无法启动教室！");
        }
    }

    private void UpdateProgramButton_Click(object sender, EventArgs e)
    {
    }

    private void SetRoomPathButton_Click(object sender, EventArgs e) => SetClassPath(false);

    private void SetClassPath(bool silent = true)
    {
        if (classPath != null)
        {
            if (silent == false)
            {
                UiUtils.ShowDialog("已获取过了教室程序路径！", null, null, TaskDialogIcon.Information);
            }
        }
        else
        {
            if (ProcessUtils.GetAll("StudentMain").Count > 0 || ProcessUtils.GetAll("REDAgent").Count > 0)
            {
                classPath = ProcessUtils.GetAll("StudentMain").Count > 0
                    ? ProcessUtils.GetAll("StudentMain")[0].MainModule!.FileName
                    : ProcessUtils.GetAll("REDAgent")[0].MainModule!.FileName;
                if (silent == false)
                {
                    UiUtils.ShowDialog("已自动获取到教室程序路径！");
                }
            }
            else
            {
                var defRoomPath = @"C:\Program Files\Mythware\e-Learning Class\StudentMain.exe";
                if (File.Exists(defRoomPath))
                {
                    classPath = defRoomPath;
                    if (silent == false)
                    {
                        UiUtils.ShowDialog("已自动获取到教室程序路径！");
                    }
                }
                else
                {
                    if (silent == false)
                    {
                        if (UiUtils.ShowDialog("无法自动找到教室程序路径！\n按[确定]在下一界面手动选择；\n按[取消]放弃。", ["确定", "取消"], "错误", TaskDialogIcon.Error) == 1)
                        {
                            return;
                        }
                        var fileDialog = new OpenFileDialog
                        {
                            Multiselect = false,
                            Title = "选择教室程序",
                            Filter = "教室程序(*.exe)|*.exe"
                        };
                        if (fileDialog.ShowDialog() == DialogResult.OK)
                        {
                            classPath = fileDialog.FileName;
                        }
                    }
                }
            }
        }
    }

    //private void ReceiveMessage()
    //{
    //    while (Running)
    //    {
    //        EndPoint serverPoint = new IPEndPoint(IPAddress.Any, 0);
    //        var buffer = new byte[10240];
    //        int length = socket.ReceiveFrom(buffer, ref serverPoint);
    //        var message = Encoding.UTF8.GetString(buffer, 0, length);
    //        Task.Run(() => UiUtils.ShowDialog(message, null, $"来自 {serverPoint} 的消息"));
    //    }
    //}

    private void GetPswdButton_Click(object sender, EventArgs e)
    {
        if (UiUtils.ShowDialog("友情提示：可在任何时候使用超级密码 mythware_super_password！\n按[确定]继续获取；\n按[取消]将其复制到剪贴板并放弃获取。", ["确定", "取消"]) == 1)
        {
            Clipboard.SetDataObject("mythware_super_password");
            return;
        }

        try
        {
            var pswdKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\TopDomain\e-Learning Class Standard\1.00") ?? throw new Exception();
            var pswd = (string)(pswdKey.GetValue("UninstallPasswd") ?? throw new Exception());
            UiUtils.ShowDialog($"密码为：{pswd[6..]}");
        }
        catch
        {
            UiUtils.ShowErrorDialog("无法获取到教室密码！");
            return;
        }
    }

    private void SetPswdButton_Click(object sender, EventArgs e)
    {
        if (UiUtils.ShowDialog("设置密码完成后将自动重启教室！\n按[确定]继续设置；\n按[取消]放弃设置。", ["确定", "取消"], "警告", TaskDialogIcon.Warning) == 1)
        {
            return;
        }

        if (UiUtils.ShowDialog("温馨提示：可先尝试使用超级密码 mythware_super_password！\n按[确定]继续设置；\n按[取消]将其复制到剪贴板并放弃设置。", ["确定", "取消"]) == 1)
        {
            Clipboard.SetDataObject("mythware_super_password");
            return;
        }
        string keyDir = @"HKEY_LOCAL_MACHINE\SOFTWARE\TopDomain\e-Learning Class Standard\1.00";
        var (ok, pswdText) = UiUtils.ShowInputDialog("请输入要设置的密码：", "信息");
        if (!ok) return;
        Registry.SetValue(keyDir, "UninstallPasswd", $@"Passwd{pswdText}");
        ProcessUtilsWin.TerminateAll("StudentMain", killClassMethods);
        Thread.Sleep(250);
        ProcessUtils.Run(classPath ?? throw new Exception("教室程序路径未设置！"));
    }

    private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        if (firstTimeHide == true)
        {
            UiUtils.ShowDialog("机房助手已隐藏到后台，按下 Alt+H 即可显示！");
            firstTimeHide = false;
        }
        Hide();
    }

    private void ExitProgramButton_Click(object sender, EventArgs e)
    {
        Running = false;
        //socket.Close();
        //socket.Dispose();
        hotKey.Unregister();
        StopSimpleFileServer();
        StopCopyparty();
        Environment.Exit(0);
    }

    private void DeviceManageButton_Click(object sender, EventArgs e) => deviceManage.Show();

    //private void CommunicationButton_Click(object sender, EventArgs e)
    //{
    //    var (ipOk, clientIp) = UiUtils.ShowInputDialog("请输入服务器的 IP 地址：", null, null, null, UiUtils.ValidateIp);
    //    if (!ipOk) return;
    //    EndPoint clientPoint = new IPEndPoint(IPAddress.Parse(clientIp!), Const.ServerPort);
    //    var (msgOk, msg) = UiUtils.ShowInputDialog("请输入要发送的消息内容：");
    //    if (!msgOk) return;
    //    var bytes = Encoding.UTF8.GetBytes($"msg\n{msg!}");
    //    try
    //    {
    //        socket.SendTo(bytes, clientPoint);
    //    }
    //    catch (SocketException ex)
    //    {
    //        UiUtils.ShowErrorDialog($"发送消息失败！\n错因：{ex.Message}");
    //        return;
    //    }
    //    UiUtils.ShowDialog("发送消息成功！");
    //}

    private void ProgramSettingsCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        foreach (var control in new List<Control> {
            UpdateProgramButton, SetRoomPathButton, DeviceManageButton, RemoveSystemLimitsButton, FileServerButton
        })
        {
            control.Visible = !ProgramSettingsCheckBox.Checked;
        }
    }

    protected override void WndProc(ref Message msg)
    {
        const int WM_HOTKEY = 0x0312;
        switch (msg.Msg)
        {
            case WM_HOTKEY:
                switch (msg.WParam.ToInt32())
                {
                    case 100:
                        Visible = !Visible;
                        Focus();
                        break;
                }
                break;
        }
        base.WndProc(ref msg);
    }

    private void RemoveFlashDriveLimitButton_Click(object sender, EventArgs e)
    {
        try
        {
            ServiceUtilsWin.Stop("TDFileFilter");
            ServiceUtilsWin.Delete("TDFileFilter");
            UiUtils.ShowDialog("移除 U 盘限制成功！");
        }
        catch (Exception ex)
        {
            UiUtils.ShowErrorDialog($"移除 U 盘限制失败！（提示：请勿重复移除！）\n错因：{ex.Message}");
        }
        try
        {
            ServiceUtilsWin.Stop("TDNetFilter");
            ServiceUtilsWin.Delete("TDNetFilter");
            UiUtils.ShowDialog("移除网络限制成功！");
            if (UiUtils.ShowDialog("是否重启所有网络适配器以使更改生效？", ["确定", "取消"], "选择") == 0)
            {
                NetworkUtilsWin.RestartRealInterfaces();
            }
        }
        catch (Exception ex)
        {
            UiUtils.ShowErrorDialog($"移除网络限制失败！（提示：请勿重复移除！）\n错因：{ex.Message}");
        }
    }

    private SimpleFileServer? fileServer = null;
    private Process? copypartyProcess = null;

    private void StopSimpleFileServer()
    {
        if (fileServer == null) return;
        fileServer.Stop();
        fileServer = null;
    }

    private void StopCopyparty()
    {
        if (copypartyProcess == null) return;
        ProcessUtilsWin.SendCtrlEvent(copypartyProcess, Kernel32.CTRL_EVENT.CTRL_C_EVENT);
        copypartyProcess.WaitForExit();
        copypartyProcess = null;
    }

    private void FileServerButton_Click(object sender, EventArgs e)
    {
        if (fileServer != null || copypartyProcess != null)
        {
            StopSimpleFileServer();
            StopCopyparty();
            FileServerButton.Text = "启动文件服务器";
        }
        else
        {
            var dialog = new VistaFolderBrowserDialog
            {
                Description = "选择文件服务器根目录"
            };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            var (mtdOk, method) = UiUtils.ShowInputDialog("请输入文件服务器实现：\n1. 内置 (SimpleFileServer)\n2. copyparty", null, "1", null, x => UiUtils.ValidateInt(x, 1, 2));
            if (!mtdOk) return;

            switch (method)
            {
                case "1":
                    fileServer = new([$"http://*:{Const.FileServerPort}/"], dialog.SelectedPath);
                    fileServer.Start();
                    break;
                case "2":
                    var process = new Process()
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = Const.CopypartyPath,
                            Arguments = Const.CopypartyArgs.Replace("{Path}", dialog.SelectedPath),
                            WindowStyle = ProcessWindowStyle.Hidden,
                            CreateNoWindow = true,
                            UseShellExecute = false,
                            RedirectStandardInput = true
                        }
                    };
                    process.Start();
                    copypartyProcess = process;
                    break;
            }
            UiUtils.ShowDialog($"已启动文件服务器于端口 {Const.FileServerPort}！", null, null, TaskDialogIcon.Information);
            FileUtils.Open($"http://127.0.0.1:{Const.FileServerPort}");
            FileServerButton.Text = "关闭文件服务器";
        }
    }
}
