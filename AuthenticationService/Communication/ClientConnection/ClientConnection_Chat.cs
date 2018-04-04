using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.Chat;
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
        private ServiceBusResponse chatRequest(ChatServiceRequest request)
        {
            switch (request.requestType)
            {
                case (ChatRequest.getChatContacts):
                    return getChatContacts((ChatServiceRequest)request);
                case (ChatRequest.getChatHistory):
                    return getChatHistory((ChatServiceRequest)request);
                case (ChatRequest.sendMessage):
                    return sendMessage((ChatServiceRequest)request);
                default:
                    return new ServiceBusResponse(false, "Error: Invalid Request. Request received was:" + request.requestType.ToString());
            }
        }

        /// <summary>
        /// Publishes an EchoEvent.
        /// </summary>
        /// <param name="request">The data to be echo'd back to the client</param>
        /// <returns>The data sent by the client</returns>
        private ServiceBusResponse getChatContacts(ChatServiceRequest request)
        {
            

            SendOptions sendOptions = new SendOptions();
            sendOptions.SetDestination("ChatBox");


            return requestingEndpoint.Request<ServiceBusResponse>(request, sendOptions).
                ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private ServiceBusResponse getChatHistory(ChatServiceRequest request)
        {


            SendOptions sendOptions = new SendOptions();
            sendOptions.SetDestination("ChatBox");


            return requestingEndpoint.Request<ServiceBusResponse>(request, sendOptions).
                ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private ServiceBusResponse sendMessage(ChatServiceRequest request)
        {


            SendOptions sendOptions = new SendOptions();
            sendOptions.SetDestination("ChatBox");


            return requestingEndpoint.Request<ServiceBusResponse>(request, sendOptions).
                ConfigureAwait(false).GetAwaiter().GetResult();
        }


    }
}
