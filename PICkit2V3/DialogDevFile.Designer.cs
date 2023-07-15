namespace PICkit2V3
{
	// Token: 0x02000033 RID: 51
	public partial class DialogDevFile : global::System.Windows.Forms.Form
	{
		// Token: 0x06000220 RID: 544 RVA: 0x000408AC File Offset: 0x0003F8AC
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000408CC File Offset: 0x0003F8CC
		private void InitializeComponent()
		{
			this.label1 = new global::System.Windows.Forms.Label();
			this.buttonLoadDevFile = new global::System.Windows.Forms.Button();
			this.listBoxDevFiles = new global::System.Windows.Forms.ListBox();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(16, 11);
			this.label1.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(183, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "Select a Device File to load:";
			this.buttonLoadDevFile.Location = new global::System.Drawing.Point(144, 284);
			this.buttonLoadDevFile.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.buttonLoadDevFile.Name = "buttonLoadDevFile";
			this.buttonLoadDevFile.Size = new global::System.Drawing.Size(100, 28);
			this.buttonLoadDevFile.TabIndex = 2;
			this.buttonLoadDevFile.Text = "Load";
			this.buttonLoadDevFile.UseVisualStyleBackColor = true;
			this.buttonLoadDevFile.Click += new global::System.EventHandler(this.buttonLoadDevFile_Click);
			this.listBoxDevFiles.FormattingEnabled = true;
			this.listBoxDevFiles.ItemHeight = 16;
			this.listBoxDevFiles.Location = new global::System.Drawing.Point(20, 31);
			this.listBoxDevFiles.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.listBoxDevFiles.Name = "listBoxDevFiles";
			this.listBoxDevFiles.Size = new global::System.Drawing.Size(352, 244);
			this.listBoxDevFiles.TabIndex = 3;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(120f, 120f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Dpi;
			base.ClientSize = new global::System.Drawing.Size(389, 327);
			base.Controls.Add(this.listBoxDevFiles);
			base.Controls.Add(this.buttonLoadDevFile);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DialogDevFile";
			this.Text = "DialogDevFile";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400050D RID: 1293
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400050E RID: 1294
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400050F RID: 1295
		private global::System.Windows.Forms.Button buttonLoadDevFile;

		// Token: 0x04000510 RID: 1296
		private global::System.Windows.Forms.ListBox listBoxDevFiles;
	}
}
