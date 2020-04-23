using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using TemplateParser;

namespace Tests
{
    public class TestsSourceXmlReader
    {
        public SourceXmlReader sr;

        public string xml = @"<whatever>
                                <configTemplates>
                                  <dev>
                                     <testConfig>
                                        <value1>1</value1>
                                        <value2>2</value2>
                                        <value3>&amp;&lt;dev&gt;</value3>
                                     </testConfig>
                                  </dev>
                                  <tst>
                                        <testConfig>
                                            <value1>3</value1>
                                            <value2>1</value2>
                                            <value3>&amp;&lt;tst&gt;</value3>
                                        </testConfig>
                                  </tst>
                                  <acc>
                                        <testConfig>
                                            <value1>3</value1>
                                            <value2>1</value2>
                                            <value3>&amp;&lt;acc&gt;</value3>
                                        </testConfig>
                                  </acc>
                                  <prd>
                                        <testConfig>
                                            <value1>3</value1>
                                            <value2>1</value2>
                                            <value3>&amp;&lt;prd&gt;</value3>
                                        </testConfig>
                                  </prd>
                                </configTemplates>
                            </whatever>
                            ";

        [SetUp]
        public void Setup()
        {
            //there's a copy of the test.xml made into the in the Debug/bin directory

            string test = Path.Combine(TestContext.CurrentContext.TestDirectory, @"test.xml");

            System.IO.File.WriteAllText(test, xml);

            sr = new SourceXmlReader(test, "dev/testConfig");
        }

        [Test]
         public void Test1ProperKeyValueFromXML()
         {
            var a = sr.getKeyValuePair();
            Assert.AreEqual(3, a.Count);

            List<string> keys = new List<string>(a.Keys);

            Assert.AreEqual(true, keys.Contains("value1"));
            Assert.AreEqual("1", a["value1"]);

            Assert.AreEqual(true, keys.Contains("value2"));
            Assert.AreEqual("2", a["value2"]);

            Assert.AreEqual(true, keys.Contains("value3"));
            Assert.AreEqual("&<dev>", a["value3"]);
        }

        [Test]
        public void Test2ChangeSection()
        {
            sr.section = "/prd/testConfig";
            var a = sr.getKeyValuePair();
            Assert.AreEqual(3, a.Count);

            List<string> keys = new List<string>(a.Keys);

            Assert.AreEqual(true, keys.Contains("value1"));
            Assert.AreEqual("3", a["value1"]);

            Assert.AreEqual(true, keys.Contains("value2"));
            Assert.AreEqual("1", a["value2"]);

            Assert.AreEqual(true, keys.Contains("value3"));
            Assert.AreEqual("&<prd>", a["value3"]);
        }
    }
}