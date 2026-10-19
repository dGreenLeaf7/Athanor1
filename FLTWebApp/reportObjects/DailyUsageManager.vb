' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports FLTBizObjects.FLT.Dal
Imports System.Configuration

Namespace FltWebApp

    <DataObjectAttribute()> _
    Public Class DailyUsageManager

        Public Shared Function GetArrayList(ByVal ProgId As Integer, ByVal LocationId As Integer, ByVal FlightDate As Date) As ArrayList

            Dim myArrayList As New ArrayList

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = ConfigurationManager.ConnectionStrings("FLTReport").ConnectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "rptDailyUsage"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.Add(New SqlParameter("@ProgId", SqlDbType.Int))
            myDbCommand.Parameters("@ProgId").Value = ProgId
            myDbCommand.Parameters.Add(New SqlParameter("@LocationId", SqlDbType.Int))
            myDbCommand.Parameters("@LocationId").Value = LocationId
            myDbCommand.Parameters.Add(New SqlParameter("@FlightDate", SqlDbType.DateTime))
            myDbCommand.Parameters("@FlightDate").Value = FlightDate

            Try
                myDbConnection.Open()

                Dim myReader As SqlDataReader = Nothing

                Try
                    myReader = myDbCommand.ExecuteReader()

                    If myReader.HasRows Then
                        Do While (myReader.Read())
                            myArrayList.Add(FillDataRecord(myReader))
                        Loop
                    End If
                    myReader.Close()
                Catch myException As Exception
                    Contoso.Utilities.Logger.Write(myException.Message)
                    Throw New ApplicationException("An application error has occurred.")
                Finally
                    CType(myReader, IDisposable).Dispose()
                End Try

                myDbConnection.Close()

            Catch myException As Exception
                Contoso.Utilities.Logger.Write(myException.Message)
                Throw New ApplicationException("An application error has occurred.")
            Finally
                CType(myDbConnection, IDisposable).Dispose()
            End Try

            Return myArrayList

        End Function

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As DailyUsageObject

            Dim dailyUsageObject As New DailyUsageObject

            Try

                dailyUsageObject.ProgId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ProgId"))
                dailyUsageObject.ProgName = myDataRecord.GetString(myDataRecord.GetOrdinal("ProgName"))
                dailyUsageObject.LocationId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("LocationId"))
                dailyUsageObject.LocationName = myDataRecord.GetString(myDataRecord.GetOrdinal("LocationName"))
                If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("FlightDate")) Then
                    dailyUsageObject.FlightDate = CDate(myDataRecord.GetDateTime(myDataRecord.GetOrdinal("FlightDate")))
                End If
                dailyUsageObject.AircraftId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("AircraftId"))
                dailyUsageObject.MSN = myDataRecord.GetString(myDataRecord.GetOrdinal("MSN"))
                dailyUsageObject.ModelName = myDataRecord.GetString(myDataRecord.GetOrdinal("ModelName"))
                dailyUsageObject.TestName = myDataRecord.GetString(myDataRecord.GetOrdinal("TestName"))
                dailyUsageObject.FlightId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlightId"))
                dailyUsageObject.HoursOnDate = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("HoursOnDate"))
                If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("DateLastRun")) Then
                    dailyUsageObject.DateLastRun = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("DateLastRun"))
                End If
                dailyUsageObject.LastMeterReading = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("LastMeterReading"))
                If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("LocationCurrentHours")) Then
                    dailyUsageObject.LocationCurrentHours = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("LocationCurrentHours"))
                End If
                dailyUsageObject.LocationPercentComplete = myDataRecord.GetDouble(myDataRecord.GetOrdinal("LocationPercentComplete"))
                dailyUsageObject.LocationGoalHours = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("LocationGoalHours"))
                If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("LocationEndTestDate")) Then
                    dailyUsageObject.LocationEndTestDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("LocationEndTestDate"))
                End If
                If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("AircraftCurrentUsage")) Then
                    dailyUsageObject.AircraftCurrentUsage = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("AircraftCurrentUsage"))
                End If
                dailyUsageObject.ProgramPercentComplete = myDataRecord.GetDouble(myDataRecord.GetOrdinal("ProgramPercentComplete"))
                If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("ProgramEndTestDate")) Then
                    dailyUsageObject.ProgramEndTestDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("ProgramEndTestDate"))
                End If
                dailyUsageObject.TotalRequiredUsage = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("TotalRequiredUsage"))

            Catch ex As Exception
                Contoso.Utilities.Logger.Write(ex.Message)
                Throw New ApplicationException("An application error has occurred.")
            End Try

            Return dailyUsageObject

        End Function

    End Class

End Namespace
