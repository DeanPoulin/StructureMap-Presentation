using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DeanIS.YBSquare.DataTypes.Responses
{
    [DataContract(Name="PlacesResponse", Namespace = Constants.Namespace)]
    public class PlacesResponse
    {
        [DataMember]
        public List<Place> Places { get; set; }
        [DataMember]
        public Status Status { get; set; }
    }

    [DataContract(Namespace = Constants.Namespace)]
    public enum Status
    {

        [EnumMember] Ok, 
        [EnumMember] Error,
        [EnumMember] UnhandledException
    }
}