using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RazielSignal
{
    class Folder : File
    {
        public Folder(string Name, DateTime CreationDate, string Path)
        {
            this.Name = Name;
            this.CreationDate = CreationDate;
            this.Path = Path;
            Type = "File Folder";
        }

        public static bool CreateFolder(string Path, string Name)
        {
            if (Directory.Exists(Path + "\\" + Name))
            {
                return false;
            }
            else
            {
                Directory.CreateDirectory(Path + "\\" + Name);
                return true;
            }
        }
        public static bool DeleteFolder(string Path)
        {
            if (Directory.Exists(Path))
            {
                return false;
            }
            else
            {
                Directory.Delete(Path, true);
                return true;
            }
        }

        public static List<Folder> ListAllFolders(string path)
        {
            return (from Folder in Directory.EnumerateDirectories(path) select new Folder(Folder.Split('\\').Last(), Directory.GetCreationTime(Folder), Folder)).ToList();
        }
    }
}
