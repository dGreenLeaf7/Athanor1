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

    Public Class DocumentInfoDB

        Public Shared Function GetItem(ByVal DocumentId As Integer) As DocumentInfo

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "fetchDocumentInfo"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@DocumentId", DocumentId)
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

        Public Shared Function GetList(ByVal ProgId As Integer, ByVal DocumentTypeId As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As DocumentInfoList

            Dim tempList As DocumentInfoList = Nothing

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listDocumentInfos"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@ProgId", ProgId)
            myDbCommand.Parameters.AddWithValue("@DocumentTypeId", DocumentTypeId)
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
                        tempList = New DocumentInfoList
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

        Public Shared Sub Update(ByVal myDocumentInfo As DocumentInfo)

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "updateDocumentInfo"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@DocumentId", myDocumentInfo.DocumentId)
            myDbCommand.Parameters.AddWithValue("@ProgId", myDocumentInfo.ProgId)
            myDbCommand.Parameters.AddWithValue("@DocumentTypeId", myDocumentInfo.DocumentTypeId)
            myDbCommand.Parameters.AddWithValue("@DocumentName", myDocumentInfo.DocumentName)
            myDbCommand.Parameters.AddWithValue("@DocumentDescription", myDocumentInfo.DocumentDescription)
            myDbCommand.Parameters.AddWithValue("@DocumentSize", myDocumentInfo.DocumentSize)
            myDbCommand.Parameters.AddWithValue("@IsDeleted", myDocumentInfo.IsDeleted)
            myDbCommand.Parameters.AddWithValue("@StampUserName", myDocumentInfo.StampUserName)
            myDbCommand.Parameters.Add(New SqlParameter("@ConcurrencyId", SqlDbType.Timestamp, 8))
            myDbCommand.Parameters("@ConcurrencyId").Value = DalUtility.ConvertStringToRowVersion(myDocumentInfo.ConcurrencyId)
            myDbCommand.Parameters.Add(New SqlParameter("@NewConcurrencyId", SqlDbType.Timestamp, 8))
            myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.Add(New SqlParameter("@NewUTCDate", SqlDbType.DateTime, 8))
            myDbCommand.Parameters("@NewUTCDate").Direction = ParameterDirection.Output
            Try
                myDbConnection.Open()
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

        Public Shared Function Insert(ByVal myDocumentInfo As DocumentInfo) As Integer

            Dim newId As Long = 0

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "addDocumentInfo"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@ProgId", myDocumentInfo.ProgId)
            myDbCommand.Parameters.AddWithValue("@DocumentTypeId", myDocumentInfo.DocumentTypeId)
            myDbCommand.Parameters.AddWithValue("@DocumentName", myDocumentInfo.DocumentName)
            myDbCommand.Parameters.AddWithValue("@DocumentDescription", myDocumentInfo.DocumentDescription)
            myDbCommand.Parameters.AddWithValue("@DocumentSize", myDocumentInfo.DocumentSize)
            myDbCommand.Parameters.Add(New SqlParameter("@DocumentId", SqlDbType.Int))
            myDbCommand.Parameters("@DocumentId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.AddWithValue("@NewConcurrencyId", 0)
            myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.AddWithValue("@StampUserName", myDocumentInfo.StampUserName)
            myDbCommand.Parameters.Add(New SqlParameter("@NewUTCDate", SqlDbType.DateTime, 8))
            myDbCommand.Parameters("@NewUTCDate").Direction = ParameterDirection.Output

            Try
                myDbConnection.Open()
                myDbCommand.ExecuteNonQuery()
                myDbConnection.Close()
                newId = CInt(myDbCommand.Parameters("@DocumentId").Value)

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            Finally
                CType(myDbConnection, IDisposable).Dispose()
            End Try

            Return newId

        End Function

        Public Shared Sub Delete(ByVal DocumentId As Integer, ByVal StampUserName As String)

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "deleteDocumentInfo"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@DocumentId", DocumentId)
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

        Public Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As DocumentInfo

            Dim documentInfo As DocumentInfo = New DocumentInfo()
            documentInfo.DocumentId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("DocumentId"))
            documentInfo.ProgId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ProgId"))
            documentInfo.DocumentTypeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("DocumentTypeId"))
            documentInfo.FileExtension = myDataRecord.GetString(myDataRecord.GetOrdinal("FileExtension"))
            documentInfo.MimeType = myDataRecord.GetString(myDataRecord.GetOrdinal("MimeType"))
            documentInfo.DocumentName = myDataRecord.GetString(myDataRecord.GetOrdinal("DocumentName"))
            documentInfo.DocumentDescription = myDataRecord.GetString(myDataRecord.GetOrdinal("DocumentDescription"))
            documentInfo.DocumentSize = myDataRecord.GetInt32(myDataRecord.GetOrdinal("DocumentSize"))
            documentInfo.IsDeleted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsDeleted"))
            documentInfo.UtcDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateCreated"))
            documentInfo.UtcDateModified = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateModified"))
            documentInfo.StampUserName = myDataRecord.GetString(myDataRecord.GetOrdinal("StampUserName"))
            documentInfo.ConcurrencyId = DalUtility.ConvertRowVersionToString(myDataRecord("ConcurrencyId"))
            Return documentInfo

        End Function

    End Class

End Namespace
