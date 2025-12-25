namespace ITClassHelper;

partial class FormDeviceManage
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDeviceManage));
        DeviceList = new ListView();
        IpColumn = new ColumnHeader();
        MacColumn = new ColumnHeader();
        HostnameColumn = new ColumnHeader();
        ClientIdColumn = new ColumnHeader();
        DeviceContextMenu = new ContextMenuStrip(components);
        SendCmdMenuItem = new ToolStripMenuItem();
        SendMsgMenuItem = new ToolStripMenuItem();
        SendScriptMenuItem = new ToolStripMenuItem();
        Seperator1MenuItem = new ToolStripSeparator();
        ShutdownMenuItem = new ToolStripMenuItem();
        RebootMenuItem = new ToolStripMenuItem();
        MagicCommandMenuItem = new ToolStripMenuItem();
        BluescreenMenuItem = new ToolStripMenuItem();
        PortLabel = new Label();
        IPButton = new Button();
        PortTextBox = new TextBox();
        IPRangeTextBox = new TextBox();
        IPLabel3 = new Label();
        IPTextBox = new TextBox();
        ScanButton = new Button();
        ScanHostnameCheckBox = new CheckBox();
        ScanMacAddressCheckBox = new CheckBox();
        ConvertHostNameIPLabel = new Label();
        HostNameTextBox = new TextBox();
        ConvertHostNameIPButton = new Button();
        ClientButton = new Button();
        ServerButton = new Button();
        DeviceContextMenu.SuspendLayout();
        SuspendLayout();
        // 
        // DeviceList
        // 
        DeviceList.Columns.AddRange(new ColumnHeader[] { IpColumn, MacColumn, HostnameColumn, ClientIdColumn });
        DeviceList.ContextMenuStrip = DeviceContextMenu;
        DeviceList.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
        DeviceList.Location = new Point(14, 46);
        DeviceList.Name = "DeviceList";
        DeviceList.Size = new Size(531, 508);
        DeviceList.TabIndex = 41;
        DeviceList.UseCompatibleStateImageBehavior = false;
        DeviceList.View = View.Details;
        // 
        // IpColumn
        // 
        IpColumn.Text = "IP 地址";
        IpColumn.Width = 147;
        // 
        // MacColumn
        // 
        MacColumn.Text = "MAC 地址";
        MacColumn.Width = 154;
        // 
        // HostnameColumn
        // 
        HostnameColumn.Text = "计算机名";
        HostnameColumn.Width = 150;
        // 
        // ClientIdColumn
        // 
        ClientIdColumn.Tag = "";
        ClientIdColumn.Text = "ID";
        // 
        // DeviceContextMenu
        // 
        DeviceContextMenu.ImageScalingSize = new Size(20, 20);
        DeviceContextMenu.Items.AddRange(new ToolStripItem[] { SendCmdMenuItem, SendMsgMenuItem, SendScriptMenuItem, Seperator1MenuItem, ShutdownMenuItem, RebootMenuItem, MagicCommandMenuItem });
        DeviceContextMenu.Name = "DeviceContextMenu";
        DeviceContextMenu.RenderMode = ToolStripRenderMode.System;
        DeviceContextMenu.ShowImageMargin = false;
        DeviceContextMenu.Size = new Size(100, 142);
        // 
        // SendCmdMenuItem
        // 
        SendCmdMenuItem.Name = "SendCmdMenuItem";
        SendCmdMenuItem.Size = new Size(99, 22);
        SendCmdMenuItem.Text = "发送命令";
        SendCmdMenuItem.Click += SendCmdMenuItem_Click;
        // 
        // SendMsgMenuItem
        // 
        SendMsgMenuItem.Name = "SendMsgMenuItem";
        SendMsgMenuItem.Size = new Size(99, 22);
        SendMsgMenuItem.Text = "发送消息";
        SendMsgMenuItem.Click += SendMsgMenuItem_Click;
        // 
        // SendScriptMenuItem
        // 
        SendScriptMenuItem.Name = "SendScriptMenuItem";
        SendScriptMenuItem.Size = new Size(99, 22);
        SendScriptMenuItem.Text = "发送脚本";
        SendScriptMenuItem.Click += SendScriptMenuItem_Click;
        // 
        // Seperator1MenuItem
        // 
        Seperator1MenuItem.Name = "Seperator1MenuItem";
        Seperator1MenuItem.Size = new Size(96, 6);
        // 
        // ShutdownMenuItem
        // 
        ShutdownMenuItem.Name = "ShutdownMenuItem";
        ShutdownMenuItem.Size = new Size(99, 22);
        ShutdownMenuItem.Text = "远程关机";
        ShutdownMenuItem.Click += ShutdownMenuItem_Click;
        // 
        // RebootMenuItem
        // 
        RebootMenuItem.Name = "RebootMenuItem";
        RebootMenuItem.Size = new Size(99, 22);
        RebootMenuItem.Text = "远程重启";
        RebootMenuItem.Click += RebootMenuItem_Click;
        // 
        // MagicCommandMenuItem
        // 
        MagicCommandMenuItem.DropDownItems.AddRange(new ToolStripItem[] { BluescreenMenuItem });
        MagicCommandMenuItem.Name = "MagicCommandMenuItem";
        MagicCommandMenuItem.Size = new Size(99, 22);
        MagicCommandMenuItem.Text = "魔法指令";
        // 
        // BluescreenMenuItem
        // 
        BluescreenMenuItem.Name = "BluescreenMenuItem";
        BluescreenMenuItem.Size = new Size(124, 22);
        BluescreenMenuItem.Text = "快速蓝屏";
        BluescreenMenuItem.Click += BsodMenuItem_Click;
        // 
        // PortLabel
        // 
        PortLabel.AutoSize = true;
        PortLabel.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        PortLabel.Location = new Point(338, 18);
        PortLabel.Name = "PortLabel";
        PortLabel.Size = new Size(12, 20);
        PortLabel.TabIndex = 46;
        PortLabel.Text = ":";
        // 
        // IPButton
        // 
        IPButton.Cursor = Cursors.Hand;
        IPButton.FlatStyle = FlatStyle.System;
        IPButton.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        IPButton.Location = new Point(14, 15);
        IPButton.Name = "IPButton";
        IPButton.Size = new Size(114, 25);
        IPButton.TabIndex = 44;
        IPButton.Text = "IP 地址";
        IPButton.UseVisualStyleBackColor = true;
        IPButton.Click += IPButton_Click;
        // 
        // PortTextBox
        // 
        PortTextBox.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        PortTextBox.Location = new Point(356, 15);
        PortTextBox.Name = "PortTextBox";
        PortTextBox.Size = new Size(85, 25);
        PortTextBox.TabIndex = 47;
        PortTextBox.Text = "4705";
        PortTextBox.TextAlign = HorizontalAlignment.Center;
        // 
        // IPRangeTextBox
        // 
        IPRangeTextBox.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        IPRangeTextBox.Location = new Point(287, 15);
        IPRangeTextBox.Name = "IPRangeTextBox";
        IPRangeTextBox.Size = new Size(47, 25);
        IPRangeTextBox.TabIndex = 45;
        IPRangeTextBox.Text = "1";
        IPRangeTextBox.TextAlign = HorizontalAlignment.Center;
        // 
        // IPLabel3
        // 
        IPLabel3.AutoSize = true;
        IPLabel3.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        IPLabel3.Location = new Point(266, 18);
        IPLabel3.Name = "IPLabel3";
        IPLabel3.Size = new Size(15, 20);
        IPLabel3.TabIndex = 43;
        IPLabel3.Text = "-";
        // 
        // IPTextBox
        // 
        IPTextBox.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        IPTextBox.Location = new Point(133, 15);
        IPTextBox.Name = "IPTextBox";
        IPTextBox.Size = new Size(128, 25);
        IPTextBox.TabIndex = 42;
        IPTextBox.Text = "192.168.1.1";
        IPTextBox.TextAlign = HorizontalAlignment.Center;
        // 
        // ScanButton
        // 
        ScanButton.Cursor = Cursors.Hand;
        ScanButton.FlatStyle = FlatStyle.System;
        ScanButton.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        ScanButton.Location = new Point(444, 15);
        ScanButton.Name = "ScanButton";
        ScanButton.Size = new Size(99, 25);
        ScanButton.TabIndex = 48;
        ScanButton.Text = "扫描";
        ScanButton.UseVisualStyleBackColor = true;
        ScanButton.Click += ScanButton_Click;
        // 
        // ScanHostnameCheckBox
        // 
        ScanHostnameCheckBox.AutoSize = true;
        ScanHostnameCheckBox.Checked = true;
        ScanHostnameCheckBox.CheckState = CheckState.Checked;
        ScanHostnameCheckBox.FlatStyle = FlatStyle.System;
        ScanHostnameCheckBox.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        ScanHostnameCheckBox.Location = new Point(263, 560);
        ScanHostnameCheckBox.Name = "ScanHostnameCheckBox";
        ScanHostnameCheckBox.Size = new Size(258, 25);
        ScanHostnameCheckBox.TabIndex = 49;
        ScanHostnameCheckBox.Text = "扫描计算机名（大幅减慢扫描速度）";
        ScanHostnameCheckBox.UseVisualStyleBackColor = true;
        // 
        // ScanMacAddressCheckBox
        // 
        ScanMacAddressCheckBox.AutoSize = true;
        ScanMacAddressCheckBox.Checked = true;
        ScanMacAddressCheckBox.CheckState = CheckState.Checked;
        ScanMacAddressCheckBox.FlatStyle = FlatStyle.System;
        ScanMacAddressCheckBox.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        ScanMacAddressCheckBox.Location = new Point(14, 560);
        ScanMacAddressCheckBox.Name = "ScanMacAddressCheckBox";
        ScanMacAddressCheckBox.Size = new Size(243, 25);
        ScanMacAddressCheckBox.TabIndex = 50;
        ScanMacAddressCheckBox.Text = "扫描 MAC 地址（减慢扫描速度）";
        ScanMacAddressCheckBox.UseVisualStyleBackColor = true;
        // 
        // ConvertHostNameIPLabel
        // 
        ConvertHostNameIPLabel.AutoSize = true;
        ConvertHostNameIPLabel.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        ConvertHostNameIPLabel.Location = new Point(14, 594);
        ConvertHostNameIPLabel.Name = "ConvertHostNameIPLabel";
        ConvertHostNameIPLabel.Size = new Size(65, 20);
        ConvertHostNameIPLabel.TabIndex = 53;
        ConvertHostNameIPLabel.Text = "计算机名";
        // 
        // HostNameTextBox
        // 
        HostNameTextBox.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        HostNameTextBox.Location = new Point(85, 591);
        HostNameTextBox.Name = "HostNameTextBox";
        HostNameTextBox.Size = new Size(339, 25);
        HostNameTextBox.TabIndex = 52;
        // 
        // ConvertHostNameIPButton
        // 
        ConvertHostNameIPButton.Cursor = Cursors.Hand;
        ConvertHostNameIPButton.FlatStyle = FlatStyle.System;
        ConvertHostNameIPButton.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        ConvertHostNameIPButton.Location = new Point(430, 591);
        ConvertHostNameIPButton.Name = "ConvertHostNameIPButton";
        ConvertHostNameIPButton.Size = new Size(113, 25);
        ConvertHostNameIPButton.TabIndex = 51;
        ConvertHostNameIPButton.Text = "→ IP 地址";
        ConvertHostNameIPButton.UseVisualStyleBackColor = true;
        ConvertHostNameIPButton.Click += ConvertHostNameIPButton_Click;
        // 
        // ClientButton
        // 
        ClientButton.Cursor = Cursors.Hand;
        ClientButton.FlatStyle = FlatStyle.System;
        ClientButton.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        ClientButton.Location = new Point(15, 622);
        ClientButton.Name = "ClientButton";
        ClientButton.Size = new Size(261, 25);
        ClientButton.TabIndex = 54;
        ClientButton.Text = "连接服务器";
        ClientButton.UseVisualStyleBackColor = true;
        ClientButton.Click += ClientButton_Click;
        // 
        // ServerButton
        // 
        ServerButton.Cursor = Cursors.Hand;
        ServerButton.FlatStyle = FlatStyle.System;
        ServerButton.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        ServerButton.Location = new Point(282, 622);
        ServerButton.Name = "ServerButton";
        ServerButton.Size = new Size(261, 25);
        ServerButton.TabIndex = 55;
        ServerButton.Text = "启动服务器";
        ServerButton.UseVisualStyleBackColor = true;
        ServerButton.Click += ServerButton_Click;
        // 
        // FormDeviceManage
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(555, 659);
        Controls.Add(ServerButton);
        Controls.Add(ClientButton);
        Controls.Add(ConvertHostNameIPLabel);
        Controls.Add(HostNameTextBox);
        Controls.Add(ConvertHostNameIPButton);
        Controls.Add(ScanMacAddressCheckBox);
        Controls.Add(ScanHostnameCheckBox);
        Controls.Add(ScanButton);
        Controls.Add(PortLabel);
        Controls.Add(IPButton);
        Controls.Add(PortTextBox);
        Controls.Add(IPRangeTextBox);
        Controls.Add(IPLabel3);
        Controls.Add(IPTextBox);
        Controls.Add(DeviceList);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "FormDeviceManage";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "设备管理器";
        FormClosing += FormDeviceManage_FormClosing;
        DeviceContextMenu.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();

    }

    #endregion
    private System.Windows.Forms.ListView DeviceList;
    private System.Windows.Forms.ColumnHeader IpColumn;
    private System.Windows.Forms.ColumnHeader MacColumn;
    private System.Windows.Forms.ContextMenuStrip DeviceContextMenu;
    private System.Windows.Forms.ToolStripMenuItem SendCmdMenuItem;
    private System.Windows.Forms.ToolStripMenuItem SendMsgMenuItem;
    private System.Windows.Forms.ToolStripSeparator Seperator1MenuItem;
    private System.Windows.Forms.ToolStripMenuItem ShutdownMenuItem;
    private System.Windows.Forms.ToolStripMenuItem RebootMenuItem;
    private System.Windows.Forms.Label PortLabel;
    private System.Windows.Forms.Button IPButton;
    private System.Windows.Forms.TextBox PortTextBox;
    private System.Windows.Forms.TextBox IPRangeTextBox;
    private System.Windows.Forms.Label IPLabel3;
    private System.Windows.Forms.Button ScanButton;
    private System.Windows.Forms.ColumnHeader HostnameColumn;
    private System.Windows.Forms.CheckBox ScanHostnameCheckBox;
    private System.Windows.Forms.CheckBox ScanMacAddressCheckBox;
    private System.Windows.Forms.ToolStripMenuItem SendScriptMenuItem;
    private System.Windows.Forms.Label ConvertHostNameIPLabel;
    private System.Windows.Forms.TextBox HostNameTextBox;
    private System.Windows.Forms.Button ConvertHostNameIPButton;
    private System.Windows.Forms.ToolStripMenuItem MagicCommandMenuItem;
    private System.Windows.Forms.ToolStripMenuItem BluescreenMenuItem;
    private System.Windows.Forms.TextBox IPTextBox;
    private Button ClientButton;
    private Button ServerButton;
    private ColumnHeader ClientIdColumn;
}