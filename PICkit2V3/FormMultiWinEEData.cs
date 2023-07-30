using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	// Token: 0x02000004 RID: 4
	public partial class FormMultiWinEEData : Form
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002AE8 File Offset: 0x00001AE8
		public FormMultiWinEEData()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002B0C File Offset: 0x00001B0C
		public void InitMemDisplay(int viewMode)
		{
			this.dataGridProgramMemory.DefaultCellStyle.Font = new Font("Courier New", 9f);
			this.comboBoxProgMemView.SelectedIndex = viewMode;
			this.ReCalcMultiWinMem();
			this.InitDone = true;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002B46 File Offset: 0x00001B46
		public int GetViewMode()
		{
			return this.comboBoxProgMemView.SelectedIndex;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002B53 File Offset: 0x00001B53
		public void DisplayDisable()
		{
			this.comboBoxProgMemView.Enabled = false;
			this.dataGridProgramMemory.Enabled = false;
			this.dataGridProgramMemory.ForeColor = SystemColors.GrayText;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002B7D File Offset: 0x00001B7D
		public void DisplayEnable()
		{
			this.comboBoxProgMemView.Enabled = true;
			this.dataGridProgramMemory.Enabled = true;
			this.dataGridProgramMemory.ForeColor = SystemColors.WindowText;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002BA7 File Offset: 0x00001BA7
		public void DisplayEETextOn(string displayText)
		{
			this.displayEEProgInfo.Text = displayText;
			this.displayEEProgInfo.Visible = true;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002BC1 File Offset: 0x00001BC1
		public void DisplayEETextOff()
		{
			this.displayEEProgInfo.Visible = false;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002BD0 File Offset: 0x00001BD0
		public void ReCalcMultiWinMem()
		{
			uint eemem = (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem;
			if (eemem == 0U)
			{
				return;
			}
			if (base.WindowState == FormWindowState.Minimized)
			{
				return;
			}
			uint num = eemem * (uint)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemAddressIncrement - 1U;
			int num2 = 30;
			this.addrFormat = "{0:X2}";
			if (num > 4095U)
			{
				num2 = 40;
				this.addrFormat = "{0:X4}";
			}
			else if (num > 255U)
			{
				num2 = 32;
				this.addrFormat = "{0:X3}";
			}
			num2 = (int)((float)num2 * FormPICkit2.scalefactW);
			uint blankValue = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
			int num3 = 24;
			int num4 = 16;
			this.asciiBytes = 1;
			this.dataFormat = "{0:X2}";
			if (blankValue > 65535U)
			{
				num3 = 36;
				num4 = 28;
				this.asciiBytes = 2;
				this.dataFormat = "{0:X4}";
			}
			else if (blankValue == 4095U)
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
			num3 = (int)((float)num3 * FormPICkit2.scalefactW);
			int num6 = this.dataGridProgramMemory.Size.Width - num2 - (int)(20f * FormPICkit2.scalefactW);
			int num7 = num6 / num3;
			for (int i = 1; i <= 256; i *= 2)
			{
				if (i > num7)
				{
					num7 = i / 2;
					break;
				}
			}
			if (num7 > (int)eemem)
			{
				num7 = (int)eemem;
			}
			num3 = num6 / num7;
			if (this.comboBoxProgMemView.SelectedIndex > 0)
			{
				num4 = (int)(num5 * (float)num3);
				num3 -= num4;
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
			int num8 = (int)(eemem / (uint)num7);
			if ((ulong)eemem % (ulong)((long)num7) > 0UL)
			{
				num8++;
			}
			this.dataGridProgramMemory.RowCount = num8;
			int num9 = num7 * (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemAddressIncrement;
			int l = 0;
			int num10 = 0;
			while (l < num8)
			{
				this.dataGridProgramMemory[0, l].Value = string.Format(this.addrFormat, num10);
				this.dataGridProgramMemory[0, l].ReadOnly = true;
				this.dataGridProgramMemory[0, l].Style.BackColor = SystemColors.ControlLight;
				num10 += num9;
				l++;
			}
			this.updateDisplay();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002FD2 File Offset: 0x00001FD2
		public void UpdateMultiWinMem()
		{
			if (this.lastPart != PICkitFunctions.ActivePart || this.lastFam != PICkitFunctions.GetActiveFamily())
			{
				this.ReCalcMultiWinMem();
				return;
			}
			this.updateDisplay();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002FFC File Offset: 0x00001FFC
		private void updateDisplay()
		{
			int num = this.dataGridProgramMemory.RowCount - 1;
			int num2 = this.dataGridProgramMemory.ColumnCount - 1;
			int eememAddressIncrement = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemAddressIncrement;
			if (this.comboBoxProgMemView.SelectedIndex > 0)
			{
				num2 /= 2;
			}
			int i = 0;
			int num3 = 0;
			int num4 = 0;
			while (i < num)
			{
				for (int j = 1; j <= num2; j++)
				{
					this.dataGridProgramMemory[j, i].Value = string.Format(this.dataFormat, PICkitFunctions.DeviceBuffers.EEPromMemory[num3++]);
					this.dataGridProgramMemory[j, i].ToolTipText = string.Format(this.addrFormat, num4);
					num4 += eememAddressIncrement;
				}
				i++;
			}
			int num5 = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem % num2;
			if (num5 == 0)
			{
				num5 = num2;
			}
			int num6 = num * num2;
			for (int k = 1; k <= num2; k++)
			{
				if (k <= num5)
				{
					this.dataGridProgramMemory[k, num].Value = string.Format(this.dataFormat, PICkitFunctions.DeviceBuffers.EEPromMemory[num6++]);
					this.dataGridProgramMemory[k, num].ToolTipText = string.Format(this.addrFormat, num6 * eememAddressIncrement);
				}
				else
				{
					this.dataGridProgramMemory[k, num].Value = "";
					this.dataGridProgramMemory[k, num].ToolTipText = "";
					this.dataGridProgramMemory[k, num].ReadOnly = true;
				}
			}
			if (this.comboBoxProgMemView.SelectedIndex > 0)
			{
				for (int l = num2 + 1; l <= 2 * num2; l++)
				{
					this.dataGridProgramMemory.Columns[l].ReadOnly = true;
				}
				if (this.comboBoxProgMemView.SelectedIndex == 1)
				{
					int m = 0;
					int num7 = 0;
					int num8 = 0;
					while (m < num)
					{
						for (int n = num2 + 1; n <= 2 * num2; n++)
						{
							this.dataGridProgramMemory[n, m].Value = Utilities.ConvertIntASCII((int)PICkitFunctions.DeviceBuffers.EEPromMemory[num7++], this.asciiBytes);
							this.dataGridProgramMemory[n, m].ToolTipText = string.Format(this.addrFormat, num8);
							num8 += eememAddressIncrement;
						}
						m++;
					}
					num6 = num * num2;
					for (int num9 = num2 + 1; num9 <= 2 * num2; num9++)
					{
						if (num9 <= num2 + num5)
						{
							this.dataGridProgramMemory[num9, num].Value = Utilities.ConvertIntASCII((int)PICkitFunctions.DeviceBuffers.EEPromMemory[num6++], this.asciiBytes);
							this.dataGridProgramMemory[num9, num].ToolTipText = string.Format(this.addrFormat, num6 * eememAddressIncrement);
						}
						else
						{
							this.dataGridProgramMemory[num9, num].Value = "";
							this.dataGridProgramMemory[num9, num].ToolTipText = "";
							this.dataGridProgramMemory[num9, num].ReadOnly = true;
						}
					}
				}
				else
				{
					int num10 = 0;
					int num11 = 0;
					int num12 = 0;
					while (num10 < num)
					{
						for (int num13 = num2 + 1; num13 <= 2 * num2; num13++)
						{
							this.dataGridProgramMemory[num13, num10].Value = Utilities.ConvertIntASCIIReverse((int)PICkitFunctions.DeviceBuffers.EEPromMemory[num11++], this.asciiBytes);
							this.dataGridProgramMemory[num13, num10].ToolTipText = string.Format(this.addrFormat, num12);
							num12 += eememAddressIncrement;
						}
						num10++;
					}
					num6 = num * num2;
					for (int num14 = num2 + 1; num14 <= 2 * num2; num14++)
					{
						if (num14 <= num2 + num5)
						{
							this.dataGridProgramMemory[num14, num].Value = Utilities.ConvertIntASCIIReverse((int)PICkitFunctions.DeviceBuffers.EEPromMemory[num6++], this.asciiBytes);
							this.dataGridProgramMemory[num14, num].ToolTipText = string.Format(this.addrFormat, num6 * eememAddressIncrement);
						}
						else
						{
							this.dataGridProgramMemory[num14, num].Value = "";
							this.dataGridProgramMemory[num14, num].ToolTipText = "";
							this.dataGridProgramMemory[num14, num].ReadOnly = true;
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
			else if (this.dataGridProgramMemory.FirstDisplayedCell == null)
			{
				this.dataGridProgramMemory.MultiSelect = false;
				this.dataGridProgramMemory[0, 0].Selected = true;
				this.dataGridProgramMemory[0, 0].Selected = false;
				this.dataGridProgramMemory.MultiSelect = true;
			}
			this.progMemJustEdited = false;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00003560 File Offset: 0x00002560
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
			uint num3 = 255U;
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 65535U)
			{
				num3 = 65535U;
			}
			else if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue == 4095U)
			{
				num3 = 4095U;
			}
			PICkitFunctions.DeviceBuffers.EEPromMemory[rowIndex * num2 + columnIndex - 1] = (uint)((long)num & (long)((ulong)num3));
			this.TellMainFormProgMemEdited();
			this.progMemJustEdited = true;
			this.TellMainFormUpdateGUI();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00003670 File Offset: 0x00002670
		private void FormMultiWinEEData_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason != CloseReason.FormOwnerClosing)
			{
				e.Cancel = true;
				this.TellMainFormEEMemClosed();
				base.Hide();
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00003693 File Offset: 0x00002693
		private void comboBoxProgMemView_SelectionChangeCommitted(object sender, EventArgs e)
		{
			this.ReCalcMultiWinMem();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000369B File Offset: 0x0000269B
		private void FormMultiWinEEData_ResizeEnd(object sender, EventArgs e)
		{
			this.ReCalcMultiWinMem();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000036A3 File Offset: 0x000026A3
		private void FormMultiWinEEData_Resize(object sender, EventArgs e)
		{
			if (base.WindowState == FormWindowState.Maximized)
			{
				this.maxed = true;
				this.ReCalcMultiWinMem();
				return;
			}
			if (this.maxed)
			{
				this.maxed = false;
				this.ReCalcMultiWinMem();
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000036D1 File Offset: 0x000026D1
		private void toolStripMenuItemContextSelectAll_Click(object sender, EventArgs e)
		{
			this.dataGridProgramMemory.SelectAll();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000036DE File Offset: 0x000026DE
		private void toolStripMenuItemContextCopy_Click(object sender, EventArgs e)
		{
			Clipboard.SetDataObject(this.dataGridProgramMemory.GetClipboardContent());
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000036F0 File Offset: 0x000026F0
		private void dataGridProgramMemory_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				this.dataGridProgramMemory.Focus();
			}
		}

		// Token: 0x04000008 RID: 8
		public bool InitDone;

		// Token: 0x04000009 RID: 9
		private string dataFormat = "";

		// Token: 0x0400000A RID: 10
		private string addrFormat = "";

		// Token: 0x0400000B RID: 11
		private bool maxed;

		// Token: 0x0400000C RID: 12
		private bool progMemJustEdited;

		// Token: 0x0400000D RID: 13
		private int asciiBytes;

		// Token: 0x0400000E RID: 14
		private int lastPart;

		// Token: 0x0400000F RID: 15
		private int lastFam;

		// Token: 0x04000010 RID: 16
		public DelegateMemEdited TellMainFormProgMemEdited;

		// Token: 0x04000011 RID: 17
		public DelegateUpdateGUI TellMainFormUpdateGUI;

		// Token: 0x04000012 RID: 18
		public DelegateMultiEEMemClosed TellMainFormEEMemClosed;
	}
}
