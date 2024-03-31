using System.Net;
using System.Net.Mail;

public class MailService
{
    SmtpClient emailService = new SmtpClient();
    public MailService()
    {
        emailService.Host = "smtp.mail.ru";
        emailService.Port = 587;
        //465
        emailService.Credentials = new NetworkCredential("ilchenkor17@mail.ru", "uCqVJebBv08tMGaAdquM");
        emailService.EnableSsl = true;
    }

    internal async Task Send(string v1, string? mail, string v2, string v3)
    {
         emailService.Send(v1, mail, v2, v3);
    }




    
}
