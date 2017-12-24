Imports System.Data.Odbc
Module KONEKSI
    Public conn As Odbc.OdbcConnection
    Public cmd As Odbc.OdbcCommand
    Public dr As OdbcDataReader
    Sub koneksinya()
        conn = New Odbc.OdbcConnection("Dsn=crud")
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
    End Sub
End Module
