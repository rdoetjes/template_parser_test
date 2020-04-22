using System.IO;
using NUnit.Framework;
using TemplateParser;

namespace Tests
{
    public class TestsSourceXmlReader
    {
        public SourceXmlReader sr;

        [SetUp]
        public void Setup()
        {
            //there's a copy of the test.xml made into the in the Debug/bin directory
            string test = Path.Combine(TestContext.CurrentContext.TestDirectory, @"test.xml");
            sr = new SourceXmlReader(test, "dev/testConfig");
        }

        [Test]
         public void Test1ProperKeyValueFromXML()
         {
            var a = sr.getKeyValuePair();
            Assert.AreEqual(3, a.Count);
            Assert.AreEqual("value1", a[0].Key);
            Assert.AreEqual("1", a[0].Value);
            Assert.AreEqual("value2", a[1].Key);
            Assert.AreEqual("2", a[1].Value);
            Assert.AreEqual("value3", a[2].Key);
            Assert.AreEqual("&<dev>", a[2].Value);
        }

        [Test]
        public void Test2ChangeSection()
        {
            sr.section = "/prd/testConfig";
            var a = sr.getKeyValuePair();
            Assert.AreEqual(3, a.Count);
            Assert.AreEqual("value1", a[0].Key);
            Assert.AreEqual("3", a[0].Value);
            Assert.AreEqual("value2", a[1].Key);
            Assert.AreEqual("1", a[1].Value);
            Assert.AreEqual("value3", a[2].Key);
            Assert.AreEqual("&<prd>", a[2].Value);
        }
    }
}