using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCMSP_MVC.Models
{
    public class PageSeparateModel
    {
        public string category { get; set; }
        public string Tags { get; set; }
        public List<int> pages { get; set; }
        public int CurrentPage { get; set; }
    }
}