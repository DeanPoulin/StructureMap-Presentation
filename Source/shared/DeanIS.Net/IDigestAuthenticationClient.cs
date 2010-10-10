using System;

namespace DeanIS.Net
{
    public interface IDigestAuthenticationClient
    {
        string GetResponseString(string username, string password, Uri uri);
    }
}