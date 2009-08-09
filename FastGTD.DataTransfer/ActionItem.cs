namespace FastGTD.DataTransfer
{
    public class ActionItem : GTDItem
    {
        public ActionItem() { }
        public ActionItem(string name) : base(name) {}

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ActionItem)) return false;
            return Equals((ActionItem)obj);
        }

        protected virtual bool Equals(ActionItem obj)
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
