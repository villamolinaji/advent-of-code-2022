using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace _07
{
    public class Folder
    {
        public List<Folder> Folders { get; set; }
        public List<FileClass> Files { get; set; }
        public string FolderName { get; set; }
        public int TotalSize { get; set; }
        public int FolderId { get; set; }

        public bool IsChosen { get; set; }

        public static int CalculateSize(Folder folder)
        {
            foreach(var foderChild in folder.Folders)
            {
                folder.TotalSize = folder.TotalSize + Folder.CalculateSize(foderChild);
            }
            folder.TotalSize = folder.TotalSize + folder.Files.Sum(f => f.Size);

            return folder.TotalSize;
        }

        public static void MarkChosen(Folder folder)
        {
            foreach (var foderChild in folder.Folders)
            {
                Folder.MarkChosen(foderChild);
            }

            if (folder.TotalSize < 100000)
            {
                folder.IsChosen = true;
            }                        
        }

        public static int CalculateChosen(Folder folder)
        {
            int result = 0;
            foreach (var foderChild in folder.Folders)
            {
                result = result + Folder.CalculateChosen(foderChild);
            }
            if (folder.IsChosen)
            {
                result = result + folder.TotalSize;
            }

            return result;
        }

        public static int FolderToDelete(Folder folder, int needed, int currentSize)
        {            
            int result = currentSize;
            if (folder.TotalSize >= needed && folder.TotalSize <= currentSize)
            {
                result = folder.TotalSize;
                foreach (var folderChild in folder.Folders)
                {
                    if (folderChild.TotalSize >= needed && folderChild.TotalSize <= result)
                    {
                        result = Folder.FolderToDelete(folderChild, needed, result);
                    }
                }
            }

            return result;
        }
    }
}
