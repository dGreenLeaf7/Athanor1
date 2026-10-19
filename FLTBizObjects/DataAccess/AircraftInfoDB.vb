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

    Public Class AircraftInfoDB

        Public Shared Function GetList(ByVal ProgId As Integer, ByVal CurrentLocationId As Integer, ByVal AircraftTextMatch As String, ByVal IsActiveFilter As Integer, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As AircraftInfoList

            Dim tempList As New AircraftInfoList

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listAircraftInfos"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@ProgId", ProgId)
            myDbCommand.Parameters.AddWithValue("@CurrentLocationId", CurrentLocationId)
            myDbCommand.Parameters.AddWithValue("@AircraftTextMatch", AircraftTextMatch)
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

        Friend Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As AircraftInfo

            Dim aircraftInfo As AircraftInfo = New AircraftInfo()
            aircraftInfo.AircraftId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("AircraftId"))
            aircraftInfo.CurrentLocationName = myDataRecord.GetString(myDataRecord.GetOrdinal("CurrentLocationName"))
            aircraftInfo.ModelName = myDataRecord.GetString(myDataRecord.GetOrdinal("ModelName"))
            aircraftInfo.MSN = myDataRecord.GetString(myDataRecord.GetOrdinal("MSN"))
            aircraftInfo.ShipNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("ShipNbr"))
            aircraftInfo.SerialNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("SerialNbr"))
            aircraftInfo.Description = myDataRecord.GetString(myDataRecord.GetOrdinal("Description"))
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("CurrentLocationId")) Then
                aircraftInfo.CurrentLocationId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("CurrentLocationId"))
            End If
            aircraftInfo.RegistrationNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("RegistrationNbr"))
                aircraftInfo.LiveryColor = myDataRecord.GetString(myDataRecord.GetOrdinal("LiveryColor"))
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("AirframeHoursStart")) Then
                aircraftInfo.AirframeHoursStart = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("AirframeHoursStart")), "#,##0.0")
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("AirframeHoursCurrent")) Then
                aircraftInfo.AirframeHoursCurrent = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("AirframeHoursCurrent")), "#,##0.0")
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("AirframeHoursOffset")) Then
                aircraftInfo.AirframeHoursOffset = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("AirframeHoursOffset")), "#,##0.0")
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("MeterUnitId")) Then
                aircraftInfo.AirframeHoursUnitId = Format(myDataRecord.GetInt32(myDataRecord.GetOrdinal("MeterUnitId")), "#,##0.0")
            End If
            aircraftInfo.TotalRequiredUsage = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("TotalRequiredUsage")), "#,##0.0")
            aircraftInfo.CurrentUsage = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("CurrentUsage")), "#,##0.0")
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("EstEndOfTesting")) Then
                aircraftInfo.EstEndOfTesting = Format(myDataRecord.GetDateTime(myDataRecord.GetOrdinal("EstEndOfTesting")), "MM-dd-yyyy")
            End If
            If Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("ActEndOfTesting")) Then
                aircraftInfo.ActEndOfTesting = Format(myDataRecord.GetDateTime(myDataRecord.GetOrdinal("ActEndOfTesting")), "MM-dd-yyyy")
            End If
            aircraftInfo.AircraftStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("AircraftStatus"))
            aircraftInfo.AircraftEngineHoursCurrent = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("AircraftEngineHoursCurrent"))
            aircraftInfo.IsActive = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsActive"))
            aircraftInfo.IsDeleted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsDeleted"))
            aircraftInfo.UtcDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateCreated"))
            aircraftInfo.UtcDateModified = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateModified"))

            Return aircraftInfo

        End Function

    End Class

End Namespace
