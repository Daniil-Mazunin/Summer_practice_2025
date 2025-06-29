namespace task04;

public interface ISpaceship
{
    void MoveForward();      
    void Rotate(int angle);  
    void Fire();           
    int Speed { get; }       
    int FirePower { get; }  
}

public class Cruiser : ISpaceship
{
    public int Speed { get; }
    public int FirePower { get; }
    public double X;
    public double Y;
    public double Angle;
    public int Damage;
    public Cruiser()
    {
        Speed = 50;
        FirePower = 100;
        X = 0;
        Y = 0;
        Angle = 0;
        Damage = 0;
    }
    public void MoveForward()
    {
        double radian = Angle * (Math.PI / 180);
        X += Speed * Math.Cos(radian);
        Y += Speed * Math.Sin(radian); 
    }
    public void Rotate(int angle)
    {
        Angle = (Angle + angle) % 360;
    }
    public void Fire()
    {
        Damage += 100;
    }
}

public class Fighter : ISpaceship
{
    public int Speed { get; }
    public int FirePower { get; }
    public double X;
    public double Y;
    public double Angle;
    public int Damage;
    public Fighter()
    {
        Speed = 100;
        FirePower = 50;
        X = 0;
        Y = 0;
        Angle = 0;
        Damage = 0;
    }
    public void MoveForward()
    {
        double radian = Angle * (Math.PI / 180);
        X += Speed * Math.Cos(radian);
        Y += Speed * Math.Sin(radian); 
    }
    public void Rotate(int angle)
    {
        Angle = (Angle + angle) % 360;
    }
    public void Fire()
    {
        Damage += 50;
    }
}
