namespace PICkit2V3
{
	// Token: 0x02000026 RID: 38
	public partial class DialogPK2Go : global::System.Windows.Forms.Form
	{
		// Token: 0x06000203 RID: 515 RVA: 0x0003C80E File Offset: 0x0003B80E
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0003C830 File Offset: 0x0003B830
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::PICkit2V3.DialogPK2Go));
			this.panelIntro = new global::System.Windows.Forms.Panel();
			this.label8 = new global::System.Windows.Forms.Label();
			this.label15 = new global::System.Windows.Forms.Label();
			this.label16 = new global::System.Windows.Forms.Label();
			this.buttonBack = new global::System.Windows.Forms.Button();
			this.buttonNext = new global::System.Windows.Forms.Button();
			this.buttonCancel = new global::System.Windows.Forms.Button();
			this.buttonHelp = new global::System.Windows.Forms.Button();
			this.panelSettings = new global::System.Windows.Forms.Panel();
			this.groupBox2 = new global::System.Windows.Forms.GroupBox();
			this.label14 = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			this.labelVerify = new global::System.Windows.Forms.Label();
			this.labelMemRegions = new global::System.Windows.Forms.Label();
			this.label11 = new global::System.Windows.Forms.Label();
			this.labelDataProtect = new global::System.Windows.Forms.Label();
			this.label9 = new global::System.Windows.Forms.Label();
			this.labelCodeProtect = new global::System.Windows.Forms.Label();
			this.label10 = new global::System.Windows.Forms.Label();
			this.labelOSCCAL_BandGap = new global::System.Windows.Forms.Label();
			this.labelDataSource = new global::System.Windows.Forms.Label();
			this.label5 = new global::System.Windows.Forms.Label();
			this.labelPartNumber = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.groupBox1 = new global::System.Windows.Forms.GroupBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.labelRowErase = new global::System.Windows.Forms.Label();
			this.checkBoxRowErase = new global::System.Windows.Forms.CheckBox();
			this.radioButtonPK2Power = new global::System.Windows.Forms.RadioButton();
			this.radioButtonSelfPower = new global::System.Windows.Forms.RadioButton();
			this.label1 = new global::System.Windows.Forms.Label();
			this.panelDownload = new global::System.Windows.Forms.Panel();
			this.label7 = new global::System.Windows.Forms.Label();
			this.groupBox3 = new global::System.Windows.Forms.GroupBox();
			this.labelSourceSmmry = new global::System.Windows.Forms.Label();
			this.labelTargetPowerSmmry = new global::System.Windows.Forms.Label();
			this.label12 = new global::System.Windows.Forms.Label();
			this.labelVDDMin = new global::System.Windows.Forms.Label();
			this.labelPNsmmry = new global::System.Windows.Forms.Label();
			this.label6 = new global::System.Windows.Forms.Label();
			this.labelMemRegionsSmmry = new global::System.Windows.Forms.Label();
			this.labelVPP1stSmmry = new global::System.Windows.Forms.Label();
			this.labelVerifySmmry = new global::System.Windows.Forms.Label();
			this.labelFastProgSmmry = new global::System.Windows.Forms.Label();
			this.labelMCLRHoldSmmry = new global::System.Windows.Forms.Label();
			this.panelDownloading = new global::System.Windows.Forms.Panel();
			this.labelDOWNLOADING = new global::System.Windows.Forms.Label();
			this.panelDownloadDone = new global::System.Windows.Forms.Panel();
			this.label13 = new global::System.Windows.Forms.Label();
			this.label17 = new global::System.Windows.Forms.Label();
			this.timerBlink = new global::System.Windows.Forms.Timer(this.components);
			this.pictureBoxTarget = new global::System.Windows.Forms.PictureBox();
			this.label18 = new global::System.Windows.Forms.Label();
			this.label19 = new global::System.Windows.Forms.Label();
			this.label20 = new global::System.Windows.Forms.Label();
			this.panelErrors = new global::System.Windows.Forms.Panel();
			this.label21 = new global::System.Windows.Forms.Label();
			this.label22 = new global::System.Windows.Forms.Label();
			this.pictureBoxBusy = new global::System.Windows.Forms.PictureBox();
			this.label23 = new global::System.Windows.Forms.Label();
			this.label24 = new global::System.Windows.Forms.Label();
			this.radioButtonVErr = new global::System.Windows.Forms.RadioButton();
			this.radioButton2Blinks = new global::System.Windows.Forms.RadioButton();
			this.radioButton3Blinks = new global::System.Windows.Forms.RadioButton();
			this.radioButton4Blinks = new global::System.Windows.Forms.RadioButton();
			this.label25 = new global::System.Windows.Forms.Label();
			this.label26 = new global::System.Windows.Forms.Label();
			this.label27 = new global::System.Windows.Forms.Label();
			this.label28 = new global::System.Windows.Forms.Label();
			this.label29 = new global::System.Windows.Forms.Label();
			this.label256K = new global::System.Windows.Forms.Label();
			this.panelIntro.SuspendLayout();
			this.panelSettings.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.panelDownload.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.panelDownloading.SuspendLayout();
			this.panelDownloadDone.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBoxTarget).BeginInit();
			this.panelErrors.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBoxBusy).BeginInit();
			base.SuspendLayout();
			this.panelIntro.Controls.Add(this.label256K);
			this.panelIntro.Controls.Add(this.label8);
			this.panelIntro.Controls.Add(this.label15);
			this.panelIntro.Controls.Add(this.label16);
			this.panelIntro.Location = new global::System.Drawing.Point(12, 12);
			this.panelIntro.Name = "panelIntro";
			this.panelIntro.Size = new global::System.Drawing.Size(351, 331);
			this.panelIntro.TabIndex = 0;
			this.label8.AutoSize = true;
			this.label8.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label8.Location = new global::System.Drawing.Point(69, 31);
			this.label8.Name = "label8";
			this.label8.Size = new global::System.Drawing.Size(213, 19);
			this.label8.TabIndex = 5;
			this.label8.Text = "Programmer-To-Go Wizard";
			this.label15.AutoSize = true;
			this.label15.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label15.Location = new global::System.Drawing.Point(29, 90);
			this.label15.Name = "label15";
			this.label15.Size = new global::System.Drawing.Size(290, 195);
			this.label15.TabIndex = 4;
			this.label15.Text = componentResourceManager.GetString("label15.Text");
			this.label16.AutoSize = true;
			this.label16.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label16.Location = new global::System.Drawing.Point(81, 12);
			this.label16.Name = "label16";
			this.label16.Size = new global::System.Drawing.Size(188, 19);
			this.label16.TabIndex = 2;
			this.label16.Text = "Welcome to the PICkit 2";
			this.buttonBack.Enabled = false;
			this.buttonBack.Location = new global::System.Drawing.Point(102, 350);
			this.buttonBack.Name = "buttonBack";
			this.buttonBack.Size = new global::System.Drawing.Size(82, 22);
			this.buttonBack.TabIndex = 7;
			this.buttonBack.Text = "< Back";
			this.buttonBack.UseVisualStyleBackColor = true;
			this.buttonBack.Click += new global::System.EventHandler(this.buttonBack_Click);
			this.buttonNext.Location = new global::System.Drawing.Point(192, 350);
			this.buttonNext.Name = "buttonNext";
			this.buttonNext.Size = new global::System.Drawing.Size(82, 22);
			this.buttonNext.TabIndex = 6;
			this.buttonNext.Text = "Next >";
			this.buttonNext.UseVisualStyleBackColor = true;
			this.buttonNext.Click += new global::System.EventHandler(this.buttonNext_Click);
			this.buttonCancel.Location = new global::System.Drawing.Point(282, 350);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new global::System.Drawing.Size(82, 22);
			this.buttonCancel.TabIndex = 5;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new global::System.EventHandler(this.buttonCancel_Click);
			this.buttonHelp.Location = new global::System.Drawing.Point(12, 350);
			this.buttonHelp.Name = "buttonHelp";
			this.buttonHelp.Size = new global::System.Drawing.Size(82, 22);
			this.buttonHelp.TabIndex = 6;
			this.buttonHelp.Text = "Help";
			this.buttonHelp.UseVisualStyleBackColor = true;
			this.buttonHelp.Click += new global::System.EventHandler(this.buttonHelp_Click);
			this.panelSettings.Controls.Add(this.groupBox2);
			this.panelSettings.Controls.Add(this.groupBox1);
			this.panelSettings.Controls.Add(this.label1);
			this.panelSettings.Location = new global::System.Drawing.Point(12, 12);
			this.panelSettings.Name = "panelSettings";
			this.panelSettings.Size = new global::System.Drawing.Size(351, 331);
			this.panelSettings.TabIndex = 8;
			this.panelSettings.Visible = false;
			this.groupBox2.Controls.Add(this.label14);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.labelVerify);
			this.groupBox2.Controls.Add(this.labelMemRegions);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.labelDataProtect);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.labelCodeProtect);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.labelOSCCAL_BandGap);
			this.groupBox2.Controls.Add(this.labelDataSource);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.labelPartNumber);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Bold | global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.groupBox2.Location = new global::System.Drawing.Point(12, 34);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new global::System.Drawing.Size(325, 154);
			this.groupBox2.TabIndex = 6;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Buffer Settings";
			this.label14.AutoSize = true;
			this.label14.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label14.Location = new global::System.Drawing.Point(6, 109);
			this.label14.Name = "label14";
			this.label14.Size = new global::System.Drawing.Size(79, 15);
			this.label14.TabIndex = 18;
			this.label14.Text = "Verify Device:";
			this.label4.AutoSize = true;
			this.label4.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label4.Location = new global::System.Drawing.Point(59, 135);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(198, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Click CANCEL to change buffer settings.\r\n";
			this.labelVerify.AutoSize = true;
			this.labelVerify.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelVerify.Location = new global::System.Drawing.Point(90, 109);
			this.labelVerify.Name = "labelVerify";
			this.labelVerify.Size = new global::System.Drawing.Size(30, 15);
			this.labelVerify.TabIndex = 17;
			this.labelVerify.Text = "Yes";
			this.labelMemRegions.AutoSize = true;
			this.labelMemRegions.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelMemRegions.Location = new global::System.Drawing.Point(115, 94);
			this.labelMemRegions.Name = "labelMemRegions";
			this.labelMemRegions.Size = new global::System.Drawing.Size(151, 15);
			this.labelMemRegions.TabIndex = 12;
			this.labelMemRegions.Text = "Program Entire Device";
			this.label11.AutoSize = true;
			this.label11.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label11.Location = new global::System.Drawing.Point(5, 94);
			this.label11.Name = "label11";
			this.label11.Size = new global::System.Drawing.Size(104, 15);
			this.label11.TabIndex = 15;
			this.label11.Text = "Memory Regions:";
			this.labelDataProtect.AutoSize = true;
			this.labelDataProtect.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelDataProtect.Location = new global::System.Drawing.Point(251, 79);
			this.labelDataProtect.Name = "labelDataProtect";
			this.labelDataProtect.Size = new global::System.Drawing.Size(25, 15);
			this.labelDataProtect.TabIndex = 14;
			this.labelDataProtect.Text = "NA";
			this.label9.AutoSize = true;
			this.label9.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label9.Location = new global::System.Drawing.Point(168, 79);
			this.label9.Name = "label9";
			this.label9.Size = new global::System.Drawing.Size(77, 15);
			this.label9.TabIndex = 12;
			this.label9.Text = "Data Protect:";
			this.labelCodeProtect.AutoSize = true;
			this.labelCodeProtect.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelCodeProtect.Location = new global::System.Drawing.Point(91, 79);
			this.labelCodeProtect.Name = "labelCodeProtect";
			this.labelCodeProtect.Size = new global::System.Drawing.Size(27, 15);
			this.labelCodeProtect.TabIndex = 13;
			this.labelCodeProtect.Text = "ON";
			this.label10.AutoSize = true;
			this.label10.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label10.Location = new global::System.Drawing.Point(5, 79);
			this.label10.Name = "label10";
			this.label10.Size = new global::System.Drawing.Size(80, 15);
			this.label10.TabIndex = 11;
			this.label10.Text = "Code Protect:";
			this.labelOSCCAL_BandGap.AutoSize = true;
			this.labelOSCCAL_BandGap.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Bold | global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelOSCCAL_BandGap.Location = new global::System.Drawing.Point(59, 33);
			this.labelOSCCAL_BandGap.Name = "labelOSCCAL_BandGap";
			this.labelOSCCAL_BandGap.Size = new global::System.Drawing.Size(177, 15);
			this.labelOSCCAL_BandGap.TabIndex = 10;
			this.labelOSCCAL_BandGap.Text = "OSCCAL will be preserved.";
			this.labelOSCCAL_BandGap.Visible = false;
			this.labelDataSource.AutoSize = true;
			this.labelDataSource.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelDataSource.Location = new global::System.Drawing.Point(32, 63);
			this.labelDataSource.Name = "labelDataSource";
			this.labelDataSource.Size = new global::System.Drawing.Size(88, 13);
			this.labelDataSource.TabIndex = 8;
			this.labelDataSource.Text = "<DataSource>";
			this.label5.AutoSize = true;
			this.label5.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label5.Location = new global::System.Drawing.Point(6, 48);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(109, 15);
			this.label5.TabIndex = 7;
			this.label5.Text = "Buffer data source:";
			this.labelPartNumber.AutoSize = true;
			this.labelPartNumber.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelPartNumber.Location = new global::System.Drawing.Point(59, 18);
			this.labelPartNumber.Name = "labelPartNumber";
			this.labelPartNumber.Size = new global::System.Drawing.Size(100, 15);
			this.labelPartNumber.TabIndex = 6;
			this.labelPartNumber.Text = "<PartNumber>";
			this.label3.AutoSize = true;
			this.label3.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label3.Location = new global::System.Drawing.Point(6, 18);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(47, 15);
			this.label3.TabIndex = 5;
			this.label3.Text = "Device:";
			this.groupBox1.Controls.Add(this.labelVDDMin);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.labelRowErase);
			this.groupBox1.Controls.Add(this.checkBoxRowErase);
			this.groupBox1.Controls.Add(this.radioButtonPK2Power);
			this.groupBox1.Controls.Add(this.radioButtonSelfPower);
			this.groupBox1.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Bold | global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.groupBox1.Location = new global::System.Drawing.Point(12, 194);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new global::System.Drawing.Size(325, 137);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Power Settings";
			this.label2.AutoSize = true;
			this.label2.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label2.Location = new global::System.Drawing.Point(66, 107);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(191, 26);
			this.label2.TabIndex = 5;
			this.label2.Text = "To change PICkit 2 VDD voltage, click\r\n    CANCEL and adjust the VDD box.\r\n";
			this.labelRowErase.AutoSize = true;
			this.labelRowErase.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelRowErase.ForeColor = global::System.Drawing.Color.OrangeRed;
			this.labelRowErase.Location = new global::System.Drawing.Point(13, 89);
			this.labelRowErase.Name = "labelRowErase";
			this.labelRowErase.Size = new global::System.Drawing.Size(281, 13);
			this.labelRowErase.TabIndex = 11;
			this.labelRowErase.Text = "Row Erase used: Will NOT program Code Protected parts!";
			this.labelRowErase.Visible = false;
			this.checkBoxRowErase.AutoSize = true;
			this.checkBoxRowErase.Enabled = false;
			this.checkBoxRowErase.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.checkBoxRowErase.Location = new global::System.Drawing.Point(48, 44);
			this.checkBoxRowErase.Name = "checkBoxRowErase";
			this.checkBoxRowErase.Size = new global::System.Drawing.Size(176, 20);
			this.checkBoxRowErase.TabIndex = 2;
			this.checkBoxRowErase.Text = "Use low voltage row erase";
			this.checkBoxRowErase.UseVisualStyleBackColor = true;
			this.checkBoxRowErase.CheckedChanged += new global::System.EventHandler(this.checkBoxRowErase_CheckedChanged);
			this.radioButtonPK2Power.AutoSize = true;
			this.radioButtonPK2Power.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.radioButtonPK2Power.Location = new global::System.Drawing.Point(16, 66);
			this.radioButtonPK2Power.Name = "radioButtonPK2Power";
			this.radioButtonPK2Power.Size = new global::System.Drawing.Size(249, 20);
			this.radioButtonPK2Power.TabIndex = 1;
			this.radioButtonPK2Power.TabStop = true;
			this.radioButtonPK2Power.Text = "Power target from PICkit 2 at 0.0 Volts";
			this.radioButtonPK2Power.UseVisualStyleBackColor = true;
			this.radioButtonPK2Power.Click += new global::System.EventHandler(this.radioButtonPK2Power_Click);
			this.radioButtonSelfPower.AutoSize = true;
			this.radioButtonSelfPower.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.radioButtonSelfPower.Location = new global::System.Drawing.Point(16, 21);
			this.radioButtonSelfPower.Name = "radioButtonSelfPower";
			this.radioButtonSelfPower.Size = new global::System.Drawing.Size(216, 20);
			this.radioButtonSelfPower.TabIndex = 0;
			this.radioButtonSelfPower.TabStop = true;
			this.radioButtonSelfPower.Text = "Target has its own power supply.";
			this.radioButtonSelfPower.UseVisualStyleBackColor = true;
			this.radioButtonSelfPower.Click += new global::System.EventHandler(this.radioButtonSelfPower_Click);
			this.label1.AutoSize = true;
			this.label1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new global::System.Drawing.Point(90, 12);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(171, 19);
			this.label1.TabIndex = 3;
			this.label1.Text = "Programmer Settings";
			this.panelDownload.Controls.Add(this.label7);
			this.panelDownload.Controls.Add(this.groupBox3);
			this.panelDownload.Controls.Add(this.label12);
			this.panelDownload.Location = new global::System.Drawing.Point(12, 12);
			this.panelDownload.Name = "panelDownload";
			this.panelDownload.Size = new global::System.Drawing.Size(351, 331);
			this.panelDownload.TabIndex = 9;
			this.panelDownload.Visible = false;
			this.label7.AutoSize = true;
			this.label7.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label7.Location = new global::System.Drawing.Point(70, 239);
			this.label7.Name = "label7";
			this.label7.Size = new global::System.Drawing.Size(212, 45);
			this.label7.TabIndex = 7;
			this.label7.Text = "Click the DOWNLOAD button below to\r\nset up PICkit 2 for Programmer-To-Go\r\noperation.\r\n";
			this.groupBox3.Controls.Add(this.labelMCLRHoldSmmry);
			this.groupBox3.Controls.Add(this.labelFastProgSmmry);
			this.groupBox3.Controls.Add(this.labelVerifySmmry);
			this.groupBox3.Controls.Add(this.labelVPP1stSmmry);
			this.groupBox3.Controls.Add(this.labelMemRegionsSmmry);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.labelPNsmmry);
			this.groupBox3.Controls.Add(this.labelSourceSmmry);
			this.groupBox3.Controls.Add(this.labelTargetPowerSmmry);
			this.groupBox3.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Bold | global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.groupBox3.Location = new global::System.Drawing.Point(12, 34);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new global::System.Drawing.Size(327, 171);
			this.groupBox3.TabIndex = 6;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Download Summary";
			this.labelSourceSmmry.AutoSize = true;
			this.labelSourceSmmry.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelSourceSmmry.Location = new global::System.Drawing.Point(6, 58);
			this.labelSourceSmmry.Name = "labelSourceSmmry";
			this.labelSourceSmmry.Size = new global::System.Drawing.Size(88, 13);
			this.labelSourceSmmry.TabIndex = 10;
			this.labelSourceSmmry.Text = "<DataSource>";
			this.labelTargetPowerSmmry.AutoSize = true;
			this.labelTargetPowerSmmry.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelTargetPowerSmmry.Location = new global::System.Drawing.Point(6, 75);
			this.labelTargetPowerSmmry.Name = "labelTargetPowerSmmry";
			this.labelTargetPowerSmmry.Size = new global::System.Drawing.Size(108, 15);
			this.labelTargetPowerSmmry.TabIndex = 7;
			this.labelTargetPowerSmmry.Text = "<Target Power>";
			this.label12.AutoSize = true;
			this.label12.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label12.Location = new global::System.Drawing.Point(81, 12);
			this.label12.Name = "label12";
			this.label12.Size = new global::System.Drawing.Size(168, 19);
			this.label12.TabIndex = 3;
			this.label12.Text = "Download to PICkit 2";
			this.labelVDDMin.AutoSize = true;
			this.labelVDDMin.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelVDDMin.ForeColor = global::System.Drawing.Color.OrangeRed;
			this.labelVDDMin.Location = new global::System.Drawing.Point(45, 47);
			this.labelVDDMin.Name = "labelVDDMin";
			this.labelVDDMin.Size = new global::System.Drawing.Size(132, 13);
			this.labelVDDMin.TabIndex = 12;
			this.labelVDDMin.Text = "VDD must be >= 0.0 Volts.";
			this.labelVDDMin.Visible = false;
			this.labelPNsmmry.AutoSize = true;
			this.labelPNsmmry.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelPNsmmry.Location = new global::System.Drawing.Point(6, 21);
			this.labelPNsmmry.Name = "labelPNsmmry";
			this.labelPNsmmry.Size = new global::System.Drawing.Size(100, 15);
			this.labelPNsmmry.TabIndex = 11;
			this.labelPNsmmry.Text = "<PartNumber>";
			this.label6.AutoSize = true;
			this.label6.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label6.Location = new global::System.Drawing.Point(6, 41);
			this.label6.Name = "label6";
			this.label6.Size = new global::System.Drawing.Size(76, 15);
			this.label6.TabIndex = 12;
			this.label6.Text = "Data source:";
			this.labelMemRegionsSmmry.AutoSize = true;
			this.labelMemRegionsSmmry.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelMemRegionsSmmry.Location = new global::System.Drawing.Point(6, 94);
			this.labelMemRegionsSmmry.Name = "labelMemRegionsSmmry";
			this.labelMemRegionsSmmry.Size = new global::System.Drawing.Size(134, 13);
			this.labelMemRegionsSmmry.TabIndex = 13;
			this.labelMemRegionsSmmry.Text = "<MemRegions CP-DP>";
			this.labelVPP1stSmmry.AutoSize = true;
			this.labelVPP1stSmmry.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelVPP1stSmmry.Location = new global::System.Drawing.Point(6, 107);
			this.labelVPP1stSmmry.Name = "labelVPP1stSmmry";
			this.labelVPP1stSmmry.Size = new global::System.Drawing.Size(62, 13);
			this.labelVPP1stSmmry.TabIndex = 14;
			this.labelVPP1stSmmry.Text = "<VPP1st>";
			this.labelVerifySmmry.AutoSize = true;
			this.labelVerifySmmry.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelVerifySmmry.Location = new global::System.Drawing.Point(6, 120);
			this.labelVerifySmmry.Name = "labelVerifySmmry";
			this.labelVerifySmmry.Size = new global::System.Drawing.Size(53, 13);
			this.labelVerifySmmry.TabIndex = 15;
			this.labelVerifySmmry.Text = "<Verify>";
			this.labelFastProgSmmry.AutoSize = true;
			this.labelFastProgSmmry.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelFastProgSmmry.Location = new global::System.Drawing.Point(6, 133);
			this.labelFastProgSmmry.Name = "labelFastProgSmmry";
			this.labelFastProgSmmry.Size = new global::System.Drawing.Size(117, 13);
			this.labelFastProgSmmry.TabIndex = 16;
			this.labelFastProgSmmry.Text = "<FastProgramming>";
			this.labelMCLRHoldSmmry.AutoSize = true;
			this.labelMCLRHoldSmmry.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelMCLRHoldSmmry.Location = new global::System.Drawing.Point(6, 146);
			this.labelMCLRHoldSmmry.Name = "labelMCLRHoldSmmry";
			this.labelMCLRHoldSmmry.Size = new global::System.Drawing.Size(81, 13);
			this.labelMCLRHoldSmmry.TabIndex = 17;
			this.labelMCLRHoldSmmry.Text = "<MCLRHold>";
			this.panelDownloading.Controls.Add(this.labelDOWNLOADING);
			this.panelDownloading.Location = new global::System.Drawing.Point(12, 12);
			this.panelDownloading.Name = "panelDownloading";
			this.panelDownloading.Size = new global::System.Drawing.Size(351, 331);
			this.panelDownloading.TabIndex = 6;
			this.panelDownloading.Visible = false;
			this.labelDOWNLOADING.AutoSize = true;
			this.labelDOWNLOADING.Font = new global::System.Drawing.Font("Arial", 15.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelDOWNLOADING.Location = new global::System.Drawing.Point(72, 153);
			this.labelDOWNLOADING.Name = "labelDOWNLOADING";
			this.labelDOWNLOADING.Size = new global::System.Drawing.Size(206, 24);
			this.labelDOWNLOADING.TabIndex = 2;
			this.labelDOWNLOADING.Text = "Downloading Now...";
			this.panelDownloadDone.Controls.Add(this.label20);
			this.panelDownloadDone.Controls.Add(this.label19);
			this.panelDownloadDone.Controls.Add(this.label18);
			this.panelDownloadDone.Controls.Add(this.pictureBoxTarget);
			this.panelDownloadDone.Controls.Add(this.label17);
			this.panelDownloadDone.Controls.Add(this.label13);
			this.panelDownloadDone.Location = new global::System.Drawing.Point(12, 12);
			this.panelDownloadDone.Name = "panelDownloadDone";
			this.panelDownloadDone.Size = new global::System.Drawing.Size(351, 331);
			this.panelDownloadDone.TabIndex = 10;
			this.panelDownloadDone.Visible = false;
			this.label13.AutoSize = true;
			this.label13.Font = new global::System.Drawing.Font("Arial", 14.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label13.Location = new global::System.Drawing.Point(75, 15);
			this.label13.Name = "label13";
			this.label13.Size = new global::System.Drawing.Size(201, 22);
			this.label13.TabIndex = 2;
			this.label13.Text = "Download Complete!";
			this.label17.AutoSize = true;
			this.label17.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label17.Location = new global::System.Drawing.Point(17, 62);
			this.label17.Name = "label17";
			this.label17.Size = new global::System.Drawing.Size(323, 48);
			this.label17.TabIndex = 3;
			this.label17.Text = "The PICkit 2 unit will indicate it's in Programmer-To-Go\r\nmode and ready to program by blinking the \"Target\" \r\nLED twice in succession:";
			this.timerBlink.Interval = 250;
			this.timerBlink.Tick += new global::System.EventHandler(this.timerBlink_Tick);
			this.pictureBoxTarget.BackColor = global::System.Drawing.SystemColors.ControlText;
			this.pictureBoxTarget.Location = new global::System.Drawing.Point(73, 125);
			this.pictureBoxTarget.Name = "pictureBoxTarget";
			this.pictureBoxTarget.Size = new global::System.Drawing.Size(15, 15);
			this.pictureBoxTarget.TabIndex = 4;
			this.pictureBoxTarget.TabStop = false;
			this.label18.AutoSize = true;
			this.label18.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label18.Location = new global::System.Drawing.Point(101, 125);
			this.label18.Name = "label18";
			this.label18.Size = new global::System.Drawing.Size(48, 15);
			this.label18.TabIndex = 6;
			this.label18.Text = "Target";
			this.label19.AutoSize = true;
			this.label19.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Bold | global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label19.Location = new global::System.Drawing.Point(16, 159);
			this.label19.Name = "label19";
			this.label19.Size = new global::System.Drawing.Size(230, 16);
			this.label19.TabIndex = 7;
			this.label19.Text = "Remove the PICkit 2 from USB now.";
			this.label20.AutoSize = true;
			this.label20.Font = new global::System.Drawing.Font("Arial", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label20.Location = new global::System.Drawing.Point(16, 175);
			this.label20.Name = "label20";
			this.label20.Size = new global::System.Drawing.Size(332, 144);
			this.label20.TabIndex = 8;
			this.label20.Text = componentResourceManager.GetString("label20.Text");
			this.panelErrors.Controls.Add(this.label29);
			this.panelErrors.Controls.Add(this.label28);
			this.panelErrors.Controls.Add(this.label27);
			this.panelErrors.Controls.Add(this.label26);
			this.panelErrors.Controls.Add(this.label25);
			this.panelErrors.Controls.Add(this.radioButton4Blinks);
			this.panelErrors.Controls.Add(this.radioButton3Blinks);
			this.panelErrors.Controls.Add(this.radioButton2Blinks);
			this.panelErrors.Controls.Add(this.radioButtonVErr);
			this.panelErrors.Controls.Add(this.label24);
			this.panelErrors.Controls.Add(this.label23);
			this.panelErrors.Controls.Add(this.pictureBoxBusy);
			this.panelErrors.Controls.Add(this.label22);
			this.panelErrors.Controls.Add(this.label21);
			this.panelErrors.Location = new global::System.Drawing.Point(12, 12);
			this.panelErrors.Name = "panelErrors";
			this.panelErrors.Size = new global::System.Drawing.Size(351, 331);
			this.panelErrors.TabIndex = 11;
			this.panelErrors.Visible = false;
			this.label21.AutoSize = true;
			this.label21.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label21.Location = new global::System.Drawing.Point(69, 12);
			this.label21.Name = "label21";
			this.label21.Size = new global::System.Drawing.Size(226, 19);
			this.label21.TabIndex = 4;
			this.label21.Text = "Programming && Error Codes";
			this.label22.AutoSize = true;
			this.label22.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label22.Location = new global::System.Drawing.Point(25, 33);
			this.label22.Name = "label22";
			this.label22.Size = new global::System.Drawing.Size(287, 150);
			this.label22.TabIndex = 8;
			this.label22.Text = componentResourceManager.GetString("label22.Text");
			this.pictureBoxBusy.BackColor = global::System.Drawing.SystemColors.ControlText;
			this.pictureBoxBusy.Location = new global::System.Drawing.Point(107, 194);
			this.pictureBoxBusy.Name = "pictureBoxBusy";
			this.pictureBoxBusy.Size = new global::System.Drawing.Size(15, 15);
			this.pictureBoxBusy.TabIndex = 9;
			this.pictureBoxBusy.TabStop = false;
			this.label23.AutoSize = true;
			this.label23.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label23.Location = new global::System.Drawing.Point(138, 194);
			this.label23.Name = "label23";
			this.label23.Size = new global::System.Drawing.Size(37, 15);
			this.label23.TabIndex = 10;
			this.label23.Text = "Busy";
			this.label24.AutoSize = true;
			this.label24.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Underline, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label24.Location = new global::System.Drawing.Point(25, 212);
			this.label24.Name = "label24";
			this.label24.Size = new global::System.Drawing.Size(188, 15);
			this.label24.TabIndex = 11;
			this.label24.Text = "Error Codes (Select for example):";
			this.radioButtonVErr.AutoSize = true;
			this.radioButtonVErr.Checked = true;
			this.radioButtonVErr.Location = new global::System.Drawing.Point(28, 230);
			this.radioButtonVErr.Name = "radioButtonVErr";
			this.radioButtonVErr.Size = new global::System.Drawing.Size(85, 17);
			this.radioButtonVErr.TabIndex = 12;
			this.radioButtonVErr.TabStop = true;
			this.radioButtonVErr.Text = "Fast Blinking";
			this.radioButtonVErr.UseVisualStyleBackColor = true;
			this.radioButtonVErr.Click += new global::System.EventHandler(this.radioButtonVErr_Click);
			this.radioButton2Blinks.AutoSize = true;
			this.radioButton2Blinks.Location = new global::System.Drawing.Point(28, 264);
			this.radioButton2Blinks.Name = "radioButton2Blinks";
			this.radioButton2Blinks.Size = new global::System.Drawing.Size(62, 17);
			this.radioButton2Blinks.TabIndex = 13;
			this.radioButton2Blinks.Text = "2 Blinks";
			this.radioButton2Blinks.UseVisualStyleBackColor = true;
			this.radioButton2Blinks.Click += new global::System.EventHandler(this.radioButtonVErr_Click);
			this.radioButton3Blinks.AutoSize = true;
			this.radioButton3Blinks.Location = new global::System.Drawing.Point(186, 230);
			this.radioButton3Blinks.Name = "radioButton3Blinks";
			this.radioButton3Blinks.Size = new global::System.Drawing.Size(62, 17);
			this.radioButton3Blinks.TabIndex = 14;
			this.radioButton3Blinks.Text = "3 Blinks";
			this.radioButton3Blinks.UseVisualStyleBackColor = true;
			this.radioButton3Blinks.Click += new global::System.EventHandler(this.radioButtonVErr_Click);
			this.radioButton4Blinks.AutoSize = true;
			this.radioButton4Blinks.Location = new global::System.Drawing.Point(186, 264);
			this.radioButton4Blinks.Name = "radioButton4Blinks";
			this.radioButton4Blinks.Size = new global::System.Drawing.Size(62, 17);
			this.radioButton4Blinks.TabIndex = 15;
			this.radioButton4Blinks.Text = "4 Blinks";
			this.radioButton4Blinks.UseVisualStyleBackColor = true;
			this.radioButton4Blinks.Click += new global::System.EventHandler(this.radioButtonVErr_Click);
			this.label25.AutoSize = true;
			this.label25.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label25.Location = new global::System.Drawing.Point(48, 246);
			this.label25.Name = "label25";
			this.label25.Size = new global::System.Drawing.Size(88, 15);
			this.label25.TabIndex = 16;
			this.label25.Text = "VDD/VPP Error";
			this.label26.AutoSize = true;
			this.label26.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label26.Location = new global::System.Drawing.Point(47, 280);
			this.label26.Name = "label26";
			this.label26.Size = new global::System.Drawing.Size(89, 15);
			this.label26.TabIndex = 17;
			this.label26.Text = "Device ID Error";
			this.label27.AutoSize = true;
			this.label27.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label27.Location = new global::System.Drawing.Point(206, 246);
			this.label27.Name = "label27";
			this.label27.Size = new global::System.Drawing.Size(73, 15);
			this.label27.TabIndex = 18;
			this.label27.Text = "Verify Failed";
			this.label28.AutoSize = true;
			this.label28.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label28.Location = new global::System.Drawing.Point(206, 280);
			this.label28.Name = "label28";
			this.label28.Size = new global::System.Drawing.Size(78, 15);
			this.label28.TabIndex = 19;
			this.label28.Text = "Internal Error";
			this.label29.AutoSize = true;
			this.label29.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label29.Location = new global::System.Drawing.Point(28, 312);
			this.label29.Name = "label29";
			this.label29.Size = new global::System.Drawing.Size(294, 13);
			this.label29.TabIndex = 19;
			this.label29.Text = "Click EXIT to close Wizard.  Click HELP for more information.\r\n";
			this.label256K.AutoSize = true;
			this.label256K.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label256K.Location = new global::System.Drawing.Point(156, 306);
			this.label256K.Name = "label256K";
			this.label256K.Size = new global::System.Drawing.Size(196, 13);
			this.label256K.TabIndex = 6;
			this.label256K.Text = "256K PICkit 2 upgrade support enabled.\r\n";
			this.label256K.Visible = false;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(96f, 96f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Dpi;
			base.ClientSize = new global::System.Drawing.Size(374, 380);
			base.Controls.Add(this.panelErrors);
			base.Controls.Add(this.panelDownloadDone);
			base.Controls.Add(this.panelDownloading);
			base.Controls.Add(this.panelDownload);
			base.Controls.Add(this.panelSettings);
			base.Controls.Add(this.buttonHelp);
			base.Controls.Add(this.buttonBack);
			base.Controls.Add(this.panelIntro);
			base.Controls.Add(this.buttonNext);
			base.Controls.Add(this.buttonCancel);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DialogPK2Go";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Programmer-To-Go Wizard";
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.DialogPK2Go_FormClosing);
			this.panelIntro.ResumeLayout(false);
			this.panelIntro.PerformLayout();
			this.panelSettings.ResumeLayout(false);
			this.panelSettings.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.panelDownload.ResumeLayout(false);
			this.panelDownload.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.panelDownloading.ResumeLayout(false);
			this.panelDownloading.PerformLayout();
			this.panelDownloadDone.ResumeLayout(false);
			this.panelDownloadDone.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBoxTarget).EndInit();
			this.panelErrors.ResumeLayout(false);
			this.panelErrors.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBoxBusy).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x04000342 RID: 834
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000343 RID: 835
		private global::System.Windows.Forms.Panel panelIntro;

		// Token: 0x04000344 RID: 836
		private global::System.Windows.Forms.Button buttonBack;

		// Token: 0x04000345 RID: 837
		private global::System.Windows.Forms.Button buttonNext;

		// Token: 0x04000346 RID: 838
		private global::System.Windows.Forms.Button buttonCancel;

		// Token: 0x04000347 RID: 839
		private global::System.Windows.Forms.Button buttonHelp;

		// Token: 0x04000348 RID: 840
		private global::System.Windows.Forms.Panel panelSettings;

		// Token: 0x04000349 RID: 841
		private global::System.Windows.Forms.Label label16;

		// Token: 0x0400034A RID: 842
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400034B RID: 843
		private global::System.Windows.Forms.Label label15;

		// Token: 0x0400034C RID: 844
		private global::System.Windows.Forms.GroupBox groupBox1;

		// Token: 0x0400034D RID: 845
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400034E RID: 846
		private global::System.Windows.Forms.RadioButton radioButtonPK2Power;

		// Token: 0x0400034F RID: 847
		private global::System.Windows.Forms.RadioButton radioButtonSelfPower;

		// Token: 0x04000350 RID: 848
		private global::System.Windows.Forms.GroupBox groupBox2;

		// Token: 0x04000351 RID: 849
		private global::System.Windows.Forms.Label label5;

		// Token: 0x04000352 RID: 850
		private global::System.Windows.Forms.Label labelPartNumber;

		// Token: 0x04000353 RID: 851
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000354 RID: 852
		private global::System.Windows.Forms.Label labelDataSource;

		// Token: 0x04000355 RID: 853
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000356 RID: 854
		private global::System.Windows.Forms.Panel panelDownload;

		// Token: 0x04000357 RID: 855
		private global::System.Windows.Forms.GroupBox groupBox3;

		// Token: 0x04000358 RID: 856
		private global::System.Windows.Forms.Label labelTargetPowerSmmry;

		// Token: 0x04000359 RID: 857
		private global::System.Windows.Forms.Label label12;

		// Token: 0x0400035A RID: 858
		private global::System.Windows.Forms.Label label7;

		// Token: 0x0400035B RID: 859
		private global::System.Windows.Forms.Label labelSourceSmmry;

		// Token: 0x0400035C RID: 860
		private global::System.Windows.Forms.Label label8;

		// Token: 0x0400035D RID: 861
		private global::System.Windows.Forms.Label labelOSCCAL_BandGap;

		// Token: 0x0400035E RID: 862
		private global::System.Windows.Forms.Label label10;

		// Token: 0x0400035F RID: 863
		private global::System.Windows.Forms.Label labelMemRegions;

		// Token: 0x04000360 RID: 864
		private global::System.Windows.Forms.Label labelCodeProtect;

		// Token: 0x04000361 RID: 865
		private global::System.Windows.Forms.Label label14;

		// Token: 0x04000362 RID: 866
		private global::System.Windows.Forms.Label labelVerify;

		// Token: 0x04000363 RID: 867
		private global::System.Windows.Forms.Label label11;

		// Token: 0x04000364 RID: 868
		private global::System.Windows.Forms.Label labelDataProtect;

		// Token: 0x04000365 RID: 869
		private global::System.Windows.Forms.Label label9;

		// Token: 0x04000366 RID: 870
		private global::System.Windows.Forms.Label labelRowErase;

		// Token: 0x04000367 RID: 871
		private global::System.Windows.Forms.CheckBox checkBoxRowErase;

		// Token: 0x04000368 RID: 872
		private global::System.Windows.Forms.Label labelVDDMin;

		// Token: 0x04000369 RID: 873
		private global::System.Windows.Forms.Label labelPNsmmry;

		// Token: 0x0400036A RID: 874
		private global::System.Windows.Forms.Label labelMemRegionsSmmry;

		// Token: 0x0400036B RID: 875
		private global::System.Windows.Forms.Label label6;

		// Token: 0x0400036C RID: 876
		private global::System.Windows.Forms.Label labelMCLRHoldSmmry;

		// Token: 0x0400036D RID: 877
		private global::System.Windows.Forms.Label labelFastProgSmmry;

		// Token: 0x0400036E RID: 878
		private global::System.Windows.Forms.Label labelVerifySmmry;

		// Token: 0x0400036F RID: 879
		private global::System.Windows.Forms.Label labelVPP1stSmmry;

		// Token: 0x04000370 RID: 880
		private global::System.Windows.Forms.Panel panelDownloading;

		// Token: 0x04000371 RID: 881
		private global::System.Windows.Forms.Label labelDOWNLOADING;

		// Token: 0x04000372 RID: 882
		private global::System.Windows.Forms.Panel panelDownloadDone;

		// Token: 0x04000373 RID: 883
		private global::System.Windows.Forms.Label label13;

		// Token: 0x04000374 RID: 884
		private global::System.Windows.Forms.Label label17;

		// Token: 0x04000375 RID: 885
		private global::System.Windows.Forms.Timer timerBlink;

		// Token: 0x04000376 RID: 886
		private global::System.Windows.Forms.Label label20;

		// Token: 0x04000377 RID: 887
		private global::System.Windows.Forms.Label label19;

		// Token: 0x04000378 RID: 888
		private global::System.Windows.Forms.Label label18;

		// Token: 0x04000379 RID: 889
		private global::System.Windows.Forms.PictureBox pictureBoxTarget;

		// Token: 0x0400037A RID: 890
		private global::System.Windows.Forms.Panel panelErrors;

		// Token: 0x0400037B RID: 891
		private global::System.Windows.Forms.Label label21;

		// Token: 0x0400037C RID: 892
		private global::System.Windows.Forms.Label label22;

		// Token: 0x0400037D RID: 893
		private global::System.Windows.Forms.Label label23;

		// Token: 0x0400037E RID: 894
		private global::System.Windows.Forms.PictureBox pictureBoxBusy;

		// Token: 0x0400037F RID: 895
		private global::System.Windows.Forms.Label label26;

		// Token: 0x04000380 RID: 896
		private global::System.Windows.Forms.Label label25;

		// Token: 0x04000381 RID: 897
		private global::System.Windows.Forms.RadioButton radioButton4Blinks;

		// Token: 0x04000382 RID: 898
		private global::System.Windows.Forms.RadioButton radioButton3Blinks;

		// Token: 0x04000383 RID: 899
		private global::System.Windows.Forms.RadioButton radioButton2Blinks;

		// Token: 0x04000384 RID: 900
		private global::System.Windows.Forms.RadioButton radioButtonVErr;

		// Token: 0x04000385 RID: 901
		private global::System.Windows.Forms.Label label24;

		// Token: 0x04000386 RID: 902
		private global::System.Windows.Forms.Label label27;

		// Token: 0x04000387 RID: 903
		private global::System.Windows.Forms.Label label28;

		// Token: 0x04000388 RID: 904
		private global::System.Windows.Forms.Label label29;

		// Token: 0x04000389 RID: 905
		private global::System.Windows.Forms.Label label256K;
	}
}
