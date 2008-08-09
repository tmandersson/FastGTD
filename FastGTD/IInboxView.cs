namespace FastGTD
{
    public interface IInboxView
    {
        InBoxForm Form { get; }
        bool FullRowSelect { get; set; }
        void Show();
    }
}