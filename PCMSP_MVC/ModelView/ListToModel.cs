using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PCMSP_MVC.Models;

namespace PCMSP_MVC.ModelView
{
    public class ListToModel
    {
        //private List<T> ConverterL<T>(T valuee)
        //{
        //    return new[] {valuee}.ToList();
        //}

        //private object toListObj;
        //public object ToListObj
        //{
        //    set { toListObj = ConverterL(value); }

        //    get { return toListObj; }
        //}

        public List<LatestNewsModel> search { get; set; }
    }
}