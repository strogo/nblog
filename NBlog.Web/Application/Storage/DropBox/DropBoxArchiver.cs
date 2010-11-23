using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using AppLimit.CloudComputing.SharpBox;
using AppLimit.CloudComputing.SharpBox.DropBox;

namespace NBlog.Web.Application.Storage.DropBox
{
    public class DropBoxArchiver
    {
        public void Archive(string filename, MemoryStream memoryStream)
        {
            // todo: move into Config
            var credentials = new DropBoxCredentials
            {
                ConsumerKey = "",
                ConsumerSecret = "",
                UserName = "",
                Password = ""
            };

            var storage = new CloudStorage();
            storage.Open(DropBoxConfiguration.GetStandardConfiguration(), credentials);

            var backupFolder = storage.GetFolder("/NBlog");
            if (backupFolder == null) { throw new Exception("DropBox folder not found"); }

            var cloudFile = storage.CreateFile(backupFolder, filename);
            using (var cloudStream = cloudFile.GetContentStream(FileAccess.Write))
            {
                cloudStream.Write(memoryStream.GetBuffer(), 0, (int)memoryStream.Position);
            }

            if (storage.IsOpened) { storage.Close(); }
        }

    }
}