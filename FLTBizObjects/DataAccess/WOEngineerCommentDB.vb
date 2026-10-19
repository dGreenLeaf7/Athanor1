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

	Public Class WOEngineerCommentDB
		
        Public Shared Function GetItem(ByVal EngineerCommentsId As Integer) As WOEngineerComment

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "fetchWOEngineerComment"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@EngineerCommentsId", EngineerCommentsId)
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
		
        Public Shared Function GetList(ByVal WorkOrderId As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As WOEngineerCommentList

            Dim tempList As New WOEngineerCommentList
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

        Public Shared Function GetList(ByVal WorkOrderId As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction) As WOEngineerCommentList

            Dim tempList As New WOEngineerCommentList

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listWOEngineerComments"
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
                        tempList = New WOEngineerCommentList
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
		
		Public Shared Sub Update(ByVal myWOEngineerComment As WOEngineerComment)

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction

                        Try
                            Update(myWOEngineerComment, myDbConnection, myDbTransaction)
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

        Public Shared Sub Update(ByVal myWOEngineerComment As WOEngineerComment, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "updateWOEngineerComment"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.AddWithValue("@EngineerCommentsId", myWOEngineerComment.EngineerCommentsId)
            myDbCommand.Parameters.AddWithValue("@WorkOrderId", myWOEngineerComment.WorkOrderId)
            myDbCommand.Parameters.Add(New SqlParameter("@ComponentId", SqlDbType.Int))
            myDbCommand.Parameters("@ComponentId").Value = myWOEngineerComment.ComponentId
            myDbCommand.Parameters.Add(New SqlParameter("@CommentDate", SqlDbType.DateTime))
            myDbCommand.Parameters("@CommentDate").Value = CDate(myWOEngineerComment.CommentDate)
            myDbCommand.Parameters.Add(New SqlParameter("@CommentRating", SqlDbType.SmallInt))
            myDbCommand.Parameters("@CommentRating").Value = myWOEngineerComment.CommentRating
            myDbCommand.Parameters.Add(New SqlParameter("@IssueCodeId", SqlDbType.Int))
            myDbCommand.Parameters("@IssueCodeId").Value = myWOEngineerComment.IssueCodeId
            myDbCommand.Parameters.AddWithValue("@Comments", myWOEngineerComment.Comments)
            myDbCommand.Parameters.AddWithValue("@StampUserName", myWOEngineerComment.StampUserName)
            myDbCommand.Parameters.Add(New SqlParameter("@ConcurrencyId", SqlDbType.Timestamp, 8))
            myDbCommand.Parameters("@ConcurrencyId").Value = DalUtility.ConvertStringToRowVersion(myWOEngineerComment.ConcurrencyId)
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

        Public Shared Sub Insert(ByVal myWOEngineerComment As WOEngineerComment)

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction

                        Try
                            Insert(myWOEngineerComment, myDbConnection, myDbTransaction)
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

        Public Shared Sub Insert(ByVal myWOEngineerComment As WOEngineerComment, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "addWOEngineerComment"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.AddWithValue("@EngineerCommentsId", myWOEngineerComment.EngineerCommentsId)
            myDbCommand.Parameters.AddWithValue("@WorkOrderId", myWOEngineerComment.WorkOrderId)
            myDbCommand.Parameters.Add(New SqlParameter("@ComponentId", SqlDbType.Int))
            myDbCommand.Parameters("@ComponentId").Value = myWOEngineerComment.ComponentId
            myDbCommand.Parameters.Add(New SqlParameter("@CommentDate", SqlDbType.DateTime))
            myDbCommand.Parameters("@CommentDate").Value = CDate(myWOEngineerComment.CommentDate)
            myDbCommand.Parameters.Add(New SqlParameter("@CommentRating", SqlDbType.SmallInt))
            myDbCommand.Parameters("@CommentRating").Value = myWOEngineerComment.CommentRating
            myDbCommand.Parameters.Add(New SqlParameter("@IssueCodeId", SqlDbType.Int))
            myDbCommand.Parameters("@IssueCodeId").Value = myWOEngineerComment.IssueCodeId
            myDbCommand.Parameters.AddWithValue("@Comments", myWOEngineerComment.Comments)
            myDbCommand.Parameters.AddWithValue("@NewConcurrencyId", 0)
            myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.AddWithValue("@StampUserName", myWOEngineerComment.StampUserName)
            myDbCommand.Parameters.Add(New SqlParameter("@NewUTCDate", SqlDbType.DateTime, 8))
            myDbCommand.Parameters("@NewUTCDate").Direction = ParameterDirection.Output

            Try
                myDbCommand.ExecuteNonQuery()

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try

        End Sub

        Public Shared Sub Delete(ByVal EngineerCommentsId As Integer, ByVal StampUserName As String)

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction

                        Try
                            Delete(EngineerCommentsId, StampUserName, myDbConnection, myDbTransaction)
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

        Public Shared Sub Delete(ByVal EngineerCommentsId As Integer, ByVal StampUsername As String, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "deleteWOEngineerComment"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.AddWithValue("@EngineerCommentsId", EngineerCommentsId)
            myDbCommand.Parameters.AddWithValue("@StampUserName", StampUsername)
            Try
                myDbCommand.ExecuteNonQuery()
            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try
        End Sub

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As WOEngineerComment

            Dim wOEngineerComment As WOEngineerComment = New WOEngineerComment()
            wOEngineerComment.EngineerCommentsId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("EngineerCommentsId"))
            wOEngineerComment.WorkOrderId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("WorkOrderId"))
            wOEngineerComment.ComponentId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ComponentId"))
            wOEngineerComment.Component = myDataRecord.GetString(myDataRecord.GetOrdinal("Component"))
            wOEngineerComment.CommentDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("CommentDate"))
            wOEngineerComment.CommentRating = myDataRecord.GetInt16(myDataRecord.GetOrdinal("CommentRating"))
            wOEngineerComment.IssueCodeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("IssueCodeId"))
            wOEngineerComment.IssueCode = myDataRecord.GetString(myDataRecord.GetOrdinal("IssueCode"))
            wOEngineerComment.Comments = myDataRecord.GetString(myDataRecord.GetOrdinal("Comments"))
            wOEngineerComment.EngineerCommentGUID = myDataRecord.GetGuid(myDataRecord.GetOrdinal("EngineerCommentGUID"))
            wOEngineerComment.IsDeleted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsDeleted"))
            wOEngineerComment.UtcDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateCreated"))
            wOEngineerComment.UtcDateModified = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateModified"))
            wOEngineerComment.StampUserName = myDataRecord.GetString(myDataRecord.GetOrdinal("StampUserName"))
            wOEngineerComment.ConcurrencyId = DalUtility.ConvertRowVersionToString(myDataRecord("ConcurrencyId"))
            Return wOEngineerComment

        End Function

    End Class

End Namespace
