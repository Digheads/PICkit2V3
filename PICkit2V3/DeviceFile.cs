using System;

namespace PICkit2V3
{
	// Token: 0x02000027 RID: 39
	public class DeviceFile
	{
		// Token: 0x04000399 RID: 921
		public DeviceFile.DeviceFileParams Info = default(DeviceFile.DeviceFileParams);

		// Token: 0x0400039A RID: 922
		public DeviceFile.DeviceFamilyParams[] Families;

		// Token: 0x0400039B RID: 923
		public DeviceFile.DevicePartParams[] PartsList;

		// Token: 0x0400039C RID: 924
		public DeviceFile.DeviceScripts[] Scripts;

		// Token: 0x02000028 RID: 40
		public struct DeviceFileParams
		{
			// Token: 0x0400039D RID: 925
			public int VersionMajor;

			// Token: 0x0400039E RID: 926
			public int VersionMinor;

			// Token: 0x0400039F RID: 927
			public int VersionDot;

			// Token: 0x040003A0 RID: 928
			public string VersionNotes;

			// Token: 0x040003A1 RID: 929
			public int NumberFamilies;

			// Token: 0x040003A2 RID: 930
			public int NumberParts;

			// Token: 0x040003A3 RID: 931
			public int NumberScripts;

			// Token: 0x040003A4 RID: 932
			public byte Compatibility;

			// Token: 0x040003A5 RID: 933
			public byte UNUSED1A;

			// Token: 0x040003A6 RID: 934
			public ushort UNUSED1B;

			// Token: 0x040003A7 RID: 935
			public uint UNUSED2;
		}

		// Token: 0x02000029 RID: 41
		public struct DeviceFamilyParams
		{
			// Token: 0x040003A8 RID: 936
			public ushort FamilyID;

			// Token: 0x040003A9 RID: 937
			public ushort FamilyType;

			// Token: 0x040003AA RID: 938
			public ushort SearchPriority;

			// Token: 0x040003AB RID: 939
			public string FamilyName;

			// Token: 0x040003AC RID: 940
			public ushort ProgEntryScript;

			// Token: 0x040003AD RID: 941
			public ushort ProgExitScript;

			// Token: 0x040003AE RID: 942
			public ushort ReadDevIDScript;

			// Token: 0x040003AF RID: 943
			public uint DeviceIDMask;

			// Token: 0x040003B0 RID: 944
			public uint BlankValue;

			// Token: 0x040003B1 RID: 945
			public byte BytesPerLocation;

			// Token: 0x040003B2 RID: 946
			public byte AddressIncrement;

			// Token: 0x040003B3 RID: 947
			public bool PartDetect;

			// Token: 0x040003B4 RID: 948
			public ushort ProgEntryVPPScript;

			// Token: 0x040003B5 RID: 949
			public ushort UNUSED1;

			// Token: 0x040003B6 RID: 950
			public byte EEMemBytesPerWord;

			// Token: 0x040003B7 RID: 951
			public byte EEMemAddressIncrement;

			// Token: 0x040003B8 RID: 952
			public byte UserIDHexBytes;

			// Token: 0x040003B9 RID: 953
			public byte UserIDBytes;

			// Token: 0x040003BA RID: 954
			public byte ProgMemHexBytes;

			// Token: 0x040003BB RID: 955
			public byte EEMemHexBytes;

			// Token: 0x040003BC RID: 956
			public byte ProgMemShift;

			// Token: 0x040003BD RID: 957
			public uint TestMemoryStart;

			// Token: 0x040003BE RID: 958
			public ushort TestMemoryLength;

			// Token: 0x040003BF RID: 959
			public float Vpp;
		}

		// Token: 0x0200002A RID: 42
		public struct DevicePartParams
		{
			// Token: 0x040003C0 RID: 960
			public string PartName;

			// Token: 0x040003C1 RID: 961
			public ushort Family;

			// Token: 0x040003C2 RID: 962
			public uint DeviceID;

			// Token: 0x040003C3 RID: 963
			public uint ProgramMem;

			// Token: 0x040003C4 RID: 964
			public ushort EEMem;

			// Token: 0x040003C5 RID: 965
			public uint EEAddr;

			// Token: 0x040003C6 RID: 966
			public byte ConfigWords;

			// Token: 0x040003C7 RID: 967
			public uint ConfigAddr;

			// Token: 0x040003C8 RID: 968
			public byte UserIDWords;

			// Token: 0x040003C9 RID: 969
			public uint UserIDAddr;

			// Token: 0x040003CA RID: 970
			public uint BandGapMask;

			// Token: 0x040003CB RID: 971
			public ushort[] ConfigMasks;

			// Token: 0x040003CC RID: 972
			public ushort[] ConfigBlank;

			// Token: 0x040003CD RID: 973
			public ushort CPMask;

			// Token: 0x040003CE RID: 974
			public byte CPConfig;

			// Token: 0x040003CF RID: 975
			public bool OSSCALSave;

			// Token: 0x040003D0 RID: 976
			public uint IgnoreAddress;

			// Token: 0x040003D1 RID: 977
			public float VddMin;

			// Token: 0x040003D2 RID: 978
			public float VddMax;

			// Token: 0x040003D3 RID: 979
			public float VddErase;

			// Token: 0x040003D4 RID: 980
			public byte CalibrationWords;

			// Token: 0x040003D5 RID: 981
			public ushort ChipEraseScript;

			// Token: 0x040003D6 RID: 982
			public ushort ProgMemAddrSetScript;

			// Token: 0x040003D7 RID: 983
			public byte ProgMemAddrBytes;

