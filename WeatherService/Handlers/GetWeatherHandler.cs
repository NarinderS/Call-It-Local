using Messages;
using Messages.DataTypes.Database.Weather;
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
using WeatherService.Methods;

namespace WeatherService.Handlers
{
    class GetWeatherHandler : IHandleMessages<WeatherServiceRequest>
    {
        static ILog log = LogManager.GetLogger<WeatherServiceRequest>();

        public Task Handle(WeatherServiceRequest message, IMessageHandlerContext context)
        {
            Debug.consoleMsg("Got to GetWeatherHandler");


            WeatherReturnObject returnVal = WeatherMethods.getWeatherStuff(message.location);
            Debug.consoleMsg(returnVal.realFeelTemperature.ToString());
            string response = "success";


            return context.Reply(new WeatherServiceResponse(true, response, returnVal));

        }
    }
}

    

