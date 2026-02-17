namespace Demo_IOC;


internal class Process2a
{

    // Inversion of Control (IoC) using Delegate
    // the invoker of DoSomething controls which code to call (invoke)
    internal void DoSomething(StepHandler step2Handler)
    {
        this.Step1();

        //if (step2Handler is not null)         // check if subscribed
        //{
        //    step2Handler();                   // invoke the method pointed to by the delegate
        //}

        step2Handler?.Invoke();

        this.Step3();
    }

    private void Step1()
    {
        Console.WriteLine("-- step 1");
    }

    private void Step3()
    {
        Console.WriteLine("-- step 3");
    }
}
