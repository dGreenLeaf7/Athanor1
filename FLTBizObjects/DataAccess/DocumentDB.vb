' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.IO
Imports Contoso.Utilities
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.BL

Namespace FLT.Dal

    Public Class DocumentDB

        Public Shared Function GetStream(ByVal DocumentId As Integer) As System.IO.MemoryStream

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "fetchDocument"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@DocumentId", DocumentId)

            Try
                myDbConnection.Open()

                Dim myReader As SqlDataReader = Nothing

                Try
                    myReader = myDbCommand.ExecuteReader(CommandBehavior.SingleRow)

                    If (Not myReader.HasRows) Then Return Nothing

                    myReader.Read()

                    Return GetMemoryStream(myReader)

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

        Public Shared Sub PutStream(ByVal DocumentId As Integer, ByVal DocumentContent As System.IO.Stream)

            Using myDbConnection As SqlConnection = New SqlConnection()
                myDbConnection.ConnectionString = AppConfiguration.connectionString

                Try
                    myDbConnection.Open()
                    InsertStub(myDbConnection, DocumentId)

                    Dim bufferSize As Integer = CInt(ConfigurationManager.AppSettings.Get("FileBufferSize"))
                    Dim inputBuffer(bufferSize - 1) As Byte
                    Dim index As Integer = 0
                    Dim retVal As Integer = bufferSize

                    Using bReader As New System.IO.BinaryReader(DocumentContent)

                        Do While (True)
                            retVal = bReader.Read(inputBuffer, 0, inputBuffer.Length)
                            If retVal = 0 Then
                                Exit Do
                            End If
                            If retVal < bufferSize Then
                                bufferSize = retVal
                            End If
                            AppendChunk(myDbConnection, DocumentId, inputBuffer, index, bufferSize)
                            index += bufferSize
                        Loop

                        myDbConnection.Close()

                    End Using

                Catch myException As Exception
                    Throw New ApplicationException(myException.Message)
                End Try

            End Using

        End Sub

        Private Shared Sub InsertStub(ByVal myDbconnection As SqlConnection, ByVal documentId As Integer)
            Dim myDbCommand As New SqlCommand
            Try
                myDbCommand.CommandType = CommandType.StoredProcedure
                myDbCommand.CommandText = "addDocument"
                myDbCommand.Connection = myDbconnection
                myDbCommand.Parameters.AddWithValue("@DocumentId", documentId)
                myDbCommand.ExecuteNonQuery()
            Catch ex As Exception
                Logger.Write(ex, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try
        End Sub

        Private Shared Sub AppendChunk(ByVal myDbConnection As SqlConnection, ByVal DocumentId As Integer, ByVal chunk As Byte(), ByVal offset As Integer, ByVal chunkLength As Integer)
            Dim myDbCommand As New SqlCommand
            Try
                myDbCommand.Connection = myDbConnection
                myDbCommand.CommandType = CommandType.StoredProcedure
                myDbCommand.CommandText = "appendDocumentChunk"
                myDbCommand.Parameters.AddWithValue("@DocumentId", DocumentId)
                myDbCommand.Parameters.AddWithValue("@Chunk", chunk)
                myDbCommand.Parameters.AddWithValue("@Offset", offset)
                myDbCommand.Parameters.AddWithValue("@ChunkLength", chunkLength)
                myDbCommand.ExecuteNonQuery()
            Catch ex As Exception
                Logger.Write(ex, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try
        End Sub

        Private Shared Function GetMemoryStream(ByVal myDataRecord As IDataRecord) As System.IO.MemoryStream

            Dim index As Integer = 0
            Dim bufferSize As Integer = CInt(ConfigurationManager.AppSettings.Get("FileBufferSize"))
            Dim outBuffer(bufferSize - 1) As Byte
            Dim retVal As Integer

            Dim sqlRead As Integer = 0

            Dim documentSize As Integer = myDataRecord.GetInt32(myDataRecord.GetOrdinal("DocumentSize"))
            Dim documentContent(documentSize - 1) As Byte

            Try

                Do While (index < documentSize)
                    retVal = myDataRecord.GetBytes(myDataRecord.GetOrdinal("DocumentContent"), index, outBuffer, 0, bufferSize)
                    If retVal = 0 Then
                        Exit Do
                    End If
                    If retVal < bufferSize Then
                        bufferSize = retVal
                    End If
                    For i As Integer = 0 To (bufferSize - 1)
                        If ((index + i) > (documentSize - 1)) Then
                            Exit For
                        End If
                        documentContent(index + i) = outBuffer(i)
                    Next
                    index += bufferSize
                Loop

                Return New System.IO.MemoryStream(documentContent)

            Catch ex As Exception
                Logger.Write(ex)
                Throw New ApplicationException("An application error occurred.")
            End Try

        End Function

    End Class

End Namespace