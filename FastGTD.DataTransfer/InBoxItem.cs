namespace FastGTD.DataTransfer
{
    public class InBoxItem : GTDItem
    {
        protected InBoxItem() { }
        public InBoxItem(string name)
        {
            _name = name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (InBoxItem)) return false;
            return Equals((InBoxItem) obj);
        }

        protected virtual bool Equals(InBoxItem obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj._name, _name) && obj.Id == Id;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_name != null ? _name.GetHashCode() : 0)*397) ^ Id;
            }
        }
    }
}
