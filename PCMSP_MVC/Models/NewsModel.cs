using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCMSP_MVC.Models
{
    public class NewsModel
    {
        public int  Id { get; set; }
        public string title { get; set; }
        public string day { get; set; }
        public string year_month { get; set; }
        public string DayOfWeek { get; set; }
        public string by { get; set; }
        public string text_min { get; set; }
        public string text { get; set; }
        public string tags { get; set; }
        public string ImagePath { get; set; }
        public string InGroup { get; set; }
        public List<Comment> Comments { get; set; }
        public int Comments__ { get; set; }
        public List<string> Tags { get; set; }
        public bool IsImportant { get; set; }
    }
}