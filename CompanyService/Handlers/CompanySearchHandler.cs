﻿using CompanyService.Database;
using Messages;
using Messages.DataTypes.Database.CompanyDirectory;
using Messages.NServiceBus.Events;
using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.CompanyDirectory.Requests;
using Messages.ServiceBusRequest.CompanyDirectory.Responses;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyService.Handlers
{
    class CompanySearchHandler : IHandleMessages<CompanySearchRequest>
    {
        /// <summary>
        /// This is a class provided by NServiceBus. Its main purpose is to be use log.Info() instead of Messages.Debug.consoleMsg().
        /// When log.Info() is called, it will write to the console as well as to a log file managed by NServiceBus
        /// </summary>
        /// It is important that all logger member variables be static, because NServiceBus tutorials warn that GetLogger<>()
        /// is an expensive call, and there is no need to instantiate a new logger every time a handler is created.
        static ILog log = LogManager.GetLogger<CompanySearchRequest>();

        /// <summary>
        /// Saves the echo to the database, reverses the data, and returns it back to the calling endpoint.
        /// </summary>
        /// <param name="message">Information about the echo</param>
        /// <param name="context">Used to access information regarding the endpoints used for this handle</param>
        /// <returns>The response to be sent back to the calling process</returns>
        public Task Handle(CompanySearchRequest message, IMessageHandlerContext context)
        {
            Debug.consoleMsg("Got to CompanySearchHandler");
            Debug.consoleMsg(message.searchDeliminator);
            //Save the echo to the database
            CompanyList temp = CompanyServiceDatabase.getInstance().searchCompanies(message.searchDeliminator);

            //Format the company list so it looks like what it is below, With ; dividing the different companies //NARINDER
            string response = CompanyServiceDatabase.companyListToString(temp);
            


            Debug.consoleMsg(response);
            //The context is used to give a reply back to the endpoint that sent the request
            return context.Reply(new CompanySearchResponse(true, response, temp));
        }
    }
}
