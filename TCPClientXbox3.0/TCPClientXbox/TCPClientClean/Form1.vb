﻿
Imports Microsoft.Xna
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Input
Imports Microsoft.Xna.Framework.GamerServices
Imports Microsoft.Xna.Framework.Content


Public Class Form1

    '-------Kill Switch-----------
    Dim kills As Integer = 0 'kill switch
    Dim KillSwitch As Char = "'"

    '-------AUGER System-----------
    Dim AugerExcavate As Integer = 0
    Dim AugerExcavateMax As Integer = 0

    Dim AugerDump As Integer = 0
    Dim AugerDumpMax As Integer = 0

    Dim FullSpeedReverseAuger As Char = "6"
    Dim SpeedReverseAuger As Char = "7"
    Dim StopAuger As Char = "8"
    Dim SpeedForwardAuger As Char = "9"
    Dim FullSpeedFowardAuger As Char = "0"

    Dim AugerUp As Char = "i"
    Dim AugerDown As Char = "k"
    Dim AugerLeft As Char = "j"
    Dim AugerRight As Char = "l"

    Dim StopAugerActuators As Char = "m"

    '--------Auger Actuator Movements-----------------
    Dim LeftThumbCenter As Integer = 0
    Dim a1 As Integer = 0
    Dim a2 As Integer = 0
    Dim a6 As Integer = 0
    Dim LeftThumbUp As Integer = 0
    Dim LeftThumbLeft As Integer = 0
    Dim LeftThumbRight As Integer = 0
    Dim LeftThumbDown As Integer = 0

    '---------CONVEYOR BELT----------
    Dim ConvBeltRev As Integer = 0
    Dim ConvBeltForw As Integer = 0

    Dim ReverseConveyor As Char = "z"
    Dim StopConveyor As Char = "x"
    Dim ForwardConveyor As Char = "c"

    '--------Wheel Movements-----------------
    Dim wheelmovement1 As Integer = 0
    Dim wheelmovement2 As Integer = 0
    Dim RightThumbCenter As Integer = 0

    Dim RightThumbLeft As Integer = 0
    Dim RightThumbRight As Integer = 0

    Dim c1 As Integer = 0
    Dim c2 As Integer = 0
    Dim RightThumbUp As Integer = 0

    Dim c6 As Integer = 0
    Dim c7 As Integer = 0
    Dim RightThumbDown As Integer = 0

    Dim inc As Integer = 0
    Dim dec As Integer = 0

    Dim DecrementSpeed As Char = "1"
    Dim IncrementSpeed As Char = "2"

    Dim Forward As Char = "w" 'foward and back wheels clockwise
    Dim Backward As Char = "s" 'foward and back wheels counter clockwise
    Dim LeftTurn As Char = "a" 'left wheels are clockwise and right wheels are ccw
    Dim RightTurn As Char = "d" 'left wheels are ccw and right wheels are clockwise

    Dim StopWheels As Char = "f"

    '--------Steering Actuator Movements-----------------
    Dim ActuatorsExtract As Integer = 0
    Dim ActuatorsRetract As Integer = 0

    Dim RetractSteeringActuators As Char = "q" 'Diagonal Wheels
    Dim ExtendSteeringActuators As Char = "e" 'Straight Wheels

    Dim StopSteeringActuators As Char = "r"

    '----------Global Speed-----------
    Dim TriggerLeft As Integer = 0
    Dim TriggerRight As Integer = 0


    Private Client As TCPControl

    'Xbox 360 Controller Buttons
    '-------Triggers-----------
    'Left Trigger decreases global speed
    'Right Trigger increases global speed

    '-------Bumpers-----------
    'Left Bumper reverses (Dump) auger at high speed
    'Right Bumper forwards (Excavates) auger at high speed

    '-------Buttons-----------
    'Y button reverses (Dump) auger at normal speed
    'X button forwards (Excavates) auger at normal speed

    'B button kills everything
    'A button does nothing

    '-------Left Thumbstick-----------
    'Forward (moving stick up) moves the actuators to move auger up (retract) 
    'Backward (moving stick down) moves the actuators to move auger down (extract)
    'Left (moving stick left) moves the actuators to the left (retracts)
    'Right (moving stick right) moves the actuators to the right (extracts)
    'Center of the thumbstick stops the auger movements

    '-------Right Thumbstick-----------
    'Forward (moving stick up) rotates wheels to go forwards
    'Backward (moving stick down) roates wheels to go backwards
    'Left (moving stick left) moves the left side of the wheels counterclockwise and right side of the wheels clockwise
    'Right (moving stick right) moves the left side of the wheels clockwise and right side of the wheels counterclockwise
    'Center of the thumbstick stops the wheel movements

    '-------Dpad-----------
    'Left direction reverse conveyor so that it can dump dirt
    'Right direction forward conveyor so that it can remove dirt downward
    'Up direction retract steering actuators so that the wheels can straighten out
    'Down direciton extract steering actuators sot that the wheels can be diagonally

    Public Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Timer1.Enabled = True

        'Timer5.Enabled = True


        'ButtonLayout.Show()

    End Sub
    Public Sub augermovements(currentState As GamePadState) 'must change wheelLF to something like augermovements
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left thumbstick<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Stop Auger Movements<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left Thumbstick at the center<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState.ThumbSticks.Left.Y > -0.1F And currentState.ThumbSticks.Left.Y < 0.1F And currentState.ThumbSticks.Left.X < -0.1F And currentState.ThumbSticks.Left.X < 0.1F And LeftThumbCenter = 0 Then
            LeftThumbCenter = 1
            a1 = 0
            LeftThumbUp = 0
            a6 = 0
            LeftThumbDown = 0
            SendKeys.Send(StopAugerActuators)
            ListBox1.Items.Add(StopAugerActuators)

        ElseIf currentState.ThumbSticks.Left.Y > 0.1F Or currentState.ThumbSticks.Left.Y < -0.1F And LeftThumbCenter = 1 Then
            LeftThumbCenter = 0
        End If

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left Thumbstick Foward<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState.ThumbSticks.Left.Y > 0.5F And currentState.ThumbSticks.Left.Y < 1.5F Then
            If LeftThumbUp = 0 Then
                LeftThumbUp = 1
                SendKeys.Send(AugerUp)
                ListBox1.Items.Add(AugerUp)

            End If

        End If

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left Thumbstick Back<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState.ThumbSticks.Left.Y < -0.5F And currentState.ThumbSticks.Left.Y > -1.5F Then

            If LeftThumbDown = 0 Then
                LeftThumbDown = 1
                SendKeys.Send(AugerDown)
                ListBox1.Items.Add(AugerDown)

            End If

        End If

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left Thumbstick Left<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState.ThumbSticks.Left.X < -0.5F And currentState.ThumbSticks.Left.X > -1.5F Then

            If LeftThumbLeft = 0 Then
                LeftThumbLeft = 1
                SendKeys.Send(AugerLeft)
                ListBox1.Items.Add(AugerLeft)

            End If

        End If

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left Thumbstick Right<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState.ThumbSticks.Left.X > 0.5F And currentState.ThumbSticks.Left.X < 1.5F Then
            If LeftThumbRight = 0 Then
                LeftThumbRight = 1
                SendKeys.Send(AugerRight)
                ListBox1.Items.Add(AugerRight)

            End If

        End If

    End Sub
    Public Sub wheelmovements(currentState As GamePadState) 'must change wheelRF to something like wheelmovments
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Right thumbstick<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Stop Wheel Movements<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Right Thumbstick at the center<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState.ThumbSticks.Right.Y > -0.1F And currentState.ThumbSticks.Right.Y < 0.1F And currentState.ThumbSticks.Right.X < -0.1F And currentState.ThumbSticks.Right.X < 0.1F And RightThumbCenter = 0 Then
            RightThumbCenter = 1
            c1 = 0
            c2 = 0
            RightThumbUp = 0
            c6 = 0
            c7 = 0
            RightThumbDown = 0
            SendKeys.Send(StopWheels)
            ListBox1.Items.Add(StopWheels)

        ElseIf currentState.ThumbSticks.Right.Y > 0.1F Or currentState.ThumbSticks.Right.Y < -0.1F And RightThumbCenter = 1 Then
            RightThumbCenter = 0
        End If

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Right Thumbstick Foward<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState.ThumbSticks.Right.Y > 0.5F And currentState.ThumbSticks.Right.Y < 1.5F Then

            If RightThumbUp = 0 Then
                RightThumbUp = 1
                SendKeys.Send(Forward)
                ListBox1.Items.Add(Forward)

            End If

        End If


        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Right Thumbstick Backward<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState.ThumbSticks.Right.Y < -0.5F And currentState.ThumbSticks.Right.Y > -1.5F Then

            If RightThumbDown = 0 Then
                RightThumbDown = 1
                SendKeys.Send(Backward)
                ListBox1.Items.Add(Backward)

            End If
        End If

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Right Thumbstick Left<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState.ThumbSticks.Right.X < -0.5F And currentState.ThumbSticks.Right.X > -1.5F Then

            If RightThumbLeft = 0 Then
                RightThumbLeft = 1
                SendKeys.Send(LeftTurn)
                ListBox1.Items.Add(LeftTurn)

            End If
        End If

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Right Thumbstick Right<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState.ThumbSticks.Right.X > 0.5F And currentState.ThumbSticks.Right.X < 1.5F Then

            If RightThumbRight = 0 Then
                RightThumbRight = 1
                SendKeys.Send(RightTurn)
                ListBox1.Items.Add(RightTurn)

            End If

        End If

    End Sub
    Public Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim currentState As GamePadState = GamePad.GetState(PlayerIndex.One)
        'Dim currentState2 As GamePadState = GamePad.GetState(PlayerIndex.Two)
        buttons(currentState)
        dpad(currentState)
        bumpers(currentState)
        trigger(currentState)
        'If currentState2.IsConnected Then

        '    If currentState2.Buttons.B = ButtonState.Pressed And kills2 = 0 Then
        '        kills2 = 1
        '        SendKeys.Send(KillSwitch)

        '    End If
        '    If currentState2.Buttons.B = ButtonState.Released And kills2 = 1 Then
        '        kills2 = 0

        '    End If
        'End If
    End Sub
    Public Sub buttons(currentState As GamePadState) 'I created this public sub, Not sure if it is done correctly

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Auger SLOW Dumping and Excavating<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Y Button Pressed<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '---------AUGER REVERSE (Dumping the dirt up high at Low Speed)-------------
        If currentState.Buttons.Y = ButtonState.Pressed And AugerDump = 0 Then
            AugerDump = 1
            SendKeys.Send(SpeedReverseAuger)
            ListBox1.Items.Add(SpeedReverseAuger)
            'MsgBox("Im Awesome")
        End If
        If currentState.Buttons.Y = ButtonState.Released And AugerDump = 1 Then
            AugerDump = 0
            SendKeys.Send(StopAuger)
            ListBox1.Items.Add(StopAuger)

        End If

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<X Button Pressed<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '---------AUGER EXCAVATING (Excavating the dirt up down low at Slow Speed)-------------
        If currentState.Buttons.X = ButtonState.Pressed And AugerExcavate = 0 Then
            AugerExcavate = 1
            SendKeys.Send(SpeedForwardAuger)
            ListBox1.Items.Add(SpeedForwardAuger)

        End If
        If currentState.Buttons.X = ButtonState.Released And AugerExcavate = 1 Then
            AugerExcavate = 0
            SendKeys.Send(StopAuger)
            ListBox1.Items.Add(StopAuger)

        End If


        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Kill Switch<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<B Button Pressed<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState.Buttons.B = ButtonState.Pressed And kills = 0 Then
            kills = 1
            SendKeys.Send(KillSwitch)
            ListBox1.Items.Add(KillSwitch)

        End If
        If currentState.Buttons.B = ButtonState.Released And kills = 1 Then
            kills = 0
        End If

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Steering Actuators<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<A Button Pressed<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

    End Sub
    Public Sub dpad(currentState As GamePadState)

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Conveyor Belt<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Directional Pad Left and Right<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Directional Pad Left<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState.DPad.Left = ButtonState.Pressed And ConvBeltRev = 0 Then 'Auger Recolect
            ConvBeltRev = 1
            SendKeys.Send(ReverseConveyor)
            ListBox1.Items.Add(ReverseConveyor)

        End If

        If currentState.DPad.Left = ButtonState.Released And ConvBeltRev = 1 Then 'Auger Recolect
            SendKeys.Send(StopConveyor)
            ListBox1.Items.Add(StopConveyor)
            ConvBeltRev = 0
        End If

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Directional Pad Right<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState.DPad.Right = ButtonState.Pressed And ConvBeltForw = 0 Then 'Auger Reverse
            ConvBeltForw = 1
            SendKeys.Send(ForwardConveyor)
            ListBox1.Items.Add(ForwardConveyor)
        End If

        If currentState.DPad.Right = ButtonState.Released And ConvBeltForw = 1 Then 'Auger Reverse

            SendKeys.Send(StopConveyor)
            ListBox1.Items.Add(StopConveyor)
            ConvBeltForw = 0

        End If


        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Steering Actuators<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Directional Pad Up and Down<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Directional Pad Down<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState.DPad.Down = ButtonState.Pressed And ActuatorsRetract = 0 Then
            ActuatorsRetract = 1
            SendKeys.Send(RetractSteeringActuators)
            ListBox1.Items.Add(RetractSteeringActuators)

        End If

        If currentState.DPad.Down = ButtonState.Released And ActuatorsRetract = 1 Then
            ActuatorsRetract = 0
            SendKeys.Send(StopSteeringActuators)
            ListBox1.Items.Add(StopSteeringActuators)
        End If

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Directional Pad Up<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState.DPad.Up = ButtonState.Pressed And ActuatorsExtract = 0 Then
            ActuatorsExtract = 1
            SendKeys.Send(ExtendSteeringActuators)
            ListBox1.Items.Add(ExtendSteeringActuators)

        End If

        If currentState.DPad.Up = ButtonState.Released And ActuatorsExtract = 1 Then
            ActuatorsExtract = 0
            SendKeys.Send(StopSteeringActuators)
            ListBox1.Items.Add(StopSteeringActuators)

        End If
    End Sub
    Public Sub bumpers(currentState As GamePadState)

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Auger MAX Speed<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left and Right Bumpers<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left Bumper Pressed<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<MAX DUMP Speed<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState.Buttons.LeftShoulder = ButtonState.Pressed And AugerDumpMax = 0 Then

            AugerDumpMax = 1
            SendKeys.Send(FullSpeedReverseAuger)
            ListBox1.Items.Add(FullSpeedReverseAuger)

        End If

        If currentState.Buttons.LeftShoulder = ButtonState.Released And AugerDumpMax = 1 Then
            AugerDumpMax = 0
            SendKeys.Send(StopAuger)
            ListBox1.Items.Add(StopAuger)
        End If

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Right Bumper Pressed<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<MAX Excavate Speed<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState.Buttons.RightShoulder = ButtonState.Pressed And AugerExcavateMax = 0 Then

            AugerExcavateMax = 1
            SendKeys.Send(FullSpeedFowardAuger)
            ListBox1.Items.Add(FullSpeedFowardAuger)

        End If

        If currentState.Buttons.RightShoulder = ButtonState.Released And AugerExcavateMax = 1 Then
            AugerExcavateMax = 0
            SendKeys.Send(StopAuger)
            ListBox1.Items.Add(StopAuger)
        End If

    End Sub
    Public Sub trigger(currentState As GamePadState)

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Increment and Decrement Global Speeds<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left and Right Triggers<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left Trigger Pressed<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Decrease Global Speed<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState.Triggers.Left = ButtonState.Pressed And TriggerLeft = 0 Then

            TriggerLeft = 1
            SendKeys.Send(DecrementSpeed)
            ListBox1.Items.Add(DecrementSpeed)


            If currentState.ThumbSticks.Right.Y > 0.5F And currentState.ThumbSticks.Right.Y < 1.5F Then

                SendKeys.Send(Forward)
                ListBox1.Items.Add(Forward)

            End If

            If currentState.ThumbSticks.Right.Y < -0.5F And currentState.ThumbSticks.Right.Y > -1.5F Then

                SendKeys.Send(Backward)
                ListBox1.Items.Add(Backward)
            End If

            If currentState.ThumbSticks.Right.X > 0.5F And currentState.ThumbSticks.Right.X < 1.5F Then

                SendKeys.Send(RightTurn)
                ListBox1.Items.Add(RightTurn)
            End If

            If currentState.ThumbSticks.Right.X < -0.5F And currentState.ThumbSticks.Right.X > -1.5F Then

                SendKeys.Send(LeftTurn)
                ListBox1.Items.Add(LeftTurn)

            End If

        End If

        If currentState.Triggers.Left = ButtonState.Released And TriggerLeft = 1 Then
            TriggerLeft = 0
        End If

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Right Trigger Pressed<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Increase Global Speed<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState.Triggers.Right = ButtonState.Pressed And TriggerRight = 0 Then

            TriggerRight = 1
            SendKeys.Send(IncrementSpeed)
            ListBox1.Items.Add(IncrementSpeed)

            If currentState.ThumbSticks.Right.Y > 0.5F And currentState.ThumbSticks.Right.Y < 1.5F Then

                SendKeys.Send(Forward)
                ListBox1.Items.Add(Forward)
            End If

            If currentState.ThumbSticks.Right.Y < -0.5F And currentState.ThumbSticks.Right.Y > -1.5F Then

                SendKeys.Send(Backward)
                ListBox1.Items.Add(Backward)
            End If

            If currentState.ThumbSticks.Right.X > 0.5F And currentState.ThumbSticks.Right.X < 1.5F Then

                SendKeys.Send(RightTurn)
                ListBox1.Items.Add(RightTurn)
            End If

            If currentState.ThumbSticks.Right.X < -0.5F And currentState.ThumbSticks.Right.X > -1.5F Then

                SendKeys.Send(LeftTurn)
                ListBox1.Items.Add(LeftTurn)
            End If

        End If

        If currentState.Triggers.Right = ButtonState.Released And TriggerRight = 1 Then
            TriggerRight = 0
        End If


        ''''''''''' All of this is commented out because i wouldnt know if it might be needed or if we can use it.
        ''''''''''' If applied again, the variables must be declared
        'If currentState.Buttons.LeftStick = ButtonState.Pressed And inc = 0 Then 'Increment Speed
        'inc = 1
        'SendKeys.Send("<")
        '
        'End If
        'If currentState.Buttons.LeftStick = ButtonState.Released And inc = 1 Then 'Increment Speed
        'inc = 0
        '
        'End If
        '
        'If currentState.Buttons.RightStick = ButtonState.Pressed And dec = 0 Then 'Decrement Speed
        'dec = 1
        'SendKeys.Send("]")
        '
        'End If
        'If currentState.Buttons.RightStick = ButtonState.Released And dec = 1 Then 'Decrement Speed
        'dec = 0
        '
        'End If
        '
        '---------LEFT FRONT------------------------------------------------
        'If currentState.Buttons.LeftShoulder = ButtonState.Pressed Then
        'If currentState.ThumbSticks.Left.Y > 0.5F And currentState.ThumbSticks.Left.Y < 1.5F And z1 = 0 Then
        'SendKeys.Send(reactivateLFF)
        'End If
        '
        'z1 = 1
        'wheelLF(currentState)
        '
        'End If
        '
        'If currentState.Buttons.LeftShoulder = ButtonState.Released And z1 = 1 Then
        '
        'z1 = 0
        'SendKeys.Send(stopL)
        'End If
        '-----LEFT FRONT
        '
        '----------LEFT BACK
        '
        '-------RIGHT FRONT----------
        'If currentState.Buttons.RightShoulder = ButtonState.Pressed Then
        'If currentState.ThumbSticks.Right.Y > 0.5F And currentState.ThumbSticks.Right.Y < 1.5F And z3 = 0 Then
        'SendKeys.Send(reactivateRF)
        'End If
        'z3 = 1
        '
        'wheelRF(currentState)
        '
        'End If
        '
        'If currentState.Buttons.RightShoulder = ButtonState.Released And z3 = 1 Then
        '
        'z3 = 0
        '
        'SendKeys.Send(stopR)
        'End If
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click

    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub
End Class
