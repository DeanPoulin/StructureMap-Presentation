using System.Runtime.Serialization;

namespace DeanIS.YBSquare.DataTypes.Responses
{
    [DataContract(Namespace = Constants.Namespace)]
    public class CheckInResponse
    {
        [DataMember] public int CheckInId { get; set; }
        [DataMember] public bool Successful { get; set; }
    }
}