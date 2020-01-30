using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCMSP_MVC.DBConnect;
using PCMSP_MVC.Modules.Email;

namespace PCMSP_MVC.Controllers
{
    public class testController : Controller
    {
        // GET: test

        


        public ActionResult Email()
        {
            PDBC db=new PDBC("DBConnectionString",true);

            db.Connect();
            DataTable dt = db.Select("SELECT [Id],[EmailAddress] as Email FROM [dbo].[EmailModule_tbl]");
            SendEmail se=new SendEmail();
            se.EmailTask(dt, "FatemehNikaeen@tshpanda.com", "h1Sp8r_1", "htmltest", "","panda",false);

            return Content("sent");
        }
    }
}