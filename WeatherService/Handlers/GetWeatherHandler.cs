using Messages;
using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.Chat.Requests;
using Messages.ServiceBusRequest.Weather;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherService.Handlers
{
    class GetWeatherHandler : IHandleMessages<WeatherServiceRequest>
    {
        static ILog log = LogManager.GetLogger<WeatherServiceRequest>();

        public Task Handle(WeatherServiceRequest message, IMessageHandlerContext context)
        {
            Debug.consoleMsg("Got to GetWeatherHandler");
            
            

            

            return null;

        }
    }
}

    

