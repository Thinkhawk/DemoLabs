namespace Demo_LiskovSubstitutionPrinciple.Demo02;


/// <remarks>
///     BENEFITS of Liskov Substitution Principle
///     - Improved code quality
///     - Increased code reusability
///     - Simplified code maintenance
///     - Promotes modularity
///     - Easier refactoring and extensions
///     - Consistent user experience
///     - Streamlined testing and debugging processes
///     - Avoids design flaws
/// </remarks>

internal interface IBird
{
    void Fly();
}

internal abstract class Bird
{

}

internal abstract class BirdWithFlight : Bird, IBird
{
    public virtual void Fly()
    {
        Console.WriteLine("{0} is flying", this.GetType().Name.ToUpper());
    }
}

internal abstract class FlightlessBird : Bird
{
}

internal class Crow : BirdWithFlight
{
    public override void Fly()
    {
        base.Fly();
    }
}

internal class Ostrich : FlightlessBird
{

}
