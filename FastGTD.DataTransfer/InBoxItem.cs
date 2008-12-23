namespace FastGTD.DataTransfer
{
    public class InBoxItem
    {
        private string _name;

        public InBoxItem() { }
        public InBoxItem(string name)
        {
            _name = name;
        }

        public virtual int ID { get; set; }
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}
