using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using MailKit.Net.Smtp;
using System.Web.Http;
using System.Web.Mvc;
using DataBaseConnector;
using MimeKit;
using PCMSP_MVC.Models;
using PCMSP_MVC.ModelView;
using PCMSP_MVC.Modules.Email;
using PCMSP_MVC.Other;
using MD.PersianDateTime;

namespace PCMSP_MVC.Controllers
{
    public class MediaController : Controller
    {
        public ActionResult Email()
        {
            return Content("Email");
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            List<CustomerMessage> customer = new List<CustomerMessage>();
            PDBC db=new PDBC("DBConnectionString",true);
            db.Connect();
            DataTable dt= db.Select(
                "SELECT [name],[Job],[message],[star],[ImagePath],[ImageValue]FROM [TSHP_PortalCMS].[dbo].[CustomersMessge_tbl]");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var cust=new CustomerMessage()
                {
                    Name = dt.Rows[i]["name"].ToString(),
                    Job = dt.Rows[i]["Job"].ToString(),
                    Message = dt.Rows[i]["message"].ToString(),
                    Stars = (int)dt.Rows[i]["star"]
                };

                customer.Add(cust);
            }


            List<TeamMembers> members=new List<TeamMembers>();

            DataTable dt2 =
                db.Select(
                    "SELECT[Name],[Job],[Tozihat],[github],[Linkedin],[Instagram],[ImagePath],[ImageValue]FROM [TSHP_PortalCMS].[dbo].[TeamMembers_tbl]");

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                var member=new TeamMembers()
                {
                    name = dt2.Rows[i]["Name"].ToString(),
                    Job = dt2.Rows[i]["Job"].ToString(),
                    Tozihat = dt2.Rows[i]["Tozihat"].ToString(),
                    Github = dt2.Rows[i]["github"].ToString(),
                    linkedin = dt2.Rows[i]["Linkedin"].ToString(),
                    Instagram = dt2.Rows[i]["Instagram"].ToString(),
                    ImagePath = dt2.Rows[i]["ImagePath"].ToString(),
                    //  ImageValue = dt2.Rows[i]["ImageValue"].ToString()
                };

                members.Add(member);
            }

            
            var About__Us=new AboutUsModelView()
            {
                customerMessages = customer,
                teamMemberses = members
            };
            return View(About__Us);
        }

        public ActionResult News()
        {
            var Datee = DateTime.Now;
            
            PersianDateTime persian=new PersianDateTime(Datee);
            
            
            
            List<NewsModel> news=new List<NewsModel>();
            for (int i = 0; i < 5; i++)
            {
                news.Add(new NewsModel()
                {
                    title = "موضوع"+i,
                    text="متن"+i,
                    by = "نیکی",
                    ImagePath = "~/Resources/Images/Screenshot(106).png",
                    day = persian.Day.ToString(),
                    year_month = persian.GetLongMonthName+"  "+ persian.Year,
                    DayOfWeek = persian.GetLongDayOfWeekName

                });
            }

            var newsModelView = new NewsModelView()
            {
                NewsModels = news
            };
            return View(newsModelView);
        }

        public ActionResult News_Details()
        {
            var Datee = DateTime.Now;

            PersianDateTime persian = new PersianDateTime(Datee);

            var NewsModel1=new NewsModel()
            {
                title = "heloo",
                text="<h1>html</h1>"
            };
            //var NewsModel = new NewsModel()
            //{
            //    title = "موضوع" + 1,
            //    text = "متن" + 1,
            //    by = "نیکی",
            //    ImagePath = "~/Resources/Images/Screenshot(106).png",
            //    day = persian.Day.ToString(),
            //    year_month = persian.GetLongMonthName + "  " + persian.Year,
            //    DayOfWeek = persian.GetLongDayOfWeekName
            //};
        return View(NewsModel1);
        }

        public ActionResult test()
        {
            return View();
        }
    }
}