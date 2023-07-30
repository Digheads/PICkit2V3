using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	// Token: 0x0200003E RID: 62
	public partial class DialogTrigger : Form
	{
		// Token: 0x06000252 RID: 594 RVA: 0x00045848 File Offset: 0x00044848
		public DialogTrigger()
		{
			this.InitializeComponent();
			base.Size = new Size(base.Size.Width, (int)(FormPICkit2.scalefactH * (float)base.Size.Height));
		}
	}
}
