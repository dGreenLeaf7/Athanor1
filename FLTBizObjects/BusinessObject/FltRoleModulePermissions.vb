' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics

Namespace FLT.BO

	<Serializable()> _
	Public Class FltRoleModulePermissions
	
		Private _roleId As Integer
		Private _moduleName As String
		Private _canView As Boolean
		Private _canEdit As Boolean
		Private _isActive As Boolean
		Private _isDeleted As Boolean
		Private _utcDateCreated As DateTime
		Private _utcDateModified As DateTime
		Private _StampUserName As String
		Private _concurrencyId As String
		
		Private _brokenRulesList As BrokenRulesList = New BrokenRulesList()
		
		<DataObjectFieldAttribute(True, True, False)> _
		Public Property RoleId() as Integer
			Get
				Return _roleId
			End Get
			Set(ByVal value As Integer)
				If (value = Nothing) Then 
					_roleId = 0
				Else
					_roleId = value
				End If
			End Set
		End Property
		
		<DataObjectFieldAttribute(True, True, False)> _
		Public Property ModuleName() as String
			Get
				Return _moduleName
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_moduleName = String.Empty
				Else
					_moduleName = value
				End If
			End Set
		End Property
		
		Public Property CanView() as Boolean
			Get
				Return _canView
			End Get
			Set(ByVal value As Boolean)
				_canView = value
			End Set
		End Property
		
		Public Property CanEdit() as Boolean
			Get
				Return _canEdit
			End Get
			Set(ByVal value As Boolean)
				_canEdit = value
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
			Me.RoleId = 0
			Me.ModuleName = String.Empty
			Me.CanView = True
			Me.CanEdit = True
			Me.IsActive = True
			Me.IsDeleted = False
			Me.UtcDateCreated = Nothing
			Me.UtcDateModified = Nothing
			Me.StampUserName = String.Empty
			Me.ConcurrencyId = "0"
			_brokenRulesList = New BrokenRulesList
		End Sub
	
		Public Sub New(ByVal RoleId As Integer, ByVal ModuleName As String, ByVal CanView As Boolean, ByVal CanEdit As Boolean, ByVal IsActive As Boolean, ByVal IsDeleted As Boolean, ByVal UtcDateCreated As DateTime, ByVal UtcDateModified As DateTime, ByVal StampUserName As String, ByVal ConcurrencyId As String)
			Me.RoleId = RoleId
			Me.ModuleName = ModuleName
			Me.CanView = CanView
			Me.CanEdit = CanEdit
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
			If (_roleId = Nothing) Then
				_brokenRulesList.Add(New BrokenRule("RoleId", "RoleId is required"))
			Else
				If Len(_roleId) = 0 Then
					_brokenRulesList.Add(New BrokenRule("RoleId", "RoleId is required"))
				End If
			End If
			If (_moduleName = Nothing) Then
				_brokenRulesList.Add(New BrokenRule("ModuleName", "ModuleName is required"))
			Else
				If Len(_moduleName) = 0 Then
					_brokenRulesList.Add(New BrokenRule("ModuleName", "ModuleName is required"))
				End If
			End If
			If (_moduleName.Length > 50) Then
				_brokenRulesList.Add(New BrokenRule("ModuleName", "ModuleName cannot be longer than 50"))
			End If
			
			Return (_brokenRulesList.Count = 0)
			
		End Function
		
	End Class
		
End Namespace
