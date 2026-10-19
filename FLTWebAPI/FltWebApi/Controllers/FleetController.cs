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
    public class FleetController : ApiController
    {
        #region Public CRUD methods

        [Route("fleet")]
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
                Fleet modelAircraftList = new Fleet();
                FLTBizObjects.FLT.BO.AircraftInfoList fltAircraftList = FLTBizObjects.FLT.BL.AircraftInfoManager.GetList(ProgId, LocId, String.Empty, fltIdentity);
                foreach (FLTBizObjects.FLT.BO.AircraftInfo thisAircraftInfo in fltAircraftList)
                {
                    modelAircraftList.Add(CreateNewModelAircraftObject(thisAircraftInfo));
                }

                if (modelAircraftList.Count == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, String.Format(Resources.Messages.InternalErrorAircraftGetList, ProgId, LocId));
                }
                return this.Request.CreateResponse<Fleet>(HttpStatusCode.OK, modelAircraftList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, String.Format(Resources.Messages.InternalErrorAircraftGetList, ProgId, LocId));
            }
        }

        [Route("fleet/{Aid}")]
        [HttpGet]
        public HttpResponseMessage GetItem(int ProgId, int LocId, int Aid)
        {
            try
            {
                FltIdentity fltIdentity = IdentityManager.Authenticate(HttpContext.Current);
                if (fltIdentity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, Resources.Messages.UserWasNotAuthenticated);
                }
                FLTBizObjects.FLT.BO.Aircraft fltAircraft = FLTBizObjects.FLT.BL.AircraftManager.GetItem(Aid, fltIdentity);
                if (fltAircraft == null)
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format(Resources.Messages.NotFoundAircraftGetItem, ProgId, LocId, Aid));
                }
                if ((fltAircraft.ProgId != ProgId) || (fltAircraft.CurrentLocationId != LocId))
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format(Resources.Messages.NotFoundAircraftGetItem, ProgId, LocId, Aid));
                }
                return this.Request.CreateResponse<FltWebApi.Models.Aircraft>(HttpStatusCode.OK, CreateNewModelAircraftObject(fltAircraft));
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, String.Format(Resources.Messages.InternalErrorAircraftGetItem, ProgId, LocId, Aid));
            }
        }

        #endregion

        #region Private Subroutines

        private FltWebApi.Models.Aircraft CreateNewModelAircraftObject(AircraftInfo thisAircraftInfo)
        {
            try
            {
                FltWebApi.Models.Aircraft newModelAircraft = new FltWebApi.Models.Aircraft();

                newModelAircraft.AircraftId = thisAircraftInfo.AircraftId;
                newModelAircraft.MSN = thisAircraftInfo.MSN;
                newModelAircraft.RegistrationNbr = thisAircraftInfo.RegistrationNbr;
                newModelAircraft.ModelName = thisAircraftInfo.ModelName;
                newModelAircraft.Description = thisAircraftInfo.Description;
                newModelAircraft.MeterUnitId = thisAircraftInfo.AirframeHoursUnitId;
                newModelAircraft.AirframeHoursCurrent = Convert.ToDecimal(thisAircraftInfo.AirframeHoursCurrent) - Convert.ToDecimal(thisAircraftInfo.AirframeHoursOffset);
                newModelAircraft.AirframeHoursOffset = Convert.ToDecimal(thisAircraftInfo.AirframeHoursOffset);

                return newModelAircraft;
            }
            catch
            {
                throw new ApplicationException();
            }
        }

        private FltWebApi.Models.Aircraft CreateNewModelAircraftObject(FLTBizObjects.FLT.BO.Aircraft thisAircraft)
        {
            try
            {
                FltWebApi.Models.Aircraft newModelAircraft = new FltWebApi.Models.Aircraft();

                newModelAircraft.AircraftId = thisAircraft.AircraftId;
                newModelAircraft.AircraftGUID = thisAircraft.AircraftGuid;
                newModelAircraft.CurrentLocationId = thisAircraft.CurrentLocationId;
                newModelAircraft.MSN = thisAircraft.MSN;
                newModelAircraft.RegistrationNbr = thisAircraft.RegistrationNbr;
                newModelAircraft.ModelName = thisAircraft.Model;
                newModelAircraft.Description = thisAircraft.Description;
                newModelAircraft.MeterUnitId = thisAircraft.MeterUnitId;
                newModelAircraft.AirframeHoursCurrent = Convert.ToDecimal(thisAircraft.AirframeHoursCurrent) - Convert.ToDecimal(thisAircraft.AirframeHoursOffset);
                newModelAircraft.AirframeHoursOffset = Convert.ToDecimal(thisAircraft.AirframeHoursOffset);

                return newModelAircraft;
            }
            catch
            {
                throw new ApplicationException();
            }
        }

        #endregion
    }
}