using CommandLib;

namespace FileSystemCommands;

public class DirectorySizeCommand : ICommand
{
    private string DirectoryName;
    public long Size { get; private set; }
    public DirectorySizeCommand(string directoryName)
    {
        DirectoryName = directoryName;
        Size = 0;
    }
    public void Execute()
    {
        if (Directory.Exists(DirectoryName))
        {
            foreach (var file in Directory.GetFiles(DirectoryName, "*", SearchOption.AllDirectories))
            {
                Size += new FileInfo(file).Length;
            }
        }
    }
}

public class FindFilesCommand : ICommand
{
    private string DirectoryName;
    private string Mask;
    public string[] Files { get; set; } = new string[0];
    public FindFilesCommand(string directoryName, string mask)
    {
        DirectoryName = directoryName;
        Mask = mask;
    }
    public void Execute()
    {
        if (Directory.Exists(DirectoryName))
        {
            Files = Directory.GetFiles(DirectoryName, Mask);
        }
    }
}
