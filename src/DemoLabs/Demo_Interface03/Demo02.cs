namespace Demo_Interface03.Demo02
{
    interface IDemo
    {
        void m();
    }

    interface IAnotherDemo
    {
        void m();
        void n();
    }

    class Demo : IDemo, IAnotherDemo
    {
        // EXPLICIT IMPLEMENTATION of Interface signature(s)
        void IDemo.m() 
        {
            Console.WriteLine("IDemo.m() called EXPLICITLY for Demo object");
        }

        // EXPLICIT IMPLEMENTATION of Interface signature(s)
        void IAnotherDemo.m() 
        {
            Console.WriteLine("IAnotherDemo.m() called EXPLICITLY for Demo object");
        }

        public void m() 
        {
            Console.WriteLine("m() called for the Demo object");
        }

        // IMPLICIT IMPLEMENTATION of IAnotherDemo.n()
        public void n() 
        {
            Console.WriteLine("IAnotherDemo.n() called IMPLICITLY");
        }
    }
}
