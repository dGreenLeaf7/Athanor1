' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.

Option Strict On
Option Explicit On


Partial Public Class WorkOrderModify

    Protected WithEvents SuccessMessageLabel As Global.System.Web.UI.WebControls.Label

    Protected WithEvents ErrorMessageLabel As Global.System.Web.UI.WebControls.Label

    Protected WithEvents ErrorsBulletList As Global.System.Web.UI.WebControls.BulletedList

    Protected WithEvents WarningMessageLabel As Global.System.Web.UI.WebControls.Label

    Protected WithEvents WarningsCheckList As Global.System.Web.UI.WebControls.CheckBoxList

    Protected WithEvents AircraftDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents LocationDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents WorkOrderTypeDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents WorkOrderStatusDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents WorkOrderNbrLB As Global.System.Web.UI.WebControls.Label

    Protected WithEvents RequestDateTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents EngineerDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents EngineeringActionDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents FaultOccurredDateTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents FaultOccurredMeterTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents DueDateTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents DueTimeTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents StartMeterTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents MechanicDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents FinishDateTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents FinishTimeTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents EndMeterTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents AircraftDispositionTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents AircraftIssueAlertCB As Global.System.Web.UI.WebControls.CheckBox

    Protected WithEvents MaintIssueAlertCB As Global.System.Web.UI.WebControls.CheckBox

    Protected WithEvents FISComplaintNbrTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents SquawkNbrTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents WOReasonsDG As Global.System.Web.UI.WebControls.DataGrid

    Protected WithEvents WOInstructionsDG As Global.System.Web.UI.WebControls.DataGrid

    Protected WithEvents WOPerformedWorkDG As Global.System.Web.UI.WebControls.DataGrid

    Protected WithEvents WOEngineerCommentsDG As Global.System.Web.UI.WebControls.DataGrid

    Protected WithEvents WOTechCommentsDG As Global.System.Web.UI.WebControls.DataGrid

    Protected WithEvents WOPartsDG As Global.System.Web.UI.WebControls.DataGrid

    Protected WithEvents DocumentsMV As Global.System.Web.UI.WebControls.MultiView

    Protected WithEvents DocumentListView As Global.System.Web.UI.WebControls.View

    Protected WithEvents AddDocumentButton As Global.System.Web.UI.WebControls.ImageButton

    Protected WithEvents WODocumentsGV As Global.System.Web.UI.WebControls.GridView

    Protected WithEvents DocumentUploadView As Global.System.Web.UI.WebControls.View

    Protected WithEvents FileUpoad1 As Global.System.Web.UI.WebControls.FileUpload

    Protected WithEvents DocumentDescriptionTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents UploadButton As Global.System.Web.UI.WebControls.Button

    Protected WithEvents UploadCancelButton As Global.System.Web.UI.WebControls.Button

    Protected WithEvents SaveButton As Global.System.Web.UI.WebControls.Button

    Protected WithEvents CancelButton As Global.System.Web.UI.WebControls.Button

    Protected WithEvents ProgIdHiddenField As Global.System.Web.UI.WebControls.HiddenField

    Protected WithEvents TabIndexHF As Global.System.Web.UI.WebControls.HiddenField
End Class
