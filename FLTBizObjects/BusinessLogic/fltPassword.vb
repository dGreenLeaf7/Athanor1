' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System.Security.Cryptography
Imports System.Text

Public Class fltPassword

    Friend Shared Function CreatePasswordSalt() As String

        Dim rng As New RNGCryptoServiceProvider()
        Dim buff() As Byte
        ReDim buff(95)

        rng.GetBytes(buff)
        Return Convert.ToBase64String(buff)

    End Function

    Friend Shared Function CreatePasswordHash(ByVal Password As String, ByVal Salt As String) As String

        Dim saltAndPassword As String = String.Concat(Salt, Password)
        Dim charArray() As Char = saltAndPassword.ToCharArray
        Dim sha512 As New System.Security.Cryptography.SHA512CryptoServiceProvider()
        Dim buff() As Byte
        ReDim buff(255)
        For i As Integer = 0 To Len(saltAndPassword) - 1
            buff(i) = AscW(charArray(i))
        Next
        Dim hashedPassword() As Byte
        hashedPassword = sha512.ComputeHash(buff)
        Return Convert.ToBase64String(hashedPassword)

    End Function

    Friend Shared Function GeneratePassword() As String

        Dim length As Integer = 10
        Dim numberOfNonAlphanumericCharacters As Integer = 0

        Dim i As Integer
        Dim nonANcount As Integer = 0
        Dim buffer1 As Byte() = New Byte(length - 1) {}

        Dim chPassword As Char() = New Char(length - 1) {}

        Dim chPunctuations As Char() = "!@@$%^^*()_-+=[{]};:>|./?".ToCharArray()

        Dim rng As New System.Security.Cryptography.RNGCryptoServiceProvider
        rng.GetBytes(buffer1)

        For i = 0 To length - 1
            Dim rndChr As Integer = (buffer1(i) Mod 87)
            If (rndChr < 10) Then
                chPassword(i) = Convert.ToChar(Convert.ToUInt16(48 + rndChr))
            Else
                If (rndChr < 36) Then
                    chPassword(i) = Convert.ToChar(Convert.ToUInt16((65 + rndChr) - 10))
                Else
                    If (rndChr < 62) Then
                        chPassword(i) = Convert.ToChar(Convert.ToUInt16((97 + rndChr) - 36))
                    Else
                        chPassword(i) = chPunctuations(rndChr - 62)
                        nonANcount += 1
                    End If
                End If
            End If
        Next

        If nonANcount < numberOfNonAlphanumericCharacters Then
            Dim rndNumber As New Random
            For i = 0 To (numberOfNonAlphanumericCharacters - nonANcount) - 1
                Dim passwordPos As Integer
                Do
                    passwordPos = rndNumber.Next(0, length)
                Loop While Not Char.IsLetterOrDigit(chPassword(passwordPos))
                chPassword(passwordPos) = _
                        chPunctuations(rndNumber.Next(0, chPunctuations.Length))
            Next
        End If

        Return New String(chPassword)

    End Function

    Private Sub New()

    End Sub

End Class
