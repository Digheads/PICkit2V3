using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	// Token: 0x0200000A RID: 10
	public partial class FormMultiWinProgMem : Form
	{
		// Token: 0x06000071 RID: 113 RVA: 0x0000C623 File Offset: 0x0000B623
		public FormMultiWinProgMem()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000C647 File Offset: 0x0000B647
		public void InitProgMemDisplay(int viewMode)
		{
			this.dataGridProgramMemory.DefaultCellStyle.Font = new Font("Courier New", 9f);
			this.comboBoxProgMemView.SelectedIndex = viewMode;
			this.InitDone = true;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000C67B File Offset: 0x0000B67B
		public int GetViewMode()
		{
			return this.comboBoxProgMemView.SelectedIndex;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000C688 File Offset: 0x0000B688
		public void DisplayDisable()
		{
			this.comboBoxProgMemView.Enabled = false;
			this.dataGridProgramMemory.Enabled = false;
			this.dataGridProgramMemory.ForeColor = SystemColors.GrayText;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000C6B2 File Offset: 0x0000B6B2
		public void DisplayEnable()
		{
			this.comboBoxProgMemView.Enabled = true;
			this.dataGridProgramMemory.Enabled = true;
			this.dataGridProgramMemory.ForeColor = SystemColors.WindowText;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000C6DC File Offset: 0x0000B6DC
		public void ReCalcMultiWinProgMem()
		{
			uint programMem = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem;
			if (programMem == 0U)
			{
				return;
			}
			if (base.WindowState == FormWindowState.Minimized)
			{
				return;
			}
			uint blankValue = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
			if (blankValue == 4294967295U)
			{
				this.comboBoxProgMemView.SelectedIndex = 0;
				this.comboBoxProgMemView.Enabled = false;
			}
			else
			{
				this.comboBoxProgMemView.Enabled = true;
			}
			uint num = programMem * (uint)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].AddressIncrement - 1U;
			int num2 = 32;
			this.addrFormat = "{0:X3}";
			if (blankValue == 4294967295U)
			{
				num2 = 65;
				this.addrFormat = "{0:X8}";
			}
			else if (num > 65535U)
			{
				num2 = 44;
				this.addrFormat = "{0:X5}";
			}
			else if (num > 4095U)
			{
				num2 = 38;
				this.addrFormat = "{0:X4}";
			}
			num2 = (int)((float)num2 * FormPICkit2.ScalefactW);
			int num3 = 24;
			int num4 = 16;
			this.asciiBytes = 1;
			this.dataFormat = "{0:X2}";
			if (blankValue == 4294967295U)
			{
				num3 = 65;
				num4 = 58;
				this.asciiBytes = 4;
				this.dataFormat = "{0:X8}";
			}
			else if (blankValue > 65535U)
			{
				num3 = 50;
				num4 = 43;
				this.asciiBytes = 3;
				this.dataFormat = "{0:X6}";
			}
			else if (blankValue > 4095U)
			{
				num3 = 36;
				num4 = 28;
				this.asciiBytes = 2;
				this.dataFormat = "{0:X4}";
			}
			else if (blankValue > 255U)
			{
				num3 = 28;
				num4 = 28;
				this.asciiBytes = 2;
				this.dataFormat = "{0:X3}";
			}
			float num5 = 1f;
			if (this.comboBoxProgMemView.SelectedIndex > 0)
			{
				num5 = (float)num4 / ((float)num4 + (float)num3);
				num3 += num4;
			}
			num3 = (int)((float)num3 * FormPICkit2.ScalefactW);
			int num6 = this.dataGridProgramMemory.Size.Width - num2 - (int)(20f * FormPICkit2.ScalefactW);
			int num7 = num6 / num3;
			for (int i = 1; i <= 256; i *= 2)
			{
				if (i > num7)
				{
					num7 = i / 2;
					break;
				}
			}
			if (num7 > (int)programMem)
			{
				num7 = (int)programMem;
			}
			num3 = num6 / num7;
			if (this.comboBoxProgMemView.SelectedIndex > 0)
			{
				num4 = (int)(num5 * (float)num3);
				num3 -= num4;
			}
			this.dataGridProgramMemory.Rows.Clear();
			if (this.comboBoxProgMemView.SelectedIndex > 0)
			{
				this.dataGridProgramMemory.ColumnCount = 2 * num7 + 1;
			}
			else
			{
				this.dataGridProgramMemory.ColumnCount = num7 + 1;
			}
			this.dataGridProgramMemory.Columns[0].Width = num2;
			for (int j = 1; j <= num7; j++)
			{
				this.dataGridProgramMemory.Columns[j].Width = num3;
			}
			if (this.comboBoxProgMemView.SelectedIndex > 0)
			{
				for (int k = num7 + 1; k <= 2 * num7; k++)
				{
					this.dataGridProgramMemory.Columns[k].Width = num4;
				}
			}
			int num8 = (int)(programMem / (uint)num7);
			if ((ulong)programMem % (ulong)((long)num7) > 0UL)
			{
				num8++;
			}
			if (blankValue == 4294967295U)
			{
				num8 += 2;
			}
			this.dataGridProgramMemory.RowCount = num8;
			int num9 = num7 * (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].AddressIncrement;
			if (blankValue == 4294967295U)
			{
				int num10 = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem;
				int bootFlash = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BootFlash;
				num10 -= bootFlash;
				num10 /= num7;
				this.dataGridProgramMemory.ShowCellToolTips = false;
				this.dataGridProgramMemory[0, 0].Value = "Program";
				this.dataGridProgramMemory[1, 0].Value = "Flash";
				for (int l = 0; l < this.dataGridProgramMemory.ColumnCount; l++)
				{
					this.dataGridProgramMemory[l, 0].Style.BackColor = SystemColors.ControlDark;
					this.dataGridProgramMemory[l, 0].ReadOnly = true;
				}
				int m = 1;
				int num11 = 486539264;
				while (m <= num10)
				{
					this.dataGridProgramMemory[0, m].Value = string.Format(this.addrFormat, num11);
					this.dataGridProgramMemory[0, m].Style.BackColor = SystemColors.ControlLight;
					num11 += num9;
					m++;
				}
				this.dataGridProgramMemory[0, num10 + 1].Value = "Boot";
				this.dataGridProgramMemory[1, num10 + 1].Value = "Flash";
				for (int n = 0; n < this.dataGridProgramMemory.ColumnCount; n++)
				{
					this.dataGridProgramMemory[n, num10 + 1].Style.BackColor = SystemColors.ControlDark;
					this.dataGridProgramMemory[n, num10 + 1].ReadOnly = true;
				}
				int num12 = num10 + 2;
				int num13 = 532676608;
				while (num12 < this.dataGridProgramMemory.RowCount)
				{
					this.dataGridProgramMemory[0, num12].Value = string.Format(this.addrFormat, num13);
					this.dataGridProgramMemory[0, num12].Style.BackColor = SystemColors.ControlLight;
					num13 += num9;
					num12++;
				}
			}
			else
			{
				this.dataGridProgramMemory.ShowCellToolTips = true;
				int num14 = 0;
				int num15 = 0;
				while (num14 < num8)
				{
					this.dataGridProgramMemory[0, num14].Value = string.Format(this.addrFormat, num15);
					this.dataGridProgramMemory[0, num14].ReadOnly = true;
					this.dataGridProgramMemory[0, num14].Style.BackColor = SystemColors.ControlLight;
					num15 += num9;
					num14++;
				}
			}
			this.updateDisplay();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000CCD4 File Offset: 0x0000BCD4
		public void UpdateMultiWinProgMem(string dataSource)
		{
			if (this.lastPart != PICkitFunctions.ActivePart || this.lastFam != PICkitFunctions.GetActiveFamily())
			{
				this.lastPart = PICkitFunctions.ActivePart;
				this.lastFam = PICkitFunctions.GetActiveFamily();
				this.ReCalcMultiWinProgMem();
			}
			else
			{
				this.updateDisplay();
			}
			this.displayDataSource.Text = dataSource;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000CD2B File Offset: 0x0000BD2B
		public string GetDataSource()
		{
			return this.displayDataSource.Text;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000CD38 File Offset: 0x0000BD38
		private void updateDisplay()
		{
			int num = this.dataGridProgramMemory.RowCount - 1;
			int num2 = this.dataGridProgramMemory.ColumnCount - 1;
			int addressIncrement = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].AddressIncrement;
			uint blankValue = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
			if (this.comboBoxProgMemView.SelectedIndex > 0)
			{
				num2 /= 2;
			}
			int num3 = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem;
			int bootFlash = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BootFlash;
			num3 -= bootFlash;
			num3 /= num2;
			int num4 = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem % (uint)num2);
			if (num4 == 0)
			{
				num4 = num2;
			}
			int num5 = num * num2;
			if (blankValue == 4294967295U)
			{
				int num6 = 0;
				for (int i = 1; i <= num3; i++)
				{
					for (int j = 0; j < num2; j++)
					{
						this.dataGridProgramMemory[j + 1, i].Value = string.Format(this.dataFormat, PICkitFunctions.DeviceBuffers.ProgramMemory[num6++]);
					}
				}
				for (int k = num3 + 2; k < this.dataGridProgramMemory.RowCount - 1; k++)
				{
					for (int l = 0; l < num2; l++)
					{
						this.dataGridProgramMemory[l + 1, k].Value = string.Format(this.dataFormat, PICkitFunctions.DeviceBuffers.ProgramMemory[num6++]);
					}
				}
				num4 = bootFlash % num2;
				if (num4 == 0)
				{
					num4 = num2;
				}
				for (int m = 1; m <= num2; m++)
				{
					if (m <= num4)
					{
						this.dataGridProgramMemory[m, num].Value = string.Format(this.dataFormat, PICkitFunctions.DeviceBuffers.ProgramMemory[num6++]);
					}
					else
					{
						this.dataGridProgramMemory[m, num].Value = "";
						this.dataGridProgramMemory[m, num].ReadOnly = true;
					}
				}
			}
			else
			{
				int n = 0;
				int num7 = 0;
				int num8 = 0;
				while (n < num)
				{
					for (int num9 = 1; num9 <= num2; num9++)
					{
						this.dataGridProgramMemory[num9, n].Value = string.Format(this.dataFormat, PICkitFunctions.DeviceBuffers.ProgramMemory[num7++]);
						this.dataGridProgramMemory[num9, n].ToolTipText = string.Format(this.addrFormat, num8);
						num8 += addressIncrement;
					}
					n++;
				}
				for (int num10 = 1; num10 <= num2; num10++)
				{
					if (num10 <= num4)
					{
						this.dataGridProgramMemory[num10, num].Value = string.Format(this.dataFormat, PICkitFunctions.DeviceBuffers.ProgramMemory[num5]);
						this.dataGridProgramMemory[num10, num].ToolTipText = string.Format(this.addrFormat, num5++ * addressIncrement);
					}
					else
					{
						this.dataGridProgramMemory[num10, num].Value = "";
						this.dataGridProgramMemory[num10, num].ToolTipText = "";
						this.dataGridProgramMemory[num10, num].ReadOnly = true;
					}
				}
			}
			if (this.comboBoxProgMemView.SelectedIndex > 0)
			{
				for (int num11 = num2 + 1; num11 <= 2 * num2; num11++)
				{
					this.dataGridProgramMemory.Columns[num11].ReadOnly = true;
				}
				if (this.comboBoxProgMemView.SelectedIndex == 1)
				{
					int num12 = 0;
					int num13 = 0;
					int num14 = 0;
					while (num12 < num)
					{
						for (int num15 = num2 + 1; num15 <= 2 * num2; num15++)
						{
							this.dataGridProgramMemory[num15, num12].Value = Utilities.ConvertIntASCII((int)PICkitFunctions.DeviceBuffers.ProgramMemory[num13++], this.asciiBytes);
							this.dataGridProgramMemory[num15, num12].ToolTipText = string.Format(this.addrFormat, num14);
							num14 += addressIncrement;
						}
						num12++;
					}
					num5 = num * num2;
					for (int num16 = num2 + 1; num16 <= 2 * num2; num16++)
					{
						if (num16 <= num2 + num4)
						{
							this.dataGridProgramMemory[num16, num].Value = Utilities.ConvertIntASCII((int)PICkitFunctions.DeviceBuffers.ProgramMemory[num5++], this.asciiBytes);
							this.dataGridProgramMemory[num16, num].ToolTipText = string.Format(this.addrFormat, num5 * addressIncrement);
						}
						else
						{
							this.dataGridProgramMemory[num16, num].Value = "";
							this.dataGridProgramMemory[num16, num].ToolTipText = "";
							this.dataGridProgramMemory[num16, num].ReadOnly = true;
						}
					}
				}
				else
				{
					int num17 = 0;
					int num18 = 0;
					int num19 = 0;
					while (num17 < num)
					{
						for (int num20 = num2 + 1; num20 <= 2 * num2; num20++)
						{
							this.dataGridProgramMemory[num20, num17].Value = Utilities.ConvertIntASCIIReverse((int)PICkitFunctions.DeviceBuffers.ProgramMemory[num18++], this.asciiBytes);
							this.dataGridProgramMemory[num20, num17].ToolTipText = string.Format(this.addrFormat, num19);
							num19 += addressIncrement;
						}
						num17++;
					}
					num5 = num * num2;
					for (int num21 = num2 + 1; num21 <= 2 * num2; num21++)
					{
						if (num21 <= num2 + num4)
						{
							this.dataGridProgramMemory[num21, num].Value = Utilities.ConvertIntASCIIReverse((int)PICkitFunctions.DeviceBuffers.ProgramMemory[num5++], this.asciiBytes);
							this.dataGridProgramMemory[num21, num].ToolTipText = string.Format(this.addrFormat, num5 * addressIncrement);
						}
						else
						{
							this.dataGridProgramMemory[num21, num].Value = "";
							this.dataGridProgramMemory[num21, num].ToolTipText = "";
							this.dataGridProgramMemory[num21, num].ReadOnly = true;
						}
					}
				}
			}
			if (this.dataGridProgramMemory.FirstDisplayedCell != null && !this.progMemJustEdited)
			{
				int rowIndex = this.dataGridProgramMemory.FirstDisplayedCell.RowIndex;
				this.dataGridProgramMemory.MultiSelect = false;
				this.dataGridProgramMemory[0, rowIndex].Selected = true;
				this.dataGridProgramMemory[0, rowIndex].Selected = false;
				this.dataGridProgramMemory.MultiSelect = true;
			}
			else if (this.dataGridProgramMemory.FirstDisplayedCell == null && this.dataGridProgramMemory.RowCount > 0)
			{
				this.dataGridProgramMemory.MultiSelect = false;
				this.dataGridProgramMemory[0, 0].Selected = true;
				this.dataGridProgramMemory[0, 0].Selected = false;
				this.dataGridProgramMemory.MultiSelect = true;
			}
			this.progMemJustEdited = false;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000D460 File Offset: 0x0000C460
		private void progMemEdit(object sender, DataGridViewCellEventArgs e)
		{
			int rowIndex = e.RowIndex;
			int columnIndex = e.ColumnIndex;
			string p_value = "0x" + this.dataGridProgramMemory[columnIndex, rowIndex].FormattedValue.ToString();
			int num = 0;
			try
			{
				num = Utilities.Convert_Value_To_Int(p_value);
			}
			catch
			{
				num = 0;
			}
			int num2 = this.dataGridProgramMemory.ColumnCount - 1;
			if (this.comboBoxProgMemView.SelectedIndex >= 1)
			{
				num2 /= 2;
			}
			int num3 = rowIndex * num2 + columnIndex - 1;
			if (PICkitFunctions.FamilyIsPIC32())
			{
				int num4 = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem;
				int bootFlash = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BootFlash;
				num4 -= bootFlash;
				num3 -= num2;
				if (num3 > num4)
				{
					num3 -= num2;
				}
			}
			PICkitFunctions.DeviceBuffers.ProgramMemory[num3] = (uint)((long)num & (long)((ulong)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue));
			this.TellMainFormProgMemEdited();
			this.progMemJustEdited = true;
			this.TellMainFormUpdateGUI();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000D58C File Offset: 0x0000C58C
		private void FormMultiWinProgMem_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason != CloseReason.FormOwnerClosing)
			{
				e.Cancel = true;
				this.TellMainFormProgMemClosed();
				base.Hide();
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000D5AF File Offset: 0x0000C5AF
		private void FormMultiWinProgMem_ResizeEnd(object sender, EventArgs e)
		{
			this.ReCalcMultiWinProgMem();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000D5B7 File Offset: 0x0000C5B7
		private void FormMultiWinProgMem_Resize(object sender, EventArgs e)
		{
			if (base.WindowState == FormWindowState.Maximized)
			{
				this.maxed = true;
				this.ReCalcMultiWinProgMem();
				return;
			}
			if (this.maxed)
			{
				this.maxed = false;
				this.ReCalcMultiWinProgMem();
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000D5E5 File Offset: 0x0000C5E5
		private void comboBoxProgMemView_SelectionChangeCommitted(object sender, EventArgs e)
		{
			this.ReCalcMultiWinProgMem();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000D5ED File Offset: 0x0000C5ED
		private void toolStripMenuItemContextSelectAll_Click(object sender, EventArgs e)
		{
			this.dataGridProgramMemory.SelectAll();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000D5FA File Offset: 0x0000C5FA
		private void toolStripMenuItemContextCopy_Click(object sender, EventArgs e)
		{
			Clipboard.SetDataObject(this.dataGridProgramMemory.GetClipboardContent());
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000D60C File Offset: 0x0000C60C
		private void dataGridProgramMemory_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				this.dataGridProgramMemory.Focus();
			}
		}

		// Token: 0x040000B3 RID: 179
		public bool InitDone;

		// Token: 0x040000B4 RID: 180
		private string dataFormat = "";

		// Token: 0x040000B5 RID: 181
		private string addrFormat = "";

		// Token: 0x040000B6 RID: 182
		private bool maxed;

		// Token: 0x040000B7 RID: 183
		private bool progMemJustEdited;

		// Token: 0x040000B8 RID: 184
		private int asciiBytes;

		// Token: 0x040000B9 RID: 185
		private int lastPart;

		// Token: 0x040000BA RID: 186
		private int lastFam;

		// Token: 0x040000BB RID: 187
		public DelegateMemEdited TellMainFormProgMemEdited;

		// Token: 0x040000BC RID: 188
		public DelegateUpdateGUI TellMainFormUpdateGUI;

		// Token: 0x040000BD RID: 189
		public DelegateMultiProgMemClosed TellMainFormProgMemClosed;
	}
}
