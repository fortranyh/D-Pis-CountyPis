Public Class VBProcess

    Public Shared Function ftpUpload(ByVal sourceFile As String, ByVal remoteFile As String, ByVal IP As String, ByVal Port As String, ByVal UserName As String, ByVal UserPwd As String) As Boolean
        Try
            Dim url As String = String.Format("ftp://{0}:{1}/{2}", IP, Port, remoteFile)
            My.Computer.Network.UploadFile(sourceFile, url, UserName, UserPwd, False, 30000)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

End Class
