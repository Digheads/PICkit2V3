using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class DialogCustomBaud : Form
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
			this.textBox1 = new TextBox();
			this.buttonOK = new Button();
			this.buttonCancel = new Button();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new Point(13, 9);
			this.label1.Name = "label1";
			this.label1.Size = new Size(142, 65);
			this.label1.TabIndex = 0;
			this.label1.Text = "Enter baud rate in box below\r\nand click \"OK\".\r\n\r\nMinimum = 150 baud\r\nMaximum = 38400 baud";
			this.textBox1.Location = new Point(16, 82);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new Size(106, 20);
			this.textBox1.TabIndex = 1;
			this.textBox1.TextChanged += new EventHandler(this.TextBox1_TextChanged);
			this.buttonOK.Location = new Point(13, 114);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(80, 22);
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.ButtonOK_Click);
			this.buttonCancel.Location = new Point(99, 114);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(80, 22);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.ButtonCancel_Click);
			base.AutoScaleDimensions = new SizeF(96f, 96f);
			base.AutoScaleMode = AutoScaleMode.Dpi;
			base.ClientSize = new Size(188, 148);
			base.ControlBox = false;
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DialogCustomBaud";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Custom Baud Rate";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private IContainer components;
		private Label label1;
		private TextBox textBox1;
		private Button buttonOK;
		private Button buttonCancel;
	}
}
