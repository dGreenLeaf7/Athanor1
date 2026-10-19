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

    Public Class WorkOrderInfoDB

        Public Shared Function GetList(ByVal Progid As Integer, ByVal AircraftId As Integer, ByVal AircraftTextMatch As String, ByVal LocationId As Integer, ByVal MechanicId As Integer, ByVal WorkOrderNbr As String, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer) As WorkOrderInfoList

            Dim tempList As New WorkOrderInfoList

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction()

                        Try
                            tempList = GetList(Progid, AircraftId, AircraftTextMatch, LocationId, MechanicId, WorkOrderNbr, IsDeletedFilter, RowStart, RowCount, TotalRows, myDbConnection, myDbTransaction)
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

        Public Shared Function GetList(ByVal Progid As Integer, ByVal AircraftId As Integer, ByVal AircraftTextMatch As String, ByVal LocationId As Integer, ByVal MechanicId As Integer, ByVal WorkOrderNbr As String, ByVal IsDeletedFilter As Integer, ByVal RowStart As Integer, ByVal RowCount As Integer, ByRef TotalRows As Integer, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction) As WorkOrderInfoList

            Dim tempList As New WorkOrderInfoList

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "listWorkOrderInfos"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.AddWithValue("@ProgId", Progid)
            myDbCommand.Parameters.AddWithValue("@AircraftId", AircraftId)
            myDbCommand.Parameters.AddWithValue("@AircraftTextMatch", AircraftTextMatch)
            myDbCommand.Parameters.AddWithValue("@LocationId", LocationId)
            myDbCommand.Parameters.AddWithValue("@MechanicId", MechanicId)
            myDbCommand.Parameters.AddWithValue("@WorkOrderNbr", WorkOrderNbr)
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

        Public Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As WorkOrderInfo

            Dim WorkOrderInfo As WorkOrderInfo = New WorkOrderInfo()
            WorkOrderInfo.WorkOrderId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("WorkOrderId"))
            WorkOrderInfo.AircraftId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("AircraftId"))
            WorkOrderInfo.MSN = myDataRecord.GetString(myDataRecord.GetOrdinal("MSN"))
            WorkOrderInfo.ShipNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("ShipNbr"))
            WorkOrderInfo.SerialNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("SerialNbr"))
            WorkOrderInfo.RegistrationNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("RegistrationNbr"))
            WorkOrderInfo.LocationId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("LocationId"))
            WorkOrderInfo.LocationName = myDataRecord.GetString(myDataRecord.GetOrdinal("LocationName"))
            WorkOrderInfo.WorkOrderNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("WorkOrderNbr"))
            WorkOrderInfo.FISComplaintNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("FISComplaintNbr"))
            WorkOrderInfo.SquawkNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("SquawkNbr"))
            WorkOrderInfo.RequestDate = myDataRecord.GetString(myDataRecord.GetOrdinal("RequestDate"))
            WorkOrderInfo.DueDate = myDataRecord.GetString(myDataRecord.GetOrdinal("DueDate"))
            WorkOrderInfo.FinishDate = myDataRecord.GetString(myDataRecord.GetOrdinal("FinishDate"))
            WorkOrderInfo.WorkOrderStatusId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("WorkOrderStatusId"))
            WorkOrderInfo.WorkOrderStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("WorkOrderStatus"))
            WorkOrderInfo.WorkOrderStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("WorkOrderStatus"))
            WorkOrderInfo.IsDeleted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsDeleted"))
            Return WorkOrderInfo

        End Function

    End Class


End Namespace
