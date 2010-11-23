using System;

namespace Bricks.Core
{
    public class Money
    {
        public static readonly Money NULL = new NullMoney();
        private decimal amount;

        public Money() {}

        public Money(decimal? amount)
        {
            this.amount = amount ?? 0;
        }

        public Money(string amount)
        {
            amount = S.TrimSafe(amount);
            if (amount.IndexOf('£') == 0) this.amount = Convert(amount.Substring(1));
            else this.amount = Convert(amount);
        }

        private static decimal Convert(string amount)
        {
            return decimal.Parse("0" + S.TrimSafe(amount));
        }

        public virtual decimal Amount
        {
            get { return amount; }
        }

        public virtual bool IsNegativeOrZero
        {
            get { return amount <= 0; }
        }

        public virtual bool IsPositive
        {
            get { return amount > 0; }
        }

        public virtual Money NegetiveAmount
        {
            get { return new Money(0m - amount); }
        }

        public override string ToString()
        {
            return AsString();
        }

        public virtual string AsString()
        {
            return Amount.ToString();
        }

        public override bool Equals(object other)
        {
            if (this == other) return true;
            if (other == null) return false;
            if (!typeof (Money).IsAssignableFrom(other.GetType())) return false;
            return Equals((Money) other);
        }

        public virtual bool Equals(Money other)
        {
            return amount.Equals(other.amount);
        }

        public override int GetHashCode()
        {
            return amount.GetHashCode();
        }

        public static implicit operator Money(string moneyAsString)
        {
            return new Money(moneyAsString);
        }

        public static implicit operator Money(decimal? money)
        {
            return new Money(money);
        }

        public static Money operator +(Money thisMoney, Money thatMoney)
        {
            return new Money(thisMoney.Amount + thatMoney.Amount);
        }

        public static bool operator >(Money thisMoney, Money thatMoney)
        {
            return thisMoney.Amount > thatMoney.Amount;
        }

        public static bool operator <(Money thisMoney, Money thatMoney)
        {
            return thisMoney.Amount < thatMoney.Amount;
        }

        public static bool operator <=(Money thisMoney, Money thatMoney)
        {
            return thisMoney.Amount <= thatMoney.Amount;
        }

        public static bool operator >=(Money thisMoney, Money thatMoney)
        {
            return thisMoney.Amount >= thatMoney.Amount;
        }

        public static Money operator -(Money thisMoney, Money thatMoney)
        {
            return new Money(thisMoney.Amount - thatMoney.Amount);
        }

        public virtual Money Add(Money outstanding)
        {
            return new Money(amount + outstanding.amount);
        }

        public static bool IsNullOrEmpty(Money money)
        {
            return money == null || money is NullMoney;
        }

        public static Money Pounds(decimal money)
        {
            return new Money(money);
        }

        public virtual Money Abs()
        {
            return new Money(Math.Abs(amount));
        }

        public virtual string ToString(IFormatProvider provider)
        {
            return amount.ToString("C", provider);
        }
    }

    [Serializable]
    internal class NullMoney : Money
    {
        public override string ToString()
        {
            return "";
        }
    }
}