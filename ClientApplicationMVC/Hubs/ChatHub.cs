using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Messages;
using Microsoft.AspNet.SignalR;

namespace ClientApplicationMVC.Hubs
{
    public class ChatHub : Hub
    {
        public void reloadPage()
        {
            Clients.All.reloadPage();
        }
    }
}