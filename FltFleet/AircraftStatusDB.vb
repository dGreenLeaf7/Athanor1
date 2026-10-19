' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Contoso.Utilities
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.BL

Public Class AircraftStatusDB

    Public Shared Function GetItem(ByVal AircraftId As Integer) As AircraftStatus

        Dim Result As AircraftStatus = Nothing

        Dim myDbConnection As SqlConnection = New SqlConnection()
        myDbConnection.ConnectionString = FLTBizObjects.FLT.Dal.AppConfiguration.connectionString

        Dim myDbCommand As SqlCommand = New SqlCommand()
        myDbCommand.CommandType = CommandType.StoredProcedure
        myDbCommand.CommandText = "fetchAircraftStatus"
        myDbCommand.Connection = myDbConnection

        myDbCommand.Parameters.AddWithValue("@AircraftId", AircraftId)
        Try
            myDbConnection.Open()

            Dim myReader As SqlDataReader = Nothing

            Try
                myReader = myDbCommand.ExecuteReader()

                If myReader.HasRows Then
                    myReader.Read()
                    Result = FillDataRecord(myReader)
                End If
                myReader.Close()
            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            Finally
                CType(myReader, IDisposable).Dispose()
            End Try

            myDbConnection.Close()

        Catch myException As Exception
            Logger.Write(myException, myDbCommand.CommandText)
            Throw New ApplicationException("Application encountered a data error.")
        Finally
            CType(myDbConnection, IDisposable).Dispose()
        End Try

        Return Result

    End Function

    Public Shared Function GetList(ByVal ProgId As Integer, ByVal SortingColumn As String, ByVal ShowOffTest As Boolean) As AircraftStatusList

        Dim tempList As New AircraftStatusList

        Dim myDbConnection As SqlConnection = New SqlConnection()
        myDbConnection.ConnectionString = FLTBizObjects.FLT.Dal.AppConfiguration.connectionString

        Dim myDbCommand As SqlCommand = New SqlCommand()
        myDbCommand.CommandType = CommandType.StoredProcedure
        myDbCommand.CommandText = "listAircraftStatuses"
        myDbCommand.Connection = myDbConnection

        myDbCommand.Parameters.AddWithValue("@ProgId", ProgId)
        myDbCommand.Parameters.AddWithValue("@SortBy", SortingColumn)

        Try
            myDbConnection.Open()

            Dim myReader As SqlDataReader = Nothing
            Dim TempAircraftStatus As AircraftStatus

            Try
                myReader = myDbCommand.ExecuteReader()

                If myReader.HasRows Then
                    Do While (myReader.Read())
                        TempAircraftStatus = FillDataRecord(myReader)
                        If (ShowOffTest And TempAircraftStatus.StatusAsString = "OFF TEST") Or _
                            (TempAircraftStatus.StatusAsString <> "OFF TEST") Then
                            tempList.Add(TempAircraftStatus)
                        End If
                    Loop
                End If
                myReader.Close()
            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            Finally
                CType(myReader, IDisposable).Dispose()
            End Try

            myDbConnection.Close()

        Catch myException As Exception
            Logger.Write(myException, myDbCommand.CommandText)
            Throw New ApplicationException("Application encountered a data error.")
        Finally
            CType(myDbConnection, IDisposable).Dispose()
        End Try

        Return tempList

    End Function

    Friend Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As AircraftStatus

        Dim aircraftStatus As AircraftStatus = New AircraftStatus()

        With aircraftStatus
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("AircraftId")) Then
                .AircraftId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("AircraftId"))
            Else
                .AircraftId = 0
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("AircraftStatusId")) Then
                .AircraftStatusId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("AircraftStatusId"))
            Else
                .AircraftStatusId = 0
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("EngineerComment")) Then
                .EngineerComment = myDataRecord.GetString(myDataRecord.GetOrdinal("EngineerComment"))
            Else
                .EngineerComment = ""
            End If

            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Status")) Then
                .Status = myDataRecord.GetInt32(myDataRecord.GetOrdinal("Status"))
            Else
                .Status = 0
            End If

            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("LocationName")) Then
                .LocationName = myDataRecord.GetString(myDataRecord.GetOrdinal("LocationName"))
            Else
                .LocationName = ""
            End If

            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("HangarComment")) Then
                .HangarComment = myDataRecord.GetString(myDataRecord.GetOrdinal("HangarComment"))
            Else
                .HangarComment = ""
            End If

            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("UTCDateModified")) Then
                .UTCDateModified = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateModified"))
            Else
                .UTCDateModified = DateTime.MinValue
            End If

            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("MSN")) Then
                .MSN = myDataRecord.GetString(myDataRecord.GetOrdinal("MSN"))
            Else
                .MSN = ""
            End If

        End With
        Return aircraftStatus

    End Function

    Public Shared Sub SaveStatusUpdate(ByVal AircraftId As Integer, _
        ByVal NewStatus As Integer, _
        ByVal NewEngineerComment As String, _
        ByVal NewHangarComment As String)

        Dim myDbConnection As SqlConnection = New SqlConnection()
        myDbConnection.ConnectionString = FLTBizObjects.FLT.Dal.AppConfiguration.connectionString

        Dim myDbCommand As SqlCommand = New SqlCommand()
        myDbCommand.CommandType = CommandType.StoredProcedure
        myDbCommand.CommandText = "updateAircraftStatus"
        myDbCommand.Connection = myDbConnection

        myDbCommand.Parameters.AddWithValue("@AircraftId", AircraftId)
        myDbCommand.Parameters.AddWithValue("@Status", NewStatus)
        myDbCommand.Parameters.AddWithValue("@EngineerComment", NewEngineerComment)
        myDbCommand.Parameters.AddWithValue("@HangarComment", NewHangarComment)

        Try
            myDbConnection.Open()
            myDbCommand.ExecuteNonQuery()

        Catch myException As Exception
            Logger.Write(myException, myDbCommand.CommandText)
            Throw New ApplicationException("Application encountered a data error.")
        End Try

        myDbConnection.Close()

    End Sub

End Class
