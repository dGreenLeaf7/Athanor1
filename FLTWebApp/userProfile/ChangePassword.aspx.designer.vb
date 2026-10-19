' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.

Option Strict On
Option Explicit On


Partial Public Class ChangePassword

    Protected WithEvents ChangePasswordMultiview As Global.System.Web.UI.WebControls.MultiView

    Protected WithEvents ChangePasswordView As Global.System.Web.UI.WebControls.View

    Protected WithEvents OldPasswordTextBox As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents NewPassword1TextBox As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents NewPassword2TextBox As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents EmailTextBox As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents ErrorMessageLabel As Global.System.Web.UI.WebControls.Label

    Protected WithEvents ChangeButton As Global.System.Web.UI.WebControls.Button

    Protected WithEvents BrokenRulesTable As Global.System.Web.UI.WebControls.Table

    Protected WithEvents BrokenRulesList As Global.System.Web.UI.WebControls.BulletedList

    Protected WithEvents SuccessView As Global.System.Web.UI.WebControls.View
End Class
