using System;
using DeanIS.Net.Serialization;
using DeanIS.YBSquare.DataTypes.Responses;

namespace DeanIS.YBSquare.Web.Services
{
    public class YBSquareService : IYBSquareService
    {
        private readonly IApiConfiguration _configuration;
        private readonly IDownloader _downloader;

        public YBSquareService(IApiConfiguration configuration, IDownloader downloader)
        {
            _configuration = configuration;
            _downloader = downloader;
        }

        public PlacesResponse GetPlaces(string category, decimal latitude, decimal longitude)
        {
            var url = string.Format("{0}Places?what={1}&latitude={2}&longitude={3}&pagesize=20", _configuration.BasePath,
                                    category, latitude, longitude);

            var response = _downloader.GetString(new Uri(url));

            var serializer = new Serializer<PlacesResponse>();

            return serializer.FromString(response);
        }

        public PlacesResponse GetPlaces(string what, string where)
        {
            var url = string.Format("{0}Places?what={1}&where={2}&pagesize=20", _configuration.BasePath,
                                    what, where);

            var response = _downloader.GetString(new Uri(url));

            var serializer = new Serializer<PlacesResponse>();

            return serializer.FromString(response);
        }
    }
}
