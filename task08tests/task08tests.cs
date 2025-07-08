using Xunit;
using CommandRunner;
using FileSystemCommands;

public class FileSystemCommandsTests
{
    [Fact]
    public void DirectorySizeCommand_ShouldCalculateSize()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);
        File.WriteAllText(Path.Combine(testDir, "test1.txt"), "Hello");
        File.WriteAllText(Path.Combine(testDir, "test2.txt"), "World");

        var command = new DirectorySizeCommand(testDir);
        command.Execute(); // Проверяем, что не возникает исключений

        Assert.Equal(10, command.Size);

        Directory.Delete(testDir, true);
    }

    [Fact]
    public void FindFilesCommand_ShouldFindMatchingFiles()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);
        File.WriteAllText(Path.Combine(testDir, "file1.txt"), "Text");
        File.WriteAllText(Path.Combine(testDir, "file2.log"), "Log");

        var command = new FindFilesCommand(testDir, "*.txt");
        command.Execute(); // Должен найти 1 файл

        Assert.Single(command.Files);
        Assert.Contains("file1.txt", command.Files[0]);

        Directory.Delete(testDir, true);
    }

    [Fact]
    public void DirectorySizeCommand_DirectoryIsEmpty()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);

        var command = new DirectorySizeCommand(testDir);
        command.Execute(); 

        Assert.Equal(0, command.Size);

        Directory.Delete(testDir, true);
    }

    [Fact]
    public void CorrectWorkCommandApp()
    {
        var output = new StringWriter();
        Console.SetOut(output);
            
        Program.Main();

        string[] result = output.ToString().Split(new[] { '\n', '\r', ' ' }, StringSplitOptions.RemoveEmptyEntries);

        Assert.Equal(2, result.Length);
        Assert.Equal("7", result[0]);
        Assert.Contains("file1.txt", result[1]);
    }
}
