' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.

Option Strict On
Option Explicit On


Partial Public Class FlightModify_alt2

    Protected WithEvents PageTitleLB As Global.System.Web.UI.WebControls.Label

    Protected WithEvents SuccessMessageLabel As Global.System.Web.UI.WebControls.Label

    Protected WithEvents ErrorMessageLabel As Global.System.Web.UI.WebControls.Label

    Protected WithEvents ErrorsBulletList As Global.System.Web.UI.WebControls.BulletedList

    Protected WithEvents WarningMessageLabel As Global.System.Web.UI.WebControls.Label

    Protected WithEvents WarningsCheckList As Global.System.Web.UI.WebControls.CheckBoxList

    Protected WithEvents ParentChildMultiview As Global.System.Web.UI.WebControls.MultiView

    Protected WithEvents AddView As Global.System.Web.UI.WebControls.View

    Protected WithEvents LocationAddDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents FlightDateAddTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents AircraftAddDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents WorkShiftAddDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents NextButton As Global.System.Web.UI.WebControls.Button

    Protected WithEvents EditButton As Global.System.Web.UI.WebControls.Button

    Protected WithEvents RetryButton As Global.System.Web.UI.WebControls.Button

    Protected WithEvents CancelButton1 As Global.System.Web.UI.WebControls.Button

    Protected WithEvents View1 As Global.System.Web.UI.WebControls.View

    Protected WithEvents LocationEditDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents HobbsStartTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents HobbsEndTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents FlightDateEditTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents OilLevelDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents PostFlightFtiOKDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents MSNLB As Global.System.Web.UI.WebControls.Label

    Protected WithEvents PreFlightFtiOKDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents WorkShiftEditDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents SchedRouteDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents FlownRouteDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents PilotDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents AddPilotButton As Global.System.Web.UI.WebControls.ImageButton

    Protected WithEvents DepartTimeTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents ReturnTimeTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents OutsideTempTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents WeatherTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents TotalHoursLB As Global.System.Web.UI.WebControls.Label

    Protected WithEvents LiquidFillDG As Global.System.Web.UI.WebControls.DataGrid

    Protected WithEvents FlightRegionTimeDG As Global.System.Web.UI.WebControls.DataGrid

    Protected WithEvents FlightCommentDG As Global.System.Web.UI.WebControls.DataGrid

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
End Class
