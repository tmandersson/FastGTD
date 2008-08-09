namespace FastGTD
{
    public interface IInboxView
    {
        InBoxForm Form { get; }
        bool InBoxListFullRowSelect { get; set; }
        void Show();

        void SetTextBoxFocus();
    }
}