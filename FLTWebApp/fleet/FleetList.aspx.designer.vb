' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.

Option Strict On
Option Explicit On


Partial Public Class Fleet

    Protected WithEvents SuccessMessageLabel As Global.System.Web.UI.WebControls.Label

    Protected WithEvents ErrorMessageLabel As Global.System.Web.UI.WebControls.Label

    Protected WithEvents ErrorsBulletList As Global.System.Web.UI.WebControls.BulletedList

    Protected WithEvents WarningMessageLabel As Global.System.Web.UI.WebControls.Label

    Protected WithEvents WarningsCheckList As Global.System.Web.UI.WebControls.CheckBoxList

    Protected WithEvents AddButton As Global.System.Web.UI.WebControls.ImageButton

    Protected WithEvents AircraftFilterTextBox As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents LocationFilterDropdownList As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents IsActiveDropDownList As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents IsDeletedDropDownList As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents FleetGridView As Global.System.Web.UI.WebControls.GridView

    Protected WithEvents ProgIdHiddenField As Global.System.Web.UI.WebControls.HiddenField
End Class
