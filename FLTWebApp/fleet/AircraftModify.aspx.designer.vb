' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.

Option Strict On
Option Explicit On


Partial Public Class AircraftModify

    Protected WithEvents AddEditTitleLabel As Global.System.Web.UI.WebControls.Label

    Protected WithEvents SuccessMessageLabel As Global.System.Web.UI.WebControls.Label

    Protected WithEvents ErrorMessageLabel As Global.System.Web.UI.WebControls.Label

    Protected WithEvents ErrorsBulletList As Global.System.Web.UI.WebControls.BulletedList

    Protected WithEvents WarningMessageLabel As Global.System.Web.UI.WebControls.Label

    Protected WithEvents WarningsCheckList As Global.System.Web.UI.WebControls.CheckBoxList

    Protected WithEvents MSNTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents ShipNbrTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents SerialNbrTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents RegistrationTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents ManufacturerDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents ModelDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents LiveryColorTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents AircraftStatusDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents DescriptionTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents AircraftEngineHoursCurrentTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents CurrentLocationLB As Global.System.Web.UI.WebControls.Label

    Protected WithEvents EngineerDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents MechanicDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents TakeoffWeightDDL As Global.System.Web.UI.WebControls.Label

    Protected WithEvents MeterStartLB As Global.System.Web.UI.WebControls.Label

    Protected WithEvents MeterCurrentLB As Global.System.Web.UI.WebControls.Label

    Protected WithEvents CurrentMeterOffsetLB As Global.System.Web.UI.WebControls.Label

    Protected WithEvents MeterUnitsLB As Global.System.Web.UI.WebControls.Label

    Protected WithEvents EstEndOfTestingTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents ActEndOfTestingTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents TotalRequiredHoursTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents CurrentHoursLB As Global.System.Web.UI.WebControls.Label

    Protected WithEvents AircraftLocationsDG As Global.System.Web.UI.WebControls.DataGrid

    Protected WithEvents AircraftFlightsGridView As Global.System.Web.UI.WebControls.GridView

    Protected WithEvents AircraftWorkOrdersGridView As Global.System.Web.UI.WebControls.GridView

    Protected WithEvents AircraftConfigurationsMultiView As Global.System.Web.UI.WebControls.MultiView

    Protected WithEvents AircraftConfigsListView As Global.System.Web.UI.WebControls.View

    Protected WithEvents AircraftConfigGV As Global.System.Web.UI.WebControls.GridView

    Protected WithEvents AddAircraftConfigButton As Global.System.Web.UI.WebControls.ImageButton

    Protected WithEvents AircraftConfigsModifyView As Global.System.Web.UI.WebControls.View

    Protected WithEvents AircraftConfigurationIdHF As Global.System.Web.UI.WebControls.HiddenField

    Protected WithEvents ConfigurationStartDateTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents ConfigurationEndDateTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents ConfigModelDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents ConfigurationMeterStartTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents ConfigurationMeterEndTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents ConfigurationMeterOffsetTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents ConfigMeterUnits As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents ConfigurationDescriptionTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents EngineTypeDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents EngineMaturityDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents EngineEmissionDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents EngineSerialNbrTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents PropellerTypeDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents PropellerModelNbrTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents PropellerSerialNbrTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents APUTypeDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents APUMaturityDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents APUEmissionDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents APUSerialNbrTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents NoseGearTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents MainGearTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents AvionicsVersionTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents EmissionLevelTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents SaveAircraftConfigButton As Global.System.Web.UI.WebControls.ImageButton

    Protected WithEvents CancelAircraftConfigButton As Global.System.Web.UI.WebControls.ImageButton

    Protected WithEvents TestsAssignedDG As Global.System.Web.UI.WebControls.DataGrid

    Protected WithEvents TestsNotAssignedDG As Global.System.Web.UI.WebControls.DataGrid

    Protected WithEvents AircraftCommentsDG As Global.System.Web.UI.WebControls.DataGrid

    Protected WithEvents SaveButton As Global.System.Web.UI.WebControls.Button

    Protected WithEvents CancelButton As Global.System.Web.UI.WebControls.Button

    Protected WithEvents ProgIdHiddenField As Global.System.Web.UI.WebControls.HiddenField

    Protected WithEvents AircraftIdHiddenField As Global.System.Web.UI.WebControls.HiddenField

    Protected WithEvents TabIndexHF As Global.System.Web.UI.WebControls.HiddenField
End Class
