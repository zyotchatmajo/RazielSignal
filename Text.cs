using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazielSignal
{
    class Text : File
    {
        public string Content { get; set; }
        public Text(string Name, DateTime CreationDate, string Path)
        {
            this.Name = Name;
            this.CreationDate = CreationDate;
            this.Path = Path;
            Type = "Text File";
        }

        public static Text CreateTextFile(string Name)
        {
            return new Text(Name, DateTime.Now, "temp");
        }

        public void EditContent(string Content)
        {
            this.Content = Content;
        }

        public static List<Text> ListAllTextFiles(string path)
        {
            return (from TextFile in Directory.EnumerateFiles(path) select new Text(TextFile.Split('\\').Last(), Directory.GetCreationTime(TextFile), TextFile)).ToList();
        }
    }
}
