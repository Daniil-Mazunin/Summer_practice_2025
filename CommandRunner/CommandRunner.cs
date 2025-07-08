using System.Reflection;
using CommandLib;

namespace CommandRunner;

public class Program
{
    public static void Main()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);
        File.WriteAllText(Path.Combine(testDir, "file1.txt"), "Text");
        File.WriteAllText(Path.Combine(testDir, "file2.log"), "Log");
        string mask = "*.txt";
        object[] objects = [testDir, mask];

        Assembly assembly = Assembly.LoadFrom("FileSystemCommands.dll");
        var sizeCommandType = assembly.GetType("FileSystemCommands.DirectorySizeCommand");
        var sizeCommand = (ICommand)Activator.CreateInstance(sizeCommandType, objects[0]);
        sizeCommand.Execute();
        var size = (long)sizeCommandType.GetProperty("Size").GetValue(sizeCommand);
        Console.WriteLine(size);

        var findCommandType = assembly.GetType("FileSystemCommands.FindFilesCommand");
        var findCommand = (ICommand)Activator.CreateInstance(findCommandType, objects);
        findCommand.Execute();
        var files = (string[])findCommandType.GetProperty("Files").GetValue(findCommand);
        if (files != null)
        {
            foreach(var file in files)
            {
                Console.WriteLine(file);
            }
        }
        Directory.Delete(testDir, true);
    }
}
