using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ClientApplicationMVC.Models;
using Messages;

namespace ClientApplicationMVC.Controllers
{
    public class CompanyReviewController : Controller
    {
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

            if(result.IsSuccessStatusCode)
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

            return View("DisplayCompany");
        }
    }
}