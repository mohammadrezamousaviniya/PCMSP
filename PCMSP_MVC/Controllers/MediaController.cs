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

        public ActionResult News(string category,string tags,int pages)
        {


            ModelFiller MF=new ModelFiller();

            var News = MF.News_withQuery_filler(category,tags,pages);
            var newsModelView = new NewsModelView()
            {
                NewsModels = News,
                LatestNewsModels= MF.LatestNewsModels_filler(),
                popular = MF.Popular_filler(),
                CategoryModels = MF.CategoryModels_filler(),
                AllTags = MF.AllTagsFiller(News)
            };


            
            return View(newsModelView);
        }

        public ActionResult News_Details(int id_news,string category,string title)
        {
            ModelFiller MF=new ModelFiller();

            NewsDetails_ModelView modelView=new NewsDetails_ModelView()
            {
                NewsModel = MF.NewsModel_Detail_filler(id_news),
                LatestNewsModels = MF.LatestNewsModels_filler(),
                popular = MF.Popular_filler(),
                CategoryModels = MF.CategoryModels_filler()
            };




            return View(modelView);
        }

        public ActionResult test()
        {
            //Date_TimeStamp dateTimeStamp=new Date_TimeStamp();
            //var date = dateTimeStamp.GetTime_Soconds(DateTime.Now).ToString();
            //int i = 0;
            //bool n = int.TryParse("11",out i);
           
           var db = new PDBC("DBConnectionString", true);
            db.Connect();
            int count = Convert.ToInt32(db.Select("SELECT Count(*) FROM[Post_tbl]").Rows[0][0]);
            return Content(count.ToString());
        }
    }
}