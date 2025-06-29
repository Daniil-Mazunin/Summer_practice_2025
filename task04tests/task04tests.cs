using Xunit;
using task04;

public class SpaceshipTests
{
    [Fact]
    public void Cruiser_ShouldHaveCorrectStats()
    {
        ISpaceship cruiser = new Cruiser();
        Assert.Equal(50, cruiser.Speed);
        Assert.Equal(100, cruiser.FirePower);
    }

    [Fact]
    public void Fighter_ShouldBeFasterThanCruiser()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(fighter.Speed > cruiser.Speed);
    }

    [Fact]
    public void Cruiser_ShouldBeMorePowerfulThanFighter()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(fighter.FirePower < cruiser.FirePower);
    }

    [Fact]
    public void Cruiser_ShouldCorrectlyMovesInLine()
    {
        var cruiser = new Cruiser();
        cruiser.MoveForward();
        Assert.Equal(50, cruiser.X);
        Assert.Equal(0, cruiser.Y);
    }

    [Fact]
    public void Fighter_ShouldCorrectlyMovesInLine()
    {
        var fighter = new Fighter();
        fighter.MoveForward();
        Assert.Equal(100, fighter.X);
        Assert.Equal(0, fighter.Y);
    }

    [Fact]
    public void Cruiser_ShouldCorrectlyRotate()
    {
        var cruiser = new Cruiser();
        cruiser.Rotate(60);
        Assert.Equal(60, cruiser.Angle);
    }

    [Fact]
    public void Fighter_ShouldCorrectlyRotate()
    {
        var fighter = new Fighter();
        fighter.Rotate(60);
        Assert.Equal(60, fighter.Angle);
    }

    [Fact]
    public void Cruiser_ShouldCorrectlyFire()
    {
        var cruiser = new Cruiser();
        cruiser.Fire();
        Assert.Equal(100, cruiser.Damage);
    }

    [Fact]
    public void Fighter_ShouldCorrectlyFire()
    {
        var fighter = new Fighter();
        fighter.Fire();
        Assert.Equal(50, fighter.Damage);
    }
}
