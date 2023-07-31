using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class DialogDevFile : Form
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
			this.buttonLoadDevFile = new Button();
			this.listBoxDevFiles = new ListBox();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new Point(16, 11);
			this.label1.Margin = new Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new Size(183, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "Select a Device File to load:";
			this.buttonLoadDevFile.Location = new Point(144, 284);
			this.buttonLoadDevFile.Margin = new Padding(4, 4, 4, 4);
			this.buttonLoadDevFile.Name = "buttonLoadDevFile";
			this.buttonLoadDevFile.Size = new Size(100, 28);
			this.buttonLoadDevFile.TabIndex = 2;
			this.buttonLoadDevFile.Text = "Load";
			this.buttonLoadDevFile.UseVisualStyleBackColor = true;
			this.buttonLoadDevFile.Click += new EventHandler(this.ButtonLoadDevFile_Click);
			this.listBoxDevFiles.FormattingEnabled = true;
			this.listBoxDevFiles.ItemHeight = 16;
			this.listBoxDevFiles.Location = new Point(20, 31);
			this.listBoxDevFiles.Margin = new Padding(4, 4, 4, 4);
			this.listBoxDevFiles.Name = "listBoxDevFiles";
			this.listBoxDevFiles.Size = new Size(352, 244);
			this.listBoxDevFiles.TabIndex = 3;
			base.AutoScaleDimensions = new SizeF(120f, 120f);
			base.AutoScaleMode = AutoScaleMode.Dpi;
			base.ClientSize = new Size(389, 327);
			base.Controls.Add(this.listBoxDevFiles);
			base.Controls.Add(this.buttonLoadDevFile);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = FormBorderStyle.Fixed3D;
			base.Margin = new Padding(4, 4, 4, 4);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DialogDevFile";
			this.Text = "DialogDevFile";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private IContainer components;
		private Label label1;
		private Button buttonLoadDevFile;
		private ListBox listBoxDevFiles;
	}
}
