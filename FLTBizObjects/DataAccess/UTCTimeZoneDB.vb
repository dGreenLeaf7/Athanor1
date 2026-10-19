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

	Public Class UTCTimeZoneDB
		
        Public Shared Function GetItem(ByVal TimeZoneId As Integer) As UTCTimeZone

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "fetchUTCTimeZone"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.Add(New SqlParameter("@TimeZoneId", SqlDbType.Int))
            myDbCommand.Parameters("@TimeZoneId").Value = TimeZoneId
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
		
        Public Shared Function GetList(ByVal IsActiveFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As UTCTimeZoneList

            Dim tempList As UTCTimeZoneList = Nothing

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listUTCTimeZones"
            myDbCommand.Connection = myDbConnection

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
                        tempList = New UTCTimeZoneList
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
		
		Public Shared Sub Update(ByVal myUTCTimeZone As UTCTimeZone)
			
			Dim myDbConnection As SqlConnection = New SqlConnection()
			myDbConnection.ConnectionString = AppConfiguration.connectionString
			
			Dim myDbCommand As SqlCommand = New SqlCommand()
			myDbCommand.CommandType = CommandType.StoredProcedure
			myDbCommand.CommandText = "updateUTCTimeZone"
			myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@TimeZoneId", myUTCTimeZone.TimeZoneID)
            myDbCommand.Parameters.AddWithValue("@TimeZoneCode", myUTCTimeZone.TimeZoneCode)
            myDbCommand.Parameters.AddWithValue("@TimeZoneOffset", myUTCTimeZone.TimeZoneOffset)
            myDbCommand.Parameters.AddWithValue("@TimeZoneDescr", myUTCTimeZone.TimeZoneDescr)
            myDbCommand.Parameters.Add(New SqlParameter("@DaylightSavingTime", SqlDbType.Bit))
            myDbCommand.Parameters("@DaylightSavingTime").Value = myUTCTimeZone.DaylightSavingTime
            myDbCommand.Parameters.Add(New SqlParameter("@DaylightSavingOffset", SqlDbType.Float))
            myDbCommand.Parameters("@DaylightSavingOffset").Value = myUTCTimeZone.DaylightSavingOffset
            myDbCommand.Parameters.Add(New SqlParameter("@DaylightSavingBegin", SqlDbType.DateTime))
            If IsDate(myUTCTimeZone.DaylightSavingBegin) Then
                myDbCommand.Parameters("@DaylightSavingBegin").Value = myUTCTimeZone.DaylightSavingBegin
            Else
                myDbCommand.Parameters("@DaylightSavingBegin").Value = Nothing
            End If
            myDbCommand.Parameters.Add(New SqlParameter("@DaylightSavingEnd", SqlDbType.DateTime))
            If IsDate(myUTCTimeZone.DaylightSavingEnd) Then
                myDbCommand.Parameters("@DaylightSavingEnd").Value = myUTCTimeZone.DaylightSavingEnd
            Else
                myDbCommand.Parameters("@DaylightSavingEnd").Value = Nothing
            End If
            myDbCommand.Parameters.AddWithValue("@IsActive", myUTCTimeZone.IsActive)
			myDbCommand.Parameters.Add(new SqlParameter("@ConcurrencyId", SqlDbType.Timestamp, 8))
			myDbCommand.Parameters("@ConcurrencyId").Value = DalUtility.ConvertStringToRowVersion(myUTCTimeZone.ConcurrencyId)
			myDbCommand.Parameters.Add(new SqlParameter("@NewConcurrencyId", SqlDbType.Timestamp, 8))
			myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
			Try
				MyDbConnection.Open()
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
		
		Public Shared Sub Insert(ByVal myUTCTimeZone As UTCTimeZone)
			Dim myDbConnection As SqlConnection = New SqlConnection()
			myDbConnection.ConnectionString = AppConfiguration.connectionString
			
			Dim myDbCommand As SqlCommand = New SqlCommand()
			myDbCommand.CommandType = CommandType.StoredProcedure
			myDbCommand.CommandText = "addUTCTimeZone"
			myDbCommand.Connection = myDbConnection
			
			myDbCommand.Parameters.AddWithValue("@TimeZoneCode", myUTCTimeZone.TimeZoneCode)
			myDbCommand.Parameters.AddWithValue("@TimeZoneOffset", myUTCTimeZone.TimeZoneOffset)
            myDbCommand.Parameters.AddWithValue("@TimeZoneDescr", myUTCTimeZone.TimeZoneDescr)
            myDbCommand.Parameters.Add(New SqlParameter("@DaylightSavingTime", SqlDbType.Bit))
            myDbCommand.Parameters("@DaylightSavingTime").Value = myUTCTimeZone.DaylightSavingTime
            myDbCommand.Parameters.Add(New SqlParameter("@DaylightSavingOffset", SqlDbType.Float))
            myDbCommand.Parameters("@DaylightSavingOffset").Value = myUTCTimeZone.DaylightSavingOffset
            myDbCommand.Parameters.Add(New SqlParameter("@DaylightSavingBegin", SqlDbType.DateTime))
            If IsDate(myUTCTimeZone.DaylightSavingBegin) Then
                myDbCommand.Parameters("@DaylightSavingBegin").Value = myUTCTimeZone.DaylightSavingBegin
            Else
                myDbCommand.Parameters("@DaylightSavingBegin").Value = Nothing
            End If
            myDbCommand.Parameters.Add(New SqlParameter("@DaylightSavingEnd", SqlDbType.DateTime))
            If IsDate(myUTCTimeZone.DaylightSavingEnd) Then
                myDbCommand.Parameters("@DaylightSavingEnd").Value = myUTCTimeZone.DaylightSavingEnd
            Else
                myDbCommand.Parameters("@DaylightSavingEnd").Value = Nothing
            End If
            myDbCommand.Parameters.Add(New SqlParameter("@TimeZoneId", SqlDbType.Int))
            myDbCommand.Parameters("@TimeZoneId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.AddWithValue("@NewConcurrencyId", 0)
            myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output

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
		
        Public Shared Sub Delete(ByVal TimeZoneId As Integer)

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "deleteUTCTimeZone"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@TimeZoneId", TimeZoneId)
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
		
		Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As UTCTimeZone
			
            Dim uTCTimeZone As UTCTimeZone = New UTCTimeZone()
            uTCTimeZone.TimeZoneID = myDataRecord.GetInt32(myDataRecord.GetOrdinal("TimeZoneId"))
			uTCTimeZone.TimeZoneCode = myDataRecord.GetString(myDataRecord.GetOrdinal("TimeZoneCode"))
            uTCTimeZone.TimeZoneOffset = myDataRecord.GetDouble(myDataRecord.GetOrdinal("TimeZoneOffset"))
			uTCTimeZone.TimeZoneDescr = myDataRecord.GetString(myDataRecord.GetOrdinal("TimeZoneDescr"))
            uTCTimeZone.DaylightSavingTime = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("DaylightSavingTime"))
            uTCTimeZone.DaylightSavingOffset = myDataRecord.GetDouble(myDataRecord.GetOrdinal("DaylightSavingOffset"))
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("DaylightSavingBegin")) Then
                uTCTimeZone.DaylightSavingBegin = Format(myDataRecord.GetDateTime(myDataRecord.GetOrdinal("DaylightSavingBegin")), "MM-dd-yyyy hh:mm:ss tt")
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("DaylightSavingEnd")) Then
                uTCTimeZone.DaylightSavingEnd = Format(myDataRecord.GetDateTime(myDataRecord.GetOrdinal("DaylightSavingEnd")), "MM-dd-yyyy hh:mm:ss tt")
            End If
            uTCTimeZone.IsActive = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsActive"))
			uTCTimeZone.ConcurrencyId = DalUtility.ConvertRowVersionToString(myDataRecord("ConcurrencyId"))
			Return uTCTimeZone
			
		End Function
		
	End Class

End Namespace
