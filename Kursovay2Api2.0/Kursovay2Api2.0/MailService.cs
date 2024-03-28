using System.Net.Mail;

public class MailService
{
    SmtpClient emailService = new SmtpClient();
    public MailService()
    {
        emailService.Host = "smtp.mail.ru";
        emailService.Port = 587;
        //465
        emailService.Credentials = new System.Net.NetworkCredential("from mail", "pass");
    }

    internal async Task SendMailAsync(string v1, string? mail, string v2, string v3)
    {
        await emailService.SendMailAsync(v1, mail, v2, v3);
    }
}