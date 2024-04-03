using System.Net;
using System.Net.Mail;

public class MailService
{
    SmtpClient emailService = new SmtpClient();
    public MailService()
    {
        emailService.Host = "smtp.mail.ru";
        emailService.Port = 587;
        //465 - обычное защищенное соединение
        //587 - антиспам для почт(чтобы не закинуло в спам сообщения) защищенное соединение
        emailService.Credentials = new NetworkCredential("slovarsleng@mail.ru", "SULJDS0fkVubK5NPsRwC");
        emailService.EnableSsl = true;
    }

    internal async Task Send(string v1, string? mail, string v2, string v3)
    {
         emailService.Send(v1, mail, v2, v3);
    }

    internal async Task Send2(string v1, string? mail, string v2, string v3)
    {
        emailService.Send(v1, mail, v2, v3);
    }



}
