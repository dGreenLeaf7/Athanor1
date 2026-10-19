' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports FLTBizObjects.FLT.Dal
Imports Contoso.Utilities.Logger

<DataObjectAttribute()> _
Public Class FuelBurnManager

    Public Shared Function GetArrayList(ByVal ProgId As Integer, ByVal AircraftId As Integer, ByVal DateFrom As Date, ByVal DateThru As Date, ByVal LiquidGroupId As Integer) As ArrayList

        Dim myArrayList As New ArrayList

        Dim myDbConnection As SqlConnection = New SqlConnection()
        myDbConnection.ConnectionString = ConfigurationManager.ConnectionStrings("FLTReport").ConnectionString

        Dim myDbCommand As SqlCommand = New SqlCommand()
        myDbCommand.CommandType = CommandType.StoredProcedure
        myDbCommand.CommandText = "rptFuelBurn"
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

    Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As FuelBurnObject

        Dim fuelBurnObject As New FuelBurnObject

        Try

            fuelBurnObject.ProgName = myDataRecord.GetString(myDataRecord.GetOrdinal("ProgName"))
            fuelBurnObject.AircraftId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("AircraftId"))
            fuelBurnObject.MSN = myDataRecord.GetString(myDataRecord.GetOrdinal("MSN"))
            fuelBurnObject.RegistrationNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("RegistrationNbr"))
            fuelBurnObject.FlightDate = CDate(myDataRecord.GetDateTime(myDataRecord.GetOrdinal("FlightDate")))
            fuelBurnObject.LocationName = myDataRecord.GetString(myDataRecord.GetOrdinal("LocationName"))
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("MeterStart")) Then
                fuelBurnObject.MeterStart = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("MeterStart"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("MeterEnd")) Then
                fuelBurnObject.MeterEnd = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("MeterEnd"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("DailyHours")) Then
                fuelBurnObject.DailyHours = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("DailyHours"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("DailyFuel")) Then
                fuelBurnObject.DailyFuel = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("DailyFuel"))
            End If

        Catch ex As Exception
            Contoso.Utilities.Logger.Write(ex.Message)
            Throw New Exception("An application error has occurred.")
        End Try

        Return fuelBurnObject

    End Function

End Class
