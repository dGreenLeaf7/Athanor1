' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports Contoso.Utilities
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BL

	<DataObjectAttribute()> _
	Public Class AircraftManager

#Region "Parent Business Object Methods"

        <DataObjectMethod(DataObjectMethodType.Select, True)> _
        Public Shared Function GetItem(ByVal AircraftId As Integer, ByVal FltIdentity As FltIdentity) As Aircraft
            Dim newAircraft As Aircraft = Nothing
            Dim totRows As Integer = 0
            If CanView(FltIdentity) Then
                Try
                    newAircraft = AircraftDB.GetItem(AircraftId)
                Catch ex As Exception
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't fetch - user is not authorized")
            End If
            Return newAircraft
        End Function

        Public Shared Sub Save(ByVal Aircraft As Aircraft, ByVal FltIdentity As FltIdentity)
            If ((Aircraft.ConcurrencyId = Nothing) Or (Aircraft.ConcurrencyId = String.Empty) Or (Aircraft.ConcurrencyId = "0")) Then
                Insert(Aircraft, FltIdentity)
            Else
                Update(Aircraft, FltIdentity)
            End If
        End Sub

        <DataObjectMethod(DataObjectMethodType.Update, True)> _
        Public Shared Sub Update(ByVal Aircraft As Aircraft, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                If Aircraft.Validate() Then
                    Try
                        Aircraft.StampUserName = FltIdentity.UserName
                        AircraftDB.Update(Aircraft)
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
        Public Shared Function Insert(ByVal Aircraft As Aircraft, ByVal FltIdentity As FltIdentity) As Integer

            Dim newId As Integer = 0

            If CanEdit(FltIdentity) Then
                If Aircraft.Validate() Then
                    Try
                        Aircraft.StampUserName = FltIdentity.UserName
                        newId = AircraftDB.Insert(Aircraft)
                        Aircraft.AircraftId = newId

                    Catch ex As ApplicationException
                        Throw New ApplicationException(ex.Message)
                    End Try
                Else
                    Throw New ApplicationException("Can't insert - record is invalid. Please correct errors and try again.")
                End If
            Else
                Throw New Exception("Can't insert - user is not authorized")
            End If

            Return newId

        End Function

        <DataObjectMethod(DataObjectMethodType.Delete, True)> _
        Public Shared Sub Delete(ByVal Aircraft As Aircraft, ByVal FltIdentity As FltIdentity)
            If CanEdit(FltIdentity) Then
                Try
                    AircraftDB.Delete(Aircraft.AircraftId, FltIdentity.UserName)
                Catch ex As ApplicationException
                    Throw New ApplicationException(ex.Message)
                End Try
            Else
                Throw New Exception("Can't delete - user is not authorized")
            End If
        End Sub

#End Region

#Region "Child Business Object Methods"

        Public Shared Sub InsertAircraftConfiguration(ByVal Aircraft As Aircraft, ByVal AircraftConfiguration As AircraftConfiguration)

            If Not Aircraft.AircraftId > 0 Then
                Aircraft.AirframeHoursStart = AircraftConfiguration.ConfigurationMeterStart
                Aircraft.AirframeHoursOffset = AircraftConfiguration.ConfigurationMeterOffset
                Aircraft.AirframeHoursCurrent = Aircraft.AirframeHoursStart
                Aircraft.MeterUnitId = AircraftConfiguration.MeterUnitId
            Else
                AircraftConfiguration.ConfigurationMeterStart = Aircraft.AirframeHoursCurrent
                Aircraft.AirframeHoursOffset = AircraftConfiguration.ConfigurationMeterOffset
                Aircraft.MeterUnitId = AircraftConfiguration.MeterUnitId
            End If

            If AircraftConfiguration.Validate() Then

                If Aircraft.AircraftId > 0 Then
                    Dim latestAircraftConfiguration As AircraftConfiguration = RetrieveLatestAircraftConfiguration(Aircraft)
                    If latestAircraftConfiguration.AircraftConfigurationId > 0 Then
                        latestAircraftConfiguration.ConfigurationMeterEnd = Aircraft.AirframeHoursCurrent
                    End If
                End If

                Try
                    AircraftConfiguration.AircraftConfigurationId = Aircraft.NewAircraftConfigurationId
                    Aircraft.NewAircraftConfigurationId = Aircraft.NewAircraftConfigurationId - 1
                    Aircraft.AircraftConfigurationList.Add(AircraftConfiguration)

                Catch ex As Exception
                    Logger.Write(ex)
                    Throw New Exception(ex.Message)
                End Try
            Else
                Aircraft.BrokenRules.Clear()
                For Each thisRule As BrokenRule In AircraftConfiguration.BrokenRules
                    Aircraft.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't insert - record is invalid. Please correct errors and try again.")
            End If
        End Sub

        Public Shared Sub RemoveAircraftConfiguration(ByVal Aircraft As Aircraft, ByVal AircraftConfigurationId As Integer)
            Try
                If AircraftConfigurationId > 0 Then
                    Throw New Exception("Can't remove - deleting an existing AircraftConfiguration is not allowed.")
                Else
                    For Each thisAircraftConfiguration As AircraftConfiguration In Aircraft.AircraftConfigurationList
                        If thisAircraftConfiguration.AircraftConfigurationId = AircraftConfigurationId Then

                            Aircraft.AircraftConfigurationList.Remove(thisAircraftConfiguration)

                            Dim latestAircraftConfiguration As AircraftConfiguration = RetrieveLatestAircraftConfiguration(Aircraft)
                            If Not latestAircraftConfiguration Is Nothing Then
                                latestAircraftConfiguration.ConfigurationMeterEnd = 0
                                Aircraft.AirframeHoursOffset = latestAircraftConfiguration.ConfigurationMeterOffset
                            Else
                                Aircraft.AirframeHoursCurrent = 0
                                Aircraft.AirframeHoursOffset = 0
                                Aircraft.AirframeHoursStart = 0
                            End If

                            Exit Sub
                        End If
                    Next
                    Throw New Exception("Can't remove - record not found in collection!")
                End If
            Catch ex As Exception
                Logger.Write(ex)
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Public Shared Sub UpdateAircraftConfiguration(ByVal Aircraft As Aircraft, ByVal AircraftConfiguration As AircraftConfiguration)
            If AircraftConfiguration.AircraftConfigurationId > 0 Then
                Throw New Exception("Can't update - editing an existing Aircraft Configuration is not allowed.")
            Else
                AircraftConfiguration.ConfigurationMeterStart = Aircraft.AirframeHoursCurrent
                If AircraftConfiguration.Validate() Then
                    Try
                        For Each thisAircraftConfiguration As AircraftConfiguration In Aircraft.AircraftConfigurationList
                            If AircraftConfiguration.AircraftConfigurationId = thisAircraftConfiguration.AircraftConfigurationId Then
                                AircraftConfiguration.ConcurrencyId = thisAircraftConfiguration.ConcurrencyId
                                Aircraft.AircraftConfigurationList.Remove(thisAircraftConfiguration)
                                Aircraft.AircraftConfigurationList.Add(AircraftConfiguration)
                                Aircraft.AirframeHoursOffset = AircraftConfiguration.ConfigurationMeterOffset
                                Aircraft.MeterUnitId = AircraftConfiguration.MeterUnitId
                                Exit Sub
                            End If
                        Next
                    Catch ex As Exception
                        Logger.Write(ex)
                        Throw New Exception(ex.Message)
                    End Try
                Else
                    Aircraft.BrokenRules.Clear()
                    For Each thisRule As BrokenRule In AircraftConfiguration.BrokenRules
                        Aircraft.BrokenRules.Add(thisRule)
                    Next
                    Throw New Exception("Can't update - record is invalid. Please correct errors and try again.")
                End If
            End If
        End Sub

        Public Shared Sub InsertAircraftLocation(ByVal Aircraft As Aircraft, ByVal AircraftLocation As AircraftLocation)
            If AircraftLocation.Validate() Then
                Try
                    AircraftLocation.AircraftLocationId = Aircraft.NewAircraftLocationId
                    Aircraft.NewAircraftLocationId = Aircraft.NewAircraftLocationId - 1
                    If AircraftLocation.LastLocation Then
                        Aircraft.CurrentLocationId = AircraftLocation.LocationId
                        For Each thisAircraftLocation As AircraftLocation In Aircraft.AircraftLocationList
                            If thisAircraftLocation.LastLocation Then
                                thisAircraftLocation.LastLocation = False
                            End If
                        Next
                    End If
                    Aircraft.AircraftLocationList.Add(AircraftLocation)
                Catch ex As Exception
                    Logger.Write(ex)
                    Throw New Exception(ex.Message)
                End Try
            Else
                Aircraft.BrokenRules.Clear()
                For Each thisRule As BrokenRule In AircraftLocation.BrokenRules
                    Aircraft.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't insert - record is invalid. Please correct errors and try again.")
            End If
        End Sub

        Public Shared Sub RemoveAircraftLocation(ByVal Aircraft As Aircraft, ByVal AircraftLocationId As Integer)
            Try
                For Each thisAircraftLocation As AircraftLocation In Aircraft.AircraftLocationList
                    If thisAircraftLocation.AircraftLocationId = AircraftLocationId Then
                        Aircraft.AircraftLocationList.Remove(thisAircraftLocation)
                        If AircraftLocationId > 0 Then
                            Aircraft.AircraftLocationDeleteList.Add(thisAircraftLocation)
                        End If
                        If thisAircraftLocation.LastLocation Then
                            Aircraft.CurrentLocationId = 0
                        End If
                        Exit Sub
                    End If
                Next
                Logger.Write("Can't remove - record not found in collection!")
                Logger.Write("RemoveAircraftLocation(Aircraft As Aircraft = <object>, AircraftLocationId As Integer = " & AircraftLocationId & ")")
                Throw New Exception("Can't remove - record not found in collection!")
            Catch ex As Exception
                Logger.Write(ex)
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Public Shared Sub UpdateAircraftLocation(ByVal Aircraft As Aircraft, ByVal AircraftLocation As AircraftLocation)
            If AircraftLocation.Validate() Then
                Try
                    If AircraftLocation.LastLocation Then
                        Aircraft.CurrentLocationId = AircraftLocation.LocationId
                        For Each thisAircraftLocation As AircraftLocation In Aircraft.AircraftLocationList
                            If thisAircraftLocation.LastLocation Then
                                thisAircraftLocation.LastLocation = False
                            End If
                        Next
                    End If
                    For Each thisAircraftLocation As AircraftLocation In Aircraft.AircraftLocationList
                        If AircraftLocation.LocationId = thisAircraftLocation.LocationId Then
                            AircraftLocation.ConcurrencyId = thisAircraftLocation.ConcurrencyId
                            Aircraft.AircraftLocationList.Remove(thisAircraftLocation)
                            Aircraft.AircraftLocationList.Add(AircraftLocation)
                            Exit Sub
                        End If
                    Next
                Catch ex As Exception
                    Logger.Write(ex)
                    Throw New Exception(ex.Message)
                End Try
            Else
                Aircraft.BrokenRules.Clear()
                For Each thisRule As BrokenRule In AircraftLocation.BrokenRules
                    Aircraft.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't update - record is invalid. Please correct errors and try again.")
            End If
        End Sub

        Public Shared Sub InsertGenericComment(ByVal Aircraft As Aircraft, ByVal GenericComment As GenericComment)
            GenericComment.SourceTable = "AircraftComment"
            If ValidateGenericComment(GenericComment) Then
                Try
                    GenericComment.SourceID = Aircraft.NewGenericCommentId
                    Aircraft.NewGenericCommentId = Aircraft.NewGenericCommentId - 1
                    GenericComment.IsDirty = True
                    Aircraft.GenericCommentList.Add(GenericComment)
                    SortGenericCommentList(Aircraft)
                Catch ex As Exception
                    Logger.Write(ex)
                    Throw New Exception(ex.Message)
                End Try
            Else
                Aircraft.BrokenRules.Clear()
                For Each thisRule As BrokenRule In GenericComment.BrokenRules
                    Aircraft.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't insert - record is invalid. Please correct errors and try again.")
            End If
        End Sub

        Public Shared Sub RemoveGenericComment(ByVal Aircraft As Aircraft, ByVal SourceTable As String, ByVal SourceId As Integer)
            Try
                If SourceTable = "AircraftComment" Then
                    For Each thisGenericComment As GenericComment In Aircraft.GenericCommentList
                        If ((thisGenericComment.SourceTable = SourceTable) And (thisGenericComment.SourceID = SourceId)) Then
                            Aircraft.GenericCommentList.Remove(thisGenericComment)
                            If SourceId > 0 Then
                                Aircraft.GenericCommentDeleteList.Add(thisGenericComment)
                            End If
                            Exit Sub
                        End If
                    Next
                    Logger.Write("Can't remove - record not found in collection!")
                    Logger.Write("RemoveGenericComment(Aircraft As Aircraft = <object>, SourceId As Integer = " & SourceId & ")")
                    Throw New Exception("Can't remove - record not found in collection!")
                Else
                    Throw New Exception("Removing a comment that is not an Aircraft Comment is not allowed")
                End If
            Catch ex As Exception
                Logger.Write(ex)
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Public Shared Sub UpdateGenericComment(ByVal Aircraft As Aircraft, ByVal GenericComment As GenericComment)
            If ValidateGenericComment(GenericComment) Then
                Try
                    With Aircraft.GenericCommentList
                        For i As Integer = 0 To (.Count - 1)
                            If .Item(i).SourceTable = GenericComment.SourceTable And .Item(i).SourceID = GenericComment.SourceID Then
                                GenericComment.ConcurrencyId = .Item(i).ConcurrencyId
                                GenericComment.IsDirty = True
                                .Item(i) = GenericComment
                                Exit For
                            End If
                        Next
                    End With
                Catch ex As Exception
                    Logger.Write(ex)
                    Throw New Exception(ex.Message)
                End Try
            Else
                Aircraft.BrokenRules.Clear()
                For Each thisRule As BrokenRule In GenericComment.BrokenRules
                    Aircraft.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't update - record is invalid. Please correct errors and try again.")
            End If
        End Sub

        Public Shared Sub InsertTest(ByVal Aircraft As Aircraft, ByVal Test As fltLookupBase)
            If Test.Validate() Then
                Try
                    Aircraft.TestList.Add(Test)
                Catch ex As Exception
                    Logger.Write(ex)
                    Throw New Exception(ex.Message)
                End Try
            Else
                Aircraft.BrokenRules.Clear()
                For Each thisRule As BrokenRule In Test.BrokenRules
                    Aircraft.BrokenRules.Add(thisRule)
                Next
                Throw New Exception("Can't insert - record is invalid. Please correct errors and try again.")
            End If
        End Sub

        Public Shared Sub RemoveTest(ByVal Aircraft As Aircraft, ByVal Test As fltLookupBase)
            Try
                For Each thisTest As fltLookupBase In Aircraft.TestList
                    If thisTest.ID = Test.ID Then
                        Aircraft.TestList.Remove(thisTest)
                        Aircraft.TestDeleteList.Add(thisTest)
                        Exit Sub
                    End If
                Next
                Throw New Exception("Can't remove - record not found in collection!")
            Catch ex As Exception
                Logger.Write(ex)
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Public Shared Function CloneAircraftConfiguration(ByVal SourceAircraftConfiguration As AircraftConfiguration, ByVal AsNew As Boolean)
            Dim target As New AircraftConfiguration
            Try
                With SourceAircraftConfiguration
                    target.APUEmission = .APUEmission
                    target.APUEmissionId = .APUEmissionId
                    target.APUMaturity = .APUMaturity
                    target.APUMaturityId = .APUMaturityId
                    target.APUSerialNbr = .APUSerialNbr
                    target.APUType = .APUType
                    target.APUTypeId = .APUTypeId
                    target.NoseGear = .NoseGear
                    target.MainGear = .MainGear
                    target.ConfigurationDescription = .ConfigurationDescription
                    target.ConfigurationEndDate = .ConfigurationEndDate
                    target.ConfigurationMeterOffset = .ConfigurationMeterOffset
                    target.ConfigurationMeterEnd = .ConfigurationMeterStart
                    target.ConfigurationStartDate = .ConfigurationStartDate
                    target.EmissionLevel = .EmissionLevel
                    target.EngineEmission = .EngineEmission
                    target.EngineEmissionId = .EngineEmissionId
                    target.EngineMaturity = .EngineMaturity
                    target.EngineMaturityId = .EngineMaturityId
                    target.EngineSerialNbr = .EngineSerialNbr
                    target.EngineType = .EngineType
                    target.EngineTypeId = .EngineTypeId
                    target.IsDeleted = .IsDeleted
                    target.ModelDescr = .ModelDescr
                    target.ModelId = .ModelId
                    target.MeterUnit = .MeterUnit
                    target.MeterUnitId = .MeterUnitId
                    target.StampUserName = .StampUserName
                    target.AvionicsVersion = .AvionicsVersion
                    target.PropellerModelNbr = .PropellerModelNbr
                    target.PropellerSerialNo = .PropellerSerialNo
                    target.PropellerType = .PropellerType
                    target.PropellerTypeId = .PropellerTypeId
                    If AsNew Then
                        target.UtcDateCreated = Date.UtcNow
                        target.UtcDateModified = Date.UtcNow
                        target.AircraftConfigurationGUID = Guid.Empty
                        target.AircraftConfigurationId = 0
                    Else
                        target.UtcDateCreated = .UtcDateCreated
                        target.UtcDateModified = .UtcDateModified
                        target.AircraftConfigurationGUID = .AircraftConfigurationGUID
                        target.AircraftConfigurationId = .AircraftConfigurationId
                    End If
                    target.AircraftId = .AircraftId
                End With
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
            Return target
        End Function

        Public Shared Function RetrieveLatestAircraftConfiguration(ByVal Aircraft As Aircraft, ByVal FltIdentity As FltIdentity) As AircraftConfiguration
            Dim aircraftConfiguration As New AircraftConfiguration
            Try
                If CanView(FltIdentity) Then
                    aircraftConfiguration = RetrieveLatestAircraftConfiguration(Aircraft)
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
            Return aircraftConfiguration
        End Function

        Public Shared Function RetrieveLatestAircraftLocation(ByVal Aircraft As Aircraft, ByVal FltIdentity As FltIdentity) As AircraftLocation
            Dim aircraftLocation As New AircraftLocation
            Try
                If CanView(FltIdentity) Then
                    aircraftLocation = RetrieveLatestAircraftLocation(Aircraft)
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
            Return aircraftLocation
        End Function

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

        Private Shared Function ValidateGenericComment(ByVal GenericComment As GenericComment) As Boolean
            Select Case LCase(GenericComment.SourceTable)
                Case "aircraftcomment"
                    Dim newAircraftComment As AircraftComment = MorphToAircraftComment(GenericComment)
                    If Not newAircraftComment.Validate Then
                        MoveBrokenRulesToGenericComment(GenericComment, newAircraftComment.BrokenRules, newAircraftComment.ErrorsList, newAircraftComment.WarningsList)
                    End If
                Case "flightcomment"
                    Dim newFlightComment As FlightComment = MorphToFlightComment(GenericComment)
                    If Not newFlightComment.Validate Then
                        MoveBrokenRulesToGenericComment(GenericComment, newFlightComment.BrokenRules, newFlightComment.ErrorsList, newFlightComment.WarningsList)
                    End If
                Case "woengineercomment"
                    Dim newWOEngineerComment As WOEngineerComment = MorphToWOEngineerComment(GenericComment)
                    If Not newWOEngineerComment.Validate Then
                        MoveBrokenRulesToGenericComment(GenericComment, newWOEngineerComment.BrokenRules, newWOEngineerComment.ErrorsList, newWOEngineerComment.WarningsList)
                    End If
                Case "womechaniccomment"
                    Dim newWOMechanicComment As WOMechanicComment = MorphToWOMechanicComment(GenericComment)
                    If Not newWOMechanicComment.Validate Then
                        MoveBrokenRulesToGenericComment(GenericComment, newWOMechanicComment.BrokenRules, newWOMechanicComment.ErrorsList, newWOMechanicComment.WarningsList)
                    End If
                Case Else
                    GenericComment.BrokenRules.Clear()
                    GenericComment.BrokenRules.Add(New BrokenRule("SourceTable", "Unable to determine the Comment Type"))
            End Select
            Return ((GenericComment.BrokenRules.Count = 0) And (GenericComment.ErrorsList.Count = 0) And (GenericComment.WarningsList.Count = 0))
        End Function

        Private Shared Sub SortGenericCommentList(ByVal Aircraft As Aircraft)
            With Aircraft.GenericCommentList
                For i As Integer = 1 To (.Count - 1)
                    Dim holdComment As GenericComment = .Item(i)
                    Dim j As Integer = i - 1
                    Dim done As Boolean = False
                    Do
                        If CDate(.Item(j).CommentDate) < CDate(holdComment.CommentDate) Then
                            .Item(j + 1) = .Item(j)
                            j = j - 1
                            If j < 0 Then
                                done = True
                            End If
                        Else
                            done = True
                        End If
                    Loop Until done
                    .Item(j + 1) = holdComment
                Next
            End With
        End Sub

        Private Shared Sub MoveBrokenRulesToGenericComment(ByVal GenericComment As GenericComment, ByVal BrokenRules As BrokenRulesList, ByVal ErrorsList As BrokenRulesList, ByVal WarningsList As BrokenRulesList)
            GenericComment.BrokenRules.Clear()
            For Each thisBR As BrokenRule In BrokenRules
                GenericComment.BrokenRules.Add(thisBR)
            Next
            GenericComment.ErrorsList.Clear()
            For Each thisError As BrokenRule In ErrorsList
                GenericComment.ErrorsList.Add(thisError)
            Next
            GenericComment.WarningsList.Clear()
            For Each thisWarning As BrokenRule In WarningsList
                GenericComment.WarningsList.Add(thisWarning)
            Next
        End Sub

        Private Shared Function MorphToAircraftComment(ByVal GenericComment As GenericComment) As AircraftComment
            Dim newAircraftComment As New AircraftComment
            newAircraftComment.AircraftCommentId = GenericComment.SourceID
            newAircraftComment.AircraftId = GenericComment.ParentId
            newAircraftComment.EngineerId = GenericComment.AuthorID
            newAircraftComment.ComponentId = GenericComment.ComponentId
            newAircraftComment.CommentDate = GenericComment.CommentDate
            newAircraftComment.CommentRating = GenericComment.CommentRating
            newAircraftComment.IssueCodeId = GenericComment.IssueCodeId
            newAircraftComment.IssueCode = GenericComment.IssueCode
            newAircraftComment.Comments = GenericComment.Comments
            newAircraftComment.AircraftCommentGUID = GenericComment.CommentGUID
            newAircraftComment.IsDeleted = GenericComment.IsDeleted
            newAircraftComment.UtcDateCreated = GenericComment.UtcDateCreated
            newAircraftComment.UtcDateModified = GenericComment.UtcDateModified
            newAircraftComment.StampUserName = GenericComment.StampUserName
            newAircraftComment.ConcurrencyId = GenericComment.ConcurrencyId
            Return newAircraftComment
        End Function

        Private Shared Function MorphToFlightComment(ByVal GenericComment As GenericComment) As FlightComment
            Dim newFlightComment As New FlightComment
            newFlightComment.FlightCommentId = GenericComment.SourceID
            newFlightComment.FlightId = GenericComment.ParentId
            newFlightComment.ComponentId = GenericComment.ComponentId
            newFlightComment.CommentTime = GenericComment.CommentTime
            newFlightComment.CommentDateTime_UTC = GenericComment.CommentDateTime_UTC
            newFlightComment.CommentDateTime_Local = GenericComment.CommentDateTime_Local
            newFlightComment.CommentRating = GenericComment.CommentRating
            newFlightComment.IssueCodeId = GenericComment.IssueCodeId
            newFlightComment.IssueCode = GenericComment.IssueCode
            newFlightComment.PilotComment = GenericComment.Comments
            newFlightComment.FlightCommentGUID = GenericComment.CommentGUID
            newFlightComment.IsActive = True
            newFlightComment.IsDeleted = GenericComment.IsDeleted
            newFlightComment.UtcDateCreated = GenericComment.UtcDateCreated
            newFlightComment.UtcDateModified = GenericComment.UtcDateModified
            newFlightComment.StampUserName = GenericComment.StampUserName
            newFlightComment.ConcurrencyId = GenericComment.ConcurrencyId
            Return newFlightComment
        End Function

        Private Shared Function MorphToWOEngineerComment(ByVal GenericComment As GenericComment) As WOEngineerComment
            Dim newWOEngineerComment As New WOEngineerComment
            newWOEngineerComment.EngineerCommentsId = GenericComment.SourceID
            newWOEngineerComment.WorkOrderId = GenericComment.ParentId
            newWOEngineerComment.ComponentId = GenericComment.ComponentId
            newWOEngineerComment.CommentDate = GenericComment.CommentDate
            newWOEngineerComment.CommentRating = GenericComment.CommentRating
            newWOEngineerComment.IssueCodeId = GenericComment.IssueCodeId
            newWOEngineerComment.IssueCode = GenericComment.IssueCode
            newWOEngineerComment.Comments = GenericComment.Comments
            newWOEngineerComment.EngineerCommentGUID = GenericComment.CommentGUID
            newWOEngineerComment.IsDeleted = GenericComment.IsDeleted
            newWOEngineerComment.UtcDateCreated = GenericComment.UtcDateCreated
            newWOEngineerComment.UtcDateModified = GenericComment.UtcDateModified
            newWOEngineerComment.StampUserName = GenericComment.StampUserName
            newWOEngineerComment.ConcurrencyId = GenericComment.ConcurrencyId
            Return newWOEngineerComment
        End Function

        Private Shared Function MorphToWOMechanicComment(ByVal GenericComment As GenericComment) As WOMechanicComment
            Dim newWOMechanicComment As New WOMechanicComment
            newWOMechanicComment.WOMechanicCommentsId = GenericComment.SourceID
            newWOMechanicComment.WorkOrderId = GenericComment.ParentId
            newWOMechanicComment.ComponentId = GenericComment.ComponentId
            newWOMechanicComment.CommentDate = GenericComment.CommentDate
            newWOMechanicComment.CommentRating = GenericComment.CommentRating
            newWOMechanicComment.IssueCodeId = GenericComment.IssueCodeId
            newWOMechanicComment.IssueCode = GenericComment.IssueCode
            newWOMechanicComment.Comments = GenericComment.Comments
            newWOMechanicComment.MechanicCommentGUID = GenericComment.CommentGUID
            newWOMechanicComment.IsDeleted = GenericComment.IsDeleted
            newWOMechanicComment.UtcDateCreated = GenericComment.UtcDateCreated
            newWOMechanicComment.UtcDateModified = GenericComment.UtcDateModified
            newWOMechanicComment.StampUserName = GenericComment.StampUserName
            newWOMechanicComment.ConcurrencyId = GenericComment.ConcurrencyId
            Return newWOMechanicComment
        End Function

        Private Shared Function RetrieveLatestAircraftConfiguration(ByVal Aircraft As Aircraft) As AircraftConfiguration
            Dim aircraftConfiguration As New AircraftConfiguration
            For Each thisAircraftConfiguration As AircraftConfiguration In Aircraft.AircraftConfigurationList
                If thisAircraftConfiguration.ConfigurationMeterStart > aircraftConfiguration.ConfigurationMeterStart Then
                    aircraftConfiguration = thisAircraftConfiguration
                End If
            Next
            Return aircraftConfiguration
        End Function

        Private Shared Function RetrieveLatestAircraftLocation(ByVal Aircraft As Aircraft) As AircraftLocation
            Dim aircraftLocation As AircraftLocation = Nothing
            For Each thisAircraftLocation As AircraftLocation In Aircraft.AircraftLocationList
                If thisAircraftLocation.ArrivalDate > aircraftLocation.ArrivalDate Then
                    aircraftLocation = thisAircraftLocation
                End If
            Next
            Return aircraftLocation
        End Function

#End Region

    End Class

End Namespace
