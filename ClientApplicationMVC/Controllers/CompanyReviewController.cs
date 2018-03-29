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

            String json = "{\"companyname\":\"" + companyName + "\",\"review\":\"" + review + "\",\"stars\":\"" + stars + "\",timestamp\":\"" + DateTimeOffset.Now + "\",username\":\"" + username + "\"}";
            
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // TODO: Put the actual URI for the assignment 4 instance here
            var result = client.PostAsync("http://localhost/api/Reviews/", content).Result;

            if(result.IsSuccessStatusCode)
            {

            }

            else
            {

            }

            return View();
        }

        // GET: Company Reviews
        public HttpResponseMessage GetCompanyReview(String companyName) 
        {
            HttpClient client = new HttpClient();

            // TODO: Put actual URI of assignment 4
            var result = client.GetAsync("http://localhost/api/Reviews/" + companyName);

            return result.Result;
        }
    }
}