﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using MailKit.Net.Smtp;
using System.Web.Http;
using System.Web.Mvc;
using DataBaseConnector;
using MimeKit;
using PCMSP_MVC.Modules.Email;


namespace PCMSP_MVC.Controllers
{
    public class MediaController : Controller
    {
        public ActionResult Email()
        {
            return Content("Email");
        }


}