﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GitHub.Controllers
{
    public class GigsController : Controller
    {
        // GET: Gigs
        public ActionResult Create()
        {
            return View();
        }
    }
}