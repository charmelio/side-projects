' Program:      Tampa Bay Yacht Charters [Visual Basic 2010]
' Programmer:   Tommy Bishop
' Description:  Find the price of the charter by the length of the yacht and hours chartered
'               Also prints reports for the Summary Report and Yacht Types

Option Strict On
Imports System.IO

Public Class mainForm
    Dim HoursChartered As Integer       ' For Input: Hours Chartered
    Dim YachtSize As Integer            ' For Input: Size of the Yacht
    Dim numOfCharters As Integer        ' Number of Charters
    Dim totalHours As Integer           ' Total number of Hours
    Dim yachtPrice As Integer           ' Price of a Yacht Charter per Length
    Dim charterPrice As Double          ' Hours times Yacht Price (by length) equals this
    Dim TotalRevenue As Double          ' All charterPrices added together
    Dim AverageHours As Double          ' Total Revenue divded by Number of Charters

    Private IsDirtyBoolean As Boolean   ' Boolean to check for buffer changes


    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        If ValidateData() Then
            Try
                HoursChartered = Integer.Parse(txtHours.Text)
                YachtSize = Integer.Parse(comboSize.Text)

                ' Case selections are fixed numbers via unchangeable comboBox in a listBox setup
                Select Case YachtSize
                    Case 22
                        yachtPrice = 95
                    Case 24
                        yachtPrice = 137
                    Case 30
                        yachtPrice = 160
                    Case 32
                        yachtPrice = 192
                    Case 36
                        yachtPrice = 250
                    Case 38
                        yachtPrice = 400
                    Case Else
                        yachtPrice = 550
                End Select

                charterPrice = (yachtPrice * HoursChartered)
                lblPrice.Text = charterPrice.ToString("C")          ' Converts Charter Price to Currency Format 

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Processing Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Finally
                numOfCharters += 1
                totalHours += HoursChartered
                TotalRevenue += charterPrice
                AverageHours = (totalHours / numOfCharters)

                ' Changes the Print Summary to be enabled afterwards
                PrintSummaryToolStripMenuItem.Enabled = True
            End Try
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()              ' Closes the form
    End Sub

    Private Sub ClearForNextCharterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearForNextCharterToolStripMenuItem.Click
        txtParty.Clear()
        txtHours.Clear()
        comboType.Text = ""
        comboSize.SelectedIndex = -1                    ' Unselects any of the variables in the comboBox
        lblPrice.Text = ""
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        ' About form to show programmer information
        Dim AboutForm As New aboutForm
        AboutForm.ShowDialog()
    End Sub

    Private Sub PrintSummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintSummaryToolStripMenuItem.Click
        ' Print dialog for the summary report
        PrintPreviewDialog1.Document = PrintDocument1
        PrintPreviewDialog1.ShowDialog()
    End Sub

    Private Sub PrintYachtTypesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintYachtTypesToolStripMenuItem.Click
        ' Print dialog for all the names in the comboType field
        PrintPreviewDialog2.Document = PrintDocument2
        PrintPreviewDialog2.ShowDialog()
    End Sub

    Private Sub AddAYachtTypeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddAYachtTypeToolStripMenuItem.Click
        ' Adds type to the comboType
        Dim BooleanNameFound As Boolean = False
        Dim intIndex As Integer = 0

        ' Checks to make sure it's not creating duplicates
        With comboType
            Do Until BooleanNameFound Or intIndex >= .Items.Count
                If .Text.Trim.ToUpper = .Items(intIndex).ToString.ToUpper Then
                    BooleanNameFound = True
                End If
                intIndex += 1
            Loop

            ' If a duplicate is found, add nothing to the comboBox
            If Not BooleanNameFound Then
                If .Text.Trim <> "" Then
                    .Items.Add(.Text)
                    .Text = ""
                    IsDirtyBoolean = True
                Else
                    MessageBox.Show("Enter A Yacht Type", "Data Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        End With
    End Sub

    Private Sub RemoveAYachtTypeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveAYachtTypeToolStripMenuItem.Click
        ' Removes Selected from comboType
        If comboType.SelectedIndex > -1 Then
            comboType.Items.RemoveAt(comboType.SelectedIndex)
            IsDirtyBoolean = True
        Else
            MessageBox.Show("Please Select A Yacht Type To Remove", "Data Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub DisplayACountOfYachtTypesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisplayACountOfYachtTypesToolStripMenuItem.Click
        ' MessageBox of the length of the comboType field
        MessageBox.Show("Number of different Yacht Types:  " & comboType.Items.Count, "Total Yachts", _
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Function ValidateData() As Boolean
        Dim ErrorMessage As String = ""
        Dim NoErrorsFound As Boolean = True

        ' Checks for party name entry
        If txtParty.Text.Trim = "" Then
            ErrorMessage &= "Party Name is Required" & ControlChars.NewLine
            NoErrorsFound = False
        End If

        ' Checks to make sure that hours is numeric
        If Not IsNumeric(txtHours.Text) Then
            ErrorMessage &= "Hours Chartered Must be Numeric" & ControlChars.NewLine
            NoErrorsFound = False
        End If

        ' Checks to make sure somethings selected in the combo box
        If comboType.Text.Trim = "" Then
            ErrorMessage &= "Yacht Type is Required" & ControlChars.NewLine
            NoErrorsFound = False
        End If

        ' Checks to make sure a length has been entered
        If Not IsNumeric(comboSize.Text) Then
            ErrorMessage &= "Yacht Size is Required" & ControlChars.NewLine
            NoErrorsFound = False
        End If

        If NoErrorsFound Then
            Return True
        Else
            MessageBox.Show(ErrorMessage, "Data Entry Error", MessageBoxButtons.OK, _
                            MessageBoxIcon.Information)
            Return False
        End If
    End Function

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ' Calls another sub
        ClearForNextCharterToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        ' Prints Summary Report
        Dim PrintXCoord As Single = e.MarginBounds.Left             ' Set up on Left Margin
        Dim PrintYCoord As Single = e.MarginBounds.Top              ' Set up on Top Margin
        Dim PrintFont As Font = New Font("Courier New", 16)         ' Set up font type and size
        Dim LineHeight As Single = PrintFont.GetHeight + 2          ' Set up height of a line
        Dim PrintString As String                                   ' String for output

        ' Prints the page header
        PrintString = "Tampa Bay Charters Summary Data"
        e.Graphics.DrawString(PrintString, PrintFont, Brushes.Black, PrintXCoord, PrintYCoord)      ' Basically the print command
        PrintYCoord += LineHeight                                                                   ' Line break

        PrintString = "By Tommy Bishop"
        e.Graphics.DrawString(PrintString, PrintFont, Brushes.Black, PrintXCoord, PrintYCoord)
        PrintYCoord += LineHeight * 2

        ' Prints the total revenue, number of charters and average hours
        PrintString = "Total Revenue" & ControlChars.Tab & ControlChars.Tab & TotalRevenue.ToString("C")
        e.Graphics.DrawString(PrintString, PrintFont, Brushes.Black, PrintXCoord, PrintYCoord)
        PrintYCoord += LineHeight

        PrintString = "Total Number of Charters" & ControlChars.Tab & numOfCharters
        e.Graphics.DrawString(PrintString, PrintFont, Brushes.Black, PrintXCoord, PrintYCoord)
        PrintYCoord += LineHeight

        PrintString = "Average Hours Chartered" & ControlChars.Tab & AverageHours.ToString("F1")
        e.Graphics.DrawString(PrintString, PrintFont, Brushes.Black, PrintXCoord, PrintYCoord)
        PrintYCoord += LineHeight

    End Sub

    Private Sub PrintDocument2_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument2.PrintPage
        ' Prints Yacht Types
        Dim PrintXCoord As Single = e.MarginBounds.Left
        Dim PrintYCoord As Single = e.MarginBounds.Top
        Dim PrintFont As Font = New Font("Courier New", 16)
        Dim LineHeight As Single = PrintFont.GetHeight + 2
        Dim PrintString As String
        Dim index As Integer = 0

        PrintString = "Tampa Bay Charters Yacht Types"
        e.Graphics.DrawString(PrintString, PrintFont, Brushes.Black, PrintXCoord, PrintYCoord)
        PrintYCoord += LineHeight

        PrintString = "By Tommy Bishop"
        e.Graphics.DrawString(PrintString, PrintFont, Brushes.Black, PrintXCoord, PrintYCoord)
        PrintYCoord += LineHeight * 2

        ' Loop to print out how many items are currently in the comboBox for different types
        Do Until index >= comboType.Items.Count

            PrintString = comboType.Items(index).ToString
            e.Graphics.DrawString(PrintString, PrintFont, Brushes.Black, PrintXCoord, PrintYCoord)
            PrintYCoord += LineHeight

            index += 1
        Loop
    End Sub

    Private Sub mainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim yachtDialogResult As DialogResult
        Dim yachtTypeString As String

        Try     ' When form loads, try to read the file
            Dim yachtStreamReader As StreamReader = New StreamReader("yachts.txt")
            Do Until yachtStreamReader.Peek = -1
                yachtTypeString = yachtStreamReader.ReadLine()
                comboType.Items.Add(yachtTypeString)
            Loop
            yachtStreamReader.Close()           ' When done reading close the file

        Catch ex As Exception
            yachtDialogResult = MessageBox.Show("Create a new file?", "File Not Found", MessageBoxButtons.YesNo, _
                                           MessageBoxIcon.Question)
            If yachtDialogResult = DialogResult.No Then
                Me.Close()
            End If

        End Try
    End Sub

    Private Sub SaveYachtTypeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveYachtTypeToolStripMenuItem.Click
        Dim numOfYachts As Integer

        ' Variable to Write the file to yachts.txt -- always overwrites the file
        Dim yachtStreamWriter As StreamWriter = New StreamWriter("yachts.txt", False)
        numOfYachts = comboType.Items.Count - 1

        For intIndex As Integer = 0 To numOfYachts
            yachtStreamWriter.WriteLine(comboType.Items(intIndex))
        Next intIndex

        yachtStreamWriter.Close()
        IsDirtyBoolean = False

    End Sub

    Private Sub mainForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Dim yachtDialogResult As DialogResult
        Dim MessageString As String = "Yacht Type List has changed. Do you want to save the list?"

        ' When file closes, askes the user if they want to save if they haven't already
        If IsDirtyBoolean Then
            yachtDialogResult = MessageBox.Show(MessageString, "File Changed", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If yachtDialogResult = DialogResult.Yes Then
                SaveYachtTypeToolStripMenuItem_Click(sender, e)
            End If
        End If
    End Sub
End Class
