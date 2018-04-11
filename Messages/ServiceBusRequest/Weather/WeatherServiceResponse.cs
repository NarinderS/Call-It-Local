using Messages.DataTypes.Database.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.ServiceBusRequest.Weather
{
    
    [Serializable]
    public class WeatherServiceResponse : ServiceBusResponse
    {
        public WeatherServiceResponse(bool result, string response, WeatherReturnObject returnData)
            : base(result, response)
        {
            this.returnData = returnData;
        }

        public WeatherReturnObject returnData;       

        
    }
}
