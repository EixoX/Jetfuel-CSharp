using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace EixoX.Web.AuthComponent
{
    public abstract class AuthenticationBasedController<T> : Controller
    {
        public abstract T CurrentUser { get; set; }
        public abstract string LoginUrl { get; }
        public bool IsLoggedIn
        {
            get
            {
                return CurrentUser != null;
            }
        }
    }
}