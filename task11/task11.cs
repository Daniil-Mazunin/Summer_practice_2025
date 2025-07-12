using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace task11;

public interface ICalculator
{
    public int Add(int a, int b);
    public int Minus(int a, int b);
    public int Mul(int a, int b);
    public int Div(int a, int b);
}
public class GeneratorCalculatorClass
{
    public static ICalculator GeneratorCalculator()
    {
        string classCalculator = @"public class Calculator : task11.ICalculator
{
    public int Add(int a, int b) => a + b;
    public int Minus(int a, int b) => a - b;
    public int Mul(int a, int b) => a * b;
    public int Div(int a, int b) => a / b;
}";
        var syntaxTree = CSharpSyntaxTree.ParseText(classCalculator);  

        var references = new MetadataReference[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(ICalculator).Assembly.Location)
        };

        var option = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
        var compilation = CSharpCompilation.Create("Calculator", [syntaxTree], references, option);

        var memoryStream = new MemoryStream();
        var emitResult = compilation.Emit(memoryStream);
        if (!emitResult.Success)
        {
            throw new Exception("Ошибка, не удалось скомпилировать");
        }

        memoryStream.Seek(0, SeekOrigin.Begin);
        Assembly assembly = Assembly.Load(memoryStream.ToArray());
        var calculatorType = assembly.GetType("Calculator");
        if (calculatorType == null)
        {
            throw new Exception("Ошибка, класс Calculator не найден");
        }

        ICalculator calculatorInstance = (ICalculator)Activator.CreateInstance(calculatorType)!;
        if (calculatorInstance == null)
        {
            throw new Exception("Ошибка, класс Calculator не удалось создать");
        }

        return calculatorInstance;
    }
}
