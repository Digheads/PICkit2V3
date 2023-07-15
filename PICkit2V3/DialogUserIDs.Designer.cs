namespace PICkit2V3
{
	// Token: 0x02000040 RID: 64
	public partial class DialogUserIDs : global::System.Windows.Forms.Form
	{
		// Token: 0x0600025B RID: 603 RVA: 0x00045A25 File Offset: 0x00044A25
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00045A44 File Offset: 0x00044A44
		private void InitializeComponent()
		{
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new global::System.Windows.Forms.DataGridViewCellStyle();
			this.dataGridViewIDMem = new global::System.Windows.Forms.DataGridView();
			this.buttonClose = new global::System.Windows.Forms.Button();
			((global::System.ComponentModel.ISupportInitialize)this.dataGridViewIDMem).BeginInit();
			base.SuspendLayout();
			this.dataGridViewIDMem.AllowUserToAddRows = false;
			this.dataGridViewIDMem.AllowUserToDeleteRows = false;
			this.dataGridViewIDMem.AllowUserToResizeColumns = false;
			this.dataGridViewIDMem.AllowUserToResizeRows = false;
			this.dataGridViewIDMem.BackgroundColor = global::System.Drawing.SystemColors.Window;
			this.dataGridViewIDMem.CellBorderStyle = global::System.Windows.Forms.DataGridViewCellBorderStyle.None;
			dataGridViewCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = global::System.Drawing.SystemColors.Control;
			dataGridViewCellStyle.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle.ForeColor = global::System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewIDMem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dataGridViewIDMem.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridViewIDMem.ColumnHeadersVisible = false;
			dataGridViewCellStyle2.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = global::System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.ForeColor = global::System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = global::System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewIDMem.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridViewIDMem.GridColor = global::System.Drawing.SystemColors.Window;
			this.dataGridViewIDMem.Location = new global::System.Drawing.Point(16, 15);
			this.dataGridViewIDMem.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.dataGridViewIDMem.MultiSelect = false;
			this.dataGridViewIDMem.Name = "dataGridViewIDMem";
			this.dataGridViewIDMem.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = global::System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle3.ForeColor = global::System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewIDMem.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridViewIDMem.RowHeadersVisible = false;
			this.dataGridViewIDMem.RowHeadersWidthSizeMode = global::System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dataGridViewCellStyle4.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.dataGridViewIDMem.RowsDefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridViewIDMem.RowTemplate.Height = 17;
			this.dataGridViewIDMem.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.dataGridViewIDMem.SelectionMode = global::System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.dataGridViewIDMem.ShowCellToolTips = false;
			this.dataGridViewIDMem.Size = new global::System.Drawing.Size(308, 170);
			this.dataGridViewIDMem.TabIndex = 0;
			this.buttonClose.Location = new global::System.Drawing.Point(133, 192);
			this.buttonClose.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new global::System.Drawing.Size(80, 28);
			this.buttonClose.TabIndex = 1;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new global::System.EventHandler(this.buttonClose_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(120f, 120f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Dpi;
			base.ClientSize = new global::System.Drawing.Size(339, 233);
			base.Controls.Add(this.buttonClose);
			base.Controls.Add(this.dataGridViewIDMem);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Margin = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DialogUserIDs";
			this.Text = "ID Memory";
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.DialogUserIDs_FormClosing);
			((global::System.ComponentModel.ISupportInitialize)this.dataGridViewIDMem).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x0400055D RID: 1373
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400055E RID: 1374
		private global::System.Windows.Forms.DataGridView dataGridViewIDMem;

		// Token: 0x0400055F RID: 1375
		private global::System.Windows.Forms.Button buttonClose;
	}
}
