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

	Public Class ProgramDB
		
        Public Shared Function GetItem(ByVal ProgID As Integer) As Program

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "fetchProgram"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@ProgID", ProgID)
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
		
        Public Shared Function GetList(ByVal IsActiveFilter As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As ProgramList

            Dim tempList As ProgramList = Nothing

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listPrograms"
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
                        tempList = New ProgramList
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
		
		Public Shared Sub Update(ByVal myProgram As Program)
			
			Dim myDbConnection As SqlConnection = New SqlConnection()
			myDbConnection.ConnectionString = AppConfiguration.connectionString
			
			Dim myDbCommand As SqlCommand = New SqlCommand()
			myDbCommand.CommandType = CommandType.StoredProcedure
			myDbCommand.CommandText = "updateProgram"
			myDbCommand.Connection = myDbConnection
			
			myDbCommand.Parameters.AddWithValue("@ProgID", myProgram.ProgID)
			myDbCommand.Parameters.AddWithValue("@ProgName", myProgram.ProgName)
			myDbCommand.Parameters.AddWithValue("@ProgDescription", myProgram.ProgDescription)
			myDbCommand.Parameters.AddWithValue("@ProgVersion", myProgram.ProgVersion)
			myDbCommand.Parameters.AddWithValue("@ProgramLogo", myProgram.ProgramLogo)
			myDbCommand.Parameters.AddWithValue("@ReportLogo", myProgram.ReportLogo)
			myDbCommand.Parameters.AddWithValue("@YearLogo", myProgram.YearLogo)
			myDbCommand.Parameters.AddWithValue("@ProgStatus", myProgram.ProgStatus)
			myDbCommand.Parameters.AddWithValue("@DateOpened", myProgram.DateOpened)
			myDbCommand.Parameters.AddWithValue("@DateClosed", myProgram.DateClosed)
			myDbCommand.Parameters.AddWithValue("@ActualValue", myProgram.ActualValue)
			myDbCommand.Parameters.AddWithValue("@ProjectCode", myProgram.ProjectCode)
			myDbCommand.Parameters.AddWithValue("@ActivityCode", myProgram.ActivityCode)
			myDbCommand.Parameters.AddWithValue("@DeptCode", myProgram.DeptCode)
			myDbCommand.Parameters.AddWithValue("@CompanyName", myProgram.CompanyName)
			myDbCommand.Parameters.AddWithValue("@NearEndUsage", myProgram.NearEndUsage)
			myDbCommand.Parameters.AddWithValue("@AvgDailyUsage", myProgram.AvgDailyUsage)
			myDbCommand.Parameters.AddWithValue("@WeeklyFlightDays", myProgram.WeeklyFlightDays)
			myDbCommand.Parameters.AddWithValue("@ProgramGuid", myProgram.ProgramGuid)
			myDbCommand.Parameters.AddWithValue("@IsActive", myProgram.IsActive)
			myDbCommand.Parameters.AddWithValue("@StampUserName", myProgram.StampUserName)
			myDbCommand.Parameters.Add(new SqlParameter("@ConcurrencyId", SqlDbType.Timestamp, 8))
			myDbCommand.Parameters("@ConcurrencyId").Value = DalUtility.ConvertStringToRowVersion(myProgram.ConcurrencyId)
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
		
        Public Shared Function Insert(ByVal myProgram As Program) As Integer

            Dim newId As Integer = 0

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "addProgram"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@ProgName", myProgram.ProgName)
            myDbCommand.Parameters.AddWithValue("@ProgDescription", myProgram.ProgDescription)
            myDbCommand.Parameters.AddWithValue("@ProgVersion", myProgram.ProgVersion)
            myDbCommand.Parameters.AddWithValue("@ProgramLogo", myProgram.ProgramLogo)
            myDbCommand.Parameters.AddWithValue("@ReportLogo", myProgram.ReportLogo)
            myDbCommand.Parameters.AddWithValue("@YearLogo", myProgram.YearLogo)
            myDbCommand.Parameters.AddWithValue("@ProgStatus", myProgram.ProgStatus)
            myDbCommand.Parameters.AddWithValue("@DateOpened", myProgram.DateOpened)
            myDbCommand.Parameters.AddWithValue("@DateClosed", myProgram.DateClosed)
            myDbCommand.Parameters.AddWithValue("@ActualValue", myProgram.ActualValue)
            myDbCommand.Parameters.AddWithValue("@ProjectCode", myProgram.ProjectCode)
            myDbCommand.Parameters.AddWithValue("@ActivityCode", myProgram.ActivityCode)
            myDbCommand.Parameters.AddWithValue("@DeptCode", myProgram.DeptCode)
            myDbCommand.Parameters.AddWithValue("@CompanyName", myProgram.CompanyName)
            myDbCommand.Parameters.AddWithValue("@NearEndUsage", myProgram.NearEndUsage)
            myDbCommand.Parameters.AddWithValue("@AvgDailyUsage", myProgram.AvgDailyUsage)
            myDbCommand.Parameters.AddWithValue("@WeeklyFlightDays", myProgram.WeeklyFlightDays)
            myDbCommand.Parameters.Add(New SqlParameter("@ProgID", SqlDbType.Int))
            myDbCommand.Parameters("@ProgId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.Add(New SqlParameter("@NewConcurrencyId", SqlDbType.Int))
            myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.AddWithValue("@StampUserName", myProgram.StampUserName)
            myDbCommand.Parameters.Add(New SqlParameter("@NewUTCDate", SqlDbType.DateTime, 8))
            myDbCommand.Parameters("@NewUTCDate").Direction = ParameterDirection.Output

            Try
                myDbConnection.Open()
                myDbCommand.ExecuteNonQuery()
                myDbConnection.Close()

                newId = myDbCommand.Parameters("@ProgId").Value

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            Finally
                CType(myDbConnection, IDisposable).Dispose()
            End Try

            Return newId

        End Function
		
        Public Shared Sub Delete(ByVal ProgID As Integer, ByVal StampUserName As String)

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "deleteProgram"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@ProgID", ProgID)
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
		
		Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As Program
			
			Dim program As Program = New Program()
			program.ProgID = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ProgID"))
			program.ProgName = myDataRecord.GetString(myDataRecord.GetOrdinal("ProgName"))
			program.ProgDescription = myDataRecord.GetString(myDataRecord.GetOrdinal("ProgDescription"))
			program.ProgVersion = myDataRecord.GetString(myDataRecord.GetOrdinal("ProgVersion"))
			program.ProgramLogo = myDataRecord.GetString(myDataRecord.GetOrdinal("ProgramLogo"))
			program.ReportLogo = myDataRecord.GetString(myDataRecord.GetOrdinal("ReportLogo"))
			program.YearLogo = myDataRecord.GetString(myDataRecord.GetOrdinal("YearLogo"))
			program.ProgStatus = myDataRecord.GetByte(myDataRecord.GetOrdinal("ProgStatus"))
			program.DateOpened = myDataRecord.GetString(myDataRecord.GetOrdinal("DateOpened"))
			program.DateClosed = myDataRecord.GetString(myDataRecord.GetOrdinal("DateClosed"))
			program.ActualValue = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("ActualValue"))
			program.ProjectCode = myDataRecord.GetString(myDataRecord.GetOrdinal("ProjectCode"))
			program.ActivityCode = myDataRecord.GetString(myDataRecord.GetOrdinal("ActivityCode"))
			program.DeptCode = myDataRecord.GetString(myDataRecord.GetOrdinal("DeptCode"))
			program.CompanyName = myDataRecord.GetString(myDataRecord.GetOrdinal("CompanyName"))
			program.NearEndUsage = myDataRecord.GetInt32(myDataRecord.GetOrdinal("NearEndUsage"))
			program.AvgDailyUsage = myDataRecord.GetInt32(myDataRecord.GetOrdinal("AvgDailyUsage"))
			program.WeeklyFlightDays = myDataRecord.GetInt32(myDataRecord.GetOrdinal("WeeklyFlightDays"))
			program.ProgramGuid = myDataRecord.GetGuid(myDataRecord.GetOrdinal("ProgramGuid"))
			program.IsActive = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsActive"))
			program.IsDeleted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsDeleted"))
			program.UtcDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateCreated"))
			program.UtcDateModified = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateModified"))
            program.StampUserName = myDataRecord.GetString(myDataRecord.GetOrdinal("StampUserName"))
			program.ConcurrencyId = DalUtility.ConvertRowVersionToString(myDataRecord("ConcurrencyId"))
			Return program
			
		End Function
		
	End Class

End Namespace
