' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.BL
Imports System
Imports System.Configuration
Imports System.Collections
Imports Microsoft.Security.Application
Imports System.IO
Imports ClosedXML.Excel

Public Class rptWeightTracking
    Inherits System.Web.UI.Page

    Private _fltIdentity As FltIdentity = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        _fltIdentity = CType(Session("fltIdentity"), FltIdentity)
        If ((_fltIdentity Is Nothing) Or (_fltIdentity.IsLoggedIn = False)) Then
            Response.Redirect("~/Login.aspx")
        End If
        ClearErrorFields()
        If Not Me.IsPostBack Then
        Else
        End If

    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
    End Sub

    Protected Sub DownloadDataButton_Click(sender As Object, e As EventArgs)
        Try
            ClearErrorFields()
            Dim progId As Integer = _fltIdentity.CurrentProgId

            Using wb As XLWorkbook = RptWeightTrackingManager.GetSpreadsheet(_fltIdentity, progId)

                If wb Is Nothing Then
                    Throw New Exception("No information retrieved")
                End If

                Dim downloadFileName As String = ProgramManager.GetItem(_fltIdentity.CurrentProgId, _fltIdentity).ProgName & "_WeightTracking_" & CDate(Date.Now()).ToString("yyyyMMddTHHmmss") & ".xlsx"""

                Dim httpResponse As HttpResponse = Response
                httpResponse.Clear()
                httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                httpResponse.AddHeader("content-disposition", "attachment;filename=""" & downloadFileName)

                Using ms As New MemoryStream
                    wb.SaveAs(ms)
                    ms.WriteTo(httpResponse.OutputStream)
                    ms.Close()
                End Using

                httpResponse.End()

            End Using

        Catch ex As System.Threading.ThreadAbortException
        Catch ex As Exception
            ProcessException(ex)
        End Try
    End Sub

    Private Sub ProcessException(ByVal ex As Exception)
        Me.ErrorMessageLabel.Text = ex.Message
        Me.ErrorMessageLabel.Visible = True
    End Sub

    Private Sub ClearErrorFields()
        Me.ErrorMessageLabel.Text = String.Empty
        Me.ErrorMessageLabel.Visible = False
        Me.SuccessMessageLabel.Text = String.Empty
        Me.SuccessMessageLabel.Visible = False
        Me.BrokenRulesList.Items.Clear()
        Me.BrokenRulesList.Visible = False
    End Sub

End Class