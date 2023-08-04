using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PICkit2V3
{
	public class USB
	{
		[DllImport("hid.dll")]
		public static extern void HidD_GetHidGuid(ref Guid HidGuid);

		[DllImport("setupapi.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SetupDiGetClassDevs(ref Guid ClassGuid, string Enumerator, int hwndParent, int Flags);

		[DllImport("setupapi.dll")]
		public static extern int SetupDiEnumDeviceInterfaces(IntPtr DeviceInfoSet, int DeviceInfoData, ref Guid InterfaceClassGuid, int MemberIndex, ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData);

		[DllImport("setupapi.dll", CharSet = CharSet.Auto)]
		public static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr DeviceInfoSet, ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData, IntPtr DeviceInterfaceDetailData, int DeviceInterfaceDetailDataSize, ref int RequiredSize, IntPtr DeviceInfoData);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, ref SECURITY_ATTRIBUTES lpSecurityAttributes, int dwCreationDisposition, uint dwFlagsAndAttributes, int hTemplateFile);

		[DllImport("hid.dll")]
		public static extern int HidD_GetAttributes(IntPtr HidDeviceObject, ref HIDD_ATTRIBUTES Attributes);

		[DllImport("hid.dll")]
		public static extern bool HidD_GetPreparsedData(IntPtr HidDeviceObject, ref IntPtr PreparsedData);

		[DllImport("hid.dll")]
		public static extern bool HidD_GetSerialNumberString(IntPtr HidDeviceObject, byte[] Buffer, ulong BufferLength);

		[DllImport("hid.dll")]
		public static extern int HidP_GetCaps(IntPtr PreparsedData, ref HIDP_CAPS Capabilities);

		[DllImport("setupapi.dll")]
		public static extern int SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

		[DllImport("hid.dll")]
		public static extern bool HidD_FreePreparsedData(ref IntPtr PreparsedData);

		[DllImport("kernel32.dll")]
		public static extern int CloseHandle(IntPtr hObject);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteFile(IntPtr hFile, byte[] Buffer, int numBytesToWrite, ref int numBytesWritten, int Overlapped);

		[DllImport("kernel32", SetLastError = true)]
		public static extern bool ReadFile(IntPtr hFile, byte[] Buffer, int NumberOfBytesToRead, ref int pNumberOfBytesRead, int Overlapped);

		public static bool Find_This_Device(ushort p_VendorID, ushort p_PoductID, ushort p_index, ref IntPtr p_ReadHandle, ref IntPtr p_WriteHandle)
		{
            IntPtr zero = IntPtr.Zero;
			HIDP_CAPS hidp_CAPS = default;
			ushort num = 0;
            int num2 = 0;
			SECURITY_ATTRIBUTES security_ATTRIBUTES = default;
			IntPtr value = new IntPtr(-1);
			byte[] array = new byte[64];
			security_ATTRIBUTES.lpSecurityDescriptor = 0;
			security_ATTRIBUTES.bInheritHandle = Convert.ToInt32(true);
			security_ATTRIBUTES.nLength = Marshal.SizeOf(security_ATTRIBUTES);
			Guid empty = Guid.Empty;
			SP_DEVICE_INTERFACE_DATA sp_DEVICE_INTERFACE_DATA;
			sp_DEVICE_INTERFACE_DATA.cbSize = 0;
			sp_DEVICE_INTERFACE_DATA.Flags = 0;
			sp_DEVICE_INTERFACE_DATA.InterfaceClassGuid = Guid.Empty;
			sp_DEVICE_INTERFACE_DATA.Reserved = 0;
			SP_DEVICE_INTERFACE_DETAIL_DATA sp_DEVICE_INTERFACE_DETAIL_DATA;
			sp_DEVICE_INTERFACE_DETAIL_DATA.cbSize = 0;
			sp_DEVICE_INTERFACE_DETAIL_DATA.DevicePath = "";
			HIDD_ATTRIBUTES hidd_ATTRIBUTES;
			hidd_ATTRIBUTES.ProductID = 0;
			hidd_ATTRIBUTES.Size = 0;
			hidd_ATTRIBUTES.VendorID = 0;
			hidd_ATTRIBUTES.VersionNumber = 0;
			bool result = false;
			security_ATTRIBUTES.lpSecurityDescriptor = 0;
			security_ATTRIBUTES.bInheritHandle = Convert.ToInt32(true);
			security_ATTRIBUTES.nLength = Marshal.SizeOf(security_ATTRIBUTES);
			HidD_GetHidGuid(ref empty);
            IntPtr deviceInfoSet = SetupDiGetClassDevs(ref empty, null, 0, 18);
            sp_DEVICE_INTERFACE_DATA.cbSize = Marshal.SizeOf(sp_DEVICE_INTERFACE_DATA);
			for (int i = 0; i < 20; i++)
			{
				int num3 = SetupDiEnumDeviceInterfaces(deviceInfoSet, 0, ref empty, i, ref sp_DEVICE_INTERFACE_DATA);
				if (num3 != 0)
				{
					SetupDiGetDeviceInterfaceDetail(deviceInfoSet, ref sp_DEVICE_INTERFACE_DATA, IntPtr.Zero, 0, ref num2, IntPtr.Zero);
					sp_DEVICE_INTERFACE_DETAIL_DATA.cbSize = Marshal.SizeOf(sp_DEVICE_INTERFACE_DETAIL_DATA);
					IntPtr intPtr2 = Marshal.AllocHGlobal(num2);
					Marshal.WriteInt32(intPtr2, 4 + Marshal.SystemDefaultCharSize);
					SetupDiGetDeviceInterfaceDetail(deviceInfoSet, ref sp_DEVICE_INTERFACE_DATA, intPtr2, num2, ref num2, IntPtr.Zero);
					IntPtr ptr = new IntPtr(intPtr2.ToInt32() + 4);
					string lpFileName = Marshal.PtrToStringAuto(ptr);
                    IntPtr intPtr = CreateFile(lpFileName, 3221225472U, 3U, ref security_ATTRIBUTES, 3, 0U, 0);
                    if (intPtr != value)
					{
						hidd_ATTRIBUTES.Size = Marshal.SizeOf(hidd_ATTRIBUTES);
						num3 = HidD_GetAttributes(intPtr, ref hidd_ATTRIBUTES);
						if (num3 != 0)
						{
							if (hidd_ATTRIBUTES.VendorID == p_VendorID && hidd_ATTRIBUTES.ProductID == p_PoductID)
							{
								if (num == p_index)
								{
									result = true;
									HidD_GetSerialNumberString(intPtr, array, 64UL);
									if (array[0] == 9 || array[0] == 0)
									{
										UnitID = "-";
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
										UnitID = unitID;
									}
									p_WriteHandle = intPtr;
									HidD_GetPreparsedData(intPtr, ref zero);
									HidP_GetCaps(zero, ref hidp_CAPS);
									p_ReadHandle = CreateFile(lpFileName, 3221225472U, 3U, ref security_ATTRIBUTES, 3, 0U, 0);
									HidD_FreePreparsedData(ref zero);
									break;
								}
								CloseHandle(intPtr);
								num += 1;
							}
							else
							{
								result = false;
								CloseHandle(intPtr);
							}
						}
						else
						{
							result = false;
							CloseHandle(intPtr);
						}
					}
				}
			}
			SetupDiDestroyDeviceInfoList(deviceInfoSet);
			return result;
		}

        public static string UnitID = "";
		public struct SP_DEVICE_INTERFACE_DATA
		{
			public int cbSize;
			public Guid InterfaceClassGuid;
			public int Flags;
			public int Reserved;
		}
		public struct SP_DEVICE_INTERFACE_DETAIL_DATA
		{
			public int cbSize;
			public string DevicePath;
		}
		public struct SP_DEVINFO_DATA
		{
			public int cbSize;
			public Guid ClassGuid;
			public int DevInst;
			public int Reserved;
		}
		public struct HIDD_ATTRIBUTES
		{
			public int Size;
			public ushort VendorID;
			public ushort ProductID;
			public ushort VersionNumber;
		}
		public struct SECURITY_ATTRIBUTES
		{
			public int nLength;
			public int lpSecurityDescriptor;
			public int bInheritHandle;
		}
		public struct HIDP_CAPS
		{
			public short Usage;
			public short UsagePage;
			public short InputReportByteLength;
			public short OutputReportByteLength;
			public short FeatureReportByteLength;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
			public short[] Reserved;
			public short NumberLinkCollectionNodes;
			public short NumberInputButtonCaps;
			public short NumberInputValueCaps;
			public short NumberInputDataIndices;
			public short NumberOutputButtonCaps;
			public short NumberOutputValueCaps;
			public short NumberOutputDataIndices;
			public short NumberFeatureButtonCaps;
			public short NumberFeatureValueCaps;
			public short NumberFeatureDataIndices;
		}
	}
}
