using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	// Token: 0x02000040 RID: 64
	public partial class DialogUserIDs : Form
	{
		// Token: 0x06000257 RID: 599 RVA: 0x000458E6 File Offset: 0x000448E6
		public DialogUserIDs()
		{
			this.InitializeComponent();
			DialogUserIDs.IDMemOpen = true;
			this.dataGridViewIDMem.DefaultCellStyle.Font = new Font("Courier New", 9f);
			this.UpdateIDMemoryGrid();
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00045920 File Offset: 0x00044920
		public void UpdateIDMemoryGrid()
		{
			int width = (int)(53f * FormPICkit2.scalefactW);
			this.dataGridViewIDMem.ColumnCount = 4;
			for (int i = 0; i < this.dataGridViewIDMem.ColumnCount; i++)
			{
				this.dataGridViewIDMem.Columns[i].Width = width;
			}
			int rowCount = PICkitFunctions.DeviceBuffers.UserIDs.Length / 4;
			this.dataGridViewIDMem.RowCount = rowCount;
			int num = 0;
			int num2 = 0;
			for (int j = 0; j < PICkitFunctions.DeviceBuffers.UserIDs.Length; j++)
			{
				this.dataGridViewIDMem[num2, num].Value = string.Format("{0:X6}", PICkitFunctions.DeviceBuffers.UserIDs[j]);
				num2++;
				if (num2 >= 4)
				{
					num2 = 0;
					num++;
				}
			}
			this.dataGridViewIDMem[0, 0].Selected = true;
			this.dataGridViewIDMem[0, 0].Selected = false;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00045A15 File Offset: 0x00044A15
		private void DialogUserIDs_FormClosing(object sender, FormClosingEventArgs e)
		{
			DialogUserIDs.IDMemOpen = false;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00045A1D File Offset: 0x00044A1D
		private void buttonClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0400055C RID: 1372
		public static bool IDMemOpen;
	}
}
