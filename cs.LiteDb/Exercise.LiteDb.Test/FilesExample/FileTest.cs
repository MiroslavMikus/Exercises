using LiteDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace Exercise.LiteDb.Test.FilesExample
{
    [TestClass]
    public class FileTest
    {
        [TestMethod]
        public void WriteAndReadFile()
        {
            var fileID = "my/file-id";

            var dbName = "FileDb.db";

            using (var db = new LiteDatabase(dbName))
            {
                // Upload a file from file system
                db.FileStorage.Upload(fileID, @"TestData\ExampleFile.txt");

                // Upload a file from Stream
                //db.FileStorage.Upload("/my/file-id", myStream);
            }

            using (var db = new LiteDatabase(dbName))
            {
                // Open as an stream
                var fileStream = db.FileStorage.OpenRead(fileID);

                // Write to another stream
                var stream = new StreamReader(fileStream, Encoding.Unicode);

                var test = stream.ReadToEnd();
            }

            File.Delete(dbName);
        }
    }
}
