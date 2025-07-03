using System.Reflection;

namespace task07;

public class DisplayNameAttribute : Attribute
{
    public string DisplayName { get; }
    public DisplayNameAttribute(string displayName)
    {
        DisplayName = displayName;
    }
}

public class VersionAttribute : Attribute
{
    public int Major;
    public int Minor;
    public VersionAttribute(int major, int minor)
    {
        Major = major;
        Minor = minor;
    }
}

[DisplayName("Пример класса")]
[Version(1, 0)]
public class SampleClass
{
    [DisplayName("Тестовый метод")]
    public void TestMethod() { }
    [DisplayName("Числовое свойство")]
    public int Number { get; set; }
}
public static class ReflectionHelper
{
    public static void PrintTypeInfo(Type type)
    {
        var displayName = type.GetCustomAttribute<DisplayNameAttribute>();
        if (displayName != null)
        {
            Console.WriteLine(displayName.DisplayName);
        }
        var version = type.GetCustomAttribute<VersionAttribute>();
        if (version != null)
        {
            Console.WriteLine($"({version.Major}.{version.Minor})");
        }
        var methods = type.GetMethods();
        foreach (var method in methods)
        {
            var methodDisplayName = method.GetCustomAttribute<DisplayNameAttribute>();
            if (methodDisplayName != null)
            {
                Console.WriteLine(methodDisplayName.DisplayName);
            }
        }
        var properties = type.GetProperties();
        foreach (var property in properties)
        {
            var propertyDisplayName = property.GetCustomAttribute<DisplayNameAttribute>();
            if (propertyDisplayName != null)
            {
                Console.WriteLine(propertyDisplayName.DisplayName);
            }
        }
    }
}
