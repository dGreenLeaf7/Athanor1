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

	Public Class FltRoleModulePermissionsDB
		
        Public Shared Function GetItem(ByVal RoleId As Integer, ByVal ModuleName As String) As FltRoleModulePermissions

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "fetchFltRoleModulePermissions"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@RoleId", RoleId)
            myDbCommand.Parameters.AddWithValue("@ModuleName", ModuleName)
            Try
                myDbConnection.Open()

                Dim myReader As DbDataReader = Nothing

                Try
                    myReader = myDbCommand.ExecuteReader(CommandBehavior.SingleRow)

                    If (Not myReader.HasRows) Then Return Nothing

                    myReader.Read()

                    Return FillDataRecord(myReader)

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
        End Function
		
        Public Shared Function GetList(ByVal RoleId As Integer, ByVal ModuleName As String, ByVal IsActiveFilter As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As FltRoleModulePermissionsList

            Dim tempList As New FltRoleModulePermissionsList

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listFltRoleModulePermissionses"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@RoleId", RoleId)
            myDbCommand.Parameters.AddWithValue("@ModuleName", ModuleName)
            myDbCommand.Parameters.AddWithValue("@IsActiveFilter", IsActiveFilter)
            myDbCommand.Parameters.AddWithValue("@IsDeletedFilter", IsDeletedFilter)
            myDbCommand.Parameters.AddWithValue("@Start", RowStart)
            myDbCommand.Parameters.AddWithValue("@Count", RowCount)
            myDbCommand.Parameters.AddWithValue("@Total", 0)
            myDbCommand.Parameters("@Total").Direction = ParameterDirection.Output

            Try
                myDbConnection.Open()

                Dim myReader As SqlDataReader = Nothing

                Try
                    myReader = myDbCommand.ExecuteReader()

                    If myReader.HasRows Then
                        Do While (myReader.Read())
                            tempList.Add(FillDataRecord(myReader))
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
		
		Public Shared Sub Update(ByVal myFltRoleModulePermissions As FltRoleModulePermissions)
			
			Dim myDbConnection As SqlConnection = New SqlConnection()
			myDbConnection.ConnectionString = AppConfiguration.connectionString
			
			Dim myDbCommand As SqlCommand = New SqlCommand()
			myDbCommand.CommandType = CommandType.StoredProcedure
			myDbCommand.CommandText = "updateFltRoleModulePermissions"
			myDbCommand.Connection = myDbConnection
			
			myDbCommand.Parameters.AddWithValue("@RoleId", myFltRoleModulePermissions.RoleId)
			myDbCommand.Parameters.AddWithValue("@ModuleName", myFltRoleModulePermissions.ModuleName)
			myDbCommand.Parameters.AddWithValue("@CanView", myFltRoleModulePermissions.CanView)
			myDbCommand.Parameters.AddWithValue("@CanEdit", myFltRoleModulePermissions.CanEdit)
			myDbCommand.Parameters.AddWithValue("@IsActive", myFltRoleModulePermissions.IsActive)
			myDbCommand.Parameters.AddWithValue("@StampUserName", myFltRoleModulePermissions.StampUserName)
			myDbCommand.Parameters.Add(new SqlParameter("@ConcurrencyId", SqlDbType.Timestamp, 8))
			myDbCommand.Parameters("@ConcurrencyId").Value = DalUtility.ConvertStringToRowVersion(myFltRoleModulePermissions.ConcurrencyId)
			myDbCommand.Parameters.Add(new SqlParameter("@NewConcurrencyId", SqlDbType.Timestamp, 8))
			myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
			myDbCommand.Parameters.Add(new SqlParameter("@NewUTCDate", SqlDbType.DateTime, 8))
			myDbCommand.Parameters("@NewUTCDate").Direction = ParameterDirection.Output
			Try
				MyDbConnection.Open()
				myDbCommand.ExecuteNonQuery()
				myDbConnection.Close()
				
            Catch mySqlEx As SqlException
                If mySqlEx.Message = "Row has been edited by another user" Then
                    Throw New ApplicationException(mySqlEx.Message)
                Else
                    Logger.Write(mySqlEx, myDbCommand.CommandText)
                    Throw New ApplicationException("Application encountered a data error.")
                End If
            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
			Finally
				CType(myDbConnection, IDisposable).Dispose()
			End Try
			
		End Sub
		
		Public Shared Sub Insert(ByVal myFltRoleModulePermissions As FltRoleModulePermissions)
			Dim myDbConnection As SqlConnection = New SqlConnection()
			myDbConnection.ConnectionString = AppConfiguration.connectionString
			
			Dim myDbCommand As SqlCommand = New SqlCommand()
			myDbCommand.CommandType = CommandType.StoredProcedure
			myDbCommand.CommandText = "addFltRoleModulePermissions"
			myDbCommand.Connection = myDbConnection
			
			myDbCommand.Parameters.AddWithValue("@RoleId", myFltRoleModulePermissions.RoleId)
			myDbCommand.Parameters.AddWithValue("@ModuleName", myFltRoleModulePermissions.ModuleName)
			myDbCommand.Parameters.AddWithValue("@CanView", myFltRoleModulePermissions.CanView)
			myDbCommand.Parameters.AddWithValue("@CanEdit", myFltRoleModulePermissions.CanEdit)
			myDbCommand.Parameters.AddWithValue("@NewConcurrencyId", 0)
			myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
			myDbCommand.Parameters.AddWithValue("@StampUserName", myFltRoleModulePermissions.StampUserName)
			myDbCommand.Parameters.Add(new SqlParameter("@NewUTCDate", SqlDbType.DateTime, 8))
			myDbCommand.Parameters("@NewUTCDate").Direction = ParameterDirection.Output
			
			Try
				MyDbConnection.Open()
				myDbCommand.ExecuteNonQuery()
				myDbConnection.Close()
				
			Catch myException as Exception
				Logger.Write(myException, myDbCommand.CommandText)
				Throw New ApplicationException("Application encountered a data error.")
			Finally
				CType(myDbConnection, IDisposable).Dispose()
			End Try
			
		End Sub
		
		Public Shared Sub Delete(ByVal RoleId As Integer, ByVal ModuleName As String, ByVal StampUserName As String)
		
			Dim myDbConnection As SqlConnection = New SqlConnection()
			myDbConnection.ConnectionString = AppConfiguration.connectionString
			
			Dim myDbCommand As SqlCommand = New SqlCommand()
			myDbCommand.CommandType = CommandType.StoredProcedure
			myDbCommand.CommandText = "deleteFltRoleModulePermissions"
			myDbCommand.Connection = myDbConnection
			
			myDbCommand.Parameters.AddWithValue("@RoleId", RoleId)
			myDbCommand.Parameters.AddWithValue("@ModuleName", ModuleName)
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
		
		Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As FltRoleModulePermissions
			
			Dim fltRoleModulePermissions As FltRoleModulePermissions = New FltRoleModulePermissions()
			fltRoleModulePermissions.RoleId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("RoleId"))
			fltRoleModulePermissions.ModuleName = myDataRecord.GetString(myDataRecord.GetOrdinal("ModuleName"))
			fltRoleModulePermissions.CanView = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("CanView"))
			fltRoleModulePermissions.CanEdit = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("CanEdit"))
			fltRoleModulePermissions.IsActive = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsActive"))
			fltRoleModulePermissions.IsDeleted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsDeleted"))
			fltRoleModulePermissions.UtcDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateCreated"))
			fltRoleModulePermissions.UtcDateModified = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateModified"))
			fltRoleModulePermissions.StampUserName = myDataRecord.GetOrdinal("StampUserName")
			fltRoleModulePermissions.ConcurrencyId = DalUtility.ConvertRowVersionToString(myDataRecord("ConcurrencyId"))
			Return fltRoleModulePermissions
			
		End Function
		
	End Class

End Namespace
