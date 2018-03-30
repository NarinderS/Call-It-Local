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
using System.Threading.Tasks;
using WebApplication1.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            ViewBag.username = Globals.getUser();
            ViewBag.time = DateTime.Now.ToString();


            //Harjee format the string into an array or something, then display it nicely on the view
            string reviews = GetReview(value.companyName);
            if (!reviews.Contains(":]"))
            {

                JObject json = JObject.Parse(reviews);

                JProperty allReviews = json.Property("reviews");

                string totalReviews = allReviews.Value.ToString();
                string[] unformattedResults = totalReviews.Split(',');

                for (int i = 4; i < unformattedResults.Length; i += 5)
                {
                    int position = unformattedResults[i].IndexOf('}');

                    if (position < 0)
                    {
                        position = unformattedResults[i].IndexOf(']');
                    }

                    if (position >= 0)
                    {
                        unformattedResults[i] = unformattedResults[i].Substring(0, position);
                    }
                }
                if (unformattedResults.Length > 2)
                {
                    for (int i = 0; i < unformattedResults.Length; i += 5)
                    {
                        int reviewNumber = (i / 5) + 1;
                        string[] temp = unformattedResults[i + 1].Split(':');
                        ViewBag.reviews += "Review #" + reviewNumber + ": ";
                        ViewBag.reviews += temp[1];
                        ViewBag.reviews += " <br/> ";

                        temp = unformattedResults[i + 2].Split(':');
                        ViewBag.reviews += "Stars: ";
                        ViewBag.reviews += temp[1];
                        ViewBag.reviews += " <br/> ";

                        temp = unformattedResults[i + 3].Split(':');
                        ViewBag.reviews += "Timestamp: ";
                        ViewBag.reviews += temp[1];
                        ViewBag.reviews += " <br/> ";

                        temp = unformattedResults[i + 4].Split(':');
                        ViewBag.reviews += "Username: ";
                        ViewBag.reviews += temp[1];

                        ViewBag.reviews += " <br/> <br/> ";
                    }

                    ViewBag.reviews += " <br/> ";
                }
            }

            else
            {
                ViewBag.reviews = " No reviews found <br/> ";
            }

            return View("DisplayCompany");
        }


        // POST: CompanyReview
        [HttpPost]
        public ActionResult PostCompanyReview(PostReview PostReview)
        {
            //Harjee can you finish this?
            ViewBag.Companyreviewpost = PostReview.toString();
            string result = this.PostReview(PostReview);
            /*
            HttpClient client = new HttpClient();

            String json = "{\"companyName\":\"" + ViewBag.CompanyName + "\",\"review\":\"" + Request["review"] + "\",\"stars\":\"" + Request["star"] + "\",timestamp\":\"" + DateTimeOffset.Now + "\",username\":\"" + Globals.getUser() + "\"}";

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            Debug.consoleMsg("The value of content going into the POST request is: " + content);

            // TODO: Put the actual URI for the assignment 4 instance here
            var result = client.PostAsync("http:///home/SaveComanyReview/", content).Result;

            if (result.IsSuccessStatusCode)
            {
                ViewBag.DM2 = "Post was successful";
            }

            else
            {
                ViewBag.DM2 = "Post was unsuccessful";
            }
            */

            //ViewBag.Companyreviewpost = review.toString();
            //ViewBag.Companyreviewpost = review.companyName;

            return View();
        }

        //TEST FUNCTION USED BY SADAT
        public ActionResult GetCompanyReview()
        {

            PostReview review = new PostReview()
            {
                companyName = "Google",
                review = "Please work godDDDD",
                stars = 5,
                timestamp = "020202",
                username = "Sadat"
                

            };
            ViewBag.SADAT = PostReview(review);

            return View();
        }

        public ActionResult CreateReview()
        {
            return View();
        }
        
        

        public string GetReview(string companyName)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://35.188.34.108/home/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            //HttpContent content = new StringContent(postBody, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.GetAsync("GetCompanyReview/" + companyName).Result;

            // Read the response body as string
            string json = response.Content.ReadAsStringAsync().Result;
            return json;

            // deserialize the JSON response returned from the Web API back to a login_info object
            //return JsonConvert.DeserializeObject<Review>(json);
        }

        public string PostReview(PostReview review)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://35.188.34.108/home/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string postBody = JsonConvert.SerializeObject(review);
            HttpContent content = new StringContent(postBody, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("SaveCompanyReview/", content).Result;

            // Read the response body as string
            string json = response.Content.ReadAsStringAsync().Result;
            return json;

            // deserialize the JSON response returned from the Web API back to a login_info object
            //return JsonConvert.DeserializeObject<Review>(json);
        }

        /*
        static async Task<Review> GetProductAsync(string path)
        {
            HttpClient client = new HttpClient();
            Review product = null;
            HttpResponseMessage response = await client.GetAsync("http://35.226.124.97/home/GetCompanyReview/Google");
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Review>();
            }
            return product;
        }*/

        /*
        async Task<string> GetResponseString(string compName)
        {
            var client = new HttpClient();

            else

            {
                ViewBag.DM1 = "GET Request was unsuccessful";
                ViewBag.Companyreviews = result;
            }

            return View();
        }
        */
    }
}