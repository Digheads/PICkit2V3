using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PICkit2V3
{
	// Token: 0x02000037 RID: 55
	public class USB
	{
		// Token: 0x0600023F RID: 575
		[DllImport("hid.dll")]
		public static extern void HidD_GetHidGuid(ref Guid HidGuid);

		// Token: 0x06000240 RID: 576
		[DllImport("setupapi.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SetupDiGetClassDevs(ref Guid ClassGuid, string Enumerator, int hwndParent, int Flags);

		// Token: 0x06000241 RID: 577
		[DllImport("setupapi.dll")]
		public static extern int SetupDiEnumDeviceInterfaces(IntPtr DeviceInfoSet, int DeviceInfoData, ref Guid InterfaceClassGuid, int MemberIndex, ref USB.SP_DEVICE_INTERFACE_DATA DeviceInterfaceData);

		// Token: 0x06000242 RID: 578
		[DllImport("setupapi.dll", CharSet = CharSet.Auto)]
		public static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr DeviceInfoSet, ref USB.SP_DEVICE_INTERFACE_DATA DeviceInterfaceData, IntPtr DeviceInterfaceDetailData, int DeviceInterfaceDetailDataSize, ref int RequiredSize, IntPtr DeviceInfoData);

		// Token: 0x06000243 RID: 579
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, ref USB.SECURITY_ATTRIBUTES lpSecurityAttributes, int dwCreationDisposition, uint dwFlagsAndAttributes, int hTemplateFile);

		// Token: 0x06000244 RID: 580
		[DllImport("hid.dll")]
		public static extern int HidD_GetAttributes(IntPtr HidDeviceObject, ref USB.HIDD_ATTRIBUTES Attributes);

		// Token: 0x06000245 RID: 581
		[DllImport("hid.dll")]
		public static extern bool HidD_GetPreparsedData(IntPtr HidDeviceObject, ref IntPtr PreparsedData);

		// Token: 0x06000246 RID: 582
		[DllImport("hid.dll")]
		public static extern bool HidD_GetSerialNumberString(IntPtr HidDeviceObject, byte[] Buffer, ulong BufferLength);

		// Token: 0x06000247 RID: 583
		[DllImport("hid.dll")]
		public static extern int HidP_GetCaps(IntPtr PreparsedData, ref USB.HIDP_CAPS Capabilities);

		// Token: 0x06000248 RID: 584
		[DllImport("setupapi.dll")]
		public static extern int SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

		// Token: 0x06000249 RID: 585
		[DllImport("hid.dll")]
		public static extern bool HidD_FreePreparsedData(ref IntPtr PreparsedData);

		// Token: 0x0600024A RID: 586
		[DllImport("kernel32.dll")]
		public static extern int CloseHandle(IntPtr hObject);

		// Token: 0x0600024B RID: 587
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteFile(IntPtr hFile, byte[] Buffer, int numBytesToWrite, ref int numBytesWritten, int Overlapped);

		// Token: 0x0600024C RID: 588
		[DllImport("kernel32", SetLastError = true)]
		public static extern bool ReadFile(IntPtr hFile, byte[] Buffer, int NumberOfBytesToRead, ref int pNumberOfBytesRead, int Overlapped);

		// Token: 0x0600024D RID: 589 RVA: 0x00045158 File Offset: 0x00044158
		public static bool Find_This_Device(ushort p_VendorID, ushort p_PoductID, ushort p_index, ref IntPtr p_ReadHandle, ref IntPtr p_WriteHandle)
		{
			IntPtr deviceInfoSet = IntPtr.Zero;
			IntPtr zero = IntPtr.Zero;
			USB.HIDP_CAPS hidp_CAPS = default(USB.HIDP_CAPS);
			ushort num = 0;
			IntPtr intPtr = IntPtr.Zero;
			int num2 = 0;
			USB.SECURITY_ATTRIBUTES security_ATTRIBUTES = default(USB.SECURITY_ATTRIBUTES);
			IntPtr value = new IntPtr(-1);
			byte[] array = new byte[64];
			security_ATTRIBUTES.lpSecurityDescriptor = 0;
			security_ATTRIBUTES.bInheritHandle = Convert.ToInt32(true);
			security_ATTRIBUTES.nLength = Marshal.SizeOf(security_ATTRIBUTES);
			Guid empty = Guid.Empty;
			USB.SP_DEVICE_INTERFACE_DATA sp_DEVICE_INTERFACE_DATA;
			sp_DEVICE_INTERFACE_DATA.cbSize = 0;
			sp_DEVICE_INTERFACE_DATA.Flags = 0;
			sp_DEVICE_INTERFACE_DATA.InterfaceClassGuid = Guid.Empty;
			sp_DEVICE_INTERFACE_DATA.Reserved = 0;
			USB.SP_DEVICE_INTERFACE_DETAIL_DATA sp_DEVICE_INTERFACE_DETAIL_DATA;
			sp_DEVICE_INTERFACE_DETAIL_DATA.cbSize = 0;
			sp_DEVICE_INTERFACE_DETAIL_DATA.DevicePath = "";
			USB.HIDD_ATTRIBUTES hidd_ATTRIBUTES;
			hidd_ATTRIBUTES.ProductID = 0;
			hidd_ATTRIBUTES.Size = 0;
			hidd_ATTRIBUTES.VendorID = 0;
			hidd_ATTRIBUTES.VersionNumber = 0;
			bool result = false;
			security_ATTRIBUTES.lpSecurityDescriptor = 0;
			security_ATTRIBUTES.bInheritHandle = Convert.ToInt32(true);
			security_ATTRIBUTES.nLength = Marshal.SizeOf(security_ATTRIBUTES);
			USB.HidD_GetHidGuid(ref empty);
			deviceInfoSet = USB.SetupDiGetClassDevs(ref empty, null, 0, 18);
			sp_DEVICE_INTERFACE_DATA.cbSize = Marshal.SizeOf(sp_DEVICE_INTERFACE_DATA);
			for (int i = 0; i < 20; i++)
			{
				int num3 = USB.SetupDiEnumDeviceInterfaces(deviceInfoSet, 0, ref empty, i, ref sp_DEVICE_INTERFACE_DATA);
				if (num3 != 0)
				{
					USB.SetupDiGetDeviceInterfaceDetail(deviceInfoSet, ref sp_DEVICE_INTERFACE_DATA, IntPtr.Zero, 0, ref num2, IntPtr.Zero);
					sp_DEVICE_INTERFACE_DETAIL_DATA.cbSize = Marshal.SizeOf(sp_DEVICE_INTERFACE_DETAIL_DATA);
					IntPtr intPtr2 = Marshal.AllocHGlobal(num2);
					Marshal.WriteInt32(intPtr2, 4 + Marshal.SystemDefaultCharSize);
					USB.SetupDiGetDeviceInterfaceDetail(deviceInfoSet, ref sp_DEVICE_INTERFACE_DATA, intPtr2, num2, ref num2, IntPtr.Zero);
					IntPtr ptr = new IntPtr(intPtr2.ToInt32() + 4);
					string lpFileName = Marshal.PtrToStringAuto(ptr);
					intPtr = USB.CreateFile(lpFileName, 3221225472U, 3U, ref security_ATTRIBUTES, 3, 0U, 0);
					if (intPtr != value)
					{
						hidd_ATTRIBUTES.Size = Marshal.SizeOf(hidd_ATTRIBUTES);
						num3 = USB.HidD_GetAttributes(intPtr, ref hidd_ATTRIBUTES);
						if (num3 != 0)
						{
							if (hidd_ATTRIBUTES.VendorID == p_VendorID && hidd_ATTRIBUTES.ProductID == p_PoductID)
							{
								if (num == p_index)
								{
									result = true;
									USB.HidD_GetSerialNumberString(intPtr, array, 64UL);
									if (array[0] == 9 || array[0] == 0)
									{
										USB.UnitID = "-";
									}
									else
									{
										int j;
										for (j = 2; j < 28; j += 2)
										{
											array[j / 2] = array[j];
											if (array[j] == 0 || array[j] == 224)
											{
												break;
											}
											array[j] = 0;
											array[j + 1] = 0;
										}
										j /= 2;
										char[] array2 = new char[Encoding.ASCII.GetCharCount(array, 0, j)];
										Encoding.ASCII.GetChars(array, 0, j, array2, 0);
										string unitID = new string(array2);
										USB.UnitID = unitID;
									}
									p_WriteHandle = intPtr;
									USB.HidD_GetPreparsedData(intPtr, ref zero);
									USB.HidP_GetCaps(zero, ref hidp_CAPS);
									p_ReadHandle = USB.CreateFile(lpFileName, 3221225472U, 3U, ref security_ATTRIBUTES, 3, 0U, 0);
									USB.HidD_FreePreparsedData(ref zero);
									break;
								}
								USB.CloseHandle(intPtr);
								num += 1;
							}
							else
							{
								result = false;
								USB.CloseHandle(intPtr);
							}
						}
						else
						{
							result = false;
							USB.CloseHandle(intPtr);
						}
					}
				}
			}
			USB.SetupDiDestroyDeviceInfoList(deviceInfoSet);
			return result;
		}

		// Token: 0x04000529 RID: 1321
		private const uint GENERIC_READ = 2147483648U;

		// Token: 0x0400052A RID: 1322
		private const uint GENERIC_WRITE = 1073741824U;

		// Token: 0x0400052B RID: 1323
		private const uint FILE_SHARE_READ = 1U;

		// Token: 0x0400052C RID: 1324
		private const uint FILE_SHARE_WRITE = 2U;

		// Token: 0x0400052D RID: 1325
		private const uint FILE_FLAG_OVERLAPPED = 1073741824U;

		// Token: 0x0400052E RID: 1326
		private const int INVALID_HANDLE_VALUE = -1;

		// Token: 0x0400052F RID: 1327
		private const short OPEN_EXISTING = 3;

		// Token: 0x04000530 RID: 1328
		private const short DIGCF_PRESENT = 2;

		// Token: 0x04000531 RID: 1329
		private const short DIGCF_DEVICEINTERFACE = 16;

		// Token: 0x04000532 RID: 1330
		public static string UnitID = "";

		// Token: 0x02000038 RID: 56
		public struct SP_DEVICE_INTERFACE_DATA
		{
			// Token: 0x04000533 RID: 1331
			public int cbSize;

			// Token: 0x04000534 RID: 1332
			public Guid InterfaceClassGuid;

			// Token: 0x04000535 RID: 1333
			public int Flags;

			// Token: 0x04000536 RID: 1334
			public int Reserved;
		}

		// Token: 0x02000039 RID: 57
		public struct SP_DEVICE_INTERFACE_DETAIL_DATA
		{
			// Token: 0x04000537 RID: 1335
			public int cbSize;

			// Token: 0x04000538 RID: 1336
			public string DevicePath;
		}

		// Token: 0x0200003A RID: 58
		public struct SP_DEVINFO_DATA
		{
			// Token: 0x04000539 RID: 1337
			public int cbSize;

			// Token: 0x0400053A RID: 1338
			public Guid ClassGuid;

			// Token: 0x0400053B RID: 1339
			public int DevInst;

			// Token: 0x0400053C RID: 1340
			public int Reserved;
		}

		// Token: 0x0200003B RID: 59
		public struct HIDD_ATTRIBUTES
		{
			// Token: 0x0400053D RID: 1341
			public int Size;

			// Token: 0x0400053E RID: 1342
			public ushort VendorID;

			// Token: 0x0400053F RID: 1343
			public ushort ProductID;

			// Token: 0x04000540 RID: 1344
			public ushort VersionNumber;
		}

		// Token: 0x0200003C RID: 60
		public struct SECURITY_ATTRIBUTES
		{
			// Token: 0x04000541 RID: 1345
			public int nLength;

			// Token: 0x04000542 RID: 1346
			public int lpSecurityDescriptor;

			// Token: 0x04000543 RID: 1347
			public int bInheritHandle;
		}

		// Token: 0x0200003D RID: 61
		public struct HIDP_CAPS
		{
			// Token: 0x04000544 RID: 1348
			public short Usage;

			// Token: 0x04000545 RID: 1349
			public short UsagePage;

			// Token: 0x04000546 RID: 1350
			public short InputReportByteLength;

			// Token: 0x04000547 RID: 1351
			public short OutputReportByteLength;

			// Token: 0x04000548 RID: 1352
			public short FeatureReportByteLength;

			// Token: 0x04000549 RID: 1353
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
			public short[] Reserved;

			// Token: 0x0400054A RID: 1354
			public short NumberLinkCollectionNodes;

			// Token: 0x0400054B RID: 1355
			public short NumberInputButtonCaps;

			// Token: 0x0400054C RID: 1356
			public short NumberInputValueCaps;

			// Token: 0x0400054D RID: 1357
			public short NumberInputDataIndices;

			// Token: 0x0400054E RID: 1358
			public short NumberOutputButtonCaps;

			// Token: 0x0400054F RID: 1359
			public short NumberOutputValueCaps;

			// Token: 0x04000550 RID: 1360
			public short NumberOutputDataIndices;

			// Token: 0x04000551 RID: 1361
			public short NumberFeatureButtonCaps;

			// Token: 0x04000552 RID: 1362
			public short NumberFeatureValueCaps;

			// Token: 0x04000553 RID: 1363
			public short NumberFeatureDataIndices;
		}
	}
}
