using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using PCMSP_MVC.Models;

namespace PCMSP_MVC.Controllers
{
    public class MediaController : Controller
    {
        public ActionResult index()
        {
            return View();
        }


    }
}