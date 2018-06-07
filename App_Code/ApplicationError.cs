using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cflora.App_Code
{
    public class ApplicationError
    {
        #region (LOG ERRORS)
        public static void LogErrors(Exception ex)
        {
            //HttpRequest request = HttpContext.Current.Request;
            //StreamWriter swErrors = new StreamWriter(request.PhysicalApplicationPath + "//Gallery//Error.htm", true);
            //swErrors.WriteLine("<b>----- Start Error : " + System.DateTime.Now.ToString() + " -----</b>");
            //swErrors.WriteLine("<table><tr><td>" + ex.StackTrace.ToString() + "</td></tr>");
            //swErrors.WriteLine("<tr><td>" + (request.RawUrl) + "</td></tr>");
            //swErrors.WriteLine("<tr><td>" + (ex.Message.ToString()) + "</td></tr>");
            //swErrors.WriteLine("<tr><td>Login Id :<b> " + Convert.ToString(MiscFunction.GetUserName()) + "</b></td></tr>");
            //swErrors.WriteLine("<tr><td>Ip : " + MiscFunction.GetIpAndMachine()[0] + "; Machine Name :  " + MiscFunction.GetIpAndMachine()[1] + "</td></tr>");
            //swErrors.WriteLine("<tr><td>" + (ex.TargetSite.ToString()) + "</td></tr></table><hr />");
            //swErrors.Close();
        }
        #endregion
    }
}