using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class DialogLogic : Form
	{
		public DialogLogic()
		{
			InitializeComponent();
			KeyPress += DialogLogic_KeyPress;
			for (int i = 0; i < 1024; i++)
				sampleArray[i] = 0;
			InitLogicIO();
			comboBoxCh1Trig.SelectedIndex = 0;
			comboBoxCh2Trig.SelectedIndex = 0;
			comboBoxCh3Trig.SelectedIndex = 0;
			comboBoxSampleRate.SelectedIndex = 0;
			labelCursor1Val.Text = "0 us";
			labelCursor2Val.Text = "0 us";
			labelCursorDeltaVal.Text = "0 us";
			UpdateDisplay();
		}

		public bool GetModeAnalyzer()
		{
			return radioButtonAnalyzer.Checked;
		}

		public void SetModeAnalyzer()
		{
			radioButtonLogicIO.Checked = false;
			radioButtonAnalyzer.Checked = true;
		}

		public int GetZoom()
		{
			return lastZoomLevel;
		}

		public void SetZoom(int zoom)
		{
			lastZoomLevel = zoom;
			if (zoom != 1)
			{
				radioButtonZoom1x.Checked = false;
				if (zoom == 0)
					radioButtonZoom05.Checked = true;
				else if (zoom == 2)
					radioButton2x.Checked = true;
				else if (zoom == 3)
					radioButton4x.Checked = true;
				UpdateDisplay();
			}
		}

		public int GetCh1Trigger()
		{
			return comboBoxCh1Trig.SelectedIndex;
		}

		public void SetCh1Trigger(int trig)
		{
			comboBoxCh1Trig.SelectedIndex = trig;
		}

		public int GetCh2Trigger()
		{
			return comboBoxCh2Trig.SelectedIndex;
		}

		public void SetCh2Trigger(int trig)
		{
			comboBoxCh2Trig.SelectedIndex = trig;
		}

		public int GetCh3Trigger()
		{
			return comboBoxCh3Trig.SelectedIndex;
		}

		public void SetCh3Trigger(int trig)
		{
			comboBoxCh3Trig.SelectedIndex = trig;
		}

		public int GetTrigCount()
		{
			return int.Parse(textBox1.Text);
		}

		public void SetTrigCount(int count)
		{
			textBox1.Text = count.ToString();
		}

		public int GetSampleRate()
		{
			return comboBoxSampleRate.SelectedIndex;
		}

		public void SetSampleRate(int rate)
		{
			comboBoxSampleRate.SelectedIndex = rate;
		}

		public int GetTriggerPosition()
		{
			int result = 0;
			if (radioButtonTrigMid.Checked)
				result = 1;
			else if (radioButtonTrigEnd.Checked)
				result = 2;
			else if (radioButtonTrigDly1.Checked)
				result = 3;
			else if (radioButtonTrigDly2.Checked)
				result = 4;
			else if (radioButtonTrigDly3.Checked)
				result = 5;
			return result;
		}

		public void SetTriggerPosition(int trigpos)
		{
			if (trigpos > 0)
			{
				radioButtonTrigStart.Checked = false;
				if (trigpos == 1)
					radioButtonTrigMid.Checked = true;
				else if (trigpos == 2)
					radioButtonTrigEnd.Checked = true;
				else if (trigpos == 3)
					radioButtonTrigDly1.Checked = true;
				else if (trigpos == 4)
					radioButtonTrigDly2.Checked = true;
				else if (trigpos == 5)
					radioButtonTrigDly3.Checked = true;
				UpdateDisplay();
			}
		}

		public bool GetCursorsEnabled()
		{
			return checkBoxCursors.Checked;
		}

		public void SetCursorsEnabled(bool enable)
		{
			checkBoxCursors.Checked = enable;
		}

		public int GetXCursorPos()
		{
			return cursor1Pos;
		}

		public void SetXCursorPos(int pos)
		{
			cursor1Pos = pos;
			UpdateDisplay();
		}

		public int GetYCursorPos()
		{
			return cursor2Pos;
		}

		public void SetYCursorPos(int pos)
		{
			cursor2Pos = pos;
			UpdateDisplay();
		}

		public void SetVddBox(bool enable, bool check)
		{
			checkBoxVDD.Enabled = enable;
			checkBoxVDD.Checked = check;
		}

		private void UpdateDisplay()
		{
			Bitmap bitmap = DrawSampleData(lastZoomLevel, lastTrigPos);
			pictureBoxDisplay.Width = bitmap.Width;
			if (!checkBoxCursors.Checked)
				pictureBoxDisplay.Image = bitmap;

			lastDrawnDisplay = bitmap;
			UpdateDisplayCursors();
			float num = sampleRates[lastSampleRate];
			num *= 50;
			if (lastZoomLevel == 0)
				num *= 2;
			else if (lastZoomLevel == 2)
				num /= 2;
			else if (lastZoomLevel == 3)
				num /= 4;

			string str = "s";
			if (num < 0.001f)
			{
				num *= 1000000f;
				str = "us";
			}
			else if (num < 1)
			{
				num *= 1000f;
				str = "ms";
			}
			labelTimeScale.Text = string.Format("{0:G} ", num) + str + " / Div";
		}

		private Bitmap DrawSampleData(int zoom, int triggerPos)
		{
			int num = 100;
			int num2 = 1;
			if (zoom == 0)
			{
				triggerPos /= 2;
				num2 = 0;
			}
			else if (zoom == 2)
			{
				triggerPos *= 2;
				num2 = 2;
			}
			else if (zoom == 3)
			{
				triggerPos *= 4;
				num2 = 4;
			}
			Bitmap displayGraph = GetDisplayGraph(zoom);
			Graphics graphics = Graphics.FromImage(displayGraph);
			SolidBrush solidBrush = new SolidBrush(Color.Red);
			graphics.FillRectangle(solidBrush, triggerPos, 0, 1, num);
			graphics.FillRectangle(solidBrush, triggerPos - 2, 4, 5, 2);
			graphics.FillRectangle(solidBrush, triggerPos - 2, num - 5, 5, 2);
			if (lastTrigDelay > 0)
			{
				solidBrush = new SolidBrush(Color.White);
				graphics.FillPolygon(solidBrush, new Point[]
				{
					new Point(triggerPos - 4, 0),
					new Point(triggerPos - 4, 8),
					new Point(triggerPos - 9, 4)
				});
				float num3 = sampleRates[lastSampleRate] * 1000 * lastTrigDelay;
				Font font = new Font(FontFamily.GenericSansSerif, 7, FontStyle.Bold);
				string str = "s";
				if (num3 < 0.001f)
				{
					num3 *= 1000000;
					str = "us";
				}
				else if (num3 < 1)
				{
					num3 *= 1000;
					str = "ms";
				}
				graphics.DrawString(string.Format("DLY {0:G} ", num3) + str, font, solidBrush, (float)(triggerPos + 3), -2);
				font.Dispose();
			}
			solidBrush.Dispose();
			SolidBrush solidBrush2 = new SolidBrush(Color.LimeGreen);
			int num4 = 0;
			byte b = sampleArray[0];
			if (num2 > 0)
			{
				for (int i = 0; i < 1024; i++)
				{
					if (((sampleArray[i] & 1) ^ (b & 1)) > 0)
					{
						graphics.FillRectangle(solidBrush2, num4, 10, 1, 20);
						if (num2 > 1)
						{
							if ((sampleArray[i] & 1) == 0)
								graphics.FillRectangle(solidBrush2, num4 + 1, 27, num2 - 1, 3);
							else
								graphics.FillRectangle(solidBrush2, num4 + 1, 10, num2 - 1, 1);
						}
					}
					else if ((sampleArray[i] & 1) == 0)
						graphics.FillRectangle(solidBrush2, num4, 27, num2, 3);
					else
						graphics.FillRectangle(solidBrush2, num4, 10, num2, 1);
					if (((sampleArray[i] & 2) ^ (b & 2)) > 0)
					{
						graphics.FillRectangle(solidBrush2, num4, 40, 1, 20);
						if (num2 > 1)
						{
							if ((sampleArray[i] & 2) == 0)
								graphics.FillRectangle(solidBrush2, num4 + 1, 57, num2 - 1, 3);
							else
								graphics.FillRectangle(solidBrush2, num4 + 1, 40, num2 - 1, 1);
						}
					}
					else if ((sampleArray[i] & 2) == 0)
						graphics.FillRectangle(solidBrush2, num4, 57, num2, 3);
					else
						graphics.FillRectangle(solidBrush2, num4, 40, num2, 1);
					if (((sampleArray[i] & 4) ^ (b & 4)) > 0)
					{
						graphics.FillRectangle(solidBrush2, num4, 70, 1, 20);
						if (num2 > 1)
						{
							if ((sampleArray[i] & 4) == 0)
								graphics.FillRectangle(solidBrush2, num4 + 1, 87, num2 - 1, 3);
							else
								graphics.FillRectangle(solidBrush2, num4 + 1, 70, num2 - 1, 1);
						}
					}
					else if ((sampleArray[i] & 4) == 0)
						graphics.FillRectangle(solidBrush2, num4, 87, num2, 3);
					else
						graphics.FillRectangle(solidBrush2, num4, 70, num2, 1);
					num4 += num2;
					b = sampleArray[i];
				}
			}
			else
			{
				for (int j = 0; j < 1024; j += 2)
				{
					if (((sampleArray[j] & 1) ^ (b & 1)) > 0 || ((sampleArray[j + 1] & 1) ^ (b & 1)) > 0)
						graphics.FillRectangle(solidBrush2, num4, 10, 1, 20);
					else if ((sampleArray[j] & 1) == 0)
						graphics.FillRectangle(solidBrush2, num4, 27, 1, 3);
					else
						graphics.FillRectangle(solidBrush2, num4, 10, 1, 1);
					if (((sampleArray[j] & 2) ^ (b & 2)) > 0 || ((sampleArray[j + 1] & 2) ^ (b & 2)) > 0)
						graphics.FillRectangle(solidBrush2, num4, 40, 1, 20);
					else if ((sampleArray[j] & 2) == 0)
						graphics.FillRectangle(solidBrush2, num4, 57, 1, 3);
					else
						graphics.FillRectangle(solidBrush2, num4, 40, 1, 1);
					if (((sampleArray[j] & 4) ^ (b & 4)) > 0 || ((sampleArray[j + 1] & 4) ^ (b & 4)) > 0)
						graphics.FillRectangle(solidBrush2, num4, 70, 1, 20);
					else if ((sampleArray[j] & 4) == 0)
						graphics.FillRectangle(solidBrush2, num4, 87, 1, 3);
					else
						graphics.FillRectangle(solidBrush2, num4, 70, 1, 1);
					num4++;
					b = sampleArray[j + 1];
				}
			}
			graphics.Dispose();
			solidBrush2.Dispose();
			return displayGraph;
		}

		private Bitmap GetDisplayGraph(int zoom)
		{
			int num = 1024;
			int num2 = 100;
			if (zoom == 0)
				num = 512;
			else if (zoom == 2)
				num = 2048;
			else if (zoom == 3)
				num = 4096;
			Bitmap bitmap = new Bitmap(num, num2, PixelFormat.Format16bppRgb555);
			Graphics graphics = Graphics.FromImage(bitmap);
			SolidBrush solidBrush = new SolidBrush(Color.Black);
			graphics.FillRectangle(solidBrush, 0, 0, num, num2);
			solidBrush.Dispose();
			SolidBrush solidBrush2 = new SolidBrush(Color.DarkGray);
			for (int i = 0; i < num - 50; i += 50)
			{
				graphics.FillRectangle(solidBrush2, i, 0, 1, num2);
				graphics.FillRectangle(solidBrush2, i + 10, num2 - 7, 1, 7);
				graphics.FillRectangle(solidBrush2, i + 20, num2 - 7, 1, 7);
				graphics.FillRectangle(solidBrush2, i + 30, num2 - 7, 1, 7);
				graphics.FillRectangle(solidBrush2, i + 40, num2 - 7, 1, 7);
				graphics.FillRectangle(solidBrush2, i + 10, 0, 1, 7);
				graphics.FillRectangle(solidBrush2, i + 20, 0, 1, 7);
				graphics.FillRectangle(solidBrush2, i + 30, 0, 1, 7);
				graphics.FillRectangle(solidBrush2, i + 40, 0, 1, 7);
			}
			int num3 = (num - 50) / 50 + 1;
			num3 *= 50;
			if (num3 < num)
				graphics.FillRectangle(solidBrush2, num3, 0, 1, num2);
			num3 += 10;
			if (num3 < num)
			{
				graphics.FillRectangle(solidBrush2, num3, num2 - 7, 1, 7);
				graphics.FillRectangle(solidBrush2, num3, 0, 1, 7);
			}
			num3 += 10;
			if (num3 < num)
			{
				graphics.FillRectangle(solidBrush2, num3, num2 - 7, 1, 7);
				graphics.FillRectangle(solidBrush2, num3, 0, 1, 7);
			}
			num3 += 10;
			if (num3 < num)
			{
				graphics.FillRectangle(solidBrush2, num3, num2 - 7, 1, 7);
				graphics.FillRectangle(solidBrush2, num3, 0, 1, 7);
			}
			num3 += 10;
			if (num3 < num)
			{
				graphics.FillRectangle(solidBrush2, num3, num2 - 7, 1, 7);
				graphics.FillRectangle(solidBrush2, num3, 0, 1, 7);
			}
			solidBrush2.Dispose();
			graphics.Dispose();
			return bitmap;
		}

		private void RadioButtonZoom05_Click(object sender, EventArgs e)
		{
			if (radioButtonZoom05.Checked)
			{
				if (lastZoomLevel == 1)
				{
					cursor1Pos /= 2;
					cursor2Pos /= 2;
				}
				else if (lastZoomLevel == 2)
				{
					cursor1Pos /= 4;
					cursor2Pos /= 4;
				}
				else if (lastZoomLevel == 3)
				{
					cursor1Pos /= 8;
					cursor2Pos /= 8;
				}
				lastZoomLevel = 0;
			}
			else if (radioButtonZoom1x.Checked)
			{
				if (lastZoomLevel == 0)
				{
					cursor1Pos *= 2;
					cursor2Pos *= 2;
				}
				else if (lastZoomLevel == 2)
				{
					cursor1Pos /= 2;
					cursor2Pos /= 2;
				}
				else if (lastZoomLevel == 3)
				{
					cursor1Pos /= 4;
					cursor2Pos /= 4;
				}
				lastZoomLevel = 1;
			}
			else if (radioButton2x.Checked)
			{
				if (lastZoomLevel == 0)
				{
					cursor1Pos *= 4;
					cursor2Pos *= 4;
				}
				else if (lastZoomLevel == 1)
				{
					cursor1Pos *= 2;
					cursor2Pos *= 2;
				}
				else if (lastZoomLevel == 3)
				{
					cursor1Pos /= 2;
					cursor2Pos /= 2;
				}
				lastZoomLevel = 2;
			}
			else if (radioButton4x.Checked)
			{
				if (lastZoomLevel == 0)
				{
					cursor1Pos *= 8;
					cursor2Pos *= 8;
				}
				else if (lastZoomLevel == 1)
				{
					cursor1Pos *= 4;
					cursor2Pos *= 4;
				}
				else if (lastZoomLevel == 2)
				{
					cursor1Pos *= 2;
					cursor2Pos *= 2;
				}
				lastZoomLevel = 3;
			}
			UpdateDisplay();
		}

		private void CheckBoxCursors_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxCursors.Checked)
			{
				labelCursor1.Enabled = true;
				labelCursor1Val.Enabled = true;
				labelCursor2.Enabled = true;
				labelCursor2Val.Enabled = true;
				labelCursorDelta.Enabled = true;
				labelCursorDeltaVal.Enabled = true;
				UpdateDisplay();
				return;
			}
			labelCursor1.Enabled = false;
			labelCursor1Val.Enabled = false;
			labelCursor2.Enabled = false;
			labelCursor2Val.Enabled = false;
			labelCursorDelta.Enabled = false;
			labelCursorDeltaVal.Enabled = false;
			UpdateDisplay();
		}

		private void PictureBoxDisplay_MouseDown(object sender, MouseEventArgs e)
		{
			if (checkBoxCursors.Checked)
			{
				if (e.Button == MouseButtons.Left)
				{
					cursor1Pos = e.X;
				}
				else if (e.Button == MouseButtons.Right)
				{
					cursor2Pos = e.X;
				}
			}
			UpdateDisplayCursors();
		}

		private void UpdateDisplayCursors()
		{
			if (!checkBoxCursors.Checked)
				return;

			int height = lastDrawnDisplay.Height;
			int num = 1;
			int num2 = cursor1Pos;
			int num3 = cursor2Pos;
			if (lastZoomLevel == 0)
			{
				num2 *= 2;
				num3 *= 2;
			}
			else if (lastZoomLevel == 2)
			{
				num = 2;
				num2 /= 2;
				num3 /= 2;
				cursor1Pos -= cursor1Pos % 2;
				cursor2Pos -= cursor2Pos % 2;
			}
			else if (lastZoomLevel == 3)
			{
				num = 4;
				num2 /= 4;
				num3 /= 4;
				cursor1Pos -= cursor1Pos % 4;
				cursor2Pos -= cursor2Pos % 4;
			}
			Bitmap image = (Bitmap)lastDrawnDisplay.Clone();
			Graphics graphics = Graphics.FromImage(image);
			Font font = new Font(FontFamily.GenericSansSerif, 7);
			SolidBrush solidBrush = new SolidBrush(Color.DodgerBlue);
			graphics.FillRectangle(solidBrush, cursor1Pos, 0, num, height);
			graphics.DrawString("X", font, solidBrush, cursor1Pos - 10, 29);
			float num4 = (num2 - lastTrigPos) * sampleRates[lastSampleRate];
			num4 += sampleRates[lastSampleRate] * 1000 * lastTrigDelay;
			string str = "s";
			int decimals = 3;
			if (Math.Abs(num4) < 0.001f)
			{
				num4 *= 1000000;
				str = "us";
				decimals = 0;
			}
			else if (Math.Abs(num4) < 1)
			{
				num4 *= 1000;
				str = "ms";
			}
			labelCursor1Val.Text = string.Format("{0:G} ", Math.Round((decimal)num4, decimals)) + str;
			solidBrush = new SolidBrush(Color.MediumOrchid);
			graphics.FillRectangle(solidBrush, cursor2Pos, 0, num, height);
			graphics.DrawString("Y", font, solidBrush, cursor2Pos + num + 2, 29);
			float num5 = (num3 - lastTrigPos) * sampleRates[lastSampleRate];
			num5 += sampleRates[lastSampleRate] * 1000f * lastTrigDelay;
			str = "s";
			if (Math.Abs(num5) < 0.001f)
			{
				num5 *= 1000000;
				str = "us";
			}
			else if (Math.Abs(num5) < 1)
			{
				num5 *= 1000;
				str = "ms";
			}
			labelCursor2Val.Text = string.Format("{0:G} ", Math.Round((decimal)num5, 3)) + str;
			pictureBoxDisplay.Image = image;
			num5 = (num3 - num2) * sampleRates[lastSampleRate];
			float num6 = 0;
			if (Math.Abs(num5) > 0)
				num6 = Math.Abs(1 / num5);
			str = "s";
			if (Math.Abs(num5) < 0.001f)
			{
				num5 *= 1000000;
				str = "us";
			}
			else if (Math.Abs(num5) < 1)
			{
				num5 *= 1000;
				str = "ms";
			}
			string str2 = "Hz)";
			if (num6 >= 10000)
			{
				num6 /= 1000;
				str2 = "kHz)";
			}
			labelCursorDeltaVal.Text = string.Format("{0:G} ", Math.Round((decimal)num5, 2)) + str + string.Format(" ({0:G} ", Math.Round((decimal)num6, 2)) + str2;
			solidBrush.Dispose();
			graphics.Dispose();
		}

		private void ComboBoxCh1Trig_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxCh1Trig.SelectedIndex > 2 && comboBoxCh2Trig.SelectedIndex > 2 && comboBoxCh1Trig.SelectedIndex != comboBoxCh2Trig.SelectedIndex)
			{
				MessageBox.Show("If more than one Channel is set to\nedge detect, all Channel edges must\nbe in the same direction.\n\n(Rising or Falling)");
				comboBoxCh1Trig.SelectedIndex = 0;
			}
			if (comboBoxCh1Trig.SelectedIndex > 2 && comboBoxCh3Trig.SelectedIndex > 2 && comboBoxCh1Trig.SelectedIndex != comboBoxCh3Trig.SelectedIndex)
			{
				MessageBox.Show("If more than one Channel is set to\nedge detect, all Channel edges must\nbe in the same direction.\n\n(Rising or Falling)");
				comboBoxCh1Trig.SelectedIndex = 0;
			}
		}

		private void ComboBoxCh2Trig_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxCh1Trig.SelectedIndex > 2 && comboBoxCh2Trig.SelectedIndex > 2 && comboBoxCh1Trig.SelectedIndex != comboBoxCh2Trig.SelectedIndex)
			{
				MessageBox.Show("If more than one Channel is set to\nedge detect, all Channel edges must\nbe in the same direction.\n\n(Rising or Falling)");
				comboBoxCh2Trig.SelectedIndex = 0;
			}
			if (comboBoxCh2Trig.SelectedIndex > 2 && comboBoxCh3Trig.SelectedIndex > 2 && comboBoxCh2Trig.SelectedIndex != comboBoxCh3Trig.SelectedIndex)
			{
				MessageBox.Show("If more than one Channel is set to\nedge detect, all Channel edges must\nbe in the same direction.\n\n(Rising or Falling)");
				comboBoxCh2Trig.SelectedIndex = 0;
			}
		}

		private void ComboBoxCh3Trig_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxCh1Trig.SelectedIndex > 2 && comboBoxCh3Trig.SelectedIndex > 2 && comboBoxCh1Trig.SelectedIndex != comboBoxCh3Trig.SelectedIndex)
			{
				MessageBox.Show("If more than one Channel is set to\nedge detect, all Channel edges must\nbe in the same direction.\n\n(Rising or Falling)");
				comboBoxCh3Trig.SelectedIndex = 0;
			}
			if (comboBoxCh2Trig.SelectedIndex > 2 && comboBoxCh3Trig.SelectedIndex > 2 && comboBoxCh2Trig.SelectedIndex != comboBoxCh3Trig.SelectedIndex)
			{
				MessageBox.Show("If more than one Channel is set to\nedge detect, all Channel edges must\nbe in the same direction.\n\n(Rising or Falling)");
				comboBoxCh3Trig.SelectedIndex = 0;
			}
		}

		private void TextBox1_Leave(object sender, EventArgs e)
		{
			uint num = uint.Parse(textBox1.Text);
			if (num == 0)
				num = 1;
			if (num > 256)
				num = 256;
			textBox1.Text = num.ToString();
		}

		private void ComboBoxSampleRate_SelectedIndexChanged(object sender, EventArgs e)
		{
			float num = sampleRates[comboBoxSampleRate.SelectedIndex];
			num = 0.5f / num;
			string str = "Hz";
			if (num >= 10000)
			{
				num /= 1000;
				str = "kHz";
			}
			labelAliasFreq.Text = string.Format("NOTE: Signals greater than {0:G} ", Math.Round((decimal)num, 1)) + str + " will alias.";
		}

		private void ButtonRun_Click(object sender, EventArgs e)
		{
			if (comboBoxCh1Trig.SelectedIndex == 0 && comboBoxCh2Trig.SelectedIndex == 0 && comboBoxCh3Trig.SelectedIndex == 0)
			{
				MessageBox.Show("At least one trigger condition\n must be defined.\n\nAll are set to Don't Care.", "PICkit 2 Logic Tool");
				return;
			}
			byte b = 1;
			if (comboBoxCh1Trig.SelectedIndex == 4 || comboBoxCh2Trig.SelectedIndex == 4 || comboBoxCh3Trig.SelectedIndex == 4)
				b = 0;
			byte b2 = 0;
			if (comboBoxCh1Trig.SelectedIndex > 0)
				b2 |= 4;
			if (comboBoxCh2Trig.SelectedIndex > 0)
				b2 |= 8;
			if (comboBoxCh3Trig.SelectedIndex > 0)
				b2 |= 16;
			byte b3 = 0;
			if (comboBoxCh1Trig.SelectedIndex == 1 || comboBoxCh1Trig.SelectedIndex == 3)
				b3 |= 4;
			if (comboBoxCh2Trig.SelectedIndex == 1 || comboBoxCh2Trig.SelectedIndex == 3)
				b3 |= 8;
			if (comboBoxCh3Trig.SelectedIndex == 1 || comboBoxCh3Trig.SelectedIndex == 3)
				b3 |= 16;
			byte b4 = 0;
			if (comboBoxCh1Trig.SelectedIndex == 4 || comboBoxCh1Trig.SelectedIndex == 3)
				b4 |= 4;
			if (comboBoxCh2Trig.SelectedIndex == 4 || comboBoxCh2Trig.SelectedIndex == 3)
				b4 |= 8;
			if (comboBoxCh3Trig.SelectedIndex == 4 || comboBoxCh3Trig.SelectedIndex == 3)
				b4 |= 16;
			byte b5 = byte.Parse(textBox1.Text);
			postTrigCount = 2;
			if (radioButtonTrigStart.Checked)
				postTrigCount = 973;
			else if (radioButtonTrigMid.Checked)
				postTrigCount = 523;
			else if (radioButtonTrigEnd.Checked)
				postTrigCount = 73;
			else if (radioButtonTrigDly1.Checked)
				postTrigCount = 1973;
			else if (radioButtonTrigDly2.Checked)
				postTrigCount = 2973;
			else if (radioButtonTrigDly3.Checked)
				postTrigCount = 3973;
			trigDialog = new DialogTrigger();
			AddOwnedForm(trigDialog);
			trigDialog.Show();
			byte[] array = new byte[9];
			int num = 0;
			array[num++] = 184;
			array[num++] = b;
			array[num++] = b2;
			array[num++] = b3;
			array[num++] = b4;
			array[num++] = b5;
			array[num++] = (byte)(postTrigCount - 1 & 255);
			array[num++] = (byte)(postTrigCount - 1 >> 8 & 255);
			array[num++] = sampleFactors[comboBoxSampleRate.SelectedIndex];
			PICkitFunctions.WriteUSB(array);
			timerRun.Enabled = true;
		}

		private void TimerRun_Tick(object sender, EventArgs e)
		{
			timerRun.Enabled = false;
			bool flag = PICkitFunctions.ReadUSB();
			Thread.Sleep(250);
			RemoveOwnedForm(trigDialog);
			trigDialog.Close();
			if (!flag)
				return;
			int num = PICkitFunctions.Usb_read_array[1] + PICkitFunctions.Usb_read_array[2] * 256;
			if ((num & 16384) > 0)
				return;
			lastSampleRate = comboBoxSampleRate.SelectedIndex;
			bool flag2 = (num & 32768) > 0;
			num &= 4095;
			num++;
			if (num == 2048)
				num = 1536;
			num -= 1536;
			byte[] array = new byte[512];
			byte[] array2 = new byte[3];
			int num2 = 0;
			array2[num2++] = 185;
			array2[num2++] = 0;
			array2[num2++] = 6;
			PICkitFunctions.WriteUSB(array2);
			PICkitFunctions.UploadDataNoLen();
			Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 0, 64);
			PICkitFunctions.UploadDataNoLen();
			Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 64, 64);
			num2 = 0;
			array2[num2++] = 185;
			array2[num2++] = 128;
			array2[num2++] = 6;
			PICkitFunctions.WriteUSB(array2);
			PICkitFunctions.UploadDataNoLen();
			Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 128, 64);
			PICkitFunctions.UploadDataNoLen();
			Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 192, 64);
			num2 = 0;
			array2[num2++] = 185;
			array2[num2++] = 0;
			array2[num2++] = 7;
			PICkitFunctions.WriteUSB(array2);
			PICkitFunctions.UploadDataNoLen();
			Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 256, 64);
			PICkitFunctions.UploadDataNoLen();
			Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 320, 64);
			num2 = 0;
			array2[num2++] = 185;
			array2[num2++] = 128;
			array2[num2++] = 7;
			PICkitFunctions.WriteUSB(array2);
			PICkitFunctions.UploadDataNoLen();
			Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 384, 64);
			PICkitFunctions.UploadDataNoLen();
			Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 448, 64);
			lastTrigPos = 1023 - postTrigCount % 1000;
			lastTrigDelay = postTrigCount / 1000;
			num += lastTrigPos / 2 + postTrigCount / 1000 * 12;
			if (lastTrigPos % 2 > 0)
			{
				flag2 = !flag2;
				if (flag2)
					num++;
			}
			num %= 512;
			for (int i = 0; i < sampleArray.Length; i++)
			{
				byte b = array[num];
				if (flag2)
				{
					num--;
					if (num < 0)
						num += 512;
					b = (byte)((b >> 4) + (b << 4));
				}
				b &= 28;
				sampleArray[i] = (byte)(b >> 2);
				flag2 = !flag2;
			}
			sampleArray[0] = sampleArray[1];
			UpdateDisplay();
		}

		private void ButtonSave_Click(object sender, EventArgs e)
		{
			Bitmap bitmap = (Bitmap)pictureBoxDisplay.Image.Clone();
			Graphics graphics = Graphics.FromImage(bitmap);
			Font font = new Font(FontFamily.GenericSansSerif, 7, FontStyle.Bold);
			SolidBrush solidBrush = new SolidBrush(Color.White);
			graphics.DrawString(labelTimeScale.Text, font, solidBrush, 5, 88);
			if (checkBoxCursors.Checked)
			{
				graphics.DrawString("X=" + labelCursor1Val.Text, font, solidBrush, 100, 88);
				graphics.DrawString("Y=" + labelCursor2Val.Text, font, solidBrush, 200, 88);
			}
			saveFileDialogDisplay.ShowDialog();
			try
			{
				bitmap.Save(saveFileDialogDisplay.FileName);
			}
			catch
			{
			}
			graphics.Dispose();
			solidBrush.Dispose();
			font.Dispose();
			bitmap.Dispose();
		}

		private void ButtonExit_Click(object sender, EventArgs e)
		{
			checkBoxIOEnable.Checked = false;
			Close();
		}

		private void RadioButtonAnalyzer_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonLogicIO.Checked)
			{
				panelLogicIO.Visible = true;
				panelAnalyzer.Visible = false;
				return;
			}
			panelLogicIO.Visible = false;
			checkBoxIOEnable.Checked = false;
			panelAnalyzer.Visible = true;
		}

		private void InitLogicIO()
		{
			radioButtonPin4In.Checked = true;
			radioButtonPin5In.Checked = true;
			radioButtonPin6In.Checked = true;
			textBoxPin1Out.Enabled = true;
			textBoxPin1Out.Text = "0";
			textBoxPin1Out.BackColor = Color.DarkRed;
			labelOut1Click.Visible = true;
			textBoxPin4In.Enabled = true;
			textBoxPin4In.Text = "0";
			textBoxPin4In.BackColor = Color.DarkBlue;
			textBoxPin4Out.Enabled = false;
			textBoxPin4Out.Text = "0";
			textBoxPin4Out.BackColor = SystemColors.Control;
			labelOut4Click.Visible = false;
			textBoxPin5In.Enabled = true;
			textBoxPin5In.Text = "0";
			textBoxPin5In.BackColor = Color.DarkBlue;
			textBoxPin5Out.Enabled = false;
			textBoxPin5Out.Text = "0";
			textBoxPin5Out.BackColor = SystemColors.Control;
			labelOut5Click.Visible = false;
			textBoxPin6In.Enabled = true;
			textBoxPin6In.Text = "0";
			textBoxPin6In.BackColor = Color.DarkBlue;
			textBoxPin6Out.Enabled = false;
			textBoxPin6Out.Text = "0";
			textBoxPin6Out.BackColor = SystemColors.Control;
			labelOut6Click.Visible = false;
		}

		private void TextBoxPin1Out_Click(object sender, EventArgs e)
		{
			PinOut(textBoxPin1Out);
		}

		private void TextBoxPin4Out_Click(object sender, EventArgs e)
		{
			PinOut(textBoxPin4Out);
		}

		private void TextBoxPin5Out_Click(object sender, EventArgs e)
		{
			PinOut(textBoxPin5Out);
		}

		private void TextBoxPin6Out_Click(object sender, EventArgs e)
		{
			PinOut(textBoxPin6Out);
		}

		private void PinOut(TextBox textBoxObject)
		{
			if (checkBoxIOEnable.Checked)
			{
				if (textBoxObject.Enabled)
				{
					if (textBoxObject.Text == "0")
					{
						textBoxObject.Text = "1";
						textBoxObject.BackColor = Color.Red;
					}
					else
					{
						textBoxObject.Text = "0";
						textBoxObject.BackColor = Color.DarkRed;
					}
					UpdateOutputs();
					return;
				}
			}
			else
				MessageBox.Show("Click the 'Enable IO' button\n to use the Logic IO.", "PICkit 2 Logic Tool");
		}

		private void CheckBoxIOEnable_CheckedChanged(object sender, EventArgs e)
		{
			if (!checkBoxIOEnable.Checked)
			{
				timerIO.Enabled = false;
				radioButtonPin4In.Enabled = false;
				radioButtonPin4Out.Enabled = false;
				radioButtonPin5In.Enabled = false;
				radioButtonPin5Out.Enabled = false;
				radioButtonPin6In.Enabled = false;
				radioButtonPin6Out.Enabled = false;
				ExitLogicIO();
				return;
			}
			if (!InitLogicPins())
			{
				MessageBox.Show("No valid voltage detected on\nPICkit 2 VDD pin.\n\nA valid voltage (2.5V to 5.0V)\nis required for the Logic IO.", "PICkit 2 Logic Tool");
				checkBoxIOEnable.Checked = false;
				return;
			}
			if (PICkitFunctions.PowerStatus() == Constants.PICkit2PWR.vdd_on)
				vddOn = true;
			else
				vddOn = false;
			radioButtonPin4In.Enabled = true;
			radioButtonPin4Out.Enabled = true;
			radioButtonPin5In.Enabled = true;
			radioButtonPin5Out.Enabled = true;
			radioButtonPin6In.Enabled = true;
			radioButtonPin6Out.Enabled = true;
			UpdateOutputs();
			timerIO.Enabled = true;
		}

		private void RadioButtonPin4Out_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonPin4Out.Checked)
			{
				textBoxPin4In.Text = "";
				textBoxPin4In.BackColor = SystemColors.Control;
				textBoxPin4In.Enabled = false;
				textBoxPin4Out.Enabled = true;
				if (textBoxPin4Out.Text == "0")
					textBoxPin4Out.BackColor = Color.DarkRed;
				else
					textBoxPin4Out.BackColor = Color.Red;
				labelOut4Click.Visible = true;
			}
			else
			{
				textBoxPin4In.Enabled = true;
				textBoxPin4In.Text = "0";
				textBoxPin4In.BackColor = Color.DarkBlue;
				textBoxPin4Out.Enabled = false;
				textBoxPin4Out.BackColor = SystemColors.Control;
				labelOut4Click.Visible = false;
			}
			UpdateOutputs();
		}

		private void RadioButtonPin5Out_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonPin5Out.Checked)
			{
				textBoxPin5In.Text = "";
				textBoxPin5In.BackColor = SystemColors.Control;
				textBoxPin5In.Enabled = false;
				textBoxPin5Out.Enabled = true;
				if (textBoxPin5Out.Text == "0")
					textBoxPin5Out.BackColor = Color.DarkRed;
				else
					textBoxPin5Out.BackColor = Color.Red;
				labelOut5Click.Visible = true;
			}
			else
			{
				textBoxPin5In.Enabled = true;
				textBoxPin5In.Text = "0";
				textBoxPin5In.BackColor = Color.DarkBlue;
				textBoxPin5Out.Enabled = false;
				textBoxPin5Out.BackColor = SystemColors.Control;
				labelOut5Click.Visible = false;
			}
			UpdateOutputs();
		}

		private void RadioButtonPin6Out_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonPin6Out.Checked)
			{
				textBoxPin6In.Text = "";
				textBoxPin6In.BackColor = SystemColors.Control;
				textBoxPin6In.Enabled = false;
				textBoxPin6Out.Enabled = true;
				if (textBoxPin6Out.Text == "0")
					textBoxPin6Out.BackColor = Color.DarkRed;
				else
					textBoxPin6Out.BackColor = Color.Red;
				labelOut6Click.Visible = true;
			}
			else
			{
				textBoxPin6In.Enabled = true;
				textBoxPin6In.Text = "0";
				textBoxPin6In.BackColor = Color.DarkBlue;
				textBoxPin6Out.Enabled = false;
				textBoxPin6Out.BackColor = SystemColors.Control;
				labelOut6Click.Visible = false;
			}
			UpdateOutputs();
		}

		private bool InitLogicPins()
		{
			float num = 0;
			float num2 = 0;
			if (PICkitFunctions.ReadPICkitVoltages(ref num, ref num2) && num >= 2.5f)
			{
				PICkitFunctions.SetVppVoltage(num, 0.7f);
				PICkitFunctions.SetVddVoltage(num, 0.85f);
				byte[] array = new byte[11];
				int num3 = 0;
				array[num3++] = 166;
				array[num3++] = 9;
				array[num3++] = 250;
				array[num3++] = 247;
				array[num3++] = 249;
				array[num3++] = 243;
				array[num3++] = 3;
				array[num3++] = 207;
				array[num3++] = 1;
				array[num3++] = 232;
				array[num3++] = 20;
				return PICkitFunctions.WriteUSB(array);
			}
			return false;
		}

		private bool ExitLogicIO()
		{
			byte[] array = new byte[9];
			int num = 0;
			array[num++] = 166;
			array[num++] = 7;
			array[num++] = 250;
			array[num++] = 247;
			array[num++] = 248;
			array[num++] = 243;
			array[num++] = 3;
			array[num++] = 207;
			array[num++] = 1;
			return PICkitFunctions.WriteUSB(array);
		}

		private bool UpdateOutputs()
		{
			byte b = 3;
			byte b2 = 1;
			if (radioButtonPin4Out.Checked)
			{
				b &= 253;
				if (textBoxPin4Out.Text == "1")
					b |= 8;
			}
			if (radioButtonPin5Out.Checked)
			{
				b &= 254;
				if (textBoxPin5Out.Text == "1")
					b |= 4;
			}
			if (radioButtonPin6Out.Checked)
			{
				b2 = 0;
				if (textBoxPin6Out.Text == "1")
					b2 = 2;
			}
			byte[] array = new byte[8];
			int num = 0;
			array[num++] = 166;
			array[num++] = 6;
			if (textBoxPin1Out.Text == "0")
			{
				array[num++] = 250;
				array[num++] = 247;
			}
			else
			{
				array[num++] = 246;
				array[num++] = 251;
			}
			array[num++] = 243;
			array[num++] = b;
			array[num++] = 207;
			array[num++] = b2;
			return PICkitFunctions.WriteUSB(array);
		}

		public void DialogLogic_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (panelLogicIO.Visible && checkBoxIOEnable.Checked)
			{
				if (e.KeyChar == 'a' || e.KeyChar == 'A')
				{
					PinOut(textBoxPin1Out);
					return;
				}
				if (e.KeyChar == 's' || e.KeyChar == 'S')
				{
					PinOut(textBoxPin4Out);
					return;
				}
				if (e.KeyChar == 'd' || e.KeyChar == 'D')
				{
					PinOut(textBoxPin5Out);
					return;
				}
				if (e.KeyChar == 'f' || e.KeyChar == 'F')
				{
					PinOut(textBoxPin6Out);
				}
			}
		}

		private void TimerIO_Tick(object sender, EventArgs e)
		{
			Constants.PICkit2PWR pickit2PWR = PICkitFunctions.PowerStatus();
			if (pickit2PWR == Constants.PICkit2PWR.vdderror || pickit2PWR == Constants.PICkit2PWR.vddvpperrors)
			{
				MessageBox.Show("PICkit 2 VDD voltage level error.\nVDD shut off: Disabling IO", "PICkit 2 Error");
				checkBoxIOEnable.Checked = false;
				return;
			}
			if (pickit2PWR == Constants.PICkit2PWR.vpperror)
			{
				if (vddOn)
				{
					MessageBox.Show("Voltage error on Pin 1:\nVDD was shut off.\n\nDisabling IO", "PICkit 2 Error");
					checkBoxIOEnable.Checked = false;
					return;
				}
				MessageBox.Show("Voltage error on Pin 1:\nState reset to '0'", "PICkit 2 Error");
				textBoxPin1Out.Text = "0";
				textBoxPin1Out.BackColor = Color.DarkRed;
			}
			byte[] array = new byte[5];
			int num = 0;
			array[num++] = 166;
			array[num++] = 2;
			array[num++] = 220;
			array[num++] = 206;
			array[num++] = 170;
			PICkitFunctions.WriteUSB(array);
			PICkitFunctions.ReadUSB();

			if ((PICkitFunctions.Usb_read_array[2] & 2) > 0)
				UpdateInputBox(textBoxPin4In, "1");
			else
				UpdateInputBox(textBoxPin4In, "0");

			if ((PICkitFunctions.Usb_read_array[2] & 1) > 0)
				UpdateInputBox(textBoxPin5In, "1");
			else
				UpdateInputBox(textBoxPin5In, "0");

			if ((PICkitFunctions.Usb_read_array[3] & 1) > 0)
			{
				UpdateInputBox(textBoxPin6In, "1");
				return;
			}
			UpdateInputBox(textBoxPin6In, "0");
		}

		private void UpdateInputBox(TextBox inputBox, string value)
		{
			if (inputBox.Enabled)
			{
				inputBox.Text = value;
				if (value == "1")
				{
					inputBox.BackColor = Color.DodgerBlue;
					return;
				}
				inputBox.BackColor = Color.DarkBlue;
			}
		}

		private void ButtonHelp_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(FormPICkit2.homeDirectory + "\\Logic Tool User Guide.pdf");
			}
			catch
			{
				MessageBox.Show("Unable to open Logic Tool User Guide.");
			}
		}

		private void CheckBoxVdd_Click(object sender, EventArgs e)
		{
			VddCallback(true, checkBoxVDD.Checked);
		}

        public DelegateVddCallback VddCallback;
        private readonly byte[] sampleArray = new byte[1024];
        private int lastZoomLevel = 1;
        private int lastSampleRate;
        private int lastTrigPos = 50;
        private int lastTrigDelay;
        private readonly float[] sampleRates = new float[]
        {
            1E-06f,
            2E-06f,
            4E-06f,
            1E-05f,
            2E-05f,
            4E-05f,
            0.0001f,
            0.0002f,
            0.001f
        };
        private readonly byte[] sampleFactors = new byte[]
        {
            0,
            1,
            3,
            9,
            19,
            39,
            99,
            199,
            byte.MaxValue
        };
        private Bitmap lastDrawnDisplay;
        private int cursor1Pos;
        private int cursor2Pos;
        private int postTrigCount = 1;
        private DialogTrigger trigDialog;
        private bool vddOn;
    }
}
