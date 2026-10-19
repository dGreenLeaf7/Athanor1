// Copyright (c) 2026 CODE Éxitos. All rights reserved.
// Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using FLTBizObjects.FLT.BO;

namespace FltWebApi.App_Code
{
    internal class IdentityManager
    {
        public static FltIdentity Authenticate(HttpContext ctx)
        {
            string userName = string.Empty;
            string password = string.Empty;

            if (System.Configuration.ConfigurationManager.AppSettings.Get("userName") != null)
            {
                userName = System.Configuration.ConfigurationManager.AppSettings.Get("userName").ToString();
            }
            if (System.Configuration.ConfigurationManager.AppSettings.Get("Password") != null)
            {
                password = System.Configuration.ConfigurationManager.AppSettings.Get("password").ToString();
            }

            string encodedString = ctx.Request.Headers["Authorization"];

            if (encodedString != null)
            {
                if (encodedString.Substring(0,6) == "Basic ")
                {
                    string decodedString = new ASCIIEncoding().GetString(Convert.FromBase64String(encodedString.Substring(6)));

                    int delimiterPosition = decodedString.IndexOf(":");
                    userName = decodedString.Substring(0, delimiterPosition);
                    password = decodedString.Substring(delimiterPosition + 1, decodedString.Length - delimiterPosition - 1);
                }
            }
            try
            {
                return FLTBizObjects.FLT.BL.FltIdentityManager.AuthenticateUserLogin(userName, password);
            }
            catch
            {
                return null;
            }
        }
    }
}