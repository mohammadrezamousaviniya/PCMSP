using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DataBaseConnector;
using MimeKit;
using Quartz;
using MailKit.Net.Smtp;

namespace PCMSP_MVC.Modules.Email
{
    public class EmailJob :IJob
    {
        public string _EmailFrom { get; set; }
        public string _Password { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
        public string Name { get; set; }
        public int IsHtml { get; set; }

        public async Task Execute(IJobExecutionContext context)
        {
            int count = 100;
            PDBC db = new PDBC("DBConnectionString",true);
            db.Connect();
            DataTable dt = db.Select("SELECT [Id],[EmailAddress]FROM [dbo].[EmailModule_tbl]");
            if (dt.Rows.Count < 100)
            {
                count = dt.Rows.Count;
            }

            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(Name, _EmailFrom));
            mimeMessage.Subject = Subject; //Subject  


            if (IsHtml==1)
            {
                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = Body;
                bodyBuilder.TextBody = "this is a html message";
                mimeMessage.Body = bodyBuilder.ToMessageBody();
            }
            else
            {
                mimeMessage.Body = new TextPart("plain")
                {
                    Text = Body
                };

            }






            using (var client = new SmtpClient())
            {
                client.Connect("mail.tshpanda.com", 587, false);
                client.Authenticate(
                    _EmailFrom,
                    _Password
                );

                for (int i = 0; i < count; i++)
                {
                    mimeMessage.To.Clear();
                    mimeMessage.To.Add(new MailboxAddress
                    (
                        dt.Rows[i]["EmailAddress"].ToString()
                    ));

                   client.SendAsync(mimeMessage).Wait();
                    db.Script("DELETE FROM [dbo].[EmailModule_tbl] WHERE Id=" + dt.Rows[i]["Id"]);
                }

                Console.WriteLine("The mail has been sent successfully !!");
                Console.ReadLine();
                client.DisconnectAsync(true);
            }

        
    }
    }
}