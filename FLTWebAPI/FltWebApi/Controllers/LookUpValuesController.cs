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
    public class LookUpValuesController : ApiController
    {
        #region Public CRUD methods
        [Route("evaluators")]
        [HttpGet]
        public HttpResponseMessage GetEvaluatorList(int ProgId)
        {
            try
            {
                FltIdentity fltIdentity = IdentityManager.Authenticate(HttpContext.Current);
                if (fltIdentity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, Resources.Messages.UserWasNotAuthenticated);
                }
                LookUpValues modelLookUpValueList = GetLookUpValueList("Pilot", ProgId);
                if (modelLookUpValueList.Count == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format(Resources.Messages.NotFoundPilotGetList, ProgId));
                }
                return Request.CreateResponse<LookUpValues>(HttpStatusCode.OK, modelLookUpValueList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, String.Format(Resources.Messages.InternalErrorPilotGetList, ProgId));
            }
        }

        [Route("workshifts")]
        [HttpGet]
        public HttpResponseMessage GetWorkShiftList(int ProgId)
        {
            try
            {
                FltIdentity fltIdentity = IdentityManager.Authenticate(HttpContext.Current);
                if (fltIdentity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, Resources.Messages.UserWasNotAuthenticated);
                }
                LookUpValues modelLookUpValueList = GetLookUpValueList("WorkShift", ProgId);
                if (modelLookUpValueList.Count == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format(Resources.Messages.NotFoundWorkShiftGetList, ProgId));
                }
                return Request.CreateResponse<LookUpValues>(HttpStatusCode.OK, modelLookUpValueList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, String.Format(Resources.Messages.InternalErrorWorkShiftGetList, ProgId));
            }
        }

        [Route("liquidfilltypes")]
        [HttpGet]
        public HttpResponseMessage GetLiquidFillTypeList(int ProgId)
        {
            try
            {
                FltIdentity fltIdentity = IdentityManager.Authenticate(HttpContext.Current);
                if (fltIdentity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, Resources.Messages.UserWasNotAuthenticated);
                }
                LookUpValues modelLookUpValueList = GetLookUpValueList("LiquidGroup", ProgId);
                if (modelLookUpValueList.Count == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format(Resources.Messages.NotFoundLiquidFillTypeGetList, ProgId));
                }
                return Request.CreateResponse<LookUpValues>(HttpStatusCode.OK, modelLookUpValueList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, String.Format(Resources.Messages.InternalErrorLiquidFillTypeGetList, ProgId));
            }
        }

        [Route("liquidfillsubtypes")]
        [HttpGet]
        public HttpResponseMessage GetLiquidFillSubtypeList(int ProgId)
        {
            try
            {
                FltIdentity fltIdentity = IdentityManager.Authenticate(HttpContext.Current);
                if (fltIdentity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, Resources.Messages.UserWasNotAuthenticated);
                }
                LookUpValues modelLookUpValueList = GetLookUpValueList("LiquidType", ProgId);
                if (modelLookUpValueList.Count == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format(Resources.Messages.NotFoundLiquidFillSubTypeGetList, ProgId));
                }
                return Request.CreateResponse<LookUpValues>(HttpStatusCode.OK, modelLookUpValueList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, String.Format(Resources.Messages.InternalErrorLiquidFillSubTypeGetList, ProgId));
            }
        }

        [Route("liquidfillunits")]
        [HttpGet]
        public HttpResponseMessage GetLiquidFillUnitList(int ProgId)
        {
            try
            {
                FltIdentity fltIdentity = IdentityManager.Authenticate(HttpContext.Current);
                if (fltIdentity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, Resources.Messages.UserWasNotAuthenticated);
                }
                LookUpValues modelLookUpValueList = GetLookUpValueList("LiquidUnit", ProgId);
                if (modelLookUpValueList.Count == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format(Resources.Messages.NotFoundLiquidFillUnitGetList, ProgId));
                }
                return Request.CreateResponse<LookUpValues>(HttpStatusCode.OK, modelLookUpValueList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, String.Format(Resources.Messages.InternalErrorLiquidFillUnitGetList, ProgId));
            }
        }

        [Route("commentcomponentcodes")]
        [HttpGet]
        public HttpResponseMessage GetCommentComponentCodeList(int ProgId)
        {
            try
            {
                FltIdentity fltIdentity = IdentityManager.Authenticate(HttpContext.Current);
                if (fltIdentity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, Resources.Messages.UserWasNotAuthenticated);
                }
                LookUpValues modelLookUpValueList = GetLookUpValueList("AircraftComponent", ProgId);
                if (modelLookUpValueList.Count == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format(Resources.Messages.NotFoundCommentComponentCodeGetList, ProgId));
                }
                return Request.CreateResponse<LookUpValues>(HttpStatusCode.OK, modelLookUpValueList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, String.Format(Resources.Messages.InternalErrorCommentComponentCodeGetList, ProgId));
            }
        }

        [Route("commentissuecodes")]
        [HttpGet]
        public HttpResponseMessage GetCommentIssueCodeList(int ProgId)
        {
            try
            {
                FltIdentity fltIdentity = IdentityManager.Authenticate(HttpContext.Current);
                if (fltIdentity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, Resources.Messages.UserWasNotAuthenticated);
                }
                LookUpValues modelLookUpValueList = GetLookUpValueList("IssueCode", ProgId);
                if (modelLookUpValueList.Count == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format(Resources.Messages.NotFoundCommentIssueCodeGetList, ProgId));
                }
                return Request.CreateResponse<LookUpValues>(HttpStatusCode.OK, modelLookUpValueList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, String.Format(Resources.Messages.InternalErrorCommentIssueCodeGetList, ProgId));
            }
        }

        [Route("lengthunits")]
        [HttpGet]
        public HttpResponseMessage GetLengthUnitList(int ProgId)
        {
            try
            {
                FltIdentity fltIdentity = IdentityManager.Authenticate(HttpContext.Current);
                if (fltIdentity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, Resources.Messages.UserWasNotAuthenticated);
                }
                LookUpValues modelLookUpValueList = GetLookUpValueList("LengthUnit", ProgId);
                if (modelLookUpValueList.Count == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format(Resources.Messages.NotFoundLengthUnitGetList, ProgId));
                }
                return Request.CreateResponse<LookUpValues>(HttpStatusCode.OK, modelLookUpValueList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, String.Format(Resources.Messages.InternalErrorLengthUnitGetList, ProgId));
            }
        }
        #endregion

        #region Private Subroutines
        private LookUpValues GetLookUpValueList(string LookUpBaseName, int ProgId)
        {
            try
            {
                fltLookupBaseList lookUpBaseList = fltLookupBaseManager.GetList(LookUpBaseName, ProgId);
                LookUpValues modelsLookUpList = new LookUpValues();
                foreach (fltLookupBase thisLookUpValue in lookUpBaseList)
                {
                    modelsLookUpList.Add(CreateNewModelLookUpValueObject(thisLookUpValue));
                }
                return modelsLookUpList;
            }
            catch
            {
                throw new ApplicationException();
            }
        }

        private LookUpValue CreateNewModelLookUpValueObject(fltLookupBase thisLookUp)
        {
            try
            {
                LookUpValue newModelLookUpValue = new LookUpValue();

                newModelLookUpValue.ID = thisLookUp.ID;
                newModelLookUpValue.StringValue = thisLookUp.StringValue;
                newModelLookUpValue.SortOrder = thisLookUp.SortOrder;

                return newModelLookUpValue;
            }
            catch
            {
                throw new ApplicationException();
            }
        }
        #endregion
    }
}
