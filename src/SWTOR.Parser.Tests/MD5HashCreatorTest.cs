using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SWTOR.Parser.Tests
{
    [TestClass]
    public class MD5HashCreatorTest
    {
        [TestMethod]
        public void CreateHash_HelloString_Ok()
        {
            var creator = new MD5HashCreator();
            var result = creator.CreateHash("hello");
            Assert.AreEqual("4229d691b07b13341da53f17ab9f2416", result);
        }
    }
}