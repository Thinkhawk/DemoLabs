namespace Demo_DI;

interface IStepActivity
{
    void Step1();
    void Step2();
    void Step3();

}

internal class Process4
{
    private IStepActivity _activity;

    // The Dependency is Injected during construction of the object
    internal Process4(IStepActivity activity)
    {
        _activity = activity;
    }

    internal void DoSomething()
    {
        _activity.Step1();
        _activity.Step2();
        _activity.Step3();
    }

    internal void DoSomethingElse()
    {
        _activity.Step1();
    }


    internal void DoDifferent()
    {
        _activity.Step1();
        _activity.Step3();
    }
}

internal class SilverVerisionProcess : IStepActivity
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
