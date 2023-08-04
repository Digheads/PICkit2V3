using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class DialogUnitSelect : Form
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
			this.label1 = new Label();
			this.buttonSelectUnit = new Button();
			this.listBoxUnits = new ListBox();
			this.label2 = new Label();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.label1.Location = new Point(13, 9);
			this.label1.Name = "label1";
			this.label1.Size = new Size(190, 48);
			this.label1.TabIndex = 0;
			this.label1.Text = "More than one PICkit 2 unit has\r\nbeen detected. \r\nPlease select a PICkit 2 to use:";
			this.buttonSelectUnit.Enabled = false;
			this.buttonSelectUnit.Location = new Point(76, 166);
			this.buttonSelectUnit.Name = "buttonSelectUnit";
			this.buttonSelectUnit.Size = new Size(80, 26);
			this.buttonSelectUnit.TabIndex = 2;
			this.buttonSelectUnit.Text = "Select";
			this.buttonSelectUnit.UseVisualStyleBackColor = true;
			this.buttonSelectUnit.Click += new EventHandler(this.ButtonSelectUnit_Click);
			this.listBoxUnits.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.listBoxUnits.FormattingEnabled = true;
			this.listBoxUnits.ItemHeight = 15;
			this.listBoxUnits.Location = new Point(16, 86);
			this.listBoxUnits.Name = "listBoxUnits";
			this.listBoxUnits.Size = new Size(199, 64);
			this.listBoxUnits.TabIndex = 4;
			this.listBoxUnits.MouseDoubleClick += new MouseEventHandler(this.ListBoxUnits_MouseDoubleClick);
			this.listBoxUnits.SelectedIndexChanged += new EventHandler(this.ListBoxUnits_SelectedIndexChanged);
			this.label2.AutoSize = true;
			this.label2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
			this.label2.Location = new Point(13, 70);
			this.label2.Name = "label2";
			this.label2.Size = new Size(122, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Unit#            UnitID";
			base.AutoScaleDimensions = new SizeF(96f, 96f);
			base.AutoScaleMode = AutoScaleMode.Dpi;
			base.ClientSize = new Size(231, 211);
			base.ControlBox = false;
			base.Controls.Add(this.label2);
			base.Controls.Add(this.listBoxUnits);
			base.Controls.Add(this.buttonSelectUnit);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DialogUnitSelect";
			base.ShowIcon = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Select PICkit 2 Unit";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private IContainer components;
		private Label label1;
		private Button buttonSelectUnit;
		private ListBox listBoxUnits;
		private Label label2;
	}
}
