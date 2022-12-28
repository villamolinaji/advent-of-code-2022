using _07;

string[] lines = File.ReadAllLines("input.txt");

int result = 0;
var folder = new Folder() { Files = new List<FileClass>(), Folders = new List<Folder>(), FolderName = "/", TotalSize = 0, FolderId = 1 };
var currentFolder = new Folder() { Files = new List<FileClass>(), Folders = new List<Folder>(), FolderName = "/", TotalSize = 0 };
folder.Folders.Add(currentFolder);
var prevFolder = new Folder() { Files = new List<FileClass>(), Folders = new List<Folder>(), FolderName = "/", TotalSize = 0 };

int folderIdCount = 1;
int currentFolderId = 1; ;
int prevFolderId = 1;
var prevFolders = new List<Folder>();
prevFolders.Add(prevFolder);
foreach (string line in lines)
{
    if (line.StartsWith("$ cd /"))
    {
        
    }

    else if (line.StartsWith("$ cd .."))
    {
        currentFolder = prevFolder;
        currentFolderId = prevFolderId;
        prevFolders.Remove(prevFolder);
        prevFolder = prevFolders.Last();
    }

    else if (line.StartsWith("$ cd"))
    {        
        prevFolderId = currentFolderId;
        currentFolderId = currentFolder.Folders.First(f => f.FolderName == line.Substring(5)).FolderId;
        prevFolder = currentFolder;
        prevFolders.Add(prevFolder);
        currentFolder = currentFolder.Folders.First(f => f.FolderName == line.Substring(5));

    }

    else if (line.StartsWith("dir"))
    {
        folderIdCount++;        
        currentFolder.Folders.Add(new Folder() { Files = new List<FileClass>(), Folders = new List<Folder>(), FolderName = line.Substring(4), TotalSize = 0, FolderId = folderIdCount });
    }

    else if (line.StartsWith("$ ls"))
    {

    }

    else
    {
        currentFolder.Files.Add(new FileClass() { Size = int.Parse(line.Substring(0, line.IndexOf(" "))), FileName = line.Substring(line.IndexOf(" ") + 1) });      
    }
}

Folder.CalculateSize(folder);
Folder.MarkChosen(folder);
result = Folder.CalculateChosen(folder);

Console.WriteLine(result.ToString());

int totalSpace = 70000000 ;
int currentSpace = folder.TotalSize;
int freeSpace = totalSpace - currentSpace;
int needed = 30000000 - freeSpace;

int result2 = Folder.FolderToDelete(folder, needed, currentSpace);
Console.WriteLine(result2.ToString());
