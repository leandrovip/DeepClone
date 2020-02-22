using System;
using System.Linq;
using DeepCopyTestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeepCopyTests
{
    [TestClass]
    public class ComplexClassTests
    {
        [TestMethod]
        public void Test1()
        {
            var c = ComplexClass.CreateForTests();
            var cCopy = (ComplexClass)CopyFunctionSelection.CopyMethod(c);

            // test that the copy is a different instance but with equal content
            Assert_AreEqualButNotSame(c, cCopy);

            // test that the same subobjects should remain the same in a copy (we put same objects to different dictionaries)
            Assert.AreSame(cCopy.SampleDictionary[typeof(ComplexClass).ToString()],
                                                 cCopy.ISampleDictionary[typeof(ComplexClass).ToString()]);
            Assert.AreSame(cCopy.SampleDictionary[typeof(ModerateClass).ToString()],
                                                 cCopy.ISampleDictionary[typeof(ModerateClass).ToString()]);
            Assert.AreNotSame(cCopy.SampleDictionary[typeof(SimpleClass).ToString()],
                                                 cCopy.ISampleDictionary[typeof(SimpleClass).ToString()]);
            Assert.AreSame(cCopy.ISimpleMultiDimArray[0, 0, 0], cCopy.SimpleMultiDimArray[1][1][1]);
        }
        
        public static void Assert_AreEqualButNotSame(ComplexClass c, ComplexClass cCopy)
        {
            if (c == null && cCopy == null)
            {
                return;
            }

            // objects are different instances
            Assert.AreNotSame(c, cCopy);

            // test on circular references
            Assert.AreSame(cCopy, cCopy.ThisComplexClass);
            Assert.AreSame(cCopy, cCopy.TupleOfThis.Item1);
            Assert.AreSame(cCopy, cCopy.TupleOfThis.Item2);
            Assert.AreSame(cCopy, cCopy.TupleOfThis.Item3);

            // original had nonnull delegates and events but copy has it null (for ExpressionTree copy method)
            Assert.IsTrue(c.JustDelegate != null);
            Assert.IsTrue(cCopy.JustDelegate == null); 
            Assert.IsTrue(c.ReadonlyDelegate != null);
            Assert.IsTrue(cCopy.ReadonlyDelegate == null);
            Assert.IsTrue(!c.IsJustEventNull);
            Assert.IsTrue(cCopy.IsJustEventNull);

            // test of regular dictionary
            Assert.AreEqual(c.SampleDictionary.Count, cCopy.SampleDictionary.Count);
            
            foreach (var pair in c.SampleDictionary.Zip(cCopy.SampleDictionary, (item, itemCopy) => new { item, itemCopy }))
            {
                Assert.AreEqual(pair.item.Key, pair.itemCopy.Key);

                Assert_AreEqualButNotSame_ChooseForType(pair.item.Value, pair.itemCopy.Value);
            }

            // test of dictionary of interfaces
            Assert.AreEqual(c.ISampleDictionary.Count, cCopy.ISampleDictionary.Count);

            foreach (var pair in c.ISampleDictionary.Zip(cCopy.ISampleDictionary, (item, itemCopy) => new { item, itemCopy }))
            {
                Assert.AreEqual(pair.item.Key, pair.itemCopy.Key);

                Assert_AreEqualButNotSame_ChooseForType(pair.item.Value, pair.itemCopy.Value);
            }

            // test of [,,] interface array
            if (c.ISimpleMultiDimArray != null)
            {
                Assert.AreEqual(c.ISimpleMultiDimArray.Rank, cCopy.ISimpleMultiDimArray.Rank);

                for (int i = 0; i < c.ISimpleMultiDimArray.Rank; i++)
                {
                    Assert.AreEqual(c.ISimpleMultiDimArray.GetLength(i), cCopy.ISimpleMultiDimArray.GetLength(i));
                }

                foreach (var pair in c.ISimpleMultiDimArray.Cast<ISimpleClass>().Zip(cCopy.ISimpleMultiDimArray.Cast<ISimpleClass>(), (item, itemCopy) => new { item, itemCopy }))
                {
                    Assert_AreEqualButNotSame_ChooseForType(pair.item, pair.itemCopy);
                }
            }

            // test of array of arrays of arrays (SimpleClass[][][])
            if (c.SimpleMultiDimArray != null)
            {
                Assert.AreEqual(c.SimpleMultiDimArray.Length, cCopy.SimpleMultiDimArray.Length);

                for (int i = 0; i < c.SimpleMultiDimArray.Length; i++)
                {
                    var subArray = c.SimpleMultiDimArray[i];
                    var subArrayCopy = cCopy.SimpleMultiDimArray[i];

                    if (subArray != null)
                    {
                        Assert.AreEqual(subArray.Length, subArrayCopy.Length);

                        for (int j = 0; j < subArray.Length; j++)
                        {
                            var subSubArray = subArray[j];
                            var subSubArrayCopy = subArrayCopy[j];

                            if (subSubArray != null)
                            {
                                Assert.AreEqual(subSubArray.Length, subSubArrayCopy.Length);

                                for (int k = 0; k < subSubArray.Length; k++)
                                {
                                    var item = subSubArray[k];
                                    var itemCopy = subSubArrayCopy[k];

                                    Assert_AreEqualButNotSame_ChooseForType(item, itemCopy);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void Assert_AreEqualButNotSame_ChooseForType(ISimpleClass s, ISimpleClass sCopy)
        {
            if (s == null && sCopy == null)
            {
                return;
            }

            if (s is ComplexClass)
            {
                Assert_AreEqualButNotSame((ComplexClass)s, (ComplexClass)sCopy);
            }
            else if (s is ModerateClass)
            {
                ModerateClassTests.Assert_AreEqualButNotSame((ModerateClass)s, (ModerateClass)sCopy);
            }
            else
            {
                SimpleClassTests.Assert_AreEqualButNotSame((SimpleClass)s, (SimpleClass)sCopy);
            }
        }
    }
}
