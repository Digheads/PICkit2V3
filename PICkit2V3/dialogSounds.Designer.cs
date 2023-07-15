namespace PICkit2V3
{
	// Token: 0x0200000C RID: 12
	public partial class dialogSounds : global::System.Windows.Forms.Form
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00010291 File Offset: 0x0000F291
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000102B0 File Offset: 0x0000F2B0
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::PICkit2V3.dialogSounds));
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.checkBoxSuccess = new global::System.Windows.Forms.CheckBox();
			this.checkBoxWarning = new global::System.Windows.Forms.CheckBox();
			this.checkBoxError = new global::System.Windows.Forms.CheckBox();
			this.textBoxSuccessFile = new global::System.Windows.Forms.TextBox();
			this.textBoxWarningFile = new global::System.Windows.Forms.TextBox();
			this.textBoxErrorFile = new global::System.Windows.Forms.TextBox();
			this.buttonSuccessBrowse = new global::System.Windows.Forms.Button();
			this.buttonWarningBrowse = new global::System.Windows.Forms.Button();
			this.buttonErrorBrowse = new global::System.Windows.Forms.Button();
			this.buttonOK = new global::System.Windows.Forms.Button();
			this.openFileDialogWAV = new global::System.Windows.Forms.OpenFileDialog();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(516, 39);
			this.label1.TabIndex = 0;
			this.label1.Text = componentResourceManager.GetString("label1.Text");
			this.label2.AutoSize = true;
			this.label2.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label2.Location = new global::System.Drawing.Point(12, 61);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(50, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Enable:";
			this.label3.AutoSize = true;
			this.label3.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label3.Location = new global::System.Drawing.Point(110, 61);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(112, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Play this WAV file:";
			this.checkBoxSuccess.BackColor = global::System.Drawing.Color.LimeGreen;
			this.checkBoxSuccess.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.checkBoxSuccess.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.checkBoxSuccess.Location = new global::System.Drawing.Point(15, 80);
			this.checkBoxSuccess.Name = "checkBoxSuccess";
			this.checkBoxSuccess.Size = new global::System.Drawing.Size(74, 17);
			this.checkBoxSuccess.TabIndex = 1;
			this.checkBoxSuccess.Text = "Success";
			this.checkBoxSuccess.UseVisualStyleBackColor = false;
			this.checkBoxSuccess.Click += new global::System.EventHandler(this.checkBoxSuccess_CheckedChanged);
			this.checkBoxWarning.BackColor = global::System.Drawing.Color.Yellow;
			this.checkBoxWarning.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.checkBoxWarning.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.checkBoxWarning.Location = new global::System.Drawing.Point(15, 103);
			this.checkBoxWarning.Name = "checkBoxWarning";
			this.checkBoxWarning.Size = new global::System.Drawing.Size(74, 17);
			this.checkBoxWarning.TabIndex = 1;
			this.checkBoxWarning.Text = "Warning";
			this.checkBoxWarning.UseVisualStyleBackColor = false;
			this.checkBoxWarning.Click += new global::System.EventHandler(this.checkBoxWarning_CheckedChanged);
			this.checkBoxError.BackColor = global::System.Drawing.Color.Salmon;
			this.checkBoxError.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.checkBoxError.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.checkBoxError.Location = new global::System.Drawing.Point(15, 126);
			this.checkBoxError.Name = "checkBoxError";
			this.checkBoxError.Size = new global::System.Drawing.Size(74, 17);
			this.checkBoxError.TabIndex = 1;
			this.checkBoxError.Text = "Error";
			this.checkBoxError.UseVisualStyleBackColor = false;
			this.checkBoxError.Click += new global::System.EventHandler(this.checkBoxError_CheckedChanged);
			this.textBoxSuccessFile.Location = new global::System.Drawing.Point(113, 78);
			this.textBoxSuccessFile.Name = "textBoxSuccessFile";
			this.textBoxSuccessFile.Size = new global::System.Drawing.Size(341, 20);
			this.textBoxSuccessFile.TabIndex = 2;
			this.textBoxWarningFile.Location = new global::System.Drawing.Point(113, 101);
			this.textBoxWarningFile.Name = "textBoxWarningFile";
			this.textBoxWarningFile.Size = new global::System.Drawing.Size(341, 20);
			this.textBoxWarningFile.TabIndex = 2;
			this.textBoxErrorFile.Location = new global::System.Drawing.Point(113, 124);
			this.textBoxErrorFile.Name = "textBoxErrorFile";
			this.textBoxErrorFile.Size = new global::System.Drawing.Size(341, 20);
			this.textBoxErrorFile.TabIndex = 2;
			this.buttonSuccessBrowse.Location = new global::System.Drawing.Point(460, 76);
			this.buttonSuccessBrowse.Name = "buttonSuccessBrowse";
			this.buttonSuccessBrowse.Size = new global::System.Drawing.Size(60, 23);
			this.buttonSuccessBrowse.TabIndex = 3;
			this.buttonSuccessBrowse.Text = "Browse";
			this.buttonSuccessBrowse.UseVisualStyleBackColor = true;
			this.buttonSuccessBrowse.Click += new global::System.EventHandler(this.buttonSuccessBrowse_Click);
			this.buttonWarningBrowse.Location = new global::System.Drawing.Point(460, 99);
			this.buttonWarningBrowse.Name = "buttonWarningBrowse";
			this.buttonWarningBrowse.Size = new global::System.Drawing.Size(60, 23);
			this.buttonWarningBrowse.TabIndex = 3;
			this.buttonWarningBrowse.Text = "Browse";
			this.buttonWarningBrowse.UseVisualStyleBackColor = true;
			this.buttonWarningBrowse.Click += new global::System.EventHandler(this.button1_Click);
			this.buttonErrorBrowse.Location = new global::System.Drawing.Point(460, 122);
			this.buttonErrorBrowse.Name = "buttonErrorBrowse";
			this.buttonErrorBrowse.Size = new global::System.Drawing.Size(60, 23);
			this.buttonErrorBrowse.TabIndex = 3;
			this.buttonErrorBrowse.Text = "Browse";
			this.buttonErrorBrowse.UseVisualStyleBackColor = true;
			this.buttonErrorBrowse.Click += new global::System.EventHandler(this.buttonErrorBrowse_Click);
			this.buttonOK.Location = new global::System.Drawing.Point(437, 151);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new global::System.Drawing.Size(83, 23);
			this.buttonOK.TabIndex = 3;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new global::System.EventHandler(this.buttonOK_Click);
			this.openFileDialogWAV.DefaultExt = "wav";
			this.openFileDialogWAV.Filter = "WAV files|*.wav|All files|*.*";
			this.openFileDialogWAV.FileOk += new global::System.ComponentModel.CancelEventHandler(this.openFileDialogWAV_FileOk);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(96f, 96f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Dpi;
			base.ClientSize = new global::System.Drawing.Size(532, 182);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.buttonErrorBrowse);
			base.Controls.Add(this.buttonWarningBrowse);
			base.Controls.Add(this.buttonSuccessBrowse);
			base.Controls.Add(this.textBoxErrorFile);
			base.Controls.Add(this.textBoxWarningFile);
			base.Controls.Add(this.textBoxSuccessFile);
			base.Controls.Add(this.checkBoxError);
			base.Controls.Add(this.checkBoxWarning);
			base.Controls.Add(this.checkBoxSuccess);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dialogSounds";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Alert Sounds";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000C7 RID: 199
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040000C8 RID: 200
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040000C9 RID: 201
		private global::System.Windows.Forms.Label label2;

		// Token: 0x040000CA RID: 202
		private global::System.Windows.Forms.Label label3;

		// Token: 0x040000CB RID: 203
		private global::System.Windows.Forms.CheckBox checkBoxSuccess;

		// Token: 0x040000CC RID: 204
		private global::System.Windows.Forms.CheckBox checkBoxWarning;

		// Token: 0x040000CD RID: 205
		private global::System.Windows.Forms.CheckBox checkBoxError;

		// Token: 0x040000CE RID: 206
		private global::System.Windows.Forms.TextBox textBoxSuccessFile;

		// Token: 0x040000CF RID: 207
		private global::System.Windows.Forms.TextBox textBoxWarningFile;

		// Token: 0x040000D0 RID: 208
		private global::System.Windows.Forms.TextBox textBoxErrorFile;

		// Token: 0x040000D1 RID: 209
		private global::System.Windows.Forms.Button buttonSuccessBrowse;

		// Token: 0x040000D2 RID: 210
		private global::System.Windows.Forms.Button buttonWarningBrowse;

		// Token: 0x040000D3 RID: 211
		private global::System.Windows.Forms.Button buttonErrorBrowse;

		// Token: 0x040000D4 RID: 212
		private global::System.Windows.Forms.Button buttonOK;

		// Token: 0x040000D5 RID: 213
		private global::System.Windows.Forms.OpenFileDialog openFileDialogWAV;
	}
}
