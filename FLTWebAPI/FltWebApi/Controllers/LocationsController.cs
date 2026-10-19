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
    [RoutePrefix("api/flt/programs/{ProgId}")]
    public class LocationController : ApiController
    {
        #region Public CRUD methods

        [Route("locations")]
        [HttpGet]
        public HttpResponseMessage GetList(int ProgId)
        {
            try
            {
                FltIdentity fltIdentity = IdentityManager.Authenticate(HttpContext.Current);
                if (fltIdentity == null)
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, Resources.Messages.UserWasNotAuthenticated);
                }
                Locations modelLocationList = new Locations();
                FLTBizObjects.FLT.BO.LocationList fltLocationList = FLTBizObjects.FLT.BL.LocationManager.GetList(ProgId, fltIdentity);
                foreach (FLTBizObjects.FLT.BO.Location thisLocation in fltLocationList)
                {
                    modelLocationList.Add(CreateNewModelLocationObject(thisLocation));
                }

                if (modelLocationList.Count == 0)
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format(Resources.Messages.NotFoundLocationGetList, ProgId));
                }
                return Request.CreateResponse<Locations>(HttpStatusCode.OK, modelLocationList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, String.Format(Resources.Messages.InternalErrorLocationGetList, ProgId));
            }
        }

        [Route("locations/{LocId}")]
        [HttpGet]
        public HttpResponseMessage GetItem(int ProgId, int LocId)
        {
            try
            {
                FltIdentity fltIdentity = IdentityManager.Authenticate(HttpContext.Current);
                if (fltIdentity == null)
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, Resources.Messages.UserWasNotAuthenticated);
                }
                FLTBizObjects.FLT.BO.Location fltLocation = FLTBizObjects.FLT.BL.LocationManager.GetItem(LocId, fltIdentity);
                if (fltLocation == null)
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format(Resources.Messages.NotFoundLocationGetItem, ProgId, LocId));
                }
                if (fltLocation.ProgId != ProgId)
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format(Resources.Messages.NotFoundLocationGetItem, ProgId, LocId));
                }
                return Request.CreateResponse<FltWebApi.Models.Location>(HttpStatusCode.OK, CreateNewModelLocationObject(fltLocation));
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, String.Format(Resources.Messages.InternalErrorLocationGetItem, ProgId, LocId));
            }
        }
        
        #endregion

        #region Private Subroutines
        private FltWebApi.Models.Location CreateNewModelLocationObject(FLTBizObjects.FLT.BO.Location thisLocation)
        {
            try
            {
                FltWebApi.Models.Location newModelLocation = new FltWebApi.Models.Location();

                newModelLocation.LocationId = thisLocation.LocactionId;
                newModelLocation.ProgId = thisLocation.ProgId;
                newModelLocation.LocationName = thisLocation.LocationName;

                return newModelLocation;
            }
            catch
            {
                throw new ApplicationException();
            }
        }
        #endregion
    }
}