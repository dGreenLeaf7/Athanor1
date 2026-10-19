' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports FLTBizObjects.FLT.Dal

<DataObjectAttribute()> _
Public Class FlightDataManager

    Public Shared Function GetArrayList(ByVal ProgId As Integer, ByVal LocationId As Integer, ByVal AircraftId As Integer, ByVal DateRangeStart As Date, ByVal DateRangeEnd As Date, ByVal FuelLiquidGroupId As Integer, ByVal OilLiquidGroupId As Integer, ByVal FluidsLiquidGroupId As Integer) As ArrayList

        Dim myArrayList As New ArrayList

        Dim myDbConnection As SqlConnection = New SqlConnection()
        myDbConnection.ConnectionString = ConfigurationManager.ConnectionStrings("FLTReport").ConnectionString

        Dim myDbCommand As SqlCommand = New SqlCommand()
        myDbCommand.CommandType = CommandType.StoredProcedure
        myDbCommand.CommandText = "rptFlightData"
        myDbCommand.Connection = myDbConnection

        myDbCommand.Parameters.Add(New SqlParameter("@ProgId", SqlDbType.Int))
        myDbCommand.Parameters("@ProgId").Value = ProgId
        myDbCommand.Parameters.Add(New SqlParameter("@LocationId", SqlDbType.Int))
        myDbCommand.Parameters("@LocationId").Value = LocationId
        myDbCommand.Parameters.Add(New SqlParameter("@AircraftId", SqlDbType.Int))
        myDbCommand.Parameters("@AircraftId").Value = AircraftId
        myDbCommand.Parameters.Add(New SqlParameter("@DateRangeStart", SqlDbType.DateTime))
        myDbCommand.Parameters("@DateRangeStart").Value = DateRangeStart
        myDbCommand.Parameters.Add(New SqlParameter("@DateRangeEnd", SqlDbType.DateTime))
        myDbCommand.Parameters("@DateRangeEnd").Value = DateRangeEnd
        myDbCommand.Parameters.Add(New SqlParameter("@FuelLiquidGroupId", SqlDbType.Int))
        myDbCommand.Parameters("@FuelLiquidGroupId").Value = FuelLiquidGroupId
        myDbCommand.Parameters.Add(New SqlParameter("@OilLiquidGroupId", SqlDbType.Int))
        myDbCommand.Parameters("@OilLiquidGroupId").Value = OilLiquidGroupId
        myDbCommand.Parameters.Add(New SqlParameter("@FluidsLiquidGroupId", SqlDbType.Int))
        myDbCommand.Parameters("@FluidsLiquidGroupId").Value = FluidsLiquidGroupId

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

    Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As FlightDataObject

        Dim flightDataObject As New FlightDataObject

        Try
            flightDataObject.ProgName = myDataRecord.GetString(myDataRecord.GetOrdinal("ProgName"))
            flightDataObject.AircraftId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("AircraftId"))
            flightDataObject.MSN = myDataRecord.GetString(myDataRecord.GetOrdinal("MSN"))
            flightDataObject.RegistrationNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("RegistrationNbr"))
            flightDataObject.FlightId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlightId"))
            flightDataObject.FlightDate = CDate(myDataRecord.GetString(myDataRecord.GetOrdinal("FlightDate")))
            flightDataObject.Shift = myDataRecord.GetString(myDataRecord.GetOrdinal("Shift"))
            flightDataObject.DepartureTime = myDataRecord.GetString(myDataRecord.GetOrdinal("DepartureTime"))
            flightDataObject.ReturnTime = myDataRecord.GetString(myDataRecord.GetOrdinal("ReturnTime"))
            flightDataObject.LocationName = myDataRecord.GetString(myDataRecord.GetOrdinal("LocationName"))
            flightDataObject.SchedRouteName = myDataRecord.GetString(myDataRecord.GetOrdinal("SchedRouteName"))
            flightDataObject.FlownRouteName = myDataRecord.GetString(myDataRecord.GetOrdinal("FlownRouteName"))
            flightDataObject.PilotName = myDataRecord.GetString(myDataRecord.GetOrdinal("PilotName"))
            flightDataObject.MeterStart = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("MeterStart"))
            flightDataObject.MeterEnd = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("MeterEnd"))
            flightDataObject.DailyHours = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("DailyHours"))
            flightDataObject.Fuel = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("Fuel"))
            flightDataObject.Oil = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("Oil"))
            flightDataObject.Fluids = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("Fluids"))
            flightDataObject.Component = myDataRecord.GetString(myDataRecord.GetOrdinal("Component"))
            flightDataObject.PilotComments = myDataRecord.GetString(myDataRecord.GetOrdinal("PilotComment"))
            flightDataObject.Rating = CStr(myDataRecord.GetInt16(myDataRecord.GetOrdinal("CommentRating")))

        Catch myException As Exception
            Contoso.Utilities.Logger.Write(myException.Message)
            Throw New ApplicationException("An application error has occurred.")

        End Try

        Return flightDataObject

    End Function

End Class
