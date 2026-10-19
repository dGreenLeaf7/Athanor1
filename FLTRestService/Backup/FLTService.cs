// Copyright (c) 2026 CODE Éxitos. All rights reserved.
// Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using FLTBizObjects.FLT.BO;
using FLTBizObjects.FLT.BL;

namespace FltRestService
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    
    public class FLTService
    {
        #region "WebService Methods"

        [Description("Returns a list of Programs")]
        [WebGet(UriTemplate = "programs")]
        public FltRestService.Model.Programs GetProgramList()
        {
            FLTBizObjects.FLT.BO.FltIdentity fltIdentity = AuthenticateUser();

            FLTBizObjects.FLT.BO.ProgramList programList = null;
            FltRestService.Model.Programs programs = new Model.Programs();

            try
            {
                programList = ProgramManager.GetList();
            }
            catch (Exception ex)
            {
                ThrowInternalServerError(ex.Message);
                return null;
            }
            if (programList.Count == 0)
            {
                ThrowEmptyList();
                return null;
            }
            foreach (FLTBizObjects.FLT.BO.Program thisProgram in programList)
            {
                foreach (FLTBizObjects.FLT.BO.FltIdentity.UserProgram userProgram in fltIdentity.UserProgramsList)
                {
                    if (Convert.ToInt32(userProgram.ProgId) == thisProgram.ProgID)
                    {
                        programs.Add(CreateNewModelProgram(thisProgram));
                    }
                }
            }
            return programs;
        }

        [Description("Returns a Program")]
        [WebGet(UriTemplate = "programs/{ProgId}")]
        public FltRestService.Model.Program GetProgram(string ProgId)
        {
            FLTBizObjects.FLT.BO.FltIdentity fltIdentity = AuthenticateUser();
            int progId = ParseIdValue(ProgId);

            FLTBizObjects.FLT.BO.Program program = null;

            try
            {
                program = ProgramManager.GetItem(progId, fltIdentity);
            }
            catch (Exception ex)
            {
                ThrowInternalServerError(ex.Message);
                return null;
            }
            return CreateNewModelProgram(program);
        }

        [Description("Returns a list of all Locations for a specific Program")]
        [WebGet(UriTemplate = "programs/{ProgId}/locations")]
        public Model.Locations GetLocationList(string ProgId)
        {
            FLTBizObjects.FLT.BO.FltIdentity fltIdentity = AuthenticateUser();

            int progId = ParseIdValue(ProgId);

            FLTBizObjects.FLT.BO.LocationList locationList = null;
            Model.Locations locations = new Model.Locations();

            try
            {
                locationList = LocationManager.GetList(progId, fltIdentity);
            }
            catch (Exception ex)
            {
                ThrowInternalServerError(ex.Message);
                return null;
            }
            if (locationList.Count == 0)
            {
                ThrowEmptyList();
            }
            foreach (FLTBizObjects.FLT.BO.Location thisLocation in locationList)
            {
                locations.Add(CreateNewModelLocation(thisLocation));
            }
            return locations;
        }

        [Description("Returns a specific Location in a specific Program")]
        [WebGet(UriTemplate = "programs/{ProgId}/locations/{LocId}")]
        public Model.Location GetLocation(string ProgId, string LocId)
        {
            FLTBizObjects.FLT.BO.FltIdentity fltIdentity = AuthenticateUser();

            int progId = ParseIdValue(ProgId);
            int locId = ParseIdValue(LocId);

            FLTBizObjects.FLT.BO.Location location = null;

            try
            {
                location = LocationManager.GetItem(locId, fltIdentity);
            }
            catch (Exception ex)
            {
                ThrowInternalServerError(ex.Message);
                return null;
            }
                return CreateNewModelLocation(location);
        }

        [Description("Returns a list of all Fleet for a specific Program and Location")]
        [WebGet(UriTemplate = "programs/{ProgId}/locations/{LocId}/fleet")]
        public Model.Fleet GetAircraftList(string ProgId, string LocId)
        {
            FltIdentity fltIdentity = AuthenticateUser();

            int progId = ParseIdValue(ProgId);
            int locId = ParseIdValue(LocId);

            FLTBizObjects.FLT.BO.AircraftInfoList aircraftList = null;
            Model.Fleet fleet = new Model.Fleet();

            try
            {
                aircraftList = AircraftInfoManager.GetList(progId, locId, string.Empty, fltIdentity);
            }
            catch (Exception ex)
            {
                ThrowInternalServerError(ex.Message);
                return null;
            }
            if (aircraftList.Count == 0)
            {
                ThrowEmptyList();
            }
            foreach (FLTBizObjects.FLT.BO.AircraftInfo thisAircraft in aircraftList)
            {
                fleet.Add(CreateNewModelAircraft(thisAircraft));
            }
            return fleet;
        }

        [Description("Returns a specific Aircraft within a specific Program and Location")]
        [WebGet(UriTemplate = "programs/{ProgId}/locations/{LocId}/fleet/{Aid}")]
        public Model.Aircraft GetAircraft(string ProgId, string LocId, string Aid)
        {
            FltIdentity fltIdentity = AuthenticateUser();

            int progId = ParseIdValue(ProgId);
            int locId = ParseIdValue(LocId);
            int aId = ParseIdValue(Aid);

            FLTBizObjects.FLT.BO.Aircraft aircraft = null;

            try
            {
                aircraft = AircraftManager.GetItem(aId, fltIdentity);
            }
            catch (Exception ex)
            {
                ThrowInternalServerError(ex.Message);
                return null;
            }
                return CreateNewModelAircraft(aircraft);
        }

        [Description("Returns a list of all Routes for a specific Program and Location")]
        [WebGet(UriTemplate = "programs/{ProgId}/locations/{LocId}/routes")]
        public Model.Routes GetRouteList(string ProgId, string LocId)
        {
            FltIdentity fltIdentity = AuthenticateUser();

            int progId = ParseIdValue(ProgId);
            int locId = ParseIdValue(LocId);

            FLTBizObjects.FLT.BO.RouteList routeList = null;
            Model.Routes routes = new Model.Routes();

            try
            {
                routeList = RouteManager.GetList(progId, locId, fltIdentity);
            }
            catch (Exception ex)
            {
                ThrowInternalServerError(ex.Message);
                return null;
            }
            if (routeList.Count == 0)
            {
                ThrowEmptyList();
            }
            foreach (FLTBizObjects.FLT.BO.Route thisRoute in routeList)
            {
                routes.Add(CreateNewModelRoute(thisRoute));
            }
            return routes;
        }

        [Description("Returns a list of all Regions for a specific Program")]
        [WebGet(UriTemplate = "programs/{ProgId}/regions")]
        public Model.Regions GetRegionList(string ProgId)
        {
            FltIdentity fltIdentity = AuthenticateUser();

            int progId = ParseIdValue(ProgId);

            FLTBizObjects.FLT.BO.RegionList regionList = null;
            Model.Regions regions = new Model.Regions();

            try
            {
                regionList = RegionManager.GetList(progId, fltIdentity);
            }
            catch (Exception ex)
            {
                ThrowInternalServerError(ex.Message);
                return null;
            }
            if (regionList.Count == 0)
            {
                ThrowEmptyList();
            }
            foreach (FLTBizObjects.FLT.BO.Region thisRegion in regionList)
            {
                regions.Add(CreateNewModelRegion(thisRegion));
            }
            return regions;
        }

        [Description("Returns a list of all Airframes for a specific Program")]
        [WebGet(UriTemplate = "programs/{ProgId}/airframes")]
        public Model.Airframes GetAirframeList(string ProgId)
        {
            FltIdentity fltIdentity = AuthenticateUser();

            int progId = ParseIdValue(ProgId);

            int airframeId = GetWeightTypeIdForWeightTypeDescription("Airframe", fltIdentity);

            FLTBizObjects.FLT.BO.WeightDefList AirframeList = null;
            Model.Airframes Airframes = new Model.Airframes();

            try
            {
                AirframeList = WeightDefManager.GetList(progId, airframeId, fltIdentity);
            }
            catch (Exception ex)
            {
                ThrowInternalServerError(ex.Message);
                return null;
            }
            if (AirframeList.Count == 0)
            {
                ThrowEmptyList();
            }
            foreach (FLTBizObjects.FLT.BO.WeightDef thisAirframe in AirframeList)
            {
                Airframes.Add(CreateNewModelAirframe(thisAirframe));
            }
            return Airframes;
        }

        [Description("Returns a list of all Cargos for a specific Program")]
        [WebGet(UriTemplate = "programs/{ProgId}/cargos")]
        public Model.Cargos GetCargoList(string ProgId)
        {
            FltIdentity fltIdentity = AuthenticateUser();

            int progId = ParseIdValue(ProgId);

            int cargoId = GetWeightTypeIdForWeightTypeDescription("Cargo", fltIdentity);

            FLTBizObjects.FLT.BO.WeightDefList cargoList = null;
            Model.Cargos cargos = new Model.Cargos();

            try
            {
                cargoList = WeightDefManager.GetList(progId, cargoId, fltIdentity);
            }
            catch (Exception ex)
            {
                ThrowInternalServerError(ex.Message);
                return null;
            }
            if (cargoList.Count == 0)
            {
                ThrowEmptyList();
            }
            foreach (FLTBizObjects.FLT.BO.WeightDef thiscargo in cargoList)
            {
                cargos.Add(CreateNewModelCargo(thiscargo));
            }
            return cargos;
        }

        [Description("Returns a list of all Dollies for a specific Program")]
        [WebGet(UriTemplate = "programs/{ProgId}/dollies")]
        public Model.Dollies GetBallastList(string ProgId)
        {
            FltIdentity fltIdentity = AuthenticateUser();

            int progId = ParseIdValue(ProgId);

            int ballastId = GetWeightTypeIdForWeightTypeDescription("Ballast", fltIdentity);

            FLTBizObjects.FLT.BO.WeightDefList ballastList = null;
            Model.Dollies dollies = new Model.Dollies();

            try
            {
                ballastList = WeightDefManager.GetList(progId, ballastId, fltIdentity);
            }
            catch (Exception ex)
            {
                ThrowInternalServerError(ex.Message);
                return null;
            }
            if (ballastList.Count == 0)
            {
                ThrowEmptyList();
            }
            foreach (FLTBizObjects.FLT.BO.WeightDef thisBallast in ballastList)
            {
                dollies.Add(CreateNewModelBallast(thisBallast));
            }
            return dollies;
        }

        [Description("Gets a list of Flight Infos for a specific Aircraft at a Location in a Program")]
        [WebInvoke(UriTemplate = "programs/{ProgId}/locations/{LocId}/fleet/{Aid}/flightinfos?dateFrom={DateFrom}&dateThru={DateThru}&rowStart={RowStart}&nbrOfRows={NbrOfRows}", Method="GET")]
        public Model.FlightInfos GetFlightsList(string ProgId, string LocId, string Aid, string DateFrom, string DateThru, string RowStart, string NbrOfRows)
        {
            FltIdentity fltIdentity = AuthenticateUser();

            int progId = ParseIdValue(ProgId);
            int locId = ParseIdValue(LocId);
            int aId = ParseIdValue(Aid);
            int schedRouteId = 0;
            int flownRouteId = 0;
            int workShiftIndicatorId = 0;
            string filterDateBegin = ParseDateValue("DateFrom", DateFrom);
            string filterDateEnd = ParseDateValue("DateThru", DateThru);
            int isApprovedFilter = -1;
            int isActiveFilter = 1;
            int isDeletedFilter = 0;
            int rowStart = ParseRowValue("RowStart", RowStart, 0, 999999);
            int rowCount = ParseRowValue("NbrOfRows", NbrOfRows, 1, 100);
            int totalRows = 0;

            FLTBizObjects.FLT.BO.FlightInfoList flightList = null;
            Model.FlightInfos flightInfos = new Model.FlightInfos();

            try
            {
                flightList = FlightInfoManager.GetList(aId, "", locId, schedRouteId, flownRouteId, workShiftIndicatorId, filterDateBegin, filterDateEnd, isApprovedFilter, isActiveFilter, isDeletedFilter, rowStart, rowCount, ref totalRows, fltIdentity);
            }
            catch (Exception ex)
            {
                ThrowInternalServerError(ex.Message);
                return null;
            }
            if (flightList.Count == 0)
            {
                ThrowEmptyList();
            }
            foreach (FLTBizObjects.FLT.BO.FlightInfo thisFlight in flightList)
            {
                flightInfos.Add(CreateNewModelFlightInfo(thisFlight));
            }
            return flightInfos;
        }

        [Description("Gets one Flight for a specific Aircraft")]
        [WebInvoke(UriTemplate = "programs/{Progid}/locations/{LocId}/fleet/{Aid}/flights/{Tid}", Method = "GET")]
        public Model.Flight GetFlight(string ProgId, string LocId, string Aid, string Tid)
        {
            FLTBizObjects.FLT.BO.FltIdentity fltIdentity = AuthenticateUser();
            int progId = ParseIdValue(ProgId);
            int locId = ParseIdValue(LocId);
            int aId = ParseIdValue(Aid);
            int tId = ParseIdValue(Tid);

            FLTBizObjects.FLT.BO.Flight flight = null;

            try
            {
                flight = FlightManager.GetItem(tId, fltIdentity);
            }
            catch (Exception ex)
            {
                ThrowInternalServerError(ex.Message);
                return null;
            }
            return CreateNewModelFlight(flight);
        }

        [Description("Echoes a simple POST function, returning the XML structure")]
        [WebInvoke(UriTemplate = "posttest", Method = "POST")]
        public string testPost(Model.TestString postedXML)
        {
            FLTBizObjects.FLT.BO.FltIdentity fltIdentity = AuthenticateUser();

            string acceptedValue = postedXML.Value1;

            return acceptedValue;
        }
        
        [Description("Adds a New Flight record and returns the URI for the newly added Flight")]
        [WebInvoke(UriTemplate = "programs/{ProgId}/locations/{LocId}/fleet/{Aid}/flights", Method = "POST")]
        public string AddNewFlight(string ProgId, string LocId, string Aid, Model.Flight Flight)
        {
            FLTBizObjects.FLT.BO.FltIdentity fltIdentity = AuthenticateUser();

            int progId = ParseIdValue(ProgId);
            int locId = ParseIdValue(LocId);
            int aircraftId = ParseIdValue(Aid);

            FLTBizObjects.FLT.BO.Flight boFlight = new Flight();

            #region "Load Flight Object"

            boFlight.HobbsEnd = Flight.HobbsEnd.ToString("##0.0");
            boFlight.HobbsStart = Flight.HobbsStart.ToString("##0.0");
            boFlight.DepartureTime = Flight.DepartureTime;
            boFlight.BallastWeightId = Flight.BallastId;
            boFlight.FlownRouteId = Flight.FlownRouteId;
            boFlight.PilotId = Flight.EvaluatorId;
            boFlight.EngineHoursEnd = Flight.EngineHoursEnd.ToString("##0.0");
            boFlight.EngineHoursStart = Flight.EngineHoursStart.ToString("##0.0");
            boFlight.NacelleCheckBuildUp = Flight.NacelleCheckBuildUp;
            boFlight.NacelleCheckDiscoloration = Flight.NacelleCheckDiscoloration;
            boFlight.IsActive = true;
            boFlight.IsDeleted = false;
            boFlight.LocationId = locId;
            boFlight.MeterUnitId = Flight.MeterUnitId;
            boFlight.OilLevelIndicatorStart = Flight.OilLevelIndicatorStart;
            boFlight.PostFlightFtiOK = Flight.PostFlightFtiOK;
            boFlight.PostFlightTelemetryCheckComplete = Flight.PostFlightTelemetryCheckComplete;
            boFlight.PreFlightFtiOK = Flight.PreFlightFtiOK;
            boFlight.PreFlightTelemetryCheckComplete = Flight.PreFlightTelemetryCheckComplete;
            boFlight.PreFlightTelemetryOK = Flight.PreFlightTelemetryOK;
            boFlight.ReturnTime = Flight.ReturnTime;
            boFlight.SchedRouteId = Flight.SchedRouteId;
            boFlight.AirframeWeightId = Flight.AirframeId;
            boFlight.Cargo1WeightId = Flight.Cargo1Id;
            boFlight.Cargo2WeightId = Flight.Cargo2Id;
            boFlight.FlightId = -1;
            boFlight.FlightDate = Flight.FlightDate;
            boFlight.HydraulicLevelIndicatorStart = Flight.HydraulicLevelIndicatorStart;
            boFlight.UsedMeterOffset = Convert.ToString(Flight.UsedMeterOffset);
            boFlight.AirframeHoursEnd = Convert.ToString(Convert.ToDecimal(boFlight.HobbsEnd) + Convert.ToDecimal(boFlight.UsedMeterOffset));
            boFlight.AirframeHoursStart = Convert.ToString(Convert.ToDecimal(boFlight.HobbsStart) + Convert.ToDecimal(boFlight.UsedMeterOffset));
            boFlight.AircraftId = aircraftId;
            boFlight.WorkShiftIndicatorId = Flight.WorkShiftIndicatorId;

            #region "LiquidFillList"
            foreach (Model.LiquidFill thisLiquidFill in Flight.LiquidFills)
            {
                FLTBizObjects.FLT.BO.LiquidFill boLiquidFill = new LiquidFill();
                boLiquidFill.HobbsReading = thisLiquidFill.HobbsReading.ToString("##0.0");
                boLiquidFill.LiquidAmt = thisLiquidFill.LiquidAmt.ToString("##0.000");
                boLiquidFill.LiquidFillApprover = thisLiquidFill.LiquidFillApprover;
                boLiquidFill.LiquidFillLocation = thisLiquidFill.LiquidFillLocation;
                boLiquidFill.LiquidGroupId = thisLiquidFill.LiquidFillTypeId;
                boLiquidFill.LiquidTypeId = thisLiquidFill.LiquidFillSubTypeId;
                boLiquidFill.LiquidUnitId = thisLiquidFill.LiquidUnitId;
                boLiquidFill.FlightId = boFlight.FlightId;

                FLTBizObjects.FLT.BL.FlightManager.InsertLiquidFill(boFlight, boLiquidFill);
            }
            #endregion

            #region "FlightCommentList"
            foreach (Model.FlightComment thisComment in Flight.FlightComments)
            {
                FLTBizObjects.FLT.BO.FlightComment boComment = new FlightComment();
                boComment.CommentRating = thisComment.CommentRating;
                boComment.CommentTime = thisComment.CommentTime;
                boComment.ComponentId = thisComment.ComponentId;
                boComment.PilotComment = thisComment.EvaluatorComment;
                boComment.IssueCodeId = thisComment.IssueCodeId;
                boComment.FlightId = boFlight.FlightId;

                FLTBizObjects.FLT.BL.FlightManager.InsertFlightComment(boFlight, boComment);
            }
            #endregion

            #region "FlightEngineRunList"
            foreach (Model.EngineRun thisRun in Flight.EngineRuns)
            {
                FLTBizObjects.FLT.BO.FlightEngineRun boRun = new FlightEngineRun();
                boRun.EngineSpeed = thisRun.EngineSpeed.ToString("#,##0");
                boRun.RunEndTime = thisRun.RunEndTime;
                boRun.RunStartTime = thisRun.RunStartTime;
                boRun.FlightId = boFlight.FlightId;

                FLTBizObjects.FLT.BL.FlightManager.InsertFlightEngineRun(boFlight, boRun);
            }
            #endregion

            #region "FlightRegionTimeList"
            foreach (Model.RegionTime thisUsage in Flight.RegionTimes)
            {
                FLTBizObjects.FLT.BO.FlightRegionTime boUsage = new FlightRegionTime();
                boUsage.Hours = thisUsage.Hours.ToString("#,##0.0");
                boUsage.RegionId = thisUsage.RegionId;
                boUsage.FlightId = boFlight.FlightId;

                FLTBizObjects.FLT.BL.FlightManager.InsertFlightRegionTime(boFlight, boUsage);
            }

            #endregion

            #endregion

            try
            {
                int flightid = FLTBizObjects.FLT.BL.FlightManager.Insert(boFlight, fltIdentity, false);
                string flightUri = "programs/" + ProgId + "/locations/" + LocId + "/fleet/" + Aid + "/flights/" + flightid.ToString();
                return flightUri;
            }
            catch (Exception ex)
            {
                ThrowInternalServerError(ex.Message);
                return null;
            }
        }

        #region "LookUpValues"

        [Description("Returns a list of all Evaluators (aka Pilots) for a specific Program")]
        [WebGet(UriTemplate = "programs/{ProgId}/evaluators")]
        public Model.LookUpValues GetPilotList(string ProgId)
        {
            return GetLookUpValueList("Pilot", ProgId);
        }

        [Description("Returns a list of all Work Shifts for a specific Program")]
        [WebGet(UriTemplate = "programs/{ProgId}/workshifts")]
        public Model.LookUpValues GetWorkShiftList(string ProgId)
        {
            return GetLookUpValueList("WorkShift", ProgId);
        }

        [Description("Returns a list of all liquidfill types for a specific Program")]
        [WebGet(UriTemplate = "programs/{ProgId}/liquidfilltypes")]
        public Model.LookUpValues GetLiquidGroupList(string ProgId)
        {
            return GetLookUpValueList("LiquidGroup", ProgId);
        }

        [Description("Returns a list of all liquidfill sub-types for a specific Program")]
        [WebGet(UriTemplate = "programs/{ProgId}/liquidfillsubtypes")]
        public Model.LookUpValues GetLiquidTypeList(string ProgId)
        {
            return GetLookUpValueList("LiquidType", ProgId);
        }

        [Description("Returns a list of all liquidfill units for a specific Program")]
        [WebGet(UriTemplate = "programs/{ProgId}/liquidfillunits")]
        public Model.LookUpValues GetLiquidUnitList(string ProgId)
        {
            return GetLookUpValueList("LiquidUnit", ProgId);
        }

        [Description("Returns a list of all flight comment component codes for a specific Program")]
        [WebGet(UriTemplate = "programs/{ProgId}/commentcomponentcodes")]
        public Model.LookUpValues GetAircraftComponentList(string ProgId)
        {
            return GetLookUpValueList("AircraftComponent", ProgId);
        }

        [Description("Returns a list of all flight comment issue codes for a specific Program")]
        [WebGet(UriTemplate = "programs/{ProgId}/commentissuecodes")]
        public Model.LookUpValues GetIssueCodeList(string ProgId)
        {
            return GetLookUpValueList("IssueCode", ProgId);
        }

        [Description("Returns a list of all length units for a specific Program")]
        [WebGet(UriTemplate = "programs/{ProgId}/lengthunits")]
        public Model.LookUpValues GetLengthUnitList(string ProgId)
        {
            return GetLookUpValueList("LengthUnit", ProgId);
        }

        private Model.LookUpValues GetLookUpValueList(string LookUpBaseName, string ProgId)
        {
            FltIdentity fltIdentity = AuthenticateUser();

            int progId = ParseIdValue(ProgId);

            FLTBizObjects.FLT.BO.fltLookupBaseList lookUpBaseList = null;
            Model.LookUpValues lookUpBases = new Model.LookUpValues();

            try
            {
                lookUpBaseList = fltLookupBaseManager.GetList(LookUpBaseName, progId);
            }
            catch (Exception ex)
            {
                ThrowInternalServerError(ex.Message);
                return null;
            }
            if (lookUpBaseList.Count == 0)
            {
                ThrowEmptyList();
            }
            foreach (FLTBizObjects.FLT.BO.fltLookupBase thisLookUpBase in lookUpBaseList)
            {
                lookUpBases.Add(CreateNewModelLookUpBase(thisLookUpBase));
            }
            return lookUpBases;
        }

        #endregion

        #endregion

        #region "Authentication"

        private FLTBizObjects.FLT.BO.FltIdentity AuthenticateUser()
        {
            string userName = string.Empty;
            string password = string.Empty;

            userName = System.Configuration.ConfigurationManager.AppSettings.Get("userName").ToString();
            password = System.Configuration.ConfigurationManager.AppSettings.Get("password").ToString();

            var ctx = WebOperationContext.Current;
            string encryptedString = ctx.IncomingRequest.Headers[HttpRequestHeader.Authorization];

            if (encryptedString != null)
            {
                if (encryptedString.Substring(0, 6) == "Basic ")
                {
                    string decryptedString = new ASCIIEncoding().GetString(Convert.FromBase64String(encryptedString.Substring(6)));

                    int delimiterPosition = decryptedString.IndexOf(":");
                    userName = decryptedString.Substring(0, delimiterPosition);
                    password = decryptedString.Substring(delimiterPosition + 1, decryptedString.Length - delimiterPosition - 1);
                }
            }

            try
            {
                return FltIdentityManager.AuthenticateUserLogin(userName, password);
            }
            catch 
            {
                ThrowUserUnauthorized();
                return null;
            }
        }

        #endregion

        #region "Error Processing"

        private static void ThrowUserUnauthorized()
        {
            throw new WebFaultException<string>(
                "User is not Authorized",
                HttpStatusCode.Unauthorized);
        }

        private static void ThrowNoSuchItem(int id)
        {
            throw new WebFaultException<string>(
                "No information returned for id " + Convert.ToString(id),
                HttpStatusCode.NotFound);
        }

        private static void ThrowEmptyList()
        {
            throw new WebFaultException<string>(
                "Request Returned No Information",
                HttpStatusCode.NotFound);
        }

        private static void ThrowInternalServerError(string errorMessage)
        {
            throw new WebFaultException<string>(
                "Error occurred on Server: " + errorMessage,
                HttpStatusCode.InternalServerError);
        }

        #endregion

        #region "Private Functions"

        private static int ParseIdValue(string id)
        {
            int parsedId;
            if ((id == null) || (!int.TryParse(id.Trim(), out parsedId)))
            {
                throw new WebFaultException<string>(
                    string.Format("The value '{0}' is not a valid id. The id must be an integer.", id),
                    HttpStatusCode.BadRequest);
            }
            return parsedId;
        }

        private static string ParseDateValue(string DateType, string DateValue)
        {
            if ((DateValue == null) || (DateValue.Trim().Length == 0))
            {
                return String.Empty;
            }
            DateTime dateValue;
            if (!DateTime.TryParse(DateValue.Trim(), out dateValue))
            {
                throw new WebFaultException<string>(
                    string.Format("The {0} value '{1}' is not a valid value. It must be a date.", DateType, DateValue),
                    HttpStatusCode.BadRequest);
            }
            return dateValue.ToString("yyyy-MM-dd");
        }

        private static int ParseRowValue(string ValueType, string RowValue, int MinValue, int MaxValue)
        {
            if ((RowValue == null) || (RowValue.Trim().Length == 0))
            {
                return -1;
            }
            int rowValue;
            if (!int.TryParse(RowValue.Trim(), out rowValue))
            {
                throw new WebFaultException<string>(
                    string.Format("The {0} value '{1}' is not a valid value. It must be an integer.", ValueType, RowValue),
                    HttpStatusCode.BadRequest);
            }
            if ((rowValue < MinValue) || (rowValue > MaxValue))
            {
                throw new WebFaultException<string>(
                    string.Format("The {0} value '{1}' must be between {2} and {3}'", ValueType, RowValue, MinValue, MaxValue),
                    HttpStatusCode.BadRequest);
            }
            return rowValue;
        }

        private static int GetWeightTypeIdForWeightTypeDescription(string WeightTypeDescription, FltIdentity fltIdentity)
        {
            FLTBizObjects.FLT.BO.WeightDefTypeList weightDefTypeList = FLTBizObjects.FLT.BL.WeightDefTypeManager.GetList(fltIdentity);
            foreach (FLTBizObjects.FLT.BO.WeightDefType thisType in weightDefTypeList)
            {
                if (thisType.WeightDefTypeDescr.ToLower() == WeightTypeDescription.ToLower())
                {
                    return thisType.WeightDefTypeId;
                }
            }
            return 0;
        }

        #endregion

        #region "Object Conversions"

        private static FltRestService.Model.Program CreateNewModelProgram(FLTBizObjects.FLT.BO.Program boProgram)
        {
            FltRestService.Model.Program modelProgram = new Model.Program();

            modelProgram.ProgID = boProgram.ProgID;
            modelProgram.ProgName = boProgram.ProgName;
            modelProgram.ProgDescription = boProgram.ProgDescription;
            modelProgram.ProgVersion = boProgram.ProgDescription;

            return modelProgram;
        }

        private static FltRestService.Model.Location CreateNewModelLocation(FLTBizObjects.FLT.BO.Location boLocation)
        {
            FltRestService.Model.Location modelLocation = new Model.Location();

            modelLocation.LocationId = boLocation.LocactionId;
            modelLocation.LocationName = boLocation.LocationName;
            modelLocation.ProgId = boLocation.ProgId;

            return modelLocation;
        }

        private static FltRestService.Model.Aircraft CreateNewModelAircraft(FLTBizObjects.FLT.BO.AircraftInfo boAircraft)
        {
            FltRestService.Model.Aircraft modelAircraft = new Model.Aircraft();

            modelAircraft.AircraftId = boAircraft.AircraftId;
            modelAircraft.MSN = boAircraft.MSN;
            modelAircraft.RegistrationNbr = boAircraft.RegistrationNbr;
            modelAircraft.ModelName = boAircraft.ModelName;
            modelAircraft.Description = boAircraft.Description;
            modelAircraft.MeterUnitId = boAircraft.AirframeHoursUnitId;
            modelAircraft.AirframeHoursCurrent = Convert.ToDecimal(boAircraft.AirframeHoursCurrent) - Convert.ToDecimal(boAircraft.AirframeHoursOffset);
            modelAircraft.AirframeHoursOffset = Convert.ToDecimal(boAircraft.AirframeHoursOffset);

            return modelAircraft;
        }

        private static FltRestService.Model.Aircraft CreateNewModelAircraft(FLTBizObjects.FLT.BO.Aircraft boAircraft)
        {
            FltRestService.Model.Aircraft modelAircraft = new Model.Aircraft();

            modelAircraft.AircraftId = boAircraft.AircraftId;
            modelAircraft.MSN = boAircraft.MSN;
            modelAircraft.RegistrationNbr = boAircraft.RegistrationNbr;
            modelAircraft.ModelName = boAircraft.Model;
            modelAircraft.Description = boAircraft.Description;
            modelAircraft.MeterUnitId = boAircraft.MeterUnitId;
            modelAircraft.AirframeHoursCurrent = Convert.ToDecimal(boAircraft.AirframeHoursCurrent) - Convert.ToDecimal(boAircraft.AirframeHoursOffset);
            modelAircraft.AirframeHoursOffset = Convert.ToDecimal(boAircraft.AirframeHoursOffset);

            return modelAircraft;
        }

        private static FltRestService.Model.Route CreateNewModelRoute(FLTBizObjects.FLT.BO.Route boRoute)
        {
            FltRestService.Model.Route modelRoute = new Model.Route();

            modelRoute.RouteId = boRoute.RouteId;
            modelRoute.Name = boRoute.Name;
            modelRoute.Description = boRoute.Description;

            return modelRoute;
        }

        private static FltRestService.Model.Region CreateNewModelRegion(FLTBizObjects.FLT.BO.Region boRegion)
        {
            FltRestService.Model.Region modelRegion = new Model.Region();

            modelRegion.Id = boRegion.Id;
            modelRegion.Code = boRegion.Code;
            modelRegion.Name = boRegion.Name;

            return modelRegion;
        }

        private static FltRestService.Model.LookUpValue CreateNewModelLookUpBase(FLTBizObjects.FLT.BO.fltLookupBase boLookUpBase)
        {
            Model.LookUpValue modelLookUpBase = new Model.LookUpValue();

            modelLookUpBase.ID = boLookUpBase.ID;
            modelLookUpBase.SortOrder = boLookUpBase.SortOrder;
            modelLookUpBase.StringValue = boLookUpBase.StringValue;

            return modelLookUpBase;
        }

        private static FltRestService.Model.Airframe CreateNewModelAirframe(FLTBizObjects.FLT.BO.WeightDef boAirframe)
        {
            Model.Airframe modelAirframe = new Model.Airframe();

            modelAirframe.AirframeId = boAirframe.WeightDefId;
            modelAirframe.Description = boAirframe.WeightDefDescr;
            modelAirframe.Weight = boAirframe.Weight;
            modelAirframe.WeightUnit = boAirframe.WeightUnit;

            return modelAirframe;
        }

        private static FltRestService.Model.Cargo CreateNewModelCargo(FLTBizObjects.FLT.BO.WeightDef boCargo)
        {
            Model.Cargo modelCargo = new Model.Cargo();

            modelCargo.CargoId = boCargo.WeightDefId;
            modelCargo.Description = boCargo.WeightDefDescr;
            modelCargo.Weight = boCargo.Weight;
            modelCargo.WeightUnit = boCargo.WeightUnit;

            return modelCargo;
        }

        private static FltRestService.Model.Ballast CreateNewModelBallast(FLTBizObjects.FLT.BO.WeightDef boBallast)
        {
            Model.Ballast modelBallast = new Model.Ballast();

            modelBallast.BallastId = boBallast.WeightDefId;
            modelBallast.Description = boBallast.WeightDefDescr;
            modelBallast.Weight = boBallast.Weight;
            modelBallast.WeightUnit = boBallast.WeightUnit;

            return modelBallast;
        }

        private static FltRestService.Model.FlightInfo CreateNewModelFlightInfo(FLTBizObjects.FLT.BO.FlightInfo boFlightInfo)
        {
            Model.FlightInfo modelFlightInfo = new Model.FlightInfo();

            modelFlightInfo.FlightId = boFlightInfo.FlightId;
            modelFlightInfo.FlightDate = boFlightInfo.FlightDate;
            modelFlightInfo.AirframeHoursStart = Convert.ToDecimal(boFlightInfo.AirframeHoursStart);
            modelFlightInfo.AirframeHoursEnd = Convert.ToDecimal(boFlightInfo.AirframeHoursEnd);
            modelFlightInfo.DepartureTime = boFlightInfo.DepartureTime;
            modelFlightInfo.ReturnTime = boFlightInfo.ReturnTime;
            
            return modelFlightInfo;
        }

        private static FltRestService.Model.Flight CreateNewModelFlight(FLTBizObjects.FLT.BO.Flight boFlight)
        {
            Model.Flight modelFlight = new Model.Flight();

            #region "Flight Properties"
            modelFlight.HobbsEnd = Convert.ToDecimal(boFlight.HobbsEnd);
            modelFlight.HobbsStart = Convert.ToDecimal(boFlight.HobbsStart);
            modelFlight.DepartureTime = boFlight.DepartureTime;
            modelFlight.BallastId = boFlight.BallastWeightId;
            modelFlight.FlownRouteId = boFlight.FlownRouteId;
            modelFlight.EngineHoursEnd = Convert.ToDecimal(boFlight.EngineHoursEnd);
            modelFlight.EngineHoursStart = Convert.ToDecimal(boFlight.EngineHoursStart);
            modelFlight.EvaluatorId = boFlight.PilotId;
            modelFlight.NacelleCheckBuildUp = boFlight.NacelleCheckBuildUp;
            modelFlight.NacelleCheckDiscoloration = boFlight.NacelleCheckDiscoloration;
            modelFlight.LocationId = boFlight.LocationId;
            modelFlight.MeterUnitId = boFlight.MeterUnitId;
            modelFlight.OilLevelIndicatorStart = boFlight.OilLevelIndicatorStart;
            modelFlight.PostFlightFtiOK = boFlight.PostFlightFtiOK;
            modelFlight.PostFlightTelemetryCheckComplete = boFlight.PostFlightTelemetryCheckComplete;
            modelFlight.PreFlightFtiOK = boFlight.PreFlightFtiOK;
            modelFlight.PreFlightTelemetryCheckComplete = boFlight.PreFlightTelemetryCheckComplete;
            modelFlight.PreFlightTelemetryOK = boFlight.PreFlightTelemetryOK;
            modelFlight.ReturnTime = boFlight.ReturnTime;
            modelFlight.SchedRouteId = boFlight.SchedRouteId;
            modelFlight.AirframeId = boFlight.AirframeWeightId;
            modelFlight.Cargo1Id = boFlight.Cargo1WeightId;
            modelFlight.Cargo2Id = boFlight.Cargo2WeightId;
            modelFlight.FlightDate = boFlight.FlightDate;
            modelFlight.FlightId = boFlight.FlightId;
            modelFlight.HydraulicLevelIndicatorStart = modelFlight.HydraulicLevelIndicatorStart;
            modelFlight.AircraftId = modelFlight.AircraftId;
            modelFlight.WorkShiftIndicatorId = modelFlight.WorkShiftIndicatorId;
            #endregion

            #region "LiquidFills"
            foreach (FLTBizObjects.FLT.BO.LiquidFill boLiquidFill in boFlight.LiquidFillList)
            {
                Model.LiquidFill modelLiquidFill = new Model.LiquidFill();
                
                modelLiquidFill.HobbsReading = Convert.ToDecimal(boLiquidFill.HobbsReading);
                modelLiquidFill.LiquidAmt = Convert.ToDecimal(boLiquidFill.LiquidAmt);
                modelLiquidFill.LiquidFillApprover = boLiquidFill.LiquidFillApprover;
                modelLiquidFill.LiquidFillId = boLiquidFill.LiquidFillId;
                modelLiquidFill.LiquidFillLocation = boLiquidFill.LiquidFillLocation;
                modelLiquidFill.LiquidFillTypeId = boLiquidFill.LiquidGroupId;
                modelLiquidFill.LiquidFillSubTypeId = boLiquidFill.LiquidTypeId;
                modelLiquidFill.LiquidUnitId = boLiquidFill.LiquidUnitId;

                modelFlight.LiquidFills.Add(modelLiquidFill);
            }
            #endregion

            #region "FlightComments"
            foreach (FLTBizObjects.FLT.BO.FlightComment boComment in boFlight.FlightCommentList)
            {
                Model.FlightComment modelComment = new Model.FlightComment();

                modelComment.CommentRating = boComment.CommentRating;
                modelComment.CommentTime = boComment.CommentTime;
                modelComment.ComponentId = boComment.ComponentId;
                modelComment.EvaluatorComment = boComment.PilotComment;
                modelComment.IssueCodeId = boComment.IssueCodeId;
                modelComment.FlightCommentId = boComment.FlightCommentId;

                modelFlight.FlightComments.Add(modelComment);
            }
            #endregion

            #region "EngineRuns"
            foreach (FLTBizObjects.FLT.BO.FlightEngineRun boRun in boFlight.FlightEngineRunList)
            {
                Model.EngineRun modelRun = new Model.EngineRun();

                modelRun.EngineSpeed = Convert.ToInt32(boRun.EngineSpeed);
                modelRun.EngineRunId = boRun.FlightEngineRunId;
                modelRun.RunEndTime = boRun.RunEndTime;
                modelRun.RunStartTime = boRun.RunStartTime;

                modelFlight.EngineRuns.Add(modelRun);
            }
            #endregion

            #region "RegionTimes"
            foreach (FLTBizObjects.FLT.BO.FlightRegionTime boUsage in boFlight.FlightRegionTimeList)
            {
                Model.RegionTime modelUsage = new Model.RegionTime();

                modelUsage.Hours = Convert.ToDecimal(boUsage.Hours);
                modelUsage.RegionId = boUsage.RegionId;
                modelUsage.RegionTimeId = boUsage.FlightRegionTimeId;

                modelFlight.RegionTimes.Add(modelUsage);
            }
            #endregion

            return modelFlight;
        }

        #endregion
    }
}
