using System.Web.Security;

namespace DeanIS.YBSquare.Web.Services
{
    public interface IMembershipService
    {
        int MinPasswordLength { get; }

        bool ValidateUser(string email, string password);
        MembershipCreateStatus CreateUser(string email, string password);
        bool ChangePassword(string email, string oldPassword, string newPassword);
    }
}