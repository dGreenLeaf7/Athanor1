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

	Public Class WorkOrderPartDB
		
        Public Shared Function GetItem(ByVal WorkOrderPartId As Integer) As WorkOrderPart

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "fetchWorkOrderPart"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@WorkOrderPartId", WorkOrderPartId)
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

        Public Shared Function GetList(ByVal WorkOrderId As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As WorkOrderPartList

            Dim tempList As New WorkOrderPartList
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

        Public Shared Function GetList(ByVal WorkOrderId As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction) As WorkOrderPartList

            Dim tempList As New WorkOrderPartList

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listWorkOrderParts"
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
                        tempList = New WorkOrderPartList
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
		
        Public Shared Sub Update(ByVal myWorkOrderPart As WorkOrderPart)

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            Update(myWorkOrderPart, myDbConnection, myDbTransaction)
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

        Public Shared Sub Update(ByVal myWorkOrderPart As WorkOrderPart, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "updateWorkOrderPart"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.AddWithValue("@WorkOrderPartId", myWorkOrderPart.WorkOrderPartId)
            myDbCommand.Parameters.AddWithValue("@WorkOrderId", myWorkOrderPart.WorkOrderId)
            myDbCommand.Parameters.AddWithValue("@PartName", myWorkOrderPart.PartName)
            myDbCommand.Parameters.AddWithValue("@PartDescription", myWorkOrderPart.PartDescription)
            myDbCommand.Parameters.AddWithValue("@FisIDRemoved", myWorkOrderPart.FisIDRemoved)
            myDbCommand.Parameters.AddWithValue("@PartNbrFailed", CInt(myWorkOrderPart.PartNbrFailed))
            myDbCommand.Parameters.AddWithValue("@FisIDAdded", myWorkOrderPart.FisIDAdded)
            myDbCommand.Parameters.AddWithValue("@PartModule", myWorkOrderPart.PartModule)
            myDbCommand.Parameters.AddWithValue("@PartNbrRemoved", CInt(myWorkOrderPart.PartNbrRemoved))
            myDbCommand.Parameters.AddWithValue("@PartNbrAdded", CInt(myWorkOrderPart.PartNbrAdded))
            myDbCommand.Parameters.AddWithValue("@StampUserName", myWorkOrderPart.StampUserName)
            myDbCommand.Parameters.Add(New SqlParameter("@ConcurrencyId", SqlDbType.Timestamp, 8))
            myDbCommand.Parameters("@ConcurrencyId").Value = DalUtility.ConvertStringToRowVersion(myWorkOrderPart.ConcurrencyId)
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
		
        Public Shared Sub Insert(ByVal myWorkOrderPart As WorkOrderPart)

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            Insert(myWorkOrderPart, myDbConnection, myDbTransaction)
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

        Public Shared Sub Insert(ByVal myWorkOrderPart As WorkOrderPart, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "addWorkOrderPart"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.AddWithValue("@WorkOrderPartId", myWorkOrderPart.WorkOrderPartId)
            myDbCommand.Parameters.AddWithValue("@WorkOrderId", myWorkOrderPart.WorkOrderId)
            myDbCommand.Parameters.AddWithValue("@PartName", myWorkOrderPart.PartName)
            myDbCommand.Parameters.AddWithValue("@PartDescription", myWorkOrderPart.PartDescription)
            myDbCommand.Parameters.AddWithValue("@FisIDRemoved", myWorkOrderPart.FisIDRemoved)
            myDbCommand.Parameters.AddWithValue("@PartNbrFailed", CInt(myWorkOrderPart.PartNbrFailed))
            myDbCommand.Parameters.AddWithValue("@FisIDAdded", myWorkOrderPart.FisIDAdded)
            myDbCommand.Parameters.AddWithValue("@PartModule", myWorkOrderPart.PartModule)
            myDbCommand.Parameters.AddWithValue("@PartNbrRemoved", CInt(myWorkOrderPart.PartNbrRemoved))
            myDbCommand.Parameters.AddWithValue("@PartNbrAdded", CInt(myWorkOrderPart.PartNbrAdded))
            myDbCommand.Parameters.AddWithValue("@NewConcurrencyId", 0)
            myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.AddWithValue("@StampUserName", myWorkOrderPart.StampUserName)
            myDbCommand.Parameters.Add(New SqlParameter("@NewUTCDate", SqlDbType.DateTime, 8))
            myDbCommand.Parameters("@NewUTCDate").Direction = ParameterDirection.Output

            Try
                myDbCommand.ExecuteNonQuery()

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try

        End Sub
		
        Public Shared Sub Delete(ByVal myWorkOrderPartId As Integer, ByVal StampUserName As String)

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            Delete(myWorkOrderPartId, StampUserName, myDbConnection, myDbTransaction)
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

        Public Shared Sub Delete(ByVal WorkOrderPartId As Integer, ByVal StampUserName As String, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "deleteWorkOrderPart"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.AddWithValue("@WorkOrderPartId", WorkOrderPartId)
            myDbCommand.Parameters.AddWithValue("@StampUserName", StampUserName)
            Try
                myDbCommand.ExecuteNonQuery()
            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try
        End Sub
		
		Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As WorkOrderPart
			
			Dim workOrderPart As WorkOrderPart = New WorkOrderPart()
			workOrderPart.WorkOrderPartId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("WorkOrderPartId"))
			workOrderPart.WorkOrderId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("WorkOrderId"))
			workOrderPart.PartName = myDataRecord.GetString(myDataRecord.GetOrdinal("PartName"))
			workOrderPart.PartDescription = myDataRecord.GetString(myDataRecord.GetOrdinal("PartDescription"))
			workOrderPart.FisIDRemoved = myDataRecord.GetString(myDataRecord.GetOrdinal("FisIDRemoved"))
            workOrderPart.PartNbrFailed = myDataRecord.GetInt32(myDataRecord.GetOrdinal("PartNbrFailed")).ToString
			workOrderPart.FisIDAdded = myDataRecord.GetString(myDataRecord.GetOrdinal("FisIDAdded"))
			workOrderPart.PartModule = myDataRecord.GetString(myDataRecord.GetOrdinal("PartModule"))
            workOrderPart.PartNbrRemoved = myDataRecord.GetInt32(myDataRecord.GetOrdinal("PartNbrRemoved")).ToString
            workOrderPart.PartNbrAdded = myDataRecord.GetInt32(myDataRecord.GetOrdinal("PartNbrAdded")).ToString
            workOrderPart.IsDeleted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsDeleted"))
			workOrderPart.UtcDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateCreated"))
			workOrderPart.UtcDateModified = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateModified"))
            workOrderPart.StampUserName = myDataRecord.GetString(myDataRecord.GetOrdinal("StampUserName"))
			workOrderPart.ConcurrencyId = DalUtility.ConvertRowVersionToString(myDataRecord("ConcurrencyId"))
			Return workOrderPart
			
		End Function
		
	End Class

End Namespace
