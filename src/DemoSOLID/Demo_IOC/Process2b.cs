namespace Demo_IOC;

internal class Process2b
{
    internal event StepHandler? Step2Event;


    // Inversion of Control (IoC) using Event mapped to the Delegate
    // the invoker of DoSomething controls when to invoke the method pointed to by the delegate
    internal void DoSomethingUsingDelegate()
    {
        this.Step1();

        Step2Event?.Invoke();           // check if event is subscribed, and raise the event.

        this.Step3();
    }


    // Inversion of Control (IoC) using Event mapped to the Delegate
    // the invoker of DoSomething controls when to invoke the method pointed to by the delegate
    internal void DoSomethingInParallelUsingThread()
    {
		bool isEventSubscribed = Step2Event is not null;
        System.Threading.Thread? t = null;

        this.Step1();

		if( isEventSubscribed )
		{
            // raise the event in a different THREAD.
            t = new Thread(() => Step2Event?.Invoke());
            t.Start();
		}

        this.Step3();

        if ( isEventSubscribed)
        {
			// join the branch thread (running the event handler) back the branch thread
        	t?.Join();
		}
    }



    internal void DoSomethingInParallelUsingTask()
    {
        bool isEventSubscribed = Step2Event is not null;

        Task? t = null;

        this.Step1();

        if (isEventSubscribed)
        {
            // raise the event using a TASK object to run on a different thread.
            t = Task.Run(() => Step2Event?.Invoke());
        }

        this.Step3();

        if (isEventSubscribed)
        {
            // wait for the task to complete
            t?.Wait();
        }
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
