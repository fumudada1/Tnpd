Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim objSMS As Object
        Dim ServerIp As String
        Dim ServerPort As String
        Dim UserID As String
        Dim Passwd As String
        Dim ret_code As Integer
        Dim ret_description As String
        Dim Tel As String
        Dim Message As String

        ServerIp = "api.hiair.hinet.net"
        ServerPort = "8000"
        UserID = "±b¸¹"
        Passwd = "±K½X"

        objSMS = CreateObject("HiAir.HiNetSMS")
        ret_code = objSMS.StartCon(ServerIp, ServerPort, UserID, Passwd)
        If ret_code = 0 Then
            Tel = "0910128xxx"
            Message = "Â²°T´ú¸Õ"
            ret_code = objSMS.SendMsg(Tel, Message)
            ret_description = objSMS.Get_Message()
            MsgBox(ret_description)
        Else
            ret_description = objSMS.Get_Message()
            MsgBox(ret_description)
        End If

        objSMS.EndCon()


    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        Dim objSMS As Object
        Dim ServerIp As String
        Dim ServerPort As String
        Dim UserID As String
        Dim Passwd As String
        Dim ret_code As Integer
        Dim ret_description As String
        Dim Send_MSISDN As String


        ServerIp = "api.hiair.hinet.net"
        ServerPort = "8000"
        UserID = "±b¸¹"
        Passwd = "±K½X"

        objSMS = CreateObject("HiAir.HiNetSMS")
        ret_code = objSMS.StartCon(ServerIp, ServerPort, UserID, Passwd)
        If ret_code = 0 Then
            ret_code = objSMS.RecvMsg()
            ret_description = objSMS.Get_Message()
            Send_MSISDN = objSMS.Get_Send_MSISDN()
            MsgBox(ret_description)
            MsgBox(Send_MSISDN)
        Else
            ret_description = objSMS.Get_Message()
            MsgBox(ret_description)
        End If

        objSMS.EndCon()
    End Sub
End Class
