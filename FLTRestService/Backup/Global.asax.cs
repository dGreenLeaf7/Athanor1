// Copyright (c) 2026 CODE Éxitos. All rights reserved.
// Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
using System;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;

namespace FltRestService
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            WebServiceHostFactory factory = new WebServiceHostFactory();
            RouteTable.Routes.Add(new ServiceRoute("FLT", factory, typeof(FLTService)));
        }
    }
}
