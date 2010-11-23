using System.Collections;
using NUnit.Framework;

namespace Bricks.Core
{
    [TestFixture]
    public class CustomEnumTest
    {
        [Test]
        public void ReturnsAllTypesCorrecly()
        {
            IList types = RoundingMethod.AllTypes<RoundingMethod>();
            Assert.AreEqual(2, types.Count);
            Assert.AreEqual(typeof (RoundUpOn5), types[0].GetType());
            Assert.AreEqual(typeof (RoundUpOn6), types[1].GetType());
        }

        [Test]
        public void ReturnsOnlyTypes()
        {
            IList types = RoundingMethod.OnlyTypes<RoundingMethod>(RoundingMethod.RoundUpOn5);
            Assert.AreEqual(1, types.Count);
            Assert.AreEqual(typeof (RoundUpOn5), types[0].GetType());
        }

//
//        [Test]
//        public void ShouldPickupParentTypesFieldsAsWell()
//        {
//            Assert.AreEqual(DirectPaymentType.DirectDebit, CustomEnum.ValueOf<DirectPaymentType>(DirectPaymentType.DirectDebit.Name));
//            Assert.AreEqual(DirectPaymentType.ChequeOrTravellersCheque, CustomEnum.ValueOf<DirectPaymentType>(DirectPaymentType.ChequeOrTravellersCheque.Name));
//            Assert.AreEqual(DirectPaymentType.AgentThirdPartyFinance, CustomEnum.ValueOf<DirectPaymentType>(DirectPaymentType.AgentThirdPartyFinance.Name));
//            
//        }
    }
}