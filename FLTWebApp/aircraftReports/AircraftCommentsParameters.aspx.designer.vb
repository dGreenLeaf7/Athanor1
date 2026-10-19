' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.

Option Strict On
Option Explicit On


Partial Public Class AircraftCommentsParameters

    Protected WithEvents ReportMultiView As Global.System.Web.UI.WebControls.MultiView

    Protected WithEvents ParametersView As Global.System.Web.UI.WebControls.View

    Protected WithEvents ErrorMessageLabel As Global.System.Web.UI.WebControls.Label

    Protected WithEvents SuccessMessageLabel As Global.System.Web.UI.WebControls.Label

    Protected WithEvents BrokenRulesList As Global.System.Web.UI.WebControls.BulletedList

    Protected WithEvents LocationDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents AircraftDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents ComponentDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents RatingFromDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents RatingThruDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents DateFromTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents DateThruTB As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents SortOrderDDL As Global.System.Web.UI.WebControls.DropDownList

    Protected WithEvents ShowReportButton As Global.System.Web.UI.WebControls.Button

    Protected WithEvents DownloadButton As Global.System.Web.UI.WebControls.Button

    Protected WithEvents ReportView As Global.System.Web.UI.WebControls.View

    Protected WithEvents BackButton As Global.System.Web.UI.WebControls.Button

    Protected WithEvents myCrystalReportViewer As Global.CrystalDecisions.Web.CrystalReportViewer

    Protected WithEvents ProgIdHiddenField As Global.System.Web.UI.WebControls.HiddenField
End Class
