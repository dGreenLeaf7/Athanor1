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
    public class CargosController : ApiController
    {
        #region Public CRUD methods

        [Route("Cargos")]
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
                int weightTypeId = GetWeightTypeIdForWeightTypeDescription("Cargo", fltIdentity);
                Cargos modelCargoList = new Cargos();

                FLTBizObjects.FLT.BO.WeightDefList fltAirframesList = FLTBizObjects.FLT.BL.WeightDefManager.GetList(ProgId, weightTypeId, fltIdentity);

                foreach (WeightDef thisWeightDef in fltAirframesList)
                {
                    modelCargoList.Add(CreateNewModelCargoObject(thisWeightDef));
                }
                if (modelCargoList.Count == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format(Resources.Messages.NotFoundCargoGetList, ProgId));
                }
                return Request.CreateResponse<Cargos>(HttpStatusCode.OK, modelCargoList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, String.Format(Resources.Messages.InternalErrorCargoGetList, ProgId));
            }
        }
        
        #endregion

        #region Private Subroutines

        private Cargo CreateNewModelCargoObject(WeightDef thisWeightDef)
        {
            try
            {
                Cargo newModelCargo = new Cargo();

                newModelCargo.CargoId = thisWeightDef.WeightDefId;
                newModelCargo.Description = thisWeightDef.WeightDefDescr;
                newModelCargo.Weight = thisWeightDef.Weight;
                newModelCargo.WeightUnit = thisWeightDef.WeightUnit;

                return newModelCargo;
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
