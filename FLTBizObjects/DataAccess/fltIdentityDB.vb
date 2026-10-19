' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Contoso.Utilities
Imports FLTBizObjects.FLT.BO

Namespace FLT.Dal

    Public Class fltIdentityDB

        Public Shared Function AuthenticateUserLogin(ByVal UserName As String, ByVal Password As String) As FltIdentity

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "fltValidateLogin"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@UserName", UserName)
            myDbCommand.Parameters.AddWithValue("@Password", Password)
            Try
                myDbConnection.Open()

                Dim myReader As SqlDataReader = Nothing

                myReader = myDbCommand.ExecuteReader()

                If (Not myReader.HasRows) Then Return Nothing
                myReader.Read()

                Dim fltIdentity = CreateNewIdentity(UserName, myReader)

                myReader.NextResult()
                If myReader.HasRows Then

                    Do While (myReader.Read())

                        fltIdentity.UserProgramsList.Add(CreateNewUserProgram(myReader))

                    Loop

                End If

                Return fltIdentity

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error. Please Review the application error log for further information.")
            Finally
                CType(myDbConnection, IDisposable).Dispose()
            End Try

        End Function

        Public Shared Sub FetchUserProgramPermissions(ByVal FltIdentity As FltIdentity)

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "fltFetchUserProgramPermissions"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@UserName", FltIdentity.UserName)
            myDbCommand.Parameters.AddWithValue("@ProgId", FltIdentity.CurrentProgId)
            Try
                myDbConnection.Open()

                Dim myReader As SqlDataReader = Nothing

                myReader = myDbCommand.ExecuteReader()

                FltIdentity.IsCurrentProgramManager = False
                If myReader.HasRows Then
                    myReader.Read()
                    Dim myDataRecord As IDataRecord = CType(myReader, IDataRecord)
                    FltIdentity.IsCurrentProgramManager = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsProgramManager"))
                End If

                myReader.NextResult()
                If myReader.HasRows Then

                    Do While (myReader.Read())

                        FltIdentity.CurrentModulePermissionsList.Add(CreateNewUserModulePermissions(myReader))

                    Loop

                End If

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error. Please Review the application error log for further information.")
            Finally
                CType(myDbConnection, IDisposable).Dispose()
            End Try


        End Sub

        Private Shared Function CreateNewIdentity(ByVal UserName As String, ByVal myDataRecord As IDataRecord) As FltIdentity

            Dim isLoggedIn As Boolean = True
            Dim userDisplayName As String = myDataRecord.GetString(myDataRecord.GetOrdinal("DisplayName"))
            Dim defaultProgName As String = myDataRecord.GetString(myDataRecord.GetOrdinal("DefaultProgName"))
            Dim lastLoginDate As DateTime = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("LastLoginDate"))
            Dim defaultProgId As Integer = myDataRecord.GetInt32(myDataRecord.GetOrdinal("DefaultProgId"))
            Dim isSuperUser As Boolean = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsSuperUser"))

            Return New FltIdentity(CBool(True), userDisplayName, defaultProgName, lastLoginDate, UserName, isSuperUser, defaultProgId, False)

        End Function

        Private Shared Function CreateNewUserProgram(ByVal myDataRecord As IDataRecord) As FltIdentity.UserProgram

            Dim progId As Integer = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ProgId"))
            Dim progName As String = myDataRecord.GetString(myDataRecord.GetOrdinal("ProgName"))

            Return New FltIdentity.UserProgram(progId, progName)

        End Function

        Private Shared Function CreateNewUserModulePermissions(ByVal myDataRecord As IDataRecord) As FltIdentity.UserModulePermissions

            Dim moduleName As String = myDataRecord.GetString(myDataRecord.GetOrdinal("ModuleName"))
            Dim canView As Boolean = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("CanView"))
            Dim canEdit As Boolean = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("CanEdit"))

            Return New FltIdentity.UserModulePermissions(moduleName, canView, canEdit)

        End Function

    End Class

End Namespace
