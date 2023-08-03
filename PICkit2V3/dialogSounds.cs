using System;
using System.ComponentModel;
using System.Media;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class DialogSounds : Form
	{
		public DialogSounds()
		{
			InitializeComponent();
			checkBoxSuccess.Checked = FormPICkit2.playSuccessWav;
			checkBoxWarning.Checked = FormPICkit2.playWarningWav;
			checkBoxError.Checked = FormPICkit2.playErrorWav;
			textBoxSuccessFile.Text = FormPICkit2.successWavFile;
			textBoxWarningFile.Text = FormPICkit2.warningWavFile;
			textBoxErrorFile.Text = FormPICkit2.errorWavFile;
		}

		private void ButtonOK_Click(object sender, EventArgs e)
		{
			FormPICkit2.playSuccessWav = checkBoxSuccess.Checked;
			FormPICkit2.playWarningWav = checkBoxWarning.Checked;
			FormPICkit2.playErrorWav = checkBoxError.Checked;
			FormPICkit2.successWavFile = textBoxSuccessFile.Text;
			FormPICkit2.warningWavFile = textBoxWarningFile.Text;
			FormPICkit2.errorWavFile = textBoxErrorFile.Text;
			Close();
		}

		private void ButtonSuccessBrowse_Click(object sender, EventArgs e)
		{
			destSoundTextBox = textBoxSuccessFile;
			openFileDialogWAV.FileName = textBoxSuccessFile.Text;
			openFileDialogWAV.ShowDialog();
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			destSoundTextBox = textBoxWarningFile;
			openFileDialogWAV.FileName = textBoxWarningFile.Text;
			openFileDialogWAV.ShowDialog();
		}

		private void ButtonErrorBrowse_Click(object sender, EventArgs e)
		{
			destSoundTextBox = textBoxErrorFile;
			openFileDialogWAV.FileName = textBoxErrorFile.Text;
			openFileDialogWAV.ShowDialog();
		}

		private void CheckBoxSuccess_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxSuccess.Checked)
			{
				try
				{
					wavPlayer.SoundLocation = textBoxSuccessFile.Text;
					wavPlayer.Play();
				}
				catch
				{
				}
			}
		}

		private void CheckBoxWarning_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxWarning.Checked)
			{
				try
				{
					wavPlayer.SoundLocation = textBoxWarningFile.Text;
					wavPlayer.Play();
				}
				catch
				{
				}
			}
		}

		private void CheckBoxError_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxError.Checked)
			{
				try
				{
					wavPlayer.SoundLocation = textBoxErrorFile.Text;
					wavPlayer.Play();
				}
				catch
				{
				}
			}
		}

		private void OpenFileDialogWAV_FileOk(object sender, CancelEventArgs e)
		{
			destSoundTextBox.Text = openFileDialogWAV.FileName;
		}

		private readonly SoundPlayer wavPlayer = new SoundPlayer();
		private TextBox destSoundTextBox;
	}
}
