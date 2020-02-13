using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PCMSP_MVC.Models;

namespace PCMSP_MVC.ModelView
{
    public class NewsDetails_ModelView
    {
        public NewsModel NewsModel { get; set; }
        public List<LatestNewsModel> LatestNewsModels { get; set; }
        public List<LatestNewsModel> popular { get; set; }
        public List<CategoryModel> CategoryModels { get; set; }
        public Comment comment { get; set; }
    }
}