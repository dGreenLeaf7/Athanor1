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

	Public Class FltRoleDB
		
        Public Shared Function GetItem(ByVal RoleId As Integer) As FltRole

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "fetchFltRole"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@RoleId", RoleId)
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
		
        Public Shared Function GetList(ByVal ProgId As Integer, ByVal IsActiveFilter As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As FltRoleList

            Dim tempList As FltRoleList = Nothing

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listFltRoles"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@ProgId", ProgId)
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
                        tempList = New FltRoleList
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
		
		Public Shared Sub Update(ByVal myFltRole As FltRole)
			
			Dim myDbConnection As SqlConnection = New SqlConnection()
			myDbConnection.ConnectionString = AppConfiguration.connectionString
			
			Dim myDbCommand As SqlCommand = New SqlCommand()
			myDbCommand.CommandType = CommandType.StoredProcedure
			myDbCommand.CommandText = "updateFltRole"
			myDbCommand.Connection = myDbConnection
			
			myDbCommand.Parameters.AddWithValue("@ProgId", myFltRole.ProgId)
			myDbCommand.Parameters.AddWithValue("@RoleId", myFltRole.RoleId)
			myDbCommand.Parameters.AddWithValue("@RoleName", myFltRole.RoleName)
			myDbCommand.Parameters.AddWithValue("@RoleDescription", myFltRole.RoleDescription)
			myDbCommand.Parameters.AddWithValue("@IsProgramManager", myFltRole.IsProgramManager)
			myDbCommand.Parameters.AddWithValue("@IsActive", myFltRole.IsActive)
			myDbCommand.Parameters.AddWithValue("@StampUserName", myFltRole.StampUserName)
			myDbCommand.Parameters.Add(new SqlParameter("@ConcurrencyId", SqlDbType.Timestamp, 8))
			myDbCommand.Parameters("@ConcurrencyId").Value = DalUtility.ConvertStringToRowVersion(myFltRole.ConcurrencyId)
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
		
        Public Shared Function Insert(ByVal myFltRole As FltRole) As Integer
            Dim newId As Integer = 0
            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "addFltRole"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@ProgId", myFltRole.ProgId)
            myDbCommand.Parameters.AddWithValue("@RoleName", myFltRole.RoleName)
            myDbCommand.Parameters.AddWithValue("@RoleDescription", myFltRole.RoleDescription)
            myDbCommand.Parameters.AddWithValue("@IsProgramManager", myFltRole.IsProgramManager)
            myDbCommand.Parameters.AddWithValue("@NewConcurrencyId", 0)
            myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.AddWithValue("@StampUserName", myFltRole.StampUserName)
            myDbCommand.Parameters.Add(New SqlParameter("@RoleId", SqlDbType.Int))
            myDbCommand.Parameters("@RoleId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.Add(New SqlParameter("@NewUTCDate", SqlDbType.DateTime, 8))
            myDbCommand.Parameters("@NewUTCDate").Direction = ParameterDirection.Output

            Try
                myDbConnection.Open()
                myDbCommand.ExecuteNonQuery()
                myDbConnection.Close()
                newId = myDbCommand.Parameters("@RoleId").Value

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            Finally
                CType(myDbConnection, IDisposable).Dispose()
            End Try
            Return newId
        End Function
		
        Public Shared Sub Delete(ByVal RoleId As Integer, ByVal StampUserName As String)

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "deleteFltRole"
            myDbCommand.Connection = myDbConnection

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
		
        Friend Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As FltRole

            Dim fltRole As FltRole = New FltRole()
            fltRole.ProgId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ProgId"))
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("ProgName")) Then
                fltRole.ProgName = myDataRecord.GetString(myDataRecord.GetOrdinal("ProgName"))
            End If
            fltRole.RoleId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("RoleId"))
            fltRole.RoleName = myDataRecord.GetString(myDataRecord.GetOrdinal("RoleName"))
            fltRole.RoleDescription = myDataRecord.GetString(myDataRecord.GetOrdinal("RoleDescription"))
            fltRole.IsProgramManager = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsProgramManager"))
            fltRole.IsActive = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsActive"))
            fltRole.IsDeleted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsDeleted"))
            fltRole.UtcDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateCreated"))
            fltRole.UtcDateModified = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateModified"))
            fltRole.StampUserName = myDataRecord.GetOrdinal("StampUserName")
            fltRole.ConcurrencyId = DalUtility.ConvertRowVersionToString(myDataRecord("ConcurrencyId"))
            Return fltRole

        End Function
		
	End Class

End Namespace
