using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCMSP_MVC.Other
{
    public class AppendUrl
    {
        public static string AppendServername(string url)
        {
            return "https://" + HttpContext.Current.Request.Url.Authority + "/" + url;
        }

    }
}