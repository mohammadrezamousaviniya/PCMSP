using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using MD.PersianDateTime;
using PCMSP_MVC.DBConnect;
using PCMSP_MVC.Models;

namespace PCMSP_MVC.Other
{
    public class ModelFiller
    {
        private PDBC db;
        private Date_TimeStamp dateTimeStamp;
        public ModelFiller()
        {
            db = new PDBC("DBConnectionString", true);
            db.Connect();
            dateTimeStamp = new Date_TimeStamp();
        }

        public NewsModel NewsModel_Detail_filler(int id_news)
        {
            
            /////////news Model
            DataTable dt = db.Select(
                "SELECT [Id],[Title],[Text],[WrittenBy],[Date],[ImagePath],[ImageValue],[InGroup],[IsImportant]FROM [TSHP_PortalCMS].[dbo].[Post_tbl]WHERE Id=" + id_news);
            /////////////comments
            DataTable comment_dt =
                db.Select(
                    "SELECT [Id],[Email],[message],[Name],[PostId],[ImagePath],[ImageValue],[Date]FROM [TSHP_PortalCMS].[dbo].[Comment_tbl]WHERE PostId=" + id_news);

            List<Comment> comment_list = new List<Comment>();
            for (int i = 0; i < comment_dt.Rows.Count; i++)
            {
                DateTime date_comment = dateTimeStamp.GetDateTime_fromUnix(comment_dt.Rows[i]["Date"].ToString());
                PersianDateTime persian_comm = new PersianDateTime(date_comment);
                //////////////////replies
                DataTable reply_dt =
                    db.Select(
                        "SELECT [Id],[Email],[Name],[Message],[commentId],[ImagePath],[ImageValue],[Date]FROM [TSHP_PortalCMS].[dbo].[Reply_tbl]WHERE commentId=" +
                        comment_dt.Rows[i]["Id"]);
                List<Reply> rep = new List<Reply>();

                for (int j = 0; j < reply_dt.Rows.Count; j++)
                {
                    DateTime date_Rep = dateTimeStamp.GetDateTime_fromUnix(reply_dt.Rows[j]["Date"].ToString());
                    PersianDateTime persian_rep = new PersianDateTime(date_Rep);
                    var reply = new Reply()
                    {
                        Name = reply_dt.Rows[j]["Name"].ToString(),
                        Email = reply_dt.Rows[j]["Email"].ToString(),
                        Message = reply_dt.Rows[j]["Message"].ToString(),
                        ImagePath = reply_dt.Rows[j]["ImagePath"].ToString(),
                        Date = persian_rep.ToString()
                    };
                    rep.Add(reply);
                }

                //////////////////replies end


                var comment = new Comment()
                {
                    Name = comment_dt.Rows[i]["Name"].ToString(),
                    Email = comment_dt.Rows[i]["Email"].ToString(),
                    message = comment_dt.Rows[i]["message"].ToString(),
                    ImagePath = comment_dt.Rows[i]["ImagePath"].ToString(),
                    Date = persian_comm.ToString(),
                    Replies = rep
                };
                comment_list.Add(comment);
            }
            ///////////comments end

            /////////Tags
            DataTable Tags_dt = db.Select("SELECT[name]FROM [Tags_tbl]WHERE PostId=" + id_news);
            List<string> tagsList = new List<string>();
            for (int i = 0; i < Tags_dt.Rows.Count; i++)
            {
                tagsList.Add(Tags_dt.Rows[i]["name"].ToString());
            }
            /////////Tags end

            DateTime date = dateTimeStamp.GetDateTime_fromUnix(dt.Rows[0]["Date"].ToString());
            PersianDateTime persian = new PersianDateTime(date);
            var NewsModel1 = new NewsModel()
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
                Comments__ = comment_list.Count,
                Tags = tagsList,
                IsImportant = Convert.ToBoolean(dt.Rows[0]["IsImportant"])
            };
            /////////news Model end

            return NewsModel1;
        }

        public List<NewsModel> NewsModels_fillere(string Query)
        {
            
            DataTable dt = db.Select(Query);


            List<NewsModel> news = new List<NewsModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                /////////Tags
                DataTable Tags_dt = db.Select("SELECT[name]FROM [Categories_tbl]WHERE PostId=" + dt.Rows[i]["Id"]);
                List<string> tagsList = new List<string>();
                for (int j = 0; j < Tags_dt.Rows.Count; j++)
                {
                    tagsList.Add(Tags_dt.Rows[j]["name"].ToString());
                }
                /////////Tags end

                DateTime date = dateTimeStamp.GetDateTime_fromUnix(dt.Rows[i]["Date"].ToString());
                PersianDateTime persian = new PersianDateTime(date);
                news.Add(new NewsModel()
                {
                    Id = (int)dt.Rows[i]["Id"],
                    title = dt.Rows[i]["Title"].ToString(),
                    text_min = dt.Rows[i]["Text_min"].ToString(),
                    by = dt.Rows[i]["WrittenBy"].ToString(),
                    ImagePath = dt.Rows[i]["ImagePath"].ToString(),
                    day = persian.Day.ToString(),
                    year_month = persian.GetLongMonthName + "  " + persian.Year,
                    DayOfWeek = persian.GetLongDayOfWeekName,
                    InGroup = dt.Rows[i]["InGroup"].ToString(),
                    Comments__ = Convert.ToInt32(dt.Rows[i]["number"].ToString()),
                    Tags = tagsList,
                    IsImportant = Convert.ToBoolean(dt.Rows[i]["IsImportant"])

                });
            }
            ///////////end news

