using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DeanIS.YBSquare.DataTypes.Responses
{
    [DataContract(Namespace = Constants.Namespace)]
    public class CheckInsResponse
    {
        [DataMember] public List<CheckIn> CheckIns { get; set; }
    }
}