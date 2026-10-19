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
    [RoutePrefix("api/flt/programs/{ProgId}/locations/{LocId}/fleet/{Aid}")]
    public class FlightInfosController : ApiController
    {
        #region Public CRUD methods

        [Route("flightinfos")]
        [HttpGet]
        public HttpResponseMessage GetList(int ProgId, int LocId, int Aid)
        {
            try
            {
                FltIdentity fltIdentity = IdentityManager.Authenticate(HttpContext.Current);
                if (fltIdentity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, Resources.Messages.UserWasNotAuthenticated);
                }
                FlightInfos modelFlightInfoList = new FlightInfos();
                FLTBizObjects.FLT.BO.FlightInfoList fltFlightInfoList = FLTBizObjects.FLT.BL.FlightInfoManager.GetList(Aid, String.Empty, LocId, fltIdentity);
                foreach (FLTBizObjects.FLT.BO.FlightInfo thisFlightInfo in fltFlightInfoList)
                {
                    modelFlightInfoList.Add(CreateNewModelFlightInfoObject(thisFlightInfo));
                }

                if (modelFlightInfoList.Count == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format(Resources.Messages.NotFoundFlightInfoGetList, ProgId, LocId, Aid));
                }
                return Request.CreateResponse<FlightInfos>(HttpStatusCode.OK, modelFlightInfoList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, String.Format(Resources.Messages.InternalErrorFlightInfoGetList, ProgId, LocId, Aid));
            }
        }
        
        #endregion

        #region Private Subroutines
        private FltWebApi.Models.FlightInfo CreateNewModelFlightInfoObject(FLTBizObjects.FLT.BO.FlightInfo thisFlightInfo)
        {
            try
            {
                FltWebApi.Models.FlightInfo newFlightInfo = new FltWebApi.Models.FlightInfo();

                newFlightInfo.FlightId = thisFlightInfo.FlightId;
                newFlightInfo.FlightDate = thisFlightInfo.FlightDate;
                newFlightInfo.DepartureTime = thisFlightInfo.DepartureTime;
                newFlightInfo.ReturnTime = thisFlightInfo.ReturnTime;
                newFlightInfo.AirframeHoursStart = Convert.ToDecimal(thisFlightInfo.AirframeHoursStart);
                newFlightInfo.AirframeHoursEnd = Convert.ToDecimal(thisFlightInfo.AirframeHoursEnd);

                return newFlightInfo;
            }
            catch
            {
                throw new ApplicationException();
            }
        }
        #endregion

    }
}