namespace ITClassHelper;

partial class FormCastControl
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCastControl));
        FormControllerLabel = new Label();
        HideCastButton = new Button();
        MinimizeCastButton = new Button();
        ShowCastButton = new Button();
        HideControllerButton = new Button();
        SetCastTitleBarButton = new Button();
        SuspendLayout();
        // 
        // FormControllerLabel
        // 
        FormControllerLabel.AutoSize = true;
        FormControllerLabel.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        FormControllerLabel.Location = new Point(10, 10);
        FormControllerLabel.Name = "FormControllerLabel";
        FormControllerLabel.Size = new Size(79, 20);
        FormControllerLabel.TabIndex = 8;
        FormControllerLabel.Text = "广播控制器";
        // 
        // HideCastButton
        // 
        HideCastButton.Cursor = Cursors.Hand;
        HideCastButton.FlatStyle = FlatStyle.System;
        HideCastButton.Font = new Font("微软雅黑", 11F, FontStyle.Regular, GraphicsUnit.Point, 134);
        HideCastButton.Location = new Point(11, 41);
        HideCastButton.Name = "HideCastButton";
        HideCastButton.Size = new Size(88, 75);
        HideCastButton.TabIndex = 9;
        HideCastButton.Text = "隐藏\r\n广播";
        HideCastButton.UseVisualStyleBackColor = true;
        HideCastButton.Click += HideCastButton_Click;
        // 
        // MinimizeCastButton
        // 
        MinimizeCastButton.Cursor = Cursors.Hand;
        MinimizeCastButton.FlatStyle = FlatStyle.System;
        MinimizeCastButton.Font = new Font("微软雅黑", 11F, FontStyle.Regular, GraphicsUnit.Point, 134);
        MinimizeCastButton.Location = new Point(104, 41);
        MinimizeCastButton.Name = "MinimizeCastButton";
        MinimizeCastButton.Size = new Size(88, 75);
        MinimizeCastButton.TabIndex = 10;
        MinimizeCastButton.Text = "缩小\r\n广播";
        MinimizeCastButton.UseVisualStyleBackColor = true;
        MinimizeCastButton.Click += MinimizeCastButton_Click;
        // 
        // ShowCastButton
        // 
        ShowCastButton.Cursor = Cursors.Hand;
        ShowCastButton.FlatStyle = FlatStyle.System;
        ShowCastButton.Font = new Font("微软雅黑", 11F, FontStyle.Regular, GraphicsUnit.Point, 134);
        ShowCastButton.Location = new Point(197, 41);
        ShowCastButton.Name = "ShowCastButton";
        ShowCastButton.Size = new Size(88, 75);
        ShowCastButton.TabIndex = 11;
        ShowCastButton.Text = "恢复\r\n广播";
        ShowCastButton.UseVisualStyleBackColor = true;
        ShowCastButton.Click += ShowCastButton_Click;
        // 
        // HideControllerButton
        // 
        HideControllerButton.Cursor = Cursors.Hand;
        HideControllerButton.FlatStyle = FlatStyle.System;
        HideControllerButton.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134);
        HideControllerButton.Location = new Point(356, 8);
        HideControllerButton.Name = "HideControllerButton";
        HideControllerButton.Size = new Size(23, 22);
        HideControllerButton.TabIndex = 13;
        HideControllerButton.Text = "-";
        HideControllerButton.UseVisualStyleBackColor = true;
        HideControllerButton.Click += HideControllerButton_Click;
        // 
        // SetCastTitleBarButton
        // 
        SetCastTitleBarButton.Cursor = Cursors.Hand;
        SetCastTitleBarButton.FlatStyle = FlatStyle.System;
        SetCastTitleBarButton.Font = new Font("微软雅黑", 11F, FontStyle.Regular, GraphicsUnit.Point, 134);
        SetCastTitleBarButton.Location = new Point(291, 42);
        SetCastTitleBarButton.Name = "SetCastTitleBarButton";
        SetCastTitleBarButton.Size = new Size(88, 75);
        SetCastTitleBarButton.TabIndex = 14;
        SetCastTitleBarButton.Text = "显示\r\n标题栏";
        SetCastTitleBarButton.UseVisualStyleBackColor = true;
        SetCastTitleBarButton.Click += SetCastTitleBarButton_Click;
        // 
        // FormCastControl
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(387, 129);
        ControlBox = false;
        Controls.Add(SetCastTitleBarButton);
        Controls.Add(HideControllerButton);
        Controls.Add(ShowCastButton);
        Controls.Add(MinimizeCastButton);
        Controls.Add(HideCastButton);
        Controls.Add(FormControllerLabel);
        FormBorderStyle = FormBorderStyle.None;
        Icon = (Icon)resources.GetObject("$this.Icon");
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FormCastControl";
        ShowIcon = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.Manual;
        Text = "FormCastControl";
        TopMost = true;
        ResumeLayout(false);
        PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label FormControllerLabel;
    private System.Windows.Forms.Button HideCastButton;
    private System.Windows.Forms.Button MinimizeCastButton;
    private System.Windows.Forms.Button ShowCastButton;
    private System.Windows.Forms.Button HideControllerButton;
    private System.Windows.Forms.Button SetCastTitleBarButton;
}