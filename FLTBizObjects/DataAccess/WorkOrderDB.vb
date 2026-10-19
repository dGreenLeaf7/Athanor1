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

    Public Class WorkOrderDB

        Public Shared Function GetItem(ByVal WorkOrderId As Integer) As WorkOrder

            Dim newWorkOrder As WorkOrder = Nothing

            Using myDbConnection As SqlConnection = New SqlConnection()

                Try
                    myDbConnection.ConnectionString = AppConfiguration.connectionString
                    myDbConnection.Open()

                    Using myDbTransaction As SqlTransaction = myDbConnection.BeginTransaction

                        Dim totRows As Integer = 0

                        Try
                            newWorkOrder = GetItem(WorkOrderId, myDbConnection, myDbTransaction)
                            If newWorkOrder Is Nothing Then
                                myDbTransaction.Rollback()
                                Return Nothing
                            End If
                            newWorkOrder.WOReasonList = WOReasonDB.GetList(WorkOrderId, 0, -1, -1, totRows, myDbConnection, myDbTransaction)
                            newWorkOrder.WOInstructionList = WOInstructionDB.GetList(WorkOrderId, 0, -1, -1, totRows, myDbConnection, myDbTransaction)
                            newWorkOrder.WOPerformedWorkList = WOPerformedWorkDB.GetList(WorkOrderId, 0, -1, -1, totRows, myDbConnection, myDbTransaction)
                            newWorkOrder.WOPartList = WorkOrderPartDB.GetList(WorkOrderId, 0, -1, -1, totRows, myDbConnection, myDbTransaction)
                            newWorkOrder.WOEngineerCommentList = WOEngineerCommentDB.GetList(WorkOrderId, 0, -1, -1, totRows, myDbConnection, myDbTransaction)
                            newWorkOrder.WOMechanicCommentList = WOMechanicCommentDB.GetList(WorkOrderId, 0, -1, -1, totRows, myDbConnection, myDbTransaction)
                            newWorkOrder.WODocumentInfosList = DocumentWorkOrderXrefDB.ListDocumentInfosForWorkOrder(WorkOrderId, myDbConnection, myDbTransaction)
                            myDbTransaction.Commit()

                        Catch ex As ApplicationException
                            Throw New ApplicationException("Application encountered a data error.")

                        Catch ex As Exception
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

            Return newWorkOrder

        End Function

        Public Shared Function GetItem(ByVal WorkOrderId As Integer, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction) As WorkOrder

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "fetchWorkOrder"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.AddWithValue("@WorkOrderId", WorkOrderId)

            Try
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

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")

            End Try

        End Function

        Public Shared Sub Update(ByVal myWorkOrder As WorkOrder)

            Using myDbConn As SqlConnection = New SqlConnection()

                myDbConn.ConnectionString = AppConfiguration.connectionString

                Try

                    myDbConn.Open()

                    Using myDbTrans As SqlTransaction = myDbConn.BeginTransaction()

                        Try
                            _update(myWorkOrder, myDbConn, myDbTrans)

                            _processWOReasonList(myWorkOrder, myDbConn, myDbTrans)
                            _processWOReasonDeleteList(myWorkOrder, myDbConn, myDbTrans)

                            _processWOInstructionList(myWorkOrder, myDbConn, myDbTrans)
                            _processWOInstructionDeleteList(myWorkOrder, myDbConn, myDbTrans)

                            _processWOPerformedWorkList(myWorkOrder, myDbConn, myDbTrans)
                            _processWOPerformedWorkDeleteList(myWorkOrder, myDbConn, myDbTrans)

                            _processWOPartList(myWorkOrder, myDbConn, myDbTrans)
                            _processWOPartDeleteList(myWorkOrder, myDbConn, myDbTrans)

                            _processWOEngineerCommentList(myWorkOrder, myDbConn, myDbTrans)
                            _processWOEngineerCommentDeleteList(myWorkOrder, myDbConn, myDbTrans)

                            _processWOMechanicCommentList(myWorkOrder, myDbConn, myDbTrans)
                            _processWOMechanicCommentDeleteList(myWorkOrder, myDbConn, myDbTrans)

                            _processWODocumentInfosList(myWorkOrder, myDbConn, myDbTrans)
                            _processWODocumentInfosDeleteList(myWorkOrder, myDbConn, myDbTrans)

                            myDbTrans.Commit()

                        Catch ex As ApplicationException
                            myDbTrans.Rollback()
                            Throw New ApplicationException(ex.Message)

                        Catch myException As Exception
                            myDbTrans.Rollback()
                            Logger.Write(myException.Message)
                            Throw New ApplicationException("Application encountered a data error.")

                        End Try

                    End Using

                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)

                Catch ex As Exception
                    Logger.Write(ex.Message)
                    Throw New ApplicationException("Application encountered a data error.")

                Finally
                    If myDbConn.Region = ConnectionState.Open Then
                        myDbConn.Close()
                    End If

                End Try

            End Using

        End Sub

        Private Shared Sub _update(ByVal myWorkOrder As WorkOrder, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "updateWorkOrder"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.AddWithValue("@WorkOrderId", myWorkOrder.WorkOrderId)
            myDbCommand.Parameters.AddWithValue("@ProgId", myWorkOrder.ProgId)
            myDbCommand.Parameters.AddWithValue("@AircraftId", myWorkOrder.AircraftId)
            myDbCommand.Parameters.AddWithValue("@LocationId", myWorkOrder.LocationId)
            myDbCommand.Parameters.AddWithValue("@WorkOrderTypeId", myWorkOrder.WorkOrderTypeId)
            myDbCommand.Parameters.AddWithValue("@WorkOrderStatusId", myWorkOrder.WorkOrderStatusId)
            myDbCommand.Parameters.AddWithValue("@EngineerId", myWorkOrder.EngineerId)
            myDbCommand.Parameters.AddWithValue("@EngineeringActionId", myWorkOrder.EngineeringActionId)
            myDbCommand.Parameters.AddWithValue("@MechanicId", myWorkOrder.MechanicId)
            myDbCommand.Parameters.AddWithValue("@RequestDate", myWorkOrder.RequestDate)
            myDbCommand.Parameters.AddWithValue("@FaultOccurredDate", myWorkOrder.FaultOccurredDate)
            myDbCommand.Parameters.AddWithValue("@FaultOccurredMeter", CDec(myWorkOrder.FaultOccurredMeter))
            myDbCommand.Parameters.AddWithValue("@FISComplaintNbr", myWorkOrder.FISComplaintNbr)
            myDbCommand.Parameters.AddWithValue("@SquawkNbr", myWorkOrder.SquawkNbr)
            myDbCommand.Parameters.AddWithValue("@DueDate", myWorkOrder.DueDate)
            myDbCommand.Parameters.AddWithValue("@DueTime", myWorkOrder.DueTime)
            myDbCommand.Parameters.AddWithValue("@MeterStartWork", CDec(myWorkOrder.MeterStartWork))
            myDbCommand.Parameters.AddWithValue("@FinishDate", myWorkOrder.FinishDate)
            myDbCommand.Parameters.AddWithValue("@FinishTime", myWorkOrder.FinishTime)
            myDbCommand.Parameters.AddWithValue("@MeterEndWork", CDec(myWorkOrder.MeterEndWork))
            myDbCommand.Parameters.AddWithValue("@AircraftDispostion", myWorkOrder.AircraftDispostion)
            myDbCommand.Parameters.AddWithValue("@AircraftIssueAlert", myWorkOrder.AircraftIssueAlert)
            myDbCommand.Parameters.AddWithValue("@MaintenanceIssueAlert", myWorkOrder.MaintIssueAlert)
            myDbCommand.Parameters.AddWithValue("@IsDeleted", myWorkOrder.IsDeleted)
            myDbCommand.Parameters.AddWithValue("@StampUserName", myWorkOrder.StampUserName)
            myDbCommand.Parameters.Add(New SqlParameter("@ConcurrencyId", SqlDbType.Timestamp, 8))
            myDbCommand.Parameters("@ConcurrencyId").Value = DalUtility.ConvertStringToRowVersion(myWorkOrder.ConcurrencyId)
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

        Public Shared Function Insert(ByVal myWorkOrder As WorkOrder) As Integer

            Dim newId As Integer = 0

            Using myDbConn As SqlConnection = New SqlConnection()

                myDbConn.ConnectionString = AppConfiguration.connectionString

                Try

                    myDbConn.Open()

                    Using myDbTrans As SqlTransaction = myDbConn.BeginTransaction()

                        Try
                            newId = _insert(myWorkOrder, myDbConn, myDbTrans)
                            myWorkOrder.WorkOrderId = newId

                            _processWOReasonList(myWorkOrder, myDbConn, myDbTrans)

                            _processWOInstructionList(myWorkOrder, myDbConn, myDbTrans)

                            _processWOPerformedWorkList(myWorkOrder, myDbConn, myDbTrans)

                            _processWOPartList(myWorkOrder, myDbConn, myDbTrans)

                            _processWOEngineerCommentList(myWorkOrder, myDbConn, myDbTrans)

                            _processWOMechanicCommentList(myWorkOrder, myDbConn, myDbTrans)

                            _processWODocumentInfosList(myWorkOrder, myDbConn, myDbTrans)

                            myDbTrans.Commit()

                        Catch ex As ApplicationException
                            myDbTrans.Rollback()
                            Throw New ApplicationException(ex.Message)

                        Catch myException As Exception
                            myDbTrans.Rollback()
                            Logger.Write(myException.Message)
                            Throw New ApplicationException("Application encountered a data error.")

                        End Try

                    End Using

                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)

                Catch ex As Exception
                    Logger.Write(ex.Message)
                    Throw New ApplicationException("Application encountered a data error.")

                Finally
                    If myDbConn.Region = ConnectionState.Open Then
                        myDbConn.Close()
                    End If

                End Try

            End Using

            Return newId

        End Function

        Public Shared Function _insert(ByVal myWorkOrder As WorkOrder, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction) As Integer
            Dim newId As Integer = 0

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "addWorkOrder"
            myDbCommand.Connection = myDbConnection
            myDbCommand.Transaction = myDbTransaction

            myDbCommand.Parameters.AddWithValue("@ProgId", myWorkOrder.ProgId)
            myDbCommand.Parameters.AddWithValue("@AircraftId", myWorkOrder.AircraftId)
            myDbCommand.Parameters.AddWithValue("@LocationId", myWorkOrder.LocationId)
            myDbCommand.Parameters.AddWithValue("@WorkOrderTypeId", myWorkOrder.WorkOrderTypeId)
            myDbCommand.Parameters.AddWithValue("@WorkOrderStatusId", myWorkOrder.WorkOrderStatusId)
            myDbCommand.Parameters.AddWithValue("@EngineerId", myWorkOrder.EngineerId)
            myDbCommand.Parameters.AddWithValue("@EngineeringActionId", myWorkOrder.EngineeringActionId)
            myDbCommand.Parameters.AddWithValue("@MechanicId", myWorkOrder.MechanicId)
            myDbCommand.Parameters.AddWithValue("@RequestDate", myWorkOrder.RequestDate)
            myDbCommand.Parameters.AddWithValue("@FaultOccurredDate", myWorkOrder.FaultOccurredDate)
            myDbCommand.Parameters.AddWithValue("@FaultOccurredMeter", CDec(myWorkOrder.FaultOccurredMeter))
            myDbCommand.Parameters.AddWithValue("@FISComplaintNbr", myWorkOrder.FISComplaintNbr)
            myDbCommand.Parameters.AddWithValue("@SquawkNbr", myWorkOrder.SquawkNbr)
            myDbCommand.Parameters.AddWithValue("@DueDate", myWorkOrder.DueDate)
            myDbCommand.Parameters.AddWithValue("@DueTime", myWorkOrder.DueTime)
            myDbCommand.Parameters.AddWithValue("@MeterStartWork", CDec(myWorkOrder.MeterStartWork))
            myDbCommand.Parameters.AddWithValue("@FinishDate", myWorkOrder.FinishDate)
            myDbCommand.Parameters.AddWithValue("@FinishTime", myWorkOrder.FinishTime)
            myDbCommand.Parameters.AddWithValue("@MeterEndWork", CDec(myWorkOrder.MeterEndWork))
            myDbCommand.Parameters.AddWithValue("@AircraftDispostion", myWorkOrder.AircraftDispostion)
            myDbCommand.Parameters.AddWithValue("@AircraftIssueAlert", myWorkOrder.AircraftIssueAlert)
            myDbCommand.Parameters.AddWithValue("@MaintenanceIssueAlert", myWorkOrder.MaintIssueAlert)

            myDbCommand.Parameters.Add(New SqlParameter("@WorkOrderId", SqlDbType.Int))
            myDbCommand.Parameters("@WorkOrderID").Direction = ParameterDirection.Output
            myDbCommand.Parameters.AddWithValue("@NewConcurrencyId", 0)
            myDbCommand.Parameters("@NewConcurrencyId").Direction = ParameterDirection.Output
            myDbCommand.Parameters.AddWithValue("@StampUserName", myWorkOrder.StampUserName)
            myDbCommand.Parameters.Add(New SqlParameter("@NewUTCDate", SqlDbType.DateTime, 8))
            myDbCommand.Parameters("@NewUTCDate").Direction = ParameterDirection.Output

            Try
                myDbCommand.ExecuteNonQuery()
                newId = myDbCommand.Parameters("@WorkOrderID").Value

            Catch myException As Exception
                Logger.Write(myException, myDbCommand.CommandText)
                Throw New ApplicationException("Application encountered a data error.")
            End Try

            Return newId

        End Function

        Public Shared Sub Delete(ByVal WorkOrderId As Integer, ByVal StampUserName As String)

            Dim myDbConnection As SqlConnection = New SqlConnection()
            myDbConnection.ConnectionString = AppConfiguration.connectionString

            Dim myDbCommand As SqlCommand = New SqlCommand()
            myDbCommand.CommandType = CommandType.StoredProcedure
            myDbCommand.CommandText = "deleteWorkOrder"
            myDbCommand.Connection = myDbConnection

            myDbCommand.Parameters.AddWithValue("@WorkOrderId", WorkOrderId)
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

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As WorkOrder

            Dim workOrder As WorkOrder = New WorkOrder()
            workOrder.WorkOrderId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("WorkOrderId"))
            workOrder.WorkOrderNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("WorkOrderNbr"))
            workOrder.ProgId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ProgId"))
            workOrder.AircraftId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("AircraftId"))
            workOrder.MSN = myDataRecord.GetString(myDataRecord.GetOrdinal("MSN"))
            workOrder.TailNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("RegistrationNbr"))
            workOrder.LocationId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("LocationId"))
            workOrder.LocationName = myDataRecord.GetString(myDataRecord.GetOrdinal("LocationName"))
            workOrder.WorkOrderTypeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("WorkOrderTypeId"))
            workOrder.WorkOrderType = myDataRecord.GetString(myDataRecord.GetOrdinal("WorkOrderType"))
            workOrder.WorkOrderStatusId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("WorkOrderStatusId"))
            workOrder.WorkOrderStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("WorkorderStatus"))
            workOrder.EngineerId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("EngineerId"))
            workOrder.EngineerName = myDataRecord.GetString(myDataRecord.GetOrdinal("Engineer"))
            workOrder.EngineeringActionId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("EngineeringActionId"))
            workOrder.EngineeringAction = myDataRecord.GetString(myDataRecord.GetOrdinal("EngineeringAction"))
            workOrder.MechanicId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("MechanicId"))
            workOrder.MechanicName = myDataRecord.GetString(myDataRecord.GetOrdinal("Mechanic"))
            workOrder.RequestDate = myDataRecord.GetString(myDataRecord.GetOrdinal("RequestDate"))
            workOrder.FaultOccurredMeter = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("FaultOccurredMeter")), "#,##0.0")
            workOrder.FaultOccurredDate = myDataRecord.GetString(myDataRecord.GetOrdinal("FaultOccurredDate"))
            workOrder.DueDate = myDataRecord.GetString(myDataRecord.GetOrdinal("DueDate"))
            workOrder.DueTime = myDataRecord.GetString(myDataRecord.GetOrdinal("DueTime"))
            workOrder.MeterStartWork = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("MeterStartWork")), "#,##0.0")
            workOrder.FinishDate = myDataRecord.GetString(myDataRecord.GetOrdinal("FinishDate"))
            workOrder.FinishTime = myDataRecord.GetString(myDataRecord.GetOrdinal("FinishTime"))
            workOrder.MeterEndWork = Format(myDataRecord.GetDecimal(myDataRecord.GetOrdinal("MeterEndWork")), "#,##0.0")
            workOrder.AircraftDispostion = myDataRecord.GetString(myDataRecord.GetOrdinal("AircraftDispostion"))
            workOrder.FISComplaintNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("FisComplaintNbr"))
            workOrder.SquawkNbr = myDataRecord.GetString(myDataRecord.GetOrdinal("SquawkNbr"))
            workOrder.AircraftIssueAlert = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("AircraftIssueAlert"))
            workOrder.MaintIssueAlert = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("MaintenanceIssueAlert"))
            workOrder.IsDeleted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsDeleted"))
            workOrder.UtcDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateCreated"))
            workOrder.UtcDateModified = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("UTCDateModified"))
            workOrder.StampUserName = myDataRecord.GetString(myDataRecord.GetOrdinal("StampUserName"))
            workOrder.ConcurrencyId = DalUtility.ConvertRowVersionToString(myDataRecord("ConcurrencyId"))
            Return workOrder

        End Function

