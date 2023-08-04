using System;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class DialogUserIDs : Form
	{
		public DialogUserIDs()
		{
			InitializeComponent();
			idMemOpen = true;
			dataGridViewIDMem.DefaultCellStyle.Font = new Font("Courier New", 9f);
			UpdateIDMemoryGrid();
		}

		public void UpdateIDMemoryGrid()
		{
			int width = (int)(53 * FormPICkit2.scalefactW);
			dataGridViewIDMem.ColumnCount = 4;
			for (int i = 0; i < dataGridViewIDMem.ColumnCount; i++)
				dataGridViewIDMem.Columns[i].Width = width;

			int rowCount = PICkitFunctions.DeviceBuffers.UserIDs.Length / 4;
			dataGridViewIDMem.RowCount = rowCount;
			int num = 0;
			int num2 = 0;
			for (int j = 0; j < PICkitFunctions.DeviceBuffers.UserIDs.Length; j++)
			{
				dataGridViewIDMem[num2, num].Value = string.Format("{0:X6}", PICkitFunctions.DeviceBuffers.UserIDs[j]);
				num2++;
				if (num2 >= 4)
				{
					num2 = 0;
					num++;
				}
			}
			dataGridViewIDMem[0, 0].Selected = true;
			dataGridViewIDMem[0, 0].Selected = false;
		}

		private void DialogUserIDs_FormClosing(object sender, FormClosingEventArgs e)
		{
			idMemOpen = false;
		}

		private void ButtonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		public static bool idMemOpen;
	}
}
