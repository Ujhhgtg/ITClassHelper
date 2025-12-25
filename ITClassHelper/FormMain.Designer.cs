namespace ITClassHelper;

partial class FormMain
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
        DeviceManageButton = new Button();
        UpdateProgramButton = new Button();
        ClassStatusGroup = new GroupBox();
        ClassStatusLabel = new Label();
        RoomStatusButton = new Button();
        ProcessControlGroup = new GroupBox();
        ResumeRoomButton = new Button();
        KillRoomButton = new Button();
        SuspendRoomButton = new Button();
        UtilitiesGroup = new GroupBox();
        FileServerButton = new Button();
        RemoveSystemLimitsButton = new Button();
        ProgramSettingsCheckBox = new CheckBox();
        ExitProgramButton = new Button();
        ProgramAboutLabel = new Label();
        SetRoomPathButton = new Button();
        PswdControlGroup = new GroupBox();
        GetPswdButton = new Button();
        SetPswdButton = new Button();
        LogGroup = new GroupBox();
        LogTextBox = new RichTextBox();
        contextMenuStrip1 = new ContextMenuStrip(components);
        ClassStatusGroup.SuspendLayout();
        ProcessControlGroup.SuspendLayout();
        UtilitiesGroup.SuspendLayout();
        PswdControlGroup.SuspendLayout();
        LogGroup.SuspendLayout();
        SuspendLayout();
        // 
        // DeviceManageButton
        // 
        DeviceManageButton.Cursor = Cursors.Hand;
        DeviceManageButton.FlatStyle = FlatStyle.System;
        DeviceManageButton.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        DeviceManageButton.Location = new Point(210, 22);
        DeviceManageButton.Name = "DeviceManageButton";
        DeviceManageButton.Size = new Size(197, 40);
        DeviceManageButton.TabIndex = 35;
        DeviceManageButton.Text = "设备管理器";
        DeviceManageButton.UseVisualStyleBackColor = true;
        DeviceManageButton.Click += DeviceManageButton_Click;
        // 
        // UpdateProgramButton
        // 
        UpdateProgramButton.Cursor = Cursors.Hand;
        UpdateProgramButton.Enabled = false;
        UpdateProgramButton.FlatStyle = FlatStyle.System;
        UpdateProgramButton.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        UpdateProgramButton.Location = new Point(7, 22);
        UpdateProgramButton.Name = "UpdateProgramButton";
        UpdateProgramButton.Size = new Size(197, 40);
        UpdateProgramButton.TabIndex = 11;
        UpdateProgramButton.Text = "更新已禁用";
        UpdateProgramButton.UseVisualStyleBackColor = true;
        UpdateProgramButton.Click += UpdateProgramButton_Click;
        // 
        // ClassStatusGroup
        // 
        ClassStatusGroup.Controls.Add(ClassStatusLabel);
        ClassStatusGroup.Controls.Add(RoomStatusButton);
        ClassStatusGroup.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
        ClassStatusGroup.Location = new Point(10, 8);
        ClassStatusGroup.Name = "ClassStatusGroup";
        ClassStatusGroup.Size = new Size(246, 97);
        ClassStatusGroup.TabIndex = 0;
        ClassStatusGroup.TabStop = false;
        ClassStatusGroup.Text = "教室状态";
        // 
        // ClassStatusLabel
        // 
        ClassStatusLabel.AutoSize = true;
        ClassStatusLabel.BackColor = Color.White;
        ClassStatusLabel.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        ClassStatusLabel.Location = new Point(82, 46);
        ClassStatusLabel.Name = "ClassStatusLabel";
        ClassStatusLabel.Size = new Size(65, 20);
        ClassStatusLabel.TabIndex = 2;
        ClassStatusLabel.Text = "状态未知";
        // 
        // RoomStatusButton
        // 
        RoomStatusButton.BackColor = Color.White;
        RoomStatusButton.Enabled = false;
        RoomStatusButton.Location = new Point(6, 22);
        RoomStatusButton.Name = "RoomStatusButton";
        RoomStatusButton.Size = new Size(234, 69);
        RoomStatusButton.TabIndex = 1;
        RoomStatusButton.UseVisualStyleBackColor = false;
        // 
        // ProcessControlGroup
        // 
        ProcessControlGroup.Controls.Add(ResumeRoomButton);
        ProcessControlGroup.Controls.Add(KillRoomButton);
        ProcessControlGroup.Controls.Add(SuspendRoomButton);
        ProcessControlGroup.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
        ProcessControlGroup.Location = new Point(10, 112);
        ProcessControlGroup.Name = "ProcessControlGroup";
        ProcessControlGroup.Size = new Size(246, 76);
        ProcessControlGroup.TabIndex = 3;
        ProcessControlGroup.TabStop = false;
        ProcessControlGroup.Text = "进程控制";
        // 
        // ResumeRoomButton
        // 
        ResumeRoomButton.Cursor = Cursors.Hand;
        ResumeRoomButton.FlatStyle = FlatStyle.System;
        ResumeRoomButton.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        ResumeRoomButton.Location = new Point(166, 22);
        ResumeRoomButton.Name = "ResumeRoomButton";
        ResumeRoomButton.Size = new Size(74, 48);
        ResumeRoomButton.TabIndex = 6;
        ResumeRoomButton.Text = "恢复";
        ResumeRoomButton.UseVisualStyleBackColor = true;
        ResumeRoomButton.Click += ResumeRoomButton_Click;
        // 
        // KillRoomButton
        // 
        KillRoomButton.Cursor = Cursors.Hand;
        KillRoomButton.FlatStyle = FlatStyle.System;
        KillRoomButton.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        KillRoomButton.Location = new Point(6, 22);
        KillRoomButton.Name = "KillRoomButton";
        KillRoomButton.Size = new Size(74, 48);
        KillRoomButton.TabIndex = 4;
        KillRoomButton.Text = "关闭";
        KillRoomButton.UseVisualStyleBackColor = true;
        KillRoomButton.Click += KillRoomButton_Click;
        // 
        // SuspendRoomButton
        // 
        SuspendRoomButton.Cursor = Cursors.Hand;
        SuspendRoomButton.FlatStyle = FlatStyle.System;
        SuspendRoomButton.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        SuspendRoomButton.Location = new Point(86, 22);
        SuspendRoomButton.Name = "SuspendRoomButton";
        SuspendRoomButton.Size = new Size(74, 48);
        SuspendRoomButton.TabIndex = 5;
        SuspendRoomButton.Text = "挂起";
        SuspendRoomButton.UseVisualStyleBackColor = true;
        SuspendRoomButton.Click += SuspendRoomButton_Click;
        // 
        // UtilitiesGroup
        // 
        UtilitiesGroup.Controls.Add(FileServerButton);
        UtilitiesGroup.Controls.Add(RemoveSystemLimitsButton);
        UtilitiesGroup.Controls.Add(ProgramSettingsCheckBox);
        UtilitiesGroup.Controls.Add(DeviceManageButton);
        UtilitiesGroup.Controls.Add(ExitProgramButton);
        UtilitiesGroup.Controls.Add(ProgramAboutLabel);
        UtilitiesGroup.Controls.Add(SetRoomPathButton);
        UtilitiesGroup.Controls.Add(UpdateProgramButton);
        UtilitiesGroup.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
        UtilitiesGroup.Location = new Point(262, 8);
        UtilitiesGroup.Name = "UtilitiesGroup";
        UtilitiesGroup.Size = new Size(413, 262);
        UtilitiesGroup.TabIndex = 10;
        UtilitiesGroup.TabStop = false;
        UtilitiesGroup.Text = "实用工具";
        // 
        // FileServerButton
        // 
        FileServerButton.Cursor = Cursors.Hand;
        FileServerButton.FlatStyle = FlatStyle.System;
        FileServerButton.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        FileServerButton.Location = new Point(7, 114);
        FileServerButton.Name = "FileServerButton";
        FileServerButton.Size = new Size(197, 40);
        FileServerButton.TabIndex = 40;
        FileServerButton.Text = "启动文件服务器";
        FileServerButton.UseVisualStyleBackColor = true;
        FileServerButton.Click += FileServerButton_Click;
        // 
        // RemoveSystemLimitsButton
        // 
        RemoveSystemLimitsButton.Cursor = Cursors.Hand;
        RemoveSystemLimitsButton.FlatStyle = FlatStyle.System;
        RemoveSystemLimitsButton.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        RemoveSystemLimitsButton.Location = new Point(210, 68);
        RemoveSystemLimitsButton.Name = "RemoveSystemLimitsButton";
        RemoveSystemLimitsButton.Size = new Size(197, 40);
        RemoveSystemLimitsButton.TabIndex = 39;
        RemoveSystemLimitsButton.Text = "移除系统限制（U 盘与网络）";
        RemoveSystemLimitsButton.UseVisualStyleBackColor = true;
        RemoveSystemLimitsButton.Click += RemoveFlashDriveLimitButton_Click;
        // 
        // ProgramSettingsCheckBox
        // 
        ProgramSettingsCheckBox.AutoSize = true;
        ProgramSettingsCheckBox.FlatStyle = FlatStyle.System;
        ProgramSettingsCheckBox.Location = new Point(144, 160);
        ProgramSettingsCheckBox.Name = "ProgramSettingsCheckBox";
        ProgramSettingsCheckBox.Size = new Size(129, 22);
        ProgramSettingsCheckBox.TabIndex = 38;
        ProgramSettingsCheckBox.Text = "显示机房助手设置";
        ProgramSettingsCheckBox.UseVisualStyleBackColor = true;
        ProgramSettingsCheckBox.CheckedChanged += ProgramSettingsCheckBox_CheckedChanged;
        // 
        // ExitProgramButton
        // 
        ExitProgramButton.Cursor = Cursors.Hand;
        ExitProgramButton.FlatStyle = FlatStyle.System;
        ExitProgramButton.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        ExitProgramButton.Location = new Point(210, 210);
        ExitProgramButton.Name = "ExitProgramButton";
        ExitProgramButton.Size = new Size(85, 40);
        ExitProgramButton.TabIndex = 33;
        ExitProgramButton.Text = "退出";
        ExitProgramButton.UseVisualStyleBackColor = true;
        ExitProgramButton.Click += ExitProgramButton_Click;
        // 
        // ProgramAboutLabel
        // 
        ProgramAboutLabel.Anchor = AnchorStyles.Right;
        ProgramAboutLabel.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        ProgramAboutLabel.Location = new Point(7, 210);
        ProgramAboutLabel.Name = "ProgramAboutLabel";
        ProgramAboutLabel.Size = new Size(197, 40);
        ProgramAboutLabel.TabIndex = 14;
        ProgramAboutLabel.Text = "版本号：X.Y.Z\r\n作者：Ujhhgtg";
        ProgramAboutLabel.TextAlign = ContentAlignment.MiddleRight;
        // 
        // SetRoomPathButton
        // 
        SetRoomPathButton.Cursor = Cursors.Hand;
        SetRoomPathButton.FlatStyle = FlatStyle.System;
        SetRoomPathButton.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        SetRoomPathButton.Location = new Point(7, 68);
        SetRoomPathButton.Name = "SetRoomPathButton";
        SetRoomPathButton.Size = new Size(197, 40);
        SetRoomPathButton.TabIndex = 13;
        SetRoomPathButton.Text = "设置教室位置";
        SetRoomPathButton.UseVisualStyleBackColor = true;
        SetRoomPathButton.Click += SetRoomPathButton_Click;
        // 
        // PswdControlGroup
        // 
        PswdControlGroup.Controls.Add(GetPswdButton);
        PswdControlGroup.Controls.Add(SetPswdButton);
        PswdControlGroup.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
        PswdControlGroup.Location = new Point(10, 194);
        PswdControlGroup.Name = "PswdControlGroup";
        PswdControlGroup.Size = new Size(246, 76);
        PswdControlGroup.TabIndex = 7;
        PswdControlGroup.TabStop = false;
        PswdControlGroup.Text = "密码设置";
        // 
        // GetPswdButton
        // 
        GetPswdButton.Cursor = Cursors.Hand;
        GetPswdButton.FlatStyle = FlatStyle.System;
        GetPswdButton.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        GetPswdButton.Location = new Point(6, 22);
        GetPswdButton.Name = "GetPswdButton";
        GetPswdButton.Size = new Size(115, 48);
        GetPswdButton.TabIndex = 8;
        GetPswdButton.Text = "读取";
        GetPswdButton.UseVisualStyleBackColor = true;
        GetPswdButton.Click += GetPswdButton_Click;
        // 
        // SetPswdButton
        // 
        SetPswdButton.Cursor = Cursors.Hand;
        SetPswdButton.FlatStyle = FlatStyle.System;
        SetPswdButton.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        SetPswdButton.Location = new Point(126, 22);
        SetPswdButton.Name = "SetPswdButton";
        SetPswdButton.Size = new Size(114, 48);
        SetPswdButton.TabIndex = 9;
        SetPswdButton.Text = "设置";
        SetPswdButton.UseVisualStyleBackColor = true;
        SetPswdButton.Click += SetPswdButton_Click;
        // 
        // LogGroup
        // 
        LogGroup.Controls.Add(LogTextBox);
        LogGroup.Location = new Point(10, 276);
        LogGroup.Name = "LogGroup";
        LogGroup.Size = new Size(665, 140);
        LogGroup.TabIndex = 11;
        LogGroup.TabStop = false;
        LogGroup.Text = "日志输出";
        // 
        // LogTextBox
        // 
        LogTextBox.Location = new Point(6, 22);
        LogTextBox.Multiline = true;
        LogTextBox.Name = "LogTextBox";
        LogTextBox.ReadOnly = true;
        LogTextBox.ScrollBars = RichTextBoxScrollBars.Both;
        LogTextBox.Size = new Size(653, 112);
        LogTextBox.TabIndex = 0;
        // 
        // contextMenuStrip1
        // 
        contextMenuStrip1.Name = "contextMenuStrip1";
        contextMenuStrip1.Size = new Size(61, 4);
        // 
        // FormMain
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(685, 428);
        Controls.Add(LogGroup);
        Controls.Add(PswdControlGroup);
        Controls.Add(UtilitiesGroup);
        Controls.Add(ClassStatusGroup);
        Controls.Add(ProcessControlGroup);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Icon = (Icon)resources.GetObject("$this.Icon");
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FormMain";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "机房助手";
        FormClosing += FormMain_FormClosing;
        ClassStatusGroup.ResumeLayout(false);
        ClassStatusGroup.PerformLayout();
        ProcessControlGroup.ResumeLayout(false);
        UtilitiesGroup.ResumeLayout(false);
        UtilitiesGroup.PerformLayout();
        PswdControlGroup.ResumeLayout(false);
        LogGroup.ResumeLayout(false);
        LogGroup.PerformLayout();
        ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Button UpdateProgramButton;
    private System.Windows.Forms.GroupBox ClassStatusGroup;
    private System.Windows.Forms.Label ClassStatusLabel;
    private System.Windows.Forms.GroupBox ProcessControlGroup;
    private System.Windows.Forms.Button ResumeRoomButton;
    private System.Windows.Forms.Button KillRoomButton;
    private System.Windows.Forms.Button SuspendRoomButton;
    private System.Windows.Forms.GroupBox UtilitiesGroup;
    private System.Windows.Forms.Label ProgramAboutLabel;
    private System.Windows.Forms.Button SetRoomPathButton;
    private System.Windows.Forms.Button RoomStatusButton;
    private System.Windows.Forms.GroupBox PswdControlGroup;
    private System.Windows.Forms.Button GetPswdButton;
    private System.Windows.Forms.Button SetPswdButton;
    private System.Windows.Forms.Button ExitProgramButton;
    private System.Windows.Forms.Button DeviceManageButton;
    private Button RemoveSystemLimitsButton;
    private CheckBox ProgramSettingsCheckBox;
    private Button FileServerButton;
    private GroupBox LogGroup;
    private RichTextBox LogTextBox;
    private ContextMenuStrip contextMenuStrip1;
}