using System.Reflection;
using Ujhhgtg.Library;
using Ujhhgtg.Library.ExtensionMethods;

namespace ITClassHelper.Common;

internal static class Const
{
    public static readonly string AppName = "ITClassHelper";
    public static string AppVersion
    {
        get
        {
            _appVersion ??= Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()!.InformationalVersion;
            return _appVersion;
        }
    }
    private static string? _appVersion = null;
    public static readonly PathObject AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).AsPath() / AppName;
    public static readonly PathObject AppPath = AppDataPath / $"{AppName}.exe";

    public static readonly PathObject ClientIdPath = AppDataPath / "client.id";
    public static readonly PathObject NtsdPath = AppDataPath / "ntsd.exe";
    public static readonly PathObject CopypartyPath = AppDataPath / "copyparty.exe";
    public static readonly string CopypartyArgs = "-i :: -e2dsa --dotpart --lang chi --theme 2 --ui-filesz 5c --localtime  --no-bauth -p 4567 --chdir {Path}";

    public static readonly int ServerPort = 4545;
    public static readonly int FileServerPort = 4546;
}
