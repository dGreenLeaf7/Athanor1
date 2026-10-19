' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Collections.ObjectModel

Namespace FLT.BO

    <Serializable()> _
     Public Class fltLookupNameList
        Inherits Collection(Of String)

        Public Sub New()
            Me.Add("APUType")
            Me.Add("Pilot")
            Me.Add("Emission")
            Me.Add("Engineer")
            Me.Add("EngineeringAction")
            Me.Add("EngineType")
            Me.Add("IssueCode")
            Me.Add("LengthUnit")
            Me.Add("LiquidGroup")
            Me.Add("LiquidType")
            Me.Add("LiquidUnit")
            Me.Add("Manufacturer")
            Me.Add("MaturityDef")
            Me.Add("MechAction")
            Me.Add("Model")
            Me.Add("Mechanic")
            Me.Add("Test")
            Me.Add("LoadingConfig")
            Me.Add("Propeller")
            Me.Add("PropellerType")
            Me.Add("AircraftComponent")
            Me.Add("AircraftStatusDef")
            Me.Add("WeightUnit")
            Me.Add("WorkOrderStatus")
            Me.Add("WorkOrderType")
            Me.Add("WorkShift")
        End Sub

    End Class
End Namespace