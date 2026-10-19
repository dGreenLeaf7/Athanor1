' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BL
Imports FLTBizObjects.FLT.BO
Imports System.Configuration

Partial Public Class DocumentAdd
    Inherits System.Web.UI.Page
#Region "Private Variables"

    Private _fltIdentity As FltIdentity = Nothing

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _fltIdentity = Session("fltIdentity")
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        Me.ProgIdHiddenField.Value = _fltIdentity.CurrentProgId
        If Not Me.IsPostBack Then
        End If
    End Sub

    Protected Sub UploadButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UploadButton.Click

        Dim newDocumentInfo As New DocumentInfo
        Try
            If Me.FileUpoad1.FileName.ToString = String.Empty Then
                Throw New Exception("Please select a document to upload")
            End If

            Dim maxDocSize As Integer = CInt(ConfigurationManager.AppSettings.Get("MaxFileUploadSize"))

            If Me.FileUpoad1.FileBytes.Length > maxDocSize Then
                Throw New Exception("Document size exceeds maximum limit of " & Format((maxDocSize / 1000000), "#,##0") & "MB")
            End If

            With newDocumentInfo
                .ProgId = Me.ProgIdHiddenField.Value
                .DocumentName = Server.HtmlDecode(Me.FileUpoad1.FileName.ToString)
                .FileExtension = Server.HtmlDecode(.DocumentName.Substring(.DocumentName.LastIndexOf(".")))
                .DocumentTypeId = GetDocumentType(.FileExtension)
                .DocumentSize = Me.FileUpoad1.FileBytes.Length
                .DocumentDescription = Server.HtmlDecode(Me.DocumentDescriptionTB.Text)
            End With

            Dim newID As Integer = DocumentInfoManager.Insert(newDocumentInfo, _fltIdentity)

            DocumentManager.PutStream(newID, Me.FileUpoad1.PostedFile.InputStream, _fltIdentity)

            Response.Redirect("~/documents/documentsList.aspx")

        Catch ex As Exception
            ProcessException(ex, newDocumentInfo.BrokenRules)
        End Try
    End Sub

    Protected Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        Response.Redirect("~/documents/documentsList.aspx")
    End Sub

    Private Function GetDocumentType(ByVal thisFileExtension As String) As Integer
        Dim newDocumentTypeId As Integer = 0
        Try
            For Each thisDocumentType As DocumentType In DocumentTypeManager.GetList(_fltIdentity)
                If thisDocumentType.FileExtension.ToLower = thisFileExtension.ToLower Then
                    newDocumentTypeId = thisDocumentType.DocumentTypeId
                    Exit For
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return newDocumentTypeId
    End Function

#Region "Private Procedures"

    Private Sub ClearErrorFields()
        Me.ErrorMessageLabel.Text = String.Empty
        Me.ErrorMessageLabel.Visible = False
        Me.SuccessMessageLabel.Text = String.Empty
        Me.SuccessMessageLabel.Visible = False
        Me.BrokenRulesList.Items.Clear()
        Me.BrokenRulesList.Visible = False
    End Sub

    Private Sub ProcessException(ByVal ex As Exception)
        ProcessException(ex, New BrokenRulesList)
    End Sub

    Private Sub ProcessException(ByVal ex As Exception, ByVal BrokenRules As BrokenRulesList)
        Me.ErrorMessageLabel.Text = ex.Message
        Me.ErrorMessageLabel.Visible = True
        If BrokenRules.Count <> 0 Then
            Me.BrokenRulesList.Visible = True
            For Each thisRule As BrokenRule In BrokenRules
                Me.BrokenRulesList.Items.Add(New ListItem(thisRule.Message))
            Next
        End If
    End Sub

#End Region

End Class