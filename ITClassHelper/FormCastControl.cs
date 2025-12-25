using ITClassHelper.Common;
using Ujhhgtg.Library.Windows;
using Vanara.PInvoke;

namespace ITClassHelper;

internal partial class FormCastControl : Form
{
    private readonly FormMain parent;

    public FormCastControl(FormMain parent)
    {
        InitializeComponent();
        this.parent = parent;
    }

    private void HideCastButton_Click(object sender, EventArgs e)
    {
        HideCast();
        Hide();
    }

    private void ShowCastButton_Click(object sender, EventArgs e) => ShowCast();

    private void HideControllerButton_Click(object sender, EventArgs e) => Hide();

    private void ShowCast()
    {
        var castWindow = ClassUtils.GetCastWindow();
        User32.SetWindowPos(castWindow, WindowPositionWin.NoTopMost, 0, 0, Screen.PrimaryScreen!.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, User32.SetWindowPosFlags.SWP_SHOWWINDOW);
        Hide();
    }

    private void HideCast()
    {
        var castWindow = ClassUtils.GetCastWindow();
        User32.SetWindowPos(castWindow, WindowPositionWin.NoTopMost, Size.Width, Size.Height, 0, 0, User32.SetWindowPosFlags.SWP_SHOWWINDOW);
        Hide();
    }

    private void MinimizeCastButton_Click(object sender, EventArgs e)
    {
        var castWindow = ClassUtils.GetCastWindow();
        User32.SetWindowPos(castWindow, WindowPositionWin.NoTopMost, Size.Width, Size.Height, 1000, 500, User32.SetWindowPosFlags.SWP_SHOWWINDOW);
        Hide();
    }

    private void SetCastTitleBarButton_Click(object sender, EventArgs e)
    {
        var castWindow = ClassUtils.GetCastWindow();
        if (!castWindow.IsNull)
        {
            WindowUtilsWin.EnableChrome(castWindow);
        }
    }
}
