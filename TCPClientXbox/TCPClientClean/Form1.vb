

Imports Microsoft.Xna
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Input
Imports Microsoft.Xna.Framework.GamerServices
Imports Microsoft.Xna.Framework.Content

'Xbox 360 Controller Buttons

'A button is not being used
'Back buttons is not being used
'Start button is not being used

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

Public Class Form1

    '-------Kill Switch-----------
    Dim kills As Integer = 0 'kill switch
    Dim kills2 As Integer = 0 'kill2 switch
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
    
    '---------PanTilt Base---------- 
    Dim tiltUp as Char = "y"
    Dim tiltDown as Char = "t"
    Dim panLeft as Char = "g"
    Dim panRight as Char = "h"
    Dim resetAll as Char = "b"
    
    Dim tilt_Up As Integer = 0
    Dim tilt_Down As Integer = 0
    Dim pan_Left As Integer = 0
    Dim pan_Right As Integer = 0
    Dim reset_All As Integer = 0
    '---------end of PanTilt Base----------
    

    '--------Auger Actuator Movements-----------------
    Dim RightThumbCenter2 As Integer = 0
    Dim a1 As Integer = 0
    Dim a2 As Integer = 0
    Dim a6 As Integer = 0
    Dim RightThumbUp2 As Integer = 0
    Dim RightThumbLeft2 As Integer = 0
    Dim RightThumbRight2 As Integer = 0
    Dim RightThumbDown2 As Integer = 0

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

    Public Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Timer1.Enabled = True

        'Timer5.Enabled = True


        'ButtonLayout.Show()

    End Sub

    Public Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim currentState As GamePadState = GamePad.GetState(PlayerIndex.One)
        Dim currentState2 As GamePadState = GamePad.GetState(PlayerIndex.Two)
        augermovements(currentState2)
        wheelmovements(currentState)
        buttons(currentState)
        buttons2(currentState2)
        dpad(currentState)
        dpad2(currentState2)
        triggers(currentState)
        triggers2(currentState2)
        camera(currentState)
        ' Commented out by lisa because it is in buttons2 function
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

    Public Sub augermovements(currentState2 As GamePadState) 'must change wheelLF to something like augermovements
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left thumbstick<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Stop Auger Movements<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Right Thumbstick at the center<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState2.ThumbSticks.Right.Y > -0.1F And currentState2.ThumbSticks.Right.Y < 0.1F And currentState2.ThumbSticks.Right.X > -0.1F And currentState2.ThumbSticks.Right.X < 0.1F And RightThumbCenter2 = 0 Then
            RightThumbCenter2 = 1
            RightThumbUp2 = 0
            RightThumbDown2 = 0
            RightThumbLeft2 = 0
            RightThumbRight2 = 0

            a1 = 0
            a6 = 0

            SendKeys.Send(StopAugerActuators)
            ListBox2.Items.Add(StopAugerActuators)

        ElseIf currentState2.ThumbSticks.Right.Y > 0.1F Or currentState2.ThumbSticks.Right.Y < -0.1F Or currentState2.ThumbSticks.Right.X > 0.1F Or currentState2.ThumbSticks.Right.X < -0.1F And RightThumbCenter2 = 1 Then
            RightThumbCenter2 = 0
        End If

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Right Thumbstick Foward<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState2.ThumbSticks.Right.Y > 0.5F And currentState2.ThumbSticks.Right.Y < 1.5F Then
            If RightThumbUp2 = 0 Then
                RightThumbUp2 = 1
                SendKeys.Send(AugerUp)
                ListBox2.Items.Add(AugerUp)

            End If

        End If

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left Thumbstick Backward<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState2.ThumbSticks.Right.Y < -0.5F And currentState2.ThumbSticks.Right.Y > -1.5F Then

            If RightThumbDown2 = 0 Then
                RightThumbDown2 = 1
                SendKeys.Send(AugerDown)
                ListBox2.Items.Add(AugerDown)

            End If

        End If

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Right Thumbstick Left<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState2.ThumbSticks.Right.X < -0.5F And currentState2.ThumbSticks.Right.X > -1.5F Then

            If RightThumbLeft2 = 0 Then
                RightThumbLeft2 = 1
                SendKeys.Send(AugerLeft)
                ListBox2.Items.Add(AugerLeft)

            End If

        End If

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Right Thumbstick Right<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState2.ThumbSticks.Right.X > 0.5F And currentState2.ThumbSticks.Right.X < 1.5F Then
            If RightThumbRight2 = 0 Then
                RightThumbRight2 = 1
                SendKeys.Send(AugerRight)
                ListBox2.Items.Add(AugerRight)

            End If

        End If

    End Sub

    Public Sub wheelmovements(currentState As GamePadState) 'must change wheelRF to something like wheelmovments
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Right thumbstick<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Stop Wheel Movements<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Right Thumbstick at the center<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState.ThumbSticks.Right.Y > -0.1F And currentState.ThumbSticks.Right.Y < 0.1F And currentState.ThumbSticks.Right.X > -0.1F And currentState.ThumbSticks.Right.X < 0.1F And RightThumbCenter = 0 Then
            RightThumbCenter = 1
            RightThumbUp = 0
            RightThumbDown = 0
            RightThumbLeft = 0
            RightThumbRight = 0
            c1 = 0
            c2 = 0
            c6 = 0
            c7 = 0

            SendKeys.Send(StopWheels)
            ListBox1.Items.Add(StopWheels)

        ElseIf currentState.ThumbSticks.Right.Y > 0.1F Or currentState.ThumbSticks.Right.Y < -0.1F Or currentState.ThumbSticks.Right.X > 0.1F Or currentState.ThumbSticks.Right.X < -0.1F And RightThumbCenter = 1 Then
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
    
    Public Sub camera(currentState As GamePadState) 'must change wheelLF to something like augermovements
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left thumbstick<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Reset Camera Position<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<left Thumbstick at the center<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState.ThumbSticks.Left.Y > -0.1F And currentState2.ThumbSticks.Left.Y < 0.1F And currentState.ThumbSticks.Left.X > -0.1F And currentState.ThumbSticks.Left.X < 0.1F And reset_All = 0 Then
            reset_All = 1
            tilt_Up = 0
            tilt_Down = 0
            pan_Left = 0
            pan_Right = 0

            SendKeys.Send(resetAll)
            ListBox.Items.Add(resetAll)

        ElseIf currentState.ThumbSticks.Left.Y > 0.1F Or currentState.ThumbSticks.Left.Y < -0.1F Or currentState.ThumbSticks.Left.X > 0.1F Or currentState.ThumbSticks.Left.X < -0.1F And reset_All = 1 Then
            reset_All = 0
        End If

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left Thumbstick Foward<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState.ThumbSticks.Left.Y > 0.5F And currentState.ThumbSticks.Left.Y < 1.5F Then
            If tilt_Up = 0 Then
                tilt_Up = 1
                SendKeys.Send(tiltUp)
                ListBox.Items.Add(tiltUp)

            End If

        End If

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left Thumbstick Backward<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState.ThumbSticks.Left.Y < -0.5F And currentState.ThumbSticks.Left.Y > -1.5F Then

            If tilt_Down = 0 Then
                tilt_down = 1
                SendKeys.Send(tiltDown)
                ListBox.Items.Add(tiltDown)

            End If

        End If

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left Thumbstick Left<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState.ThumbSticks.Left.X < -0.5F And currentState.ThumbSticks.Left.X > -1.5F Then

            If pan_Left = 0 Then
                pan_Left = 1
                SendKeys.Send(panLeft)
                ListBox.Items.Add(panLeft)

            End If

        End If

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left Thumbstick Right<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState.ThumbSticks.Left.X > 0.5F And currentState.ThumbSticks.Left.X < 1.5F Then
            If pan_Right = 0 Then
                pan_Right = 1
                SendKeys.Send(panRight)
                ListBox.Items.Add(panRight)

            End If

        End If

    End Sub

    Public Sub buttons(currentState As GamePadState) 'Alex created this public sub, Not sure if it is done correctly


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



    End Sub
    
    

    Public Sub buttons2(currentState2 As GamePadState)

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Auger SLOW Dumping and Excavating<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Y Button Pressed<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '---------AUGER REVERSE (Dumping the dirt up high at Low Speed)-------------
        If currentState2.Buttons.Y = ButtonState.Pressed And AugerDump = 0 Then
            AugerDump = 1
            SendKeys.Send(SpeedReverseAuger)
            ListBox2.Items.Add(SpeedReverseAuger)
            'MsgBox("Im Awesome")
        End If
        If currentState2.Buttons.Y = ButtonState.Released And AugerDump = 1 Then
            AugerDump = 0
            SendKeys.Send(StopAuger)
            ListBox2.Items.Add(StopAuger)

        End If

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<X Button Pressed<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '---------AUGER EXCAVATING (Excavating the dirt up down low at Slow Speed)-------------
        If currentState2.Buttons.A = ButtonState.Pressed And AugerExcavate = 0 Then
            AugerExcavate = 1
            SendKeys.Send(SpeedForwardAuger)
            ListBox2.Items.Add(SpeedForwardAuger)

        End If
        If currentState2.Buttons.A = ButtonState.Released And AugerExcavate = 1 Then
            AugerExcavate = 0
            SendKeys.Send(StopAuger)
            ListBox2.Items.Add(StopAuger)

        End If


        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Kill Switch<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<B Button Pressed<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState2.Buttons.B = ButtonState.Pressed And kills2 = 0 Then
            kills2 = 1
            SendKeys.Send(KillSwitch)
            ListBox2.Items.Add(KillSwitch)

        End If
        If currentState2.Buttons.B = ButtonState.Released And kills2 = 1 Then
            kills2 = 0
        End If


    End Sub

    Public Sub dpad(currentState As GamePadState)


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

    Public Sub dpad2(currentState2 As GamePadState)

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Conveyor Belt<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Directional Pad Left and Right<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Directional Pad Left<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState2.DPad.Down = ButtonState.Pressed And ConvBeltRev = 0 Then 'Auger Recolect
            ConvBeltRev = 1
            SendKeys.Send(ReverseConveyor)
            ListBox2.Items.Add(ReverseConveyor)

        End If

        If currentState2.DPad.Down = ButtonState.Released And ConvBeltRev = 1 Then 'Auger Recolect
            SendKeys.Send(StopConveyor)
            ListBox2.Items.Add(StopConveyor)
            ConvBeltRev = 0
        End If

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Directional Pad Right<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState2.DPad.Up = ButtonState.Pressed And ConvBeltForw = 0 Then 'Auger Reverse
            ConvBeltForw = 1
            SendKeys.Send(ForwardConveyor)
            ListBox2.Items.Add(ForwardConveyor)
        End If

        If currentState2.DPad.Up = ButtonState.Released And ConvBeltForw = 1 Then 'Auger Reverse

            SendKeys.Send(StopConveyor)
            ListBox2.Items.Add(StopConveyor)
            ConvBeltForw = 0

        End If


    End Sub

    Public Sub triggers(currentState As GamePadState)

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

    Public Sub triggers2(currentState2 As GamePadState)
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Auger MAX Speed<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left and Right Bumpers<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left Bumper Pressed<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<MAX DUMP Speed<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState2.Triggers.Left = ButtonState.Pressed And AugerDumpMax = 0 Then

            AugerDumpMax = 1
            SendKeys.Send(FullSpeedReverseAuger)
            ListBox2.Items.Add(FullSpeedReverseAuger)

        End If

        If currentState2.Triggers.Left = ButtonState.Released And AugerDumpMax = 1 Then
            AugerDumpMax = 0
            SendKeys.Send(StopAuger)
            ListBox2.Items.Add(StopAuger)
        End If

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Right Bumper Pressed<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<MAX Excavate Speed<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState2.Triggers.Right = ButtonState.Pressed And AugerExcavateMax = 0 Then

            AugerExcavateMax = 1
            SendKeys.Send(FullSpeedFowardAuger)
            ListBox2.Items.Add(FullSpeedFowardAuger)

        End If

        If currentState2.Triggers.Right = ButtonState.Released And AugerExcavateMax = 1 Then
            AugerExcavateMax = 0
            SendKeys.Send(StopAuger)
            ListBox2.Items.Add(StopAuger)
        End If

    End Sub


End Class
