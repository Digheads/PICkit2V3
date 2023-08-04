using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class DialogUserIDs : Form
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
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			this.dataGridViewIDMem = new DataGridView();
			this.buttonClose = new Button();
			((ISupportInitialize)this.dataGridViewIDMem).BeginInit();
			base.SuspendLayout();
			this.dataGridViewIDMem.AllowUserToAddRows = false;
			this.dataGridViewIDMem.AllowUserToDeleteRows = false;
			this.dataGridViewIDMem.AllowUserToResizeColumns = false;
			this.dataGridViewIDMem.AllowUserToResizeRows = false;
			this.dataGridViewIDMem.BackgroundColor = SystemColors.Window;
			this.dataGridViewIDMem.CellBorderStyle = DataGridViewCellBorderStyle.None;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = SystemColors.Control;
			dataGridViewCellStyle.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			dataGridViewCellStyle.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dataGridViewIDMem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dataGridViewIDMem.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridViewIDMem.ColumnHeadersVisible = false;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = SystemColors.Window;
			dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
			this.dataGridViewIDMem.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridViewIDMem.GridColor = SystemColors.Window;
			this.dataGridViewIDMem.Location = new Point(16, 15);
			this.dataGridViewIDMem.Margin = new Padding(4, 4, 4, 4);
			this.dataGridViewIDMem.MultiSelect = false;
			this.dataGridViewIDMem.Name = "dataGridViewIDMem";
			this.dataGridViewIDMem.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = SystemColors.Control;
			dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
			this.dataGridViewIDMem.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridViewIDMem.RowHeadersVisible = false;
			this.dataGridViewIDMem.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dataGridViewIDMem.RowsDefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridViewIDMem.RowTemplate.Height = 17;
			this.dataGridViewIDMem.ScrollBars = ScrollBars.Vertical;
			this.dataGridViewIDMem.SelectionMode = DataGridViewSelectionMode.CellSelect;
			this.dataGridViewIDMem.ShowCellToolTips = false;
			this.dataGridViewIDMem.Size = new Size(308, 170);
			this.dataGridViewIDMem.TabIndex = 0;
			this.buttonClose.Location = new Point(133, 192);
			this.buttonClose.Margin = new Padding(4, 4, 4, 4);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new Size(80, 28);
			this.buttonClose.TabIndex = 1;
			this.buttonClose.Text = "Close";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new EventHandler(this.ButtonClose_Click);
			base.AutoScaleDimensions = new SizeF(120f, 120f);
			base.AutoScaleMode = AutoScaleMode.Dpi;
			base.ClientSize = new Size(339, 233);
			base.Controls.Add(this.buttonClose);
			base.Controls.Add(this.dataGridViewIDMem);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.Margin = new Padding(4, 4, 4, 4);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DialogUserIDs";
			this.Text = "ID Memory";
			base.FormClosing += new FormClosingEventHandler(this.DialogUserIDs_FormClosing);
			((ISupportInitialize)this.dataGridViewIDMem).EndInit();
			base.ResumeLayout(false);
		}

		private IContainer components;
		private DataGridView dataGridViewIDMem;
		private Button buttonClose;
	}
}
