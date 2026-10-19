' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports FLTBizObjects.FLT.Dal

<DataObjectAttribute()> _
Public Class FlightOutlierManager

    Public Shared Function GetArrayList(ByVal ProgId As Integer, ByVal DateBegin As Date, ByVal DateEnd As Date, ByVal NbrOfStDevs As Integer) As ArrayList

        Dim myArrayList As New ArrayList

        Dim myDbConnection As SqlConnection = New SqlConnection()
        myDbConnection.ConnectionString = ConfigurationManager.ConnectionStrings("FLTReport").ConnectionString

        Dim myDbCommand As SqlCommand = New SqlCommand()
        myDbCommand.CommandType = CommandType.StoredProcedure
        myDbCommand.CommandText = "rptFlightOutliers"
        myDbCommand.Connection = myDbConnection

        myDbCommand.Parameters.Add(New SqlParameter("@ProgId", SqlDbType.Int))
        myDbCommand.Parameters("@ProgId").Value = ProgId
        myDbCommand.Parameters.Add(New SqlParameter("@DateBegin", SqlDbType.VarChar, 8))
        myDbCommand.Parameters("@DateBegin").Value = Format(DateBegin, "yyyy-MM-dd")
        myDbCommand.Parameters.Add(New SqlParameter("@DateEnd", SqlDbType.VarChar, 8))
        myDbCommand.Parameters("@DateEnd").Value = Format(DateEnd, "yyyy-MM-dd")
        myDbCommand.Parameters.Add(New SqlParameter("@NbrOfStDevs", SqlDbType.Int))
        myDbCommand.Parameters("@NbrOfStDevs").Value = NbrOfStDevs

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

    Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As FlightOutlierObject

        Dim flightOutlierObject As New FlightOutlierObject

        Try
            flightOutlierObject.ProgName = myDataRecord.GetString(myDataRecord.GetOrdinal("ProgName"))
            flightOutlierObject.LocationId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("LocationId"))
            flightOutlierObject.LocationName = myDataRecord.GetString(myDataRecord.GetOrdinal("LocationName"))
            flightOutlierObject.SchedRouteId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("SchedRouteId"))
            flightOutlierObject.SchedRouteName = myDataRecord.GetString(myDataRecord.GetOrdinal("SchedRouteName"))
            flightOutlierObject.FlownRouteId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlownRouteId"))
            flightOutlierObject.FlownRouteName = myDataRecord.GetString(myDataRecord.GetOrdinal("FlownRouteName"))
            flightOutlierObject.AircraftId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("AircraftId"))
            flightOutlierObject.MSN = myDataRecord.GetString(myDataRecord.GetOrdinal("MSN"))
            flightOutlierObject.FlightId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlightId"))
            flightOutlierObject.PilotId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("PilotId"))
            flightOutlierObject.PilotName = myDataRecord.GetString(myDataRecord.GetOrdinal("PilotName"))
            flightOutlierObject.FlightTimeOutlier = CBool(myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlightTimeOutlier")))
            flightOutlierObject.FlightAirtimeOutlier = CBool(myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlightAirtimeOutlier")))
            flightOutlierObject.FlightDateSort = myDataRecord.GetString(myDataRecord.GetOrdinal("FlightDateSort"))
            flightOutlierObject.DepartureTime = CDate(myDataRecord.GetString(myDataRecord.GetOrdinal("DepartureTime")))
            flightOutlierObject.ReturnTime = CDate(myDataRecord.GetString(myDataRecord.GetOrdinal("ReturnTime")))
            flightOutlierObject.FlightMinutes = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlightMinutes"))
            flightOutlierObject.FlightMinutesAvg = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlightMinutesAvg"))
            flightOutlierObject.FlightMinutesStDev = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlightMinutesStDev"))
            flightOutlierObject.HobbsStart = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("HobbsStart"))
            flightOutlierObject.HobbsEnd = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("HobbsEnd"))
            flightOutlierObject.MeterUnitId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("MeterUnitId"))
            flightOutlierObject.MeterUnit = myDataRecord.GetString(myDataRecord.GetOrdinal("MeterUnit"))
            flightOutlierObject.FlightAirtime = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("FlightAirtime"))
            flightOutlierObject.FlightAirtimeAvg = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("FlightAirtimeAvg"))
            flightOutlierObject.FlightAirtimeStDev = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("FlightAirtimeStDev"))

        Catch myException As Exception
            Contoso.Utilities.Logger.Write(myException.Message)
            Throw New ApplicationException("An application error has occurred.")
        End Try

        Return flightOutlierObject

    End Function

End Class
