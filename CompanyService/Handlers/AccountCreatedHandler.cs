
using CompanyService.Database;
using Messages;
using Messages.NServiceBus.Events;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyService.Handlers
{
    class AccountCreatedHandler: IHandleMessages<AccountCreated>
    {

        /// <summary>
        /// This is a class provided by NServiceBus. Its main purpose is to be use log.Info() instead of Messages.Debug.consoleMsg().
        /// When log.Info() is called, it will write to the console as well as to a log file managed by NServiceBus
        /// </summary>
        /// It is important that all logger member variables be static, because NServiceBus tutorials warn that GetLogger<>()
        /// is an expensive call, and there is no need to instantiate a new logger every time a handler is created.
        static ILog log = LogManager.GetLogger<AccountCreated>();

        public Task Handle(AccountCreated message, IMessageHandlerContext context)
        {

            
            CompanyServiceDatabase.getInstance().saveAccount(message);
            Debug.consoleMsg("Account has been saved");
            return Task.CompletedTask;
        }
    }
}
