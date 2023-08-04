using System;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class FormMultiWinEEData : Form
	{
		public FormMultiWinEEData()
		{
			InitializeComponent();
		}

		public void InitMemDisplay(int viewMode)
		{
			dataGridProgramMemory.DefaultCellStyle.Font = new Font("Courier New", 9f);
			comboBoxProgMemView.SelectedIndex = viewMode;
			ReCalcMultiWinMem();
			InitDone = true;
		}

		public int GetViewMode()
		{
			return comboBoxProgMemView.SelectedIndex;
		}

		public void DisplayDisable()
		{
			comboBoxProgMemView.Enabled = false;
			dataGridProgramMemory.Enabled = false;
			dataGridProgramMemory.ForeColor = SystemColors.GrayText;
		}

		public void DisplayEnable()
		{
			comboBoxProgMemView.Enabled = true;
			dataGridProgramMemory.Enabled = true;
			dataGridProgramMemory.ForeColor = SystemColors.WindowText;
		}

		public void DisplayEETextOn(string displayText)
		{
			displayEEProgInfo.Text = displayText;
			displayEEProgInfo.Visible = true;
		}

		public void DisplayEETextOff()
		{
			displayEEProgInfo.Visible = false;
		}

		public void ReCalcMultiWinMem()
		{
			uint eemem = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem;
			if (eemem == 0U)
				return;

			if (WindowState == FormWindowState.Minimized)
				return;

			uint num = eemem * PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemAddressIncrement - 1;
			int num2 = 30;
			addrFormat = "{0:X2}";
			if (num > 4095)
			{
				num2 = 40;
				addrFormat = "{0:X4}";
			}
			else if (num > 255)
			{
				num2 = 32;
				addrFormat = "{0:X3}";
			}

			num2 = (int)(num2 * FormPICkit2.scalefactW);
			uint blankValue = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
			int num3 = 24;
			int num4 = 16;
			asciiBytes = 1;
			dataFormat = "{0:X2}";
			if (blankValue > 65535)
			{
				num3 = 36;
				num4 = 28;
				asciiBytes = 2;
				dataFormat = "{0:X4}";
			}
			else if (blankValue == 4095)
			{
				num3 = 28;
				num4 = 28;
				asciiBytes = 2;
				dataFormat = "{0:X3}";
			}
			float num5 = 1;
			if (comboBoxProgMemView.SelectedIndex > 0)
			{
				num5 = num4 / (num4 + (float)num3);
				num3 += num4;
			}
			num3 = (int)(num3 * FormPICkit2.scalefactW);
			int num6 = dataGridProgramMemory.Size.Width - num2 - (int)(20 * FormPICkit2.scalefactW);
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
				num7 = (int)eemem;

			num3 = num6 / num7;
			if (comboBoxProgMemView.SelectedIndex > 0)
			{
				num4 = (int)(num5 * num3);
				num3 -= num4;
			}
			if (dataGridProgramMemory.FirstDisplayedCell != null && !progMemJustEdited)
			{
				int rowIndex = dataGridProgramMemory.FirstDisplayedCell.RowIndex;
				dataGridProgramMemory.MultiSelect = false;
				dataGridProgramMemory[0, rowIndex].Selected = true;
				dataGridProgramMemory[0, rowIndex].Selected = false;
				dataGridProgramMemory.MultiSelect = true;
			}
			else if (dataGridProgramMemory.FirstDisplayedCell == null && dataGridProgramMemory.RowCount > 0)
			{
				dataGridProgramMemory.MultiSelect = false;
				dataGridProgramMemory[0, 0].Selected = true;
				dataGridProgramMemory[0, 0].Selected = false;
				dataGridProgramMemory.MultiSelect = true;
			}
			progMemJustEdited = false;
			dataGridProgramMemory.Rows.Clear();
			if (comboBoxProgMemView.SelectedIndex > 0)
				dataGridProgramMemory.ColumnCount = 2 * num7 + 1;
			else
				dataGridProgramMemory.ColumnCount = num7 + 1;

			dataGridProgramMemory.Columns[0].Width = num2;
			for (int j = 1; j <= num7; j++)
				dataGridProgramMemory.Columns[j].Width = num3;

			if (comboBoxProgMemView.SelectedIndex > 0)
			{
				for (int k = num7 + 1; k <= 2 * num7; k++)
					dataGridProgramMemory.Columns[k].Width = num4;
			}

			int num8 = (int)(eemem / (uint)num7);
			if (eemem % num7 > 0)
				num8++;

			dataGridProgramMemory.RowCount = num8;
			int num9 = num7 * PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemAddressIncrement;
			int l = 0;
			int num10 = 0;
			while (l < num8)
			{
				dataGridProgramMemory[0, l].Value = string.Format(addrFormat, num10);
				dataGridProgramMemory[0, l].ReadOnly = true;
				dataGridProgramMemory[0, l].Style.BackColor = SystemColors.ControlLight;
				num10 += num9;
				l++;
			}
			UpdateDisplay();
		}

		public void UpdateMultiWinMem()
		{
			if (lastPart != PICkitFunctions.ActivePart || lastFam != PICkitFunctions.GetActiveFamily())
			{
				ReCalcMultiWinMem();
				return;
			}
			UpdateDisplay();
		}

		private void UpdateDisplay()
		{
			int num = dataGridProgramMemory.RowCount - 1;
			int num2 = dataGridProgramMemory.ColumnCount - 1;
			int eememAddressIncrement = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemAddressIncrement;
			if (comboBoxProgMemView.SelectedIndex > 0)
				num2 /= 2;

			int i = 0;
			int num3 = 0;
			int num4 = 0;
			while (i < num)
			{
				for (int j = 1; j <= num2; j++)
				{
					dataGridProgramMemory[j, i].Value = string.Format(dataFormat, PICkitFunctions.DeviceBuffers.EEPromMemory[num3++]);
					dataGridProgramMemory[j, i].ToolTipText = string.Format(addrFormat, num4);
					num4 += eememAddressIncrement;
				}
				i++;
			}
			int num5 = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem % num2;
			if (num5 == 0)
				num5 = num2;

			int num6 = num * num2;
			for (int k = 1; k <= num2; k++)
			{
				if (k <= num5)
				{
					dataGridProgramMemory[k, num].Value = string.Format(dataFormat, PICkitFunctions.DeviceBuffers.EEPromMemory[num6++]);
					dataGridProgramMemory[k, num].ToolTipText = string.Format(addrFormat, num6 * eememAddressIncrement);
				}
				else
				{
					dataGridProgramMemory[k, num].Value = "";
					dataGridProgramMemory[k, num].ToolTipText = "";
					dataGridProgramMemory[k, num].ReadOnly = true;
				}
			}
			if (comboBoxProgMemView.SelectedIndex > 0)
			{
				for (int l = num2 + 1; l <= 2 * num2; l++)
					dataGridProgramMemory.Columns[l].ReadOnly = true;

				if (comboBoxProgMemView.SelectedIndex == 1)
				{
					int m = 0;
					int num7 = 0;
					int num8 = 0;
					while (m < num)
					{
						for (int n = num2 + 1; n <= 2 * num2; n++)
						{
							dataGridProgramMemory[n, m].Value = Utilities.ConvertIntASCII((int)PICkitFunctions.DeviceBuffers.EEPromMemory[num7++], asciiBytes);
							dataGridProgramMemory[n, m].ToolTipText = string.Format(addrFormat, num8);
							num8 += eememAddressIncrement;
						}
						m++;
					}
					num6 = num * num2;
					for (int num9 = num2 + 1; num9 <= 2 * num2; num9++)
					{
						if (num9 <= num2 + num5)
						{
							dataGridProgramMemory[num9, num].Value = Utilities.ConvertIntASCII((int)PICkitFunctions.DeviceBuffers.EEPromMemory[num6++], asciiBytes);
							dataGridProgramMemory[num9, num].ToolTipText = string.Format(addrFormat, num6 * eememAddressIncrement);
						}
						else
						{
							dataGridProgramMemory[num9, num].Value = "";
							dataGridProgramMemory[num9, num].ToolTipText = "";
							dataGridProgramMemory[num9, num].ReadOnly = true;
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
							dataGridProgramMemory[num13, num10].Value = Utilities.ConvertIntASCIIReverse((int)PICkitFunctions.DeviceBuffers.EEPromMemory[num11++], asciiBytes);
							dataGridProgramMemory[num13, num10].ToolTipText = string.Format(addrFormat, num12);
							num12 += eememAddressIncrement;
						}
						num10++;
					}
					num6 = num * num2;
					for (int num14 = num2 + 1; num14 <= 2 * num2; num14++)
					{
						if (num14 <= num2 + num5)
						{
							dataGridProgramMemory[num14, num].Value = Utilities.ConvertIntASCIIReverse((int)PICkitFunctions.DeviceBuffers.EEPromMemory[num6++], asciiBytes);
							dataGridProgramMemory[num14, num].ToolTipText = string.Format(addrFormat, num6 * eememAddressIncrement);
						}
						else
						{
							dataGridProgramMemory[num14, num].Value = "";
							dataGridProgramMemory[num14, num].ToolTipText = "";
							dataGridProgramMemory[num14, num].ReadOnly = true;
						}
					}
				}
			}
			if (dataGridProgramMemory.FirstDisplayedCell != null && !progMemJustEdited)
			{
				int rowIndex = dataGridProgramMemory.FirstDisplayedCell.RowIndex;
				dataGridProgramMemory.MultiSelect = false;
				dataGridProgramMemory[0, rowIndex].Selected = true;
				dataGridProgramMemory[0, rowIndex].Selected = false;
				dataGridProgramMemory.MultiSelect = true;
			}
			else if (dataGridProgramMemory.FirstDisplayedCell == null)
			{
				dataGridProgramMemory.MultiSelect = false;
				dataGridProgramMemory[0, 0].Selected = true;
				dataGridProgramMemory[0, 0].Selected = false;
				dataGridProgramMemory.MultiSelect = true;
			}
			progMemJustEdited = false;
		}

		private void ProgMemEdit(object sender, DataGridViewCellEventArgs e)
		{
			int rowIndex = e.RowIndex;
			int columnIndex = e.ColumnIndex;
			string p_value = "0x" + dataGridProgramMemory[columnIndex, rowIndex].FormattedValue.ToString();
            int num;
            try
			{
				num = Utilities.Convert_Value_To_Int(p_value);
			}
			catch
			{
				num = 0;
			}

			int num2 = dataGridProgramMemory.ColumnCount - 1;
			if (comboBoxProgMemView.SelectedIndex >= 1)
				num2 /= 2;

			uint num3 = 255;
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 65535)
				num3 = 65535;

			else if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue == 4095)
				num3 = 4095;

			PICkitFunctions.DeviceBuffers.EEPromMemory[rowIndex * num2 + columnIndex - 1] = (uint)(num & num3);
			TellMainFormProgMemEdited();
			progMemJustEdited = true;
			TellMainFormUpdateGUI();
		}

		private void FormMultiWinEEData_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason != CloseReason.FormOwnerClosing)
			{
				e.Cancel = true;
				TellMainFormEEMemClosed();
				Hide();
			}
		}

		private void ComboBoxProgMemView_SelectionChangeCommitted(object sender, EventArgs e)
		{
			ReCalcMultiWinMem();
		}

		private void FormMultiWinEEData_ResizeEnd(object sender, EventArgs e)
		{
			ReCalcMultiWinMem();
		}

		private void FormMultiWinEEData_Resize(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Maximized)
			{
				maxed = true;
				ReCalcMultiWinMem();
				return;
			}
			if (maxed)
			{
				maxed = false;
				ReCalcMultiWinMem();
			}
		}

		private void ToolStripMenuItemContextSelectAll_Click(object sender, EventArgs e)
		{
			dataGridProgramMemory.SelectAll();
		}

		private void ToolStripMenuItemContextCopy_Click(object sender, EventArgs e)
		{
			Clipboard.SetDataObject(dataGridProgramMemory.GetClipboardContent());
		}

		private void DataGridProgramMemory_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				dataGridProgramMemory.Focus();
		}

		public bool InitDone;
		private string dataFormat = "";
		private string addrFormat = "";
		private bool maxed;
		private bool progMemJustEdited;
		private int asciiBytes;
		private readonly int lastPart;
		private readonly int lastFam;
		public DelegateMemEdited TellMainFormProgMemEdited;
		public DelegateUpdateGUI TellMainFormUpdateGUI;
		public DelegateMultiEEMemClosed TellMainFormEEMemClosed;
	}
}
