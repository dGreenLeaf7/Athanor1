' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports FLTBizObjects.FLT.Dal

<DataObjectAttribute()> _
Public Class FlightStatisticManager

    Public Shared Function GetArrayList(ByVal ProgId As Integer, ByVal DateBegin As Date, ByVal DateEnd As Date) As ArrayList

        Dim myArrayList As New ArrayList

        Dim myDbConnection As SqlConnection = New SqlConnection()
        myDbConnection.ConnectionString = ConfigurationManager.ConnectionStrings("FLTReport").ConnectionString

        Dim myDbCommand As SqlCommand = New SqlCommand()
        myDbCommand.CommandType = CommandType.StoredProcedure
        myDbCommand.CommandText = "rptFlightStatistics"
        myDbCommand.Connection = myDbConnection

        myDbCommand.Parameters.Add(New SqlParameter("@ProgId", SqlDbType.Int))
        myDbCommand.Parameters("@ProgId").Value = ProgId
        myDbCommand.Parameters.Add(New SqlParameter("@DateBegin", SqlDbType.VarChar, 8))
        myDbCommand.Parameters("@DateBegin").Value = Format(DateBegin, "yyyy-MM-dd")
        myDbCommand.Parameters.Add(New SqlParameter("@DateEnd", SqlDbType.VarChar, 8))
        myDbCommand.Parameters("@DateEnd").Value = Format(DateEnd, "yyyy-MM-dd")

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

    Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As FlightStatisticObject

        Dim flightStatisticObject As New FlightStatisticObject

        Try
            flightStatisticObject.ProgName = myDataRecord.GetString(myDataRecord.GetOrdinal("ProgName"))
            flightStatisticObject.LocationId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("LocactionId"))
            flightStatisticObject.LocationName = myDataRecord.GetString(myDataRecord.GetOrdinal("LocationName"))
            flightStatisticObject.FlownRouteId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlownRouteId"))
            flightStatisticObject.RouteName = myDataRecord.GetString(myDataRecord.GetOrdinal("RouteName"))
            flightStatisticObject.MeterUnitId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("MeterUnitId"))
            flightStatisticObject.MeterUnit = myDataRecord.GetString(myDataRecord.GetOrdinal("MeterUnit"))
            flightStatisticObject.FlightCount = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlightCount"))
            flightStatisticObject.FlightMinutesMin = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlightMinutesMin"))
            flightStatisticObject.FlightMinutesMax = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlightMinutesMax"))
            flightStatisticObject.FlightMinutesAvg = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlightMinutesAvg"))
            flightStatisticObject.FlightMinutesStDev = CInt(myDataRecord.GetDouble(myDataRecord.GetOrdinal("FlightMinutesStDev")))
            flightStatisticObject.FlightAirtimeMin = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("FlightAirtimeMin"))
            flightStatisticObject.FlightAirtimeMax = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("FlightAirtimeMax"))
            flightStatisticObject.FlightAirtimeAvg = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("FlightAirtimeAvg"))
            flightStatisticObject.FlightairtimeStDev = CInt(myDataRecord.GetDouble(myDataRecord.GetOrdinal("FlightAirtimeStDev")))

        Catch myException As Exception
            Contoso.Utilities.Logger.Write(myException.Message)
            Throw New ApplicationException("An application error has occurred.")
        End Try

        Return flightStatisticObject

    End Function

End Class
