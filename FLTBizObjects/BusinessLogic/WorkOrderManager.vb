' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports Contoso.Utilities
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

	<DataObjectAttribute()> _
	Public Class WorkOrderManager

#Region "Parent Business Object Methods"

        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function GetItem(ByVal WorkOrderId As Integer, ByVal FltIdentity As FltIdentity) As WorkOrder
            Dim newWorkOrder As WorkOrder = Nothing
            Dim totRows As Integer = 0
            If CanView(FltIdentity) Then
                Try
                    newWorkOrder = WorkOrderDB.GetItem(WorkOrderId)

                Catch ex As Exception
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't fetch - user is not authorized")
            End If
            Return newWorkOrder
        End Function

        Public Shared Sub Save(ByVal WorkOrder As WorkOrder, ByVal FltIdentity As FltIdentity)
            If ((WorkOrder.ConcurrencyId = Nothing) Or (WorkOrder.ConcurrencyId = String.Empty) Or (WorkOrder.ConcurrencyId = "0")) Then
                Insert(WorkOrder, FltIdentity)
            Else
                Update(WorkOrder, FltIdentity)
            End If
        End Sub

        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal WorkOrder As WorkOrder, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                If WorkOrder.Validate() Then
                    WorkOrder.StampUserName = FltIdentity.UserName
                    Try
                        WorkOrderDB.Update(WorkOrder)

                    Catch ex As ApplicationException
                        Throw New ApplicationException(ex.Message)
                    End Try
                Else
                    Throw New ApplicationException("Can't update - record is invalid. Please correct errors and try again.")
                End If
            Else
                Throw New Exception("Can't update - user is not authorized")
            End If
        End Sub

        <DataObjectMethod(DataObjectMethodType.Insert, True)> _
        Public Shared Function Insert(ByVal WorkOrder As WorkOrder, ByVal FltIdentity As FltIdentity) As Integer
            Dim newID As Integer = 0
            Dim totRows As Integer = 0
            If CanEdit(FltIdentity) Then
                If WorkOrder.Validate() Then
                    WorkOrder.StampUserName = FltIdentity.UserName
                    Try
                        newID = WorkOrderDB.Insert(WorkOrder)
                        WorkOrder.WorkOrderId = newID

                        ProcessWOReasonList(WorkOrder.WOReasonList, WorkOrder.WorkOrderId, FltIdentity)
                        ProcessWOInstructionList(WorkOrder.WOInstructionList, WorkOrder.WorkOrderId, FltIdentity)
                        ProcessWOPerformedWorkList(WorkOrder.WOPerformedWorkList, WorkOrder.WorkOrderId, FltIdentity)
                        ProcessWOEngineerCommentList(WorkOrder.WOEngineerCommentList, WorkOrder.WorkOrderId, FltIdentity)
                        ProcessWOMechanicCommentList(WorkOrder.WOMechanicCommentList, WorkOrder.WorkOrderId, FltIdentity)
                        ProcessWOPartList(WorkOrder.WOPartList, WorkOrder.WorkOrderId, FltIdentity)
                        ProcessWODocumentInfosList(WorkOrder.WODocumentInfosList, WorkOrder.WorkOrderId, FltIdentity)

                    Catch ex As ApplicationException
                        Throw New ApplicationException(ex.Message)
                    End Try
                Else
                    Throw New ApplicationException("Can't insert - record is invalid. Please correct errors and try again.")
                End If
            Else
                Throw New Exception("Can't insert - user is not authorized")
            End If

            Return newID

        End Function

        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub Delete(ByVal WorkOrder As WorkOrder, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                Try








                    WorkOrderDB.Delete(WorkOrder.WorkOrderId, FltIdentity.UserName)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't delete - user is not authorized")
            End If
        End Sub

#End Region

#Region "Child Business Object Methods"

        Public Shared Sub InsertWOReason(ByVal Workorder As WorkOrder, ByVal WOReason As WOReason)
            If WOReason.Validate Then
                Try
                    WOReason.WOReasonId = Workorder.NewWoReasonId
                    Workorder.NewWoReasonId = Workorder.NewWoReasonId - 1
                    Workorder.WOReasonList.Add(WOReason)
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            Else
                Workorder.BrokenRules.Clear()
                For Each thisRule As BrokenRule In WOReason.BrokenRules
                    Workorder.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't insert - record is invalid. Please correct errors and try again.")
            End If
        End Sub

        Public Shared Sub RemoveWOReason(ByVal WorkOrder As WorkOrder, ByVal WOReasonId As Integer)
            Try
                For Each thisWOReason As WOReason In WorkOrder.WOReasonList
                    If thisWOReason.WOReasonId = WOReasonId Then
                        WorkOrder.WOReasonList.Remove(thisWOReason)
                        If WOReasonId > 0 Then
                            WorkOrder.WOReasonDeleteList.Add(thisWOReason)
                        End If
                        Exit Sub
                    End If
                Next
                Throw New Exception("Can't remove - record not found in collection!")
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Public Shared Sub UpdateWOReason(ByVal WorkOrder As WorkOrder, ByVal WOReason As WOReason)
            If WOReason.Validate() Then
                Try
                    For Each thisWoReason As WOReason In WorkOrder.WOReasonList
                        If WOReason.WOReasonId = thisWoReason.WOReasonId Then
                            WOReason.ConcurrencyId = thisWoReason.ConcurrencyId
                            WorkOrder.WOReasonList.Remove(thisWoReason)
                            WorkOrder.WOReasonList.Add(WOReason)
                            Exit Sub
                        End If
                    Next
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            Else
                WorkOrder.BrokenRules.Clear()
                For Each thisRule As BrokenRule In WOReason.BrokenRules
                    WorkOrder.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't update - record is invalid. Please correct errors and try again.")
            End If
        End Sub

        Public Shared Sub InsertWOInstruction(ByVal WorkOrder As WorkOrder, ByVal WOInstruction As WOInstruction)
            If WOInstruction.Validate() Then
                Try
                    WOInstruction.WOInstructionsId = WorkOrder.NewWoInstructionId
                    WorkOrder.NewWoInstructionId = WorkOrder.NewWoInstructionId - 1
                    WorkOrder.WOInstructionList.Add(WOInstruction)
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            Else
                WorkOrder.BrokenRules.Clear()
                For Each thisRule As BrokenRule In WOInstruction.BrokenRules
                    WorkOrder.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't insert - record is invalid. Please correct errors and try again.")
            End If
        End Sub

        Public Shared Sub RemoveWOInstruction(ByVal WorkOrder As WorkOrder, ByVal WOInstructionId As Integer)
            Try
                For Each thisWOInstruction As WOInstruction In WorkOrder.WOInstructionList
                    If thisWOInstruction.WOInstructionsId = WOInstructionId Then
                        WorkOrder.WOInstructionList.Remove(thisWOInstruction)
                        If WOInstructionId > 0 Then
                            WorkOrder.WOInstructionDeleteList.Add(thisWOInstruction)
                        End If
                        Exit Sub
                    End If
                Next
                Throw New Exception("Can't remove - record not found in collection!")
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Public Shared Sub UpdateWOInstruction(ByVal WorkOrder As WorkOrder, ByVal WOInstruction As WOInstruction)
            If WOInstruction.Validate() Then
                Try
                    For Each thisWoInstruction As WOInstruction In WorkOrder.WOInstructionList
                        If WOInstruction.WOInstructionsId = thisWoInstruction.WOInstructionsId Then
                            WOInstruction.ConcurrencyId = thisWoInstruction.ConcurrencyId
                            WorkOrder.WOInstructionList.Remove(thisWoInstruction)
                            WorkOrder.WOInstructionList.Add(WOInstruction)
                            Exit Sub
                        End If
                    Next
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            Else
                WorkOrder.BrokenRules.Clear()
                For Each thisRule As BrokenRule In WOInstruction.BrokenRules
                    WorkOrder.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't update - record is invalid. Please correct errors and try again.")
            End If
        End Sub

        Public Shared Sub InsertWOPerformedWork(ByVal WorkOrder As WorkOrder, ByVal WOPerformedWork As WOPerformedWork)
            If WOPerformedWork.Validate() Then
                Try
                    WOPerformedWork.PerformedWorkId = WorkOrder.NewWoPerformedWorkId
                    WorkOrder.NewWoPerformedWorkId = WorkOrder.NewWoPerformedWorkId - 1
                    WorkOrder.WOPerformedWorkList.Add(WOPerformedWork)
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            Else
                WorkOrder.BrokenRules.Clear()
                For Each thisRule As BrokenRule In WOPerformedWork.BrokenRules
                    WorkOrder.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't insert - record is invalid. Please correct errors and try again.")
            End If
        End Sub

        Public Shared Sub RemoveWOPerformedWork(ByVal WorkOrder As WorkOrder, ByVal WOPerformedWorkId As Integer)
            Try
                For Each thisWOPerformedWork As WOPerformedWork In WorkOrder.WOPerformedWorkList
                    If thisWOPerformedWork.PerformedWorkId = WOPerformedWorkId Then
                        WorkOrder.WOPerformedWorkList.Remove(thisWOPerformedWork)
                        If WOPerformedWorkId > 0 Then
                            WorkOrder.WOPerformedWorkDeleteList.Add(thisWOPerformedWork)
                        End If
                        Exit Sub
                    End If
                Next
                Throw New Exception("Can't remove - record not found in collection!")
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Public Shared Sub UpdateWOPerformedWork(ByVal WorkOrder As WorkOrder, ByVal WOPerformedWork As WOPerformedWork)
            If WOPerformedWork.Validate() Then
                Try
                    For Each thisWOPerformedWork As WOPerformedWork In WorkOrder.WOPerformedWorkList
                        If WOPerformedWork.PerformedWorkId = thisWOPerformedWork.PerformedWorkId Then
                            WOPerformedWork.ConcurrencyId = thisWOPerformedWork.ConcurrencyId
                            WorkOrder.WOPerformedWorkList.Remove(thisWOPerformedWork)
                            WorkOrder.WOPerformedWorkList.Add(WOPerformedWork)
                            Exit Sub
                        End If
                    Next
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            Else
                WorkOrder.BrokenRules.Clear()
                For Each thisRule As BrokenRule In WOPerformedWork.BrokenRules
                    WorkOrder.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't update - record is invalid. Please correct errors and try again.")
            End If
        End Sub

        Public Shared Sub InsertWorkOrderPart(ByVal WorkOrder As WorkOrder, ByVal WorkOrderPart As WorkOrderPart)
            If WorkOrderPart.Validate() Then
                Try
                    WorkOrderPart.WorkOrderPartId = WorkOrder.NewWoPartId
                    WorkOrder.NewWoPartId = WorkOrder.NewWoPartId - 1
                    WorkOrder.WOPartList.Add(WorkOrderPart)
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            Else
                WorkOrder.BrokenRules.Clear()
                For Each thisRule As BrokenRule In WorkOrderPart.BrokenRules
                    WorkOrder.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't insert - record is invalid. Please correct errors and try again.")
            End If
        End Sub

        Public Shared Sub RemoveWorkOrderPart(ByVal WorkOrder As WorkOrder, ByVal WorkOrderPartId As Integer)
            Try
                For Each thisWorkOrderPart As WorkOrderPart In WorkOrder.WOPartList
                    If thisWorkOrderPart.WorkOrderPartId = WorkOrderPartId Then
                        WorkOrder.WOPartList.Remove(thisWorkOrderPart)
                        If WorkOrderPartId > 0 Then
                            WorkOrder.WOPartDeleteList.Add(thisWorkOrderPart)
                        End If
                        Exit Sub
                    End If
                Next
                Throw New Exception("Can't remove - record not found in collection!")
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Public Shared Sub UpdateWorkOrderPart(ByVal WorkOrder As WorkOrder, ByVal WorkOrderPart As WorkOrderPart)
            If WorkOrderPart.Validate() Then
                Try
                    For Each thisWorkOrderPart As WorkOrderPart In WorkOrder.WOPartList
                        If WorkOrderPart.WorkOrderPartId = thisWorkOrderPart.WorkOrderPartId Then
                            WorkOrderPart.ConcurrencyId = thisWorkOrderPart.ConcurrencyId
                            WorkOrder.WOPartList.Remove(thisWorkOrderPart)
                            WorkOrder.WOPartList.Add(WorkOrderPart)
                            Exit Sub
                        End If
                    Next
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            Else
                WorkOrder.BrokenRules.Clear()
                For Each thisRule As BrokenRule In WorkOrderPart.BrokenRules
                    WorkOrder.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't update - record is invalid. Please correct errors and try again.")
            End If
        End Sub

        Public Shared Sub InsertWOEngineerComment(ByVal WorkOrder As WorkOrder, ByVal WOEngineerComment As WOEngineerComment)
            If WOEngineerComment.Validate() Then
                Try
                    WOEngineerComment.EngineerCommentsId = WorkOrder.NewWoEngineerCommentId
                    WorkOrder.NewWoEngineerCommentId = WorkOrder.NewWoEngineerCommentId - 1
                    WorkOrder.WOEngineerCommentList.Add(WOEngineerComment)
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            Else
                WorkOrder.BrokenRules.Clear()
                For Each thisRule As BrokenRule In WOEngineerComment.BrokenRules
                    WorkOrder.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't insert - record is invalid. Please correct errors and try again.")
            End If
        End Sub

        Public Shared Sub RemoveWOEngineerComment(ByVal WorkOrder As WorkOrder, ByVal WOEngineerCommentId As Integer)
            Try
                For Each thisWOEngineerComment As WOEngineerComment In WorkOrder.WOEngineerCommentList
                    If thisWOEngineerComment.EngineerCommentsId = WOEngineerCommentId Then
                        WorkOrder.WOEngineerCommentList.Remove(thisWOEngineerComment)
                        If WOEngineerCommentId > 0 Then
                            WorkOrder.WOEngineerCommentDeleteList.Add(thisWOEngineerComment)
                        End If
                        Exit Sub
                    End If
                Next
                Throw New Exception("Can't remove - record not found in collection!")
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Public Shared Sub UpdateWOEngineerComment(ByVal WorkOrder As WorkOrder, ByVal WOEngineerComment As WOEngineerComment)
            If WOEngineerComment.Validate() Then
                Try
                    For Each thisWOEngineerComment As WOEngineerComment In WorkOrder.WOEngineerCommentList
                        If WOEngineerComment.EngineerCommentsId = thisWOEngineerComment.EngineerCommentsId Then
                            WOEngineerComment.ConcurrencyId = thisWOEngineerComment.ConcurrencyId
                            WorkOrder.WOEngineerCommentList.Remove(thisWOEngineerComment)
                            WorkOrder.WOEngineerCommentList.Add(WOEngineerComment)
                            Exit Sub
                        End If
                    Next
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            Else
                WorkOrder.BrokenRules.Clear()
                For Each thisRule As BrokenRule In WOEngineerComment.BrokenRules
                    WorkOrder.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't update - record is invalid. Please correct errors and try again.")
            End If
        End Sub

        Public Shared Sub InsertWOMechanicComment(ByVal WorkOrder As WorkOrder, ByVal WOMechanicComment As WOMechanicComment)
            If WOMechanicComment.Validate() Then
                Try
                    WOMechanicComment.WOMechanicCommentsId = WorkOrder.NewWoMechanicCommentId
                    WorkOrder.NewWoMechanicCommentId = WorkOrder.NewWoMechanicCommentId - 1
                    WorkOrder.WOMechanicCommentList.Add(WOMechanicComment)
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            Else
                WorkOrder.BrokenRules.Clear()
                For Each thisRule As BrokenRule In WOMechanicComment.BrokenRules
                    WorkOrder.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't insert - record is invalid. Please correct errors and try again.")
            End If
        End Sub

        Public Shared Sub RemoveWOMechanicComment(ByVal WorkOrder As WorkOrder, ByVal WOMechanicCommentId As Integer)
            Try
                For Each thisWOMechanicComment As WOMechanicComment In WorkOrder.WOMechanicCommentList
                    If thisWOMechanicComment.WOMechanicCommentsId = WOMechanicCommentId Then
                        WorkOrder.WOMechanicCommentList.Remove(thisWOMechanicComment)
                        If WOMechanicCommentId > 0 Then
                            WorkOrder.WOMechanicCommentDeleteList.Add(thisWOMechanicComment)
                        End If
                        Exit Sub
                    End If
                Next
                Throw New Exception("Can't remove - record not found in collection!")
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Public Shared Sub UpdateWOMechanicComment(ByVal WorkOrder As WorkOrder, ByVal WOMechanicComment As WOMechanicComment)
            If WOMechanicComment.Validate() Then
                Try
                    For Each thisWOMechanicComment As WOMechanicComment In WorkOrder.WOMechanicCommentList
                        If WOMechanicComment.WOMechanicCommentsId = thisWOMechanicComment.WOMechanicCommentsId Then
                            WOMechanicComment.ConcurrencyId = thisWOMechanicComment.ConcurrencyId
                            WorkOrder.WOMechanicCommentList.Remove(thisWOMechanicComment)
                            WorkOrder.WOMechanicCommentList.Add(WOMechanicComment)
                            Exit Sub
                        End If
                    Next
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            Else
                WorkOrder.BrokenRules.Clear()
                For Each thisRule As BrokenRule In WOMechanicComment.BrokenRules
                    WorkOrder.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't update - record is invalid. Please correct errors and try again.")
            End If
        End Sub

        Public Shared Sub InsertDocument(ByVal WorkOrder As WorkOrder, ByVal DocumentInfo As DocumentInfo, ByVal DocumentStream As System.IO.Stream, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                If DocumentInfo.Validate Then
                    Try
                        Dim documentId As Integer = DocumentInfoDB.Insert(DocumentInfo)
                        PutDocumentStream(documentId, DocumentStream)
                        Dim newDocumentInfo As DocumentInfo = DocumentInfoDB.GetItem(documentId)
                        WorkOrder.WODocumentInfosList.Add(newDocumentInfo)
                    Catch ex As Exception
                        Logger.Write(ex)
                        Throw New Exception(ex.Message)
                    End Try
                Else
                    WorkOrder.BrokenRules.Clear()
                    For Each thisBrokenRule As BrokenRule In DocumentInfo.BrokenRules
                        WorkOrder.BrokenRules.Add(thisBrokenRule)
                    Next
                    Throw New Exception("Can't insert - record is invalid. Please correct errors and try again.")
                End If
            Else
                Throw New Exception("Can't insert new document - user is not authorized")
            End If
        End Sub

        Public Shared Sub RemoveDocument(ByVal WorkOrder As WorkOrder, ByVal DocumentInfo As DocumentInfo)
            Try
                For Each thisDocumentInfo As DocumentInfo In WorkOrder.WODocumentInfosList
                    If thisDocumentInfo.DocumentId = DocumentInfo.DocumentId Then
                        WorkOrder.WODocumentInfosList.Remove(thisDocumentInfo)
                        WorkOrder.WODocumentInfosDeleteList.Add(thisDocumentInfo)
                        Exit Sub
                    End If
                Next
                Throw New Exception("Can't remove - record not found in collection!")
            Catch ex As Exception
                Logger.Write(ex)
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Public Shared Sub UpdateDocument(ByVal WorkOrder As WorkOrder, ByVal DocumentInfo As DocumentInfo)
            If DocumentInfo.Validate() Then
                Try
                    For Each thisDocumentInfo As DocumentInfo In WorkOrder.WODocumentInfosList
                        If DocumentInfo.DocumentId = thisDocumentInfo.DocumentId Then
                            DocumentInfo.ProgId = thisDocumentInfo.ProgId
                            DocumentInfo.DocumentTypeId = thisDocumentInfo.DocumentTypeId
                            DocumentInfo.MimeType = thisDocumentInfo.MimeType
                            DocumentInfo.ConcurrencyId = thisDocumentInfo.ConcurrencyId
                            WorkOrder.WODocumentInfosList.Remove(thisDocumentInfo)
                            WorkOrder.WODocumentInfosList.Add(DocumentInfo)
                            Exit Sub
                        End If
                    Next
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            Else
                WorkOrder.BrokenRules.Clear()
                For Each thisRule As BrokenRule In DocumentInfo.BrokenRules
                    WorkOrder.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't update - record is invalid. Please correct errors and try again.")
            End If
        End Sub

        Public Shared Function GetDocumentStream(ByVal DocumentId As Integer) As System.IO.Stream
            Try
                Return DocumentDB.GetStream(DocumentId)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Private Shared Sub PutDocumentStream(ByVal DocumentId As Integer, ByVal DocumentStream As System.IO.Stream)
            Try
                DocumentDB.PutStream(DocumentId, DocumentStream)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

#End Region

#Region "Business Object Authorization"

        Public Shared Function CanView(ByVal FltIdentity As FltIdentity) As Boolean
            Try
                Return FltIdentityManager.AuthorizeUserAction(FltIdentity, "Fleet", fltUserAction.CanView)
            Catch ex As Exception
                Logger.Write(ex)
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function CanEdit(ByVal FltIdentity As FltIdentity) As Boolean
            Try
                Return FltIdentityManager.AuthorizeUserAction(FltIdentity, "Fleet", fltUserAction.CanEdit)
            Catch ex As Exception
                Logger.Write(ex)
                Throw New Exception(ex.Message)
            End Try
        End Function

#End Region

#Region "Private Child Business Object Data Access Procedures"

        Private Shared Sub ProcessWOReasonList(ByVal WOReasonList As WOReasonList, ByVal WorkOrderId As Integer, ByVal FltIdentity As FltIdentity)
            Try
                For Each thisWOReason As WOReason In WOReasonList
                    thisWOReason.WorkOrderId = WorkOrderId
                    thisWOReason.StampUserName = FltIdentity.UserName
                    If ((thisWOReason.ConcurrencyId = Nothing) Or (thisWOReason.ConcurrencyId = "0") Or (thisWOReason.ConcurrencyId = "0")) Then
                        WOReasonDB.Insert(thisWOReason)
                    Else
                        WOReasonDB.Update(thisWOReason)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub DeleteWOReasonList(ByVal WOReasonList As WOReasonList, ByVal FltIdentity As FltIdentity)
            Try
                For Each thisWOReason As WOReason In WOReasonList
                    WOReasonDB.Delete(thisWOReason.WOReasonId, FltIdentity.UserName)
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub ProcessWOReasonDeleteList(ByVal WOReasonDeleteList As WOReasonList, ByVal FltIdentity As FltIdentity)
            Try
                For Each thisWOReason As WOReason In WOReasonDeleteList
                    If thisWOReason.WOReasonId > 0 Then
                        WOReasonDB.Delete(thisWOReason.WOReasonId, FltIdentity.UserName)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub ProcessWOInstructionList(ByVal WOInstructionList As WOInstructionList, ByVal WorkOrderId As Integer, ByVal FltIdentity As FltIdentity)
            Try
                For Each thisWOInstruction As WOInstruction In WOInstructionList
                    thisWOInstruction.WorkOrderId = WorkOrderId
                    thisWOInstruction.StampUserName = FltIdentity.UserName
                    If ((thisWOInstruction.ConcurrencyId = Nothing) Or (thisWOInstruction.ConcurrencyId = "0") Or (thisWOInstruction.ConcurrencyId = "0")) Then
                        WOInstructionDB.Insert(thisWOInstruction)
                    Else
                        WOInstructionDB.Update(thisWOInstruction)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub DeleteWOInstructionList(ByVal WOInstructionList As WOInstructionList, ByVal FltIdentity As FltIdentity)
            Try
                For Each thisWOInstruction As WOInstruction In WOInstructionList
                    WOInstructionDB.Delete(thisWOInstruction.WOInstructionsId, FltIdentity.UserName)
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub ProcessWOInstructionDeleteList(ByVal WOInstructionDeleteList As WOInstructionList, ByVal FltIdentity As FltIdentity)
            Try
                For Each thisWOInstruction As WOInstruction In WOInstructionDeleteList
                    If thisWOInstruction.WOInstructionsId > 0 Then
                        WOInstructionDB.Delete(thisWOInstruction.WOInstructionsId, FltIdentity.UserName)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub ProcessWOPerformedWorkList(ByVal WOPerformedWorkList As WOPerformedWorkList, ByVal WorkOrderId As Integer, ByVal FltIdentity As FltIdentity)
            Try
                For Each thisWOPerformedWork As WOPerformedWork In WOPerformedWorkList
                    thisWOPerformedWork.WorkOrderId = WorkOrderId
                    thisWOPerformedWork.StampUserName = FltIdentity.UserName
                    If ((thisWOPerformedWork.ConcurrencyId = Nothing) Or (thisWOPerformedWork.ConcurrencyId = "0") Or (thisWOPerformedWork.ConcurrencyId = "0")) Then
                        WOPerformedWorkDB.Insert(thisWOPerformedWork)
                    Else
                        WOPerformedWorkDB.Update(thisWOPerformedWork)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub DeleteWOPerformedWorkList(ByVal WOPerformedWorkList As WOPerformedWorkList, ByVal FltIdentity As FltIdentity)
            Try
                For Each thisWOPerformedWork As WOPerformedWork In WOPerformedWorkList
                    WOPerformedWorkDB.Delete(thisWOPerformedWork.PerformedWorkId, FltIdentity.UserName)
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub ProcessWOPerformedWorkDeleteList(ByVal WOPerformedWorkDeleteList As WOPerformedWorkList, ByVal FltIdentity As FltIdentity)
            Try
                For Each thisWOPerformedWork As WOPerformedWork In WOPerformedWorkDeleteList
                    If thisWOPerformedWork.PerformedWorkId > 0 Then
                        WOPerformedWorkDB.Delete(thisWOPerformedWork.PerformedWorkId, FltIdentity.UserName)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub ProcessWOPartList(ByVal WOPartList As WorkOrderPartList, ByVal WorkOrderId As Integer, ByVal FltIdentity As FltIdentity)
            Try
                For Each thisWOPart As WorkOrderPart In WOPartList
                    thisWOPart.WorkOrderId = WorkOrderId
                    thisWOPart.StampUserName = FltIdentity.UserName
                    If ((thisWOPart.ConcurrencyId = Nothing) Or (thisWOPart.ConcurrencyId = "0") Or (thisWOPart.ConcurrencyId = "0")) Then
                        WorkOrderPartDB.Insert(thisWOPart)
                    Else
                        WorkOrderPartDB.Update(thisWOPart)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub DeleteWOPartList(ByVal WOPartList As WorkOrderPartList, ByVal FltIdentity As FltIdentity)
            Try
                For Each thisWOPart As WorkOrderPart In WOPartList
                    WorkOrderPartDB.Delete(thisWOPart.WorkOrderPartId, FltIdentity.UserName)
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub ProcessWOPartDeleteList(ByVal WOPartDeleteList As WorkOrderPartList, ByVal FltIdentity As FltIdentity)
            Try
                For Each thisWOPart As WorkOrderPart In WOPartDeleteList
                    If thisWOPart.WorkOrderPartId > 0 Then
                        WorkOrderPartDB.Delete(thisWOPart.WorkOrderPartId, FltIdentity.UserName)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub ProcessWOEngineerCommentList(ByVal WOEngineerCommentList As WOEngineerCommentList, ByVal WorkOrderId As Integer, ByVal FltIdentity As FltIdentity)
            Try
                For Each thisWOEngineerComment As WOEngineerComment In WOEngineerCommentList
                    thisWOEngineerComment.WorkOrderId = WorkOrderId
                    thisWOEngineerComment.StampUserName = FltIdentity.UserName
                    If ((thisWOEngineerComment.ConcurrencyId = Nothing) Or (thisWOEngineerComment.ConcurrencyId = "0") Or (thisWOEngineerComment.ConcurrencyId = "0")) Then
                        WOEngineerCommentDB.Insert(thisWOEngineerComment)
                    Else
                        WOEngineerCommentDB.Update(thisWOEngineerComment)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub DeleteWOEngineerCommentList(ByVal WOEngineerCommentList As WOEngineerCommentList, ByVal FltIdentity As FltIdentity)
            Try
                For Each thisWOEngineerComment As WOEngineerComment In WOEngineerCommentList
                    WOEngineerCommentDB.Delete(thisWOEngineerComment.EngineerCommentsId, FltIdentity.UserName)
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub ProcessWOEngineerCommentDeleteList(ByVal WOEngineerCommentDeleteList As WOEngineerCommentList, ByVal FltIdentity As FltIdentity)
            Try
                For Each thisWOEngineerComment As WOEngineerComment In WOEngineerCommentDeleteList
                    If thisWOEngineerComment.EngineerCommentsId > 0 Then
                        WOEngineerCommentDB.Delete(thisWOEngineerComment.EngineerCommentsId, FltIdentity.UserName)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub ProcessWOMechanicCommentList(ByVal WOMechanicCommentList As WOMechanicCommentList, ByVal WorkOrderId As Integer, ByVal FltIdentity As FltIdentity)
            Try
                For Each thisWOMechanicComment As WOMechanicComment In WOMechanicCommentList
                    thisWOMechanicComment.WorkOrderId = WorkOrderId
                    thisWOMechanicComment.StampUserName = FltIdentity.UserName
                    If ((thisWOMechanicComment.ConcurrencyId = Nothing) Or (thisWOMechanicComment.ConcurrencyId = "0") Or (thisWOMechanicComment.ConcurrencyId = "0")) Then
                        WOMechanicCommentDB.Insert(thisWOMechanicComment)
                    Else
                        WOMechanicCommentDB.Update(thisWOMechanicComment)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub DeleteWOMechanicCommentList(ByVal WOMechanicCommentList As WOMechanicCommentList, ByVal FltIdentity As FltIdentity)
            Try
                For Each thisWOMechanicComment As WOMechanicComment In WOMechanicCommentList
                    WOMechanicCommentDB.Delete(thisWOMechanicComment.WOMechanicCommentsId, FltIdentity.UserName)
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub ProcessWOMechanicCommentDeleteList(ByVal WOMechanicCommentDeleteList As WOMechanicCommentList, ByVal FltIdentity As FltIdentity)
            Try
                For Each thisWOMechanicComment As WOMechanicComment In WOMechanicCommentDeleteList
                    If thisWOMechanicComment.WOMechanicCommentsId > 0 Then
                        WOMechanicCommentDB.Delete(thisWOMechanicComment.WOMechanicCommentsId, FltIdentity.UserName)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub ProcessWODocumentInfosList(ByVal DocumentInfoList As DocumentInfoList, ByVal WorkOrderId As Integer, ByVal fltIdentity As FltIdentity)
            Try
                For Each thisDocumentInfo As DocumentInfo In DocumentInfoList
                    thisDocumentInfo.StampUserName = fltIdentity.UserName
                    DocumentInfoDB.Update(thisDocumentInfo)
                    If Not DocumentWorkOrderXrefDB.Exists(thisDocumentInfo.DocumentId, WorkOrderId) Then
                        DocumentWorkOrderXrefDB.Insert(thisDocumentInfo.DocumentId, WorkOrderId, fltIdentity.UserName)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub DeleteWODocumentInfosList(ByVal DocumentInfoList As DocumentInfoList, ByVal WorkOrderId As Integer, ByVal FltIdentity As FltIdentity)
            Try
                For Each thisDocumentInfo As DocumentInfo In DocumentInfoList
                    DocumentWorkOrderXrefDB.Delete(thisDocumentInfo.DocumentId, WorkOrderId, FltIdentity.UserName)
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Shared Sub ProcessWODocumentInfosDeleteList(ByVal DocumentInfoList As DocumentInfoList, ByVal WorkOrderId As Integer, ByVal FltIdentity As FltIdentity)
            Try
                For Each thisDocumentInfo As DocumentInfo In DocumentInfoList
                    DocumentWorkOrderXrefDB.Delete(thisDocumentInfo.DocumentId, WorkOrderId, FltIdentity.UserName)
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

#End Region

    End Class

End Namespace
