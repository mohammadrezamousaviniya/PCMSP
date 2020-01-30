using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PCMSP_MVC.Models
{
    public class Customer
    {
        public int id { set; get; }
        [Required]
        [StringLength(255)]
        public string name { get; set; }
        public string email { get; set; }
        [Required]
        [StringLength(25)]
        public string phone { get; set; }
        [Required]
        [StringLength(255)]
        public string subject { get; set; }
        [Required]
        [StringLength(2048)]
        public string message { get; set; }
    }
}