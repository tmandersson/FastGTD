namespace FastGTD.DataTransfer
{
    public class InBoxItem
    {
        private string _name;

        protected InBoxItem() { }
        protected InBoxItem(string name)
        {
            _name = name;
        }

        public virtual int ID { get; set; }
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public static InBoxItem CreateNew(IInBoxItemPersister persister)
        {
            var item = new InBoxItem();
            persister.Save(item);
            return item;
        }
    }

    public interface IInBoxItemPersister
    {
        void Save(InBoxItem item);
    }
}
