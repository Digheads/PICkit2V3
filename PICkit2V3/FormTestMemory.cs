using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	// Token: 0x02000012 RID: 18
	public partial class FormTestMemory : Form
	{
		// Token: 0x0600013E RID: 318 RVA: 0x000347E7 File Offset: 0x000337E7
		public FormTestMemory()
		{
			this.InitializeComponent();
			this.initTestMemoryGUI();
			this.ClearTestMemory();
			this.UpdateTestMemForm();
			this.UpdateTestMemoryGrid();
			FormPICkit2.testMemoryOpen = true;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00034814 File Offset: 0x00033814
		public void ReadTestMemory()
		{
			byte[] array = new byte[128];
			PICkitFunctions.RunScript(0, 1);
			int bytesPerLocation = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
			int num = 128 / ((int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].TestMemoryRdWords * bytesPerLocation);
			int num2 = num * (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].TestMemoryRdWords;
			int num3 = 0;
			this.prepTestMem();
			do
			{
				PICkitFunctions.RunScriptUploadNoLen(27, num);
				Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 0L, 64L);
				PICkitFunctions.UploadDataNoLen();
				Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 64L, 64L);
				int num4 = 0;
				for (int i = 0; i < num2; i++)
				{
					int num5 = 0;
					uint num6 = (uint)array[num4 + num5++];
					if (num5 < bytesPerLocation)
					{
						num6 |= (uint)((uint)array[num4 + num5++] << 8);
					}
					if (num5 < bytesPerLocation)
					{
						num6 |= (uint)((uint)array[num4 + num5++] << 16);
					}
					if (num5 < bytesPerLocation)
					{
						num6 |= (uint)((uint)array[num4 + num5++] << 24);
					}
					num4 += num5;
					if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
					{
						num6 = (num6 >> 1 & PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue);
					}
					FormTestMemory.TestMemory[num3++] = num6;
				}
			}
			while (num3 < FormPICkit2.testMemoryWords);
			PICkitFunctions.RunScript(1, 1);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000349A3 File Offset: 0x000339A3
		public bool HexImportExportTM()
		{
			return this.checkBoxTestMemImportExport.Enabled && this.checkBoxTestMemImportExport.Checked;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000349C0 File Offset: 0x000339C0
		private void prepTestMem()
		{
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart == 16384U || PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart == 65536U)
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
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart == 2048U)
			{
				int num = this.getTestMemAddress() / 4 - 1;
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
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart == 2097152U)
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

		// Token: 0x06000142 RID: 322 RVA: 0x00034BB0 File Offset: 0x00033BB0
		private void initTestMemoryGUI()
		{
			this.dataGridTestMemory.DefaultCellStyle.Font = new Font("Courier New", 9f);
			this.dataGridTestMemory.ColumnCount = 9;
			this.dataGridTestMemory.RowCount = 512;
			this.dataGridTestMemory[0, 0].Selected = true;
			this.dataGridTestMemory[0, 0].Selected = false;
			this.dataGridTestMemory.Columns[0].Width = (int)(59f * FormPICkit2.scalefactW);
			this.dataGridTestMemory.Columns[0].ReadOnly = true;
			if (FormPICkit2.testMemoryImportExport)
			{
				this.checkBoxTestMemImportExport.Checked = true;
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00034C6C File Offset: 0x00033C6C
		public void UpdateTestMemForm()
		{
			this.textBoxTestMemSize.Text = FormPICkit2.testMemoryWords.ToString();
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart > 0U)
			{
				this.textBoxBaselineConfig.Enabled = true;
				this.textBoxTestMemSize.Enabled = true;
				this.checkBoxTestMemImportExport.Enabled = true;
				this.labelNotSupported.Visible = false;
				return;
			}
			this.textBoxBaselineConfig.Enabled = false;
			this.textBoxTestMemSize.Enabled = false;
			this.checkBoxTestMemImportExport.Enabled = false;
			this.dataGridTestMemory.Enabled = false;
			this.labelNotSupported.Visible = true;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00034D18 File Offset: 0x00033D18
		public void ClearTestMemory()
		{
			for (int i = 0; i < FormTestMemory.TestMemory.Length; i++)
			{
				FormTestMemory.TestMemory[i] = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00034D58 File Offset: 0x00033D58
		public void UpdateTestMemoryGrid()
		{
			bool flag = false;
			int num = 0;
			int num2 = this.getTestMemAddress() * (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemHexBytes;
			int num3 = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDAddr;
			if (PICkitFunctions.ActivePart != this.lastPart || PICkitFunctions.GetActiveFamily() != this.lastFamily)
			{
				this.ClearTestMemory();
				this.lastPart = PICkitFunctions.ActivePart;
				this.lastFamily = PICkitFunctions.GetActiveFamily();
			}
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart == 2048U)
			{
				num3 = num2;
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords > 0 && num3 >= num2 && num3 < num2 + FormPICkit2.testMemoryWords)
			{
				flag = true;
				num = (num3 - num2) / (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
			}
			bool flag2 = false;
			int num4 = 0;
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords > 0 && (ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr >= (ulong)((long)num2) && (ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr < (ulong)((long)(num2 + FormPICkit2.testMemoryWords)))
			{
				flag2 = true;
				num4 = (int)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr - (ulong)((long)num2)) / (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
			}
			int num5 = 9;
			this.dataGridTestMemory.Columns[0].Width = (int)(51f * FormPICkit2.scalefactW);
			int num6 = 8;
			int width = (int)(35f * FormPICkit2.scalefactW);
			int num7 = this.getTestMemAddress();
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart == 2097152U)
			{
				num7 *= 2;
			}
			if (this.dataGridTestMemory.ColumnCount != num5)
			{
				this.dataGridTestMemory.Rows.Clear();
				this.dataGridTestMemory.ColumnCount = num5;
			}
			for (int i = 1; i < this.dataGridTestMemory.ColumnCount; i++)
			{
				this.dataGridTestMemory.Columns[i].Width = width;
			}
			int addressIncrement = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].AddressIncrement;
			int num8 = num6;
			int num9 = FormPICkit2.testMemoryWords / num8;
			int num10 = addressIncrement * num6;
			if (this.dataGridTestMemory.RowCount != num9)
			{
				this.dataGridTestMemory.Rows.Clear();
				this.dataGridTestMemory.RowCount = num9;
			}
			int num11 = this.dataGridTestMemory.RowCount * num10 - 1;
			string format = "{0:X3}";
			if (num11 > 65535)
			{
				format = "{0:X5}";
			}
			else if (num11 > 4095)
			{
				format = "{0:X4}";
			}
			int j = 0;
			int num12 = 0;
			while (j < this.dataGridTestMemory.RowCount)
			{
				this.dataGridTestMemory[0, j].Value = string.Format(format, num7 + num12);
				this.dataGridTestMemory[0, j].Style.BackColor = SystemColors.ControlLight;
				num12 += num10;
				j++;
			}
			string format2 = "{0:X3}";
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 4095U)
			{
				format2 = "{0:X4}";
			}
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 65535U)
			{
				format2 = "{0:X6}";
			}
			for (int k = 0; k < num8; k++)
			{
				this.dataGridTestMemory.Columns[k + 1].ReadOnly = true;
			}
			int l = 0;
			int num13 = 0;
			while (l < this.dataGridTestMemory.RowCount)
			{
				for (int m = 0; m < num8; m++)
				{
					if (flag && num13 >= num && num13 < num + (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords)
					{
						FormTestMemory.TestMemory[num13] = PICkitFunctions.DeviceBuffers.UserIDs[num13 - num];
						this.dataGridTestMemory[m + 1, l].ToolTipText = string.Format(format, num7 + num13 * addressIncrement);
						this.dataGridTestMemory[m + 1, l].Value = string.Format(format2, PICkitFunctions.DeviceBuffers.UserIDs[num13++ - num]);
						this.dataGridTestMemory[m + 1, l].Style.BackColor = Color.LightSteelBlue;
						this.dataGridTestMemory[m + 1, l].ReadOnly = false;
					}
					else if (flag2 && num13 >= num4 && num13 < num4 + (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords)
					{
						FormTestMemory.TestMemory[num13] = PICkitFunctions.DeviceBuffers.ConfigWords[num13 - num4];
						this.dataGridTestMemory[m + 1, l].ToolTipText = string.Format(format, num7 + num13 * addressIncrement);
						this.dataGridTestMemory[m + 1, l].Value = string.Format(format2, PICkitFunctions.DeviceBuffers.ConfigWords[num13++ - num4]);
						this.dataGridTestMemory[m + 1, l].Style.BackColor = Color.LightSalmon;
						this.dataGridTestMemory[m + 1, l].ReadOnly = false;
					}
					else if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CalibrationWords > 0 && num13 >= num4 + (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords && num13 < num4 + (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords + (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CalibrationWords)
					{
						this.dataGridTestMemory[m + 1, l].ToolTipText = string.Format(format, num7 + num13 * addressIncrement);
						this.dataGridTestMemory[m + 1, l].Value = string.Format(format2, FormTestMemory.TestMemory[num13++]);
						this.dataGridTestMemory[m + 1, l].Style.BackColor = Color.Gold;
						this.dataGridTestMemory[m + 1, l].ReadOnly = false;
					}
					else
					{
						this.dataGridTestMemory[m + 1, l].ToolTipText = string.Format(format, num7 + num13 * addressIncrement);
						this.dataGridTestMemory[m + 1, l].Value = string.Format(format2, FormTestMemory.TestMemory[num13++]);
						this.dataGridTestMemory[m + 1, l].Style.BackColor = SystemColors.Window;
					}
				}
				l++;
			}
			if (this.dataGridTestMemory.FirstDisplayedCell != null && !this.testMemJustEdited)
			{
				int rowIndex = this.dataGridTestMemory.FirstDisplayedCell.RowIndex;
				this.dataGridTestMemory[0, rowIndex].Selected = true;
				this.dataGridTestMemory[0, rowIndex].Selected = false;
			}
			else if (this.dataGridTestMemory.FirstDisplayedCell == null)
			{
				this.dataGridTestMemory[0, 0].Selected = true;
				this.dataGridTestMemory[0, 0].Selected = false;
			}
			this.testMemJustEdited = false;
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart == 2048U)
			{
				this.labelBLConfig.Visible = true;
				this.textBoxBaselineConfig.Visible = true;
				this.textBoxBaselineConfig.Text = string.Format("{0:X4}", PICkitFunctions.DeviceBuffers.ConfigWords[0]);
			}
			else
			{
				this.labelBLConfig.Visible = false;
				this.textBoxBaselineConfig.Visible = false;
			}
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart == 16384U)
			{
				this.labelCalWarning.Visible = true;
				this.buttonWriteCalWords.Visible = true;
				this.buttonWriteCalWords.Enabled = (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CalibrationWords > 0);
				return;
			}
			this.labelCalWarning.Visible = false;
			this.buttonWriteCalWords.Visible = false;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00035631 File Offset: 0x00034631
		private void FormTestMemory_FormClosing(object sender, FormClosingEventArgs e)
		{
			FormPICkit2.testMemoryOpen = false;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0003563C File Offset: 0x0003463C
		private int getTestMemAddress()
		{
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart == 2048U)
			{
				int num = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem;
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0)
				{
					num += (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem;
				}
				return num;
			}
			return (int)(PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart / (uint)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemHexBytes);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000356F0 File Offset: 0x000346F0
		private void textBoxTestMemSize_Leave(object sender, EventArgs e)
		{
			this.memSizeEdit();
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000356F8 File Offset: 0x000346F8
		private void textBoxTestMemSize_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.memSizeEdit();
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0003570C File Offset: 0x0003470C
		private void memSizeEdit()
		{
			this.labelTestMemSize8.Visible = false;
			try
			{
				string p_value = this.textBoxTestMemSize.Text;
				if (this.textBoxTestMemSize.Text.Length > 1)
				{
					if (this.textBoxTestMemSize.Text.Substring(0, 2) == "0x")
					{
						p_value = this.textBoxTestMemSize.Text;
					}
					else if (this.textBoxTestMemSize.Text.Substring(0, 1) == "x")
					{
						p_value = "0" + this.textBoxTestMemSize.Text;
					}
				}
				FormPICkit2.testMemoryWords = Utilities.Convert_Value_To_Int(p_value);
				if (FormPICkit2.testMemoryWords > 1024)
				{
					FormPICkit2.testMemoryWords = 1024;
				}
				else if (FormPICkit2.testMemoryWords < 16)
				{
					FormPICkit2.testMemoryWords = 16;
				}
				else if (FormPICkit2.testMemoryWords % 16 != 0)
				{
					FormPICkit2.testMemoryWords = (FormPICkit2.testMemoryWords / 16 + 1) * 16;
					this.labelTestMemSize8.Visible = true;
				}
			}
			catch
			{
			}
			this.UpdateTestMemForm();
			this.UpdateTestMemoryGrid();
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00035824 File Offset: 0x00034824
		private void dataGridTestMemory_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			int rowIndex = e.RowIndex;
			int columnIndex = e.ColumnIndex;
			string p_value = "0x" + this.dataGridTestMemory[columnIndex, rowIndex].FormattedValue.ToString();
			int num = Utilities.Convert_Value_To_Int(p_value);
			int num2 = this.dataGridTestMemory.ColumnCount - 1;
			int num3 = rowIndex * num2 + columnIndex - 1;
			int num4 = this.getTestMemAddress() * (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemHexBytes;
			int num5 = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDAddr;
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart == 2048U)
			{
				num5 = num4;
			}
			FormTestMemory.TestMemory[num3] = (uint)((long)num & (long)((ulong)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue));
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords > 0 && num5 >= num4 && num5 < num4 + FormPICkit2.testMemoryWords)
			{
				int num6 = (num5 - num4) / (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
				if (num3 >= num6 && num3 < num6 + (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords)
				{
					PICkitFunctions.DeviceBuffers.UserIDs[num3 - num6] = (uint)((long)num & (long)((ulong)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue));
					if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart == 2097152U)
					{
						PICkitFunctions.DeviceBuffers.UserIDs[num3 - num6] &= 255U;
					}
				}
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords > 0 && (ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr >= (ulong)((long)num4) && (ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr < (ulong)((long)(num4 + FormPICkit2.testMemoryWords)))
			{
				int num7 = (int)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr - (ulong)((long)num4)) / (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
				if (num3 >= num7 && num3 < num7 + (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords)
				{
					PICkitFunctions.DeviceBuffers.ConfigWords[num3 - num7] = (uint)(num & (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[num3 - num7]);
				}
			}
			this.testMemJustEdited = true;
			this.UpdateMainFormGUI();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00035B09 File Offset: 0x00034B09
		private void textBoxBaselineConfig_Leave(object sender, EventArgs e)
		{
			this.baselineConfigEdit();
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00035B11 File Offset: 0x00034B11
		private void textBoxBaselineConfig_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.baselineConfigEdit();
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00035B24 File Offset: 0x00034B24
		private void baselineConfigEdit()
		{
			string p_value = "0x" + this.textBoxBaselineConfig.Text;
			int num = Utilities.Convert_Value_To_Int(p_value);
			PICkitFunctions.DeviceBuffers.ConfigWords[0] = (uint)num;
			this.UpdateTestMemoryGrid();
			this.UpdateMainFormGUI();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00035B6C File Offset: 0x00034B6C
		private void checkBoxTestMemImportExport_CheckedChanged(object sender, EventArgs e)
		{
			FormPICkit2.testMemoryImportExport = this.checkBoxTestMemImportExport.Checked;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00035B7E File Offset: 0x00034B7E
		private void buttonClearTestMem_Click(object sender, EventArgs e)
		{
			this.ClearTestMemory();
			this.UpdateTestMemoryGrid();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00035B8C File Offset: 0x00034B8C
		private void buttonWriteCalWords_Click(object sender, EventArgs e)
		{
			uint[] array = new uint[(int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CalibrationWords];
			int num = this.getTestMemAddress() * (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemHexBytes;
			int num2 = (int)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr - (ulong)((long)num)) / (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
			int num3 = num2 + (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = FormTestMemory.TestMemory[num3 + i];
			}
			this.CallMainFormEraseWrCal(array);
		}

		// Token: 0x04000317 RID: 791
		public static uint[] TestMemory = new uint[1024];

		// Token: 0x04000318 RID: 792
		public static bool ReWriteCalWords = false;

		// Token: 0x04000319 RID: 793
		private bool testMemJustEdited;

		// Token: 0x0400031A RID: 794
		private int lastPart;

		// Token: 0x0400031B RID: 795
		private int lastFamily;

		// Token: 0x0400031C RID: 796
		public DelegateUpdateGUI UpdateMainFormGUI;

		// Token: 0x0400031D RID: 797
		public DelegateWriteCal CallMainFormEraseWrCal;
	}
}
