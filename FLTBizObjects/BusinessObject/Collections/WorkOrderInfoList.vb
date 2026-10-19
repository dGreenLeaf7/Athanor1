' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel

Namespace FLT.BO
    <Serializable()> _
    Public Class WorkOrderInfoList
        Inherits Collection(Of WorkOrderInfo)

        Public Sub New()
        End Sub

    End Class

End Namespace