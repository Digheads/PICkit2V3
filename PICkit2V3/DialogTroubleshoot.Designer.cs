namespace PICkit2V3
{
	// Token: 0x02000009 RID: 9
	public partial class DialogTroubleshoot : global::System.Windows.Forms.Form
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00008FA5 File Offset: 0x00007FA5
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00008FC4 File Offset: 0x00007FC4
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::PICkit2V3.DialogTroubleshoot));
			this.panelIntro = new global::System.Windows.Forms.Panel();
			this.labelIntroText1 = new global::System.Windows.Forms.Label();
			this.labelIntroTitle = new global::System.Windows.Forms.Label();
			this.panelStep1VDDExt = new global::System.Windows.Forms.Panel();
			this.pictureBox2 = new global::System.Windows.Forms.PictureBox();
			this.labelVoltageOnVDD2 = new global::System.Windows.Forms.Label();
			this.labelVoltageOnVDD = new global::System.Windows.Forms.Label();
			this.buttonStep1Recheck = new global::System.Windows.Forms.Button();
			this.labelStep1ExtTitle = new global::System.Windows.Forms.Label();
			this.buttonCancel = new global::System.Windows.Forms.Button();
			this.buttonNext = new global::System.Windows.Forms.Button();
			this.buttonBack = new global::System.Windows.Forms.Button();
			this.labelTestVDD = new global::System.Windows.Forms.Label();
			this.numericUpDown1 = new global::System.Windows.Forms.NumericUpDown();
			this.panelStep1VDDTest = new global::System.Windows.Forms.Panel();
			this.label11 = new global::System.Windows.Forms.Label();
			this.label12 = new global::System.Windows.Forms.Label();
			this.label10 = new global::System.Windows.Forms.Label();
			this.labelGood = new global::System.Windows.Forms.Label();
			this.label1 = new global::System.Windows.Forms.Label();
			this.labelVDDLow = new global::System.Windows.Forms.Label();
			this.labelVDDShort = new global::System.Windows.Forms.Label();
			this.labelReadVDD = new global::System.Windows.Forms.Label();
			this.buttonVDDOn = new global::System.Windows.Forms.Button();
			this.pictureBox1 = new global::System.Windows.Forms.PictureBox();
			this.labelStep1Title = new global::System.Windows.Forms.Label();
			this.panelCautionVDD = new global::System.Windows.Forms.Panel();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.panelStep2VPP = new global::System.Windows.Forms.Panel();
			this.buttonMCLROff = new global::System.Windows.Forms.Button();
			this.labelVPPMCLROff = new global::System.Windows.Forms.Label();
			this.labelVPPPass = new global::System.Windows.Forms.Label();
			this.label9 = new global::System.Windows.Forms.Label();
			this.label7 = new global::System.Windows.Forms.Label();
			this.labelVPPMCLR = new global::System.Windows.Forms.Label();
			this.labelVPPShort = new global::System.Windows.Forms.Label();
			this.labelVPPLow = new global::System.Windows.Forms.Label();
			this.labelVPPVDDShort = new global::System.Windows.Forms.Label();
			this.labelVPPResults = new global::System.Windows.Forms.Label();
			this.buttonMCLR = new global::System.Windows.Forms.Button();
			this.labelReadVPP = new global::System.Windows.Forms.Label();
			this.buttonTestVPP = new global::System.Windows.Forms.Button();
			this.label5 = new global::System.Windows.Forms.Label();
			this.labelStep2FamilyVPP = new global::System.Windows.Forms.Label();
			this.pictureBox3 = new global::System.Windows.Forms.PictureBox();
			this.label6 = new global::System.Windows.Forms.Label();
			this.panelPGCPGD = new global::System.Windows.Forms.Panel();
			this.labelPGxVDDShort = new global::System.Windows.Forms.Label();
			this.labelPGxOScope = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			this.groupBoxPGD = new global::System.Windows.Forms.GroupBox();
			this.radioButtonPGDToggle = new global::System.Windows.Forms.RadioButton();
			this.radioButtonPGDLow = new global::System.Windows.Forms.RadioButton();
			this.radioButtonPGDHigh = new global::System.Windows.Forms.RadioButton();
			this.pictureBox5 = new global::System.Windows.Forms.PictureBox();
			this.groupBoxPGC = new global::System.Windows.Forms.GroupBox();
			this.radioButtonPGCToggle = new global::System.Windows.Forms.RadioButton();
			this.radioButtonPGCLow = new global::System.Windows.Forms.RadioButton();
			this.radioButtonPGCHigh = new global::System.Windows.Forms.RadioButton();
			this.label8 = new global::System.Windows.Forms.Label();
			this.pictureBox4 = new global::System.Windows.Forms.PictureBox();
			this.timerPGxToggle = new global::System.Windows.Forms.Timer(this.components);
			this.panelIntro.SuspendLayout();
			this.panelStep1VDDExt.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.numericUpDown1).BeginInit();
			this.panelStep1VDDTest.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
			this.panelCautionVDD.SuspendLayout();
			this.panelStep2VPP.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox3).BeginInit();
			this.panelPGCPGD.SuspendLayout();
			this.groupBoxPGD.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox5).BeginInit();
			this.groupBoxPGC.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox4).BeginInit();
			base.SuspendLayout();
			this.panelIntro.Controls.Add(this.labelIntroText1);
			this.panelIntro.Controls.Add(this.labelIntroTitle);
			this.panelIntro.Location = new global::System.Drawing.Point(16, 15);
			this.panelIntro.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.panelIntro.Name = "panelIntro";
			this.panelIntro.Size = new global::System.Drawing.Size(413, 295);
			this.panelIntro.TabIndex = 0;
			this.labelIntroText1.AutoSize = true;
			this.labelIntroText1.Location = new global::System.Drawing.Point(16, 42);
			this.labelIntroText1.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelIntroText1.Name = "labelIntroText1";
			this.labelIntroText1.Size = new global::System.Drawing.Size(384, 238);
			this.labelIntroText1.TabIndex = 1;
			this.labelIntroText1.Text = componentResourceManager.GetString("labelIntroText1.Text");
			this.labelIntroTitle.AutoSize = true;
			this.labelIntroTitle.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelIntroTitle.Location = new global::System.Drawing.Point(68, 0);
			this.labelIntroTitle.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelIntroTitle.Name = "labelIntroTitle";
			this.labelIntroTitle.Size = new global::System.Drawing.Size(244, 24);
			this.labelIntroTitle.TabIndex = 0;
			this.labelIntroTitle.Text = "PICkit 2 Troubleshooting";
			this.panelStep1VDDExt.Controls.Add(this.pictureBox2);
			this.panelStep1VDDExt.Controls.Add(this.labelVoltageOnVDD2);
			this.panelStep1VDDExt.Controls.Add(this.labelVoltageOnVDD);
			this.panelStep1VDDExt.Controls.Add(this.buttonStep1Recheck);
			this.panelStep1VDDExt.Controls.Add(this.labelStep1ExtTitle);
			this.panelStep1VDDExt.Location = new global::System.Drawing.Point(16, 15);
			this.panelStep1VDDExt.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.panelStep1VDDExt.Name = "panelStep1VDDExt";
			this.panelStep1VDDExt.Size = new global::System.Drawing.Size(413, 295);
			this.panelStep1VDDExt.TabIndex = 1;
			this.panelStep1VDDExt.Visible = false;
			this.pictureBox2.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("pictureBox2.Image");
			this.pictureBox2.Location = new global::System.Drawing.Point(0, 0);
			this.pictureBox2.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new global::System.Drawing.Size(78, 116);
			this.pictureBox2.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox2.TabIndex = 7;
			this.pictureBox2.TabStop = false;
			this.labelVoltageOnVDD2.AutoSize = true;
			this.labelVoltageOnVDD2.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelVoltageOnVDD2.ForeColor = global::System.Drawing.Color.Red;
			this.labelVoltageOnVDD2.Location = new global::System.Drawing.Point(4, 146);
			this.labelVoltageOnVDD2.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelVoltageOnVDD2.Name = "labelVoltageOnVDD2";
			this.labelVoltageOnVDD2.Size = new global::System.Drawing.Size(349, 80);
			this.labelVoltageOnVDD2.TabIndex = 3;
			this.labelVoltageOnVDD2.Text = "Click \"Next >\" to skip VDD testing.\r\n\r\nTo test VDD, remove the external voltage and\r\nclick \"Recheck\".";
			this.labelVoltageOnVDD.AutoSize = true;
			this.labelVoltageOnVDD.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelVoltageOnVDD.ForeColor = global::System.Drawing.Color.Red;
			this.labelVoltageOnVDD.Location = new global::System.Drawing.Point(124, 60);
			this.labelVoltageOnVDD.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelVoltageOnVDD.Name = "labelVoltageOnVDD";
			this.labelVoltageOnVDD.Size = new global::System.Drawing.Size(255, 40);
			this.labelVoltageOnVDD.TabIndex = 2;
			this.labelVoltageOnVDD.Text = "An external voltage was detected\r\non the VDD pin at ";
			this.buttonStep1Recheck.Location = new global::System.Drawing.Point(155, 254);
			this.buttonStep1Recheck.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.buttonStep1Recheck.Name = "buttonStep1Recheck";
			this.buttonStep1Recheck.Size = new global::System.Drawing.Size(100, 28);
			this.buttonStep1Recheck.TabIndex = 1;
			this.buttonStep1Recheck.Text = "Recheck";
			this.buttonStep1Recheck.UseVisualStyleBackColor = true;
			this.buttonStep1Recheck.Click += new global::System.EventHandler(this.buttonStep1Recheck_Click);
			this.labelStep1ExtTitle.AutoEllipsis = true;
			this.labelStep1ExtTitle.AutoSize = true;
			this.labelStep1ExtTitle.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelStep1ExtTitle.Location = new global::System.Drawing.Point(149, 0);
			this.labelStep1ExtTitle.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelStep1ExtTitle.Name = "labelStep1ExtTitle";
			this.labelStep1ExtTitle.Size = new global::System.Drawing.Size(185, 24);
			this.labelStep1ExtTitle.TabIndex = 0;
			this.labelStep1ExtTitle.Text = "Step 1: Verify VDD";
			this.buttonCancel.Location = new global::System.Drawing.Point(329, 318);
			this.buttonCancel.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new global::System.Drawing.Size(100, 28);
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new global::System.EventHandler(this.buttonCancel_Click);
			this.buttonNext.Location = new global::System.Drawing.Point(221, 318);
			this.buttonNext.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.buttonNext.Name = "buttonNext";
			this.buttonNext.Size = new global::System.Drawing.Size(100, 28);
			this.buttonNext.TabIndex = 3;
			this.buttonNext.Text = "Next >";
			this.buttonNext.UseVisualStyleBackColor = true;
			this.buttonNext.Click += new global::System.EventHandler(this.buttonNext_Click);
			this.buttonBack.Enabled = false;
			this.buttonBack.Location = new global::System.Drawing.Point(113, 318);
			this.buttonBack.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.buttonBack.Name = "buttonBack";
			this.buttonBack.Size = new global::System.Drawing.Size(100, 28);
			this.buttonBack.TabIndex = 4;
			this.buttonBack.Text = "< Back";
			this.buttonBack.UseVisualStyleBackColor = true;
			this.buttonBack.Click += new global::System.EventHandler(this.buttonBack_Click);
			this.labelTestVDD.AutoSize = true;
			this.labelTestVDD.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelTestVDD.Location = new global::System.Drawing.Point(119, 23);
			this.labelTestVDD.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelTestVDD.Name = "labelTestVDD";
			this.labelTestVDD.Size = new global::System.Drawing.Size(235, 36);
			this.labelTestVDD.TabIndex = 4;
			this.labelTestVDD.Text = "1) Adjust VDD level for your circuit.\r\n(Limits set by active family)";
			this.numericUpDown1.DecimalPlaces = 1;
			this.numericUpDown1.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.numericUpDown1.Increment = new decimal(new int[]
			{
				1,
				0,
				0,
				65536
			});
			this.numericUpDown1.Location = new global::System.Drawing.Point(23, 150);
			this.numericUpDown1.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new global::System.Drawing.Size(80, 26);
			this.numericUpDown1.TabIndex = 5;
			this.numericUpDown1.Value = new decimal(new int[]
			{
				45,
				0,
				0,
				65536
			});
			this.panelStep1VDDTest.Controls.Add(this.label11);
			this.panelStep1VDDTest.Controls.Add(this.label12);
			this.panelStep1VDDTest.Controls.Add(this.label10);
			this.panelStep1VDDTest.Controls.Add(this.labelGood);
			this.panelStep1VDDTest.Controls.Add(this.label1);
			this.panelStep1VDDTest.Controls.Add(this.labelVDDLow);
			this.panelStep1VDDTest.Controls.Add(this.labelVDDShort);
			this.panelStep1VDDTest.Controls.Add(this.labelReadVDD);
			this.panelStep1VDDTest.Controls.Add(this.buttonVDDOn);
			this.panelStep1VDDTest.Controls.Add(this.pictureBox1);
			this.panelStep1VDDTest.Controls.Add(this.numericUpDown1);
			this.panelStep1VDDTest.Controls.Add(this.labelStep1Title);
			this.panelStep1VDDTest.Controls.Add(this.labelTestVDD);
			this.panelStep1VDDTest.Location = new global::System.Drawing.Point(16, 15);
			this.panelStep1VDDTest.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.panelStep1VDDTest.Name = "panelStep1VDDTest";
			this.panelStep1VDDTest.Size = new global::System.Drawing.Size(413, 295);
			this.panelStep1VDDTest.TabIndex = 5;
			this.panelStep1VDDTest.Visible = false;
			this.label11.AutoSize = true;
			this.label11.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label11.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.label11.Location = new global::System.Drawing.Point(119, 133);
			this.label11.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label11.Name = "label11";
			this.label11.Size = new global::System.Drawing.Size(214, 36);
			this.label11.TabIndex = 12;
			this.label11.Text = "The actual voltage is dependent\r\non the host USB Voltage.";
			this.label12.AutoSize = true;
			this.label12.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label12.Location = new global::System.Drawing.Point(119, 69);
			this.label12.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label12.Name = "label12";
			this.label12.Size = new global::System.Drawing.Size(207, 18);
			this.label12.TabIndex = 12;
			this.label12.Text = "2) Click \"Test\" to turn on VDD.";
			this.label10.AutoSize = true;
			this.label10.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label10.ForeColor = global::System.Drawing.Color.DarkRed;
			this.label10.Location = new global::System.Drawing.Point(119, 96);
			this.label10.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label10.Name = "label10";
			this.label10.Size = new global::System.Drawing.Size(228, 36);
			this.label10.TabIndex = 11;
			this.label10.Text = "3) It is important to verify results\r\nusing a volt meter at all VDD pins.";
			this.labelGood.AutoSize = true;
			this.labelGood.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelGood.ForeColor = global::System.Drawing.Color.Blue;
			this.labelGood.Location = new global::System.Drawing.Point(119, 191);
			this.labelGood.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelGood.Name = "labelGood";
			this.labelGood.Size = new global::System.Drawing.Size(253, 90);
			this.labelGood.TabIndex = 10;
			this.labelGood.Text = "Test Passed:\r\nPICkit 2 detected an expected voltage\r\non the VDD pin.  (NOTE: slow rise\r\ntimes can still cause VDD Errors.)\r\nClick \"Next >\" to test VPP.";
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(32, 229);
			this.label1.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(59, 17);
			this.label1.TabIndex = 9;
			this.label1.Text = "Results:";
			this.labelVDDLow.AutoSize = true;
			this.labelVDDLow.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelVDDLow.ForeColor = global::System.Drawing.Color.Red;
			this.labelVDDLow.Location = new global::System.Drawing.Point(119, 191);
			this.labelVDDLow.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelVDDLow.Name = "labelVDDLow";
			this.labelVDDLow.Size = new global::System.Drawing.Size(256, 90);
			this.labelVDDLow.TabIndex = 6;
			this.labelVDDLow.Text = "Test Failed: The VDD result is low.\r\nThe target circuit may be pulling too\r\nmuch current from VDD, or there may\r\nbe too much capacitance on VDD.\r\nTry using an external supply.";
			this.labelVDDShort.AutoSize = true;
			this.labelVDDShort.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelVDDShort.ForeColor = global::System.Drawing.Color.Red;
			this.labelVDDShort.Location = new global::System.Drawing.Point(119, 191);
			this.labelVDDShort.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelVDDShort.Name = "labelVDDShort";
			this.labelVDDShort.Size = new global::System.Drawing.Size(248, 90);
			this.labelVDDShort.TabIndex = 6;
			this.labelVDDShort.Text = "Test Failed:\r\nA short was detected, and VDD was \r\nshut off.\r\nIf no target is connected, the PICkit 2\r\nmay be damaged.";
			this.labelVDDShort.Visible = false;
			this.labelReadVDD.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.labelReadVDD.Location = new global::System.Drawing.Point(23, 254);
			this.labelReadVDD.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelReadVDD.Name = "labelReadVDD";
			this.labelReadVDD.Size = new global::System.Drawing.Size(80, 28);
			this.labelReadVDD.TabIndex = 8;
			this.buttonVDDOn.Location = new global::System.Drawing.Point(23, 185);
			this.buttonVDDOn.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.buttonVDDOn.Name = "buttonVDDOn";
			this.buttonVDDOn.Size = new global::System.Drawing.Size(80, 28);
			this.buttonVDDOn.TabIndex = 7;
			this.buttonVDDOn.Text = "Test";
			this.buttonVDDOn.UseVisualStyleBackColor = true;
			this.buttonVDDOn.Click += new global::System.EventHandler(this.buttonVDDOn_Click);
			this.pictureBox1.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("pictureBox1.Image");
			this.pictureBox1.Location = new global::System.Drawing.Point(0, 0);
			this.pictureBox1.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new global::System.Drawing.Size(78, 116);
			this.pictureBox1.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 4;
			this.pictureBox1.TabStop = false;
			this.labelStep1Title.AutoEllipsis = true;
			this.labelStep1Title.AutoSize = true;
			this.labelStep1Title.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelStep1Title.Location = new global::System.Drawing.Point(151, 0);
			this.labelStep1Title.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelStep1Title.Name = "labelStep1Title";
			this.labelStep1Title.Size = new global::System.Drawing.Size(185, 24);
			this.labelStep1Title.TabIndex = 6;
			this.labelStep1Title.Text = "Step 1: Verify VDD";
			this.panelCautionVDD.Controls.Add(this.label2);
			this.panelCautionVDD.Controls.Add(this.label3);
			this.panelCautionVDD.Location = new global::System.Drawing.Point(16, 15);
			this.panelCautionVDD.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.panelCautionVDD.Name = "panelCautionVDD";
			this.panelCautionVDD.Size = new global::System.Drawing.Size(413, 295);
			this.panelCautionVDD.TabIndex = 2;
			this.panelCautionVDD.Visible = false;
			this.label2.AutoSize = true;
			this.label2.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label2.Location = new global::System.Drawing.Point(36, 91);
			this.label2.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(314, 120);
			this.label2.TabIndex = 1;
			this.label2.Text = "VDD will be turned on in all the following\r\ntests at the voltage set in Step 1, unless\r\nan external supply is detected.\r\n\r\nEnsure that VDD is set to an appropriate\r\nvoltage in Step 1.\r\n";
			this.label3.AutoSize = true;
			this.label3.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label3.ForeColor = global::System.Drawing.Color.Red;
			this.label3.Location = new global::System.Drawing.Point(125, 33);
			this.label3.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(139, 24);
			this.label3.TabIndex = 0;
			this.label3.Text = "-- CAUTION --";
			this.panelStep2VPP.Controls.Add(this.buttonMCLROff);
			this.panelStep2VPP.Controls.Add(this.labelVPPMCLROff);
			this.panelStep2VPP.Controls.Add(this.labelVPPPass);
			this.panelStep2VPP.Controls.Add(this.label9);
			this.panelStep2VPP.Controls.Add(this.label7);
			this.panelStep2VPP.Controls.Add(this.labelVPPMCLR);
			this.panelStep2VPP.Controls.Add(this.labelVPPShort);
			this.panelStep2VPP.Controls.Add(this.labelVPPLow);
			this.panelStep2VPP.Controls.Add(this.labelVPPVDDShort);
			this.panelStep2VPP.Controls.Add(this.labelVPPResults);
			this.panelStep2VPP.Controls.Add(this.buttonMCLR);
			this.panelStep2VPP.Controls.Add(this.labelReadVPP);
			this.panelStep2VPP.Controls.Add(this.buttonTestVPP);
			this.panelStep2VPP.Controls.Add(this.label5);
			this.panelStep2VPP.Controls.Add(this.labelStep2FamilyVPP);
			this.panelStep2VPP.Controls.Add(this.pictureBox3);
			this.panelStep2VPP.Controls.Add(this.label6);
			this.panelStep2VPP.Location = new global::System.Drawing.Point(16, 15);
			this.panelStep2VPP.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.panelStep2VPP.Name = "panelStep2VPP";
			this.panelStep2VPP.Size = new global::System.Drawing.Size(413, 295);
			this.panelStep2VPP.TabIndex = 8;
			this.panelStep2VPP.Visible = false;
			this.buttonMCLROff.Location = new global::System.Drawing.Point(8, 261);
			this.buttonMCLROff.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.buttonMCLROff.Name = "buttonMCLROff";
			this.buttonMCLROff.Size = new global::System.Drawing.Size(96, 28);
			this.buttonMCLROff.TabIndex = 19;
			this.buttonMCLROff.Text = "/MCLR Off";
			this.buttonMCLROff.UseVisualStyleBackColor = true;
			this.buttonMCLROff.Click += new global::System.EventHandler(this.buttonMCLROff_Click);
			this.labelVPPMCLROff.AutoSize = true;
			this.labelVPPMCLROff.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelVPPMCLROff.ForeColor = global::System.Drawing.Color.Blue;
			this.labelVPPMCLROff.Location = new global::System.Drawing.Point(112, 188);
			this.labelVPPMCLROff.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelVPPMCLROff.Name = "labelVPPMCLROff";
			this.labelVPPMCLROff.Size = new global::System.Drawing.Size(252, 90);
			this.labelVPPMCLROff.TabIndex = 18;
			this.labelVPPMCLROff.Text = "/MCLR released.\r\n\r\nIf /MCLR has a pull-up, it should be at\r\nthe pull-up voltage.  If not, it will be at\r\nan indeterminate voltage.";
			this.labelVPPPass.AutoSize = true;
			this.labelVPPPass.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelVPPPass.ForeColor = global::System.Drawing.Color.Blue;
			this.labelVPPPass.Location = new global::System.Drawing.Point(112, 172);
			this.labelVPPPass.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelVPPPass.Name = "labelVPPPass";
			this.labelVPPPass.Size = new global::System.Drawing.Size(252, 90);
			this.labelVPPPass.TabIndex = 14;
			this.labelVPPPass.Text = "Test Passed:\r\n\r\nPlease check the device /MCLR-VPP\r\npin with a voltmeter to verify it sees\r\nthe appropriate VPP voltage.";
			this.label9.AutoSize = true;
			this.label9.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label9.Location = new global::System.Drawing.Point(112, 126);
			this.label9.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label9.Name = "label9";
			this.label9.Size = new global::System.Drawing.Size(267, 36);
			this.label9.TabIndex = 17;
			this.label9.Text = "4) Click \"/MCLR Off\" to check releasing\r\n/MCLR (VPP = tri-state)";
			this.label7.AutoSize = true;
			this.label7.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label7.Location = new global::System.Drawing.Point(112, 82);
			this.label7.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label7.Name = "label7";
			this.label7.Size = new global::System.Drawing.Size(268, 36);
			this.label7.TabIndex = 16;
			this.label7.Text = "3) Click \"/MCLR On\" to check asserting\r\n/MCLR (VPP = 0 V).";
			this.labelVPPMCLR.AutoSize = true;
			this.labelVPPMCLR.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelVPPMCLR.ForeColor = global::System.Drawing.Color.Blue;
			this.labelVPPMCLR.Location = new global::System.Drawing.Point(112, 172);
			this.labelVPPMCLR.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelVPPMCLR.Name = "labelVPPMCLR";
			this.labelVPPMCLR.Size = new global::System.Drawing.Size(241, 90);
			this.labelVPPMCLR.TabIndex = 15;
			this.labelVPPMCLR.Text = "/MCLR asserted.\r\n\r\nPlease check the device /MCLR pin\r\nwith a voltmeter to verify the pin\r\nis pulled low.";
			this.labelVPPShort.AutoSize = true;
			this.labelVPPShort.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelVPPShort.ForeColor = global::System.Drawing.Color.Red;
			this.labelVPPShort.Location = new global::System.Drawing.Point(112, 172);
			this.labelVPPShort.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelVPPShort.Name = "labelVPPShort";
			this.labelVPPShort.Size = new global::System.Drawing.Size(263, 90);
			this.labelVPPShort.TabIndex = 12;
			this.labelVPPShort.Text = "Test Failed:\r\nShort Detected.\r\n\r\nA short or very heavy load on VPP was\r\ndetected, and VPP was shut off.";
			this.labelVPPLow.AutoSize = true;
			this.labelVPPLow.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelVPPLow.ForeColor = global::System.Drawing.Color.Red;
			this.labelVPPLow.Location = new global::System.Drawing.Point(112, 172);
			this.labelVPPLow.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelVPPLow.Name = "labelVPPLow";
			this.labelVPPLow.Size = new global::System.Drawing.Size(238, 108);
			this.labelVPPLow.TabIndex = 13;
			this.labelVPPLow.Text = "Test Failed:\r\nLow VPP detected.\r\n\r\nVPP is not reaching the expected\r\nvoltage.  VPP cannot support more\r\nthan a few mA of current.";
			this.labelVPPVDDShort.AutoSize = true;
			this.labelVPPVDDShort.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelVPPVDDShort.ForeColor = global::System.Drawing.Color.Red;
			this.labelVPPVDDShort.Location = new global::System.Drawing.Point(112, 172);
			this.labelVPPVDDShort.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelVPPVDDShort.Name = "labelVPPVDDShort";
			this.labelVPPVDDShort.Size = new global::System.Drawing.Size(253, 90);
			this.labelVPPVDDShort.TabIndex = 11;
			this.labelVPPVDDShort.Text = "Test Failed:\r\nVDD Short Detected.\r\n\r\nVPP cannot be tested if a short exists\r\non VDD.";
			this.labelVPPResults.AutoSize = true;
			this.labelVPPResults.Location = new global::System.Drawing.Point(28, 172);
			this.labelVPPResults.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelVPPResults.Name = "labelVPPResults";
			this.labelVPPResults.Size = new global::System.Drawing.Size(59, 17);
			this.labelVPPResults.TabIndex = 12;
			this.labelVPPResults.Text = "Results:";
			this.buttonMCLR.Location = new global::System.Drawing.Point(8, 225);
			this.buttonMCLR.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.buttonMCLR.Name = "buttonMCLR";
			this.buttonMCLR.Size = new global::System.Drawing.Size(96, 28);
			this.buttonMCLR.TabIndex = 15;
			this.buttonMCLR.Text = "/MCLR On";
			this.buttonMCLR.UseVisualStyleBackColor = true;
			this.buttonMCLR.Click += new global::System.EventHandler(this.buttonMCLR_Click);
			this.labelReadVPP.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.labelReadVPP.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelReadVPP.Location = new global::System.Drawing.Point(8, 188);
			this.labelReadVPP.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelReadVPP.Name = "labelReadVPP";
			this.labelReadVPP.Size = new global::System.Drawing.Size(95, 28);
			this.labelReadVPP.TabIndex = 11;
			this.labelReadVPP.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.buttonTestVPP.Location = new global::System.Drawing.Point(8, 142);
			this.buttonTestVPP.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.buttonTestVPP.Name = "buttonTestVPP";
			this.buttonTestVPP.Size = new global::System.Drawing.Size(96, 28);
			this.buttonTestVPP.TabIndex = 14;
			this.buttonTestVPP.Text = "Test VPP";
			this.buttonTestVPP.UseVisualStyleBackColor = true;
			this.buttonTestVPP.Click += new global::System.EventHandler(this.buttonTestVPP_Click);
			this.label5.AutoSize = true;
			this.label5.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label5.Location = new global::System.Drawing.Point(112, 57);
			this.label5.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(283, 18);
			this.label5.TabIndex = 12;
			this.label5.Text = "2) Click \"Test VPP\" to check VPP voltage.";
			this.labelStep2FamilyVPP.AutoSize = true;
			this.labelStep2FamilyVPP.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelStep2FamilyVPP.Location = new global::System.Drawing.Point(112, 30);
			this.labelStep2FamilyVPP.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelStep2FamilyVPP.Name = "labelStep2FamilyVPP";
			this.labelStep2FamilyVPP.Size = new global::System.Drawing.Size(190, 18);
			this.labelStep2FamilyVPP.TabIndex = 11;
			this.labelStep2FamilyVPP.Text = "1) VPP for this family is \r\n";
			this.pictureBox3.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("pictureBox3.Image");
			this.pictureBox3.Location = new global::System.Drawing.Point(0, 0);
			this.pictureBox3.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new global::System.Drawing.Size(78, 116);
			this.pictureBox3.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox3.TabIndex = 7;
			this.pictureBox3.TabStop = false;
			this.label6.AutoEllipsis = true;
			this.label6.AutoSize = true;
			this.label6.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label6.Location = new global::System.Drawing.Point(149, 0);
			this.label6.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new global::System.Drawing.Size(183, 24);
			this.label6.TabIndex = 0;
			this.label6.Text = "Step 2: Verify VPP";
			this.panelPGCPGD.Controls.Add(this.labelPGxVDDShort);
			this.panelPGCPGD.Controls.Add(this.labelPGxOScope);
			this.panelPGCPGD.Controls.Add(this.label4);
			this.panelPGCPGD.Controls.Add(this.groupBoxPGD);
			this.panelPGCPGD.Controls.Add(this.pictureBox5);
			this.panelPGCPGD.Controls.Add(this.groupBoxPGC);
			this.panelPGCPGD.Controls.Add(this.label8);
			this.panelPGCPGD.Controls.Add(this.pictureBox4);
			this.panelPGCPGD.Location = new global::System.Drawing.Point(16, 15);
			this.panelPGCPGD.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.panelPGCPGD.Name = "panelPGCPGD";
			this.panelPGCPGD.Size = new global::System.Drawing.Size(413, 295);
			this.panelPGCPGD.TabIndex = 9;
			this.panelPGCPGD.Visible = false;
			this.labelPGxVDDShort.AutoSize = true;
			this.labelPGxVDDShort.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelPGxVDDShort.ForeColor = global::System.Drawing.Color.Red;
			this.labelPGxVDDShort.Location = new global::System.Drawing.Point(267, 126);
			this.labelPGxVDDShort.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelPGxVDDShort.Name = "labelPGxVDDShort";
			this.labelPGxVDDShort.Size = new global::System.Drawing.Size(102, 90);
			this.labelPGxVDDShort.TabIndex = 18;
			this.labelPGxVDDShort.Text = "VDD Short\r\ndetected!\r\n\r\nMust be\r\ncleared first.";
			this.labelPGxOScope.AutoSize = true;
			this.labelPGxOScope.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelPGxOScope.Location = new global::System.Drawing.Point(267, 105);
			this.labelPGxOScope.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelPGxOScope.Name = "labelPGxOScope";
			this.labelPGxOScope.Size = new global::System.Drawing.Size(130, 162);
			this.labelPGxOScope.TabIndex = 17;
			this.labelPGxOScope.Text = "It is recommended\r\nto use an oscillo-\r\nscope to verify \r\nwaveform edges\r\nare sharp.\r\n\"Toggle 30kHz\"\r\nwill toggle the pin\r\nat approximately\r\n30kHz.\r\n";
			this.label4.AutoSize = true;
			this.label4.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label4.Location = new global::System.Drawing.Point(267, 27);
			this.label4.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(130, 72);
			this.label4.TabIndex = 16;
			this.label4.Text = "Verify signal states\r\nat device pins with\r\na volt meter.\r\n\r\n";
			this.groupBoxPGD.Controls.Add(this.radioButtonPGDToggle);
			this.groupBoxPGD.Controls.Add(this.radioButtonPGDLow);
			this.groupBoxPGD.Controls.Add(this.radioButtonPGDHigh);
			this.groupBoxPGD.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.groupBoxPGD.Location = new global::System.Drawing.Point(112, 27);
			this.groupBoxPGD.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.groupBoxPGD.Name = "groupBoxPGD";
			this.groupBoxPGD.Padding = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.groupBoxPGD.Size = new global::System.Drawing.Size(140, 116);
			this.groupBoxPGD.TabIndex = 13;
			this.groupBoxPGD.TabStop = false;
			this.groupBoxPGD.Text = "PGD / ICSPDAT";
			this.radioButtonPGDToggle.Appearance = global::System.Windows.Forms.Appearance.Button;
			this.radioButtonPGDToggle.AutoSize = true;
			this.radioButtonPGDToggle.Location = new global::System.Drawing.Point(8, 17);
			this.radioButtonPGDToggle.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.radioButtonPGDToggle.Name = "radioButtonPGDToggle";
			this.radioButtonPGDToggle.Size = new global::System.Drawing.Size(106, 27);
			this.radioButtonPGDToggle.TabIndex = 11;
			this.radioButtonPGDToggle.TabStop = true;
			this.radioButtonPGDToggle.Text = "Toggle 30kHz";
			this.radioButtonPGDToggle.UseVisualStyleBackColor = true;
			this.radioButtonPGDToggle.Click += new global::System.EventHandler(this.radioButtonPGDToggle_Click);
			this.radioButtonPGDLow.Appearance = global::System.Windows.Forms.Appearance.Button;
			this.radioButtonPGDLow.Location = new global::System.Drawing.Point(8, 81);
			this.radioButtonPGDLow.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.radioButtonPGDLow.Name = "radioButtonPGDLow";
			this.radioButtonPGDLow.Size = new global::System.Drawing.Size(112, 28);
			this.radioButtonPGDLow.TabIndex = 10;
			this.radioButtonPGDLow.TabStop = true;
			this.radioButtonPGDLow.Text = "Low (GND)";
			this.radioButtonPGDLow.UseVisualStyleBackColor = true;
			this.radioButtonPGDLow.CheckedChanged += new global::System.EventHandler(this.radioButtonPGCHigh_CheckedChanged);
			this.radioButtonPGDHigh.Appearance = global::System.Windows.Forms.Appearance.Button;
			this.radioButtonPGDHigh.Location = new global::System.Drawing.Point(8, 49);
			this.radioButtonPGDHigh.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.radioButtonPGDHigh.Name = "radioButtonPGDHigh";
			this.radioButtonPGDHigh.Size = new global::System.Drawing.Size(112, 28);
			this.radioButtonPGDHigh.TabIndex = 9;
			this.radioButtonPGDHigh.TabStop = true;
			this.radioButtonPGDHigh.Text = "High (VDD)";
			this.radioButtonPGDHigh.UseVisualStyleBackColor = true;
			this.radioButtonPGDHigh.CheckedChanged += new global::System.EventHandler(this.radioButtonPGCHigh_CheckedChanged);
			this.pictureBox5.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("pictureBox5.Image");
			this.pictureBox5.Location = new global::System.Drawing.Point(0, 0);
			this.pictureBox5.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pictureBox5.Name = "pictureBox5";
			this.pictureBox5.Size = new global::System.Drawing.Size(78, 116);
			this.pictureBox5.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox5.TabIndex = 8;
			this.pictureBox5.TabStop = false;
			this.groupBoxPGC.Controls.Add(this.radioButtonPGCToggle);
			this.groupBoxPGC.Controls.Add(this.radioButtonPGCLow);
			this.groupBoxPGC.Controls.Add(this.radioButtonPGCHigh);
			this.groupBoxPGC.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.groupBoxPGC.Location = new global::System.Drawing.Point(112, 153);
			this.groupBoxPGC.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.groupBoxPGC.Name = "groupBoxPGC";
			this.groupBoxPGC.Padding = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.groupBoxPGC.Size = new global::System.Drawing.Size(140, 116);
			this.groupBoxPGC.TabIndex = 12;
			this.groupBoxPGC.TabStop = false;
			this.groupBoxPGC.Text = "PGC / ICSPCLK";
			this.radioButtonPGCToggle.Appearance = global::System.Windows.Forms.Appearance.Button;
			this.radioButtonPGCToggle.Location = new global::System.Drawing.Point(8, 17);
			this.radioButtonPGCToggle.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.radioButtonPGCToggle.Name = "radioButtonPGCToggle";
			this.radioButtonPGCToggle.Size = new global::System.Drawing.Size(112, 28);
			this.radioButtonPGCToggle.TabIndex = 12;
			this.radioButtonPGCToggle.TabStop = true;
			this.radioButtonPGCToggle.Text = "Toggle 30kHz";
			this.radioButtonPGCToggle.UseVisualStyleBackColor = true;
			this.radioButtonPGCToggle.Click += new global::System.EventHandler(this.radioButtonPGDToggle_Click);
			this.radioButtonPGCLow.Appearance = global::System.Windows.Forms.Appearance.Button;
			this.radioButtonPGCLow.Location = new global::System.Drawing.Point(8, 81);
			this.radioButtonPGCLow.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.radioButtonPGCLow.Name = "radioButtonPGCLow";
			this.radioButtonPGCLow.Size = new global::System.Drawing.Size(112, 28);
			this.radioButtonPGCLow.TabIndex = 10;
			this.radioButtonPGCLow.TabStop = true;
			this.radioButtonPGCLow.Text = "Low (GND)";
			this.radioButtonPGCLow.UseVisualStyleBackColor = true;
			this.radioButtonPGCLow.CheckedChanged += new global::System.EventHandler(this.radioButtonPGCHigh_CheckedChanged);
			this.radioButtonPGCHigh.Appearance = global::System.Windows.Forms.Appearance.Button;
			this.radioButtonPGCHigh.Location = new global::System.Drawing.Point(8, 49);
			this.radioButtonPGCHigh.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.radioButtonPGCHigh.Name = "radioButtonPGCHigh";
			this.radioButtonPGCHigh.Size = new global::System.Drawing.Size(112, 28);
			this.radioButtonPGCHigh.TabIndex = 9;
			this.radioButtonPGCHigh.TabStop = true;
			this.radioButtonPGCHigh.Text = "High (VDD)";
			this.radioButtonPGCHigh.UseVisualStyleBackColor = true;
			this.radioButtonPGCHigh.CheckedChanged += new global::System.EventHandler(this.radioButtonPGCHigh_CheckedChanged);
			this.label8.AutoEllipsis = true;
			this.label8.AutoSize = true;
			this.label8.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label8.Location = new global::System.Drawing.Point(125, 0);
			this.label8.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label8.Name = "label8";
			this.label8.Size = new global::System.Drawing.Size(254, 24);
			this.label8.TabIndex = 0;
			this.label8.Text = "Step 3: Verify PGC + PGD";
			this.pictureBox4.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("pictureBox4.Image");
			this.pictureBox4.Location = new global::System.Drawing.Point(0, 153);
			this.pictureBox4.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pictureBox4.Name = "pictureBox4";
			this.pictureBox4.Size = new global::System.Drawing.Size(78, 116);
			this.pictureBox4.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox4.TabIndex = 7;
			this.pictureBox4.TabStop = false;
			this.timerPGxToggle.Interval = 450;
			this.timerPGxToggle.Tick += new global::System.EventHandler(this.timerPGxToggle_Tick);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(120f, 120f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = global::System.Drawing.SystemColors.Control;
			base.ClientSize = new global::System.Drawing.Size(445, 354);
			base.Controls.Add(this.panelPGCPGD);
			base.Controls.Add(this.panelStep2VPP);
			base.Controls.Add(this.panelCautionVDD);
			base.Controls.Add(this.panelStep1VDDTest);
			base.Controls.Add(this.buttonBack);
			base.Controls.Add(this.buttonNext);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.panelStep1VDDExt);
			base.Controls.Add(this.panelIntro);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DialogTroubleshoot";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "PICkit 2 Troubleshooting";
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.trblshtingFormClosing);
			this.panelIntro.ResumeLayout(false);
			this.panelIntro.PerformLayout();
			this.panelStep1VDDExt.ResumeLayout(false);
			this.panelStep1VDDExt.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.numericUpDown1).EndInit();
			this.panelStep1VDDTest.ResumeLayout(false);
			this.panelStep1VDDTest.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
			this.panelCautionVDD.ResumeLayout(false);
			this.panelCautionVDD.PerformLayout();
			this.panelStep2VPP.ResumeLayout(false);
			this.panelStep2VPP.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox3).EndInit();
			this.panelPGCPGD.ResumeLayout(false);
			this.panelPGCPGD.PerformLayout();
			this.groupBoxPGD.ResumeLayout(false);
			this.groupBoxPGD.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox5).EndInit();
			this.groupBoxPGC.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox4).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x0400006B RID: 107
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400006C RID: 108
		private global::System.Windows.Forms.Panel panelIntro;

		// Token: 0x0400006D RID: 109
		private global::System.Windows.Forms.Label labelIntroTitle;

		// Token: 0x0400006E RID: 110
		private global::System.Windows.Forms.Panel panelStep1VDDExt;

		// Token: 0x0400006F RID: 111
		private global::System.Windows.Forms.Button buttonStep1Recheck;

		// Token: 0x04000070 RID: 112
		private global::System.Windows.Forms.Label labelStep1ExtTitle;

		// Token: 0x04000071 RID: 113
		private global::System.Windows.Forms.Button buttonCancel;

		// Token: 0x04000072 RID: 114
		private global::System.Windows.Forms.Button buttonNext;

		// Token: 0x04000073 RID: 115
		private global::System.Windows.Forms.Button buttonBack;

		// Token: 0x04000074 RID: 116
		private global::System.Windows.Forms.Label labelIntroText1;

		// Token: 0x04000075 RID: 117
		private global::System.Windows.Forms.Label labelVoltageOnVDD;

		// Token: 0x04000076 RID: 118
		private global::System.Windows.Forms.Label labelVoltageOnVDD2;

		// Token: 0x04000077 RID: 119
		private global::System.Windows.Forms.NumericUpDown numericUpDown1;

		// Token: 0x04000078 RID: 120
		private global::System.Windows.Forms.Label labelTestVDD;

		// Token: 0x04000079 RID: 121
		private global::System.Windows.Forms.Panel panelStep1VDDTest;

		// Token: 0x0400007A RID: 122
		private global::System.Windows.Forms.Label labelStep1Title;

		// Token: 0x0400007B RID: 123
		private global::System.Windows.Forms.PictureBox pictureBox1;

		// Token: 0x0400007C RID: 124
		private global::System.Windows.Forms.PictureBox pictureBox2;

		// Token: 0x0400007D RID: 125
		private global::System.Windows.Forms.Button buttonVDDOn;

		// Token: 0x0400007E RID: 126
		private global::System.Windows.Forms.Label labelReadVDD;

		// Token: 0x0400007F RID: 127
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000080 RID: 128
		private global::System.Windows.Forms.Label labelGood;

		// Token: 0x04000081 RID: 129
		private global::System.Windows.Forms.Label labelVDDShort;

		// Token: 0x04000082 RID: 130
		private global::System.Windows.Forms.Label labelVDDLow;

		// Token: 0x04000083 RID: 131
		private global::System.Windows.Forms.Panel panelCautionVDD;

		// Token: 0x04000084 RID: 132
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000085 RID: 133
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000086 RID: 134
		private global::System.Windows.Forms.Panel panelStep2VPP;

		// Token: 0x04000087 RID: 135
		private global::System.Windows.Forms.PictureBox pictureBox3;

		// Token: 0x04000088 RID: 136
		private global::System.Windows.Forms.Label label6;

		// Token: 0x04000089 RID: 137
		private global::System.Windows.Forms.Label label5;

		// Token: 0x0400008A RID: 138
		private global::System.Windows.Forms.Label labelStep2FamilyVPP;

		// Token: 0x0400008B RID: 139
		private global::System.Windows.Forms.Button buttonTestVPP;

		// Token: 0x0400008C RID: 140
		private global::System.Windows.Forms.Button buttonMCLR;

		// Token: 0x0400008D RID: 141
		private global::System.Windows.Forms.Label labelVPPVDDShort;

		// Token: 0x0400008E RID: 142
		private global::System.Windows.Forms.Label labelVPPResults;

		// Token: 0x0400008F RID: 143
		private global::System.Windows.Forms.Label labelReadVPP;

		// Token: 0x04000090 RID: 144
		private global::System.Windows.Forms.Label labelVPPShort;

		// Token: 0x04000091 RID: 145
		private global::System.Windows.Forms.Label labelVPPLow;

		// Token: 0x04000092 RID: 146
		private global::System.Windows.Forms.Label labelVPPPass;

		// Token: 0x04000093 RID: 147
		private global::System.Windows.Forms.Label labelVPPMCLR;

		// Token: 0x04000094 RID: 148
		private global::System.Windows.Forms.Panel panelPGCPGD;

		// Token: 0x04000095 RID: 149
		private global::System.Windows.Forms.PictureBox pictureBox5;

		// Token: 0x04000096 RID: 150
		private global::System.Windows.Forms.PictureBox pictureBox4;

		// Token: 0x04000097 RID: 151
		private global::System.Windows.Forms.Label label8;

		// Token: 0x04000098 RID: 152
		private global::System.Windows.Forms.RadioButton radioButtonPGCLow;

		// Token: 0x04000099 RID: 153
		private global::System.Windows.Forms.RadioButton radioButtonPGCHigh;

		// Token: 0x0400009A RID: 154
		private global::System.Windows.Forms.GroupBox groupBoxPGD;

		// Token: 0x0400009B RID: 155
		private global::System.Windows.Forms.RadioButton radioButtonPGDLow;

		// Token: 0x0400009C RID: 156
		private global::System.Windows.Forms.RadioButton radioButtonPGDHigh;

		// Token: 0x0400009D RID: 157
		private global::System.Windows.Forms.GroupBox groupBoxPGC;

		// Token: 0x0400009E RID: 158
		private global::System.Windows.Forms.Label label4;

		// Token: 0x0400009F RID: 159
		private global::System.Windows.Forms.Label labelPGxOScope;

		// Token: 0x040000A0 RID: 160
		private global::System.Windows.Forms.Label labelPGxVDDShort;

		// Token: 0x040000A1 RID: 161
		private global::System.Windows.Forms.Label label9;

		// Token: 0x040000A2 RID: 162
		private global::System.Windows.Forms.Label label7;

		// Token: 0x040000A3 RID: 163
		private global::System.Windows.Forms.Label labelVPPMCLROff;

		// Token: 0x040000A4 RID: 164
		private global::System.Windows.Forms.Button buttonMCLROff;

		// Token: 0x040000A5 RID: 165
		private global::System.Windows.Forms.RadioButton radioButtonPGDToggle;

		// Token: 0x040000A6 RID: 166
		private global::System.Windows.Forms.Timer timerPGxToggle;

		// Token: 0x040000A7 RID: 167
		private global::System.Windows.Forms.RadioButton radioButtonPGCToggle;

		// Token: 0x040000A8 RID: 168
		private global::System.Windows.Forms.Label label10;

		// Token: 0x040000A9 RID: 169
		private global::System.Windows.Forms.Label label11;

		// Token: 0x040000AA RID: 170
		private global::System.Windows.Forms.Label label12;
	}
}
