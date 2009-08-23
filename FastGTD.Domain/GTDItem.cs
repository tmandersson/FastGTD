namespace FastGTD.Domain
{
    public class GTDItem
    {
        protected string _name;

        public GTDItem() { }
        public GTDItem(string name) { _name = name; }

        public virtual int Id { get; set; }

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}