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

    Public Class WeightDefDB

        Public Shared Function GetItem(ByVal WeightDefId As Integer) As WeightDef

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "fetchWeightDef"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@WeightDefId", WeightDefId)
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

        Public Shared Function GetList(ByVal ProgId As Integer, ByVal WeightDefTypeDescr As String, ByVal WeightDefTypeId As Integer, ByVal IsActiveFilter As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As WeightDefList

            Dim tempList As WeightDefList = New WeightDefList

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listWeightDefs"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@ProgId", ProgId)
            myDbCommand.Parameters.AddWithValue("@WeightDefTypeDescr", WeightDefTypeDescr)
            myDbCommand.Parameters.AddWithValue("@WeightDefTypeId", WeightDefTypeId)
            myDbCommand.Parameters.AddWithValue("@IsActiveFilter", IsActiveFilter)
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

        Public Shared Sub Update(ByVal myWeightDef As WeightDef)

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "updateWeightDef"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@WeightDefId", myWeightDef.WeightDefId)
            myDbCommand.Parameters.AddWithValue("@WeightDefTypeid", myWeightDef.WeightDefTypeId)
            myDbCommand.Parameters.AddWithValue("@WeightDefDescr", myWeightDef.WeightDefDescr)
            myDbCommand.Parameters.AddWithValue("@Weight", CInt(myWeightDef.Weight))
            myDbCommand.Parameters.AddWithValue("@WeightUnitId", myWeightDef.WeightUnitId)
            myDbCommand.Parameters.AddWithValue("@SortOrder", myWeightDef.SortOrder)
            myDbCommand.Parameters.AddWithValue("@IsActive", myWeightDef.IsActive)
            myDbCommand.Parameters.AddWithValue("@IsDeleted", myWeightDef.IsDeleted)
            myDbCommand.Parameters.AddWithValue("@StampUserName", myWeightDef.StampUserName)
            myDbCommand.Parameters.Add(New SqlParameter("@ConcurrencyId", SqlDbType.Timestamp, 8))
            myDbCommand.Parameters("@ConcurrencyId").Value = DalUtility.ConvertStringToRowVersion(myWeightDef.ConcurrencyId)
            myDbCommand.Parameters.Add(New SqlParameter("@NewConcurrencyId", SqlDbType.Timestamp, 8))
            myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
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

        Public Shared Function Insert(ByVal myWeightDef As WeightDef) As Integer
            Dim newId As Integer = 0
            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "addWeightDef"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@ProgId", myWeightDef.ProgId)
            myDbCommand.Parameters.AddWithValue("@WeightDefTypeId", myWeightDef.WeightDefTypeId)
            myDbCommand.Parameters.AddWithValue("@WeightDefDescr", myWeightDef.WeightDefDescr)
            myDbCommand.Parameters.AddWithValue("@Weight", CInt(myWeightDef.Weight))
            myDbCommand.Parameters.AddWithValue("@WeightUnitId", myWeightDef.WeightUnitId)
            myDbCommand.Parameters.AddWithValue("@SortOrder", myWeightDef.SortOrder)
            myDbCommand.Parameters.Add(New SqlParameter("@WeightDefId", SqlDbType.Int))
            myDbCommand.Parameters("@WeightDefId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.AddWithValue("@NewConcurrencyId", 0)
            myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.AddWithValue("@StampUserName", myWeightDef.StampUserName)

            Try
                myDbConnection.Open()
                myDbCommand.ExecuteNonQuery()
                myDbConnection.Close()

                newId = myDbCommand.Parameters("@WeightDefId").Value

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            Finally
                CType(myDbConnection, IDisposable).Dispose()
            End Try

            Return newId

        End Function

        Public Shared Sub Delete(ByVal WeightDefId As Integer, ByVal StampUserName As String)

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "deleteWeightDef"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@WeightDefId", WeightDefId)
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

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As WeightDef

            Dim WeightDef As WeightDef = New WeightDef()
            WeightDef.WeightDefId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("WeightDefId"))
            WeightDef.ProgId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ProgId"))
            WeightDef.WeightDefTypeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("WeightDefTypeId"))
            WeightDef.weightDefTypeDescr = myDataRecord.GetString(myDataRecord.GetOrdinal("WeightDefTypeDescr"))
            WeightDef.WeightDefDescr = myDataRecord.GetString(myDataRecord.GetOrdinal("WeightDefDescr"))
            WeightDef.Weight = Format(myDataRecord.GetInt32(myDataRecord.GetOrdinal("Weight")), "#,##0")
            WeightDef.WeightUnitId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("WeightUnitId"))
            WeightDef.WeightUnit = myDataRecord.GetString(myDataRecord.GetOrdinal("WeightUnit"))
            WeightDef.SortOrder = myDataRecord.GetInt32(myDataRecord.GetOrdinal("SortOrder"))
            WeightDef.WeightDefGuid = myDataRecord.GetGuid(myDataRecord.GetOrdinal("WeightDefGuid"))
            WeightDef.IsActive = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsActive"))
            WeightDef.IsDeleted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsDeleted"))
            WeightDef.UtcDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateCreated"))
            WeightDef.UtcDateModified = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateModified"))
            WeightDef.StampUserName = myDataRecord.GetString(myDataRecord.GetOrdinal("StampUserName"))
            WeightDef.ConcurrencyId = DalUtility.ConvertRowVersionToString(myDataRecord("ConcurrencyId"))
            Return WeightDef

        End Function

    End Class

End Namespace
