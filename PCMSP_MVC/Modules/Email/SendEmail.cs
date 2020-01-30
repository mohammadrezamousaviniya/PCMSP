using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DataBaseConnector;
using Quartz;
using Quartz.Impl;

namespace PCMSP_MVC.Modules.Email
{
    public class SendEmail
    {
        public void AddToEmailList(DataTable dt)
        {
            PDBC db = new PDBC("DBConnectionString",true);
            db.Connect();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                db.Script("INSERT INTO [dbo].[EmailModule_tbl]VALUES('" + dt.Rows[i]["Email"] + "')");
            }
            db.DC();
        }

        public void AddNewTemplate(string TempName, string Html)
        {
            PDBC db = new PDBC("DBConnectionString", true);
            db.Connect();
            db.Script("INSERT INTO [dbo].[EmailTemplate_tbl]VALUES('" + TempName + "','" + Html + "')");

        }

        public async System.Threading.Tasks.Task EmailTask(
            DataTable dataTable, string EmailFrom, string Password, string Subject, string Body,string Name,bool IsHtml)
        {
            int html = 0;
            if (IsHtml)
            {
                html = 1;
            }



            AddToEmailList(dataTable);

            PDBC db = new PDBC("DBConnectionString", true);
            db.Connect();
            DataTable dt = db.Select("SELECT [Id],[EmailAddress]FROM [dbo].[EmailModule_tbl]");

            try
            {
                var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
                if (!scheduler.IsStarted)
                {
                    await scheduler.Start();
                }
                var job = JobBuilder.Create<EmailJob>()
                    .UsingJobData("_EmailFrom", EmailFrom)
                    .UsingJobData("_Password", Password)
                    .UsingJobData("Subject", Subject)
                    .UsingJobData("Body", Body)
                    .UsingJobData("Name", Name)
                    .UsingJobData("IsHtml",html)
                    .Build();


                var trigger = TriggerBuilder.Create()
                    .WithSimpleSchedule(s => s.WithIntervalInSeconds(5).WithRepeatCount(dt.Rows.Count / 100))
                    .StartNow()
                    .Build();

                await scheduler.ScheduleJob(job, trigger);
            }
            catch (Exception ex)
            {

            }
        }


        public async System.Threading.Tasks.Task EmailWithTemplate(
            DataTable dataTable, string EmailFrom, string Password, string Subject,  string Name,string templateName,List<string> varList)
        {
            
            AddToEmailList(dataTable);

            PDBC db = new PDBC("DBConnectionString", true);
            db.Connect();
            DataTable dt = db.Select("SELECT [Id],[EmailAddress]FROM [dbo].[EmailModule_tbl]");
            DataTable temp =db.Select("SELECT [HtmlCode]FROM [TSHP_PortalCMS].[dbo].[EmailTemplate_tbl]WHERE (TemplateName='"+templateName+"')");

            string Body =string.Format(temp.Rows[0]["HtmlCode"].ToString(), varList);


            try
            {
                var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
                if (!scheduler.IsStarted)
                {
                    await scheduler.Start();
                }
                var job = JobBuilder.Create<EmailJob>()
                    .UsingJobData("_EmailFrom", EmailFrom)
                    .UsingJobData("_Password", Password)
                    .UsingJobData("Subject", Subject)
                    .UsingJobData("Body",Body)
                    .UsingJobData("Name", Name)
                    .UsingJobData("IsHtml", 1)
                    .Build();


                var trigger = TriggerBuilder.Create()
                    .WithSimpleSchedule(s => s.WithIntervalInSeconds(5).WithRepeatCount(dt.Rows.Count / 100))
                    .StartNow()
                    .Build();

                await scheduler.ScheduleJob(job, trigger);
            }
            catch (Exception ex)
            {

            }
        }
    }
}