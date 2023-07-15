using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace PICkit2V3.Properties
{
	// Token: 0x0200003F RID: 63
	[CompilerGenerated]
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
	internal class Resources
	{
		// Token: 0x06000253 RID: 595 RVA: 0x00045890 File Offset: 0x00044890
		internal Resources()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00045898 File Offset: 0x00044898
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("PICkit2V2.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000255 RID: 597 RVA: 0x000458D7 File Offset: 0x000448D7
		// (set) Token: 0x06000256 RID: 598 RVA: 0x000458DE File Offset: 0x000448DE
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x0400055A RID: 1370
		private static ResourceManager resourceMan;

		// Token: 0x0400055B RID: 1371
		private static CultureInfo resourceCulture;
	}
}
