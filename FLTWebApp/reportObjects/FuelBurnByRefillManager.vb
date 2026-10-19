' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports FLTBizObjects.FLT.Dal
Imports Contoso.Utilities.Logger

<DataObjectAttribute()> _
Public Class FuelBurnByRefillManager

    Public Shared Function GetArrayList(ByVal ProgId As Integer, ByVal AircraftId As Integer, ByVal DateFrom As Date, ByVal DateThru As Date, ByVal LiquidGroupId As Integer) As ArrayList

        Dim myArrayList As New ArrayList

        Dim myDbConnection As SqlConnection = New SqlConnection()
        myDbConnection.ConnectionString = ConfigurationManager.ConnectionStrings("FLTReport").ConnectionString

        Dim myDbCommand As SqlCommand = New SqlCommand()
        myDbCommand.CommandType = CommandType.StoredProcedure
        myDbCommand.CommandText = "rptFuelBurnByRefill"
        myDbCommand.Connection = myDbConnection

        myDbCommand.Parameters.Add(New SqlParameter("@ProgId", SqlDbType.Int))
        myDbCommand.Parameters("@ProgId").Value = ProgId
        myDbCommand.Parameters.Add(New SqlParameter("@AircraftId", SqlDbType.Int))
        myDbCommand.Parameters("@AircraftId").Value = AircraftId
        myDbCommand.Parameters.Add(New SqlParameter("@DateFrom", SqlDbType.DateTime))
        myDbCommand.Parameters("@DateFrom").Value = DateFrom
        myDbCommand.Parameters.Add(New SqlParameter("@DateThru", SqlDbType.DateTime))
        myDbCommand.Parameters("@DateThru").Value = DateThru
        myDbCommand.Parameters.Add(New SqlParameter("@LiquidGroupId", SqlDbType.Int))
        myDbCommand.Parameters("@LiquidGroupId").Value = LiquidGroupId

        myDbCommand.CommandTimeout = 360

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

    Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As FuelBurnByRefillObject

        Dim fuelBurnByRefillObject As New FuelBurnByRefillObject

        Try

            fuelBurnByRefillObject.ProgName = myDataRecord.GetString(myDataRecord.GetOrdinal("ProgName"))
            fuelBurnByRefillObject.AircraftId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("AircraftId"))
            fuelBurnByRefillObject.MSN = myDataRecord.GetString(myDataRecord.GetOrdinal("MSN"))
            fuelBurnByRefillObject.RegistrationNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("RegistrationNbr"))
            fuelBurnByRefillObject.FlightDate = CDate(myDataRecord.GetDateTime(myDataRecord.GetOrdinal("FlightDate")))
            fuelBurnByRefillObject.LocationName = myDataRecord.GetString(myDataRecord.GetOrdinal("LocationName"))
            fuelBurnByRefillObject.WorkShiftName = myDataRecord.GetString(myDataRecord.GetOrdinal("WorkShiftName"))
            fuelBurnByRefillObject.SchedRouteName = myDataRecord.GetString(myDataRecord.GetOrdinal("SchedRouteName"))
            fuelBurnByRefillObject.FlownRouteName = myDataRecord.GetString(myDataRecord.GetOrdinal("FlownRouteName"))
            fuelBurnByRefillObject.FlightMeterSort = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("FlightMeterSort"))
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("FlightMeterStart")) Then
                fuelBurnByRefillObject.FlightMeterStart = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("FlightMeterStart"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("FlightMeterEnd")) Then
                fuelBurnByRefillObject.FlightMeterEnd = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("FlightMeterEnd"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("FlightAirtime")) Then
                fuelBurnByRefillObject.FlightAirtime = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("FlightAirtime"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("TotalFlightAirtime")) Then
                fuelBurnByRefillObject.TotalFlightAirtime = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("TotalFlightAirtime"))
            End If
            fuelBurnByRefillObject.MeterUnit = myDataRecord.GetString(myDataRecord.GetOrdinal("MeterUnit"))
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("LiquidFillId")) Then
                fuelBurnByRefillObject.LiquidFillId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("LiquidFillId"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("RefillMeterLast")) Then
                fuelBurnByRefillObject.RefillMeterLast = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("RefillMeterLast"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("RefillMeterCurrent")) Then
                fuelBurnByRefillObject.RefillMeterCurrent = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("RefillMeterCurrent"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("RefillAirtime")) Then
                fuelBurnByRefillObject.RefillAirtime = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("RefillAirtime"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("LiquidGroup")) Then
                fuelBurnByRefillObject.LiquidGroup = myDataRecord.GetString(myDataRecord.GetOrdinal("LiquidGroup"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("LiquidAmt")) Then
                fuelBurnByRefillObject.LiquidAmt = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("LiquidAmt"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("LiquidUnit")) Then
                fuelBurnByRefillObject.LiquidUnit = myDataRecord.GetString(myDataRecord.GetOrdinal("LiquidUnit"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Usage")) Then
                fuelBurnByRefillObject.Usage = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("Usage"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("TotalRefillAirtime")) Then
                fuelBurnByRefillObject.TotalRefillAirtime = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("TotalRefillAirtime"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("TotalLiquidAmt")) Then
                fuelBurnByRefillObject.TotalLiquidAmt = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("TotalLiquidAmt"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Avgusage")) Then
                fuelBurnByRefillObject.AvgUsage = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("Avgusage"))
            End If

        Catch ex As Exception
            Contoso.Utilities.Logger.Write(ex.Message)
            Throw New Exception("An application error has occurred.")
        End Try

        Return fuelBurnByRefillObject

    End Function

End Class
