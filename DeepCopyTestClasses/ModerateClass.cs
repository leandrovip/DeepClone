using System;

namespace DeepCopyTestClasses
{
    [Serializable]
    public class ModerateClass : SimpleClass
    {
        public string PropertyPublic2 { get; set; }

        protected bool PropertyProtected2 { get; set; }

        public int FieldPublic2;

        private int PropertyPrivate { get; set; }

        private string FieldPrivate;

        public Struct StructField;

        public DeeperStruct DeeperStructField;
        
        public GenericClass<SimpleClass> GenericClassField; 

        public SimpleClass SimpleClassProperty { get; set; }

        public SimpleClass ReadonlySimpleClassField;

        public SimpleClass[] SimpleClassArray { get; set; }

        public Object ObjectTextProperty { get; set; }

        public ModerateClass(int propertyPrivate, bool propertyProtected, string fieldPrivate)
            : base(propertyPrivate, propertyProtected, fieldPrivate)
        {
            PropertyPrivate = propertyPrivate + 1;
            FieldPrivate = fieldPrivate + "_" + typeof(ModerateClass);
            ObjectTextProperty = "Test";
        }

        public static ModerateClass CreateForTests(int seed)
        {
            var moderateClass = new ModerateClass(seed, seed % 2 == 1, "seed_" + seed);

            moderateClass.FieldPublic = seed;
            moderateClass.FieldPublic2 = seed + 1;

            moderateClass.StructField = new Struct(seed, moderateClass, SimpleClass.CreateForTests(seed));
            moderateClass.DeeperStructField = new DeeperStruct(seed, SimpleClass.CreateForTests(seed));

            moderateClass.GenericClassField = new GenericClass<SimpleClass>(moderateClass, SimpleClass.CreateForTests(seed));

            var seedSimple = seed + 1000;

            moderateClass.SimpleClassProperty = new SimpleClass(seedSimple, seed % 2 == 1, "seed_" + seedSimple);

            moderateClass.ReadonlySimpleClassField = new SimpleClass(seedSimple + 1, seed % 2 == 1, "seed_" + (seedSimple + 1));

            moderateClass.SimpleClassArray = new SimpleClass[10];

            for (int i = 1; i <= 10; i++)
            {
                moderateClass.SimpleClassArray[i - 1] = new SimpleClass(seedSimple + i, seed % 2 == 1, "seed_" + (seedSimple + i));
            }

            return moderateClass;
        }

        public int GetPrivateProperty2()
        {
            return PropertyPrivate;
        }

        public string GetPrivateField2()
        {
            return FieldPrivate;
        }
    }
}
