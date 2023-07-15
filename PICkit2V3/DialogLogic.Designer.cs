﻿namespace PICkit2V3
{
	// Token: 0x02000043 RID: 67
	public partial class DialogLogic : global::System.Windows.Forms.Form
	{
		// Token: 0x0600026A RID: 618 RVA: 0x000483ED File Offset: 0x000473ED
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0004840C File Offset: 0x0004740C
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::PICkit2V3.DialogLogic));
			this.label2 = new global::System.Windows.Forms.Label();
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.radioButtonAnalyzer = new global::System.Windows.Forms.RadioButton();
			this.radioButtonLogicIO = new global::System.Windows.Forms.RadioButton();
			this.panelAnalyzer = new global::System.Windows.Forms.Panel();
			this.buttonSave = new global::System.Windows.Forms.Button();
			this.labelCursor1Val = new global::System.Windows.Forms.Label();
			this.labelCursor1 = new global::System.Windows.Forms.Label();
			this.labelCursorDelta = new global::System.Windows.Forms.Label();
			this.labelCursorDeltaVal = new global::System.Windows.Forms.Label();
			this.labelCursor2 = new global::System.Windows.Forms.Label();
			this.checkBoxCursors = new global::System.Windows.Forms.CheckBox();
			this.labelCursor2Val = new global::System.Windows.Forms.Label();
			this.label24 = new global::System.Windows.Forms.Label();
			this.label23 = new global::System.Windows.Forms.Label();
			this.label22 = new global::System.Windows.Forms.Label();
			this.groupBox2 = new global::System.Windows.Forms.GroupBox();
			this.label25 = new global::System.Windows.Forms.Label();
			this.radioButtonTrigDly3 = new global::System.Windows.Forms.RadioButton();
			this.radioButtonTrigDly2 = new global::System.Windows.Forms.RadioButton();
			this.radioButtonTrigDly1 = new global::System.Windows.Forms.RadioButton();
			this.radioButtonTrigEnd = new global::System.Windows.Forms.RadioButton();
			this.radioButtonTrigMid = new global::System.Windows.Forms.RadioButton();
			this.radioButtonTrigStart = new global::System.Windows.Forms.RadioButton();
			this.label21 = new global::System.Windows.Forms.Label();
			this.labelAliasFreq = new global::System.Windows.Forms.Label();
			this.comboBoxSampleRate = new global::System.Windows.Forms.ComboBox();
			this.label19 = new global::System.Windows.Forms.Label();
			this.buttonRun = new global::System.Windows.Forms.Button();
			this.pictureBox1 = new global::System.Windows.Forms.PictureBox();
			this.groupBox1 = new global::System.Windows.Forms.GroupBox();
			this.label18 = new global::System.Windows.Forms.Label();
			this.label17 = new global::System.Windows.Forms.Label();
			this.label15 = new global::System.Windows.Forms.Label();
			this.textBox1 = new global::System.Windows.Forms.TextBox();
			this.label5 = new global::System.Windows.Forms.Label();
			this.label16 = new global::System.Windows.Forms.Label();
			this.label6 = new global::System.Windows.Forms.Label();
			this.label7 = new global::System.Windows.Forms.Label();
			this.label14 = new global::System.Windows.Forms.Label();
			this.comboBoxCh1Trig = new global::System.Windows.Forms.ComboBox();
			this.label13 = new global::System.Windows.Forms.Label();
			this.comboBoxCh2Trig = new global::System.Windows.Forms.ComboBox();
			this.label12 = new global::System.Windows.Forms.Label();
			this.comboBoxCh3Trig = new global::System.Windows.Forms.ComboBox();
			this.label11 = new global::System.Windows.Forms.Label();
			this.label8 = new global::System.Windows.Forms.Label();
			this.label10 = new global::System.Windows.Forms.Label();
			this.label9 = new global::System.Windows.Forms.Label();
			this.groupBoxZoom = new global::System.Windows.Forms.GroupBox();
			this.radioButton4x = new global::System.Windows.Forms.RadioButton();
			this.radioButtonZoom05 = new global::System.Windows.Forms.RadioButton();
			this.radioButton2x = new global::System.Windows.Forms.RadioButton();
			this.radioButtonZoom1x = new global::System.Windows.Forms.RadioButton();
			this.labelTimeScale = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label1 = new global::System.Windows.Forms.Label();
			this.panelDisplay = new global::System.Windows.Forms.Panel();
			this.pictureBoxDisplay = new global::System.Windows.Forms.PictureBox();
			this.buttonExit = new global::System.Windows.Forms.Button();
			this.buttonHelp = new global::System.Windows.Forms.Button();
			this.timerRun = new global::System.Windows.Forms.Timer(this.components);
			this.saveFileDialogDisplay = new global::System.Windows.Forms.SaveFileDialog();
			this.panelLogicIO = new global::System.Windows.Forms.Panel();
			this.checkBoxIOEnable = new global::System.Windows.Forms.CheckBox();
			this.labelOut6Click = new global::System.Windows.Forms.Label();
			this.labelOut5Click = new global::System.Windows.Forms.Label();
			this.labelOut4Click = new global::System.Windows.Forms.Label();
			this.labelOut1Click = new global::System.Windows.Forms.Label();
			this.textBoxPin6Out = new global::System.Windows.Forms.TextBox();
			this.textBoxPin6In = new global::System.Windows.Forms.TextBox();
			this.textBoxPin5Out = new global::System.Windows.Forms.TextBox();
			this.textBoxPin5In = new global::System.Windows.Forms.TextBox();
			this.textBoxPin4Out = new global::System.Windows.Forms.TextBox();
			this.textBoxPin1Out = new global::System.Windows.Forms.TextBox();
			this.label34 = new global::System.Windows.Forms.Label();
			this.label33 = new global::System.Windows.Forms.Label();
			this.textBoxPin4In = new global::System.Windows.Forms.TextBox();
			this.label32 = new global::System.Windows.Forms.Label();
			this.label31 = new global::System.Windows.Forms.Label();
			this.panel4 = new global::System.Windows.Forms.Panel();
			this.radioButtonPin6In = new global::System.Windows.Forms.RadioButton();
			this.radioButtonPin6Out = new global::System.Windows.Forms.RadioButton();
			this.panel3 = new global::System.Windows.Forms.Panel();
			this.radioButtonPin5In = new global::System.Windows.Forms.RadioButton();
			this.radioButtonPin5Out = new global::System.Windows.Forms.RadioButton();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.radioButtonPin4In = new global::System.Windows.Forms.RadioButton();
			this.radioButtonPin4Out = new global::System.Windows.Forms.RadioButton();
			this.label30 = new global::System.Windows.Forms.Label();
			this.label29 = new global::System.Windows.Forms.Label();
			this.label28 = new global::System.Windows.Forms.Label();
			this.label27 = new global::System.Windows.Forms.Label();
			this.label26 = new global::System.Windows.Forms.Label();
			this.label20 = new global::System.Windows.Forms.Label();
			this.pictureBox2 = new global::System.Windows.Forms.PictureBox();
			this.timerIO = new global::System.Windows.Forms.Timer(this.components);
			this.checkBoxVDD = new global::System.Windows.Forms.CheckBox();
			this.panel2.SuspendLayout();
			this.panelAnalyzer.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBoxZoom.SuspendLayout();
			this.panelDisplay.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBoxDisplay).BeginInit();
			this.panelLogicIO.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel1.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
			base.SuspendLayout();
			this.label2.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.label2.AutoSize = true;
			this.label2.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label2.Location = new global::System.Drawing.Point(442, 15);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(47, 15);
			this.label2.TabIndex = 10;
			this.label2.Text = "Mode:";
			this.panel2.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.panel2.Controls.Add(this.radioButtonAnalyzer);
			this.panel2.Controls.Add(this.radioButtonLogicIO);
			this.panel2.Location = new global::System.Drawing.Point(494, 9);
			this.panel2.Name = "panel2";
			this.panel2.Size = new global::System.Drawing.Size(130, 30);
			this.panel2.TabIndex = 9;
			this.radioButtonAnalyzer.Appearance = global::System.Windows.Forms.Appearance.Button;
			this.radioButtonAnalyzer.Location = new global::System.Drawing.Point(69, 2);
			this.radioButtonAnalyzer.Name = "radioButtonAnalyzer";
			this.radioButtonAnalyzer.Size = new global::System.Drawing.Size(60, 22);
			this.radioButtonAnalyzer.TabIndex = 17;
			this.radioButtonAnalyzer.Text = "Analyzer";
			this.radioButtonAnalyzer.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.radioButtonAnalyzer.UseVisualStyleBackColor = true;
			this.radioButtonAnalyzer.CheckedChanged += new global::System.EventHandler(this.radioButtonAnalyzer_CheckedChanged);
			this.radioButtonLogicIO.Appearance = global::System.Windows.Forms.Appearance.Button;
			this.radioButtonLogicIO.Checked = true;
			this.radioButtonLogicIO.Location = new global::System.Drawing.Point(3, 2);
			this.radioButtonLogicIO.Name = "radioButtonLogicIO";
			this.radioButtonLogicIO.Size = new global::System.Drawing.Size(60, 22);
			this.radioButtonLogicIO.TabIndex = 16;
			this.radioButtonLogicIO.TabStop = true;
			this.radioButtonLogicIO.Text = "Logic I/O";
			this.radioButtonLogicIO.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.radioButtonLogicIO.UseVisualStyleBackColor = true;
			this.panelAnalyzer.Controls.Add(this.buttonSave);
			this.panelAnalyzer.Controls.Add(this.labelCursor1Val);
			this.panelAnalyzer.Controls.Add(this.labelCursor1);
			this.panelAnalyzer.Controls.Add(this.labelCursorDelta);
			this.panelAnalyzer.Controls.Add(this.labelCursorDeltaVal);
			this.panelAnalyzer.Controls.Add(this.labelCursor2);
			this.panelAnalyzer.Controls.Add(this.checkBoxCursors);
			this.panelAnalyzer.Controls.Add(this.labelCursor2Val);
			this.panelAnalyzer.Controls.Add(this.label24);
			this.panelAnalyzer.Controls.Add(this.label23);
			this.panelAnalyzer.Controls.Add(this.label22);
			this.panelAnalyzer.Controls.Add(this.groupBox2);
			this.panelAnalyzer.Controls.Add(this.pictureBox1);
			this.panelAnalyzer.Controls.Add(this.groupBox1);
			this.panelAnalyzer.Controls.Add(this.groupBoxZoom);
			this.panelAnalyzer.Controls.Add(this.labelTimeScale);
			this.panelAnalyzer.Controls.Add(this.label4);
			this.panelAnalyzer.Controls.Add(this.label3);
			this.panelAnalyzer.Controls.Add(this.label1);
			this.panelAnalyzer.Controls.Add(this.panelDisplay);
			this.panelAnalyzer.Location = new global::System.Drawing.Point(12, 39);
			this.panelAnalyzer.Name = "panelAnalyzer";
			this.panelAnalyzer.Size = new global::System.Drawing.Size(610, 326);
			this.panelAnalyzer.TabIndex = 11;
			this.panelAnalyzer.Visible = false;
			this.buttonSave.Location = new global::System.Drawing.Point(542, 119);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new global::System.Drawing.Size(68, 23);
			this.buttonSave.TabIndex = 34;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new global::System.EventHandler(this.buttonSave_Click);
			this.labelCursor1Val.AutoSize = true;
			this.labelCursor1Val.Enabled = false;
			this.labelCursor1Val.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelCursor1Val.ForeColor = global::System.Drawing.Color.RoyalBlue;
			this.labelCursor1Val.Location = new global::System.Drawing.Point(260, 2);
			this.labelCursor1Val.Name = "labelCursor1Val";
			this.labelCursor1Val.Size = new global::System.Drawing.Size(63, 13);
			this.labelCursor1Val.TabIndex = 29;
			this.labelCursor1Val.Text = "100.52 us";
			this.labelCursor1.AutoSize = true;
			this.labelCursor1.Enabled = false;
			this.labelCursor1.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelCursor1.ForeColor = global::System.Drawing.Color.RoyalBlue;
			this.labelCursor1.Location = new global::System.Drawing.Point(237, 2);
			this.labelCursor1.Name = "labelCursor1";
			this.labelCursor1.Size = new global::System.Drawing.Size(26, 13);
			this.labelCursor1.TabIndex = 28;
			this.labelCursor1.Text = "X =";
			this.labelCursorDelta.AutoSize = true;
			this.labelCursorDelta.Enabled = false;
			this.labelCursorDelta.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelCursorDelta.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.labelCursorDelta.Location = new global::System.Drawing.Point(439, 2);
			this.labelCursorDelta.Name = "labelCursorDelta";
			this.labelCursorDelta.Size = new global::System.Drawing.Size(38, 13);
			this.labelCursorDelta.TabIndex = 32;
			this.labelCursorDelta.Text = "Y-X =";
			this.labelCursorDeltaVal.AutoSize = true;
			this.labelCursorDeltaVal.Enabled = false;
			this.labelCursorDeltaVal.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelCursorDeltaVal.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.labelCursorDeltaVal.Location = new global::System.Drawing.Point(477, 2);
			this.labelCursorDeltaVal.Name = "labelCursorDeltaVal";
			this.labelCursorDeltaVal.Size = new global::System.Drawing.Size(122, 13);
			this.labelCursorDeltaVal.TabIndex = 33;
			this.labelCursorDeltaVal.Text = "100.26 us (9974 Hz)";
			this.labelCursor2.AutoSize = true;
			this.labelCursor2.Enabled = false;
			this.labelCursor2.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelCursor2.ForeColor = global::System.Drawing.Color.DarkViolet;
			this.labelCursor2.Location = new global::System.Drawing.Point(338, 2);
			this.labelCursor2.Name = "labelCursor2";
			this.labelCursor2.Size = new global::System.Drawing.Size(26, 13);
			this.labelCursor2.TabIndex = 29;
			this.labelCursor2.Text = "Y =";
			this.checkBoxCursors.AutoSize = true;
			this.checkBoxCursors.Location = new global::System.Drawing.Point(161, 1);
			this.checkBoxCursors.Name = "checkBoxCursors";
			this.checkBoxCursors.Size = new global::System.Drawing.Size(61, 17);
			this.checkBoxCursors.TabIndex = 30;
			this.checkBoxCursors.Text = "Cursors";
			this.checkBoxCursors.UseVisualStyleBackColor = true;
			this.checkBoxCursors.CheckedChanged += new global::System.EventHandler(this.checkBoxCursors_CheckedChanged);
			this.labelCursor2Val.AutoSize = true;
			this.labelCursor2Val.Enabled = false;
			this.labelCursor2Val.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelCursor2Val.ForeColor = global::System.Drawing.Color.DarkViolet;
			this.labelCursor2Val.Location = new global::System.Drawing.Point(365, 2);
			this.labelCursor2Val.Name = "labelCursor2Val";
			this.labelCursor2Val.Size = new global::System.Drawing.Size(63, 13);
			this.labelCursor2Val.TabIndex = 31;
			this.labelCursor2Val.Text = "200.78 us";
			this.label24.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.label24.AutoSize = true;
			this.label24.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label24.ForeColor = global::System.Drawing.SystemColors.ActiveCaption;
			this.label24.Location = new global::System.Drawing.Point(3, 297);
			this.label24.Name = "label24";
			this.label24.Size = new global::System.Drawing.Size(117, 26);
			this.label24.TabIndex = 29;
			this.label24.Text = "Set VDD Voltage value\r\nin main form.";
			this.label23.AutoSize = true;
			this.label23.Location = new global::System.Drawing.Point(84, 157);
			this.label23.Name = "label23";
			this.label23.Size = new global::System.Drawing.Size(65, 78);
			this.label23.TabIndex = 28;
			this.label23.Text = "NOTE:\r\nCh 1 && Ch 2\r\ninputs have \r\n4.7k Ohm\r\npull-down\r\nresistors.";
			this.label22.AutoSize = true;
			this.label22.Location = new global::System.Drawing.Point(3, 265);
			this.label22.Name = "label22";
			this.label22.Size = new global::System.Drawing.Size(158, 26);
			this.label22.TabIndex = 27;
			this.label22.Text = "PICkit 2 VDD MUST connect to\r\ncircuit VDD.";
			this.groupBox2.Controls.Add(this.label25);
			this.groupBox2.Controls.Add(this.radioButtonTrigDly3);
			this.groupBox2.Controls.Add(this.radioButtonTrigDly2);
			this.groupBox2.Controls.Add(this.radioButtonTrigDly1);
			this.groupBox2.Controls.Add(this.radioButtonTrigEnd);
			this.groupBox2.Controls.Add(this.radioButtonTrigMid);
			this.groupBox2.Controls.Add(this.radioButtonTrigStart);
			this.groupBox2.Controls.Add(this.label21);
			this.groupBox2.Controls.Add(this.labelAliasFreq);
			this.groupBox2.Controls.Add(this.comboBoxSampleRate);
			this.groupBox2.Controls.Add(this.label19);
			this.groupBox2.Controls.Add(this.buttonRun);
			this.groupBox2.Location = new global::System.Drawing.Point(359, 146);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new global::System.Drawing.Size(251, 180);
			this.groupBox2.TabIndex = 26;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Aquisition";
			this.label25.AutoSize = true;
			this.label25.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label25.Location = new global::System.Drawing.Point(110, 164);
			this.label25.Name = "label25";
			this.label25.Size = new global::System.Drawing.Size(135, 13);
			this.label25.TabIndex = 31;
			this.label25.Text = "1 Window = 1000 samples.";
			this.radioButtonTrigDly3.AutoSize = true;
			this.radioButtonTrigDly3.Location = new global::System.Drawing.Point(126, 148);
			this.radioButtonTrigDly3.Name = "radioButtonTrigDly3";
			this.radioButtonTrigDly3.Size = new global::System.Drawing.Size(108, 17);
			this.radioButtonTrigDly3.TabIndex = 30;
			this.radioButtonTrigDly3.Text = "Delay 3 Windows";
			this.radioButtonTrigDly3.UseVisualStyleBackColor = true;
			this.radioButtonTrigDly2.AutoSize = true;
			this.radioButtonTrigDly2.Location = new global::System.Drawing.Point(126, 130);
			this.radioButtonTrigDly2.Name = "radioButtonTrigDly2";
			this.radioButtonTrigDly2.Size = new global::System.Drawing.Size(108, 17);
			this.radioButtonTrigDly2.TabIndex = 29;
			this.radioButtonTrigDly2.Text = "Delay 2 Windows";
			this.radioButtonTrigDly2.UseVisualStyleBackColor = true;
			this.radioButtonTrigDly1.AutoSize = true;
			this.radioButtonTrigDly1.Location = new global::System.Drawing.Point(126, 112);
			this.radioButtonTrigDly1.Name = "radioButtonTrigDly1";
			this.radioButtonTrigDly1.Size = new global::System.Drawing.Size(103, 17);
			this.radioButtonTrigDly1.TabIndex = 28;
			this.radioButtonTrigDly1.Text = "Delay 1 Window";
			this.radioButtonTrigDly1.UseVisualStyleBackColor = true;
			this.radioButtonTrigEnd.AutoSize = true;
			this.radioButtonTrigEnd.Location = new global::System.Drawing.Point(9, 148);
			this.radioButtonTrigEnd.Name = "radioButtonTrigEnd";
			this.radioButtonTrigEnd.Size = new global::System.Drawing.Size(82, 17);
			this.radioButtonTrigEnd.TabIndex = 27;
			this.radioButtonTrigEnd.Text = "End of Data";
			this.radioButtonTrigEnd.UseVisualStyleBackColor = true;
			this.radioButtonTrigMid.AutoSize = true;
			this.radioButtonTrigMid.Location = new global::System.Drawing.Point(9, 130);
			this.radioButtonTrigMid.Name = "radioButtonTrigMid";
			this.radioButtonTrigMid.Size = new global::System.Drawing.Size(94, 17);
			this.radioButtonTrigMid.TabIndex = 26;
			this.radioButtonTrigMid.Text = "Center of Data";
			this.radioButtonTrigMid.UseVisualStyleBackColor = true;
			this.radioButtonTrigStart.AutoSize = true;
			this.radioButtonTrigStart.Checked = true;
			this.radioButtonTrigStart.Location = new global::System.Drawing.Point(9, 112);
			this.radioButtonTrigStart.Name = "radioButtonTrigStart";
			this.radioButtonTrigStart.Size = new global::System.Drawing.Size(85, 17);
			this.radioButtonTrigStart.TabIndex = 25;
			this.radioButtonTrigStart.TabStop = true;
			this.radioButtonTrigStart.Text = "Start of Data";
			this.radioButtonTrigStart.UseVisualStyleBackColor = true;
			this.label21.AutoSize = true;
			this.label21.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label21.Location = new global::System.Drawing.Point(6, 89);
			this.label21.Name = "label21";
			this.label21.Size = new global::System.Drawing.Size(113, 15);
			this.label21.TabIndex = 24;
			this.label21.Text = "Trigger Position:";
			this.labelAliasFreq.AutoSize = true;
			this.labelAliasFreq.Location = new global::System.Drawing.Point(6, 63);
			this.labelAliasFreq.Name = "labelAliasFreq";
			this.labelAliasFreq.Size = new global::System.Drawing.Size(224, 13);
			this.labelAliasFreq.TabIndex = 23;
			this.labelAliasFreq.Text = "NOTE: Signals greater than 500 kHz will alias.";
			this.comboBoxSampleRate.FormattingEnabled = true;
			this.comboBoxSampleRate.Items.AddRange(new object[]
			{
				"1 MHz - 1 ms Window",
				"500 kHz - 2 ms Window",
				"250 kHz - 4 ms Window",
				"100 kHz - 10 ms Window",
				"50 kHz - 20 ms Window",
				"25 kHz - 40 ms Window",
				"10 kHz - 100 ms Window",
				"5 kHz - 200 ms Window"
			});
			this.comboBoxSampleRate.Location = new global::System.Drawing.Point(9, 37);
			this.comboBoxSampleRate.Name = "comboBoxSampleRate";
			this.comboBoxSampleRate.Size = new global::System.Drawing.Size(168, 21);
			this.comboBoxSampleRate.TabIndex = 22;
			this.comboBoxSampleRate.SelectedIndexChanged += new global::System.EventHandler(this.comboBoxSampleRate_SelectedIndexChanged);
			this.label19.AutoSize = true;
			this.label19.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label19.Location = new global::System.Drawing.Point(6, 16);
			this.label19.Name = "label19";
			this.label19.Size = new global::System.Drawing.Size(94, 15);
			this.label19.TabIndex = 21;
			this.label19.Text = "Sample Rate:";
			this.buttonRun.Location = new global::System.Drawing.Point(189, 13);
			this.buttonRun.Name = "buttonRun";
			this.buttonRun.Size = new global::System.Drawing.Size(56, 45);
			this.buttonRun.TabIndex = 0;
			this.buttonRun.Text = "RUN";
			this.buttonRun.UseVisualStyleBackColor = true;
			this.buttonRun.Click += new global::System.EventHandler(this.buttonRun_Click);
			this.pictureBox1.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("pictureBox1.Image");
			this.pictureBox1.Location = new global::System.Drawing.Point(0, 146);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new global::System.Drawing.Size(78, 116);
			this.pictureBox1.TabIndex = 25;
			this.pictureBox1.TabStop = false;
			this.groupBox1.Controls.Add(this.label18);
			this.groupBox1.Controls.Add(this.label17);
			this.groupBox1.Controls.Add(this.label15);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label16);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label14);
			this.groupBox1.Controls.Add(this.comboBoxCh1Trig);
			this.groupBox1.Controls.Add(this.label13);
			this.groupBox1.Controls.Add(this.comboBoxCh2Trig);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.comboBoxCh3Trig);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Location = new global::System.Drawing.Point(167, 146);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new global::System.Drawing.Size(186, 180);
			this.groupBox1.TabIndex = 24;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Trigger";
			this.label18.AutoSize = true;
			this.label18.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label18.Location = new global::System.Drawing.Point(51, 164);
			this.label18.Name = "label18";
			this.label18.Size = new global::System.Drawing.Size(46, 13);
			this.label18.TabIndex = 24;
			this.label18.Text = "(1 - 256)";
			this.label17.AutoSize = true;
			this.label17.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label17.Location = new global::System.Drawing.Point(102, 144);
			this.label17.Name = "label17";
			this.label17.Size = new global::System.Drawing.Size(46, 15);
			this.label17.TabIndex = 23;
			this.label17.Text = "times.";
			this.label15.AutoSize = true;
			this.label15.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label15.Location = new global::System.Drawing.Point(6, 16);
			this.label15.Name = "label15";
			this.label15.Size = new global::System.Drawing.Size(91, 15);
			this.label15.TabIndex = 20;
			this.label15.Text = "Trigger when";
			this.textBox1.Location = new global::System.Drawing.Point(53, 143);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new global::System.Drawing.Size(43, 20);
			this.textBox1.TabIndex = 22;
			this.textBox1.Text = "1";
			this.textBox1.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.textBox1.Leave += new global::System.EventHandler(this.textBox1_Leave);
			this.label5.AutoSize = true;
			this.label5.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label5.Location = new global::System.Drawing.Point(6, 48);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(45, 15);
			this.label5.TabIndex = 7;
			this.label5.Text = "Ch 1 = ";
			this.label16.AutoSize = true;
			this.label16.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label16.Location = new global::System.Drawing.Point(6, 144);
			this.label16.Name = "label16";
			this.label16.Size = new global::System.Drawing.Size(49, 15);
			this.label16.TabIndex = 21;
			this.label16.Text = "occurs";
			this.label6.AutoSize = true;
			this.label6.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label6.Location = new global::System.Drawing.Point(6, 79);
			this.label6.Name = "label6";
			this.label6.Size = new global::System.Drawing.Size(45, 15);
			this.label6.TabIndex = 8;
			this.label6.Text = "Ch 2 = ";
			this.label7.AutoSize = true;
			this.label7.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label7.Location = new global::System.Drawing.Point(6, 111);
			this.label7.Name = "label7";
			this.label7.Size = new global::System.Drawing.Size(45, 15);
			this.label7.TabIndex = 9;
			this.label7.Text = "Ch 3 = ";
			this.label14.AutoSize = true;
			this.label14.Location = new global::System.Drawing.Point(102, 63);
			this.label14.Name = "label14";
			this.label14.Size = new global::System.Drawing.Size(73, 13);
			this.label14.TabIndex = 19;
			this.label14.Text = "1 - Logic High";
			this.comboBoxCh1Trig.FormattingEnabled = true;
			this.comboBoxCh1Trig.Items.AddRange(new object[]
			{
				"*",
				"1",
				"0",
				"/",
				"\\"
			});
			this.comboBoxCh1Trig.Location = new global::System.Drawing.Point(53, 45);
			this.comboBoxCh1Trig.Name = "comboBoxCh1Trig";
			this.comboBoxCh1Trig.Size = new global::System.Drawing.Size(43, 21);
			this.comboBoxCh1Trig.TabIndex = 10;
			this.comboBoxCh1Trig.SelectedIndexChanged += new global::System.EventHandler(this.comboBoxCh1Trig_SelectedIndexChanged);
			this.label13.AutoSize = true;
			this.label13.Location = new global::System.Drawing.Point(102, 81);
			this.label13.Name = "label13";
			this.label13.Size = new global::System.Drawing.Size(71, 13);
			this.label13.TabIndex = 18;
			this.label13.Text = "0 - Logic Low";
			this.comboBoxCh2Trig.FormattingEnabled = true;
			this.comboBoxCh2Trig.Items.AddRange(new object[]
			{
				"*",
				"1",
				"0",
				"/",
				"\\"
			});
			this.comboBoxCh2Trig.Location = new global::System.Drawing.Point(53, 76);
			this.comboBoxCh2Trig.Name = "comboBoxCh2Trig";
			this.comboBoxCh2Trig.Size = new global::System.Drawing.Size(43, 21);
			this.comboBoxCh2Trig.TabIndex = 11;
			this.comboBoxCh2Trig.SelectedIndexChanged += new global::System.EventHandler(this.comboBoxCh2Trig_SelectedIndexChanged);
			this.label12.AutoSize = true;
			this.label12.Location = new global::System.Drawing.Point(102, 99);
			this.label12.Name = "label12";
			this.label12.Size = new global::System.Drawing.Size(78, 13);
			this.label12.TabIndex = 17;
			this.label12.Text = "/ - Rising Edge";
			this.comboBoxCh3Trig.FormattingEnabled = true;
			this.comboBoxCh3Trig.Items.AddRange(new object[]
			{
				"*",
				"1",
				"0",
				"/",
				"\\"
			});
			this.comboBoxCh3Trig.Location = new global::System.Drawing.Point(53, 108);
			this.comboBoxCh3Trig.Name = "comboBoxCh3Trig";
			this.comboBoxCh3Trig.Size = new global::System.Drawing.Size(43, 21);
			this.comboBoxCh3Trig.TabIndex = 12;
			this.comboBoxCh3Trig.SelectedIndexChanged += new global::System.EventHandler(this.comboBoxCh3Trig_SelectedIndexChanged);
			this.label11.AutoSize = true;
			this.label11.Location = new global::System.Drawing.Point(102, 117);
			this.label11.Name = "label11";
			this.label11.Size = new global::System.Drawing.Size(79, 13);
			this.label11.TabIndex = 16;
			this.label11.Text = "\\ - Falling Edge";
			this.label8.AutoSize = true;
			this.label8.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label8.Location = new global::System.Drawing.Point(22, 63);
			this.label8.Name = "label8";
			this.label8.Size = new global::System.Drawing.Size(25, 13);
			this.label8.TabIndex = 13;
			this.label8.Text = "and";
			this.label10.AutoSize = true;
			this.label10.Location = new global::System.Drawing.Point(102, 45);
			this.label10.Name = "label10";
			this.label10.Size = new global::System.Drawing.Size(70, 13);
			this.label10.TabIndex = 15;
			this.label10.Text = "* - Don't Care";
			this.label9.AutoSize = true;
			this.label9.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label9.Location = new global::System.Drawing.Point(22, 94);
			this.label9.Name = "label9";
			this.label9.Size = new global::System.Drawing.Size(25, 13);
			this.label9.TabIndex = 14;
			this.label9.Text = "and";
			this.groupBoxZoom.Controls.Add(this.radioButton4x);
			this.groupBoxZoom.Controls.Add(this.radioButtonZoom05);
			this.groupBoxZoom.Controls.Add(this.radioButton2x);
			this.groupBoxZoom.Controls.Add(this.radioButtonZoom1x);
			this.groupBoxZoom.Location = new global::System.Drawing.Point(542, 13);
			this.groupBoxZoom.Name = "groupBoxZoom";
			this.groupBoxZoom.Size = new global::System.Drawing.Size(68, 100);
			this.groupBoxZoom.TabIndex = 6;
			this.groupBoxZoom.TabStop = false;
			this.groupBoxZoom.Text = "Zoom";
			this.radioButton4x.AutoSize = true;
			this.radioButton4x.Location = new global::System.Drawing.Point(9, 73);
			this.radioButton4x.Name = "radioButton4x";
			this.radioButton4x.Size = new global::System.Drawing.Size(36, 17);
			this.radioButton4x.TabIndex = 10;
			this.radioButton4x.Text = "4x";
			this.radioButton4x.UseVisualStyleBackColor = true;
			this.radioButton4x.Click += new global::System.EventHandler(this.radioButtonZoom05_Click);
			this.radioButtonZoom05.AutoSize = true;
			this.radioButtonZoom05.Location = new global::System.Drawing.Point(9, 19);
			this.radioButtonZoom05.Name = "radioButtonZoom05";
			this.radioButtonZoom05.Size = new global::System.Drawing.Size(45, 17);
			this.radioButtonZoom05.TabIndex = 7;
			this.radioButtonZoom05.Text = "0.5x";
			this.radioButtonZoom05.UseVisualStyleBackColor = true;
			this.radioButtonZoom05.Click += new global::System.EventHandler(this.radioButtonZoom05_Click);
			this.radioButton2x.AutoSize = true;
			this.radioButton2x.Location = new global::System.Drawing.Point(9, 55);
			this.radioButton2x.Name = "radioButton2x";
			this.radioButton2x.Size = new global::System.Drawing.Size(36, 17);
			this.radioButton2x.TabIndex = 9;
			this.radioButton2x.Text = "2x";
			this.radioButton2x.UseVisualStyleBackColor = true;
			this.radioButton2x.Click += new global::System.EventHandler(this.radioButtonZoom05_Click);
			this.radioButtonZoom1x.AutoSize = true;
			this.radioButtonZoom1x.Checked = true;
			this.radioButtonZoom1x.Location = new global::System.Drawing.Point(9, 37);
			this.radioButtonZoom1x.Name = "radioButtonZoom1x";
			this.radioButtonZoom1x.Size = new global::System.Drawing.Size(36, 17);
			this.radioButtonZoom1x.TabIndex = 8;
			this.radioButtonZoom1x.TabStop = true;
			this.radioButtonZoom1x.Text = "1x";
			this.radioButtonZoom1x.UseVisualStyleBackColor = true;
			this.radioButtonZoom1x.Click += new global::System.EventHandler(this.radioButtonZoom05_Click);
			this.labelTimeScale.AutoSize = true;
			this.labelTimeScale.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelTimeScale.Location = new global::System.Drawing.Point(33, 0);
			this.labelTimeScale.Name = "labelTimeScale";
			this.labelTimeScale.Size = new global::System.Drawing.Size(70, 15);
			this.labelTimeScale.TabIndex = 5;
			this.labelTimeScale.Text = "50us / Div";
			this.label4.AutoSize = true;
			this.label4.Location = new global::System.Drawing.Point(1, 95);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(29, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Ch.3";
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(1, 65);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(29, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Ch.2";
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(1, 35);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(29, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Ch.1";
			this.panelDisplay.AutoScroll = true;
			this.panelDisplay.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.panelDisplay.Controls.Add(this.pictureBoxDisplay);
			this.panelDisplay.Location = new global::System.Drawing.Point(36, 20);
			this.panelDisplay.Name = "panelDisplay";
			this.panelDisplay.Size = new global::System.Drawing.Size(500, 122);
			this.panelDisplay.TabIndex = 1;
			this.pictureBoxDisplay.Location = new global::System.Drawing.Point(0, 0);
			this.pictureBoxDisplay.Name = "pictureBoxDisplay";
			this.pictureBoxDisplay.Size = new global::System.Drawing.Size(1024, 100);
			this.pictureBoxDisplay.TabIndex = 0;
			this.pictureBoxDisplay.TabStop = false;
			this.pictureBoxDisplay.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.pictureBoxDisplay_MouseDown);
			this.buttonExit.Location = new global::System.Drawing.Point(508, 371);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new global::System.Drawing.Size(116, 23);
			this.buttonExit.TabIndex = 27;
			this.buttonExit.Text = "Exit Logic Tool";
			this.buttonExit.UseVisualStyleBackColor = true;
			this.buttonExit.Click += new global::System.EventHandler(this.buttonExit_Click);
			this.buttonHelp.Location = new global::System.Drawing.Point(12, 371);
			this.buttonHelp.Name = "buttonHelp";
			this.buttonHelp.Size = new global::System.Drawing.Size(56, 23);
			this.buttonHelp.TabIndex = 32;
			this.buttonHelp.Text = "Help";
			this.buttonHelp.UseVisualStyleBackColor = true;
			this.buttonHelp.Click += new global::System.EventHandler(this.buttonHelp_Click);
			this.timerRun.Interval = 250;
			this.timerRun.Tick += new global::System.EventHandler(this.timerRun_Tick);
			this.saveFileDialogDisplay.DefaultExt = "bmp";
			this.saveFileDialogDisplay.Filter = "Bitmap(.bmp)|*.bmp";
			this.saveFileDialogDisplay.InitialDirectory = "c:\\";
			this.saveFileDialogDisplay.Title = "Save Logic Analyzer Display";
			this.panelLogicIO.Controls.Add(this.checkBoxIOEnable);
			this.panelLogicIO.Controls.Add(this.labelOut6Click);
			this.panelLogicIO.Controls.Add(this.labelOut5Click);
			this.panelLogicIO.Controls.Add(this.labelOut4Click);
			this.panelLogicIO.Controls.Add(this.labelOut1Click);
			this.panelLogicIO.Controls.Add(this.textBoxPin6Out);
			this.panelLogicIO.Controls.Add(this.textBoxPin6In);
			this.panelLogicIO.Controls.Add(this.textBoxPin5Out);
			this.panelLogicIO.Controls.Add(this.textBoxPin5In);
			this.panelLogicIO.Controls.Add(this.textBoxPin4Out);
			this.panelLogicIO.Controls.Add(this.textBoxPin1Out);
			this.panelLogicIO.Controls.Add(this.label34);
			this.panelLogicIO.Controls.Add(this.label33);
			this.panelLogicIO.Controls.Add(this.textBoxPin4In);
			this.panelLogicIO.Controls.Add(this.label32);
			this.panelLogicIO.Controls.Add(this.label31);
			this.panelLogicIO.Controls.Add(this.panel4);
			this.panelLogicIO.Controls.Add(this.panel3);
			this.panelLogicIO.Controls.Add(this.panel1);
			this.panelLogicIO.Controls.Add(this.label30);
			this.panelLogicIO.Controls.Add(this.label29);
			this.panelLogicIO.Controls.Add(this.label28);
			this.panelLogicIO.Controls.Add(this.label27);
			this.panelLogicIO.Controls.Add(this.label26);
			this.panelLogicIO.Controls.Add(this.label20);
			this.panelLogicIO.Controls.Add(this.pictureBox2);
			this.panelLogicIO.Location = new global::System.Drawing.Point(12, 39);
			this.panelLogicIO.Name = "panelLogicIO";
			this.panelLogicIO.Size = new global::System.Drawing.Size(610, 326);
			this.panelLogicIO.TabIndex = 33;
			this.checkBoxIOEnable.Appearance = global::System.Windows.Forms.Appearance.Button;
			this.checkBoxIOEnable.Location = new global::System.Drawing.Point(515, 277);
			this.checkBoxIOEnable.Name = "checkBoxIOEnable";
			this.checkBoxIOEnable.Size = new global::System.Drawing.Size(78, 34);
			this.checkBoxIOEnable.TabIndex = 52;
			this.checkBoxIOEnable.Text = "Enable IO";
			this.checkBoxIOEnable.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBoxIOEnable.UseVisualStyleBackColor = true;
			this.checkBoxIOEnable.CheckedChanged += new global::System.EventHandler(this.checkBoxIOEnable_CheckedChanged);
			this.labelOut6Click.AutoSize = true;
			this.labelOut6Click.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelOut6Click.ForeColor = global::System.Drawing.SystemColors.ControlDarkDark;
			this.labelOut6Click.Location = new global::System.Drawing.Point(419, 243);
			this.labelOut6Click.Name = "labelOut6Click";
			this.labelOut6Click.Size = new global::System.Drawing.Size(178, 13);
			this.labelOut6Click.TabIndex = 51;
			this.labelOut6Click.Text = "<- Click Output box or press <F> key";
			this.labelOut5Click.AutoSize = true;
			this.labelOut5Click.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelOut5Click.ForeColor = global::System.Drawing.SystemColors.ControlDarkDark;
			this.labelOut5Click.Location = new global::System.Drawing.Point(419, 177);
			this.labelOut5Click.Name = "labelOut5Click";
			this.labelOut5Click.Size = new global::System.Drawing.Size(180, 13);
			this.labelOut5Click.TabIndex = 50;
			this.labelOut5Click.Text = "<- Click Output box or press <D> key";
			this.labelOut4Click.AutoSize = true;
			this.labelOut4Click.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelOut4Click.ForeColor = global::System.Drawing.SystemColors.ControlDarkDark;
			this.labelOut4Click.Location = new global::System.Drawing.Point(419, 111);
			this.labelOut4Click.Name = "labelOut4Click";
			this.labelOut4Click.Size = new global::System.Drawing.Size(179, 13);
			this.labelOut4Click.TabIndex = 49;
			this.labelOut4Click.Text = "<- Click Output box or press <S> key";
			this.labelOut1Click.AutoSize = true;
			this.labelOut1Click.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelOut1Click.ForeColor = global::System.Drawing.SystemColors.ControlDarkDark;
			this.labelOut1Click.Location = new global::System.Drawing.Point(419, 45);
			this.labelOut1Click.Name = "labelOut1Click";
			this.labelOut1Click.Size = new global::System.Drawing.Size(179, 13);
			this.labelOut1Click.TabIndex = 48;
			this.labelOut1Click.Text = "<- Click Output box or press <A> key";
			this.textBoxPin6Out.BackColor = global::System.Drawing.SystemColors.Control;
			this.textBoxPin6Out.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.textBoxPin6Out.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 14.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.textBoxPin6Out.ForeColor = global::System.Drawing.SystemColors.Window;
			this.textBoxPin6Out.Location = new global::System.Drawing.Point(350, 237);
			this.textBoxPin6Out.Name = "textBoxPin6Out";
			this.textBoxPin6Out.ReadOnly = true;
			this.textBoxPin6Out.Size = new global::System.Drawing.Size(30, 29);
			this.textBoxPin6Out.TabIndex = 47;
			this.textBoxPin6Out.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.textBoxPin6Out.Click += new global::System.EventHandler(this.textBoxPin6Out_Click);
			this.textBoxPin6In.BackColor = global::System.Drawing.SystemColors.Control;
			this.textBoxPin6In.Cursor = global::System.Windows.Forms.Cursors.Default;
			this.textBoxPin6In.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 14.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.textBoxPin6In.ForeColor = global::System.Drawing.SystemColors.Window;
			this.textBoxPin6In.Location = new global::System.Drawing.Point(266, 239);
			this.textBoxPin6In.Name = "textBoxPin6In";
			this.textBoxPin6In.ReadOnly = true;
			this.textBoxPin6In.Size = new global::System.Drawing.Size(30, 29);
			this.textBoxPin6In.TabIndex = 46;
			this.textBoxPin6In.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.textBoxPin5Out.BackColor = global::System.Drawing.Color.Red;
			this.textBoxPin5Out.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.textBoxPin5Out.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 14.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.textBoxPin5Out.ForeColor = global::System.Drawing.SystemColors.Window;
			this.textBoxPin5Out.Location = new global::System.Drawing.Point(350, 173);
			this.textBoxPin5Out.Name = "textBoxPin5Out";
			this.textBoxPin5Out.ReadOnly = true;
			this.textBoxPin5Out.Size = new global::System.Drawing.Size(30, 29);
			this.textBoxPin5Out.TabIndex = 45;
			this.textBoxPin5Out.Text = "1";
			this.textBoxPin5Out.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.textBoxPin5Out.Click += new global::System.EventHandler(this.textBoxPin5Out_Click);
			this.textBoxPin5In.BackColor = global::System.Drawing.Color.DodgerBlue;
			this.textBoxPin5In.Cursor = global::System.Windows.Forms.Cursors.Default;
			this.textBoxPin5In.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 14.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.textBoxPin5In.ForeColor = global::System.Drawing.SystemColors.Window;
			this.textBoxPin5In.Location = new global::System.Drawing.Point(266, 173);
			this.textBoxPin5In.Name = "textBoxPin5In";
			this.textBoxPin5In.ReadOnly = true;
			this.textBoxPin5In.Size = new global::System.Drawing.Size(30, 29);
			this.textBoxPin5In.TabIndex = 44;
			this.textBoxPin5In.Text = "1";
			this.textBoxPin5In.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.textBoxPin4Out.BackColor = global::System.Drawing.Color.DarkRed;
			this.textBoxPin4Out.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.textBoxPin4Out.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 14.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.textBoxPin4Out.ForeColor = global::System.Drawing.SystemColors.Window;
			this.textBoxPin4Out.Location = new global::System.Drawing.Point(350, 107);
			this.textBoxPin4Out.Name = "textBoxPin4Out";
			this.textBoxPin4Out.ReadOnly = true;
			this.textBoxPin4Out.Size = new global::System.Drawing.Size(30, 29);
			this.textBoxPin4Out.TabIndex = 43;
			this.textBoxPin4Out.Text = "0";
			this.textBoxPin4Out.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.textBoxPin4Out.Click += new global::System.EventHandler(this.textBoxPin4Out_Click);
			this.textBoxPin1Out.BackColor = global::System.Drawing.Color.DarkRed;
			this.textBoxPin1Out.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.textBoxPin1Out.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 14.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.textBoxPin1Out.ForeColor = global::System.Drawing.SystemColors.Window;
			this.textBoxPin1Out.Location = new global::System.Drawing.Point(350, 39);
			this.textBoxPin1Out.Name = "textBoxPin1Out";
			this.textBoxPin1Out.ReadOnly = true;
			this.textBoxPin1Out.Size = new global::System.Drawing.Size(30, 29);
			this.textBoxPin1Out.TabIndex = 42;
			this.textBoxPin1Out.Text = "0";
			this.textBoxPin1Out.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.textBoxPin1Out.Click += new global::System.EventHandler(this.textBoxPin1Out_Click);
			this.label34.AutoSize = true;
			this.label34.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Bold | global::System.Drawing.FontStyle.Underline, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label34.Location = new global::System.Drawing.Point(260, 13);
			this.label34.Name = "label34";
			this.label34.Size = new global::System.Drawing.Size(49, 16);
			this.label34.TabIndex = 41;
			this.label34.Text = "Inputs";
			this.label33.AutoSize = true;
			this.label33.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Bold | global::System.Drawing.FontStyle.Underline, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label33.Location = new global::System.Drawing.Point(338, 13);
			this.label33.Name = "label33";
			this.label33.Size = new global::System.Drawing.Size(60, 16);
			this.label33.TabIndex = 40;
			this.label33.Text = "Outputs";
			this.textBoxPin4In.BackColor = global::System.Drawing.Color.DarkBlue;
			this.textBoxPin4In.Cursor = global::System.Windows.Forms.Cursors.Default;
			this.textBoxPin4In.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 14.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.textBoxPin4In.ForeColor = global::System.Drawing.SystemColors.Window;
			this.textBoxPin4In.Location = new global::System.Drawing.Point(266, 107);
			this.textBoxPin4In.Name = "textBoxPin4In";
			this.textBoxPin4In.ReadOnly = true;
			this.textBoxPin4In.Size = new global::System.Drawing.Size(30, 29);
			this.textBoxPin4In.TabIndex = 38;
			this.textBoxPin4In.Text = "0";
			this.textBoxPin4In.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.label32.AutoSize = true;
			this.label32.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label32.ForeColor = global::System.Drawing.SystemColors.ControlDarkDark;
			this.label32.Location = new global::System.Drawing.Point(108, 197);
			this.label32.Name = "label32";
			this.label32.Size = new global::System.Drawing.Size(53, 26);
			this.label32.TabIndex = 37;
			this.label32.Text = "4.7k Ohm\r\npulldown";
			this.label31.AutoSize = true;
			this.label31.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label31.ForeColor = global::System.Drawing.SystemColors.ControlDarkDark;
			this.label31.Location = new global::System.Drawing.Point(108, 131);
			this.label31.Name = "label31";
			this.label31.Size = new global::System.Drawing.Size(53, 26);
			this.label31.TabIndex = 36;
			this.label31.Text = "4.7k Ohm\r\npulldown";
			this.panel4.Controls.Add(this.radioButtonPin6In);
			this.panel4.Controls.Add(this.radioButtonPin6Out);
			this.panel4.Location = new global::System.Drawing.Point(170, 233);
			this.panel4.Name = "panel4";
			this.panel4.Size = new global::System.Drawing.Size(60, 45);
			this.panel4.TabIndex = 35;
			this.radioButtonPin6In.AutoSize = true;
			this.radioButtonPin6In.Checked = true;
			this.radioButtonPin6In.Enabled = false;
			this.radioButtonPin6In.Location = new global::System.Drawing.Point(3, 21);
			this.radioButtonPin6In.Name = "radioButtonPin6In";
			this.radioButtonPin6In.Size = new global::System.Drawing.Size(49, 17);
			this.radioButtonPin6In.TabIndex = 1;
			this.radioButtonPin6In.TabStop = true;
			this.radioButtonPin6In.Text = "Input";
			this.radioButtonPin6In.UseVisualStyleBackColor = true;
			this.radioButtonPin6Out.AutoSize = true;
			this.radioButtonPin6Out.Enabled = false;
			this.radioButtonPin6Out.Location = new global::System.Drawing.Point(3, 3);
			this.radioButtonPin6Out.Name = "radioButtonPin6Out";
			this.radioButtonPin6Out.Size = new global::System.Drawing.Size(57, 17);
			this.radioButtonPin6Out.TabIndex = 0;
			this.radioButtonPin6Out.Text = "Output";
			this.radioButtonPin6Out.UseVisualStyleBackColor = true;
			this.radioButtonPin6Out.CheckedChanged += new global::System.EventHandler(this.radioButtonPin6Out_CheckedChanged);
			this.panel3.Controls.Add(this.radioButtonPin5In);
			this.panel3.Controls.Add(this.radioButtonPin5Out);
			this.panel3.Location = new global::System.Drawing.Point(170, 167);
			this.panel3.Name = "panel3";
			this.panel3.Size = new global::System.Drawing.Size(60, 45);
			this.panel3.TabIndex = 34;
			this.radioButtonPin5In.AutoSize = true;
			this.radioButtonPin5In.Checked = true;
			this.radioButtonPin5In.Enabled = false;
			this.radioButtonPin5In.Location = new global::System.Drawing.Point(3, 21);
			this.radioButtonPin5In.Name = "radioButtonPin5In";
			this.radioButtonPin5In.Size = new global::System.Drawing.Size(49, 17);
			this.radioButtonPin5In.TabIndex = 1;
			this.radioButtonPin5In.TabStop = true;
			this.radioButtonPin5In.Text = "Input";
			this.radioButtonPin5In.UseVisualStyleBackColor = true;
			this.radioButtonPin5Out.AutoSize = true;
			this.radioButtonPin5Out.Enabled = false;
			this.radioButtonPin5Out.Location = new global::System.Drawing.Point(3, 3);
			this.radioButtonPin5Out.Name = "radioButtonPin5Out";
			this.radioButtonPin5Out.Size = new global::System.Drawing.Size(57, 17);
			this.radioButtonPin5Out.TabIndex = 0;
			this.radioButtonPin5Out.Text = "Output";
			this.radioButtonPin5Out.UseVisualStyleBackColor = true;
			this.radioButtonPin5Out.CheckedChanged += new global::System.EventHandler(this.radioButtonPin5Out_CheckedChanged);
			this.panel1.Controls.Add(this.radioButtonPin4In);
			this.panel1.Controls.Add(this.radioButtonPin4Out);
			this.panel1.Location = new global::System.Drawing.Point(170, 101);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(60, 45);
			this.panel1.TabIndex = 33;
			this.radioButtonPin4In.AutoSize = true;
			this.radioButtonPin4In.Checked = true;
			this.radioButtonPin4In.Enabled = false;
			this.radioButtonPin4In.Location = new global::System.Drawing.Point(3, 21);
			this.radioButtonPin4In.Name = "radioButtonPin4In";
			this.radioButtonPin4In.Size = new global::System.Drawing.Size(49, 17);
			this.radioButtonPin4In.TabIndex = 1;
			this.radioButtonPin4In.TabStop = true;
			this.radioButtonPin4In.Text = "Input";
			this.radioButtonPin4In.UseVisualStyleBackColor = true;
			this.radioButtonPin4Out.AutoSize = true;
			this.radioButtonPin4Out.Enabled = false;
			this.radioButtonPin4Out.Location = new global::System.Drawing.Point(3, 3);
			this.radioButtonPin4Out.Name = "radioButtonPin4Out";
			this.radioButtonPin4Out.Size = new global::System.Drawing.Size(57, 17);
			this.radioButtonPin4Out.TabIndex = 0;
			this.radioButtonPin4Out.Text = "Output";
			this.radioButtonPin4Out.UseVisualStyleBackColor = true;
			this.radioButtonPin4Out.CheckedChanged += new global::System.EventHandler(this.radioButtonPin4Out_CheckedChanged);
			this.label30.AutoSize = true;
			this.label30.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label30.Location = new global::System.Drawing.Point(167, 50);
			this.label30.Name = "label30";
			this.label30.Size = new global::System.Drawing.Size(63, 13);
			this.label30.TabIndex = 32;
			this.label30.Text = "Output Only";
			this.label29.AutoSize = true;
			this.label29.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label29.Location = new global::System.Drawing.Point(106, 243);
			this.label29.Name = "label29";
			this.label29.Size = new global::System.Drawing.Size(49, 20);
			this.label29.TabIndex = 31;
			this.label29.Text = "Pin 6";
			this.label28.AutoSize = true;
			this.label28.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label28.Location = new global::System.Drawing.Point(106, 177);
			this.label28.Name = "label28";
			this.label28.Size = new global::System.Drawing.Size(49, 20);
			this.label28.TabIndex = 30;
			this.label28.Text = "Pin 5";
			this.label27.AutoSize = true;
			this.label27.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label27.Location = new global::System.Drawing.Point(106, 111);
			this.label27.Name = "label27";
			this.label27.Size = new global::System.Drawing.Size(49, 20);
			this.label27.TabIndex = 29;
			this.label27.Text = "Pin 4";
			this.label26.AutoSize = true;
			this.label26.Location = new global::System.Drawing.Point(-3, 300);
			this.label26.Name = "label26";
			this.label26.Size = new global::System.Drawing.Size(356, 26);
			this.label26.TabIndex = 28;
			this.label26.Text = "PICkit 2 VDD pin MUST have a valid voltage (either sourced from PICkit 2\r\nor the target) or pins 4, 5, && 6 will not function.";
			this.label20.AutoSize = true;
			this.label20.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label20.Location = new global::System.Drawing.Point(106, 45);
			this.label20.Name = "label20";
			this.label20.Size = new global::System.Drawing.Size(49, 20);
			this.label20.TabIndex = 27;
			this.label20.Text = "Pin 1";
			this.pictureBox2.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("pictureBox2.Image");
			this.pictureBox2.Location = new global::System.Drawing.Point(0, 106);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new global::System.Drawing.Size(78, 116);
			this.pictureBox2.TabIndex = 26;
			this.pictureBox2.TabStop = false;
			this.timerIO.Tick += new global::System.EventHandler(this.timerIO_Tick);
			this.checkBoxVDD.AutoSize = true;
			this.checkBoxVDD.Location = new global::System.Drawing.Point(12, 16);
			this.checkBoxVDD.Name = "checkBoxVDD";
			this.checkBoxVDD.Size = new global::System.Drawing.Size(66, 17);
			this.checkBoxVDD.TabIndex = 34;
			this.checkBoxVDD.Text = "VDD On";
			this.checkBoxVDD.UseVisualStyleBackColor = true;
			this.checkBoxVDD.Click += new global::System.EventHandler(this.checkBoxVDD_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(96f, 96f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Dpi;
			base.ClientSize = new global::System.Drawing.Size(634, 401);
			base.Controls.Add(this.checkBoxVDD);
			base.Controls.Add(this.panelLogicIO);
			base.Controls.Add(this.buttonHelp);
			base.Controls.Add(this.buttonExit);
			base.Controls.Add(this.panelAnalyzer);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.panel2);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DialogLogic";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PICkit 2 Logic Tool";
			this.panel2.ResumeLayout(false);
			this.panelAnalyzer.ResumeLayout(false);
			this.panelAnalyzer.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBoxZoom.ResumeLayout(false);
			this.groupBoxZoom.PerformLayout();
			this.panelDisplay.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.pictureBoxDisplay).EndInit();
			this.panelLogicIO.ResumeLayout(false);
			this.panelLogicIO.PerformLayout();
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400056B RID: 1387
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400056C RID: 1388
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400056D RID: 1389
		private global::System.Windows.Forms.Panel panel2;

		// Token: 0x0400056E RID: 1390
		private global::System.Windows.Forms.RadioButton radioButtonAnalyzer;

		// Token: 0x0400056F RID: 1391
		private global::System.Windows.Forms.RadioButton radioButtonLogicIO;

		// Token: 0x04000570 RID: 1392
		private global::System.Windows.Forms.Panel panelAnalyzer;

		// Token: 0x04000571 RID: 1393
		private global::System.Windows.Forms.PictureBox pictureBoxDisplay;

		// Token: 0x04000572 RID: 1394
		private global::System.Windows.Forms.Panel panelDisplay;

		// Token: 0x04000573 RID: 1395
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000574 RID: 1396
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000575 RID: 1397
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000576 RID: 1398
		private global::System.Windows.Forms.GroupBox groupBoxZoom;

		// Token: 0x04000577 RID: 1399
		private global::System.Windows.Forms.Label labelTimeScale;

		// Token: 0x04000578 RID: 1400
		private global::System.Windows.Forms.RadioButton radioButton4x;

		// Token: 0x04000579 RID: 1401
		private global::System.Windows.Forms.RadioButton radioButtonZoom05;

		// Token: 0x0400057A RID: 1402
		private global::System.Windows.Forms.RadioButton radioButton2x;

		// Token: 0x0400057B RID: 1403
		private global::System.Windows.Forms.RadioButton radioButtonZoom1x;

		// Token: 0x0400057C RID: 1404
		private global::System.Windows.Forms.Label label8;

		// Token: 0x0400057D RID: 1405
		private global::System.Windows.Forms.ComboBox comboBoxCh3Trig;

		// Token: 0x0400057E RID: 1406
		private global::System.Windows.Forms.ComboBox comboBoxCh2Trig;

		// Token: 0x0400057F RID: 1407
		private global::System.Windows.Forms.ComboBox comboBoxCh1Trig;

		// Token: 0x04000580 RID: 1408
		private global::System.Windows.Forms.Label label7;

		// Token: 0x04000581 RID: 1409
		private global::System.Windows.Forms.Label label6;

		// Token: 0x04000582 RID: 1410
		private global::System.Windows.Forms.Label label5;

		// Token: 0x04000583 RID: 1411
		private global::System.Windows.Forms.Label label9;

		// Token: 0x04000584 RID: 1412
		private global::System.Windows.Forms.Label label14;

		// Token: 0x04000585 RID: 1413
		private global::System.Windows.Forms.Label label13;

		// Token: 0x04000586 RID: 1414
		private global::System.Windows.Forms.Label label12;

		// Token: 0x04000587 RID: 1415
		private global::System.Windows.Forms.Label label11;

		// Token: 0x04000588 RID: 1416
		private global::System.Windows.Forms.Label label10;

		// Token: 0x04000589 RID: 1417
		private global::System.Windows.Forms.Label label17;

		// Token: 0x0400058A RID: 1418
		private global::System.Windows.Forms.TextBox textBox1;

		// Token: 0x0400058B RID: 1419
		private global::System.Windows.Forms.Label label16;

		// Token: 0x0400058C RID: 1420
		private global::System.Windows.Forms.Label label15;

		// Token: 0x0400058D RID: 1421
		private global::System.Windows.Forms.Button buttonExit;

		// Token: 0x0400058E RID: 1422
		private global::System.Windows.Forms.GroupBox groupBox2;

		// Token: 0x0400058F RID: 1423
		private global::System.Windows.Forms.PictureBox pictureBox1;

		// Token: 0x04000590 RID: 1424
		private global::System.Windows.Forms.GroupBox groupBox1;

		// Token: 0x04000591 RID: 1425
		private global::System.Windows.Forms.Button buttonRun;

		// Token: 0x04000592 RID: 1426
		private global::System.Windows.Forms.Label label18;

		// Token: 0x04000593 RID: 1427
		private global::System.Windows.Forms.ComboBox comboBoxSampleRate;

		// Token: 0x04000594 RID: 1428
		private global::System.Windows.Forms.Label label19;

		// Token: 0x04000595 RID: 1429
		private global::System.Windows.Forms.RadioButton radioButtonTrigStart;

		// Token: 0x04000596 RID: 1430
		private global::System.Windows.Forms.Label label21;

		// Token: 0x04000597 RID: 1431
		private global::System.Windows.Forms.Label labelAliasFreq;

		// Token: 0x04000598 RID: 1432
		private global::System.Windows.Forms.RadioButton radioButtonTrigDly3;

		// Token: 0x04000599 RID: 1433
		private global::System.Windows.Forms.RadioButton radioButtonTrigDly2;

		// Token: 0x0400059A RID: 1434
		private global::System.Windows.Forms.RadioButton radioButtonTrigDly1;

		// Token: 0x0400059B RID: 1435
		private global::System.Windows.Forms.RadioButton radioButtonTrigEnd;

		// Token: 0x0400059C RID: 1436
		private global::System.Windows.Forms.RadioButton radioButtonTrigMid;

		// Token: 0x0400059D RID: 1437
		private global::System.Windows.Forms.Label label22;

		// Token: 0x0400059E RID: 1438
		private global::System.Windows.Forms.Label label23;

		// Token: 0x0400059F RID: 1439
		private global::System.Windows.Forms.Label label24;

		// Token: 0x040005A0 RID: 1440
		private global::System.Windows.Forms.Label label25;

		// Token: 0x040005A1 RID: 1441
		private global::System.Windows.Forms.Label labelCursor1Val;

		// Token: 0x040005A2 RID: 1442
		private global::System.Windows.Forms.CheckBox checkBoxCursors;

		// Token: 0x040005A3 RID: 1443
		private global::System.Windows.Forms.Label labelCursor1;

		// Token: 0x040005A4 RID: 1444
		private global::System.Windows.Forms.Label labelCursor2Val;

		// Token: 0x040005A5 RID: 1445
		private global::System.Windows.Forms.Label labelCursor2;

		// Token: 0x040005A6 RID: 1446
		private global::System.Windows.Forms.Label labelCursorDelta;

		// Token: 0x040005A7 RID: 1447
		private global::System.Windows.Forms.Label labelCursorDeltaVal;

		// Token: 0x040005A8 RID: 1448
		private global::System.Windows.Forms.Button buttonHelp;

		// Token: 0x040005A9 RID: 1449
		private global::System.Windows.Forms.Timer timerRun;

		// Token: 0x040005AA RID: 1450
		private global::System.Windows.Forms.Button buttonSave;

		// Token: 0x040005AB RID: 1451
		private global::System.Windows.Forms.SaveFileDialog saveFileDialogDisplay;

		// Token: 0x040005AC RID: 1452
		private global::System.Windows.Forms.Panel panelLogicIO;

		// Token: 0x040005AD RID: 1453
		private global::System.Windows.Forms.PictureBox pictureBox2;

		// Token: 0x040005AE RID: 1454
		private global::System.Windows.Forms.Label label20;

		// Token: 0x040005AF RID: 1455
		private global::System.Windows.Forms.Label label29;

		// Token: 0x040005B0 RID: 1456
		private global::System.Windows.Forms.Label label28;

		// Token: 0x040005B1 RID: 1457
		private global::System.Windows.Forms.Label label27;

		// Token: 0x040005B2 RID: 1458
		private global::System.Windows.Forms.Label label26;

		// Token: 0x040005B3 RID: 1459
		private global::System.Windows.Forms.Panel panel4;

		// Token: 0x040005B4 RID: 1460
		private global::System.Windows.Forms.RadioButton radioButtonPin6In;

		// Token: 0x040005B5 RID: 1461
		private global::System.Windows.Forms.RadioButton radioButtonPin6Out;

		// Token: 0x040005B6 RID: 1462
		private global::System.Windows.Forms.Panel panel3;

		// Token: 0x040005B7 RID: 1463
		private global::System.Windows.Forms.RadioButton radioButtonPin5In;

		// Token: 0x040005B8 RID: 1464
		private global::System.Windows.Forms.RadioButton radioButtonPin5Out;

		// Token: 0x040005B9 RID: 1465
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x040005BA RID: 1466
		private global::System.Windows.Forms.RadioButton radioButtonPin4In;

		// Token: 0x040005BB RID: 1467
		private global::System.Windows.Forms.RadioButton radioButtonPin4Out;

		// Token: 0x040005BC RID: 1468
		private global::System.Windows.Forms.Label label30;

		// Token: 0x040005BD RID: 1469
		private global::System.Windows.Forms.TextBox textBoxPin4In;

		// Token: 0x040005BE RID: 1470
		private global::System.Windows.Forms.Label label32;

		// Token: 0x040005BF RID: 1471
		private global::System.Windows.Forms.Label label31;

		// Token: 0x040005C0 RID: 1472
		private global::System.Windows.Forms.TextBox textBoxPin4Out;

		// Token: 0x040005C1 RID: 1473
		private global::System.Windows.Forms.TextBox textBoxPin1Out;

		// Token: 0x040005C2 RID: 1474
		private global::System.Windows.Forms.Label label34;

		// Token: 0x040005C3 RID: 1475
		private global::System.Windows.Forms.Label label33;

		// Token: 0x040005C4 RID: 1476
		private global::System.Windows.Forms.TextBox textBoxPin5In;

		// Token: 0x040005C5 RID: 1477
		private global::System.Windows.Forms.TextBox textBoxPin5Out;

		// Token: 0x040005C6 RID: 1478
		private global::System.Windows.Forms.TextBox textBoxPin6Out;

		// Token: 0x040005C7 RID: 1479
		private global::System.Windows.Forms.TextBox textBoxPin6In;

		// Token: 0x040005C8 RID: 1480
		private global::System.Windows.Forms.CheckBox checkBoxIOEnable;

		// Token: 0x040005C9 RID: 1481
		private global::System.Windows.Forms.Label labelOut6Click;

		// Token: 0x040005CA RID: 1482
		private global::System.Windows.Forms.Label labelOut5Click;

		// Token: 0x040005CB RID: 1483
		private global::System.Windows.Forms.Label labelOut4Click;

		// Token: 0x040005CC RID: 1484
		private global::System.Windows.Forms.Label labelOut1Click;

		// Token: 0x040005CD RID: 1485
		private global::System.Windows.Forms.Timer timerIO;

		// Token: 0x040005CE RID: 1486
		private global::System.Windows.Forms.CheckBox checkBoxVDD;
	}
}
