// Copyright (c) 2026 CODE Éxitos. All rights reserved.
// Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FltWebApi.App_Code;
using FltWebApi.Models;
using FLTBizObjects.FLT.BL;
using FLTBizObjects.FLT.BO;
using System.Web;

namespace FltWebApi.Controllers
{
    [RoutePrefix("api/flt/programs/{ProgId}/locations/{LocId}")]
    public class RoutesController : ApiController
    {
        #region Public CRUD methods

        [Route("routes")]
        [HttpGet]
        public HttpResponseMessage GetList(int ProgId, int LocId)
        {
            try
            {
                FltIdentity fltIdentity = IdentityManager.Authenticate(HttpContext.Current);
                if (fltIdentity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, Resources.Messages.UserWasNotAuthenticated);
                }

                Routes modelRouteList = new Routes();
                FLTBizObjects.FLT.BO.RouteList fltRouteList = FLTBizObjects.FLT.BL.RouteManager.GetList(ProgId, LocId, fltIdentity);
                foreach (FLTBizObjects.FLT.BO.Route thisRoute in fltRouteList)
                {
                    modelRouteList.Add(CreateNewModelRouteObject(thisRoute));
                }

                if (modelRouteList.Count == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format(Resources.Messages.NotFoundRouteGetList, ProgId, LocId));
                }
                return this.Request.CreateResponse<Routes>(HttpStatusCode.OK, modelRouteList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, String.Format(Resources.Messages.InternalErrorRouteGetList, ProgId, LocId));
            }
        }
        
        #endregion

        #region Private Subroutines
        private FltWebApi.Models.Route CreateNewModelRouteObject(FLTBizObjects.FLT.BO.Route thisRoute)
        {
            try
            {
                FltWebApi.Models.Route newModelRoute = new FltWebApi.Models.Route();

                newModelRoute.RouteId = thisRoute.RouteId;
                newModelRoute.Name = thisRoute.Name;
                newModelRoute.Description = thisRoute.Description;

                return newModelRoute;
            }
            catch
            {
                throw new ApplicationException();
            }
        }
        #endregion
    }
}