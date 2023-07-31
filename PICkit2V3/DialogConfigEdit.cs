using System;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class DialogConfigEdit : Form
	{
		public DialogConfigEdit()
		{
			InitializeComponent();
			for (int i = 0; i < PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords; i++)
				configSaves[i] = PICkitFunctions.DeviceBuffers.ConfigWords[i];
			configWords[0].configPanel = panel1;
			configWords[0].name = labelName1;
			configWords[0].addr = labelAdr1;
			configWords[0].value = labelVal1;
			configWords[0].bits = new TextBox[16];
			configWords[0].bits[0] = textBox1_0;
			configWords[0].bits[1] = textBox1_1;
			configWords[0].bits[2] = textBox1_2;
			configWords[0].bits[3] = textBox1_3;
			configWords[0].bits[4] = textBox1_4;
			configWords[0].bits[5] = textBox1_5;
			configWords[0].bits[6] = textBox1_6;
			configWords[0].bits[7] = textBox1_7;
			configWords[0].bits[8] = textBox1_8;
			configWords[0].bits[9] = textBox1_9;
			configWords[0].bits[10] = textBox1_10;
			configWords[0].bits[11] = textBox1_11;
			configWords[0].bits[12] = textBox1_12;
			configWords[0].bits[13] = textBox1_13;
			configWords[0].bits[14] = textBox1_14;
			configWords[0].bits[15] = textBox1_15;
			configWords[1].configPanel = panel2;
			configWords[1].name = labelName2;
			configWords[1].addr = labelAdr2;
			configWords[1].value = labelVal2;
			configWords[1].bits = new TextBox[16];
			configWords[1].bits[0] = textBox2_0;
			configWords[1].bits[1] = textBox2_1;
			configWords[1].bits[2] = textBox2_2;
			configWords[1].bits[3] = textBox2_3;
			configWords[1].bits[4] = textBox2_4;
			configWords[1].bits[5] = textBox2_5;
			configWords[1].bits[6] = textBox2_6;
			configWords[1].bits[7] = textBox2_7;
			configWords[1].bits[8] = textBox2_8;
			configWords[1].bits[9] = textBox2_9;
			configWords[1].bits[10] = textBox2_10;
			configWords[1].bits[11] = textBox2_11;
			configWords[1].bits[12] = textBox2_12;
			configWords[1].bits[13] = textBox2_13;
			configWords[1].bits[14] = textBox2_14;
			configWords[1].bits[15] = textBox2_15;
			configWords[2].configPanel = panel3;
			configWords[2].name = labelName3;
			configWords[2].addr = labelAdr3;
			configWords[2].value = labelVal3;
			configWords[2].bits = new TextBox[16];
			configWords[2].bits[0] = textBox3_0;
			configWords[2].bits[1] = textBox3_1;
			configWords[2].bits[2] = textBox3_2;
			configWords[2].bits[3] = textBox3_3;
			configWords[2].bits[4] = textBox3_4;
			configWords[2].bits[5] = textBox3_5;
			configWords[2].bits[6] = textBox3_6;
			configWords[2].bits[7] = textBox3_7;
			configWords[2].bits[8] = textBox3_8;
			configWords[2].bits[9] = textBox3_9;
			configWords[2].bits[10] = textBox3_10;
			configWords[2].bits[11] = textBox3_11;
			configWords[2].bits[12] = textBox3_12;
			configWords[2].bits[13] = textBox3_13;
			configWords[2].bits[14] = textBox3_14;
			configWords[2].bits[15] = textBox3_15;
			configWords[3].configPanel = panel4;
			configWords[3].name = labelName4;
			configWords[3].addr = labelAdr4;
			configWords[3].value = labelVal4;
			configWords[3].bits = new TextBox[16];
			configWords[3].bits[0] = textBox4_0;
			configWords[3].bits[1] = textBox4_1;
			configWords[3].bits[2] = textBox4_2;
			configWords[3].bits[3] = textBox4_3;
			configWords[3].bits[4] = textBox4_4;
			configWords[3].bits[5] = textBox4_5;
			configWords[3].bits[6] = textBox4_6;
			configWords[3].bits[7] = textBox4_7;
			configWords[3].bits[8] = textBox4_8;
			configWords[3].bits[9] = textBox4_9;
			configWords[3].bits[10] = textBox4_10;
			configWords[3].bits[11] = textBox4_11;
			configWords[3].bits[12] = textBox4_12;
			configWords[3].bits[13] = textBox4_13;
			configWords[3].bits[14] = textBox4_14;
			configWords[3].bits[15] = textBox4_15;
			configWords[4].configPanel = panel5;
			configWords[4].name = labelName5;
			configWords[4].addr = labelAdr5;
			configWords[4].value = labelVal5;
			configWords[4].bits = new TextBox[16];
			configWords[4].bits[0] = textBox5_0;
			configWords[4].bits[1] = textBox5_1;
			configWords[4].bits[2] = textBox5_2;
			configWords[4].bits[3] = textBox5_3;
			configWords[4].bits[4] = textBox5_4;
			configWords[4].bits[5] = textBox5_5;
			configWords[4].bits[6] = textBox5_6;
			configWords[4].bits[7] = textBox5_7;
			configWords[4].bits[8] = textBox5_8;
			configWords[4].bits[9] = textBox5_9;
			configWords[4].bits[10] = textBox5_10;
			configWords[4].bits[11] = textBox5_11;
			configWords[4].bits[12] = textBox5_12;
			configWords[4].bits[13] = textBox5_13;
			configWords[4].bits[14] = textBox5_14;
			configWords[4].bits[15] = textBox5_15;
			configWords[5].configPanel = panel6;
			configWords[5].name = labelName6;
			configWords[5].addr = labelAdr6;
			configWords[5].value = labelVal6;
			configWords[5].bits = new TextBox[16];
			configWords[5].bits[0] = textBox6_0;
			configWords[5].bits[1] = textBox6_1;
			configWords[5].bits[2] = textBox6_2;
			configWords[5].bits[3] = textBox6_3;
			configWords[5].bits[4] = textBox6_4;
			configWords[5].bits[5] = textBox6_5;
			configWords[5].bits[6] = textBox6_6;
			configWords[5].bits[7] = textBox6_7;
			configWords[5].bits[8] = textBox6_8;
			configWords[5].bits[9] = textBox6_9;
			configWords[5].bits[10] = textBox6_10;
			configWords[5].bits[11] = textBox6_11;
			configWords[5].bits[12] = textBox6_12;
			configWords[5].bits[13] = textBox6_13;
			configWords[5].bits[14] = textBox6_14;
			configWords[5].bits[15] = textBox6_15;
			configWords[6].configPanel = panel7;
			configWords[6].name = labelName7;
			configWords[6].addr = labelAdr7;
			configWords[6].value = labelVal7;
			configWords[6].bits = new TextBox[16];
			configWords[6].bits[0] = textBox7_0;
			configWords[6].bits[1] = textBox7_1;
			configWords[6].bits[2] = textBox7_2;
			configWords[6].bits[3] = textBox7_3;
			configWords[6].bits[4] = textBox7_4;
			configWords[6].bits[5] = textBox7_5;
			configWords[6].bits[6] = textBox7_6;
			configWords[6].bits[7] = textBox7_7;
			configWords[6].bits[8] = textBox7_8;
			configWords[6].bits[9] = textBox7_9;
			configWords[6].bits[10] = textBox7_10;
			configWords[6].bits[11] = textBox7_11;
			configWords[6].bits[12] = textBox7_12;
			configWords[6].bits[13] = textBox7_13;
			configWords[6].bits[14] = textBox7_14;
			configWords[6].bits[15] = textBox7_15;
			configWords[7].configPanel = panel8;
			configWords[7].name = labelName8;
			configWords[7].addr = labelAdr8;
			configWords[7].value = labelVal8;
			configWords[7].bits = new TextBox[16];
			configWords[7].bits[0] = textBox8_0;
			configWords[7].bits[1] = textBox8_1;
			configWords[7].bits[2] = textBox8_2;
			configWords[7].bits[3] = textBox8_3;
			configWords[7].bits[4] = textBox8_4;
			configWords[7].bits[5] = textBox8_5;
			configWords[7].bits[6] = textBox8_6;
			configWords[7].bits[7] = textBox8_7;
			configWords[7].bits[8] = textBox8_8;
			configWords[7].bits[9] = textBox8_9;
			configWords[7].bits[10] = textBox8_10;
			configWords[7].bits[11] = textBox8_11;
			configWords[7].bits[12] = textBox8_12;
			configWords[7].bits[13] = textBox8_13;
			configWords[7].bits[14] = textBox8_14;
			configWords[7].bits[15] = textBox8_15;
			configWords[8].configPanel = panel9;
			configWords[8].name = labelName9;
			configWords[8].addr = labelAdr9;
			configWords[8].value = labelVal9;
			configWords[8].bits = new TextBox[16];
			configWords[8].bits[0] = textBox9_0;
			configWords[8].bits[1] = textBox9_1;
			configWords[8].bits[2] = textBox9_2;
			configWords[8].bits[3] = textBox9_3;
			configWords[8].bits[4] = textBox9_4;
			configWords[8].bits[5] = textBox9_5;
			configWords[8].bits[6] = textBox9_6;
			configWords[8].bits[7] = textBox9_7;
			configWords[8].bits[8] = textBox9_8;
			configWords[8].bits[9] = textBox9_9;
			configWords[8].bits[10] = textBox9_10;
			configWords[8].bits[11] = textBox9_11;
			configWords[8].bits[12] = textBox9_12;
			configWords[8].bits[13] = textBox9_13;
			configWords[8].bits[14] = textBox9_14;
			configWords[8].bits[15] = textBox9_15;
			int num = 0;
			for (int j = 0; j < 9; j++)
			{
				for (int k = 0; k < 16; k++)
					configWords[j].bits[k].Tag = num++;
			}
		}

		public void SetDisplayMask(int option)
		{
			displayMask = option;
			Redraw();
		}

		private void Redraw()
		{
			int num = 9 - PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords;
			num *= (int)(48 * scalefactH);
			num -= (int)(24 * scalefactH);
			Size = new Size(Size.Width, Size.Height - num);
			string[] array = new string[9];
			for (int i = 1; i <= array.Length; i++)
				array[i - 1] = string.Format("CONFIG{0:G}", i);
			int num2 = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr / 2);
			int num3 = 1;
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords == 1)
				array[0] = "CONFIG";
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue == 65535)
			{
				num2 = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr;
				num3 = 2;
			}
			else if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue == 16777215)
			{
				if (PICkitFunctions.FamilyIsPIC24FJ())
				{
					for (int j = 1; j <= PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords; j++)
						array[PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords - j] = string.Format("CW{0:G}", j);
					num2 = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr / 2);
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
					num2 = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr / 2);
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
					num2 = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr / 2);
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
				if (k < PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords)
				{
					ushort num4 = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[k];
					configWords[k].name.Text = array[k];
					configWords[k].addr.Text = string.Format("{0:X}", num2 + k * num3);
					ushort num5 = (ushort)PICkitFunctions.DeviceBuffers.ConfigWords[k];
					if (displayMask == 0)
						num5 &= PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[k];
					else if (displayMask == 1)
						num5 |= (ushort)~PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[k];
					num5 &= (ushort)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
					configWords[k].value.Text = string.Format("{0:X4}", num5);
					ushort num6 = 1;
					for (int l = 0; l < 16; l++)
					{
						if ((PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[k] & num6) > 0)
						{
							if ((PICkitFunctions.DeviceBuffers.ConfigWords[k] & num6) > 0)
								configWords[k].bits[l].Text = "1";
							else
								configWords[k].bits[l].Text = "0";
						}
						else
						{
							configWords[k].bits[l].Text = "-";
							configWords[k].bits[l].BackColor = SystemColors.Control;
							configWords[k].bits[l].Enabled = false;
						}
						num6 = (ushort)(num6 << 1);
					}
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[k] == 0)
						configWords[k].configPanel.Enabled = false;
				}
				else
					configWords[k].configPanel.Visible = false;
			}
		}

		private void TextBox1_15_Click(object sender, EventArgs e)
		{
			int num = (int)((TextBox)sender).Tag;
			int num2 = num / 16;
			num %= 16;
			uint num3 = 1U;
			num3 <<= num;
			if (configWords[num2].bits[num].Text == "1")
			{
				configWords[num2].bits[num].Text = "0";
				PICkitFunctions.DeviceBuffers.ConfigWords[num2] &= ~num3;
			}
			else
			{
				configWords[num2].bits[num].Text = "1";
				PICkitFunctions.DeviceBuffers.ConfigWords[num2] |= num3;
			}
			if (configWords[num2].bits[num].ForeColor == Color.Crimson)
				configWords[num2].bits[num].ForeColor = SystemColors.WindowText;
			else
				configWords[num2].bits[num].ForeColor = Color.Crimson;
			ushort num4 = (ushort)PICkitFunctions.DeviceBuffers.ConfigWords[num2];
			if (displayMask == 0)
				num4 &= PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[num2];
			else if (displayMask == 1)
				num4 |= (ushort)~PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[num2];
			num4 &= (ushort)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
			configWords[num2].value.Text = string.Format("{0:X4}", num4);
			configWords[num2].value.ForeColor = SystemColors.ActiveCaption;
			for (int i = 0; i < 16; i++)
			{
				if (configWords[num2].bits[i].ForeColor == Color.Crimson)
				{
					configWords[num2].value.ForeColor = Color.Crimson;
					return;
				}
			}
		}

		private void DialogConfigEdit_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool flag = false;
			for (int i = 0; i < PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords; i++)
			{
				if (configWords[i].value.ForeColor == Color.Crimson)
				{
					flag = true;
					break;
				}
			}
			if (flag && !saveChanges)
			{
				if (MessageBox.Show("Are you sure you wish to exit\nwithout saving your Configuration edits?\n\nClick 'OK' to exit without saving your changes.", "Exit without Saving?", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
					e.Cancel = true;
				for (int j = 0; j < PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords; j++)
					PICkitFunctions.DeviceBuffers.ConfigWords[j] = this.configSaves[j];
			}
		}

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			saveChanges = false;
			Close();
		}

		private void ButtonSave_Click(object sender, EventArgs e)
		{
			saveChanges = true;
			FormPICkit2.configsEdited = true;
			Close();
		}

        public float scalefactW = 1f;
		public float scalefactH = 1f;
		private int displayMask;
		private readonly Config[] configWords = new Config[9];
		private readonly uint[] configSaves = new uint[9];
		private bool saveChanges;
		private struct Config
		{
			public Panel configPanel;
			public Label name;
			public Label addr;
			public Label value;
			public TextBox[] bits;
		}
	}
}
