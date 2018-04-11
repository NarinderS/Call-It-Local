using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.ServiceBusRequest.Weather
{
    
    [Serializable]
    public class WeatherServiceRequest : ServiceBusRequest, IMessage
    {
        public WeatherServiceRequest(string location)
            : base(Service.Weather)
        {
            this.location= location;
        }

        public string location;
    }
}
