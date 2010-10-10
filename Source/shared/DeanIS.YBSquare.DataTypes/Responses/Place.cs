using System.Runtime.Serialization;

namespace DeanIS.YBSquare.DataTypes.Responses
{
    [DataContract(Namespace = Constants.Namespace)]
    public class Place
    {
        public Place()
        {
            this.Address = new Address();
        }

        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Address Address { get; set; }

        [DataMember (EmitDefaultValue = false)]
        public WebSite WebSite { get; set; }

        [DataMember (EmitDefaultValue = false)]
        public string PhoneNumber { get; set; }

        [DataMember]
        public decimal Distance { get; set; }
    }
}