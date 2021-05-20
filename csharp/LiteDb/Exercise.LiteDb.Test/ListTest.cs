using LiteDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace Exercise.LiteDb.Test.FilesExample
{
    [TestClass]
    public class ListTest
    {
        [TestMethod]
        public void WriteList()
        {
            var dbName = "List.db";

            using (var db = new LiteRepository(dbName))
            {
                var entry = new ListEntry
                {
                    Content = new List<string>()
                    {
                        "Miroslav",
                        "Mikus",
                        "Juliane",
                        "Mirka"
                    }
                };

                db.Insert(entry, "someList");
            }

            File.Delete(dbName);
        }

        [TestMethod]
        public void WriteDictionary()
        {
            var dbName = "Dict.db";

            using (var db = new LiteRepository(dbName))
            {
                var entry = new DictionaryEntry
                {
                    Content = new Dictionary<string, string[]>()
                    {
                        {  "Miroslav", new string []{ "1", "2" , "3" ,} },
                        {  "Mikus", new string []{ "4","5","6"} },
                        {  "Juliane", new string []{ "7","8","9"} },
                        {  "Mirka", new string []{ "10","11","12"} }
                    }
                };

                db.Insert(entry, "someList");
            }

            File.Delete(dbName);
        }

        class ListEntry
        {
            public int Id { get; set; } = 2;
            public List<string> Content { get; set; }
        }

        class DictionaryEntry
        {
            public int Id { get; set; } = 2;
            public Dictionary<string, string[]> Content { get; set; }
        }
    }
}
