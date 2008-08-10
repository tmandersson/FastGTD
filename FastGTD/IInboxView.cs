namespace FastGTD
{
    public interface IInBoxView
    {
        InBoxForm Form { get; }
        bool InBoxListFullRowSelect { get; set; }
        void Show();

        void SetTextBoxFocus();

        event AddItemEvent AddItemAction;
    }

    public delegate void AddItemEvent(string new_in_item);
}