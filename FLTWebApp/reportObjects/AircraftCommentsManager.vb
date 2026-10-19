' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports FLTBizObjects.FLT.Dal

<DataObjectAttribute()> _
    Public Class AircraftCommentsManager

    Public Shared Function GetArrayList(ByVal ProgId As Integer, ByVal LocationId As Integer, ByVal AircraftId As Integer, ByVal ComponentId As Integer, ByVal RatingLow As Short, ByVal RatingHigh As Short, ByVal DateFrom As Date, ByVal DateThru As Date) As ArrayList

        Dim myArrayList As New ArrayList

        Dim myDbConnection As SqlConnection = New SqlConnection()
        myDbConnection.ConnectionString = ConfigurationManager.ConnectionStrings("FLTReport").ConnectionString

        Dim myDbCommand As SqlCommand = New SqlCommand()
        myDbCommand.CommandType = CommandType.StoredProcedure
        myDbCommand.CommandText = "rptAircraftComments"
        myDbCommand.Connection = myDbConnection
        myDbCommand.CommandTimeout = 360

        myDbCommand.Parameters.Add(New SqlParameter("@ProgId", SqlDbType.Int))
        myDbCommand.Parameters("@ProgId").Value = ProgId
        myDbCommand.Parameters.Add(New SqlParameter("@LocationId", SqlDbType.Int))
        myDbCommand.Parameters("@LocationId").Value = LocationId
        myDbCommand.Parameters.Add(New SqlParameter("@AircraftId", SqlDbType.Int))
        myDbCommand.Parameters("@AircraftId").Value = AircraftId
        myDbCommand.Parameters.Add(New SqlParameter("@ComponentId", SqlDbType.Int))
        myDbCommand.Parameters("@ComponentId").Value = ComponentId
        myDbCommand.Parameters.Add(New SqlParameter("@RatingLow", SqlDbType.SmallInt))
        myDbCommand.Parameters("@RatingLow").Value = RatingLow
        myDbCommand.Parameters.Add(New SqlParameter("@RatingHigh", SqlDbType.SmallInt))
        myDbCommand.Parameters("@RatingHigh").Value = RatingHigh
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

    Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As AircraftCommentsObject

        Dim aircraftCommentsObject As New AircraftCommentsObject

        Try
            aircraftCommentsObject.ProgName = myDataRecord.GetString(myDataRecord.GetOrdinal("ProgName"))
            aircraftCommentsObject.LocationName = myDataRecord.GetString(myDataRecord.GetOrdinal("LocationName"))
            aircraftCommentsObject.LocationId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("LocationId"))
            aircraftCommentsObject.MSN = myDataRecord.GetString(myDataRecord.GetOrdinal("MSN"))
            aircraftCommentsObject.AircraftId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("AircraftId"))
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("CommentDate")) Then
                aircraftCommentsObject.CommentDate = CDate(myDataRecord.GetDateTime(myDataRecord.GetOrdinal("CommentDate")))
            End If
            aircraftCommentsObject.SourceId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("SourceId"))
            aircraftCommentsObject.Source = myDataRecord.GetString(myDataRecord.GetOrdinal("Source"))
            aircraftCommentsObject.CommentAuthor = myDataRecord.GetString(myDataRecord.GetOrdinal("CommentAuthor"))
            aircraftCommentsObject.CommentRating = CStr(myDataRecord.GetInt16(myDataRecord.GetOrdinal("CommentRating")))
            aircraftCommentsObject.Component = myDataRecord.GetString(myDataRecord.GetOrdinal("Component"))
            aircraftCommentsObject.ComponentId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ComponentId"))
            aircraftCommentsObject.Comment = myDataRecord.GetString(myDataRecord.GetOrdinal("Comment"))
        Catch myException As Exception
            Contoso.Utilities.Logger.Write(myException.Message)
            Throw New ApplicationException("An application error has occurred.")

        End Try

        Return aircraftCommentsObject

    End Function

End Class
