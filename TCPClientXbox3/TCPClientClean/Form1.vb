

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
	Dim RightThumbCenter2 As Integer = 0
	Dim AugerStopExc As Char = "i"

	Dim RightThumbDown2 As Integer = 0
	Dim RightThumbUp2 As Integer = 0
	Dim AugerExcavate As Char = "l"
	Dim AugerRevExcavate As Char = "k"

	'Dim RightThumbLeft2 As Integer = 0
	'Dim RightThumbRight2 As Integer = 0

	Dim TriggerRight2 As Integer = 0
	Dim TriggerLeft2 As Integer = 0
	Dim DecrementSpeedAuger As Char = "6"
	Dim IncrementSpeedAuger As Char = "7"

	Dim StopAuger_Actuators As Integer = 0

	Dim DirectionalUp2 As Integer = 0
	Dim DirectionalDown2 As Integer = 0
	Dim AugerUp As Char = "u"
	Dim AugerDown As Char = "j"
	Dim StopAugerActuators As Char = "y"

	'---------PanTilt Camera Front of Robot---------- 
	Dim tiltUp As Char = "-"
	Dim tiltDown As Char = "["
	Dim panLeft As Char = "p"
	Dim panRight As Char = "]"
	Dim resetAll As Char = "0"

	Dim tilt_Up As Integer = 0
	Dim tilt_Down As Integer = 0
	Dim pan_Left As Integer = 0
	Dim pan_Right As Integer = 0
	Dim reset_All As Integer = 0
	'---------end of PanTilt Base----------

	'---------PanTilt Camera Back of Robot---------- 
	'Dim tiltUp2 As Char = "-"
	'Dim tiltDown2 As Char = "["
	'Dim panLeft2 As Char = "p"
	'Dim panRight2 As Char = "]"
	'Dim resetAll2 As Char = "0"

	'Dim tilt_Up2 As Integer = 0
	'Dim tilt_Down2 As Integer = 0
	'Dim pan_Left2 As Integer = 0
	'Dim pan_Right2 As Integer = 0
	'Dim reset_All2 As Integer = 0
	'---------end of PanTilt Base----------


	'---------CONVEYOR BELT----------
	Dim DirectionalLeft2 As Integer = 0
	Dim DirectionalRight2 As Integer = 0

	Dim ReverseConveyor As Char = "b"
	Dim StopConveyor As Char = "n"
	Dim ForwardConveyor As Char = "m"

	'---------Robot Lifting Actuators----------
	Dim DirectionalUp As Integer = 0
	Dim DirectionalDown As Integer = 0
	Dim Robot_Stop As Integer = 0

	Dim RobotUp As Char = "e"
	Dim RobotDown As Char = "r"
	Dim RobotStop As Char = "f"

	'--------Wheel Movements-----------------
	Dim wheelmovement1 As Integer = 0
    Dim wheelmovement2 As Integer = 0
    Dim RightThumbCenter As Integer = 0

    Dim RightThumbLeft As Integer = 0
	Dim RightThumbRight As Integer = 0
	Dim RightThumbUp As Integer = 0
	Dim RightThumbDown As Integer = 0
	'----------Wheel Speed-----------
	Dim TriggerLeft As Integer = 0
	Dim TriggerRight As Integer = 0

	Dim DecrementSpeed As Char = "1"
    Dim IncrementSpeed As Char = "2"

    Dim Forward As Char = "w" 'foward and back wheels clockwise
    Dim Backward As Char = "s" 'foward and back wheels counter clockwise
    Dim LeftTurn As Char = "a" 'left wheels are clockwise and right wheels are ccw
    Dim RightTurn As Char = "d" 'left wheels are ccw and right wheels are clockwise

	Dim StopWheels As Char = "q"

	'--------Steering Actuator Movements-----------------
	Dim DirectionalRight As Integer = 0
	Dim DirectionalLeft As Integer = 0

	Dim RetractSteeringActuators As Char = "z" 'Straight Wheels
	Dim ExtendSteeringActuators As Char = "c" 'Diagonal Wheels

	Dim StopSteeringActuators As Char = "x"


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
		'camera2(currentState2)
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

		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Stop Auger Excavating<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Right Thumbstick at the center<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		If currentState2.ThumbSticks.Right.Y > -0.1F And currentState2.ThumbSticks.Right.Y < 0.1F And currentState2.ThumbSticks.Right.X > -0.1F And currentState2.ThumbSticks.Right.X < 0.1F And RightThumbCenter2 = 0 Then
			RightThumbCenter2 = 1
			RightThumbDown2 = 0
			RightThumbUp2 = 0

			SendKeys.Send(AugerStopExc)
			ListBox2.Items.Add(AugerStopExc)

		ElseIf currentState2.ThumbSticks.Right.Y > 0.1F Or currentState2.ThumbSticks.Right.Y < -0.1F Or currentState2.ThumbSticks.Right.X > 0.1F Or currentState2.ThumbSticks.Right.X < -0.1F And RightThumbCenter2 = 1 Then
			RightThumbCenter2 = 0
		End If

        '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Right Thumbstick Foward<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        If currentState2.ThumbSticks.Right.Y > 0.5F And currentState2.ThumbSticks.Right.Y < 1.5F Then
			If RightThumbUp2 = 0 Then
				RightThumbUp2 = 1
				SendKeys.Send(AugerRevExcavate)
				ListBox2.Items.Add(AugerRevExcavate)

			End If

		End If

		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left Thumbstick Backward<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		If currentState2.ThumbSticks.Right.Y < -0.5F And currentState2.ThumbSticks.Right.Y > -1.5F Then
			If RightThumbDown2 = 0 Then
				RightThumbDown2 = 1
				SendKeys.Send(AugerExcavate)
				ListBox2.Items.Add(AugerExcavate)

			End If

		End If

		''<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Right Thumbstick Left<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		'If currentState2.ThumbSticks.Right.X < -0.5F And currentState2.ThumbSticks.Right.X > -1.5F Then
		'	If (RightThumbRight2) = 0 Then
		'		(RightThumbRight2) = 1
		'		SendKeys.Send(something)
		'		ListBox2.Items.Add(something)

		'	End If

		'End If

		''<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Right Thumbstick Right<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		'If currentState2.ThumbSticks.Right.X > 0.5F And currentState2.ThumbSticks.Right.X < 1.5F Then
		'	If (RightThumbLeft2) = 0 Then
		'		(RightThumbLeft2) = 1
		'		SendKeys.Send(something)
		'		ListBox2.Items.Add(something)

		'	End If

		'End If

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
		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<left Thumbstick at the center click<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


		If currentState.Buttons.LeftStick = ButtonState.Pressed And reset_All = 1 Then
			reset_All = 0
			SendKeys.Send(resetAll)
			ListBox1.Items.Add(resetAll)
		End If
		'If currentState.Buttons.LeftStick = ButtonState.Released And reset_All = 0 Then
		'    reset_All = 1

		'End If


		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Reset Camera Position<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<left Thumbstick at the center<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		If currentState.ThumbSticks.Left.Y > -0.1F And currentState.ThumbSticks.Left.Y < 0.1F And currentState.ThumbSticks.Left.X > -0.1F And currentState.ThumbSticks.Left.X < 0.1F And reset_All = 0 Then
			reset_All = 1
			tilt_Up = 0
			tilt_Down = 0
			pan_Left = 0
			pan_Right = 0

			'SendKeys.Send()
			'ListBox1.Items.Add(resetAll)

		ElseIf currentState.ThumbSticks.Left.Y > 0.1F Or currentState.ThumbSticks.Left.Y < -0.1F Or currentState.ThumbSticks.Left.X > 0.1F Or currentState.ThumbSticks.Left.X < -0.1F And reset_All = 1 Then
			reset_All = 0
		End If

		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left Thumbstick Foward<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

		If currentState.ThumbSticks.Left.Y > 0.5F And currentState.ThumbSticks.Left.Y < 1.5F Then
			If tilt_Up = 0 Then
				tilt_Up = 1
				SendKeys.Send(tiltUp)
				ListBox1.Items.Add(tiltUp)

			End If

		End If

		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left Thumbstick Backward<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

		If currentState.ThumbSticks.Left.Y < -0.5F And currentState.ThumbSticks.Left.Y > -1.5F Then

			If tilt_Down = 0 Then
				tilt_Down = 1
				SendKeys.Send(tiltDown)
				ListBox1.Items.Add(tiltDown)

			End If

		End If

		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left Thumbstick Left<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


		If currentState.ThumbSticks.Left.X < -0.5F And currentState.ThumbSticks.Left.X > -1.5F Then

			If pan_Left = 0 Then
				pan_Left = 1
				SendKeys.Send(panLeft)
				ListBox1.Items.Add(panLeft)

			End If

		End If

		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left Thumbstick Right<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

		If currentState.ThumbSticks.Left.X > 0.5F And currentState.ThumbSticks.Left.X < 1.5F Then

			If pan_Right = 0 Then
				pan_Right = 1
				SendKeys.Send(panRight)
				ListBox1.Items.Add(panRight)

			End If

		End If

	End Sub

	'Public Sub camera2(currentState2 As GamePadState) 'must change wheelLF to something like augermovements
	'	'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left thumbstick<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

	'	'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Reset Camera Position<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
	'	'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<left Thumbstick at the center click<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


	'	If currentState2.Buttons.LeftStick = ButtonState.Pressed And reset_All = 1 Then
	'		reset_All = 0
	'		SendKeys.Send(resetAll)
	'		ListBox1.Items.Add(resetAll)
	'	End If
	'	'If currentState.Buttons.LeftStick = ButtonState.Released And reset_All = 0 Then
	'	'    reset_All = 1

	'	'End If


	'	'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Reset Camera Position<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
	'	'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<left Thumbstick at the center<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
	'	If currentState2.ThumbSticks.Left.Y > -0.1F And currentState2.ThumbSticks.Left.Y < 0.1F And currentState2.ThumbSticks.Left.X > -0.1F And currentState2.ThumbSticks.Left.X < 0.1F And reset_All2 = 0 Then
	'		reset_All2 = 1
	'		tilt_Up2 = 0
	'		tilt_Down2 = 0
	'		pan_Left2 = 0
	'		pan_Right2 = 0

	'		'SendKeys.Send()
	'		'ListBox1.Items.Add(resetAll)

	'	ElseIf currentState2.ThumbSticks.Left.Y > 0.1F Or currentState2.ThumbSticks.Left.Y < -0.1F Or currentState2.ThumbSticks.Left.X > 0.1F Or currentState2.ThumbSticks.Left.X < -0.1F And reset_All2 = 1 Then
	'		reset_All2 = 0
	'	End If

	'	'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left Thumbstick Foward<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

	'	If currentState2.ThumbSticks.Left.Y > 0.5F And currentState2.ThumbSticks.Left.Y < 1.5F Then
	'		If tilt_Up2 = 0 Then
	'			tilt_Up2 = 1
	'			SendKeys.Send(tiltUp2)
	'			ListBox1.Items.Add(tiltUp2)

	'		End If

	'	End If

	'	'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left Thumbstick Backward<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

	'	If currentState2.ThumbSticks.Left.Y < -0.5F And currentState2.ThumbSticks.Left.Y > -1.5F Then

	'		If tilt_Down2 = 0 Then
	'			tilt_Down2 = 1
	'			SendKeys.Send(tiltDown2)
	'			ListBox1.Items.Add(tiltDown2)

	'		End If

	'	End If

	'	'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left Thumbstick Left<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


	'	If currentState2.ThumbSticks.Left.X < -0.5F And currentState2.ThumbSticks.Left.X > -1.5F Then

	'		If pan_Left2 = 0 Then
	'			pan_Left2 = 1
	'			SendKeys.Send(panLeft2)
	'			ListBox1.Items.Add(panLeft2)

	'		End If

	'	End If

	'	'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left Thumbstick Right<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

	'	If currentState2.ThumbSticks.Left.X > 0.5F And currentState2.ThumbSticks.Left.X < 1.5F Then

	'		If pan_Right2 = 0 Then
	'			pan_Right2 = 1
	'			SendKeys.Send(panRight2)
	'			ListBox1.Items.Add(panRight2)

	'		End If

	'	End If

	'End Sub

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
		If currentState.DPad.Down = ButtonState.Pressed And DirectionalDown = 0 Then
			DirectionalDown = 1
			SendKeys.Send(RobotDown)
			ListBox1.Items.Add(RobotDown)

		End If

		If currentState.DPad.Down = ButtonState.Released And DirectionalDown = 1 Then
			DirectionalDown = 0
			SendKeys.Send(RobotStop)
			ListBox1.Items.Add(RobotStop)
		End If

		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Directional Pad Up<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		If currentState.DPad.Up = ButtonState.Pressed And DirectionalUp = 0 Then
			DirectionalUp = 1
			SendKeys.Send(RobotUp)
			ListBox1.Items.Add(RobotUp)

		End If

		If currentState.DPad.Up = ButtonState.Released And DirectionalUp = 1 Then
			DirectionalUp = 0
			SendKeys.Send(RobotStop)
			ListBox1.Items.Add(RobotStop)

		End If

		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Steering Actuators<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Directional Pad Left and Right<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Directional Left<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		If currentState.DPad.Left = ButtonState.Pressed And DirectionalLeft = 0 Then
			DirectionalLeft = 1
			SendKeys.Send(RetractSteeringActuators)
			ListBox1.Items.Add(RetractSteeringActuators)

		End If

		If currentState.DPad.Left = ButtonState.Released And DirectionalLeft = 1 Then
			DirectionalLeft = 0
			SendKeys.Send(StopSteeringActuators)
			ListBox1.Items.Add(StopSteeringActuators)
		End If

		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Directional Pad Right<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		If currentState.DPad.Right = ButtonState.Pressed And DirectionalRight = 0 Then
			DirectionalRight = 1
			SendKeys.Send(ExtendSteeringActuators)
			ListBox1.Items.Add(ExtendSteeringActuators)

		End If

		If currentState.DPad.Right = ButtonState.Released And DirectionalRight = 1 Then
			DirectionalRight = 0
			SendKeys.Send(StopSteeringActuators)
			ListBox1.Items.Add(StopSteeringActuators)

		End If
	End Sub

    Public Sub dpad2(currentState2 As GamePadState)

		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Conveyor Belt<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Directional Pad Left and Right<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Directional Pad Left<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		If currentState2.DPad.Left = ButtonState.Pressed And DirectionalLeft2 = 0 Then
			DirectionalLeft2 = 1
			SendKeys.Send(ReverseConveyor)
			ListBox2.Items.Add(ReverseConveyor)

		End If

		If currentState2.DPad.Left = ButtonState.Released And DirectionalLeft2 = 1 Then
			DirectionalLeft2 = 0

			SendKeys.Send(StopConveyor)
			ListBox2.Items.Add(StopConveyor)

		End If

		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Directional Pad Right<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		If currentState2.DPad.Right = ButtonState.Pressed And DirectionalRight2 = 0 Then
			DirectionalRight2 = 1
			SendKeys.Send(ForwardConveyor)
			ListBox2.Items.Add(ForwardConveyor)
		End If

		If currentState2.DPad.Up = ButtonState.Released And DirectionalRight2 = 1 Then
			DirectionalRight2 = 0

			SendKeys.Send(StopConveyor)
			ListBox2.Items.Add(StopConveyor)

		End If
		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Auger Movement<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Directional Pad Up and Down <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Directional Pad Down<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		If currentState2.DPad.Down = ButtonState.Pressed And DirectionalDown2 = 0 Then
			DirectionalDown2 = 1
			SendKeys.Send(AugerDown)
			ListBox2.Items.Add(AugerDown)

		End If

		If currentState2.DPad.Down = ButtonState.Released And DirectionalDown2 = 1 Then
			DirectionalDown2 = 0

			SendKeys.Send(StopAugerActuators)
			ListBox2.Items.Add(StopAugerActuators)

		End If

		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Directional Pad Up<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		If currentState2.DPad.Up = ButtonState.Pressed And DirectionalUp2 = 0 Then
			DirectionalUp2 = 1
			SendKeys.Send(AugerUp)
			ListBox2.Items.Add(AugerUp)
		End If

		If currentState2.DPad.Up = ButtonState.Released And DirectionalUp2 = 1 Then
			DirectionalUp2 = 0

			SendKeys.Send(StopAugerActuators)
			ListBox2.Items.Add(StopAugerActuators)

		End If

	End Sub

    Public Sub triggers(currentState As GamePadState)

		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Increment and Decrement Wheel Speeds<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
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

	End Sub

	Public Sub triggers2(currentState2 As GamePadState)
		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Increment and Decrement Auger Speeds<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left and Right Triggers<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Left Trigger Pressed<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Decrease Auger Speed<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		If currentState2.Triggers.Left = ButtonState.Pressed And TriggerLeft2 = 0 Then

			TriggerLeft2 = 1
			SendKeys.Send(DecrementSpeedAuger)
			ListBox1.Items.Add(DecrementSpeedAuger)


			If currentState2.ThumbSticks.Right.Y > 0.5F And currentState2.ThumbSticks.Right.Y < 1.5F Then

				SendKeys.Send(AugerRevExcavate)
				ListBox1.Items.Add(AugerRevExcavate)

			End If

			If currentState2.ThumbSticks.Right.Y < -0.5F And currentState2.ThumbSticks.Right.Y > -1.5F Then

				SendKeys.Send(AugerExcavate)
				ListBox1.Items.Add(AugerExcavate)
			End If

			'If currentState2.ThumbSticks.Right.X > 0.5F And currentState2.ThumbSticks.Right.X < 1.5F Then

			'	SendKeys.Send(RightTurn)
			'	ListBox1.Items.Add(RightTurn)
			'End If

			'If currentState.ThumbSticks.Right.X < -0.5F And currentState.ThumbSticks.Right.X > -1.5F Then

			'	SendKeys.Send(LeftTurn)
			'	ListBox1.Items.Add(LeftTurn)

			'End If

		End If

		If currentState2.Triggers.Left = ButtonState.Released And TriggerLeft2 = 1 Then
			TriggerLeft = 0
		End If

		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Right Trigger Pressed<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		'<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Increase Global Speed<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		If currentState2.Triggers.Right = ButtonState.Pressed And TriggerRight2 = 0 Then
			TriggerRight2 = 1
			SendKeys.Send(IncrementSpeedAuger)
			ListBox1.Items.Add(IncrementSpeedAuger)

			If currentState2.ThumbSticks.Right.Y > 0.5F And currentState2.ThumbSticks.Right.Y < 1.5F Then

				SendKeys.Send(AugerRevExcavate)
				ListBox1.Items.Add(AugerRevExcavate)
			End If

			If currentState2.ThumbSticks.Right.Y < -0.5F And currentState2.ThumbSticks.Right.Y > -1.5F Then

				SendKeys.Send(AugerExcavate)
				ListBox1.Items.Add(AugerExcavate)
			End If

			'If currentState.ThumbSticks.Right.X > 0.5F And currentState.ThumbSticks.Right.X < 1.5F Then

			'	SendKeys.Send(RightTurn)
			'	ListBox1.Items.Add(RightTurn)
			'End If

			'If currentState.ThumbSticks.Right.X < -0.5F And currentState.ThumbSticks.Right.X > -1.5F Then

			'	SendKeys.Send(LeftTurn)
			'	ListBox1.Items.Add(LeftTurn)
			'End If

		End If

		If currentState2.Triggers.Right = ButtonState.Released And TriggerRight2 = 1 Then
			TriggerRight2 = 0
		End If
	End Sub

	Private Sub Label38_Click(sender As Object, e As EventArgs) Handles Label38.Click

	End Sub

	'Public Sub StartandBack(currentState As GamePadState)
	'    '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Start and Back Button <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
	'    '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Wheel movement to go left and right<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

	'    '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<back<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

	'    If currentState.Buttons.Back = ButtonState.Pressed And SelectButton = 0 Then
	'        SelectButton = 1
	'        SendKeys.Send(LeftTurn)
	'        ListBox1.Items.Add(LeftTurn)
	'    End If
	'    If currentState.Buttons.Back = ButtonState.Released And StartButton = 1 Then
	'        StartButton = 0
	'        SendKeys.Send(StopWheels)
	'        ListBox1.Items.Add(StopWheels)
	'    End If

	'    '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Back<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
	'    If currentState.Buttons.Start = ButtonState.Pressed And StartButton = 0 Then
	'        StartButton = 1
	'        SendKeys.Send(RightTurn)
	'        ListBox1.Items.Add(RightTurn)
	'    End If
	'    If currentState.Buttons.Start = ButtonState.Released And StartButton = 1 Then
	'        StartButton = 0
	'        SendKeys.Send(StopWheels)
	'        ListBox1.Items.Add(StopWheels)
	'    End If
	'End Sub

End Class
