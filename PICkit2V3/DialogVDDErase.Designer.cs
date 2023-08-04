using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class DialogVDDErase : Form
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
			this.label1 = new Label();
			this.label2 = new Label();
			this.label3 = new Label();
			this.checkBoxDoNotShow = new CheckBox();
			this.buttonContinue = new Button();
			this.buttonCancel = new Button();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label1.Location = new Point(13, 9);
			this.label1.Name = "label1";
			this.label1.Size = new Size(58, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Warning:";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(13, 22);
			this.label2.Name = "label2";
			this.label2.Size = new Size(217, 26);
			this.label2.TabIndex = 1;
			this.label2.Text = "This device requires a minimum VDD of ?.?V\r\nfor Bulk Erase operations.";
			this.label3.AutoSize = true;
			this.label3.Location = new Point(13, 62);
			this.label3.Name = "label3";
			this.label3.Size = new Size(217, 26);
			this.label3.TabIndex = 2;
			this.label3.Text = "If you continue it may fail to erase or program\r\nproperly.";
			this.checkBoxDoNotShow.AutoSize = true;
			this.checkBoxDoNotShow.Location = new Point(13, 102);
			this.checkBoxDoNotShow.Name = "checkBoxDoNotShow";
			this.checkBoxDoNotShow.Size = new Size(209, 17);
			this.checkBoxDoNotShow.TabIndex = 3;
			this.checkBoxDoNotShow.Text = "In the future, do not show this warning.";
			this.checkBoxDoNotShow.UseVisualStyleBackColor = true;
			this.buttonContinue.Location = new Point(78, 126);
			this.buttonContinue.Name = "buttonContinue";
			this.buttonContinue.Size = new Size(80, 22);
			this.buttonContinue.TabIndex = 4;
			this.buttonContinue.Text = "Continue";
			this.buttonContinue.UseVisualStyleBackColor = true;
			this.buttonContinue.Click += new EventHandler(this.ContinueClick);
			this.buttonCancel.Location = new Point(164, 126);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(80, 22);
			this.buttonCancel.TabIndex = 5;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.CancelClick);
			base.AutoScaleDimensions = new SizeF(96f, 96f);
			base.AutoScaleMode = AutoScaleMode.Dpi;
			base.ClientSize = new Size(254, 161);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonContinue);
			base.Controls.Add(this.checkBoxDoNotShow);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DialogVDDErase";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Warning!";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private IContainer components;
		private Label label1;
		private Label label2;
		private Label label3;
		private CheckBox checkBoxDoNotShow;
		private Button buttonContinue;
		private Button buttonCancel;
	}
}