            return news;
        }

        public List<LatestNewsModel> LatestNewsModels_filler()
        {
            ///////latest news
            List<LatestNewsModel> latest = new List<LatestNewsModel>();
            DataTable latest_dt =
                db.Select(
                    "SELECT TOP 5 [Id],[Title],[Date],[ImagePath],[ImageValue] FROM [Post_tbl] order by(Date)DESC");

            for (int i = 0; i < latest_dt.Rows.Count; i++)
            {

                DateTime date = dateTimeStamp.GetDateTime_fromUnix(latest_dt.Rows[i]["Date"].ToString());
                PersianDateTime persianDateTime = new PersianDateTime(date);
                var Late = new LatestNewsModel()
                {
                    Id = Convert.ToInt32(latest_dt.Rows[i]["Id"]),
                    title = latest_dt.Rows[i]["Title"].ToString(),
                    date = persianDateTime.ToString(),
                    ImagePath = latest_dt.Rows[i]["ImagePath"].ToString()
                };
                latest.Add(Late);
            }
            //////// latest end

            return latest;
        }

        public List<LatestNewsModel> Popular_filler()
        {
            ///////popular news
            List<LatestNewsModel> Popular = new List<LatestNewsModel>();
            DataTable popular_dt =
                db.Select(
                    "SELECT TOP 5 [Id],[Title],[Date],[ImagePath],[ImageValue]FROM [Post_tbl] order by(SELECT count(*) FROM [Comment_tbl]WHERE PostId=[Post_tbl].[Id]) DESC,Date DESC");

            for (int i = 0; i < popular_dt.Rows.Count; i++)
            {

                DateTime date = dateTimeStamp.GetDateTime_fromUnix(popular_dt.Rows[i]["Date"].ToString());
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

            return Popular;

        }

        public List<string> AllTagsFiller(List<NewsModel> news)
        {
            List<string> tags=new List<string>();
            StringBuilder sT=new StringBuilder();
            
            for (int i = 0; i < news.Count; i++)
            {
                sT.Append(news[i].Id);
                if (i != news.Count - 1)
                {
                    sT.Append(",");
                }
            }
            DataTable dt=db.Select("SELECT distinct[name]FROM[Tags_tbl]WHERE PostId in("+sT.ToString()+")");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tags.Add(dt.Rows[i]["name"].ToString());
            }
            return tags;
        }

        public List<CategoryModel> CategoryModels_filler()
        {
            ///////Categories
            List<CategoryModel> category = new List<CategoryModel>();
            DataTable category_dt =
                db.Select(
                    "SELECT COUNT(*)as [count],[name] FROM [Categories_tbl] GROUP BY([name])");

            for (int i = 0; i < category_dt.Rows.Count; i++)
            {

                var cat = new CategoryModel()
                {
                    name = category_dt.Rows[i]["name"].ToString(),
                    count = Convert.ToInt32(category_dt.Rows[i]["count"])
                };
                category.Add(cat);
            }
            ///////////categories end
            return category;
        }

        public List<NewsModel> News_withQuery_filler(string category, string tags, int pages)
        {
            string FinalQuery = "";
            int num = 0;
            if (tags.Equals("همه"))
            {
                if (category.Equals("همه"))
                {
                    num = Convert.ToInt32(db.Select("SELECT Count(*) FROM[Post_tbl]").Rows[0][0]);





                    FinalQuery =
                        "select * from(SELECT NTILE("+num+")over(order by(Date)DESC)as tile, [Id],[Title],[Text_min],[Text],[WrittenBy],[Date],[ImagePath],[ImageValue],[InGroup],[IsImportant] ,(SELECT count(*) FROM [Comment_tbl]WHERE PostId=[Post_tbl].[Id])as number FROM[Post_tbl]) b where b.tile=" +
                        pages;
                }
                else
                {
                    FinalQuery =
                        "select * from(SELECT NTILE("+num+")over(order by(Date)DESC)as tile, [Post_tbl].Id,[Categories_tbl].name,[Post_tbl].[Title],[Post_tbl].[Text_min],[Post_tbl].[Text],[Post_tbl].[WrittenBy],[Post_tbl].[Date],[Post_tbl].[ImagePath],[Post_tbl].[ImageValue],[Post_tbl].[InGroup],[Post_tbl].[IsImportant] ,(SELECT count(*) FROM [Comment_tbl]WHERE PostId=[Post_tbl].[Id])as number FROM[Post_tbl] INNER JOIN [Categories_tbl] ON [Post_tbl].Id=[Categories_tbl].PostId AND [Categories_tbl].name LIKE N'"+category+"')b where b.tile="+pages;
                }
            }
            else
            {
                FinalQuery =
                    "select *from(SELECT NTILE("+num+")over(order by(Date)DESC)as tile,[Post_tbl].Id,[Tags_tbl].[Name],[Post_tbl].[Title],[Post_tbl].[Text_min],[Post_tbl].[Text],[Post_tbl].[WrittenBy],[Post_tbl].[Date],[Post_tbl].[ImagePath],[Post_tbl].[ImageValue],[Post_tbl].[InGroup],[Post_tbl].[IsImportant] ,(SELECT count(*) FROM [Comment_tbl]WHERE PostId=[Post_tbl].[Id])as number FROM[Post_tbl] INNER JOIN [Tags_tbl] ON [Post_tbl].Id=[Tags_tbl].[PostId] AND [Tags_tbl].[Name] LIKE N'" +
                    tags + "')b where b.tile=" + pages;
            }

            return NewsModels_fillere(FinalQuery);
        }
    }


}