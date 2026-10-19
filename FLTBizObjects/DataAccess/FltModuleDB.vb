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

	Public Class FltModuleDB
		
		Public Shared Function GetItem(ByVal ModuleName As String) As FltModule
			
			Dim myDbConnection As SqlConnection = New SqlConnection()
			myDbConnection.ConnectionString = AppConfiguration.connectionString
			
			Dim myDbCommand As SqlCommand = New SqlCommand()
			myDbCommand.CommandType = CommandType.StoredProcedure
			myDbCommand.CommandText = "fetchFltModule"
			myDbCommand.Connection = myDbConnection
			
			myDbCommand.Parameters.AddWithValue("@ModuleName", ModuleName)
			Try
				myDbConnection.Open()
				
				Dim myReader As DbDataReader = nothing
				
				Try
					myReader = myDbCommand.ExecuteReader(CommandBehavior.SingleRow)
					
					If (Not myReader.HasRows) Then Return Nothing
					
					myReader.Read()
					
					Return FillDataRecord(myReader)
					
				Catch myException as Exception
					Logger.Write(myException, myDbCommand.CommandText)
					Throw New ApplicationException("Application encountered a data error.")
				Finally
					CType(myReader, IDisposable).Dispose()
				End Try
				
				myDbConnection.Close()
				
			Catch myException as Exception
				Logger.Write(myException, myDbCommand.CommandText)
				Throw New ApplicationException("Application encountered a data error.")
			Finally
				CType(myDbConnection, IDisposable).Dispose()
			End Try
		End Function
		
        Public Shared Function GetList(ByVal IsActiveFilter As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As FltModuleList

            Dim tempList As FltModuleList = Nothing

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listFltModules"
            myDbCommand.Connection = myDbConnection

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
                        tempList = New FltModuleList
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
		
		Public Shared Sub Update(ByVal myFltModule As FltModule)
			
			Dim myDbConnection As SqlConnection = New SqlConnection()
			myDbConnection.ConnectionString = AppConfiguration.connectionString
			
			Dim myDbCommand As SqlCommand = New SqlCommand()
			myDbCommand.CommandType = CommandType.StoredProcedure
			myDbCommand.CommandText = "updateFltModule"
			myDbCommand.Connection = myDbConnection
			
			myDbCommand.Parameters.AddWithValue("@ModuleName", myFltModule.ModuleName)
			myDbCommand.Parameters.AddWithValue("@ModuleDescription", myFltModule.ModuleDescription)
			myDbCommand.Parameters.AddWithValue("@IsActive", myFltModule.IsActive)
			myDbCommand.Parameters.AddWithValue("@StampUserName", myFltModule.StampUserName)
			myDbCommand.Parameters.Add(new SqlParameter("@ConcurrencyId", SqlDbType.Timestamp, 8))
			myDbCommand.Parameters("@ConcurrencyId").Value = DalUtility.ConvertStringToRowVersion(myFltModule.ConcurrencyId)
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
		
		Public Shared Sub Insert(ByVal myFltModule As FltModule)
			Dim myDbConnection As SqlConnection = New SqlConnection()
			myDbConnection.ConnectionString = AppConfiguration.connectionString
			
			Dim myDbCommand As SqlCommand = New SqlCommand()
			myDbCommand.CommandType = CommandType.StoredProcedure
			myDbCommand.CommandText = "addFltModule"
			myDbCommand.Connection = myDbConnection
			
			myDbCommand.Parameters.AddWithValue("@ModuleName", myFltModule.ModuleName)
			myDbCommand.Parameters.AddWithValue("@ModuleDescription", myFltModule.ModuleDescription)
			myDbCommand.Parameters.AddWithValue("@NewConcurrencyId", 0)
			myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
			myDbCommand.Parameters.AddWithValue("@StampUserName", myFltModule.StampUserName)
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
		
		Public Shared Sub Delete(ByVal ModuleName As String, ByVal StampUserName As String)
		
			Dim myDbConnection As SqlConnection = New SqlConnection()
			myDbConnection.ConnectionString = AppConfiguration.connectionString
			
			Dim myDbCommand As SqlCommand = New SqlCommand()
			myDbCommand.CommandType = CommandType.StoredProcedure
			myDbCommand.CommandText = "deleteFltModule"
			myDbCommand.Connection = myDbConnection
			
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
		
		Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As FltModule
			
			Dim fltModule As FltModule = New FltModule()
			fltModule.ModuleName = myDataRecord.GetString(myDataRecord.GetOrdinal("ModuleName"))
			fltModule.ModuleDescription = myDataRecord.GetString(myDataRecord.GetOrdinal("ModuleDescription"))
			fltModule.IsActive = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsActive"))
			fltModule.IsDeleted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsDeleted"))
			fltModule.UtcDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateCreated"))
			fltModule.UtcDateModified = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateModified"))
			fltModule.StampUserName = myDataRecord.GetOrdinal("StampUserName")
			fltModule.ConcurrencyId = DalUtility.ConvertRowVersionToString(myDataRecord("ConcurrencyId"))
			Return fltModule
			
		End Function
		
	End Class

End Namespace
