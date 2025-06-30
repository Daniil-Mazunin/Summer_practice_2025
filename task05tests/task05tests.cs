using Xunit;
using task05;

public class TestClass
{
    public int PublicField;
    private string _privateField = string.Empty;
    public int Property { get; set; }

    public void Method() { }
    public void Parametr(int x) { }
}

[Serializable]
public class AttributedClass { }

public class ClassAnalyzerTests
{
    [Fact]
    public void GetPublicMethods_ReturnsCorrectMethods()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var methods = analyzer.GetPublicMethods();

        Assert.Contains("Method", methods);
    }

    [Fact]
    public void GetAllFields_IncludesPrivateFields()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var fields = analyzer.GetAllFields();

        Assert.Contains("_privateField", fields);
    }

    [Fact]
    public void GetMethodParams_ReturnCorrectParams()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var param = analyzer.GetMethodParams("Parametr");

        Assert.Contains("x", param);
    }

    [Fact]
    public void GetProperties_ReturnCorrectProperties()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var property = analyzer.GetProperties();

        Assert.Contains("Property", property);
    }

    [Fact]
    public void HasAttribute_ReturnPresenceAttribute()
    {
        var analyzer = new ClassAnalyzer(typeof(AttributedClass));
        var attribute = analyzer.HasAttribute<SerializableAttribute>();

        Assert.True(attribute);
    }
}
