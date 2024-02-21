using JustSay_API_Service.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JustSay_API_Service.Controllers
{
    public class APIController : ApiController
    {
        #region Department
        public RQRS.ResponseData Reg_and_Login_srvc(RQRS.Reg_and_Login get_login_cred)
        {
            DataSet dsOutput = new DataSet();
            RQRS.ResponseData ResponseData = new RQRS.ResponseData();
            string strErrorMsg = string.Empty;
            try
            {
                Hashtable hs_Param = new Hashtable();
                hs_Param.Add("MOBILE_NO", get_login_cred.phone);
                hs_Param.Add("GENDER", get_login_cred.gender);
                hs_Param.Add("DOB", get_login_cred.dob);
                hs_Param.Add("FLAG", get_login_cred.flag);

                if (get_login_cred.flag == "L")
                {
                    dsOutput = DBHandler.ExecProcedureReturnsDataset(DBHandler.StoreProcedure.P_USER_DETAILS, hs_Param, ref strErrorMsg);
                    if (dsOutput != null && dsOutput.Tables.Count > 0 && dsOutput.Tables[0].Rows.Count > 0)
                    {
                        ResponseData.strStatus = "01";
                        ResponseData.strResponse = "Successfully Verified";
                    }
                    else
                    {
                        ResponseData.strStatus = "00";
                        ResponseData.strResponse = "User not found in this number, Please Register!";
                        ResponseData.strErrorMessage = strErrorMsg != "" ? strErrorMsg : "No Records found";
                    }
                }
                else
                {
                    int ResultCount = 0;
                    bool result = DBHandler.InsertUpdateDelete(DBHandler.StoreProcedure.P_USER_DETAILS, hs_Param, ref strErrorMsg, ref ResultCount);
                    if (ResultCount > 0)
                    {
                        ResponseData.strStatus = "01";
                        ResponseData.strResponse = "Successfully Registered";
                    }
                    else
                    {
                        ResponseData.strStatus = "00";
                        ResponseData.strResponse = "Unable to Register now, Please Try again!";
                        ResponseData.strErrorMessage = strErrorMsg != "" ? strErrorMsg : "Problem occured while Register!";
                    }
                }
            }
            catch (Exception ex)
            {
                ResponseData.strStatus = "01";
                ResponseData.strResponse = "Unable to connect database(#05)";
                ResponseData.strErrorMessage = Convert.ToString(ex);
            }
            return ResponseData;
        }
        #endregion
    }
}
