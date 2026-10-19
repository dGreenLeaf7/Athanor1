' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics

Namespace FLT.BO

	<Serializable()> _
	Public Class Program
	
        Private _progID As Integer
		Private _progName As String
		Private _progDescription As String
		Private _progVersion As String
		Private _programLogo As String
		Private _reportLogo As String
		Private _yearLogo As String
		Private _progStatus As Byte
		Private _dateOpened As String
		Private _dateClosed As String
		Private _actualValue As Decimal
		Private _projectCode As String
		Private _activityCode As String
		Private _deptCode As String
		Private _companyName As String
		Private _nearEndUsage As Integer
		Private _avgDailyUsage As Integer
		Private _weeklyFlightDays As Integer
		Private _programGuid As Guid
		Private _isActive As Boolean
		Private _isDeleted As Boolean
		Private _utcDateCreated As DateTime
		Private _utcDateModified As DateTime
		Private _StampUserName As String
		Private _concurrencyId As String
		
		Private _brokenRulesList As BrokenRulesList = New BrokenRulesList()
		
        <DataObjectFieldAttribute(True, True, False)> _
        Public Property ProgID() As Integer
            Get
                Return _progID
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _progID = 0
                Else
                    _progID = value
                End If
            End Set
        End Property
		
		Public Property ProgName() as String
			Get
				Return _progName
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_progName = String.Empty
				Else
					_progName = value
				End If
			End Set
		End Property
		
		Public Property ProgDescription() as String
			Get
				Return _progDescription
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_progDescription = String.Empty
				Else
					_progDescription = value
				End If
			End Set
		End Property
		
		Public Property ProgVersion() as String
			Get
				Return _progVersion
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_progVersion = String.Empty
				Else
					_progVersion = value
				End If
			End Set
		End Property
		
		Public Property ProgramLogo() as String
			Get
				Return _programLogo
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_programLogo = String.Empty
				Else
					_programLogo = value
				End If
			End Set
		End Property
		
		Public Property ReportLogo() as String
			Get
				Return _reportLogo
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_reportLogo = String.Empty
				Else
					_reportLogo = value
				End If
			End Set
		End Property
		
		Public Property YearLogo() as String
			Get
				Return _yearLogo
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_yearLogo = String.Empty
				Else
					_yearLogo = value
				End If
			End Set
		End Property
		
		Public Property ProgStatus() as Byte
			Get
				Return _progStatus
			End Get
			Set(ByVal value As Byte)
				If (value = Nothing) Then 
					_progStatus = 0
				Else
					_progStatus = value
				End If
			End Set
		End Property
		
		Public Property DateOpened() as String
			Get
				Return _dateOpened
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_dateOpened = String.Empty
				Else
					_dateOpened = value
				End If
			End Set
		End Property
		
		Public Property DateClosed() as String
			Get
				Return _dateClosed
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_dateClosed = String.Empty
				Else
					_dateClosed = value
				End If
			End Set
		End Property
		
		Public Property ActualValue() as Decimal
			Get
				Return _actualValue
			End Get
			Set(ByVal value As Decimal)
				If (value = Nothing) Then 
					_actualValue = 0
				Else
					_actualValue = value
				End If
			End Set
		End Property
		
		Public Property ProjectCode() as String
			Get
				Return _projectCode
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_projectCode = String.Empty
				Else
					_projectCode = value
				End If
			End Set
		End Property
		
		Public Property ActivityCode() as String
			Get
				Return _activityCode
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_activityCode = String.Empty
				Else
					_activityCode = value
				End If
			End Set
		End Property
		
		Public Property DeptCode() as String
			Get
				Return _deptCode
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_deptCode = String.Empty
				Else
					_deptCode = value
				End If
			End Set
		End Property
		
		Public Property CompanyName() as String
			Get
				Return _companyName
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_companyName = String.Empty
				Else
					_companyName = value
				End If
			End Set
		End Property
		
		Public Property NearEndUsage() as Integer
			Get
				Return _nearEndUsage
			End Get
			Set(ByVal value As Integer)
				If (value = Nothing) Then 
					_nearEndUsage = 0
				Else
					_nearEndUsage = value
				End If
			End Set
		End Property
		
		Public Property AvgDailyUsage() as Integer
			Get
				Return _avgDailyUsage
			End Get
			Set(ByVal value As Integer)
				If (value = Nothing) Then 
					_avgDailyUsage = 0
				Else
					_avgDailyUsage = value
				End If
			End Set
		End Property
		
		Public Property WeeklyFlightDays() as Integer
			Get
				Return _weeklyFlightDays
			End Get
			Set(ByVal value As Integer)
				If (value = Nothing) Then 
					_weeklyFlightDays = 0
				Else
					_weeklyFlightDays = value
				End If
			End Set
		End Property
		
		Public Property ProgramGuid() as Guid
			Get
				Return _programGuid
			End Get
			Set(ByVal value As Guid)
				If (value = Nothing) Then 
					_programGuid = Guid.Empty
				Else
					_programGuid = value
				End If
			End Set
		End Property
		
		Public Property IsActive() as Boolean
			Get
				Return _isActive
			End Get
			Set(ByVal value As Boolean)
				_isActive = value
			End Set
		End Property
		
		Public Property IsDeleted() as Boolean
			Get
				Return _isDeleted
			End Get
			Set(ByVal value As Boolean)
				_isDeleted = value
			End Set
		End Property
		
		Public Property UtcDateCreated() as Date
			Get
				Return _utcDateCreated
			End Get
			Set(ByVal value As Date)
				If (value = Nothing) Then 
					_utcDateCreated = Nothing
				Else
					_utcDateCreated = value
				End If
			End Set
		End Property
		
		Public Property UtcDateModified() as Date
			Get
				Return _utcDateModified
			End Get
			Set(ByVal value As Date)
				If (value = Nothing) Then 
					_utcDateModified = Nothing
				Else
					_utcDateModified = value
				End If
			End Set
		End Property
		
		Public Property StampUserName() as String
			Get
				Return _StampUserName
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_StampUserName = String.Empty
				Else
					_StampUserName = value
				End If
			End Set
		End Property
		
		Public Property ConcurrencyId() as String
			Get
				Return _concurrencyId
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_concurrencyId = String.Empty
				Else
					_concurrencyId = value
				End If
			End Set
		End Property
		
		Public ReadOnly Property BrokenRules As BrokenRulesList
			Get
				Return _brokenRulesList
			End Get
		End Property
		
		Public Sub New()
			Me.ProgID = 0
			Me.ProgName = String.Empty
			Me.ProgDescription = String.Empty
			Me.ProgVersion = String.Empty
			Me.ProgramLogo = String.Empty
			Me.ReportLogo = String.Empty
			Me.YearLogo = String.Empty
			Me.ProgStatus = 0
			Me.DateOpened = String.Empty
			Me.DateClosed = String.Empty
			Me.ActualValue = 0
			Me.ProjectCode = String.Empty
			Me.ActivityCode = String.Empty
			Me.DeptCode = String.Empty
			Me.CompanyName = String.Empty
			Me.NearEndUsage = 0
			Me.AvgDailyUsage = 0
			Me.WeeklyFlightDays = 0
			Me.ProgramGuid = Guid.Empty
			Me.IsActive = True
			Me.IsDeleted = False
			Me.UtcDateCreated = Nothing
			Me.UtcDateModified = Nothing
			Me.StampUserName = String.Empty
			Me.ConcurrencyId = "0"
			_brokenRulesList = New BrokenRulesList
		End Sub
	
        Public Sub New(ByVal ProgID As Integer, ByVal ProgName As String, ByVal ProgDescription As String, ByVal ProgVersion As String, ByVal ProgramLogo As String, ByVal ReportLogo As String, ByVal YearLogo As String, ByVal ProgStatus As Byte, ByVal DateOpened As String, ByVal DateClosed As String, ByVal ActualValue As Decimal, ByVal ProjectCode As String, ByVal ActivityCode As String, ByVal DeptCode As String, ByVal CompanyName As String, ByVal NearEndUsage As Integer, ByVal AvgDailyUsage As Integer, ByVal WeeklyFlightDays As Integer, ByVal ProgramGuid As Guid, ByVal IsActive As Boolean, ByVal IsDeleted As Boolean, ByVal UtcDateCreated As DateTime, ByVal UtcDateModified As DateTime, ByVal StampUserName As String, ByVal ConcurrencyId As String)
            Me.ProgID = ProgID
            Me.ProgName = ProgName
            Me.ProgDescription = ProgDescription
            Me.ProgVersion = ProgVersion
            Me.ProgramLogo = ProgramLogo
            Me.ReportLogo = ReportLogo
            Me.YearLogo = YearLogo
            Me.ProgStatus = ProgStatus
            Me.DateOpened = DateOpened
            Me.DateClosed = DateClosed
            Me.ActualValue = ActualValue
            Me.ProjectCode = ProjectCode
            Me.ActivityCode = ActivityCode
            Me.DeptCode = DeptCode
            Me.CompanyName = CompanyName
            Me.NearEndUsage = NearEndUsage
            Me.AvgDailyUsage = AvgDailyUsage
            Me.WeeklyFlightDays = WeeklyFlightDays
            Me.ProgramGuid = ProgramGuid
            Me.IsActive = IsActive
            Me.IsDeleted = IsDeleted
            Me.UtcDateCreated = UtcDateCreated
            Me.UtcDateModified = UtcDateModified
            Me.StampUserName = StampUserName
            Me.ConcurrencyId = ConcurrencyId
            _brokenRulesList = New BrokenRulesList
        End Sub
	
		Public Function Validate() As Boolean
			_brokenRulesList = new BrokenRulesList()
			If (_progName = Nothing) Then
				_brokenRulesList.Add(New BrokenRule("ProgName", "ProgName is required"))
			Else
				If Len(_progName) = 0 Then
					_brokenRulesList.Add(New BrokenRule("ProgName", "ProgName is required"))
				End If
			End If
			If (_progName.Length > 50) Then
				_brokenRulesList.Add(New BrokenRule("ProgName", "ProgName cannot be longer than 50"))
            End If
            If ObjectPropertyValidator.ValidateCommonString(_progName) = False Then
                _brokenRulesList.Add(New BrokenRule("ProgName", "ProgName" & ObjectPropertyValidator.CommonStringIsInvalidMessage))
            End If
			If (_progDescription <> Nothing) Then
			If (_progDescription.Length > 255) Then
				_brokenRulesList.Add(New BrokenRule("ProgDescription", "ProgDescription cannot be longer than 255"))
			End If
            End If
            If ObjectPropertyValidator.ValidateCommonString(_progDescription) = False Then
                _brokenRulesList.Add(New BrokenRule("ProgDescription", "ProgDescription" & ObjectPropertyValidator.CommonStringIsInvalidMessage))
            End If
			If (_progVersion <> Nothing) Then
			If (_progVersion.Length > 50) Then
				_brokenRulesList.Add(New BrokenRule("ProgVersion", "ProgVersion cannot be longer than 50"))
			End If
			End If
			If (_programLogo <> Nothing) Then
			If (_programLogo.Length > 255) Then
				_brokenRulesList.Add(New BrokenRule("ProgramLogo", "ProgramLogo cannot be longer than 255"))
			End If
			End If
			If (_reportLogo <> Nothing) Then
			If (_reportLogo.Length > 255) Then
				_brokenRulesList.Add(New BrokenRule("ReportLogo", "ReportLogo cannot be longer than 255"))
			End If
			End If
			If (_yearLogo <> Nothing) Then
			If (_yearLogo.Length > 255) Then
				_brokenRulesList.Add(New BrokenRule("YearLogo", "YearLogo cannot be longer than 255"))
			End If
			End If
			If (_dateOpened <> Nothing) Then
			If (_dateOpened.Length > 50) Then
				_brokenRulesList.Add(New BrokenRule("DateOpened", "DateOpened cannot be longer than 50"))
			End If
			End If
			If (_dateClosed <> Nothing) Then
			If (_dateClosed.Length > 50) Then
				_brokenRulesList.Add(New BrokenRule("DateClosed", "DateClosed cannot be longer than 50"))
			End If
			End If
			If (_projectCode <> Nothing) Then
			If (_projectCode.Length > 50) Then
				_brokenRulesList.Add(New BrokenRule("ProjectCode", "ProjectCode cannot be longer than 50"))
			End If
			End If
			If (_activityCode <> Nothing) Then
			If (_activityCode.Length > 50) Then
				_brokenRulesList.Add(New BrokenRule("ActivityCode", "ActivityCode cannot be longer than 50"))
			End If
			End If
			If (_deptCode <> Nothing) Then
			If (_deptCode.Length > 50) Then
				_brokenRulesList.Add(New BrokenRule("DeptCode", "DeptCode cannot be longer than 50"))
			End If
			End If
			If (_companyName <> Nothing) Then
			If (_companyName.Length > 255) Then
				_brokenRulesList.Add(New BrokenRule("CompanyName", "CompanyName cannot be longer than 255"))
			End If
			End If
			
			Return (_brokenRulesList.Count = 0)
			
		End Function
		
	End Class
		
End Namespace
