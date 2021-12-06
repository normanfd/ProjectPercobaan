using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Action
{
    public static class ReadFileAction
    {
        public static string ReadFromPath(string URL_CONFIGURATION)
        {
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            string HOME_DIRECTORY = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "C:" + Environment.GetEnvironmentVariable("HOMEPATH") : Environment.GetEnvironmentVariable("HOME");
            string URL_CONFIG = Path(HOME_DIRECTORY + URL_CONFIGURATION);
            return File.ReadAllText(URL_CONFIG);
        }
        private static string Path(string Path)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return Path.Replace(@"\", "/");
            }
            return Path;
        }
        public static string ReadFile(string URL_CONFIGURATION)
        {
            return File.ReadAllText(URL_CONFIGURATION);
        }
    }
}
