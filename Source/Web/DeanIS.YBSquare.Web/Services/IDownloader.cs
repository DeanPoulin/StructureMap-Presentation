using System;

namespace DeanIS.YBSquare.Web.Services
{
    public interface IDownloader
    {
        string GetString(Uri uri);
    }
}