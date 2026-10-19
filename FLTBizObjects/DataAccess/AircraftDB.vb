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

	Public Class AircraftDB
		
        Public Shared Function GetItem(ByVal AircraftId As Integer) As Aircraft

            Dim newAircraft As Aircraft = Nothing

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction

                        Dim totRows As Integer = 0

                        Try
                            newAircraft = GetItem(AircraftId, myDbConnection, myDbTransaction)
                            If newAircraft Is Nothing Then
                                myDbTransaction.Rollback()
                                Return Nothing
                            End If
                            newAircraft.AircraftConfigurationList = AircraftConfigurationDB.GetList(AircraftId, 0, -1, -1, totRows, myDbConnection, myDbTransaction)
                            newAircraft.AircraftLocationList = AircraftLocationDB.GetList(AircraftId, 0, 1, 0, -1, -1, totRows, myDbConnection, myDbTransaction)
                            newAircraft.GenericCommentList = GenericCommentDB.GetList(AircraftId, myDbConnection, myDbTransaction)
                            newAircraft.TestList = AircraftTestXrefDB.ListTestsForAircraft(AircraftId, newAircraft.ProgId, myDbConnection, myDbTransaction)
                            newAircraft.FlightInfoList = FlightInfoDB.GetList(AircraftId, String.Empty, 0, 0, 0, 0, String.Empty, String.Empty, 1, 1, 0, -1, -1, totRows, myDbConnection, myDbTransaction)
                            newAircraft.WorkOrderInfoList = WorkOrderInfoDB.GetList(newAircraft.ProgId, AircraftId, String.Empty, 0, 0, String.Empty, 0, -1, -1, 0, myDbConnection, myDbTransaction)
                            myDbTransaction.Commit()

                        Catch ex As ApplicationException
                            Throw New ApplicationException("Application encountered a data error.")

                        Catch ex As Exception
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

            Return newAircraft

        End Function

        Public Shared Function GetItem(ByVal AircraftId As Integer, ByVal myDbConnection As SqlConnection, ByVal mydbtransaction As SqlTransaction) As Aircraft

            Dim newAircraft As Aircraft = Nothing

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "fetchAircraft"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = mydbtransaction

            myDbCommand.Parameters.AddWithValue("@AircraftId", AircraftId)
            Dim myReader As DbDataReader = Nothing
            Dim totRows As Integer = 0

            Try
                myReader = myDbCommand.ExecuteReader(CommandBehavior.SingleRow)

                If (myReader.HasRows) Then

                    myReader.Read()
                    newAircraft = FillDataRecord(myReader)

                End If

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            Finally
                CType(myReader, IDisposable).Dispose()
            End Try

            Return newAircraft

        End Function

        Public Shared Sub Update(ByVal myAircraft As Aircraft)

            Using myDbConn As SqlConnection = New SqlConnection()

                myDbConn.ConnectionString = AppConfiguration.connectionString

                Try

                    myDbConn.Open()

                    Using myTrans As SqlTransaction = myDbConn.BeginTransaction()

                        Try
                            _update(myAircraft, myDbConn, myTrans)

                            _processAircraftConfigurationList(myAircraft, myDbConn, myTrans)
                            _processAircraftConfigurationDeleteList(myAircraft, myDbConn, myTrans)

                            _processAircraftLocationList(myAircraft, myDbConn, myTrans)
                            _processAircraftLocationDeleteList(myAircraft, myDbConn, myTrans)

                            _processGenericCommentList(myAircraft, myDbConn, myTrans)
                            _processGenericCommentDeleteList(myAircraft, myDbConn, myTrans)

                            _processTestList(myAircraft, myDbConn, myTrans)
                            _processTestDeleteList(myAircraft, myDbConn, myTrans)

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

        Private Shared Sub _update(ByVal myAircraft As Aircraft, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "updateAircraft"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.Add(New SqlParameter("@AircraftId", SqlDbType.Int))
            myDbCommand.Parameters("@AircraftId").Value = myAircraft.AircraftId
            myDbCommand.Parameters.Add(New SqlParameter("@ProgId", SqlDbType.Int))
            myDbCommand.Parameters("@ProgId").Value = myAircraft.ProgId
            myDbCommand.Parameters.Add(New SqlParameter("@CurrentLocationID", SqlDbType.Int))
            myDbCommand.Parameters("@CurrentLocationId").Value = myAircraft.CurrentLocationId
            myDbCommand.Parameters.Add(New SqlParameter("@MSN", SqlDbType.VarChar, 20))
            myDbCommand.Parameters("@MSN").Value = myAircraft.MSN
            myDbCommand.Parameters.Add(New SqlParameter("@ShipNbr", SqlDbType.VarChar, 15))
            myDbCommand.Parameters("@ShipNbr").Value = myAircraft.ShipNbr
            myDbCommand.Parameters.Add(New SqlParameter("@SerialNbr", SqlDbType.VarChar, 6))
            myDbCommand.Parameters("@SerialNbr").Value = myAircraft.SerialNbr
            myDbCommand.Parameters.Add(New SqlParameter("@ModelId", SqlDbType.Int))
            myDbCommand.Parameters("@ModelId").Value = myAircraft.ModelId
            myDbCommand.Parameters.Add(New SqlParameter("@Description", SqlDbType.NVarChar, 255))
            myDbCommand.Parameters("@Description").Value = myAircraft.Description
            myDbCommand.Parameters.Add(New SqlParameter("@EngineerId", SqlDbType.Int))
            myDbCommand.Parameters("@EngineerId").Value = myAircraft.EngineerId
            myDbCommand.Parameters.Add(New SqlParameter("@MechanicId", SqlDbType.Int))
            myDbCommand.Parameters("@MechanicId").Value = myAircraft.MechanicId
            myDbCommand.Parameters.Add(New SqlParameter("@RegistrationNbr", SqlDbType.VarChar, 10))
            myDbCommand.Parameters("@RegistrationNbr").Value = myAircraft.RegistrationNbr
            myDbCommand.Parameters.Add(New SqlParameter("@LiveryColor", SqlDbType.VarChar, 10))
            myDbCommand.Parameters("@LiveryColor").Value = myAircraft.LiveryColor
            myDbCommand.Parameters.Add(New SqlParameter("@AircraftStatusId", SqlDbType.Int))
            myDbCommand.Parameters("@AircraftStatusId").Value = myAircraft.AircraftStatusId
            myDbCommand.Parameters.Add(New SqlParameter("@ManufacturerId", SqlDbType.Int))
            myDbCommand.Parameters("@ManufacturerId").Value = myAircraft.ManufacturerId
            myDbCommand.Parameters.Add(New SqlParameter("@MeterUnitId", SqlDbType.Int))
            myDbCommand.Parameters("@MeterUnitId").Value = myAircraft.MeterUnitId
            myDbCommand.Parameters.Add(New SqlParameter("@AirframeHoursStart", SqlDbType.Decimal))
            myDbCommand.Parameters("@AirframeHoursStart").Value = CDec(myAircraft.AirframeHoursStart)
            myDbCommand.Parameters.Add(New SqlParameter("@AirframeHoursCurrent", SqlDbType.Decimal))
            myDbCommand.Parameters("@AirframeHoursCurrent").Value = CDec(myAircraft.AirframeHoursCurrent)
            myDbCommand.Parameters.Add(New SqlParameter("@AirframeHoursOffset", SqlDbType.Decimal))
            myDbCommand.Parameters("@AirframeHoursOffset").Value = CDec(myAircraft.AirframeHoursOffset)
            myDbCommand.Parameters.Add(New SqlParameter("@TotalRequiredUsage", SqlDbType.Decimal))
            myDbCommand.Parameters("@TotalRequiredUsage").Value = CDec(myAircraft.TotalRequiredUsage)
            myDbCommand.Parameters.Add(New SqlParameter("@AircraftEngineHoursCurrent", SqlDbType.Decimal))
            myDbCommand.Parameters("@AircraftEngineHoursCurrent").Value = CDec(myAircraft.AircraftEngineHoursCurrent)
            myDbCommand.Parameters.Add(New SqlParameter("@EstEndOfTesting", SqlDbType.DateTime))
            If IsDate(myAircraft.EstEndOfTesting) Then
                myDbCommand.Parameters("@EstEndOfTesting").Value = CDate(myAircraft.EstEndOfTesting)
            Else
                myDbCommand.Parameters("@EstEndOfTesting").Value = DBNull.Value
            End If
            myDbCommand.Parameters.Add(New SqlParameter("@ActEndOfTesting", SqlDbType.DateTime))
            If IsDate(myAircraft.ActEndOfTesting) Then
                myDbCommand.Parameters("@ActEndOfTesting").Value = CDate(myAircraft.ActEndOfTesting)
            Else
                myDbCommand.Parameters("@ActEndOfTesting").Value = DBNull.Value
            End If
            myDbCommand.Parameters.Add(New SqlParameter("@AircraftGuid", SqlDbType.UniqueIdentifier))
            myDbCommand.Parameters("@AircraftGuid").Value = myAircraft.AircraftGuid
            myDbCommand.Parameters.Add(New SqlParameter("@IsActive", SqlDbType.Bit))
            myDbCommand.Parameters("@IsActive").Value = myAircraft.IsActive
            myDbCommand.Parameters.Add(New SqlParameter("@StampUserName", SqlDbType.NVarChar, 20))
            myDbCommand.Parameters("@StampUserName").Value = myAircraft.StampUserName
            myDbCommand.Parameters.Add(New SqlParameter("@ConcurrencyId", SqlDbType.Timestamp, 8))
            myDbCommand.Parameters("@ConcurrencyId").Value = DalUtility.ConvertStringToRowVersion(myAircraft.ConcurrencyId)
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

        Public Shared Function Insert(ByVal myAircraft As Aircraft) As Integer

            Dim newId As Integer = 0

            Using myDbConn As SqlConnection = New SqlConnection()

                myDbConn.ConnectionString = AppConfiguration.connectionString

                Try

                    myDbConn.Open()

                    Using myTrans As SqlTransaction = myDbConn.BeginTransaction()

                        Try
                            newId = _insert(myAircraft, myDbConn, myTrans)
                            myAircraft.AircraftId = newId

                            _processAircraftConfigurationList(myAircraft, myDbConn, myTrans)

                            _processAircraftLocationList(myAircraft, myDbConn, myTrans)

                            _processGenericCommentList(myAircraft, myDbConn, myTrans)

                            _processTestList(myAircraft, myDbConn, myTrans)

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

        Public Shared Function _insert(ByVal myAircraft As Aircraft, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction) As Integer

            Dim newId As Integer = 0

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "addAircraft"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.Add(New SqlParameter("@ProgId", SqlDbType.Int))
            myDbCommand.Parameters("@ProgId").Value = myAircraft.ProgId
            myDbCommand.Parameters.Add(New SqlParameter("@CurrentLocationID", SqlDbType.Int))
            myDbCommand.Parameters("@CurrentLocationId").Value = myAircraft.CurrentLocationId
            myDbCommand.Parameters.Add(New SqlParameter("@MSN", SqlDbType.VarChar, 20))
            myDbCommand.Parameters("@MSN").Value = myAircraft.MSN
            myDbCommand.Parameters.Add(New SqlParameter("@ShipNbr", SqlDbType.VarChar, 15))
            myDbCommand.Parameters("@ShipNbr").Value = myAircraft.ShipNbr
            myDbCommand.Parameters.Add(New SqlParameter("@SerialNbr", SqlDbType.VarChar, 6))
            myDbCommand.Parameters("@SerialNbr").Value = myAircraft.SerialNbr
            myDbCommand.Parameters.Add(New SqlParameter("@ModelId", SqlDbType.Int))
            myDbCommand.Parameters("@ModelId").Value = myAircraft.ModelId
            myDbCommand.Parameters.Add(New SqlParameter("@Description", SqlDbType.NVarChar, 255))
            myDbCommand.Parameters("@Description").Value = myAircraft.Description
            myDbCommand.Parameters.Add(New SqlParameter("@EngineerId", SqlDbType.Int))
            myDbCommand.Parameters("@EngineerId").Value = myAircraft.EngineerId
            myDbCommand.Parameters.Add(New SqlParameter("@MechanicId", SqlDbType.Int))
            myDbCommand.Parameters("@MechanicId").Value = myAircraft.MechanicId
            myDbCommand.Parameters.Add(New SqlParameter("@RegistrationNbr", SqlDbType.VarChar, 10))
            myDbCommand.Parameters("@RegistrationNbr").Value = myAircraft.RegistrationNbr
            myDbCommand.Parameters.Add(New SqlParameter("@LiveryColor", SqlDbType.VarChar, 10))
            myDbCommand.Parameters("@LiveryColor").Value = myAircraft.LiveryColor
            myDbCommand.Parameters.Add(New SqlParameter("@AircraftStatusId", SqlDbType.Int))
            myDbCommand.Parameters("@AircraftStatusId").Value = myAircraft.AircraftStatusId
            myDbCommand.Parameters.Add(New SqlParameter("@ManufacturerId", SqlDbType.Int))
            myDbCommand.Parameters("@ManufacturerId").Value = myAircraft.ManufacturerId
            myDbCommand.Parameters.Add(New SqlParameter("@MeterUnitId", SqlDbType.Int))
            myDbCommand.Parameters("@MeterUnitId").Value = myAircraft.MeterUnitId
            myDbCommand.Parameters.Add(New SqlParameter("@AirframeHoursStart", SqlDbType.Decimal))
            myDbCommand.Parameters("@AirframeHoursStart").Value = CDec(myAircraft.AirframeHoursStart)
            myDbCommand.Parameters.Add(New SqlParameter("@AirframeHoursCurrent", SqlDbType.Decimal))
            myDbCommand.Parameters("@AirframeHoursCurrent").Value = CDec(myAircraft.AirframeHoursCurrent)
            myDbCommand.Parameters.Add(New SqlParameter("@AirframeHoursOffset", SqlDbType.Decimal))
            myDbCommand.Parameters("@AirframeHoursOffset").Value = CDec(myAircraft.AirframeHoursOffset)
            myDbCommand.Parameters.Add(New SqlParameter("@TotalRequiredUsage", SqlDbType.Decimal))
            myDbCommand.Parameters("@TotalRequiredUsage").Value = CDec(myAircraft.TotalRequiredUsage)
            myDbCommand.Parameters.Add(New SqlParameter("@AircraftEngineHoursCurrent", SqlDbType.Decimal))
            myDbCommand.Parameters("@AircraftEngineHoursCurrent").Value = CDec(myAircraft.AircraftEngineHoursCurrent)
            myDbCommand.Parameters.Add(New SqlParameter("@EstEndOfTesting", SqlDbType.DateTime))
            If IsDate(myAircraft.EstEndOfTesting) Then
                myDbCommand.Parameters("@EstEndOfTesting").Value = CDate(myAircraft.EstEndOfTesting)
            Else
                myDbCommand.Parameters("@EstEndOfTesting").Value = DBNull.Value
            End If
            myDbCommand.Parameters.Add(New SqlParameter("@ActEndOfTesting", SqlDbType.DateTime))
            If IsDate(myAircraft.ActEndOfTesting) Then
                myDbCommand.Parameters("@ActEndOfTesting").Value = CDate(myAircraft.ActEndOfTesting)
            Else
                myDbCommand.Parameters("@ActEndOfTesting").Value = DBNull.Value
            End If
            myDbCommand.Parameters.AddWithValue("@StampUserName", myAircraft.StampUserName)
            myDbCommand.Parameters.AddWithValue("@AircraftId", 0)
            myDbCommand.Parameters("@AircraftId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.AddWithValue("@NewConcurrencyId", 0)
            myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.Add(New SqlParameter("@NewUTCDate", SqlDbType.DateTime, 8))
            myDbCommand.Parameters("@NewUTCDate").Direction = ParameterDirection.Output

            Try
                myDbCommand.ExecuteNonQuery()

                newId = myDbCommand.Parameters("@AircraftId").Value

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try

            Return newId

        End Function

        Public Shared Sub Delete(ByVal AircraftId As Integer, ByVal StampUserName As String)

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "deleteAircraft"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@AircraftId", AircraftId)
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

        Friend Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As Aircraft

            Dim aircraft As Aircraft = New Aircraft()
            aircraft.AircraftId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("AircraftId"))
            aircraft.ProgId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ProgId"))
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("CurrentLocationId")) Then
                aircraft.CurrentLocationId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("CurrentLocationId"))
            End If
            aircraft.CurrentLocation = myDataRecord.GetString(myDataRecord.GetOrdinal("LocationName"))
            aircraft.MSN = myDataRecord.GetString(myDataRecord.GetOrdinal("MSN"))
            aircraft.ShipNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("ShipNbr"))
            aircraft.SerialNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("SerialNbr"))
            aircraft.ModelId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ModelId"))
            aircraft.Model = myDataRecord.GetString(myDataRecord.GetOrdinal("Model"))
            aircraft.Description = myDataRecord.GetString(myDataRecord.GetOrdinal("Description"))
            aircraft.EngineerId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("EngineerId"))
            aircraft.Engineer = myDataRecord.GetString(myDataRecord.GetOrdinal("Engineer"))
            aircraft.MechanicId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("MechanicId"))
            aircraft.Mechanic = myDataRecord.GetString(myDataRecord.GetOrdinal("Mechanic"))
            aircraft.RegistrationNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("RegistrationNbr"))
            aircraft.LiveryColor = myDataRecord.GetString(myDataRecord.GetOrdinal("LiveryColor"))
            aircraft.AircraftStatusId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("AircraftStatusId"))
            aircraft.AircraftStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("AircraftStatus"))
            aircraft.ManufacturerId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ManufacturerId"))
            aircraft.Manufacturer = myDataRecord.GetString(myDataRecord.GetOrdinal("Manufacturer"))
            aircraft.MeterUnitId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("MeterUnitId"))
            aircraft.MeterUnit = myDataRecord.GetString(myDataRecord.GetOrdinal("MeterUnit"))
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("AirframeHoursStart")) Then
                aircraft.AirframeHoursStart = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("AirframeHoursStart")), "#,##0.0")
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("AirframeHoursCurrent")) Then
                aircraft.AirframeHoursCurrent = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("AirframeHoursCurrent")), "#,##0.0")
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("AirframeHoursOffset")) Then
                aircraft.AirframeHoursOffset = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("AirframeHoursOffset")), "#,##0.0")
            End If
            aircraft.TotalRequiredUsage = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("TotalRequiredUsage")), "#,##0.0")
            aircraft.CurrentFlightUsage = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("CurrentFlightUsage")), "#,##0.0")
            aircraft.AircraftEngineHoursCurrent = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("AircraftEngineHoursCurrent")), "#,##0.0")
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("EstEndOfTesting")) Then
                aircraft.EstEndOfTesting = Format(myDataRecord.GetDateTime(myDataRecord.GetOrdinal("EstEndOfTesting")), "MM-dd-yyyy")
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("ActEndOfTesting")) Then
                aircraft.ActEndOfTesting = Format(myDataRecord.GetDateTime(myDataRecord.GetOrdinal("ActEndOfTesting")), "MM-dd-yyyy")
            End If

            aircraft.AircraftGuid = myDataRecord.GetGuid(myDataRecord.GetOrdinal("AircraftGuid"))
            aircraft.IsActive = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsActive"))
            aircraft.IsDeleted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsDeleted"))
            aircraft.UtcDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateCreated"))
            aircraft.UtcDateModified = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateModified"))
            aircraft.StampUserName = myDataRecord.GetString(myDataRecord.GetOrdinal("StampUserName"))
            aircraft.ConcurrencyId = DalUtility.ConvertRowVersionToString(myDataRecord("ConcurrencyId"))
            Return aircraft

        End Function

