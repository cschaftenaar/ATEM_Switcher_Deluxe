﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.grpDongle = New System.Windows.Forms.GroupBox()
        Me.txtByte2 = New System.Windows.Forms.TextBox()
        Me.txtByte3 = New System.Windows.Forms.TextBox()
        Me.txtByte4 = New System.Windows.Forms.TextBox()
        Me.btnCheckDongle = New System.Windows.Forms.Button()
        Me.btnSetDongle = New System.Windows.Forms.Button()
        Me.txtByte1 = New System.Windows.Forms.TextBox()
        Me.groupBox9 = New System.Windows.Forms.GroupBox()
        Me.btn1195 = New System.Windows.Forms.Button()
        Me.btn1192 = New System.Windows.Forms.Button()
        Me.groupBox11 = New System.Windows.Forms.GroupBox()
        Me.btnJoystick = New System.Windows.Forms.Button()
        Me.btnMouse = New System.Windows.Forms.Button()
        Me.txtKeyboard = New System.Windows.Forms.TextBox()
        Me.btnKeyboard = New System.Windows.Forms.Button()
        Me.label6 = New System.Windows.Forms.Label()
        Me.groupBox5 = New System.Windows.Forms.GroupBox()
        Me.btnFrequency = New System.Windows.Forms.Button()
        Me.spnFrequency = New System.Windows.Forms.NumericUpDown()
        Me.groupBox6 = New System.Windows.Forms.GroupBox()
        Me.btnRedLEDFlash = New System.Windows.Forms.Button()
        Me.btnGreenLEDFlash = New System.Windows.Forms.Button()
        Me.btnGreenLED = New System.Windows.Forms.Button()
        Me.btnRedLED = New System.Windows.Forms.Button()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblButton12 = New System.Windows.Forms.Label()
        Me.lblButton11 = New System.Windows.Forms.Label()
        Me.lblButton10 = New System.Windows.Forms.Label()
        Me.lblButton09 = New System.Windows.Forms.Label()
        Me.lblButton08 = New System.Windows.Forms.Label()
        Me.lblButton07 = New System.Windows.Forms.Label()
        Me.lblButton06 = New System.Windows.Forms.Label()
        Me.lblButton05 = New System.Windows.Forms.Label()
        Me.lblButton04 = New System.Windows.Forms.Label()
        Me.lblButton03 = New System.Windows.Forms.Label()
        Me.lblButton02 = New System.Windows.Forms.Label()
        Me.lblButton01 = New System.Windows.Forms.Label()
        Me.groupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblProductID = New System.Windows.Forms.Label()
        Me.btnOEM = New System.Windows.Forms.Button()
        Me.btnUID = New System.Windows.Forms.Button()
        Me.txtUID = New System.Windows.Forms.TextBox()
        Me.txtOEM = New System.Windows.Forms.TextBox()
        Me.lblOEM = New System.Windows.Forms.Label()
        Me.lblUID = New System.Windows.Forms.Label()
        Me.statusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.deviceStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.groupBox10 = New System.Windows.Forms.GroupBox()
        Me.Xk12SI_1 = New Xk12SI.Xk12SI_(Me.components)
        Me.groupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblDTime = New System.Windows.Forms.Label()
        Me.lblATime = New System.Windows.Forms.Label()
        Me.grpDongle.SuspendLayout()
        Me.groupBox9.SuspendLayout()
        Me.groupBox11.SuspendLayout()
        Me.groupBox5.SuspendLayout()
        CType(Me.spnFrequency, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupBox6.SuspendLayout()
        Me.groupBox1.SuspendLayout()
        Me.groupBox2.SuspendLayout()
        Me.statusStrip1.SuspendLayout()
        Me.groupBox10.SuspendLayout()
        Me.groupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpDongle
        '
        Me.grpDongle.Controls.Add(Me.txtByte2)
        Me.grpDongle.Controls.Add(Me.txtByte3)
        Me.grpDongle.Controls.Add(Me.txtByte4)
        Me.grpDongle.Controls.Add(Me.btnCheckDongle)
        Me.grpDongle.Controls.Add(Me.btnSetDongle)
        Me.grpDongle.Controls.Add(Me.txtByte1)
        Me.grpDongle.Location = New System.Drawing.Point(12, 166)
        Me.grpDongle.Name = "grpDongle"
        Me.grpDongle.Size = New System.Drawing.Size(327, 56)
        Me.grpDongle.TabIndex = 47
        Me.grpDongle.TabStop = False
        Me.grpDongle.Text = "Security Key"
        '
        'txtByte2
        '
        Me.txtByte2.Location = New System.Drawing.Point(63, 19)
        Me.txtByte2.Name = "txtByte2"
        Me.txtByte2.Size = New System.Drawing.Size(45, 20)
        Me.txtByte2.TabIndex = 5
        '
        'txtByte3
        '
        Me.txtByte3.Location = New System.Drawing.Point(114, 19)
        Me.txtByte3.Name = "txtByte3"
        Me.txtByte3.Size = New System.Drawing.Size(45, 20)
        Me.txtByte3.TabIndex = 4
        '
        'txtByte4
        '
        Me.txtByte4.Location = New System.Drawing.Point(166, 19)
        Me.txtByte4.Name = "txtByte4"
        Me.txtByte4.Size = New System.Drawing.Size(45, 20)
        Me.txtByte4.TabIndex = 3
        '
        'btnCheckDongle
        '
        Me.btnCheckDongle.Location = New System.Drawing.Point(271, 17)
        Me.btnCheckDongle.Name = "btnCheckDongle"
        Me.btnCheckDongle.Size = New System.Drawing.Size(50, 23)
        Me.btnCheckDongle.TabIndex = 2
        Me.btnCheckDongle.Text = "Check"
        Me.btnCheckDongle.UseVisualStyleBackColor = True
        '
        'btnSetDongle
        '
        Me.btnSetDongle.Location = New System.Drawing.Point(217, 17)
        Me.btnSetDongle.Name = "btnSetDongle"
        Me.btnSetDongle.Size = New System.Drawing.Size(50, 23)
        Me.btnSetDongle.TabIndex = 1
        Me.btnSetDongle.Text = "Set"
        Me.btnSetDongle.UseVisualStyleBackColor = True
        '
        'txtByte1
        '
        Me.txtByte1.Location = New System.Drawing.Point(12, 19)
        Me.txtByte1.Name = "txtByte1"
        Me.txtByte1.Size = New System.Drawing.Size(45, 20)
        Me.txtByte1.TabIndex = 0
        '
        'groupBox9
        '
        Me.groupBox9.Controls.Add(Me.btn1195)
        Me.groupBox9.Controls.Add(Me.btn1192)
        Me.groupBox9.Location = New System.Drawing.Point(347, 167)
        Me.groupBox9.Name = "groupBox9"
        Me.groupBox9.Size = New System.Drawing.Size(123, 55)
        Me.groupBox9.TabIndex = 45
        Me.groupBox9.TabStop = False
        Me.groupBox9.Text = "PID"
        '
        'btn1195
        '
        Me.btn1195.Location = New System.Drawing.Point(66, 17)
        Me.btn1195.Name = "btn1195"
        Me.btn1195.Size = New System.Drawing.Size(51, 23)
        Me.btn1195.TabIndex = 33
        Me.btn1195.Text = "1195"
        Me.btn1195.UseVisualStyleBackColor = True
        '
        'btn1192
        '
        Me.btn1192.Location = New System.Drawing.Point(7, 17)
        Me.btn1192.Name = "btn1192"
        Me.btn1192.Size = New System.Drawing.Size(53, 23)
        Me.btn1192.TabIndex = 34
        Me.btn1192.Text = "1192"
        Me.btn1192.UseVisualStyleBackColor = True
        '
        'groupBox11
        '
        Me.groupBox11.Controls.Add(Me.btnJoystick)
        Me.groupBox11.Controls.Add(Me.btnMouse)
        Me.groupBox11.Controls.Add(Me.txtKeyboard)
        Me.groupBox11.Controls.Add(Me.btnKeyboard)
        Me.groupBox11.Controls.Add(Me.label6)
        Me.groupBox11.Location = New System.Drawing.Point(476, 149)
        Me.groupBox11.Name = "groupBox11"
        Me.groupBox11.Size = New System.Drawing.Size(181, 182)
        Me.groupBox11.TabIndex = 50
        Me.groupBox11.TabStop = False
        Me.groupBox11.Text = "Reflector"
        '
        'btnJoystick
        '
        Me.btnJoystick.Location = New System.Drawing.Point(10, 148)
        Me.btnJoystick.Name = "btnJoystick"
        Me.btnJoystick.Size = New System.Drawing.Size(97, 23)
        Me.btnJoystick.TabIndex = 3
        Me.btnJoystick.Text = "Send Joystick"
        Me.btnJoystick.UseVisualStyleBackColor = True
        '
        'btnMouse
        '
        Me.btnMouse.Location = New System.Drawing.Point(10, 90)
        Me.btnMouse.Name = "btnMouse"
        Me.btnMouse.Size = New System.Drawing.Size(97, 23)
        Me.btnMouse.TabIndex = 2
        Me.btnMouse.Text = "Send Mouse"
        Me.btnMouse.UseVisualStyleBackColor = True
        '
        'txtKeyboard
        '
        Me.txtKeyboard.Location = New System.Drawing.Point(113, 121)
        Me.txtKeyboard.Name = "txtKeyboard"
        Me.txtKeyboard.Size = New System.Drawing.Size(49, 20)
        Me.txtKeyboard.TabIndex = 1
        '
        'btnKeyboard
        '
        Me.btnKeyboard.Location = New System.Drawing.Point(10, 119)
        Me.btnKeyboard.Name = "btnKeyboard"
        Me.btnKeyboard.Size = New System.Drawing.Size(97, 23)
        Me.btnKeyboard.TabIndex = 0
        Me.btnKeyboard.Text = "Send Keyboard"
        Me.btnKeyboard.UseVisualStyleBackColor = True
        '
        'label6
        '
        Me.label6.Location = New System.Drawing.Point(7, 17)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(168, 102)
        Me.label6.TabIndex = 4
        Me.label6.Text = "Reflector messages send into the hardware, then back into the OS. Device must be " & _
            "in a configuration that supports the kind of input one is expecting to reflect."
        '
        'groupBox5
        '
        Me.groupBox5.Controls.Add(Me.btnFrequency)
        Me.groupBox5.Controls.Add(Me.spnFrequency)
        Me.groupBox5.Location = New System.Drawing.Point(299, 98)
        Me.groupBox5.Name = "groupBox5"
        Me.groupBox5.Size = New System.Drawing.Size(169, 55)
        Me.groupBox5.TabIndex = 44
        Me.groupBox5.TabStop = False
        Me.groupBox5.Text = "Flash Freq"
        '
        'btnFrequency
        '
        Me.btnFrequency.Location = New System.Drawing.Point(73, 17)
        Me.btnFrequency.Name = "btnFrequency"
        Me.btnFrequency.Size = New System.Drawing.Size(76, 23)
        Me.btnFrequency.TabIndex = 11
        Me.btnFrequency.Text = "Set"
        Me.btnFrequency.UseVisualStyleBackColor = True
        '
        'spnFrequency
        '
        Me.spnFrequency.Location = New System.Drawing.Point(15, 18)
        Me.spnFrequency.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.spnFrequency.Name = "spnFrequency"
        Me.spnFrequency.Size = New System.Drawing.Size(54, 20)
        Me.spnFrequency.TabIndex = 5
        Me.spnFrequency.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'groupBox6
        '
        Me.groupBox6.Controls.Add(Me.btnRedLEDFlash)
        Me.groupBox6.Controls.Add(Me.btnGreenLEDFlash)
        Me.groupBox6.Controls.Add(Me.btnGreenLED)
        Me.groupBox6.Controls.Add(Me.btnRedLED)
        Me.groupBox6.Location = New System.Drawing.Point(299, 12)
        Me.groupBox6.Name = "groupBox6"
        Me.groupBox6.Size = New System.Drawing.Size(169, 80)
        Me.groupBox6.TabIndex = 41
        Me.groupBox6.TabStop = False
        Me.groupBox6.Text = "Indicators"
        '
        'btnRedLEDFlash
        '
        Me.btnRedLEDFlash.Location = New System.Drawing.Point(73, 48)
        Me.btnRedLEDFlash.Name = "btnRedLEDFlash"
        Me.btnRedLEDFlash.Size = New System.Drawing.Size(76, 23)
        Me.btnRedLEDFlash.TabIndex = 22
        Me.btnRedLEDFlash.Text = "Red Flash"
        Me.btnRedLEDFlash.UseVisualStyleBackColor = True
        '
        'btnGreenLEDFlash
        '
        Me.btnGreenLEDFlash.Location = New System.Drawing.Point(73, 19)
        Me.btnGreenLEDFlash.Name = "btnGreenLEDFlash"
        Me.btnGreenLEDFlash.Size = New System.Drawing.Size(76, 23)
        Me.btnGreenLEDFlash.TabIndex = 21
        Me.btnGreenLEDFlash.Text = "Green Flash"
        Me.btnGreenLEDFlash.UseVisualStyleBackColor = True
        '
        'btnGreenLED
        '
        Me.btnGreenLED.Location = New System.Drawing.Point(17, 19)
        Me.btnGreenLED.Name = "btnGreenLED"
        Me.btnGreenLED.Size = New System.Drawing.Size(50, 23)
        Me.btnGreenLED.TabIndex = 18
        Me.btnGreenLED.Text = "Green"
        Me.btnGreenLED.UseVisualStyleBackColor = True
        '
        'btnRedLED
        '
        Me.btnRedLED.Location = New System.Drawing.Point(17, 48)
        Me.btnRedLED.Name = "btnRedLED"
        Me.btnRedLED.Size = New System.Drawing.Size(50, 23)
        Me.btnRedLED.TabIndex = 17
        Me.btnRedLED.Text = "Red"
        Me.btnRedLED.UseVisualStyleBackColor = True
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.lblButton12)
        Me.groupBox1.Controls.Add(Me.lblButton11)
        Me.groupBox1.Controls.Add(Me.lblButton10)
        Me.groupBox1.Controls.Add(Me.lblButton09)
        Me.groupBox1.Controls.Add(Me.lblButton08)
        Me.groupBox1.Controls.Add(Me.lblButton07)
        Me.groupBox1.Controls.Add(Me.lblButton06)
        Me.groupBox1.Controls.Add(Me.lblButton05)
        Me.groupBox1.Controls.Add(Me.lblButton04)
        Me.groupBox1.Controls.Add(Me.lblButton03)
        Me.groupBox1.Controls.Add(Me.lblButton02)
        Me.groupBox1.Controls.Add(Me.lblButton01)
        Me.groupBox1.Location = New System.Drawing.Point(476, 12)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(181, 135)
        Me.groupBox1.TabIndex = 40
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "Button Activity"
        '
        'lblButton12
        '
        Me.lblButton12.AutoSize = True
        Me.lblButton12.Location = New System.Drawing.Point(97, 116)
        Me.lblButton12.Name = "lblButton12"
        Me.lblButton12.Size = New System.Drawing.Size(47, 13)
        Me.lblButton12.TabIndex = 23
        Me.lblButton12.Tag = "12"
        Me.lblButton12.Text = "Jack 6R"
        '
        'lblButton11
        '
        Me.lblButton11.AutoSize = True
        Me.lblButton11.Location = New System.Drawing.Point(97, 97)
        Me.lblButton11.Name = "lblButton11"
        Me.lblButton11.Size = New System.Drawing.Size(45, 13)
        Me.lblButton11.TabIndex = 22
        Me.lblButton11.Tag = "11"
        Me.lblButton11.Text = "Jack 6L"
        '
        'lblButton10
        '
        Me.lblButton10.AutoSize = True
        Me.lblButton10.Location = New System.Drawing.Point(97, 78)
        Me.lblButton10.Name = "lblButton10"
        Me.lblButton10.Size = New System.Drawing.Size(45, 13)
        Me.lblButton10.TabIndex = 21
        Me.lblButton10.Tag = "10"
        Me.lblButton10.Text = "Jack 5L"
        '
        'lblButton09
        '
        Me.lblButton09.AutoSize = True
        Me.lblButton09.Location = New System.Drawing.Point(97, 59)
        Me.lblButton09.Name = "lblButton09"
        Me.lblButton09.Size = New System.Drawing.Size(47, 13)
        Me.lblButton09.TabIndex = 20
        Me.lblButton09.Tag = "9"
        Me.lblButton09.Text = "Jack 5R"
        '
        'lblButton08
        '
        Me.lblButton08.AutoSize = True
        Me.lblButton08.Location = New System.Drawing.Point(97, 40)
        Me.lblButton08.Name = "lblButton08"
        Me.lblButton08.Size = New System.Drawing.Size(45, 13)
        Me.lblButton08.TabIndex = 19
        Me.lblButton08.Tag = "8"
        Me.lblButton08.Text = "Jack 4L"
        '
        'lblButton07
        '
        Me.lblButton07.AutoSize = True
        Me.lblButton07.Location = New System.Drawing.Point(97, 23)
        Me.lblButton07.Name = "lblButton07"
        Me.lblButton07.Size = New System.Drawing.Size(47, 13)
        Me.lblButton07.TabIndex = 18
        Me.lblButton07.Tag = "7"
        Me.lblButton07.Text = "Jack 4R"
        '
        'lblButton06
        '
        Me.lblButton06.AutoSize = True
        Me.lblButton06.Location = New System.Drawing.Point(16, 117)
        Me.lblButton06.Name = "lblButton06"
        Me.lblButton06.Size = New System.Drawing.Size(45, 13)
        Me.lblButton06.TabIndex = 17
        Me.lblButton06.Tag = "6"
        Me.lblButton06.Text = "Jack 3L"
        '
        'lblButton05
        '
        Me.lblButton05.AutoSize = True
        Me.lblButton05.Location = New System.Drawing.Point(16, 98)
        Me.lblButton05.Name = "lblButton05"
        Me.lblButton05.Size = New System.Drawing.Size(47, 13)
        Me.lblButton05.TabIndex = 16
        Me.lblButton05.Tag = "5"
        Me.lblButton05.Text = "Jack 3R"
        '
        'lblButton04
        '
        Me.lblButton04.AutoSize = True
        Me.lblButton04.Location = New System.Drawing.Point(16, 79)
        Me.lblButton04.Name = "lblButton04"
        Me.lblButton04.Size = New System.Drawing.Size(45, 13)
        Me.lblButton04.TabIndex = 15
        Me.lblButton04.Tag = "4"
        Me.lblButton04.Text = "Jack 2L"
        '
        'lblButton03
        '
        Me.lblButton03.AutoSize = True
        Me.lblButton03.Location = New System.Drawing.Point(16, 60)
        Me.lblButton03.Name = "lblButton03"
        Me.lblButton03.Size = New System.Drawing.Size(47, 13)
        Me.lblButton03.TabIndex = 14
        Me.lblButton03.Tag = "3"
        Me.lblButton03.Text = "Jack 2R"
        '
        'lblButton02
        '
        Me.lblButton02.AutoSize = True
        Me.lblButton02.Location = New System.Drawing.Point(16, 41)
        Me.lblButton02.Name = "lblButton02"
        Me.lblButton02.Size = New System.Drawing.Size(45, 13)
        Me.lblButton02.TabIndex = 13
        Me.lblButton02.Tag = "2"
        Me.lblButton02.Text = "Jack 1L"
        '
        'lblButton01
        '
        Me.lblButton01.AutoSize = True
        Me.lblButton01.Location = New System.Drawing.Point(16, 22)
        Me.lblButton01.Name = "lblButton01"
        Me.lblButton01.Size = New System.Drawing.Size(47, 13)
        Me.lblButton01.TabIndex = 12
        Me.lblButton01.Tag = "1"
        Me.lblButton01.Text = "Jack 1R"
        '
        'groupBox2
        '
        Me.groupBox2.Controls.Add(Me.lblProductID)
        Me.groupBox2.Controls.Add(Me.btnOEM)
        Me.groupBox2.Controls.Add(Me.btnUID)
        Me.groupBox2.Controls.Add(Me.txtUID)
        Me.groupBox2.Controls.Add(Me.txtOEM)
        Me.groupBox2.Controls.Add(Me.lblOEM)
        Me.groupBox2.Controls.Add(Me.lblUID)
        Me.groupBox2.Location = New System.Drawing.Point(12, 12)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(281, 80)
        Me.groupBox2.TabIndex = 39
        Me.groupBox2.TabStop = False
        Me.groupBox2.Text = "Device IDs"
        '
        'lblProductID
        '
        Me.lblProductID.AutoSize = True
        Me.lblProductID.Location = New System.Drawing.Point(10, 62)
        Me.lblProductID.Name = "lblProductID"
        Me.lblProductID.Size = New System.Drawing.Size(61, 13)
        Me.lblProductID.TabIndex = 10
        Me.lblProductID.Text = "Product ID:"
        '
        'btnOEM
        '
        Me.btnOEM.Location = New System.Drawing.Point(217, 35)
        Me.btnOEM.Name = "btnOEM"
        Me.btnOEM.Size = New System.Drawing.Size(49, 23)
        Me.btnOEM.TabIndex = 9
        Me.btnOEM.Text = "Set"
        Me.btnOEM.UseVisualStyleBackColor = True
        '
        'btnUID
        '
        Me.btnUID.Location = New System.Drawing.Point(217, 13)
        Me.btnUID.Name = "btnUID"
        Me.btnUID.Size = New System.Drawing.Size(49, 23)
        Me.btnUID.TabIndex = 7
        Me.btnUID.Text = "Set"
        Me.btnUID.UseVisualStyleBackColor = True
        '
        'txtUID
        '
        Me.txtUID.Location = New System.Drawing.Point(169, 15)
        Me.txtUID.Name = "txtUID"
        Me.txtUID.Size = New System.Drawing.Size(42, 20)
        Me.txtUID.TabIndex = 7
        '
        'txtOEM
        '
        Me.txtOEM.Location = New System.Drawing.Point(137, 37)
        Me.txtOEM.Name = "txtOEM"
        Me.txtOEM.Size = New System.Drawing.Size(74, 20)
        Me.txtOEM.TabIndex = 8
        '
        'lblOEM
        '
        Me.lblOEM.AutoSize = True
        Me.lblOEM.Location = New System.Drawing.Point(10, 40)
        Me.lblOEM.Name = "lblOEM"
        Me.lblOEM.Size = New System.Drawing.Size(48, 13)
        Me.lblOEM.TabIndex = 6
        Me.lblOEM.Text = "OEM ID:"
        '
        'lblUID
        '
        Me.lblUID.AutoSize = True
        Me.lblUID.Location = New System.Drawing.Point(10, 18)
        Me.lblUID.Name = "lblUID"
        Me.lblUID.Size = New System.Drawing.Size(43, 13)
        Me.lblUID.TabIndex = 5
        Me.lblUID.Text = "Unit ID:"
        '
        'statusStrip1
        '
        Me.statusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.deviceStatus})
        Me.statusStrip1.Location = New System.Drawing.Point(0, 341)
        Me.statusStrip1.Name = "statusStrip1"
        Me.statusStrip1.Size = New System.Drawing.Size(670, 22)
        Me.statusStrip1.TabIndex = 51
        Me.statusStrip1.Text = "statusStrip1"
        '
        'deviceStatus
        '
        Me.deviceStatus.Name = "deviceStatus"
        Me.deviceStatus.Size = New System.Drawing.Size(118, 17)
        Me.deviceStatus.Text = "toolStripStatusLabel1"
        '
        'btnGenerate
        '
        Me.btnGenerate.Location = New System.Drawing.Point(6, 17)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(98, 25)
        Me.btnGenerate.TabIndex = 24
        Me.btnGenerate.Text = "Generate Report"
        Me.btnGenerate.UseVisualStyleBackColor = True
        '
        'groupBox10
        '
        Me.groupBox10.Controls.Add(Me.btnGenerate)
        Me.groupBox10.Location = New System.Drawing.Point(12, 98)
        Me.groupBox10.Name = "groupBox10"
        Me.groupBox10.Size = New System.Drawing.Size(110, 55)
        Me.groupBox10.TabIndex = 53
        Me.groupBox10.TabStop = False
        Me.groupBox10.Text = "Other"
        '
        'Xk12SI_1
        '
        Me.Xk12SI_1.ContainerControl = Me
        Me.Xk12SI_1.Tag = Nothing
        '
        'groupBox3
        '
        Me.groupBox3.Controls.Add(Me.lblDTime)
        Me.groupBox3.Controls.Add(Me.lblATime)
        Me.groupBox3.Location = New System.Drawing.Point(129, 98)
        Me.groupBox3.Name = "groupBox3"
        Me.groupBox3.Size = New System.Drawing.Size(164, 55)
        Me.groupBox3.TabIndex = 54
        Me.groupBox3.TabStop = False
        Me.groupBox3.Text = "Time Stamp"
        '
        'lblDTime
        '
        Me.lblDTime.AutoSize = True
        Me.lblDTime.Location = New System.Drawing.Point(7, 34)
        Me.lblDTime.Name = "lblDTime"
        Me.lblDTime.Size = New System.Drawing.Size(61, 13)
        Me.lblDTime.TabIndex = 1
        Me.lblDTime.Text = "Delta Time:"
        '
        'lblATime
        '
        Me.lblATime.AutoSize = True
        Me.lblATime.Location = New System.Drawing.Point(7, 18)
        Me.lblATime.Name = "lblATime"
        Me.lblATime.Size = New System.Drawing.Size(77, 13)
        Me.lblATime.TabIndex = 0
        Me.lblATime.Text = "Absolute Time:"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(670, 363)
        Me.Controls.Add(Me.groupBox3)
        Me.Controls.Add(Me.groupBox10)
        Me.Controls.Add(Me.statusStrip1)
        Me.Controls.Add(Me.grpDongle)
        Me.Controls.Add(Me.groupBox9)
        Me.Controls.Add(Me.groupBox11)
        Me.Controls.Add(Me.groupBox5)
        Me.Controls.Add(Me.groupBox6)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.groupBox2)
        Me.Name = "Form1"
        Me.Text = "XK-12 Switch Interface .NET Component VB Sample"
        Me.grpDongle.ResumeLayout(False)
        Me.grpDongle.PerformLayout()
        Me.groupBox9.ResumeLayout(False)
        Me.groupBox11.ResumeLayout(False)
        Me.groupBox11.PerformLayout()
        Me.groupBox5.ResumeLayout(False)
        CType(Me.spnFrequency, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupBox6.ResumeLayout(False)
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        Me.groupBox2.ResumeLayout(False)
        Me.groupBox2.PerformLayout()
        Me.statusStrip1.ResumeLayout(False)
        Me.statusStrip1.PerformLayout()
        Me.groupBox10.ResumeLayout(False)
        Me.groupBox3.ResumeLayout(False)
        Me.groupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents grpDongle As System.Windows.Forms.GroupBox
    Private WithEvents txtByte2 As System.Windows.Forms.TextBox
    Private WithEvents txtByte3 As System.Windows.Forms.TextBox
    Private WithEvents txtByte4 As System.Windows.Forms.TextBox
    Private WithEvents btnCheckDongle As System.Windows.Forms.Button
    Private WithEvents btnSetDongle As System.Windows.Forms.Button
    Private WithEvents txtByte1 As System.Windows.Forms.TextBox
    Private WithEvents groupBox9 As System.Windows.Forms.GroupBox
    Private WithEvents btn1195 As System.Windows.Forms.Button
    Private WithEvents btn1192 As System.Windows.Forms.Button
    Private WithEvents groupBox11 As System.Windows.Forms.GroupBox
    Private WithEvents btnJoystick As System.Windows.Forms.Button
    Private WithEvents btnMouse As System.Windows.Forms.Button
    Private WithEvents txtKeyboard As System.Windows.Forms.TextBox
    Private WithEvents btnKeyboard As System.Windows.Forms.Button
    Private WithEvents label6 As System.Windows.Forms.Label
    Private WithEvents groupBox5 As System.Windows.Forms.GroupBox
    Private WithEvents btnFrequency As System.Windows.Forms.Button
    Private WithEvents spnFrequency As System.Windows.Forms.NumericUpDown
    Private WithEvents groupBox6 As System.Windows.Forms.GroupBox
    Private WithEvents btnRedLEDFlash As System.Windows.Forms.Button
    Private WithEvents btnGreenLEDFlash As System.Windows.Forms.Button
    Private WithEvents btnGreenLED As System.Windows.Forms.Button
    Private WithEvents btnRedLED As System.Windows.Forms.Button
    Private WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Private WithEvents groupBox2 As System.Windows.Forms.GroupBox
    Private WithEvents lblProductID As System.Windows.Forms.Label
    Private WithEvents btnOEM As System.Windows.Forms.Button
    Private WithEvents btnUID As System.Windows.Forms.Button
    Private WithEvents txtUID As System.Windows.Forms.TextBox
    Private WithEvents txtOEM As System.Windows.Forms.TextBox
    Private WithEvents lblOEM As System.Windows.Forms.Label
    Private WithEvents lblUID As System.Windows.Forms.Label
    Private WithEvents statusStrip1 As System.Windows.Forms.StatusStrip
    Private WithEvents deviceStatus As System.Windows.Forms.ToolStripStatusLabel
    Private WithEvents groupBox10 As System.Windows.Forms.GroupBox
    Private WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents Xk12SI_1 As Xk12SI.Xk12SI_
    Private WithEvents lblButton12 As System.Windows.Forms.Label
    Private WithEvents lblButton11 As System.Windows.Forms.Label
    Private WithEvents lblButton10 As System.Windows.Forms.Label
    Private WithEvents lblButton09 As System.Windows.Forms.Label
    Private WithEvents lblButton08 As System.Windows.Forms.Label
    Private WithEvents lblButton07 As System.Windows.Forms.Label
    Private WithEvents lblButton06 As System.Windows.Forms.Label
    Private WithEvents lblButton05 As System.Windows.Forms.Label
    Private WithEvents lblButton04 As System.Windows.Forms.Label
    Private WithEvents lblButton03 As System.Windows.Forms.Label
    Private WithEvents lblButton02 As System.Windows.Forms.Label
    Private WithEvents lblButton01 As System.Windows.Forms.Label
    Private WithEvents groupBox3 As System.Windows.Forms.GroupBox
    Private WithEvents lblDTime As System.Windows.Forms.Label
    Private WithEvents lblATime As System.Windows.Forms.Label

End Class
