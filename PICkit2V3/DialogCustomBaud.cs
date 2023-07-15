using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	// Token: 0x0200000D RID: 13
	public partial class DialogCustomBaud : Form
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00010DD0 File Offset: 0x0000FDD0
		public DialogCustomBaud()
		{
			this.InitializeComponent();
			this.textBox1.Focus();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00010DEC File Offset: 0x0000FDEC
		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			if (this.textBox1.Text.Length > 0 && !char.IsDigit(this.textBox1.Text[this.textBox1.Text.Length - 1]))
			{
				this.textBox1.Text = this.textBox1.Text.Substring(0, this.textBox1.Text.Length - 1);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00010E63 File Offset: 0x0000FE63
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00010E6C File Offset: 0x0000FE6C
		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				int num = int.Parse(this.textBox1.Text);
				if (num < 150 || num > 38400)
				{
					MessageBox.Show("Baud value is outside\nthe Min / Max range.");
				}
				else
				{
					DialogUART.CustomBaud = this.textBox1.Text;
					base.Close();
				}
			}
			catch
			{
				MessageBox.Show("Illegal Value.");
			}
		}
	}
}
