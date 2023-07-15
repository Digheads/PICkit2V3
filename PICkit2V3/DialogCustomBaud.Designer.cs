namespace PICkit2V3
{
	// Token: 0x0200000D RID: 13
	public partial class DialogCustomBaud : global::System.Windows.Forms.Form
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00010EE0 File Offset: 0x0000FEE0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00010F00 File Offset: 0x0000FF00
		private void InitializeComponent()
		{
			this.label1 = new global::System.Windows.Forms.Label();
			this.textBox1 = new global::System.Windows.Forms.TextBox();
			this.buttonOK = new global::System.Windows.Forms.Button();
			this.buttonCancel = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(13, 9);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(142, 65);
			this.label1.TabIndex = 0;
			this.label1.Text = "Enter baud rate in box below\r\nand click \"OK\".\r\n\r\nMinimum = 150 baud\r\nMaximum = 38400 baud";
			this.textBox1.Location = new global::System.Drawing.Point(16, 82);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new global::System.Drawing.Size(106, 20);
			this.textBox1.TabIndex = 1;
			this.textBox1.TextChanged += new global::System.EventHandler(this.textBox1_TextChanged);
			this.buttonOK.Location = new global::System.Drawing.Point(13, 114);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new global::System.Drawing.Size(80, 22);
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new global::System.EventHandler(this.buttonOK_Click);
			this.buttonCancel.Location = new global::System.Drawing.Point(99, 114);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new global::System.Drawing.Size(80, 22);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new global::System.EventHandler(this.buttonCancel_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(96f, 96f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Dpi;
			base.ClientSize = new global::System.Drawing.Size(188, 148);
			base.ControlBox = false;
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DialogCustomBaud";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Custom Baud Rate";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000D8 RID: 216
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040000D9 RID: 217
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040000DA RID: 218
		private global::System.Windows.Forms.TextBox textBox1;

		// Token: 0x040000DB RID: 219
		private global::System.Windows.Forms.Button buttonOK;

		// Token: 0x040000DC RID: 220
		private global::System.Windows.Forms.Button buttonCancel;
	}
}
