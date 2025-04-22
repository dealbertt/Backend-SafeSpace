using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;


namespace Backend_SafeSpace
{
    public class EmailService
    {
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUser = "your-email@gmail.com";
        private readonly string _smtpPass = "your-app-password"; // Not your Gmail password

        public async Task SendConfirmationEmailAsync(string toEmail, string userName, string confirmationLink)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("SafeSpace App", _smtpUser));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = "Confirm your registration";

            message.Body = new TextPart("html")
            {
                Text = $@"
                <h2>Welcome to SafeSpace confirmation email page, {userName}!</h2>
                <p>Please confirm your email by clicking the link below:</p>
                <a href='{confirmationLink}'>Confirm Email</a>
            "
            }; 

            using var client = new SmtpClient();
            await client.ConnectAsync(_smtpServer, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_smtpUser, _smtpPass);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
