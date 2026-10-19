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
    public class RegionsController : ApiController
    {
        #region Public CRUD methods

        [Route("regions")]
        [HttpGet]
        public HttpResponseMessage GetList(int ProgId)
        {
            try
            {
                FltIdentity fltIdentity = IdentityManager.Authenticate(HttpContext.Current);
                if (fltIdentity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, Resources.Messages.UserWasNotAuthenticated);
                }
                Regions modelRegionList = new Regions();
                FLTBizObjects.FLT.BO.RegionList altRegionList = FLTBizObjects.FLT.BL.RegionManager.GetList(ProgId, fltIdentity);
                foreach (FLTBizObjects.FLT.BO.Region thisRegion in altRegionList)
                {
                    modelRegionList.Add(CreateNewModelRegionObject(thisRegion));
                }

                if (modelRegionList.Count == 0)
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format(Resources.Messages.NotFoundRegionGetList, ProgId));
                }
                return Request.CreateResponse<Regions>(HttpStatusCode.OK, modelRegionList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, String.Format(Resources.Messages.InternalErrorRegionGetList, ProgId));
            }
        }
        
        #endregion

        #region Private Subroutines
        private FltWebApi.Models.Region CreateNewModelRegionObject(FLTBizObjects.FLT.BO.Region thisRegion)
        {
            try
            {
                FltWebApi.Models.Region newModelRegion = new FltWebApi.Models.Region();

                newModelRegion.Id = thisRegion.Id;
                newModelRegion.Name = thisRegion.Name;
                newModelRegion.Code = thisRegion.Code;

                return newModelRegion;
            }
            catch
            {
                throw new ApplicationException();
            }
        }
        #endregion
    }
}