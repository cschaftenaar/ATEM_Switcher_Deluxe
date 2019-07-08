﻿namespace NETComp_Csharp_Sample
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGreenLEDFlash = new System.Windows.Forms.Button();
            this.btnRedLEDFlash = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnGreenLED = new System.Windows.Forms.Button();
            this.btnRedLED = new System.Windows.Forms.Button();
            this.btnFrequency = new System.Windows.Forms.Button();
            this.spnFrequency = new System.Windows.Forms.NumericUpDown();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.btnJoystick = new System.Windows.Forms.Button();
            this.btnMouse = new System.Windows.Forms.Button();
            this.txtKeyboard = new System.Windows.Forms.TextBox();
            this.btnKeyboard = new System.Windows.Forms.Button();
            this.txtByte3 = new System.Windows.Forms.TextBox();
            this.txtByte2 = new System.Windows.Forms.TextBox();
            this.grpDongle = new System.Windows.Forms.GroupBox();
            this.txtByte4 = new System.Windows.Forms.TextBox();
            this.btnCheckDongle = new System.Windows.Forms.Button();
            this.btnSetDongle = new System.Windows.Forms.Button();
            this.txtByte1 = new System.Windows.Forms.TextBox();
            this.btn1221 = new System.Windows.Forms.Button();
            this.btn1224 = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.deviceStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblProductID = new System.Windows.Forms.Label();
            this.btnOEM = new System.Windows.Forms.Button();
            this.btnUID = new System.Windows.Forms.Button();
            this.txtUID = new System.Windows.Forms.TextBox();
            this.txtOEM = new System.Windows.Forms.TextBox();
            this.lblOEM = new System.Windows.Forms.Label();
            this.lblUID = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblButton05 = new System.Windows.Forms.Label();
            this.lblButton02 = new System.Windows.Forms.Label();
            this.lblButton01 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblDTime = new System.Windows.Forms.Label();
            this.lblATime = new System.Windows.Forms.Label();
            this.xk3SI_1 = new Xk3SI.Xk3SI_(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnFrequency)).BeginInit();
            this.groupBox11.SuspendLayout();
            this.grpDongle.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(168, 72);
            this.label6.TabIndex = 4;
            this.label6.Text = "Reflector messages send into the hardware, then back into the OS. Device must be " +
                "in a configuration that supports the kind of input one is expecting to reflect.";
            // 
            // btnGreenLEDFlash
            // 
            this.btnGreenLEDFlash.Location = new System.Drawing.Point(74, 17);
            this.btnGreenLEDFlash.Name = "btnGreenLEDFlash";
            this.btnGreenLEDFlash.Size = new System.Drawing.Size(76, 23);
            this.btnGreenLEDFlash.TabIndex = 19;
            this.btnGreenLEDFlash.Text = "Green Flash";
            this.btnGreenLEDFlash.UseVisualStyleBackColor = true;
            this.btnGreenLEDFlash.Click += new System.EventHandler(this.btnGreenLEDFlash_Click);
            // 
            // btnRedLEDFlash
            // 
            this.btnRedLEDFlash.Location = new System.Drawing.Point(75, 46);
            this.btnRedLEDFlash.Name = "btnRedLEDFlash";
            this.btnRedLEDFlash.Size = new System.Drawing.Size(76, 23);
            this.btnRedLEDFlash.TabIndex = 20;
            this.btnRedLEDFlash.Text = "Red Flash";
            this.btnRedLEDFlash.UseVisualStyleBackColor = true;
            this.btnRedLEDFlash.Click += new System.EventHandler(this.btnRedLEDFlash_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnRedLEDFlash);
            this.groupBox6.Controls.Add(this.btnGreenLEDFlash);
            this.groupBox6.Controls.Add(this.btnGreenLED);
            this.groupBox6.Controls.Add(this.btnRedLED);
            this.groupBox6.Location = new System.Drawing.Point(301, 2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(164, 80);
            this.groupBox6.TabIndex = 43;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Indicators";
            // 
            // btnGreenLED
            // 
            this.btnGreenLED.Location = new System.Drawing.Point(15, 17);
            this.btnGreenLED.Name = "btnGreenLED";
            this.btnGreenLED.Size = new System.Drawing.Size(50, 23);
            this.btnGreenLED.TabIndex = 18;
            this.btnGreenLED.Text = "Green";
            this.btnGreenLED.UseVisualStyleBackColor = true;
            this.btnGreenLED.Click += new System.EventHandler(this.btnGreenLED_Click);
            // 
            // btnRedLED
            // 
            this.btnRedLED.Location = new System.Drawing.Point(15, 46);
            this.btnRedLED.Name = "btnRedLED";
            this.btnRedLED.Size = new System.Drawing.Size(50, 23);
            this.btnRedLED.TabIndex = 17;
            this.btnRedLED.Text = "Red";
            this.btnRedLED.UseVisualStyleBackColor = true;
            this.btnRedLED.Click += new System.EventHandler(this.btnRedLED_Click);
            // 
            // btnFrequency
            // 
            this.btnFrequency.Location = new System.Drawing.Point(75, 17);
            this.btnFrequency.Name = "btnFrequency";
            this.btnFrequency.Size = new System.Drawing.Size(75, 23);
            this.btnFrequency.TabIndex = 11;
            this.btnFrequency.Text = "Set";
            this.btnFrequency.UseVisualStyleBackColor = true;
            this.btnFrequency.Click += new System.EventHandler(this.btnFrequency_Click);
            // 
            // spnFrequency
            // 
            this.spnFrequency.Location = new System.Drawing.Point(15, 18);
            this.spnFrequency.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spnFrequency.Name = "spnFrequency";
            this.spnFrequency.Size = new System.Drawing.Size(54, 20);
            this.spnFrequency.TabIndex = 5;
            this.spnFrequency.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.btnJoystick);
            this.groupBox11.Controls.Add(this.btnMouse);
            this.groupBox11.Controls.Add(this.txtKeyboard);
            this.groupBox11.Controls.Add(this.btnKeyboard);
            this.groupBox11.Controls.Add(this.label6);
            this.groupBox11.Location = new System.Drawing.Point(478, 88);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(181, 177);
            this.groupBox11.TabIndex = 47;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Reflector";
            // 
            // btnJoystick
            // 
            this.btnJoystick.Location = new System.Drawing.Point(10, 148);
            this.btnJoystick.Name = "btnJoystick";
            this.btnJoystick.Size = new System.Drawing.Size(97, 23);
            this.btnJoystick.TabIndex = 3;
            this.btnJoystick.Text = "Send Joystick";
            this.btnJoystick.UseVisualStyleBackColor = true;
            this.btnJoystick.Click += new System.EventHandler(this.btnJoystick_Click);
            // 
            // btnMouse
            // 
            this.btnMouse.Location = new System.Drawing.Point(10, 91);
            this.btnMouse.Name = "btnMouse";
            this.btnMouse.Size = new System.Drawing.Size(97, 23);
            this.btnMouse.TabIndex = 2;
            this.btnMouse.Text = "Send Mouse";
            this.btnMouse.UseVisualStyleBackColor = true;
            this.btnMouse.Click += new System.EventHandler(this.btnMouse_Click);
            // 
            // txtKeyboard
            // 
            this.txtKeyboard.Location = new System.Drawing.Point(113, 121);
            this.txtKeyboard.Name = "txtKeyboard";
            this.txtKeyboard.Size = new System.Drawing.Size(49, 20);
            this.txtKeyboard.TabIndex = 1;
            // 
            // btnKeyboard
            // 
            this.btnKeyboard.Location = new System.Drawing.Point(10, 119);
            this.btnKeyboard.Name = "btnKeyboard";
            this.btnKeyboard.Size = new System.Drawing.Size(97, 23);
            this.btnKeyboard.TabIndex = 0;
            this.btnKeyboard.Text = "Send Keyboard";
            this.btnKeyboard.UseVisualStyleBackColor = true;
            this.btnKeyboard.Click += new System.EventHandler(this.btnKeyboard_Click);
            // 
            // txtByte3
            // 
            this.txtByte3.Location = new System.Drawing.Point(114, 19);
            this.txtByte3.Name = "txtByte3";
            this.txtByte3.Size = new System.Drawing.Size(45, 20);
            this.txtByte3.TabIndex = 4;
            // 
            // txtByte2
            // 
            this.txtByte2.Location = new System.Drawing.Point(63, 19);
            this.txtByte2.Name = "txtByte2";
            this.txtByte2.Size = new System.Drawing.Size(45, 20);
            this.txtByte2.TabIndex = 5;
            // 
            // grpDongle
            // 
            this.grpDongle.Controls.Add(this.txtByte2);
            this.grpDongle.Controls.Add(this.txtByte3);
            this.grpDongle.Controls.Add(this.txtByte4);
            this.grpDongle.Controls.Add(this.btnCheckDongle);
            this.grpDongle.Controls.Add(this.btnSetDongle);
            this.grpDongle.Controls.Add(this.txtByte1);
            this.grpDongle.Location = new System.Drawing.Point(12, 149);
            this.grpDongle.Name = "grpDongle";
            this.grpDongle.Size = new System.Drawing.Size(327, 47);
            this.grpDongle.TabIndex = 46;
            this.grpDongle.TabStop = false;
            this.grpDongle.Text = "Security Key";
            // 
            // txtByte4
            // 
            this.txtByte4.Location = new System.Drawing.Point(165, 19);
            this.txtByte4.Name = "txtByte4";
            this.txtByte4.Size = new System.Drawing.Size(45, 20);
            this.txtByte4.TabIndex = 3;
            // 
            // btnCheckDongle
            // 
            this.btnCheckDongle.Location = new System.Drawing.Point(271, 17);
            this.btnCheckDongle.Name = "btnCheckDongle";
            this.btnCheckDongle.Size = new System.Drawing.Size(50, 23);
            this.btnCheckDongle.TabIndex = 2;
            this.btnCheckDongle.Text = "Check";
            this.btnCheckDongle.UseVisualStyleBackColor = true;
            this.btnCheckDongle.Click += new System.EventHandler(this.btnCheckDongle_Click);
            // 
            // btnSetDongle
            // 
            this.btnSetDongle.Location = new System.Drawing.Point(216, 17);
            this.btnSetDongle.Name = "btnSetDongle";
            this.btnSetDongle.Size = new System.Drawing.Size(50, 23);
            this.btnSetDongle.TabIndex = 1;
            this.btnSetDongle.Text = "Set";
            this.btnSetDongle.UseVisualStyleBackColor = true;
            this.btnSetDongle.Click += new System.EventHandler(this.btnSetDongle_Click);
            // 
            // txtByte1
            // 
            this.txtByte1.Location = new System.Drawing.Point(12, 19);
            this.txtByte1.Name = "txtByte1";
            this.txtByte1.Size = new System.Drawing.Size(45, 20);
            this.txtByte1.TabIndex = 0;
            // 
            // btn1221
            // 
            this.btn1221.Location = new System.Drawing.Point(7, 17);
            this.btn1221.Name = "btn1221";
            this.btn1221.Size = new System.Drawing.Size(53, 23);
            this.btn1221.TabIndex = 34;
            this.btn1221.Text = "1221";
            this.btn1221.UseVisualStyleBackColor = true;
            this.btn1221.Click += new System.EventHandler(this.btn1221_Click);
            // 
            // btn1224
            // 
            this.btn1224.Location = new System.Drawing.Point(66, 17);
            this.btn1224.Name = "btn1224";
            this.btn1224.Size = new System.Drawing.Size(51, 23);
            this.btn1224.TabIndex = 33;
            this.btn1224.Text = "1224";
            this.btn1224.UseVisualStyleBackColor = true;
            this.btn1224.Click += new System.EventHandler(this.btn1224_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.btn1224);
            this.groupBox9.Controls.Add(this.btn1221);
            this.groupBox9.Location = new System.Drawing.Point(342, 149);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(123, 47);
            this.groupBox9.TabIndex = 45;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "PID";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnFrequency);
            this.groupBox5.Controls.Add(this.spnFrequency);
            this.groupBox5.Location = new System.Drawing.Point(301, 88);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(164, 55);
            this.groupBox5.TabIndex = 40;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Flash Freq";
            // 
            // deviceStatus
            // 
            this.deviceStatus.Name = "deviceStatus";
            this.deviceStatus.Size = new System.Drawing.Size(118, 17);
            this.deviceStatus.Text = "toolStripStatusLabel1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblProductID);
            this.groupBox2.Controls.Add(this.btnOEM);
            this.groupBox2.Controls.Add(this.btnUID);
            this.groupBox2.Controls.Add(this.txtUID);
            this.groupBox2.Controls.Add(this.txtOEM);
            this.groupBox2.Controls.Add(this.lblOEM);
            this.groupBox2.Controls.Add(this.lblUID);
            this.groupBox2.Location = new System.Drawing.Point(12, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(281, 80);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Device IDs";
            // 
            // lblProductID
            // 
            this.lblProductID.AutoSize = true;
            this.lblProductID.Location = new System.Drawing.Point(6, 58);
            this.lblProductID.Name = "lblProductID";
            this.lblProductID.Size = new System.Drawing.Size(61, 13);
            this.lblProductID.TabIndex = 10;
            this.lblProductID.Text = "Product ID:";
            // 
            // btnOEM
            // 
            this.btnOEM.Location = new System.Drawing.Point(216, 35);
            this.btnOEM.Name = "btnOEM";
            this.btnOEM.Size = new System.Drawing.Size(49, 23);
            this.btnOEM.TabIndex = 9;
            this.btnOEM.Text = "Set";
            this.btnOEM.UseVisualStyleBackColor = true;
            this.btnOEM.Click += new System.EventHandler(this.btnOEM_Click);
            // 
            // btnUID
            // 
            this.btnUID.Location = new System.Drawing.Point(216, 12);
            this.btnUID.Name = "btnUID";
            this.btnUID.Size = new System.Drawing.Size(49, 23);
            this.btnUID.TabIndex = 7;
            this.btnUID.Text = "Set";
            this.btnUID.UseVisualStyleBackColor = true;
            this.btnUID.Click += new System.EventHandler(this.btnUID_Click);
            // 
            // txtUID
            // 
            this.txtUID.Location = new System.Drawing.Point(168, 15);
            this.txtUID.Name = "txtUID";
            this.txtUID.Size = new System.Drawing.Size(42, 20);
            this.txtUID.TabIndex = 7;
            // 
            // txtOEM
            // 
            this.txtOEM.Location = new System.Drawing.Point(136, 36);
            this.txtOEM.Name = "txtOEM";
            this.txtOEM.Size = new System.Drawing.Size(74, 20);
            this.txtOEM.TabIndex = 8;
            // 
            // lblOEM
            // 
            this.lblOEM.AutoSize = true;
            this.lblOEM.Location = new System.Drawing.Point(6, 39);
            this.lblOEM.Name = "lblOEM";
            this.lblOEM.Size = new System.Drawing.Size(48, 13);
            this.lblOEM.TabIndex = 6;
            this.lblOEM.Text = "OEM ID:";
            // 
            // lblUID
            // 
            this.lblUID.AutoSize = true;
            this.lblUID.Location = new System.Drawing.Point(6, 18);
            this.lblUID.Name = "lblUID";
            this.lblUID.Size = new System.Drawing.Size(43, 13);
            this.lblUID.TabIndex = 5;
            this.lblUID.Text = "Unit ID:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblButton05);
            this.groupBox1.Controls.Add(this.lblButton02);
            this.groupBox1.Controls.Add(this.lblButton01);
            this.groupBox1.Location = new System.Drawing.Point(478, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(181, 80);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Button Activity*";
            // 
            // lblButton05
            // 
            this.lblButton05.AutoSize = true;
            this.lblButton05.Location = new System.Drawing.Point(7, 57);
            this.lblButton05.Name = "lblButton05";
            this.lblButton05.Size = new System.Drawing.Size(31, 13);
            this.lblButton05.TabIndex = 4;
            this.lblButton05.Tag = "5";
            this.lblButton05.Text = "SW3";
            // 
            // lblButton02
            // 
            this.lblButton02.AutoSize = true;
            this.lblButton02.Location = new System.Drawing.Point(7, 39);
            this.lblButton02.Name = "lblButton02";
            this.lblButton02.Size = new System.Drawing.Size(31, 13);
            this.lblButton02.TabIndex = 1;
            this.lblButton02.Tag = "2";
            this.lblButton02.Text = "SW2";
            // 
            // lblButton01
            // 
            this.lblButton01.AutoSize = true;
            this.lblButton01.Location = new System.Drawing.Point(7, 21);
            this.lblButton01.Name = "lblButton01";
            this.lblButton01.Size = new System.Drawing.Size(31, 13);
            this.lblButton01.TabIndex = 0;
            this.lblButton01.Tag = "1";
            this.lblButton01.Text = "SW1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deviceStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 279);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(669, 22);
            this.statusStrip1.TabIndex = 35;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.btnGenerate);
            this.groupBox10.Location = new System.Drawing.Point(12, 88);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(110, 55);
            this.groupBox10.TabIndex = 48;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Other";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(6, 17);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(98, 25);
            this.btnGenerate.TabIndex = 24;
            this.btnGenerate.Text = "Generate Report";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblDTime);
            this.groupBox3.Controls.Add(this.lblATime);
            this.groupBox3.Location = new System.Drawing.Point(129, 88);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(164, 55);
            this.groupBox3.TabIndex = 49;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Time Stamp";
            // 
            // lblDTime
            // 
            this.lblDTime.AutoSize = true;
            this.lblDTime.Location = new System.Drawing.Point(7, 34);
            this.lblDTime.Name = "lblDTime";
            this.lblDTime.Size = new System.Drawing.Size(61, 13);
            this.lblDTime.TabIndex = 1;
            this.lblDTime.Text = "Delta Time:";
            // 
            // lblATime
            // 
            this.lblATime.AutoSize = true;
            this.lblATime.Location = new System.Drawing.Point(7, 18);
            this.lblATime.Name = "lblATime";
            this.lblATime.Size = new System.Drawing.Size(77, 13);
            this.lblATime.TabIndex = 0;
            this.lblATime.Text = "Absolute Time:";
            // 
            // xk3SI_1
            // 
            this.xk3SI_1.ContainerControl = this;
            this.xk3SI_1.Tag = null;
            this.xk3SI_1.ButtonChange += new Xk3SI.Xk3SI_.OnButtonHandler(this.xk3SI_1_ButtonChange);
            this.xk3SI_1.DevicePlug += new Xk3SI.Xk3SI_.OnDeviceChangeHandler(this.xk3SI_1_DevicePlug);
            this.xk3SI_1.DeviceUnplug += new Xk3SI.Xk3SI_.OnDeviceChangeHandler(this.xk3SI_1_DeviceUnplug);
            this.xk3SI_1.GenerateReportData += new Xk3SI.Xk3SI_.OnGenerateReportData(this.xk3SI_1_GenerateReportData);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 216);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(453, 49);
            this.label1.TabIndex = 11;
            this.label1.Text = "*Note: For mono switch only SW2 active, SW1 and SW3 are closed.  For stereo SW1 i" +
                "s right, SW2 is left and SW3 is closed.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 301);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox11);
            this.Controls.Add(this.grpDongle);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "XK-3 Switch Interface .NET Component C# Sample";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spnFrequency)).EndInit();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.grpDongle.ResumeLayout(false);
            this.grpDongle.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnGreenLEDFlash;
        private System.Windows.Forms.Button btnRedLEDFlash;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnGreenLED;
        private System.Windows.Forms.Button btnRedLED;
        private System.Windows.Forms.Button btnFrequency;
        private System.Windows.Forms.NumericUpDown spnFrequency;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Button btnJoystick;
        private System.Windows.Forms.Button btnMouse;
        private System.Windows.Forms.TextBox txtKeyboard;
        private System.Windows.Forms.Button btnKeyboard;
        private System.Windows.Forms.TextBox txtByte3;
        private System.Windows.Forms.TextBox txtByte2;
        private System.Windows.Forms.GroupBox grpDongle;
        private System.Windows.Forms.TextBox txtByte4;
        private System.Windows.Forms.Button btnCheckDongle;
        private System.Windows.Forms.Button btnSetDongle;
        private System.Windows.Forms.TextBox txtByte1;
        private System.Windows.Forms.Button btn1221;
        private System.Windows.Forms.Button btn1224;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ToolStripStatusLabel deviceStatus;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblProductID;
        private System.Windows.Forms.Button btnOEM;
        private System.Windows.Forms.Button btnUID;
        private System.Windows.Forms.TextBox txtUID;
        private System.Windows.Forms.TextBox txtOEM;
        private System.Windows.Forms.Label lblOEM;
        private System.Windows.Forms.Label lblUID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblButton05;
        private System.Windows.Forms.Label lblButton02;
        private System.Windows.Forms.Label lblButton01;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblATime;
        private System.Windows.Forms.Label lblDTime;
        private Xk3SI.Xk3SI_ xk3SI_1;
        private System.Windows.Forms.Label label1;
    }
}

