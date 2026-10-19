' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.

Option Strict On
Option Explicit On


Partial Public Class WorkOrdersList

    Protected WithEvents SuccessMessageLabel As Global.System.Web.UI.WebControls.Label

    Protected WithEvents ErrorMessageLabel As Global.System.Web.UI.WebControls.Label

    Protected WithEvents ErrorsBulletList As Global.System.Web.UI.WebControls.BulletedList

    Protected WithEvents WarningMessageLabel As Global.System.Web.UI.WebControls.Label

    Protected WithEvents WarningsCheckList As Global.System.Web.UI.WebControls.CheckBoxList

    Protected WithEvents AddButton As Global.System.Web.UI.WebControls.ImageButton

    Protected WithEvents AircraftFilterTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents LocationFilterDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents IsDeletedDropDownList As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents WorkOrdersGV As Global.System.Web.UI.WebControls.GridView

    Protected WithEvents ProgIdHiddenField As Global.System.Web.UI.WebControls.HiddenField
End Class
