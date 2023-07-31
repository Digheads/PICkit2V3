using System;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class DialogCustomBaud : Form
	{
		public DialogCustomBaud()
		{
			InitializeComponent();
			textBox1.Focus();
		}

		private void TextBox1_TextChanged(object sender, EventArgs e)
		{
			if (textBox1.Text.Length > 0 && !char.IsDigit(textBox1.Text[textBox1.Text.Length - 1]))
				textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
		}

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void ButtonOK_Click(object sender, EventArgs e)
		{
			try
			{
				int num = int.Parse(textBox1.Text);
				if (num < 150 || num > 38400)
					MessageBox.Show("Baud value is outside\nthe Min / Max range.");
				else
				{
					DialogUART.CustomBaud = textBox1.Text;
					Close();
				}
			}
			catch
			{
				MessageBox.Show("Illegal Value.");
			}
		}
	}
}
