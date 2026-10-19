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

	Public Class LiquidFillDB
		
        Public Shared Function GetItem(ByVal LiquidFillId As Integer) As LiquidFill

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "fetchLiquidFill"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@LiquidFillId", LiquidFillId)
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
		
        Public Shared Function GetList(ByVal FlightId As Integer, ByVal LiquidGroupId As Integer, ByVal LiquidTypeId As Integer, ByVal LiquidUnitId As Integer, ByVal IsActiveFilter As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As LiquidFillList

            Dim templist As New LiquidFillList

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()
                        Try
                            templist = GetList(FlightId, LiquidGroupId, LiquidTypeId, LiquidUnitId, IsActiveFilter, IsDeletedFilter, RowStart, RowCount, TotalRows)
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
                Catch
                Finally
                    If myDbConnection.Region = ConnectionState.Open Then
                        myDbConnection.Close()
                    End If
                End Try

            End Using

            Return templist

        End Function

        Public Shared Function GetList(ByVal FlightId As Integer, ByVal LiquidGroupId As Integer, ByVal LiquidTypeId As Integer, ByVal LiquidUnitId As Integer, ByVal IsActiveFilter As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction) As LiquidFillList

            Dim tempList As New LiquidFillList

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listLiquidFills"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.Add(New SqlParameter("@FlightId", SqlDbType.Int))
            myDbCommand.Parameters("@FlightId").Value = FlightId
            myDbCommand.Parameters.Add(New SqlParameter("@LiquidGroupId", SqlDbType.Int))
            myDbCommand.Parameters("@LiquidGroupId").Value = LiquidGroupId
            myDbCommand.Parameters.AddWithValue("@LiquidTypeId", LiquidTypeId)
            myDbCommand.Parameters.AddWithValue("@LiquidUnitId", LiquidUnitId)
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

        Public Shared Sub Update(ByVal myLiquidFill As LiquidFill)

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try

                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            Update(myLiquidFill, myDbConnection, myDbTransaction)
                            myDbTransaction.Commit()

                        Catch ex As ApplicationException
                            myDbTransaction.Rollback()
                            Throw New ApplicationException("Application encountered a data error.")

                        Catch ex As Exception
                            Logger.Write(ex.Message)
                            myDbTransaction.Rollback()
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

        Public Shared Sub Update(ByVal myLiquidFill As LiquidFill, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "updateLiquidFill"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.Add(New SqlParameter("@LiquidFillId", SqlDbType.Int))
            myDbCommand.Parameters("@LiquidFillId").Value = myLiquidFill.LiquidFillId
            myDbCommand.Parameters.Add(New SqlParameter("@FlightId", SqlDbType.Int))
            myDbCommand.Parameters("@FlightId").Value = myLiquidFill.FlightId
            myDbCommand.Parameters.Add(New SqlParameter("@LiquidGroupId", SqlDbType.Int))
            myDbCommand.Parameters("@LiquidGroupId").Value = myLiquidFill.LiquidGroupId
            myDbCommand.Parameters.Add(New SqlParameter("@LiquidTypeId", SqlDbType.Int))
            myDbCommand.Parameters("@LiquidTypeId").Value = myLiquidFill.LiquidTypeId
            myDbCommand.Parameters.Add(New SqlParameter("@LiquidUnitId", SqlDbType.Int))
            myDbCommand.Parameters("@LiquidUnitId").Value = myLiquidFill.LiquidUnitId
            myDbCommand.Parameters.Add(New SqlParameter("@LiquidAmt", SqlDbType.Decimal))
            myDbCommand.Parameters("@LiquidAmt").Value = CDec(myLiquidFill.LiquidAmt)
            myDbCommand.Parameters.Add(New SqlParameter("@HobbsReading", SqlDbType.Decimal))
            myDbCommand.Parameters("@HobbsReading").Value = CDec(myLiquidFill.HobbsReading)
            myDbCommand.Parameters.Add(New SqlParameter("@AirframeHoursReading", SqlDbType.Decimal))
            myDbCommand.Parameters("@AirframeHoursReading").Value = CDec(myLiquidFill.AirframeHoursReading)
            myDbCommand.Parameters.Add(New SqlParameter("@LiquidFillLocation", SqlDbType.NVarChar, 255))
            myDbCommand.Parameters("@LiquidFillLocation").Value = myLiquidFill.LiquidFillLocation
            myDbCommand.Parameters.Add(New SqlParameter("@LiquidFillApprover", SqlDbType.NVarChar, 30))
            myDbCommand.Parameters("@LiquidFillApprover").Value = myLiquidFill.LiquidFillApprover
            myDbCommand.Parameters.Add(New SqlParameter("@IsActive", SqlDbType.Int))
            myDbCommand.Parameters("@IsActive").Value = myLiquidFill.IsActive
            myDbCommand.Parameters.Add(New SqlParameter("@StampUserName", SqlDbType.NVarChar, 20))
            myDbCommand.Parameters("@StampUserName").Value = myLiquidFill.StampUserName
            myDbCommand.Parameters.Add(New SqlParameter("@ConcurrencyId", SqlDbType.Timestamp, 8))
            myDbCommand.Parameters("@ConcurrencyId").Value = DalUtility.ConvertStringToRowVersion(myLiquidFill.ConcurrencyId)
            myDbCommand.Parameters.Add(New SqlParameter("@NewConcurrencyId", SqlDbType.Timestamp, 8))
            myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.Add(New SqlParameter("@NewUTCDate", SqlDbType.DateTime, 8))
            myDbCommand.Parameters("@NewUTCDate").Direction = ParameterDirection.Output
            Try
                myDbCommand.ExecuteNonQuery()

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try

        End Sub

        Public Shared Function Insert(ByVal myLiquidFill As LiquidFill) As Integer

            Dim newLiquidFillId As Integer = 0

            Using myDbConnection As New SqlConnection()

                Try

                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            newLiquidFillId = Insert(myLiquidFill, myDbConnection, myDbTransaction)
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

            Return newLiquidFillId

        End Function

        Public Shared Function Insert(ByVal myLiquidFill As LiquidFill, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction) As Integer

            Dim newLiquidFillId As Integer = 0

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "addLiquidFill"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.Add(New SqlParameter("@FlightId", SqlDbType.Int))
            myDbCommand.Parameters("@FlightId").Value = myLiquidFill.FlightId
            myDbCommand.Parameters.Add(New SqlParameter("@LiquidGroupId", SqlDbType.Int))
            myDbCommand.Parameters("@LiquidGroupId").Value = myLiquidFill.LiquidGroupId
            myDbCommand.Parameters.Add(New SqlParameter("@LiquidTypeId", SqlDbType.Int))
            myDbCommand.Parameters("@LiquidTypeId").Value = myLiquidFill.LiquidTypeId
            myDbCommand.Parameters.Add(New SqlParameter("@LiquidUnitId", SqlDbType.Int))
            myDbCommand.Parameters("@LiquidUnitId").Value = myLiquidFill.LiquidUnitId
            myDbCommand.Parameters.Add(New SqlParameter("@LiquidAmt", SqlDbType.Decimal))
            myDbCommand.Parameters("@LiquidAmt").Value = CDec(myLiquidFill.LiquidAmt)
            myDbCommand.Parameters.Add(New SqlParameter("@HobbsReading", SqlDbType.Decimal))
            myDbCommand.Parameters("@HobbsReading").Value = CDec(myLiquidFill.HobbsReading)
            myDbCommand.Parameters.Add(New SqlParameter("@AirframeHoursReading", SqlDbType.Decimal))
            myDbCommand.Parameters("@AirframeHoursReading").Value = CDec(myLiquidFill.AirframeHoursReading)
            myDbCommand.Parameters.Add(New SqlParameter("@LiquidFillLocation", SqlDbType.NVarChar, 255))
            myDbCommand.Parameters("@LiquidFillLocation").Value = myLiquidFill.LiquidFillLocation
            myDbCommand.Parameters.Add(New SqlParameter("@LiquidFillApprover", SqlDbType.NVarChar, 30))
            myDbCommand.Parameters("@LiquidFillApprover").Value = myLiquidFill.LiquidFillApprover
            myDbCommand.Parameters.Add(New SqlParameter("@LiquidFillId", SqlDbType.Int))
            myDbCommand.Parameters("@LiquidFillId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.Add(New SqlParameter("@NewConcurrencyId", SqlDbType.BigInt))
            myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.Add(New SqlParameter("@StampUserName", SqlDbType.NVarChar, 20))
            myDbCommand.Parameters("@StampUserName").Value = myLiquidFill.StampUserName
            myDbCommand.Parameters.Add(New SqlParameter("@NewUTCDate", SqlDbType.DateTime, 8))
            myDbCommand.Parameters("@NewUTCDate").Direction = ParameterDirection.Output

            Try
                myDbCommand.ExecuteNonQuery()
                newLiquidFillId = myDbCommand.Parameters("@LiquidFillId").Value

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try

            Return newLiquidFillId

        End Function

        Public Shared Sub Delete(ByVal LiquidFillId As Integer, ByVal StampUserName As String)

            Using myDbConnection As SqlConnection = New SqlConnection()

                myDbConnection.ConnectionString = AppConfiguration.connectionString

                Try
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            Delete(LiquidFillId, StampUserName, myDbConnection, myDbTransaction)
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

        Public Shared Sub Delete(ByVal LiquidFillId As Integer, ByVal StampUserName As String, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "deleteLiquidFill"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = MyDbTransaction

            myDbCommand.Parameters.AddWithValue("@LiquidFillId", LiquidFillId)
            myDbCommand.Parameters.AddWithValue("@StampUserName", StampUserName)
            Try
                myDbCommand.ExecuteNonQuery()
            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try
        End Sub

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As LiquidFill

            Dim liquidFill As LiquidFill = New LiquidFill()
            liquidFill.LiquidFillId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("LiquidFillId"))
            liquidFill.FlightId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FlightId"))
            liquidFill.LiquidGroupId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("LiquidGroupId"))
            liquidFill.LiquidGroupName = myDataRecord.GetString(myDataRecord.GetOrdinal("LiquidGroupName"))
            liquidFill.LiquidTypeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("LiquidTypeId"))
            liquidFill.LiquidTypeName = myDataRecord.GetString(myDataRecord.GetOrdinal("LiquidTypeName"))
            liquidFill.LiquidUnitId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("LiquidUnitId"))
            liquidFill.LiquidUnitName = myDataRecord.GetString(myDataRecord.GetOrdinal("LiquidUnitName"))
            liquidFill.LiquidAmt = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("LiquidAmt")), "#,##0.000")
            liquidFill.HobbsReading = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("HobbsReading")), "#,##0.0")
            liquidFill.AirframeHoursReading = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("AirframeHoursReading")), "#,##0.0")
            liquidFill.LiquidFillLocation = myDataRecord.GetString(myDataRecord.GetOrdinal("LiquidFillLocation"))
            liquidFill.LiquidFillApprover = myDataRecord.GetString(myDataRecord.GetOrdinal("LiquidFillApprover"))
            liquidFill.LiquidFillGUID = myDataRecord.GetGuid(myDataRecord.GetOrdinal("LiquidFillGUID"))
            liquidFill.IsActive = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsActive"))
            liquidFill.IsDeleted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsDeleted"))
            liquidFill.UtcDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateCreated"))
            liquidFill.UtcDateModified = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateModified"))
            liquidFill.StampUserName = myDataRecord.GetString(myDataRecord.GetOrdinal("StampUserName"))
            liquidFill.ConcurrencyId = DalUtility.ConvertRowVersionToString(myDataRecord("ConcurrencyId"))
            Return liquidFill

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
