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

            Date_TimeStamp dateTimeStamp = new Date_TimeStamp();
            PDBC db = new PDBC("DBConnectionString", true);
            db.Connect();
            DataTable dt = db.Select(
                "SELECT [Id],[Title],[Text_min],[Text],[WrittenBy],[Date],[ImagePath],[ImageValue],[InGroup] ,(SELECT count(*) FROM [Comment_tbl]WHERE PostId=[Post_tbl].[Id])as number FROM [TSHP_PortalCMS].[dbo].[Post_tbl]");


            List<NewsModel> news=new List<NewsModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DateTime date = dateTimeStamp.GetDateTime_fromUnix(dt.Rows[i]["Date"].ToString());
                PersianDateTime persian = new PersianDateTime(date);
                news.Add(new NewsModel()
                {
                    Id =(int)dt.Rows[i]["Id"],
                    title = dt.Rows[i]["Title"].ToString(),
                    text_min= dt.Rows[i]["Text_min"].ToString(),
                    by = dt.Rows[i]["WrittenBy"].ToString(),
                    ImagePath = dt.Rows[i]["ImagePath"].ToString(),
                    day = persian.Day.ToString(),
                    year_month = persian.GetLongMonthName+"  "+ persian.Year,
                    DayOfWeek = persian.GetLongDayOfWeekName,
                    InGroup = dt.Rows[i]["InGroup"].ToString(),
                    Comments__ = Convert.ToInt32(dt.Rows[i]["number"].ToString())

                });
            }
            ///////////end news
            
            ///////latest news
            List<LatestNewsModel> latest=new List<LatestNewsModel>();
            DataTable latest_dt =
                db.Select(
                    "SELECT TOP 5 [Id],[Title],[Date],[ImagePath],[ImageValue] FROM [Post_tbl] order by(Date)DESC");

            for (int i = 0; i < latest_dt.Rows.Count; i++)
            {

                DateTime date = dateTimeStamp.GetDateTime_fromUnix(dt.Rows[i]["Date"].ToString());
                PersianDateTime persianDateTime=new PersianDateTime(date);
                var Late=new LatestNewsModel()
                {
                    Id =Convert.ToInt32(latest_dt.Rows[i]["Id"]),
                    title = latest_dt.Rows[i]["Title"].ToString(),
                    date = persianDateTime.ToString(),
                    ImagePath = latest_dt.Rows[i]["ImagePath"].ToString()
                };
                latest.Add(Late);
            }
            //////// latest end

            ///////popular news
            List<LatestNewsModel> Popular = new List<LatestNewsModel>();
            DataTable popular_dt =
                db.Select(
                    "SELECT TOP 5 [Id],[Title],[Date],[ImagePath],[ImageValue]FROM [Post_tbl] order by(SELECT count(*) FROM [Comment_tbl]WHERE PostId=[Post_tbl].[Id]) DESC,Date DESC");

            for (int i = 0; i < popular_dt.Rows.Count; i++)
            {

                DateTime date = dateTimeStamp.GetDateTime_fromUnix(dt.Rows[i]["Date"].ToString());
                PersianDateTime persianDateTime = new PersianDateTime(date);
                var Late = new LatestNewsModel()
                {
                    Id = Convert.ToInt32(popular_dt.Rows[i]["Id"]),
                    title = popular_dt.Rows[i]["Title"].ToString(),
                    date = persianDateTime.ToString(),
                    ImagePath = popular_dt.Rows[i]["ImagePath"].ToString()
                };
                Popular.Add(Late);
            }
            //////// popular end

            ///////Categories
            List<CategoryModel> category = new List<CategoryModel>();
            DataTable category_dt =
                db.Select(
                    "SELECT COUNT(*)as [count],[name] FROM [Categories_tbl] GROUP BY([name])");

            for (int i = 0; i < popular_dt.Rows.Count; i++)
            {

                DateTime date = dateTimeStamp.GetDateTime_fromUnix(dt.Rows[i]["Date"].ToString());
                PersianDateTime persianDateTime = new PersianDateTime(date);
                var Late = new LatestNewsModel()
                {
                    Id = Convert.ToInt32(popular_dt.Rows[i]["Id"]),
                    title = popular_dt.Rows[i]["Title"].ToString(),
                    date = persianDateTime.ToString(),
                    ImagePath = popular_dt.Rows[i]["ImagePath"].ToString()
                };
                Popular.Add(Late);
            }
