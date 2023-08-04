using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class SetOSCCAL : Form
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
			this.textBoxOSCCAL = new TextBox();
			this.label2 = new Label();
			this.buttonSet = new Button();
			this.buttonCancel = new Button();
			this.label3 = new Label();
			this.label4 = new Label();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new Point(10, 15);
			this.label1.Name = "label1";
			this.label1.Size = new Size(81, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "OSCCAL value:";
			this.textBoxOSCCAL.Location = new Point(93, 12);
			this.textBoxOSCCAL.Name = "textBoxOSCCAL";
			this.textBoxOSCCAL.Size = new Size(54, 20);
			this.textBoxOSCCAL.TabIndex = 1;
			this.label2.AutoSize = true;
			this.label2.Location = new Point(153, 15);
			this.label2.Name = "label2";
			this.label2.Size = new Size(30, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "(hex)";
			this.buttonSet.Location = new Point(12, 88);
			this.buttonSet.Name = "buttonSet";
			this.buttonSet.Size = new Size(75, 23);
			this.buttonSet.TabIndex = 3;
			this.buttonSet.Text = "Set";
			this.buttonSet.UseVisualStyleBackColor = true;
			this.buttonSet.Click += new EventHandler(this.ClickSet);
			this.buttonCancel.Location = new Point(105, 88);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(75, 23);
			this.buttonCancel.TabIndex = 4;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.ClickCancel);
			this.label3.AutoSize = true;
			this.label3.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label3.ForeColor = Color.Red;
			this.label3.Location = new Point(10, 37);
			this.label3.Name = "label3";
			this.label3.Size = new Size(71, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "WARNING:";
			this.label4.AutoSize = true;
			this.label4.ForeColor = Color.Red;
			this.label4.Location = new Point(10, 53);
			this.label4.Name = "label4";
			this.label4.Size = new Size(153, 26);
			this.label4.TabIndex = 6;
			this.label4.Text = "Setting OSCCAL will erase ALL\r\nmemory in part!";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(192, 123);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonSet);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.textBoxOSCCAL);
			base.Controls.Add(this.label1);
			base.Name = "SetOSCCAL";
			base.ShowIcon = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Set OSCCAL";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private IContainer components;
		private Label label1;
		private TextBox textBoxOSCCAL;
		private Label label2;
		private Button buttonSet;
		private Button buttonCancel;
		private Label label3;
		private Label label4;
	}
}
