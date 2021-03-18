namespace MyApp.BLL.Serveces
{
    using MyApp.BLL.DTO.EmailData;
    using MyApp.BLL.Interfaces;
    using System.Net.Mail;
    using System.Net;

    public class EmailSender : IEmailSender
    {
        readonly string email;
        readonly string password;
        public EmailSender(string email = "shopveb@gmail.com", string passwd = "ag4313112")
        {
            this.email = email;
            this.password = passwd;
        }

        public void SendMessageAsync(EmailData data, string attachment = null)
        {
            MailAddress from = new MailAddress(data.RecipientMail, "MyApp Shop");
            MailAddress to = new MailAddress(data.RecipientMail);
            MailMessage msg = new MailMessage(from, to);
            if (attachment != null) 
                msg.Attachments.Add(new Attachment(attachment));
            msg.Subject = data.Subject;
            msg.Body = data.MsgContent + "\n\n" + data.Text;
            using (SmtpClient sender = new SmtpClient("smtp.gmail.com", 587))
            {
                sender.Credentials = new NetworkCredential(email, password);
                sender.EnableSsl = true;
                //sender.DeliveryMethod = SmtpDeliveryMethod.Network;
                try
                {
                    sender.Send(msg);
                }
                catch { }
            }
        }
    }
}