			// Token: 0x040003D8 RID: 984
			public ushort ProgMemRdScript;

			// Token: 0x040003D9 RID: 985
			public ushort ProgMemRdWords;

			// Token: 0x040003DA RID: 986
			public ushort EERdPrepScript;

			// Token: 0x040003DB RID: 987
			public ushort EERdScript;

			// Token: 0x040003DC RID: 988
			public ushort EERdLocations;

			// Token: 0x040003DD RID: 989
			public ushort UserIDRdPrepScript;

			// Token: 0x040003DE RID: 990
			public ushort UserIDRdScript;

			// Token: 0x040003DF RID: 991
			public ushort ConfigRdPrepScript;

			// Token: 0x040003E0 RID: 992
			public ushort ConfigRdScript;

			// Token: 0x040003E1 RID: 993
			public ushort ProgMemWrPrepScript;

			// Token: 0x040003E2 RID: 994
			public ushort ProgMemWrScript;

			// Token: 0x040003E3 RID: 995
			public ushort ProgMemWrWords;

			// Token: 0x040003E4 RID: 996
			public byte ProgMemPanelBufs;

			// Token: 0x040003E5 RID: 997
			public uint ProgMemPanelOffset;

			// Token: 0x040003E6 RID: 998
			public ushort EEWrPrepScript;

			// Token: 0x040003E7 RID: 999
			public ushort EEWrScript;

			// Token: 0x040003E8 RID: 1000
			public ushort EEWrLocations;

			// Token: 0x040003E9 RID: 1001
			public ushort UserIDWrPrepScript;

			// Token: 0x040003EA RID: 1002
			public ushort UserIDWrScript;

			// Token: 0x040003EB RID: 1003
			public ushort ConfigWrPrepScript;

			// Token: 0x040003EC RID: 1004
			public ushort ConfigWrScript;

			// Token: 0x040003ED RID: 1005
			public ushort OSCCALRdScript;

			// Token: 0x040003EE RID: 1006
			public ushort OSCCALWrScript;

			// Token: 0x040003EF RID: 1007
			public ushort DPMask;

			// Token: 0x040003F0 RID: 1008
			public bool WriteCfgOnErase;

			// Token: 0x040003F1 RID: 1009
			public bool BlankCheckSkipUsrIDs;

			// Token: 0x040003F2 RID: 1010
			public ushort IgnoreBytes;

			// Token: 0x040003F3 RID: 1011
			public ushort ChipErasePrepScript;

			// Token: 0x040003F4 RID: 1012
			public uint BootFlash;

			// Token: 0x040003F5 RID: 1013
			public ushort Config9Mask;

			// Token: 0x040003F6 RID: 1014
			public ushort Config9Blank;

			// Token: 0x040003F7 RID: 1015
			public ushort ProgMemEraseScript;

			// Token: 0x040003F8 RID: 1016
			public ushort EEMemEraseScript;

			// Token: 0x040003F9 RID: 1017
			public ushort ConfigMemEraseScript;

			// Token: 0x040003FA RID: 1018
			public ushort reserved1EraseScript;

			// Token: 0x040003FB RID: 1019
			public ushort reserved2EraseScript;

			// Token: 0x040003FC RID: 1020
			public ushort TestMemoryRdScript;

			// Token: 0x040003FD RID: 1021
			public ushort TestMemoryRdWords;

			// Token: 0x040003FE RID: 1022
			public ushort EERowEraseScript;

			// Token: 0x040003FF RID: 1023
			public ushort EERowEraseWords;

			// Token: 0x04000400 RID: 1024
			public bool ExportToMPLAB;

			// Token: 0x04000401 RID: 1025
			public ushort DebugHaltScript;

			// Token: 0x04000402 RID: 1026
			public ushort DebugRunScript;

			// Token: 0x04000403 RID: 1027
			public ushort DebugStatusScript;

			// Token: 0x04000404 RID: 1028
			public ushort DebugReadExecVerScript;

			// Token: 0x04000405 RID: 1029
			public ushort DebugSingleStepScript;

			// Token: 0x04000406 RID: 1030
			public ushort DebugBulkWrDataScript;

			// Token: 0x04000407 RID: 1031
			public ushort DebugBulkRdDataScript;

			// Token: 0x04000408 RID: 1032
			public ushort DebugWriteVectorScript;

			// Token: 0x04000409 RID: 1033
			public ushort DebugReadVectorScript;

			// Token: 0x0400040A RID: 1034
			public ushort DebugRowEraseScript;

			// Token: 0x0400040B RID: 1035
			public ushort DebugRowEraseSize;

			// Token: 0x0400040C RID: 1036
			public ushort DebugReserved5Script;

			// Token: 0x0400040D RID: 1037
			public ushort DebugReserved6Script;

			// Token: 0x0400040E RID: 1038
			public ushort DebugReserved7Script;

			// Token: 0x0400040F RID: 1039
			public ushort DebugReserved8Script;

			// Token: 0x04000410 RID: 1040
			public ushort LVPScript;
		}

		// Token: 0x0200002B RID: 43
		public struct DeviceScripts
		{
			// Token: 0x04000411 RID: 1041
			public ushort ScriptNumber;

			// Token: 0x04000412 RID: 1042
			public string ScriptName;

			// Token: 0x04000413 RID: 1043
			public ushort ScriptVersion;

			// Token: 0x04000414 RID: 1044
			public uint UNUSED1;

			// Token: 0x04000415 RID: 1045
			public ushort ScriptLength;

			// Token: 0x04000416 RID: 1046
			public ushort[] Script;

			// Token: 0x04000417 RID: 1047
			public string Comment;
		}
	}
}
