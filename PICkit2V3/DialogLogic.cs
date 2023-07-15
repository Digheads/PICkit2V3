using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace PICkit2V3
{
	// Token: 0x02000043 RID: 67
	public partial class DialogLogic : Form
	{
		// Token: 0x0600026C RID: 620 RVA: 0x0004C45C File Offset: 0x0004B45C
		public DialogLogic()
		{
			this.InitializeComponent();
			base.KeyPress += this.DialogLogic_KeyPress;
			for (int i = 0; i < 1024; i++)
			{
				this.sampleArray[i] = 0;
			}
			this.initLogicIO();
			this.comboBoxCh1Trig.SelectedIndex = 0;
			this.comboBoxCh2Trig.SelectedIndex = 0;
			this.comboBoxCh3Trig.SelectedIndex = 0;
			this.comboBoxSampleRate.SelectedIndex = 0;
			this.labelCursor1Val.Text = "0 us";
			this.labelCursor2Val.Text = "0 us";
			this.labelCursorDeltaVal.Text = "0 us";
			this.updateDisplay();
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0004C562 File Offset: 0x0004B562
		public bool getModeAnalyzer()
		{
			return this.radioButtonAnalyzer.Checked;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0004C56F File Offset: 0x0004B56F
		public void setModeAnalyzer()
		{
			this.radioButtonLogicIO.Checked = false;
			this.radioButtonAnalyzer.Checked = true;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0004C589 File Offset: 0x0004B589
		public int getZoom()
		{
			return this.lastZoomLevel;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0004C594 File Offset: 0x0004B594
		public void setZoom(int zoom)
		{
			this.lastZoomLevel = zoom;
			if (zoom != 1)
			{
				this.radioButtonZoom1x.Checked = false;
				if (zoom == 0)
				{
					this.radioButtonZoom05.Checked = true;
				}
				else if (zoom == 2)
				{
					this.radioButton2x.Checked = true;
				}
				else if (zoom == 3)
				{
					this.radioButton4x.Checked = true;
				}
				this.updateDisplay();
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0004C5F1 File Offset: 0x0004B5F1
		public int getCh1Trigger()
		{
			return this.comboBoxCh1Trig.SelectedIndex;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0004C5FE File Offset: 0x0004B5FE
		public void setCh1Trigger(int trig)
		{
			this.comboBoxCh1Trig.SelectedIndex = trig;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0004C60C File Offset: 0x0004B60C
		public int getCh2Trigger()
		{
			return this.comboBoxCh2Trig.SelectedIndex;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0004C619 File Offset: 0x0004B619
		public void setCh2Trigger(int trig)
		{
			this.comboBoxCh2Trig.SelectedIndex = trig;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0004C627 File Offset: 0x0004B627
		public int getCh3Trigger()
		{
			return this.comboBoxCh3Trig.SelectedIndex;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0004C634 File Offset: 0x0004B634
		public void setCh3Trigger(int trig)
		{
			this.comboBoxCh3Trig.SelectedIndex = trig;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0004C642 File Offset: 0x0004B642
		public int getTrigCount()
		{
			return int.Parse(this.textBox1.Text);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0004C654 File Offset: 0x0004B654
		public void setTrigCount(int count)
		{
			this.textBox1.Text = count.ToString();
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0004C668 File Offset: 0x0004B668
		public int getSampleRate()
		{
			return this.comboBoxSampleRate.SelectedIndex;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0004C675 File Offset: 0x0004B675
		public void setSampleRate(int rate)
		{
			this.comboBoxSampleRate.SelectedIndex = rate;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0004C684 File Offset: 0x0004B684
		public int getTriggerPosition()
		{
			int result = 0;
			if (this.radioButtonTrigMid.Checked)
			{
				result = 1;
			}
			else if (this.radioButtonTrigEnd.Checked)
			{
				result = 2;
			}
			else if (this.radioButtonTrigDly1.Checked)
			{
				result = 3;
			}
			else if (this.radioButtonTrigDly2.Checked)
			{
				result = 4;
			}
			else if (this.radioButtonTrigDly3.Checked)
			{
				result = 5;
			}
			return result;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0004C6E8 File Offset: 0x0004B6E8
		public void setTriggerPosition(int trigpos)
		{
			if (trigpos > 0)
			{
				this.radioButtonTrigStart.Checked = false;
				if (trigpos == 1)
				{
					this.radioButtonTrigMid.Checked = true;
				}
				else if (trigpos == 2)
				{
					this.radioButtonTrigEnd.Checked = true;
				}
				else if (trigpos == 3)
				{
					this.radioButtonTrigDly1.Checked = true;
				}
				else if (trigpos == 4)
				{
					this.radioButtonTrigDly2.Checked = true;
				}
				else if (trigpos == 5)
				{
					this.radioButtonTrigDly3.Checked = true;
				}
				this.updateDisplay();
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0004C763 File Offset: 0x0004B763
		public bool getCursorsEnabled()
		{
			return this.checkBoxCursors.Checked;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0004C770 File Offset: 0x0004B770
		public void setCursorsEnabled(bool enable)
		{
			this.checkBoxCursors.Checked = enable;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0004C77E File Offset: 0x0004B77E
		public int getXCursorPos()
		{
			return this.cursor1Pos;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0004C786 File Offset: 0x0004B786
		public void setXCursorPos(int pos)
		{
			this.cursor1Pos = pos;
			this.updateDisplay();
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0004C795 File Offset: 0x0004B795
		public int getYCursorPos()
		{
			return this.cursor2Pos;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0004C79D File Offset: 0x0004B79D
		public void setYCursorPos(int pos)
		{
			this.cursor2Pos = pos;
			this.updateDisplay();
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0004C7AC File Offset: 0x0004B7AC
		public void SetVddBox(bool enable, bool check)
		{
			this.checkBoxVDD.Enabled = enable;
			this.checkBoxVDD.Checked = check;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0004C7C8 File Offset: 0x0004B7C8
		private void updateDisplay()
		{
			Bitmap bitmap = this.drawSampleData(this.lastZoomLevel, this.lastTrigPos, this.firstSample);
			this.pictureBoxDisplay.Width = bitmap.Width;
			if (!this.checkBoxCursors.Checked)
			{
				this.pictureBoxDisplay.Image = bitmap;
			}
			this.lastDrawnDisplay = bitmap;
			this.updateDisplayCursors();
			float num = this.sampleRates[this.lastSampleRate];
			num *= 50f;
			if (this.lastZoomLevel == 0)
			{
				num *= 2f;
			}
			else if (this.lastZoomLevel == 2)
			{
				num /= 2f;
			}
			else if (this.lastZoomLevel == 3)
			{
				num /= 4f;
			}
			string str = "s";
			if (num < 0.001f)
			{
				num *= 1000000f;
				str = "us";
			}
			else if (num < 1f)
			{
				num *= 1000f;
				str = "ms";
			}
			this.labelTimeScale.Text = string.Format("{0:G} ", num) + str + " / Div";
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0004C8CC File Offset: 0x0004B8CC
		private Bitmap drawSampleData(int zoom, int triggerPos, int startPos)
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
			Bitmap displayGraph = this.getDisplayGraph(zoom);
			Graphics graphics = Graphics.FromImage(displayGraph);
			SolidBrush solidBrush = new SolidBrush(Color.Red);
			graphics.FillRectangle(solidBrush, triggerPos, 0, 1, num);
			graphics.FillRectangle(solidBrush, triggerPos - 2, 4, 5, 2);
			graphics.FillRectangle(solidBrush, triggerPos - 2, num - 5, 5, 2);
			if (this.lastTrigDelay > 0)
			{
				solidBrush = new SolidBrush(Color.White);
				graphics.FillPolygon(solidBrush, new Point[]
				{
					new Point(triggerPos - 4, 0),
					new Point(triggerPos - 4, 8),
					new Point(triggerPos - 9, 4)
				});
				float num3 = this.sampleRates[this.lastSampleRate] * 1000f * (float)this.lastTrigDelay;
				Font font = new Font(FontFamily.GenericSansSerif, 7f, FontStyle.Bold);
				string str = "s";
				if (num3 < 0.001f)
				{
					num3 *= 1000000f;
					str = "us";
				}
				else if (num3 < 1f)
				{
					num3 *= 1000f;
					str = "ms";
				}
				graphics.DrawString(string.Format("DLY {0:G} ", num3) + str, font, solidBrush, (float)(triggerPos + 3), -2f);
				font.Dispose();
			}
			solidBrush.Dispose();
			SolidBrush solidBrush2 = new SolidBrush(Color.LimeGreen);
			int num4 = 0;
			byte b = this.sampleArray[0];
			if (num2 > 0)
			{
				for (int i = 0; i < 1024; i++)
				{
					if (((this.sampleArray[i] & 1) ^ (b & 1)) > 0)
					{
						graphics.FillRectangle(solidBrush2, num4, 10, 1, 20);
						if (num2 > 1)
						{
							if ((this.sampleArray[i] & 1) == 0)
							{
								graphics.FillRectangle(solidBrush2, num4 + 1, 27, num2 - 1, 3);
							}
							else
							{
								graphics.FillRectangle(solidBrush2, num4 + 1, 10, num2 - 1, 1);
							}
						}
					}
					else if ((this.sampleArray[i] & 1) == 0)
					{
						graphics.FillRectangle(solidBrush2, num4, 27, num2, 3);
					}
					else
					{
						graphics.FillRectangle(solidBrush2, num4, 10, num2, 1);
					}
					if (((this.sampleArray[i] & 2) ^ (b & 2)) > 0)
					{
						graphics.FillRectangle(solidBrush2, num4, 40, 1, 20);
						if (num2 > 1)
						{
							if ((this.sampleArray[i] & 2) == 0)
							{
								graphics.FillRectangle(solidBrush2, num4 + 1, 57, num2 - 1, 3);
							}
							else
							{
								graphics.FillRectangle(solidBrush2, num4 + 1, 40, num2 - 1, 1);
							}
						}
					}
					else if ((this.sampleArray[i] & 2) == 0)
					{
						graphics.FillRectangle(solidBrush2, num4, 57, num2, 3);
					}
					else
					{
						graphics.FillRectangle(solidBrush2, num4, 40, num2, 1);
					}
					if (((this.sampleArray[i] & 4) ^ (b & 4)) > 0)
					{
						graphics.FillRectangle(solidBrush2, num4, 70, 1, 20);
						if (num2 > 1)
						{
							if ((this.sampleArray[i] & 4) == 0)
							{
								graphics.FillRectangle(solidBrush2, num4 + 1, 87, num2 - 1, 3);
							}
							else
							{
								graphics.FillRectangle(solidBrush2, num4 + 1, 70, num2 - 1, 1);
							}
						}
					}
					else if ((this.sampleArray[i] & 4) == 0)
					{
						graphics.FillRectangle(solidBrush2, num4, 87, num2, 3);
					}
					else
					{
						graphics.FillRectangle(solidBrush2, num4, 70, num2, 1);
					}
					num4 += num2;
					b = this.sampleArray[i];
				}
			}
			else
			{
				for (int j = 0; j < 1024; j += 2)
				{
					if (((this.sampleArray[j] & 1) ^ (b & 1)) > 0 || ((this.sampleArray[j + 1] & 1) ^ (b & 1)) > 0)
					{
						graphics.FillRectangle(solidBrush2, num4, 10, 1, 20);
					}
					else if ((this.sampleArray[j] & 1) == 0)
					{
						graphics.FillRectangle(solidBrush2, num4, 27, 1, 3);
					}
					else
					{
						graphics.FillRectangle(solidBrush2, num4, 10, 1, 1);
					}
					if (((this.sampleArray[j] & 2) ^ (b & 2)) > 0 || ((this.sampleArray[j + 1] & 2) ^ (b & 2)) > 0)
					{
						graphics.FillRectangle(solidBrush2, num4, 40, 1, 20);
					}
					else if ((this.sampleArray[j] & 2) == 0)
					{
						graphics.FillRectangle(solidBrush2, num4, 57, 1, 3);
					}
					else
					{
						graphics.FillRectangle(solidBrush2, num4, 40, 1, 1);
					}
					if (((this.sampleArray[j] & 4) ^ (b & 4)) > 0 || ((this.sampleArray[j + 1] & 4) ^ (b & 4)) > 0)
					{
						graphics.FillRectangle(solidBrush2, num4, 70, 1, 20);
					}
					else if ((this.sampleArray[j] & 4) == 0)
					{
						graphics.FillRectangle(solidBrush2, num4, 87, 1, 3);
					}
					else
					{
						graphics.FillRectangle(solidBrush2, num4, 70, 1, 1);
					}
					num4++;
					b = this.sampleArray[j + 1];
				}
			}
			graphics.Dispose();
			solidBrush2.Dispose();
			return displayGraph;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0004CDA4 File Offset: 0x0004BDA4
		private Bitmap getDisplayGraph(int zoom)
		{
			int num = 1024;
			int num2 = 100;
			if (zoom == 0)
			{
				num = 512;
			}
			else if (zoom == 2)
			{
				num = 2048;
			}
			else if (zoom == 3)
			{
				num = 4096;
			}
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
			{
				graphics.FillRectangle(solidBrush2, num3, 0, 1, num2);
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

		// Token: 0x06000287 RID: 647 RVA: 0x0004CF9C File Offset: 0x0004BF9C
		private void radioButtonZoom05_Click(object sender, EventArgs e)
		{
			if (this.radioButtonZoom05.Checked)
			{
				if (this.lastZoomLevel == 1)
				{
					this.cursor1Pos /= 2;
					this.cursor2Pos /= 2;
				}
				else if (this.lastZoomLevel == 2)
				{
					this.cursor1Pos /= 4;
					this.cursor2Pos /= 4;
				}
				else if (this.lastZoomLevel == 3)
				{
					this.cursor1Pos /= 8;
					this.cursor2Pos /= 8;
				}
				this.lastZoomLevel = 0;
			}
			else if (this.radioButtonZoom1x.Checked)
			{
				if (this.lastZoomLevel == 0)
				{
					this.cursor1Pos *= 2;
					this.cursor2Pos *= 2;
				}
				else if (this.lastZoomLevel == 2)
				{
					this.cursor1Pos /= 2;
					this.cursor2Pos /= 2;
				}
				else if (this.lastZoomLevel == 3)
				{
					this.cursor1Pos /= 4;
					this.cursor2Pos /= 4;
				}
				this.lastZoomLevel = 1;
			}
			else if (this.radioButton2x.Checked)
			{
				if (this.lastZoomLevel == 0)
				{
					this.cursor1Pos *= 4;
					this.cursor2Pos *= 4;
				}
				else if (this.lastZoomLevel == 1)
				{
					this.cursor1Pos *= 2;
					this.cursor2Pos *= 2;
				}
				else if (this.lastZoomLevel == 3)
				{
					this.cursor1Pos /= 2;
					this.cursor2Pos /= 2;
				}
				this.lastZoomLevel = 2;
			}
			else if (this.radioButton4x.Checked)
			{
				if (this.lastZoomLevel == 0)
				{
					this.cursor1Pos *= 8;
					this.cursor2Pos *= 8;
				}
				else if (this.lastZoomLevel == 1)
				{
					this.cursor1Pos *= 4;
					this.cursor2Pos *= 4;
				}
				else if (this.lastZoomLevel == 2)
				{
					this.cursor1Pos *= 2;
					this.cursor2Pos *= 2;
				}
				this.lastZoomLevel = 3;
			}
			this.updateDisplay();
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0004D1D8 File Offset: 0x0004C1D8
		private void checkBoxCursors_CheckedChanged(object sender, EventArgs e)
		{
			if (this.checkBoxCursors.Checked)
			{
				this.labelCursor1.Enabled = true;
				this.labelCursor1Val.Enabled = true;
				this.labelCursor2.Enabled = true;
				this.labelCursor2Val.Enabled = true;
				this.labelCursorDelta.Enabled = true;
				this.labelCursorDeltaVal.Enabled = true;
				this.updateDisplay();
				return;
			}
			this.labelCursor1.Enabled = false;
			this.labelCursor1Val.Enabled = false;
			this.labelCursor2.Enabled = false;
			this.labelCursor2Val.Enabled = false;
			this.labelCursorDelta.Enabled = false;
			this.labelCursorDeltaVal.Enabled = false;
			this.updateDisplay();
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0004D290 File Offset: 0x0004C290
		private void pictureBoxDisplay_MouseDown(object sender, MouseEventArgs e)
		{
			if (this.checkBoxCursors.Checked)
			{
				if (e.Button == MouseButtons.Left)
				{
					this.cursor1Pos = e.X;
				}
				else if (e.Button == MouseButtons.Right)
				{
					this.cursor2Pos = e.X;
				}
			}
			this.updateDisplayCursors();
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0004D2E4 File Offset: 0x0004C2E4
		private void updateDisplayCursors()
		{
			if (!this.checkBoxCursors.Checked)
			{
				return;
			}
			int height = this.lastDrawnDisplay.Height;
			int num = 1;
			int num2 = this.cursor1Pos;
			int num3 = this.cursor2Pos;
			if (this.lastZoomLevel == 0)
			{
				num2 *= 2;
				num3 *= 2;
			}
			else if (this.lastZoomLevel == 2)
			{
				num = 2;
				num2 /= 2;
				num3 /= 2;
				this.cursor1Pos -= this.cursor1Pos % 2;
				this.cursor2Pos -= this.cursor2Pos % 2;
			}
			else if (this.lastZoomLevel == 3)
			{
				num = 4;
				num2 /= 4;
				num3 /= 4;
				this.cursor1Pos -= this.cursor1Pos % 4;
				this.cursor2Pos -= this.cursor2Pos % 4;
			}
			Bitmap image = (Bitmap)this.lastDrawnDisplay.Clone();
			Graphics graphics = Graphics.FromImage(image);
			Font font = new Font(FontFamily.GenericSansSerif, 7f);
			SolidBrush solidBrush = new SolidBrush(Color.DodgerBlue);
			graphics.FillRectangle(solidBrush, this.cursor1Pos, 0, num, height);
			graphics.DrawString("X", font, solidBrush, (float)(this.cursor1Pos - 10), 29f);
			float num4 = (float)(num2 - this.lastTrigPos) * this.sampleRates[this.lastSampleRate];
			num4 += this.sampleRates[this.lastSampleRate] * 1000f * (float)this.lastTrigDelay;
			string str = "s";
			int decimals = 3;
			if (Math.Abs(num4) < 0.001f)
			{
				num4 *= 1000000f;
				str = "us";
				decimals = 0;
			}
			else if (Math.Abs(num4) < 1f)
			{
				num4 *= 1000f;
				str = "ms";
			}
			this.labelCursor1Val.Text = string.Format("{0:G} ", Math.Round((decimal)num4, decimals)) + str;
			solidBrush = new SolidBrush(Color.MediumOrchid);
			graphics.FillRectangle(solidBrush, this.cursor2Pos, 0, num, height);
			graphics.DrawString("Y", font, solidBrush, (float)(this.cursor2Pos + num + 2), 29f);
			float num5 = (float)(num3 - this.lastTrigPos) * this.sampleRates[this.lastSampleRate];
			num5 += this.sampleRates[this.lastSampleRate] * 1000f * (float)this.lastTrigDelay;
			str = "s";
			if (Math.Abs(num5) < 0.001f)
			{
				num5 *= 1000000f;
				str = "us";
			}
			else if (Math.Abs(num5) < 1f)
			{
				num5 *= 1000f;
				str = "ms";
			}
			this.labelCursor2Val.Text = string.Format("{0:G} ", Math.Round((decimal)num5, 3)) + str;
			this.pictureBoxDisplay.Image = image;
			num5 = (float)(num3 - num2) * this.sampleRates[this.lastSampleRate];
			float num6 = 0f;
			if (Math.Abs(num5) > 0f)
			{
				num6 = Math.Abs(1f / num5);
			}
			str = "s";
			if (Math.Abs(num5) < 0.001f)
			{
				num5 *= 1000000f;
				str = "us";
			}
			else if (Math.Abs(num5) < 1f)
			{
				num5 *= 1000f;
				str = "ms";
			}
			string str2 = "Hz)";
			if (num6 >= 10000f)
			{
				num6 /= 1000f;
				str2 = "kHz)";
			}
			this.labelCursorDeltaVal.Text = string.Format("{0:G} ", Math.Round((decimal)num5, 2)) + str + string.Format(" ({0:G} ", Math.Round((decimal)num6, 2)) + str2;
			solidBrush.Dispose();
			graphics.Dispose();
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0004D6C8 File Offset: 0x0004C6C8
		private void comboBoxCh1Trig_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.comboBoxCh1Trig.SelectedIndex > 2 && this.comboBoxCh2Trig.SelectedIndex > 2 && this.comboBoxCh1Trig.SelectedIndex != this.comboBoxCh2Trig.SelectedIndex)
			{
				MessageBox.Show("If more than one Channel is set to\nedge detect, all Channel edges must\nbe in the same direction.\n\n(Rising or Falling)");
				this.comboBoxCh1Trig.SelectedIndex = 0;
			}
			if (this.comboBoxCh1Trig.SelectedIndex > 2 && this.comboBoxCh3Trig.SelectedIndex > 2 && this.comboBoxCh1Trig.SelectedIndex != this.comboBoxCh3Trig.SelectedIndex)
			{
				MessageBox.Show("If more than one Channel is set to\nedge detect, all Channel edges must\nbe in the same direction.\n\n(Rising or Falling)");
				this.comboBoxCh1Trig.SelectedIndex = 0;
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0004D76C File Offset: 0x0004C76C
		private void comboBoxCh2Trig_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.comboBoxCh1Trig.SelectedIndex > 2 && this.comboBoxCh2Trig.SelectedIndex > 2 && this.comboBoxCh1Trig.SelectedIndex != this.comboBoxCh2Trig.SelectedIndex)
			{
				MessageBox.Show("If more than one Channel is set to\nedge detect, all Channel edges must\nbe in the same direction.\n\n(Rising or Falling)");
				this.comboBoxCh2Trig.SelectedIndex = 0;
			}
			if (this.comboBoxCh2Trig.SelectedIndex > 2 && this.comboBoxCh3Trig.SelectedIndex > 2 && this.comboBoxCh2Trig.SelectedIndex != this.comboBoxCh3Trig.SelectedIndex)
			{
				MessageBox.Show("If more than one Channel is set to\nedge detect, all Channel edges must\nbe in the same direction.\n\n(Rising or Falling)");
				this.comboBoxCh2Trig.SelectedIndex = 0;
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0004D810 File Offset: 0x0004C810
		private void comboBoxCh3Trig_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.comboBoxCh1Trig.SelectedIndex > 2 && this.comboBoxCh3Trig.SelectedIndex > 2 && this.comboBoxCh1Trig.SelectedIndex != this.comboBoxCh3Trig.SelectedIndex)
			{
				MessageBox.Show("If more than one Channel is set to\nedge detect, all Channel edges must\nbe in the same direction.\n\n(Rising or Falling)");
				this.comboBoxCh3Trig.SelectedIndex = 0;
			}
			if (this.comboBoxCh2Trig.SelectedIndex > 2 && this.comboBoxCh3Trig.SelectedIndex > 2 && this.comboBoxCh2Trig.SelectedIndex != this.comboBoxCh3Trig.SelectedIndex)
			{
				MessageBox.Show("If more than one Channel is set to\nedge detect, all Channel edges must\nbe in the same direction.\n\n(Rising or Falling)");
				this.comboBoxCh3Trig.SelectedIndex = 0;
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0004D8B4 File Offset: 0x0004C8B4
		private void textBox1_Leave(object sender, EventArgs e)
		{
			uint num = uint.Parse(this.textBox1.Text);
			if (num == 0U)
			{
				num = 1U;
			}
			if (num > 256U)
			{
				num = 256U;
			}
			this.textBox1.Text = num.ToString();
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0004D8F8 File Offset: 0x0004C8F8
		private void comboBoxSampleRate_SelectedIndexChanged(object sender, EventArgs e)
		{
			float num = this.sampleRates[this.comboBoxSampleRate.SelectedIndex];
			num = 0.5f / num;
			string str = "Hz";
			if (num >= 10000f)
			{
				num /= 1000f;
				str = "kHz";
			}
			this.labelAliasFreq.Text = string.Format("NOTE: Signals greater than {0:G} ", Math.Round((decimal)num, 1)) + str + " will alias.";
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0004D970 File Offset: 0x0004C970
		private void buttonRun_Click(object sender, EventArgs e)
		{
			if (this.comboBoxCh1Trig.SelectedIndex == 0 && this.comboBoxCh2Trig.SelectedIndex == 0 && this.comboBoxCh3Trig.SelectedIndex == 0)
			{
				MessageBox.Show("At least one trigger condition\n must be defined.\n\nAll are set to Don't Care.", "PICkit 2 Logic Tool");
				return;
			}
			byte b = 1;
			if (this.comboBoxCh1Trig.SelectedIndex == 4 || this.comboBoxCh2Trig.SelectedIndex == 4 || this.comboBoxCh3Trig.SelectedIndex == 4)
			{
				b = 0;
			}
			byte b2 = 0;
			if (this.comboBoxCh1Trig.SelectedIndex > 0)
			{
				b2 |= 4;
			}
			if (this.comboBoxCh2Trig.SelectedIndex > 0)
			{
				b2 |= 8;
			}
			if (this.comboBoxCh3Trig.SelectedIndex > 0)
			{
				b2 |= 16;
			}
			byte b3 = 0;
			if (this.comboBoxCh1Trig.SelectedIndex == 1 || this.comboBoxCh1Trig.SelectedIndex == 3)
			{
				b3 |= 4;
			}
			if (this.comboBoxCh2Trig.SelectedIndex == 1 || this.comboBoxCh2Trig.SelectedIndex == 3)
			{
				b3 |= 8;
			}
			if (this.comboBoxCh3Trig.SelectedIndex == 1 || this.comboBoxCh3Trig.SelectedIndex == 3)
			{
				b3 |= 16;
			}
			byte b4 = 0;
			if (this.comboBoxCh1Trig.SelectedIndex == 4 || this.comboBoxCh1Trig.SelectedIndex == 3)
			{
				b4 |= 4;
			}
			if (this.comboBoxCh2Trig.SelectedIndex == 4 || this.comboBoxCh2Trig.SelectedIndex == 3)
			{
				b4 |= 8;
			}
			if (this.comboBoxCh3Trig.SelectedIndex == 4 || this.comboBoxCh3Trig.SelectedIndex == 3)
			{
				b4 |= 16;
			}
			byte b5 = byte.Parse(this.textBox1.Text);
			this.postTrigCount = 2;
			if (this.radioButtonTrigStart.Checked)
			{
				this.postTrigCount = 973;
			}
			else if (this.radioButtonTrigMid.Checked)
			{
				this.postTrigCount = 523;
			}
			else if (this.radioButtonTrigEnd.Checked)
			{
				this.postTrigCount = 73;
			}
			else if (this.radioButtonTrigDly1.Checked)
			{
				this.postTrigCount = 1973;
			}
			else if (this.radioButtonTrigDly2.Checked)
			{
				this.postTrigCount = 2973;
			}
			else if (this.radioButtonTrigDly3.Checked)
			{
				this.postTrigCount = 3973;
			}
			this.trigDialog = new DialogTrigger();
			base.AddOwnedForm(this.trigDialog);
			this.trigDialog.Show();
			byte[] array = new byte[9];
			int num = 0;
			array[num++] = 184;
			array[num++] = b;
			array[num++] = b2;
			array[num++] = b3;
			array[num++] = b4;
			array[num++] = b5;
			array[num++] = (byte)(this.postTrigCount - 1 & 255);
			array[num++] = (byte)(this.postTrigCount - 1 >> 8 & 255);
			array[num++] = this.sampleFactors[this.comboBoxSampleRate.SelectedIndex];
			PICkitFunctions.writeUSB(array);
			this.timerRun.Enabled = true;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0004DC74 File Offset: 0x0004CC74
		private void timerRun_Tick(object sender, EventArgs e)
		{
			this.timerRun.Enabled = false;
			bool flag = PICkitFunctions.readUSB();
			Thread.Sleep(250);
			base.RemoveOwnedForm(this.trigDialog);
			this.trigDialog.Close();
			if (!flag)
			{
				return;
			}
			int num = (int)PICkitFunctions.Usb_read_array[1] + (int)PICkitFunctions.Usb_read_array[2] * 256;
			if ((num & 16384) > 0)
			{
				return;
			}
			this.lastSampleRate = this.comboBoxSampleRate.SelectedIndex;
			bool flag2 = (num & 32768) > 0;
			num &= 4095;
			num++;
			if (num == 2048)
			{
				num = 1536;
			}
			num -= 1536;
			byte[] array = new byte[512];
			byte[] array2 = new byte[3];
			int num2 = 0;
			array2[num2++] = 185;
			array2[num2++] = 0;
			array2[num2++] = 6;
			PICkitFunctions.writeUSB(array2);
			PICkitFunctions.UploadDataNoLen();
			Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 0L, 64L);
			PICkitFunctions.UploadDataNoLen();
			Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 64L, 64L);
			num2 = 0;
			array2[num2++] = 185;
			array2[num2++] = 128;
			array2[num2++] = 6;
			PICkitFunctions.writeUSB(array2);
			PICkitFunctions.UploadDataNoLen();
			Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 128L, 64L);
			PICkitFunctions.UploadDataNoLen();
			Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 192L, 64L);
			num2 = 0;
			array2[num2++] = 185;
			array2[num2++] = 0;
			array2[num2++] = 7;
			PICkitFunctions.writeUSB(array2);
			PICkitFunctions.UploadDataNoLen();
			Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 256L, 64L);
			PICkitFunctions.UploadDataNoLen();
			Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 320L, 64L);
			num2 = 0;
			array2[num2++] = 185;
			array2[num2++] = 128;
			array2[num2++] = 7;
			PICkitFunctions.writeUSB(array2);
			PICkitFunctions.UploadDataNoLen();
			Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 384L, 64L);
			PICkitFunctions.UploadDataNoLen();
			Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 448L, 64L);
			this.lastTrigPos = 1023 - this.postTrigCount % 1000;
			this.lastTrigDelay = this.postTrigCount / 1000;
			num += this.lastTrigPos / 2 + this.postTrigCount / 1000 * 12;
			if (this.lastTrigPos % 2 > 0)
			{
				flag2 = !flag2;
				if (flag2)
				{
					num++;
				}
			}
			num %= 512;
			for (int i = 0; i < this.sampleArray.Length; i++)
			{
				byte b = array[num];
				if (flag2)
				{
					num--;
					if (num < 0)
					{
						num += 512;
					}
					b = (byte)((b >> 4) + ((int)b << 4));
				}
				b &= 28;
				this.sampleArray[i] = (byte)(b >> 2);
				flag2 = !flag2;
			}
			this.sampleArray[0] = this.sampleArray[1];
			this.updateDisplay();
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0004DF9C File Offset: 0x0004CF9C
		private void buttonSave_Click(object sender, EventArgs e)
		{
			Bitmap bitmap = (Bitmap)this.pictureBoxDisplay.Image.Clone();
			Graphics graphics = Graphics.FromImage(bitmap);
			Font font = new Font(FontFamily.GenericSansSerif, 7f, FontStyle.Bold);
			SolidBrush solidBrush = new SolidBrush(Color.White);
			graphics.DrawString(this.labelTimeScale.Text, font, solidBrush, 5f, 88f);
			if (this.checkBoxCursors.Checked)
			{
				graphics.DrawString("X=" + this.labelCursor1Val.Text, font, solidBrush, 100f, 88f);
				graphics.DrawString("Y=" + this.labelCursor2Val.Text, font, solidBrush, 200f, 88f);
			}
			this.saveFileDialogDisplay.ShowDialog();
			try
			{
				bitmap.Save(this.saveFileDialogDisplay.FileName);
			}
			catch
			{
			}
			graphics.Dispose();
			solidBrush.Dispose();
			font.Dispose();
			bitmap.Dispose();
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0004E0A4 File Offset: 0x0004D0A4
		private void buttonExit_Click(object sender, EventArgs e)
		{
			this.checkBoxIOEnable.Checked = false;
			base.Close();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0004E0B8 File Offset: 0x0004D0B8
		private void radioButtonAnalyzer_CheckedChanged(object sender, EventArgs e)
		{
			if (this.radioButtonLogicIO.Checked)
			{
				this.panelLogicIO.Visible = true;
				this.panelAnalyzer.Visible = false;
				return;
			}
			this.panelLogicIO.Visible = false;
			this.checkBoxIOEnable.Checked = false;
			this.panelAnalyzer.Visible = true;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0004E110 File Offset: 0x0004D110
		private void initLogicIO()
		{
			this.radioButtonPin4In.Checked = true;
			this.radioButtonPin5In.Checked = true;
			this.radioButtonPin6In.Checked = true;
			this.textBoxPin1Out.Enabled = true;
			this.textBoxPin1Out.Text = "0";
			this.textBoxPin1Out.BackColor = Color.DarkRed;
			this.labelOut1Click.Visible = true;
			this.textBoxPin4In.Enabled = true;
			this.textBoxPin4In.Text = "0";
			this.textBoxPin4In.BackColor = Color.DarkBlue;
			this.textBoxPin4Out.Enabled = false;
			this.textBoxPin4Out.Text = "0";
			this.textBoxPin4Out.BackColor = SystemColors.Control;
			this.labelOut4Click.Visible = false;
			this.textBoxPin5In.Enabled = true;
			this.textBoxPin5In.Text = "0";
			this.textBoxPin5In.BackColor = Color.DarkBlue;
			this.textBoxPin5Out.Enabled = false;
			this.textBoxPin5Out.Text = "0";
			this.textBoxPin5Out.BackColor = SystemColors.Control;
			this.labelOut5Click.Visible = false;
			this.textBoxPin6In.Enabled = true;
			this.textBoxPin6In.Text = "0";
			this.textBoxPin6In.BackColor = Color.DarkBlue;
			this.textBoxPin6Out.Enabled = false;
			this.textBoxPin6Out.Text = "0";
			this.textBoxPin6Out.BackColor = SystemColors.Control;
			this.labelOut6Click.Visible = false;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0004E2A5 File Offset: 0x0004D2A5
		private void textBoxPin1Out_Click(object sender, EventArgs e)
		{
			this.pinOut(this.textBoxPin1Out);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0004E2B3 File Offset: 0x0004D2B3
		private void textBoxPin4Out_Click(object sender, EventArgs e)
		{
			this.pinOut(this.textBoxPin4Out);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0004E2C1 File Offset: 0x0004D2C1
		private void textBoxPin5Out_Click(object sender, EventArgs e)
		{
			this.pinOut(this.textBoxPin5Out);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0004E2CF File Offset: 0x0004D2CF
		private void textBoxPin6Out_Click(object sender, EventArgs e)
		{
			this.pinOut(this.textBoxPin6Out);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0004E2E0 File Offset: 0x0004D2E0
		private void pinOut(TextBox textBoxObject)
		{
			if (this.checkBoxIOEnable.Checked)
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
					this.updateOutputs();
					return;
				}
			}
			else
			{
				MessageBox.Show("Click the 'Enable IO' button\n to use the Logic IO.", "PICkit 2 Logic Tool");
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0004E35C File Offset: 0x0004D35C
		private void checkBoxIOEnable_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.checkBoxIOEnable.Checked)
			{
				this.timerIO.Enabled = false;
				this.radioButtonPin4In.Enabled = false;
				this.radioButtonPin4Out.Enabled = false;
				this.radioButtonPin5In.Enabled = false;
				this.radioButtonPin5Out.Enabled = false;
				this.radioButtonPin6In.Enabled = false;
				this.radioButtonPin6Out.Enabled = false;
				this.exitLogicIO();
				return;
			}
			if (!this.initLogicPins())
			{
				MessageBox.Show("No valid voltage detected on\nPICkit 2 VDD pin.\n\nA valid voltage (2.5V to 5.0V)\nis required for the Logic IO.", "PICkit 2 Logic Tool");
				this.checkBoxIOEnable.Checked = false;
				return;
			}
			if (PICkitFunctions.PowerStatus() == Constants.PICkit2PWR.vdd_on)
			{
				this.vddOn = true;
			}
			else
			{
				this.vddOn = false;
			}
			this.radioButtonPin4In.Enabled = true;
			this.radioButtonPin4Out.Enabled = true;
			this.radioButtonPin5In.Enabled = true;
			this.radioButtonPin5Out.Enabled = true;
			this.radioButtonPin6In.Enabled = true;
			this.radioButtonPin6Out.Enabled = true;
			this.updateOutputs();
			this.timerIO.Enabled = true;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0004E470 File Offset: 0x0004D470
		private void radioButtonPin4Out_CheckedChanged(object sender, EventArgs e)
		{
			if (this.radioButtonPin4Out.Checked)
			{
				this.textBoxPin4In.Text = "";
				this.textBoxPin4In.BackColor = SystemColors.Control;
				this.textBoxPin4In.Enabled = false;
				this.textBoxPin4Out.Enabled = true;
				if (this.textBoxPin4Out.Text == "0")
				{
					this.textBoxPin4Out.BackColor = Color.DarkRed;
				}
				else
				{
					this.textBoxPin4Out.BackColor = Color.Red;
				}
				this.labelOut4Click.Visible = true;
			}
			else
			{
				this.textBoxPin4In.Enabled = true;
				this.textBoxPin4In.Text = "0";
				this.textBoxPin4In.BackColor = Color.DarkBlue;
				this.textBoxPin4Out.Enabled = false;
				this.textBoxPin4Out.BackColor = SystemColors.Control;
				this.labelOut4Click.Visible = false;
			}
			this.updateOutputs();
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0004E564 File Offset: 0x0004D564
		private void radioButtonPin5Out_CheckedChanged(object sender, EventArgs e)
		{
			if (this.radioButtonPin5Out.Checked)
			{
				this.textBoxPin5In.Text = "";
				this.textBoxPin5In.BackColor = SystemColors.Control;
				this.textBoxPin5In.Enabled = false;
				this.textBoxPin5Out.Enabled = true;
				if (this.textBoxPin5Out.Text == "0")
				{
					this.textBoxPin5Out.BackColor = Color.DarkRed;
				}
				else
				{
					this.textBoxPin5Out.BackColor = Color.Red;
				}
				this.labelOut5Click.Visible = true;
			}
			else
			{
				this.textBoxPin5In.Enabled = true;
				this.textBoxPin5In.Text = "0";
				this.textBoxPin5In.BackColor = Color.DarkBlue;
				this.textBoxPin5Out.Enabled = false;
				this.textBoxPin5Out.BackColor = SystemColors.Control;
				this.labelOut5Click.Visible = false;
			}
			this.updateOutputs();
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0004E658 File Offset: 0x0004D658
		private void radioButtonPin6Out_CheckedChanged(object sender, EventArgs e)
		{
			if (this.radioButtonPin6Out.Checked)
			{
				this.textBoxPin6In.Text = "";
				this.textBoxPin6In.BackColor = SystemColors.Control;
				this.textBoxPin6In.Enabled = false;
				this.textBoxPin6Out.Enabled = true;
				if (this.textBoxPin6Out.Text == "0")
				{
					this.textBoxPin6Out.BackColor = Color.DarkRed;
				}
				else
				{
					this.textBoxPin6Out.BackColor = Color.Red;
				}
				this.labelOut6Click.Visible = true;
			}
			else
			{
				this.textBoxPin6In.Enabled = true;
				this.textBoxPin6In.Text = "0";
				this.textBoxPin6In.BackColor = Color.DarkBlue;
				this.textBoxPin6Out.Enabled = false;
				this.textBoxPin6Out.BackColor = SystemColors.Control;
				this.labelOut6Click.Visible = false;
			}
			this.updateOutputs();
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0004E74C File Offset: 0x0004D74C
		private bool initLogicPins()
		{
			float num = 0f;
			float num2 = 0f;
			if (PICkitFunctions.ReadPICkitVoltages(ref num, ref num2) && num >= 2.5f)
			{
				PICkitFunctions.SetVppVoltage(num, 0.7f);
				PICkitFunctions.SetVDDVoltage(num, 0.85f);
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
				return PICkitFunctions.writeUSB(array);
			}
			return false;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0004E820 File Offset: 0x0004D820
		private bool exitLogicIO()
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
			return PICkitFunctions.writeUSB(array);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0004E8A0 File Offset: 0x0004D8A0
		private bool updateOutputs()
		{
			byte b = 3;
			byte b2 = 1;
			if (this.radioButtonPin4Out.Checked)
			{
				b &= 253;
				if (this.textBoxPin4Out.Text == "1")
				{
					b |= 8;
				}
			}
			if (this.radioButtonPin5Out.Checked)
			{
				b &= 254;
				if (this.textBoxPin5Out.Text == "1")
				{
					b |= 4;
				}
			}
			if (this.radioButtonPin6Out.Checked)
			{
				b2 = 0;
				if (this.textBoxPin6Out.Text == "1")
				{
					b2 = 2;
				}
			}
			byte[] array = new byte[8];
			int num = 0;
			array[num++] = 166;
			array[num++] = 6;
			if (this.textBoxPin1Out.Text == "0")
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
			return PICkitFunctions.writeUSB(array);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0004E9D4 File Offset: 0x0004D9D4
		public void DialogLogic_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (this.panelLogicIO.Visible && this.checkBoxIOEnable.Checked)
			{
				if (e.KeyChar == 'a' || e.KeyChar == 'A')
				{
					this.pinOut(this.textBoxPin1Out);
					return;
				}
				if (e.KeyChar == 's' || e.KeyChar == 'S')
				{
					this.pinOut(this.textBoxPin4Out);
					return;
				}
				if (e.KeyChar == 'd' || e.KeyChar == 'D')
				{
					this.pinOut(this.textBoxPin5Out);
					return;
				}
				if (e.KeyChar == 'f' || e.KeyChar == 'F')
				{
					this.pinOut(this.textBoxPin6Out);
				}
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0004EA84 File Offset: 0x0004DA84
		private void timerIO_Tick(object sender, EventArgs e)
		{
			Constants.PICkit2PWR pickit2PWR = PICkitFunctions.PowerStatus();
			if (pickit2PWR == Constants.PICkit2PWR.vdderror || pickit2PWR == Constants.PICkit2PWR.vddvpperrors)
			{
				MessageBox.Show("PICkit 2 VDD voltage level error.\nVDD shut off: Disabling IO", "PICkit 2 Error");
				this.checkBoxIOEnable.Checked = false;
				return;
			}
			if (pickit2PWR == Constants.PICkit2PWR.vpperror)
			{
				if (this.vddOn)
				{
					MessageBox.Show("Voltage error on Pin 1:\nVDD was shut off.\n\nDisabling IO", "PICkit 2 Error");
					this.checkBoxIOEnable.Checked = false;
					return;
				}
				MessageBox.Show("Voltage error on Pin 1:\nState reset to '0'", "PICkit 2 Error");
				this.textBoxPin1Out.Text = "0";
				this.textBoxPin1Out.BackColor = Color.DarkRed;
			}
			byte[] array = new byte[5];
			int num = 0;
			array[num++] = 166;
			array[num++] = 2;
			array[num++] = 220;
			array[num++] = 206;
			array[num++] = 170;
			PICkitFunctions.writeUSB(array);
			PICkitFunctions.readUSB();
			if ((PICkitFunctions.Usb_read_array[2] & 2) > 0)
			{
				this.updateInputBox(this.textBoxPin4In, "1");
			}
			else
			{
				this.updateInputBox(this.textBoxPin4In, "0");
			}
			if ((PICkitFunctions.Usb_read_array[2] & 1) > 0)
			{
				this.updateInputBox(this.textBoxPin5In, "1");
			}
			else
			{
				this.updateInputBox(this.textBoxPin5In, "0");
			}
			if ((PICkitFunctions.Usb_read_array[3] & 1) > 0)
			{
				this.updateInputBox(this.textBoxPin6In, "1");
				return;
			}
			this.updateInputBox(this.textBoxPin6In, "0");
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0004EBF2 File Offset: 0x0004DBF2
		private void updateInputBox(TextBox inputBox, string value)
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

		// Token: 0x060002A5 RID: 677 RVA: 0x0004EC28 File Offset: 0x0004DC28
		private void buttonHelp_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(FormPICkit2.HomeDirectory + "\\Logic Tool User Guide.pdf");
			}
			catch
			{
				MessageBox.Show("Unable to open Logic Tool User Guide.");
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0004EC6C File Offset: 0x0004DC6C
		private void checkBoxVDD_Click(object sender, EventArgs e)
		{
			this.VddCallback(true, this.checkBoxVDD.Checked);
		}

		// Token: 0x0400056A RID: 1386
		private const int SAMPLE_ARRAY_LENGTH = 1024;

		// Token: 0x040005CF RID: 1487
		public DelegateVddCallback VddCallback;

		// Token: 0x040005D0 RID: 1488
		private byte[] sampleArray = new byte[1024];

		// Token: 0x040005D1 RID: 1489
		private int lastZoomLevel = 1;

		// Token: 0x040005D2 RID: 1490
		private int lastSampleRate;

		// Token: 0x040005D3 RID: 1491
		private int lastTrigPos = 50;

		// Token: 0x040005D4 RID: 1492
		private int lastTrigDelay;

		// Token: 0x040005D5 RID: 1493
		private int firstSample;

		// Token: 0x040005D6 RID: 1494
		private float[] sampleRates = new float[]
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

		// Token: 0x040005D7 RID: 1495
		private byte[] sampleFactors = new byte[]
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

		// Token: 0x040005D8 RID: 1496
		private Bitmap lastDrawnDisplay;

		// Token: 0x040005D9 RID: 1497
		private int cursor1Pos;

		// Token: 0x040005DA RID: 1498
		private int cursor2Pos;

		// Token: 0x040005DB RID: 1499
		private int postTrigCount = 1;

		// Token: 0x040005DC RID: 1500
		private DialogTrigger trigDialog;

		// Token: 0x040005DD RID: 1501
		private bool vddOn;
	}
}
