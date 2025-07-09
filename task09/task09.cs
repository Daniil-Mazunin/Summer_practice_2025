using System.Reflection;
namespace task09;

public class Program
{
    public static void MethodAndParametrInfo(Type type)
    {
        var methods = type.GetMethods();
        Console.WriteLine("Список методов:");
        foreach (var method in methods)
        {
            if (method.IsSpecialName)
            {
                continue;
            }
            Console.WriteLine($"Метод: {method.Name}");
            var parametrs = method.GetParameters();
            Console.WriteLine("Параметры метода:");
            if (parametrs.Any())
            {
                foreach (var parametr in parametrs)
                {
                    Console.WriteLine($"Имя: {parametr.Name}. Тип: {parametr.ParameterType.Name}");
                }
            }
            else
            {
                Console.WriteLine("Параметров нет");
            }
        }
    }
    public static void AttributeInfo(Type type)
    {
        var attributes = type.GetCustomAttributes();
        Console.WriteLine("Список атрибутов:");
        foreach (var attribute in attributes)
        {
            Console.WriteLine(attribute.GetType().Name);
        }
    }
    public static void ConstructorAndParametrInfo(Type type)
    {
        var constructors = type.GetConstructors();
        Console.WriteLine("Список конструкторов:");
        foreach (var constructor in constructors)
        {
            Console.WriteLine($"Конструктор: {constructor.Name}");
            var parametrs = constructor.GetParameters();
            Console.WriteLine("Параметры конструктора:");
            if (parametrs.Any())
            {
                foreach (var parametr in parametrs)
                {
                    Console.WriteLine($"Имя: {parametr.Name}. Тип: {parametr.ParameterType.Name}");
                }
            }
            else
            {
                Console.WriteLine("Параметров нет");
            }
        }
    }
    public static void AllTypeInfo(Type type)
    {
        Console.WriteLine($"Класс: {type.Name}");
        MethodAndParametrInfo(type);
        AttributeInfo(type);
        ConstructorAndParametrInfo(type);
    }
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Путь не указан");
            return;
        }
        string path = args[0];
        Assembly assembly = Assembly.LoadFrom(path);
        var types = assembly.GetTypes();
        foreach (var type in types)
        {
            AllTypeInfo(type);
        }
    }
}
