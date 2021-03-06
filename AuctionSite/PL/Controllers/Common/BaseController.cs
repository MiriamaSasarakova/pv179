﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers.Common
{
    public class BaseController : Controller
    {
        public int UserId => User.Identity.GetUserId<int>();

        public ActionResult Error()
        {
            return Redirect("https://www.youtube.com/watch?v=DLzxrzFCyOs");
        }

        public ActionResult Denied()
        {
            return View("PermisionDenied");
        }

    }
}