using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_Interface03.Demo01
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

    // IMPLICIT IMPLEMENTATION of Interface signature(s)
    class Demo : IDemo, IAnotherDemo
    {
        public void m() { }

        public void n() { }
    }
}