#Region "Private Child Object List Data Access Procedures"

        Private Shared Sub _processAircraftConfigurationList(ByVal Aircraft As Aircraft, ByVal Conn As SqlConnection, ByVal Trans As SqlTransaction)
            Try
                For Each thisAircraftConfiguration As AircraftConfiguration In Aircraft.AircraftConfigurationList
                    thisAircraftConfiguration.AircraftId = Aircraft.AircraftId
                    thisAircraftConfiguration.StampUserName = Aircraft.StampUserName
                    If ((thisAircraftConfiguration.ConcurrencyId = Nothing) Or (thisAircraftConfiguration.ConcurrencyId = "0") Or (thisAircraftConfiguration.ConcurrencyId = "0")) Then
                        AircraftConfigurationDB.Insert(thisAircraftConfiguration, Conn, Trans)
                    Else
                        AircraftConfigurationDB.Update(thisAircraftConfiguration, Conn, Trans)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub _processAircraftConfigurationDeleteList(ByVal Aircraft As Aircraft, ByVal Conn As SqlConnection, ByVal Trans As SqlTransaction)
            Try
                For Each thisAircraftConfiguration As AircraftConfiguration In Aircraft.AircraftConfigurationDeleteList
                    If thisAircraftConfiguration.AircraftConfigurationId > 0 Then
                        AircraftConfigurationDB.Delete(thisAircraftConfiguration.AircraftConfigurationId, Aircraft.StampUserName, Conn, Trans)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub _processAircraftLocationList(ByVal Aircraft As Aircraft, ByVal Conn As SqlConnection, ByVal Trans As SqlTransaction)
            Try
                For Each thisAircraftLocation As AircraftLocation In Aircraft.AircraftLocationList
                    thisAircraftLocation.AircraftId = Aircraft.AircraftId
                    thisAircraftLocation.StampUserName = Aircraft.StampUserName
                    If ((thisAircraftLocation.ConcurrencyId = Nothing) Or (thisAircraftLocation.ConcurrencyId = "0") Or (thisAircraftLocation.ConcurrencyId = "0")) Then
                        AircraftLocationDB.Insert(thisAircraftLocation, Conn, Trans)
                    Else
                        AircraftLocationDB.Update(thisAircraftLocation, Conn, Trans)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub _processAircraftLocationDeleteList(ByVal Aircraft As Aircraft, ByVal Conn As SqlConnection, ByVal Trans As SqlTransaction)
            For Each thisAircraftLocation As AircraftLocation In Aircraft.AircraftLocationDeleteList
                If thisAircraftLocation.AircraftLocationId > 0 Then
                    AircraftLocationDB.Delete(thisAircraftLocation.AircraftLocationId, Aircraft.StampUserName)
                End If
            Next
        End Sub

        Private Shared Sub _processGenericCommentList(ByVal Aircraft As Aircraft, ByVal Conn As SqlConnection, ByVal Trans As SqlTransaction)
            Try
                For Each thisGenericComment As GenericComment In Aircraft.GenericCommentList
                    If thisGenericComment.IsDirty Then
                        Select Case thisGenericComment.SourceTable
                            Case "AircraftComment"
                                Dim thisAircraftComment As AircraftComment = _morphToAircraftComment(thisGenericComment)
                                thisAircraftComment.AircraftId = Aircraft.AircraftId
                                thisAircraftComment.StampUserName = Aircraft.StampUserName
                                If ((thisAircraftComment.ConcurrencyId = Nothing) Or (thisAircraftComment.ConcurrencyId = "0")) Then
                                    AircraftCommentDB.Insert(thisAircraftComment, Conn, Trans)
                                Else
                                    AircraftCommentDB.Update(thisAircraftComment, Conn, Trans)
                                End If
                            Case "FlightComment"
                                Dim thisFlightComment As FlightComment = _morphToFlightComment(thisGenericComment)
                                thisFlightComment.StampUserName = Aircraft.StampUserName
                                If ((thisFlightComment.ConcurrencyId = Nothing) Or (thisFlightComment.ConcurrencyId = "0")) Then
                                    Throw New Exception("Adding a comment that is not an Aircraft Comment is not allowed")
                                Else
                                    FlightCommentDB.Update(thisFlightComment, Conn, Trans)
                                End If
                            Case "WOEngineerComment"
                                Dim thisWOEngineerComment As WOEngineerComment = _morphToWOEngineerComment(thisGenericComment)
                                thisWOEngineerComment.StampUserName = Aircraft.StampUserName
                                If ((thisWOEngineerComment.ConcurrencyId = Nothing) Or (thisWOEngineerComment.ConcurrencyId = "0")) Then
                                    Throw New Exception("Adding a comment that is not an Aircraft Comment is not allowed")
                                Else
                                    WOEngineerCommentDB.Update(thisWOEngineerComment, Conn, Trans)
                                End If
                            Case "WOMechanicComment"
                                Dim thisWOMechanicComment As WOMechanicComment = _morphToWOMechanicComment(thisGenericComment)
                                thisWOMechanicComment.StampUserName = Aircraft.StampUserName
                                If ((thisWOMechanicComment.ConcurrencyId = Nothing) Or (thisWOMechanicComment.ConcurrencyId = "0")) Then
                                    Throw New Exception("Adding a comment that is not an Aircraft Comment is not allowed")
                                Else
                                    WOMechanicCommentDB.Update(thisWOMechanicComment, Conn, Trans)
                                End If
                            Case Else
                                Throw New Exception("Unknown Comment Type")
                        End Select
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub _processGenericCommentDeleteList(ByVal Aircraft As Aircraft, ByVal Conn As SqlConnection, ByVal Trans As SqlTransaction)
            For Each thisGenericComment As GenericComment In Aircraft.GenericCommentDeleteList
                If thisGenericComment.SourceTable = "AircraftComment" And thisGenericComment.SourceID > 0 Then
                    AircraftCommentDB.Delete(thisGenericComment.SourceID, Aircraft.StampUserName)
                End If
            Next
        End Sub

        Private Shared Sub _processTestList(ByVal Aircraft As Aircraft, ByVal Conn As SqlConnection, ByVal Trans As SqlTransaction)
            Try
                For Each thisTest As fltLookupBase In Aircraft.TestList
                    If Not (AircraftTestXrefDB.Exists(Aircraft.AircraftId, thisTest.ID)) Then
                        AircraftTestXrefDB.Insert(Aircraft.AircraftId, thisTest.ID, Aircraft.StampUserName)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub _processTestDeleteList(ByVal Aircraft As Aircraft, ByVal Conn As SqlConnection, ByVal Trans As SqlTransaction)
            Try
                For Each thisTest As fltLookupBase In Aircraft.TestDeleteList
                    AircraftTestXrefDB.Delete(Aircraft.AircraftId, thisTest.ID, Aircraft.StampUserName)
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub _sortGenericCommentList(ByVal Aircraft As Aircraft)
            With Aircraft.GenericCommentList
                For i As Integer = 1 To (.Count - 1)
                    Dim holdComment As GenericComment = .Item(i)
                    Dim j As Integer = i - 1
                    Dim done As Boolean = False
                    Do
                        If CDate(.Item(j).CommentDate) < CDate(holdComment.CommentDate) Then
                            .Item(j + 1) = .Item(j)
                            j = j - 1
                            If j < 0 Then
                                done = True
                            End If
                        Else
                            done = True
                        End If
                    Loop Until done
                    .Item(j + 1) = holdComment
                Next
            End With
        End Sub

        Private Shared Function _morphToAircraftComment(ByVal GenericComment As GenericComment) As AircraftComment
            Dim newAircraftComment As New AircraftComment
            newAircraftComment.AircraftCommentId = GenericComment.SourceID
            newAircraftComment.AircraftId = GenericComment.ParentId
            newAircraftComment.EngineerId = GenericComment.AuthorID
            newAircraftComment.ComponentId = GenericComment.ComponentId
            newAircraftComment.CommentDate = GenericComment.CommentDate
            newAircraftComment.CommentRating = GenericComment.CommentRating
            newAircraftComment.IssueCodeId = GenericComment.IssueCodeId
            newAircraftComment.IssueCode = GenericComment.IssueCode
            newAircraftComment.Comments = GenericComment.Comments
            newAircraftComment.AircraftCommentGUID = GenericComment.CommentGUID
            newAircraftComment.IsDeleted = GenericComment.IsDeleted
            newAircraftComment.UtcDateCreated = GenericComment.UtcDateCreated
            newAircraftComment.UtcDateModified = GenericComment.UtcDateModified
            newAircraftComment.StampUserName = GenericComment.StampUserName
            newAircraftComment.ConcurrencyId = GenericComment.ConcurrencyId
            Return newAircraftComment
        End Function

        Private Shared Function _morphToFlightComment(ByVal GenericComment As GenericComment) As FlightComment
            Dim newFlightComment As New FlightComment
            newFlightComment.FlightCommentId = GenericComment.SourceID
            newFlightComment.FlightId = GenericComment.ParentId
            newFlightComment.ComponentId = GenericComment.ComponentId
            newFlightComment.CommentTime = GenericComment.CommentTime
            newFlightComment.CommentDateTime_UTC = GenericComment.CommentDateTime_UTC
            newFlightComment.CommentDateTime_Local = GenericComment.CommentDateTime_Local
            newFlightComment.CommentRating = GenericComment.CommentRating
            newFlightComment.IssueCodeId = GenericComment.IssueCodeId
            newFlightComment.IssueCode = GenericComment.IssueCode
            newFlightComment.PilotComment = GenericComment.Comments
            newFlightComment.FlightCommentGUID = GenericComment.CommentGUID
            newFlightComment.IsActive = True
            newFlightComment.IsDeleted = GenericComment.IsDeleted
            newFlightComment.UtcDateCreated = GenericComment.UtcDateCreated
            newFlightComment.UtcDateModified = GenericComment.UtcDateModified
            newFlightComment.StampUserName = GenericComment.StampUserName
            newFlightComment.ConcurrencyId = GenericComment.ConcurrencyId
            Return newFlightComment
        End Function

        Private Shared Function _morphToWOEngineerComment(ByVal GenericComment As GenericComment) As WOEngineerComment
            Dim newWOEngineerComment As New WOEngineerComment
            newWOEngineerComment.EngineerCommentsId = GenericComment.SourceID
            newWOEngineerComment.WorkOrderId = GenericComment.ParentId
            newWOEngineerComment.ComponentId = GenericComment.ComponentId
            newWOEngineerComment.CommentDate = GenericComment.CommentDate
            newWOEngineerComment.CommentRating = GenericComment.CommentRating
            newWOEngineerComment.IssueCodeId = GenericComment.IssueCodeId
            newWOEngineerComment.IssueCode = GenericComment.IssueCode
            newWOEngineerComment.Comments = GenericComment.Comments
            newWOEngineerComment.EngineerCommentGUID = GenericComment.CommentGUID
            newWOEngineerComment.IsDeleted = GenericComment.IsDeleted
            newWOEngineerComment.UtcDateCreated = GenericComment.UtcDateCreated
            newWOEngineerComment.UtcDateModified = GenericComment.UtcDateModified
            newWOEngineerComment.StampUserName = GenericComment.StampUserName
            newWOEngineerComment.ConcurrencyId = GenericComment.ConcurrencyId
            Return newWOEngineerComment
        End Function

        Private Shared Function _morphToWOMechanicComment(ByVal GenericComment As GenericComment) As WOMechanicComment
            Dim newWOMechanicComment As New WOMechanicComment
            newWOMechanicComment.WOMechanicCommentsId = GenericComment.SourceID
            newWOMechanicComment.WorkOrderId = GenericComment.ParentId
            newWOMechanicComment.ComponentId = GenericComment.ComponentId
            newWOMechanicComment.CommentDate = GenericComment.CommentDate
            newWOMechanicComment.CommentRating = GenericComment.CommentRating
            newWOMechanicComment.IssueCodeId = GenericComment.IssueCodeId
            newWOMechanicComment.IssueCode = GenericComment.IssueCode
            newWOMechanicComment.Comments = GenericComment.Comments
            newWOMechanicComment.MechanicCommentGUID = GenericComment.CommentGUID
            newWOMechanicComment.IsDeleted = GenericComment.IsDeleted
            newWOMechanicComment.UtcDateCreated = GenericComment.UtcDateCreated
            newWOMechanicComment.UtcDateModified = GenericComment.UtcDateModified
            newWOMechanicComment.StampUserName = GenericComment.StampUserName
            newWOMechanicComment.ConcurrencyId = GenericComment.ConcurrencyId
            Return newWOMechanicComment
        End Function

#End Region

    End Class

End Namespace
