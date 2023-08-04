using System;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class FormTestMemory : Form
	{
		public FormTestMemory()
		{
			InitializeComponent();
			InitTestMemoryGUI();
			ClearTestMemory();
			UpdateTestMemForm();
			UpdateTestMemoryGrid();
			FormPICkit2.testMemoryOpen = true;
		}

		public void ReadTestMemory()
		{
			byte[] array = new byte[128];
			PICkitFunctions.RunScript(0, 1);
			int bytesPerLocation = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
			int num = 128 / (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].TestMemoryRdWords * bytesPerLocation);
			int num2 = num * PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].TestMemoryRdWords;
			int num3 = 0;
			PrepTestMem();
			do
			{
				PICkitFunctions.RunScriptUploadNoLen(27, num);
				Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 0, 64);
				PICkitFunctions.UploadDataNoLen();
				Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 64, 64);
				int num4 = 0;
				for (int i = 0; i < num2; i++)
				{
					int num5 = 0;
					uint num6 = array[num4 + num5++];
					if (num5 < bytesPerLocation)
					{
						num6 |= (uint)array[num4 + num5++] << 8;
					}
					if (num5 < bytesPerLocation)
					{
						num6 |= (uint)array[num4 + num5++] << 16;
					}
					if (num5 < bytesPerLocation)
					{
						num6 |= (uint)array[num4 + num5++] << 24;
					}
					num4 += num5;
					if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
					{
						num6 = num6 >> 1 & PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
					}
					TestMemory[num3++] = num6;
				}
			}
			while (num3 < FormPICkit2.testMemoryWords);
			PICkitFunctions.RunScript(1, 1);
		}

		public bool HexImportExportTM()
		{
			return checkBoxTestMemImportExport.Enabled && checkBoxTestMemImportExport.Checked;
		}

		private void PrepTestMem()
		{
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart == 16384 || PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart == 65536)
			{
				PICkitFunctions.SendScript(new byte[]
				{
					238,
					6,
					0,
					242,
					0,
					242,
					0
				});
				return;
			}
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart == 2048)
			{
				int num = GetTestMemAddress() / 4 - 1;
				PICkitFunctions.SendScript(new byte[]
				{
					210,
					(byte)(num & 255),
					210,
					(byte)(num >> 8 & 255),
					238,
					6,
					6,
					238,
					6,
					6,
					238,
					6,
					6,
					238,
					6,
					6,
					221,
					12
				});
				return;
			}
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart == 2097152)
			{
				PICkitFunctions.SendScript(new byte[]
				{
					218,
					32,
					14,
					218,
					248,
					110,
					218,
					0,
					14,
					218,
					247,
					110,
					218,
					0,
					14,
					218,
					246,
					110
				});
			}
		}

		private void InitTestMemoryGUI()
		{
			dataGridTestMemory.DefaultCellStyle.Font = new Font("Courier New", 9);
			dataGridTestMemory.ColumnCount = 9;
			dataGridTestMemory.RowCount = 512;
			dataGridTestMemory[0, 0].Selected = true;
			dataGridTestMemory[0, 0].Selected = false;
			dataGridTestMemory.Columns[0].Width = (int)(59f * FormPICkit2.scalefactW);
			dataGridTestMemory.Columns[0].ReadOnly = true;
			if (FormPICkit2.testMemoryImportExport)
			{
				checkBoxTestMemImportExport.Checked = true;
			}
		}

		public void UpdateTestMemForm()
		{
			textBoxTestMemSize.Text = FormPICkit2.testMemoryWords.ToString();
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart > 0)
			{
				textBoxBaselineConfig.Enabled = true;
				textBoxTestMemSize.Enabled = true;
				checkBoxTestMemImportExport.Enabled = true;
				labelNotSupported.Visible = false;
				return;
			}
			textBoxBaselineConfig.Enabled = false;
			textBoxTestMemSize.Enabled = false;
			checkBoxTestMemImportExport.Enabled = false;
			dataGridTestMemory.Enabled = false;
			labelNotSupported.Visible = true;
		}

		public void ClearTestMemory()
		{
			for (int i = 0; i < TestMemory.Length; i++)
				TestMemory[i] = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
		}

		public void UpdateTestMemoryGrid()
		{
			bool flag = false;
			int num = 0;
			int num2 = GetTestMemAddress() * PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemHexBytes;
			int num3 = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDAddr;
			if (PICkitFunctions.ActivePart != lastPart || PICkitFunctions.GetActiveFamily() != lastFamily)
			{
				ClearTestMemory();
				lastPart = PICkitFunctions.ActivePart;
				lastFamily = PICkitFunctions.GetActiveFamily();
			}
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart == 2048)
				num3 = num2;

			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords > 0 && num3 >= num2 && num3 < num2 + FormPICkit2.testMemoryWords)
			{
				flag = true;
				num = (num3 - num2) / PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
			}
			bool flag2 = false;
			int num4 = 0;
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords > 0 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr >= num2 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr < num2 + FormPICkit2.testMemoryWords)
			{
				flag2 = true;
				num4 = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr - num2) / PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
			}
			int num5 = 9;
			dataGridTestMemory.Columns[0].Width = (int)(51 * FormPICkit2.scalefactW);
			int num6 = 8;
			int width = (int)(35 * FormPICkit2.scalefactW);
			int num7 = GetTestMemAddress();
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart == 2097152)
				num7 *= 2;

			if (dataGridTestMemory.ColumnCount != num5)
			{
				dataGridTestMemory.Rows.Clear();
				dataGridTestMemory.ColumnCount = num5;
			}
			for (int i = 1; i < dataGridTestMemory.ColumnCount; i++)
				dataGridTestMemory.Columns[i].Width = width;

			int addressIncrement = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].AddressIncrement;
			int num8 = num6;
			int num9 = FormPICkit2.testMemoryWords / num8;
			int num10 = addressIncrement * num6;
			if (dataGridTestMemory.RowCount != num9)
			{
				dataGridTestMemory.Rows.Clear();
				dataGridTestMemory.RowCount = num9;
			}

			int num11 = dataGridTestMemory.RowCount * num10 - 1;
			string format = "{0:X3}";
			if (num11 > 65535)
				format = "{0:X5}";
			else if (num11 > 4095)
				format = "{0:X4}";

			int j = 0;
			int num12 = 0;
			while (j < dataGridTestMemory.RowCount)
			{
				dataGridTestMemory[0, j].Value = string.Format(format, num7 + num12);
				dataGridTestMemory[0, j].Style.BackColor = SystemColors.ControlLight;
				num12 += num10;
				j++;
			}
			string format2 = "{0:X3}";
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 4095)
				format2 = "{0:X4}";

			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 65535)
				format2 = "{0:X6}";

			for (int k = 0; k < num8; k++)
				dataGridTestMemory.Columns[k + 1].ReadOnly = true;

			int l = 0;
			int num13 = 0;
			while (l < dataGridTestMemory.RowCount)
			{
				for (int m = 0; m < num8; m++)
				{
					if (flag && num13 >= num && num13 < num + PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords)
					{
						TestMemory[num13] = PICkitFunctions.DeviceBuffers.UserIDs[num13 - num];
						dataGridTestMemory[m + 1, l].ToolTipText = string.Format(format, num7 + num13 * addressIncrement);
						dataGridTestMemory[m + 1, l].Value = string.Format(format2, PICkitFunctions.DeviceBuffers.UserIDs[num13++ - num]);
						dataGridTestMemory[m + 1, l].Style.BackColor = Color.LightSteelBlue;
						dataGridTestMemory[m + 1, l].ReadOnly = false;
					}
					else if (flag2 && num13 >= num4 && num13 < num4 + PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords)
					{
						TestMemory[num13] = PICkitFunctions.DeviceBuffers.ConfigWords[num13 - num4];
						dataGridTestMemory[m + 1, l].ToolTipText = string.Format(format, num7 + num13 * addressIncrement);
						dataGridTestMemory[m + 1, l].Value = string.Format(format2, PICkitFunctions.DeviceBuffers.ConfigWords[num13++ - num4]);
						dataGridTestMemory[m + 1, l].Style.BackColor = Color.LightSalmon;
						dataGridTestMemory[m + 1, l].ReadOnly = false;
					}
					else if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CalibrationWords > 0 && num13 >= num4 + PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords && num13 < num4 + PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords + PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CalibrationWords)
					{
						dataGridTestMemory[m + 1, l].ToolTipText = string.Format(format, num7 + num13 * addressIncrement);
						dataGridTestMemory[m + 1, l].Value = string.Format(format2, TestMemory[num13++]);
						dataGridTestMemory[m + 1, l].Style.BackColor = Color.Gold;
						dataGridTestMemory[m + 1, l].ReadOnly = false;
					}
					else
					{
						dataGridTestMemory[m + 1, l].ToolTipText = string.Format(format, num7 + num13 * addressIncrement);
						dataGridTestMemory[m + 1, l].Value = string.Format(format2, TestMemory[num13++]);
						dataGridTestMemory[m + 1, l].Style.BackColor = SystemColors.Window;
					}
				}
				l++;
			}
			if (dataGridTestMemory.FirstDisplayedCell != null && !testMemJustEdited)
			{
				int rowIndex = dataGridTestMemory.FirstDisplayedCell.RowIndex;
				dataGridTestMemory[0, rowIndex].Selected = true;
				dataGridTestMemory[0, rowIndex].Selected = false;
			}
			else if (dataGridTestMemory.FirstDisplayedCell == null)
			{
				dataGridTestMemory[0, 0].Selected = true;
				dataGridTestMemory[0, 0].Selected = false;
			}
			testMemJustEdited = false;
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart == 2048)
			{
				labelBLConfig.Visible = true;
				textBoxBaselineConfig.Visible = true;
				textBoxBaselineConfig.Text = string.Format("{0:X4}", PICkitFunctions.DeviceBuffers.ConfigWords[0]);
			}
			else
			{
				labelBLConfig.Visible = false;
				textBoxBaselineConfig.Visible = false;
			}
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart == 16384)
			{
				labelCalWarning.Visible = true;
				buttonWriteCalWords.Visible = true;
				buttonWriteCalWords.Enabled = (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CalibrationWords > 0);
				return;
			}
			labelCalWarning.Visible = false;
			buttonWriteCalWords.Visible = false;
		}

		private void FormTestMemory_FormClosing(object sender, FormClosingEventArgs e)
		{
			FormPICkit2.testMemoryOpen = false;
		}

		private int GetTestMemAddress()
		{
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart == 2048)
			{
				int num = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem;
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0)
					num += PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem;

				return num;
			}
			return (int)(PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart / PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemHexBytes);
		}

		private void TextBoxTestMemSize_Leave(object sender, EventArgs e)
		{
			MemSizeEdit();
		}

		private void TextBoxTestMemSize_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
				MemSizeEdit();
		}

		private void MemSizeEdit()
		{
			labelTestMemSize8.Visible = false;
			try
			{
				string p_value = textBoxTestMemSize.Text;
				if (textBoxTestMemSize.Text.Length > 1)
				{
					if (textBoxTestMemSize.Text.Substring(0, 2) == "0x")
						p_value = textBoxTestMemSize.Text;
					else if (textBoxTestMemSize.Text.Substring(0, 1) == "x")
						p_value = "0" + textBoxTestMemSize.Text;
				}

				FormPICkit2.testMemoryWords = Utilities.Convert_Value_To_Int(p_value);
				if (FormPICkit2.testMemoryWords > 1024)
					FormPICkit2.testMemoryWords = 1024;
				else if (FormPICkit2.testMemoryWords < 16)
					FormPICkit2.testMemoryWords = 16;
				else if (FormPICkit2.testMemoryWords % 16 != 0)
					FormPICkit2.testMemoryWords = (FormPICkit2.testMemoryWords / 16 + 1) * 16;
					labelTestMemSize8.Visible = true;
			}
			catch
			{
			}
			UpdateTestMemForm();
			UpdateTestMemoryGrid();
		}

		private void DataGridTestMemory_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			int rowIndex = e.RowIndex;
			int columnIndex = e.ColumnIndex;
			string p_value = "0x" + dataGridTestMemory[columnIndex, rowIndex].FormattedValue.ToString();
			int num = Utilities.Convert_Value_To_Int(p_value);
			int num2 = dataGridTestMemory.ColumnCount - 1;
			int num3 = rowIndex * num2 + columnIndex - 1;
			int num4 = GetTestMemAddress() * PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemHexBytes;
			int num5 = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDAddr;
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart == 2048)
				num5 = num4;

			TestMemory[num3] = (uint)(num & PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue);
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords > 0 && num5 >= num4 && num5 < num4 + FormPICkit2.testMemoryWords)
			{
				int num6 = (num5 - num4) / PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
				if (num3 >= num6 && num3 < num6 + PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords)
				{
					PICkitFunctions.DeviceBuffers.UserIDs[num3 - num6] = (uint)(num & PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue);
					if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart == 2097152)
						PICkitFunctions.DeviceBuffers.UserIDs[num3 - num6] &= 255U;
				}
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords > 0 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr >= num4 && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr < num4 + FormPICkit2.testMemoryWords)
			{
				int num7 = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr - num4) / PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
				if (num3 >= num7 && num3 < num7 + PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords)
				{
					PICkitFunctions.DeviceBuffers.ConfigWords[num3 - num7] = (uint)(num & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[num3 - num7]);
				}
			}
			testMemJustEdited = true;
			UpdateMainFormGUI();
		}

		private void TextBoxBaselineConfig_Leave(object sender, EventArgs e)
		{
			BaselineConfigEdit();
		}

		private void TextBoxBaselineConfig_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
				BaselineConfigEdit();
		}

		private void BaselineConfigEdit()
		{
			string p_value = "0x" + textBoxBaselineConfig.Text;
			int num = Utilities.Convert_Value_To_Int(p_value);
			PICkitFunctions.DeviceBuffers.ConfigWords[0] = (uint)num;
			UpdateTestMemoryGrid();
			UpdateMainFormGUI();
		}

		private void CheckBoxTestMemImportExport_CheckedChanged(object sender, EventArgs e)
		{
			FormPICkit2.testMemoryImportExport = checkBoxTestMemImportExport.Checked;
		}

		private void ButtonClearTestMem_Click(object sender, EventArgs e)
		{
			ClearTestMemory();
			UpdateTestMemoryGrid();
		}

		private void ButtonWriteCalWords_Click(object sender, EventArgs e)
		{
			uint[] array = new uint[PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CalibrationWords];
			int num = GetTestMemAddress() * PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemHexBytes;
			int num2 = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr - num) / PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
			int num3 = num2 + PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords;
			for (int i = 0; i < array.Length; i++)
				array[i] = TestMemory[num3 + i];

			CallMainFormEraseWrCal(array);
		}

		public static uint[] TestMemory = new uint[1024];
		public static bool ReWriteCalWords = false;
		private bool testMemJustEdited;
		private int lastPart;
		private int lastFamily;
		public DelegateUpdateGUI UpdateMainFormGUI;
		public DelegateWriteCal CallMainFormEraseWrCal;
	}
}
