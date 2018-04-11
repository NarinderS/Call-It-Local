using Messages.ServiceBusRequest;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Communication
{
    partial class ClientConnection
    {
        private ServiceBusResponse weatherRequest(ServiceBusRequest request)
        {
            SendOptions sendOptions = new SendOptions();
            sendOptions.SetDestination("Weather");


            return requestingEndpoint.Request<ServiceBusResponse>(request, sendOptions).
                ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
