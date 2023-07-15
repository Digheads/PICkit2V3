namespace PICkit2V3
{
	// Token: 0x02000032 RID: 50
	public partial class DialogVDDErase : global::System.Windows.Forms.Form
	{
		// Token: 0x0600021E RID: 542 RVA: 0x000404AF File Offset: 0x0003F4AF
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000404D0 File Offset: 0x0003F4D0
		private void InitializeComponent()
		{
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.checkBoxDoNotShow = new global::System.Windows.Forms.CheckBox();
			this.buttonContinue = new global::System.Windows.Forms.Button();
			this.buttonCancel = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new global::System.Drawing.Point(13, 9);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(58, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Warning:";
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(13, 22);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(217, 26);
			this.label2.TabIndex = 1;
			this.label2.Text = "This device requires a minimum VDD of ?.?V\r\nfor Bulk Erase operations.";
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(13, 62);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(217, 26);
			this.label3.TabIndex = 2;
			this.label3.Text = "If you continue it may fail to erase or program\r\nproperly.";
			this.checkBoxDoNotShow.AutoSize = true;
			this.checkBoxDoNotShow.Location = new global::System.Drawing.Point(13, 102);
			this.checkBoxDoNotShow.Name = "checkBoxDoNotShow";
			this.checkBoxDoNotShow.Size = new global::System.Drawing.Size(209, 17);
			this.checkBoxDoNotShow.TabIndex = 3;
			this.checkBoxDoNotShow.Text = "In the future, do not show this warning.";
			this.checkBoxDoNotShow.UseVisualStyleBackColor = true;
			this.buttonContinue.Location = new global::System.Drawing.Point(78, 126);
			this.buttonContinue.Name = "buttonContinue";
			this.buttonContinue.Size = new global::System.Drawing.Size(80, 22);
			this.buttonContinue.TabIndex = 4;
			this.buttonContinue.Text = "Continue";
			this.buttonContinue.UseVisualStyleBackColor = true;
			this.buttonContinue.Click += new global::System.EventHandler(this.continueClick);
			this.buttonCancel.Location = new global::System.Drawing.Point(164, 126);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new global::System.Drawing.Size(80, 22);
			this.buttonCancel.TabIndex = 5;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new global::System.EventHandler(this.cancelClick);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(96f, 96f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Dpi;
			base.ClientSize = new global::System.Drawing.Size(254, 161);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonContinue);
			base.Controls.Add(this.checkBoxDoNotShow);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DialogVDDErase";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Warning!";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000506 RID: 1286
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000507 RID: 1287
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000508 RID: 1288
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000509 RID: 1289
		private global::System.Windows.Forms.Label label3;

		// Token: 0x0400050A RID: 1290
		private global::System.Windows.Forms.CheckBox checkBoxDoNotShow;

		// Token: 0x0400050B RID: 1291
		private global::System.Windows.Forms.Button buttonContinue;

		// Token: 0x0400050C RID: 1292
		private global::System.Windows.Forms.Button buttonCancel;
	}
}
