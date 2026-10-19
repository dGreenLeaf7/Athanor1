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

	Public Class AircraftLocationDB
		
        Public Shared Function GetItem(ByVal AircraftLocationId As Integer) As AircraftLocation

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "fetchAircraftLocation"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@AircraftLocationId", AircraftLocationId)
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
		
        Public Shared Function GetList(ByVal AircraftId As Integer, ByVal LocationId As Integer, ByVal IsActiveFilter As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As AircraftLocationList

            Dim tempList As New AircraftLocationList

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            tempList = GetList(AircraftId, LocationId, IsActiveFilter, IsDeletedFilter, RowStart, RowCount, TotalRows, myDbConnection, myDbTransaction)
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

        Public Shared Function GetList(ByVal AircraftId As Integer, ByVal LocationId As Integer, ByVal IsActiveFilter As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction) As AircraftLocationList

            Dim tempList As New AircraftLocationList

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listAircraftLocations"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.AddWithValue("@AircraftId", AircraftId)
            myDbCommand.Parameters.AddWithValue("@LocationId", LocationId)
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
                    myReader.Close()
                Catch myException As Exception
                    Logger.Write(myException, myDbCommand.CommandText)
                    Throw New ApplicationException("Application encountered a data error.")
                Finally
                    CType(myReader, IDisposable).Dispose()
                End Try

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try

            Return tempList

        End Function
		
        Public Shared Sub Update(ByVal myAircraftLocation As AircraftLocation)

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            Update(myAircraftLocation, myDbConnection, myDbTransaction)
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

        Public Shared Sub Update(ByVal myAircraftLocation As AircraftLocation, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "updateAircraftLocation"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.Add(New SqlParameter("@AircraftLocationId", SqlDbType.Int))
            myDbCommand.Parameters("@AircraftLocationId").Value = myAircraftLocation.AircraftLocationId
            myDbCommand.Parameters.Add(New SqlParameter("@AircraftId", SqlDbType.Int))
            myDbCommand.Parameters("@AircraftId").Value = myAircraftLocation.AircraftId
            myDbCommand.Parameters.Add(New SqlParameter("@LocationId", SqlDbType.Int))
            myDbCommand.Parameters("@LocationId").Value = myAircraftLocation.LocationId
            myDbCommand.Parameters.Add(New SqlParameter("@ArrivalDate", SqlDbType.DateTime))
            If IsDate(myAircraftLocation.ArrivalDate) Then
                myDbCommand.Parameters("@ArrivalDate").Value = CDate(myAircraftLocation.ArrivalDate)
            End If
            myDbCommand.Parameters.Add(New SqlParameter("@RequiredUsage", SqlDbType.Decimal))
            myDbCommand.Parameters("@RequiredUsage").Value = CDec(myAircraftLocation.RequiredUsage)
            myDbCommand.Parameters.Add(New SqlParameter("@AccumUsage", SqlDbType.Decimal))
            myDbCommand.Parameters("@AccumUsage").Value = myAircraftLocation.AccumUsage
            myDbCommand.Parameters.Add(New SqlParameter("@EstEndOfTest", SqlDbType.DateTime))
            If IsDate(myAircraftLocation.EstEndOfTest) Then
                myDbCommand.Parameters("@EstEndOfTest").Value = myAircraftLocation.EstEndOfTest
            End If
            myDbCommand.Parameters.Add(New SqlParameter("@ActEndOfTest", SqlDbType.DateTime))
            If IsDate(myAircraftLocation.ActEndOfTest) Then
                myDbCommand.Parameters("@ActEndOfTest").Value = myAircraftLocation.ActEndOfTest
            End If
            myDbCommand.Parameters.Add(New SqlParameter("@LastLocation", SqlDbType.Bit))
            myDbCommand.Parameters("@LastLocation").Value = myAircraftLocation.LastLocation
            myDbCommand.Parameters.Add(New SqlParameter("@IsActive", SqlDbType.Bit))
            myDbCommand.Parameters("@IsActive").Value = myAircraftLocation.IsActive
            myDbCommand.Parameters.Add(New SqlParameter("@StampUserName", SqlDbType.NVarChar, 20))
            myDbCommand.Parameters("@StampUserName").Value = myAircraftLocation.StampUserName
            myDbCommand.Parameters.Add(New SqlParameter("@ConcurrencyId", SqlDbType.Timestamp, 8))
            myDbCommand.Parameters("@ConcurrencyId").Value = DalUtility.ConvertStringToRowVersion(myAircraftLocation.ConcurrencyId)
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

        Public Shared Function Insert(ByVal myAircraftLocation As AircraftLocation) As Integer

            Dim newId As Integer = 0

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            newId = Insert(myAircraftLocation, myDbConnection, myDbTransaction)
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

            Return newId

        End Function

        Public Shared Function Insert(ByVal myAircraftLocation As AircraftLocation, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)

            Dim newID As Integer = 0

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "addAircraftLocation"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.Add(New SqlParameter("@AircraftId", SqlDbType.Int))
            myDbCommand.Parameters("@AircraftId").Value = myAircraftLocation.AircraftId
            myDbCommand.Parameters.Add(New SqlParameter("@LocationId", SqlDbType.Int))
            myDbCommand.Parameters("@LocationId").Value = myAircraftLocation.LocationId
            myDbCommand.Parameters.Add(New SqlParameter("@ArrivalDate", SqlDbType.DateTime))
            If IsDate(myAircraftLocation.ArrivalDate) Then
                myDbCommand.Parameters("@ArrivalDate").Value = CDate(myAircraftLocation.ArrivalDate)
            End If
            myDbCommand.Parameters.Add(New SqlParameter("@RequiredUsage", SqlDbType.Decimal))
            myDbCommand.Parameters("@RequiredUsage").Value = CDec(myAircraftLocation.RequiredUsage)
            myDbCommand.Parameters.Add(New SqlParameter("@AccumUsage", SqlDbType.Decimal))
            myDbCommand.Parameters("@AccumUsage").Value = myAircraftLocation.AccumUsage
            myDbCommand.Parameters.Add(New SqlParameter("@EstEndOfTest", SqlDbType.DateTime))
            If IsDate(myAircraftLocation.EstEndOfTest) Then
                myDbCommand.Parameters("@EstEndOfTest").Value = myAircraftLocation.EstEndOfTest
            End If
            myDbCommand.Parameters.Add(New SqlParameter("@ActEndOfTest", SqlDbType.DateTime))
            If IsDate(myAircraftLocation.ActEndOfTest) Then
                myDbCommand.Parameters("@ActEndOfTest").Value = myAircraftLocation.ActEndOfTest
            End If
            myDbCommand.Parameters.Add(New SqlParameter("@LastLocation", SqlDbType.Bit))
            myDbCommand.Parameters("@LastLocation").Value = myAircraftLocation.LastLocation
            myDbCommand.Parameters.AddWithValue("@AircraftLocationId", 0)
            myDbCommand.Parameters("@AircraftLocationId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.AddWithValue("@NewConcurrencyId", 0)
            myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.AddWithValue("@StampUserName", myAircraftLocation.StampUserName)
            myDbCommand.Parameters.Add(New SqlParameter("@NewUTCDate", SqlDbType.DateTime, 8))
            myDbCommand.Parameters("@NewUTCDate").Direction = ParameterDirection.Output

            Try
                myDbCommand.ExecuteNonQuery()

                newID = myDbCommand.Parameters("@AircraftLocationId").Value

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try

            Return newID

        End Function

        Public Shared Sub Delete(ByVal AircraftLocationId As Integer, ByVal StampUserName As String)
            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            Delete(AircraftLocationId, StampUserName, myDbConnection, myDbTransaction)
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

        Public Shared Sub Delete(ByVal AircraftLocationId As Integer, ByVal StampUserName As String, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)
            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "deleteAircraftLocation"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.AddWithValue("@AircraftLocationId", AircraftLocationId)
            myDbCommand.Parameters.AddWithValue("@StampUserName", StampUserName)
            Try
                myDbCommand.ExecuteNonQuery()
            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try
        End Sub

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As AircraftLocation

            Dim aircraftLocation As AircraftLocation = New AircraftLocation()
            aircraftLocation.AircraftLocationId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("AircraftLocationId"))
            aircraftLocation.AircraftId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("AircraftId"))
            aircraftLocation.SerialNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("SerialNbr"))
            aircraftLocation.RegistrationNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("RegistrationNbr"))
            aircraftLocation.MSN = myDataRecord.GetString(myDataRecord.GetOrdinal("MSN"))
            aircraftLocation.ShipNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("ShipNbr"))
            aircraftLocation.LocationId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("LocationId"))
            aircraftLocation.LocationName = myDataRecord.GetString(myDataRecord.GetOrdinal("LocationName"))
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("ArrivalDate")) Then
                aircraftLocation.ArrivalDate = Format(myDataRecord.GetDateTime(myDataRecord.GetOrdinal("ArrivalDate")), "MM-dd-yyyy")
            End If
            aircraftLocation.RequiredUsage = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("RequiredUsage")), "#,##0.0")
            aircraftLocation.AccumUsage = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("AccumUsage")), "#,##0.0")
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("EstEndOfTest")) Then
                aircraftLocation.EstEndOfTest = Format(myDataRecord.GetDateTime(myDataRecord.GetOrdinal("EstEndOfTest")), "MM-dd-yyyy")
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("ActEndOfTest")) Then
                aircraftLocation.ActEndOfTest = Format(myDataRecord.GetDateTime(myDataRecord.GetOrdinal("ActEndOfTest")), "MM-dd-yyyy")
            End If
            aircraftLocation.LastLocation = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("LastLocation"))
            aircraftLocation.AircraftLocationGUID = myDataRecord.GetGuid(myDataRecord.GetOrdinal("AircraftLocationGUID"))
            aircraftLocation.IsActive = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsActive"))
            aircraftLocation.IsDeleted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsDeleted"))
            aircraftLocation.UtcDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateCreated"))
            aircraftLocation.UtcDateModified = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateModified"))
            aircraftLocation.StampUserName = myDataRecord.GetString(myDataRecord.GetOrdinal("StampUserName"))
            aircraftLocation.ConcurrencyId = DalUtility.ConvertRowVersionToString(myDataRecord("ConcurrencyId"))
            Return aircraftLocation

        End Function

    End Class

End Namespace
