' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports FLTBizObjects.FLT.Dal

<DataObjectAttribute()> _
Public Class AircraftDataManager

    Public Shared Function GetArrayList(ByVal ProgId As Integer, ByVal LocationId As Integer, ByVal AircraftId As Integer) As ArrayList

        Dim myArrayList As New ArrayList

        Dim myDbConnection As SqlConnection = New SqlConnection()
        myDbConnection.ConnectionString = ConfigurationManager.ConnectionStrings("FLTReport").ConnectionString

        Dim myDbCommand As SqlCommand = New SqlCommand()
        myDbCommand.CommandType = CommandType.StoredProcedure
        myDbCommand.CommandText = "rptAircraftData"
        myDbCommand.Connection = myDbConnection

        myDbCommand.Parameters.Add(New SqlParameter("@ProgId", SqlDbType.Int))
        myDbCommand.Parameters("@ProgId").Value = ProgId
        myDbCommand.Parameters.Add(New SqlParameter("@LocationId", SqlDbType.Int))
        myDbCommand.Parameters("@LocationId").Value = LocationId
        myDbCommand.Parameters.Add(New SqlParameter("@AircraftId", SqlDbType.Int))
        myDbCommand.Parameters("@AircraftId").Value = AircraftId

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

    Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As AircraftDataObject

        Dim aircraftDataObject As New AircraftDataObject

        Try
            aircraftDataObject.ProgName = myDataRecord.GetString(myDataRecord.GetOrdinal("ProgName"))
            aircraftDataObject.AircraftId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("AircraftId"))
            aircraftDataObject.MSN = myDataRecord.GetString(myDataRecord.GetOrdinal("MSN"))
            aircraftDataObject.RegistrationNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("RegistrationNbr"))
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("CurrentLocation")) Then
                aircraftDataObject.CurrentLocation = myDataRecord.GetString(myDataRecord.GetOrdinal("CurrentLocation"))
            End If
            aircraftDataObject.ShipNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("ShipNbr"))
            aircraftDataObject.SerialNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("SerialNbr"))
            aircraftDataObject.Model = myDataRecord.GetString(myDataRecord.GetOrdinal("Model"))
            aircraftDataObject.Description = myDataRecord.GetString(myDataRecord.GetOrdinal("Description"))
            aircraftDataObject.LiveryColor = myDataRecord.GetString(myDataRecord.GetOrdinal("LiveryColor"))
            aircraftDataObject.Engineer = myDataRecord.GetString(myDataRecord.GetOrdinal("Engineer"))
            aircraftDataObject.Mechanic = myDataRecord.GetString(myDataRecord.GetOrdinal("Mechanic"))
            aircraftDataObject.AircraftStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("AircraftStatus"))
            aircraftDataObject.Manufacturer = myDataRecord.GetString(myDataRecord.GetOrdinal("Manufacturer"))
            aircraftDataObject.AirframeHoursStart = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("AirframeHoursStart"))
            aircraftDataObject.MeterUnit = myDataRecord.GetString(myDataRecord.GetOrdinal("MeterUnit"))
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("TotalRequiredUsage")) Then
                aircraftDataObject.TotalRequiredUsage = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("TotalRequiredUsage"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("EstEndOfTesting")) Then
                aircraftDataObject.EstEndOfTesting = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("EstEndOfTesting")).ToString
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("ActEndOfTesting")) Then
                aircraftDataObject.ActEndOfTesting = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("ActEndOfTesting")).ToString
            End If
            aircraftDataObject.ActiveStatus = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("ActiveStatus"))
        Catch myException As Exception
            Contoso.Utilities.Logger.Write(myException.Message)
            Throw New ApplicationException("An application error has occurred.")
        End Try

        Return aircraftDataObject

    End Function

End Class
