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

    Public Class FlightKeyInfoDB

        Public Shared Function GetItem(ByVal AircraftId As Integer, ByVal LocationId As Integer, ByVal FlightDateSort As String, ByVal WorkShiftId As Integer) As FlightKeyInfo

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "fetchFlightKeyInfo"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@AircraftId", AircraftId)
            myDbCommand.Parameters.AddWithValue("@LocationId", LocationId)
            myDbCommand.Parameters.AddWithValue("@FlightDateSort", FlightDateSort)
            myDbCommand.Parameters.AddWithValue("@WorkShiftId", WorkShiftId)

            Try
                myDbConnection.Open()

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

                myDbConnection.Close()

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            Finally
                CType(myDbConnection, IDisposable).Dispose()
            End Try
        End Function

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As FlightKeyInfo

            Dim flightKeyInfo As New FlightKeyInfo
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("FlightId")) Then
                flightKeyInfo.FlightId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlightId"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("AircraftId")) Then
                flightKeyInfo.AircraftId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("AircraftId"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("LocationId")) Then

                flightKeyInfo.LocationId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("LocationId"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("FlightDateSort")) Then
                flightKeyInfo.FlightDateSort = myDataRecord.GetString(myDataRecord.GetOrdinal("FlightDateSort"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("WorkShiftId")) Then
                flightKeyInfo.WorkShiftId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("WorkShiftId"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("WorkShift")) Then
                flightKeyInfo.WorkShift = myDataRecord.GetString(myDataRecord.GetOrdinal("WorkShift"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("MSN")) Then
                flightKeyInfo.MSN = myDataRecord.GetString(myDataRecord.GetOrdinal("MSN"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("SerialNbr")) Then
                flightKeyInfo.SerialNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("SerialNbr"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("RegistrationNbr")) Then
                flightKeyInfo.RegistrationNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("RegistrationNbr"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("AirframeHoursCurrent")) Then
                flightKeyInfo.AirframeHoursCurrent = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("AirframeHoursCurrent"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("AirframeHoursOffset")) Then
                flightKeyInfo.AirframeHoursOffset = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("AirframeHoursOffset"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("MeterUnitId")) Then
                flightKeyInfo.MeterUnitId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("MeterUnitId"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("MeterUnit")) Then
                flightKeyInfo.MeterUnit = myDataRecord.GetString(myDataRecord.GetOrdinal("MeterUnit"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("LocationName")) Then
                flightKeyInfo.LocationName = myDataRecord.GetString(myDataRecord.GetOrdinal("LocationName"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("WorkShift")) Then
                flightKeyInfo.WorkShift = myDataRecord.GetString(myDataRecord.GetOrdinal("WorkShift"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("ValidFlight")) Then
                flightKeyInfo.ValidFlight = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("ValidFlight"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("ValidAircraft")) Then
                flightKeyInfo.ValidAircraft = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("ValidAircraft"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("ValidLocation")) Then
                flightKeyInfo.ValidLocation = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("ValidLocation"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("ValidAircraftLocation")) Then
                flightKeyInfo.ValidAircraftLocation = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("ValidAircraftLocation"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("ValidWorkShift")) Then
                flightKeyInfo.ValidWorkShift = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("ValidWorkShift"))
            End If
            Return flightKeyInfo

        End Function

    End Class

End Namespace