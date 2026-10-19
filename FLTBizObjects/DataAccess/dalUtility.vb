' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System

Namespace FLT.Dal

	Public Class DalUtility
		
		Public Shared Function ConvertRowVersionToString(ByVal inputValue as Byte()) As String
			Dim thisValue As Int64 = 0
			For i As Integer = 0 To 7 Step 1
				thisValue = (thisValue * 1000) + inputValue(i)
			Next
			Return Cstr(thisValue)
		End Function
		
		Public Shared Function ConvertStringToRowVersion(ByVal inputValue as String) As Byte()
			Dim thisValue As Int64 = 0
			Dim w As Int64 = 0
			Dim x As Int64 = 0
			Dim y As Int64 = 0
			Dim z As Int64 = 0
			thisValue = Convert.ToInt64(inputValue)
			Dim b as Byte()
			ReDim b(7)
			For i As Integer = 1 To 7 Step 1
				x = Convert.ToInt64(thisValue Mod (Math.Pow(1000, (8 - i))))
				y = Convert.ToInt64(thisValue Mod (Math.Pow(1000, (7 - i))))
				w = Convert.ToInt64(Math.Pow(1000, (7 - i)))
				z = (x - y) / w
				b(i) = Convert.ToByte(z)
			Next
			Return b
		End Function
		
	End Class

End Namespace
