using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DeepCopyTestClasses
{
    [Serializable]
    public class DerivedDictionary<T1, T2> : Dictionary<T1, T2>
    {
        public DerivedDictionary()
        { }

        public DerivedDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
