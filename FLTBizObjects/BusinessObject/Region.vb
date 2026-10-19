' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics

Namespace FLT.BO

	<Serializable()> _
	Public Class Region
	
        Private _id As Integer
		Private _progId As Integer
		Private _code As String
		Private _name As String
		Private _isActive As Boolean
		Private _concurrencyId As String
		
		Private _brokenRulesList As BrokenRulesList = New BrokenRulesList()
		
        <DataObjectFieldAttribute(True, True, False)> _
        Public Property Id() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _id = 0
                Else
                    _id = value
                End If
            End Set
        End Property
		
		Public Property ProgId() as Integer
			Get
				Return _progId
			End Get
			Set(ByVal value As Integer)
				If (value = Nothing) Then 
					_progId = 0
				Else
					_progId = value
				End If
			End Set
		End Property
		
		Public Property Code() as String
			Get
				Return _code
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_code = String.Empty
				Else
					_code = value
				End If
			End Set
		End Property
		
		Public Property Name() as String
			Get
				Return _name
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_name = String.Empty
				Else
					_name = value
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
			Me.Id = 0
			Me.ProgId = 0
			Me.Code = String.Empty
			Me.Name = String.Empty
			Me.IsActive = True
			Me.ConcurrencyId = "0"
			_brokenRulesList = New BrokenRulesList
		End Sub
	
        Public Sub New(ByVal Id As Integer, ByVal ProgId As Integer, ByVal Code As String, ByVal Name As String, ByVal IsActive As Boolean, ByVal ConcurrencyId As String)
            Me.Id = Id
            Me.ProgId = ProgId
            Me.Code = Code
            Me.Name = Name
            Me.IsActive = IsActive
            Me.ConcurrencyId = ConcurrencyId
            _brokenRulesList = New BrokenRulesList
        End Sub
	
		Public Function Validate() As Boolean
			_brokenRulesList = new BrokenRulesList()
			If (_progId = Nothing) Then
				_brokenRulesList.Add(New BrokenRule("ProgId", "ProgId is required"))
			Else
				If Len(_progId) = 0 Then
					_brokenRulesList.Add(New BrokenRule("ProgId", "ProgId is required"))
				End If
			End If
			If (_code = Nothing) Then
				_brokenRulesList.Add(New BrokenRule("Code", "Code is required"))
			Else
				If Len(_code) = 0 Then
					_brokenRulesList.Add(New BrokenRule("Code", "Code is required"))
				End If
			End If
			If (_code.Length > 2) Then
				_brokenRulesList.Add(New BrokenRule("Code", "Code cannot be longer than 2"))
            End If
            If ObjectPropertyValidator.ValidateCommonString(_code) = False Then
                _brokenRulesList.Add(New BrokenRule("Code", "Code" & ObjectPropertyValidator.CommonStringIsInvalidMessage))
            End If
			If (_name = Nothing) Then
				_brokenRulesList.Add(New BrokenRule("Name", "Name is required"))
			Else
				If Len(_name) = 0 Then
					_brokenRulesList.Add(New BrokenRule("Name", "Name is required"))
				End If
			End If
			If (_name.Length > 255) Then
				_brokenRulesList.Add(New BrokenRule("Name", "Name cannot be longer than 255"))
            End If
            If ObjectPropertyValidator.ValidateCommonString(_name) = False Then
                _brokenRulesList.Add(New BrokenRule("Name", "Name" & ObjectPropertyValidator.CommonStringIsInvalidMessage))
            End If
			
			Return (_brokenRulesList.Count = 0)
			
		End Function
		
	End Class
		
End Namespace
