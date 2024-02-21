using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustSay_API_Service.Models
{
    public class RQRS
    {
        #region Resopnse
        public class ResponseData
        {
            public string strStatus { get; set; }
            public string strResponse { get; set; }
            public string strErrorMessage { get; set; }

        };
        #endregion
        public class Reg_and_Login
        {
            public string phone { get; set; }
            public string gender { get; set; }
            public string dob { get; set; }
            public string flag { get; set; }

        };
    }
}