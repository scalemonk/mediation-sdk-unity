using System.Collections;
using UnityEditor;
using UnityEngine.Assertions;

namespace ScaleMonk.Ads.Tests
{
    public class Test
    {

        [NUnit.Framework.Test]
        public void TestSimplePasses()
        {
            // Use the Assert class to test conditions.
            Assert.IsTrue(true);
        }

        // A UnityTest behaves like a coroutine in PlayMode
        // and allows you to yield null to skip a frame in EditMode
        [UnityEngine.TestTools.UnityTest]
        public IEnumerator TestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // yield to skip a frame
            yield return null;
        }
    }
}