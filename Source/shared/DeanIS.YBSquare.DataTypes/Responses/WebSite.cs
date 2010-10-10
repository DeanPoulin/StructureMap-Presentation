using System;
using System.Runtime.Serialization;

namespace DeanIS.YBSquare.DataTypes.Responses
{
    [DataContract(Name = Constants.Namespace)]
    public class WebSite
    {
        [DataMember] public string Title { get; set; }
        [DataMember] public Uri Uri { get; set; }
    }
}