using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCMSP_MVC.Models;

namespace PCMSP_MVC.Controllers
{
    public class userCustomerController : Controller
    {
        [System.Web.Http.HttpPost]
        public ActionResult submitContact(Customer viewModel)
        {
            return Content("1");
        }



    }


}