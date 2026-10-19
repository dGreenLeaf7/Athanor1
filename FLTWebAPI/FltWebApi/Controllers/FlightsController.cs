// Copyright (c) 2026 CODE Éxitos. All rights reserved.
// Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FltWebApi.App_Code;
using FltWebApi.Models;
using FLTBizObjects.FLT.BL;
using FLTBizObjects.FLT.BO;
using System.Web;
using Contoso.Utilities;

namespace FltWebApi.Controllers
{
    [RoutePrefix("api/flt/programs/{ProgId}/locations/{LocId}/fleet/{Aid}")]
    public class FlightsController : ApiController
    {
        [Route("flights/{Tid}")]
        [HttpGet]
        public HttpResponseMessage GetItem(int ProgId, int LocId, int Aid, int Tid)
        {
            try
            {
                FltIdentity fltIdentity = IdentityManager.Authenticate(HttpContext.Current);
                if (fltIdentity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, Resources.Messages.UserWasNotAuthenticated);
                }
                FltWebApi.Models.Flight modelFlight = new FltWebApi.Models.Flight();
                FLTBizObjects.FLT.BO.Flight fltFlight = FLTBizObjects.FLT.BL.FlightManager.GetItem(Tid, fltIdentity);
                if (fltFlight == null)
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format(Resources.Messages.NotFoundFlightGetItem, ProgId, LocId, Aid, Tid));
                }
                if ((fltFlight.AircraftId != Aid)||(fltFlight.LocationId != LocId))
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format(Resources.Messages.NotFoundFlightGetItem, ProgId, LocId, Aid, Tid));
                }
            return Request.CreateResponse<FltWebApi.Models.Flight>(HttpStatusCode.OK, CreateNewModelFlightObject(fltFlight));
            }
            catch
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, String.Format(Resources.Messages.InternalErrorFlightGetItem, ProgId, LocId, Aid, Tid));
            }
        }

        [Route("flights")]
        [HttpPost]
        public HttpResponseMessage AddNewItem(int ProgId, int LocId, int Aid, FltWebApi.Models.Flight flight)
        {
            try
            {
                FltIdentity fltIdentity = IdentityManager.Authenticate(HttpContext.Current);
                if (fltIdentity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, Resources.Messages.UserWasNotAuthenticated);
                }
                if (flight == null)
                {
                    Logger.Write("FltWebApi/Controllers/FlightsController.cs/AddNewItem::Parameter FltWebApi.Models.Flight object is null. Please check the client's Upload XML Schema.");
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, String.Format(Resources.Messages.InternalErrorFlightAddNewItem, ProgId, LocId, Aid));
                }
                FLTBizObjects.FLT.BO.Flight altFlight = CreateNewBizObjectFlight(LocId, Aid, flight);
                if (altFlight == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, String.Format(Resources.Messages.InternalErrorFlightAddNewItem, ProgId, LocId, Aid));
                }
                int newFlightId;
                try
                {
                    newFlightId = FLTBizObjects.FLT.BL.FlightManager.Insert(altFlight, fltIdentity, false);
                }
                catch(Exception ex)
                {
                    ProcessException(ex, "Flight", altFlight.BrokenRules);
                    throw new ApplicationException();
                }
                flight.FlightId = newFlightId;

                var response = Request.CreateResponse<FltWebApi.Models.Flight>(HttpStatusCode.Created, flight);
                string uri = Request.RequestUri + "/" + Convert.ToString(flight.FlightId);
                response.Headers.Location = new Uri(uri);
                return response;
            }
            catch(Exception ex)
            {
                Logger.Write(ex);
                
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, String.Format(Resources.Messages.InternalErrorFlightAddNewItem, ProgId, LocId, Aid));
            }
        }

        #region Private Subroutines

        #region CreateNewBizObjectFlight

        private static FLTBizObjects.FLT.BO.Flight CreateNewBizObjectFlight(int LocId, int Aid, FltWebApi.Models.Flight modelFlight)
        {
            FLTBizObjects.FLT.BO.Flight newBoFlight = new FLTBizObjects.FLT.BO.Flight();
            try
            {
                LoadNewBizObjectFlightBaseProperties(LocId, Aid, modelFlight, newBoFlight);

                LoadNewBizObjectLiquidFillList(modelFlight, newBoFlight);

                LoadNewBizObjectCommentList(modelFlight, newBoFlight);

                LoadNewBizObjectEngineRunList(modelFlight, newBoFlight);

                LoadNewBizObjectRegionTimeList(modelFlight, newBoFlight);

                return newBoFlight;
            }
            catch(Exception ex)
            {
                ProcessException(ex);
                return null;
            }
        }

        private static void LoadNewBizObjectFlightBaseProperties(int LocId, int Aid, FltWebApi.Models.Flight modelFlight, FLTBizObjects.FLT.BO.Flight newBoFlight)
        {
            try
            {
                newBoFlight.HobbsEnd = modelFlight.HobbsEnd.ToString("##0.0");
                newBoFlight.HobbsStart = modelFlight.HobbsStart.ToString("##0.0");
                newBoFlight.DepartureTime = modelFlight.DepartureTime;
                newBoFlight.BallastWeightId = modelFlight.BallastId;
                newBoFlight.FlownRouteId = modelFlight.FlownRouteId;
                newBoFlight.PilotId = modelFlight.EvaluatorId;
                newBoFlight.EngineHoursEnd = modelFlight.EngineHoursEnd.ToString("##0.0");
                newBoFlight.EngineHoursStart = modelFlight.EngineHoursStart.ToString("##0.0");
                newBoFlight.NacelleCheckBuildUp = modelFlight.NacelleCheckBuildUp;
                newBoFlight.NacelleCheckDiscoloration = modelFlight.NacelleCheckDiscoloration;
                newBoFlight.IsActive = true;
                newBoFlight.IsApproved = false;
                newBoFlight.IsDeleted = false;
                newBoFlight.LocationId = LocId;
                newBoFlight.MeterUnitId = modelFlight.MeterUnitId;
                newBoFlight.OilLevelIndicatorStart = modelFlight.OilLevelIndicatorStart;
                newBoFlight.PostFlightFtiOK = modelFlight.PostFlightFtiOK;
                newBoFlight.PostFlightTelemetryCheckComplete = modelFlight.PostFlightTelemetryCheckComplete;
                newBoFlight.PreFlightFtiOK = modelFlight.PreFlightFtiOK;
                newBoFlight.PreFlightTelemetryCheckComplete = modelFlight.PreFlightTelemetryCheckComplete;
                newBoFlight.PreFlightTelemetryOK = modelFlight.PreFlightTelemetryOK;
                newBoFlight.ReturnTime = modelFlight.ReturnTime;
                newBoFlight.SchedRouteId = modelFlight.SchedRouteId;
                newBoFlight.AirframeWeightId = modelFlight.AirframeId;
                newBoFlight.Cargo1WeightId = modelFlight.Cargo1Id;
                newBoFlight.Cargo2WeightId = modelFlight.Cargo2Id;
                newBoFlight.FlightId = -1;
                newBoFlight.FlightDate = modelFlight.FlightDate;
                newBoFlight.HydraulicLevelIndicatorStart = modelFlight.HydraulicLevelIndicatorStart;
                newBoFlight.UsedMeterOffset = Convert.ToString(modelFlight.UsedMeterOffset);
                newBoFlight.AirframeHoursEnd = Convert.ToString(Convert.ToDecimal(modelFlight.HobbsEnd) + Convert.ToDecimal(modelFlight.UsedMeterOffset));
                newBoFlight.AirframeHoursStart = Convert.ToString(Convert.ToDecimal(modelFlight.HobbsStart) + Convert.ToDecimal(modelFlight.UsedMeterOffset));
                newBoFlight.AircraftId = Aid;
                newBoFlight.WorkShiftIndicatorId = modelFlight.WorkShiftIndicatorId;
            }
            catch (Exception ex)
            {
                ProcessException(ex, "Flight", newBoFlight.BrokenRules);
                throw new ApplicationException();
            }
        }

        private static void LoadNewBizObjectLiquidFillList(FltWebApi.Models.Flight modelFlight, FLTBizObjects.FLT.BO.Flight newBoFlight)
        {
            foreach (FltWebApi.Models.LiquidFill modelLiquidFill in modelFlight.LiquidFills)
            {
                FLTBizObjects.FLT.BO.LiquidFill newBoLiquidFill = new FLTBizObjects.FLT.BO.LiquidFill();
                try
                {
                    newBoLiquidFill.HobbsReading = modelLiquidFill.HobbsReading.ToString("##0.0");
                    newBoLiquidFill.LiquidAmt = modelLiquidFill.LiquidAmt.ToString("##0.000");
                    newBoLiquidFill.LiquidFillApprover = modelLiquidFill.LiquidFillApprover;
                    newBoLiquidFill.LiquidFillLocation = modelLiquidFill.LiquidFillLocation;
                    newBoLiquidFill.LiquidGroupId = modelLiquidFill.LiquidFillTypeId;
                    newBoLiquidFill.LiquidTypeId = modelLiquidFill.LiquidFillSubTypeId;
                    newBoLiquidFill.LiquidUnitId = modelLiquidFill.LiquidUnitId;
                    newBoLiquidFill.FlightId = newBoFlight.FlightId;

                    FLTBizObjects.FLT.BL.FlightManager.InsertLiquidFill(newBoFlight, newBoLiquidFill);
                }
                catch (Exception ex)
                {
                    ProcessException(ex, "LiquidFill", newBoLiquidFill.BrokenRules);
                    throw new ApplicationException();
                }
            }
        }

        private static void LoadNewBizObjectCommentList(FltWebApi.Models.Flight modelFlight, FLTBizObjects.FLT.BO.Flight newBoFlight)
        {
            foreach (FltWebApi.Models.FlightComment modelComment in modelFlight.FlightComments)
            {
                FLTBizObjects.FLT.BO.FlightComment newBoComment = new FLTBizObjects.FLT.BO.FlightComment();
                try
                {
                    newBoComment.CommentRating = modelComment.CommentRating;
                    newBoComment.CommentTime = modelComment.CommentTime;
                    newBoComment.ComponentId = modelComment.ComponentId;
                    newBoComment.PilotComment = modelComment.EvaluatorComment;
                    newBoComment.IssueCodeId = modelComment.IssueCodeId;
                    newBoComment.FlightId = newBoFlight.FlightId;
                    FLTBizObjects.FLT.BL.FlightManager.InsertFlightComment(newBoFlight, newBoComment);
                }
                catch (Exception ex)
                {
                    ProcessException(ex, "FlightComment", newBoComment.BrokenRules);
                    throw new ApplicationException();
                }
            }
        }

        private static void LoadNewBizObjectEngineRunList(FltWebApi.Models.Flight modelFlight, FLTBizObjects.FLT.BO.Flight newBoFlight)
        {
            foreach (EngineRun modelEngineRun in modelFlight.EngineRuns)
            {
                FLTBizObjects.FLT.BO.FlightEngineRun newBoRun = new FlightEngineRun();
                try
                {
                    newBoRun.EngineSpeed = modelEngineRun.EngineSpeed.ToString("#,##0");
                    newBoRun.RunEndTime = modelEngineRun.RunEndTime;
                    newBoRun.RunStartTime = modelEngineRun.RunStartTime;
                    newBoRun.FlightId = modelFlight.FlightId;
                    FLTBizObjects.FLT.BL.FlightManager.InsertFlightEngineRun(newBoFlight, newBoRun);
                }
                catch (Exception ex)
                {
                    ProcessException(ex, "EngineRun", newBoRun.BrokenRules);
                    throw new ApplicationException();
                }
            }
        }

        private static void LoadNewBizObjectRegionTimeList(FltWebApi.Models.Flight modelFlight, FLTBizObjects.FLT.BO.Flight newBoFlight)
        {
            foreach (RegionTime modelRegionTime in modelFlight.RegionTimes)
            {
                FLTBizObjects.FLT.BO.FlightRegionTime newBoUsage = new FlightRegionTime();
                try
                {
                    newBoUsage.Hours = modelRegionTime.Hours.ToString("#,##0.0");
                    newBoUsage.RegionId = modelRegionTime.RegionId;
                    newBoUsage.FlightId = modelFlight.FlightId;
                    FLTBizObjects.FLT.BL.FlightManager.InsertFlightRegionTime(newBoFlight, newBoUsage);
                }
                catch (Exception ex)
                {
                    ProcessException(ex, "RegionTime", newBoUsage.BrokenRules);
                    throw new ApplicationException();
                }
            }
        }

        #endregion

        #region ProcessException

        private static void ProcessException(Exception ex)
        {
            ProcessException(ex, null, null);
        }

        private static void ProcessException(Exception ex, String objectName)
        {
            ProcessException(ex, objectName, null);
        }

        private static void ProcessException(Exception ex, String objectName, BrokenRulesList brokenRulesList)
        {
            Logger.Write(ex);
            if (brokenRulesList != null)
            {
                foreach (BrokenRule brokenRule in brokenRulesList)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(objectName);
                    sb.Append("|");
                    sb.Append(brokenRule.PropertyName);
                    sb.Append("|");
                    sb.Append(brokenRule.Message);
                    Logger.Write(sb.ToString());
                }
            }
        }

        #endregion

        #region CreateNewModelFlightObject

        private FltWebApi.Models.Flight CreateNewModelFlightObject(FLTBizObjects.FLT.BO.Flight boFlight)
        {
            FltWebApi.Models.Flight newModelFlight = new FltWebApi.Models.Flight();

            LoadNewModelFlightBaseProperties(boFlight, newModelFlight);

            LoadNewModelFlightLiquidFills(boFlight, newModelFlight);

            LoadNewModelFlightComments(boFlight, newModelFlight);

            LoadNewModelFlightEngineRuns(boFlight, newModelFlight);

            LoadNewModelFlightRegionTimes(boFlight, newModelFlight);

            return newModelFlight;
        }

        private static void LoadNewModelFlightBaseProperties(FLTBizObjects.FLT.BO.Flight boFlight, FltWebApi.Models.Flight newModelFlight)
        {
            newModelFlight.HobbsEnd = Convert.ToDecimal(boFlight.HobbsEnd);
            newModelFlight.HobbsStart = Convert.ToDecimal(boFlight.HobbsStart);
            newModelFlight.DepartureTime = boFlight.DepartureTime;
            newModelFlight.BallastId = boFlight.BallastWeightId;
            newModelFlight.FlownRouteId = boFlight.FlownRouteId;
            newModelFlight.EngineHoursEnd = Convert.ToDecimal(boFlight.EngineHoursEnd);
            newModelFlight.EngineHoursStart = Convert.ToDecimal(boFlight.EngineHoursStart);
            newModelFlight.EvaluatorId = boFlight.PilotId;
            newModelFlight.NacelleCheckBuildUp = boFlight.NacelleCheckBuildUp;
            newModelFlight.NacelleCheckDiscoloration = boFlight.NacelleCheckDiscoloration;
            newModelFlight.LocationId = boFlight.LocationId;
            newModelFlight.MeterUnitId = boFlight.MeterUnitId;
            newModelFlight.OilLevelIndicatorStart = boFlight.OilLevelIndicatorStart;
            newModelFlight.PostFlightFtiOK = boFlight.PostFlightFtiOK;
            newModelFlight.PostFlightTelemetryCheckComplete = boFlight.PostFlightTelemetryCheckComplete;
            newModelFlight.PreFlightFtiOK = boFlight.PreFlightFtiOK;
            newModelFlight.PreFlightTelemetryCheckComplete = boFlight.PreFlightTelemetryCheckComplete;
            newModelFlight.PreFlightTelemetryOK = boFlight.PreFlightTelemetryOK;
            newModelFlight.ReturnTime = boFlight.ReturnTime;
            newModelFlight.SchedRouteId = boFlight.SchedRouteId;
            newModelFlight.AirframeId = boFlight.AirframeWeightId;
            newModelFlight.Cargo1Id = boFlight.Cargo1WeightId;
            newModelFlight.Cargo2Id = boFlight.Cargo2WeightId;
            newModelFlight.FlightDate = boFlight.FlightDate;
            newModelFlight.FlightId = boFlight.FlightId;
            newModelFlight.HydraulicLevelIndicatorStart = newModelFlight.HydraulicLevelIndicatorStart;
            newModelFlight.AircraftId = boFlight.AircraftId;
            newModelFlight.WorkShiftIndicatorId = newModelFlight.WorkShiftIndicatorId;
        }

        private static void LoadNewModelFlightLiquidFills(FLTBizObjects.FLT.BO.Flight boFlight, FltWebApi.Models.Flight newModelFlight)
        {
            foreach (FLTBizObjects.FLT.BO.LiquidFill boLiquidFill in boFlight.LiquidFillList)
            {
                FltWebApi.Models.LiquidFill newModelLiquidFill = new FltWebApi.Models.LiquidFill();

                newModelLiquidFill.HobbsReading = Convert.ToDecimal(boLiquidFill.HobbsReading);
                newModelLiquidFill.LiquidAmt = Convert.ToDecimal(boLiquidFill.LiquidAmt);
                newModelLiquidFill.LiquidFillApprover = boLiquidFill.LiquidFillApprover;
                newModelLiquidFill.LiquidFillId = boLiquidFill.LiquidFillId;
                newModelLiquidFill.LiquidFillLocation = boLiquidFill.LiquidFillLocation;
                newModelLiquidFill.LiquidFillTypeId = boLiquidFill.LiquidGroupId;
                newModelLiquidFill.LiquidFillSubTypeId = boLiquidFill.LiquidTypeId;
                newModelLiquidFill.LiquidUnitId = boLiquidFill.LiquidUnitId;

                newModelFlight.LiquidFills.Add(newModelLiquidFill);
            }
        }

        private static void LoadNewModelFlightComments(FLTBizObjects.FLT.BO.Flight boFlight, FltWebApi.Models.Flight newModelFlight)
        {
            foreach (FLTBizObjects.FLT.BO.FlightComment boComment in boFlight.FlightCommentList)
            {
                FltWebApi.Models.FlightComment newModelComment = new FltWebApi.Models.FlightComment();

                newModelComment.CommentRating = boComment.CommentRating;
                newModelComment.CommentTime = boComment.CommentTime;
                newModelComment.ComponentId = boComment.ComponentId;
                newModelComment.EvaluatorComment = boComment.PilotComment;
                newModelComment.IssueCodeId = boComment.IssueCodeId;
                newModelComment.FlightCommentId = boComment.FlightCommentId;

                newModelFlight.FlightComments.Add(newModelComment);
            }
        }

        private static void LoadNewModelFlightEngineRuns(FLTBizObjects.FLT.BO.Flight boFlight, FltWebApi.Models.Flight newModelFlight)
        {
            foreach (FLTBizObjects.FLT.BO.FlightEngineRun boRun in boFlight.FlightEngineRunList)
            {
                EngineRun newModelRun = new EngineRun();

                newModelRun.EngineSpeed = Convert.ToInt32(boRun.EngineSpeed);
                newModelRun.EngineRunId = boRun.FlightEngineRunId;
                newModelRun.RunEndTime = boRun.RunEndTime;
                newModelRun.RunStartTime = boRun.RunStartTime;

                newModelFlight.EngineRuns.Add(newModelRun);
            }
        }

        private static void LoadNewModelFlightRegionTimes(FLTBizObjects.FLT.BO.Flight fltFlight, FltWebApi.Models.Flight newModelFlight)
        {
            foreach (FLTBizObjects.FLT.BO.FlightRegionTime boUsage in fltFlight.FlightRegionTimeList)
            {
                RegionTime newModelUsage = new RegionTime();

                newModelUsage.Hours = Convert.ToDecimal(boUsage.Hours);
                newModelUsage.RegionId = boUsage.RegionId;
                newModelUsage.RegionTimeId = boUsage.FlightRegionTimeId;

                newModelFlight.RegionTimes.Add(newModelUsage);
            }
        }

        #endregion

        #endregion
    }
}