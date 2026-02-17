namespace Demo_IOC;

// Inversion of Control (IoC) using Interface
interface IStepActivity
{
    void Step1();
    void Step2();
    void Step3();
}

internal class Process3
{
    internal void DoSomething(IStepActivity activity)
    {
        activity?.Step1();
        activity?.Step2();
        activity?.Step3();
    }
}

internal class SilverVersionProcess : IStepActivity
{
    public void Step1()
    {
        Console.WriteLine("-- silver version - step 1");
    }

    public void Step2()
    {
        Console.WriteLine("-- silver version - step 2");
    }

    public void Step3()
    {
        Console.WriteLine("-- silver version - step 3");
    }
}

internal class GoldVersionProcess : IStepActivity
{
    public void Step1()
    {
        Console.WriteLine("-- gold version - step 1");
    }

    public void Step2()
    {
        Console.WriteLine("-- gold version - step 2");
    }

    public void Step3()
    {
        Console.WriteLine("-- gold version - step 3");
    }
}
