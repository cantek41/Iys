﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iys.ModelProject;

namespace iys.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        public ActionResult Index()
        {
            return View();
        }
	}
}