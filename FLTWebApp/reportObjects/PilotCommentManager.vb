' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports FLTBizObjects.FLT.Dal

<DataObjectAttribute()> _
Public Class PilotCommentManager

    Public Shared Function GetArrayList(ByVal ProgId As Integer, ByVal LocationId As Integer, ByVal AircraftId As Integer, ByVal DateFrom As Date, ByVal DateThru As Date) As ArrayList

        Dim myArrayList As New ArrayList

        Dim myDbConnection As SqlConnection = New SqlConnection()
        myDbConnection.ConnectionString = ConfigurationManager.ConnectionStrings("FLTReport").ConnectionString

        Dim myDbCommand As SqlCommand = New SqlCommand()
        myDbCommand.CommandType = CommandType.StoredProcedure
        myDbCommand.CommandText = "rptPilotComments"
        myDbCommand.Connection = myDbConnection

        myDbCommand.Parameters.Add(New SqlParameter("@ProgId", SqlDbType.Int))
        myDbCommand.Parameters("@ProgId").Value = ProgId
        myDbCommand.Parameters.Add(New SqlParameter("@LocationId", SqlDbType.Int))
        myDbCommand.Parameters("@LocationId").Value = LocationId
        myDbCommand.Parameters.Add(New SqlParameter("@AircraftId", SqlDbType.Int))
        myDbCommand.Parameters("@AircraftId").Value = AircraftId
        myDbCommand.Parameters.Add(New SqlParameter("@DateFrom", SqlDbType.DateTime))
        myDbCommand.Parameters("@DateFrom").Value = DateFrom
        myDbCommand.Parameters.Add(New SqlParameter("@DateThru", SqlDbType.DateTime))
        myDbCommand.Parameters("@DateThru").Value = DateThru

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

    Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As PilotCommentObject

        Dim pilotCommentObject As New PilotCommentObject

        Try
            pilotCommentObject.ProgName = myDataRecord.GetString(myDataRecord.GetOrdinal("ProgName"))
            pilotCommentObject.LocationName = myDataRecord.GetString(myDataRecord.GetOrdinal("LocationName"))
            pilotCommentObject.LocationId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("LocationId"))
            pilotCommentObject.MSN = myDataRecord.GetString(myDataRecord.GetOrdinal("MSN"))
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("FlightDateSort")) Then
                If IsDate(myDataRecord.GetString(myDataRecord.GetOrdinal("FlightDate"))) Then
                    pilotCommentObject.FlightDate = CDate(myDataRecord.GetString(myDataRecord.GetOrdinal("FlightDate")))
                End If
            End If
            pilotCommentObject.FlightDateSort = myDataRecord.GetString(myDataRecord.GetOrdinal("FlightDateSort"))
            pilotCommentObject.FlightId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlightId"))
            pilotCommentObject.AirframeHoursEnd = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("AirframeHoursEnd"))
            pilotCommentObject.PilotName = myDataRecord.GetString(myDataRecord.GetOrdinal("PilotName"))
            pilotCommentObject.FlightCommentId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlightCommentId"))
            pilotCommentObject.ComponentName = myDataRecord.GetString(myDataRecord.GetOrdinal("ComponentName"))
            pilotCommentObject.CommentRating = CStr(myDataRecord.GetInt16(myDataRecord.GetOrdinal("CommentRating")))
            pilotCommentObject.PilotComment = myDataRecord.GetString(myDataRecord.GetOrdinal("PilotComment"))

        Catch myException As Exception
            Contoso.Utilities.Logger.Write(myException.Message)
            Throw New ApplicationException("An application error has occurred.")
        End Try

        Return pilotCommentObject

    End Function

End Class
