using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class DialogSounds : Form
	{
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			ComponentResourceManager resources = new ComponentResourceManager(typeof(PICkit2V3.DialogSounds));
			this.label1 = new Label();
			this.label2 = new Label();
			this.label3 = new Label();
			this.checkBoxSuccess = new CheckBox();
			this.checkBoxWarning = new CheckBox();
			this.checkBoxError = new CheckBox();
			this.textBoxSuccessFile = new TextBox();
			this.textBoxWarningFile = new TextBox();
			this.textBoxErrorFile = new TextBox();
			this.buttonSuccessBrowse = new Button();
			this.buttonWarningBrowse = new Button();
			this.buttonErrorBrowse = new Button();
			this.buttonOK = new Button();
			this.openFileDialogWAV = new OpenFileDialog();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new Size(516, 39);
			this.label1.TabIndex = 0;
			this.label1.Text = resources.GetString("label1.Text");
			this.label2.AutoSize = true;
			this.label2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label2.Location = new Point(12, 61);
			this.label2.Name = "label2";
			this.label2.Size = new Size(50, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Enable:";
			this.label3.AutoSize = true;
			this.label3.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label3.Location = new Point(110, 61);
			this.label3.Name = "label3";
			this.label3.Size = new Size(112, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Play this WAV file:";
			this.checkBoxSuccess.BackColor = Color.LimeGreen;
			this.checkBoxSuccess.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.checkBoxSuccess.ForeColor = SystemColors.ControlText;
			this.checkBoxSuccess.Location = new Point(15, 80);
			this.checkBoxSuccess.Name = "checkBoxSuccess";
			this.checkBoxSuccess.Size = new Size(74, 17);
			this.checkBoxSuccess.TabIndex = 1;
			this.checkBoxSuccess.Text = "Success";
			this.checkBoxSuccess.UseVisualStyleBackColor = false;
			this.checkBoxSuccess.Click += new EventHandler(this.CheckBoxSuccess_CheckedChanged);
			this.checkBoxWarning.BackColor = Color.Yellow;
			this.checkBoxWarning.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.checkBoxWarning.ForeColor = SystemColors.ControlText;
			this.checkBoxWarning.Location = new Point(15, 103);
			this.checkBoxWarning.Name = "checkBoxWarning";
			this.checkBoxWarning.Size = new Size(74, 17);
			this.checkBoxWarning.TabIndex = 1;
			this.checkBoxWarning.Text = "Warning";
			this.checkBoxWarning.UseVisualStyleBackColor = false;
			this.checkBoxWarning.Click += new EventHandler(this.CheckBoxWarning_CheckedChanged);
			this.checkBoxError.BackColor = Color.Salmon;
			this.checkBoxError.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.checkBoxError.ForeColor = SystemColors.ControlText;
			this.checkBoxError.Location = new Point(15, 126);
			this.checkBoxError.Name = "checkBoxError";
			this.checkBoxError.Size = new Size(74, 17);
			this.checkBoxError.TabIndex = 1;
			this.checkBoxError.Text = "Error";
			this.checkBoxError.UseVisualStyleBackColor = false;
			this.checkBoxError.Click += new EventHandler(this.CheckBoxError_CheckedChanged);
			this.textBoxSuccessFile.Location = new Point(113, 78);
			this.textBoxSuccessFile.Name = "textBoxSuccessFile";
			this.textBoxSuccessFile.Size = new Size(341, 20);
			this.textBoxSuccessFile.TabIndex = 2;
			this.textBoxWarningFile.Location = new Point(113, 101);
			this.textBoxWarningFile.Name = "textBoxWarningFile";
			this.textBoxWarningFile.Size = new Size(341, 20);
			this.textBoxWarningFile.TabIndex = 2;
			this.textBoxErrorFile.Location = new Point(113, 124);
			this.textBoxErrorFile.Name = "textBoxErrorFile";
			this.textBoxErrorFile.Size = new Size(341, 20);
			this.textBoxErrorFile.TabIndex = 2;
			this.buttonSuccessBrowse.Location = new Point(460, 76);
			this.buttonSuccessBrowse.Name = "buttonSuccessBrowse";
			this.buttonSuccessBrowse.Size = new Size(60, 23);
			this.buttonSuccessBrowse.TabIndex = 3;
			this.buttonSuccessBrowse.Text = "Browse";
			this.buttonSuccessBrowse.UseVisualStyleBackColor = true;
			this.buttonSuccessBrowse.Click += new EventHandler(this.ButtonSuccessBrowse_Click);
			this.buttonWarningBrowse.Location = new Point(460, 99);
			this.buttonWarningBrowse.Name = "buttonWarningBrowse";
			this.buttonWarningBrowse.Size = new Size(60, 23);
			this.buttonWarningBrowse.TabIndex = 3;
			this.buttonWarningBrowse.Text = "Browse";
			this.buttonWarningBrowse.UseVisualStyleBackColor = true;
			this.buttonWarningBrowse.Click += new EventHandler(this.Button1_Click);
			this.buttonErrorBrowse.Location = new Point(460, 122);
			this.buttonErrorBrowse.Name = "buttonErrorBrowse";
			this.buttonErrorBrowse.Size = new Size(60, 23);
			this.buttonErrorBrowse.TabIndex = 3;
			this.buttonErrorBrowse.Text = "Browse";
			this.buttonErrorBrowse.UseVisualStyleBackColor = true;
			this.buttonErrorBrowse.Click += new EventHandler(this.ButtonErrorBrowse_Click);
			this.buttonOK.Location = new Point(437, 151);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(83, 23);
			this.buttonOK.TabIndex = 3;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.ButtonOK_Click);
			this.openFileDialogWAV.DefaultExt = "wav";
			this.openFileDialogWAV.Filter = "WAV files|*.wav|All files|*.*";
			this.openFileDialogWAV.FileOk += new CancelEventHandler(this.OpenFileDialogWAV_FileOk);
			base.AutoScaleDimensions = new SizeF(96f, 96f);
			base.AutoScaleMode = AutoScaleMode.Dpi;
			base.ClientSize = new Size(532, 182);
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
			base.FormBorderStyle = FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dialogSounds";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Alert Sounds";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private IContainer components;
		private Label label1;
		private Label label2;
		private Label label3;
		private CheckBox checkBoxSuccess;
		private CheckBox checkBoxWarning;
		private CheckBox checkBoxError;
		private TextBox textBoxSuccessFile;
		private TextBox textBoxWarningFile;
		private TextBox textBoxErrorFile;
		private Button buttonSuccessBrowse;
		private Button buttonWarningBrowse;
		private Button buttonErrorBrowse;
		private Button buttonOK;
		private OpenFileDialog openFileDialogWAV;
	}
}
