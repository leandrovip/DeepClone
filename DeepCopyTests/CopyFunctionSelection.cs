using System;
using DeepCopyExtensions;

namespace DeepCopyTests
{
    public static class CopyFunctionSelection
    {
        public static Func<object, object> CopyMethod;

        static CopyFunctionSelection()
        {
            CopyMethod = (obj) => DeepCopyByExpressionTrees.DeepCopyByExpressionTree(obj);
            //CopyMethod = DeepCopyByReflection.Copy;
            //CopyMethod = DeepCopyBySerialization.DeepClone;
        }
    }
}
