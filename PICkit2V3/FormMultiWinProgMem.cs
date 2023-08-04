using System;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class FormMultiWinProgMem : Form
	{
		public FormMultiWinProgMem()
		{
			InitializeComponent();
		}

		public void InitProgMemDisplay(int viewMode)
		{
			dataGridProgramMemory.DefaultCellStyle.Font = new Font("Courier New", 9f);
			comboBoxProgMemView.SelectedIndex = viewMode;
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

		public void ReCalcMultiWinProgMem()
		{
			uint programMem = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem;
			if (programMem == 0U)
				return;

			if (WindowState == FormWindowState.Minimized)
				return;

			uint blankValue = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
			if (blankValue == 4294967295)
			{
				comboBoxProgMemView.SelectedIndex = 0;
				comboBoxProgMemView.Enabled = false;
			}
			else
				comboBoxProgMemView.Enabled = true;

			uint num = programMem * PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].AddressIncrement - 1;
			int num2 = 32;
			addrFormat = "{0:X3}";
			if (blankValue == 4294967295)
			{
				num2 = 65;
				addrFormat = "{0:X8}";
			}
			else if (num > 65535)
			{
				num2 = 44;
				addrFormat = "{0:X5}";
			}
			else if (num > 4095)
			{
				num2 = 38;
				addrFormat = "{0:X4}";
			}
			num2 = (int)(num2 * FormPICkit2.scalefactW);
			int num3 = 24;
			int num4 = 16;
			asciiBytes = 1;
			dataFormat = "{0:X2}";
			if (blankValue == 4294967295)
			{
				num3 = 65;
				num4 = 58;
				asciiBytes = 4;
				dataFormat = "{0:X8}";
			}
			else if (blankValue > 65535)
			{
				num3 = 50;
				num4 = 43;
				asciiBytes = 3;
				dataFormat = "{0:X6}";
			}
			else if (blankValue > 4095)
			{
				num3 = 36;
				num4 = 28;
				asciiBytes = 2;
				dataFormat = "{0:X4}";
			}
			else if (blankValue > 255)
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
			if (num7 > (int)programMem)
				num7 = (int)programMem;

			num3 = num6 / num7;
			if (comboBoxProgMemView.SelectedIndex > 0)
			{
				num4 = (int)(num5 * num3);
				num3 -= num4;
			}
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

			int num8 = (int)(programMem / num7);
			if (programMem % num7 > 0)
				num8++;

			if (blankValue == 4294967295)
				num8 += 2;

			dataGridProgramMemory.RowCount = num8;
			int num9 = num7 * PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].AddressIncrement;
			if (blankValue == 4294967295)
			{
				int num10 = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem;
				int bootFlash = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BootFlash;
				num10 -= bootFlash;
				num10 /= num7;
				dataGridProgramMemory.ShowCellToolTips = false;
				dataGridProgramMemory[0, 0].Value = "Program";
				dataGridProgramMemory[1, 0].Value = "Flash";
				for (int l = 0; l < dataGridProgramMemory.ColumnCount; l++)
				{
					dataGridProgramMemory[l, 0].Style.BackColor = SystemColors.ControlDark;
					dataGridProgramMemory[l, 0].ReadOnly = true;
				}
				int m = 1;
				int num11 = 486539264;
				while (m <= num10)
				{
					dataGridProgramMemory[0, m].Value = string.Format(addrFormat, num11);
					dataGridProgramMemory[0, m].Style.BackColor = SystemColors.ControlLight;
					num11 += num9;
					m++;
				}
				dataGridProgramMemory[0, num10 + 1].Value = "Boot";
				dataGridProgramMemory[1, num10 + 1].Value = "Flash";
				for (int n = 0; n < dataGridProgramMemory.ColumnCount; n++)
				{
					dataGridProgramMemory[n, num10 + 1].Style.BackColor = SystemColors.ControlDark;
					dataGridProgramMemory[n, num10 + 1].ReadOnly = true;
				}
				int num12 = num10 + 2;
				int num13 = 532676608;
				while (num12 < dataGridProgramMemory.RowCount)
				{
					dataGridProgramMemory[0, num12].Value = string.Format(addrFormat, num13);
					dataGridProgramMemory[0, num12].Style.BackColor = SystemColors.ControlLight;
					num13 += num9;
					num12++;
				}
			}
			else
			{
				dataGridProgramMemory.ShowCellToolTips = true;
				int num14 = 0;
				int num15 = 0;
				while (num14 < num8)
				{
					dataGridProgramMemory[0, num14].Value = string.Format(addrFormat, num15);
					dataGridProgramMemory[0, num14].ReadOnly = true;
					dataGridProgramMemory[0, num14].Style.BackColor = SystemColors.ControlLight;
					num15 += num9;
					num14++;
				}
			}
			UpdateDisplay();
		}

		public void UpdateMultiWinProgMem(string dataSource)
		{
			if (lastPart != PICkitFunctions.ActivePart || lastFam != PICkitFunctions.GetActiveFamily())
			{
				lastPart = PICkitFunctions.ActivePart;
				lastFam = PICkitFunctions.GetActiveFamily();
				ReCalcMultiWinProgMem();
			}
			else
				UpdateDisplay();
			displayDataSource.Text = dataSource;
		}

		public string GetDataSource()
		{
			return displayDataSource.Text;
		}

		private void UpdateDisplay()
		{
			int num = dataGridProgramMemory.RowCount - 1;
			int num2 = dataGridProgramMemory.ColumnCount - 1;
			int addressIncrement = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].AddressIncrement;
			uint blankValue = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
			if (comboBoxProgMemView.SelectedIndex > 0)
				num2 /= 2;

			int num3 = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem;
			int bootFlash = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BootFlash;
			num3 -= bootFlash;
			num3 /= num2;
			int num4 = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem % num2);
			if (num4 == 0)
				num4 = num2;

			int num5 = num * num2;
			if (blankValue == 4294967295)
			{
				int num6 = 0;
				for (int i = 1; i <= num3; i++)
				{
					for (int j = 0; j < num2; j++)
						dataGridProgramMemory[j + 1, i].Value = string.Format(dataFormat, PICkitFunctions.DeviceBuffers.ProgramMemory[num6++]);
				}
				for (int k = num3 + 2; k < dataGridProgramMemory.RowCount - 1; k++)
				{
					for (int l = 0; l < num2; l++)
						dataGridProgramMemory[l + 1, k].Value = string.Format(dataFormat, PICkitFunctions.DeviceBuffers.ProgramMemory[num6++]);
				}
				num4 = bootFlash % num2;
				if (num4 == 0)
					num4 = num2;
				for (int m = 1; m <= num2; m++)
				{
					if (m <= num4)
						dataGridProgramMemory[m, num].Value = string.Format(dataFormat, PICkitFunctions.DeviceBuffers.ProgramMemory[num6++]);
					else
					{
						dataGridProgramMemory[m, num].Value = "";
						dataGridProgramMemory[m, num].ReadOnly = true;
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
						dataGridProgramMemory[num9, n].Value = string.Format(dataFormat, PICkitFunctions.DeviceBuffers.ProgramMemory[num7++]);
						dataGridProgramMemory[num9, n].ToolTipText = string.Format(addrFormat, num8);
						num8 += addressIncrement;
					}
					n++;
				}
				for (int num10 = 1; num10 <= num2; num10++)
				{
					if (num10 <= num4)
					{
						dataGridProgramMemory[num10, num].Value = string.Format(dataFormat, PICkitFunctions.DeviceBuffers.ProgramMemory[num5]);
						dataGridProgramMemory[num10, num].ToolTipText = string.Format(addrFormat, num5++ * addressIncrement);
					}
					else
					{
						dataGridProgramMemory[num10, num].Value = "";
						dataGridProgramMemory[num10, num].ToolTipText = "";
						dataGridProgramMemory[num10, num].ReadOnly = true;
					}
				}
			}
			if (comboBoxProgMemView.SelectedIndex > 0)
			{
				for (int num11 = num2 + 1; num11 <= 2 * num2; num11++)
					dataGridProgramMemory.Columns[num11].ReadOnly = true;

				if (comboBoxProgMemView.SelectedIndex == 1)
				{
					int num12 = 0;
					int num13 = 0;
					int num14 = 0;
					while (num12 < num)
					{
						for (int num15 = num2 + 1; num15 <= 2 * num2; num15++)
						{
							dataGridProgramMemory[num15, num12].Value = Utilities.ConvertIntASCII((int)PICkitFunctions.DeviceBuffers.ProgramMemory[num13++], asciiBytes);
							dataGridProgramMemory[num15, num12].ToolTipText = string.Format(addrFormat, num14);
							num14 += addressIncrement;
						}
						num12++;
					}
					num5 = num * num2;
					for (int num16 = num2 + 1; num16 <= 2 * num2; num16++)
					{
						if (num16 <= num2 + num4)
						{
							dataGridProgramMemory[num16, num].Value = Utilities.ConvertIntASCII((int)PICkitFunctions.DeviceBuffers.ProgramMemory[num5++], asciiBytes);
							dataGridProgramMemory[num16, num].ToolTipText = string.Format(addrFormat, num5 * addressIncrement);
						}
						else
						{
							dataGridProgramMemory[num16, num].Value = "";
							dataGridProgramMemory[num16, num].ToolTipText = "";
							dataGridProgramMemory[num16, num].ReadOnly = true;
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
							dataGridProgramMemory[num20, num17].Value = Utilities.ConvertIntASCIIReverse((int)PICkitFunctions.DeviceBuffers.ProgramMemory[num18++], asciiBytes);
							dataGridProgramMemory[num20, num17].ToolTipText = string.Format(addrFormat, num19);
							num19 += addressIncrement;
						}
						num17++;
					}
					num5 = num * num2;
					for (int num21 = num2 + 1; num21 <= 2 * num2; num21++)
					{
						if (num21 <= num2 + num4)
						{
							dataGridProgramMemory[num21, num].Value = Utilities.ConvertIntASCIIReverse((int)PICkitFunctions.DeviceBuffers.ProgramMemory[num5++], asciiBytes);
							dataGridProgramMemory[num21, num].ToolTipText = string.Format(addrFormat, num5 * addressIncrement);
						}
						else
						{
							dataGridProgramMemory[num21, num].Value = "";
							dataGridProgramMemory[num21, num].ToolTipText = "";
							dataGridProgramMemory[num21, num].ReadOnly = true;
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
			else if (dataGridProgramMemory.FirstDisplayedCell == null && dataGridProgramMemory.RowCount > 0)
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
			PICkitFunctions.DeviceBuffers.ProgramMemory[num3] = (uint)(num & PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue);
			TellMainFormProgMemEdited();
			progMemJustEdited = true;
			TellMainFormUpdateGUI();
		}

		private void FormMultiWinProgMem_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason != CloseReason.FormOwnerClosing)
			{
				e.Cancel = true;
				TellMainFormProgMemClosed();
				Hide();
			}
		}

		private void FormMultiWinProgMem_ResizeEnd(object sender, EventArgs e)
		{
			ReCalcMultiWinProgMem();
		}

		private void FormMultiWinProgMem_Resize(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Maximized)
			{
				maxed = true;
				ReCalcMultiWinProgMem();
				return;
			}
			if (maxed)
			{
				maxed = false;
				ReCalcMultiWinProgMem();
			}
		}

		private void ComboBoxProgMemView_SelectionChangeCommitted(object sender, EventArgs e)
		{
			ReCalcMultiWinProgMem();
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
			{
				dataGridProgramMemory.Focus();
			}
		}

		public bool InitDone;
		private string dataFormat = "";
		private string addrFormat = "";
		private bool maxed;
		private bool progMemJustEdited;
		private int asciiBytes;
		private int lastPart;
		private int lastFam;
		public DelegateMemEdited TellMainFormProgMemEdited;
		public DelegateUpdateGUI TellMainFormUpdateGUI;
		public DelegateMultiProgMemClosed TellMainFormProgMemClosed;
	}
}
