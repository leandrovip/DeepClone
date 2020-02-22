using System;

namespace DeepCopyTestClasses
{
    [Serializable]
    public class SimpleClass : ISimpleClass
    {
        public string PropertyPublic { get; set; }

        protected bool PropertyProtected { get; set; }

        private int PropertyPrivate { get; set; }

        public int FieldPublic;

        private string FieldPrivate;

        public readonly string ReadOnlyField;

        public SimpleClass(int propertyPrivate, bool propertyProtected, string fieldPrivate)
        {
            PropertyPrivate = propertyPrivate;
            PropertyProtected = propertyProtected;
            FieldPrivate = fieldPrivate + "_" + typeof(SimpleClass);
            ReadOnlyField = FieldPrivate + "_readonly";
        }

        public static SimpleClass CreateForTests(int seed)
        {
            return new SimpleClass(seed, seed % 2 == 1, "seed_" + seed)
                {
                    FieldPublic = -seed,
                    PropertyPublic = "seed_" + seed + "_public"
                };
        }

        public int GetPrivateProperty()
        {
            return PropertyPrivate;
        }

        public string GetPrivateField()
        {
            return FieldPrivate;
        }
    }
}
