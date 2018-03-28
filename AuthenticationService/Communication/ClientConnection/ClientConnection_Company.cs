using Messages.NServiceBus.Events;
using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.CompanyDirectory;
using Messages.ServiceBusRequest.CompanyDirectory.Requests;
using Messages.ServiceBusRequest.Echo;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Communication
{
    /// <summary>
    /// This portion of the class contains methods specifically for accessing the echo service.
    /// </summary>
    partial class ClientConnection
    {


        /// <summary>
        /// Listens for the client to secifify which task is being requested from the echo service
        /// </summary>
        /// <param name="request">Includes which task is being requested and any additional information required for the task to be executed</param>
        /// <returns>A response message</returns>
        private ServiceBusResponse companyRequest(CompanyDirectoryServiceRequest request)
        {
            switch (request.requestType)
            {
                case (CompanyDirectoryRequest.CompanySearch):
                    return companySearch((CompanySearchRequest)request);
                case (CompanyDirectoryRequest.GetCompanyInfo):
                    return companyInfo((GetCompanyInfoRequest)request);
                default:
                    return new ServiceBusResponse(false, "Error: Invalid Request. Request received was:" + request.requestType.ToString());
            }
        }

        /// <summary>
        /// Publishes an EchoEvent.
        /// </summary>
        /// <param name="request">The data to be echo'd back to the client</param>
        /// <returns>The data sent by the client</returns>
        private ServiceBusResponse companySearch(CompanySearchRequest request)
        {
            CompanyEvent companyEvent = new CompanyEvent
            {
                data = request.searchDeliminator,
                username = username
            };

            SendOptions sendOptions = new SendOptions();
            sendOptions.SetDestination("Company");


            
            return requestingEndpoint.Request<ServiceBusResponse>(request, sendOptions).
                ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private ServiceBusResponse companyInfo(GetCompanyInfoRequest request)
        {


            SendOptions sendOptions = new SendOptions();
            sendOptions.SetDestination("Company");


            return requestingEndpoint.Request<ServiceBusResponse>(request, sendOptions).
                ConfigureAwait(false).GetAwaiter().GetResult();
        }


    }
}
