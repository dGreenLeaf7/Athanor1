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

	Public Class LocationDB
		
        Public Shared Function GetItem(ByVal LocactionId As Integer) As Location

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "fetchLocation"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@LocactionId", LocactionId)
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
		
        Public Shared Function GetList(ByVal ProgId As Integer, ByVal TimeZoneId As Integer, ByVal IsActiveFilter As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As LocationList

            Dim tempList As New LocationList

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listLocations"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@ProgId", ProgId)
            myDbCommand.Parameters.AddWithValue("@TimeZoneId", TimeZoneId)
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
		
		Public Shared Sub Update(ByVal myLocation As Location)
			
			Dim myDbConnection As SqlConnection = New SqlConnection()
			myDbConnection.ConnectionString = AppConfiguration.connectionString
			
			Dim myDbCommand As SqlCommand = New SqlCommand()
			myDbCommand.CommandType = CommandType.StoredProcedure
			myDbCommand.CommandText = "updateLocation"
			myDbCommand.Connection = myDbConnection
			
			myDbCommand.Parameters.AddWithValue("@LocactionId", myLocation.LocactionId)
			myDbCommand.Parameters.AddWithValue("@ProgId", myLocation.ProgId)
            myDbCommand.Parameters.AddWithValue("@TimeZoneId", myLocation.TimeZoneId)
			myDbCommand.Parameters.AddWithValue("@LocationName", myLocation.LocationName)
			myDbCommand.Parameters.AddWithValue("@IsActive", myLocation.IsActive)
			myDbCommand.Parameters.AddWithValue("@StampUserName", myLocation.StampUserName)
			myDbCommand.Parameters.Add(new SqlParameter("@ConcurrencyId", SqlDbType.Timestamp, 8))
			myDbCommand.Parameters("@ConcurrencyId").Value = DalUtility.ConvertStringToRowVersion(myLocation.ConcurrencyId)
			myDbCommand.Parameters.Add(new SqlParameter("@NewConcurrencyId", SqlDbType.Timestamp, 8))
			myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
			myDbCommand.Parameters.Add(new SqlParameter("@NewUTCDate", SqlDbType.DateTime, 8))
			myDbCommand.Parameters("@NewUTCDate").Direction = ParameterDirection.Output
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
		
        Public Shared Function Insert(ByVal myLocation As Location) As Integer

            Dim newId As Integer = 0

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "addLocation"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@ProgId", myLocation.ProgId)
            myDbCommand.Parameters.AddWithValue("@TimeZoneId", myLocation.TimeZoneId)
            myDbCommand.Parameters.AddWithValue("@LocationName", myLocation.LocationName)
            myDbCommand.Parameters.Add(New SqlParameter("@LocactionId", SqlDbType.Int))
            myDbCommand.Parameters("@LocactionId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.Add(New SqlParameter("@NewConcurrencyId", SqlDbType.Int))
            myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.AddWithValue("@StampUserName", myLocation.StampUserName)
            myDbCommand.Parameters.Add(New SqlParameter("@NewUTCDate", SqlDbType.DateTime, 8))
            myDbCommand.Parameters("@NewUTCDate").Direction = ParameterDirection.Output

            Try
                myDbConnection.Open()
                myDbCommand.ExecuteNonQuery()
                myDbConnection.Close()

                newId = myDbCommand.Parameters("@LocactionId").Value

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            Finally
                CType(myDbConnection, IDisposable).Dispose()
            End Try
            Return newId
        End Function
		
        Public Shared Sub Delete(ByVal LocactionId As Integer, ByVal StampUserName As String)

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "deleteLocation"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@LocactionId", LocactionId)
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
		
		Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As Location
			
			Dim location As Location = New Location()
			location.LocactionId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("LocactionId"))
            location.ProgId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ProgId"))
            location.TimeZoneId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("TimeZoneId"))
            location.TimeZoneCode = myDataRecord.GetString(myDataRecord.GetOrdinal("TimeZoneCode"))
            location.TimeZoneOffset = myDataRecord.GetDouble(myDataRecord.GetOrdinal("TimeZoneOffset"))
            location.DaylightSavingTime = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("DaylightSavingTime"))
            location.DaylightSavingOffset = myDataRecord.GetDouble(myDataRecord.GetOrdinal("DaylightSavingOffset"))
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("DaylightSavingBegin")) Then
                location.DaylightSavingBegin = Format(myDataRecord.GetDateTime(myDataRecord.GetOrdinal("DaylightSavingBegin")), "MM-dd-yyyy hh:mm:ss tt")
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("DaylightSavingEnd")) Then
                location.DaylightSavingEnd = Format(myDataRecord.GetDateTime(myDataRecord.GetOrdinal("DaylightSavingEnd")), "MM-dd-yyyy hh:mm:ss tt")
            End If
            location.LocationName = myDataRecord.GetString(myDataRecord.GetOrdinal("LocationName"))
            location.IsActive = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsActive"))
            location.IsDeleted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsDeleted"))
            location.UtcDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateCreated"))
            location.UtcDateModified = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateModified"))
            location.StampUserName = myDataRecord.GetString(myDataRecord.GetOrdinal("StampUserName"))
            location.ConcurrencyId = DalUtility.ConvertRowVersionToString(myDataRecord("ConcurrencyId"))
            Return location

        End Function
		
	End Class

End Namespace
