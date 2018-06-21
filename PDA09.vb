'2018/06/21 CTY 2018.6.21.5 新增零数料卷查询
Public Class PDA09
    Private Sqlcommand As String
    Dim dt As New DataTable

    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar1.ButtonClick
        Select Case ToolBar1.Buttons.IndexOf(e.Button)
            Case 0

            Case 3
                Search()
            Case 6
                OutPut()
            Case 13 '"結束"'
                Me.Close()
        End Select
    End Sub
    Private Sub Search()

        Sqlcommand = "select map.part_sn,reel.part_no,reel.chuge,map.qty" & _
                     "  from sajet.zero_reel reel, g_part_map map" & _
                     " where reel.reelno Is Not null" & _
                     "   and reel.reelno = map.part_sn"
        dt = New DataTable
        Me.ClsDBInfo.ExecuteSQL(Sqlcommand, dt)

        If dt.Rows.Count > 0 Then
            Me.dgv1.DataSource = dt
        End If


    End Sub
   
   
    Private Sub OutPut()
        If dgv1.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim xlExcelApp As Object = CreateObject("Excel.Application")
        Dim xlsBook As Object
        Dim xlSheet As Object
        xlsBook = xlExcelApp.Workbooks.Add()

        xlSheet = xlExcelApp.Sheets(1)
        Dim array(,) As String = TableToArray(dt)
        xlSheet.range(xlSheet.cells(1, 1), xlSheet.cells(UBound(array) + 1, UBound(array, 2) + 1)).value2 = array

        xlExcelApp.Visible = True
        xlSheet = Nothing
        xlsBook = Nothing
        xlExcelApp = Nothing

    End Sub
    Private Function TableToArray(ByVal dt As DataTable) As String(,)
        Dim x As Integer = dt.Rows.Count
        Dim y As Integer = dt.Columns.Count - 1

        Dim result(x, y) As String
        For i As Integer = 0 To y
            result(0, i) = dt.Columns(i).ColumnName
        Next

        For i As Integer = 0 To x - 1
            For ii As Integer = 0 To y
                result(i + 1, ii) = IIf(IsDBNull(dt.Rows(i)(ii)), " ", dt.Rows(i)(ii))
            Next
        Next

        Return result
    End Function

   

End Class
