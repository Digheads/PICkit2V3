using System;
using System.IO;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class DialogDevFile : Form
	{
		public DialogDevFile()
		{
			InitializeComponent();
			DirectoryInfo directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
			foreach (FileInfo fileInfo in directoryInfo.GetFiles("*.dat"))
				listBoxDevFiles.Items.Add(fileInfo.Name);
		}

		private void ButtonLoadDevFile_Click(object sender, EventArgs e)
		{
			FormPICkit2.deviceFileName = listBoxDevFiles.SelectedItem.ToString();
			Close();
		}
	}
}
