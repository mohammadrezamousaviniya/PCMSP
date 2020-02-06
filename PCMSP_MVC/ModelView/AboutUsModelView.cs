using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PCMSP_MVC.Models;

namespace PCMSP_MVC.ModelView
{
    public class AboutUsModelView
    {
        public List<TeamMembers> teamMemberses { get; set; }
        public List<CustomerMessage> customerMessages { get; set; }

    }
}