using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCMSP_MVC.DBConnect
{
    public class TransActionReport
    {
        public string SqlQuery { get; set; }
        public int RowsAffected { get; set; }
        public Exception TransactionExeption { set; get; }
    }
}