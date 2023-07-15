namespace PICkit2V3
{
	// Token: 0x02000005 RID: 5
	public partial class DialogUnitSelect : global::System.Windows.Forms.Form
	{
		// Token: 0x0600001A RID: 26 RVA: 0x0000370B File Offset: 0x0000270B
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000372C File Offset: 0x0000272C
		private void InitializeComponent()
		{
			this.label1 = new global::System.Windows.Forms.Label();
			this.buttonSelectUnit = new global::System.Windows.Forms.Button();
			this.listBoxUnits = new global::System.Windows.Forms.ListBox();
			this.label2 = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new global::System.Drawing.Point(13, 9);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(190, 48);
			this.label1.TabIndex = 0;
			this.label1.Text = "More than one PICkit 2 unit has\r\nbeen detected. \r\nPlease select a PICkit 2 to use:";
			this.buttonSelectUnit.Enabled = false;
			this.buttonSelectUnit.Location = new global::System.Drawing.Point(76, 166);
			this.buttonSelectUnit.Name = "buttonSelectUnit";
			this.buttonSelectUnit.Size = new global::System.Drawing.Size(80, 26);
			this.buttonSelectUnit.TabIndex = 2;
			this.buttonSelectUnit.Text = "Select";
			this.buttonSelectUnit.UseVisualStyleBackColor = true;
			this.buttonSelectUnit.Click += new global::System.EventHandler(this.buttonSelectUnit_Click);
			this.listBoxUnits.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.listBoxUnits.FormattingEnabled = true;
			this.listBoxUnits.ItemHeight = 15;
			this.listBoxUnits.Location = new global::System.Drawing.Point(16, 86);
			this.listBoxUnits.Name = "listBoxUnits";
			this.listBoxUnits.Size = new global::System.Drawing.Size(199, 64);
			this.listBoxUnits.TabIndex = 4;
			this.listBoxUnits.MouseDoubleClick += new global::System.Windows.Forms.MouseEventHandler(this.listBoxUnits_MouseDoubleClick);
			this.listBoxUnits.SelectedIndexChanged += new global::System.EventHandler(this.listBoxUnits_SelectedIndexChanged);
			this.label2.AutoSize = true;
			this.label2.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold | global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label2.Location = new global::System.Drawing.Point(13, 70);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(122, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Unit#            UnitID";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(96f, 96f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Dpi;
			base.ClientSize = new global::System.Drawing.Size(231, 211);
			base.ControlBox = false;
			base.Controls.Add(this.label2);
			base.Controls.Add(this.listBoxUnits);
			base.Controls.Add(this.buttonSelectUnit);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DialogUnitSelect";
			base.ShowIcon = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select PICkit 2 Unit";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000013 RID: 19
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000014 RID: 20
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000015 RID: 21
		private global::System.Windows.Forms.Button buttonSelectUnit;

		// Token: 0x04000016 RID: 22
		private global::System.Windows.Forms.ListBox listBoxUnits;

		// Token: 0x04000017 RID: 23
		private global::System.Windows.Forms.Label label2;
	}
}
