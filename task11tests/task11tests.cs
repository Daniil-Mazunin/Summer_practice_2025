using Xunit;
using task11;

public class CalculatorTests
{
    [Fact]
    public void CalculatorAdd_ReturnCorrectSum()
    {
        ICalculator calculator = GeneratorCalculatorClass.GeneratorCalculator();

        int result = calculator.Add(1, 1);

        Assert.Equal(2, result);
    }

    [Fact]
    public void CalculatorMinus_ReturnCorrectDifference()
    {
        ICalculator calculator = GeneratorCalculatorClass.GeneratorCalculator();

        int result = calculator.Minus(1, 5);

        Assert.Equal(-4, result);
    }

    [Fact]
    public void CalculatorMul_ReturnCorrectValueMul()
    {
        ICalculator calculator = GeneratorCalculatorClass.GeneratorCalculator();

        int result = calculator.Mul(5, 5);

        Assert.Equal(25, result);
    }

    [Fact]
    public void CalculatorDiv_ReturnCorrectValueDiv()
    {
        ICalculator calculator = GeneratorCalculatorClass.GeneratorCalculator();

        int result = calculator.Div(20, 4);

        Assert.Equal(5, result);
    }
}
