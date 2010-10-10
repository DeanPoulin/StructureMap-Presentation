using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace DeanIS.YBSquare.Web
{
    public class GlobalContext
    {
        public const string Key = "__ThEsAfEsTkEyToEvErExIsT";
        public GlobalContext()
        {
            Current = this;
        }

        public static GlobalContext Current
        {
            get
            {
                if (HttpContext.Current != null)
                    return HttpContext.Current.Items[Key] as GlobalContext;

                return CallContext.GetData(Key) as GlobalContext;
            }
            set
            {
                if (HttpContext.Current != null)
                    HttpContext.Current.Items[Key] = value;
                else
                    CallContext.SetData(Key, value);
            }
        }


        public UserActivity UserActivity { get; set; }
        public string AnotherValue { get; set; }
    }

    public class UserActivity
    {
        public List<long> PlacesViewed { get; set; }
    }
}
