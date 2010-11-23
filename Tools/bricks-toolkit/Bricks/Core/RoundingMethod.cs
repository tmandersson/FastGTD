using System;

namespace Bricks.Core
{
    [Serializable]
    public abstract class RoundingMethod : CustomEnum
    {
        public static readonly RoundingMethod RoundUpOn5 = new RoundUpOn5("Round up on 5");
        public static readonly RoundingMethod RoundUpOn6 = new RoundUpOn6("Round up on 6");

        protected RoundingMethod(string name) : base(name) {}

        public static RoundingMethod Default
        {
            get { return RoundUpOn5; }
        }

        public abstract decimal Round(decimal amount);

        protected virtual decimal RoundAwayFromZero(decimal amount)
        {
            return Math.Round(amount, 2, MidpointRounding.AwayFromZero);
        }
    }

    [Serializable]
    public class RoundUpOn5 : RoundingMethod
    {
        private RoundUpOn5() : base("foo") {}
        public RoundUpOn5(string name) : base(name) {}

        public override decimal Round(decimal amount)
        {
            return RoundAwayFromZero(amount);
        }
    }

    [Serializable]
    public class RoundUpOn6 : RoundingMethod
    {
        public RoundUpOn6(string name) : base(name) {}
        private RoundUpOn6() : base("foo") {}

        public override decimal Round(decimal amount)
        {
            string amountText = amount.ToString();
            if (amountText.IndexOf('.') == -1) return amount;

            string decimalPart = amountText.Substring(amountText.IndexOf('.') + 1);
            if (decimalPart.Length <= 2) return amount;

            string integerPartWithDecimal = amountText.Substring(0, amountText.Length - decimalPart.Length);

            if (decimalPart.Length > 2 && int.Parse(decimalPart[2].ToString()) > 5)
                return RoundAwayFromZero(amount);

            return decimal.Parse(integerPartWithDecimal + decimalPart.Substring(0, 2));
        }
    }
}