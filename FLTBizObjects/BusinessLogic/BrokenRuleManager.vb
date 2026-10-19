' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.

Imports FLTBizObjects.FLT.BO

Friend Class BrokenRuleManager

    Shared Function ConsiderRules(ByVal BrokenRulesList As BrokenRulesList, ByVal PrevWarningsList As BrokenRulesList, ByVal WarningsList As BrokenRulesList, ByVal ErrorsList As BrokenRulesList) As Integer

        Dim errorCount As Integer = 0

        For Each BrokenRule As BrokenRule In BrokenRulesList
            If BrokenRule.OverRideableRule Then
                If Not BrokenRuleExistsInCollection(PrevWarningsList, BrokenRule) Then
                    errorCount = errorCount + 1
                    WarningsList.Add(BrokenRule)
                Else
                    For Each prevWarning As BrokenRule In PrevWarningsList
                        If BrokenRule.RuleIdentifier = prevWarning.RuleIdentifier Then
                            If prevWarning.OverRideRule Then
                                BrokenRule.OverRideRule = True
                            Else
                                errorCount = errorCount + 1
                            End If
                            WarningsList.Add(BrokenRule)
                        End If
                    Next
                End If
            Else
                errorCount = errorCount + 1
                ErrorsList.Add(BrokenRule)
            End If
        Next

        Return errorCount

    End Function

    Private Shared Function BrokenRuleExistsInCollection(RuleCollection As BrokenRulesList, BrokenRule As BrokenRule) As Boolean
        Dim ruleExists As Boolean = False
        For Each thisExistingRule As BrokenRule In RuleCollection
            If thisExistingRule.PropertyName = BrokenRule.PropertyName _
            And thisExistingRule.RuleIdentifier = BrokenRule.RuleIdentifier _
            And thisExistingRule.Message = BrokenRule.Message Then
                ruleExists = True
                Exit For
            End If
        Next
        Return ruleExists
    End Function

    Private Sub New()
    End Sub

End Class
