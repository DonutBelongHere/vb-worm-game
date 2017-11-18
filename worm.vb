Public Class Form1

    Dim score As Integer = 0
    Dim score2 As Integer = 0
    Dim time As Integer = 0
    Dim foodxvalue As Integer
    Dim foodyvalue As Integer
    Dim snakeup As Boolean = False
    Dim snakedown As Boolean = False
    Dim snakeleft As Boolean = False
    Dim snakeright As Boolean = True
    Dim bodyextra As Integer = 5
    Dim snakebody(200) As PictureBox

    'Dim snakeup2 As Boolean = False
    'Dim snakedown2 As Boolean = False
    'Dim snakeleft2 As Boolean = False
    'Dim snakeright2 As Boolean = True
    'Dim bodyextra2 As Integer = 5
    'Dim snakebody2(200) As PictureBox

    Dim pause As Boolean = False
    Dim bHandled As Boolean = False
    Dim x As Integer

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Begin()
        TimerTime.Enabled = True
        TimerMoveSnake.Enabled = True
    End Sub

    Private Sub Begin()
        SnakeFood.Visible = True
        SnakeHead.Visible = True
        Button1.Visible = False
        For Me.x = 1 To bodyextra
            snakebody(x) = New PictureBox()
            snakebody(x).Image = My.Resources.SnakeBody
            snakebody(x).Width = 15
            snakebody(x).Height = 15
            snakebody(x).BackColor = Color.Transparent
            snakebody(x).Left = 120 - (15 * (x - 1))
            snakebody(x).Top = 180
            snakebody(x).SizeMode = PictureBoxSizeMode.StretchImage
            Controls.Add(snakebody(x))
        Next
        'SnakeHead2.Visible = True
        'For Me.x = 1 To bodyextra2
        '    snakebody2(x) = New PictureBox()
        '    snakebody2(x).Image = My.Resources.SnakeBody2
        '    snakebody2(x).Width = 15
        '    snakebody2(x).Height = 15
        '    snakebody2(x).BackColor = Color.Transparent
        '    snakebody2(x).Left = 120 - (15 * (x - 1))
        '    snakebody2(x).Top = 120
        '    snakebody2(x).SizeMode = PictureBoxSizeMode.StretchImage
        '    Controls.Add(snakebody2(x))
        'Next
    End Sub

    Private Sub NewFood()
        Randomize()
        foodyvalue = Rnd() * 23
        foodxvalue = Rnd() * 23
        SnakeFood.Location = New Point(foodxvalue * 15, foodyvalue * 15)
    End Sub

    Private Sub TimerTime_Tick(sender As Object, e As EventArgs) Handles TimerTime.Tick
        time += 1
    End Sub

    Private Sub TimerMoveSnake_Tick(sender As Object, e As EventArgs) Handles TimerMoveSnake.Tick
        MoveSnake()
        Me.Text = "P1: " & score & "P2: " & score2 & " - Time: " & time & " - Press P to Pause"
    End Sub

    Private Sub MoveSnake()
        snakebody(1).Location = SnakeHead.Location
        If snakeup = True Then
            SnakeHead.Top = SnakeHead.Top - 15
        End If
        If snakedown = True Then
            SnakeHead.Top = SnakeHead.Top + 15
        End If
        If snakeright = True Then
            SnakeHead.Left = SnakeHead.Left + 15
        End If
        If snakeleft = True Then
            SnakeHead.Left = SnakeHead.Left - 15
        End If
        x = bodyextra
        Do Until snakebody(2).Location = snakebody(1).Location
            snakebody(x).Location = snakebody(x - 1).Location
            x = x - 1
        Loop
        CheckForFood()
        snakebody(bodyextra).Visible = True
        CheckForCollision()

        'snakebody2(1).Location = SnakeHead2.Location
        'If snakeup2 = True Then
        '    SnakeHead2.Top = SnakeHead2.Top - 15
        'End If
        'If snakedown2 = True Then
        '    SnakeHead2.Top = SnakeHead2.Top + 15
        'End If
        'If snakeright2 = True Then
        '    SnakeHead2.Left = SnakeHead2.Left + 15
        'End If
        'If snakeleft2 = True Then
        '    SnakeHead2.Left = SnakeHead2.Left - 15
        'End If
        'x = bodyextra2
        'Do Until snakebody2(2).Location = snakebody2(1).Location
        '    snakebody2(x).Location = snakebody2(x - 1).Location
        '    x = x - 1
        'Loop
        'CheckForFood()
        'snakebody2(bodyextra2).Visible = True
        'CheckForCollision()
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
        Select Case keyData
            Case Keys.Up And snakedown = False
                snakeup = True
                snakedown = False
                snakeleft = False
                snakeright = False
                bHandled = True
            Case Keys.Down And snakeup = False
                snakeup = False
                snakedown = True
                snakeleft = False
                snakeright = False
                bHandled = True
            Case Keys.Left And snakeright = False
                snakeup = False
                snakedown = False
                snakeleft = True
                snakeright = False
                bHandled = True
            Case Keys.Right And snakeleft = False
                snakeup = False
                snakedown = False
                snakeleft = False
                snakeright = True
                bHandled = True
                'Case Keys.W And snakedown2 = False
                '    snakeup2 = True
                '    snakedown2 = False
                '    snakeleft2 = False
                '    snakeright2 = False
                '    bHandled = True
                'Case Keys.S And snakeup2 = False
                '    snakeup2 = False
                '    snakedown2 = True
                '    snakeleft2 = False
                '    snakeright2 = False
                '    bHandled = True
                'Case Keys.A And snakeright2 = False
                '    snakeup2 = False
                '    snakedown2 = False
                '    snakeleft2 = True
                '    snakeright2 = False
                '    bHandled = True
                'Case Keys.D And snakeleft2 = False
                '    snakeup2 = False
                '    snakedown2 = False
                '    snakeleft2 = False
                '    snakeright2 = True
                'bHandled = True
            Case Keys.P And pause = False
                pause = True
                Label1.Visible = True
                TimerTime.Enabled = False
                TimerMoveSnake.Enabled = False
                Me.Text = "P1: " & score & "P2: " & score2 & " - Time: " & time & " - Press R to Resume"
            Case Keys.R And pause = True
                pause = False
                Label1.Visible = False
                TimerTime.Enabled = True
                TimerMoveSnake.Enabled = True
        End Select
        Return bHandled
    End Function

    Private Sub CheckForFood()
        If SnakeHead.Location = SnakeFood.Location Then
            FoodEaten()
            'ElseIf SnakeHead2.Location = SnakeFood.Location Then
            '    FoodEaten2()
        End If
    End Sub

    Private Sub FoodEaten()
        NewFood()
        score = score + 1
        bodyextra = bodyextra + 1
        snakebody(bodyextra) = New PictureBox()
        snakebody(bodyextra).Image = My.Resources.SnakeBody
        snakebody(bodyextra).Width = 15
        snakebody(bodyextra).Height = 15
        snakebody(bodyextra).BackColor = Color.Transparent
        snakebody(bodyextra).SizeMode = PictureBoxSizeMode.StretchImage
        snakebody(bodyextra).Visible = False
        Controls.Add(snakebody(bodyextra))
    End Sub

    'Private Sub FoodEaten2()
    '    NewFood()
    '    score2 = score2 + 1
    '    bodyextra2 = bodyextra2 + 1
    '    snakebody2(bodyextra2) = New PictureBox()
    '    snakebody2(bodyextra2).Image = My.Resources.SnakeBody2
    '    snakebody2(bodyextra2).Width = 15
    '    snakebody2(bodyextra2).Height = 15
    '    snakebody2(bodyextra2).BackColor = Color.Transparent
    '    snakebody2(bodyextra2).SizeMode = PictureBoxSizeMode.StretchImage
    '    snakebody2(bodyextra2).Visible = False
    '    Controls.Add(snakebody2(bodyextra2))
    'End Sub

    Private Sub CheckForCollision()
        For Me.x = 1 To bodyextra
            If snakebody(x).Location = SnakeHead.Location Then
                BodyHit()
            End If
            If SnakeHead.Left <= 0 Then
                SnakeHead.Left = SnakeHead.Left + Me.Width
            ElseIf SnakeHead.Top <= 0 Then
                SnakeHead.Top = SnakeHead.Top + Me.Height
            ElseIf SnakeHead.Top + SnakeHead.Height >= Me.Height Then
                SnakeHead.Top = SnakeHead.Top - Me.Height
            ElseIf SnakeHead.Left + SnakeHead.Width >= Me.Width Then
                SnakeHead.Left = SnakeHead.Left - Me.Width
            ElseIf SnakeHead2.Left <= 0 Then
                SnakeHead2.Left = SnakeHead2.Left + Me.Width
            ElseIf SnakeHead2.Top <= 0 Then
                SnakeHead2.Top = SnakeHead2.Top + Me.Height
            ElseIf SnakeHead2.Top + SnakeHead2.Height >= Me.Height Then
                SnakeHead2.Top = SnakeHead2.Top - Me.Height
            ElseIf SnakeHead2.Left + SnakeHead2.Width >= Me.Width Then
                SnakeHead2.Left = SnakeHead2.Left - Me.Width
            End If
        Next
    End Sub

    Private Sub BodyHit()
        TimerTime.Enabled = False
        TimerMoveSnake.Enabled = False
        MessageBox.Show("Game over." & vbNewLine & "P1 score: " & score & vbNewLine & "P2 score: " & score2 & vbNewLine & "Time: " & time)
        End
    End Sub
End Class
