using Messages;
using Messages.ServiceBusRequest.Chat.Requests;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Handlers
{
    
    class SendMessageHandler : IHandleMessages<SendMessageRequest>
    {

        static ILog log = LogManager.GetLogger<SendMessageRequest>();

        public Task Handle(SendMessageRequest message, IMessageHandlerContext context)
        {
            Debug.consoleMsg("Got to SendMessageRequestHandler");

            return null;

        }
    }
}
