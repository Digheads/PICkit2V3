namespace PICkit2V3
{
	// Token: 0x0200003E RID: 62
	public partial class DialogTrigger : global::System.Windows.Forms.Form
	{
		// Token: 0x06000250 RID: 592 RVA: 0x000454A4 File Offset: 0x000444A4
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000454C4 File Offset: 0x000444C4
		private void InitializeComponent()
		{
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.pictureBox1 = new global::System.Windows.Forms.PictureBox();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new global::System.Drawing.Point(12, 42);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(194, 32);
			this.label1.TabIndex = 0;
			this.label1.Text = "PICkit 2 Logic Tool is waiting for\r\na valid trigger condition.";
			this.label2.AutoSize = true;
			this.label2.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label2.Location = new global::System.Drawing.Point(12, 138);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(190, 48);
			this.label2.TabIndex = 1;
			this.label2.Text = "Do not disconnect the PICkit 2 -\r\ndoing so may cause this\r\napplication to hang.";
			this.pictureBox1.BackColor = global::System.Drawing.Color.Red;
			this.pictureBox1.Location = new global::System.Drawing.Point(15, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new global::System.Drawing.Size(16, 18);
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			this.label3.AutoSize = true;
			this.label3.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label3.Location = new global::System.Drawing.Point(37, 14);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(137, 16);
			this.label3.TabIndex = 3;
			this.label3.Text = "Waiting for Trigger";
			this.label4.AutoSize = true;
			this.label4.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label4.Location = new global::System.Drawing.Point(12, 91);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(195, 32);
			this.label4.TabIndex = 4;
			this.label4.Text = "To cancel (abort) press the\r\nPICkit 2 pushbutton.";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(96f, 96f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Dpi;
			base.ClientSize = new global::System.Drawing.Size(237, 196);
			base.ControlBox = false;
			base.Controls.Add(this.label4);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.pictureBox1);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "DialogTrigger";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PICkit 2 Logic Tool Running";
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000554 RID: 1364
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000555 RID: 1365
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000556 RID: 1366
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000557 RID: 1367
		private global::System.Windows.Forms.PictureBox pictureBox1;

		// Token: 0x04000558 RID: 1368
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000559 RID: 1369
		private global::System.Windows.Forms.Label label4;
	}
}
