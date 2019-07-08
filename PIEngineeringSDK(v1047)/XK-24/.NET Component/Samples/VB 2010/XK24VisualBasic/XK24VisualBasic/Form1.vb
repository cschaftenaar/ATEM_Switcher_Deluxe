Public Class Form1

    'The following is a sample showing how to interact with an XK-24 device using the X-keys XK-24 .NET component.

    'Variables to track the device lights, initial states here based on device defaults
    Dim IndicatorRedFlash As Boolean = False
    Dim IndicatorGreenFlash As Boolean = True
    Dim AllBlue As Boolean = True
    Dim AllRed As Boolean = True



    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (xk24_1.ConnectedDevices.Length > 0) Then
                deviceStatus.Text = "XK-24 Connected"
                deviceStatus.ForeColor = Color.Green

                'Gets the Unit ID from the device
                lblUID.Text = "Unit ID: " & xk24_1.GetDeviceUID(0).ToString()

                'Gets the OEM Version ID from the device
                lblOEM.Text = "OEM Version ID: " & xk24_1.GetOEMVersionID(0).ToString()

                'Gets the Product ID from the device
                lblProductID.Text = "Product ID: " & xk24_1.GetProductID(0).ToString()
            End If
        Catch ex As Exception
            deviceStatus.Text = "No XK-24 Connected"
            deviceStatus.ForeColor = Color.Red
        End Try


    End Sub

    Private Sub xk24_1_ButtonChange(ByVal e As Xk24.XKeyEventArgs) Handles xk24_1.ButtonChange
        'This method handles the button change event for the device

        'Gets the button number (CID) of the button that has changed state
        Dim ButtonNum As String = e.CID.ToString()
        ButtonNum = ButtonNum.Remove(0, 2)
        If e.PressState = True Then
            'button press
            Select Case e.CID
                Case 1001
                    lblButton01.Text = "01-dn"
                    Exit Select
                Case 1002
                    lblButton02.Text = "02-dn"
                    Exit Select
                Case 1003
                    lblButton03.Text = "03-dn"
                    Exit Select
                Case 1004
                    lblButton04.Text = "04-dn"
                    Exit Select
                Case 1005
                    lblButton05.Text = "05-dn"
                    Exit Select
                Case 1006
                    lblButton06.Text = "06-dn"
                    Exit Select
                Case 1007
                    lblButton07.Text = "07-dn"
                    Exit Select
                Case 1008
                    lblButton08.Text = "08-dn"
                    Exit Select
                Case 1009
                    lblButton09.Text = "09-dn"
                    Exit Select
                Case 1010
                    lblButton10.Text = "10-dn"
                    Exit Select
                Case 1011
                    lblButton11.Text = "11-dn"
                    Exit Select
                Case 1012
                    lblButton12.Text = "12-dn"
                    Exit Select
                Case 1013
                    lblButton13.Text = "13-dn"
                    Exit Select
                Case 1014
                    lblButton14.Text = "14-dn"
                    Exit Select
                Case 1015
                    lblButton15.Text = "15-dn"
                    Exit Select
                Case 1016
                    lblButton16.Text = "16-dn"
                    Exit Select
                Case 1017
                    lblButton17.Text = "17-dn"
                    Exit Select
                Case 1018
                    lblButton18.Text = "18-dn"
                    Exit Select
                Case 1019
                    lblButton19.Text = "19-dn"
                    Exit Select
                Case 1020
                    lblButton20.Text = "20-dn"
                    Exit Select
                Case 1021
                    lblButton21.Text = "21-dn"
                    Exit Select
                Case 1022
                    lblButton22.Text = "22-dn"
                    Exit Select
                Case 1023
                    lblButton23.Text = "23-dn"
                    Exit Select
                Case 1024
                    lblButton24.Text = "24-dn"
                    Exit Select
            End Select
        Else
            'button release
            Select Case e.CID
                Case 1001
                    lblButton01.Text = "01-up"
                    Exit Select
                Case 1002
                    lblButton02.Text = "02-up"
                    Exit Select
                Case 1003
                    lblButton03.Text = "03-up"
                    Exit Select
                Case 1004
                    lblButton04.Text = "04-up"
                    Exit Select
                Case 1005
                    lblButton05.Text = "05-up"
                    Exit Select
                Case 1006
                    lblButton06.Text = "06-up"
                    Exit Select
                Case 1007
                    lblButton07.Text = "07-up"
                    Exit Select
                Case 1008
                    lblButton08.Text = "08-up"
                    Exit Select
                Case 1009
                    lblButton09.Text = "09-up"
                    Exit Select
                Case 1010
                    lblButton10.Text = "10-up"
                    Exit Select
                Case 1011
                    lblButton11.Text = "11-up"
                    Exit Select
                Case 1012
                    lblButton12.Text = "12-up"
                    Exit Select
                Case 1013
                    lblButton13.Text = "13-up"
                    Exit Select
                Case 1014
                    lblButton14.Text = "14-up"
                    Exit Select
                Case 1015
                    lblButton15.Text = "15-up"
                    Exit Select
                Case 1016
                    lblButton16.Text = "16-up"
                    Exit Select
                Case 1017
                    lblButton17.Text = "17-up"
                    Exit Select
                Case 1018
                    lblButton18.Text = "18-up"
                    Exit Select
                Case 1019
                    lblButton19.Text = "19-up"
                    Exit Select
                Case 1020
                    lblButton20.Text = "20-up"
                    Exit Select
                Case 1021
                    lblButton21.Text = "21-up"
                    Exit Select
                Case 1022
                    lblButton22.Text = "22-up"
                    Exit Select
                Case 1023
                    lblButton23.Text = "23-up"
                    Exit Select
                Case 1024
                    lblButton24.Text = "24-up"
                    Exit Select
            End Select
        End If
        lblUID.Text = "Unit ID: " + xk24_1.ConnectedDevices(0).UnitID.ToString()

    End Sub


    Private Sub btnGreenLED_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGreenLED.Click
        'Toggles the green indicator LED
        If (xk24_1.GetGreenIndicatorLED(0) = False) Then
            xk24_1.SetGreenIndicator(0, 1)
        Else
            xk24_1.SetGreenIndicator(0, 0)
        End If
    End Sub

    Private Sub btnRedLED_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRedLED.Click
        'Toggles the red indicator LED
        If (xk24_1.GetRedIndicatorLED(0) = False) Then
            xk24_1.SetRedIndicator(0, 1)
        Else
            xk24_1.SetRedIndicator(0, 0)
        End If
    End Sub

    Private Sub btnGreenLEDFlash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGreenLEDFlash.Click
        'Toggles the green indicator LED between flashing and on
        If IndicatorGreenFlash = False Then
            xk24_1.SetGreenIndicator(0, 2)
            IndicatorGreenFlash = True
        Else
            xk24_1.SetGreenIndicator(0, 1)
            IndicatorGreenFlash = False
        End If
    End Sub

    Private Sub btnRedLEDFlash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRedLEDFlash.Click
        'Toggles the red indicator LED between flashing and off
        If IndicatorRedFlash = False Then
            xk24_1.SetRedIndicator(0, 2)
            IndicatorRedFlash = True
        Else
            xk24_1.SetRedIndicator(0, 0)
            IndicatorRedFlash = False
        End If
    End Sub

    Private Sub btnBlue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBlue.Click
        Dim LightState As Integer

        If (cboBacklightStatus.Text = "On") Then
            LightState = 1
        ElseIf (cboBacklightStatus.Text = "Off") Then
            LightState = 0
        ElseIf (cboBacklightStatus.Text = "Flash") Then
            LightState = 2
        Else
            LightState = 1
        End If

        Dim ButtonID As Integer = Convert.ToInt32(spnButton.Value)

        'Sets an individual LED (on bank 0) based on chosen ButtonID and LightState
        xk24_1.SetBacklightLED(0, ButtonID, 0, LightState)
    End Sub
    Private Sub btnRed_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRed.Click
        Dim LightState As Integer

        If (cboBacklightStatus.Text = "On") Then
            LightState = 1
        ElseIf (cboBacklightStatus.Text = "Off") Then
            LightState = 0
        ElseIf (cboBacklightStatus.Text = "Flash") Then
            LightState = 2
        Else
            LightState = 1
        End If

        Dim ButtonID As Integer = Convert.ToInt32(spnButton.Value)

        'Sets an individual LED (on bank 0) based on chosen ButtonID and LightState
        xk24_1.SetBacklightLED(0, ButtonID, 1, LightState)
    End Sub

    Private Sub btnSaveState_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveState.Click
        'Saves backlighting state to the memory of the device, when called the current state of the backlights will be the default state when device is booted
        xk24_1.SaveBacklightState(0)
    End Sub

    Private Sub btnUID_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUID.Click
        Dim id As Integer = Convert.ToInt32(txtUID.Text)

        'Sets device Unit ID
        xk24_1.SetDeviceUID(0, id)

    End Sub

    Private Sub btnOEM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOEM.Click
        Dim id = Convert.ToInt32(txtOEM.Text)

        'Sets device OEM Version ID
        xk24_1.SetOEMVersionID(0, id)
    End Sub

    Private Sub btnIntensity_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIntensity.Click

        Dim Intensity1 As Integer = Convert.ToInt32(spnBlueIntensity.Value)
        Dim Intensity2 As Integer = Convert.ToInt32(spnRedIntensity.Value)

        'Sets the intensity for both backlighting banks
        xk24_1.SetBacklightIntensity(0, Intensity1, Intensity2)
    End Sub

    Private Sub btnFrequency_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFrequency.Click
        Dim Frequency As Integer = Convert.ToInt32(spnFrequency.Value)

        'Sets the flash frequency for the device
        xk24_1.SetFlashFrequency(0, Frequency)
    End Sub

    Private Sub btnRowsBlue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRowsBlue.Click
        Dim row1, row2, row3, row4, row5, row6 As Boolean

        If (cboRow1.Text = "On") Then
            row1 = True
        Else
            row1 = False
        End If

        If (cboRow2.Text = "On") Then
            row2 = True
        Else
            row2 = False
        End If

        If (cboRow3.Text = "On") Then
            row3 = True
        Else
            row3 = False
        End If

        If (cboRow4.Text = "On") Then
            row4 = True
        Else
            row4 = False
        End If

        If (cboRow5.Text = "On") Then
            row5 = True
        Else
            row5 = False
        End If

        If (cboRow6.Text = "On") Then
            row6 = True
        Else
            row6 = False
        End If

        'Sets individual rows of backlights on bank 0
        xk24_1.SetRowsOfBacklights(0, 0, row1, row2, row3, row4, row5, row6)
    End Sub

    Private Sub btnRowsRed_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRowsRed.Click
        Dim row1, row2, row3, row4, row5, row6 As Boolean

        If (cboRow1.Text = "On") Then
            row1 = True
        Else
            row1 = False
        End If

        If (cboRow2.Text = "On") Then
            row2 = True
        Else
            row2 = False
        End If

        If (cboRow3.Text = "On") Then
            row3 = True
        Else
            row3 = False
        End If

        If (cboRow4.Text = "On") Then
            row4 = True
        Else
            row4 = False
        End If

        If (cboRow5.Text = "On") Then
            row5 = True
        Else
            row5 = False
        End If

        If (cboRow6.Text = "On") Then
            row6 = True
        Else
            row6 = False
        End If

        'Sets individual rows of backlights on bank 0
        xk24_1.SetRowsOfBacklights(0, 1, row1, row2, row3, row4, row5, row6)
    End Sub

    Private Sub btnToggleAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnToggleAll.Click
        'Toggles backlighting on and off
        xk24_1.ToggleBacklights(0)
    End Sub

    Private Sub btnToggleBlue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnToggleBlue.Click
        'Toggles blue bank
        If (AllBlue = True) Then
            xk24_1.SetAllBlue(0, False)
            AllBlue = False
        Else
            xk24_1.SetAllBlue(0, True)
            AllBlue = True
        End If
    End Sub

    Private Sub btnToggleRed_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnToggleRed.Click
        'Toggles red bank
        If (AllRed = True) Then
            xk24_1.SetAllRed(0, False)
            AllRed = False
        Else
            xk24_1.SetAllRed(0, True)
            AllRed = True
        End If
    End Sub

    Private Sub btn1029_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn1029.Click
        'Changes device to the 1029 Product ID (device will "replug" itself)
        'Change to this Product ID if you desire Keyboard and Mouse endpoints
        xk24_1.ChangePID(0, 1029)
    End Sub

    Private Sub btn1027_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn1027.Click
        'Changes device to the 1027 Product ID (device will "replug" itself)
        'Change to this Product ID if you desire Keyboard and Joystick endpoints
        xk24_1.ChangePID(0, 1027)
    End Sub

    Private Sub btnKeyboard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKeyboard.Click
        txtKeyboard.Focus()
        'Sends the letters 'a' 'b' and 'c'
        xk24_1.SendKeystrokes(0, False, False, False, False, False, False, False, False, 4, 5, 6, 0, 0, 0)
        'Releases all sent keys
        xk24_1.SendKeystrokes(0, False, False, False, False, False, False, False, False, 0, 0, 0, 0, 0, 0)
    End Sub

    Private Sub btnMouse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMouse.Click
        'Moves mouse cursor right and up from its current position
        'Mouse endpoint required therefore must be in Product ID 1029 to work
        If (xk24_1.ConnectedDevices(0).MouseInterface = True) Then
            xk24_1.SendMouse(0, False, False, False, False, False, 20, 20, 0)
        Else
            MessageBox.Show("No mouse endpoint available, change to pid 1029")
        End If
    End Sub

    Private Sub btnJoystick_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnJoystick.Click
        'Sends game controller message, open Game Controllers control panel to see
        'Joystick endpoint required therefore must be in Product ID 1027 to work
        If (xk24_1.ConnectedDevices(0).JoystickInterface = True) Then
            Dim buttons As Boolean() = {False, False, False, False, False, False, False, False, False, False, False, False, False, False, False, False, False, False, False, False, False, False, False, False, False, False, False, False, False, False, False, False}
            xk24_1.SendJoystick(0, 100, 100, 0, 0, 0, 8, buttons)
        Else
            MessageBox.Show("No joystick endpoint available, change to pid 1027")
        End If
    End Sub

    Private Sub btnCheckDongle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCheckDongle.Click
        'Enter the 4 bytes used when xk24_1.SetSecurityKey was called
        Dim byte1 As Byte = Convert.ToByte(txtByte1.Text)
        Dim byte2 As Byte = Convert.ToByte(txtByte2.Text)
        Dim byte3 As Byte = Convert.ToByte(txtByte3.Text)
        Dim byte4 As Byte = Convert.ToByte(txtByte4.Text)

        'Checks the security key by sending the selected bytes to the device; bytes were valid if it returns true
        Dim dongle As Boolean = xk24_1.GetSecurityKey(0, byte1, byte2, byte3, byte4)

        If (dongle = True) Then
            grpDongle.BackColor = Color.Green
        ElseIf (dongle = False) Then
            grpDongle.BackColor = Color.Red
        End If

    End Sub

    Private Sub btnSetDongle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSetDongle.Click
        'Enter in 4 bytes. REMEMBER these 4 bytes they will be required to check the dongle
        Dim byte1 As Byte = Convert.ToByte(txtByte1.Text)
        Dim byte2 As Byte = Convert.ToByte(txtByte2.Text)
        Dim byte3 As Byte = Convert.ToByte(txtByte3.Text)
        Dim byte4 As Byte = Convert.ToByte(txtByte4.Text)

        xk24_1.SetSecurityKey(0, byte1, byte2, byte3, byte4)
    End Sub

    Private Sub xk24_1_DevicePlug(ByVal e As Xk24.XKeyPlugEventArgs) Handles xk24_1.DevicePlug
        deviceStatus.Text = "XK-24 Plugged, Unit ID: " + e.UID.ToString() + ", OEM ID: " + e.OEMVersionID.ToString() + ", Product ID: " + e.PID.ToString()
        deviceStatus.ForeColor = Color.Green

        'Gets the Unit ID from the device
        lblUID.Text = "Unit ID: " + e.UID.ToString()

        'Gets the OEM Version ID from the device
        lblOEM.Text = "OEM ID: " + e.OEMVersionID.ToString()

        'Gets the Product ID from the device
        lblProductID.Text = "Product ID: " + e.PID.ToString()
    End Sub

    Private Sub xk24_1_DeviceUnplug(ByVal e As Xk24.XKeyPlugEventArgs) Handles xk24_1.DeviceUnplug
        deviceStatus.Text = "XK-24 Unplugged, Unit ID: " + e.UID.ToString() + ", OEM ID: " + e.OEMVersionID.ToString() + ", Product ID: " + e.PID.ToString()
        deviceStatus.ForeColor = Color.Red

        lblUID.Text = "Unit ID: "
        lblOEM.Text = "OEM ID: "
        lblProductID.Text = "Product ID: "
    End Sub

    
  
End Class
