' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.ComponentModel
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.Dal
Imports Contoso.Utilities
Imports ClosedXML.Excel

Namespace FLT.BL
    Public Class RptFleetManager

        Public Shared Function GetSpreadsheet(fltIdentity As FltIdentity, ProgId As Integer) As ClosedXML.Excel.XLWorkbook
            If Not CanView(fltIdentity) Then
                Throw New Exception("User is not authorized")
            Else
                Try
                    Dim totalRows As Integer = 0
                    Dim newList As AircraftInfoList = AircraftInfoDB.GetList(ProgId:=ProgId, CurrentLocationId:=0, AircraftTextMatch:="", IsActiveFilter:=1, IsDeletedFilter:=0, RowStart:=-1, RowCount:=-1, TotalRows:=totalRows)

                    If newList Is Nothing Then
                        Return Nothing
                    End If

                    If newList.Count = 0 Then
                        Return Nothing
                    End If

                    Return CreateExcelWorkBook(newList)

                Catch ex As Exception
                    Logger.Write(ex.Message)
                    Throw New ApplicationException("An application error occurred")
                End Try
            End If
        End Function

        Private Shared Function CreateExcelWorkBook(dataList As AircraftInfoList) As XLWorkbook
            Try
                Dim wb As New XLWorkbook()
                Dim ws As IXLWorksheet = Nothing
                Dim rowNbr As Integer = 2

                ws = CreateNewWorksheet(wb)

                For Each thisItem As AircraftInfo In dataList

                    CreateDetailLine(ws, thisItem, rowNbr)
                    rowNbr = rowNbr + 1

                Next

                Return wb

            Catch ex As Exception
                ProcessException(ex)
                Return Nothing
            End Try
        End Function

        Private Shared Function CreateNewWorksheet(wb As XLWorkbook) As IXLWorksheet

            Dim ws As IXLWorksheet = wb.AddWorksheet("Fleet")
            ws.Style.Font.SetFontName("Calibri")
            ws.Style.Font.SetFontSize("11")

            ws.Columns(1).Width = 9
            ws.Columns(2).Width = 20
            ws.Columns(3).Width = 20
            ws.Columns(4).Width = 40
            ws.Columns(5).Width = 18
            ws.Columns(6).Width = 15
            ws.Columns(7).Width = 26
            ws.Columns(8).Width = 26
            ws.Columns(9).Width = 26
            ws.Columns(10).Width = 26

            Dim headerRow As String = "1"
            AddCellHeader(ws, "A", headerRow, "AircraftId")
            AddCellHeader(ws, "B", headerRow, "MSN")
            AddCellHeader(ws, "C", headerRow, "ShipNbr")
            AddCellHeader(ws, "D", headerRow, "Description")
            AddCellHeader(ws, "E", headerRow, "CurrentLocationId")
            AddCellHeader(ws, "F", headerRow, "AirframeHoursStart")
            AddCellHeader(ws, "G", headerRow, "AirframeHoursCurrent")
            AddCellHeader(ws, "H", headerRow, "airframeHoursOffset")
            AddCellHeader(ws, "I", headerRow, "MeterUnitId")
            AddCellHeader(ws, "J", headerRow, "AircraftEngineHoursCurrent")

            ws.Row(1).Height = 15

            Return ws

        End Function

        Private Shared Sub CreateDetailLine(ws As IXLWorksheet, thisItem As AircraftInfo, rowNbr As Int32)

            SetCellAsNumber(ws.Cell("A" & rowNbr.ToString))
            SetCellAsNumber(ws.Cell("E" & rowNbr.ToString))
            SetCellAsNumber(ws.Cell("F" & rowNbr.ToString))
            SetCellAsNumber(ws.Cell("G" & rowNbr.ToString))
            SetCellAsNumber(ws.Cell("H" & rowNbr.ToString))
            SetCellAsNumber(ws.Cell("I" & rowNbr.ToString))
            SetCellAsNumber(ws.Cell("J" & rowNbr.ToString))

            AddCellDetail(ws, "A", rowNbr, thisItem.AircraftId)
            AddCellDetail(ws, "B", rowNbr, thisItem.MSN)
            AddCellDetail(ws, "C", rowNbr, thisItem.ShipNbr)
            AddCellDetail(ws, "D", rowNbr, thisItem.Description)
            AddCellDetail(ws, "E", rowNbr, thisItem.CurrentLocationId)
            AddCellDetail(ws, "F", rowNbr, CDec(thisItem.AirframeHoursStart))
            AddCellDetail(ws, "G", rowNbr, CDec(thisItem.AirframeHoursCurrent))
            AddCellDetail(ws, "H", rowNbr, CDec(thisItem.AirframeHoursOffset))
            AddCellDetail(ws, "I", rowNbr, CDec(thisItem.AirframeHoursUnitId))
            AddCellDetail(ws, "J", rowNbr, CDec(thisItem.AircraftEngineHoursCurrent))

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

        Private Shared Sub AddCellDetail(ws As IXLWorksheet, col As String, row As Integer, value As String)
            ws.Cell(col + row.ToString()).Value = value
        End Sub

        Private Shared Sub AddCellDetail(ws As IXLWorksheet, col As String, row As Integer, value As Integer)
            ws.Cell(col + row.ToString()).Value = value
        End Sub

        Private Shared Sub AddCellDetail(ws As IXLWorksheet, col As String, row As Integer, value As Decimal)
            ws.Cell(col + row.ToString()).Value = value
        End Sub

        Private Shared Sub AddCellDetail(ws As IXLWorksheet, col As String, row As Integer, value As DateTime)
            ws.Cell(col + row.ToString()).Value = value
        End Sub

        Private Shared Sub SetCellAsNumber(Cell As IXLCell, Optional NumberFormat As String = "")
            Cell.SetDataType(XLDataType.Number)
            If Not String.IsNullOrEmpty(NumberFormat) Then
                Cell.Style.NumberFormat.Format = NumberFormat
            End If
        End Sub

        Private Shared Sub SetCellAsDateTime(Cell As IXLCell, Optional DateFormat As String = "")
            Cell.SetDataType(XLDataType.DateTime)
            If Not String.IsNullOrEmpty(DateFormat) Then
                Cell.Style.DateFormat.Format = DateFormat
            End If
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
