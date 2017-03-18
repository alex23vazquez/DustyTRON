Public Class ButtonLayout



    Private Sub ButtonLayout_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub LeftTBox_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub PlayersBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PlayersBox.SelectedIndexChanged



        If PlayersBox.Text = "Player1" Then
            LT.Text = "Left"
            RT.Text = "Right"
            ABut.Text = "Forward"
            XBut.Text = "Backward"
            Bbut.Text = "Kill Switch"
            LB.Text = "Decrease Speed"
            RB.Text = "Increase Speed"
        End If

        If PlayersBox.Text = "Player2" Then
            LT.Text = "Tilt Auger Down"
            RT.Text = "Tilt Auger Up"
            ABut.Text = "Auger CW"
            XBut.Text = "Auger CCW"
            Bbut.Text = "Kill Switch"
            LB.Text = "Decrease Auger speed"
            RB.Text = "Increase Auger speed"
        End If
        'LT.Enabled = False
        'RT.Enabled = False
        'LB.Enabled = False
        'RB.Enabled = False
        'Sel.Enabled = False
        'StartTB.Enabled = False
        'YBut.Enabled = False
        'Bbut.Enabled = False
        'XBut.Enabled = False
        'ABut.Enabled = False
        'UP.Enabled = False
        'LeftB.Enabled = False
        'RightB.Enabled = False
        'Down.Enabled = False
        'LeftStD.Enabled = False
        'LeftStB.Enabled = False
        'RightStD.Enabled = False
        'RightStB.Enabled = False

    End Sub

    Private Sub RightStB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RightStB.SelectedIndexChanged

    End Sub

    Private Sub Center_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Center.SelectedIndexChanged

    End Sub

    Private Sub RT_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RT.SelectedIndexChanged

    End Sub

    Private Sub LT_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LT.SelectedIndexChanged

    End Sub
End Class