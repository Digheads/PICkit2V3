﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class FormMultiWinEEData : Form
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
			this.components = new Container();
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(PICkit2V3.FormMultiWinEEData));
			this.comboBoxProgMemView = new ComboBox();
			this.dataGridProgramMemory = new DataGridView();
			this.displayEEProgInfo = new Label();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.toolStripMenuItemContextSelectAll = new ToolStripMenuItem();
			this.toolStripMenuItemContextCopy = new ToolStripMenuItem();
			((ISupportInitialize)this.dataGridProgramMemory).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.comboBoxProgMemView.BackColor = SystemColors.Info;
			this.comboBoxProgMemView.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxProgMemView.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.comboBoxProgMemView.FormattingEnabled = true;
			this.comboBoxProgMemView.Items.AddRange(new object[]
			{
				"Hex Only",
				"Word ASCII",
				"Byte ASCII"
			});
			this.comboBoxProgMemView.Location = new Point(12, 11);
			this.comboBoxProgMemView.Margin = new Padding(2);
			this.comboBoxProgMemView.Name = "comboBoxProgMemView";
			this.comboBoxProgMemView.Size = new Size(91, 21);
			this.comboBoxProgMemView.TabIndex = 8;
			this.comboBoxProgMemView.SelectionChangeCommitted += new EventHandler(this.ComboBoxProgMemView_SelectionChangeCommitted);
			this.dataGridProgramMemory.AllowUserToAddRows = false;
			this.dataGridProgramMemory.AllowUserToDeleteRows = false;
			this.dataGridProgramMemory.AllowUserToResizeColumns = false;
			this.dataGridProgramMemory.AllowUserToResizeRows = false;
			this.dataGridProgramMemory.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.dataGridProgramMemory.BackgroundColor = SystemColors.Window;
			this.dataGridProgramMemory.CellBorderStyle = DataGridViewCellBorderStyle.None;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = SystemColors.Control;
			dataGridViewCellStyle.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			dataGridViewCellStyle.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dataGridProgramMemory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dataGridProgramMemory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridProgramMemory.ColumnHeadersVisible = false;
			this.dataGridProgramMemory.ContextMenuStrip = this.contextMenuStrip1;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = SystemColors.Window;
			dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
			this.dataGridProgramMemory.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridProgramMemory.Enabled = false;
			this.dataGridProgramMemory.Location = new Point(12, 39);
			this.dataGridProgramMemory.Name = "dataGridProgramMemory";
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = SystemColors.Control;
			dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
			this.dataGridProgramMemory.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridProgramMemory.RowHeadersVisible = false;
			this.dataGridProgramMemory.RowHeadersWidth = 75;
			this.dataGridProgramMemory.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dataGridProgramMemory.RowsDefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridProgramMemory.RowTemplate.Height = 17;
			this.dataGridProgramMemory.ScrollBars = ScrollBars.Vertical;
			this.dataGridProgramMemory.SelectionMode = DataGridViewSelectionMode.CellSelect;
			this.dataGridProgramMemory.Size = new Size(510, 49);
			this.dataGridProgramMemory.TabIndex = 7;
			this.dataGridProgramMemory.CellMouseDown += new DataGridViewCellMouseEventHandler(this.DataGridProgramMemory_CellMouseDown);
			this.dataGridProgramMemory.CellEndEdit += new DataGridViewCellEventHandler(this.ProgMemEdit);
			this.displayEEProgInfo.AutoSize = true;
			this.displayEEProgInfo.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.displayEEProgInfo.ForeColor = Color.Red;
			this.displayEEProgInfo.Location = new Point(107, 14);
			this.displayEEProgInfo.Margin = new Padding(2, 0, 2, 0);
			this.displayEEProgInfo.Name = "displayEEProgInfo";
			this.displayEEProgInfo.Size = new Size(206, 13);
			this.displayEEProgInfo.TabIndex = 9;
			this.displayEEProgInfo.Text = "Preserve EEPROM and User IDs on write.";
			this.displayEEProgInfo.Visible = false;
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripMenuItemContextSelectAll,
				this.toolStripMenuItemContextCopy
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new Size(164, 48);
			this.toolStripMenuItemContextSelectAll.Name = "toolStripMenuItemContextSelectAll";
			this.toolStripMenuItemContextSelectAll.ShortcutKeyDisplayString = "Ctrl-A";
			this.toolStripMenuItemContextSelectAll.Size = new Size(163, 22);
			this.toolStripMenuItemContextSelectAll.Text = "Select All";
			this.toolStripMenuItemContextSelectAll.Click += new EventHandler(this.ToolStripMenuItemContextSelectAll_Click);
			this.toolStripMenuItemContextCopy.Name = "toolStripMenuItemContextCopy";
			this.toolStripMenuItemContextCopy.ShortcutKeyDisplayString = "Ctrl-C";
			this.toolStripMenuItemContextCopy.Size = new Size(163, 22);
			this.toolStripMenuItemContextCopy.Text = "Copy";
			this.toolStripMenuItemContextCopy.Click += new EventHandler(this.ToolStripMenuItemContextCopy_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(536, 100);
			base.Controls.Add(this.displayEEProgInfo);
			base.Controls.Add(this.comboBoxProgMemView);
			base.Controls.Add(this.dataGridProgramMemory);
			base.Icon = (Icon)resources.GetObject("$this.Icon");
			this.MinimumSize = new Size(200, 110);
			base.Name = "FormMultiWinEEData";
			base.SizeGripStyle = SizeGripStyle.Show;
			base.StartPosition = FormStartPosition.Manual;
			this.Text = "PICkit 2 EEPROM Data";
			base.Resize += new EventHandler(this.FormMultiWinEEData_Resize);
			base.FormClosing += new FormClosingEventHandler(this.FormMultiWinEEData_FormClosing);
			base.ResizeEnd += new EventHandler(this.FormMultiWinEEData_ResizeEnd);
			((ISupportInitialize)this.dataGridProgramMemory).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private IContainer components;
		private ComboBox comboBoxProgMemView;
		private DataGridView dataGridProgramMemory;
		private Label displayEEProgInfo;
		private ContextMenuStrip contextMenuStrip1;
		private ToolStripMenuItem toolStripMenuItemContextSelectAll;
		private ToolStripMenuItem toolStripMenuItemContextCopy;
	}
}
