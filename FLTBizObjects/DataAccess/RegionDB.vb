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

	Public Class RegionDB
		
		Public Shared Function GetItem(ByVal Id As Long) As Region
			
			Dim myDbConnection As SqlConnection = New SqlConnection()
			myDbConnection.ConnectionString = AppConfiguration.connectionString
			
			Dim myDbCommand As SqlCommand = New SqlCommand()
			myDbCommand.CommandType = CommandType.StoredProcedure
			myDbCommand.CommandText = "fetchRegion"
			myDbCommand.Connection = myDbConnection
			
			myDbCommand.Parameters.AddWithValue("@Id", Id)
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
		
        Public Shared Function GetList(ByVal ProgId As Integer, ByVal IsActiveFilter As Long, ByVal RowStart As Long, ByVal RowCount As Long, ByRef TotalRows As Long) As RegionList

            Dim tempList As New RegionList

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listRegions"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@ProgId", ProgId)
            myDbCommand.Parameters.AddWithValue("@IsActiveFilter", IsActiveFilter)
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
		
		Public Shared Sub Update(ByVal myRegion As Region)
			
			Dim myDbConnection As SqlConnection = New SqlConnection()
			myDbConnection.ConnectionString = AppConfiguration.connectionString
			
			Dim myDbCommand As SqlCommand = New SqlCommand()
			myDbCommand.CommandType = CommandType.StoredProcedure
			myDbCommand.CommandText = "updateRegion"
			myDbCommand.Connection = myDbConnection
			
			myDbCommand.Parameters.AddWithValue("@Id", myRegion.Id)
			myDbCommand.Parameters.AddWithValue("@ProgId", myRegion.ProgId)
			myDbCommand.Parameters.AddWithValue("@Code", myRegion.Code)
			myDbCommand.Parameters.AddWithValue("@Name", myRegion.Name)
			myDbCommand.Parameters.AddWithValue("@IsActive", myRegion.IsActive)
			myDbCommand.Parameters.Add(new SqlParameter("@ConcurrencyId", SqlDbType.Timestamp, 8))
			myDbCommand.Parameters("@ConcurrencyId").Value = DalUtility.ConvertStringToRowVersion(myRegion.ConcurrencyId)
			myDbCommand.Parameters.Add(new SqlParameter("@NewConcurrencyId", SqlDbType.Timestamp, 8))
			myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
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
		
        Public Shared Function Insert(ByVal myRegion As Region) As Integer
            Dim newId As Integer = 0
            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "addRegion"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@ProgId", myRegion.ProgId)
            myDbCommand.Parameters.AddWithValue("@Code", myRegion.Code)
            myDbCommand.Parameters.AddWithValue("@Name", myRegion.Name)
            myDbCommand.Parameters.AddWithValue("@Id", myRegion.Id)
            myDbCommand.Parameters("@Id").Direction = ParameterDirection.Output
            myDbCommand.Parameters.AddWithValue("@NewConcurrencyId", 0)
            myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output

            Try
                myDbConnection.Open()
                myDbCommand.ExecuteNonQuery()
                myDbConnection.Close()

                newId = myDbCommand.Parameters("@Id").Value

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            Finally
                CType(myDbConnection, IDisposable).Dispose()
            End Try

            Return newId

        End Function
		
		Public Shared Sub Delete(ByVal Id As Long)
		
			Dim myDbConnection As SqlConnection = New SqlConnection()
			myDbConnection.ConnectionString = AppConfiguration.connectionString
			
			Dim myDbCommand As SqlCommand = New SqlCommand()
			myDbCommand.CommandType = CommandType.StoredProcedure
			myDbCommand.CommandText = "deleteRegion"
			myDbCommand.Connection = myDbConnection
			
			myDbCommand.Parameters.AddWithValue("@Id", Id)
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
		
		Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As Region
			
			Dim region As Region = New Region()
			region.Id = myDataRecord.GetInt32(myDataRecord.GetOrdinal("Id"))
			region.ProgId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ProgId"))
			region.Code = myDataRecord.GetString(myDataRecord.GetOrdinal("Code"))
			region.Name = myDataRecord.GetString(myDataRecord.GetOrdinal("Name"))
			region.IsActive = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsActive"))
			region.ConcurrencyId = DalUtility.ConvertRowVersionToString(myDataRecord("ConcurrencyId"))
			Return region
			
		End Function
		
	End Class

End Namespace
