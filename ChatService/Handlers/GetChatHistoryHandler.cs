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
    
    class GetChatHistoryHandler : IHandleMessages<GetChatHistoryRequest>
    {

        static ILog log = LogManager.GetLogger<GetChatHistoryRequest>();

        public Task Handle(GetChatHistoryRequest message, IMessageHandlerContext context)
        {
            Debug.consoleMsg("Got to GetChatHistoryHandler");

            return null;

        }
    }
}
