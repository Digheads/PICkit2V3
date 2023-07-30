using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	// Token: 0x0200000E RID: 14
	public partial class DialogConfigEdit : Form
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x0001EC78 File Offset: 0x0001DC78
		public DialogConfigEdit()
		{
			this.InitializeComponent();
			for (int i = 0; i < (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords; i++)
			{
				this.configSaves[i] = PICkitFunctions.DeviceBuffers.ConfigWords[i];
			}
			this.configWords[0].configPanel = this.panel1;
			this.configWords[0].name = this.labelName1;
			this.configWords[0].addr = this.labelAdr1;
			this.configWords[0].value = this.labelVal1;
			this.configWords[0].bits = new TextBox[16];
			this.configWords[0].bits[0] = this.textBox1_0;
			this.configWords[0].bits[1] = this.textBox1_1;
			this.configWords[0].bits[2] = this.textBox1_2;
			this.configWords[0].bits[3] = this.textBox1_3;
			this.configWords[0].bits[4] = this.textBox1_4;
			this.configWords[0].bits[5] = this.textBox1_5;
			this.configWords[0].bits[6] = this.textBox1_6;
			this.configWords[0].bits[7] = this.textBox1_7;
			this.configWords[0].bits[8] = this.textBox1_8;
			this.configWords[0].bits[9] = this.textBox1_9;
			this.configWords[0].bits[10] = this.textBox1_10;
			this.configWords[0].bits[11] = this.textBox1_11;
			this.configWords[0].bits[12] = this.textBox1_12;
			this.configWords[0].bits[13] = this.textBox1_13;
			this.configWords[0].bits[14] = this.textBox1_14;
			this.configWords[0].bits[15] = this.textBox1_15;
			this.configWords[1].configPanel = this.panel2;
			this.configWords[1].name = this.labelName2;
			this.configWords[1].addr = this.labelAdr2;
			this.configWords[1].value = this.labelVal2;
			this.configWords[1].bits = new TextBox[16];
			this.configWords[1].bits[0] = this.textBox2_0;
			this.configWords[1].bits[1] = this.textBox2_1;
			this.configWords[1].bits[2] = this.textBox2_2;
			this.configWords[1].bits[3] = this.textBox2_3;
			this.configWords[1].bits[4] = this.textBox2_4;
			this.configWords[1].bits[5] = this.textBox2_5;
			this.configWords[1].bits[6] = this.textBox2_6;
			this.configWords[1].bits[7] = this.textBox2_7;
			this.configWords[1].bits[8] = this.textBox2_8;
			this.configWords[1].bits[9] = this.textBox2_9;
			this.configWords[1].bits[10] = this.textBox2_10;
			this.configWords[1].bits[11] = this.textBox2_11;
			this.configWords[1].bits[12] = this.textBox2_12;
			this.configWords[1].bits[13] = this.textBox2_13;
			this.configWords[1].bits[14] = this.textBox2_14;
			this.configWords[1].bits[15] = this.textBox2_15;
			this.configWords[2].configPanel = this.panel3;
			this.configWords[2].name = this.labelName3;
			this.configWords[2].addr = this.labelAdr3;
			this.configWords[2].value = this.labelVal3;
			this.configWords[2].bits = new TextBox[16];
			this.configWords[2].bits[0] = this.textBox3_0;
			this.configWords[2].bits[1] = this.textBox3_1;
			this.configWords[2].bits[2] = this.textBox3_2;
			this.configWords[2].bits[3] = this.textBox3_3;
			this.configWords[2].bits[4] = this.textBox3_4;
			this.configWords[2].bits[5] = this.textBox3_5;
			this.configWords[2].bits[6] = this.textBox3_6;
			this.configWords[2].bits[7] = this.textBox3_7;
			this.configWords[2].bits[8] = this.textBox3_8;
			this.configWords[2].bits[9] = this.textBox3_9;
			this.configWords[2].bits[10] = this.textBox3_10;
			this.configWords[2].bits[11] = this.textBox3_11;
			this.configWords[2].bits[12] = this.textBox3_12;
			this.configWords[2].bits[13] = this.textBox3_13;
			this.configWords[2].bits[14] = this.textBox3_14;
			this.configWords[2].bits[15] = this.textBox3_15;
			this.configWords[3].configPanel = this.panel4;
			this.configWords[3].name = this.labelName4;
			this.configWords[3].addr = this.labelAdr4;
			this.configWords[3].value = this.labelVal4;
			this.configWords[3].bits = new TextBox[16];
			this.configWords[3].bits[0] = this.textBox4_0;
			this.configWords[3].bits[1] = this.textBox4_1;
			this.configWords[3].bits[2] = this.textBox4_2;
			this.configWords[3].bits[3] = this.textBox4_3;
			this.configWords[3].bits[4] = this.textBox4_4;
			this.configWords[3].bits[5] = this.textBox4_5;
			this.configWords[3].bits[6] = this.textBox4_6;
			this.configWords[3].bits[7] = this.textBox4_7;
			this.configWords[3].bits[8] = this.textBox4_8;
			this.configWords[3].bits[9] = this.textBox4_9;
			this.configWords[3].bits[10] = this.textBox4_10;
			this.configWords[3].bits[11] = this.textBox4_11;
			this.configWords[3].bits[12] = this.textBox4_12;
			this.configWords[3].bits[13] = this.textBox4_13;
			this.configWords[3].bits[14] = this.textBox4_14;
			this.configWords[3].bits[15] = this.textBox4_15;
			this.configWords[4].configPanel = this.panel5;
			this.configWords[4].name = this.labelName5;
			this.configWords[4].addr = this.labelAdr5;
			this.configWords[4].value = this.labelVal5;
			this.configWords[4].bits = new TextBox[16];
			this.configWords[4].bits[0] = this.textBox5_0;
			this.configWords[4].bits[1] = this.textBox5_1;
			this.configWords[4].bits[2] = this.textBox5_2;
			this.configWords[4].bits[3] = this.textBox5_3;
			this.configWords[4].bits[4] = this.textBox5_4;
			this.configWords[4].bits[5] = this.textBox5_5;
			this.configWords[4].bits[6] = this.textBox5_6;
			this.configWords[4].bits[7] = this.textBox5_7;
			this.configWords[4].bits[8] = this.textBox5_8;
			this.configWords[4].bits[9] = this.textBox5_9;
			this.configWords[4].bits[10] = this.textBox5_10;
			this.configWords[4].bits[11] = this.textBox5_11;
			this.configWords[4].bits[12] = this.textBox5_12;
			this.configWords[4].bits[13] = this.textBox5_13;
			this.configWords[4].bits[14] = this.textBox5_14;
			this.configWords[4].bits[15] = this.textBox5_15;
			this.configWords[5].configPanel = this.panel6;
			this.configWords[5].name = this.labelName6;
			this.configWords[5].addr = this.labelAdr6;
			this.configWords[5].value = this.labelVal6;
			this.configWords[5].bits = new TextBox[16];
			this.configWords[5].bits[0] = this.textBox6_0;
			this.configWords[5].bits[1] = this.textBox6_1;
			this.configWords[5].bits[2] = this.textBox6_2;
			this.configWords[5].bits[3] = this.textBox6_3;
			this.configWords[5].bits[4] = this.textBox6_4;
			this.configWords[5].bits[5] = this.textBox6_5;
			this.configWords[5].bits[6] = this.textBox6_6;
			this.configWords[5].bits[7] = this.textBox6_7;
			this.configWords[5].bits[8] = this.textBox6_8;
			this.configWords[5].bits[9] = this.textBox6_9;
			this.configWords[5].bits[10] = this.textBox6_10;
			this.configWords[5].bits[11] = this.textBox6_11;
			this.configWords[5].bits[12] = this.textBox6_12;
			this.configWords[5].bits[13] = this.textBox6_13;
			this.configWords[5].bits[14] = this.textBox6_14;
			this.configWords[5].bits[15] = this.textBox6_15;
			this.configWords[6].configPanel = this.panel7;
			this.configWords[6].name = this.labelName7;
			this.configWords[6].addr = this.labelAdr7;
			this.configWords[6].value = this.labelVal7;
			this.configWords[6].bits = new TextBox[16];
			this.configWords[6].bits[0] = this.textBox7_0;
			this.configWords[6].bits[1] = this.textBox7_1;
			this.configWords[6].bits[2] = this.textBox7_2;
			this.configWords[6].bits[3] = this.textBox7_3;
			this.configWords[6].bits[4] = this.textBox7_4;
			this.configWords[6].bits[5] = this.textBox7_5;
			this.configWords[6].bits[6] = this.textBox7_6;
			this.configWords[6].bits[7] = this.textBox7_7;
			this.configWords[6].bits[8] = this.textBox7_8;
			this.configWords[6].bits[9] = this.textBox7_9;
			this.configWords[6].bits[10] = this.textBox7_10;
			this.configWords[6].bits[11] = this.textBox7_11;
			this.configWords[6].bits[12] = this.textBox7_12;
			this.configWords[6].bits[13] = this.textBox7_13;
			this.configWords[6].bits[14] = this.textBox7_14;
			this.configWords[6].bits[15] = this.textBox7_15;
			this.configWords[7].configPanel = this.panel8;
			this.configWords[7].name = this.labelName8;
			this.configWords[7].addr = this.labelAdr8;
			this.configWords[7].value = this.labelVal8;
			this.configWords[7].bits = new TextBox[16];
			this.configWords[7].bits[0] = this.textBox8_0;
			this.configWords[7].bits[1] = this.textBox8_1;
			this.configWords[7].bits[2] = this.textBox8_2;
			this.configWords[7].bits[3] = this.textBox8_3;
			this.configWords[7].bits[4] = this.textBox8_4;
			this.configWords[7].bits[5] = this.textBox8_5;
			this.configWords[7].bits[6] = this.textBox8_6;
			this.configWords[7].bits[7] = this.textBox8_7;
			this.configWords[7].bits[8] = this.textBox8_8;
			this.configWords[7].bits[9] = this.textBox8_9;
			this.configWords[7].bits[10] = this.textBox8_10;
			this.configWords[7].bits[11] = this.textBox8_11;
			this.configWords[7].bits[12] = this.textBox8_12;
			this.configWords[7].bits[13] = this.textBox8_13;
			this.configWords[7].bits[14] = this.textBox8_14;
			this.configWords[7].bits[15] = this.textBox8_15;
			this.configWords[8].configPanel = this.panel9;
			this.configWords[8].name = this.labelName9;
			this.configWords[8].addr = this.labelAdr9;
			this.configWords[8].value = this.labelVal9;
			this.configWords[8].bits = new TextBox[16];
			this.configWords[8].bits[0] = this.textBox9_0;
			this.configWords[8].bits[1] = this.textBox9_1;
			this.configWords[8].bits[2] = this.textBox9_2;
			this.configWords[8].bits[3] = this.textBox9_3;
			this.configWords[8].bits[4] = this.textBox9_4;
			this.configWords[8].bits[5] = this.textBox9_5;
			this.configWords[8].bits[6] = this.textBox9_6;
			this.configWords[8].bits[7] = this.textBox9_7;
			this.configWords[8].bits[8] = this.textBox9_8;
			this.configWords[8].bits[9] = this.textBox9_9;
			this.configWords[8].bits[10] = this.textBox9_10;
			this.configWords[8].bits[11] = this.textBox9_11;
			this.configWords[8].bits[12] = this.textBox9_12;
			this.configWords[8].bits[13] = this.textBox9_13;
			this.configWords[8].bits[14] = this.textBox9_14;
			this.configWords[8].bits[15] = this.textBox9_15;
			int num = 0;
			for (int j = 0; j < 9; j++)
			{
				for (int k = 0; k < 16; k++)
				{
					this.configWords[j].bits[k].Tag = num++;
				}
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0001FF9A File Offset: 0x0001EF9A
		public void SetDisplayMask(int option)
		{
			this.displayMask = option;
			this.redraw();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0001FFAC File Offset: 0x0001EFAC
		private void redraw()
		{
			int num = (int)(9 - PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords);
			num *= (int)(48f * this.ScalefactH);
			num -= (int)(24f * this.ScalefactH);
			base.Size = new Size(base.Size.Width, base.Size.Height - num);
			string[] array = new string[9];
			for (int i = 1; i <= array.Length; i++)
			{
				array[i - 1] = string.Format("CONFIG{0:G}", i);
			}
			int num2 = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr / 2U);
			int num3 = 1;
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords == 1)
			{
				array[0] = "CONFIG";
			}
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue == 65535U)
			{
				num2 = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr;
				num3 = 2;
			}
			else if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue == 16777215U)
			{
				if (PICkitFunctions.FamilyIsPIC24FJ())
				{
					for (int j = 1; j <= (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords; j++)
					{
						array[(int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords - j] = string.Format("CW{0:G}", j);
					}
					num2 = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr / 2U);
					num3 = 2;
				}
				else if (PICkitFunctions.FamilyIsPIC24H() || PICkitFunctions.FamilyIsdsPIC33F() || PICkitFunctions.FamilyIsdsPIC30SMPS() || PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords == 9)
				{
					array[0] = "FBS";
					array[1] = "FSS";
					array[2] = "FGS";
					array[3] = "FOSCSEL";
					array[4] = "FOSC";
					array[5] = "FWDT";
					array[6] = "FPOR";
					array[7] = "FICD";
					array[8] = "FDS";
					num2 = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr / 2U);
					num3 = 2;
				}
				else
				{
					array[0] = "FOSC";
					array[1] = "FWDT";
					array[2] = "FBORPOR";
					array[3] = "FBS";
					array[4] = "FSS";
					array[5] = "FGS";
					array[6] = "FICD";
					num2 = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr / 2U);
					num3 = 2;
				}
			}
			else if (PICkitFunctions.FamilyIsPIC32())
			{
				array[0] = "DEVCFG2L";
				array[1] = "DEVCFG2H";
				array[2] = "DEVCFG1L";
				array[3] = "DEVCFG1H";
				array[4] = "DEVCFG0L";
				array[5] = "DEVCFG0H";
				num2 = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr;
				num3 = 2;
			}
			for (int k = 0; k < 9; k++)
			{
				if (k < (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords)
				{
					ushort num4 = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[k];
					this.configWords[k].name.Text = array[k];
					this.configWords[k].addr.Text = string.Format("{0:X}", num2 + k * num3);
					ushort num5 = (ushort)PICkitFunctions.DeviceBuffers.ConfigWords[k];
					if (this.displayMask == 0)
					{
						num5 &= PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[k];
					}
					else if (this.displayMask == 1)
					{
						num5 |= (ushort)~PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[k];
					}
					num5 &= (ushort)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
					this.configWords[k].value.Text = string.Format("{0:X4}", num5);
					ushort num6 = 1;
					for (int l = 0; l < 16; l++)
					{
						if ((PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[k] & num6) > 0)
						{
							if ((PICkitFunctions.DeviceBuffers.ConfigWords[k] & (uint)num6) > 0U)
							{
								this.configWords[k].bits[l].Text = "1";
							}
							else
							{
								this.configWords[k].bits[l].Text = "0";
							}
						}
						else
						{
							this.configWords[k].bits[l].Text = "-";
							this.configWords[k].bits[l].BackColor = SystemColors.Control;
							this.configWords[k].bits[l].Enabled = false;
						}
						num6 = (ushort)(num6 << 1);
					}
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[k] == 0)
					{
						this.configWords[k].configPanel.Enabled = false;
					}
				}
				else
				{
					this.configWords[k].configPanel.Visible = false;
				}
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00020550 File Offset: 0x0001F550
		private void textBox1_15_Click(object sender, EventArgs e)
		{
			int num = (int)((TextBox)sender).Tag;
			int num2 = num / 16;
			num %= 16;
			uint num3 = 1U;
			num3 <<= num;
			if (this.configWords[num2].bits[num].Text == "1")
			{
				this.configWords[num2].bits[num].Text = "0";
				PICkitFunctions.DeviceBuffers.ConfigWords[num2] &= ~num3;
			}
			else
			{
				this.configWords[num2].bits[num].Text = "1";
				PICkitFunctions.DeviceBuffers.ConfigWords[num2] |= num3;
			}
			if (this.configWords[num2].bits[num].ForeColor == Color.Crimson)
			{
				this.configWords[num2].bits[num].ForeColor = SystemColors.WindowText;
			}
			else
			{
				this.configWords[num2].bits[num].ForeColor = Color.Crimson;
			}
			ushort num4 = (ushort)PICkitFunctions.DeviceBuffers.ConfigWords[num2];
			if (this.displayMask == 0)
			{
				num4 &= PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[num2];
			}
			else if (this.displayMask == 1)
			{
				num4 |= (ushort)~PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[num2];
			}
			num4 &= (ushort)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
			this.configWords[num2].value.Text = string.Format("{0:X4}", num4);
			this.configWords[num2].value.ForeColor = SystemColors.ActiveCaption;
			for (int i = 0; i < 16; i++)
			{
				if (this.configWords[num2].bits[i].ForeColor == Color.Crimson)
				{
					this.configWords[num2].value.ForeColor = Color.Crimson;
					return;
				}
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00020790 File Offset: 0x0001F790
		private void DialogConfigEdit_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool flag = false;
			for (int i = 0; i < (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords; i++)
			{
				if (this.configWords[i].value.ForeColor == Color.Crimson)
				{
					flag = true;
					break;
				}
			}
			if (flag && !this.saveChanges)
			{
				if (MessageBox.Show("Are you sure you wish to exit\nwithout saving your Configuration edits?\n\nClick 'OK' to exit without saving your changes.", "Exit without Saving?", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
				{
					e.Cancel = true;
				}
				for (int j = 0; j < (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords; j++)
				{
					PICkitFunctions.DeviceBuffers.ConfigWords[j] = this.configSaves[j];
				}
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00020846 File Offset: 0x0001F846
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.saveChanges = false;
			base.Close();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00020855 File Offset: 0x0001F855
		private void buttonSave_Click(object sender, EventArgs e)
		{
			this.saveChanges = true;
			FormPICkit2.configsEdited = true;
			base.Close();
		}

		// Token: 0x040000DD RID: 221
		private const int K_MAXCONFIGS = 9;

		// Token: 0x04000232 RID: 562
		public float ScalefactW = 1f;

		// Token: 0x04000233 RID: 563
		public float ScalefactH = 1f;

		// Token: 0x04000234 RID: 564
		private int displayMask;

		// Token: 0x04000235 RID: 565
		private DialogConfigEdit.config[] configWords = new DialogConfigEdit.config[9];

		// Token: 0x04000236 RID: 566
		private uint[] configSaves = new uint[9];

		// Token: 0x04000237 RID: 567
		private bool saveChanges;

		// Token: 0x0200000F RID: 15
		private struct config
		{
			// Token: 0x04000238 RID: 568
			public Panel configPanel;

			// Token: 0x04000239 RID: 569
			public Label name;

			// Token: 0x0400023A RID: 570
			public Label addr;

			// Token: 0x0400023B RID: 571
			public Label value;

			// Token: 0x0400023C RID: 572
			public TextBox[] bits;
		}
	}
}
