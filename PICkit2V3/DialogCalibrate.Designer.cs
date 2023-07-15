namespace PICkit2V3
{
	// Token: 0x02000008 RID: 8
	public partial class DialogCalibrate : global::System.Windows.Forms.Form
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00006811 File Offset: 0x00005811
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00006830 File Offset: 0x00005830
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::PICkit2V3.DialogCalibrate));
			this.panelIntro = new global::System.Windows.Forms.Panel();
			this.label2 = new global::System.Windows.Forms.Label();
			this.buttonClearUnitID = new global::System.Windows.Forms.Button();
			this.buttonClearCal = new global::System.Windows.Forms.Button();
			this.label1 = new global::System.Windows.Forms.Label();
			this.buttonBack = new global::System.Windows.Forms.Button();
			this.buttonNext = new global::System.Windows.Forms.Button();
			this.buttonCancel = new global::System.Windows.Forms.Button();
			this.panelSetup = new global::System.Windows.Forms.Panel();
			this.label11 = new global::System.Windows.Forms.Label();
			this.label6 = new global::System.Windows.Forms.Label();
			this.label5 = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			this.pictureBox1 = new global::System.Windows.Forms.PictureBox();
			this.label3 = new global::System.Windows.Forms.Label();
			this.panelCal = new global::System.Windows.Forms.Panel();
			this.labelBadCal = new global::System.Windows.Forms.Label();
			this.labelGoodCal = new global::System.Windows.Forms.Label();
			this.buttonCalibrate = new global::System.Windows.Forms.Button();
			this.label8 = new global::System.Windows.Forms.Label();
			this.label7 = new global::System.Windows.Forms.Label();
			this.textBoxVDD = new global::System.Windows.Forms.TextBox();
			this.label9 = new global::System.Windows.Forms.Label();
			this.pictureBox2 = new global::System.Windows.Forms.PictureBox();
			this.label10 = new global::System.Windows.Forms.Label();
			this.panelUnitID = new global::System.Windows.Forms.Panel();
			this.labelAssignedID = new global::System.Windows.Forms.Label();
			this.buttonSetUnitID = new global::System.Windows.Forms.Button();
			this.textBoxUnitID = new global::System.Windows.Forms.TextBox();
			this.label12 = new global::System.Windows.Forms.Label();
			this.label15 = new global::System.Windows.Forms.Label();
			this.label16 = new global::System.Windows.Forms.Label();
			this.panelIntro.SuspendLayout();
			this.panelSetup.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
			this.panelCal.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
			this.panelUnitID.SuspendLayout();
			base.SuspendLayout();
			this.panelIntro.Controls.Add(this.label2);
			this.panelIntro.Controls.Add(this.buttonClearUnitID);
			this.panelIntro.Controls.Add(this.buttonClearCal);
			this.panelIntro.Controls.Add(this.label1);
			this.panelIntro.Location = new global::System.Drawing.Point(13, 12);
			this.panelIntro.Name = "panelIntro";
			this.panelIntro.Size = new global::System.Drawing.Size(330, 236);
			this.panelIntro.TabIndex = 0;
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(0, 29);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(307, 169);
			this.label2.TabIndex = 3;
			this.label2.Text = componentResourceManager.GetString("label2.Text");
			this.buttonClearUnitID.Enabled = false;
			this.buttonClearUnitID.Location = new global::System.Drawing.Point(176, 205);
			this.buttonClearUnitID.Name = "buttonClearUnitID";
			this.buttonClearUnitID.Size = new global::System.Drawing.Size(134, 22);
			this.buttonClearUnitID.TabIndex = 2;
			this.buttonClearUnitID.Text = "Clear Unit ID";
			this.buttonClearUnitID.UseVisualStyleBackColor = true;
			this.buttonClearUnitID.Click += new global::System.EventHandler(this.buttonClearUnitID_Click);
			this.buttonClearCal.Enabled = false;
			this.buttonClearCal.Location = new global::System.Drawing.Point(22, 205);
			this.buttonClearCal.Name = "buttonClearCal";
			this.buttonClearCal.Size = new global::System.Drawing.Size(134, 22);
			this.buttonClearCal.TabIndex = 1;
			this.buttonClearCal.Text = "Clear Calibration";
			this.buttonClearCal.UseVisualStyleBackColor = true;
			this.buttonClearCal.Click += new global::System.EventHandler(this.buttonClearCal_Click);
			this.label1.AutoSize = true;
			this.label1.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new global::System.Drawing.Point(63, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(193, 19);
			this.label1.TabIndex = 0;
			this.label1.Text = "PICkit 2 VDD Calibration";
			this.buttonBack.Enabled = false;
			this.buttonBack.Location = new global::System.Drawing.Point(90, 254);
			this.buttonBack.Name = "buttonBack";
			this.buttonBack.Size = new global::System.Drawing.Size(80, 22);
			this.buttonBack.TabIndex = 1;
			this.buttonBack.Text = "< Back";
			this.buttonBack.UseVisualStyleBackColor = true;
			this.buttonBack.Click += new global::System.EventHandler(this.buttonBack_Click);
			this.buttonNext.Location = new global::System.Drawing.Point(177, 254);
			this.buttonNext.Name = "buttonNext";
			this.buttonNext.Size = new global::System.Drawing.Size(80, 22);
			this.buttonNext.TabIndex = 2;
			this.buttonNext.Text = "Next >";
			this.buttonNext.UseVisualStyleBackColor = true;
			this.buttonNext.Click += new global::System.EventHandler(this.buttonNext_Click);
			this.buttonCancel.Location = new global::System.Drawing.Point(263, 254);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new global::System.Drawing.Size(80, 22);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new global::System.EventHandler(this.buttonCancel_Click);
			this.panelSetup.Controls.Add(this.label11);
			this.panelSetup.Controls.Add(this.label6);
			this.panelSetup.Controls.Add(this.label5);
			this.panelSetup.Controls.Add(this.label4);
			this.panelSetup.Controls.Add(this.pictureBox1);
			this.panelSetup.Controls.Add(this.label3);
			this.panelSetup.Location = new global::System.Drawing.Point(13, 12);
			this.panelSetup.Name = "panelSetup";
			this.panelSetup.Size = new global::System.Drawing.Size(330, 236);
			this.panelSetup.TabIndex = 4;
			this.panelSetup.Visible = false;
			this.label11.AutoSize = true;
			this.label11.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label11.ForeColor = global::System.Drawing.Color.Red;
			this.label11.Location = new global::System.Drawing.Point(4, 222);
			this.label11.Name = "label11";
			this.label11.Size = new global::System.Drawing.Size(306, 15);
			this.label11.TabIndex = 6;
			this.label11.Text = "CAUTION: Clicking NEXT will erase existing calibration.";
			this.label6.AutoSize = true;
			this.label6.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label6.Location = new global::System.Drawing.Point(4, 168);
			this.label6.Name = "label6";
			this.label6.Size = new global::System.Drawing.Size(289, 45);
			this.label6.TabIndex = 5;
			this.label6.Text = "Step 3:\r\nClick NEXT and the PICkit 2 will apply approximately\r\n4 Volts to the VDD pin.";
			this.label5.AutoSize = true;
			this.label5.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label5.Location = new global::System.Drawing.Point(102, 94);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(195, 60);
			this.label5.TabIndex = 4;
			this.label5.Text = "Step 2:\r\nConnect a voltage meter between\r\npin 2 (VDD) and pin 3 (GND) of the\r\nPICkit 2 ICSP connector.";
			this.label4.AutoSize = true;
			this.label4.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label4.Location = new global::System.Drawing.Point(102, 22);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(186, 60);
			this.label4.TabIndex = 3;
			this.label4.Text = "Step 1:\r\nMake sure the PICkit 2 is not\r\nconnected to any device or circuit\r\nboard.";
			this.pictureBox1.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("pictureBox1.Image");
			this.pictureBox1.Location = new global::System.Drawing.Point(0, 22);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new global::System.Drawing.Size(78, 116);
			this.pictureBox1.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			this.label3.AutoSize = true;
			this.label3.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label3.Location = new global::System.Drawing.Point(61, 0);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(193, 19);
			this.label3.TabIndex = 1;
			this.label3.Text = "PICkit 2 VDD Calibration";
			this.panelCal.Controls.Add(this.labelBadCal);
			this.panelCal.Controls.Add(this.labelGoodCal);
			this.panelCal.Controls.Add(this.buttonCalibrate);
			this.panelCal.Controls.Add(this.label8);
			this.panelCal.Controls.Add(this.label7);
			this.panelCal.Controls.Add(this.textBoxVDD);
			this.panelCal.Controls.Add(this.label9);
			this.panelCal.Controls.Add(this.pictureBox2);
			this.panelCal.Controls.Add(this.label10);
			this.panelCal.Location = new global::System.Drawing.Point(13, 12);
			this.panelCal.Name = "panelCal";
			this.panelCal.Size = new global::System.Drawing.Size(330, 236);
			this.panelCal.TabIndex = 6;
			this.panelCal.Visible = false;
			this.labelBadCal.AutoSize = true;
			this.labelBadCal.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelBadCal.ForeColor = global::System.Drawing.Color.Red;
			this.labelBadCal.Location = new global::System.Drawing.Point(3, 198);
			this.labelBadCal.Name = "labelBadCal";
			this.labelBadCal.Size = new global::System.Drawing.Size(276, 30);
			this.labelBadCal.TabIndex = 9;
			this.labelBadCal.Text = "Could not fully calibrate the unit.  The USB voltage\r\nmay be too low to completely calibrate.";
			this.labelBadCal.Visible = false;
			this.labelGoodCal.AutoSize = true;
			this.labelGoodCal.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelGoodCal.ForeColor = global::System.Drawing.Color.Blue;
			this.labelGoodCal.Location = new global::System.Drawing.Point(3, 208);
			this.labelGoodCal.Name = "labelGoodCal";
			this.labelGoodCal.Size = new global::System.Drawing.Size(170, 15);
			this.labelGoodCal.TabIndex = 8;
			this.labelGoodCal.Text = "CALIBRATION SUCCESSFUL!";
			this.labelGoodCal.Visible = false;
			this.buttonCalibrate.Location = new global::System.Drawing.Point(6, 172);
			this.buttonCalibrate.Name = "buttonCalibrate";
			this.buttonCalibrate.Size = new global::System.Drawing.Size(112, 22);
			this.buttonCalibrate.TabIndex = 7;
			this.buttonCalibrate.Text = "Calibrate";
			this.buttonCalibrate.UseVisualStyleBackColor = true;
			this.buttonCalibrate.Click += new global::System.EventHandler(this.buttonCalibrate_Click);
			this.label8.AutoSize = true;
			this.label8.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label8.Location = new global::System.Drawing.Point(3, 139);
			this.label8.Name = "label8";
			this.label8.Size = new global::System.Drawing.Size(287, 30);
			this.label8.TabIndex = 6;
			this.label8.Text = "Step 5:\r\nClick the CALIBRATE button to calibrate the PICkit 2.";
			this.label7.AutoSize = true;
			this.label7.Location = new global::System.Drawing.Point(164, 104);
			this.label7.Name = "label7";
			this.label7.Size = new global::System.Drawing.Size(80, 13);
			this.label7.TabIndex = 5;
			this.label7.Text = "Volts Measured";
			this.textBoxVDD.Location = new global::System.Drawing.Point(105, 102);
			this.textBoxVDD.Name = "textBoxVDD";
			this.textBoxVDD.Size = new global::System.Drawing.Size(53, 20);
			this.textBoxVDD.TabIndex = 4;
			this.textBoxVDD.Text = "4.000";
			this.label9.AutoSize = true;
			this.label9.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label9.Location = new global::System.Drawing.Point(102, 22);
			this.label9.Name = "label9";
			this.label9.Size = new global::System.Drawing.Size(209, 60);
			this.label9.TabIndex = 3;
			this.label9.Text = "Step 4:\r\nEnter the actual voltage measured\r\non the volt meter in the box below, up\r\nto 3 decimal places.";
			this.pictureBox2.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("pictureBox2.Image");
			this.pictureBox2.Location = new global::System.Drawing.Point(0, 22);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new global::System.Drawing.Size(78, 116);
			this.pictureBox2.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox2.TabIndex = 2;
			this.pictureBox2.TabStop = false;
			this.label10.AutoSize = true;
			this.label10.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label10.Location = new global::System.Drawing.Point(61, 0);
			this.label10.Name = "label10";
			this.label10.Size = new global::System.Drawing.Size(193, 19);
			this.label10.TabIndex = 1;
			this.label10.Text = "PICkit 2 VDD Calibration";
			this.panelUnitID.Controls.Add(this.labelAssignedID);
			this.panelUnitID.Controls.Add(this.buttonSetUnitID);
			this.panelUnitID.Controls.Add(this.textBoxUnitID);
			this.panelUnitID.Controls.Add(this.label12);
			this.panelUnitID.Controls.Add(this.label15);
			this.panelUnitID.Controls.Add(this.label16);
			this.panelUnitID.Location = new global::System.Drawing.Point(13, 12);
			this.panelUnitID.Name = "panelUnitID";
			this.panelUnitID.Size = new global::System.Drawing.Size(330, 236);
			this.panelUnitID.TabIndex = 7;
			this.panelUnitID.Visible = false;
			this.labelAssignedID.AutoSize = true;
			this.labelAssignedID.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelAssignedID.ForeColor = global::System.Drawing.Color.Blue;
			this.labelAssignedID.Location = new global::System.Drawing.Point(58, 217);
			this.labelAssignedID.Name = "labelAssignedID";
			this.labelAssignedID.Size = new global::System.Drawing.Size(179, 15);
			this.labelAssignedID.TabIndex = 7;
			this.labelAssignedID.Text = "Unit ID Assigned to this PICkit 2.";
			this.labelAssignedID.Visible = false;
			this.buttonSetUnitID.Location = new global::System.Drawing.Point(186, 188);
			this.buttonSetUnitID.Name = "buttonSetUnitID";
			this.buttonSetUnitID.Size = new global::System.Drawing.Size(106, 22);
			this.buttonSetUnitID.TabIndex = 6;
			this.buttonSetUnitID.Text = "Assign Unit ID";
			this.buttonSetUnitID.UseVisualStyleBackColor = true;
			this.buttonSetUnitID.Click += new global::System.EventHandler(this.buttonSetUnitID_Click);
			this.textBoxUnitID.Location = new global::System.Drawing.Point(26, 190);
			this.textBoxUnitID.Name = "textBoxUnitID";
			this.textBoxUnitID.Size = new global::System.Drawing.Size(134, 20);
			this.textBoxUnitID.TabIndex = 5;
			this.textBoxUnitID.Text = "AAAAAAAAAAAAAA";
			this.textBoxUnitID.TextChanged += new global::System.EventHandler(this.textBoxUnitID_TextChanged);
			this.label12.AutoSize = true;
			this.label12.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label12.Location = new global::System.Drawing.Point(118, 18);
			this.label12.Name = "label12";
			this.label12.Size = new global::System.Drawing.Size(76, 18);
			this.label12.TabIndex = 4;
			this.label12.Text = "(Optional)";
			this.label15.AutoSize = true;
			this.label15.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label15.Location = new global::System.Drawing.Point(0, 48);
			this.label15.Name = "label15";
			this.label15.Size = new global::System.Drawing.Size(308, 120);
			this.label15.TabIndex = 3;
			this.label15.Text = componentResourceManager.GetString("label15.Text");
			this.label16.AutoSize = true;
			this.label16.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label16.Location = new global::System.Drawing.Point(61, 0);
			this.label16.Name = "label16";
			this.label16.Size = new global::System.Drawing.Size(190, 19);
			this.label16.TabIndex = 1;
			this.label16.Text = "Unit Identification Name";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(96f, 96f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Dpi;
			base.ClientSize = new global::System.Drawing.Size(354, 282);
			base.Controls.Add(this.panelUnitID);
			base.Controls.Add(this.panelCal);
			base.Controls.Add(this.panelSetup);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonNext);
			base.Controls.Add(this.buttonBack);
			base.Controls.Add(this.panelIntro);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DialogCalibrate";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "DialogCalibrate";
			this.panelIntro.ResumeLayout(false);
			this.panelIntro.PerformLayout();
			this.panelSetup.ResumeLayout(false);
			this.panelSetup.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
			this.panelCal.ResumeLayout(false);
			this.panelCal.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
			this.panelUnitID.ResumeLayout(false);
			this.panelUnitID.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x04000049 RID: 73
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400004A RID: 74
		private global::System.Windows.Forms.Panel panelIntro;

		// Token: 0x0400004B RID: 75
		private global::System.Windows.Forms.Button buttonBack;

		// Token: 0x0400004C RID: 76
		private global::System.Windows.Forms.Button buttonNext;

		// Token: 0x0400004D RID: 77
		private global::System.Windows.Forms.Button buttonCancel;

		// Token: 0x0400004E RID: 78
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400004F RID: 79
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000050 RID: 80
		private global::System.Windows.Forms.Button buttonClearUnitID;

		// Token: 0x04000051 RID: 81
		private global::System.Windows.Forms.Button buttonClearCal;

		// Token: 0x04000052 RID: 82
		private global::System.Windows.Forms.Panel panelSetup;

		// Token: 0x04000053 RID: 83
		private global::System.Windows.Forms.PictureBox pictureBox1;

		// Token: 0x04000054 RID: 84
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000055 RID: 85
		private global::System.Windows.Forms.Label label5;

		// Token: 0x04000056 RID: 86
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000057 RID: 87
		private global::System.Windows.Forms.Label label6;

		// Token: 0x04000058 RID: 88
		private global::System.Windows.Forms.Panel panelCal;

		// Token: 0x04000059 RID: 89
		private global::System.Windows.Forms.Label label9;

		// Token: 0x0400005A RID: 90
		private global::System.Windows.Forms.PictureBox pictureBox2;

		// Token: 0x0400005B RID: 91
		private global::System.Windows.Forms.Label label10;

		// Token: 0x0400005C RID: 92
		private global::System.Windows.Forms.TextBox textBoxVDD;

		// Token: 0x0400005D RID: 93
		private global::System.Windows.Forms.Label label7;

		// Token: 0x0400005E RID: 94
		private global::System.Windows.Forms.Label labelBadCal;

		// Token: 0x0400005F RID: 95
		private global::System.Windows.Forms.Label labelGoodCal;

		// Token: 0x04000060 RID: 96
		private global::System.Windows.Forms.Button buttonCalibrate;

		// Token: 0x04000061 RID: 97
		private global::System.Windows.Forms.Label label8;

		// Token: 0x04000062 RID: 98
		private global::System.Windows.Forms.Label label11;

		// Token: 0x04000063 RID: 99
		private global::System.Windows.Forms.Panel panelUnitID;

		// Token: 0x04000064 RID: 100
		private global::System.Windows.Forms.Label label12;

		// Token: 0x04000065 RID: 101
		private global::System.Windows.Forms.Label label15;

		// Token: 0x04000066 RID: 102
		private global::System.Windows.Forms.Label label16;

		// Token: 0x04000067 RID: 103
		private global::System.Windows.Forms.Button buttonSetUnitID;

		// Token: 0x04000068 RID: 104
		private global::System.Windows.Forms.TextBox textBoxUnitID;

		// Token: 0x04000069 RID: 105
		private global::System.Windows.Forms.Label labelAssignedID;
	}
}
