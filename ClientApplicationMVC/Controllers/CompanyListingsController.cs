using ClientApplicationMVC.Models;

using Messages.DataTypes.Database.CompanyDirectory;
using Messages.ServiceBusRequest.CompanyDirectory.Responses;
using Messages.ServiceBusRequest.CompanyDirectory.Requests;
using System.Web.Mvc;
using System.Net.Http;
using System;
using System.Web.Routing;
using Messages.ServiceBusRequest;
using Messages;
using System.Text;

namespace ClientApplicationMVC.Controllers
{
    /// <summary>
    /// This class contains the functions responsible for handling requests routed to *Hostname*/CompanyListings/*
    /// </summary>
    public class CompanyListingsController : Controller
    {
        /// <summary>
        /// This function is called when the client navigates to *hostname*/CompanyListings
        /// </summary>
        /// <returns>A view to be sent to the client</returns>
        public ActionResult Index()
        {
            if (Globals.isLoggedIn())
            {
                ViewBag.Companylist = null;

                return View("Index");
            }
            ViewBag.DebugMessage = "User Is not logged in (global)";
            return View("Index");
        }

        /// <summary>
        /// This function is called when the client navigates to *hostname*/CompanyListings/Search
        /// </summary>
        /// <returns>A view to be sent to the client</returns>
        public ActionResult Search(string textCompanyName)
        {
            ViewBag.DebugMessage = "Point 1";

            if (Globals.isLoggedIn() == false)
            {
                ViewBag.DM = "User Is not logged in";
                return View("Index");
            }

            ViewBag.DebugMessage = "Point 2";


            ServiceBusConnection connection = ConnectionManager.getConnectionObject(Globals.getUser());
            if (connection == null)
            {
                ViewBag.DM = "Connection is null";
                return View("Index");
            }

            ViewBag.DebugMessage = "Point 3";


            CompanySearchRequest request = new CompanySearchRequest(textCompanyName);

            ServiceBusResponse response = connection.searchCompanyByName(request);
            if (response.result == false)
            {
                ViewBag.DM = response.response;
                return View("Index");
            }

            ViewBag.DebugMessage = "Point 4";



            ViewBag.Companylist = response.response.Split(new String[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            ViewBag.DM = response.response;
            return View("Index");
        }

        /// <summary>
        /// This function is called when the client navigates to *hostname*/CompanyListings/DisplayCompany/*info*
        /// </summary>
        /// <param name="id">The name of the company whos info is to be displayed</param>
        /// <returns>A view to be sent to the client</returns>
        public ActionResult DisplayCompany(string id)
        {
            if (Globals.isLoggedIn() == false)
            {
                return RedirectToAction("Index", "Authentication");
            }
            if ("".Equals(id))
            {
                return View("Index");
            }

            ServiceBusConnection connection = ConnectionManager.getConnectionObject(Globals.getUser());
            if (connection == null)
            {
                return RedirectToAction("Index", "Authentication");
            }

            ViewBag.CompanyName = id;

            GetCompanyInfoRequest infoRequest = new GetCompanyInfoRequest(new CompanyInstance(id));
            ServiceBusResponse infoResponse = connection.getCompanyInfo(infoRequest);




            String[] responseToArray = infoResponse.response.Split(new String[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            String[] locations = new String[responseToArray.Length - 3];

            Array.Copy(responseToArray, 3, locations, 0, responseToArray.Length - 3);
            CompanyInstance value = new CompanyInstance(responseToArray[0], responseToArray[1], responseToArray[2], locations);

            ViewBag.CompanyInfo = value;

            return View("DisplayCompany");
        }


        // POST: CompanyReview
        [HttpPost]
        public ActionResult PostCompanyReview()
        {
            HttpClient client = new HttpClient();

            String json = "{\"companyName\":\"" + ViewBag.CompanyName + "\",\"review\":\"" + Request["review"] + "\",\"stars\":\"" + Request["star"] + "\",timestamp\":\"" + DateTimeOffset.Now + "\",username\":\"" + Globals.getUser() + "\"}";

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            Debug.consoleMsg("The value of content going into the POST request is: " + content);

            // TODO: Put the actual URI for the assignment 4 instance here
            var result = client.PostAsync("http://35.226.124.97/home/SaveComanyReview/", content).Result;

            if (result.IsSuccessStatusCode)
            {
                ViewBag.DM2 = "Post was successful";
            }

            else
            {
                ViewBag.DM2 = "Post was unsuccessful";
            }

            ViewBag.Companyreviewpost = result.Content.ToString();

            return View("DisplayCompany");
        }

        // GET: Company Reviews
        public ActionResult GetCompanyReview()
        {
            HttpClient client = new HttpClient();
            // TODO: Put actual URI of assignment 4
            var result = client.GetAsync("http://35.226.124.97/home/GetCompanyReview/" + ViewBag.CompanyName);

            Debug.consoleMsg("The value of companyName going into the GET request is: " + ViewBag.CompanyName);

            ViewBag.DM1 = "GET Request executed, results are below:";

            var body = result.Result;
            var returnValue = body.ToString();

            ViewBag.Companyreviews = returnValue;

            //return View("DisplayCompany");
            return View();
        }
    }
}