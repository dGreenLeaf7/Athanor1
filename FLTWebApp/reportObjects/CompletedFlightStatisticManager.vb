' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports FLTBizObjects.FLT.Dal

<DataObjectAttribute()> _
Public Class CompletedFlightStatisticManager
    Public Shared Function GetArrayList(ByVal ProgId As Integer, ByVal DateBegin As Date, ByVal DateEnd As Date) As ArrayList

        Dim myArrayList As New ArrayList

        Dim myDbConnection As SqlConnection = New SqlConnection()
        myDbConnection.ConnectionString = ConfigurationManager.ConnectionStrings("FLTReport").ConnectionString

        Dim myDbCommand As SqlCommand = New SqlCommand()
        myDbCommand.CommandType = CommandType.StoredProcedure
        myDbCommand.CommandText = "rptFlightsCompleted_Statistics"
        myDbCommand.Connection = myDbConnection

        myDbCommand.Parameters.Add(New SqlParameter("@ProgId", SqlDbType.Int))
        myDbCommand.Parameters("@ProgId").Value = ProgId
        myDbCommand.Parameters.Add(New SqlParameter("@DateBegin", SqlDbType.VarChar, 10))
        myDbCommand.Parameters("@DateBegin").Value = Format(DateBegin, "yyyy-MM-dd")
        myDbCommand.Parameters.Add(New SqlParameter("@DateEnd", SqlDbType.VarChar, 10))
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

    Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As CompletedFlightStatisticObject

        Dim completedFlightStatisticObject As New CompletedFlightStatisticObject

        Try
            completedFlightStatisticObject.ProgName = myDataRecord.GetString(myDataRecord.GetOrdinal("ProgName"))
            completedFlightStatisticObject.LocationId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("LocactionId"))
            completedFlightStatisticObject.LocationName = myDataRecord.GetString(myDataRecord.GetOrdinal("LocationName"))
            completedFlightStatisticObject.FlownRouteId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlownRouteId"))
            completedFlightStatisticObject.RouteName = myDataRecord.GetString(myDataRecord.GetOrdinal("RouteName"))
            completedFlightStatisticObject.MeterUnitId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("MeterUnitId"))
            completedFlightStatisticObject.MeterUnit = myDataRecord.GetString(myDataRecord.GetOrdinal("MeterUnit"))
            completedFlightStatisticObject.FlightCount = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlightCount"))
            completedFlightStatisticObject.FlightMinutesMin = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlightMinutesMin"))
            completedFlightStatisticObject.FlightMinutesMax = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlightMinutesMax"))
            completedFlightStatisticObject.FlightMinutesAvg = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlightMinutesAvg"))
            completedFlightStatisticObject.FlightMinutesStDev = CInt(myDataRecord.GetDouble(myDataRecord.GetOrdinal("FlightMinutesStDev")))
            completedFlightStatisticObject.FlightAirtimeMin = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("FlightAirtimeMin"))
            completedFlightStatisticObject.FlightAirtimeMax = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("FlightAirtimeMax"))
            completedFlightStatisticObject.FlightAirtimeAvg = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("FlightAirtimeAvg"))
            completedFlightStatisticObject.FlightairtimeStDev = CDec(myDataRecord.GetDouble(myDataRecord.GetOrdinal("FlightAirtimeStDev")))

        Catch myException As Exception
            Contoso.Utilities.Logger.Write(myException.Message)
            Throw New ApplicationException("An application error has occurred.")
        End Try

        Return completedFlightStatisticObject

    End Function

End Class
