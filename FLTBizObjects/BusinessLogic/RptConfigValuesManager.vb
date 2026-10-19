' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal
Imports Contoso.Utilities
Imports ClosedXML.Excel

Namespace FLT.BL

    Public Class RptConfigValuesManager
        Public Shared Function GetSpreadsheet(fltIdentity As FltIdentity, LookUpName As String, ProgId As Integer) As ClosedXML.Excel.XLWorkbook
            If Not CanView(fltIdentity) Then
                Throw New Exception("User is not authorized")
            Else
                Try
                    Dim totalRows As Integer = 0
                    Dim newList As fltLookupBaseList = fltLookupBaseDB.GetList(LookupName:=LookUpName, ProgId:=ProgId, IsActiveFilter:=1, RowStart:=-1, RowCount:=-1, TotalRows:=totalRows)

                    If newList Is Nothing Then
                        Return Nothing
                    End If

                    If newList.Count = 0 Then
                        Return Nothing
                    End If

                    Return CreateExcelWorkBook(newList, LookUpName, ProgId)

                Catch ex As Exception
                    Logger.Write(ex.Message)
                    Throw New ApplicationException("An application error occurred")
                End Try
            End If
        End Function

        Private Shared Function GetData(LookUpName As String, ProgId As Integer) As fltLookupBaseList

            Dim fltLookUpBaseList As New fltLookupBaseList
            Dim fltLookUpBaseDB As New fltLookupBaseDB
            Dim TotalRows As Integer

            Try

                fltLookUpBaseList = fltLookUpBaseDB.GetList(LookUpName, ProgId, 1, 0, 0, TotalRows)

                Return fltLookUpBaseList

            Catch ex As Exception
                Logger.Write(ex.Message)
                Throw New ApplicationException("An application error occurred")
            End Try
        End Function

        Private Shared Function CreateExcelWorkBook(dataList As fltLookupBaseList, LookUpName As String, ProgId As Int32) As XLWorkbook
            Try
                Dim wb As New XLWorkbook()
                Dim ws As IXLWorksheet = Nothing
                Dim rowNbr As Integer = 2

                ws = CreateNewWorksheet(wb, dataList, LookUpName)

                For Each thisItem As fltLookupBase In dataList

                    CreateDetailLine(ws, thisItem, rowNbr)
                    rowNbr = rowNbr + 1

                Next

                Return wb

            Catch ex As Exception
                ProcessException(ex)
                Return Nothing
            End Try
        End Function

        Private Shared Function CreateNewWorksheet(wb As XLWorkbook, dataList As fltLookupBaseList, LookUpName As String) As IXLWorksheet

            Dim ws As IXLWorksheet = wb.AddWorksheet(LookUpName)
            ws.Style.Font.SetFontName("Calibri")
            ws.Style.Font.SetFontSize("11")

            ws.Columns(1).Width = 9
            ws.Columns(2).Width = 40
            ws.Columns(3).Width = 10
            ws.Columns(4).Width = 10

            Dim headerRow As String = "1"
            AddCellHeader(ws, "A", headerRow, "Id")
            AddCellHeader(ws, "B", headerRow, "StringValue")
            AddCellHeader(ws, "C", headerRow, "SortOrder")
            AddCellHeader(ws, "D", headerRow, "IsActive")

            ws.Row(1).Height = 15

            Return ws

        End Function

        Private Shared Sub CreateDetailLine(ws As IXLWorksheet, thisItem As fltLookupBase, rowNbr As Int32)

            AddCellDetail(ws, "A", rowNbr, thisItem.ID)
            AddCellDetail(ws, "B", rowNbr, thisItem.StringValue)
            AddCellDetail(ws, "C", rowNbr, thisItem.SortOrder)
            AddCellDetail(ws, "D", rowNbr, thisItem.IsActive)

        End Sub

        Private Shared Sub SetCellColor(ws As IXLWorksheet, cellCol As String, cellRow As Int32, ARGBCode As String, ContrastARGBCode As String)
            Try
                ws.Cell(cellCol & cellRow.ToString()).Style.Fill.SetBackgroundColor(XLColor.FromArgb(Convert.ToInt32(ARGBCode, 16)))
                ws.Cell(cellCol & cellRow.ToString()).Style.Font.SetFontColor(XLColor.FromArgb(Convert.ToInt32(ContrastARGBCode, 16)))
            Catch
            End Try
        End Sub

        Private Shared Sub AddCellHeader(ws As IXLWorksheet, col As String, row As String, caption As String, Optional FontSize As Double = 11, Optional FontBold As Boolean = True, Optional WrapText As Boolean = True)
            Dim cellAddr As String = col.Trim & row.Trim
            ws.Cell(cellAddr).Value = caption
            ws.Cell(cellAddr).Style.Font.FontSize = FontSize
            ws.Cell(cellAddr).Style.Font.Bold = FontBold
            ws.Cell(cellAddr).Style.Alignment.WrapText = WrapText
        End Sub

        Private Shared Sub AddCellHeaderWithBorder(ws As IXLWorksheet, col As String, row As String, caption As String, Optional FontSize As Double = 11, Optional FontBold As Boolean = True, Optional WrapText As Boolean = True)
            AddCellHeader(ws, col, row, caption, FontSize, FontBold, WrapText)
            SetBorderAroundCell(ws.Cell(col + row))
        End Sub

        Private Shared Sub AddCellDetail(ws As IXLWorksheet, col As String, row As String, value As String)
            ws.Cell(col + row.ToString()).Value = value
        End Sub

        Private Shared Sub SetBorderAroundCell(cell As IXLCell)
            SetBorderAroundCell(cell, XLBorderStyleValues.Thin, XLColor.Black)
        End Sub

        Private Shared Sub SetBorderAroundCell(cell As IXLCell, borderStyleThickness As XLBorderStyleValues)
            SetBorderAroundCell(cell, borderStyleThickness, XLColor.Black)
        End Sub

        Private Shared Sub SetBorderAroundCell(cell As IXLCell, color As XLColor)
            SetBorderAroundCell(cell, XLBorderStyleValues.Thin, color)
        End Sub

        Private Shared Sub SetBorderAroundCell(cell As IXLCell, borderStyleThickness As XLBorderStyleValues, color As XLColor)
            cell.Style.Border.SetBottomBorder(borderStyleThickness)
            cell.Style.Border.SetLeftBorder(borderStyleThickness)
            cell.Style.Border.SetRightBorder(borderStyleThickness)
            cell.Style.Border.SetTopBorder(borderStyleThickness)
            cell.Style.Border.SetBottomBorderColor(color)
            cell.Style.Border.SetLeftBorderColor(color)
            cell.Style.Border.SetRightBorderColor(color)
            cell.Style.Border.SetTopBorderColor(color)
        End Sub

        Private Shared Sub SetBorderAroundRange(range As IXLRange)
            SetBorderAroundRange(range, XLBorderStyleValues.Thin, XLColor.Black)
        End Sub

        Private Shared Sub SetBorderAroundRange(range As IXLRange, borderStyleThickness As XLBorderStyleValues)
            SetBorderAroundRange(range, borderStyleThickness, XLColor.Black)
        End Sub

        Private Shared Sub SetBorderAroundRange(range As IXLRange, color As XLColor)
            SetBorderAroundRange(range, XLBorderStyleValues.Thin, color)
        End Sub

        Private Shared Sub SetBorderAroundRange(range As IXLRange, borderStyleThickness As XLBorderStyleValues, color As XLColor)
            range.Style.Border.OutsideBorder = borderStyleThickness
            range.Style.Border.OutsideBorderColor = color
        End Sub



        Private Shared Function GetMeterAbbr(MeterUnit As String)
            Select Case MeterUnit.ToLower
                Case "hours", "hour", "hr"
                    Return "hr"
                Case Else
                    Return "hh:mm"
            End Select
        End Function

        Public Shared Function CanView(ByVal FltIdentity As FltIdentity) As Boolean
            Try
                Return True
            Catch ex As Exception
                Logger.Write(ex)
                Throw New Exception(ex.Message)
            End Try
        End Function

        Private Shared Sub ProcessException(ex As Exception)
            Logger.Write(ex.Message)
            Throw New ApplicationException("An application error occurred")
        End Sub

    End Class

End Namespace
