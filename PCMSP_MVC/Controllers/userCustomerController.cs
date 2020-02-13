using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCMSP_MVC.DBConnect;
using PCMSP_MVC.Models;
using PCMSP_MVC.Other;

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

        [System.Web.Http.HttpPost]
        public ActionResult SubmitComment(Comment viewModel)
        {
            PDBC db = new PDBC("DBConnectionString", true);
            db.Connect();
            var dateTimeStamp = new Date_TimeStamp();

            db.Script(
                "INSERT INTO [Comment_tbl]VALUES(N'"+viewModel.Email+"',N'"+viewModel.message+"',N'"+viewModel.Name+"',"+viewModel.PostId+",N'https://localhost:44331//Resources/Images/photo_2019-10-25_15-12-57.jpg',N'',"+dateTimeStamp.GetTime_Soconds(DateTime.Now)+")");
            return Content("1");
        }

    }


}