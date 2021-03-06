﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.ServiceBusRequest.Authentication.Requests
{
    [Serializable]
    public class LogInRequest : AuthenticationServiceRequest
    {

        /// <summary>
        /// The username being used to log in
        /// </summary>
        [Required]
        public string username { get; set; }

        /// <summary>
        /// The password being used to log in
        /// </summary>
        [Required]
        public string password { get; set; }

        public LogInRequest() : base(AuthenticationRequest.LogIn)
        {

        }
        public LogInRequest(string username, string password) : base(AuthenticationRequest.LogIn)
        {
            this.username = username;
            this.password = password;
        }

        
    }
}
