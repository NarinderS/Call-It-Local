using Messages;
using Messages.ServiceBusRequest.Chat.Requests;
using Messages.ServiceBusRequest.CompanyDirectory.Requests;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Handlers
{
    class GetChatContactsHandler : IHandleMessages<GetChatContactsRequest>
    {

        static ILog log = LogManager.GetLogger<GetChatContactsRequest>();

        public Task Handle(GetChatContactsRequest message, IMessageHandlerContext context)
        {
            Debug.consoleMsg("Got to GetChatContactHandler");

            return null;

        }
    }
    
}
