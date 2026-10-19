' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.

Option Strict On
Option Explicit On


Partial Public Class FlightModify

    Protected WithEvents PageTitleLB As Global.System.Web.UI.WebControls.Label

    Protected WithEvents SuccessMessageLabel As Global.System.Web.UI.WebControls.Label

    Protected WithEvents ErrorMessageLabel As Global.System.Web.UI.WebControls.Label

    Protected WithEvents ErrorsBulletList As Global.System.Web.UI.WebControls.BulletedList

    Protected WithEvents WarningMessageLabel As Global.System.Web.UI.WebControls.Label

    Protected WithEvents WarningsCheckList As Global.System.Web.UI.WebControls.CheckBoxList

    Protected WithEvents ParentChildMultiview As Global.System.Web.UI.WebControls.MultiView

    Protected WithEvents AddView As Global.System.Web.UI.WebControls.View

    Protected WithEvents LocationIdDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents AircraftIdDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents FlightDateTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents WorkShiftDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents NextButton As Global.System.Web.UI.WebControls.Button

    Protected WithEvents EditButton As Global.System.Web.UI.WebControls.Button

    Protected WithEvents RetryButton As Global.System.Web.UI.WebControls.Button

    Protected WithEvents CancelButton1 As Global.System.Web.UI.WebControls.Button

    Protected WithEvents EditView As Global.System.Web.UI.WebControls.View

    Protected WithEvents Table2 As Global.System.Web.UI.WebControls.Table

    Protected WithEvents LocationEditDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents AircraftEditDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents FlightDateEditTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents WorkShiftEditDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents PilotDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents AddPilotButton As Global.System.Web.UI.WebControls.ImageButton

    Protected WithEvents SchedRouteDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents DepartDateTimeTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents ReturnDateTimeTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents AirframeHoursStartLB As Global.System.Web.UI.WebControls.Label

    Protected WithEvents AirframeHoursEndLB As Global.System.Web.UI.WebControls.Label

    Protected WithEvents HobbsStartLB As Global.System.Web.UI.WebControls.Label

    Protected WithEvents HobbsEndLB As Global.System.Web.UI.WebControls.Label

    Protected WithEvents UsedMeterOffsetLB As Global.System.Web.UI.WebControls.Label

    Protected WithEvents TotalHoursLabel As Global.System.Web.UI.WebControls.Label

    Protected WithEvents HobbsStartTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents EngineHoursStartTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents OilLevelIndicatorStartDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents HydraulicLevelIndicatorStart As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents PreFlightFtiOKDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents PreFlightTelemetryOKDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents PreFlightTelemetryCheckCompleteDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents TakeoffWeightLB As Global.System.Web.UI.WebControls.Label

    Protected WithEvents AirframeDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents Cargo1DDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents BallastDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents Cargo2DDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents LiquidFillDG As Global.System.Web.UI.WebControls.DataGrid

    Protected WithEvents FlightCommentDG As Global.System.Web.UI.WebControls.DataGrid

    Protected WithEvents FlightEngineRunDG As Global.System.Web.UI.WebControls.DataGrid

    Protected WithEvents HobbsEndTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents EngineHoursEndTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents MeterUnitLB As Global.System.Web.UI.WebControls.Label

    Protected WithEvents PostFlightFtiOKDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents CarbonSootBuildupDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents PostFlightTelemetryCheckCompleteDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents LeaksOrDiscolorationDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents FlightRegionTimeDG As Global.System.Web.UI.WebControls.DataGrid

    Protected WithEvents SaveButton As Global.System.Web.UI.WebControls.Button

    Protected WithEvents CancelButton As Global.System.Web.UI.WebControls.Button

    Protected WithEvents AddPilotView As Global.System.Web.UI.WebControls.View

    Protected WithEvents PilotNameTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents PilotSortOrderTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents PilotIsActiveCheckBox As Global.System.Web.UI.WebControls.CheckBox

    Protected WithEvents PilotSaveButton As Global.System.Web.UI.WebControls.ImageButton

    Protected WithEvents PilotCancelButton As Global.System.Web.UI.WebControls.ImageButton

    Protected WithEvents ProgIdHiddenField As Global.System.Web.UI.WebControls.HiddenField

    Protected WithEvents LocationIdHiddenField As Global.System.Web.UI.WebControls.HiddenField

    Protected WithEvents AircraftIdHiddenField As Global.System.Web.UI.WebControls.HiddenField

    Protected WithEvents FlightIdHiddenField As Global.System.Web.UI.WebControls.HiddenField

    Protected WithEvents AircraftEngineHoursCurrentHF As Global.System.Web.UI.WebControls.HiddenField

    Protected WithEvents TabIndexHF As Global.System.Web.UI.WebControls.HiddenField
End Class
