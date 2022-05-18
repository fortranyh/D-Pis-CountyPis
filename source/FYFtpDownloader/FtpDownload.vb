Imports System.Threading

Public Class FtpDownload
    Private ExeFlag As Boolean = True
    Public DownCompleteFileCount As Integer = 0
    Public Event DownLoadComplete()
    Public Event DownLoadError(ByVal errorInfo As String)
    Public TotalFileCount As Integer = 0
    Dim t As Thread
    '加锁标记
    Private Shared lockObj As Object = New Object()
    Public Sub ExecuteDownLoad(ByVal ftpip As String, ByVal ftpport As Integer, ByVal user As String, ByVal pwd As String, ByVal RemoteParas As List(Of String), ByVal LocalParas As List(Of String), ByVal timeout As Integer, ByVal total As Integer)
        ExeFlag = True
        TotalFileCount = total
        DownCompleteFileCount = 0
        Try
            Me.Label1.Text = String.Format("{0}/{1}", DownCompleteFileCount, TotalFileCount)
            Dim FtpParasIns As New FtpParas With {.ftpip = ftpip, .ftpport = ftpport, .ftppwd = pwd, .ftpuser = user, .RemoteFiles = RemoteParas, .LocalFiles = LocalParas, .timeout = timeout, .total = total}
            t = New Thread(New ParameterizedThreadStart(AddressOf Process))
            t.IsBackground = True
            t.Start(FtpParasIns)
        Catch ex As Exception
            ExeFlag = False
            RaiseEvent DownLoadError(ex.ToString())
        End Try
    End Sub
    Public Sub SetOver()
        Try
            If IsNothing(t) = False Then
                ExeFlag = False
                Thread.Sleep(500)
                t.Abort()
                DownCompleteFileCount = 0
                TotalFileCount = 0
                Me.Label1.Text = String.Format("{0}/{1}", DownCompleteFileCount, TotalFileCount)
            End If
        Catch ex As Exception
            ExeFlag = False
            RaiseEvent DownLoadError(ex.ToString())
        End Try
    End Sub
    Private Sub updateui()
        Me.Label1.Text = String.Format("{0}/{1}", DownCompleteFileCount, TotalFileCount)
    End Sub

    Private Delegate Sub ProgressChangeHandler()

    Sub Process(ByVal arg As Object)
        Try
            Dim FtpParasIns As FtpParas = CType(arg, FtpParas)
            Dim FtpAddr = String.Format("ftp://{0}:{1}/", FtpParasIns.ftpip, FtpParasIns.ftpport)
            While (ExeFlag)
                For i As Integer = 0 To FtpParasIns.RemoteFiles.Count - 1

                    DownCompleteFileCount = DownCompleteFileCount + 1

                    Me.BeginInvoke(New ProgressChangeHandler(AddressOf updateui))

                    My.Computer.Network.DownloadFile(FtpAddr & FtpParasIns.RemoteFiles(i), FtpParasIns.LocalFiles(i), FtpParasIns.ftpuser, FtpParasIns.ftppwd, False, FtpParasIns.timeout, True)

                    'Me.BeginInvoke(New MethodInvoker(AddressOf updateui))

                    If DownCompleteFileCount = FtpParasIns.total Then
                        ExeFlag = False
                        RaiseEvent DownLoadComplete()
                        Exit For
                    End If
                Next
            End While
        Catch ex As Exception
            ExeFlag = False
            RaiseEvent DownLoadError(ex.ToString())
        End Try
    End Sub
End Class

Public Class FtpParas
    Public Property ftpip As String
    Public Property ftpport As Integer
    Public Property ftppwd As String
    Public Property ftpuser As String
    Public Property RemoteFiles As List(Of String)
    Public Property LocalFiles As List(Of String)
    '毫秒为单位的超时
    Public Property timeout As Integer
    '控件全部文件数目
    Public Property total As Integer
End Class
