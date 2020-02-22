using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeepCopyTestClasses
{
    [Serializable]
    public struct DeeperStruct
    {
        [Serializable]
        private struct SubStruct
        {
            public int Item1;

            public SimpleClass Item2;
        }

        private SubStruct SubStructItem;
        
        public DeeperStruct(int item1, SimpleClass item2)
        {
            SubStructItem = new SubStruct() { Item1 = item1, Item2 = item2 };
        }

        public int GetItem1()
        {
            return SubStructItem.Item1;
        }

        public void IncrementItem1()
        {
            SubStructItem.Item1++;
        }

        public void DecrementItem1()
        {
            SubStructItem.Item1--;
        }

        public SimpleClass GetItem2()
        {
            return SubStructItem.Item2;
        }
    }
}
