using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PCMSP_MVC.Other
{
    public class Date_TimeStamp
    {
        public PersianCalendar PersianCalendar { get; set; }

        public Date_TimeStamp()
        {
            PersianCalendar=new PersianCalendar();
        }
        public double GetTime_Soconds(DateTime date)
        {
            var now = date;
            var dateTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, DateTimeKind.Utc);
            Console.WriteLine(dateTime);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            double unixDateTime = (dateTime.ToUniversalTime() - epoch).TotalSeconds;

            return unixDateTime;
        }

        private readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public DateTime GetDateTime_fromUnix(string num)
        {

            var localDateTime = epoch.AddSeconds(Convert.ToDouble(num));

            return localDateTime;

        }


        public static string AppendServername(string url)
        {
            return "https://" + HttpContext.Current.Request.Url.Authority + "/" + url;
        }

    }
}