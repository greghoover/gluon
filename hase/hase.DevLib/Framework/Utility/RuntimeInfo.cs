using System.Runtime.InteropServices;

namespace hase.DevLib.Framework.Utility
{
    public static class RuntimeInfo
    {
        public static string Framework => RuntimeInformation.FrameworkDescription;
        public static string OS => RuntimeInformation.OSDescription;
        public static Architecture OSArch => RuntimeInformation.OSArchitecture;
        public static Architecture ProcArch => RuntimeInformation.ProcessArchitecture;

        public static bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        public static bool IsUWP => OS == "Microsoft Windows"; // e.g. 'Microsoft Windows 10.0.17134' for Win10 desktop
        public static bool IsLinux => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        public static bool IsAndroid => RuntimeInformation.IsOSPlatform(OSPlatform.Create("ANDROID"));
        public static bool IsOsx => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        public static bool IsIos => RuntimeInformation.IsOSPlatform(OSPlatform.Create("IOS"));
    }
}
