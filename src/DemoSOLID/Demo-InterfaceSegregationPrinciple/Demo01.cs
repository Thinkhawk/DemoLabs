namespace Demo_InterfaceSegregationPrinciple.Demo01;


/// <remarks>
///     BENEFITS of Interface Segregation Principle
///     - Improved maintainability
///     - Enhanced flexibility
///     - Reduced coupling
///     - Enhanced readability
///     - Improved reusability
///     - Easier testing
///     - Scalability
/// </remarks>

internal interface IBird
{
    void Eat();
}

internal interface IBirdWithFlight : IBird
{
    void Eat();

    void Fly();
}

internal abstract class BirdWithFlight : IBirdWithFlight
{
    public virtual void Eat()
    {
        Console.WriteLine("{0} is eating", this.GetType().Name.ToUpper());
    }

    public virtual void Fly()
    {
        Console.WriteLine("{0} is flying", this.GetType().Name.ToUpper());
    }
}

internal abstract class FlightlessBird : IBird
{
    public virtual void Eat()
    {
        Console.WriteLine("{0} is eating", this.GetType().Name.ToUpper());
    }
}

internal class Crow : BirdWithFlight
{
    public override void Eat()
    {
        base.Fly();
    }

    public override void Fly()
    {
        base.Fly();
    }
}

internal class Ostrich : FlightlessBird
{

}
