' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.

Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.Dal
Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Configuration

Namespace FLT.BL

    Public Class FltExportManager

        Private Const UtcDateFormat As String = "yyyy-MM-ddTHH:mm:ssZ"

#Region "Business Object Methods"

        Public Shared Sub CreateDownloadFile(ByVal PhysicalDownloadFolderPath As String, ByVal FLTIdentity As FltIdentity)
            Try
                CreateDownloadFile(PhysicalDownloadFolderPath, CDate("01-Jan-2000"), FLTIdentity)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Public Shared Sub CreateDownloadFile(ByVal PhysicalDownloadFolderPath As String, ByVal ChangesSinceDate As Date, ByVal fltIdentity As FltIdentity)
            If CanView(fltIdentity) Then
                BuildDownloadFile(PhysicalDownloadFolderPath, ChangesSinceDate, fltIdentity)
            Else
                Throw New Exception("Can't Export - user is not authorized")
            End If
        End Sub

        Public Shared Function CanView(ByVal FltIdentity As FltIdentity)
            Return FltIdentityManager.AuthorizeUserAction(FltIdentity, "Export", fltUserAction.CanView)
        End Function

#End Region

#Region "Private Procedures"

        Private Shared Sub BuildDownloadFile(ByVal PhysicalDownloadFolderPath As String, ByVal ChangesSince As Date, ByVal FltIdentity As FltIdentity)

            Dim currentDateTimeString As String = Format(Date.UtcNow, "yyyyMMddTHHmmssz")
            Dim appPrefix As String = ConfigurationManager.AppSettings("appPrefix")
            Dim tempFilePath As String = PhysicalDownloadFolderPath & "\Temp_Download_" & appPrefix & "_" & currentDateTimeString & ".xml"
            Dim finalFileName As String = "Download_" & appPrefix & "_" & currentDateTimeString & ".xml"

            Using xtw As New XmlTextWriter(tempFilePath, Encoding.UTF8)

                xtw.Formatting = Formatting.Indented

                xtw.WriteStartDocument()

                xtw.WriteStartElement("Programs")
                xtw.WriteAttributeString("ChangesSince", Format(ChangesSince, UtcDateFormat))

                Dim programList As ProgramList = ProgramManager.GetList()

                For Each thisProgram As Program In programList

                    ProcessOneProgram(xtw, thisProgram, ChangesSince, FltIdentity)

                Next

                xtw.WriteEndElement()

                xtw.Close()

            End Using

            My.Computer.FileSystem.RenameFile(tempFilePath, finalFileName)
        End Sub

        Private Shared Sub ProcessOneProgram(ByVal xtw As XmlTextWriter, ByVal thisProgram As Program, ByVal ChangesSince As Date, ByVal FltIdentity As FltIdentity)

            xtw.WriteStartElement("Program")
            xtw.WriteAttributeString("ProgramGUID", thisProgram.ProgramGuid.ToString)
            xtw.WriteAttributeString("UTCDateCreated", Format(thisProgram.UtcDateCreated, UtcDateFormat))
            xtw.WriteAttributeString("UTCDateModified", Format(thisProgram.UtcDateModified, UtcDateFormat))
            xtw.WriteAttributeString("IsActive", thisProgram.IsActive.ToString)
            xtw.WriteAttributeString("IsDeleted", thisProgram.IsDeleted.ToString)

            xtw.WriteElementString("ProgramName", thisProgram.ProgName)

            xtw.WriteElementString("Customer", thisProgram.CompanyName)

            Dim dateOpened As String = String.Empty
            If IsDate(thisProgram.DateOpened) Then
                dateOpened = Format(CDate(thisProgram.DateOpened), "yyyy-MM-dd")
            End If
            xtw.WriteElementString("DateOpened", dateOpened)

            Dim dateClosed As String = String.Empty
            If Not (thisProgram.DateClosed = Nothing) Then
                If IsDate(thisProgram.DateClosed) Then
                    dateClosed = Format(CDate(thisProgram.DateClosed), "yyyy-MM-dd")
                End If
            End If
            xtw.WriteElementString("DateClosed", dateClosed)

            xtw.WriteStartElement("Fleet")

            Dim totRows As Integer
            Dim aircraftInfoList As AircraftInfoList = AircraftInfoManager.GetList(thisProgram.ProgID, 0, String.Empty, -1, -1, -1, -1, totRows, FltIdentity)

            For Each thisAircraftInfo As AircraftInfo In aircraftInfoList

                ProcessOneAircraft(xtw, thisAircraftInfo.AircraftId, ChangesSince, FltIdentity)

            Next

            xtw.WriteEndElement()

            xtw.WriteEndElement()

        End Sub

        Private Shared Sub ProcessOneAircraft(ByVal xtw As XmlTextWriter, ByVal AircraftId As Integer, ByVal ChangesSince As Date, ByVal FltIdentity As FltIdentity)

            Dim thisAircraft As Aircraft = AircraftManager.GetItem(AircraftId, FltIdentity)

            xtw.WriteStartElement("Aircraft")
            xtw.WriteAttributeString("AircraftGUID", thisAircraft.AircraftGuid.ToString)
            xtw.WriteAttributeString("UTCDateCreated", Format(thisAircraft.UtcDateCreated, UtcDateFormat))
            xtw.WriteAttributeString("UTCDateModified", Format(thisAircraft.UtcDateModified, UtcDateFormat))
            xtw.WriteAttributeString("IsActive", thisAircraft.IsActive.ToString)
            xtw.WriteAttributeString("IsDeleted", thisAircraft.IsDeleted.ToString)

            xtw.WriteElementString("MSN", thisAircraft.MSN)
            xtw.WriteElementString("ShipNbr", thisAircraft.ShipNbr)
            xtw.WriteElementString("VSerNo", thisAircraft.SerialNbr)
            xtw.WriteElementString("Model", thisAircraft.Model)
            xtw.WriteElementString("VDescr", thisAircraft.Description)
            xtw.WriteElementString("RespEngineer", thisAircraft.Engineer)
            xtw.WriteElementString("RespTechnician", thisAircraft.Mechanic)
            xtw.WriteElementString("Tail", thisAircraft.RegistrationNbr)
            xtw.WriteElementString("Color", thisAircraft.LiveryColor)
            xtw.WriteElementString("AircraftStatus", thisAircraft.AircraftStatus)
            xtw.WriteElementString("Manufacturer", thisAircraft.Manufacturer)
            xtw.WriteElementString("CustWorkOrder", String.Empty)
            xtw.WriteElementString("TotalRqrdUsage", thisAircraft.TotalRequiredUsage)
            xtw.WriteElementString("CurrentUsage", thisAircraft.CurrentFlightUsage)
            xtw.WriteElementString("StartEngCalibration", String.Empty)
            xtw.WriteElementString("DimODO", thisAircraft.MeterUnit)
            xtw.WriteElementString("StartODO", Format(CDec(thisAircraft.AirframeHoursStart), "#,##0.0"))
            xtw.WriteElementString("EndODO", Format(CDec(thisAircraft.AirframeHoursCurrent), "#,##0.0"))
            xtw.WriteElementString("CurrentMeterOffset", Format(CDec(thisAircraft.AirframeHoursOffset), "#,##0.00"))

            Dim flightLocationDictionary As New Dictionary(Of Integer, String)
            For Each thisFlight As FlightInfo In thisAircraft.FlightInfoList
                If Not flightLocationDictionary.ContainsKey(thisFlight.LocationId) Then
                    flightLocationDictionary.Add(thisFlight.LocationId, thisFlight.LocationName)
                End If
            Next

            xtw.WriteStartElement("TestProcedures")

            Dim testList As fltLookupBaseList = AircraftTestXrefManager.GetTestListForAircraft(thisAircraft.AircraftId, thisAircraft.ProgId)
            For Each thisTest As fltLookupBase In testList
                ProcessOneAircraftTestProcedure(xtw, thisTest)
            Next

            xtw.WriteEndElement()

            xtw.WriteStartElement("Configurations")

            For Each thisAircraftConfiguration As AircraftConfiguration In thisAircraft.AircraftConfigurationList
                ProcessOneAircraftConfiguration(xtw, thisAircraftConfiguration)
            Next

            xtw.WriteEndElement()

            xtw.WriteElementString("EstEndOfTesting", String.Empty)
            xtw.WriteElementString("ActEndOfTesting", String.Empty)

            xtw.WriteStartElement("Locations")

            For Each thisAircraftLocation As AircraftLocation In thisAircraft.AircraftLocationList
                ProcessOneAircraftLocation(xtw, thisAircraft, thisAircraftLocation, ChangesSince, FltIdentity)
                If flightLocationDictionary.ContainsKey(thisAircraftLocation.LocationId) Then
                    flightLocationDictionary.Remove(thisAircraftLocation.LocationId)
                End If
            Next
            If flightLocationDictionary.Count <> 0 Then
                For Each thisLocationId As Integer In flightLocationDictionary.Keys
                    Dim thisAircraftLocation As New AircraftLocation
                    With thisAircraftLocation
                        .AircraftId = thisAircraft.AircraftId
                        .LocationId = thisLocationId
                        .ArrivalDate = DateTime.UtcNow
                        .StampUserName = "export"
                    End With
                    Dim aircraftLocationId As Integer = AircraftLocationDB.Insert(thisAircraftLocation)
                    thisAircraftLocation = AircraftLocationDB.GetItem(aircraftLocationId)
                    ProcessOneAircraftLocation(xtw, thisAircraft, thisAircraftLocation, ChangesSince, FltIdentity)
                Next
            End If

            xtw.WriteEndElement()

            xtw.WriteEndElement()

        End Sub

        Private Shared Sub ProcessOneAircraftTestProcedure(ByVal xtw As XmlTextWriter, ByVal thisTest As fltLookupBase)

            xtw.WriteElementString("TestName", thisTest.StringValue)

        End Sub

        Private Shared Sub ProcessOneAircraftConfiguration(ByVal xtw As XmlTextWriter, ByVal thisAircraftConfiguration As AircraftConfiguration)

            xtw.WriteStartElement("Configuration")
            xtw.WriteAttributeString("ConfigurationGUID", thisAircraftConfiguration.AircraftConfigurationGUID.ToString)
            xtw.WriteAttributeString("UTCDateCreated", Format(thisAircraftConfiguration.UtcDateCreated, UtcDateFormat))
            xtw.WriteAttributeString("UTCDateModified", Format(thisAircraftConfiguration.UtcDateModified, UtcDateFormat))
            xtw.WriteAttributeString("IsActive", "True")
            xtw.WriteAttributeString("IsDeleted", thisAircraftConfiguration.IsDeleted.ToString)

            If IsDate(thisAircraftConfiguration.ConfigurationStartDate) Then
                xtw.WriteElementString("DateBegin", Format(CDate(thisAircraftConfiguration.ConfigurationStartDate), "yyyy-MM-dd"))
            Else
                xtw.WriteElementString("DateBegin", String.Empty)
            End If
            If IsDate(thisAircraftConfiguration.ConfigurationEndDate) Then
                xtw.WriteElementString("DateEnd", Format(CDate(thisAircraftConfiguration.ConfigurationEndDate), "yyyy-MM-dd"))
            Else
                xtw.WriteElementString("DateEnd", String.Empty)
            End If
            xtw.WriteElementString("MeterBegin", Format(CDec(thisAircraftConfiguration.ConfigurationMeterStart), "#,##0.0"))
            xtw.WriteElementString("MeterEnd", Format(CDec(thisAircraftConfiguration.ConfigurationMeterEnd), "#,##0.0"))
            xtw.WriteElementString("MeterOffset", Format(CDec(thisAircraftConfiguration.ConfigurationMeterOffset), "#,##0.0"))
            xtw.WriteElementString("MeterUnit", thisAircraftConfiguration.MeterUnit)
            xtw.WriteElementString("Model", thisAircraftConfiguration.ModelDescr)
            xtw.WriteElementString("EngineType", thisAircraftConfiguration.EngineType)
            xtw.WriteElementString("EngineEmission", thisAircraftConfiguration.EngineEmission)
            xtw.WriteElementString("EngineMaturity", thisAircraftConfiguration.EngineMaturity)
            xtw.WriteElementString("EngineSerNo", thisAircraftConfiguration.EngineSerialNbr)
            xtw.WriteElementString("APUType", thisAircraftConfiguration.APUType)
            xtw.WriteElementString("APUEmission", thisAircraftConfiguration.APUEmission)
            xtw.WriteElementString("APUMaturity", thisAircraftConfiguration.APUMaturity)
            xtw.WriteElementString("APUSerNo", thisAircraftConfiguration.APUSerialNbr)
            xtw.WriteElementString("NoseGear", thisAircraftConfiguration.NoseGear)
            xtw.WriteElementString("MainGear", thisAircraftConfiguration.MainGear)
            xtw.WriteElementString("AvionicsVersion", thisAircraftConfiguration.AvionicsVersion)
            xtw.WriteElementString("Propeller", thisAircraftConfiguration.PropellerType)
            xtw.WriteElementString("PropellerType", thisAircraftConfiguration.PropellerModelNbr)
            xtw.WriteElementString("PropellerSerNo", thisAircraftConfiguration.PropellerSerialNo)
            xtw.WriteElementString("Description", thisAircraftConfiguration.ConfigurationDescription)

            xtw.WriteEndElement()

        End Sub

        Private Shared Sub ProcessOneAircraftLocation(ByVal xtw As XmlTextWriter, ByVal thisAircraft As Aircraft, ByVal thisAircraftLocation As AircraftLocation, ByVal ChangesSince As Date, ByVal FltIdentity As FltIdentity)

            xtw.WriteStartElement("Location")
            xtw.WriteAttributeString("AircraftLocationGUID", thisAircraftLocation.AircraftLocationGUID.ToString)
            xtw.WriteAttributeString("UTCDateCreated", Format(thisAircraftLocation.UtcDateCreated, UtcDateFormat))
            xtw.WriteAttributeString("UTCDateModified", Format(thisAircraftLocation.UtcDateModified, UtcDateFormat))
            xtw.WriteAttributeString("IsActive", thisAircraftLocation.IsActive.ToString)
            xtw.WriteAttributeString("IsDeleted", thisAircraftLocation.IsDeleted.ToString)

            xtw.WriteElementString("LocationName", thisAircraftLocation.LocationName)
            Dim thisLocation As Location = LocationManager.GetItem(thisAircraftLocation.LocationId, FltIdentity)
            xtw.WriteElementString("TimeZone", "UTC" & Format(thisLocation.TimeZoneOffset, "00"))

            If CDec(thisAircraftLocation.RequiredUsage) > 0 Then
                xtw.WriteElementString("RqrdUsage", Format(CDec(thisAircraftLocation.RequiredUsage), "#,##0.0"))
            Else
                xtw.WriteElementString("RqrdUsage", String.Empty)
            End If
            If CDec(thisAircraftLocation.AccumUsage) > 0 Then
                xtw.WriteElementString("AccumUsage", Format(CDec(thisAircraftLocation.AccumUsage), "#,##0.0"))
            Else
                xtw.WriteElementString("AccumUsage", String.Empty)
            End If
            If IsDate(thisAircraftLocation.EstEndOfTest) Then
                xtw.WriteElementString("EstEndOfTestSegment", Format(CDate(thisAircraftLocation.EstEndOfTest), "yyyy-MM-dd"))
            Else
                xtw.WriteElementString("EstEndOfTestSegment", String.Empty)
            End If
            If IsDate(thisAircraftLocation.ActEndOfTest) Then
                xtw.WriteElementString("ActEndOfTestSegment", Format(CDate(thisAircraftLocation.ActEndOfTest), "yyyy-MM-dd"))
            Else
                xtw.WriteElementString("ActEndOfTestSegment", String.Empty)
            End If
            xtw.WriteElementString("LastTestLocation", thisAircraftLocation.LastLocation.ToString)

            xtw.WriteStartElement("Usages")

            For Each thisAircraftFlightInfo As FlightInfo In thisAircraft.FlightInfoList
                If thisAircraftFlightInfo.LocationId = thisAircraftLocation.LocationId Then
                    If Not (Date.Compare(thisAircraftFlightInfo.UtcDateModified, ChangesSince) < 0) Then
                        ProcessOneFlight(xtw, thisAircraft, thisAircraftFlightInfo, FltIdentity)
                    End If
                End If
            Next

            xtw.WriteEndElement()

            xtw.WriteStartElement("WorkOrders")

            For Each thisWorkOrderInfo As WorkOrderInfo In thisAircraft.WorkOrderInfoList
                ProcessOneWorkOrder(xtw, thisWorkOrderInfo)
            Next

            xtw.WriteEndElement()

            xtw.WriteEndElement()

        End Sub

        Private Shared Sub ProcessOneFlight(ByVal xtw As XmlTextWriter, ByVal thisaircraft As Aircraft, ByVal thisAircraftFlightInfo As FlightInfo, ByVal FltIdentity As FltIdentity)

            Dim thisFlight As Flight = FlightManager.GetItem(thisAircraftFlightInfo.FlightId, FltIdentity)

            xtw.WriteStartElement("Flight")
            xtw.WriteAttributeString("FlightGUID", thisFlight.FlightGuid.ToString)
            xtw.WriteAttributeString("UTCDateCreated", Format(thisFlight.UtcDateCreated, UtcDateFormat))
            xtw.WriteAttributeString("UTCDateModified", Format(thisFlight.UtcDateModified, UtcDateFormat))
            xtw.WriteAttributeString("IsActive", thisFlight.IsActive.ToString)
            xtw.WriteAttributeString("IsDeleted", thisFlight.IsDeleted.ToString)

            xtw.WriteElementString("Pilot", thisFlight.PilotName)
            xtw.WriteElementString("ScheduledRoute", thisFlight.SchedRouteName)
            xtw.WriteElementString("FlownRoute", thisFlight.FlownRouteName)
            xtw.WriteElementString("ShiftIndicator", thisFlight.WorkShiftName)

            xtw.WriteElementString("TakeoffWeight", Format(thisFlight.TakeoffWeight, "#,##0"))

            xtw.WriteElementString("AirframeId", thisFlight.AirframeWeightDescr)
            xtw.WriteElementString("AirframeWeight", Format(thisFlight.AirframeWeight, "#,##0"))
            xtw.WriteElementString("AirframeWeightUnit", thisFlight.AirframeWeightUnit)

            xtw.WriteElementString("Cargo1ID", thisFlight.Cargo1WeightDescr)
            xtw.WriteElementString("Cargo1Weight", Format(thisFlight.Cargo1Weight, "#,##0"))
            xtw.WriteElementString("Cargo1WeightUnit", thisFlight.Cargo1WeightUnit)

            xtw.WriteElementString("Cargo2ID", thisFlight.Cargo2WeightDescr)
            xtw.WriteElementString("Cargo2Weight", Format(thisFlight.Cargo2Weight, "#,##0"))
            xtw.WriteElementString("Cargo2WeightUnit", thisFlight.Cargo2WeightUnit)

            xtw.WriteElementString("BallastId", thisFlight.BallastWeightDescr)
            xtw.WriteElementString("BallastWeight", Format(thisFlight.BallastWeight, "#,##0"))
            xtw.WriteElementString("BallastWeightUnit", thisFlight.BallastWeightUnit)

            xtw.WriteElementString("OilLevelIndicatorStart", CStr(thisFlight.OilLevelIndicatorStart) & "/4")
            xtw.WriteElementString("HydraulicLevelIndicatorStart", CStr(thisFlight.HydraulicLevelIndicatorStart) & "/4")
            xtw.WriteElementString("PreFlightFtiOK", CheckYesNo(thisFlight.PreFlightFtiOK))
            xtw.WriteElementString("PreFlightTelemetryOK", CheckYesNo(thisFlight.PreFlightTelemetryOK))
            xtw.WriteElementString("PreFlightTelemetryCheckComplete", CheckYesNo(thisFlight.PostFlightTelemetryCheckComplete))

            xtw.WriteElementString("DepartureDate", FormatLocalDateTime(thisFlight.DepartureDateTime_Local, thisFlight.DepartureDateTime_UTC))
            xtw.WriteElementString("ReturnDate", FormatLocalDateTime(thisFlight.ReturnDateTime_Local, thisFlight.ReturnDateTime_UTC))
            xtw.WriteElementString("UsedMeterOffset", Format(CDec(thisFlight.UsedMeterOffset), "#,##0.0"))
            xtw.WriteElementString("UsageStartODO", Format(CDec(thisFlight.AirframeHoursStart), "#,##0.0"))
            xtw.WriteElementString("UsageEndODO", Format(CDec(thisFlight.AirframeHoursEnd), "#,##0.0"))
            xtw.WriteElementString("EngineHoursStart", Format(CDec(thisFlight.EngineHoursStart), "#,##0.0"))
            xtw.WriteElementString("EngineHoursEnd", Format(CDec(thisFlight.EngineHoursEnd), "#,##0.0"))

            xtw.WriteElementString("PostFlightFtiOK", CheckYesNo(thisFlight.PostFlightFtiOK))
            xtw.WriteElementString("PostFlightTelemetryCheckComplete", CheckYesNo(thisFlight.PostFlightTelemetryCheckComplete))
            xtw.WriteStartElement("PostTestNacelleCheck")
            xtw.WriteElementString("CarbonSootBuildup", CheckYesNo(thisFlight.NacelleCheckBuildUp))
            xtw.WriteElementString("LeaksOrDiscoloration", CheckYesNo(thisFlight.NacelleCheckDiscoloration))
            xtw.WriteEndElement()

            xtw.WriteStartElement("LiquidFills")

            For Each thisLiquidFill As LiquidFill In thisFlight.LiquidFillList
                thisLiquidFill.HobbsReading = String.Format(CDec(thisLiquidFill.HobbsReading) + CDec(thisFlight.UsedMeterOffset), "#,##9.9")
                ProcessOneLiquidFill(xtw, thisLiquidFill)
            Next

            xtw.WriteEndElement()

            xtw.WriteStartElement("UsageComments")

            For Each thisFlightComment As FlightComment In thisFlight.FlightCommentList
                ProcessOneFlightComment(xtw, thisFlightComment)
            Next

            xtw.WriteEndElement()

            xtw.WriteStartElement("RegionTimes")

            For Each thisRegionTime As FlightRegionTime In thisFlight.FlightRegionTimeList
                ProcessOneRegionTime(xtw, thisRegionTime)
            Next

            xtw.WriteEndElement()

            xtw.WriteStartElement("EngineGroundRuns")

            For Each thisEngineRun As FLT.BO.FlightEngineRun In thisFlight.FlightEngineRunList
                ProcessOneEngineRun(xtw, thisEngineRun)
            Next
            xtw.WriteEndElement()

            xtw.WriteEndElement()

        End Sub

        Private Shared Sub ProcessOneLiquidFill(ByVal xtw As XmlTextWriter, ByVal thisLiquidFill As LiquidFill)

            xtw.WriteStartElement("LiquidFill")
            xtw.WriteAttributeString("LiquidFillGUID", thisLiquidFill.LiquidFillGUID.ToString)
            xtw.WriteAttributeString("UTCDateCreated", Format(thisLiquidFill.UtcDateCreated, UtcDateFormat))
            xtw.WriteAttributeString("UTCDateModified", Format(thisLiquidFill.UtcDateModified, UtcDateFormat))
            xtw.WriteAttributeString("IsActive", thisLiquidFill.IsActive.ToString)
            xtw.WriteAttributeString("IsDeleted", thisLiquidFill.IsDeleted.ToString)

            xtw.WriteElementString("LiquidGroup", thisLiquidFill.LiquidGroupName)
            xtw.WriteElementString("LiquidAmnt", Format(CDec(thisLiquidFill.LiquidAmt), "#,##0.000"))
            xtw.WriteElementString("LiquidDim", thisLiquidFill.LiquidUnitName)
            xtw.WriteElementString("LiquidType", thisLiquidFill.LiquidTypeName)
            xtw.WriteElementString("LiquidFillMeter", thisLiquidFill.HobbsReading)
            xtw.WriteElementString("LiquidFillLocation", thisLiquidFill.LiquidFillLocation)
            xtw.WriteElementString("LiquidFillApprover", thisLiquidFill.LiquidFillApprover)

            xtw.WriteEndElement()

        End Sub

        Private Shared Sub ProcessOneFlightComment(ByVal xtw As XmlTextWriter, ByVal thisFlightComment As FlightComment)

            xtw.WriteStartElement("Comment")
            xtw.WriteAttributeString("CommentGUID", thisFlightComment.FlightCommentGUID.ToString)
            xtw.WriteAttributeString("UTCDateCreated", Format(thisFlightComment.UtcDateCreated, UtcDateFormat))
            xtw.WriteAttributeString("UTCDateModified", Format(thisFlightComment.UtcDateModified, UtcDateFormat))
            xtw.WriteAttributeString("IsActive", thisFlightComment.IsActive.ToString)
            xtw.WriteAttributeString("IsDeleted", thisFlightComment.IsDeleted.ToString)

            xtw.WriteElementString("Event_Time", FormatLocalDateTime(thisFlightComment.CommentDateTime_Local, thisFlightComment.CommentDateTime_UTC))
            xtw.WriteElementString("Component", thisFlightComment.ComponentName)
            xtw.WriteElementString("PilotComment", thisFlightComment.PilotComment)
            xtw.WriteElementString("Rating", thisFlightComment.CommentRating)

            xtw.WriteEndElement()

        End Sub

        Private Shared Sub ProcessOneRegionTime(ByVal xtw As XmlTextWriter, ByVal thisRegionTime As FlightRegionTime)

            xtw.WriteStartElement("RegionTime")
            xtw.WriteAttributeString("RegionTimeGUID", thisRegionTime.FlightRegionTimeGUID.ToString)
            xtw.WriteAttributeString("UTCDateCreated", Format(thisRegionTime.UtcDateCreated, UtcDateFormat))
            xtw.WriteAttributeString("UTCDateModified", Format(thisRegionTime.UtcDateModified, UtcDateFormat))
            xtw.WriteAttributeString("IsActive", thisRegionTime.IsActive.ToString)
            xtw.WriteAttributeString("IsDeleted", thisRegionTime.IsDeleted.ToString)

            xtw.WriteElementString("Region", thisRegionTime.RegionCode)
            xtw.WriteElementString("RegionHours", Format(CDec(thisRegionTime.Hours), "#,##0.0"))

            xtw.WriteEndElement()

        End Sub

        Private Shared Sub ProcessOneEngineRun(ByVal xtw As XmlTextWriter, ByVal thisEngineRun As FlightEngineRun)

            xtw.WriteStartElement("EngineRun")
            xtw.WriteAttributeString("EngineRunGUID", thisEngineRun.FlightEngineRunGUID.ToString)
            xtw.WriteAttributeString("UTCDateCreated", Format(thisEngineRun.UtcDateCreated, UtcDateFormat))
            xtw.WriteAttributeString("UTCDateModified", Format(thisEngineRun.UtcDateModified, UtcDateFormat))
            xtw.WriteAttributeString("IsActive", "True")
            xtw.WriteAttributeString("IsDeleted", thisEngineRun.IsDeleted.ToString)

            xtw.WriteElementString("RunStartTime", FormatLocalDateTime(thisEngineRun.RunStartDateTime_Local, thisEngineRun.RunStartDateTime_UTC))
            xtw.WriteElementString("RunEndTime", FormatLocalDateTime(thisEngineRun.RunEndDateTime_Local, thisEngineRun.RunEndDateTime_UTC))
            xtw.WriteElementString("EngineSpeed", thisEngineRun.EngineSpeed.ToString)

            xtw.WriteEndElement()

        End Sub

        Private Shared Sub ProcessOneWorkOrder(ByVal xtw As XmlTextWriter, ByVal thisWorkOrderInfo As WorkOrderInfo)

        End Sub

        Private Shared Function FormatLocalDateTime(ByVal LocalDateTime As DateTime, ByVal UtcDateTime As DateTime) As String

            Dim timeSpan As TimeSpan = LocalDateTime.Subtract(UtcDateTime)

            Dim hourDiff As Double = timeSpan.Hours
            Dim minuteDiff As Double = timeSpan.Minutes
            Dim timeDiffSign As String = String.Empty

            If hourDiff > 0 Then
                timeDiffSign = "+"
            Else
                timeDiffSign = "-"
            End If

            Dim localTimeOffset As String = String.Empty

            localTimeOffset = timeDiffSign & Format(Math.Abs(hourDiff), "00") & ":" & Format(Math.Abs(minuteDiff), "00")

            Return Format(LocalDateTime, "yyyy-MM-ddTHH:mm") & localTimeOffset

        End Function

        Private Shared Function CheckYesNo(ByVal CheckValue As Short) As String
            Dim retValue As String = String.Empty
            Select Case CheckValue
                Case 1
                    retValue = "Yes"
                Case -1
                    retValue = "No"
                Case Else
                    retValue = "Not Answered"
            End Select
            Return retValue
        End Function

#End Region

    End Class

End Namespace
