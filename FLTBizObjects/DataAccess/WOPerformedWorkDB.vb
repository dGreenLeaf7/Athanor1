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

	Public Class WOPerformedWorkDB
		
        Public Shared Function GetItem(ByVal PerformedWorkId As Integer) As WOPerformedWork

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "fetchWOPerformedWork"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@PerformedWorkId", PerformedWorkId)
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
		
        Public Shared Function GetList(ByVal WorkOrderId As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As WOPerformedWorkList

            Dim tempList As New WOPerformedWorkList
            Dim totRows As Integer = 0

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction

                        Try
                            tempList = GetList(WorkOrderId, 0, -1, -1, totRows, myDbConnection, myDbTransaction)
                            myDbTransaction.Commit()

                        Catch ex As ApplicationException
                            myDbTransaction.Rollback()
                            Throw New ApplicationException("Application encountered a data error.")

                        Catch ex As Exception
                            myDbTransaction.Rollback()
                            Logger.Write(ex.Message)
                            Throw New ApplicationException("Application encountered a data error.")

                        End Try
                    End Using

                Catch ex As ApplicationException
                    Throw New ApplicationException("Application encountered a data error.")

                Catch ex As Exception
                    Logger.Write(ex.Message)
                    Throw New ApplicationException("Application encountered a data error.")

                Finally
                    If myDbConnection.Region = ConnectionState.Open Then
                        myDbConnection.Close()
                    End If
                End Try

            End Using

            Return tempList

        End Function

        Public Shared Function GetList(ByVal WorkOrderId As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction) As WOPerformedWorkList

            Dim tempList As New WOPerformedWorkList

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listWOPerformedWorks"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.AddWithValue("@WorkOrderId", WorkOrderId)
            myDbCommand.Parameters.AddWithValue("@IsDeletedFilter", IsDeletedFilter)
            myDbCommand.Parameters.AddWithValue("@Start", RowStart)
            myDbCommand.Parameters.AddWithValue("@Count", RowCount)
            myDbCommand.Parameters.AddWithValue("@Total", 0)
            myDbCommand.Parameters("@Total").Direction = ParameterDirection.Output

            Try
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

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try

            Return tempList

        End Function
		
        Public Shared Sub Update(ByVal myWOPerformedWork As WOPerformedWork)

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            Update(myWOPerformedWork, myDbConnection, myDbTransaction)
                            myDbTransaction.Commit()

                        Catch ex As ApplicationException
                            myDbTransaction.Rollback()
                            Throw New ApplicationException("Application encountered a data error.")

                        Catch ex As Exception
                            myDbTransaction.Rollback()
                            Logger.Write(ex.Message)
                            Throw New ApplicationException("Application encountered a data error.")

                        End Try

                    End Using

                Catch ex As ApplicationException
                    Throw New ApplicationException("Application encountered a data error.")

                Catch ex As Exception
                    Logger.Write(ex.Message)
                    Throw New ApplicationException("Application encountered a data error.")

                Finally
                    If myDbConnection.Region = ConnectionState.Open Then
                        myDbConnection.Close()
                    End If

                End Try

            End Using

        End Sub

        Public Shared Sub Update(ByVal myWOPerformedWork As WOPerformedWork, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "updateWOPerformedWork"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.AddWithValue("@PerformedWorkId", myWOPerformedWork.PerformedWorkId)
            myDbCommand.Parameters.AddWithValue("@WorkOrderId", myWOPerformedWork.WorkOrderId)
            myDbCommand.Parameters.AddWithValue("@PerformedWorkLine", myWOPerformedWork.PerformedWorkLine)
            myDbCommand.Parameters.AddWithValue("@StampUserName", myWOPerformedWork.StampUserName)
            myDbCommand.Parameters.Add(New SqlParameter("@ConcurrencyId", SqlDbType.Timestamp, 8))
            myDbCommand.Parameters("@ConcurrencyId").Value = DalUtility.ConvertStringToRowVersion(myWOPerformedWork.ConcurrencyId)
            myDbCommand.Parameters.Add(New SqlParameter("@NewConcurrencyId", SqlDbType.Timestamp, 8))
            myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.Add(New SqlParameter("@NewUTCDate", SqlDbType.DateTime, 8))
            myDbCommand.Parameters("@NewUTCDate").Direction = ParameterDirection.Output
            Try
                myDbCommand.ExecuteNonQuery()

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
            End Try

        End Sub
		
        Public Shared Sub Insert(ByVal myWOPerformedWork As WOPerformedWork)

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            Insert(myWOPerformedWork, myDbConnection, myDbTransaction)
                            myDbTransaction.Commit()

                        Catch ex As ApplicationException
                            myDbTransaction.Rollback()
                            Throw New ApplicationException("Application encountered a data error.")

                        Catch ex As Exception
                            myDbTransaction.Rollback()
                            Logger.Write(ex.Message)
                            Throw New ApplicationException("Application encountered a data error.")

                        End Try

                    End Using

                Catch ex As ApplicationException
                    Throw New ApplicationException("Application encountered a data error.")

                Catch ex As Exception
                    Logger.Write(ex.Message)
                    Throw New ApplicationException("Application encountered a data error.")

                Finally
                    If myDbConnection.Region = ConnectionState.Open Then
                        myDbConnection.Close()
                    End If
                End Try

            End Using

        End Sub

        Public Shared Sub Insert(ByVal myWOPerformedWork As WOPerformedWork, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "addWOPerformedWork"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.AddWithValue("@PerformedWorkId", myWOPerformedWork.PerformedWorkId)
            myDbCommand.Parameters.AddWithValue("@WorkOrderId", myWOPerformedWork.WorkOrderId)
            myDbCommand.Parameters.AddWithValue("@PerformedWorkLine", myWOPerformedWork.PerformedWorkLine)
            myDbCommand.Parameters.AddWithValue("@NewConcurrencyId", 0)
            myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.AddWithValue("@StampUserName", myWOPerformedWork.StampUserName)
            myDbCommand.Parameters.Add(New SqlParameter("@NewUTCDate", SqlDbType.DateTime, 8))
            myDbCommand.Parameters("@NewUTCDate").Direction = ParameterDirection.Output

            Try
                myDbCommand.ExecuteNonQuery()

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try

        End Sub
		
        Public Shared Sub Delete(ByVal myWOPerformedWorkId As Integer, ByVal StampUserName As String)

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            Delete(myWOPerformedWorkId, StampUserName, myDbConnection, myDbTransaction)
                            myDbTransaction.Commit()

                        Catch ex As ApplicationException
                            myDbTransaction.Rollback()
                            Throw New ApplicationException("Application encountered a data error.")

                        Catch ex As Exception
                            myDbTransaction.Rollback()
                            Logger.Write(ex.Message)
                            Throw New ApplicationException("Application encountered a data error.")

                        End Try

                    End Using

                Catch ex As ApplicationException
                    Throw New ApplicationException("Application encountered a data error.")

                Catch ex As Exception
                    Logger.Write(ex.Message)
                    Throw New ApplicationException("Application encountered a data error.")

                Finally
                    If myDbConnection.Region = ConnectionState.Open Then
                        myDbConnection.Close()
                    End If
                End Try

            End Using

        End Sub

        Public Shared Sub Delete(ByVal PerformedWorkId As Integer, ByVal StampUserName As String, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)
            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "deleteWOPerformedWork"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.AddWithValue("@PerformedWorkId", PerformedWorkId)
            myDbCommand.Parameters.AddWithValue("@StampUserName", StampUserName)
            Try
                myDbCommand.ExecuteNonQuery()
            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try
        End Sub
		
		Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As WOPerformedWork
			
			Dim wOPerformedWork As WOPerformedWork = New WOPerformedWork()
			wOPerformedWork.PerformedWorkId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("PerformedWorkId"))
			wOPerformedWork.WorkOrderId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("WorkOrderId"))
            wOPerformedWork.PerformedWorkLine = myDataRecord.GetString(myDataRecord.GetOrdinal("PerformedWorkLine"))
            wOPerformedWork.PerformedWorkGUID = myDataRecord.GetGuid(myDataRecord.GetOrdinal("WOPerformedWorkGuid"))
            wOPerformedWork.IsDeleted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsDeleted"))
			wOPerformedWork.UtcDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateCreated"))
			wOPerformedWork.UtcDateModified = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateModified"))
            wOPerformedWork.StampUserName = myDataRecord.GetString(myDataRecord.GetOrdinal("StampUserName"))
			wOPerformedWork.ConcurrencyId = DalUtility.ConvertRowVersionToString(myDataRecord("ConcurrencyId"))
			Return wOPerformedWork
			
		End Function
		
	End Class

End Namespace
