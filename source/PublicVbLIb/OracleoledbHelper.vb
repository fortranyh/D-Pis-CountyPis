Imports System.Data.OleDb
Imports System.Text

''' <summary>
''' 运用此类库，第一步：初始化此类的实例 第二步：打开ConnOpen 第三步：执行相关函数或者过程完成增删改查 第四步：关闭 ConnClose
''' </summary>
''' <remarks>必须按照四步走(所有查询结果都对结果进行Nothing判断；所有增删改都已经加上了事务处理，成功返回true，失败返回false)</remarks>
Public Class OracleoledbHelper
    '数据库连接对象
    Dim Conn As OleDbConnection
    '事务处理对象
    Dim Trans As OleDbTransaction = Nothing
    '数据库连接字符串
    Dim ConnectionStr As String
    '构造函数
    Sub New(ByVal ConnStr As String)
        ConnectionStr = ConnStr
        Conn = New OleDbConnection(ConnectionStr)
    End Sub
    ''' <summary>
    ''' 关闭数据库连接
    ''' </summary>
    ''' <returns>布尔值</returns>
    ''' <remarks>成功关闭返回true;否则返回false</remarks>
    Public Function ConnClose() As Boolean
        Try
            If Conn.State <> ConnectionState.Closed Then
                Conn.Close()
                Conn.Dispose()
                Conn = Nothing
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    ''' <summary>
    ''' 打开数据库连接
    ''' </summary>
    ''' <returns>布尔值</returns>
    ''' <remarks>成功关闭返回true;否则返回false</remarks>
    Public Function ConnOpen() As Boolean
        Try
            If Conn.State <> ConnectionState.Open Then
                Conn.Open()
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    ''' <summary>
    ''' 执行SQL语句或者存储过程；提供事务处理支持，执行update和insert和delete
    ''' </summary>
    ''' <param name="cmdType">执行类型</param>
    ''' <param name="cmdText">执行文本</param>
    ''' <returns>成功返回true;否则返回false</returns>
    ''' <remarks></remarks>
    Public Function ExecuteNoneQuery(ByVal cmdType As CommandType, ByVal cmdText As String) As Boolean
        Dim cmd As New OleDbCommand
        Try
            cmd.Connection = Conn
            cmd.CommandType = cmdType
            cmd.CommandText = cmdText
            '事务处理
            Trans = Conn.BeginTransaction
            cmd.Transaction = Trans
            cmd.ExecuteNonQuery()
            Trans.Commit()
            '清理资源
            Trans.Dispose()
            Trans = Nothing

            cmd.Dispose()
            cmd = Nothing
            Return True
        Catch ex As Exception
            If IsNothing(Trans) = False Then
                Trans.Rollback()
                Trans.Dispose()
                Trans = Nothing
            End If

            '清理资源
            cmd.Dispose()
            cmd = Nothing
        End Try
        Return False
    End Function

    ''' <summary>
    ''' 执行SQL语句或者存储过程；此为增删改，提供事务处理支持
    ''' </summary>
    ''' <param name="cmdTypes">执行类型</param>
    ''' <returns>返回是否执行成功</returns>
    ''' <remarks>成功返回true;否则返回false</remarks>
    Public Function ExecuteNoneQueryTrans(ByVal cmdTypes As List(Of CommandType), ByVal listSqls As List(Of String)) As Boolean
        Try
            If listSqls.Count > 0 AndAlso cmdTypes.Count = listSqls.Count Then
                Dim cmd As New OleDbCommand
                Trans = Conn.BeginTransaction
                cmd.Connection = Conn
                '事务支持
                cmd.Transaction = Trans
                For i As Integer = 0 To listSqls.Count - 1
                    cmd.CommandType = cmdTypes(i)
                    cmd.CommandText = listSqls(i)
                    cmd.ExecuteNonQuery()
                Next
                If IsNothing(Trans) = False Then
                    Trans.Commit()
                    '清理资源
                    Trans.Dispose()
                    Trans = Nothing
                End If
                Return True
            Else
                '传参有问题，返回失败标识false
                Return False
            End If
        Catch ex As Exception
            If IsNothing(Trans) = False Then
                Trans.Rollback()
                Trans.Dispose()
                Trans = Nothing
            End If
        End Try
        Return False
    End Function

    ''' <summary>
    ''' 返回记录集 查询
    ''' </summary>
    ''' <param name="DsTable">记录集中表名</param>
    ''' <param name="cmdType">类型</param>
    ''' <param name="cmdText">文本</param>
    ''' <returns>返回dateset或者nothing</returns>
    ''' <remarks>必须首先判断返回值是否是nothing</remarks>
    Public Function SelectDataSet(ByVal DsTable As String, ByVal cmdType As CommandType, ByVal cmdText As String) As DataSet
        Dim ds As DataSet = Nothing
        Try
            Dim cmd As New OleDbCommand
            cmd.Connection = Conn
            cmd.CommandType = cmdType
            cmd.CommandText = cmdText
            Dim da As New OleDbDataAdapter(cmd)
            ds = New DataSet
            da.Fill(ds, DsTable)
            '清理资源
            cmd.Dispose()
            cmd = Nothing
            da.Dispose()
            da = Nothing
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    ''' <summary>
    ''' 此为查询类型
    ''' </summary>
    ''' <param name="cmdType">执行类型</param>
    ''' <param name="cmdText">执行文本</param>
    ''' <returns>返回一个object类型的结果</returns>
    ''' <remarks>首先判断是否为Nothing;之后对返回值进行ctype()类型装换后使用</remarks>
    Public Function SelectScalar(ByVal cmdType As CommandType, ByVal cmdText As String) As Object
        Dim Result As Object = Nothing
        Try
            Dim cmd As New OleDbCommand
            cmd.Connection = Conn
            cmd.CommandType = cmdType
            cmd.CommandText = cmdText
            Result = cmd.ExecuteScalar()
            '清理资源
            cmd.Dispose()
            cmd = Nothing
            If IsNothing(Result) = True OrElse Result Is System.DBNull.Value Then
                '返回0 2013-7-11
                Return "0"
            End If
            Return Result
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' 返回datareader
    ''' </summary>
    ''' <param name="cmdType">类型</param>
    ''' <param name="cmdText">文本</param>
    ''' <returns>返回OleDbDataReader 或者nothing</returns>
    ''' <remarks>注意：调用该方法后，一定要对OracleDataReader进行Close</remarks>
    Public Function ExecuteDataReader(ByVal cmdType As CommandType, ByVal cmdText As String) As OleDb.OleDbDataReader
        Dim dr As OleDb.OleDbDataReader
        Try
            Dim cmd As New OleDbCommand
            cmd.Connection = Conn
            cmd.CommandType = cmdType
            cmd.CommandText = cmdText
            '执行
            dr = cmd.ExecuteReader()
            '清理资源
            cmd.Dispose()
            cmd = Nothing
            Return dr
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' 插入表
    ''' </summary>
    ''' <param name="tableName">表名</param>
    ''' <param name="data">数据</param>
    ''' <returns>成功返回TRUE</returns>
    ''' <remarks></remarks>
    Public Function InsertTable(ByVal tableName As String, ByVal data As Dictionary(Of String, String), ByVal list As Dictionary(Of String, String)) As Boolean
        Dim returnCode As Boolean = True
        Dim columnBuilder As StringBuilder = New StringBuilder()
        Dim valueBuilder As StringBuilder = New StringBuilder()
        Dim val As KeyValuePair(Of String, String)
        Dim i As Integer = 0
        For Each val In data
            columnBuilder.AppendFormat(" {0},", val.Key)
            Select Case list(val.Key).ToString
                Case "String"
                    valueBuilder.AppendFormat(" '{0}',", val.Value.Replace("'", "''"))
                Case "Integer"
                    valueBuilder.AppendFormat(" {0},", CInt(val.Value))
                Case "Double"
                    valueBuilder.AppendFormat(" {0},", Math.Round(CDbl(val.Value), 2))
                Case "DateTime"
                    valueBuilder.AppendFormat(" {0},", "to_date('" & val.Value & "','YYYY-MM-DD HH24:MI:SS')")
            End Select
        Next
        columnBuilder.Remove(columnBuilder.Length - 1, 1)
        valueBuilder.Remove(valueBuilder.Length - 1, 1)
        Try
            returnCode = ExecuteNoneQuery(CommandType.Text, String.Format("INSERT INTO {0}({1}) VALUES({2})", tableName, columnBuilder, valueBuilder))
        Catch ex As Exception

            columnBuilder.Remove(0, columnBuilder.Length)
            columnBuilder = Nothing
            valueBuilder.Remove(0, valueBuilder.Length)
            valueBuilder = Nothing
            returnCode = False
        End Try
        columnBuilder.Remove(0, columnBuilder.Length)
        columnBuilder = Nothing
        valueBuilder.Remove(0, valueBuilder.Length)
        valueBuilder = Nothing
        Return returnCode
    End Function

    ''' <summary>
    ''' 更新表
    ''' </summary>
    ''' <param name="tableName">表名</param>
    ''' <param name="data">数据</param>
    ''' <returns>成功返回TRUE</returns>
    ''' <remarks></remarks>
    Public Function UpdateTable(ByVal tableName As String, ByVal data As Dictionary(Of String, String), ByVal list As Dictionary(Of String, String), ByVal Tiaojian As String) As Boolean
        Dim returnCode As String = True
        Dim valueBuilder As StringBuilder = New StringBuilder()
        Dim val As KeyValuePair(Of String, String)
        Dim i As Integer = 0
        valueBuilder.Append(" set ")
        For Each val In data
            Select Case list(val.Key).ToString
                Case "String"
                    valueBuilder.AppendFormat("{0}='{1}',", val.Key, val.Value.Replace("'", "''"))
                Case "Integer"
                    valueBuilder.AppendFormat("{0}={1},", val.Key, CInt(val.Value))
                Case "Double"
                    valueBuilder.AppendFormat("{0}={1},", val.Key, Math.Round(CDbl(val.Value), 2))
                Case "DateTime"
                    valueBuilder.AppendFormat("{0}={1},", val.Key, "to_date('" & val.Value & "','YYYY-MM-DD HH24:MI:SS')")
            End Select
        Next
        valueBuilder.Remove(valueBuilder.Length - 1, 1)
        Try
            returnCode = ExecuteNoneQuery(CommandType.Text, String.Format("UPDATE {0}{1}{2}", tableName, valueBuilder, Tiaojian))
        Catch ex As Exception

            valueBuilder.Remove(0, valueBuilder.Length)
            valueBuilder = Nothing
            returnCode = False
        End Try
        valueBuilder.Remove(0, valueBuilder.Length)
        valueBuilder = Nothing
        Return returnCode
    End Function


    ''' <summary>
    ''' 批量插入表
    ''' </summary>
    ''' <param name="tableName">表名</param>
    ''' <param name="data">数据</param>
    ''' <returns>成功返回插入的sql语句</returns>
    ''' <remarks></remarks>
    Public Function PLInsertTable(ByVal tableName As String, ByVal data As Dictionary(Of String, String), ByVal list As Dictionary(Of String, String)) As String
        Dim returnstr As String = ""
        Dim columnBuilder As StringBuilder = New StringBuilder()
        Dim valueBuilder As StringBuilder = New StringBuilder()
        Dim val As KeyValuePair(Of String, String)
        Dim i As Integer = 0
        For Each val In data
            columnBuilder.AppendFormat(" {0},", val.Key)
            Select Case list(val.Key).ToString
                Case "String"
                    valueBuilder.AppendFormat(" '{0}',", val.Value.Replace("'", "''"))
                Case "Integer"
                    valueBuilder.AppendFormat(" {0},", CInt(val.Value))
                Case "Double"
                    valueBuilder.AppendFormat(" {0},", Math.Round(CDbl(val.Value), 2))
                Case "DateTime"
                    valueBuilder.AppendFormat(" {0},", "to_date('" & val.Value & "','YYYY-MM-DD HH24:MI:SS')")
            End Select
        Next
        columnBuilder.Remove(columnBuilder.Length - 1, 1)
        valueBuilder.Remove(valueBuilder.Length - 1, 1)
        Try
            returnstr = String.Format("INSERT INTO {0}({1}) VALUES({2})", tableName, columnBuilder, valueBuilder)
        Catch ex As Exception

            columnBuilder.Remove(0, columnBuilder.Length)
            columnBuilder = Nothing
            valueBuilder.Remove(0, valueBuilder.Length)
            valueBuilder = Nothing
            returnstr = ""
        End Try
        columnBuilder.Remove(0, columnBuilder.Length)
        columnBuilder = Nothing
        valueBuilder.Remove(0, valueBuilder.Length)
        valueBuilder = Nothing
        Return returnstr
    End Function

    ''' <summary>
    ''' 批量更新表
    ''' </summary>
    ''' <param name="tableName">表名</param>
    ''' <returns>成功返回TRUE</returns>
    ''' <remarks></remarks>
    Public Function PLDeleteTable(ByVal tableName As String, ByVal Tiaojian As String) As String
        Return String.Format(" delete from {0}{1}", tableName, Tiaojian)
    End Function
End Class
