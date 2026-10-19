' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Diagnostics
Imports FLTBizObjects.FLT.Dal

Namespace FLT.BO

	<Serializable()> _
	Public Class AircraftConfiguration

        Private _aircraftConfigurationId As Integer
		Private _aircraftId As Integer
		Private _configurationDescription As String
        Private _configurationStartDate As String
        Private _configurationEndDate As String
        Private _configurationMeterStart As Decimal
        Private _configurationMeterEnd As Decimal
        Private _configurationMeterOffset As Decimal
        Private _meterUnitId As Integer
        Private _meterUnit As String
        Private _modelId As Integer
        Private _modelDescr As String
        Private _engineTypeId As Integer
        Private _engineType As String
        Private _engineEmissionId As Integer
        Private _engineEmission As String
        Private _engineMaturityId As Integer
        Private _engineMaturity As String
        Private _engineSerialNbr As String
        Private _propellerTypeId As Integer
        Private _propellerType As String
        Private _propellerModelNbr As String
        Private _propellerSerialNo As String
        Private _aPUTypeId As Integer
        Private _aPUType As String
        Private _aPUEmissionId As Integer
        Private _aPUEmission As String
        Private _aPUMaturityId As Integer
        Private _aPUMaturity As String
        Private _aPUSerialNbr As String
        Private _noseGear As String
        Private _mainGear As String
        Private _avionicsversion As String
        Private _emissionLevel As String
        Private _aircraftConfigurationGuid As Guid
        Private _isDeleted As Boolean
		Private _utcDateCreated As DateTime
		Private _utcDateModified As DateTime
		Private _StampUserName As String
		Private _concurrencyId As String
		
        Private _brokenRulesList As BrokenRulesList = Nothing
        Private _errorsList As BrokenRulesList = Nothing
        Private _warningsList As BrokenRulesList = Nothing

        <DataObjectFieldAttribute(True, True, False)> _
        Public Property AircraftConfigurationId() As Integer
            Get
                Return _aircraftConfigurationId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _aircraftConfigurationId = 0
                Else
                    _aircraftConfigurationId = value
                End If
            End Set
        End Property
		
		Public Property AircraftId() as Integer
			Get
				Return _aircraftId
			End Get
			Set(ByVal value As Integer)
				If (value = Nothing) Then 
					_aircraftId = 0
				Else
					_aircraftId = value
				End If
			End Set
		End Property
		
		Public Property ConfigurationDescription() as String
			Get
				Return _configurationDescription
			End Get
			Set(ByVal value As String)
				If (value = Nothing) Then 
					_configurationDescription = String.Empty
				Else
					_configurationDescription = value
				End If
			End Set
		End Property
		
        Public Property ConfigurationStartDate() As String
            Get
                Return _configurationStartDate
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _configurationStartDate = String.Empty
                ElseIf IsDate(value) Then
                    _configurationStartDate = Format(CDate(value), "MM/dd/yyyy")
                Else
                    _configurationStartDate = value
                End If
            End Set
        End Property
		
        Public Property ConfigurationEndDate() As String
            Get
                Return _configurationEndDate
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _configurationEndDate = String.Empty
                ElseIf IsDate(value) Then
                    _configurationEndDate = Format(CDate(value), "MM/dd/yyyy")
                Else
                    _configurationEndDate = value
                End If
            End Set
        End Property
		
        Public Property ConfigurationMeterStart() As String
            Get
                Return _configurationMeterStart
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _configurationMeterStart = 0.0
                ElseIf IsNumeric(value) Then
                    _configurationMeterStart = CDec(value)
                Else
                    _configurationMeterStart = 0.0
                End If
            End Set
        End Property
		
        Public Property ConfigurationMeterEnd() As String
            Get
                Return _configurationMeterEnd
            End Get
            Friend Set(ByVal value As String)
                If (value = Nothing) Then
                    _configurationMeterEnd = 0.0
                ElseIf IsNumeric(value) Then
                    _configurationMeterEnd = CDec(value)
                Else
                    _configurationMeterEnd = 0.0
                End If
            End Set
        End Property

        Public Property ConfigurationMeterOffset() As String
            Get
                Return _configurationMeterOffset
            End Get
            Set(ByVal value As String)
                If value = Nothing Then
                    _configurationMeterOffset = 0.0
                ElseIf IsNumeric(value) Then
                    _configurationMeterOffset = CDec(value)
                Else
                    _configurationMeterOffset = 0.0
                End If
            End Set
        End Property

        Public Property MeterUnitId() As Integer
            Get
                Return _meterUnitId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _meterUnitId = 0
                Else
                    _meterUnitId = value
                End If
            End Set
        End Property

        Public Property MeterUnit() As String
            Get
                Return _meterUnit
            End Get
            Set(ByVal value As String)
                _meterUnit = value
            End Set
        End Property

        Public Property ModelId() As Integer
            Get
                Return _modelId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _modelId = 0
                Else
                    _modelId = value
                End If
            End Set
        End Property

        Public Property ModelDescr() As String
            Get
                Return _modelDescr
            End Get
            Set(ByVal value As String)
                _modelDescr = value
            End Set
        End Property

        Public Property EngineTypeId() As Integer
            Get
                Return _engineTypeId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _engineTypeId = 0
                Else
                    _engineTypeId = value
                End If
            End Set
        End Property

        Public Property EngineType() As String
            Get
                Return _engineType
            End Get
            Set(ByVal value As String)
                _engineType = value
            End Set
        End Property

        Public Property EngineEmissionId() As Integer
            Get
                Return _engineEmissionId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _engineEmissionId = 0
                Else
                    _engineEmissionId = value
                End If
            End Set
        End Property

        Public Property EngineEmission() As String
            Get
                Return _engineEmission
            End Get
            Set(ByVal value As String)
                _engineEmission = value
            End Set
        End Property

        Public Property EngineMaturityId() As Integer
            Get
                Return _engineMaturityId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _engineMaturityId = 0
                Else
                    _engineMaturityId = value
                End If
            End Set
        End Property

        Public Property EngineMaturity() As String
            Get
                Return _engineMaturity
            End Get
            Set(ByVal value As String)
                _engineMaturity = value
            End Set
        End Property

        Public Property EngineSerialNbr() As String
            Get
                Return _engineSerialNbr
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _engineSerialNbr = String.Empty
                Else
                    _engineSerialNbr = value
                End If
            End Set
        End Property

        Public Property APUTypeId() As Integer
            Get
                Return _aPUTypeId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _aPUTypeId = 0
                Else
                    _aPUTypeId = value
                End If
            End Set
        End Property

        Public Property APUType() As String
            Get
                Return _aPUType
            End Get
            Set(ByVal value As String)
                _aPUType = value
            End Set
        End Property

        Public Property APUEmissionId() As Integer
            Get
                Return _aPUEmissionId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _aPUEmissionId = 0
                Else
                    _aPUEmissionId = value
                End If
            End Set
        End Property

        Public Property APUEmission() As String
            Get
                Return _aPUEmission
            End Get
            Set(ByVal value As String)
                _aPUEmission = value
            End Set
        End Property

        Public Property APUMaturityId() As Integer
            Get
                Return _aPUMaturityId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _aPUMaturityId = 0
                Else
                    _aPUMaturityId = value
                End If
            End Set
        End Property

        Public Property APUMaturity() As String
            Get
                Return _aPUMaturity
            End Get
            Set(ByVal value As String)
                _aPUMaturity = value
            End Set
        End Property

        Public Property APUSerialNbr() As String
            Get
                Return _aPUSerialNbr
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _aPUSerialNbr = String.Empty
                Else
                    _aPUSerialNbr = value
                End If
            End Set
        End Property

        Public Property NoseGear() As String
            Get
                Return _noseGear
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _noseGear = String.Empty
                Else
                    _noseGear = value
                End If
            End Set
        End Property

        Public Property MainGear() As String
            Get
                Return _mainGear
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _mainGear = String.Empty
                Else
                    _mainGear = value
                End If
            End Set
        End Property

        Public Property AvionicsVersion() As String
            Get
                Return _avionicsversion
            End Get
            Set(ByVal value As String)
                _avionicsversion = value
            End Set
        End Property

        Public Property EmissionLevel() As String
            Get
                Return _emissionLevel
            End Get
            Set(ByVal value As String)
                _emissionLevel = value
            End Set
        End Property

        Public Property PropellerTypeId() As Integer
            Get
                Return _propellerTypeId
            End Get
            Set(ByVal value As Integer)
                If (value = Nothing) Then
                    _propellerTypeId = 0
                Else
                    _propellerTypeId = value
                End If
            End Set
        End Property

        Public Property PropellerType() As String
            Get
                Return _propellerType
            End Get
            Set(ByVal value As String)
                _propellerType = value
            End Set
        End Property

        Public Property PropellerModelNbr() As String
            Get
                Return _propellerModelNbr
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _propellerModelNbr = String.Empty
                Else
                    _propellerModelNbr = value
                End If
            End Set
        End Property

        Public Property PropellerSerialNo() As String
            Get
                Return _propellerSerialNo
            End Get
            Set(ByVal value As String)
                If (value = Nothing) Then
                    _propellerSerialNo = String.Empty
                Else
                    _propellerSerialNo = value
                End If
            End Set
        End Property

        Public Property AircraftConfigurationGUID() As Guid
            Get
                Return _aircraftConfigurationGuid
            End Get
            Set(ByVal value As Guid)
                If value = Nothing Then
                    _aircraftConfigurationGuid = Guid.Empty
                Else
                    _aircraftConfigurationGuid = value
                End If
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

        Public ReadOnly Property ErrorsList() As BrokenRulesList
            Get
                Return _errorsList
            End Get
        End Property

        Public ReadOnly Property WarningsList() As BrokenRulesList
            Get
                Return _warningsList
            End Get
        End Property

		Public Sub New()
			Me.AircraftConfigurationId = 0
			Me.AircraftId = 0
			Me.ConfigurationDescription = String.Empty
            Me.ConfigurationStartDate = String.Empty
            Me.ConfigurationEndDate = String.Empty
            Me.ConfigurationMeterStart = 0.0
            Me.ConfigurationMeterEnd = 0.0
            Me.ConfigurationMeterOffset = 0.0
            Me.MeterUnitId = 0
            Me.MeterUnit = String.Empty
            Me.ModelId = 0
            Me.ModelDescr = String.Empty
            Me.EngineTypeId = 0
            Me.EngineType = String.Empty
            Me.EngineEmissionId = 0
            Me.EngineEmission = String.Empty
            Me.EngineMaturityId = 0
            Me.EngineMaturity = String.Empty
            Me.EngineSerialNbr = String.Empty
            Me.APUTypeId = 0
            Me.APUType = String.Empty
            Me.APUEmissionId = 0
            Me.APUEmission = String.Empty
            Me.APUMaturityId = 0
            Me.APUMaturity = String.Empty
            Me.APUSerialNbr = String.Empty
            Me.NoseGear = String.Empty
            Me.MainGear = String.Empty
            Me.AvionicsVersion = String.Empty
            Me.EmissionLevel = String.Empty
            Me.PropellerTypeId = 0
            Me.PropellerType = String.Empty
            Me.PropellerModelNbr = String.Empty
            Me.PropellerSerialNo = String.Empty
            Me.IsDeleted = False
			Me.UtcDateCreated = Nothing
			Me.UtcDateModified = Nothing
			Me.StampUserName = String.Empty
			Me.ConcurrencyId = "0"
            _brokenRulesList = New BrokenRulesList
            _errorsList = New BrokenRulesList
            _warningsList = New BrokenRulesList
        End Sub

        Public Function Validate() As Boolean
            Return Validate(_warningsList)
        End Function

        Public Function Validate(ByVal WarningsList As BrokenRulesList) As Boolean

            Dim prevWarningsList As New BrokenRulesList
            For Each thisBrokenRule As BrokenRule In WarningsList
                prevWarningsList.Add(thisBrokenRule)
            Next
            _brokenRulesList = New BrokenRulesList

            If (_aircraftId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("AircraftId", "AircraftId is required", "01-02-001", False, False))
            Else
                If Len(_aircraftId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("AircraftId", "AircraftId is required", "01-02-001", False, False))
                End If
            End If
            If (_configurationDescription.Length > 255) Then
                _brokenRulesList.Add(New BrokenRule("ConfigurationDescription", "ConfigurationDescription cannot be longer than 255", "01-02-001", False, False))
            End If
            If (_configurationStartDate = String.Empty) Then
                _brokenRulesList.Add(New BrokenRule("ConfigurationStartDate", "ConfigurationStateDate is required", "01-02-001", False, False))
            Else
                If Not IsDate(_configurationStartDate) Then
                    _brokenRulesList.Add(New BrokenRule("ConfigurationStartDate", "ConfigurationStartDate must be a date", "01-02-001", False, False))
                End If
            End If
            If Not (_configurationEndDate = String.Empty) Then
                If Not IsDate(_configurationEndDate) Then
                    _brokenRulesList.Add(New BrokenRule("ConfigurationEndDate", "ConfigurationEndDate must be a date", "01-02-001", False, False))
                End If
            End If
            If _configurationStartDate <> String.Empty And _configurationEndDate <> String.Empty Then
                If IsDate(_configurationStartDate) And IsDate(_configurationEndDate) Then
                    If DateTime.Compare(_configurationStartDate, _configurationEndDate) > 0 Then
                        _brokenRulesList.Add(New BrokenRule("ConfiguratinonEndDate", "ConfigurationEndDate must be later than ConfigurationStartDate", "01-02-001", False, False))
                    End If
                End If
            End If
            If (_meterUnitId = Nothing) Then
                _brokenRulesList.Add(New BrokenRule("MeterUnitId", "MeterUnitId is required", "01-02-001", False, False))
            Else
                If Len(_meterUnitId) = 0 Then
                    _brokenRulesList.Add(New BrokenRule("MeterUnitId", "MeterUnitId is required", "01-02-001", False, False))
                End If
            End If
            If AppConfiguration.BusinessRuleEnabled("01-02-002") Then
                If (_modelId = Nothing) Then
                    _brokenRulesList.Add(New BrokenRule("ModelId", "ModelId is required", "01-02-002", False, False))
                Else
                    If Len(_modelId) = 0 Then
                        _brokenRulesList.Add(New BrokenRule("ModelId", "ModelId is required", "01-02-002", False, False))
                    End If
                End If
            End If
            If AppConfiguration.BusinessRuleEnabled("01-02-003") Then
                If (_engineTypeId = Nothing) Then
                    _brokenRulesList.Add(New BrokenRule("EngineTypeId", "EngineTypeId is required", "01-02-003", False, False))
                Else
                    If Len(_engineTypeId) = 0 Then
                        _brokenRulesList.Add(New BrokenRule("EngineTypeId", "EngineTypeId is required", "01-02-003", False, False))
                    End If
                End If
            End If
            If AppConfiguration.BusinessRuleEnabled("01-02-004") Then
                If (_engineEmissionId = Nothing) Then
                    _brokenRulesList.Add(New BrokenRule("EngineEmissionId", "EngineEmissionId is required", "01-02-004", False, False))
                Else
                    If Len(_engineEmissionId) = 0 Then
                        _brokenRulesList.Add(New BrokenRule("EngineEmissionId", "EngineEmissionId is required", "01-02-004", False, False))
                    End If
                End If
            End If
            If AppConfiguration.BusinessRuleEnabled("01-02-005") Then
                If (_engineMaturityId = Nothing) Then
                    _brokenRulesList.Add(New BrokenRule("EngineMaturityId", "EngineMaturityId is required", "01-02-005", False, False))
                Else
                    If Len(_engineMaturityId) = 0 Then
                        _brokenRulesList.Add(New BrokenRule("EngineMaturityId", "EngineMaturityId is required", "01-02-005", False, False))
                    End If
                End If
            End If
            If (_engineSerialNbr.Length > 20) Then
                _brokenRulesList.Add(New BrokenRule("EngineSerialNbr", "EngineSerialNbr cannot be longer than 20", "01-02-006", False, False))
            End If
            If AppConfiguration.BusinessRuleEnabled("01-02-007") Then
                If (_aPUTypeId = Nothing) Then
                    _brokenRulesList.Add(New BrokenRule("APUTypeId", "APUTypeId is required", "01-02-007", False, False))
                Else
                    If Len(_aPUTypeId) = 0 Then
                        _brokenRulesList.Add(New BrokenRule("APUTypeId", "APUTypeId is required", "01-02-007", False, False))
                    End If
                End If
            End If
            If AppConfiguration.BusinessRuleEnabled("01-02-008") Then
                If (_aPUEmissionId = Nothing) Then
                    _brokenRulesList.Add(New BrokenRule("APUEmissionId", "APUEmissionId is required", "01-02-008", False, False))
                Else
                    If Len(_aPUEmissionId) = 0 Then
                        _brokenRulesList.Add(New BrokenRule("APUEmissionId", "APUEmissionId is required", "01-02-008", False, False))
                    End If
                End If
            End If
            If AppConfiguration.BusinessRuleEnabled("01-02-009") Then
                If (_aPUMaturityId = Nothing) Then
                    _brokenRulesList.Add(New BrokenRule("APUMaturityId", "APUMaturityId is required", "01-02-009", False, False))
                Else
                    If Len(_aPUMaturityId) = 0 Then
                        _brokenRulesList.Add(New BrokenRule("APUMaturityId", "APUMaturityId is required", "01-02-009", False, False))
                    End If
                End If
            End If
            If AppConfiguration.BusinessRuleEnabled("01-02-00A") Then
                If (_aPUSerialNbr = Nothing) Then
                    _brokenRulesList.Add(New BrokenRule("APUSerialNbr", "APUSerialNbr is required", "01-02-00A", False, False))
                Else
                    If Len(_aPUSerialNbr) = 0 Then
                        _brokenRulesList.Add(New BrokenRule("APUSerialNbr", "APUSerialNbr is required", "01-02-00A", False, False))
                    End If
                End If
            End If
            If (_aPUSerialNbr.Length > 30) Then
                _brokenRulesList.Add(New BrokenRule("APUSerialNbr", "APUSerialNbr cannot be longer than 30", "01-02-00B", False, False))
            End If
            If (_noseGear.Length > 30) Then
                _brokenRulesList.Add(New BrokenRule("NoseGear", "NoseGear cannot be longer than 30", "01-02-00C", False, False))
            End If
            If (_mainGear.Length > 30) Then
                _brokenRulesList.Add(New BrokenRule("MainGear", "MainGear cannot be longer than 30", "01-02-00D", False, False))
            End If
            If AppConfiguration.BusinessRuleEnabled("01-02-00E") Then
                If (_propellerTypeId = Nothing) Then
                    _brokenRulesList.Add(New BrokenRule("PropellerTypeId", "PropellerTypeId is required", "01-02-00E", False, False))
                Else
                    If Len(_propellerTypeId) = 0 Then
                        _brokenRulesList.Add(New BrokenRule("PropellerTypeId", "PropellerTypeId is required", "01-02-00E", False, False))
                    End If
                End If
            End If
            If (_propellerModelNbr.Length > 20) Then
                _brokenRulesList.Add(New BrokenRule("PropellerModelNbr", "PropellerModelNbr cannot be longer than 20", "01-02-00F", False, False))
            End If
            If (_propellerSerialNo.Length > 20) Then
                _brokenRulesList.Add(New BrokenRule("PropellerSerialNo", "PropellerSerialNo cannot be longer than 20", "01-02-010", False, False))
            End If

            If IsDate(_configurationEndDate) And IsDate(_configurationStartDate) Then
                If CDate(_configurationEndDate) < CDate(_configurationStartDate) Then
                    _brokenRulesList.Add(New BrokenRule("ConfigurationEndDate", "ConfigurationEndDate cannot be less than ConfigurationStartDate", "01-02-011", False, False))
                End If
            End If
            If _configurationMeterEnd > 0 And _configurationMeterStart > 0 Then
                If _configurationMeterEnd < _configurationMeterStart Then
                    _brokenRulesList.Add(New BrokenRule("ConfigurationdMeterEnd", "ConfigurationMeterEnd cannot be less than ConfigurationMeterStart", "01-02-012", False, False))
                End If
            End If

            _errorsList = New BrokenRulesList
            _warningsList = New BrokenRulesList

            Return (BrokenRuleManager.ConsiderRules(_brokenRulesList, prevWarningsList, _warningsList, _errorsList) = 0)

        End Function
		
	End Class
		
End Namespace
