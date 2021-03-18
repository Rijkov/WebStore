namespace MyApp.BLL.Interfaces
{
    using MyApp.BLL.DTO.EmailData;

    public interface IEmailSender
    {
        void SendMessageAsync(EmailData content, string attachment);
    }
}
