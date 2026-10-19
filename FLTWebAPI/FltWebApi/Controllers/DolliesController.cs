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
using FltWebApi;

namespace FltWebApi.Controllers
{
    [RoutePrefix("api/flt/programs/{ProgId}")]
    public class DolliesController : ApiController
    {
        #region Public CRUD methods
        [Route("dollies")]
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
                int weightTypeId = GetWeightTypeIdForWeightTypeDescription("Ballast", fltIdentity);

                Dollies modelBallastList = new Dollies();
                FLTBizObjects.FLT.BO.WeightDefList fltDolliesList = FLTBizObjects.FLT.BL.WeightDefManager.GetList(ProgId, weightTypeId, fltIdentity);

                foreach (WeightDef thisWeightDef in fltDolliesList)
                {
                    modelBallastList.Add(CreateNewModelBallastObject(thisWeightDef));
                }
                if (modelBallastList.Count == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format(Resources.Messages.NotFoundBallastGetList, ProgId));
                }
                return Request.CreateResponse<Dollies>(HttpStatusCode.OK, modelBallastList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, String.Format(Resources.Messages.InternalErrorBallastGetList, ProgId));
            }
        }
        #endregion

        #region Private Subroutines
        private Ballast CreateNewModelBallastObject(WeightDef thisWeightDef)
        {
            try
            {
                Ballast newModelBallast = new Ballast();

                newModelBallast.BallastId = thisWeightDef.WeightDefId;
                newModelBallast.Description = thisWeightDef.WeightDefDescr;
                newModelBallast.Weight = thisWeightDef.Weight;
                newModelBallast.WeightUnit = thisWeightDef.WeightUnit;

                return newModelBallast;
            }
            catch
            {
                throw new ApplicationException();
            }
        }

        private int GetWeightTypeIdForWeightTypeDescription(string WeightTypeDescription, FltIdentity fltIdentity)
        {
            try
            {
                FLTBizObjects.FLT.BO.WeightDefTypeList typeList = FLTBizObjects.FLT.BL.WeightDefTypeManager.GetList(fltIdentity);
                foreach (WeightDefType thisType in typeList)
                {
                    if (thisType.WeightDefTypeDescr.ToLower() == WeightTypeDescription.ToLower())
                    {
                        return thisType.WeightDefTypeId;
                    }
                }
                throw new ApplicationException();
            }
            catch
            {
                throw new ApplicationException();
            }
        }
        #endregion
    }
}
