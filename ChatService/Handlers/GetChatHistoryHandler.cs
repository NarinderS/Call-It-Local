using ChatService.Database;
using Messages;
using Messages.ServiceBusRequest.Chat.Requests;
using Messages.ServiceBusRequest.Chat.Responses;
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

            //Error Checking
            string response = "Success";
            var returnVal = ChatServiceDatabase.getInstance().getChatHistory(message.getCommand.history.user1, message.getCommand.history.user2);
            Debug.consoleMsg("Leaving to GetChatHistoryHandler");
            return context.Reply(new GetChatHistoryResponse(true, response, returnVal));

        }
    }
}
