Imports System.Data.Odbc
Imports System.Threading.Thread
Public Class Form1
    Dim nomor As String
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Button5.Enabled = False
        Call koneksinya()
        cmd = New OdbcCommand("select * from pegawai", conn)
        dr = cmd.ExecuteReader
        ComboBox2.Items.Add("NO HP")
        ComboBox2.SelectedIndex = 0
        While dr.Read()
            ComboBox2.Items.Add(dr.Item("hp"))
        End While

        For i As Integer = 0 To My.Computer.Ports.SerialPortNames.Count - 1
            ComboBox1.Items.Add(My.Computer.Ports.SerialPortNames(i))
        Next

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Call koneksinya()
        If TextBox2.Text = "" Then
            MessageBox.Show("Lengkapi Isian", "SMS Sender VB.Net", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            cmd = New OdbcCommand("select * from pegawai", conn)
            dr = cmd.ExecuteReader
            While dr.Read()
                nomor = dr.Item("hp")
                SerialPort1.Write("AT+CMGF=1" & Chr(13))
                Sleep(300)
                SerialPort1.WriteLine("AT+CMGS=" & Chr(34) & nomor & Chr(34) & vbCrLf)
                Sleep(300)
                SerialPort1.WriteLine(TextBox2.Text & Chr(26)) 'SMS sending
                Sleep(300)

            End While
            MessageBox.Show("Pesan Terkirim", "SMS Sender VB.Net", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ComboBox2.SelectedIndex = 0
            TextBox2.Clear()
        End If
    End Sub
    '------------------------------------------------------------------------------------------------------------
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ComboBox2.SelectedIndex = 0 Or TextBox2.Text = "" Then
            MessageBox.Show("Lengkapi Isian", "SMS Sender VB.Net", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            nomor = ComboBox2.SelectedItem.ToString
            SerialPort1.Write("AT+CMGF=1" & Chr(13))
            Sleep(300)
            SerialPort1.WriteLine("AT+CMGS=" & Chr(34) & nomor & Chr(34) & vbCrLf)
            Sleep(300)
            SerialPort1.WriteLine(TextBox2.Text & Chr(26)) 'SMS sending
            Sleep(300)
            MessageBox.Show("Pesan Terkirim", "SMS Sender VB.Net", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ComboBox2.SelectedIndex = 0
            TextBox2.Clear()
        End If

    End Sub
    '------------------------------------------------------------------------------------------------------------
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        ComboBox2.SelectedIndex = 0
        TextBox2.Clear()
    End Sub
    '------------------------------------------------------------------------------------------------------------

    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If ComboBox1.Text = Nothing Then
            MsgBox("Silahkan Pilih Port Dulu", MsgBoxStyle.Exclamation, "Koneksi")
        Else
            Try
                SerialPort1.PortName = ComboBox1.Text
                If SerialPort1.IsOpen = False Then
                    With SerialPort1
                        .Open()
                        .Handshake = IO.Ports.Handshake.RequestToSend
                    End With
                    MsgBox("Koneksi Port Sukses", MsgBoxStyle.Information, "Koneksi")
                    ComboBox1.Enabled = False
                    Button3.Enabled = False
                    Button5.Enabled = True
                End If
            Catch
                MsgBox("Tidak bisa terhubung ke PORT. Mungkin port sedang digunakan.", MsgBoxStyle.Exclamation, "Koneksi")
            End Try
        End If

    End Sub
    '------------------------------------------------------------------------------------------------------------
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        SerialPort1.Close()
        MsgBox("Koneksi Port Tertutup", MsgBoxStyle.Information, "Koneksi")
        Button5.Enabled = False
        Button3.Enabled = True
        ComboBox1.Enabled = True
        ComboBox1.SelectedItem = Nothing
    End Sub
End Class
