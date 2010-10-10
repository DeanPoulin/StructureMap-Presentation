namespace DeanIS.Net.Mail
{
    public interface IPostOffice
    {
        void DeliverMail(string to, string from, string subject, string message, bool isHtml);
    }
}