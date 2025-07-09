using Xunit;
using task09;
using task07;
namespace task09tests
{
    [DisplayName("Пример класса")]
    [Version(1, 0)]
    public class Test
    {
        public int Number;
        public Test(int number)
        {
            Number = number;
        }
        [DisplayName("Тестовый метод")]
        public static void Method(int x, int y) { }
    }
    public class Task09tests
    {

        [Fact]
        public void MethodAndParametrInfo_ReturnCorrectMethodAndParametr()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var type = typeof(Test);
            Program.MethodAndParametrInfo(type);

            Assert.Contains("Список методов:", output.ToString());
            Assert.Contains("Метод: Method", output.ToString());
            Assert.Contains("Параметры метода:", output.ToString());
            Assert.Contains("Имя: x. Тип: Int32", output.ToString());
            Assert.Contains("Имя: y. Тип: Int32", output.ToString());
        }

        [Fact]
        public void AttributeInfo_ReturnCorrectAttribute()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var type = typeof(Test);
            Program.AttributeInfo(type);

            Assert.Contains("Список атрибутов:", output.ToString());
            Assert.Contains("DisplayNameAttribute", output.ToString());
            Assert.Contains("VersionAttribute", output.ToString());
        }

        [Fact]
        public void ConstructorAndParametrInfo_ReturnCorrectConstructorAndParam()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var type = typeof(Test);
            Program.ConstructorAndParametrInfo(type);

            Assert.Contains("Список конструкторов:", output.ToString());
            Assert.Contains("Конструктор: .ctor", output.ToString());
            Assert.Contains("Параметры конструктора:", output.ToString());
            Assert.Contains("Имя: number. Тип: Int32", output.ToString());
        }

        [Fact]
        public void AllTypeInfo_ReturnCorrectInformation()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var type = typeof(Test);
            Program.AllTypeInfo(type);

            Assert.Contains("Список методов:", output.ToString());
            Assert.Contains("Метод: Method", output.ToString());
            Assert.Contains("Параметры метода:", output.ToString());
            Assert.Contains("Имя: x. Тип: Int32", output.ToString());
            Assert.Contains("Имя: y. Тип: Int32", output.ToString());
            Assert.Contains("Список атрибутов:", output.ToString());
            Assert.Contains("DisplayNameAttribute", output.ToString());
            Assert.Contains("VersionAttribute", output.ToString());
            Assert.Contains("Список конструкторов:", output.ToString());
            Assert.Contains("Конструктор: .ctor", output.ToString());
            Assert.Contains("Параметры конструктора:", output.ToString());
            Assert.Contains("Имя: number. Тип: Int32", output.ToString());
        }

        [Fact]
        public void Main_ReturnCorrectAllClass()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            string currentDir = Directory.GetCurrentDirectory();
            string dir = Path.GetFullPath(Path.Combine(currentDir, ".."));
            string path = Directory.GetFiles(dir, "task07.dll", SearchOption.AllDirectories)[0];

            string[] args = new string[] { path };
            Program.Main(args);
            Assert.Contains("Класс: DisplayNameAttribute", output.ToString());
            Assert.Contains("Класс: VersionAttribute", output.ToString());
            Assert.Contains("Класс: SampleClass", output.ToString());
            Assert.Contains("Класс: ReflectionHelper", output.ToString());
        }

        [Fact]
        public void Main_NoExistPath()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            string[] args = new string[0];
            Program.Main(args);

            Assert.Contains("Путь не указан", output.ToString());
        }
    }
}    
