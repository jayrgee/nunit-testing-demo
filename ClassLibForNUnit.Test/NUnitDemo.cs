using NUnit.Framework;
using ClassLibForNUnit;
using System;
using System.Collections.Generic;

namespace ClassLibForNUnit.Test
{
    /// <summary>
    /// NUnit Categories
    /// NUnit has a different way of setting up categories, in fact it can be done in two different ways. You can add the category as a text string, the same way as you do it in MSTest above, or you can derive a new class from the Unit CategoryAttribute class
    /// https://blogs.msdn.microsoft.com/devops/2012/11/20/part-2using-traits-with-different-test-frameworks-in-the-unit-test-explorer/
    /// </summary>
    public class DD : CategoryAttribute { }

    /// <summary>
    /// NUnit Testing with Visual Studio 2015
    /// Learn NUnit testing starting from TDD to converting it to data driven test and how to incorporate it for Visual Studio 2015.
    /// https://www.dotnetcurry.com/visualstudio/1352/nunit-testing-visual-studio-2015
    /// </summary>
    [TestFixture]
    public class NUnitDemo
    {
        [Test]
        [Category("BLAH")]
        // bug in nunit3-vs-adapter 3.11
        // Categories cause duplicate entries in test explorer
        // https://github.com/nunit/nunit3-vs-adapter/issues/559
        public void TestMethodSum()
        {
            int actual, expected = 30;
            ClassForNUnit target = new ClassForNUnit();
            actual = target.Sum(12, 18);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestMethodSumArrayArgs()
        {
            int actual, expected = 30;
            ClassForNUnit target = new ClassForNUnit();
            actual = target.Sum(new int[] { 12, 18 });
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestMethodSubtract()
        {
            int actual, expected = 10;
            ClassForNUnit target = new ClassForNUnit();
            actual = target.Subtract(10, 0);
            Assert.AreEqual(expected, actual);

        }

        // Data Driven NUnit Test

        //[Test, TestCase(10, 20, 30)]
        [Test, DD]
        //[Test]
        [TestCase(1, 2, 3)]
        [TestCase(10, 20, 30)]
        [TestCase(12, 20, 32)]
        public void TestMethodSumDD(int num1, int num2, int res)
        {
            int actual, expected = res;
            ClassForNUnit target = new ClassForNUnit();
            actual = target.Sum(num1, num2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        // Sending Arrays as Parameters to the Test
        /// </summary>
        static int[][] Numbers = {
            new int[] { 5, 4, 2, 11 },
            new int[] { 6, 4, 10 }
        };

        [Test, TestCaseSource("Numbers")]
        public void TestSumConstant(int[] num)
        {
            int actual, expected = num[num.Length - 1];
            ClassForNUnit target = new ClassForNUnit();
            List<int> param = new List<int>();
            for (int i = 0; i < num.Length - 1; i++)
            {
                param.Add(num[i]);
            }
            actual = target.Sum(param.ToArray());
            Assert.AreEqual(expected, actual);
        }

        [Test, TestCaseSource(typeof(GetTestDt), "GetTestData")]
        public void TestSumCsvFile(int[] num)
        {
            int actual, expected = num[num.Length - 1];
            ClassForNUnit target = new ClassForNUnit();
            List<int> param = new List<int>();
            for (int i = 0; i < num.Length - 1; i++)
            {
                param.Add(num[i]);
            }
            actual = target.Sum(param.ToArray());
            Assert.AreEqual(expected, actual);
        }
    }
}
