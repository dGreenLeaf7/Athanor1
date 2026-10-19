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

	Public Class FltUserDB
		
		Public Shared Function GetItem(ByVal UserName As String) As FltUser
			
			Dim myDbConnection As SqlConnection = New SqlConnection()
			myDbConnection.ConnectionString = AppConfiguration.connectionString
            Dim myDbCommand As SqlCommand = New SqlCommand()
			myDbCommand.CommandType = CommandType.StoredProcedure
			myDbCommand.CommandText = "fetchFltUser"
			myDbCommand.Connection = myDbConnection
            myDbCommand.CommandTimeout = 120

			myDbCommand.Parameters.AddWithValue("@UserName", UserName)
			Try
				myDbConnection.Open()
				
				Dim myReader As DbDataReader = nothing
				
				Try
					myReader = myDbCommand.ExecuteReader(CommandBehavior.SingleRow)
					
					If (Not myReader.HasRows) Then Return Nothing
					
					myReader.Read()
					
					Return FillDataRecord(myReader)
					
				Catch myException as Exception
					Logger.Write(myException, myDbCommand.CommandText)
					Throw New ApplicationException("Application encountered a data error.")
				Finally
					CType(myReader, IDisposable).Dispose()
				End Try
				
				myDbConnection.Close()
				
			Catch myException as Exception
				Logger.Write(myException, myDbCommand.CommandText)
				Throw New ApplicationException("Application encountered a data error.")
			Finally
				CType(myDbConnection, IDisposable).Dispose()
			End Try
		End Function
		
        Public Shared Function GetList(ByVal IsActiveFilter As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As FltUserList

            Dim tempList As FltUserList = Nothing

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listFltUsers"
            myDbCommand.Connection = myDbConnection

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
                        tempList = New FltUserList
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
		
		Public Shared Sub Update(ByVal myFltUser As FltUser)
			
			Dim myDbConnection As SqlConnection = New SqlConnection()
			myDbConnection.ConnectionString = AppConfiguration.connectionString
			
			Dim myDbCommand As SqlCommand = New SqlCommand()
			myDbCommand.CommandType = CommandType.StoredProcedure
			myDbCommand.CommandText = "updateFltUser"
			myDbCommand.Connection = myDbConnection
			
			myDbCommand.Parameters.AddWithValue("@UserName", myFltUser.UserName)
			myDbCommand.Parameters.AddWithValue("@Password", myFltUser.Password)
			myDbCommand.Parameters.AddWithValue("@PasswordSalt", myFltUser.PasswordSalt)
			myDbCommand.Parameters.AddWithValue("@Email", myFltUser.Email)
			myDbCommand.Parameters.AddWithValue("@PasswordQuestion", myFltUser.PasswordQuestion)
			myDbCommand.Parameters.AddWithValue("@PasswordAnswer", myFltUser.PasswordAnswer)
			myDbCommand.Parameters.AddWithValue("@IsSuperUser", myFltUser.IsSuperUser)
			myDbCommand.Parameters.AddWithValue("@IsApproved", myFltUser.IsApproved)
			myDbCommand.Parameters.AddWithValue("@IsLockedOut", myFltUser.IsLockedOut)
			myDbCommand.Parameters.Add("@UtcLastLoginDate", SqlDbType.DateTime)
			If myFltUser.UtcLastLoginDate.Year < 1800 Then
				myDbCommand.Parameters("@UtcLastLoginDate").Value = Nothing
			Else
				myDbCommand.Parameters("@UtcLastLoginDate").Value = myFltUser.UtcLastLoginDate
			End If
			myDbCommand.Parameters.Add("@UtcLastLockoutDate", SqlDbType.DateTime)
			If myFltUser.UtcLastLockoutDate.Year < 1800 Then
				myDbCommand.Parameters("@UtcLastLockoutDate").Value = Nothing
			Else
				myDbCommand.Parameters("@UtcLastLockoutDate").Value = myFltUser.UtcLastLockoutDate
			End If
			myDbCommand.Parameters.Add("@UtcLastPasswordChangedDate", SqlDbType.DateTime)
			If myFltUser.UtcLastPasswordChangedDate.Year < 1800 Then
				myDbCommand.Parameters("@UtcLastPasswordChangedDate").Value = Nothing
			Else
				myDbCommand.Parameters("@UtcLastPasswordChangedDate").Value = myFltUser.UtcLastPasswordChangedDate
			End If
			myDbCommand.Parameters.AddWithValue("@FailedPasswordAttemptCount", myFltUser.FailedPasswordAttemptCount)
			myDbCommand.Parameters.Add("@UtcFailedPasswordAttemptWindowStart", SqlDbType.DateTime)
			If myFltUser.UtcFailedPasswordAttemptWindowStart.Year < 1800 Then
				myDbCommand.Parameters("@UtcFailedPasswordAttemptWindowStart").Value = Nothing
			Else
				myDbCommand.Parameters("@UtcFailedPasswordAttemptWindowStart").Value = myFltUser.UtcFailedPasswordAttemptWindowStart
			End If
			myDbCommand.Parameters.AddWithValue("@FailedPasswordAnswerAttemptCount", myFltUser.FailedPasswordAnswerAttemptCount)
			myDbCommand.Parameters.Add("@UtcFailedPasswordAnswerAttemptWindowStart", SqlDbType.DateTime)
			If myFltUser.UtcFailedPasswordAnswerAttemptWindowStart.Year < 1800 Then
				myDbCommand.Parameters("@UtcFailedPasswordAnswerAttemptWindowStart").Value = Nothing
			Else
				myDbCommand.Parameters("@UtcFailedPasswordAnswerAttemptWindowStart").Value = myFltUser.UtcFailedPasswordAnswerAttemptWindowStart
			End If
			myDbCommand.Parameters.AddWithValue("@DisplayName", myFltUser.DisplayName)
			myDbCommand.Parameters.AddWithValue("@Location", myFltUser.Location)
			myDbCommand.Parameters.AddWithValue("@CountryCode", myFltUser.CountryCode)
			myDbCommand.Parameters.AddWithValue("@PhoneNumber", myFltUser.PhoneNumber)
			myDbCommand.Parameters.AddWithValue("@MgrUserName", myFltUser.MgrUserName)
			myDbCommand.Parameters.AddWithValue("@Department", myFltUser.Department)
			myDbCommand.Parameters.AddWithValue("@Title", myFltUser.Title)
			myDbCommand.Parameters.AddWithValue("@Company", myFltUser.Company)
			myDbCommand.Parameters.AddWithValue("@DefaultProgId", myFltUser.DefaultProgId)
			myDbCommand.Parameters.AddWithValue("@IsActive", myFltUser.IsActive)
			myDbCommand.Parameters.AddWithValue("@StampUserName", myFltUser.StampUserName)
			myDbCommand.Parameters.Add(new SqlParameter("@ConcurrencyId", SqlDbType.Timestamp, 8))
			myDbCommand.Parameters("@ConcurrencyId").Value = DalUtility.ConvertStringToRowVersion(myFltUser.ConcurrencyId)
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
		
		Public Shared Sub Insert(ByVal myFltUser As FltUser)
			Dim myDbConnection As SqlConnection = New SqlConnection()
			myDbConnection.ConnectionString = AppConfiguration.connectionString
			
			Dim myDbCommand As SqlCommand = New SqlCommand()
			myDbCommand.CommandType = CommandType.StoredProcedure
			myDbCommand.CommandText = "addFltUser"
			myDbCommand.Connection = myDbConnection
			
			myDbCommand.Parameters.AddWithValue("@UserName", myFltUser.UserName)
			myDbCommand.Parameters.AddWithValue("@Password", myFltUser.Password)
			myDbCommand.Parameters.AddWithValue("@PasswordSalt", myFltUser.PasswordSalt)
			myDbCommand.Parameters.AddWithValue("@Email", myFltUser.Email)
			myDbCommand.Parameters.AddWithValue("@PasswordQuestion", myFltUser.PasswordQuestion)
			myDbCommand.Parameters.AddWithValue("@PasswordAnswer", myFltUser.PasswordAnswer)
			myDbCommand.Parameters.AddWithValue("@IsSuperUser", myFltUser.IsSuperUser)
			myDbCommand.Parameters.AddWithValue("@IsApproved", myFltUser.IsApproved)
			myDbCommand.Parameters.AddWithValue("@IsLockedOut", myFltUser.IsLockedOut)
			myDbCommand.Parameters.Add("@UtcLastLoginDate", SqlDbType.DateTime)
			If myFltUser.UtcLastLoginDate.Year < 1800 Then
				myDbCommand.Parameters("@UtcLastLoginDate").Value = Nothing
			Else
				myDbCommand.Parameters("@UtcLastLoginDate").Value = myFltUser.UtcLastLoginDate
			End If
			myDbCommand.Parameters.Add("@UtcLastLockoutDate", SqlDbType.DateTime)
			If myFltUser.UtcLastLockoutDate.Year < 1800 Then
				myDbCommand.Parameters("@UtcLastLockoutDate").Value = Nothing
			Else
				myDbCommand.Parameters("@UtcLastLockoutDate").Value = myFltUser.UtcLastLockoutDate
			End If
			myDbCommand.Parameters.Add("@UtcLastPasswordChangedDate", SqlDbType.DateTime)
			If myFltUser.UtcLastPasswordChangedDate.Year < 1800 Then
				myDbCommand.Parameters("@UtcLastPasswordChangedDate").Value = Nothing
			Else
				myDbCommand.Parameters("@UtcLastPasswordChangedDate").Value = myFltUser.UtcLastPasswordChangedDate
			End If
			myDbCommand.Parameters.AddWithValue("@FailedPasswordAttemptCount", myFltUser.FailedPasswordAttemptCount)
			myDbCommand.Parameters.Add("@UtcFailedPasswordAttemptWindowStart", SqlDbType.DateTime)
			If myFltUser.UtcFailedPasswordAttemptWindowStart.Year < 1800 Then
				myDbCommand.Parameters("@UtcFailedPasswordAttemptWindowStart").Value = Nothing
			Else
				myDbCommand.Parameters("@UtcFailedPasswordAttemptWindowStart").Value = myFltUser.UtcFailedPasswordAttemptWindowStart
			End If
			myDbCommand.Parameters.AddWithValue("@FailedPasswordAnswerAttemptCount", myFltUser.FailedPasswordAnswerAttemptCount)
			myDbCommand.Parameters.Add("@UtcFailedPasswordAnswerAttemptWindowStart", SqlDbType.DateTime)
			If myFltUser.UtcFailedPasswordAnswerAttemptWindowStart.Year < 1800 Then
				myDbCommand.Parameters("@UtcFailedPasswordAnswerAttemptWindowStart").Value = Nothing
			Else
				myDbCommand.Parameters("@UtcFailedPasswordAnswerAttemptWindowStart").Value = myFltUser.UtcFailedPasswordAnswerAttemptWindowStart
			End If
			myDbCommand.Parameters.AddWithValue("@DisplayName", myFltUser.DisplayName)
			myDbCommand.Parameters.AddWithValue("@Location", myFltUser.Location)
			myDbCommand.Parameters.AddWithValue("@CountryCode", myFltUser.CountryCode)
			myDbCommand.Parameters.AddWithValue("@PhoneNumber", myFltUser.PhoneNumber)
			myDbCommand.Parameters.AddWithValue("@MgrUserName", myFltUser.MgrUserName)
			myDbCommand.Parameters.AddWithValue("@Department", myFltUser.Department)
			myDbCommand.Parameters.AddWithValue("@Title", myFltUser.Title)
			myDbCommand.Parameters.AddWithValue("@Company", myFltUser.Company)
			myDbCommand.Parameters.AddWithValue("@DefaultProgId", myFltUser.DefaultProgId)
			myDbCommand.Parameters.AddWithValue("@NewConcurrencyId", 0)
			myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
			myDbCommand.Parameters.AddWithValue("@StampUserName", myFltUser.StampUserName)
			myDbCommand.Parameters.Add(new SqlParameter("@NewUTCDate", SqlDbType.DateTime, 8))
			myDbCommand.Parameters("@NewUTCDate").Direction = ParameterDirection.Output
			
			Try
				MyDbConnection.Open()
				myDbCommand.ExecuteNonQuery()
				myDbConnection.Close()
				
			Catch myException as Exception
				Logger.Write(myException, myDbCommand.CommandText)
				Throw New ApplicationException("Application encountered a data error.")
			Finally
				CType(myDbConnection, IDisposable).Dispose()
			End Try
			
		End Sub
		
		Public Shared Sub Delete(ByVal UserName As String, ByVal StampUserName As String)
		
			Dim myDbConnection As SqlConnection = New SqlConnection()
			myDbConnection.ConnectionString = AppConfiguration.connectionString
			
			Dim myDbCommand As SqlCommand = New SqlCommand()
			myDbCommand.CommandType = CommandType.StoredProcedure
			myDbCommand.CommandText = "deleteFltUser"
			myDbCommand.Connection = myDbConnection
			
			myDbCommand.Parameters.AddWithValue("@UserName", UserName)
			myDbCommand.Parameters.AddWithValue("@StampUserName", StampUserName)
			Try
				myDbConnection.Open()
				myDbCommand.ExecuteNonQuery()
				myDbConnection.Close()
			Catch myException as Exception
				Logger.Write(myException, myDbCommand.CommandText)
				Throw New ApplicationException("Application encountered a data error.")
			Finally
				CType(myDbConnection, IDisposable).Dispose()
			End Try
		End Sub
		
        Friend Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As FltUser

            Dim fltUser As FltUser = New FltUser()
            fltUser.UserName = myDataRecord.GetString(myDataRecord.GetOrdinal("UserName"))
            fltUser.Password = myDataRecord.GetString(myDataRecord.GetOrdinal("Password"))
            fltUser.PasswordSalt = myDataRecord.GetString(myDataRecord.GetOrdinal("PasswordSalt"))
            fltUser.Email = myDataRecord.GetString(myDataRecord.GetOrdinal("Email"))
            fltUser.PasswordQuestion = myDataRecord.GetString(myDataRecord.GetOrdinal("PasswordQuestion"))
            fltUser.PasswordAnswer = myDataRecord.GetString(myDataRecord.GetOrdinal("PasswordAnswer"))
            fltUser.IsSuperUser = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsSuperUser"))
            fltUser.IsApproved = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsApproved"))
            fltUser.IsLockedOut = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsLockedOut"))
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("UtcLastLoginDate")) Then
                fltUser.UtcLastLoginDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UtcLastLoginDate"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("UtcLastLockoutDate")) Then
                fltUser.UtcLastLockoutDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UtcLastLockoutDate"))
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("UtcLastPasswordChangedDate")) Then
                fltUser.UtcLastPasswordChangedDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UtcLastPasswordChangedDate"))
            End If
            fltUser.FailedPasswordAttemptCount = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FailedPasswordAttemptCount"))
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("UtcFailedPasswordAttemptWindowStart")) Then
                fltUser.UtcFailedPasswordAttemptWindowStart = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UtcFailedPasswordAttemptWindowStart"))
            End If
            fltUser.FailedPasswordAnswerAttemptCount = myDataRecord.GetInt32(myDataRecord.GetOrdinal("FailedPasswordAnswerAttemptCount"))
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("UtcFailedPasswordAnswerAttemptWindowStart")) Then
                fltUser.UtcFailedPasswordAnswerAttemptWindowStart = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UtcFailedPasswordAnswerAttemptWindowStart"))
            End If
            fltUser.DisplayName = myDataRecord.GetString(myDataRecord.GetOrdinal("DisplayName"))
            fltUser.Location = myDataRecord.GetString(myDataRecord.GetOrdinal("Location"))
            fltUser.CountryCode = myDataRecord.GetString(myDataRecord.GetOrdinal("CountryCode"))
            fltUser.PhoneNumber = myDataRecord.GetString(myDataRecord.GetOrdinal("PhoneNumber"))
            fltUser.MgrUserName = myDataRecord.GetString(myDataRecord.GetOrdinal("MgrUserName"))
            fltUser.Department = myDataRecord.GetString(myDataRecord.GetOrdinal("Department"))
            fltUser.Title = myDataRecord.GetString(myDataRecord.GetOrdinal("Title"))
            fltUser.Company = myDataRecord.GetString(myDataRecord.GetOrdinal("Company"))
            fltUser.DefaultProgId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("DefaultProgId"))
            fltUser.IsActive = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsActive"))
            fltUser.IsDeleted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsDeleted"))
            fltUser.UtcDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateCreated"))
            fltUser.UtcDateModified = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateModified"))
            fltUser.StampUserName = myDataRecord.GetOrdinal("StampUserName")
            fltUser.ConcurrencyId = DalUtility.ConvertRowVersionToString(myDataRecord("ConcurrencyId"))
            Return fltUser

        End Function
		
	End Class

End Namespace
