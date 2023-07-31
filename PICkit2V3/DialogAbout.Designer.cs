using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class DialogAbout : Form
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(PICkit2V3.DialogAbout));
			this.label1 = new Label();
			this.label2 = new Label();
			this.label3 = new Label();
			this.label4 = new Label();
			this.label5 = new Label();
			this.displayAppVer = new Label();
			this.displayDevFileVer = new Label();
			this.displayPk2FWVer = new Label();
			this.textBox1 = new TextBox();
			this.linkLabel1 = new LinkLabel();
			this.buttonOK = new Button();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Font = new Font("Arial", 14.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label1.Location = new Point(13, 9);
			this.label1.Name = "label1";
			this.label1.Size = new Size(64, 22);
			this.label1.TabIndex = 0;
			this.label1.Text = "PICkit";
			this.label2.AutoSize = true;
			this.label2.Font = new Font("Lucida Handwriting", 18f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label2.ForeColor = Color.Red;
			this.label2.Location = new Point(74, 4);
			this.label2.Name = "label2";
			this.label2.Size = new Size(31, 31);
			this.label2.TabIndex = 1;
			this.label2.Text = "2";
			this.label3.AutoSize = true;
			this.label3.Location = new Point(226, 9);
			this.label3.Name = "label3";
			this.label3.Size = new Size(97, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Application Version";
			this.label4.AutoSize = true;
			this.label4.Location = new Point(226, 22);
			this.label4.Name = "label4";
			this.label4.Size = new Size(98, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Device File Version";
			this.label5.AutoSize = true;
			this.label5.Location = new Point(226, 34);
			this.label5.Name = "label5";
			this.label5.Size = new Size(105, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "OS Firmware Version";
			this.displayAppVer.AutoSize = true;
			this.displayAppVer.Location = new Point(364, 9);
			this.displayAppVer.Name = "displayAppVer";
			this.displayAppVer.Size = new Size(43, 13);
			this.displayAppVer.TabIndex = 5;
			this.displayAppVer.Text = "2.00.00";
			this.displayAppVer.TextAlign = ContentAlignment.TopRight;
			this.displayDevFileVer.AutoSize = true;
			this.displayDevFileVer.Location = new Point(364, 22);
			this.displayDevFileVer.Name = "displayDevFileVer";
			this.displayDevFileVer.Size = new Size(49, 13);
			this.displayDevFileVer.TabIndex = 6;
			this.displayDevFileVer.Text = "00.00.00";
			this.displayDevFileVer.TextAlign = ContentAlignment.TopRight;
			this.displayPk2FWVer.AutoSize = true;
			this.displayPk2FWVer.Location = new Point(364, 34);
			this.displayPk2FWVer.Name = "displayPk2FWVer";
			this.displayPk2FWVer.Size = new Size(49, 13);
			this.displayPk2FWVer.TabIndex = 7;
			this.displayPk2FWVer.Text = "00.00.00";
			this.displayPk2FWVer.TextAlign = ContentAlignment.TopRight;
			this.textBox1.BackColor = SystemColors.Window;
			this.textBox1.Location = new Point(13, 62);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new Size(403, 216);
			this.textBox1.TabIndex = 8;
			this.textBox1.Text = resources.GetString("textBox1.Text");
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new Point(14, 34);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new Size(102, 13);
			this.linkLabel1.TabIndex = 9;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "www.microchip.com";
			this.linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(this.MicrochipLinkClicked);
			this.buttonOK.Location = new Point(178, 283);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(79, 22);
			this.buttonOK.TabIndex = 10;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.ClickOK);
			base.AutoScaleDimensions = new SizeF(96f, 96f);
			base.AutoScaleMode = AutoScaleMode.Dpi;
			base.ClientSize = new Size(429, 314);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.linkLabel1);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.displayPk2FWVer);
			base.Controls.Add(this.displayDevFileVer);
			base.Controls.Add(this.displayAppVer);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DialogAbout";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "About";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private IContainer components;
		private Label label1;
		private Label label2;
		private Label label3;
		private Label label4;
		private Label label5;
		private Label displayAppVer;
		private Label displayDevFileVer;
		private Label displayPk2FWVer;
		private TextBox textBox1;
		private LinkLabel linkLabel1;
		private Button buttonOK;
	}
}