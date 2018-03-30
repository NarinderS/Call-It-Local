using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Review
    {
        public int id { get; set; }
        public string companyName { get; set; }
        public string review { get; set; }
        public int stars { get; set; }
        public string timestamp { get; set; }
        public string username { get; set; }
    }
}