#Region "Private Child Business Object Data Access Procedures"

        Private Shared Sub _processWOReasonList(ByVal myWorkOrder As WorkOrder, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)
            Try
                For Each thisWOReason As WOReason In myWorkOrder.WOReasonList
                    thisWOReason.WorkOrderId = myWorkOrder.WorkOrderId
                    thisWOReason.StampUserName = myWorkOrder.StampUserName
                    If ((thisWOReason.ConcurrencyId = Nothing) Or (thisWOReason.ConcurrencyId = "0") Or (thisWOReason.ConcurrencyId = "0")) Then
                        WOReasonDB.Insert(thisWOReason, myDbConnection, myDbTransaction)
                    Else
                        WOReasonDB.Update(thisWOReason, myDbConnection, myDbTransaction)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub _processWOReasonDeleteList(ByVal myWorkOrder As WorkOrder, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)
            Try
                For Each thisWOReason As WOReason In myWorkOrder.WOReasonDeleteList
                    If thisWOReason.WOReasonId > 0 Then
                        WOReasonDB.Delete(thisWOReason.WOReasonId, myWorkOrder.StampUserName, myDbConnection, myDbTransaction)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub _processWOInstructionList(ByVal myWorkOrder As WorkOrder, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)
            Try
                For Each thisWOInstruction As WOInstruction In myWorkOrder.WOInstructionList
                    thisWOInstruction.WorkOrderId = myWorkOrder.WorkOrderId
                    thisWOInstruction.StampUserName = myWorkOrder.StampUserName
                    If ((thisWOInstruction.ConcurrencyId = Nothing) Or (thisWOInstruction.ConcurrencyId = "0") Or (thisWOInstruction.ConcurrencyId = "0")) Then
                        WOInstructionDB.Insert(thisWOInstruction, myDbConnection, myDbTransaction)
                    Else
                        WOInstructionDB.Update(thisWOInstruction, myDbConnection, myDbTransaction)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub _processWOInstructionDeleteList(ByVal myWorkOrder As WorkOrder, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)
            Try
                For Each thisWOInstruction As WOInstruction In myWorkOrder.WOInstructionDeleteList
                    If thisWOInstruction.WOInstructionsId > 0 Then
                        WOInstructionDB.Delete(thisWOInstruction.WOInstructionsId, myWorkOrder.StampUserName, myDbConnection, myDbTransaction)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub _processWOPerformedWorkList(ByVal myWorkOrder As WorkOrder, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)
            Try
                For Each thisWOPerformedWork As WOPerformedWork In myWorkOrder.WOPerformedWorkList
                    thisWOPerformedWork.WorkOrderId = myWorkOrder.WorkOrderId
                    thisWOPerformedWork.StampUserName = myWorkOrder.StampUserName
                    If ((thisWOPerformedWork.ConcurrencyId = Nothing) Or (thisWOPerformedWork.ConcurrencyId = "0") Or (thisWOPerformedWork.ConcurrencyId = "0")) Then
                        WOPerformedWorkDB.Insert(thisWOPerformedWork, myDbConnection, myDbTransaction)
                    Else
                        WOPerformedWorkDB.Update(thisWOPerformedWork, myDbConnection, myDbTransaction)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub _processWOPerformedWorkDeleteList(ByVal myWorkOrder As WorkOrder, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)
            Try
                For Each thisWOPerformedWork As WOPerformedWork In myWorkOrder.WOPerformedWorkDeleteList
                    If thisWOPerformedWork.PerformedWorkId > 0 Then
                        WOPerformedWorkDB.Delete(thisWOPerformedWork.PerformedWorkId, myWorkOrder.StampUserName, myDbConnection, myDbTransaction)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub _processWOPartList(ByVal myWorkOrder As WorkOrder, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)
            Try
                For Each thisWOPart As WorkOrderPart In myWorkOrder.WOPartList
                    thisWOPart.WorkOrderId = myWorkOrder.WorkOrderId
                    thisWOPart.StampUserName = myWorkOrder.StampUserName
                    If ((thisWOPart.ConcurrencyId = Nothing) Or (thisWOPart.ConcurrencyId = "0") Or (thisWOPart.ConcurrencyId = "0")) Then
                        WorkOrderPartDB.Insert(thisWOPart, myDbConnection, myDbTransaction)
                    Else
                        WorkOrderPartDB.Update(thisWOPart, myDbConnection, myDbTransaction)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub _processWOPartDeleteList(ByVal myWorkOrder As WorkOrder, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)
            Try
                For Each thisWOPart As WorkOrderPart In myWorkOrder.WOPartDeleteList
                    If thisWOPart.WorkOrderPartId > 0 Then
                        WorkOrderPartDB.Delete(thisWOPart.WorkOrderPartId, myWorkOrder.StampUserName, myDbConnection, myDbTransaction)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub _processWOEngineerCommentList(ByVal myWorkOrder As WorkOrder, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)
            Try
                For Each thisWOEngineerComment As WOEngineerComment In myWorkOrder.WOEngineerCommentList
                    thisWOEngineerComment.WorkOrderId = myWorkOrder.WorkOrderId
                    thisWOEngineerComment.StampUserName = myWorkOrder.StampUserName
                    If ((thisWOEngineerComment.ConcurrencyId = Nothing) Or (thisWOEngineerComment.ConcurrencyId = "0") Or (thisWOEngineerComment.ConcurrencyId = "0")) Then
                        WOEngineerCommentDB.Insert(thisWOEngineerComment, myDbConnection, myDbTransaction)
                    Else
                        WOEngineerCommentDB.Update(thisWOEngineerComment, myDbConnection, myDbTransaction)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub _processWOEngineerCommentDeleteList(ByVal myWorkOrder As WorkOrder, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)
            Try
                For Each thisWOEngineerComment As WOEngineerComment In myWorkOrder.WOEngineerCommentDeleteList
                    If thisWOEngineerComment.EngineerCommentsId > 0 Then
                        WOEngineerCommentDB.Delete(thisWOEngineerComment.EngineerCommentsId, myWorkOrder.StampUserName, myDbConnection, myDbTransaction)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub _processWOMechanicCommentList(ByVal myWorkOrder As WorkOrder, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)
            Try
                For Each thisWOMechanicComment As WOMechanicComment In myWorkOrder.WOMechanicCommentList
                    thisWOMechanicComment.WorkOrderId = myWorkOrder.WorkOrderId
                    thisWOMechanicComment.StampUserName = myWorkOrder.StampUserName
                    If ((thisWOMechanicComment.ConcurrencyId = Nothing) Or (thisWOMechanicComment.ConcurrencyId = "0") Or (thisWOMechanicComment.ConcurrencyId = "0")) Then
                        WOMechanicCommentDB.Insert(thisWOMechanicComment, myDbConnection, myDbTransaction)
                    Else
                        WOMechanicCommentDB.Update(thisWOMechanicComment, myDbConnection, myDbTransaction)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub _processWOMechanicCommentDeleteList(ByVal myWorkOrder As WorkOrder, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)
            Try
                For Each thisWOMechanicComment As WOMechanicComment In myWorkOrder.WOMechanicCommentDeleteList
                    If thisWOMechanicComment.WOMechanicCommentsId > 0 Then
                        WOMechanicCommentDB.Delete(thisWOMechanicComment.WOMechanicCommentsId, myWorkOrder.StampUserName, myDbConnection, myDbTransaction)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub _processWODocumentInfosList(ByVal myWorkOrder As WorkOrder, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)
            Try
                For Each thisDocumentInfo As DocumentInfo In myWorkOrder.WODocumentInfosList
                    thisDocumentInfo.StampUserName = myWorkOrder.StampUserName
                    DocumentInfoDB.Update(thisDocumentInfo)
                    If Not DocumentWorkOrderXrefDB.Exists(thisDocumentInfo.DocumentId, myWorkOrder.WorkOrderId) Then
                        DocumentWorkOrderXrefDB.Insert(thisDocumentInfo.DocumentId, myWorkOrder.WorkOrderId, myWorkOrder.StampUserName, myDbConnection, myDbTransaction)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub _processWODocumentInfosDeleteList(ByVal myWorkOrder As WorkOrder, ByVal myDbConnection As SqlConnection, ByVal myDbTransaction As SqlTransaction)
            Try
                For Each thisDocumentInfo As DocumentInfo In myWorkOrder.WODocumentInfosDeleteList
                    DocumentWorkOrderXrefDB.Delete(thisDocumentInfo.DocumentId, myWorkOrder.WorkOrderId, myWorkOrder.StampUserName, myDbConnection, myDbTransaction)
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

#End Region
    End Class

End Namespace
