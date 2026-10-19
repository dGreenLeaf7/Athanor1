' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Contoso.Utilities
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.BL

Namespace FLT.Dal

	Public Class FltUserInRoleDB
		
        Public Shared Function ListFltRolesForFltUser(ByVal UserName As String) As FltRoleList

            Dim tempList As FltRoleList = Nothing

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listFltRolesForFltUser"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@UserName", UserName)

            Try
                myDbConnection.Open()

                Dim myReader As SqlDataReader = Nothing

                Try
                    myReader = myDbCommand.ExecuteReader()

                    If myReader.HasRows Then
                        tempList = New FltRoleList
                        Do While (myReader.Read())
                            tempList.Add(FltRoleDB.FillDataRecord(myReader))
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

        Public Shared Function ListFltUsersForFltRole(ByVal RoleId As Integer) As FltUserList

            Dim tempList As New FltUserList

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listFltUsersForFltRole"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@RoleId", RoleId)

            Try
                myDbConnection.Open()

                Dim myReader As SqlDataReader = Nothing

                Try
                    myReader = myDbCommand.ExecuteReader()

                    If myReader.HasRows Then
                        Do While (myReader.Read())
                            tempList.Add(FltUserDB.FillDataRecord(myReader))
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

        Public Shared Function ListFltRolesNotForFltUser(ByVal UserName As String) As FltRoleList

            Dim tempList As FltRoleList = Nothing

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listFltRolesNotForFltUser"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@UserName", UserName)

            Try
                myDbConnection.Open()

                Dim myReader As SqlDataReader = Nothing

                Try
                    myReader = myDbCommand.ExecuteReader()

                    If myReader.HasRows Then
                        tempList = New FltRoleList
                        Do While (myReader.Read())
                            tempList.Add(FltRoleDB.FillDataRecord(myReader))
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

        Public Shared Function ListFltUsersNotForFltRole(ByVal RoleId As Integer) As FltUserList

            Dim tempList As FltUserList = Nothing

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listFltUsersNotForFltRole"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@RoleId", RoleId)

            Try
                myDbConnection.Open()

                Dim myReader As SqlDataReader = Nothing

                Try
                    myReader = myDbCommand.ExecuteReader()

                    If myReader.HasRows Then
                        tempList = New FltUserList
                        Do While (myReader.Read())
                            tempList.Add(FltUserDB.FillDataRecord(myReader))
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

        Public Shared Sub Insert(ByVal UserName As String, ByVal RoleId As Integer, ByVal StampUserName As String)
            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "addFltUserInRole"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@UserName", UserName)
            myDbCommand.Parameters.AddWithValue("@RoleId", RoleId)
            myDbCommand.Parameters.AddWithValue("@StampUserName", StampUserName)

            Try
                myDbConnection.Open()
                myDbCommand.ExecuteNonQuery()
                myDbConnection.Close()

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            Finally
                CType(myDbConnection, IDisposable).Dispose()
            End Try

        End Sub
		
		Public Shared Sub Delete(ByVal UserName As String, ByVal RoleId As Integer, ByVal StampUserName As String)
		
			Dim myDbConnection As SqlConnection = New SqlConnection()
			myDbConnection.ConnectionString = AppConfiguration.connectionString
			
			Dim myDbCommand As SqlCommand = New SqlCommand()
			myDbCommand.CommandType = CommandType.StoredProcedure
			myDbCommand.CommandText = "deleteFltUserInRole"
			myDbCommand.Connection = myDbConnection
			
			myDbCommand.Parameters.AddWithValue("@UserName", UserName)
			myDbCommand.Parameters.AddWithValue("@RoleId", RoleId)
			myDbCommand.Parameters.AddWithValue("@StampUserName", StampUserName)
			Try
				myDbConnection.Open()
				myDbCommand.ExecuteNonQuery()
				myDbConnection.Close()
			Catch myException as Exception
				Logger.Write(myException, myDbCommand.CommandText)
				Throw New ApplicationException("Application encountered a data error.")
			Finally
				CType(myDbConnection, IDisposable).Dispose()
			End Try
		End Sub

	End Class

End Namespace
