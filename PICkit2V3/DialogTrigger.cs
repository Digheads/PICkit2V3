using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class DialogTrigger : Form
	{
		public DialogTrigger()
		{
			InitializeComponent();
			Size = new Size(Size.Width, (int)(FormPICkit2.scalefactH * Size.Height));
		}
	}
}