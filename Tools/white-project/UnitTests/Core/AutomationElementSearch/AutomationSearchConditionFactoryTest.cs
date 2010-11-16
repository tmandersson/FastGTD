using System.Collections.Generic;
using System.Windows.Automation;
using White.Core.AutomationElementSearch;
using NUnit.Framework;

namespace White.UnitTests.Core.AutomationElementSearch
{
    [TestFixture]
    public class AutomationSearchConditionFactoryTest
    {
        [Test]
        public void GetWindowSearchConditionsWhenProcessIdIsValid()
        {
            List<AutomationSearchCondition> conditions = new AutomationSearchConditionFactory().GetWindowSearchConditions(1);
            Assert.AreEqual(2, conditions.Count);
            Assert.IsInstanceOfType(typeof (AndCondition), conditions[0].Condition);
            Assert.IsInstanceOfType(typeof (AndCondition), conditions[1].Condition);
        }

        [Test]
        public void GetWindowSearchConditionsWhenProcessIdIsInvalid()
        {
            List<AutomationSearchCondition> conditions = new AutomationSearchConditionFactory().GetWindowSearchConditions(0);
            Assert.AreEqual(2, conditions.Count);
            Assert.IsInstanceOfType(typeof(PropertyCondition), conditions[0].Condition);
            Assert.IsInstanceOfType(typeof(PropertyCondition), conditions[1].Condition);
        }
    }
}