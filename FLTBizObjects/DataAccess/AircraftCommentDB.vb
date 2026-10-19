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

    Public Class AircraftCommentDB

        Public Shared Function GetItem(ByVal AircraftCommentId As Integer) As AircraftComment

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "fetchAircraftComment"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@AircraftCommentId", AircraftCommentId)
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

        Public Shared Function GetList(ByVal AircraftId As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As AircraftCommentList

            Dim tempList As New AircraftCommentList

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listAircraftComments"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@AircraftId", AircraftId)
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

        Public Shared Sub Update(ByVal myAircraftComment As AircraftComment)

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            Update(myAircraftComment, myDbConnection, myDbTransaction)
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

        Public Shared Sub Update(ByVal myAircraftComment As AircraftComment, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "updateAircraftComment"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.Add(New SqlParameter("@AircraftCommentId", SqlDbType.Int))
            myDbCommand.Parameters("@AircraftCommentId").Value = myAircraftComment.AircraftCommentId
            myDbCommand.Parameters.Add(New SqlParameter("@AircraftId", SqlDbType.Int))
            myDbCommand.Parameters("@AircraftId").Value = myAircraftComment.AircraftId
            myDbCommand.Parameters.Add(New SqlParameter("@EngineerId", SqlDbType.Int))
            myDbCommand.Parameters("@EngineerId").Value = myAircraftComment.EngineerId
            myDbCommand.Parameters.Add(New SqlParameter("@ComponentId", SqlDbType.Int))
            myDbCommand.Parameters("@ComponentId").Value = myAircraftComment.ComponentId
            myDbCommand.Parameters.Add(New SqlParameter("@CommentDate", SqlDbType.DateTime))
            myDbCommand.Parameters("@CommentDate").Value = CDate(myAircraftComment.CommentDate)
            myDbCommand.Parameters.Add(New SqlParameter("@CommentRating", SqlDbType.SmallInt))
            myDbCommand.Parameters("@CommentRating").Value = myAircraftComment.CommentRating
            myDbCommand.Parameters.Add(New SqlParameter("@IssueCodeId", SqlDbType.Int))
            myDbCommand.Parameters("@IssueCodeId").Value = myAircraftComment.IssueCodeId
            myDbCommand.Parameters.Add(New SqlParameter("@Comments", SqlDbType.NVarChar, 2048))
            myDbCommand.Parameters("@comments").Value = myAircraftComment.Comments
            myDbCommand.Parameters.Add(New SqlParameter("@StampUserName", SqlDbType.NVarChar, 20))
            myDbCommand.Parameters("@StampUserName").Value = myAircraftComment.StampUserName
            myDbCommand.Parameters.Add(New SqlParameter("@ConcurrencyId", SqlDbType.Timestamp, 8))
            myDbCommand.Parameters("@ConcurrencyId").Value = DalUtility.ConvertStringToRowVersion(myAircraftComment.ConcurrencyId)
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

        Public Shared Function Insert(ByVal myAircraftComment As AircraftComment) As Integer

            Dim newId As Integer = 0

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            newId = Insert(myAircraftComment, myDbConnection, myDbTransaction)
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

            Return newId

        End Function

        Public Shared Function Insert(ByVal myAircraftComment As AircraftComment, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction) As Integer

            Dim newID As Integer = 0

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "addAircraftComment"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.Add(New SqlParameter("@AircraftId", SqlDbType.Int))
            myDbCommand.Parameters("@AircraftId").Value = myAircraftComment.AircraftId
            myDbCommand.Parameters.Add(New SqlParameter("@EngineerId", SqlDbType.Int))
            myDbCommand.Parameters("@EngineerId").Value = myAircraftComment.EngineerId
            myDbCommand.Parameters.Add(New SqlParameter("@ComponentId", SqlDbType.Int))
            myDbCommand.Parameters("@ComponentId").Value = myAircraftComment.ComponentId
            myDbCommand.Parameters.Add(New SqlParameter("@CommentDate", SqlDbType.DateTime))
            myDbCommand.Parameters("@CommentDate").Value = CDate(myAircraftComment.CommentDate)
            myDbCommand.Parameters.Add(New SqlParameter("@CommentRating", SqlDbType.SmallInt))
            myDbCommand.Parameters("@CommentRating").Value = myAircraftComment.CommentRating
            myDbCommand.Parameters.Add(New SqlParameter("@IssueCodeId", SqlDbType.Int))
            myDbCommand.Parameters("@IssueCodeId").Value = myAircraftComment.IssueCodeId
            myDbCommand.Parameters.Add(New SqlParameter("@Comments", SqlDbType.NVarChar, 2048))
            myDbCommand.Parameters("@comments").Value = myAircraftComment.Comments
            myDbCommand.Parameters.AddWithValue("@AircraftCommentId", 0)
            myDbCommand.Parameters("@AircraftCommentId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.AddWithValue("@NewConcurrencyId", 0)
            myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.AddWithValue("@StampUserName", myAircraftComment.StampUserName)
            myDbCommand.Parameters.Add(New SqlParameter("@NewUTCDate", SqlDbType.DateTime, 8))
            myDbCommand.Parameters("@NewUTCDate").Direction = ParameterDirection.Output

            Try
                myDbCommand.ExecuteNonQuery()

                newID = myDbCommand.Parameters("@AircraftCommentId").Value

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try

            Return newID

        End Function

        Public Shared Sub Delete(ByVal AircraftCommentId As Integer, ByVal StampUserName As String)

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            Delete(AircraftCommentId, StampUserName, myDbConnection, myDbTransaction)
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

        Public Shared Sub Delete(ByVal AircraftCommentId As Integer, ByVal StampUserName As String, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)
            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "deleteAircraftComment"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.AddWithValue("@AircraftCommentId", AircraftCommentId)
            myDbCommand.Parameters.AddWithValue("@StampUserName", StampUserName)
            Try
                myDbCommand.ExecuteNonQuery()
            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try
        End Sub

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As AircraftComment

            Dim AircraftComment As AircraftComment = New AircraftComment()
            AircraftComment.AircraftCommentId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("AircraftCommentId"))
            AircraftComment.AircraftId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("AircraftId"))
            AircraftComment.EngineerId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("EngineerId"))
            AircraftComment.EngineerName = myDataRecord.GetString(myDataRecord.GetOrdinal("EngineerName"))
            AircraftComment.ComponentId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ComponentId"))
            AircraftComment.Component = myDataRecord.GetString(myDataRecord.GetOrdinal("Component"))
            AircraftComment.CommentDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("CommentDate"))
            AircraftComment.CommentRating = myDataRecord.GetInt16(myDataRecord.GetOrdinal("CommentRating"))
            AircraftComment.IssueCodeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("IssueCodeId"))
            AircraftComment.IssueCode = myDataRecord.GetString(myDataRecord.GetOrdinal("IssueCode"))
            AircraftComment.Comments = myDataRecord.GetString(myDataRecord.GetOrdinal("Comments"))
            AircraftComment.IsDeleted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsDeleted"))
            AircraftComment.UtcDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateCreated"))
            AircraftComment.UtcDateModified = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateModified"))
            AircraftComment.StampUserName = myDataRecord.GetString(myDataRecord.GetOrdinal("StampUserName"))
            AircraftComment.ConcurrencyId = DalUtility.ConvertRowVersionToString(myDataRecord("ConcurrencyId"))
            Return AircraftComment

        End Function

    End Class

End Namespace
