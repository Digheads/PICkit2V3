namespace PICkit2V3
{
	// Token: 0x02000012 RID: 18
	public partial class FormTestMemory : global::System.Windows.Forms.Form
	{
		// Token: 0x0600013C RID: 316 RVA: 0x00033AF2 File Offset: 0x00032AF2
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00033B14 File Offset: 0x00032B14
		private void InitializeComponent()
		{
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new global::System.Windows.Forms.DataGridViewCellStyle();
			this.dataGridTestMemory = new global::System.Windows.Forms.DataGridView();
			this.label1 = new global::System.Windows.Forms.Label();
			this.textBoxTestMemSize = new global::System.Windows.Forms.TextBox();
			this.labelTestMemSize8 = new global::System.Windows.Forms.Label();
			this.label11 = new global::System.Windows.Forms.Label();
			this.checkBoxTestMemImportExport = new global::System.Windows.Forms.CheckBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.labelBLConfig = new global::System.Windows.Forms.Label();
			this.textBoxBaselineConfig = new global::System.Windows.Forms.TextBox();
			this.labelNotSupported = new global::System.Windows.Forms.Label();
			this.buttonClearTestMem = new global::System.Windows.Forms.Button();
			this.label4 = new global::System.Windows.Forms.Label();
			this.buttonWriteCalWords = new global::System.Windows.Forms.Button();
			this.labelCalWarning = new global::System.Windows.Forms.Label();
			((global::System.ComponentModel.ISupportInitialize)this.dataGridTestMemory).BeginInit();
			base.SuspendLayout();
			this.dataGridTestMemory.AllowUserToAddRows = false;
			this.dataGridTestMemory.AllowUserToDeleteRows = false;
			this.dataGridTestMemory.AllowUserToResizeColumns = false;
			this.dataGridTestMemory.AllowUserToResizeRows = false;
			this.dataGridTestMemory.BackgroundColor = global::System.Drawing.SystemColors.Window;
			this.dataGridTestMemory.CellBorderStyle = global::System.Windows.Forms.DataGridViewCellBorderStyle.None;
			dataGridViewCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = global::System.Drawing.SystemColors.Control;
			dataGridViewCellStyle.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle.ForeColor = global::System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridTestMemory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dataGridTestMemory.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridTestMemory.ColumnHeadersVisible = false;
			dataGridViewCellStyle2.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = global::System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.ForeColor = global::System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = global::System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridTestMemory.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridTestMemory.Location = new global::System.Drawing.Point(16, 52);
			this.dataGridTestMemory.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.dataGridTestMemory.MultiSelect = false;
			this.dataGridTestMemory.Name = "dataGridTestMemory";
			dataGridViewCellStyle3.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = global::System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle3.ForeColor = global::System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridTestMemory.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridTestMemory.RowHeadersVisible = false;
			this.dataGridTestMemory.RowHeadersWidth = 75;
			this.dataGridTestMemory.RowHeadersWidthSizeMode = global::System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dataGridViewCellStyle4.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.dataGridTestMemory.RowsDefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridTestMemory.RowTemplate.Height = 17;
			this.dataGridTestMemory.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.dataGridTestMemory.SelectionMode = global::System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.dataGridTestMemory.Size = new global::System.Drawing.Size(467, 171);
			this.dataGridTestMemory.TabIndex = 5;
			this.dataGridTestMemory.CellEndEdit += new global::System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridTestMemory_CellEndEdit);
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(12, 23);
			this.label1.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(139, 17);
			this.label1.TabIndex = 6;
			this.label1.Text = "Test Memory Words:";
			this.textBoxTestMemSize.Location = new global::System.Drawing.Point(160, 20);
			this.textBoxTestMemSize.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.textBoxTestMemSize.Name = "textBoxTestMemSize";
			this.textBoxTestMemSize.Size = new global::System.Drawing.Size(64, 22);
			this.textBoxTestMemSize.TabIndex = 7;
			this.textBoxTestMemSize.Leave += new global::System.EventHandler(this.textBoxTestMemSize_Leave);
			this.textBoxTestMemSize.KeyPress += new global::System.Windows.Forms.KeyPressEventHandler(this.textBoxTestMemSize_KeyPress);
			this.labelTestMemSize8.AutoSize = true;
			this.labelTestMemSize8.ForeColor = global::System.Drawing.Color.Red;
			this.labelTestMemSize8.Location = new global::System.Drawing.Point(233, 16);
			this.labelTestMemSize8.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelTestMemSize8.Name = "labelTestMemSize8";
			this.labelTestMemSize8.Size = new global::System.Drawing.Size(92, 34);
			this.labelTestMemSize8.TabIndex = 22;
			this.labelTestMemSize8.Text = "Must be\r\nmultiple of 16";
			this.labelTestMemSize8.Visible = false;
			this.label11.AutoSize = true;
			this.label11.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Underline, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label11.Location = new global::System.Drawing.Point(16, 226);
			this.label11.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label11.Name = "label11";
			this.label11.Size = new global::System.Drawing.Size(120, 17);
			this.label11.TabIndex = 23;
			this.label11.Text = "Hex Import-Export";
			this.checkBoxTestMemImportExport.AutoSize = true;
			this.checkBoxTestMemImportExport.Location = new global::System.Drawing.Point(16, 246);
			this.checkBoxTestMemImportExport.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.checkBoxTestMemImportExport.Name = "checkBoxTestMemImportExport";
			this.checkBoxTestMemImportExport.Size = new global::System.Drawing.Size(161, 21);
			this.checkBoxTestMemImportExport.TabIndex = 24;
			this.checkBoxTestMemImportExport.Text = "Include Test Memory";
			this.checkBoxTestMemImportExport.UseVisualStyleBackColor = true;
			this.checkBoxTestMemImportExport.CheckedChanged += new global::System.EventHandler(this.checkBoxTestMemImportExport_CheckedChanged);
			this.label2.BackColor = global::System.Drawing.Color.LightSteelBlue;
			this.label2.Location = new global::System.Drawing.Point(357, 7);
			this.label2.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(60, 16);
			this.label2.TabIndex = 25;
			this.label2.Text = "UserIDs";
			this.label3.BackColor = global::System.Drawing.Color.LightSalmon;
			this.label3.Location = new global::System.Drawing.Point(423, 7);
			this.label3.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(60, 16);
			this.label3.TabIndex = 26;
			this.label3.Text = "Config";
			this.labelBLConfig.AutoSize = true;
			this.labelBLConfig.Location = new global::System.Drawing.Point(307, 246);
			this.labelBLConfig.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelBLConfig.Name = "labelBLConfig";
			this.labelBLConfig.Size = new global::System.Drawing.Size(110, 17);
			this.labelBLConfig.TabIndex = 27;
			this.labelBLConfig.Text = "Baseline Config:";
			this.textBoxBaselineConfig.BackColor = global::System.Drawing.Color.LightSalmon;
			this.textBoxBaselineConfig.Location = new global::System.Drawing.Point(417, 242);
			this.textBoxBaselineConfig.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.textBoxBaselineConfig.Name = "textBoxBaselineConfig";
			this.textBoxBaselineConfig.Size = new global::System.Drawing.Size(64, 22);
			this.textBoxBaselineConfig.TabIndex = 28;
			this.textBoxBaselineConfig.Text = "0000";
			this.textBoxBaselineConfig.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.textBoxBaselineConfig.Leave += new global::System.EventHandler(this.textBoxBaselineConfig_Leave);
			this.textBoxBaselineConfig.KeyPress += new global::System.Windows.Forms.KeyPressEventHandler(this.textBoxBaselineConfig_KeyPress);
			this.labelNotSupported.AutoSize = true;
			this.labelNotSupported.BackColor = global::System.Drawing.SystemColors.Info;
			this.labelNotSupported.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 15.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelNotSupported.ForeColor = global::System.Drawing.Color.Red;
			this.labelNotSupported.Location = new global::System.Drawing.Point(37, 100);
			this.labelNotSupported.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelNotSupported.Name = "labelNotSupported";
			this.labelNotSupported.Size = new global::System.Drawing.Size(379, 62);
			this.labelNotSupported.TabIndex = 29;
			this.labelNotSupported.Text = "Test Memory Not Supported\r\n            on this family";
			this.labelNotSupported.Visible = false;
			this.buttonClearTestMem.Location = new global::System.Drawing.Point(208, 239);
			this.buttonClearTestMem.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.buttonClearTestMem.Name = "buttonClearTestMem";
			this.buttonClearTestMem.Size = new global::System.Drawing.Size(67, 28);
			this.buttonClearTestMem.TabIndex = 30;
			this.buttonClearTestMem.Text = "Clear";
			this.buttonClearTestMem.UseVisualStyleBackColor = true;
			this.buttonClearTestMem.Click += new global::System.EventHandler(this.buttonClearTestMem_Click);
			this.label4.BackColor = global::System.Drawing.Color.Gold;
			this.label4.Location = new global::System.Drawing.Point(423, 28);
			this.label4.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(60, 16);
			this.label4.TabIndex = 31;
			this.label4.Text = "CalWrd";
			this.buttonWriteCalWords.Location = new global::System.Drawing.Point(335, 266);
			this.buttonWriteCalWords.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.buttonWriteCalWords.Name = "buttonWriteCalWords";
			this.buttonWriteCalWords.Size = new global::System.Drawing.Size(111, 28);
			this.buttonWriteCalWords.TabIndex = 32;
			this.buttonWriteCalWords.Text = "Write CalWrd";
			this.buttonWriteCalWords.UseVisualStyleBackColor = true;
			this.buttonWriteCalWords.Click += new global::System.EventHandler(this.buttonWriteCalWords_Click);
			this.labelCalWarning.AutoSize = true;
			this.labelCalWarning.ForeColor = global::System.Drawing.Color.Red;
			this.labelCalWarning.Location = new global::System.Drawing.Point(323, 226);
			this.labelCalWarning.Margin = new global::System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelCalWarning.Name = "labelCalWarning";
			this.labelCalWarning.Size = new global::System.Drawing.Size(136, 34);
			this.labelCalWarning.TabIndex = 33;
			this.labelCalWarning.Text = "Writing Cal Words\r\nwill erase ALL Flash!";
			this.labelCalWarning.TextAlign = global::System.Drawing.ContentAlignment.TopCenter;
			this.labelCalWarning.Visible = false;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(120f, 120f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Dpi;
			base.ClientSize = new global::System.Drawing.Size(500, 300);
			base.Controls.Add(this.labelCalWarning);
			base.Controls.Add(this.buttonWriteCalWords);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.buttonClearTestMem);
			base.Controls.Add(this.labelNotSupported);
			base.Controls.Add(this.textBoxBaselineConfig);
			base.Controls.Add(this.labelBLConfig);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.checkBoxTestMemImportExport);
			base.Controls.Add(this.label11);
			base.Controls.Add(this.labelTestMemSize8);
			base.Controls.Add(this.textBoxTestMemSize);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.dataGridTestMemory);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormTestMemory";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Test memory";
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.FormTestMemory_FormClosing);
			((global::System.ComponentModel.ISupportInitialize)this.dataGridTestMemory).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000307 RID: 775
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000308 RID: 776
		private global::System.Windows.Forms.DataGridView dataGridTestMemory;

		// Token: 0x04000309 RID: 777
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400030A RID: 778
		private global::System.Windows.Forms.TextBox textBoxTestMemSize;

		// Token: 0x0400030B RID: 779
		private global::System.Windows.Forms.Label labelTestMemSize8;

		// Token: 0x0400030C RID: 780
		private global::System.Windows.Forms.Label label11;

		// Token: 0x0400030D RID: 781
		private global::System.Windows.Forms.CheckBox checkBoxTestMemImportExport;

		// Token: 0x0400030E RID: 782
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400030F RID: 783
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000310 RID: 784
		private global::System.Windows.Forms.Label labelBLConfig;

		// Token: 0x04000311 RID: 785
		private global::System.Windows.Forms.TextBox textBoxBaselineConfig;

		// Token: 0x04000312 RID: 786
		private global::System.Windows.Forms.Label labelNotSupported;

		// Token: 0x04000313 RID: 787
		private global::System.Windows.Forms.Button buttonClearTestMem;

		// Token: 0x04000314 RID: 788
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000315 RID: 789
		private global::System.Windows.Forms.Button buttonWriteCalWords;

		// Token: 0x04000316 RID: 790
		private global::System.Windows.Forms.Label labelCalWarning;
	}
}
