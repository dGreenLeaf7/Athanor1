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

	Public Class AircraftConfigurationDB
		
        Public Shared Function GetItem(ByVal AircraftConfigurationId As Integer) As AircraftConfiguration

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "fetchAircraftConfiguration"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@AircraftConfigurationId", AircraftConfigurationId)
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
		
        Public Shared Function GetList(ByVal AircraftId As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As AircraftConfigurationList

            Dim tempList As New AircraftConfigurationList

            Using myDbConnection As SqlConnection = New SqlConnection()
                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            tempList = GetList(AircraftId, IsDeletedFilter, RowStart, RowCount, TotalRows, myDbConnection, myDbTransaction)
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

        Public Shared Function GetList(ByVal AircraftId As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction) As AircraftConfigurationList

            Dim tempList As New AircraftConfigurationList

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listAircraftConfigurations"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.AddWithValue("@AircraftId", AircraftId)
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

        Public Shared Sub Update(ByVal myAircraftConfiguration As AircraftConfiguration)

            Using myDbConnection As SqlConnection = New SqlConnection()
                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            Update(myAircraftConfiguration, myDbConnection, myDbTransaction)
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

        Public Shared Sub Update(ByVal myAircraftConfiguration As AircraftConfiguration, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "updateAircraftConfiguration"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.Add(New SqlParameter("@AircraftConfigurationId", SqlDbType.Int))
            myDbCommand.Parameters("@AircraftConfigurationId").Value = myAircraftConfiguration.AircraftConfigurationId
            myDbCommand.Parameters.Add(New SqlParameter("@AircraftId", SqlDbType.Int))
            myDbCommand.Parameters("@AircraftId").Value = myAircraftConfiguration.AircraftId
            myDbCommand.Parameters.Add(New SqlParameter("@ConfigurationDescription", SqlDbType.NVarChar, 255))
            myDbCommand.Parameters("@ConfigurationDescription").Value = myAircraftConfiguration.ConfigurationDescription
            myDbCommand.Parameters.Add("@ConfigurationStartDate", SqlDbType.DateTime)
            If myAircraftConfiguration.ConfigurationStartDate = Nothing Then
                myDbCommand.Parameters("@ConfigurationStartDate").Value = Nothing
            Else
                myDbCommand.Parameters("@ConfigurationStartDate").Value = CDate(myAircraftConfiguration.ConfigurationStartDate)
            End If
            myDbCommand.Parameters.Add("@ConfigurationEndDate", SqlDbType.DateTime)
            If myAircraftConfiguration.ConfigurationEndDate = Nothing Then
                myDbCommand.Parameters("@ConfigurationEndDate").Value = Nothing
            Else
                myDbCommand.Parameters("@ConfigurationEndDate").Value = CDate(myAircraftConfiguration.ConfigurationEndDate)
            End If
            myDbCommand.Parameters.Add(New SqlParameter("@ConfigurationMeterStart", SqlDbType.Decimal))
            myDbCommand.Parameters("@ConfigurationMeterStart").Value = CDec(myAircraftConfiguration.ConfigurationMeterStart)
            myDbCommand.Parameters.Add(New SqlParameter("@ConfigurationMeterEnd", SqlDbType.Decimal))
            myDbCommand.Parameters("@ConfigurationMeterEnd").Value = CDec(myAircraftConfiguration.ConfigurationMeterEnd)
            myDbCommand.Parameters.Add(New SqlParameter("@ConfigurationMeterOffset", SqlDbType.Decimal))
            myDbCommand.Parameters("@ConfigurationMeterOffset").Value = CDec(myAircraftConfiguration.ConfigurationMeterOffset)
            myDbCommand.Parameters.Add(New SqlParameter("@MeterUnitId", SqlDbType.Int))
            myDbCommand.Parameters("@MeterUnitId").Value = myAircraftConfiguration.MeterUnitId
            myDbCommand.Parameters.Add(New SqlParameter("@ModelId", SqlDbType.Int))
            myDbCommand.Parameters("@ModelId").Value = myAircraftConfiguration.ModelId
            myDbCommand.Parameters.Add(New SqlParameter("@EngineTypeId", SqlDbType.Int))
            myDbCommand.Parameters("@EngineTypeId").Value = myAircraftConfiguration.EngineTypeId
            myDbCommand.Parameters.Add(New SqlParameter("@EngineEmissionId", SqlDbType.Int))
            myDbCommand.Parameters("@EngineEmissionId").Value = myAircraftConfiguration.EngineEmissionId
            myDbCommand.Parameters.Add(New SqlParameter("@EngineMaturityId", SqlDbType.Int))
            myDbCommand.Parameters("@EngineMaturityId").Value = myAircraftConfiguration.EngineMaturityId
            myDbCommand.Parameters.Add(New SqlParameter("@EngineSerialNbr", SqlDbType.NVarChar, 20))
            myDbCommand.Parameters("@EngineSerialNbr").Value = myAircraftConfiguration.EngineSerialNbr
            myDbCommand.Parameters.Add(New SqlParameter("@APUTypeId", SqlDbType.Int))
            myDbCommand.Parameters("@APUTypeId").Value = myAircraftConfiguration.APUTypeId
            myDbCommand.Parameters.Add(New SqlParameter("@APUEmissionId", SqlDbType.Int))
            myDbCommand.Parameters("@APUEmissionId").Value = myAircraftConfiguration.APUEmissionId
            myDbCommand.Parameters.Add(New SqlParameter("@APUMaturityId", SqlDbType.Int))
            myDbCommand.Parameters("@APUMaturityId").Value = myAircraftConfiguration.APUMaturityId
            myDbCommand.Parameters.Add(New SqlParameter("@APUSerialNbr", SqlDbType.NVarChar, 30))
            myDbCommand.Parameters("@APUSerialNbr").Value = myAircraftConfiguration.APUSerialNbr
            myDbCommand.Parameters.Add(New SqlParameter("@NoseGear", SqlDbType.NVarChar, 30))
            myDbCommand.Parameters("@NoseGear").Value = myAircraftConfiguration.NoseGear
            myDbCommand.Parameters.Add(New SqlParameter("@MainGear", SqlDbType.NVarChar, 30))
            myDbCommand.Parameters("@MainGear").Value = myAircraftConfiguration.MainGear
            myDbCommand.Parameters.Add(New SqlParameter("@PropellerTypeId", SqlDbType.Int))
            myDbCommand.Parameters("@PropellerTypeId").Value = myAircraftConfiguration.PropellerTypeId
            myDbCommand.Parameters.Add(New SqlParameter("@PropellerModelNbr", SqlDbType.NVarChar, 20))
            myDbCommand.Parameters("@PropellerModelNbr").Value = myAircraftConfiguration.PropellerModelNbr
            myDbCommand.Parameters.Add(New SqlParameter("@PropellerSerialNbr", SqlDbType.NVarChar, 20))
            myDbCommand.Parameters("@PropellerSerialNbr").Value = myAircraftConfiguration.PropellerSerialNo
            myDbCommand.Parameters.Add(New SqlParameter("@AvionicsVersion", SqlDbType.NVarChar, 20))
            myDbCommand.Parameters("@AvionicsVersion").Value = myAircraftConfiguration.AvionicsVersion
            myDbCommand.Parameters.Add(New SqlParameter("@Emissionlevel", SqlDbType.NVarChar, 20))
            myDbCommand.Parameters("@EmissionLevel").Value = myAircraftConfiguration.EmissionLevel
            myDbCommand.Parameters.Add(New SqlParameter("@StampUserName", SqlDbType.NVarChar, 20))
            myDbCommand.Parameters("@StampUserName").Value = myAircraftConfiguration.StampUserName
            myDbCommand.Parameters.Add(New SqlParameter("@ConcurrencyId", SqlDbType.Timestamp, 8))
            myDbCommand.Parameters("@ConcurrencyId").Value = DalUtility.ConvertStringToRowVersion(myAircraftConfiguration.ConcurrencyId)
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

        Public Shared Function Insert(ByVal myAircraftConfiguration As AircraftConfiguration) As Integer

            Dim newId As Integer = 0

            Using myDbConnection As SqlConnection = New SqlConnection()
                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            newId = Insert(myAircraftConfiguration, myDbConnection, myDbTransaction)
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

        Public Shared Function Insert(ByVal myAircraftConfiguration As AircraftConfiguration, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)

            Dim newId As Integer = 0

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "addAircraftConfiguration"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.Add(New SqlParameter("@AircraftId", SqlDbType.Int))
            myDbCommand.Parameters("@AircraftId").Value = myAircraftConfiguration.AircraftId
            myDbCommand.Parameters.Add(New SqlParameter("@ConfigurationDescription", SqlDbType.NVarChar, 255))
            myDbCommand.Parameters("@ConfigurationDescription").Value = myAircraftConfiguration.ConfigurationDescription
            myDbCommand.Parameters.Add("@ConfigurationStartDate", SqlDbType.DateTime)
            If myAircraftConfiguration.ConfigurationStartDate = Nothing Then
                myDbCommand.Parameters("@ConfigurationStartDate").Value = Nothing
            Else
                myDbCommand.Parameters("@ConfigurationStartDate").Value = CDate(myAircraftConfiguration.ConfigurationStartDate)
            End If
            myDbCommand.Parameters.Add("@ConfigurationEndDate", SqlDbType.DateTime)
            If myAircraftConfiguration.ConfigurationEndDate = Nothing Then
                myDbCommand.Parameters("@ConfigurationEndDate").Value = Nothing
            Else
                myDbCommand.Parameters("@ConfigurationEndDate").Value = CDate(myAircraftConfiguration.ConfigurationEndDate)
            End If
            myDbCommand.Parameters.Add(New SqlParameter("@ConfigurationMeterStart", SqlDbType.Decimal))
            myDbCommand.Parameters("@ConfigurationMeterStart").Value = CDec(myAircraftConfiguration.ConfigurationMeterStart)
            myDbCommand.Parameters.Add(New SqlParameter("@ConfigurationMeterEnd", SqlDbType.Decimal))
            myDbCommand.Parameters("@ConfigurationMeterEnd").Value = CDec(myAircraftConfiguration.ConfigurationMeterEnd)
            myDbCommand.Parameters.Add(New SqlParameter("@ConfigurationMeterOffset", SqlDbType.Decimal))
            myDbCommand.Parameters("@ConfigurationMeterOffset").Value = CDec(myAircraftConfiguration.ConfigurationMeterOffset)
            myDbCommand.Parameters.Add(New SqlParameter("@MeterUnitId", SqlDbType.Int))
            myDbCommand.Parameters("@MeterUnitId").Value = myAircraftConfiguration.MeterUnitId
            myDbCommand.Parameters.Add(New SqlParameter("@ModelId", SqlDbType.Int))
            myDbCommand.Parameters("@ModelId").Value = myAircraftConfiguration.ModelId
            myDbCommand.Parameters.Add(New SqlParameter("@EngineTypeId", SqlDbType.Int))
            myDbCommand.Parameters("@EngineTypeId").Value = myAircraftConfiguration.EngineTypeId
            myDbCommand.Parameters.Add(New SqlParameter("@EngineEmissionId", SqlDbType.Int))
            myDbCommand.Parameters("@EngineEmissionId").Value = myAircraftConfiguration.EngineEmissionId
            myDbCommand.Parameters.Add(New SqlParameter("@EngineMaturityId", SqlDbType.Int))
            myDbCommand.Parameters("@EngineMaturityId").Value = myAircraftConfiguration.EngineMaturityId
            myDbCommand.Parameters.Add(New SqlParameter("@EngineSerialNbr", SqlDbType.NVarChar, 20))
            myDbCommand.Parameters("@EngineSerialNbr").Value = myAircraftConfiguration.EngineSerialNbr
            myDbCommand.Parameters.Add(New SqlParameter("@APUTypeId", SqlDbType.Int))
            myDbCommand.Parameters("@APUTypeId").Value = myAircraftConfiguration.APUTypeId
            myDbCommand.Parameters.Add(New SqlParameter("@APUEmissionId", SqlDbType.Int))
            myDbCommand.Parameters("@APUEmissionId").Value = myAircraftConfiguration.APUEmissionId
            myDbCommand.Parameters.Add(New SqlParameter("@APUMaturityId", SqlDbType.Int))
            myDbCommand.Parameters("@APUMaturityId").Value = myAircraftConfiguration.APUMaturityId
            myDbCommand.Parameters.Add(New SqlParameter("@APUSerialNbr", SqlDbType.NVarChar, 30))
            myDbCommand.Parameters("@APUSerialNbr").Value = myAircraftConfiguration.APUSerialNbr
            myDbCommand.Parameters.Add(New SqlParameter("@NoseGear", SqlDbType.NVarChar, 30))
            myDbCommand.Parameters("@NoseGear").Value = myAircraftConfiguration.NoseGear
            myDbCommand.Parameters.Add(New SqlParameter("@MainGear", SqlDbType.NVarChar, 30))
            myDbCommand.Parameters("@MainGear").Value = myAircraftConfiguration.MainGear
            myDbCommand.Parameters.Add(New SqlParameter("@AvionicsVersion", SqlDbType.NVarChar, 20))
            myDbCommand.Parameters("@AvionicsVersion").Value = myAircraftConfiguration.AvionicsVersion
            myDbCommand.Parameters.Add(New SqlParameter("@Emissionlevel", SqlDbType.NVarChar, 20))
            myDbCommand.Parameters("@EmissionLevel").Value = myAircraftConfiguration.EmissionLevel
            myDbCommand.Parameters.Add(New SqlParameter("@PropellerTypeId", SqlDbType.Int))
            myDbCommand.Parameters("@PropellerTypeId").Value = myAircraftConfiguration.PropellerTypeId
            myDbCommand.Parameters.Add(New SqlParameter("@PropellerModelNbr", SqlDbType.NVarChar, 20))
            myDbCommand.Parameters("@PropellerModelNbr").Value = myAircraftConfiguration.PropellerModelNbr
            myDbCommand.Parameters.Add(New SqlParameter("@PropellerSerialNbr", SqlDbType.NVarChar, 20))
            myDbCommand.Parameters("@PropellerSerialNbr").Value = myAircraftConfiguration.PropellerSerialNo
            myDbCommand.Parameters.AddWithValue("@NewConcurrencyId", 0)
            myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.AddWithValue("@StampUserName", myAircraftConfiguration.StampUserName)
            myDbCommand.Parameters.Add(New SqlParameter("@AircraftConfigurationId", SqlDbType.Int))
            myDbCommand.Parameters("@AircraftConfigurationId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.Add(New SqlParameter("@NewUTCDate", SqlDbType.DateTime, 8))
            myDbCommand.Parameters("@NewUTCDate").Direction = ParameterDirection.Output

            Try
                myDbCommand.ExecuteNonQuery()

                newId = myDbCommand.Parameters("@AircraftConfigurationId").Value

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try

            Return newId

        End Function

        Public Shared Sub Delete(ByVal AircraftConfigurationId As Integer, ByVal StampUserName As String)

            Using myDbConnection As SqlConnection = New SqlConnection()
                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            Delete(AircraftConfigurationId, StampUserName, myDbConnection, myDbTransaction)
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

        Public Shared Sub Delete(ByVal AircraftConfigurationId As Integer, ByVal StampUserName As String, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "deleteAircraftConfiguration"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.AddWithValue("@AircraftConfigurationId", AircraftConfigurationId)
            myDbCommand.Parameters.AddWithValue("@StampUserName", StampUserName)
            Try
                myDbCommand.ExecuteNonQuery()
            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try
        End Sub

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As AircraftConfiguration

            Dim aircraftConfiguration As AircraftConfiguration = New AircraftConfiguration()
            aircraftConfiguration.AircraftConfigurationId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("AircraftConfigurationId"))
            aircraftConfiguration.AircraftId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("AircraftId"))
            aircraftConfiguration.ConfigurationDescription = myDataRecord.GetString(myDataRecord.GetOrdinal("ConfigurationDescription"))
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("ConfigurationStartDate")) Then
                aircraftConfiguration.ConfigurationStartDate = Format(myDataRecord.GetDateTime(myDataRecord.GetOrdinal("ConfigurationStartDate")), "MM-dd-yyyy")
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("ConfigurationEndDate")) Then
                aircraftConfiguration.ConfigurationEndDate = Format(myDataRecord.GetDateTime(myDataRecord.GetOrdinal("ConfigurationEndDate")), "MM-dd-yyyy")
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("ConfigurationMeterStart")) Then
                aircraftConfiguration.ConfigurationMeterStart = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("ConfigurationMeterStart")), "#,##0.0")
            Else
                aircraftConfiguration.ConfigurationMeterStart = "0.0"
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("ConfigurationMeterEnd")) Then
                aircraftConfiguration.ConfigurationMeterEnd = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("ConfigurationMeterEnd")), "#,#0.0")
            Else
                aircraftConfiguration.ConfigurationMeterEnd = "0.0"
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("ConfigurationMeterOffset")) Then
                aircraftConfiguration.ConfigurationMeterOffset = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("ConfigurationMeterOffset")), "#,#0.0")
            Else
                aircraftConfiguration.ConfigurationMeterOffset = "0.0"
            End If
            aircraftConfiguration.MeterUnitId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("MeterUnitId"))
            aircraftConfiguration.MeterUnit = myDataRecord.GetString(myDataRecord.GetOrdinal("MeterUnit"))
            aircraftConfiguration.ModelId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ModelId"))
            aircraftConfiguration.ModelDescr = myDataRecord.GetString(myDataRecord.GetOrdinal("ModelName"))
            aircraftConfiguration.EngineTypeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("EngineTypeId"))
            aircraftConfiguration.EngineType = myDataRecord.GetString(myDataRecord.GetOrdinal("EngineType"))
            aircraftConfiguration.EngineEmissionId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("EngineEmissionId"))
            aircraftConfiguration.EngineEmission = myDataRecord.GetString(myDataRecord.GetOrdinal("EngineEmission"))
            aircraftConfiguration.EngineMaturityId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("EngineMaturityId"))
            aircraftConfiguration.EngineMaturity = myDataRecord.GetString(myDataRecord.GetOrdinal("EngineMaturity"))
            aircraftConfiguration.EngineSerialNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("EngineSerialNbr"))
            aircraftConfiguration.APUTypeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("APUTypeId"))
            aircraftConfiguration.APUType = myDataRecord.GetString(myDataRecord.GetOrdinal("APUType"))
            aircraftConfiguration.APUEmissionId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("APUEmissionId"))
            aircraftConfiguration.APUEmission = myDataRecord.GetString(myDataRecord.GetOrdinal("APUEmission"))
            aircraftConfiguration.APUMaturityId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("APUMaturityId"))
            aircraftConfiguration.APUMaturity = myDataRecord.GetString(myDataRecord.GetOrdinal("APUMaturity"))
            aircraftConfiguration.APUSerialNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("APUSerialNbr"))
            aircraftConfiguration.NoseGear = myDataRecord.GetString(myDataRecord.GetOrdinal("NoseGear"))
            aircraftConfiguration.MainGear = myDataRecord.GetString(myDataRecord.GetOrdinal("MainGear"))
            aircraftConfiguration.AvionicsVersion = myDataRecord.GetString(myDataRecord.GetOrdinal("AvionicsVersion"))
            aircraftConfiguration.EmissionLevel = myDataRecord.GetString(myDataRecord.GetOrdinal("EmissionLevel"))
            aircraftConfiguration.PropellerTypeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("PropellerTypeId"))
            aircraftConfiguration.PropellerType = myDataRecord.GetString(myDataRecord.GetOrdinal("PropellerType"))
            aircraftConfiguration.PropellerModelNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("PropellerModelNbr"))
            aircraftConfiguration.PropellerSerialNo = myDataRecord.GetString(myDataRecord.GetOrdinal("PropellerSerialNbr"))
            aircraftConfiguration.AircraftConfigurationGUID = myDataRecord.GetGuid(myDataRecord.GetOrdinal("AircraftConfigurationGUID"))
            aircraftConfiguration.IsDeleted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsDeleted"))
            aircraftConfiguration.UtcDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateCreated"))
            aircraftConfiguration.UtcDateModified = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateModified"))
            aircraftConfiguration.StampUserName = myDataRecord.GetString(myDataRecord.GetOrdinal("StampUserName"))
            aircraftConfiguration.ConcurrencyId = DalUtility.ConvertRowVersionToString(myDataRecord("ConcurrencyId"))
            Return aircraftConfiguration

        End Function

    End Class

End Namespace
