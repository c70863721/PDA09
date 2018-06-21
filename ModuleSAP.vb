Module ModuleSAP
    Public StrSAPIP As String = ""    'prod:10.0.0.96
    Public StrSAPClient As String = ""      'prod:888
    Public StrSAPNo As String = ""
    Public StrSAPAcc As String = ""
    Public StrSAPPwd As String = ""
    Public Function SAPRFCLink(ByRef SAPConn) As Boolean
        Dim NonErr As Boolean = False

        Try
            SAPConn.applicationserver = StrSAPIP
            SAPConn.client = StrSAPClient
            SAPConn.user = StrSAPAcc
            SAPConn.password = StrSAPPwd
            SAPConn.language = "EN"
            SAPConn.CodePage = "8300"

            If SAPConn.logon(0, True) <> True Then
                NonErr = False
            Else
                NonErr = True
            End If

        Catch er As Exception
            MsgBox(er.ToString)
        End Try

        Return NonErr
    End Function

    Public Sub LoadSAPpara()
        Dim file As String = "c:\tmis\saprfc.ini"
        Dim Input As String = ""
        Using sr As IO.StreamReader = IO.File.OpenText(file)
            Input = sr.ReadLine()
            Input = Input
            While sr.Peek > -1 Or Len(Input) > 0
                If InStr(UCase(Input), "SAPIP") > 0 Then
                    StrSAPIP = Trim(Mid(Input, InStr(Input, "=") + 1))
                End If

                If InStr(UCase(Input), "SAPCLIENT") > 0 Then
                    StrSAPClient = Trim(Mid(Input, InStr(Input, "=") + 1))
                End If

                If InStr(UCase(Input), "SAPNO") > 0 Then
                    StrSAPNo = Trim(Mid(Input, InStr(Input, "=") + 1))
                End If

                If InStr(UCase(Input), "SAPACC") > 0 Then
                    StrSAPAcc = Trim(Mid(Input, InStr(Input, "=") + 1))
                End If

                If InStr(UCase(Input), "SAPPWD") > 0 Then
                    StrSAPPwd = Trim(Mid(Input, InStr(Input, "=") + 1))
                End If

                Input = sr.ReadLine()
                Input = Input
            End While
        End Using

    End Sub

  
End Module
