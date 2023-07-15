using System;

namespace PICkit2V3
{
	// Token: 0x02000042 RID: 66
	public class PIC24F_PE
	{
		// Token: 0x06000260 RID: 608 RVA: 0x00045E60 File Offset: 0x00044E60
		public static bool DownloadPE()
		{
			PICkitFunctions.RunScript(0, 1);
			PICkitFunctions.ExecuteScript((int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DebugWriteVectorScript);
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrPrepScript != 0)
			{
				PICkitFunctions.DownloadAddress3(8388608);
				PICkitFunctions.RunScript(6, 1);
			}
			int num = 0;
			byte[] array = new byte[64];
			for (int i = 0; i < 8; i++)
			{
				int k;
				for (int j = 0; j < 4; j++)
				{
					k = 0;
					if (j == 0)
					{
						array[k++] = 167;
					}
					array[k++] = 168;
					array[k++] = 48;
					for (int l = 0; l < 16; l++)
					{
						array[k++] = (byte)(PIC24F_PE.PIC24_PE_Code[num] & 255U);
						array[k++] = (byte)(PIC24F_PE.PIC24_PE_Code[num] >> 8 & 255U);
						array[k++] = (byte)(PIC24F_PE.PIC24_PE_Code[num] >> 16 & 255U);
						num++;
					}
					while (k < 64)
					{
						array[k] = 173;
						k++;
					}
					PICkitFunctions.writeUSB(array);
				}
				k = 0;
				array[k++] = 166;
				array[k++] = 0;
				array[k++] = 212;
				array[k++] = 0;
				array[k++] = 211;
				array[k++] = 1;
				array[k++] = 212;
				array[k++] = 3;
				array[k++] = 211;
				array[k++] = 2;
				array[k++] = 217;
				array[k++] = 128;
				array[k++] = 11;
				array[k++] = 187;
				array[k++] = 216;
				array[k++] = 216;
				array[k++] = 217;
				array[k++] = 129;
				array[k++] = 155;
				array[k++] = 187;
				array[k++] = 216;
				array[k++] = 216;
				array[k++] = 217;
				array[k++] = 130;
				array[k++] = 139;
				array[k++] = 187;
				array[k++] = 216;
				array[k++] = 216;
				array[k++] = 217;
				array[k++] = 131;
				array[k++] = 27;
				array[k++] = 187;
				array[k++] = 216;
				array[k++] = 216;
				array[k++] = 233;
				array[k++] = 32;
				array[k++] = 31;
				array[k++] = 217;
				array[k++] = 97;
				array[k++] = 231;
				array[k++] = 168;
				array[k++] = 216;
				array[k++] = 217;
				array[k++] = 0;
				array[k++] = 2;
				array[k++] = 4;
				array[k++] = 216;
				array[k++] = 233;
				array[k++] = 1;
				array[k++] = 3;
				array[k++] = 231;
				array[k++] = 72;
				array[1] = (byte)(k - 2);
				while (k < 64)
				{
					array[k] = 173;
					k++;
				}
				PICkitFunctions.writeUSB(array);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrPrepScript != 0)
			{
				PICkitFunctions.DownloadAddress3(8388608);
				PICkitFunctions.RunScript(5, 1);
			}
			byte[] array2 = new byte[128];
			num = 0;
			for (int m = 0; m < 16; m++)
			{
				PICkitFunctions.RunScriptUploadNoLen(3, 1);
				Array.Copy(PICkitFunctions.Usb_read_array, 1L, array2, 0L, 64L);
				PICkitFunctions.UploadDataNoLen();
				Array.Copy(PICkitFunctions.Usb_read_array, 1L, array2, 64L, 64L);
				int num2 = 0;
				for (int n = 0; n < 32; n++)
				{
					uint num3 = (uint)array2[num2++];
					num3 |= (uint)((uint)array2[num2++] << 8);
					num3 |= (uint)((uint)array2[num2++] << 16);
					if (num3 != PIC24F_PE.PIC24_PE_Code[num++])
					{
						PICkitFunctions.RunScript(1, 1);
						return false;
					}
				}
			}
			PICkitFunctions.RunScript(1, 1);
			return true;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000462D8 File Offset: 0x000452D8
		public static bool PE_Connect()
		{
			PICkitFunctions.RunScript(0, 1);
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrPrepScript != 0)
			{
				PICkitFunctions.DownloadAddress3(8389568);
				PICkitFunctions.RunScript(5, 1);
			}
			byte[] array = new byte[128];
			PICkitFunctions.RunScriptUploadNoLen(3, 1);
			Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 0L, 64L);
			PICkitFunctions.UploadDataNoLen();
			Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 64L, 64L);
			int num = (int)array[72];
			num |= (int)array[73] << 8;
			if (num != 155)
			{
				PICkitFunctions.RunScript(1, 1);
				return false;
			}
			num = (int)array[75];
			num |= (int)array[76] << 8;
			if (num != 38)
			{
				PICkitFunctions.RunScript(1, 1);
				return false;
			}
			PICkitFunctions.RunScript(1, 1);
			int i = 0;
			byte[] array2 = new byte[64];
			array2[i++] = 166;
			array2[i++] = 0;
			array2[i++] = 250;
			array2[i++] = 247;
			array2[i++] = 249;
			array2[i++] = 245;
			array2[i++] = 243;
			array2[i++] = 0;
			array2[i++] = 232;
			array2[i++] = 20;
			array2[i++] = 246;
			array2[i++] = 251;
			array2[i++] = 231;
			array2[i++] = 23;
			array2[i++] = 250;
			array2[i++] = 247;
			array2[i++] = 231;
			array2[i++] = 47;
			array2[i++] = 242;
			array2[i++] = 178;
			array2[i++] = 242;
			array2[i++] = 194;
			array2[i++] = 242;
			array2[i++] = 18;
			array2[i++] = 242;
			array2[i++] = 10;
			array2[i++] = 246;
			array2[i++] = 251;
			array2[i++] = 232;
			array2[i++] = 6;
			array2[1] = (byte)(i - 2);
			while (i < 64)
			{
				array2[i] = 173;
				i++;
			}
			PICkitFunctions.writeUSB(array2);
			i = 0;
			array2[i++] = 166;
			array2[i++] = 12;
			array2[i++] = 242;
			array2[i++] = 0;
			array2[i++] = 242;
			array2[i++] = 128;
			array2[i++] = 243;
			array2[i++] = 2;
			array2[i++] = 231;
			array2[i++] = 5;
			array2[i++] = 240;
			array2[i++] = 240;
			array2[i++] = 240;
			array2[i++] = 240;
			array2[i++] = 170;
			while (i < 64)
			{
				array2[i] = 173;
				i++;
			}
			PICkitFunctions.writeUSB(array2);
			if (!PICkitFunctions.readUSB())
			{
				PICkitFunctions.RunScript(1, 1);
				return false;
			}
			if (PICkitFunctions.Usb_read_array[1] != 4)
			{
				PICkitFunctions.RunScript(1, 1);
				return false;
			}
			if (PICkitFunctions.Usb_read_array[2] != 8 || PICkitFunctions.Usb_read_array[3] != 0 || PICkitFunctions.Usb_read_array[4] != 0 || PICkitFunctions.Usb_read_array[5] != 64)
			{
				PICkitFunctions.RunScript(1, 1);
				return false;
			}
			i = 0;
			array2[i++] = 166;
			array2[i++] = 14;
			array2[i++] = 243;
			array2[i++] = 0;
			array2[i++] = 242;
			array2[i++] = 13;
			array2[i++] = 242;
			array2[i++] = 128;
			array2[i++] = 243;
			array2[i++] = 2;
			array2[i++] = 231;
			array2[i++] = 5;
			array2[i++] = 240;
			array2[i++] = 240;
			array2[i++] = 240;
			array2[i++] = 240;
			array2[i++] = 170;
			while (i < 64)
			{
				array2[i] = 173;
				i++;
			}
			PICkitFunctions.writeUSB(array2);
			if (!PICkitFunctions.readUSB())
			{
				PICkitFunctions.RunScript(1, 1);
				return false;
			}
			if (PICkitFunctions.Usb_read_array[1] != 4)
			{
				PICkitFunctions.RunScript(1, 1);
				return false;
			}
			if (PICkitFunctions.Usb_read_array[2] != 216 || PIC24F_PE.BitReverseTable[(int)PICkitFunctions.Usb_read_array[3]] != 38 || PICkitFunctions.Usb_read_array[4] != 0 || PICkitFunctions.Usb_read_array[5] != 64)
			{
				PICkitFunctions.RunScript(1, 1);
				return false;
			}
			return true;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00046768 File Offset: 0x00045768
		public static bool PE_DownloadAndConnect()
		{
			PIC24F_PE.ICSPSpeedRestore = PICkitFunctions.LastICSPSpeed;
			if (PICkitFunctions.LastICSPSpeed < 2)
			{
				PICkitFunctions.SetProgrammingSpeed(2);
			}
			if (!PIC24F_PE.PE_Connect())
			{
				PIC24F_PE.UpdateStatusWinText("Downloading Programming Executive...");
				if (!PIC24F_PE.DownloadPE())
				{
					PIC24F_PE.UpdateStatusWinText("Downloading Programming Executive...FAILED!");
					PIC24F_PE.restoreICSPSpeed();
					return false;
				}
				if (!PIC24F_PE.PE_Connect())
				{
					PIC24F_PE.UpdateStatusWinText("Downloading Programming Executive...FAILED!");
					PIC24F_PE.restoreICSPSpeed();
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000467DF File Offset: 0x000457DF
		private static void restoreICSPSpeed()
		{
			if (PIC24F_PE.ICSPSpeedRestore != PICkitFunctions.LastICSPSpeed)
			{
				PICkitFunctions.SetProgrammingSpeed(PIC24F_PE.ICSPSpeedRestore);
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000467F8 File Offset: 0x000457F8
		public static bool PE24FBlankCheck(string saveText)
		{
			if (!PIC24F_PE.PE_DownloadAndConnect())
			{
				return false;
			}
			PIC24F_PE.UpdateStatusWinText(saveText);
			int num = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem - (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords);
			byte[] array = new byte[128];
			byte bytesPerLocation = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
			int num2 = 32;
			int num3 = 0;
			byte[] array2 = new byte[64];
			PIC24F_PE.ResetStatusBar((int)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem / (ulong)((long)num2)));
			uint num4 = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
			for (;;)
			{
				int i = 0;
				array2[i++] = 166;
				array2[i++] = 0;
				array2[i++] = 243;
				array2[i++] = 0;
				array2[i++] = 242;
				array2[i++] = 4;
				array2[i++] = 242;
				array2[i++] = 32;
				array2[i++] = 242;
				array2[i++] = 0;
				array2[i++] = 242;
				array2[i++] = PIC24F_PE.BitReverseTable[num2];
				array2[i++] = 242;
				array2[i++] = 0;
				array2[i++] = 242;
				array2[i++] = PIC24F_PE.BitReverseTable[num3 >> 15 & 255];
				array2[i++] = 242;
				array2[i++] = PIC24F_PE.BitReverseTable[num3 >> 7 & 255];
				array2[i++] = 242;
				array2[i++] = PIC24F_PE.BitReverseTable[num3 << 1 & 255];
				array2[i++] = 243;
				array2[i++] = 2;
				array2[i++] = 231;
				array2[i++] = 5;
				array2[i++] = 239;
				array2[i++] = 239;
				array2[i++] = 239;
				array2[i++] = 239;
				array2[i++] = 240;
				array2[i++] = 240;
				array2[i++] = 240;
				array2[i++] = 233;
				array2[i++] = 3;
				array2[i++] = 31;
				array2[i++] = 172;
				array2[i++] = 172;
				array2[1] = (byte)(i - 4);
				while (i < 64)
				{
					array2[i] = 173;
					i++;
				}
				PICkitFunctions.writeUSB(array2);
				PICkitFunctions.GetUpload();
				Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 0L, 64L);
				PICkitFunctions.GetUpload();
				Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 64L, 64L);
				int num5 = 0;
				for (int j = 0; j < num2; j += 2)
				{
					uint num6 = (uint)((uint)PIC24F_PE.BitReverseTable[(int)array[num5++]] << 8);
					num6 |= (uint)PIC24F_PE.BitReverseTable[(int)array[num5++]];
					uint num7 = (uint)((uint)PIC24F_PE.BitReverseTable[(int)array[num5++]] << 16);
					num6 |= (uint)((uint)PIC24F_PE.BitReverseTable[(int)array[num5++]] << 16);
					num7 |= (uint)((uint)PIC24F_PE.BitReverseTable[(int)array[num5++]] << 8);
					num7 |= (uint)PIC24F_PE.BitReverseTable[(int)array[num5++]];
					if (num3 >= num)
					{
						num4 = (16711680U | (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank[num3 - num]);
					}
					if (num4 != num6)
					{
						goto Block_4;
					}
					num3++;
					if (num3 >= num)
					{
						num4 = (16711680U | (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank[num3 - num]);
					}
					if (num4 != num7)
					{
						goto Block_6;
					}
					num3++;
					if ((long)num3 >= (long)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem))
					{
						break;
					}
				}
				PIC24F_PE.StepStatusBar();
				if ((long)num3 >= (long)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem))
				{
					goto Block_8;
				}
			}
			Block_4:
			string text = "Program Memory is not blank starting at address\n";
			text += string.Format("0x{0:X6}", num3 * (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].AddressIncrement);
			PIC24F_PE.UpdateStatusWinText(text);
			PICkitFunctions.RunScript(1, 1);
			PIC24F_PE.restoreICSPSpeed();
			return false;
			Block_6:
			string text2 = "Program Memory is not blank starting at address\n";
			text2 += string.Format("0x{0:X6}", num3 * (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].AddressIncrement);
			PIC24F_PE.UpdateStatusWinText(text2);
			PICkitFunctions.RunScript(1, 1);
			PIC24F_PE.restoreICSPSpeed();
			return false;
			Block_8:
			PICkitFunctions.RunScript(1, 1);
			PIC24F_PE.restoreICSPSpeed();
			return true;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00046D3C File Offset: 0x00045D3C
		public static bool PE24FRead(string saveText)
		{
			if (!PIC24F_PE.PE_DownloadAndConnect())
			{
				return false;
			}
			PIC24F_PE.UpdateStatusWinText(saveText);
			byte[] array = new byte[128];
			byte bytesPerLocation = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
			int num = 32;
			int num2 = 0;
			byte[] array2 = new byte[64];
			PIC24F_PE.ResetStatusBar((int)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem / (ulong)((long)num)));
			do
			{
				int i = 0;
				array2[i++] = 166;
				array2[i++] = 0;
				array2[i++] = 243;
				array2[i++] = 0;
				array2[i++] = 242;
				array2[i++] = 4;
				array2[i++] = 242;
				array2[i++] = 32;
				array2[i++] = 242;
				array2[i++] = 0;
				array2[i++] = 242;
				array2[i++] = PIC24F_PE.BitReverseTable[num];
				array2[i++] = 242;
				array2[i++] = 0;
				array2[i++] = 242;
				array2[i++] = PIC24F_PE.BitReverseTable[num2 >> 15 & 255];
				array2[i++] = 242;
				array2[i++] = PIC24F_PE.BitReverseTable[num2 >> 7 & 255];
				array2[i++] = 242;
				array2[i++] = PIC24F_PE.BitReverseTable[num2 << 1 & 255];
				array2[i++] = 243;
				array2[i++] = 2;
				array2[i++] = 231;
				array2[i++] = 5;
				array2[i++] = 239;
				array2[i++] = 239;
				array2[i++] = 239;
				array2[i++] = 239;
				array2[i++] = 240;
				array2[i++] = 240;
				array2[i++] = 240;
				array2[i++] = 233;
				array2[i++] = 3;
				array2[i++] = 31;
				array2[i++] = 172;
				array2[i++] = 172;
				array2[1] = (byte)(i - 4);
				while (i < 64)
				{
					array2[i] = 173;
					i++;
				}
				PICkitFunctions.writeUSB(array2);
				PICkitFunctions.GetUpload();
				Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 0L, 64L);
				PICkitFunctions.GetUpload();
				Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 64L, 64L);
				int num3 = 0;
				for (int j = 0; j < num; j += 2)
				{
					uint num4 = (uint)((uint)PIC24F_PE.BitReverseTable[(int)array[num3++]] << 8);
					num4 |= (uint)PIC24F_PE.BitReverseTable[(int)array[num3++]];
					uint num5 = (uint)((uint)PIC24F_PE.BitReverseTable[(int)array[num3++]] << 16);
					num4 |= (uint)((uint)PIC24F_PE.BitReverseTable[(int)array[num3++]] << 16);
					num5 |= (uint)((uint)PIC24F_PE.BitReverseTable[(int)array[num3++]] << 8);
					num5 |= (uint)PIC24F_PE.BitReverseTable[(int)array[num3++]];
					PICkitFunctions.DeviceBuffers.ProgramMemory[num2++] = num4;
					PICkitFunctions.DeviceBuffers.ProgramMemory[num2++] = num5;
					if ((long)num2 >= (long)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem))
					{
						break;
					}
				}
				PIC24F_PE.StepStatusBar();
			}
			while ((long)num2 < (long)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem));
			PICkitFunctions.RunScript(1, 1);
			PIC24F_PE.restoreICSPSpeed();
			return true;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00047134 File Offset: 0x00046134
		public static bool PE24FWrite(int endOfBuffer, string saveText, bool writeVerify)
		{
			if (!PIC24F_PE.PE_DownloadAndConnect())
			{
				PIC24F_PE.PEGoodOnWrite = false;
				return false;
			}
			PIC24F_PE.PEGoodOnWrite = true;
			PIC24F_PE.UpdateStatusWinText(saveText);
			if ((long)endOfBuffer == (long)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem))
			{
				for (int i = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords; i > 0; i--)
				{
					PICkitFunctions.DeviceBuffers.ProgramMemory[endOfBuffer - i] &= (16711680U | (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank[(int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords - i]);
				}
			}
			byte[] array = new byte[256];
			int num = 64;
			int num2 = 0;
			PIC24F_PE.ResetStatusBar(endOfBuffer / num);
			for (;;)
			{
				int num3 = 0;
				for (int j = 0; j < num; j += 2)
				{
					uint num4 = PICkitFunctions.DeviceBuffers.ProgramMemory[num2++];
					array[num3 + 1] = PIC24F_PE.BitReverseTable[(int)((UIntPtr)(num4 & 255U))];
					num4 >>= 8;
					array[num3] = PIC24F_PE.BitReverseTable[(int)((UIntPtr)(num4 & 255U))];
					num4 >>= 8;
					array[num3 + 3] = PIC24F_PE.BitReverseTable[(int)((UIntPtr)(num4 & 255U))];
					num4 = PICkitFunctions.DeviceBuffers.ProgramMemory[num2++];
					array[num3 + 5] = PIC24F_PE.BitReverseTable[(int)((UIntPtr)(num4 & 255U))];
					num4 >>= 8;
					array[num3 + 4] = PIC24F_PE.BitReverseTable[(int)((UIntPtr)(num4 & 255U))];
					num4 >>= 8;
					array[num3 + 2] = PIC24F_PE.BitReverseTable[(int)((UIntPtr)(num4 & 255U))];
					num3 += 6;
				}
				for (int k = PICkitFunctions.DataClrAndDownload(array, 0); k < num3; k = PICkitFunctions.DataDownload(array, k, num3))
				{
				}
				int l = 0;
				byte[] array2 = new byte[64];
				array2[l++] = 166;
				array2[l++] = 0;
				array2[l++] = 243;
				array2[l++] = 0;
				array2[l++] = 242;
				array2[l++] = 10;
				array2[l++] = 242;
				array2[l++] = 198;
				array2[l++] = 242;
				array2[l++] = 0;
				array2[l++] = 242;
				array2[l++] = PIC24F_PE.BitReverseTable[num2 - 64 >> 15 & 255];
				array2[l++] = 242;
				array2[l++] = PIC24F_PE.BitReverseTable[num2 - 64 >> 7 & 255];
				array2[l++] = 242;
				array2[l++] = PIC24F_PE.BitReverseTable[num2 - 64 << 1 & 255];
				array2[l++] = 241;
				array2[l++] = 241;
				array2[l++] = 241;
				array2[l++] = 233;
				array2[l++] = 3;
				array2[l++] = 63;
				array2[l++] = 243;
				array2[l++] = 2;
				array2[l++] = 231;
				array2[l++] = 118;
				array2[l++] = 240;
				array2[l++] = 240;
				array2[l++] = 240;
				array2[l++] = 240;
				array2[l++] = 170;
				array2[1] = (byte)(l - 3);
				while (l < 64)
				{
					array2[l] = 173;
					l++;
				}
				PICkitFunctions.writeUSB(array2);
				if (!PICkitFunctions.readUSB())
				{
					break;
				}
				if (PICkitFunctions.Usb_read_array[1] != 4)
				{
					goto Block_7;
				}
				if (PIC24F_PE.BitReverseTable[(int)PICkitFunctions.Usb_read_array[2]] != 21 || PICkitFunctions.Usb_read_array[3] != 0 || PICkitFunctions.Usb_read_array[4] != 0 || PIC24F_PE.BitReverseTable[(int)PICkitFunctions.Usb_read_array[5]] != 2)
				{
					goto IL_45D;
				}
				PIC24F_PE.StepStatusBar();
				if (num2 >= endOfBuffer)
				{
					goto Block_11;
				}
			}
			PIC24F_PE.UpdateStatusWinText("Programming Executive Error during Write.");
			PICkitFunctions.RunScript(1, 1);
			PIC24F_PE.restoreICSPSpeed();
			return false;
			Block_7:
			PIC24F_PE.UpdateStatusWinText("Programming Executive Error during Write.");
			PICkitFunctions.RunScript(1, 1);
			PIC24F_PE.restoreICSPSpeed();
			return false;
			IL_45D:
			PIC24F_PE.UpdateStatusWinText("Programming Executive Error during Write.");
			PICkitFunctions.RunScript(1, 1);
			PIC24F_PE.restoreICSPSpeed();
			return false;
			Block_11:
			if (!writeVerify)
			{
				PICkitFunctions.RunScript(1, 1);
				PIC24F_PE.restoreICSPSpeed();
			}
			return true;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000475E0 File Offset: 0x000465E0
		public static bool PE24FVerify(string saveText, bool writeVerify, int lastLocation)
		{
			if ((!writeVerify || !PIC24F_PE.PEGoodOnWrite) && !PIC24F_PE.PE_DownloadAndConnect())
			{
				return false;
			}
			PIC24F_PE.PEGoodOnWrite = false;
			if (!writeVerify)
			{
				lastLocation = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem;
			}
			PIC24F_PE.UpdateStatusWinText(saveText);
			byte[] array = new byte[128];
			byte bytesPerLocation = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
			int num = 32;
			int num2 = 0;
			byte[] array2 = new byte[64];
			PIC24F_PE.ResetStatusBar(lastLocation / num);
			for (;;)
			{
				int i = 0;
				array2[i++] = 166;
				array2[i++] = 0;
				array2[i++] = 243;
				array2[i++] = 0;
				array2[i++] = 242;
				array2[i++] = 4;
				array2[i++] = 242;
				array2[i++] = 32;
				array2[i++] = 242;
				array2[i++] = 0;
				array2[i++] = 242;
				array2[i++] = PIC24F_PE.BitReverseTable[num];
				array2[i++] = 242;
				array2[i++] = 0;
				array2[i++] = 242;
				array2[i++] = PIC24F_PE.BitReverseTable[num2 >> 15 & 255];
				array2[i++] = 242;
				array2[i++] = PIC24F_PE.BitReverseTable[num2 >> 7 & 255];
				array2[i++] = 242;
				array2[i++] = PIC24F_PE.BitReverseTable[num2 << 1 & 255];
				array2[i++] = 243;
				array2[i++] = 2;
				array2[i++] = 231;
				array2[i++] = 5;
				array2[i++] = 239;
				array2[i++] = 239;
				array2[i++] = 239;
				array2[i++] = 239;
				array2[i++] = 240;
				array2[i++] = 240;
				array2[i++] = 240;
				array2[i++] = 233;
				array2[i++] = 3;
				array2[i++] = 31;
				array2[i++] = 172;
				array2[i++] = 172;
				array2[1] = (byte)(i - 4);
				while (i < 64)
				{
					array2[i] = 173;
					i++;
				}
				PICkitFunctions.writeUSB(array2);
				PICkitFunctions.GetUpload();
				Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 0L, 64L);
				PICkitFunctions.GetUpload();
				Array.Copy(PICkitFunctions.Usb_read_array, 1L, array, 64L, 64L);
				int num3 = 0;
				for (int j = 0; j < num; j += 2)
				{
					uint num4 = (uint)((uint)PIC24F_PE.BitReverseTable[(int)array[num3++]] << 8);
					num4 |= (uint)PIC24F_PE.BitReverseTable[(int)array[num3++]];
					uint num5 = (uint)((uint)PIC24F_PE.BitReverseTable[(int)array[num3++]] << 16);
					num4 |= (uint)((uint)PIC24F_PE.BitReverseTable[(int)array[num3++]] << 16);
					num5 |= (uint)((uint)PIC24F_PE.BitReverseTable[(int)array[num3++]] << 8);
					num5 |= (uint)PIC24F_PE.BitReverseTable[(int)array[num3++]];
					if (PICkitFunctions.DeviceBuffers.ProgramMemory[num2++] != num4)
					{
						goto Block_5;
					}
					if (PICkitFunctions.DeviceBuffers.ProgramMemory[num2++] != num5)
					{
						goto Block_7;
					}
					if (num2 >= lastLocation)
					{
						break;
					}
				}
				PIC24F_PE.StepStatusBar();
				if (num2 >= lastLocation)
				{
					goto Block_10;
				}
			}
			Block_5:
			string text;
			if (!writeVerify)
			{
				text = "Verification of Program Memory failed at address\n";
			}
			else
			{
				text = "Programming failed at Program Memory address\n";
			}
			text += string.Format("0x{0:X6}", (num2 - 1) * (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].AddressIncrement);
			PIC24F_PE.UpdateStatusWinText(text);
			PICkitFunctions.RunScript(1, 1);
			PIC24F_PE.restoreICSPSpeed();
			return false;
			Block_7:
			string text2;
			if (!writeVerify)
			{
				text2 = "Verification of Program Memory failed at address\n";
			}
			else
			{
				text2 = "Programming failed at Program Memory address\n";
			}
			text2 += string.Format("0x{0:X6}", (num2 - 1) * (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].AddressIncrement);
			PIC24F_PE.UpdateStatusWinText(text2);
			PICkitFunctions.RunScript(1, 1);
			PIC24F_PE.restoreICSPSpeed();
			return false;
			Block_10:
			PICkitFunctions.RunScript(1, 1);
			PIC24F_PE.restoreICSPSpeed();
			return true;
		}

		// Token: 0x04000561 RID: 1377
		private const int PIC24_PE_Version = 38;

		// Token: 0x04000562 RID: 1378
		private const int PIC24_PE_ID = 155;

		// Token: 0x04000563 RID: 1379
		public static DelegateStatusWin UpdateStatusWinText;

		// Token: 0x04000564 RID: 1380
		public static DelegateResetStatusBar ResetStatusBar;

		// Token: 0x04000565 RID: 1381
		public static DelegateStepStatusBar StepStatusBar;

		// Token: 0x04000566 RID: 1382
		private static byte ICSPSpeedRestore = 0;

		// Token: 0x04000567 RID: 1383
		private static bool PEGoodOnWrite = false;

		// Token: 0x04000568 RID: 1384
		private static uint[] PIC24_PE_Code = new uint[]
		{
			262272U,
			128U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			8388894U,
			16384000U,
			2133231U,
			2260864U,
			8913152U,
			0U,
			458754U,
			16416768U,
			393216U,
			16384000U,
			15400960U,
			8927776U,
			459088U,
			458968U,
			458763U,
			3670013U,
			16252980U,
			7872392U,
			11780104U,
			8913320U,
			7865423U,
			16384000U,
			16646144U,
			16416768U,
			16318516U,
			409600U,
			16384002U,
			8404992U,
			14549068U,
			16482304U,
			7868160U,
			7864350U,
			5246949U,
			3276821U,
			7864478U,
			5279717U,
			3932165U,
			7864478U,
			4227168U,
			5246946U,
			4063252U,
			3604487U,
			7864350U,
			5246954U,
			3276812U,
			7864478U,
			5279723U,
			3276811U,
			3604492U,
			11045063U,
			11118791U,
			11126983U,
			11135175U,
			458766U,
			3604491U,
			458816U,
			3604489U,
			458881U,
			3604487U,
			458913U,
			3604485U,
			11045063U,
			11053255U,
			11126983U,
			11135175U,
			458754U,
			16416768U,
			393216U,
			16384000U,
			8406577U,
			3080192U,
			6324352U,
			2162688U,
			5279616U,
			3276806U,
			8406577U,
			3080192U,
			6324352U,
			2293760U,
			5279616U,
			3801091U,
			15417344U,
			12052678U,
			3604482U,
			11780112U,
			12052678U,
			8404992U,
			14549068U,
			16482304U,
			6291567U,
			14483784U,
			8406577U,
			3084272U,
			6324224U,
			7340034U,
			8930864U,
			8406577U,
			2158592U,
			6324352U,
			2105344U,
			5279616U,
			3801101U,
			8405008U,
			13697024U,
			12124259U,
			15237120U,
			8930880U,
			8405008U,
			6291553U,
			14681088U,
			3276806U,
			8406592U,
			15237120U,
			8930880U,
			3604482U,
			2097184U,
			8930880U,
			458882U,
			16416768U,
			393216U,
			2359312U,
			8928000U,
			2129925U,
			15400960U,
			9454629U,
			9438373U,
			4359014U,
			8913304U,
			7865225U,
			2097408U,
			12258230U,
			12311478U,
			12315574U,
			12262326U,
			12258230U,
			12311478U,
			12315574U,
			12262326U,
			15269888U,
			3866614U,
			589829U,
			7867206U,
			2097280U,
			5473152U,
			12258230U,
			11061089U,
			2098519U,
			8928055U,
			2099879U,
			8928055U,
			11069281U,
			0U,
			0U,
			8403712U,
			10743808U,
			3276797U,
			458757U,
			14483532U,
			2133089U,
			7866496U,
			524195U,
			393216U,
			2129920U,
			15401088U,
			9454624U,
			9438368U,
			2097351U,
			4194790U,
			8913304U,
			7864969U,
			2097652U,
			12190485U,
			14757939U,
			3801099U,
			12245941U,
			12243925U,
			14757939U,
			3801095U,
			12190517U,
			14757939U,
			3801092U,
			15270404U,
			3932148U,
			2097168U,
			3604481U,
			2097184U,
			393216U,
			15671346U,
			3145703U,
			3604480U,
			15434240U,
			3145702U,
			2129920U,
			9437584U,
			9437472U,
			11599890U,
			11632643U,
			3276814U,
			12189783U,
			15171588U,
			3604489U,
			12238871U,
			15172612U,
			3604486U,
			14757895U,
			3866613U,
			8389008U,
			15204352U,
			8913296U,
			3670001U,
			2097393U,
			3604481U,
			2100993U,
			2203648U,
			7340033U,
			2133091U,
			7870848U,
			2097184U,
			7866752U,
			458780U,
			393216U,
			2097761U,
			2207744U,
			7340033U,
			2133089U,
			7870592U,
			2097184U,
			7866496U,
			458771U,
			393216U,
			458855U,
			2129927U,
			7871360U,
			2162675U,
			6291843U,
			15270275U,
			3276804U,
			458848U,
			7871360U,
			15270275U,
			3866620U,
			11133505U,
			2117632U,
			8917520U,
			15704648U,
			11067969U,
			15704648U,
			393216U,
			15671872U,
			589853U,
			0U,
			2113536U,
			8917520U,
			11092101U,
			11067969U,
			2133095U,
			7864375U,
			8917568U,
			7864375U,
			458831U,
			2133088U,
			9437840U,
			15303301U,
			3276856U,
			9453584U,
			11682032U,
			11780113U,
			14746625U,
			3801098U,
			2129920U,
			9453728U,
			16482433U,
			8913297U,
			9438112U,
			12189751U,
			458815U,
			15270533U,
			3866620U,
			3604521U,
			11780129U,
			14746625U,
			3801134U,
			2129920U,
			9437840U,
			13697797U,
			11534400U,
			7880848U,
			7865345U,
			7864528U,
			7865217U,
			15269893U,
			3276821U,
			2097153U,
			8913304U,
			0U,
			12191895U,
			458794U,
			12245143U,
			11534375U,
			11567112U,
			8913304U,
			0U,
			12243095U,
			458787U,
			12189719U,
			458785U,
			11534375U,
			11567112U,
			8913304U,
			15270662U,
			3866606U,
			10878981U,
			3604487U,
			8913304U,
			0U,
			12189719U,
			458774U,
			12238871U,
			16482304U,
			458771U,
			11419781U,
			3670014U,
			11092101U,
			11133505U,
			11035203U,
			11067969U,
			8393281U,
			393216U,
			11780289U,
			14746625U,
			3866613U,
			7864375U,
			458758U,
			3670002U,
			11419781U,
			3670014U,
			11092101U,
			8393280U,
			393216U,
			7872385U,
			11419781U,
			3670014U,
			11419781U,
			3670012U,
			11092101U,
			8393281U,
			8917568U,
			7864527U,
			393216U,
			8389136U,
			11733504U,
			8913424U,
			2146304U,
			8917520U,
			11092101U,
			15671464U,
			11010217U,
			11026581U,
			2621440U,
			8917504U,
			15673186U,
			15673188U,
			393216U,
			2252U,
			2U,
			0U,
			2048U,
			204U,
			0U,
			0U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			155U,
			38U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U
		};

		// Token: 0x04000569 RID: 1385
		private static byte[] BitReverseTable = new byte[]
		{
			0,
			128,
			64,
			192,
			32,
			160,
			96,
			224,
			16,
			144,
			80,
			208,
			48,
			176,
			112,
			240,
			8,
			136,
			72,
			200,
			40,
			168,
			104,
			232,
			24,
			152,
			88,
			216,
			56,
			184,
			120,
			248,
			4,
			132,
			68,
			196,
			36,
			164,
			100,
			228,
			20,
			148,
			84,
			212,
			52,
			180,
			116,
			244,
			12,
			140,
			76,
			204,
			44,
			172,
			108,
			236,
			28,
			156,
			92,
			220,
			60,
			188,
			124,
			252,
			2,
			130,
			66,
			194,
			34,
			162,
			98,
			226,
			18,
			146,
			82,
			210,
			50,
			178,
			114,
			242,
			10,
			138,
			74,
			202,
			42,
			170,
			106,
			234,
			26,
			154,
			90,
			218,
			58,
			186,
			122,
			250,
			6,
			134,
			70,
			198,
			38,
			166,
			102,
			230,
			22,
			150,
			86,
			214,
			54,
			182,
			118,
			246,
			14,
			142,
			78,
			206,
			46,
			174,
			110,
			238,
			30,
			158,
			94,
			222,
			62,
			190,
			126,
			254,
			1,
			129,
			65,
			193,
			33,
			161,
			97,
			225,
			17,
			145,
			81,
			209,
			49,
			177,
			113,
			241,
			9,
			137,
			73,
			201,
			41,
			169,
			105,
			233,
			25,
			153,
			89,
			217,
			57,
			185,
			121,
			249,
			5,
			133,
			69,
			197,
			37,
			165,
			101,
			229,
			21,
			149,
			85,
			213,
			53,
			181,
			117,
			245,
			13,
			141,
			77,
			205,
			45,
			173,
			109,
			237,
			29,
			157,
			93,
			221,
			61,
			189,
			125,
			253,
			3,
			131,
			67,
			195,
			35,
			163,
			99,
			227,
			19,
			147,
			83,
			211,
			51,
			179,
			115,
			243,
			11,
			139,
			75,
			203,
			43,
			171,
			107,
			235,
			27,
			155,
			91,
			219,
			59,
			187,
			123,
			251,
			7,
			135,
			71,
			199,
			39,
			167,
			103,
			231,
			23,
			151,
			87,
			215,
			55,
			183,
			119,
			247,
			15,
			143,
			79,
			207,
			47,
			175,
			111,
			239,
			31,
			159,
			95,
			223,
			63,
			191,
			127,
			byte.MaxValue
		};
	}
}
