using ClientApplicationMVC.Models;

using Messages.NServiceBus.Commands;
using Messages.DataTypes;
using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.Authentication.Requests;

using System.Web.Mvc;

namespace ClientApplicationMVC.Controllers
{
    /// <summary>
    /// This class contains the functions responsible for handling requests routed to *Hostname*/Authentication/*
    /// </summary>
    public class AuthenticationController : Controller
    {
        /// <summary>
        /// The default method for this controller
        /// </summary>
        /// <returns>The login page</returns>
        public ActionResult Index()
        {
            ViewBag.Message = "Please enter your username and password.";
            return View("Index");
        }
		
		//This class is incomplete and should be completed by the students in milestone 2
		//Hint: You will need to make use of the ServiceBusConnection class. See EchoController.cs for an example.

        public ActionResult Login(LogInRequest login)
        {
            //Do stuff with the new Login Info
            LogInRequest request = login;
            ServiceBusResponse response;
            ServiceBusConnection connection = ConnectionManager.getConnectionObject(Globals.getUser());
            if (connection == null)
            {
                response = ConnectionManager.sendLogIn(request);
            }
            else
            {
                response = connection.sendLogIn(request);
            }

            ViewBag.AsIsResponse = response.response;
            //Do things with this response (if true then yay if not then nay :(   )



            return RedirectToAction("Index", "Home");
        }

        //Opens the page for creating a new account
        public ActionResult CreateAccount()
        {
            return View();
        }

        public ActionResult SaveAccount(CreateAccount account)
        {
            //Save this account info
            CreateAccountRequest request = new CreateAccountRequest(account);
            ServiceBusResponse response;
            ServiceBusConnection connection = ConnectionManager.getConnectionObject(Globals.getUser());
            if (connection == null)
            {
                response = ConnectionManager.sendNewAccountInfo(request);
            }
            else
            {
                response = connection.sendNewAccountInfo(request);
            }

            ViewBag.AsIsResponse = response.response;
            //Do things with this response (if true then yay if not then nay :(   )
            return RedirectToAction("Index" , "Home");
        }

        public ActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }

    }
}