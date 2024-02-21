using JustSay_API_Service.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JustSay_API_Service.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Alert()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult OTP_Page()
        {
            return View();
        }
        public ActionResult HomePage()
        {
            return View();
        }
        public ActionResult Logs()
        {
            try
            {

            }
            catch (Exception ex)
            {
                
            }
            return View();
        }
        public ActionResult GetDetails(string strPhnNo, string strGender, string strDOB, string strFlag)
        {
            string strStatus = string.Empty;
            string strMessage = string.Empty;
            string strResult = string.Empty;
            string strResponse = string.Empty;
            string strReqTime = DateTime.Now.ToString();
            string strResTime = string.Empty;
            string log = string.Empty;
            try
            {
                if (strFlag == "L")
                {
                    strGender = "";
                    strDOB = "01/01/1900";
                }
                RQRS.Reg_and_Login login_cred = new RQRS.Reg_and_Login();
                DateTime date = DateTime.Parse(strDOB);
                login_cred.phone = strPhnNo;
                login_cred.dob = date.ToString("yyyyMMdd");
                login_cred.gender = strGender;
                login_cred.flag = strFlag;
                string strRequest = JsonConvert.SerializeObject(login_cred);
                strResponse = Baseclass.InvokeServiceReq("API/Reg_and_Login_srvc", strRequest, "POST");
                RQRS.ResponseData Response = JsonConvert.DeserializeObject<RQRS.ResponseData>(strResponse);
                if (Response.strStatus == "01")
                {
                    strStatus = "01";
                    strMessage = Response.strResponse;
                }
                else
                {
                    strStatus = "00";
                    strMessage = Response.strResponse;
                }
                strResTime = DateTime.Now.ToString();
                log = "<Home><GetDetails><Request>"+ strRequest + "</Request><Response>"+ strResponse + "</Response></GetDetails></Home>";
            }
            catch (Exception ex)
            {
                strStatus = "00";
                strMessage = "Unable to connect Service(#05)";
                log = "<Ex>"+Convert.ToString(ex)+"</Ex>";
            }

            return Json(new { Status = strStatus, Message = strMessage });
        }
    }
}