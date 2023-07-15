namespace PICkit2V3
{
	// Token: 0x02000025 RID: 37
	public partial class SetOSCCAL : global::System.Windows.Forms.Form
	{
		// Token: 0x060001FE RID: 510 RVA: 0x0003C29D File Offset: 0x0003B29D
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0003C2BC File Offset: 0x0003B2BC
		private void InitializeComponent()
		{
			this.label1 = new global::System.Windows.Forms.Label();
			this.textBoxOSCCAL = new global::System.Windows.Forms.TextBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.buttonSet = new global::System.Windows.Forms.Button();
			this.buttonCancel = new global::System.Windows.Forms.Button();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(10, 15);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(81, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "OSCCAL value:";
			this.textBoxOSCCAL.Location = new global::System.Drawing.Point(93, 12);
			this.textBoxOSCCAL.Name = "textBoxOSCCAL";
			this.textBoxOSCCAL.Size = new global::System.Drawing.Size(54, 20);
			this.textBoxOSCCAL.TabIndex = 1;
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(153, 15);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(30, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "(hex)";
			this.buttonSet.Location = new global::System.Drawing.Point(12, 88);
			this.buttonSet.Name = "buttonSet";
			this.buttonSet.Size = new global::System.Drawing.Size(75, 23);
			this.buttonSet.TabIndex = 3;
			this.buttonSet.Text = "Set";
			this.buttonSet.UseVisualStyleBackColor = true;
			this.buttonSet.Click += new global::System.EventHandler(this.clickSet);
			this.buttonCancel.Location = new global::System.Drawing.Point(105, 88);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new global::System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 4;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new global::System.EventHandler(this.clickCancel);
			this.label3.AutoSize = true;
			this.label3.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label3.ForeColor = global::System.Drawing.Color.Red;
			this.label3.Location = new global::System.Drawing.Point(10, 37);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(71, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "WARNING:";
			this.label4.AutoSize = true;
			this.label4.ForeColor = global::System.Drawing.Color.Red;
			this.label4.Location = new global::System.Drawing.Point(10, 53);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(153, 26);
			this.label4.TabIndex = 6;
			this.label4.Text = "Setting OSCCAL will erase ALL\r\nmemory in part!";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(192, 123);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonSet);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.textBoxOSCCAL);
			base.Controls.Add(this.label1);
			base.Name = "SetOSCCAL";
			base.ShowIcon = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Set OSCCAL";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400033A RID: 826
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400033B RID: 827
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400033C RID: 828
		private global::System.Windows.Forms.TextBox textBoxOSCCAL;

		// Token: 0x0400033D RID: 829
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400033E RID: 830
		private global::System.Windows.Forms.Button buttonSet;

		// Token: 0x0400033F RID: 831
		private global::System.Windows.Forms.Button buttonCancel;

		// Token: 0x04000340 RID: 832
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000341 RID: 833
		private global::System.Windows.Forms.Label label4;
	}
}
