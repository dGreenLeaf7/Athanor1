' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Contoso.Utilities
Imports FLTBizObjects.FLT.BO

Namespace FLT.Dal

    Public Class fltLookupBaseDB

        Public Shared Function GetItem(ByVal LookupName As String, ByVal Id As Integer) As fltLookupBase

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "fetch" & LookupName
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@Id", Id)
            Try
                myDbConnection.Open()

                Dim myReader As DbDataReader = Nothing

                Try
                    myReader = myDbCommand.ExecuteReader(CommandBehavior.SingleRow)

                    If (Not myReader.HasRows) Then Return Nothing

                    myReader.Read()

                    Return FillDataRecord(myReader, LookupName)

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

        Public Shared Function GetList(ByVal LookupName As String, ByVal ProgId As Integer, ByVal IsActiveFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As fltLookupBaseList

            Dim tempList As New fltLookupBaseList

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "list" & LookupName & "s"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@ProgId", ProgId)
            myDbCommand.Parameters.AddWithValue("@IsActiveFilter", IsActiveFilter)
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
                            tempList.Add(FillDataRecord(myReader, LookupName))
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

        Public Shared Sub Update(ByVal myLookup As fltLookupBase)

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "update" & myLookup.LookupName
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@Id", myLookup.ID)
            myDbCommand.Parameters.AddWithValue("@ProgId", myLookup.ProgId)
            myDbCommand.Parameters.AddWithValue("@StringValue", myLookup.StringValue)
            myDbCommand.Parameters.AddWithValue("@SortOrder", myLookup.SortOrder)
            myDbCommand.Parameters.AddWithValue("@IsActive", myLookup.IsActive)
            myDbCommand.Parameters.Add(New SqlParameter("@ConcurrencyId", SqlDbType.Timestamp, 8))
            myDbCommand.Parameters("@ConcurrencyId").Value = DalUtility.ConvertStringToRowVersion(myLookup.ConcurrencyId)
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

        Public Shared Function Insert(ByVal myLookup As fltLookupBase) As Integer

            Dim newId As Integer = 0

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "add" & myLookup.LookupName
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@ProgId", myLookup.ProgId)
            myDbCommand.Parameters.AddWithValue("@StringValue", myLookup.StringValue)
            myDbCommand.Parameters.AddWithValue("@SortOrder", myLookup.SortOrder)
            myDbCommand.Parameters.AddWithValue("@ID", 0)
            myDbCommand.Parameters("@ID").Direction = ParameterDirection.Output
            myDbCommand.Parameters.AddWithValue("@NewConcurrencyId", 0)
            myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output

            Try
                myDbConnection.Open()
                myDbCommand.ExecuteNonQuery()
                myDbConnection.Close()

                newId = myDbCommand.Parameters("@ID").Value

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            Finally
                CType(myDbConnection, IDisposable).Dispose()
            End Try

            Return newId

        End Function

        Public Shared Sub Delete(ByVal LookupName As String, ByVal Id As Integer)

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "delete" & LookupName
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@Id", Id)
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

        Friend Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord, ByVal LookupName As String) As fltLookupBase

            Dim thisObject As fltLookupBase = New fltLookupBase
            thisObject.ID = myDataRecord.GetInt32(myDataRecord.GetOrdinal("Id"))
            thisObject.LookupName = LookupName
            thisObject.ProgId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ProgId"))
            thisObject.StringValue = myDataRecord.GetString(myDataRecord.GetOrdinal("StringValue"))
            thisObject.SortOrder = myDataRecord.GetInt32(myDataRecord.GetOrdinal("SortOrder"))
            thisObject.IsActive = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsActive"))
            thisObject.ConcurrencyId = DalUtility.ConvertRowVersionToString(myDataRecord("ConcurrencyId"))
            Return thisObject

        End Function

    End Class

End Namespace
