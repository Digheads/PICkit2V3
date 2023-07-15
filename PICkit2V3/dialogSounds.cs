using System;
using System.ComponentModel;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace PICkit2V3
{
	// Token: 0x0200000C RID: 12
	public partial class dialogSounds : Form
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00010B40 File Offset: 0x0000FB40
		public dialogSounds()
		{
			this.InitializeComponent();
			this.checkBoxSuccess.Checked = FormPICkit2.PlaySuccessWav;
			this.checkBoxWarning.Checked = FormPICkit2.PlayWarningWav;
			this.checkBoxError.Checked = FormPICkit2.PlayErrorWav;
			this.textBoxSuccessFile.Text = FormPICkit2.SuccessWavFile;
			this.textBoxWarningFile.Text = FormPICkit2.WarningWavFile;
			this.textBoxErrorFile.Text = FormPICkit2.ErrorWavFile;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00010BC4 File Offset: 0x0000FBC4
		private void buttonOK_Click(object sender, EventArgs e)
		{
			FormPICkit2.PlaySuccessWav = this.checkBoxSuccess.Checked;
			FormPICkit2.PlayWarningWav = this.checkBoxWarning.Checked;
			FormPICkit2.PlayErrorWav = this.checkBoxError.Checked;
			FormPICkit2.SuccessWavFile = this.textBoxSuccessFile.Text;
			FormPICkit2.WarningWavFile = this.textBoxWarningFile.Text;
			FormPICkit2.ErrorWavFile = this.textBoxErrorFile.Text;
			base.Close();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00010C37 File Offset: 0x0000FC37
		private void buttonSuccessBrowse_Click(object sender, EventArgs e)
		{
			this.destSoundTextBox = this.textBoxSuccessFile;
			this.openFileDialogWAV.FileName = this.textBoxSuccessFile.Text;
			this.openFileDialogWAV.ShowDialog();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00010C67 File Offset: 0x0000FC67
		private void button1_Click(object sender, EventArgs e)
		{
			this.destSoundTextBox = this.textBoxWarningFile;
			this.openFileDialogWAV.FileName = this.textBoxWarningFile.Text;
			this.openFileDialogWAV.ShowDialog();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00010C97 File Offset: 0x0000FC97
		private void buttonErrorBrowse_Click(object sender, EventArgs e)
		{
			this.destSoundTextBox = this.textBoxErrorFile;
			this.openFileDialogWAV.FileName = this.textBoxErrorFile.Text;
			this.openFileDialogWAV.ShowDialog();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00010CC8 File Offset: 0x0000FCC8
		private void checkBoxSuccess_CheckedChanged(object sender, EventArgs e)
		{
			if (this.checkBoxSuccess.Checked)
			{
				try
				{
					this.wavPlayer.SoundLocation = this.textBoxSuccessFile.Text;
					this.wavPlayer.Play();
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00010D18 File Offset: 0x0000FD18
		private void checkBoxWarning_CheckedChanged(object sender, EventArgs e)
		{
			if (this.checkBoxWarning.Checked)
			{
				try
				{
					this.wavPlayer.SoundLocation = this.textBoxWarningFile.Text;
					this.wavPlayer.Play();
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00010D68 File Offset: 0x0000FD68
		private void checkBoxError_CheckedChanged(object sender, EventArgs e)
		{
			if (this.checkBoxError.Checked)
			{
				try
				{
					this.wavPlayer.SoundLocation = this.textBoxErrorFile.Text;
					this.wavPlayer.Play();
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00010DB8 File Offset: 0x0000FDB8
		private void openFileDialogWAV_FileOk(object sender, CancelEventArgs e)
		{
			this.destSoundTextBox.Text = this.openFileDialogWAV.FileName;
		}

		// Token: 0x040000D6 RID: 214
		private SoundPlayer wavPlayer = new SoundPlayer();

		// Token: 0x040000D7 RID: 215
		private TextBox destSoundTextBox;
	}
}
