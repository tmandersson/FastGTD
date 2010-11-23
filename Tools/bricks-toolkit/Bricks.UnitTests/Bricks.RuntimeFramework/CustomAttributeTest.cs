using NUnit.Framework;

namespace Bricks.RuntimeFramework
{
    [TestFixture]
    public class CustomAttributeTest
    {
        [Test]
        public void AttributeValue()
        {
            Class @class = new Class(typeof(TestClass_WithAttribute));
            CategoryAttribute categoryAttribute = @class.Attribute<CategoryAttribute>();
            Assert.AreEqual("Normal", categoryAttribute.Name);
        }
    }

    [Category("Normal")]
    public class TestClass_WithAttribute
    {
    }
}