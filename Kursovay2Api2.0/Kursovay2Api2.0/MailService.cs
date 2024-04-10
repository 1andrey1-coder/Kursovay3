using System.Net;
using System.Net.Mail;

public class MailService
{
    SmtpClient emailService = new SmtpClient();
    string hostmail = "ilchenkor1135@suz-ppk.ru";
    public MailService()
    {
        emailService.Host = "smtp.beget.com";
        emailService.Port = 25;
        //465 - обычное защищенное соединение
        //587 - антиспам для почт(чтобы не закинуло в спам сообщения) защищенное соединение
        emailService.Credentials = new NetworkCredential(hostmail, "MBx&QIF9");
        emailService.EnableSsl = false;
    }

    internal async Task Send(string? mail, string v2, string v3)
    {
         emailService.Send(hostmail, mail, v2, v3);
    }

    



}
