using System.Diagnostics;
using System.Reflection;

namespace SilkFlo.Data.Core
{
  public class Settings
  {
    public static bool IsOpening { get; set; }

    public static string DateFormatLong => "dd MMM yyyy";

    public static string DateFormatShort => "yyyy-MM-dd";

    public static string DateTimeFormatShort => "yyyy-MM-dd hh:mm:ss";

    public static string ApplicationName => "SilkFlo";

    public static string Version => FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;

    public static string VersionString => "Version " + Settings.Version;
  }
}
