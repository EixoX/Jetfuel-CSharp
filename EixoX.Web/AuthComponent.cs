using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Web.AuthComponent
{
    //public abstract class AuthenticationBasedController<T> : Controller
    //{
    //    public abstract T CurrentUser { get; set; }
    //    public abstract string LoginUrl { get; }
    //    public bool IsLoggedIn => CurrentUser != null;
    //}

    public class AuthenticationRequired : Attribute
    {

    }

}
