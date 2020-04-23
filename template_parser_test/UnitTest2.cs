using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using TemplateParser;

namespace Tests
{
    public class TestsTextReplacer
    {
        public TextReplacer tr;

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
            string test = Path.Combine(TestContext.CurrentContext.TestDirectory, @"test.xml");

            System.IO.File.WriteAllText(test, xml);

            tr = new TextReplacer("test.xml", "testF.xml", TextReplacer.EncodeType.XML);
     
        }

        [Test]
        public void Test1Encoding()
        {
            tr.Encoding = TextReplacer.EncodeType.XML;
            Assert.AreEqual("&apos;&lt;hello&amp;welcome&gt;&apos;", tr.EncodeValue("'<hello&welcome>'"));

            tr.Encoding = TextReplacer.EncodeType.SQL;
            Assert.AreEqual("''hello''", tr.EncodeValue("'hello'"));

            tr.Encoding = TextReplacer.EncodeType.HTML;
            Assert.AreEqual("&#39;hello world&#39;", tr.EncodeValue("'hello world'"));
        }
    }
}
