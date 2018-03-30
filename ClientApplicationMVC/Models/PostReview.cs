using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientApplicationMVC.Models
{
    public class PostReview
    {
        public string companyName { get; set; }
        public string review { get; set; }
        public int stars { get; set; }
        public string timestamp { get; set; }
        public string username { get; set; }
    }
}