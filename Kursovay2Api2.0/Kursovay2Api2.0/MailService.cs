using System.Net.Mail;

public class MailService
{
    SmtpClient emailService = new SmtpClient();
    public MailService()
    {
        emailService.Host = "smtp.mail.ru";
        emailService.Port = 465;
        //587
        emailService.Credentials = new System.Net.NetworkCredential("ilchenkor17@mail.ru", "EHCtSAuDaeqR3WhBsyfT");
    }

    internal async Task SendMailAsync(string v1, string? mail, string v2, string v3)
    {
        await emailService.SendMailAsync(v1, mail, v2, v3);
    }
}