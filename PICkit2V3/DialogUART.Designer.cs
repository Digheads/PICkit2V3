namespace PICkit2V3
{
	// Token: 0x02000006 RID: 6
	public partial class DialogUART : global::System.Windows.Forms.Form
	{
		// Token: 0x0600004D RID: 77 RVA: 0x0000508B File Offset: 0x0000408B
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000050AC File Offset: 0x000040AC
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::PICkit2V3.DialogUART));
			this.textBoxDisplay = new global::System.Windows.Forms.TextBox();
			this.pictureBox1 = new global::System.Windows.Forms.PictureBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.radioButtonConnect = new global::System.Windows.Forms.RadioButton();
			this.radioButtonDisconnect = new global::System.Windows.Forms.RadioButton();
			this.comboBoxBaud = new global::System.Windows.Forms.ComboBox();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.panel2 = new global::System.Windows.Forms.Panel();
			this.radioButtonHex = new global::System.Windows.Forms.RadioButton();
			this.radioButtonASCII = new global::System.Windows.Forms.RadioButton();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.textBoxString1 = new global::System.Windows.Forms.TextBox();
			this.buttonString1 = new global::System.Windows.Forms.Button();
			this.buttonString2 = new global::System.Windows.Forms.Button();
			this.textBoxString2 = new global::System.Windows.Forms.TextBox();
			this.buttonString4 = new global::System.Windows.Forms.Button();
			this.buttonString3 = new global::System.Windows.Forms.Button();
			this.textBoxString3 = new global::System.Windows.Forms.TextBox();
			this.textBoxString4 = new global::System.Windows.Forms.TextBox();
			this.buttonLog = new global::System.Windows.Forms.Button();
			this.buttonClearScreen = new global::System.Windows.Forms.Button();
			this.buttonExit = new global::System.Windows.Forms.Button();
			this.checkBoxEcho = new global::System.Windows.Forms.CheckBox();
			this.labelMacros = new global::System.Windows.Forms.Label();
			this.timerPollForData = new global::System.Windows.Forms.Timer(this.components);
			this.label5 = new global::System.Windows.Forms.Label();
			this.checkBoxCRLF = new global::System.Windows.Forms.CheckBox();
			this.saveFileDialogLogFile = new global::System.Windows.Forms.SaveFileDialog();
			this.checkBoxWrap = new global::System.Windows.Forms.CheckBox();
			this.pictureBoxHelp = new global::System.Windows.Forms.PictureBox();
			this.checkBoxVDD = new global::System.Windows.Forms.CheckBox();
			this.panelVdd = new global::System.Windows.Forms.Panel();
			this.labelTypeHex = new global::System.Windows.Forms.Label();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBoxHelp).BeginInit();
			this.panelVdd.SuspendLayout();
			base.SuspendLayout();
			this.textBoxDisplay.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxDisplay.BackColor = global::System.Drawing.SystemColors.Window;
			this.textBoxDisplay.Cursor = global::System.Windows.Forms.Cursors.Default;
			this.textBoxDisplay.Font = new global::System.Drawing.Font("Courier New", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.textBoxDisplay.Location = new global::System.Drawing.Point(13, 44);
			this.textBoxDisplay.MaxLength = 17220;
			this.textBoxDisplay.MinimumSize = new global::System.Drawing.Size(708, 332);
			this.textBoxDisplay.Multiline = true;
			this.textBoxDisplay.Name = "textBoxDisplay";
			this.textBoxDisplay.ReadOnly = true;
			this.textBoxDisplay.ScrollBars = global::System.Windows.Forms.ScrollBars.Both;
			this.textBoxDisplay.Size = new global::System.Drawing.Size(708, 332);
			this.textBoxDisplay.TabIndex = 0;
			this.textBoxDisplay.Leave += new global::System.EventHandler(this.textBoxDisplay_Leave);
			this.pictureBox1.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.pictureBox1.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("pictureBox1.Image");
			this.pictureBox1.Location = new global::System.Drawing.Point(13, 385);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new global::System.Drawing.Size(189, 116);
			this.pictureBox1.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			this.label1.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(10, 504);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(181, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Connect PICkit 2 VDD && target VDD.";
			this.radioButtonConnect.Appearance = global::System.Windows.Forms.Appearance.Button;
			this.radioButtonConnect.AutoCheck = false;
			this.radioButtonConnect.Location = new global::System.Drawing.Point(130, 4);
			this.radioButtonConnect.Name = "radioButtonConnect";
			this.radioButtonConnect.Size = new global::System.Drawing.Size(70, 22);
			this.radioButtonConnect.TabIndex = 14;
			this.radioButtonConnect.Text = "Connect";
			this.radioButtonConnect.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.radioButtonConnect.UseVisualStyleBackColor = true;
			this.radioButtonConnect.Click += new global::System.EventHandler(this.radioButtonConnect_Click_1);
			this.radioButtonDisconnect.Appearance = global::System.Windows.Forms.Appearance.Button;
			this.radioButtonDisconnect.AutoCheck = false;
			this.radioButtonDisconnect.Checked = true;
			this.radioButtonDisconnect.Location = new global::System.Drawing.Point(206, 4);
			this.radioButtonDisconnect.Name = "radioButtonDisconnect";
			this.radioButtonDisconnect.Size = new global::System.Drawing.Size(70, 22);
			this.radioButtonDisconnect.TabIndex = 15;
			this.radioButtonDisconnect.TabStop = true;
			this.radioButtonDisconnect.Text = "Disconnect";
			this.radioButtonDisconnect.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.radioButtonDisconnect.UseVisualStyleBackColor = true;
			this.radioButtonDisconnect.Click += new global::System.EventHandler(this.radioButtonDisconnect_Click);
			this.comboBoxBaud.BackColor = global::System.Drawing.SystemColors.Info;
			this.comboBoxBaud.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxBaud.FormattingEnabled = true;
			this.comboBoxBaud.Items.AddRange(new object[]
			{
				"- Select Baud -"
			});
			this.comboBoxBaud.Location = new global::System.Drawing.Point(6, 5);
			this.comboBoxBaud.MaxDropDownItems = 12;
			this.comboBoxBaud.Name = "comboBoxBaud";
			this.comboBoxBaud.Size = new global::System.Drawing.Size(118, 21);
			this.comboBoxBaud.TabIndex = 13;
			this.comboBoxBaud.SelectedIndexChanged += new global::System.EventHandler(this.comboBoxBaud_SelectedIndexChanged);
			this.panel1.Controls.Add(this.comboBoxBaud);
			this.panel1.Controls.Add(this.radioButtonDisconnect);
			this.panel1.Controls.Add(this.radioButtonConnect);
			this.panel1.Location = new global::System.Drawing.Point(13, 8);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(280, 30);
			this.panel1.TabIndex = 6;
			this.panel2.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.panel2.Controls.Add(this.radioButtonHex);
			this.panel2.Controls.Add(this.radioButtonASCII);
			this.panel2.Location = new global::System.Drawing.Point(618, 9);
			this.panel2.Name = "panel2";
			this.panel2.Size = new global::System.Drawing.Size(106, 29);
			this.panel2.TabIndex = 7;
			this.radioButtonHex.Appearance = global::System.Windows.Forms.Appearance.Button;
			this.radioButtonHex.Location = new global::System.Drawing.Point(57, 3);
			this.radioButtonHex.Name = "radioButtonHex";
			this.radioButtonHex.Size = new global::System.Drawing.Size(47, 22);
			this.radioButtonHex.TabIndex = 17;
			this.radioButtonHex.Text = "Hex";
			this.radioButtonHex.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.radioButtonHex.UseVisualStyleBackColor = true;
			this.radioButtonHex.CheckedChanged += new global::System.EventHandler(this.radioButtonHex_CheckedChanged);
			this.radioButtonASCII.Appearance = global::System.Windows.Forms.Appearance.Button;
			this.radioButtonASCII.Checked = true;
			this.radioButtonASCII.Location = new global::System.Drawing.Point(3, 3);
			this.radioButtonASCII.Name = "radioButtonASCII";
			this.radioButtonASCII.Size = new global::System.Drawing.Size(47, 22);
			this.radioButtonASCII.TabIndex = 16;
			this.radioButtonASCII.TabStop = true;
			this.radioButtonASCII.Text = "ASCII";
			this.radioButtonASCII.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.radioButtonASCII.UseVisualStyleBackColor = true;
			this.radioButtonASCII.CheckedChanged += new global::System.EventHandler(this.radioButtonASCII_CheckedChanged);
			this.label2.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.label2.AutoSize = true;
			this.label2.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label2.Location = new global::System.Drawing.Point(570, 14);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(47, 15);
			this.label2.TabIndex = 8;
			this.label2.Text = "Mode:";
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(363, 8);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(164, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "8 data bits - No parity - 1 Stop bit.";
			this.textBoxString1.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxString1.BackColor = global::System.Drawing.SystemColors.Info;
			this.textBoxString1.Font = new global::System.Drawing.Font("Courier New", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.textBoxString1.Location = new global::System.Drawing.Point(306, 406);
			this.textBoxString1.Name = "textBoxString1";
			this.textBoxString1.Size = new global::System.Drawing.Size(286, 20);
			this.textBoxString1.TabIndex = 1;
			this.textBoxString1.TextChanged += new global::System.EventHandler(this.textBoxString1_TextChanged);
			this.buttonString1.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.buttonString1.Enabled = false;
			this.buttonString1.Location = new global::System.Drawing.Point(246, 404);
			this.buttonString1.Name = "buttonString1";
			this.buttonString1.Size = new global::System.Drawing.Size(54, 22);
			this.buttonString1.TabIndex = 5;
			this.buttonString1.Text = "Send";
			this.buttonString1.UseVisualStyleBackColor = true;
			this.buttonString1.Click += new global::System.EventHandler(this.buttonString1_Click);
			this.buttonString2.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.buttonString2.Enabled = false;
			this.buttonString2.Location = new global::System.Drawing.Point(246, 434);
			this.buttonString2.Name = "buttonString2";
			this.buttonString2.Size = new global::System.Drawing.Size(54, 22);
			this.buttonString2.TabIndex = 6;
			this.buttonString2.Text = "Send";
			this.buttonString2.UseVisualStyleBackColor = true;
			this.buttonString2.Click += new global::System.EventHandler(this.buttonString2_Click);
			this.textBoxString2.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxString2.BackColor = global::System.Drawing.SystemColors.Info;
			this.textBoxString2.Font = new global::System.Drawing.Font("Courier New", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.textBoxString2.Location = new global::System.Drawing.Point(306, 435);
			this.textBoxString2.Name = "textBoxString2";
			this.textBoxString2.Size = new global::System.Drawing.Size(286, 20);
			this.textBoxString2.TabIndex = 2;
			this.textBoxString2.TextChanged += new global::System.EventHandler(this.textBoxString2_TextChanged);
			this.buttonString4.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.buttonString4.Enabled = false;
			this.buttonString4.Location = new global::System.Drawing.Point(246, 490);
			this.buttonString4.Name = "buttonString4";
			this.buttonString4.Size = new global::System.Drawing.Size(54, 22);
			this.buttonString4.TabIndex = 8;
			this.buttonString4.Text = "Send";
			this.buttonString4.UseVisualStyleBackColor = true;
			this.buttonString4.Click += new global::System.EventHandler(this.buttonString4_Click);
			this.buttonString3.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.buttonString3.Enabled = false;
			this.buttonString3.Location = new global::System.Drawing.Point(246, 462);
			this.buttonString3.Name = "buttonString3";
			this.buttonString3.Size = new global::System.Drawing.Size(54, 22);
			this.buttonString3.TabIndex = 7;
			this.buttonString3.Text = "Send";
			this.buttonString3.UseVisualStyleBackColor = true;
			this.buttonString3.Click += new global::System.EventHandler(this.buttonString3_Click);
			this.textBoxString3.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxString3.BackColor = global::System.Drawing.SystemColors.Info;
			this.textBoxString3.Font = new global::System.Drawing.Font("Courier New", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.textBoxString3.Location = new global::System.Drawing.Point(306, 464);
			this.textBoxString3.Name = "textBoxString3";
			this.textBoxString3.Size = new global::System.Drawing.Size(286, 20);
			this.textBoxString3.TabIndex = 3;
			this.textBoxString3.TextChanged += new global::System.EventHandler(this.textBoxString3_TextChanged);
			this.textBoxString4.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxString4.BackColor = global::System.Drawing.SystemColors.Info;
			this.textBoxString4.Font = new global::System.Drawing.Font("Courier New", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.textBoxString4.Location = new global::System.Drawing.Point(306, 492);
			this.textBoxString4.Name = "textBoxString4";
			this.textBoxString4.Size = new global::System.Drawing.Size(286, 20);
			this.textBoxString4.TabIndex = 4;
			this.textBoxString4.TextChanged += new global::System.EventHandler(this.textBoxString4_TextChanged);
			this.buttonLog.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonLog.Location = new global::System.Drawing.Point(618, 404);
			this.buttonLog.Name = "buttonLog";
			this.buttonLog.Size = new global::System.Drawing.Size(102, 22);
			this.buttonLog.TabIndex = 9;
			this.buttonLog.Text = "Log to File";
			this.buttonLog.UseVisualStyleBackColor = true;
			this.buttonLog.Click += new global::System.EventHandler(this.buttonLog_Click);
			this.buttonClearScreen.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonClearScreen.Location = new global::System.Drawing.Point(618, 434);
			this.buttonClearScreen.Name = "buttonClearScreen";
			this.buttonClearScreen.Size = new global::System.Drawing.Size(102, 22);
			this.buttonClearScreen.TabIndex = 10;
			this.buttonClearScreen.Text = "Clear Screen";
			this.buttonClearScreen.UseVisualStyleBackColor = true;
			this.buttonClearScreen.Click += new global::System.EventHandler(this.buttonClearScreen_Click);
			this.buttonExit.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonExit.Location = new global::System.Drawing.Point(618, 490);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new global::System.Drawing.Size(102, 22);
			this.buttonExit.TabIndex = 12;
			this.buttonExit.Text = "Exit UART Tool";
			this.buttonExit.UseVisualStyleBackColor = true;
			this.buttonExit.Click += new global::System.EventHandler(this.buttonExit_Click);
			this.checkBoxEcho.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.checkBoxEcho.AutoSize = true;
			this.checkBoxEcho.Checked = true;
			this.checkBoxEcho.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.checkBoxEcho.Location = new global::System.Drawing.Point(638, 466);
			this.checkBoxEcho.Name = "checkBoxEcho";
			this.checkBoxEcho.Size = new global::System.Drawing.Size(68, 17);
			this.checkBoxEcho.TabIndex = 11;
			this.checkBoxEcho.Text = "Echo On";
			this.checkBoxEcho.UseVisualStyleBackColor = true;
			this.labelMacros.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.labelMacros.AutoSize = true;
			this.labelMacros.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelMacros.Location = new global::System.Drawing.Point(243, 382);
			this.labelMacros.Name = "labelMacros";
			this.labelMacros.Size = new global::System.Drawing.Size(100, 15);
			this.labelMacros.TabIndex = 22;
			this.labelMacros.Text = "String Macros:";
			this.timerPollForData.Interval = 15;
			this.timerPollForData.Tick += new global::System.EventHandler(this.timerPollForData_Tick);
			this.label5.AutoSize = true;
			this.label5.Location = new global::System.Drawing.Point(375, 22);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(137, 13);
			this.label5.TabIndex = 23;
			this.label5.Text = "ASCII newline = 0x0D 0x0A";
			this.checkBoxCRLF.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.checkBoxCRLF.AutoSize = true;
			this.checkBoxCRLF.Checked = true;
			this.checkBoxCRLF.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.checkBoxCRLF.Location = new global::System.Drawing.Point(365, 383);
			this.checkBoxCRLF.Name = "checkBoxCRLF";
			this.checkBoxCRLF.Size = new global::System.Drawing.Size(157, 17);
			this.checkBoxCRLF.TabIndex = 18;
			this.checkBoxCRLF.Text = "Append CR+LF (x0D + x0A)";
			this.checkBoxCRLF.UseVisualStyleBackColor = true;
			this.saveFileDialogLogFile.DefaultExt = "txt";
			this.saveFileDialogLogFile.Filter = "All files|*.*|Text files|*.txt";
			this.saveFileDialogLogFile.InitialDirectory = "c:\\";
			this.saveFileDialogLogFile.Title = "Log UART data to file";
			this.saveFileDialogLogFile.FileOk += new global::System.ComponentModel.CancelEventHandler(this.saveFileDialogLogFile_FileOk);
			this.checkBoxWrap.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.checkBoxWrap.AutoSize = true;
			this.checkBoxWrap.Checked = true;
			this.checkBoxWrap.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.checkBoxWrap.Location = new global::System.Drawing.Point(638, 383);
			this.checkBoxWrap.Name = "checkBoxWrap";
			this.checkBoxWrap.Size = new global::System.Drawing.Size(76, 17);
			this.checkBoxWrap.TabIndex = 24;
			this.checkBoxWrap.Text = "Wrap Text";
			this.checkBoxWrap.UseVisualStyleBackColor = true;
			this.checkBoxWrap.CheckedChanged += new global::System.EventHandler(this.checkBoxWrap_CheckedChanged);
			this.pictureBoxHelp.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.pictureBoxHelp.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("pictureBoxHelp.Image");
			this.pictureBoxHelp.Location = new global::System.Drawing.Point(540, 10);
			this.pictureBoxHelp.Name = "pictureBoxHelp";
			this.pictureBoxHelp.Size = new global::System.Drawing.Size(24, 24);
			this.pictureBoxHelp.TabIndex = 26;
			this.pictureBoxHelp.TabStop = false;
			this.pictureBoxHelp.Click += new global::System.EventHandler(this.pictureBoxHelp_Click);
			this.checkBoxVDD.AutoSize = true;
			this.checkBoxVDD.Location = new global::System.Drawing.Point(6, 5);
			this.checkBoxVDD.Name = "checkBoxVDD";
			this.checkBoxVDD.Size = new global::System.Drawing.Size(49, 17);
			this.checkBoxVDD.TabIndex = 27;
			this.checkBoxVDD.Text = "VDD";
			this.checkBoxVDD.UseVisualStyleBackColor = true;
			this.checkBoxVDD.Click += new global::System.EventHandler(this.checkBoxVDD_Click);
			this.panelVdd.Controls.Add(this.checkBoxVDD);
			this.panelVdd.Location = new global::System.Drawing.Point(299, 10);
			this.panelVdd.Name = "panelVdd";
			this.panelVdd.Size = new global::System.Drawing.Size(65, 27);
			this.panelVdd.TabIndex = 28;
			this.labelTypeHex.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.labelTypeHex.AutoSize = true;
			this.labelTypeHex.ForeColor = global::System.Drawing.SystemColors.ControlDarkDark;
			this.labelTypeHex.Location = new global::System.Drawing.Point(520, 384);
			this.labelTypeHex.Name = "labelTypeHex";
			this.labelTypeHex.Size = new global::System.Drawing.Size(75, 13);
			this.labelTypeHex.TabIndex = 29;
			this.labelTypeHex.Text = "Type Hex : A_";
			this.labelTypeHex.Visible = false;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(96f, 96f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Dpi;
			base.ClientSize = new global::System.Drawing.Size(736, 526);
			base.Controls.Add(this.labelTypeHex);
			base.Controls.Add(this.panelVdd);
			base.Controls.Add(this.pictureBoxHelp);
			base.Controls.Add(this.checkBoxWrap);
			base.Controls.Add(this.checkBoxCRLF);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.labelMacros);
			base.Controls.Add(this.checkBoxEcho);
			base.Controls.Add(this.buttonExit);
			base.Controls.Add(this.buttonClearScreen);
			base.Controls.Add(this.buttonLog);
			base.Controls.Add(this.textBoxString4);
			base.Controls.Add(this.textBoxString3);
			base.Controls.Add(this.buttonString3);
			base.Controls.Add(this.buttonString4);
			base.Controls.Add(this.textBoxString2);
			base.Controls.Add(this.buttonString2);
			base.Controls.Add(this.buttonString1);
			base.Controls.Add(this.textBoxString1);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.pictureBox1);
			base.Controls.Add(this.textBoxDisplay);
			base.KeyPreview = true;
			this.MinimumSize = new global::System.Drawing.Size(744, 559);
			base.Name = "DialogUART";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PICkit 2 UART Tool";
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.DialogUART_FormClosing);
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.pictureBoxHelp).EndInit();
			this.panelVdd.ResumeLayout(false);
			this.panelVdd.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000024 RID: 36
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000025 RID: 37
		private global::System.Windows.Forms.TextBox textBoxDisplay;

		// Token: 0x04000026 RID: 38
		private global::System.Windows.Forms.PictureBox pictureBox1;

		// Token: 0x04000027 RID: 39
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000028 RID: 40
		private global::System.Windows.Forms.RadioButton radioButtonConnect;

		// Token: 0x04000029 RID: 41
		private global::System.Windows.Forms.RadioButton radioButtonDisconnect;

		// Token: 0x0400002A RID: 42
		private global::System.Windows.Forms.ComboBox comboBoxBaud;

		// Token: 0x0400002B RID: 43
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x0400002C RID: 44
		private global::System.Windows.Forms.Panel panel2;

		// Token: 0x0400002D RID: 45
		private global::System.Windows.Forms.RadioButton radioButtonHex;

		// Token: 0x0400002E RID: 46
		private global::System.Windows.Forms.RadioButton radioButtonASCII;

		// Token: 0x0400002F RID: 47
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000030 RID: 48
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000031 RID: 49
		private global::System.Windows.Forms.TextBox textBoxString1;

		// Token: 0x04000032 RID: 50
		private global::System.Windows.Forms.Button buttonString1;

		// Token: 0x04000033 RID: 51
		private global::System.Windows.Forms.Button buttonString2;

		// Token: 0x04000034 RID: 52
		private global::System.Windows.Forms.TextBox textBoxString2;

		// Token: 0x04000035 RID: 53
		private global::System.Windows.Forms.Button buttonString4;

		// Token: 0x04000036 RID: 54
		private global::System.Windows.Forms.Button buttonString3;

		// Token: 0x04000037 RID: 55
		private global::System.Windows.Forms.TextBox textBoxString3;

		// Token: 0x04000038 RID: 56
		private global::System.Windows.Forms.TextBox textBoxString4;

		// Token: 0x04000039 RID: 57
		private global::System.Windows.Forms.Button buttonLog;

		// Token: 0x0400003A RID: 58
		private global::System.Windows.Forms.Button buttonClearScreen;

		// Token: 0x0400003B RID: 59
		private global::System.Windows.Forms.Button buttonExit;

		// Token: 0x0400003C RID: 60
		private global::System.Windows.Forms.CheckBox checkBoxEcho;

		// Token: 0x0400003D RID: 61
		private global::System.Windows.Forms.Label labelMacros;

		// Token: 0x0400003E RID: 62
		private global::System.Windows.Forms.Timer timerPollForData;

		// Token: 0x0400003F RID: 63
		private global::System.Windows.Forms.Label label5;

		// Token: 0x04000040 RID: 64
		private global::System.Windows.Forms.CheckBox checkBoxCRLF;

		// Token: 0x04000041 RID: 65
		private global::System.Windows.Forms.SaveFileDialog saveFileDialogLogFile;

		// Token: 0x04000042 RID: 66
		private global::System.Windows.Forms.CheckBox checkBoxWrap;

		// Token: 0x04000043 RID: 67
		private global::System.Windows.Forms.PictureBox pictureBoxHelp;

		// Token: 0x04000044 RID: 68
		private global::System.Windows.Forms.CheckBox checkBoxVDD;

		// Token: 0x04000045 RID: 69
		private global::System.Windows.Forms.Panel panelVdd;

		// Token: 0x04000046 RID: 70
		private global::System.Windows.Forms.Label labelTypeHex;
	}
}
