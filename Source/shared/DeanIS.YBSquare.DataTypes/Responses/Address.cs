using System;
using System.Runtime.Serialization;

namespace DeanIS.YBSquare.DataTypes.Responses
{
    [DataContract(Namespace = Constants.Namespace)]
    public class Address
    {
        [DataMember] 
        public string Address1 { get; set; }

        [DataMember] 
        public string Address2 { get; set; }
        
        [DataMember] 
        public string City { get; set; }
        
        [DataMember] 
        public string State { get; set; }
        
        [DataMember] 
        public string ZipCode { get; set; }
        
        [DataMember] 
        public string Latitude { get; set; }
        
        [DataMember] 
        public string Longitude { get; set; }

        public decimal DLatitude
        {
            get { return StringToGeo(Latitude); }
        }

        public decimal DLongitude
        {
            get { return StringToGeo(Longitude); }
        }

        public bool HasLatLong
        {
            get
            {
                return !string.IsNullOrEmpty(Longitude) && !string.IsNullOrEmpty(Latitude) && Longitude != "0" && Latitude != "0";
            }
        }

        private static decimal StringToGeo(string value)
        {
            try
            {
                decimal outdec;
                if (decimal.TryParse(value, out outdec))
                {
                    return outdec / 10000;
                }
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}