' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExportClient
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.CloseButton = New System.Windows.Forms.Button
        Me.ResponseTextBox = New System.Windows.Forms.TextBox
        Me.GetFileListButton = New System.Windows.Forms.Button
        Me.CreateFileButton = New System.Windows.Forms.Button
        Me.FetchFileButton = New System.Windows.Forms.Button
        Me.FileListView = New System.Windows.Forms.ListView
        Me.FileName = New System.Windows.Forms.ColumnHeader
        Me.FileSize = New System.Windows.Forms.ColumnHeader
        Me.UtcDateCreated = New System.Windows.Forms.ColumnHeader
        Me.SuspendLayout()
        Me.CloseButton.Location = New System.Drawing.Point(565, 12)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(75, 23)
        Me.CloseButton.TabIndex = 3
        Me.CloseButton.Text = "Close"
        Me.CloseButton.UseVisualStyleBackColor = True
        Me.ResponseTextBox.Location = New System.Drawing.Point(19, 185)
        Me.ResponseTextBox.MinimumSize = New System.Drawing.Size(621, 120)
        Me.ResponseTextBox.Multiline = True
        Me.ResponseTextBox.Name = "ResponseTextBox"
        Me.ResponseTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.ResponseTextBox.Size = New System.Drawing.Size(621, 164)
        Me.ResponseTextBox.TabIndex = 5
        Me.GetFileListButton.Location = New System.Drawing.Point(21, 12)
        Me.GetFileListButton.Name = "GetFileListButton"
        Me.GetFileListButton.Size = New System.Drawing.Size(75, 23)
        Me.GetFileListButton.TabIndex = 7
        Me.GetFileListButton.Text = "Get File List"
        Me.GetFileListButton.UseVisualStyleBackColor = True
        Me.CreateFileButton.Location = New System.Drawing.Point(102, 12)
        Me.CreateFileButton.Name = "CreateFileButton"
        Me.CreateFileButton.Size = New System.Drawing.Size(75, 23)
        Me.CreateFileButton.TabIndex = 8
        Me.CreateFileButton.Text = "Create File"
        Me.CreateFileButton.UseVisualStyleBackColor = True
        Me.FetchFileButton.Location = New System.Drawing.Point(183, 12)
        Me.FetchFileButton.Name = "FetchFileButton"
        Me.FetchFileButton.Size = New System.Drawing.Size(75, 23)
        Me.FetchFileButton.TabIndex = 9
        Me.FetchFileButton.Text = "Fetch File"
        Me.FetchFileButton.UseVisualStyleBackColor = True
        Me.FileListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.FileName, Me.FileSize, Me.UtcDateCreated})
        Me.FileListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.FileListView.Location = New System.Drawing.Point(20, 41)
        Me.FileListView.MultiSelect = False
        Me.FileListView.Name = "FileListView"
        Me.FileListView.Size = New System.Drawing.Size(620, 138)
        Me.FileListView.TabIndex = 11
        Me.FileListView.UseCompatibleStateImageBehavior = False
        Me.FileListView.View = System.Windows.Forms.View.Details
        Me.FileName.Text = "File Name"
        Me.FileName.Width = 225
        Me.FileSize.Text = "File Size"
        Me.FileSize.Width = 138
        Me.UtcDateCreated.Text = "Date Created (UTC)"
        Me.UtcDateCreated.Width = 228
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(652, 364)
        Me.Controls.Add(Me.FileListView)
        Me.Controls.Add(Me.FetchFileButton)
        Me.Controls.Add(Me.CreateFileButton)
        Me.Controls.Add(Me.GetFileListButton)
        Me.Controls.Add(Me.ResponseTextBox)
        Me.Controls.Add(Me.CloseButton)
        Me.Name = "frmExportClient"
        Me.Text = "FLT Export Client"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CloseButton As System.Windows.Forms.Button
    Friend WithEvents ResponseTextBox As System.Windows.Forms.TextBox
    Friend WithEvents GetFileListButton As System.Windows.Forms.Button
    Friend WithEvents CreateFileButton As System.Windows.Forms.Button
    Friend WithEvents FetchFileButton As System.Windows.Forms.Button
    Friend WithEvents FileListView As System.Windows.Forms.ListView
    Friend WithEvents FileName As System.Windows.Forms.ColumnHeader
    Friend WithEvents FileSize As System.Windows.Forms.ColumnHeader
    Friend WithEvents UtcDateCreated As System.Windows.Forms.ColumnHeader

End Class
