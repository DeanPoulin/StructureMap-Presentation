using System;

namespace DeanIS.Net.Mail
{
    public class SmtpPostOffice : IPostOffice
    {
        public void DeliverMail(string to, string from, string subject, string body, bool isHtml)
        {
            throw new NotImplementedException();
        }
    }
}
