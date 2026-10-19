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

	Public Class FlightCommentDB
		
        Public Shared Function GetItem(ByVal FlightCommentId As Integer) As FlightComment

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "fetchFlightComment"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@FlightCommentId", FlightCommentId)
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
		
        Public Shared Function GetList(ByVal FlightId As Integer, ByVal ComponentId As Integer, ByVal IsActiveFilter As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As FlightCommentList

            Dim tempList As New FlightCommentList

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction

                        Try
                            tempList = GetList(FlightId, ComponentId, IsActiveFilter, IsDeletedFilter, RowStart, RowCount, TotalRows, myDbConnection, myDbTransaction)
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

        Public Shared Function GetList(ByVal FlightId As Integer, ByVal ComponentId As Integer, ByVal IsActiveFilter As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction) As FlightCommentList

            Dim tempList As New FlightCommentList

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listFlightComments"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.AddWithValue("@FlightId", FlightId)
            myDbCommand.Parameters.AddWithValue("@ComponentId", ComponentId)
            myDbCommand.Parameters.AddWithValue("@IsActiveFilter", IsActiveFilter)
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
                    Logger.Write(myException, myDbCommand.CommandText & DisplayParameterList(myDbCommand.Parameters))
                    Throw New ApplicationException("Application encountered a data error.")
                Finally
                    CType(myReader, IDisposable).Dispose()
                End Try

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText & DisplayParameterList(myDbCommand.Parameters))
                Throw New ApplicationException("Application encountered a data error.")
            End Try

            Return tempList

        End Function

        Public Shared Sub Update(ByVal myFlightComment As FlightComment)

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.Open()
                    myDbConnection.ConnectionString = AppConfiguration.connectionString

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            Update(myFlightComment, myDbConnection, myDbTransaction)
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

        Public Shared Sub Update(ByVal myFlightComment As FlightComment, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "updateFlightComment"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.Add(New SqlParameter("@FlightCommentId", SqlDbType.Int))
            myDbCommand.Parameters("@FlightCommentId").Value = myFlightComment.FlightCommentId
            myDbCommand.Parameters.Add(New SqlParameter("@FlightId", SqlDbType.Int))
            myDbCommand.Parameters("@FlightId").Value = myFlightComment.FlightId
            myDbCommand.Parameters.Add(New SqlParameter("@ComponentId", SqlDbType.Int))
            myDbCommand.Parameters("@ComponentId").Value = myFlightComment.ComponentId
            myDbCommand.Parameters.Add(New SqlParameter("@CommentTime", SqlDbType.VarChar, 8))
            myDbCommand.Parameters("@CommentTime").Value = myFlightComment.CommentTime
            myDbCommand.Parameters.Add(New SqlParameter("@CommentDateTime_UTC", SqlDbType.DateTime))
            myDbCommand.Parameters("@CommentDateTime_UTC").Value = myFlightComment.CommentDateTime_UTC
            myDbCommand.Parameters.Add(New SqlParameter("@CommentDateTime_Local", SqlDbType.DateTime))
            myDbCommand.Parameters("@CommentDateTime_Local").Value = myFlightComment.CommentDateTime_Local
            myDbCommand.Parameters.Add(New SqlParameter("@IssueCodeId", SqlDbType.Int))
            myDbCommand.Parameters("@IssueCodeId").Value = myFlightComment.IssueCodeId
            myDbCommand.Parameters.Add(New SqlParameter("@PilotComment", SqlDbType.NVarChar, 2048))
            myDbCommand.Parameters("@PilotComment").Value = myFlightComment.PilotComment
            myDbCommand.Parameters.Add(New SqlParameter("@CommentRating", SqlDbType.SmallInt))
            myDbCommand.Parameters("@CommentRating").Value = myFlightComment.CommentRating
            myDbCommand.Parameters.Add(New SqlParameter("@IsActive", SqlDbType.Bit))
            myDbCommand.Parameters("@IsActive").Value = myFlightComment.IsActive
            myDbCommand.Parameters.Add(New SqlParameter("@StampUserName", SqlDbType.NVarChar, 20))
            myDbCommand.Parameters("@StampUserName").Value = myFlightComment.StampUserName
            myDbCommand.Parameters.Add(New SqlParameter("@ConcurrencyId", SqlDbType.Timestamp, 8))
            myDbCommand.Parameters("@ConcurrencyId").Value = DalUtility.ConvertStringToRowVersion(myFlightComment.ConcurrencyId)
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

        Public Shared Function Insert(ByVal myFlightComment As FlightComment) As Integer

            Dim newFlightCommentId As Integer = 0

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            newFlightCommentId = Insert(myFlightComment, myDbConnection, myDbTransaction)
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

            Return newFlightCommentId

        End Function

        Public Shared Function Insert(ByVal myFlightComment As FlightComment, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction) As Integer

            Dim newFlightCommentId As Integer = 0

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "addFlightComment"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.Add(New SqlParameter("@FlightId", SqlDbType.Int))
            myDbCommand.Parameters("@FlightId").Value = myFlightComment.FlightId
            myDbCommand.Parameters.Add(New SqlParameter("@ComponentId", SqlDbType.Int))
            myDbCommand.Parameters("@ComponentId").Value = myFlightComment.ComponentId
            myDbCommand.Parameters.Add(New SqlParameter("@CommentTime", SqlDbType.VarChar, 8))
            myDbCommand.Parameters("@CommentTime").Value = myFlightComment.CommentTime
            myDbCommand.Parameters.Add(New SqlParameter("@CommentDateTime_UTC", SqlDbType.DateTime))
            myDbCommand.Parameters("@CommentDateTime_UTC").Value = myFlightComment.CommentDateTime_UTC
            myDbCommand.Parameters.Add(New SqlParameter("@CommentDateTime_Local", SqlDbType.DateTime))
            myDbCommand.Parameters("@CommentDateTime_Local").Value = myFlightComment.CommentDateTime_Local
            myDbCommand.Parameters.Add(New SqlParameter("@IssueCodeId", SqlDbType.Int))
            myDbCommand.Parameters("@IssueCodeId").Value = myFlightComment.IssueCodeId
            myDbCommand.Parameters.Add(New SqlParameter("@PilotComment", SqlDbType.NVarChar, 2048))
            myDbCommand.Parameters("@PilotComment").Value = myFlightComment.PilotComment
            myDbCommand.Parameters.Add(New SqlParameter("@CommentRating", SqlDbType.SmallInt))
            myDbCommand.Parameters("@CommentRating").Value = myFlightComment.CommentRating
            myDbCommand.Parameters.Add(New SqlParameter("@StampUserName", SqlDbType.NVarChar, 20))
            myDbCommand.Parameters("@StampUserName").Value = myFlightComment.StampUserName
            myDbCommand.Parameters.Add(New SqlParameter("@FlightCommentId", SqlDbType.Int))
            myDbCommand.Parameters("@FlightCommentId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.Add(New SqlParameter("@NewConcurrencyId", SqlDbType.Timestamp))
            myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.Add(New SqlParameter("@NewUTCDate", SqlDbType.DateTime, 8))
            myDbCommand.Parameters("@NewUTCDate").Direction = ParameterDirection.Output

            Try
                myDbCommand.ExecuteNonQuery()
                newFlightCommentId = myDbCommand.Parameters("@FlightCommentId").Value

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try

            Return newFlightCommentId

        End Function

        Public Shared Sub Delete(ByVal FlightCommentId As Integer, ByVal StampUserName As String)

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction

                        Try
                            Delete(FlightCommentId, StampUserName, myDbConnection, myDbTransaction)
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

        Public Shared Sub Delete(ByVal FlightCommentId As Integer, ByVal StampUserName As String, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "deleteFlightComment"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.AddWithValue("@FlightCommentId", FlightCommentId)
            myDbCommand.Parameters.AddWithValue("@StampUserName", StampUserName)
            Try
                myDbCommand.ExecuteNonQuery()
            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try
        End Sub

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As FlightComment

            Dim flightComment As FlightComment = New FlightComment()
            flightComment.FlightCommentId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlightCommentId"))
            flightComment.FlightId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlightId"))
            flightComment.ComponentId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ComponentId"))
            flightComment.ComponentName = myDataRecord.GetString(myDataRecord.GetOrdinal("ComponentName"))
            flightComment.CommentTime = myDataRecord.GetString(myDataRecord.GetOrdinal("CommentTime"))
            If Not (myDataRecord.IsDBNull(myDataRecord.GetOrdinal("CommentDateTime_UTC"))) Then
                flightComment.CommentDateTime_UTC = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("CommentDateTime_UTC"))
            End If
            If Not (myDataRecord.IsDBNull(myDataRecord.GetOrdinal("CommentDateTime_Local"))) Then
                flightComment.CommentDateTime_Local = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("CommentDateTime_Local"))
            End If
            flightComment.IssueCodeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("IssueCodeId"))
            flightComment.IssueCode = myDataRecord.GetString(myDataRecord.GetOrdinal("IssueCode"))
            flightComment.PilotComment = myDataRecord.GetString(myDataRecord.GetOrdinal("PilotComment"))
            flightComment.CommentRating = myDataRecord.GetInt16(myDataRecord.GetOrdinal("CommentRating"))
            flightComment.FlightCommentGUID = myDataRecord.GetGuid(myDataRecord.GetOrdinal("FlightCommentGUID"))
            flightComment.IsActive = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsActive"))
            flightComment.IsDeleted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsDeleted"))
            flightComment.UtcDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateCreated"))
            flightComment.UtcDateModified = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateModified"))
            flightComment.StampUserName = myDataRecord.GetString(myDataRecord.GetOrdinal("StampUserName"))
            flightComment.ConcurrencyId = DalUtility.ConvertRowVersionToString(myDataRecord("ConcurrencyId"))
            Return flightComment

        End Function

        Private Shared Function DisplayParameterList(ByVal ParameterList As SqlParameterCollection)

            Dim sb As New System.Text.StringBuilder()
            sb.Append("; Parameters: ")
            For Each thisParameter As SqlParameter In ParameterList
                sb.Append(thisParameter.ParameterName)
                sb.Append("=")
                sb.Append(thisParameter.Value)
                sb.Append(";")
            Next

            Return sb.ToString()

        End Function

    End Class

End Namespace
