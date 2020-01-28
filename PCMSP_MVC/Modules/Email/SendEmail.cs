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



        //public void Send(DataTable dataTable, string EmailFrom, string Password, string Subject, string Body,string Name)
        //{
            
        //    AddToEmailList(dataTable);

        //    PDBC db = new PDBC("DBConnectionString", true);
        //    db.Connect();
        //    DataTable dt = db.Select("SELECT [Id],[EmailAddress]FROM [Core_Classes].[dbo].[EmailAddress_tbl]");
        //    /////////////
        //    ISchedulerFactory schedulerFactory=new StdSchedulerFactory();
        //    IScheduler scheduler = schedulerFactory.GetScheduler();
        //    scheduler.Start();
        //    ITrigger trigger = TriggerBuilder.Create()
        //        .WithSimpleSchedule(s => s.WithIntervalInSeconds(5).WithRepeatCount(dt.Rows.Count / 100))
        //        .StartNow()
        //        .Build();

        //    IJobDetail job = JobBuilder.Create<EmailJob>()
        //        .UsingJobData("_EmailFrom", EmailFrom)
        //        .UsingJobData("_Password", Password)
        //        .UsingJobData("Subject", Subject)
        //        .UsingJobData("Body", Body)
        //        .UsingJobData("Name", Name)
        //        .Build();

        //    await _scheduler.ScheduleJob(job, trigger);
        //    return Content("done");
        //}


        public async System.Threading.Tasks.Task EmailTask(
            DataTable dataTable, string EmailFrom, string Password, string Subject, string Body,string Name)
        {


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