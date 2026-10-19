// Copyright (c) 2026 CODE Éxitos. All rights reserved.
// Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FltWebApi.App_Code;
using FLTBizObjects.FLT.BL;
using FLTBizObjects.FLT.BO;
using System.Web;

namespace FltWebApi.Controllers
{
    [RoutePrefix("api/flt")]
    public class ProgramsController : ApiController
    {
        [Route("programs")]
        [HttpGet]
        public HttpResponseMessage GetList()
        {
            try
            {
                FltIdentity fltIdentity = IdentityManager.Authenticate(HttpContext.Current);
                if (fltIdentity == null)
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, Resources.Messages.UserWasNotAuthenticated);
                }
                Models.Programs modelsProgramList = new Models.Programs();
                ProgramList boProgramList;
                boProgramList = FLTBizObjects.FLT.BL.ProgramManager.GetList();

                foreach (FLTBizObjects.FLT.BO.Program thisProgram in boProgramList)
                {
                    modelsProgramList.Add(CreateNewModelProgramObject(thisProgram));
                }

                if (modelsProgramList.Count == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, Resources.Messages.NotFoundProgramGetList);
                }

                return Request.CreateResponse<Models.Programs>(HttpStatusCode.OK, modelsProgramList);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, Resources.Messages.InternalErrorProgramGetList);
            }
        }

        [Route("programs/{ProgId}")]
        [HttpGet]
        public HttpResponseMessage GetItem(int ProgId)
        {
            try
            {
                FltIdentity fltIdentity = IdentityManager.Authenticate(HttpContext.Current);
                if (fltIdentity == null)
                {
                    return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, Resources.Messages.UserWasNotAuthenticated);
                }
                FLTBizObjects.FLT.BO.Program boProgram;
                boProgram = FLTBizObjects.FLT.BL.ProgramManager.GetItem(ProgId, fltIdentity);

                if (boProgram == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, Resources.Messages.NotFoundProgramGetItem);
                }

                return Request.CreateResponse<Models.Program>(HttpStatusCode.OK, CreateNewModelProgramObject(boProgram));
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, Resources.Messages.InternalErrorProgramGetItem);
            }
        }

        #region Private Subroutines
        private Models.Program CreateNewModelProgramObject(FLTBizObjects.FLT.BO.Program boProgram)
        {
            try
            {
                Models.Program modelProgram = new Models.Program();

                modelProgram.ProgID = boProgram.ProgID;
                modelProgram.ProgName = boProgram.ProgName;
                modelProgram.ProgDescription = boProgram.ProgDescription;
                modelProgram.ProgVersion = boProgram.ProgVersion;

                return modelProgram;
            }
            catch
            {
                throw new ApplicationException();
            }
        }
        #endregion
    }
}
