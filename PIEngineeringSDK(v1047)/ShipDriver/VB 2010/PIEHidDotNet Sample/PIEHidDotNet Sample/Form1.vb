Public Class Form1
    Implements PIEHid32Net.PIEDataHandler
    Implements PIEHid32Net.PIEErrorHandler
    Dim devices() As PIEHid32Net.PIEDevice
    Dim selecteddevice As Integer
    Dim cbotodevice(127) As Integer 'max # of devices = 128 
    Dim wdata() As Byte = New Byte() {} 'write data buffer
    ' This delegate enables asynchronous calls for setting
    ' the text property on a TextBox control.
    Delegate Sub SetTextCallback(ByVal [text] As String)
    Dim c As Control
    Dim wheel As Int16 = 1
    Dim triggerit As Boolean = False
    Dim pdata(80) As Byte 'previous read data buffer
    Dim savelastsent As Integer
    Dim firstread As Boolean = True
    Dim runningtotal As Double
    Dim lastdegrees As Double

    Public Sub HandlePIEHidData(ByVal data() As Byte, ByVal sourceDevice As PIEHid32Net.PIEDevice, ByVal perror As Integer) Implements PIEHid32Net.PIEDataHandler.HandlePIEHidData
        'data callback
        'MsgBox("The event handler caught the event.")
        If sourceDevice.Pid = devices(selecteddevice).Pid Then
           
                Dim output As String
                output = "Callback: " + sourceDevice.Pid.ToString + ", ID: " + selecteddevice.ToString + ", data="
                For i As Integer = 0 To sourceDevice.ReadLength - 1
                output = output + data(i).ToString + "  "
                Next
                ' Wheel MSB = rdata[11]
                ' Wheel LSB = rdata[12]
                ' Lever 1 = rdata[13]
                ' Lever 2 = rdata[14]
                ' Lower Switch = rdata[15]
                ' Upper Switch = rdata[16]
                ' Ship Mode = rdata[17]
                ' Lever 1 calibrated = rdata[18]
                ' Lever 2 calibrated = rdata[19]
                ' Lower Switch calibrated = rdata[20]
                ' Upper Switch calibrated = rdata[21]

                'Use thread-safe calls to windows forms controls

                SetListBox(output)

                c = LblUnitID
            SetText(data(1).ToString)

                c = TxtWheel
            Dim theta As Double = data(11) * 256.0 + data(12)
                theta = (theta / (256.0 * 256.0)) * 360.0
                theta = 360 - theta
                SetText(theta.ToString)

                c = TxtLever1
            SetText(data(18).ToString)

                c = TxtLever2
            SetText(data(19).ToString)

                c = TxtSwitch2
            SetText(data(20).ToString)

                c = TxtSwitch1
            SetText(data(21).ToString)

                'find the direction of the wheel
                Dim delta As Double = theta - lastdegrees
                Dim halfrange As Integer = 180
                Dim fullrange As Integer = 360
                If (delta < -1 * halfrange) Then
                    delta = delta + fullrange
                ElseIf (delta > halfrange) Then
                    delta = delta - fullrange
                End If
                c = LblWheelDir
                If (delta > 0) Then
                    SetText("clockwise")
                Else
                    SetText("counter-clockwise")
                End If
                lastdegrees = theta
                'Limit the wheel data to desired bounds -180 to 180
                If (firstread = False) Then
                    runningtotal = runningtotal + delta
                    If (runningtotal > 180) Then runningtotal = 180
                    If (runningtotal < -180) Then runningtotal = -180
                    c = LblPos180
                    SetText(runningtotal.ToString())
                End If
                firstread = False


        End If
    End Sub
    Public Sub HandlePIEHidError(ByVal sourceDevice As PIEHid32Net.PIEDevice, ByVal perror As Integer) Implements PIEHid32Net.PIEErrorHandler.HandlePIEHidError
        'error callback
        Dim output As String
        output = "Error: " + perror.ToString
        c = LblStatus
        SetText(output)
        Beep()
    End Sub
    Public Sub SetListBox(ByVal [text] As String)

        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        If Me.ListBox1.InvokeRequired Then
            Dim d As New SetTextCallback(AddressOf SetListBox)
            Me.Invoke(d, New Object() {[text]})
        Else
            Me.ListBox1.Items.Add(text)
            Me.ListBox1.SelectedIndex = Me.ListBox1.Items.Count - 1
        End If
    End Sub
    Public Sub SetText(ByVal [text] As String)

        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        If Me.c.InvokeRequired Then
            Dim d As New SetTextCallback(AddressOf SetText)
            Me.Invoke(d, New Object() {[text]})
        Else
            Me.c.Text = text
        End If
    End Sub
    Private Sub BtnEnumerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEnumerate.Click
        'do this first to get the devices connected
        CboDevices.Items.Clear()
        selecteddevice = -1 'means no device is selected
        devices = PIEHid32Net.PIEDevice.EnumeratePIE()
        If devices.Length = 0 Then
            LblStatus.Text = "No Devices Found"
        Else
            Dim cbocount As Integer = 0
            For i As Integer = 0 To devices.Length - 1

                If devices(i).HidUsagePage = 12 Then

                    Select Case devices(i).Pid
                        Case 1055
                            CboDevices.Items.Add("ShipDriver (" + devices(i).Pid.ToString + "): " + i.ToString)
                            cbotodevice(cbocount) = i
                            cbocount = cbocount + 1
                        
                        Case Else
                            CboDevices.Items.Add("Unknown Device (" + devices(i).Pid.ToString + "): " + i.ToString)
                            cbotodevice(cbocount) = i
                            cbocount = cbocount + 1
                    End Select
                    Dim result As Integer = devices(i).SetupInterface()
                    If result <> 0 Then
                        LblStatus.Text = "Failed SetupInterface on device: " + i.ToString
                    Else
                        LblStatus.Text = "Success SetupInterface"
                    End If
                End If

            Next
        End If

        If CboDevices.Items.Count > 0 Then
            CboDevices.SelectedIndex = 0
            selecteddevice = cbotodevice(CboDevices.SelectedIndex)
            ReDim wdata(devices(selecteddevice).WriteLength - 1) 'initialize length of write buffer
        End If
    End Sub
    Private Sub Form1_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        'close devices
        For i As Integer = 0 To CboDevices.Items.Count - 1
            devices(cbotodevice(i)).CloseInterface()
        Next
        System.Environment.Exit(0)
    End Sub

    Private Sub BtnCallback_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCallback.Click
        'setup devices for data and error callbacks
        If CboDevices.SelectedIndex <> -1 Then
            For i As Integer = 0 To CboDevices.Items.Count - 1
                devices(cbotodevice(i)).SetDataCallback(Me)
                devices(cbotodevice(i)).SetErrorCallback(Me)
                devices(cbotodevice(i)).callNever = False
            Next
            selecteddevice = cbotodevice(CboDevices.SelectedIndex)
        End If
    End Sub

    Private Sub CboDevices_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboDevices.SelectedIndexChanged
        'update selecteddevice with that chosen and redim the write array
        selecteddevice = cbotodevice(CboDevices.SelectedIndex)
        ReDim wdata(devices(selecteddevice).WriteLength - 1) 'initialize length of write buffer
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ListBox1.Items.Clear()
    End Sub

    Private Sub BtnSpeakerOn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSpeakerOn.Click
        'turn on speaker
        'the following will turn on the RailDriver speaker
        'If you wish to
        'hear only the RailDriver speaker disconnect the Speaker
        'Pass Thru in the back of the unit.  Make sure the RailDriver
        'power and speaker are plugged in.
        If selecteddevice <> -1 Then
            For i As Integer = 0 To devices(selecteddevice).WriteLength - 1
                wdata(i) = 0
            Next
            wdata(1) = 215
            wdata(2) = 1

            Dim result As Integer
            result = 404
            While (result = 404)
                result = devices(selecteddevice).WriteData(wdata)
            End While

            If result <> 0 Then
                LblStatus.Text = "Write Fail: " + result.ToString
            Else
                LblStatus.Text = "Write Success - Speaker On"

            End If
        End If
    End Sub

    Private Sub BtnSpeakerOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSpeakerOff.Click
        If selecteddevice <> -1 Then
            For i As Integer = 0 To devices(selecteddevice).WriteLength - 1
                wdata(i) = 0
            Next
            wdata(1) = 215
            wdata(7) = 0

            Dim result As Integer
            result = 404
            While (result = 404)
                result = devices(selecteddevice).WriteData(wdata)
            End While

            If result <> 0 Then
                LblStatus.Text = "Write Fail: " + result.ToString
            Else
                LblStatus.Text = "Write Success - Speaker Off"

            End If
        End If
    End Sub

    Private Sub BtnWriteDisplay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnWriteDisplay.Click
        'write to LED segments
        If selecteddevice <> -1 Then
            For i As Integer = 0 To devices(selecteddevice).WriteLength - 1
                wdata(i) = 0
            Next

            'do this first to stop autowriting to display (from step 7)
            wdata(1) = 195

            Dim result As Integer
            result = 404
            While (result = 404)
                result = devices(selecteddevice).WriteData(wdata)
            End While

            If result <> 0 Then
                LblStatus.Text = "Write Fail: " + result.ToString
            Else
                LblStatus.Text = "Write Success - Write to Display"

            End If

            'now write to display
            wdata(1) = 187
            wdata(2) = TextBox1.Text
            wdata(3) = TextBox2.Text
            wdata(4) = TextBox3.Text

            result = 404
            While (result = 404)
                result = devices(selecteddevice).WriteData(wdata)
            End While

            If result <> 0 Then
                LblStatus.Text = "Write Fail: " + result.ToString
            Else
                LblStatus.Text = "Write Success - Write to Display"

            End If
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        selecteddevice = -1

        CboWheel.SelectedIndex = 4
        CboLever1.SelectedIndex = 0
        CboLever2.SelectedIndex = 1
        CboSw1.SelectedIndex = 3
        CboSw2.SelectedIndex = 2

    End Sub

    

    Private Sub BtnStartCal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnStartCal.Click
        'A calibration should be done at least once on each ShipDriver.
        If selecteddevice <> -1 Then
            Dim dresult As DialogResult
            dresult = MessageBox.Show("Move levers and switches over full range, leaving each in center.  Rotate wheel slowly two or three revoluations.  When all controls are centered press Save Cal.", "ShipDriver Sample", MessageBoxButtons.OKCancel)
            If dresult = Windows.Forms.DialogResult.OK Then
                For i As Integer = 0 To devices(selecteddevice).WriteLength - 1
                    wdata(i) = 0
                Next
                wdata(1) = 195
                wdata(2) = 7

                Dim result As Integer
                result = 404
                While (result = 404)
                    result = devices(selecteddevice).WriteData(wdata)
                End While

                If result <> 0 Then
                    LblStatus.Text = "Write Fail: " + result.ToString
                Else
                    LblStatus.Text = "Write Success - Start Cal"
                End If
            End If
        End If
    End Sub

    Private Sub BtnSaveCal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSaveCal.Click
        If selecteddevice <> -1 Then
            For i As Integer = 0 To devices(selecteddevice).WriteLength - 1
                wdata(i) = 0
            Next
            wdata(1) = 195
            wdata(2) = 9

            Dim result As Integer
            result = 404
            While (result = 404)
                result = devices(selecteddevice).WriteData(wdata)
            End While

            If result <> 0 Then
                LblStatus.Text = "Write Fail: " + result.ToString
            Else
                LblStatus.Text = "Write Success - Save Cal"

            End If
        End If
    End Sub

    Private Sub BtnDisplayWheel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDisplayWheel.Click
        If selecteddevice <> -1 Then
            For i As Integer = 0 To devices(selecteddevice).WriteLength - 1
                wdata(i) = 0
            Next
            wdata(1) = 195
            wdata(2) = 1

            Dim result As Integer
            result = 404
            While (result = 404)
                result = devices(selecteddevice).WriteData(wdata)
            End While

            If result <> 0 Then
                LblStatus.Text = "Write Fail: " + result.ToString
            Else
                LblStatus.Text = "Write Success - Display Wheel"

            End If
        End If
    End Sub

    Private Sub BtnDisplayLever1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDisplayLever1.Click
        If selecteddevice <> -1 Then
            For i As Integer = 0 To devices(selecteddevice).WriteLength - 1
                wdata(i) = 0
            Next
            wdata(1) = 195
            wdata(2) = 2

            Dim result As Integer
            result = 404
            While (result = 404)
                result = devices(selecteddevice).WriteData(wdata)
            End While

            If result <> 0 Then
                LblStatus.Text = "Write Fail: " + result.ToString
            Else
                LblStatus.Text = "Write Success - Display Lever 1"

            End If
        End If
    End Sub

    Private Sub BtnDisplayLever2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDisplayLever2.Click
        If selecteddevice <> -1 Then
            For i As Integer = 0 To devices(selecteddevice).WriteLength - 1
                wdata(i) = 0
            Next
            wdata(1) = 195
            wdata(2) = 3

            Dim result As Integer
            result = 404
            While (result = 404)
                result = devices(selecteddevice).WriteData(wdata)
            End While

            If result <> 0 Then
                LblStatus.Text = "Write Fail: " + result.ToString
            Else
                LblStatus.Text = "Write Success - Display Lever 2"

            End If
        End If
    End Sub

    Private Sub BtnDisplaySwitch1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDisplaySwitch1.Click
        If selecteddevice <> -1 Then
            For i As Integer = 0 To devices(selecteddevice).WriteLength - 1
                wdata(i) = 0
            Next
            wdata(1) = 195
            wdata(2) = 5

            Dim result As Integer
            result = 404
            While (result = 404)
                result = devices(selecteddevice).WriteData(wdata)
            End While

            If result <> 0 Then
                LblStatus.Text = "Write Fail: " + result.ToString
            Else
                LblStatus.Text = "Write Success - Display Switch 1"

            End If
        End If
    End Sub

  

    Private Sub BtnDisplaySwitch2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDisplaySwitch2.Click
        If selecteddevice <> -1 Then
            For i As Integer = 0 To devices(selecteddevice).WriteLength - 1
                wdata(i) = 0
            Next
            wdata(1) = 195
            wdata(2) = 4

            Dim result As Integer
            result = 404
            While (result = 404)
                result = devices(selecteddevice).WriteData(wdata)
            End While

            If result <> 0 Then
                LblStatus.Text = "Write Fail: " + result.ToString
            Else
                LblStatus.Text = "Write Success - Display Switch 2"

            End If
        End If
    End Sub

    Private Sub BtnUnitID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUnitID.Click
        If selecteddevice <> -1 Then
            For i As Integer = 0 To devices(selecteddevice).WriteLength - 1
                wdata(i) = 0
            Next
            wdata(1) = 189
            wdata(2) = Convert.ToInt16(TxtUnitID.Text)

            Dim result As Integer
            result = 404
            While (result = 404)
                result = devices(selecteddevice).WriteData(wdata)
            End While

            If result <> 0 Then
                LblStatus.Text = "Write Fail: " + result.ToString
            Else
                LblStatus.Text = "Write Success - Unit ID"

            End If
        End If
    End Sub

    Private Sub BtnKeyboardReflect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnKeyboardReflect.Click
        If selecteddevice <> -1 Then
            Dim result As Integer
            TxtKeyboardReflect.Focus() 'make sure there is a place to receive keyboard input

            For i As Integer = 0 To devices(selecteddevice).WriteLength - 1
                wdata(i) = 0
            Next
            wdata(1) = 201
            wdata(2) = 2  ' modifiers, shift down
            wdata(3) = 0    'always 0
            wdata(4) = &H4  'a down
            wdata(5) = &H5  'b down
            wdata(6) = &H6  'c down
            wdata(7) = &H7  'd down
            wdata(8) = &H8  'e down
            wdata(9) = &H9  'f down

            result = 404
            While (result = 404)
                result = devices(selecteddevice).WriteData(wdata)
            End While

            If result <> 0 Then
                LblStatus.Text = "Write Fail: " + result.ToString
            
            End If

            'send key ups
            wdata(2) = 0  ' modifiers, shift up
            wdata(3) = 0    'always 0
            wdata(4) = 0  'a up
            wdata(5) = 0  'b up
            wdata(6) = 0  'c up
            wdata(7) = 0  'd up
            wdata(8) = 0  'e up
            wdata(9) = 0  'f up

            'Dim result As Integer
            result = 404
            While (result = 404)
                result = devices(selecteddevice).WriteData(wdata)
            End While

            If result <> 0 Then
                LblStatus.Text = "Write Fail: " + result.ToString
            Else
                LblStatus.Text = "Write Success - Keyboard Reflect"

            End If
        End If
    End Sub

    Private Sub BtnJoystickReflect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnJoystickReflect.Click
        If selecteddevice <> -1 Then
            'after entering values in the text boxes and clicking this button make the Game Controller-Properties window active to see changes

            For i As Integer = 0 To devices(selecteddevice).WriteLength - 1
                wdata(i) = 0
            Next
            wdata(1) = 202
            wdata(2) = Convert.ToInt16(TxtJoyX.Text) 'x 0-127=center to full right, 255-128=center to full left
            wdata(3) = Convert.ToInt16(TxtJoyY.Text) 'y 0-127=center to bottom, 255-128=center to top
            wdata(4) = Convert.ToInt16(TxtJoyZrot.Text) 'z rot 0-127=center to bottom, 255-128=center to top
            wdata(5) = Convert.ToInt16(TxtJoyZ.Text) 'z 0-127=center to bottom, 255-128=center to top
            wdata(6) = Convert.ToInt16(TxtJoySlid.Text) 'slider 0-127=center to bottom, 255-128=center to top
            wdata(7) = Convert.ToInt16(TxtJoyButtons.Text) 'buttons 1-8
            wdata(8) = Convert.ToInt16(TxtJoyButtons2.Text) 'buttons 9-16
            wdata(9) = Convert.ToInt16(TxtJoyButtons3.Text) 'buttons 17-24
            wdata(10) = Convert.ToInt16(TxtJoyButtons4.Text) 'buttons 25-32
            wdata(12) = Convert.ToInt16(TxtJoyHat.Text) 'hat

            Dim result As Integer
            result = 404
            While (result = 404)
                result = devices(selecteddevice).WriteData(wdata)
            End While

            If result <> 0 Then
                LblStatus.Text = "Write Fail: " + result.ToString
            Else
                LblStatus.Text = "Write Success - Joystick Reflect"

            End If
        End If
    End Sub

    Private Sub BtnShipModeJoy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnShipModeJoy.Click
        ' note this setting is not permanent, ie the unit will return to its default after hotplugging or reboot.

        If selecteddevice <> -1 Then
            For i As Integer = 0 To devices(selecteddevice).WriteLength - 1
                wdata(i) = 0
            Next
            wdata(1) = 216
            wdata(2) = 1 'Joystick: Wheel=Slid. w/wrapping, Lever 1=X, Lever 2=Y, Switch 1=Z Rot, Switch 2=Z Axis

            Dim result As Integer
            result = 404
            While (result = 404)
                result = devices(selecteddevice).WriteData(wdata)
            End While

            If result <> 0 Then
                LblStatus.Text = "Write Fail: " + result.ToString
            Else
                LblStatus.Text = "Write Success - Set Ship Mode"

            End If
        End If
    End Sub

    Private Sub BtnShipModeWheel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnShipModeWheel.Click
        'note this setting is not permanent, ie the unit will return to its default after hotplugging or reboot.

        If selecteddevice <> -1 Then
            For i As Integer = 0 To devices(selecteddevice).WriteLength - 1
                wdata(i) = 0
            Next
            wdata(1) = 216
            wdata(2) = 129 'Joystick: Wheel=Slid. w/o wrapping, Lever 1=X, Lever 2=Y, Switch 1=Z Rot, Switch 2=Z Axis

            Dim result As Integer
            result = 404
            While (result = 404)
                result = devices(selecteddevice).WriteData(wdata)
            End While

            If result <> 0 Then
                LblStatus.Text = "Write Fail: " + result.ToString
            Else
                LblStatus.Text = "Write Success - Set Ship Mode"

            End If
        End If
    End Sub

    Private Sub BtnShipModeNone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnShipModeNone.Click
        'note this setting is not permanent, ie the unit will return to its default after hotplugging or reboot.

        If selecteddevice <> -1 Then
            For i As Integer = 0 To devices(selecteddevice).WriteLength - 1
                wdata(i) = 0
            Next
            wdata(1) = 216
            wdata(2) = 0 'no joystick

            Dim result As Integer
            result = 404
            While (result = 404)
                result = devices(selecteddevice).WriteData(wdata)
            End While

            If result <> 0 Then
                LblStatus.Text = "Write Fail: " + result.ToString
            Else
                LblStatus.Text = "Write Success - Set Ship Mode"

            End If
        End If
    End Sub

    Private Sub BtnDescriptor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDescriptor.Click
        If selecteddevice <> -1 Then

            'IMPORTANT turn off the callback if going so data isn't grabbed there, turn it back on later (not done here)
            devices(selecteddevice).callNever = True

            For i As Integer = 0 To devices(selecteddevice).WriteLength - 1
                wdata(i) = 0
            Next

            wdata(0) = 0
            wdata(1) = 214

            Dim result As Integer
            result = 404
            While (result = 404)
                result = devices(selecteddevice).WriteData(wdata)
            End While

            If result <> 0 Then
                LblStatus.Text = "Write Fail: " + result.ToString
            Else
                LblStatus.Text = "Write Success - to Reflector mode"
            End If
            'after this write the next read with 3rd byte = 214 gives descriptor data
            Dim ddata(devices(selecteddevice).ReadLength) As Byte
            Dim countout As Integer = 0
            result = devices(selecteddevice).BlockingReadData(ddata, 100)
            While (result = 304 Or (result = 0 And ddata(2) <> 214))
                If result = 304 Then
                    'no new data after 100ms, so increment countout extra
                    countout = countout + 99
                End If
                countout = countout + 1
                If (countout > 500) Then
                    Exit While
                End If
                result = devices(selecteddevice).BlockingReadData(ddata, 100)
            End While
            listBox2.Items.Clear()
            If (ddata(3) = 0) Then
                listBox2.Items.Add("Reflector Mode")
            ElseIf (ddata(3) = 2) Then
                listBox2.Items.Add("SPLAT Mode")
            End If
            listBox2.Items.Add("Keymapstart=" + ddata(4).ToString)
            listBox2.Items.Add("Layer2offset=" + ddata(5).ToString)
            listBox2.Items.Add("OutSize" + ddata(6).ToString)
            listBox2.Items.Add("ReportSize=" + ddata(7).ToString)
            listBox2.Items.Add("MaxCol=" + ddata(8).ToString)
            listBox2.Items.Add("MaxRow=" + ddata(9).ToString)
            Dim ledon As String = ""
            If (ddata(10) And 64) Then
                ledon = "Green LED "
            End If
            If (ddata(10) And 128) Then
                ledon = "Red LED "
            End If

            If (ledon = "") Then
                ledon = "None"
            End If
            listBox2.Items.Add("LEDs=" + ledon)
            listBox2.Items.Add("Version=" + ddata(11).ToString)
            Dim temp As String = "PID=" + (ddata(13) * 256 + ddata(12)).ToString
            listBox2.Items.Add(temp)
        End If
    End Sub

    Private Sub BtnSens_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSens.Click

        'sets the wheel sensitivity, how much movement of the wheel correlates to movement of the slider in the game control panel
        'note this setting is not permanent, ie the unit will return to its default after hotplugging or reboot.

        If selecteddevice <> -1 Then
            For i As Integer = 0 To devices(selecteddevice).WriteLength - 1
                wdata(i) = 0
            Next
            wdata(1) = 218
            wdata(2) = Convert.ToInt16(textBox4.Text) 'default=13, the bigger the number the more turning to fill the range

            Dim result As Integer
            result = 404
            While (result = 404)
                result = devices(selecteddevice).WriteData(wdata)
            End While

            If result <> 0 Then
                LblStatus.Text = "Write Fail: " + result.ToString
            Else
                LblStatus.Text = "Write Success - Wheel Sensitivity"

            End If
        End If
    End Sub

    Private Sub BtnZeroWheel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnZeroWheel.Click
        If selecteddevice <> -1 Then
            For i As Integer = 0 To devices(selecteddevice).WriteLength - 1
                wdata(i) = 0
            Next
            wdata(1) = 217
            
            Dim result As Integer
            result = 404
            While (result = 404)
                result = devices(selecteddevice).WriteData(wdata)
            End While

            If result <> 0 Then
                LblStatus.Text = "Write Fail: " + result.ToString
            Else
                LblStatus.Text = "Write Success - Zero Wheel"

            End If
        End If
    End Sub

    Private Sub BtnZero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnZero.Click
        wheel = 128
        triggerit = True
    End Sub

    Private Sub BtnMyJoystick_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnMyJoystick.Click
        'NOTE: Make sure to put the unit into Analogs Data Only in order to utilize this feature
        'This is handling all of the analog data and converting it to joystick manually using Joystick Reflector commands

        'NOTE2: Make sure to perform a calibration at least once before using this as calibrated values are used
        If (selecteddevice <> -1) Then
            If (BtnMyJoystick.Text = "My Joystick Off") Then
                BtnMyJoystick.Text = "My Joystick On"
                timer1.Enabled = True
            Else
                BtnMyJoystick.Text = "My Joystick Off"
                timer1.Enabled = False
            End If
        End If
    End Sub

    Private Sub timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer1.Tick
        Dim rldata(80) As Byte
        Dim ret2 As Integer = devices(selecteddevice).ReadLast(rldata)
        'check the analog bytes, if no changes then don't send anything
        Dim different As Boolean = False
        For i As Integer = 18 To 22
            If (pdata(i) <> rldata(i)) Then
                different = True
                Exit For
            End If
        Next
        If (pdata(11) <> rldata(11) Or pdata(12) <> rldata(12)) Then
            different = True
        End If
        If (triggerit = True) Then
            different = True
            triggerit = False
        End If
        If (different = True) Then
            For i As Integer = 0 To devices(selecteddevice).WriteLength - 1
                wdata(i) = 0
            Next
            wdata(1) = 202 'joystick reflector command
            'fill in the following bytes
            'wData[2] = X
            'wData[3] = Y
            'wData[4] = Z Rotation
            'wData[5] = Z Axis 
            'wData[6] = Slider

            'in the code below each combo box has  these items in this order
            'Joystick(X)
            'Joystick(Y)
            'Z(Rotation)
            'Z(Axis)
            'Slider()
            '(None)
            If (CboWheel.SelectedIndex <> 5) Then
              
                If (ChkNoWrap.Checked = False) Then
                    Dim wheeli As UShort = CUShort((rldata(11) * 256 + rldata(12)))
                    wheeli = CUShort(((wheeli >> 6) And 255))
                    If (ChkWheel.Checked = True) Then
                        wheeli = CByte((255 - wheeli)) 'flip polarity
                    End If
                    wdata(CboWheel.SelectedIndex + 2) = CByte((wheeli))
                Else
                    'use only the msb of the wheel for this
                    If (pdata(11) <> rldata(11)) Then 'don't change wheel if no change
                    
                        Dim sdelta As Integer = (rldata(11)) - savelastsent

                        sdelta = sdelta And 255
                        If (sdelta > 127) Then
                            sdelta = sdelta - 256
                        End If
                        
                        If ChkWheel.Checked = True Then
                            sdelta = (-1 * sdelta)
                        End If
                        If (sdelta > 7) Then
                            wheel = wheel + Convert.ToInt16(TxtSens.Text)
                            If (wheel > 255) Then wheel = 255 'stay put
                            wdata(CboWheel.SelectedIndex + 2) = (wheel Xor &H80)
                            savelastsent = rldata(11)
                        ElseIf (sdelta < -7) Then
                            wheel = wheel - Convert.ToInt16(TxtSens.Text)
                            If (wheel < 0) Then wheel = 0 'stay put
                            wdata(CboWheel.SelectedIndex + 2) = (wheel Xor &H80)
                            savelastsent = rldata(11)
                        Else
                            wdata(CboWheel.SelectedIndex + 2) = (wheel Xor &H80)
                        End If
                    Else
                        wdata(CboWheel.SelectedIndex + 2) = (wheel Xor &H80)
                    End If
                End If

            End If 'end of if cboWheel <>5

            'Lever 1
            If (CboLever1.SelectedIndex <> 5) Then
                Dim lever1 As Byte = rldata(18)
                If (ChkLever1.Checked = True) Then
                    lever1 = 255 - lever1 'flip polarity
                End If
                wdata(CboLever1.SelectedIndex + 2) = lever1 Xor &H80
            End If

            'Lever 2
            If (CboLever2.SelectedIndex <> 5) Then
                Dim lever2 As Byte = rldata(19)
                If (ChkLever2.Checked = True) Then
                    lever2 = 255 - lever2 'flip polarity
                End If
                wdata(CboLever2.SelectedIndex + 2) = lever2 Xor &H80
            End If

            'Switch 1
            If (CboSw1.SelectedIndex <> 5) Then
                Dim sw1 As Byte = rldata(20)
                If (ChkSw1.Checked = True) Then
                    sw1 = 255 - sw1 'flip polarity
                End If
                wdata(CboSw1.SelectedIndex + 2) = sw1 Xor &H80
            End If

            'Switch 2
            If (CboSw2.SelectedIndex <> 5) Then
                Dim sw2 As Byte = rldata(21)
                If (ChkSw2.Checked = True) Then
                    sw2 = 255 - sw2 'flip polarity
                End If
                wdata(CboSw2.SelectedIndex + 2) = sw2 Xor &H80
            End If

            'wData[2] = xaxis 0-127=center to full right, 255-128=center to full left
            'wData[3] = yaxis -127=center to bottom, 255-128=center to top
            'wData[4] = zrot 0-127=center to bottom, 255-128=center to top
            'wData[5] = zaxis 0-127=center to bottom, 255-128=center to top
            'wData[6] = slider 0-127=center to bottom, 255-128=center to top
            'wData[7] = buttons 1-8
            'wData[8] = buttons 9-16
            'wData[9] = buttons 17-24
            'wData[10] = buttons 25-32
            'wData[12] = hat

            Dim result As Integer
            result = 404
            While (result = 404)
                result = devices(selecteddevice).WriteData(wdata)
            End While
           
        End If 'end of if different = true
        pdata = rldata
        

        

    End Sub

    Private Sub BtnTillerSens_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTillerSens.Click
        'sets the tiller sensitivity, how much movement of the tiller correlates to movement of the slider in the game control panel
        'note this setting is not permanent, ie the unit will return to its default after hotplugging or reboot.

        If selecteddevice <> -1 Then
            For i As Integer = 0 To devices(selecteddevice).WriteLength - 1
                wdata(i) = 0
            Next
            wdata(1) = 218
            wdata(2) = Convert.ToInt16(TextBox5.Text) 'default=50, the bigger the number the less turning to fill the range

            Dim result As Integer
            result = 404
            While (result = 404)
                result = devices(selecteddevice).WriteData(wdata)
            End While

            If result <> 0 Then
                LblStatus.Text = "Write Fail: " + result.ToString
            Else
                LblStatus.Text = "Write Success - Tiller Sensitivity"

            End If
        End If
    End Sub

    
    Private Sub BtnMacrosOn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnMacrosOn.Click
        'If there are internal macros stored, make so will be executed on key presses.
        'Unit will default back to Macros On if hotplugged or rebooted
        'These macros programmed using the hardware mode feature of the MacroWorks 3 software
        If selecteddevice <> -1 Then
            For i As Integer = 0 To devices(selecteddevice).WriteLength - 1
                wdata(i) = 0
            Next
            'enable byte is bit 7=0, bit 6=0, bit 5=0, bit 4=Splat, bit 3=Mouse, bit 2=Joy, bit 1=Keyboard, bit 0=Stored Macros
            wdata(1) = 211
            wdata(2) = 23 'default 23, set to 23 to have stored macros executed on key presses

            Dim result As Integer
            result = 404
            While (result = 404)
                result = devices(selecteddevice).WriteData(wdata)
            End While

            If result <> 0 Then
                LblStatus.Text = "Write Fail: " + result.ToString
            Else
                LblStatus.Text = "Write Success - Macros On"

            End If
        End If
    End Sub

    Private Sub BtnMacroOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnMacroOff.Click
        'If there are internal macros stored, make so will not be executed on key presses.
        'Unit will default back to Macros On if hotplugged or rebooted
        'These macros programmed using the hardware mode feature of the MacroWorks 3 software

        If selecteddevice <> -1 Then
            For i As Integer = 0 To devices(selecteddevice).WriteLength - 1
                wdata(i) = 0
            Next
            'enable byte is bit 7=0, bit 6=0, bit 5=0, bit 4=Splat, bit 3=Mouse, bit 2=Joy, bit 1=Keyboard, bit 0=Stored Macros
            wdata(1) = 211
            wdata(2) = 22 'default 23, set to 22 to have stored macros not executed on key presses

            Dim result As Integer
            result = 404
            While (result = 404)
                result = devices(selecteddevice).WriteData(wdata)
            End While

            If result <> 0 Then
                LblStatus.Text = "Write Fail: " + result.ToString
            Else
                LblStatus.Text = "Write Success - Macros Off"

            End If
        End If
    End Sub

    
    Private Sub BtnDeadzone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDeadzone.Click
        'Sets the sensitivity of the "deadzone" of the switched.  0 means no deadzone in the middle, higher number are a wider deadzone.
        'note this setting is not permanent, ie the unit will return to its default after hotplugging or reboot.

        If selecteddevice <> -1 Then

            Dim sw2dz As Integer = Convert.ToInt16(textBox7.Text) '5000 default
            Dim sw1dz As Integer = Convert.ToInt16(textBox6.Text) '5000 default

            For i As Integer = 0 To devices(selecteddevice).WriteLength - 1
                wdata(i) = 0
            Next
            wdata(1) = 190
            wdata(2) = (sw2dz And &HFF) 'low byte top switch
            wdata(3) = ((sw2dz >> 8) And &HFF) 'high byte top switch
            wdata(4) = (sw1dz And &HFF) 'low byte bottom switch
            wdata(5) = ((sw1dz >> 8) And &HFF) 'high byte bottom switch

            Dim result As Integer
            result = 404
            While (result = 404)
                result = devices(selecteddevice).WriteData(wdata)
            End While

            If result <> 0 Then
                LblStatus.Text = "Write Fail: " + result.ToString
            Else
                LblStatus.Text = "Write Success - Deadzone Sensitivity"

            End If
        End If

    End Sub
End Class
