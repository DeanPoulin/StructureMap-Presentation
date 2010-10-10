using System.Runtime.Serialization;
using DeanIS.Framework.Testing;
using DeanIS.Net.Serialization;
using NUnit.Framework;

namespace DeanIS.Net.Tests
{
    [TestFixture]
    public class when_performing_serialization_of_an_object_to_a_string : context
    {
        private TestObject _test;
        private Serializer<TestObject> sut;
        private string _result;

        public override void setupContext()
        {
            sut = new Serializer<TestObject>();

            _test = new TestObject()
                        {
                            Foo = "Love it",
                            Goo = 123
                        };
        }

        public override void act()
        {
            _result = sut.ToString(_test);
        }

        [Test]
        public void the_string_contains_the_proper_xml()
        {
            Assert.That(_result, Is.Not.Null);
            Assert.That(_result.Contains("Foo"), Is.EqualTo("<Test xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.deanis.net/test\"><Foo>Love it</Foo><Goo>123</Goo></Test>"));
        }
    }

    [DataContract(Name="Test", Namespace = "http://www.deanis.net/test")]
    public class TestObject
    {
        [DataMember] public string Foo { get; set; }
        [DataMember] public int Goo { get; set; }
    }
}
