' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Runtime.InteropServices
Imports System.Text

Public Class CredentialHelper

    <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
    Private Shared Function CredRead(target As String, type As Integer, reservedFlat As Integer, ByRef credentialPtr As IntPtr) As Boolean

    End Function

    <DllImport("advapi32.dll", SetLastError:=True)>
    Private Shared Sub CredFree(buffer As IntPtr)

    End Sub

    Private Const CRED_TYPE_GENERIC As Integer = 1

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
    Private Structure CREDENTIAL
        Public Flags As Integer
        Public Type As Integer
        Public TargetName As String
        Public Comment As String
        Public LastWritten As Long
        Public CredentialBlobSize As Integer
        Public CredentialBlob As IntPtr
        Public Persists As Integer
        Public AttributeCount As Integer
        Public Attributes As IntPtr
        Public TargetAlias As String
        Public UserName As String
    End Structure

    Public Shared Function GetCredential(target As String) As (Username As String, Password As String)

        Dim credPtr As IntPtr = IntPtr.Zero

        If Not CredRead(target, CRED_TYPE_GENERIC, 0, credPtr) Then
            Throw New Exception("Credential not found")

        End If

        Dim cred As CREDENTIAL = Marshal.PtrToStructure(Of CREDENTIAL)(credPtr)

        Dim password As String = ""

        If cred.CredentialBlobSize > 0 Then
            password = Marshal.PtrToStringUni(cred.CredentialBlob, cred.CredentialBlobSize \ 2)
        End If

        Dim username = cred.UserName

        CredFree(credPtr)

        Return (username, password)

    End Function
End Class
