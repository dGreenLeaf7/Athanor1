' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Text.RegularExpressions

Namespace FLT.BO

    <Serializable()> _
    Public Class FltUser

        Private _userName As String
        Private _password As String
        Private _passwordSalt As String
        Private _email As String
        Private _passwordQuestion As String
        Private _passwordAnswer As String
        Private _isSuperUser As Boolean
        Private _isApproved As Boolean
        Private _isLockedOut As Boolean
        Private _utcLastLoginDate As DateTime
        Private _utcLastLockoutDate As DateTime
        Private _utcLastPasswordChangedDate As DateTime
        Private _failedPasswordAttemptCount As Integer
        Private _utcFailedPasswordAttemptWindowStart As DateTime
        Private _failedPasswordAnswerAttemptCount As Integer
        Private _utcFailedPasswordAnswerAttemptWindowStart As DateTime
        Private _displayName As String
        Private _location As String
        Private _countryCode As String
        Private _phoneNumber As String
        Private _mgrUserName As String
        Private _department As String
        Private _title As String
        Private _company As String
        Private _defaultProgId As Integer
        Private _isActive As Boolean
        Private _isDeleted As Boolean
        Private _utcDateCreated As DateTime
        Private _utcDateModified As DateTime
        Private _StampUserName As String
        Private _concurrencyId As String

        Private _brokenRulesList As BrokenRulesList = New BrokenRulesList()

        <DataObjectFieldAttribute(True, True, False)> _
        Public Property UserName() As String
            Get
                Return _userName
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _userName = String.Empty
                Else
                    _userName = value
                End If
            End Set
        End Property

        Public Property Password() As String
            Get
                Return _password
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _password = String.Empty
                Else
                    _password = value
                End If
            End Set
        End Property

        Public Property PasswordSalt() As String
            Get
                Return _passwordSalt
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _passwordSalt = String.Empty
                Else
                    _passwordSalt = value
                End If
            End Set
        End Property

        Public Property Email() As String
            Get
                Return _email
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _email = String.Empty
                Else
                    _email = value
                End If
            End Set
        End Property

        Public Property PasswordQuestion() As String
            Get
                Return _passwordQuestion
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _passwordQuestion = String.Empty
                Else
                    _passwordQuestion = value
                End If
            End Set
        End Property

        Public Property PasswordAnswer() As String
            Get
                Return _passwordAnswer
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _passwordAnswer = String.Empty
                Else
                    _passwordAnswer = value
                End If
            End Set
        End Property

        Public Property IsSuperUser() As Boolean
            Get
                Return _isSuperUser
            End Get
            Set(ByVal value As Boolean)
                _isSuperUser = value
            End Set
        End Property

        Public Property IsApproved() As Boolean
            Get
                Return _isApproved
            End Get
            Set(ByVal value As Boolean)
                _isApproved = value
            End Set
        End Property

        Public Property IsLockedOut() As Boolean
            Get
                Return _isLockedOut
            End Get
            Set(ByVal value As Boolean)
                _isLockedOut = value
            End Set
        End Property

        Public Property UtcLastLoginDate() As DateTime
            Get
                Return _utcLastLoginDate
            End Get
            Set(ByVal value As DateTime)
                If (value = Nothing) Then
                    _utcLastLoginDate = DateTime.MinValue
                Else
                    _utcLastLoginDate = value
                End If
            End Set
        End Property

        Public Property UtcLastLockoutDate() As DateTime
            Get
                Return _utcLastLockoutDate
            End Get
            Set(ByVal value As DateTime)
                If (value = Nothing) Then
                    _utcLastLockoutDate = DateTime.MinValue
                Else
                    _utcLastLockoutDate = value
                End If
            End Set
        End Property

        Public Property UtcLastPasswordChangedDate() As DateTime
            Get
                Return _utcLastPasswordChangedDate
            End Get
            Set(ByVal value As DateTime)
                If (value = Nothing) Then
                    _utcLastPasswordChangedDate = DateTime.MinValue
                Else
                    _utcLastPasswordChangedDate = value
                End If
            End Set
        End Property

        Public Property FailedPasswordAttemptCount() As Integer
            Get
                Return _failedPasswordAttemptCount
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _failedPasswordAttemptCount = 0
                Else
                    _failedPasswordAttemptCount = value
                End If
            End Set
        End Property

        Public Property UtcFailedPasswordAttemptWindowStart() As DateTime
            Get
                Return _utcFailedPasswordAttemptWindowStart
            End Get
            Set(ByVal value As DateTime)
                If (value = Nothing) Then
                    _utcFailedPasswordAttemptWindowStart = DateTime.MinValue
                Else
                    _utcFailedPasswordAttemptWindowStart = value
                End If
            End Set
        End Property

        Public Property FailedPasswordAnswerAttemptCount() As Integer
            Get
                Return _failedPasswordAnswerAttemptCount
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _failedPasswordAnswerAttemptCount = 0
                Else
                    _failedPasswordAnswerAttemptCount = value
                End If
            End Set
        End Property

        Public Property UtcFailedPasswordAnswerAttemptWindowStart() As DateTime
            Get
                Return _utcFailedPasswordAnswerAttemptWindowStart
            End Get
            Set(ByVal value As DateTime)
                If (value = Nothing) Then
                    _utcFailedPasswordAnswerAttemptWindowStart = DateTime.MinValue
                Else
                    _utcFailedPasswordAnswerAttemptWindowStart = value
                End If
            End Set
        End Property

        Public Property DisplayName() As String
            Get
                Return _displayName
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _displayName = String.Empty
                Else
                    _displayName = value
                End If
            End Set
        End Property

        Public Property Location() As String
            Get
                Return _location
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _location = String.Empty
                Else
                    _location = value
                End If
            End Set
        End Property

        Public Property CountryCode() As String
            Get
                Return _countryCode
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _countryCode = String.Empty
                Else
                    _countryCode = value
                End If
            End Set
        End Property

        Public Property PhoneNumber() As String
            Get
                Return _phoneNumber
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _phoneNumber = String.Empty
                Else
                    _phoneNumber = value
                End If
            End Set
        End Property

        Public Property MgrUserName() As String
            Get
                Return _mgrUserName
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _mgrUserName = String.Empty
                Else
                    _mgrUserName = value
                End If
            End Set
        End Property

        Public Property Department() As String
            Get
                Return _department
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _department = String.Empty
                Else
                    _department = value
                End If
            End Set
        End Property

        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _title = String.Empty
                Else
                    _title = value
                End If
            End Set
        End Property

        Public Property Company() As String
            Get
                Return _company
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _company = String.Empty
                Else
                    _company = value
                End If
            End Set
        End Property

        Public Property DefaultProgId() As Integer
            Get
                Return _defaultProgId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _defaultProgId = 0
                Else
                    _defaultProgId = value
                End If
            End Set
        End Property

        Public Property IsActive() As Boolean
            Get
                Return _isActive
            End Get
            Set(ByVal value As Boolean)
                _isActive = value
            End Set
        End Property

        Public Property IsDeleted() As Boolean
            Get
                Return _isDeleted
            End Get
            Set(ByVal value As Boolean)
                _isDeleted = value
            End Set
        End Property

        Public Property UtcDateCreated() As Date
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

        Public Property UtcDateModified() As Date
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

        Public Property StampUserName() As String
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

        Public Property ConcurrencyId() As String
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
            Me.UserName = String.Empty
            Me.Password = String.Empty
            Me.PasswordSalt = String.Empty
            Me.Email = String.Empty
            Me.PasswordQuestion = String.Empty
            Me.PasswordAnswer = String.Empty
            Me.IsSuperUser = True
            Me.IsApproved = True
            Me.IsLockedOut = True
            Me.UtcLastLoginDate = DateTime.MinValue
            Me.UtcLastLockoutDate = DateTime.MinValue
            Me.UtcLastPasswordChangedDate = DateTime.MinValue
            Me.FailedPasswordAttemptCount = 0
            Me.UtcFailedPasswordAttemptWindowStart = DateTime.MinValue
            Me.FailedPasswordAnswerAttemptCount = 0
            Me.UtcFailedPasswordAnswerAttemptWindowStart = DateTime.MinValue
            Me.DisplayName = String.Empty
            Me.Location = String.Empty
            Me.CountryCode = String.Empty
            Me.PhoneNumber = String.Empty
            Me.MgrUserName = String.Empty
            Me.Department = String.Empty
            Me.Title = String.Empty
            Me.Company = String.Empty
            Me.DefaultProgId = 0
            Me.IsActive = True
            Me.IsDeleted = False
            Me.UtcDateCreated = Nothing
            Me.UtcDateModified = Nothing
            Me.StampUserName = String.Empty
            Me.ConcurrencyId = "0"
            _brokenRulesList = New BrokenRulesList
        End Sub

        Public Sub New(ByVal UserName As String, ByVal Password As String, ByVal PasswordSalt As String, ByVal Email As String, ByVal PasswordQuestion As String, ByVal PasswordAnswer As String, ByVal IsSuperUser As Boolean, ByVal IsApproved As Boolean, ByVal IsLockedOut As Boolean, ByVal UtcLastLoginDate As DateTime, ByVal UtcLastLockoutDate As DateTime, ByVal UtcLastPasswordChangedDate As DateTime, ByVal FailedPasswordAttemptCount As Integer, ByVal UtcFailedPasswordAttemptWindowStart As DateTime, ByVal FailedPasswordAnswerAttemptCount As Integer, ByVal UtcFailedPasswordAnswerAttemptWindowStart As DateTime, ByVal DisplayName As String, ByVal Location As String, ByVal CountryCode As String, ByVal PhoneNumber As String, ByVal MgrUserName As String, ByVal Department As String, ByVal Title As String, ByVal Company As String, ByVal DefaultProgId As Integer, ByVal IsActive As Boolean, ByVal IsDeleted As Boolean, ByVal UtcDateCreated As DateTime, ByVal UtcDateModified As DateTime, ByVal StampUserName As String, ByVal ConcurrencyId As String)
            Me.UserName = UserName
            Me.Password = Password
            Me.PasswordSalt = PasswordSalt
            Me.Email = Email
            Me.PasswordQuestion = PasswordQuestion
            Me.PasswordAnswer = PasswordAnswer
            Me.IsSuperUser = IsSuperUser
            Me.IsApproved = IsApproved
            Me.IsLockedOut = IsLockedOut
            Me.UtcLastLoginDate = UtcLastLoginDate
            Me.UtcLastLockoutDate = UtcLastLockoutDate
            Me.UtcLastPasswordChangedDate = UtcLastPasswordChangedDate
            Me.FailedPasswordAttemptCount = FailedPasswordAttemptCount
            Me.UtcFailedPasswordAttemptWindowStart = UtcFailedPasswordAttemptWindowStart
            Me.FailedPasswordAnswerAttemptCount = FailedPasswordAnswerAttemptCount
            Me.UtcFailedPasswordAnswerAttemptWindowStart = UtcFailedPasswordAnswerAttemptWindowStart
            Me.DisplayName = DisplayName
            Me.Location = Location
            Me.CountryCode = CountryCode
            Me.PhoneNumber = PhoneNumber
            Me.MgrUserName = MgrUserName
            Me.Department = Department
            Me.Title = Title
            Me.Company = Company
            Me.DefaultProgId = DefaultProgId
            Me.IsActive = IsActive
            Me.IsDeleted = IsDeleted
            Me.UtcDateCreated = UtcDateCreated
            Me.UtcDateModified = UtcDateModified
            Me.StampUserName = StampUserName
            Me.ConcurrencyId = ConcurrencyId
            _brokenRulesList = New BrokenRulesList
        End Sub

        Public Function Validate() As Boolean
            _brokenRulesList = New BrokenRulesList()
            If (_userName = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("UserName", "UserName is required"))
            Else
                If Len(_userName) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("UserName", "UserName is required"))
                End If
            End If
            If (_userName.Length > 20) Then
                _brokenRulesList.Add(New BrokenRule("UserName", "UserName cannot be longer than 20"))
            End If
            If (_password = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("Password", "Password is required"))
            Else
                If Len(_password) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("Password", "Password is required"))
                End If
            End If
            If (_password.Length > 128) Then
                _brokenRulesList.Add(New BrokenRule("Password", "Password cannot be longer than 128"))
            End If
            If (_passwordSalt <> Nothing) Then
                If (_passwordSalt.Length > 128) Then
                    _brokenRulesList.Add(New BrokenRule("PasswordSalt", "PasswordSalt cannot be longer than 128"))
                End If
            End If
            If ObjectPropertyValidator.ValidateUserNameString(_userName) = False Then
                _brokenRulesList.Add(New BrokenRule("UserName", "UserName " & ObjectPropertyValidator.UserNameStringIsInvalidMessage))
            End If
            If ObjectPropertyValidator.ValidateCommonString(_displayName) = False Then
                _brokenRulesList.Add(New BrokenRule("DisplayName", "DisplayName" & ObjectPropertyValidator.CommonStringIsInvalidMessage))
            End If
            If (_email = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("Email", "Email is required"))
            Else
                If Len(_email) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("Email", "Email is required"))
                End If
            End If
            If (_email.Length > 255) Then
                _brokenRulesList.Add(New BrokenRule("Email", "Email cannot be longer than 255"))
            End If
            If (_passwordQuestion <> Nothing) Then
                If (_passwordQuestion.Length > 255) Then
                    _brokenRulesList.Add(New BrokenRule("PasswordQuestion", "PasswordQuestion cannot be longer than 255"))
                End If
            End If
            If (_passwordAnswer <> Nothing) Then
                If (_passwordAnswer.Length > 128) Then
                    _brokenRulesList.Add(New BrokenRule("PasswordAnswer", "PasswordAnswer cannot be longer than 128"))
                End If
            End If
            If (_displayName <> Nothing) Then
                If (_displayName.Length > 30) Then
                    _brokenRulesList.Add(New BrokenRule("DisplayName", "DisplayName cannot be longer than 30"))
                End If
            End If
            If (_location <> Nothing) Then
                If (_location.Length > 30) Then
                    _brokenRulesList.Add(New BrokenRule("Location", "Location cannot be longer than 30"))
                End If
            End If
            If (_countryCode <> Nothing) Then
                If (_countryCode.Length > 20) Then
                    _brokenRulesList.Add(New BrokenRule("CountryCode", "CountryCode cannot be longer than 20"))
                End If
            End If
            If (_phoneNumber <> Nothing) Then
                If (_phoneNumber.Length > 20) Then
                    _brokenRulesList.Add(New BrokenRule("PhoneNumber", "PhoneNumber cannot be longer than 20"))
                End If
            End If
            If (_mgrUserName <> Nothing) Then
                If (_mgrUserName.Length > 20) Then
                    _brokenRulesList.Add(New BrokenRule("MgrUserName", "MgrUserName cannot be longer than 20"))
                End If
            End If
            If (_department <> Nothing) Then
                If (_department.Length > 30) Then
                    _brokenRulesList.Add(New BrokenRule("Department", "Department cannot be longer than 30"))
                End If
            End If
            If (_title <> Nothing) Then
                If (_title.Length > 30) Then
                    _brokenRulesList.Add(New BrokenRule("Title", "Title cannot be longer than 30"))
                End If
            End If
            If (_company <> Nothing) Then
                If (_company.Length > 30) Then
                    _brokenRulesList.Add(New BrokenRule("Company", "Company cannot be longer than 30"))
                End If
            End If

            Return (_brokenRulesList.Count = 0)

        End Function

    End Class

End Namespace
