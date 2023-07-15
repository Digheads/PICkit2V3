namespace PICkit2V3
{
	// Token: 0x0200000A RID: 10
	public partial class FormMultiWinProgMem : global::System.Windows.Forms.Form
	{
		// Token: 0x0600006F RID: 111 RVA: 0x0000BE57 File Offset: 0x0000AE57
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000BE78 File Offset: 0x0000AE78
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::PICkit2V3.FormMultiWinProgMem));
			this.dataGridProgramMemory = new global::System.Windows.Forms.DataGridView();
			this.comboBoxProgMemView = new global::System.Windows.Forms.ComboBox();
			this.displayDataSource = new global::System.Windows.Forms.Label();
			this.labelDataSource = new global::System.Windows.Forms.Label();
			this.contextMenuStrip1 = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemContextSelectAll = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemContextCopy = new global::System.Windows.Forms.ToolStripMenuItem();
			((global::System.ComponentModel.ISupportInitialize)this.dataGridProgramMemory).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.dataGridProgramMemory.AllowUserToAddRows = false;
			this.dataGridProgramMemory.AllowUserToDeleteRows = false;
			this.dataGridProgramMemory.AllowUserToResizeColumns = false;
			this.dataGridProgramMemory.AllowUserToResizeRows = false;
			this.dataGridProgramMemory.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.dataGridProgramMemory.BackgroundColor = global::System.Drawing.SystemColors.Window;
			this.dataGridProgramMemory.CellBorderStyle = global::System.Windows.Forms.DataGridViewCellBorderStyle.None;
			dataGridViewCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = global::System.Drawing.SystemColors.Control;
			dataGridViewCellStyle.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle.ForeColor = global::System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridProgramMemory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dataGridProgramMemory.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridProgramMemory.ColumnHeadersVisible = false;
			this.dataGridProgramMemory.ContextMenuStrip = this.contextMenuStrip1;
			dataGridViewCellStyle2.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = global::System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.ForeColor = global::System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = global::System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridProgramMemory.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridProgramMemory.Enabled = false;
			this.dataGridProgramMemory.Location = new global::System.Drawing.Point(12, 39);
			this.dataGridProgramMemory.Name = "dataGridProgramMemory";
			dataGridViewCellStyle3.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = global::System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle3.ForeColor = global::System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridProgramMemory.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridProgramMemory.RowHeadersVisible = false;
			this.dataGridProgramMemory.RowHeadersWidth = 75;
			this.dataGridProgramMemory.RowHeadersWidthSizeMode = global::System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dataGridViewCellStyle4.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.dataGridProgramMemory.RowsDefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridProgramMemory.RowTemplate.Height = 17;
			this.dataGridProgramMemory.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.dataGridProgramMemory.SelectionMode = global::System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.dataGridProgramMemory.Size = new global::System.Drawing.Size(512, 123);
			this.dataGridProgramMemory.TabIndex = 5;
			this.dataGridProgramMemory.CellMouseDown += new global::System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridProgramMemory_CellMouseDown);
			this.dataGridProgramMemory.CellEndEdit += new global::System.Windows.Forms.DataGridViewCellEventHandler(this.progMemEdit);
			this.comboBoxProgMemView.BackColor = global::System.Drawing.SystemColors.Info;
			this.comboBoxProgMemView.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxProgMemView.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.comboBoxProgMemView.FormattingEnabled = true;
			this.comboBoxProgMemView.Items.AddRange(new object[]
			{
				"Hex Only",
				"Word ASCII",
				"Byte ASCII"
			});
			this.comboBoxProgMemView.Location = new global::System.Drawing.Point(12, 11);
			this.comboBoxProgMemView.Margin = new global::System.Windows.Forms.Padding(2);
			this.comboBoxProgMemView.Name = "comboBoxProgMemView";
			this.comboBoxProgMemView.Size = new global::System.Drawing.Size(91, 21);
			this.comboBoxProgMemView.TabIndex = 6;
			this.comboBoxProgMemView.SelectionChangeCommitted += new global::System.EventHandler(this.comboBoxProgMemView_SelectionChangeCommitted);
			this.displayDataSource.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.displayDataSource.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.displayDataSource.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.displayDataSource.Location = new global::System.Drawing.Point(172, 13);
			this.displayDataSource.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.displayDataSource.MinimumSize = new global::System.Drawing.Size(279, 16);
			this.displayDataSource.Name = "displayDataSource";
			this.displayDataSource.Size = new global::System.Drawing.Size(352, 16);
			this.displayDataSource.TabIndex = 8;
			this.displayDataSource.Text = "None (Empty/Erased)";
			this.displayDataSource.UseCompatibleTextRendering = true;
			this.labelDataSource.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.labelDataSource.AutoSize = true;
			this.labelDataSource.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelDataSource.Location = new global::System.Drawing.Point(119, 14);
			this.labelDataSource.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelDataSource.Name = "labelDataSource";
			this.labelDataSource.Size = new global::System.Drawing.Size(51, 13);
			this.labelDataSource.TabIndex = 7;
			this.labelDataSource.Text = "Source:";
			this.contextMenuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.toolStripMenuItemContextSelectAll,
				this.toolStripMenuItemContextCopy
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new global::System.Drawing.Size(164, 48);
			this.toolStripMenuItemContextSelectAll.Name = "toolStripMenuItemContextSelectAll";
			this.toolStripMenuItemContextSelectAll.ShortcutKeyDisplayString = "Ctrl-A";
			this.toolStripMenuItemContextSelectAll.Size = new global::System.Drawing.Size(163, 22);
			this.toolStripMenuItemContextSelectAll.Text = "Select All";
			this.toolStripMenuItemContextSelectAll.Click += new global::System.EventHandler(this.toolStripMenuItemContextSelectAll_Click);
			this.toolStripMenuItemContextCopy.Name = "toolStripMenuItemContextCopy";
			this.toolStripMenuItemContextCopy.ShortcutKeyDisplayString = "Ctrl-C";
			this.toolStripMenuItemContextCopy.Size = new global::System.Drawing.Size(163, 22);
			this.toolStripMenuItemContextCopy.Text = "Copy";
			this.toolStripMenuItemContextCopy.Click += new global::System.EventHandler(this.toolStripMenuItemContextCopy_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(96f, 96f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Dpi;
			base.ClientSize = new global::System.Drawing.Size(536, 174);
			base.Controls.Add(this.displayDataSource);
			base.Controls.Add(this.labelDataSource);
			base.Controls.Add(this.comboBoxProgMemView);
			base.Controls.Add(this.dataGridProgramMemory);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			this.MinimumSize = new global::System.Drawing.Size(200, 110);
			base.Name = "FormMultiWinProgMem";
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Show;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "PICkit 2 Program Memory";
			base.Resize += new global::System.EventHandler(this.FormMultiWinProgMem_Resize);
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.FormMultiWinProgMem_FormClosing);
			base.ResizeEnd += new global::System.EventHandler(this.FormMultiWinProgMem_ResizeEnd);
			((global::System.ComponentModel.ISupportInitialize)this.dataGridProgramMemory).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000AB RID: 171
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040000AC RID: 172
		private global::System.Windows.Forms.DataGridView dataGridProgramMemory;

		// Token: 0x040000AD RID: 173
		private global::System.Windows.Forms.ComboBox comboBoxProgMemView;

		// Token: 0x040000AE RID: 174
		private global::System.Windows.Forms.Label displayDataSource;

		// Token: 0x040000AF RID: 175
		private global::System.Windows.Forms.Label labelDataSource;

		// Token: 0x040000B0 RID: 176
		private global::System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

		// Token: 0x040000B1 RID: 177
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItemContextSelectAll;

		// Token: 0x040000B2 RID: 178
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItemContextCopy;
	}
}
