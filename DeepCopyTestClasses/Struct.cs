using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeepCopyTestClasses
{
    [Serializable]
    public struct Struct
    {
        private int Item1;

        public SimpleClass Item23;

        public SimpleClass Item32;

        public readonly SimpleClass Item4;
        
        public Struct(int item1, SimpleClass item23, SimpleClass item4)
        {
            Item1 = item1;
            Item23 = item23;
            Item32 = item23;
            Item4 = item4;
        }

        public int GetItem1()
        {
            return Item1;
        }

        public void IncrementItem1()
        {
            Item1++;
        }

        public void DecrementItem1()
        {
            Item1--;
        }
    }
}
