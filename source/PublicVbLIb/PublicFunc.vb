Imports System.IO

Public Class PublicFunc
    '调用独立EXE(exe全路径；参数串)
    Public Shared Sub InvokeExe(ByVal path As String, ByVal exename As String, ByVal Paras As String, Optional ByVal flagCur As Boolean = False)
        Dim objExecuteFile As New System.Diagnostics.ProcessStartInfo
        Dim objExecute As New System.Diagnostics.Process
        Try
            If File.Exists(path & "\" & exename) = True Then
                '应用程序路径
                'objExecuteFile.WorkingDirectory = path & "\"
                objExecuteFile.FileName = path & "\" & exename
                '应用程序参数
                objExecuteFile.Arguments = Paras
                objExecuteFile.UseShellExecute = False

                objExecute.StartInfo = objExecuteFile
                objExecute.Start()
                objExecute.EnableRaisingEvents = True
                If (flagCur) Then
                    '一直等待直到程序执行结束退出
                    objExecute.WaitForExit()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
