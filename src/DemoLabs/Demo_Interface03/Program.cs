namespace Demo_Interface03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Demo02.Demo objDemo = new Demo02.Demo();
            objDemo.m();
            objDemo.n();
            Console.WriteLine();

            // OVERLOAD:  Compile-time BINDING, Compile-time RESOLUTION
            // OVERRIDE:  Run-time BINDING, Run-time RESOLUTION
            // INTERFACE: Compile-time BINDING, Run-time RESOLUTION

            Demo02.IDemo objDemo2 = new Demo02.Demo();
            objDemo2.m();
            //objDemo2.n();            // COMPILER ERROR
            ((Demo02.IAnotherDemo)objDemo2).n();
            ((System.IDisposable)objDemo2).Dispose();       // RUNTIME ERROR
            Console.WriteLine();

            Demo02.IAnotherDemo objDemo3 = new Demo02.Demo();
            objDemo3.m();
            objDemo3.n();
            Console.WriteLine();


        }
    }
}
