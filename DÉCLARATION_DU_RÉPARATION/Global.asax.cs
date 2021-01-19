using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace DÉCLARATION_DU_RÉPARATION
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            DÉCLARATION_DU_RÉPARATION.CorsSupport.HandlePreflightRequest(HttpContext.Current);
        }

    }
}