using System.Runtime.Serialization;

namespace DeanIS.YBSquare.DataTypes.Responses
{
    [DataContract(Name = "PlaceDetailsResponse", Namespace = Constants.Namespace)]
    public class PlaceDetailsResponse
    {
        public Place Place { get; set; }
    }
}
