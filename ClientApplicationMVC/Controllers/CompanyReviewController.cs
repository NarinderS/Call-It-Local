using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientApplicationMVC.Controllers
{
    public class CompanyReviewController : Controller
    {
        // POST: CompanyReview
        [HttpPost]
        public ActionResult PostCompanyReview(String companyName, String review, String stars, String username)
        {
            HttpClient client = new HttpClient();

            String json = "{\"companyName\":\"" + companyName + "\",\"review\":\"" + review + "\",\"stars\":\"" + stars + "\",timestamp\":\"" + DateTimeOffset.Now + "\",username\":\"" + username + "\"}";
            
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // TODO: Put the actual URI for the assignment 4 instance here
            var result = client.PostAsync("http://35.226.124.97/home/SaveComanyReview/", content).Result;

            if(result.IsSuccessStatusCode)
            {
                
            }

            else
            {

            }

            return View();
        }

        // GET: Company Reviews
        public ActionResult GetCompanyReview(String companyName) 
        {
            HttpClient client = new HttpClient();
            String request = "{companyName: \"" + companyName + "\"}";
            // TODO: Put actual URI of assignment 4
            var result = client.GetAsync("http://35.226.124.97/home/GetCompanyReview/" + request);

            var body = result.Result;
            var returnValue = body.ToString();
            ViewBag.RESULTS = returnValue;
            return View();
        }
    }
}