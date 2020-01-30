using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCMSP_MVC.DBConnect;
using PCMSP_MVC.Models;

namespace PCMSP_MVC.Controllers
{
    public class userCustomerController : Controller
    {
        [System.Web.Http.HttpPost]
        public ActionResult submitContact(Customer viewModel)
        {

            PDBC db = new PDBC("DBConnectionString", true);
            db.Connect();
            db.Script("INSERT INTO [dbo].[REQForm_SocialNormalForms]VALUES(N'"+ viewModel.name + "',N'"+ viewModel.email + "',N'"+ viewModel.phone + "',N'"+ viewModel.subject + "',N'"+ viewModel.message + "')");

            db.DC();
           

            return Content("1");
        }



    }


}