///////////categories end



            var newsModelView = new NewsModelView()
            {
                NewsModels = news,
                LatestNewsModels= latest,
                popular = Popular
            };


            
            return View(newsModelView);
        }

        public ActionResult News_Details(int id_news,string category,string title)
        {
            Date_TimeStamp dateTimeStamp = new Date_TimeStamp();
            PDBC db = new PDBC("DBConnectionString", true);
            db.Connect();
            DataTable dt = db.Select(
                "SELECT [Id],[Title],[Text],[WrittenBy],[Date],[ImagePath],[ImageValue],[InGroup]FROM [TSHP_PortalCMS].[dbo].[Post_tbl]WHERE Id="+id_news);
            /////////////comments
            DataTable comment_dt =
                db.Select(
                    "SELECT [Id],[Email],[message],[Name],[PostId],[ImagePath],[ImageValue]FROM [TSHP_PortalCMS].[dbo].[Comment_tbl]WHERE PostId=" + id_news);
            List<Comment> comment_list = new List<Comment>();
            for (int i = 0; i < comment_dt.Rows.Count; i++)
            {
                
                //////////////////replies
                DataTable reply_dt =
                    db.Select(
                        "SELECT [Id],[Email],[Name],[Message],[commentId],[ImagePath],[ImageValue]FROM [TSHP_PortalCMS].[dbo].[Reply_tbl]WHERE commentId=" +
                        comment_dt.Rows[i]["Id"]);
                List<Reply> rep=new List<Reply>();

                for (int j = 0; j < reply_dt.Rows.Count; j++)
                {
                    var reply=new Reply()
                    {
                        Name = reply_dt.Rows[i]["Name"].ToString(),
                        Email = reply_dt.Rows[i]["Email"].ToString(),
                        Message = reply_dt.Rows[i]["Message"].ToString(),
                        ImagePath = reply_dt.Rows[i]["ImagePath"].ToString()
                    };
                    rep.Add(reply);
                }

                //////////////////replies end
                

                var comment =new Comment()
                {
                    Name = comment_dt.Rows[i]["Name"].ToString(),
                    Email = comment_dt.Rows[i]["Email"].ToString(),
                    message = comment_dt.Rows[i]["message"].ToString(),
                    ImagePath = comment_dt.Rows[i]["ImagePath"].ToString(),
                    Replies = rep
                };
                comment_list.Add(comment);
            }
            ///////////comments end
            





            DateTime date = dateTimeStamp.GetDateTime_fromUnix(dt.Rows[0]["Date"].ToString());
            PersianDateTime persian = new PersianDateTime(date);
            var NewsModel1 =new NewsModel()
            {
                Id = (int)dt.Rows[0]["Id"],
                title = dt.Rows[0]["Title"].ToString(),
                text = dt.Rows[0]["Text"].ToString(),
                by = dt.Rows[0]["WrittenBy"].ToString(),
                ImagePath = dt.Rows[0]["ImagePath"].ToString(),
                day = persian.Day.ToString(),
                year_month = persian.GetLongMonthName + "  " + persian.Year,
                DayOfWeek = persian.GetLongDayOfWeekName,
                InGroup = dt.Rows[0]["InGroup"].ToString(),
                Comments = comment_list,
                Comments__ = comment_list.Count
            };
            
            
        return View(NewsModel1);
        }

        public ActionResult test()
        {
            Date_TimeStamp dateTimeStamp=new Date_TimeStamp();
            var date = dateTimeStamp.GetTime_Soconds(DateTime.Now).ToString();

            return Content( dateTimeStamp.GetDateTime_fromUnix(date).ToString());
        }
    }
}