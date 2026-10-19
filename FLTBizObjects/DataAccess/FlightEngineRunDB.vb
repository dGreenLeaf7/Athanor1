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

    Public Class FlightEngineRunDB

        Public Shared Function GetItem(ByVal FlightEngineRunId As Integer) As FlightEngineRun

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "fetchFlightEngineRun"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@FlightEngineRunId", FlightEngineRunId)
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

        Public Shared Function GetList(ByVal FlightId As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As FlightEngineRunList

            Dim tempList As New FlightEngineRunList

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction

                        Try
                            tempList = GetList(FlightId, IsDeletedFilter, RowStart, RowCount, TotalRows, myDbConnection, myDbTransaction)
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

        Public Shared Function GetList(ByVal FlightId As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction) As FlightEngineRunList

            Dim tempList As New FlightEngineRunList

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listFlightEngineRuns"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.AddWithValue("@FlightId", FlightId)
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

        Public Shared Sub Update(ByVal myFlightEngineRun As FlightEngineRun)

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            Update(myFlightEngineRun, myDbConnection, myDbTransaction)
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

        Public Shared Sub Update(ByVal myFlightEngineRun As FlightEngineRun, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "updateFlightEngineRun"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.Add(New SqlParameter("@FlightEngineRunId", SqlDbType.Int))
            myDbCommand.Parameters("@FlightEngineRunId").Value = myFlightEngineRun.FlightEngineRunId
            myDbCommand.Parameters.Add(New SqlParameter("@FlightId", SqlDbType.Int))
            myDbCommand.Parameters("@FlightId").Value = myFlightEngineRun.FlightId
            myDbCommand.Parameters.Add(New SqlParameter("@RunStartTime", SqlDbType.VarChar, 8))
            myDbCommand.Parameters("@RunStartTime").Value = myFlightEngineRun.RunStartTime
            myDbCommand.Parameters.Add(New SqlParameter("@RunEndTime", SqlDbType.VarChar, 8))
            myDbCommand.Parameters("@RunEndTime").Value = myFlightEngineRun.RunEndTime
            myDbCommand.Parameters.Add(New SqlParameter("@EngineSpeed", SqlDbType.Int))
            myDbCommand.Parameters("@EngineSpeed").Value = CInt(myFlightEngineRun.EngineSpeed)
            myDbCommand.Parameters.Add(New SqlParameter("@RunStartDateTime_Local", SqlDbType.DateTime))
            myDbCommand.Parameters("@RunStartDateTime_Local").Value = CDate(myFlightEngineRun.RunStartDateTime_Local)
            myDbCommand.Parameters.Add(New SqlParameter("@RunStartDateTime_UTC", SqlDbType.DateTime))
            myDbCommand.Parameters("@RunStartDateTime_UTC").Value = CDate(myFlightEngineRun.RunStartDateTime_UTC)
            myDbCommand.Parameters.Add(New SqlParameter("@RunEndDateTime_Local", SqlDbType.DateTime))
            myDbCommand.Parameters("@RunEndDateTime_Local").Value = CDate(myFlightEngineRun.RunEndDateTime_Local)
            myDbCommand.Parameters.Add(New SqlParameter("@RunEndDateTime_UTC", SqlDbType.DateTime))
            myDbCommand.Parameters("@RunEndDateTime_UTC").Value = CDate(myFlightEngineRun.RunEndDateTime_UTC)
            myDbCommand.Parameters.Add(New SqlParameter("@StampUserName", SqlDbType.NVarChar, 20))
            myDbCommand.Parameters("@StampUserName").Value = myFlightEngineRun.StampUserName
            myDbCommand.Parameters.Add(New SqlParameter("@ConcurrencyId", SqlDbType.Timestamp, 8))
            myDbCommand.Parameters("@ConcurrencyId").Value = DalUtility.ConvertStringToRowVersion(myFlightEngineRun.ConcurrencyId)
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

        Public Shared Function Insert(ByVal myFlightEngineRun As FlightEngineRun) As Integer

            Dim newFlightEngineRunId As Integer = 0

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            newFlightEngineRunId = Insert(myFlightEngineRun, myDbConnection, myDbTransaction)
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

            Return newFlightEngineRunId

        End Function

        Public Shared Function Insert(ByVal myFlightEngineRun As FlightEngineRun, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)

            Dim newFlightEngineRunId As Integer = 0

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "addFlightEngineRun"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.Add(New SqlParameter("@FlightId", SqlDbType.Int))
            myDbCommand.Parameters("@FlightId").Value = myFlightEngineRun.FlightId
            myDbCommand.Parameters.Add(New SqlParameter("@RunStartTime", SqlDbType.VarChar, 8))
            myDbCommand.Parameters("@RunStartTime").Value = myFlightEngineRun.RunStartTime
            myDbCommand.Parameters.Add(New SqlParameter("@RunEndTime", SqlDbType.VarChar, 8))
            myDbCommand.Parameters("@RunEndTime").Value = myFlightEngineRun.RunEndTime
            myDbCommand.Parameters.Add(New SqlParameter("@EngineSpeed", SqlDbType.Int))
            myDbCommand.Parameters("@EngineSpeed").Value = CInt(myFlightEngineRun.EngineSpeed)
            myDbCommand.Parameters.Add(New SqlParameter("@RunStartDateTime_Local", SqlDbType.DateTime))
            myDbCommand.Parameters("@RunStartDateTime_Local").Value = myFlightEngineRun.RunStartDateTime_Local
            myDbCommand.Parameters.Add(New SqlParameter("@RunStartDateTime_UTC", SqlDbType.DateTime))
            myDbCommand.Parameters("@RunStartDateTime_UTC").Value = myFlightEngineRun.RunStartDateTime_UTC
            myDbCommand.Parameters.Add(New SqlParameter("@RunEndDateTime_Local", SqlDbType.DateTime))
            myDbCommand.Parameters("@RunEndDateTime_Local").Value = myFlightEngineRun.RunEndDateTime_Local
            myDbCommand.Parameters.Add(New SqlParameter("@RunEndDateTime_UTC", SqlDbType.DateTime))
            myDbCommand.Parameters("@RunEndDateTime_UTC").Value = myFlightEngineRun.RunEndDateTime_UTC
            myDbCommand.Parameters.Add(New SqlParameter("@StampUserName", SqlDbType.NVarChar, 20))
            myDbCommand.Parameters("@StampUserName").Value = myFlightEngineRun.StampUserName
            myDbCommand.Parameters.AddWithValue("@FlightEngineRunId", 0)
            myDbCommand.Parameters("@FlightEngineRunId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.Add(New SqlParameter("@NewConcurrencyId", SqlDbType.Timestamp))
            myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.Add(New SqlParameter("@NewUTCDate", SqlDbType.DateTime, 8))
            myDbCommand.Parameters("@NewUTCDate").Direction = ParameterDirection.Output

            Try
                myDbCommand.ExecuteNonQuery()
                newFlightEngineRunId = CLng(myDbCommand.Parameters("@FlightEngineRunId").Value)

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try

            Return newFlightEngineRunId

        End Function

        Public Shared Sub Delete(ByVal FlightEngineRunId As Integer, ByVal StampUserName As String)

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            Delete(FlightEngineRunId, StampUserName, myDbConnection, myDbTransaction)
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

        Public Shared Sub Delete(ByVal FlightEngineRunId As Integer, ByVal StampUserName As String, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "deleteFlightEngineRun"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.AddWithValue("@FlightEngineRunId", FlightEngineRunId)
            myDbCommand.Parameters.AddWithValue("@StampUserName", StampUserName)
            Try
                myDbCommand.ExecuteNonQuery()
            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try
        End Sub

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As FlightEngineRun

            Dim FlightEngineRun As FlightEngineRun = New FlightEngineRun()
            FlightEngineRun.FlightEngineRunId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlightEngineRunId"))
            FlightEngineRun.FlightId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlightId"))
            FlightEngineRun.RunStartTime = myDataRecord.GetString(myDataRecord.GetOrdinal("RunStartTime"))
            FlightEngineRun.RunEndTime = myDataRecord.GetString(myDataRecord.GetOrdinal("RunEndTime"))
            FlightEngineRun.EngineSpeed = myDataRecord.GetInt32(myDataRecord.GetOrdinal("EngineSpeed"))
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("RunStartDateTime_Local")) Then
                FlightEngineRun.RunStartDateTime_Local = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("RunStartDateTime_Local"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("RunStartDateTime_UTC")) Then
                FlightEngineRun.RunStartDateTime_UTC = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("RunStartDateTime_UTC"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("RunEndDateTime_Local")) Then
                FlightEngineRun.RunEndDateTime_Local = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("RunEndDateTime_Local"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("RunEndDateTime_UTC")) Then
                FlightEngineRun.RunEndDateTime_UTC = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("RunEndDateTime_UTC"))
            End If
            FlightEngineRun.FlightEngineRunGUID = myDataRecord.GetGuid(myDataRecord.GetOrdinal("FlightEngineRunGUID"))
            FlightEngineRun.IsDeleted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsDeleted"))
            FlightEngineRun.UtcDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateCreated"))
            FlightEngineRun.UtcDateModified = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateModified"))
            FlightEngineRun.StampUserName = myDataRecord.GetString(myDataRecord.GetOrdinal("StampUserName"))
            FlightEngineRun.ConcurrencyId = DalUtility.ConvertRowVersionToString(myDataRecord("ConcurrencyId"))
            Return FlightEngineRun

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
