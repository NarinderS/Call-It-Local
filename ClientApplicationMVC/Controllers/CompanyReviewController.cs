using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientApplicationMVC.Controllers
{
    public class CompanyReviewController : Controller
    {
        // POST: CompanyReview
        [HttpPost]
        public ActionResult PostCompanyReview(String companyName, String review, String stars, String username)
        {
            return View();
        }
    }
}