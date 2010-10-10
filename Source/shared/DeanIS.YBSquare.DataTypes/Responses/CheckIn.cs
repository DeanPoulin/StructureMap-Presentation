using System;
using System.Runtime.Serialization;

namespace DeanIS.YBSquare.DataTypes.Responses
{
    [DataContract(Namespace = Constants.Namespace)]
    public class CheckIn
    {
        [DataMember] public Guid UserId { get; set; }
        [DataMember] public long ListingId { get; set; }
        [DataMember] public DateTime CheckInTime { get; set; }
    }
}