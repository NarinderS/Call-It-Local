using ChatService.Database;
using Messages;
using Messages.ServiceBusRequest;
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

            //Error Checking
            string response = "Success";
            ChatServiceDatabase.getInstance().saveMessage(message.message);
            Debug.consoleMsg("Leaving to SendMessageRequestHandler");
            return context.Reply(new ServiceBusResponse(true, response));

        }
    }
}
