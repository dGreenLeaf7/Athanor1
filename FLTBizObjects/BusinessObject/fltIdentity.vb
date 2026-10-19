' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel

Namespace FLT.BO

	<Serializable()> _
	Public Class FltIdentity
		
        Private _isLoggedIn As Boolean = False
        Private _userDisplayName As String = String.Empty
        Private _progDisplayName As String = String.Empty
        Private _lastLoginDate As String = String.Empty
        Private _isSuperUser As Boolean = False
        Private _userName As String = String.Empty

        Private _userProgramsList As Collection(Of UserProgram)

        Private _currentProgId As Long = 0
        Private _isCurrentProgMgr As Boolean = False
        Private _currentModulePermissions As Collection(Of UserModulePermissions)

        Public Class UserProgram

            Private _progId As Long = 0
            Private _progName As String = String.Empty

            Public Property ProgId()
                Get
                    Return _progId
                End Get
                Set(ByVal value)
                    _progId = value
                End Set
            End Property

            Public Property ProgName()
                Get
                    Return _progName
                End Get
                Set(ByVal value)
                    _progName = value
                End Set
            End Property

            Public Sub New()
                Me.ProgId = 0
                Me.ProgName = String.Empty
            End Sub

            Public Sub New(ByVal ProgId As Long, ByVal ProgName As String)
                Me.ProgId = ProgId
                Me.ProgName = ProgName
            End Sub

        End Class

        Public Class UserModulePermissions

            Private _moduleName As String = String.Empty
            Private _canView As Boolean = False
            Private _canEdit As Boolean = False

            Public Property ModuleName()
                Get
                    Return _moduleName
                End Get
                Set(ByVal value)
                    _moduleName = value
                End Set
            End Property

            Public Property CanView()
                Get
                    Return _canView
                End Get
                Set(ByVal value)
                    _canView = value
                End Set
            End Property

            Public Property CanEdit()
                Get
                    Return _canEdit
                End Get
                Set(ByVal value)
                    _canEdit = value
                End Set
            End Property

            Public Sub New()
                _moduleName = String.Empty
                _canView = False
                _canEdit = False
            End Sub

            Public Sub New(ByVal ModuleName As String, ByVal CanView As Boolean, ByVal CanEdit As Boolean)
                Me.ModuleName = ModuleName
                Me.CanView = CanView
                Me.CanEdit = CanEdit
            End Sub

        End Class

        Public ReadOnly Property IsLoggedIn() As Boolean
            Get
                Return _isLoggedIn
            End Get
        End Property

        Public ReadOnly Property UserDisplayName() As String
            Get
                Return _userDisplayName
            End Get
        End Property

        Public Property ProgDisplayName() As String
            Get
                Return _progDisplayName
            End Get
            Set(ByVal value As String)
                _progDisplayName = value
            End Set
        End Property

        Public ReadOnly Property LastLoginDate() As String
            Get
                Return _lastLoginDate
            End Get
        End Property

        Public ReadOnly Property UserName() As String
            Get
                Return _userName
            End Get
        End Property

        Public ReadOnly Property IsSuperUser() As Boolean
            Get
                Return _isSuperUser
            End Get
        End Property

        Public Property CurrentProgId() As Long
            Get
                Return _currentProgId
            End Get
            Set(ByVal value As Long)
                _currentProgId = value
            End Set
        End Property

        Public Property IsCurrentProgramManager() As Boolean
            Get
                Return _isCurrentProgMgr
            End Get
            Set(ByVal value As Boolean)
                _isCurrentProgMgr = value
            End Set
        End Property

        Public ReadOnly Property UserProgramsList() As Collection(Of UserProgram)
            Get
                Return _userProgramsList
            End Get
        End Property

        Public ReadOnly Property CurrentModulePermissionsList() As Collection(Of UserModulePermissions)
            Get
                Return _currentModulePermissions
            End Get
        End Property

        Public Sub New()
            _isLoggedIn = False
            _userDisplayName = "Unknown"
            _userName = String.Empty
            _isSuperUser = False
            _currentProgId = 0
            _isCurrentProgMgr = False
            _userProgramsList = New Collection(Of UserProgram)
            _currentModulePermissions = New Collection(Of UserModulePermissions)
        End Sub

        Public Sub New(ByVal IsLoggedIn As Boolean, ByVal UserDisplayName As String, ByVal ProgDisplayName As String, ByVal LastLoginDate As DateTime, ByVal UserName As String, ByVal IsSuperUser As Boolean, ByVal CurrentProgId As Long, ByVal IsCurrentProgMgr As Boolean)
            _isLoggedIn = IsLoggedIn
            _userDisplayName = UserDisplayName
            _lastLoginDate = LastLoginDate
            _userName = UserName
            _isSuperUser = IsSuperUser
            _currentProgId = CurrentProgId
            _isCurrentProgMgr = IsCurrentProgMgr
            _userProgramsList = New Collection(Of UserProgram)
            _userProgramsList = New Collection(Of UserProgram)
            _currentModulePermissions = New Collection(Of UserModulePermissions)
        End Sub

    End Class

End Namespace
