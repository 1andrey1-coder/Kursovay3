using EmailSenderSMTP;

namespace Kursovay2Api2._0
{
    public class EmailMessageService
    {
        private static SenderSMTP _sender;
        public static SenderSMTP sender { get
            {
                if(sender == null)
                    _sender = new SenderSMTP(EmailSenderSMTP.MailService.MailRu, "slovarsleng@mail.ru", "uCqVJebBv08tMGaAdquM");
                
                return _sender;
            } }
    }
}
