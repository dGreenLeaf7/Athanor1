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

    Public Class FlightInfoDB

        Public Shared Function GetList(ByVal AircraftId As Integer, ByVal AircraftTextMatch As String, ByVal LocationId As Integer, ByVal SchedRouteId As Integer, ByVal FlownRouteId As Integer, ByVal WorkShiftIndicatorId As Integer, ByVal FilterDateBegin As String, ByVal FilterDateEnd As String, ByVal IsApprovedFilter As Integer, ByVal IsActiveFilter As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As FlightInfoList

            Dim tempList As New FlightInfoList

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            tempList = GetList(AircraftId, AircraftTextMatch, LocationId, SchedRouteId, FlownRouteId, WorkShiftIndicatorId, FilterDateBegin, FilterDateEnd, IsApprovedFilter, IsActiveFilter, IsDeletedFilter, RowStart, RowCount, TotalRows, myDbConnection, myDbTransaction)
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

            Return tempList

        End Function

        Public Shared Function GetList(ByVal AircraftId As Integer, ByVal AircraftTextMatch As String, ByVal LocationId As Integer, ByVal SchedRouteId As Integer, ByVal FlownRouteId As Integer, ByVal WorkShiftIndicatorId As Integer, ByVal FilterDateBegin As String, ByVal FilterDateEnd As String, ByVal IsApprovedFilter As Integer, ByVal IsActiveFilter As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction) As FlightInfoList

            Dim tempList As New FlightInfoList

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listFlightInfos"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.AddWithValue("@LocationId", LocationId)
            myDbCommand.Parameters.AddWithValue("@AircraftId", AircraftId)
            myDbCommand.Parameters.AddWithValue("@AircraftTextMatch", AircraftTextMatch)
            myDbCommand.Parameters.AddWithValue("@SchedRouteId", SchedRouteId)
            myDbCommand.Parameters.AddWithValue("@FlownRouteId", FlownRouteId)
            myDbCommand.Parameters.AddWithValue("@WorkShiftIndicatorId", WorkShiftIndicatorId)
            myDbCommand.Parameters.AddWithValue("@FilterDateBegin", FilterDateBegin)
            myDbCommand.Parameters.AddWithValue("@FilterDateEnd", FilterDateEnd)
            myDbCommand.Parameters.AddWithValue("@IsApprovedFilter", IsApprovedFilter)
            myDbCommand.Parameters.AddWithValue("@IsActiveFilter", IsActiveFilter)
            myDbCommand.Parameters.AddWithValue("@IsDeletedFilter", IsDeletedFilter)
            myDbCommand.Parameters.AddWithValue("@Start", RowStart)
            myDbCommand.Parameters.AddWithValue("@Count", RowCount)
            myDbCommand.Parameters.AddWithValue("@Total", 0)
            myDbCommand.Parameters("@Total").Direction = ParameterDirection.Output

            Try
                Dim myReader As SqlDataReader = Nothing

                Try
                    myReader = myDbCommand.ExecuteReader()

                    If myReader.HasRows Then
                        Do While (myReader.Read())
                            tempList.Add(FillDataRecord(myReader))
                        Loop
                    End If
                    myReader.Close()

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

            Return tempList

        End Function

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As FlightInfo

            Dim flightInfo As FlightInfo = New FlightInfo()
            flightInfo.FlightId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlightId"))
            flightInfo.LocationId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("LocationId"))
            flightInfo.LocationName = myDataRecord.GetString(myDataRecord.GetOrdinal("LocationName"))
            flightInfo.FlightDate = myDataRecord.GetString(myDataRecord.GetOrdinal("FlightDate"))
            flightInfo.WorkShift = myDataRecord.GetString(myDataRecord.GetOrdinal("WorkShift"))
            flightInfo.MSN = myDataRecord.GetString(myDataRecord.GetOrdinal("MSN"))
            flightInfo.SerialNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("SerialNbr"))
            flightInfo.RegistrationNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("RegistrationNbr"))
            flightInfo.FlightDateSort = myDataRecord.GetString(myDataRecord.GetOrdinal("FlightDateSort"))
            flightInfo.DepartureTime = myDataRecord.GetString(myDataRecord.GetOrdinal("DepartureTime"))
            flightInfo.ReturnTime = myDataRecord.GetString(myDataRecord.GetOrdinal("ReturnTime"))
            flightInfo.AirframeHoursStart = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("AirframeHoursStart")).ToString
            flightInfo.AirframeHoursEnd = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("AirframeHoursEnd")).ToString
            flightInfo.FlightUsage = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("FlightUsage")).ToString
            flightInfo.IsApproved = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsApproved"))
            flightInfo.IsActive = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsActive"))
            flightInfo.IsDeleted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsDeleted"))
            flightInfo.UtcDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateCreated"))
            flightInfo.UtcDateModified = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateModified"))
            Return flightInfo

        End Function

    End Class


End Namespace
