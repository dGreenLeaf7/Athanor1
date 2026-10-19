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

    Public Class GenericCommentDB

        Public Shared Function GetList(ByVal AircraftId As Integer) As GenericCommentList

            Dim tempList As New GenericCommentList

            Using myDbConnection As SqlConnection = New SqlConnection()
                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            tempList = GetList(AircraftId, myDbConnection, myDbTransaction)
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

        Public Shared Function GetList(ByVal AircraftId As Integer, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction) As GenericCommentList

            Dim tempList As New GenericCommentList

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listGenericComments"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.AddWithValue("@AircraftId", AircraftId)

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

        Friend Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As GenericComment

            Dim GenericComment As GenericComment = New GenericComment()
            GenericComment.SourceTable = myDataRecord.GetString(myDataRecord.GetOrdinal("SourceTable"))
            GenericComment.SourceID = myDataRecord.GetInt32(myDataRecord.GetOrdinal("SourceId"))
            GenericComment.ParentId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ParentId"))
            GenericComment.AuthorName = myDataRecord.GetString(myDataRecord.GetOrdinal("AuthorName"))
            GenericComment.AuthorID = myDataRecord.GetInt32(myDataRecord.GetOrdinal("AuthorID"))
            GenericComment.ComponentName = myDataRecord.GetString(myDataRecord.GetOrdinal("ComponentName"))
            GenericComment.ComponentId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ComponentId"))
            If Not (myDataRecord.IsDBNull(myDataRecord.GetOrdinal("CommentDate"))) Then
                GenericComment.CommentDate = Format(myDataRecord.GetDateTime(myDataRecord.GetOrdinal("CommentDate")), "MM/dd/yyyy")
            End If
            GenericComment.CommentTime = myDataRecord.GetString(myDataRecord.GetOrdinal("CommentTime"))
            If Not (myDataRecord.IsDBNull(myDataRecord.GetOrdinal("CommentDateTime_UTC"))) Then
                GenericComment.CommentDateTime_UTC = Format(myDataRecord.GetDateTime(myDataRecord.GetOrdinal("CommentDateTime_UTC")), "MM/dd/yyyy HH:mm:ss")
            End If
            If Not (myDataRecord.IsDBNull(myDataRecord.GetOrdinal("CommentDateTime_Local"))) Then
                GenericComment.CommentDateTime_Local = Format(myDataRecord.GetDateTime(myDataRecord.GetOrdinal("CommentDateTime_Local")), "MM/dd/yyyy HH:mm:ss")
            End If
            GenericComment.CommentRating = myDataRecord.GetInt16(myDataRecord.GetOrdinal("CommentRating"))
            GenericComment.IssueCodeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("IssueCodeId"))
            GenericComment.IssueCode = myDataRecord.GetString(myDataRecord.GetOrdinal("IssueCode"))
            GenericComment.Comments = myDataRecord.GetString(myDataRecord.GetOrdinal("Comments"))
            GenericComment.CommentGUID = myDataRecord.GetGuid(myDataRecord.GetOrdinal("CommentGUID"))
            GenericComment.IsDeleted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsDeleted"))
            GenericComment.UtcDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UtcDateCreated"))
            GenericComment.UtcDateModified = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UtcDateModified"))
            GenericComment.StampUserName = myDataRecord.GetString(myDataRecord.GetOrdinal("StampUserName"))
            GenericComment.ConcurrencyId = DalUtility.ConvertRowVersionToString(myDataRecord("ConcurrencyId"))

            GenericComment.IsDirty = False

            Return GenericComment

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
