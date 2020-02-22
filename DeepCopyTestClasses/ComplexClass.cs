using System;
using System.Collections.Generic;

namespace DeepCopyTestClasses
{
    [Serializable]
    public class ComplexClass : ModerateClass
    {
        public ComplexClass ThisComplexClass { get; set; }

        public Tuple<ComplexClass, ModerateClass, SimpleClass> TupleOfThis { get; protected set; }

        public Dictionary<string, SimpleClass> SampleDictionary;

        public DerivedDictionary<string, ISimpleClass> ISampleDictionary { get; private set; }

        public ISimpleClass[,,] ISimpleMultiDimArray;

        public SimpleClass[][][] SimpleMultiDimArray;

        public Struct[] StructArray;

        public delegate void DelegateType();

        public Delegate JustDelegate;

        public readonly Delegate ReadonlyDelegate;

        public event DelegateType JustEvent;

        public bool IsJustEventNull { get { return JustEvent == null; } }

        public ComplexClass()
            : base(propertyPrivate: - 1, propertyProtected: true, fieldPrivate: "fieldPrivate_" + typeof (ComplexClass))
        {
            ThisComplexClass = this;

            TupleOfThis = new Tuple<ComplexClass, ModerateClass, SimpleClass>(this, this, this);

            SampleDictionary = new DerivedDictionary<string, SimpleClass>();
            ISampleDictionary = new DerivedDictionary<string, ISimpleClass>();

            JustDelegate = new DelegateType(() => CreateForTests());
            ReadonlyDelegate = new DelegateType(() => CreateForTests());
            JustEvent += new DelegateType(() => CreateForTests());
        }

        public static ComplexClass CreateForTests()
        {
            var complexClass = new ComplexClass();

            var dict1 = new DerivedDictionary<string, SimpleClass>();
            complexClass.SampleDictionary = dict1;

            dict1[typeof(ComplexClass).ToString()] = new ComplexClass();
            dict1[typeof(ModerateClass).ToString()] = new ModerateClass(1, true, "madeInComplexClass");
            dict1[typeof(SimpleClass).ToString()] = new SimpleClass(2, false, "madeInComplexClass");

            var dict2 = complexClass.ISampleDictionary;

            dict2[typeof (ComplexClass).ToString()] = dict1[typeof (ComplexClass).ToString()];
            dict2[typeof (ModerateClass).ToString()] = dict1[typeof (ModerateClass).ToString()];
            dict2[typeof(SimpleClass).ToString()] = new SimpleClass(2, false, "madeInComplexClass");

            var array1 = new ISimpleClass[2, 1, 1];
            array1[0,0,0] = new SimpleClass(4, false, "madeForMultiDimArray");
            array1[1,0,0] = new ComplexClass();
            complexClass.ISimpleMultiDimArray = array1;

            var array2 = new SimpleClass[2][][];
            array2[1] = new SimpleClass[2][];
            array2[1][1] = new SimpleClass[2];
            array2[1][1][1] = (SimpleClass)array1[0, 0, 0];
            complexClass.SimpleMultiDimArray = array2;
            
            complexClass.StructArray = new Struct[2];
            complexClass.StructArray[0] = new Struct(1, complexClass, SimpleClass.CreateForTests(5));
            complexClass.StructArray[1] = new Struct(3, new SimpleClass(3,false,"inStruct"), SimpleClass.CreateForTests(6));

            return complexClass;
        }
    }
}
