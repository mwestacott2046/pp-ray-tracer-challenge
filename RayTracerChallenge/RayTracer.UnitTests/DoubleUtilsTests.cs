using NUnit.Framework;

namespace RayTracer.UnitTests
{
    [TestFixture]
    public class DoubleUtilsTests
    {
        [TestCase(0.0, 0.0, true)]
        [TestCase(1.0,1.0,true)]
        [TestCase(0.0, 1.0, false)]
        [TestCase(1.0, -1.0, false)]
        public void DoubleEqualsTest(double a, double b, bool expectedResult)
        {
            var result = DoubleUtils.DoubleEquals(a, b);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
