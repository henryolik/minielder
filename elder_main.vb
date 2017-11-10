﻿Imports System.Net

Public Class elder_main
    Private Sub OProgramuToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles OProgramuToolStripMenuItem.Click
        elder_about.Show()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "/mini.exe") Then
            Process.Start(Application.StartupPath & "/mini.exe")
        Else
            MsgBox("Nemůžu nalézt soubor hry. Prosím, přeinstalujte hru")
        End If
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "/mini2.exe") Then
            Process.Start(Application.StartupPath & "/mini2.exe")
        Else
            MsgBox("Nemůžu nalézt soubor hry. Prosím, přeinstalujte hru")
        End If
    End Sub
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim osVer As Version = Environment.OSVersion.Version
        If IsConnectionAvailable() = True Then
            CheckForUpdates()
        End If
        Me.Show()
        If osVer.Major = 6 And osVer.Minor = 0 Then
            MsgBox("Používáte systém Windows Vista, toto je verze spouštěče určená pro Windows 98, ME, 2000 a XP. Stáhněte si, prosím klasickou verzi na https://ministudios.ml")
        ElseIf osVer.Major = 6 And osVer.Minor = 1 Then
            MsgBox("Používáte systém Windows 7, toto je verze spouštěče určená pro Windows 98, ME, 2000 a XP. Stáhněte si, prosím klasickou verzi na https://ministudios.ml")
        ElseIf osVer.Major = 6 And osVer.Minor = 2 Then
            MsgBox("Používáte systém Windows 8 nebo vyšší, toto je verze spouštěče určená pro Windows 98, ME, 2000 a XP. Stáhněte si, prosím klasickou verzi na https://ministudios.ml")
        ElseIf osVer.Major = 6 And osVer.Minor = 3 Then
            MsgBox("Používáte systém Windows 8.1, toto je verze spouštěče určená pro Windows 98, ME, 2000 a XP. Stáhněte si, prosím klasickou verzi na https://ministudios.ml")
        End If
    End Sub
    Public Sub CheckForUpdates()
        Dim version As String = Application.StartupPath & "/version.txt"
        Dim updater As String = Application.StartupPath & "/updater.exe"
        Dim MyVer As String = My.Application.Info.Version.ToString
        If My.Computer.FileSystem.FileExists(version) Then
            My.Computer.FileSystem.DeleteFile(version)
        End If
        Dim wc As WebClient = New WebClient()
        wc.DownloadFile("http://nonssl.dl.ministudios.ml/mini/elder/version.txt", version)
        Dim LastVer As String = My.Computer.FileSystem.ReadAllText(version)
        If Not MyVer = LastVer Then
            Dim msg = "Je k dispozici aktualizace! Chcete ji stáhnout?"
            Dim title = "Aktualizace"
            Dim style = MsgBoxStyle.YesNo
            Dim response = MsgBox(msg, style, title)
            If response = MsgBoxResult.Yes Then
                wc.DownloadFile("http://nonssl.dl.ministudios.ml/updater/updater.exe", updater)
                Process.Start(Application.StartupPath & "/updater.exe", "-elder")
                Me.Close()
            End If
        End If
    End Sub
    Public Function IsConnectionAvailable() As Boolean
        Dim objUrl As New System.Uri("http://nonssl.dl.ministudios.ml/status.txt")
        Dim objWebReq As System.Net.WebRequest
        objWebReq = System.Net.WebRequest.Create(objUrl)
        Dim objResp As System.Net.WebResponse
        Try
            objResp = objWebReq.GetResponse
            objResp.Close()
            objWebReq = Nothing
            Return True
        Catch ex As Exception
            objWebReq = Nothing
            Return False
        End Try
    End Function
End Class