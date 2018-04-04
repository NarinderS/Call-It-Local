using ChatService.Database;
using Messages;
using Messages.NServiceBus.Commands;
using Messages.ServiceBusRequest.Chat.Requests;
using Messages.ServiceBusRequest.Chat.Responses;
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


            //Error Checking
            string response = "Success";
            GetChatContacts returnVal = ChatServiceDatabase.getInstance().getChatContacts(message.getCommand.usersname);

            return context.Reply(new GetChatContactsResponse(true, response, returnVal));

        }
    }
    
}
