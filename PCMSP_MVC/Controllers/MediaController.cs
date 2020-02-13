using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
using Newtonsoft.Json;

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
            PDBC db=new PDBC("DBConnectionString", true);
            db.Connect();
            int num = 0;
            if (tags.Equals("همه"))
            {
                if (category.Equals("همه"))
                {
                    num = Convert.ToInt32(db.Select("SELECT Count(*) FROM[Post_tbl]").Rows[0][0]);
                }
                else
                {
                    num = Convert.ToInt32(db.Select("SELECT COUNT(*) FROM[Post_tbl] INNER JOIN [Categories_tbl] ON [Post_tbl].Id=[Categories_tbl].PostId AND [Categories_tbl].name LIKE N'" + category + "'").Rows[0][0]);
                }
            }
            else
            {
                num = Convert.ToInt32(db.Select("SELECT COUNT(*)FROM[Post_tbl] INNER JOIN [Tags_tbl] ON [Post_tbl].Id=[Tags_tbl].[PostId] AND [Tags_tbl].[Name] LIKE N'"+tags+"'").Rows[0][0]);
            }



            if (num % 5 == 0)
            {
                num = num / 5;
            }
            else
            {
                num = (num / 5) + 1;
            }

            List<int> p=new List<int>();
            if (pages != 1)
            {
                for (int i = pages - 1; i <= num - (pages - 2); i++)
                {
                    p.Add(i);
                }
            }
            else
            {
                for (int i = 1; i <= num ; i++)
                {
                    p.Add(i);
                }
            }


            ModelFiller MF =new ModelFiller();

            var News = MF.News_withQuery_filler(category,tags,pages,num);
            var newsModelView = new NewsModelView()
            {
                NewsModels = News,
                LatestNewsModels= MF.LatestNewsModels_filler(),
                popular = MF.Popular_filler(),
                CategoryModels = MF.CategoryModels_filler(),
                AllTags = MF.AllTagsFiller(News),
                pages = new PageSeparateModel()
                {
                    category = category,
                    Tags = tags,
                    pages = p,
                    CurrentPage = pages
                }
            };

            
            
            return View(newsModelView);
        }

        public ActionResult News_Details(int id_news, string category, string title,int pre,int next)
        {
            ModelFiller MF = new ModelFiller();

            NewsDetails_ModelView modelView = new NewsDetails_ModelView()
            {
                NewsModel = MF.NewsModel_Detail_filler(id_news,pre,next),
                LatestNewsModels = MF.LatestNewsModels_filler(),
                popular = MF.Popular_filler(),
                CategoryModels = MF.CategoryModels_filler()
            };

            return View(modelView);
        }

        public ActionResult search(string searchText)
        {
            ModelFiller MF = new ModelFiller();

            var models=MF.Search_filler(searchText);
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < models.Count; i++)
            {
                result.Append("<li> <article itemscope=\"\" itemtype=\"http://schema.org/NewsArticle\" class=\"latest-news-item\"> <header> <div class=\"post-thumb\" style=\" background-image: url('");
                result.Append(models[i].ImagePath);
                result.Append(
                    "'); background-size: cover; background-position: center; background-repeat: no-repeat;\"> </div><div class=\"post-additional-info\"> <h6 class=\"post__title entry-title\" itemprop=\"name\"> <a href=\"@Url.Action(\"News_Details\",\"Media\",new{id_news=");
                result.Append(models[i].Id);
                result.Append(",category=");
                result.Append(models[i].Category);
                result.Append(",title=");
                result.Append(models[i].title);
                result.Append(",pre=0, next=0})\">");
                result.Append(models[i].title);
                result.Append(
                    "</a> </h6> <span class=\"post__date\"> <time class=\"entry-date published updated\" datetime=\"2017-03-23T16:31:34+00:00\">");
                result.Append(models[i].date);
                result.Append("</time> </span> </div></header> </article></li>");
                    
            }

            return Content(result.ToString());
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

        public ActionResult getSearch(string st)
        {
            ModelFiller MF = new ModelFiller();
            ListToModel LM=new ListToModel();
            LM.search= MF.Search_filler(st);
            var models = LM;


            return View(models);
        }
    }
}