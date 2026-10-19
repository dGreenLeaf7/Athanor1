' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Contoso.Utilities
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.BL

Namespace FLT.Dal

	Public Class FlightDB
		
        Public Shared Function GetItem(ByVal FlightId As Integer) As Flight

            Dim myFlight As Flight = Nothing
            Dim totRows As Integer = 0

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()
                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()
                        Try
                            myFlight = GetItem(FlightId, myDbConnection, myDbTransaction)
                            If myFlight Is Nothing Then
                                myDbTransaction.Rollback()
                                Return Nothing
                            End If
                            myFlight.LiquidFillList = LiquidFillDB.GetList(FlightId, 0, 0, 0, 1, 0, -1, -1, totRows, myDbConnection, myDbTransaction)
                            myFlight.FlightCommentList = FlightCommentDB.GetList(FlightId, 0, 1, 0, -1, -1, totRows, myDbConnection, myDbTransaction)
                            myFlight.FlightEngineRunList = FlightEngineRunDB.GetList(FlightId, 0, -1, -1, totRows, myDbConnection, myDbTransaction)
                            myFlight.FlightRegionTimeList = FlightRegionTimeDB.GetList(FlightId, 0, 1, 0, -1, -1, totRows, myDbConnection, myDbTransaction)
                            myDbTransaction.Commit()

                        Catch ex As ApplicationException
                            myDbTransaction.Rollback()
                            Throw New ApplicationException("Application encountered a data error.")

                        Catch ex As Exception
                            myDbTransaction.Rollback()
                            Logger.Write(ex.Message)
                            Throw New ApplicationException("Application encountered a data error.")

                        End Try

                    End Using

                Catch ex As ApplicationException
                    Throw New ApplicationException("Application encountered a data error.")

                Catch ex As Exception
                    Logger.Write(ex.Message)
                    Throw New ApplicationException("Application encountered a data error.")

                Finally
                    If myDbConnection.Region = ConnectionState.Open Then
                        myDbConnection.Close()
                    End If

                End Try

            End Using

            Return myFlight

        End Function

        Public Shared Function GetItem(ByVal FlightId As Integer, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "fetchFlight"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.AddWithValue("@FlightId", FlightId)
            Try
                Dim myReader As DbDataReader = Nothing

                Try
                    myReader = myDbCommand.ExecuteReader(CommandBehavior.SingleRow)

                    If (Not myReader.HasRows) Then Return Nothing

                    myReader.Read()

                    Return FillDataRecord(myReader)

                Catch myException As Exception
                    Logger.Write(myException, myDbCommand.CommandText)
                    Throw New ApplicationException("Application encountered a data error.")
                Finally
                    CType(myReader, IDisposable).Dispose()
                End Try

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try
        End Function

        Public Shared Sub Update(ByVal myFlight As Flight)

            Using myDbConn As SqlConnection = New SqlConnection()

                myDbConn.ConnectionString = AppConfiguration.connectionString

                Try

                    myDbConn.Open()

                    Using myTrans As SqlTransaction = myDbConn.BeginTransaction()

                        Try
                            _update(myFlight, myDbConn, myTrans)

                            _processLiquidFillList(myFlight, myDbConn, myTrans)
                            _processLiquidFillDeleteList(myFlight, myDbConn, myTrans)

                            _processFlightCommentList(myFlight, myDbConn, myTrans)
                            _processFlightCommentDeleteList(myFlight, myDbConn, myTrans)

                            _processFlightEngineRunList(myFlight, myDbConn, myTrans)
                            _processFlightEngineRunDeleteList(myFlight, myDbConn, myTrans)

                            _processFlightRegionTimeList(myFlight, myDbConn, myTrans)
                            _processFlightRegionTimeDeleteList(myFlight, myDbConn, myTrans)

                            myTrans.Commit()

                        Catch ex As ApplicationException
                            myTrans.Rollback()
                            Throw New ApplicationException(ex.Message)

                        Catch myException As Exception
                            myTrans.Rollback()
                            Logger.Write(myException.Message)
                            Throw New ApplicationException("Application encountered a data error.")

                        End Try

                    End Using

                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)

                Catch ex As Exception
                    Logger.Write(ex.Message)
                    Throw New ApplicationException("Application encountered a data error.")

                Finally
                    If myDbConn.Region = ConnectionState.Open Then
                        myDbConn.Close()
                    End If

                End Try

            End Using

        End Sub

        Private Shared Sub _update(ByVal myFlight As Flight, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "updateFlight"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.Add(New SqlParameter("@FlightId", SqlDbType.Int))
            myDbCommand.Parameters("@FlightId").Value = myFlight.FlightId
            myDbCommand.Parameters.Add(New SqlParameter("@AircraftId", SqlDbType.Int))
            myDbCommand.Parameters("@AircraftId").Value = myFlight.AircraftId
            myDbCommand.Parameters.Add(New SqlParameter("@LocationId", SqlDbType.Int))
            myDbCommand.Parameters("@LocationId").Value = myFlight.LocationId
            myDbCommand.Parameters.Add(New SqlParameter("@SchedRouteId", SqlDbType.Int))
            myDbCommand.Parameters("@SchedRouteId").Value = myFlight.SchedRouteId
            myDbCommand.Parameters.Add(New SqlParameter("@FlownRouteId", SqlDbType.Int))
            myDbCommand.Parameters("@FlownRouteId").Value = myFlight.FlownRouteId
            myDbCommand.Parameters.Add(New SqlParameter("@FlightDate", SqlDbType.VarChar, 10))
            myDbCommand.Parameters("@FlightDate").Value = myFlight.FlightDate
            myDbCommand.Parameters.Add(New SqlParameter("@FlightDateSort", SqlDbType.VarChar, 10))
            myDbCommand.Parameters("@FlightDateSort").Value = myFlight.FlightDateSort
            myDbCommand.Parameters.Add(New SqlParameter("@WorkShiftIndicatorId", SqlDbType.Int))
            myDbCommand.Parameters("@WorkShiftIndicatorId").Value = myFlight.WorkShiftIndicatorId

            myDbCommand.Parameters.Add(New SqlParameter("@AirframeWeightId", SqlDbType.Int))
            myDbCommand.Parameters("@AirframeWeightId").Value = myFlight.AirframeWeightId
            myDbCommand.Parameters.Add(New SqlParameter("@Cargo1WeightId", SqlDbType.Int))
            myDbCommand.Parameters("@Cargo1WeightId").Value = myFlight.Cargo1WeightId
            myDbCommand.Parameters.Add(New SqlParameter("@Cargo2WeightId", SqlDbType.Int))
            myDbCommand.Parameters("@Cargo2WeightId").Value = myFlight.Cargo2WeightId
            myDbCommand.Parameters.Add(New SqlParameter("@BallastWeightId", SqlDbType.Int))
            myDbCommand.Parameters("@BallastWeightId").Value = myFlight.BallastWeightId

            myDbCommand.Parameters.Add(New SqlParameter("@PilotId", SqlDbType.Int))
            myDbCommand.Parameters("@PilotId").Value = myFlight.PilotId
            myDbCommand.Parameters.Add(New SqlParameter("@PreFlightFtiOK", SqlDbType.SmallInt))
            myDbCommand.Parameters("@PreFlightFtiOK").Value = myFlight.PreFlightFtiOK
            myDbCommand.Parameters.Add(New SqlParameter("@PreFlightTelemetryOK", SqlDbType.SmallInt))
            myDbCommand.Parameters("@PreFlightTelemetryOK").Value = myFlight.PreFlightTelemetryOK
            myDbCommand.Parameters.Add(New SqlParameter("@PreFlightTelemetryCheckComplete", SqlDbType.SmallInt))
            myDbCommand.Parameters("@PreFlightTelemetryCheckComplete").Value = myFlight.PreFlightTelemetryCheckComplete
            myDbCommand.Parameters.Add(New SqlParameter("@OilLevelIndicatorStart", SqlDbType.SmallInt))
            myDbCommand.Parameters("@OilLevelIndicatorStart").Value = myFlight.OilLevelIndicatorStart
            myDbCommand.Parameters.Add(New SqlParameter("@HydraulicLevelIndicatorStart", SqlDbType.SmallInt))
            myDbCommand.Parameters("@HydraulicLevelIndicatorStart").Value = myFlight.HydraulicLevelIndicatorStart
            myDbCommand.Parameters.Add(New SqlParameter("@UsedMeterOffset", SqlDbType.Decimal))
            myDbCommand.Parameters("@UsedMeterOffset").Value = CDec(myFlight.UsedMeterOffset)
            myDbCommand.Parameters.Add(New SqlParameter("@MeterUnitId", SqlDbType.Int))
            myDbCommand.Parameters("@MeterUnitId").Value = myFlight.MeterUnitId
            myDbCommand.Parameters.Add(New SqlParameter("@DepartureTime", SqlDbType.VarChar, 8))
            myDbCommand.Parameters("@DepartureTime").Value = myFlight.DepartureTime
            myDbCommand.Parameters.Add(New SqlParameter("@DepartureDateTime_Local", SqlDbType.DateTime))
            myDbCommand.Parameters("@DepartureDateTime_Local").Value = myFlight.DepartureDateTime_Local
            myDbCommand.Parameters.Add(New SqlParameter("@DepartureDateTime_UTC", SqlDbType.DateTime))
            myDbCommand.Parameters("@DepartureDateTime_UTC").Value = myFlight.DepartureDateTime_UTC
            myDbCommand.Parameters.Add(New SqlParameter("@AircraftEngineHoursCurrent", SqlDbType.Decimal))
            myDbCommand.Parameters("@AircraftEngineHoursCurrent").Value = CDec(myFlight.AircraftEngineHoursCurrent)
            myDbCommand.Parameters.Add(New SqlParameter("@EngineHoursStart", SqlDbType.Decimal))
            myDbCommand.Parameters("@EngineHoursStart").Value = CDec(myFlight.EngineHoursStart)
            myDbCommand.Parameters.Add(New SqlParameter("@HobbsStart", SqlDbType.Decimal))
            myDbCommand.Parameters("@HobbsStart").Value = CDec(myFlight.HobbsStart)
            myDbCommand.Parameters.Add(New SqlParameter("@AirframeHoursStart", SqlDbType.Decimal))
            myDbCommand.Parameters("@AirframeHoursStart").Value = CDec(myFlight.AirframeHoursStart)
            myDbCommand.Parameters.Add(New SqlParameter("@ReturnTime", SqlDbType.VarChar, 8))
            myDbCommand.Parameters("@ReturnTime").Value = myFlight.ReturnTime
            myDbCommand.Parameters.Add(New SqlParameter("@ReturnDateTime_Local", SqlDbType.DateTime))
            myDbCommand.Parameters("@ReturnDateTime_Local").Value = myFlight.ReturnDateTime_Local
            myDbCommand.Parameters.Add(New SqlParameter("@ReturnDateTime_UTC", SqlDbType.DateTime))
            myDbCommand.Parameters("@ReturnDateTime_UTC").Value = myFlight.ReturnDateTime_UTC
            myDbCommand.Parameters.Add(New SqlParameter("@HobbsEnd", SqlDbType.Decimal))
            myDbCommand.Parameters("@HobbsEnd").Value = CDec(myFlight.HobbsEnd)
            myDbCommand.Parameters.Add(New SqlParameter("@AirframeHoursEnd", SqlDbType.Decimal))
            myDbCommand.Parameters("@AirframeHoursEnd").Value = CDec(myFlight.AirframeHoursEnd)
            myDbCommand.Parameters.Add(New SqlParameter("@EngineHoursEnd", SqlDbType.Decimal))
            myDbCommand.Parameters("@EngineHoursEnd").Value = CDec(myFlight.EngineHoursEnd)
            myDbCommand.Parameters.Add(New SqlParameter("@PostFlightFtiOK", SqlDbType.SmallInt))
            myDbCommand.Parameters("@PostFlightFtiOK").Value = myFlight.PostFlightFtiOK
            myDbCommand.Parameters.Add(New SqlParameter("@PostFlightTelemetryCheckComplete", SqlDbType.SmallInt))
            myDbCommand.Parameters("@PostFlightTelemetryCheckComplete").Value = myFlight.PostFlightTelemetryCheckComplete
            myDbCommand.Parameters.Add(New SqlParameter("@NacelleCheckBuildUp", SqlDbType.SmallInt))
            myDbCommand.Parameters("@NacelleCheckBuildUp").Value = myFlight.NacelleCheckBuildUp
            myDbCommand.Parameters.Add(New SqlParameter("@NacelleCheckDiscoloration", SqlDbType.SmallInt))
            myDbCommand.Parameters("@NacelleCheckDiscoloration").Value = myFlight.NacelleCheckDiscoloration

            myDbCommand.Parameters.Add(New SqlParameter("@IsApproved", SqlDbType.Bit))
            myDbCommand.Parameters("@IsApproved").Value = myFlight.IsApproved
            myDbCommand.Parameters.Add(New SqlParameter("@ApprovedBy", SqlDbType.NVarChar, 20))
            myDbCommand.Parameters("@ApprovedBy").Value = myFlight.ApprovedBy
            myDbCommand.Parameters.Add(New SqlParameter("@UTCDateApproved", SqlDbType.DateTime))
            myDbCommand.Parameters("@UTCDateApproved").Value = myFlight.UtcDateApproved

            myDbCommand.Parameters.Add(New SqlParameter("@FlightGuid", SqlDbType.UniqueIdentifier))
            myDbCommand.Parameters("@FlightGUID").Value = myFlight.FlightGuid
            myDbCommand.Parameters.Add(New SqlParameter("@IsActive", SqlDbType.Bit))
            myDbCommand.Parameters("@IsActive").Value = myFlight.IsActive
            myDbCommand.Parameters.Add(New SqlParameter("@StampUserName", SqlDbType.NVarChar, 20))
            myDbCommand.Parameters("@StampUserName").Value = myFlight.StampUserName
            myDbCommand.Parameters.Add(New SqlParameter("@ConcurrencyId", SqlDbType.Timestamp, 8))
            myDbCommand.Parameters("@ConcurrencyId").Value = DalUtility.ConvertStringToRowVersion(myFlight.ConcurrencyId)
            myDbCommand.Parameters.Add(New SqlParameter("@NewConcurrencyId", SqlDbType.Timestamp, 8))
            myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.Add(New SqlParameter("@NewUTCDate", SqlDbType.DateTime, 8))
            myDbCommand.Parameters("@NewUTCDate").Direction = ParameterDirection.Output
            Try
                myDbCommand.ExecuteNonQuery()

            Catch mySqlEx As SqlException
                If mySqlEx.Message = "Row has been edited by another user" Then
                    Throw New ApplicationException(mySqlEx.Message)
                Else
                    Logger.Write(mySqlEx, myDbCommand.CommandText)
                    Throw New ApplicationException("Application encountered a data error.")
                End If
            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try

        End Sub

        Public Shared Function Insert(ByVal myFlight As Flight) As Integer

            Dim newId As Integer = 0

            Using myDbConn As SqlConnection = New SqlConnection()

                Try

                    myDbConn.ConnectionString = AppConfiguration.connectionString
                    myDbConn.Open()

                    Using myTrans As SqlTransaction = myDbConn.BeginTransaction()

                        Try
                            newId = _insert(myFlight, myDbConn, myTrans)
                            myFlight.FlightId = newId

                            _processLiquidFillList(myFlight, myDbConn, myTrans)

                            _processFlightCommentList(myFlight, myDbConn, myTrans)

                            _processFlightEngineRunList(myFlight, myDbConn, myTrans)

                            _processFlightRegionTimeList(myFlight, myDbConn, myTrans)

                            myTrans.Commit()

                        Catch ex As ApplicationException
                            myTrans.Rollback()
                            Throw New ApplicationException(ex.Message)

                        Catch myException As Exception
                            myTrans.Rollback()
                            Logger.Write(myException.Message)
                            Throw New ApplicationException("Application encountered a data error.")

                        End Try

                    End Using

                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)

                Catch ex As Exception
                    Logger.Write(ex.Message)
                    Throw New ApplicationException("Application encountered a data error.")

                Finally
                    If myDbConn.Region = ConnectionState.Open Then
                        myDbConn.Close()
                    End If

                End Try

            End Using

            Return newId

        End Function

        Private Shared Function _insert(ByVal myFlight As Flight, ByVal myDbConn As SqlConnection, ByVal myTrans As SqlTransaction) As Integer

            Dim newID As Integer = 0

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "addFlight"
            myDbCommand.Connection = myDbConn
            myDbCommand.Transaction = myTrans

            myDbCommand.Parameters.Add(New SqlParameter("@AircraftId", SqlDbType.Int))
            myDbCommand.Parameters("@AircraftId").Value = myFlight.AircraftId
            myDbCommand.Parameters.Add(New SqlParameter("@LocationId", SqlDbType.Int))
            myDbCommand.Parameters("@LocationId").Value = myFlight.LocationId
            myDbCommand.Parameters.Add(New SqlParameter("@SchedRouteId", SqlDbType.Int))
            myDbCommand.Parameters("@SchedRouteId").Value = myFlight.SchedRouteId
            myDbCommand.Parameters.Add(New SqlParameter("@FlownRouteId", SqlDbType.Int))
            myDbCommand.Parameters("@FlownRouteId").Value = myFlight.FlownRouteId
            myDbCommand.Parameters.Add(New SqlParameter("@FlightDate", SqlDbType.VarChar, 10))
            myDbCommand.Parameters("@FlightDate").Value = myFlight.FlightDate
            myDbCommand.Parameters.Add(New SqlParameter("@FlightDateSort", SqlDbType.VarChar, 10))
            myDbCommand.Parameters("@FlightDateSort").Value = myFlight.FlightDateSort
            myDbCommand.Parameters.Add(New SqlParameter("@WorkShiftIndicatorId", SqlDbType.Int))
            myDbCommand.Parameters("@WorkShiftIndicatorId").Value = myFlight.WorkShiftIndicatorId
            myDbCommand.Parameters.Add(New SqlParameter("@AirframeWeightId", SqlDbType.Int))
            myDbCommand.Parameters("@AirframeWeightId").Value = myFlight.AirframeWeightId
            myDbCommand.Parameters.Add(New SqlParameter("@Cargo1WeightId", SqlDbType.Int))
            myDbCommand.Parameters("@Cargo1WeightId").Value = myFlight.Cargo1WeightId
            myDbCommand.Parameters.Add(New SqlParameter("@Cargo2WeightId", SqlDbType.Int))
            myDbCommand.Parameters("@Cargo2WeightId").Value = myFlight.Cargo2WeightId
            myDbCommand.Parameters.Add(New SqlParameter("@BallastWeightId", SqlDbType.Int))
            myDbCommand.Parameters("@BallastWeightId").Value = myFlight.BallastWeightId
            myDbCommand.Parameters.Add(New SqlParameter("@PilotId", SqlDbType.Int))
            myDbCommand.Parameters("@PilotId").Value = myFlight.PilotId
            myDbCommand.Parameters.Add(New SqlParameter("@PreFlightFtiOK", SqlDbType.SmallInt))
            myDbCommand.Parameters("@PreFlightFtiOK").Value = myFlight.PreFlightFtiOK
            myDbCommand.Parameters.Add(New SqlParameter("@PreFlightTelemetryOK", SqlDbType.SmallInt))
            myDbCommand.Parameters("@PreFlightTelemetryOK").Value = myFlight.PreFlightTelemetryOK
            myDbCommand.Parameters.Add(New SqlParameter("@PreFlightTelemetryCheckComplete", SqlDbType.SmallInt))
            myDbCommand.Parameters("@PreFlightTelemetryCheckComplete").Value = myFlight.PreFlightTelemetryCheckComplete
            myDbCommand.Parameters.Add(New SqlParameter("@OilLevelIndicatorStart", SqlDbType.SmallInt))
            myDbCommand.Parameters("@OilLevelIndicatorStart").Value = myFlight.OilLevelIndicatorStart
            myDbCommand.Parameters.Add(New SqlParameter("@HydraulicLevelIndicatorStart", SqlDbType.SmallInt))
            myDbCommand.Parameters("@HydraulicLevelIndicatorStart").Value = myFlight.HydraulicLevelIndicatorStart
            myDbCommand.Parameters.Add(New SqlParameter("@UsedMeterOffset", SqlDbType.Decimal))
            myDbCommand.Parameters("@UsedMeterOffset").Value = CDec(myFlight.UsedMeterOffset)
            myDbCommand.Parameters.Add(New SqlParameter("@MeterUnitId", SqlDbType.Int))
            myDbCommand.Parameters("@MeterUnitId").Value = myFlight.MeterUnitId
            myDbCommand.Parameters.Add(New SqlParameter("@DepartureTime", SqlDbType.VarChar, 8))
            myDbCommand.Parameters("@DepartureTime").Value = myFlight.DepartureTime
            myDbCommand.Parameters.Add(New SqlParameter("@DepartureDateTime_Local", SqlDbType.DateTime))
            myDbCommand.Parameters("@DepartureDateTime_Local").Value = myFlight.DepartureDateTime_Local
            myDbCommand.Parameters.Add(New SqlParameter("@DepartureDateTime_UTC", SqlDbType.DateTime))
            myDbCommand.Parameters("@DepartureDateTime_UTC").Value = myFlight.DepartureDateTime_UTC
            myDbCommand.Parameters.Add(New SqlParameter("@AircraftEngineHoursCurrent", SqlDbType.Decimal))
            myDbCommand.Parameters("@AircraftEngineHoursCurrent").Value = CDec(myFlight.AircraftEngineHoursCurrent)
            myDbCommand.Parameters.Add(New SqlParameter("@EngineHoursStart", SqlDbType.Decimal))
            myDbCommand.Parameters("@EngineHoursStart").Value = CDec(myFlight.EngineHoursStart)
            myDbCommand.Parameters.Add(New SqlParameter("@HobbsStart", SqlDbType.Decimal))
            myDbCommand.Parameters("@HobbsStart").Value = CDec(myFlight.HobbsStart)
            myDbCommand.Parameters.Add(New SqlParameter("@AirframeHoursStart", SqlDbType.Decimal))
            myDbCommand.Parameters("@AirframeHoursStart").Value = CDec(myFlight.AirframeHoursStart)
            myDbCommand.Parameters.Add(New SqlParameter("@ReturnTime", SqlDbType.VarChar, 8))
            myDbCommand.Parameters("@ReturnTime").Value = myFlight.ReturnTime
            myDbCommand.Parameters.Add(New SqlParameter("@ReturnDateTime_Local", SqlDbType.DateTime))
            myDbCommand.Parameters("@ReturnDateTime_Local").Value = myFlight.ReturnDateTime_Local
            myDbCommand.Parameters.Add(New SqlParameter("@ReturnDateTime_UTC", SqlDbType.DateTime))
            myDbCommand.Parameters("@ReturnDateTime_UTC").Value = myFlight.ReturnDateTime_UTC
            myDbCommand.Parameters.Add(New SqlParameter("@HobbsEnd", SqlDbType.Decimal))
            myDbCommand.Parameters("@HobbsEnd").Value = CDec(myFlight.HobbsEnd)
            myDbCommand.Parameters.Add(New SqlParameter("@AirframeHoursEnd", SqlDbType.Decimal))
            myDbCommand.Parameters("@AirframeHoursEnd").Value = CDec(myFlight.AirframeHoursEnd)
            myDbCommand.Parameters.Add(New SqlParameter("@EngineHoursEnd", SqlDbType.Decimal))
            myDbCommand.Parameters("@EngineHoursEnd").Value = CDec(myFlight.EngineHoursEnd)
            myDbCommand.Parameters.Add(New SqlParameter("@PostFlightFtiOK", SqlDbType.SmallInt))
            myDbCommand.Parameters("@PostFlightFtiOK").Value = myFlight.PostFlightFtiOK
            myDbCommand.Parameters.Add(New SqlParameter("@PostFlightTelemetryCheckComplete", SqlDbType.SmallInt))
            myDbCommand.Parameters("@PostFlightTelemetryCheckComplete").Value = myFlight.PostFlightTelemetryCheckComplete
            myDbCommand.Parameters.Add(New SqlParameter("@NacelleCheckBuildUp", SqlDbType.SmallInt))
            myDbCommand.Parameters("@NacelleCheckBuildUp").Value = myFlight.NacelleCheckBuildUp
            myDbCommand.Parameters.Add(New SqlParameter("@NacelleCheckDiscoloration", SqlDbType.SmallInt))
            myDbCommand.Parameters("@NacelleCheckDiscoloration").Value = myFlight.NacelleCheckDiscoloration

            myDbCommand.Parameters.Add(New SqlParameter("@IsApproved", SqlDbType.Bit))
            myDbCommand.Parameters("@IsApproved").Value = myFlight.IsApproved
            myDbCommand.Parameters.Add(New SqlParameter("@ApprovedBy", SqlDbType.NVarChar, 20))
            myDbCommand.Parameters("@ApprovedBy").Value = myFlight.ApprovedBy
            myDbCommand.Parameters.Add(New SqlParameter("@UTCDateApproved", SqlDbType.DateTime))
            myDbCommand.Parameters("@UTCDateApproved").Value = myFlight.UtcDateApproved

            myDbCommand.Parameters.Add(New SqlParameter("@FlightId", SqlDbType.Int))
            myDbCommand.Parameters("@FlightId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.Add(New SqlParameter("@NewConcurrencyId", SqlDbType.Timestamp))
            myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.Add(New SqlParameter("@StampUserName", SqlDbType.NVarChar, 20))
            myDbCommand.Parameters("@StampUserName").Value = myFlight.StampUserName
            myDbCommand.Parameters.Add(New SqlParameter("@NewUTCDate", SqlDbType.DateTime, 8))
            myDbCommand.Parameters("@NewUTCDate").Direction = ParameterDirection.Output

            Try
                myDbCommand.ExecuteNonQuery()

                newID = CInt(myDbCommand.Parameters("@FlightId").Value)

            Catch myException As Exception
                If myException.Message = "Duplicate Flight Entry" Then
                    Throw New ApplicationException("Can't insert - this flight already exists in the database.")
                End If
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try

            Return newID

        End Function

        Public Shared Sub Delete(ByVal FlightId As Integer, ByVal StampUserName As String)

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "deleteFlight"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@FlightId", FlightId)
            myDbCommand.Parameters.AddWithValue("@StampUserName", StampUserName)
            Try
                myDbConnection.Open()
                myDbCommand.ExecuteNonQuery()
                myDbConnection.Close()
            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            Finally
                CType(myDbConnection, IDisposable).Dispose()
            End Try
        End Sub

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As Flight

            Dim flight As Flight = New Flight()
            flight.FlightId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlightId"))
            flight.AircraftId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("AircraftId"))
            flight.LocationId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("LocationId"))
            flight.SchedRouteId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("SchedRouteId"))
            flight.SchedRouteName = myDataRecord.GetString(myDataRecord.GetOrdinal("SchedRouteName"))
            flight.FlownRouteId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlownRouteId"))
            flight.FlownRouteName = myDataRecord.GetString(myDataRecord.GetOrdinal("FlownRouteName"))
            flight.FlightDate = myDataRecord.GetString(myDataRecord.GetOrdinal("FlightDate"))
            flight.FlightDateSort = myDataRecord.GetString(myDataRecord.GetOrdinal("FlightDateSort"))
            flight.WorkShiftIndicatorId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("WorkShiftIndicatorId"))
            flight.WorkShiftName = myDataRecord.GetString(myDataRecord.GetOrdinal("WorkShiftName"))

            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("AirframeWeightId")) Then
                flight.AirframeWeightId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("AirframeWeightId"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("AirframeWeightDescr")) Then
                flight.AirframeWeightDescr = myDataRecord.GetString(myDataRecord.GetOrdinal("AirframeWeightDescr"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("AirframeWeight")) Then
                flight.AirframeWeight = myDataRecord.GetInt32(myDataRecord.GetOrdinal("AirframeWeight"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("AirframeWeightUnit")) Then
                flight.AirframeWeightUnit = myDataRecord.GetString(myDataRecord.GetOrdinal("AirframeWeightUnit"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("AirframeWeightGuid")) Then
                flight.AirframeWeightGUID = myDataRecord.GetGuid(myDataRecord.GetOrdinal("AirframeWeightGuid"))
            End If

            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Cargo1WeightId")) Then
                flight.Cargo1WeightId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("Cargo1WeightId"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Cargo1WeightDescr")) Then
                flight.Cargo1WeightDescr = myDataRecord.GetString(myDataRecord.GetOrdinal("Cargo1WeightDescr"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Cargo1Weight")) Then
                flight.Cargo1Weight = myDataRecord.GetInt32(myDataRecord.GetOrdinal("Cargo1Weight"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Cargo1WeightUnit")) Then
                flight.Cargo1WeightUnit = myDataRecord.GetString(myDataRecord.GetOrdinal("Cargo1WeightUnit"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Cargo1WeightGuid")) Then
                flight.Cargo1WeightGUID = myDataRecord.GetGuid(myDataRecord.GetOrdinal("Cargo1WeightGuid"))
            End If

            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Cargo2WeightId")) Then
                flight.Cargo2WeightId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("Cargo2WeightId"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Cargo2WeightDescr")) Then
                flight.Cargo2WeightDescr = myDataRecord.GetString(myDataRecord.GetOrdinal("Cargo2WeightDescr"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Cargo2Weight")) Then
                flight.Cargo2Weight = myDataRecord.GetInt32(myDataRecord.GetOrdinal("Cargo2Weight"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Cargo2WeightUnit")) Then
                flight.Cargo2WeightUnit = myDataRecord.GetString(myDataRecord.GetOrdinal("Cargo2WeightUnit"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Cargo2WeightGuid")) Then
                flight.Cargo2WeightGUID = myDataRecord.GetGuid(myDataRecord.GetOrdinal("Cargo2WeightGuid"))
            End If

            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("BallastWeightId")) Then
                flight.BallastWeightId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("BallastWeightId"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("BallastWeightDescr")) Then
                flight.BallastWeightDescr = myDataRecord.GetString(myDataRecord.GetOrdinal("BallastWeightDescr"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("BallastWeight")) Then
                flight.BallastWeight = myDataRecord.GetInt32(myDataRecord.GetOrdinal("BallastWeight"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("BallastWeightUnit")) Then
                flight.BallastWeightUnit = myDataRecord.GetString(myDataRecord.GetOrdinal("BallastWeightUnit"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("BallastWeightGuid")) Then
                flight.BallastWeightGUID = myDataRecord.GetGuid(myDataRecord.GetOrdinal("BallastWeightGuid"))
            End If

            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("TakeoffWeight")) Then
                flight.TakeoffWeight = myDataRecord.GetInt32(myDataRecord.GetOrdinal("TakeoffWeight"))
            End If
            If flight.TakeoffWeight = 0 Then
                flight.TakeoffWeight = flight.AirframeWeight + flight.Cargo1Weight + flight.Cargo2Weight + flight.BallastWeight
            End If

            flight.PilotId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("PilotId"))
            flight.PilotName = myDataRecord.GetString(myDataRecord.GetOrdinal("PilotName"))
            flight.PreFlightFtiOK = myDataRecord.GetInt16(myDataRecord.GetOrdinal("PreFlightFtiOK"))
            flight.PreFlightTelemetryOK = myDataRecord.GetInt16(myDataRecord.GetOrdinal("PreFlightTelemetryOK"))
            flight.PreFlightTelemetryCheckComplete = myDataRecord.GetInt16(myDataRecord.GetOrdinal("PreFlightTelemetryCheckComplete"))
            flight.OilLevelIndicatorStart = myDataRecord.GetInt16(myDataRecord.GetOrdinal("OilLevelIndicatorStart"))
            flight.HydraulicLevelIndicatorStart = myDataRecord.GetInt16(myDataRecord.GetOrdinal("HydraulicLevelIndicatorStart"))
            flight.UsedMeterOffset = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("UsedMeterOffset")), "#,##0.0")
            flight.MeterUnitId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("MeterUnitId"))
            flight.DepartureTime = myDataRecord.GetString(myDataRecord.GetOrdinal("DepartureTime"))
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("DepartureDateTime_UTC")) Then
                flight.DepartureDateTime_UTC = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("DepartureDateTime_UTC"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("DepartureDateTime_Local")) Then
                flight.DepartureDateTime_Local = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("DepartureDateTime_Local"))
            End If
            flight.AircraftEngineHoursCurrent = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("AircraftEngineHoursCurrent")), "#,##0.0")
            flight.EngineHoursStart = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("EngineHoursStart")), "#,##0.0")
            flight.HobbsStart = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("HobbsStart")), "#,##0.0")
            flight.AirframeHoursStart = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("AirframeHoursStart")), "#,##0.0")
            flight.ReturnTime = myDataRecord.GetString(myDataRecord.GetOrdinal("ReturnTime"))
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("ReturnDateTime_UTC")) Then
                flight.ReturnDateTime_UTC = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("ReturnDateTime_UTC"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("ReturnDateTime_Local")) Then
                flight.ReturnDateTime_Local = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("ReturnDateTime_Local"))
            End If
            flight.HobbsEnd = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("HobbsEnd")), "#,##0.0")
            flight.AirframeHoursEnd = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("AirframeHoursEnd")), "#,##0.0")
            flight.EngineHoursEnd = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("EngineHoursEnd")), "#,##0.0")
            flight.PostFlightFtiOK = myDataRecord.GetInt16(myDataRecord.GetOrdinal("PostFlightFtiOK"))
            flight.PostFlightTelemetryCheckComplete = myDataRecord.GetInt16(myDataRecord.GetOrdinal("PostFlightTelemetryCheckComplete"))
            flight.NacelleCheckBuildUp = myDataRecord.GetInt16(myDataRecord.GetOrdinal("NacelleCheckBuildUp"))
            flight.NacelleCheckDiscoloration = myDataRecord.GetInt16(myDataRecord.GetOrdinal("NacelleCheckDiscoloration"))

            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("IsApproved")) Then
                flight.IsApproved = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsApproved"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("ApprovedBy")) Then
                flight.ApprovedBy = myDataRecord.GetString(myDataRecord.GetOrdinal("ApprovedBy"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("UTCDateApproved")) Then
                flight.UtcDateApproved = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateApproved"))
            End If

            flight.FlightGuid = myDataRecord.GetGuid(myDataRecord.GetOrdinal("FlightGuid"))
            flight.IsActive = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsActive"))
            flight.IsDeleted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsDeleted"))
            flight.UtcDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateCreated"))
            flight.UtcDateModified = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateModified"))
            flight.StampUserName = myDataRecord.GetString(myDataRecord.GetOrdinal("StampUserName"))
            flight.ConcurrencyId = DalUtility.ConvertRowVersionToString(myDataRecord("ConcurrencyId"))
            Return flight

        End Function

#Region "Private Child Object List Data Access Procedures"

        Private Shared Sub _processLiquidFillList(ByVal Flight As Flight, ByVal Conn As SqlConnection, ByVal Trans As SqlTransaction)
            Try
                For Each thisLiquidFill As LiquidFill In Flight.LiquidFillList
                    thisLiquidFill.FlightId = Flight.FlightId
                    thisLiquidFill.StampUserName = Flight.StampUserName
                    If ((thisLiquidFill.ConcurrencyId = Nothing) Or (thisLiquidFill.ConcurrencyId = String.Empty) Or (thisLiquidFill.ConcurrencyId = "0")) Then
                        LiquidFillDB.Insert(thisLiquidFill, Conn, Trans)
                    Else
                        LiquidFillDB.Update(thisLiquidFill, Conn, Trans)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub _processLiquidFillDeleteList(ByVal Flight As Flight, ByVal Conn As SqlConnection, ByVal Trans As SqlTransaction)
            Try
                For Each thisLiquidFill As LiquidFill In Flight.LiquidFillDeleteList
                    If thisLiquidFill.LiquidFillId > 0 Then
                        LiquidFillDB.Delete(thisLiquidFill.LiquidFillId, Flight.StampUserName, Conn, Trans)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub _processFlightCommentList(ByVal Flight As Flight, ByVal Conn As SqlConnection, ByVal Trans As SqlTransaction)
            Try
                For Each thisFlightComment As FlightComment In Flight.FlightCommentList
                    thisFlightComment.FlightId = Flight.FlightId
                    thisFlightComment.StampUserName = Flight.StampUserName
                    If ((thisFlightComment.ConcurrencyId = Nothing) Or (thisFlightComment.ConcurrencyId = String.Empty) Or (thisFlightComment.ConcurrencyId = "0")) Then
                        FlightCommentDB.Insert(thisFlightComment, Conn, Trans)
                    Else
                        FlightCommentDB.Update(thisFlightComment, Conn, Trans)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub _processFlightCommentDeleteList(ByVal Flight As Flight, ByVal Conn As SqlConnection, ByVal Trans As SqlTransaction)
            Try
                For Each thisFlightComment As FlightComment In Flight.FlightCommentDeleteList
                    If thisFlightComment.FlightCommentId > 0 Then
                        FlightCommentDB.Delete(thisFlightComment.FlightCommentId, Flight.StampUserName, Conn, Trans)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub _processFlightEngineRunList(ByVal Flight As Flight, ByVal Conn As SqlConnection, ByVal Trans As SqlTransaction)
            Try
                For Each thisFlightEngineRun As FlightEngineRun In Flight.FlightEngineRunList
                    thisFlightEngineRun.FlightId = Flight.FlightId
                    thisFlightEngineRun.StampUserName = Flight.StampUserName
                    If ((thisFlightEngineRun.ConcurrencyId = Nothing) Or (thisFlightEngineRun.ConcurrencyId = String.Empty) Or (thisFlightEngineRun.ConcurrencyId = "0")) Then
                        FlightEngineRunDB.Insert(thisFlightEngineRun, Conn, Trans)
                    Else
                        FlightEngineRunDB.Update(thisFlightEngineRun, Conn, Trans)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub _processFlightEngineRunDeleteList(ByVal Flight As Flight, ByVal Conn As SqlConnection, ByVal Trans As SqlTransaction)
            Try
                For Each thisFlightEngineRun As FlightEngineRun In Flight.FlightEngineRunDeleteList
                    If thisFlightEngineRun.FlightEngineRunId > 0 Then
                        FlightEngineRunDB.Delete(thisFlightEngineRun.FlightEngineRunId, Flight.StampUserName, Conn, Trans)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub _processFlightRegionTimeList(ByVal Flight As Flight, ByVal Conn As SqlConnection, ByVal Trans As SqlTransaction)
            Try
                For Each thisFlightRegionTime As FlightRegionTime In Flight.FlightRegionTimeList
                    thisFlightRegionTime.FlightId = Flight.FlightId
                    thisFlightRegionTime.StampUserName = Flight.StampUserName
                    If ((thisFlightRegionTime.ConcurrencyId = Nothing) Or (thisFlightRegionTime.ConcurrencyId = String.Empty) Or (thisFlightRegionTime.ConcurrencyId = "0")) Then
                        FlightRegionTimeDB.Insert(thisFlightRegionTime, Conn, Trans)
                    Else
                        FlightRegionTimeDB.Update(thisFlightRegionTime, Conn, Trans)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub _processFlightRegionTimeDeleteList(ByVal Flight As Flight, ByVal Conn As SqlConnection, ByVal Trans As SqlTransaction)
            Try
                For Each thisFlightRegionTime As FlightRegionTime In Flight.FlightRegionTimeDeleteList
                    If thisFlightRegionTime.FlightRegionTimeId > 0 Then
                        FlightRegionTimeDB.Delete(thisFlightRegionTime.FlightRegionTimeId, Flight.StampUserName, Conn, Trans)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

#End Region

    End Class

End Namespace
