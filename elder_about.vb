Imports System.IO

Public Class elder_about
    Dim beta As String = Application.StartupPath & "/.beta"
    Private Sub cb_beta_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cb_beta.MouseClick
        Try
            If cb_beta.Checked = True Then
                File.Create(beta).Dispose()
                MsgBox("Změny budou provedeny při příštím spuštění")
            Else
                File.Delete(beta)
                MsgBox("Změny budou provedeny při příštím spuštění")
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub elder_about_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If My.Computer.FileSystem.FileExists(beta) = True Then
            cb_beta.Checked = True
        End If
    End Sub
End Class