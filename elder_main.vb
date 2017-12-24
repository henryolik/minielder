Option Strict On
Imports System.Net
Imports System.IO
Imports System.Environment

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
        Dim beta As String = Application.StartupPath & "/.beta"
        If IsConnectionAvailable() = True Then
            If My.Computer.FileSystem.FileExists(beta) = True Then
                Try
                    msgbeta()
                    CheckForBeta()
                Catch ex As Exception
                    MsgBox(ex.ToString, MsgBoxStyle.Critical, "Chyba při kontrole aktualizací!")
                End Try
            Else
                Try
                    CheckForUpdates()
                    msg()
                Catch ex As Exception
                    MsgBox(ex.ToString, MsgBoxStyle.Critical, "Chyba při kontrole aktualizací!")
                End Try
            End If
        End If
        Me.Show()
        If osVer.Major = 6 And osVer.Minor = 0 Then
            MsgBox("Používáte systém Windows Vista, toto je verze spouštěče určená pro Windows 98, ME, 2000 a XP. Stáhněte si, prosím klasickou verzi na https://ministudios.ml")
        ElseIf osVer.Major = 6 And osVer.Minor = 1 Then
            MsgBox("Používáte systém Windows 7, toto je verze spouštěče určená pro Windows 98, ME, 2000 a XP. Stáhněte si, prosím klasickou verzi na https://ministudios.ml")
        ElseIf osVer.Major = 6 And osVer.Minor = 2 Then
            If My.Computer.Info.OSFullName.Contains("10") Then
                MsgBox("Používáte systém Windows 10, toto je verze spouštěče určená pro Windows 98, ME, 2000 a XP. Stáhněte si, prosím klasickou verzi na https://ministudios.ml")
            Else
                If My.Computer.Info.OSFullName.Contains("8.1") Then
                    MsgBox("Používáte systém Windows 8.1, toto je verze spouštěče určená pro Windows 98, ME, 2000 a XP. Stáhněte si, prosím klasickou verzi na https://ministudios.ml")
                Else
                    MsgBox("Používáte systém Windows 8, toto je verze spouštěče určená pro Windows 98, ME, 2000 a XP. Stáhněte si, prosím klasickou verzi na https://ministudios.ml")
                End If
            End If
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
        My.Computer.Network.DownloadFile("http://nonssl.dl.ministudios.ml/mini/elder/version.txt", version)
        Dim verze() As String = IO.File.ReadAllLines(version)
        Dim LastVer As String = verze(0)
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
    Public Sub CheckForBeta()
        Dim version As String = Application.StartupPath & "/version.txt"
        Dim updater As String = Application.StartupPath & "/updater.exe"
        Dim MyVer As String = My.Application.Info.Version.ToString
        If My.Computer.FileSystem.FileExists(version) Then
            My.Computer.FileSystem.DeleteFile(version)
        End If
        Dim wc As WebClient = New WebClient()
        wc.DownloadFile("http://nonssl.dl.ministudios.ml/mini/elder/version.txt", version)
        Dim verze() As String = IO.File.ReadAllLines(version)
        Dim LastVer As String = verze(1)
        If Not MyVer = LastVer Then
            If LastVer = verze(0) Then
                CheckForUpdates()
            Else
            Dim msg = "Je k dispozici aktualizace! Chcete ji stáhnout?"
            Dim title = "Aktualizace"
            Dim style = MsgBoxStyle.YesNo
            Dim response = MsgBox(msg, style, title)
            If response = MsgBoxResult.Yes Then
                wc.DownloadFile("http://nonssl.dl.ministudios.ml/updater/updater.exe", updater)
                Process.Start(Application.StartupPath & "/updater.exe", "-elderbeta")
                    Me.Close()
                End If
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

    Public Sub msg()
        Dim MyVer As String = My.Application.Info.Version.ToString
        Dim osVer As Version = Environment.OSVersion.Version
        Dim wc As WebClient = New WebClient()
        Dim os As String = Nothing
        If Not osVer.Major = 6 Then
            If getVersion.Contains("98") Then
                os = "98"
            End If
            If getVersion.Contains("ME") Then
                os = "me"
            End If
            If osVer.Major = 5 And osVer.Minor = 0 Then
                os = "2000"
            End If
            If osVer.Major = 5 And osVer.Minor = 1 Then
                os = "xp"
            End If
            If Not os = Nothing Then
                Dim msg As String = Application.StartupPath & "/msg.txt"
                wc.DownloadFile(New Uri("http://nonssl.dl.ministudios.ml/mini/elder/msgs/" & MyVer & "/" & os & ".txt"), msg)
                If Not My.Computer.FileSystem.ReadAllText(msg) = "" Then
                    MsgBox(My.Computer.FileSystem.ReadAllText(msg), MsgBoxStyle.Information, "Oznámení")
                End If
            End If
        End If
    End Sub

    Public Sub msgbeta()
            Dim MyVer As String = My.Application.Info.Version.ToString
            Dim osVer As Version = Environment.OSVersion.Version
            Dim wc As WebClient = New WebClient()
        Dim os As String = Nothing
        If Not osVer.Major = 6 Then
            If getVersion.Contains("98") Then
                os = "98"
            End If
            If getVersion.Contains("ME") Then
                os = "me"
            End If
            If osVer.Major = 5 And osVer.Minor = 0 Then
                os = "2000"
            End If
            If osVer.Major = 5 And osVer.Minor = 1 Then
                os = "xp"
            End If
        If Not os = Nothing Then
            Dim msg As String = Application.StartupPath & "/msg.txt"
                wc.DownloadFile(New Uri("http://nonssl.dl.ministudios.ml/mini/elder/beta/msgs/" & MyVer & "/" & os & ".txt"), msg)
            If Not My.Computer.FileSystem.ReadAllText(msg) = "" Then
                MsgBox(My.Computer.FileSystem.ReadAllText(msg), MsgBoxStyle.Information, "Oznámení")
            End If
            End If
        End If
    End Sub
    Public Sub load_settings()
        elder_about.cb_beta.Checked = My.Settings.beta
    End Sub

    Public Function getVersion() As String
        Dim osInfo As OperatingSystem
        osInfo = OSVersion
        With osInfo
            Select Case .Platform
                Case .Platform.Win32Windows
                    Select Case (.Version.Minor)
                        Case 10
                            If .Version.Revision.ToString() = "2222A" Then
                                getVersion = "Windows 98 Second Edition"
                            Else
                                getVersion = "Windows 98"
                            End If
                        Case 90
                            getVersion = "Windows ME"
                    End Select
                Case Else
                    getVersion = "Nerozpoznáno"
            End Select
        End With
    End Function
End Class
