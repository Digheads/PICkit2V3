using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PICkit2V3
{
	// Token: 0x02000033 RID: 51
	public partial class DialogDevFile : Form
	{
		// Token: 0x06000222 RID: 546 RVA: 0x00040B2C File Offset: 0x0003FB2C
		public DialogDevFile()
		{
			this.InitializeComponent();
			DirectoryInfo directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
			foreach (FileInfo fileInfo in directoryInfo.GetFiles("*.dat"))
			{
				this.listBoxDevFiles.Items.Add(fileInfo.Name);
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00040B85 File Offset: 0x0003FB85
		private void buttonLoadDevFile_Click(object sender, EventArgs e)
		{
			FormPICkit2.deviceFileName = this.listBoxDevFiles.SelectedItem.ToString();
			base.Close();
		}
	}
}
