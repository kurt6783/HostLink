Imports System.IO.Ports
Public Class Form1
    Dim SerialPort As SerialPort = New SerialPort

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        SerialPort.PortName = "COM4"
        SerialPort.Parity = Parity.None
        SerialPort.DataBits = 8
        SerialPort.StopBits = 1
        SerialPort.BaudRate = 9600
        SerialPort.Open()
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        TextBox1.Text = Nothing
        Dim SendCode_String As String = "@" & ComboBox6.Text.PadLeft(2, "0") & "RD00000002"
        SendCode_String = SendCode_String & FCS(SendCode_String) & "*" & Chr(13)
        Dim SendCode_Byte() As Byte
        SendCode_Byte = System.Text.Encoding.Default.GetBytes(SendCode_String)
        SerialPort.Write(SendCode_Byte, 0, SendCode_Byte.Count)
        Threading.Thread.Sleep(100)
        Dim ReceiveData(SerialPort.BytesToRead - 1) As Byte
        SerialPort.Read(ReceiveData, 0, SerialPort.BytesToRead)
        TextBox1.Text = System.Text.Encoding.UTF8.GetString(ReceiveData)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim SendCode_String As String = "@" & ComboBox6.Text.PadLeft(2, "0") & "WD0000FFFF"
        SendCode_String = SendCode_String & FCS(SendCode_String) & "*" & Chr(13)
        Dim SendCode_Byte() As Byte
        SendCode_Byte = System.Text.Encoding.Default.GetBytes(SendCode_String)
        SerialPort.Write(SendCode_Byte, 0, SendCode_Byte.Count)
        Threading.Thread.Sleep(100)
        Dim ReceiveData(SerialPort.BytesToRead - 1) As Byte
        SerialPort.Read(ReceiveData, 0, SerialPort.BytesToRead)
        TextBox4.Text = System.Text.Encoding.UTF8.GetString(ReceiveData)
    End Sub

    Function FCS(ByVal Code As String)
        Dim Result As Integer = 0
        For i = 1 To Len(Code)
            Result = Asc(Mid(Code, i, 1)) Xor Result
        Next i
        Return Hex(Result)
    End Function

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
