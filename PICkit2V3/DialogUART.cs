using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class DialogUART : Form
	{
		public DialogUART()
		{
			InitializeComponent();
			KeyPress += OnKeyPress;
			baudList = new BaudTable[7];
			baudList[0].baudRate = "300";
			baudList[0].baudValue = 45554U;
			baudList[1].baudRate = "1200";
			baudList[1].baudValue = 60554U;
			baudList[2].baudRate = "2400";
			baudList[2].baudValue = 63054U;
			baudList[3].baudRate = "4800";
			baudList[3].baudValue = 64304U;
			baudList[4].baudRate = "9600";
			baudList[4].baudValue = 64929U;
			baudList[5].baudRate = "19200";
			baudList[5].baudValue = 65242U;
			baudList[6].baudRate = "38400";
			baudList[6].baudValue = 65398U;
			BuildBaudList();
		}

		public string GetBaudRate()
		{
			return comboBoxBaud.SelectedItem.ToString();
		}

		public bool IsHexMode()
		{
			return radioButtonHex.Checked;
		}

		public string GetStringMacro(int macroNum)
		{
			if (macroNum == 2)
				return textBoxString2.Text;

			if (macroNum == 3)
				return textBoxString3.Text;

			if (macroNum == 4)
				return textBoxString4.Text;

			return textBoxString1.Text;
		}

		public bool GetAppendCRLF()
		{
			return checkBoxCRLF.Checked;
		}

		public bool GetWrap()
		{
			return checkBoxWrap.Checked;
		}

		public bool GetEcho()
		{
			return checkBoxEcho.Checked;
		}

		public void SetBaudRate(string baudRate)
		{
			for (int i = 0; i < baudList.Length; i++)
			{
				if (baudRate == comboBoxBaud.Items[i].ToString())
				{
					comboBoxBaud.SelectedIndex = i;
					return;
				}
				if (i + 1 == baudList.Length)
				{
					comboBoxBaud.Items.Add(baudRate);
					comboBoxBaud.SelectedIndex = i + 3;
				}
			}
		}

		public void SetStringMacro(string macro, int macroNum)
		{
			if (macroNum == 2)
			{
				textBoxString2.Text = macro;
				hex1Length = macro.Length;
				return;
			}
			if (macroNum == 3)
			{
				textBoxString3.Text = macro;
				hex2Length = macro.Length;
				return;
			}
			if (macroNum == 4)
			{
				textBoxString4.Text = macro;
				hex3Length = macro.Length;
				return;
			}
			textBoxString1.Text = macro;
			hex4Length = macro.Length;
		}

		public void SetModeHex()
		{
			radioButtonHex.Checked = true;
		}

		public void ClearAppendCRLF()
		{
			checkBoxCRLF.Checked = false;
		}

		public void ClearWrap()
		{
			checkBoxWrap.Checked = false;
		}

		public void ClearEcho()
		{
			checkBoxEcho.Checked = false;
		}

		public void SetVddBox(bool enable, bool check)
		{
			checkBoxVDD.Enabled = enable;
			checkBoxVDD.Checked = check;
		}

		private void BuildBaudList()
		{
			for (int i = 0; i < baudList.Length; i++)
				comboBoxBaud.Items.Add(baudList[i].baudRate);

			comboBoxBaud.Items.Add("Custom...");
			comboBoxBaud.SelectedIndex = 0;
		}

		private void ButtonExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void DialogUART_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (logFile != null)
				CloseLogFile();

			timerPollForData.Enabled = false;
			PICkitFunctions.ExitUARTMode();
			radioButtonConnect.Checked = false;
			radioButtonDisconnect.Checked = true;
			comboBoxBaud.Enabled = true;
			buttonString1.Enabled = false;
			buttonString2.Enabled = false;
			buttonString3.Enabled = false;
			buttonString4.Enabled = false;
			panelVdd.Enabled = true;
		}

		public void OnKeyPress(object sender, KeyPressEventArgs e)
		{
			string text = "0123456789ABCDEF";
			if (textBoxString1.ContainsFocus | textBoxString2.ContainsFocus | textBoxString3.ContainsFocus | textBoxString4.ContainsFocus)
				return;

			if (e.KeyChar == '\u0003' || e.KeyChar == '\u0018')
			{
				textBoxDisplay.Copy();
				return;
			}

			if (radioButtonDisconnect.Checked)
				return;

			textBoxDisplay.Focus();
			if (radioButtonHex.Checked)
			{
				string text2 = e.KeyChar.ToString();
				text2 = text2.ToUpper();
				if (text2.IndexOfAny(text.ToCharArray()) != 0)
				{
					labelTypeHex.Text = "Type Hex : ";
					labelTypeHex.Visible = false;
					return;
				}

				if (labelTypeHex.Visible)
				{
					string text3 = labelTypeHex.Text.Substring(11, 1) + text2;
					labelTypeHex.Text = "Type Hex : ";
					labelTypeHex.Visible = false;
					byte[] array = new byte[]
					{
						(byte)Utilities.Convert_Value_To_Int("0x" + text3)
					};
					text3 = "TX:  " + text3 + "\r\n";
					textBoxDisplay.AppendText(text3);
					textBoxDisplay.SelectionStart = textBoxDisplay.Text.Length;
					textBoxDisplay.ScrollToCaret();
                    logFile?.Write(text3);

					PICkitFunctions.DataDownload(array, 0, array.Length);
					return;
				}
				labelTypeHex.Text = "Type Hex : " + text2 + "_";
				labelTypeHex.Visible = true;
				return;
			}
			else
			{
				if (e.KeyChar == '\u0016')
				{
					textBoxDisplay.SelectionStart = textBoxDisplay.Text.Length;
                    TextBox textBox = new TextBox
                    {
                        Multiline = true
                    };
                    textBox.Paste();
					do
					{
						int num = textBox.Text.Length;
						if (num > 60)
							num = 60;

						SendString(textBox.Text.Substring(0, num), false);
						textBox.Text = textBox.Text.Substring(num);
						float num2 = float.Parse(comboBoxBaud.SelectedItem.ToString());
						num2 = 1f / num2 * 12 * (float)num;
						num2 *= 1000;
						Thread.Sleep((int)num2);
					}
					while (textBox.Text.Length > 0);
					textBox.Dispose();
					return;
				}

				string text4 = e.KeyChar.ToString();
				if (text4 == "\r")
					text4 = "\r\n";

				SendString(text4, false);
				return;
			}
		}

		private void RadioButtonConnect_Click_1(object sender, EventArgs e)
		{
			if (!radioButtonConnect.Checked)
			{
				if (comboBoxBaud.SelectedIndex == 0)
				{
					MessageBox.Show("Please Select a Baud Rate.");
					return;
				}
				uint num = 0;
				for (int i = 0; i < baudList.Length; i++)
				{
					if (comboBoxBaud.SelectedItem.ToString() == baudList[i].baudRate)
					{
						num = baudList[i].baudValue;
						break;
					}
					if (i + 1 == baudList.Length)
					{
						try
						{
							float num2 = float.Parse(comboBoxBaud.SelectedItem.ToString());
							num2 = (1f / num2 - 3E-06f) / 1.6667E-07f;
							num = 65536 - (uint)num2;
						}
						catch
						{
							MessageBox.Show("Error with Baud setting.");
							return;
						}
					}
				}
				panelVdd.Enabled = false;
				PICkitFunctions.EnterUARTMode(num);
				radioButtonConnect.Checked = true;
				radioButtonDisconnect.Checked = false;
				buttonString1.Enabled = true;
				buttonString2.Enabled = true;
				buttonString3.Enabled = true;
				buttonString4.Enabled = true;
				comboBoxBaud.Enabled = false;

				if (num < 60554)
					timerPollForData.Interval = 75;
				else
					timerPollForData.Interval = 15;
				timerPollForData.Enabled = true;
			}
		}

		private void RadioButtonDisconnect_Click(object sender, EventArgs e)
		{
			if (!radioButtonDisconnect.Checked)
			{
				radioButtonConnect.Checked = false;
				radioButtonDisconnect.Checked = true;
				PICkitFunctions.ExitUARTMode();
				comboBoxBaud.Enabled = true;
				timerPollForData.Enabled = false;
				buttonString1.Enabled = false;
				buttonString2.Enabled = false;
				buttonString3.Enabled = false;
				buttonString4.Enabled = false;
				panelVdd.Enabled = true;
				labelTypeHex.Text = "Type Hex : ";
				labelTypeHex.Visible = false;
			}
		}

		private void ButtonClearScreen_Click(object sender, EventArgs e)
		{
			textBoxDisplay.Text = "";
		}

		private void TimerPollForData_Tick(object sender, EventArgs e)
		{
			PICkitFunctions.UploadData();
			if (PICkitFunctions.Usb_read_array[1] > 0)
			{
				string text = "";
				if (radioButtonASCII.Checked)
					text = Encoding.ASCII.GetString(PICkitFunctions.Usb_read_array, 2, PICkitFunctions.Usb_read_array[1]);
				else
				{
					if (newRX)
					{
						text = "RX:  ";
						newRX = false;
					}

					for (int i = 0; i < (int)PICkitFunctions.Usb_read_array[1]; i++)
						text += string.Format("{0:X2} ", PICkitFunctions.Usb_read_array[i + 2]);
				}
				logFile?.Write(text);

				textBoxDisplay.AppendText(text);
				while (textBoxDisplay.Text.Length > 16400)
				{
					int num = textBoxDisplay.Text.IndexOf("\r\n") + 2;
					if (num == 1)
						num = textBoxDisplay.Text.Length - 16000;

					textBoxDisplay.Text = textBoxDisplay.Text.Substring(num);
				}
				textBoxDisplay.SelectionStart = textBoxDisplay.Text.Length;
				textBoxDisplay.ScrollToCaret();
				return;
			}
			if (!newRX && radioButtonHex.Checked)
			{
				textBoxDisplay.AppendText("\r\n");
				logFile?.Write("\r\n");

				textBoxDisplay.SelectionStart = textBoxDisplay.Text.Length;
				textBoxDisplay.ScrollToCaret();
			}
			newRX = true;
		}

        private void TextBoxString1_TextChanged(object sender, EventArgs e)
		{
			if (textBoxString1.Text.Length > 60 && radioButtonASCII.Checked)
			{
				textBoxString1.Text = textBoxString1.Text.Substring(0, 60);
				textBoxString1.SelectionStart = 60;
			}

			if (radioButtonHex.Checked)
				FormatHexString(textBoxString1, ref hex1Length);
		}

		private void TextBoxString2_TextChanged(object sender, EventArgs e)
		{
			if (textBoxString2.Text.Length > 60 && radioButtonASCII.Checked)
			{
				textBoxString2.Text = textBoxString2.Text.Substring(0, 60);
				textBoxString2.SelectionStart = 60;
			}

			if (radioButtonHex.Checked)
				FormatHexString(textBoxString2, ref hex2Length);
		}

		private void TextBoxString3_TextChanged(object sender, EventArgs e)
		{
			if (textBoxString3.Text.Length > 60 && radioButtonASCII.Checked)
			{
				textBoxString3.Text = textBoxString3.Text.Substring(0, 60);
				textBoxString3.SelectionStart = 60;
			}

			if (radioButtonHex.Checked)
				FormatHexString(textBoxString3, ref hex3Length);
		}

		private void TextBoxString4_TextChanged(object sender, EventArgs e)
		{
			if (textBoxString4.Text.Length > 60 && radioButtonASCII.Checked)
			{
				textBoxString4.Text = textBoxString4.Text.Substring(0, 60);
				textBoxString4.SelectionStart = 60;
			}
			if (radioButtonHex.Checked)
			{
				FormatHexString(textBoxString4, ref hex4Length);
			}
		}

		private void FormatHexString(TextBox textBoxToFormat, ref int priorLength)
		{
			string text = textBoxToFormat.Text.ToUpper();
			text = text.Replace(" ", "");
			string text2 = "";
			for (int i = 0; i < text.Length; i++)
			{
				if (!char.IsNumber(text, i) && text[i] != 'A' && text[i] != 'B' && text[i] != 'C' && text[i] != 'D' && text[i] != 'E' && text[i] != 'F')
					text2 += '0';
				else
					text2 += text[i];

				if ((i + 1) % 2 == 0)
					text2 += " ";
			}

			if (text2.Length > 143)
				text2 = text2.Substring(0, 143);

			int num = textBoxToFormat.SelectionStart;
			if (num > 0 && num <= text2.Length && num < textBoxToFormat.Text.Length && textBoxToFormat.Text[num] == ' ' && text2[num - 1] == ' ')
				num++;
			else if (num >= textBoxToFormat.Text.Length && priorLength < textBoxToFormat.Text.Length)
				num = text2.Length;

			textBoxToFormat.Text = text2;
			textBoxToFormat.SelectionStart = num;
			priorLength = textBoxToFormat.Text.Length;
		}

		private void ButtonString1_Click(object sender, EventArgs e)
		{
			SendString(textBoxString1.Text, checkBoxCRLF.Checked);
		}

		private void ButtonString2_Click(object sender, EventArgs e)
		{
			SendString(textBoxString2.Text, checkBoxCRLF.Checked);
		}

		private void ButtonString3_Click(object sender, EventArgs e)
		{
			SendString(textBoxString3.Text, checkBoxCRLF.Checked);
		}

		private void ButtonString4_Click(object sender, EventArgs e)
		{
			SendString(textBoxString4.Text, checkBoxCRLF.Checked);
		}

		private void SendString(string dataString, bool appendCRLF)
		{
			if (dataString.Length == 0)
				return;

			if (radioButtonASCII.Checked)
			{
				if (appendCRLF)
					dataString += "\r\n";

				if (checkBoxEcho.Checked)
				{
					textBoxDisplay.AppendText(dataString);
					textBoxDisplay.SelectionStart = textBoxDisplay.Text.Length;
					textBoxDisplay.ScrollToCaret();
				}

				logFile?.Write(dataString);

				byte[] bytes = Encoding.Unicode.GetBytes(dataString);
				byte[] array = Encoding.Convert(Encoding.Unicode, Encoding.ASCII, bytes);
				PICkitFunctions.DataDownload(array, 0, array.Length);
				return;
			}

			int num;
			if (dataString.Length > 142)
				num = 48;
			else
			{
				num = dataString.Length / 3;
				dataString = dataString.Substring(0, num * 3);
			}

			byte[] array2 = new byte[num];
			for (int i = 0; i < num; i++)
				array2[i] = (byte)Utilities.Convert_Value_To_Int("0x" + dataString.Substring(3 * i, 2));

			dataString = "TX:  " + dataString + "\r\n";
			textBoxDisplay.AppendText(dataString);
			textBoxDisplay.SelectionStart = textBoxDisplay.Text.Length;
			textBoxDisplay.ScrollToCaret();
			logFile?.Write(dataString);

			PICkitFunctions.DataDownload(array2, 0, array2.Length);
		}

		private void ButtonLog_Click(object sender, EventArgs e)
		{
			if (logFile == null)
			{
				saveFileDialogLogFile.ShowDialog();
				return;
			}
			CloseLogFile();
		}

		private void CloseLogFile()
		{
			logFile.Close();
			logFile = null;
			buttonLog.Text = "Log to File";
			buttonLog.BackColor = SystemColors.ControlLight;
		}

		private void SaveFileDialogLogFile_FileOk(object sender, CancelEventArgs e)
		{
			logFile = new StreamWriter(saveFileDialogLogFile.FileName);
			buttonLog.Text = "Logging Data...";
			buttonLog.BackColor = Color.Green;
		}

		private void RadioButtonASCII_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonASCII.Checked)
			{
				checkBoxCRLF.Visible = true;
				checkBoxEcho.Enabled = true;
				labelTypeHex.Visible = false;
				labelTypeHex.Text = "Type Hex : ";
				labelMacros.Text = "String Macros:";
				textBoxString1.Text = ConvertHexSequenceToStringMacro(textBoxString1.Text);
				textBoxString2.Text = ConvertHexSequenceToStringMacro(textBoxString2.Text);
				textBoxString3.Text = ConvertHexSequenceToStringMacro(textBoxString3.Text);
				textBoxString4.Text = ConvertHexSequenceToStringMacro(textBoxString4.Text);
				if (textBoxDisplay.Text.Length > 0 && textBoxDisplay.Text[textBoxDisplay.Text.Length - 1] != '\n')
					textBoxDisplay.AppendText("\r\n");
			}
		}

		private void RadioButtonHex_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonHex.Checked)
			{
				checkBoxCRLF.Visible = false;
				checkBoxEcho.Enabled = false;
				labelTypeHex.Text = "Type Hex : ";
				labelTypeHex.Visible = false;
				labelMacros.Text = "Send Hex Sequences:";
				textBoxString1.Text = ConvertStringMacroToHexSequence(textBoxString1.Text);
				textBoxString2.Text = ConvertStringMacroToHexSequence(textBoxString2.Text);
				textBoxString3.Text = ConvertStringMacroToHexSequence(textBoxString3.Text);
				textBoxString4.Text = ConvertStringMacroToHexSequence(textBoxString4.Text);
				if (textBoxDisplay.Text.Length > 0 && textBoxDisplay.Text[textBoxDisplay.Text.Length - 1] != '\n')
					textBoxDisplay.AppendText("\r\n");
			}
		}

		private string ConvertHexSequenceToStringMacro(string hexSeq)
		{
			int num;
			if (hexSeq.Length > 142)
				num = 48;
			else
				num = hexSeq.Length / 3;

			byte[] array = new byte[num];
			for (int i = 0; i < num; i++)
				array[i] = (byte)Utilities.Convert_Value_To_Int("0x" + hexSeq.Substring(3 * i, 2));

			return Encoding.ASCII.GetString(array, 0, array.Length);
		}

		private string ConvertStringMacroToHexSequence(string stringMacro)
		{
			if (stringMacro.Length > 48)
				stringMacro = stringMacro.Substring(0, 48);

			byte[] bytes = Encoding.Unicode.GetBytes(stringMacro);
			byte[] array = Encoding.Convert(Encoding.Unicode, Encoding.ASCII, bytes);
			string text = "";
			for (int i = 0; i < array.Length; i++)
				text += string.Format("{0:X2} ", array[i]);

			return text;
		}

		private void CheckBoxWrap_CheckedChanged(object sender, EventArgs e)
		{
			textBoxDisplay.WordWrap = checkBoxWrap.Checked;
		}

		private void ComboBoxBaud_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxBaud.SelectedItem.ToString() == "Custom...")
			{
				DialogCustomBaud dialogCustomBaud = new DialogCustomBaud();
				dialogCustomBaud.ShowDialog();
				if (CustomBaud == "")
				{
					comboBoxBaud.SelectedIndex = 0;
					return;
				}
				if (comboBoxBaud.Items.Count != comboBoxBaud.SelectedIndex + 1)
					comboBoxBaud.Items.RemoveAt(comboBoxBaud.SelectedIndex + 1);

				comboBoxBaud.Items.Add(CustomBaud);
				comboBoxBaud.SelectedIndex++;
			}
		}

		private void PictureBoxHelp_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(FormPICkit2.homeDirectory + "\\PICkit2 User Guide 51553E.pdf");
			}
			catch
			{
				MessageBox.Show("Unable to open User's Guide.");
			}
		}

		private void CheckBoxVDD_Click(object sender, EventArgs e)
		{
			vddCallback(true, checkBoxVDD.Checked);
		}

		private void TextBoxDisplay_Leave(object sender, EventArgs e)
		{
			labelTypeHex.Visible = false;
			labelTypeHex.Text = "Type Hex : ";
		}

        public DelegateVddCallback vddCallback;
		public static string CustomBaud = "";
		private readonly BaudTable[] baudList;
		private StreamWriter logFile;
		private bool newRX = true;
		private int hex1Length;
		private int hex2Length;
		private int hex3Length;
		private int hex4Length;
		private struct BaudTable
		{
			public string baudRate;
			public uint baudValue;
		}
	}
}
