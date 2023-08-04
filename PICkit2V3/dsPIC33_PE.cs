using System;
using System.Threading;

namespace PICkit2V3
{
	public class DSPIC33_PE
	{
		public static bool DownloadPE()
		{
			PICkitFunctions.RunScript(0, 1);
			PICkitFunctions.ExecuteScript(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DebugWriteVectorScript);
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrPrepScript != 0)
			{
				PICkitFunctions.DownloadAddress3(8388608);
				PICkitFunctions.RunScript(6, 1);
			}
			int num = 0;
			uint num2 = 0U;
			byte[] array = new byte[64];
			for (int i = 0; i < 16; i++)
			{
				int k;
				for (int j = 0; j < 4; j++)
				{
					k = 0;
					if (j == 0)
						array[k++] = 167;

					array[k++] = 168;
					array[k++] = 48;
					for (int l = 0; l < 16; l++)
					{
						array[k++] = (byte)(dsPIC33_PE_Code[num] & 255U);
						array[k++] = (byte)(dsPIC33_PE_Code[num] >> 8 & 255U);
						array[k++] = (byte)(dsPIC33_PE_Code[num] >> 16 & 255U);
						num++;
					}
					num2 = dsPIC33_PE_Code[num - 4];
					while (k < 64)
					{
						array[k] = 173;
						k++;
					}
					PICkitFunctions.WriteUSB(array);
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
				array[k++] = 136;
				array[k++] = 0;
				array[k++] = 32;
				array[k++] = 217;
				array[k++] = 7;
				array[k++] = 4;
				array[k++] = 20;
				array[1] = (byte)(k - 2);
				while (k < 64)
				{
					array[k] = 173;
					k++;
				}
				PICkitFunctions.WriteUSB(array);
				k = 0;
				array[k++] = 166;
				array[k++] = 0;
				array[k++] = 217;
				array[k++] = (byte)(num2 << 4);
				array[k++] = (byte)(num2 >> 4);
				array[k++] = (byte)(32U | (15U & num2 >> 12));
				array[k++] = 217;
				num2 >>= 16;
				array[k++] = (byte)(1U | num2 << 4);
				array[k++] = (byte)(num2 >> 4);
				array[k++] = 32;
				array[k++] = 217;
				array[k++] = 0;
				array[k++] = 12;
				array[k++] = 187;
				array[k++] = 216;
				array[k++] = 216;
				array[k++] = 217;
				array[k++] = 1;
				array[k++] = 140;
				array[k++] = 187;
				array[k++] = 216;
				array[k++] = 216;
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
				PICkitFunctions.WriteUSB(array);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrPrepScript != 0)
			{
				PICkitFunctions.DownloadAddress3(8388608);
				PICkitFunctions.RunScript(5, 1);
			}
			byte[] array2 = new byte[128];
			num = 0;
			for (int m = 0; m < 32; m++)
			{
				PICkitFunctions.RunScriptUploadNoLen(3, 1);
				Array.Copy(PICkitFunctions.Usb_read_array, 1, array2, 0, 64);
				PICkitFunctions.UploadDataNoLen();
				Array.Copy(PICkitFunctions.Usb_read_array, 1, array2, 64, 64);
				int num3 = 0;
				for (int n = 0; n < 32; n++)
				{
					uint num4 = array2[num3++];
					num4 |= (uint)array2[num3++] << 8;
					num4 |= (uint)array2[num3++] << 16;
					if (num4 != dsPIC33_PE_Code[num++])
					{
						PICkitFunctions.RunScript(1, 1);
						return false;
					}
				}
			}
			PICkitFunctions.RunScript(1, 1);
			return true;
		}

		public static bool PE_Connect()
		{
			PICkitFunctions.RunScript(0, 1);
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrPrepScript != 0)
			{
				PICkitFunctions.DownloadAddress3(8390592);
				PICkitFunctions.RunScript(5, 1);
			}
			byte[] array = new byte[128];
			PICkitFunctions.RunScriptUploadNoLen(3, 1);
			Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 0, 64);
			PICkitFunctions.UploadDataNoLen();
			Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 64, 64);
			int num = array[72];
			num |= array[73] << 8;
			if (num != 203)
			{
				PICkitFunctions.RunScript(1, 1);
				return false;
			}
			num = array[75];
			num |= array[76] << 8;
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
			PICkitFunctions.WriteUSB(array2);
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
			PICkitFunctions.WriteUSB(array2);
			if (!PICkitFunctions.ReadUSB())
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
			PICkitFunctions.WriteUSB(array2);
			if (!PICkitFunctions.ReadUSB())
			{
				PICkitFunctions.RunScript(1, 1);
				return false;
			}
			if (PICkitFunctions.Usb_read_array[1] != 4)
			{
				PICkitFunctions.RunScript(1, 1);
				return false;
			}
			if (PICkitFunctions.Usb_read_array[2] != 216 || bitReverseTable[PICkitFunctions.Usb_read_array[3]] != 38 || PICkitFunctions.Usb_read_array[4] != 0 || PICkitFunctions.Usb_read_array[5] != 64)
			{
				PICkitFunctions.RunScript(1, 1);
				return false;
			}
			return true;
		}

		public static bool PE_DownloadAndConnect()
		{
            ICSPSpeedRestore = PICkitFunctions.LastICSPSpeed;
			if (PICkitFunctions.LastICSPSpeed < 2)
				PICkitFunctions.SetProgrammingSpeed(2);

			if (!PE_Connect())
			{
                UpdateStatusWinText("Downloading Programming Executive...");
				if (!DownloadPE())
				{
                    UpdateStatusWinText("Downloading Programming Executive...FAILED!");
                    RestoreICSPSpeed();
					return false;
				}
				if (!PE_Connect())
				{
                    UpdateStatusWinText("Downloading Programming Executive...FAILED!");
                    RestoreICSPSpeed();
					return false;
				}
			}
			return true;
		}

		private static void RestoreICSPSpeed()
		{
			if (ICSPSpeedRestore != PICkitFunctions.LastICSPSpeed)
				PICkitFunctions.SetProgrammingSpeed(ICSPSpeedRestore);
		}

		public static bool PEBlankCheck(uint lengthWords)
		{
			int i = 0;
			byte[] array = new byte[64];
			lengthWords += 1U;
			array[i++] = 166;
			array[i++] = 0;
			array[i++] = 243;
			array[i++] = 0;
			array[i++] = 242;
			array[i++] = 5;
			array[i++] = 242;
			array[i++] = 192;
			array[i++] = 242;
			array[i++] = 0;
			array[i++] = 242;
			array[i++] = bitReverseTable[(int)((UIntPtr)(lengthWords >> 16 & 255U))];
			array[i++] = 242;
			array[i++] = bitReverseTable[(int)((UIntPtr)(lengthWords >> 8 & 255U))];
			array[i++] = 242;
			array[i++] = bitReverseTable[(int)((UIntPtr)(lengthWords & 255U))];
			array[i++] = 243;
			array[i++] = 2;
			array[1] = (byte)(i - 2);
			while (i < 64)
			{
				array[i] = 173;
				i++;
			}
			PICkitFunctions.WriteUSB(array);
			Thread.Sleep(2000);
			i = 0;
			array[i++] = 166;
			array[i++] = 4;
			array[i++] = 240;
			array[i++] = 240;
			array[i++] = 240;
			array[i++] = 240;
			array[i++] = 170;
			while (i < 64)
			{
				array[i] = 173;
				i++;
			}
			PICkitFunctions.WriteUSB(array);
			return PICkitFunctions.ReadUSB() && PICkitFunctions.Usb_read_array[1] == 4 && PICkitFunctions.Usb_read_array[2] == bitReverseTable[26] && PICkitFunctions.Usb_read_array[3] == bitReverseTable[240] && PICkitFunctions.Usb_read_array[4] == 0 && PICkitFunctions.Usb_read_array[5] == bitReverseTable[2];
		}

		public static bool PE33BlankCheck(string saveText)
		{
			if (!PE_DownloadAndConnect())
				return false;

			UpdateStatusWinText(saveText);
			if (!PEBlankCheck(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem))
			{
				PICkitFunctions.RunScript(1, 1);
				RestoreICSPSpeed();
				return false;
			}
			PICkitFunctions.RunScript(1, 1);
			RestoreICSPSpeed();
			return true;
		}

		public static bool PE33Read(string saveText)
		{
			if (!PE_DownloadAndConnect())
				return false;

			UpdateStatusWinText(saveText);
			byte[] array = new byte[128];
			byte bytesPerLocation = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
			int num = 32;
			int num2 = 0;
			byte[] array2 = new byte[64];
			ResetStatusBar((int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem / num));
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
				array2[i++] = bitReverseTable[num];
				array2[i++] = 242;
				array2[i++] = 0;
				array2[i++] = 242;
				array2[i++] = bitReverseTable[num2 >> 15 & 255];
				array2[i++] = 242;
				array2[i++] = bitReverseTable[num2 >> 7 & 255];
				array2[i++] = 242;
				array2[i++] = bitReverseTable[num2 << 1 & 255];
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
				PICkitFunctions.WriteUSB(array2);
				PICkitFunctions.GetUpload();
				Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 0, 64);
				PICkitFunctions.GetUpload();
				Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 64, 64);
				int num3 = 0;
				for (int j = 0; j < num; j += 2)
				{
					uint num4 = (uint)bitReverseTable[array[num3++]] << 8;
					num4 |= bitReverseTable[array[num3++]];
					uint num5 = (uint)bitReverseTable[array[num3++]] << 16;
					num4 |= (uint)bitReverseTable[array[num3++]] << 16;
					num5 |= (uint)bitReverseTable[array[num3++]] << 8;
					num5 |= bitReverseTable[array[num3++]];
					PICkitFunctions.DeviceBuffers.ProgramMemory[num2++] = num4;
					PICkitFunctions.DeviceBuffers.ProgramMemory[num2++] = num5;
					if (num2 >= PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem)
						break;

				}
				StepStatusBar();
			}
			while (num2 < PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem);
			PICkitFunctions.RunScript(1, 1);
			RestoreICSPSpeed();
			return true;
		}

		public static bool PE33Write(int endOfBuffer, string saveText)
		{
			if (!PE_DownloadAndConnect())
				return false;

			UpdateStatusWinText(saveText);
			byte[] array = new byte[256];
			int num = 64;
			int num2 = 0;
			ResetStatusBar(endOfBuffer / num);
			for (;;)
			{
				int num3 = 0;
				for (int i = 0; i < num; i += 2)
				{
					uint num4 = PICkitFunctions.DeviceBuffers.ProgramMemory[num2++];
					array[num3 + 1] = bitReverseTable[(int)(num4 & 255U)];
					num4 >>= 8;
					array[num3] = bitReverseTable[(int)(num4 & 255U)];
					num4 >>= 8;
					array[num3 + 3] = bitReverseTable[(int)(num4 & 255U)];
					num4 = PICkitFunctions.DeviceBuffers.ProgramMemory[num2++];
					array[num3 + 5] = bitReverseTable[(int)(num4 & 255U)];
					num4 >>= 8;
					array[num3 + 4] = bitReverseTable[(int)(num4 & 255U)];
					num4 >>= 8;
					array[num3 + 2] = bitReverseTable[(int)(num4 & 255U)];
					num3 += 6;
				}
				for (int j = PICkitFunctions.DataClrAndDownload(array, 0); j < num3; j = PICkitFunctions.DataDownload(array, j, num3))
				{
				}
				int k = 0;
				byte[] array2 = new byte[64];
				array2[k++] = 166;
				array2[k++] = 0;
				array2[k++] = 243;
				array2[k++] = 0;
				array2[k++] = 242;
				array2[k++] = 10;
				array2[k++] = 242;
				array2[k++] = 198;
				array2[k++] = 242;
				array2[k++] = 0;
				array2[k++] = 242;
				array2[k++] = bitReverseTable[num2 - 64 >> 15 & 255];
				array2[k++] = 242;
				array2[k++] = bitReverseTable[num2 - 64 >> 7 & 255];
				array2[k++] = 242;
				array2[k++] = bitReverseTable[num2 - 64 << 1 & 255];
				array2[k++] = 241;
				array2[k++] = 241;
				array2[k++] = 241;
				array2[k++] = 233;
				array2[k++] = 3;
				array2[k++] = 63;
				array2[k++] = 243;
				array2[k++] = 2;
				array2[k++] = 231;
				array2[k++] = 118;
				array2[k++] = 240;
				array2[k++] = 240;
				array2[k++] = 240;
				array2[k++] = 240;
				array2[k++] = 170;
				array2[1] = (byte)(k - 3);
				while (k < 64)
				{
					array2[k] = 173;
					k++;
				}
				PICkitFunctions.WriteUSB(array2);
				if (!PICkitFunctions.ReadUSB())
					break;

				if (PICkitFunctions.Usb_read_array[1] != 4)
					goto Block_6;

				if (bitReverseTable[PICkitFunctions.Usb_read_array[2]] != 21 || PICkitFunctions.Usb_read_array[3] != 0 || PICkitFunctions.Usb_read_array[4] != 0 || bitReverseTable[PICkitFunctions.Usb_read_array[5]] != 2)
					goto IL_3AB;

				StepStatusBar();
				if (num2 >= endOfBuffer)
					goto Block_10;
			}
			UpdateStatusWinText("Programming Executive Error during Write.");
			PICkitFunctions.RunScript(1, 1);
			RestoreICSPSpeed();
			return false;
			Block_6:
			UpdateStatusWinText("Programming Executive Error during Write.");
			PICkitFunctions.RunScript(1, 1);
			RestoreICSPSpeed();
			return false;
			IL_3AB:
			UpdateStatusWinText("Programming Executive Error during Write.");
			PICkitFunctions.RunScript(1, 1);
			RestoreICSPSpeed();
			return false;
			Block_10:
			PICkitFunctions.RunScript(1, 1);
			RestoreICSPSpeed();
			return true;
		}

		public static bool PE33VerifyCRC(string saveText)
		{
			if (!PE_DownloadAndConnect())
				return false;

			UpdateStatusWinText(saveText);
			int i = 0;
			byte[] array = new byte[64];
			ushort num = CalcCRCProgMem();
			uint programMem = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem;
			array[i++] = 166;
			array[i++] = 0;
			array[i++] = 243;
			array[i++] = 0;
			array[i++] = 242;
			array[i++] = 3;
			array[i++] = 242;
			array[i++] = 160;
			array[i++] = 242;
			array[i++] = 128;
			array[i++] = 242;
			array[i++] = 0;
			array[i++] = 242;
			array[i++] = 0;
			array[i++] = 242;
			array[i++] = 0;
			array[i++] = 242;
			array[i++] = 0;
			array[i++] = 242;
			array[i++] = bitReverseTable[(int)(programMem >> 16 & 255U)];
			array[i++] = 242;
			array[i++] = bitReverseTable[(int)(programMem >> 8 & 255U)];
			array[i++] = 242;
			array[i++] = bitReverseTable[(int)(programMem & 255U)];
			array[i++] = 243;
			array[i++] = 2;
			array[1] = (byte)(i - 2);
			while (i < 64)
			{
				array[i] = 173;
				i++;
			}
			PICkitFunctions.WriteUSB(array);
			float num2 = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem * 0.034066133f;
			Thread.Sleep((int)num2);
			i = 0;
			array[i++] = 166;
			array[i++] = 6;
			array[i++] = 240;
			array[i++] = 240;
			array[i++] = 240;
			array[i++] = 240;
			array[i++] = 240;
			array[i++] = 240;
			array[i++] = 170;
			while (i < 64)
			{
				array[i] = 173;
				i++;
			}
			PICkitFunctions.WriteUSB(array);
			if (!PICkitFunctions.ReadUSB())
			{
				PICkitFunctions.RunScript(1, 1);
				RestoreICSPSpeed();
				return false;
			}
			if (PICkitFunctions.Usb_read_array[1] != 6)
			{
				PICkitFunctions.RunScript(1, 1);
				RestoreICSPSpeed();
				return false;
			}
			PICkitFunctions.RunScript(1, 1);
			RestoreICSPSpeed();
			ushort num3 = bitReverseTable[PICkitFunctions.Usb_read_array[7]];
			num3 += (ushort)(bitReverseTable[PICkitFunctions.Usb_read_array[6]] << 8);
			return num3 == num;
		}

		private static ushort CalcCRCProgMem()
		{
			uint num = 65535;
			int num2 = 0;
			while (num2 < PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem)
			{
				uint num3 = PICkitFunctions.DeviceBuffers.ProgramMemory[num2];
				CRC_Calculate((byte)(num3 & 255U), ref num);
				CRC_Calculate((byte)(num3 >> 8 & 255U), ref num);
				CRC_Calculate((byte)(num3 >> 16 & 255U), ref num);
				num3 = PICkitFunctions.DeviceBuffers.ProgramMemory[num2 + 1];
				CRC_Calculate((byte)(num3 >> 16 & 255U), ref num);
				CRC_Calculate((byte)(num3 & 255U), ref num);
				CRC_Calculate((byte)(num3 >> 8 & 255U), ref num);
				num2 += 2;
			}
			return (ushort)(num & 65535U);
		}

		private static void CRC_Calculate(byte ByteValue, ref uint CRC_Value)
		{
			byte b = (byte)(CRC_Value >> 8 ^ ByteValue);
			CRC_Value = crc_LUT_Array[b] ^ CRC_Value << 8;
		}

        public static DelegateStatusWin UpdateStatusWinText;
		public static DelegateResetStatusBar ResetStatusBar;
		public static DelegateStepStatusBar StepStatusBar;
		private static byte ICSPSpeedRestore = 0;
		private static readonly uint[] dsPIC33_PE_Code = new uint[]
		{
			262272U,
			128U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			8388896U,
			16384000U,
			2141647U,
			2260864U,
			8913152U,
			0U,
			458754U,
			16416768U,
			393216U,
			16384000U,
			15400960U,
			8927776U,
			459360U,
			459302U,
			459182U,
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
			16384004U,
			8409184U,
			14549068U,
			16482304U,
			12124513U,
			12488450U,
			2097376U,
			2097153U,
			12452126U,
			5312384U,
			5869441U,
			4063279U,
			12451870U,
			90112U,
			3604494U,
			3604493U,
			3604492U,
			3604521U,
			3604496U,
			3604499U,
			3604518U,
			3604499U,
			3604516U,
			3604499U,
			3604500U,
			3604503U,
			3604504U,
			3604489U,
			3604498U,
			11045587U,
			11119315U,
			11127507U,
			11135699U,
			458784U,
			3604509U,
			458926U,
			3604507U,
			458958U,
			3604505U,
			459015U,
			3604503U,
			458850U,
			3604501U,
			458871U,
			3604499U,
			459076U,
			3604497U,
			459077U,
			3604495U,
			459111U,
			3604493U,
			459251U,
			8935104U,
			11045587U,
			11119315U,
			11127507U,
			11135699U,
			458760U,
			3604485U,
			11045587U,
			11053779U,
			11127507U,
			11135699U,
			458754U,
			16416768U,
			393216U,
			16384000U,
			8410769U,
			3080192U,
			6324352U,
			2162688U,
			5279616U,
			3276806U,
			8410769U,
			3080192U,
			6324352U,
			2293760U,
			5279616U,
			3801091U,
			15417344U,
			12053202U,
			3604482U,
			11780112U,
			12053202U,
			8409184U,
			14549068U,
			16482304U,
			6291567U,
			14483784U,
			8410769U,
			3084272U,
			6324224U,
			7340034U,
			8935056U,
			8410769U,
			2158592U,
			6324352U,
			2101248U,
			5279616U,
			3801093U,
			12569103U,
			16482304U,
			15237120U,
			8935072U,
			3604512U,
			8410769U,
			2158592U,
			6324352U,
			2105344U,
			5279616U,
			3801101U,
			8409200U,
			13697024U,
			12124259U,
			15237120U,
			8935072U,
			8409200U,
			6291553U,
			14681088U,
			3276817U,
			8410784U,
			15237120U,
			8935072U,
			3604493U,
			8410769U,
			2158592U,
			6324352U,
			2146304U,
			5279616U,
			3801093U,
			2097200U,
			8935072U,
			8410816U,
			8935088U,
			3604482U,
			2097184U,
			8935072U,
			459050U,
			16416768U,
			393216U,
			16384000U,
			2360560U,
			8928000U,
			11061089U,
			2098519U,
			8928055U,
			2099879U,
			8928055U,
			11069281U,
			0U,
			0U,
			12568417U,
			11782145U,
			6307841U,
			14681088U,
			3866619U,
			11045587U,
			11119315U,
			11127507U,
			11135699U,
			524193U,
			16416768U,
			393216U,
			16384006U,
			2360352U,
			8928000U,
			12569102U,
			16482304U,
			2097153U,
			14483904U,
			2097154U,
			8409216U,
			2097153U,
			4267776U,
			4822785U,
			15400960U,
			9963296U,
			3604500U,
			11061089U,
			2098519U,
			8928055U,
			2099879U,
			8928055U,
			11069281U,
			0U,
			0U,
			12568417U,
			11782145U,
			6307841U,
			14681088U,
			3866619U,
			2098176U,
			2097153U,
			4198174U,
			4757342U,
			9437230U,
			15204352U,
			9963296U,
			12569103U,
			16482432U,
			9437230U,
			5279616U,
			4128743U,
			11045587U,
			11119315U,
			11127507U,
			11135699U,
			524146U,
			16416768U,
			393216U,
			16384004U,
			12569102U,
			16482304U,
			8913296U,
			2359296U,
			8928000U,
			8409232U,
			9963280U,
			8409216U,
			7868160U,
			9437342U,
			7864350U,
			12257281U,
			11061089U,
			2098512U,
			8928048U,
			2099872U,
			8928048U,
			11069281U,
			0U,
			0U,
			9963280U,
			12568417U,
			11782145U,
			6307841U,
			14681088U,
			3866619U,
			11045587U,
			11119315U,
			11127507U,
			11135699U,
			524112U,
			16416768U,
			393216U,
			16384004U,
			12569102U,
			16482304U,
			8913296U,
			2359344U,
			8928000U,
			8409232U,
			9963280U,
			8409216U,
			7868160U,
			9437342U,
			7864350U,
			12257281U,
			12569103U,
			16482304U,
			9963280U,
			9437342U,
			7864350U,
			12306433U,
			11061089U,
			2098512U,
			8928048U,
			2099872U,
			8928048U,
			11069281U,
			0U,
			0U,
			9963280U,
			12568417U,
			11782145U,
			6307841U,
			14681088U,
			3866619U,
			7864350U,
			12189712U,
			9963280U,
			11111123U,
			11053779U,
			11127507U,
			11135699U,
			8409233U,
			9437214U,
			5279616U,
			3801100U,
			7864350U,
			12238864U,
			9963280U,
			9437214U,
			7880832U,
			12569103U,
			5296000U,
			3801092U,
			11045587U,
			11119315U,
			11127507U,
			11135699U,
			524053U,
			16416768U,
			393216U,
			2359312U,
			8928000U,
			2138309U,
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
			2141473U,
			7866496U,
			524010U,
			393216U,
			2138304U,
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
			3604485U,
			2138304U,
			9437616U,
			8913299U,
			9437632U,
			5342178U,
			15434240U,
			3145702U,
			2138304U,
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
			2141475U,
			7870848U,
			2097184U,
			7866752U,
			458780U,
			393216U,
			2097761U,
			2207744U,
			7340033U,
			2141473U,
			7870592U,
			2097184U,
			7866496U,
			458771U,
			393216U,
			458855U,
			2138311U,
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
			2141479U,
			7864375U,
			8917568U,
			7864375U,
			458831U,
			2141472U,
			9437840U,
			15303301U,
			3276856U,
			9453584U,
			11682032U,
			11780113U,
			14746625U,
			3801098U,
			2138304U,
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
			2138304U,
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
			16384012U,
			12058720U,
			9963296U,
			9963313U,
			12058720U,
			9963328U,
			9963345U,
			12569103U,
			7884544U,
			4653156U,
			15237248U,
			12569102U,
			7882880U,
			8409216U,
			12058977U,
			9437230U,
			9437374U,
			7405568U,
			7438465U,
			9963296U,
			9963313U,
			4653160U,
			15237248U,
			8409232U,
			7866496U,
			8409248U,
			12058977U,
			9437262U,
			9437406U,
			7405568U,
			7438465U,
			9963328U,
			9963345U,
			9437518U,
			9437662U,
			9437230U,
			9437374U,
			7881246U,
			458797U,
			9963280U,
			9437214U,
			16416768U,
			393216U,
			16384006U,
			15400960U,
			9963280U,
			3604511U,
			9437214U,
			14483528U,
			9963296U,
			11780224U,
			7884544U,
			3604494U,
			9437230U,
			14680064U,
			3997703U,
			9437230U,
			4194304U,
			7864448U,
			2163216U,
			6848512U,
			9963296U,
			3604483U,
			9437230U,
			4194304U,
			9963296U,
			15290142U,
			14681118U,
			3866608U,
			9437214U,
			4194432U,
			2130112U,
			4227072U,
			9437358U,
			7866369U,
			9437214U,
			15204352U,
			9963280U,
			9437342U,
			2101232U,
			5279616U,
			3473373U,
			16416768U,
			393216U,
			16384026U,
			9963360U,
			9963377U,
			9965314U,
			9965331U,
			9983812U,
			15433728U,
			9963328U,
			9437550U,
			9437694U,
			2097152U,
			2097169U,
			4194306U,
			4751491U,
			2097154U,
			3145715U,
			7864832U,
			7864961U,
			7864322U,
			7864451U,
			6422784U,
			6455297U,
			12059233U,
			9965364U,
			9965381U,
			9439294U,
			9439438U,
			14483648U,
			2097152U,
			9965360U,
			9965377U,
			9439294U,
			9439438U,
			12063329U,
			9965364U,
			9965381U,
			9439550U,
			9439694U,
			7340034U,
			7372931U,
			9963280U,
			9963297U,
			3604540U,
			9439246U,
			9439390U,
			4194560U,
			4751745U,
			9437294U,
			9437438U,
			4194562U,
			4751747U,
			9437214U,
			9437358U,
			5312384U,
			5869441U,
			3735578U,
			9437470U,
			9437614U,
			9437294U,
			9437438U,
			5308416U,
			5865601U,
			13697153U,
			13860864U,
			9963312U,
			9437470U,
			9437614U,
			2097152U,
			2097169U,
			4194306U,
			4751491U,
			9963280U,
			9963297U,
			9437246U,
			12058977U,
			9439246U,
			9439390U,
			5242882U,
			5800067U,
			9965312U,
			9965329U,
			3604485U,
			9439630U,
			9963315U,
			12058720U,
			9965312U,
			9965329U,
			9437646U,
			9437502U,
			9437294U,
			9437438U,
			458802U,
			9963328U,
			9437246U,
			2097153U,
			4194560U,
			4751745U,
			9437294U,
			9437438U,
			4259840U,
			4817025U,
			9963360U,
			9963377U,
			9439246U,
			9439390U,
			5246944U,
			5804000U,
			3866559U,
			9437262U,
			2097153U,
			16416768U,
			393216U,
			2097958U,
			7867137U,
			7865216U,
			2130086U,
			7867138U,
			13969418U,
			2130120U,
			2129926U,
			12196631U,
			12245815U,
			12245847U,
			12196663U,
			2129924U,
			2097253U,
			16613379U,
			7880707U,
			7880884U,
			6864896U,
			15687681U,
			13762560U,
			4194312U,
			7864336U,
			15417728U,
			6914432U,
			15540234U,
			3866612U,
			15542282U,
			3866603U,
			7864323U,
			393216U,
			2097958U,
			7867137U,
			7865216U,
			2130086U,
			7867138U,
			13969418U,
			2130120U,
			2097250U,
			2129926U,
			12196631U,
			12245815U,
			12245847U,
			12196663U,
			2129924U,
			2097205U,
			15401088U,
			16613379U,
			7880707U,
			6834356U,
			15687681U,
			13762560U,
			4194312U,
			15417728U,
			6916368U,
			16613379U,
			7880707U,
			6834356U,
			15687681U,
			13762560U,
			4194312U,
			15417728U,
			6916368U,
			15540234U,
			3866606U,
			15542282U,
			3866596U,
			7864323U,
			393216U,
			2778U,
			2U,
			0U,
			2572U,
			206U,
			0U,
			2060U,
			512U,
			0U,
			2048U,
			12U,
			0U,
			0U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			203U,
			38U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U,
			16777215U
		};

		private static readonly byte[] bitReverseTable = new byte[]
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

		private static readonly ushort[] crc_LUT_Array = new ushort[]
		{
			0,
			4129,
			8258,
			12387,
			16516,
			20645,
			24774,
			28903,
			33032,
			37161,
			41290,
			45419,
			49548,
			53677,
			57806,
			61935,
			4657,
			528,
			12915,
			8786,
			21173,
			17044,
			29431,
			25302,
			37689,
			33560,
			45947,
			41818,
			54205,
			50076,
			62463,
			58334,
			9314,
			13379,
			1056,
			5121,
			25830,
			29895,
			17572,
			21637,
			42346,
			46411,
			34088,
			38153,
			58862,
			62927,
			50604,
			54669,
			13907,
			9842,
			5649,
			1584,
			30423,
			26358,
			22165,
			18100,
			46939,
			42874,
			38681,
			34616,
			63455,
			59390,
			55197,
			51132,
			18628,
			22757,
			26758,
			30887,
			2112,
			6241,
			10242,
			14371,
			51660,
			55789,
			59790,
			63919,
			35144,
			39273,
			43274,
			47403,
			23285,
			19156,
			31415,
			27286,
			6769,
			2640,
			14899,
			10770,
			56317,
			52188,
			64447,
			60318,
			39801,
			35672,
			47931,
			43802,
			27814,
			31879,
			19684,
			23749,
			11298,
			15363,
			3168,
			7233,
			60846,
			64911,
			52716,
			56781,
			44330,
			48395,
			36200,
			40265,
			32407,
			28342,
			24277,
			20212,
			15891,
			11826,
			7761,
			3696,
			65439,
			61374,
			57309,
			53244,
			48923,
			44858,
			40793,
			36728,
			37256,
			33193,
			45514,
			41451,
			53516,
			49453,
			61774,
			57711,
			4224,
			161,
			12482,
			8419,
			20484,
			16421,
			28742,
			24679,
			33721,
			37784,
			41979,
			46042,
			49981,
			54044,
			58239,
			62302,
			689,
			4752,
			8947,
			13010,
			16949,
			21012,
			25207,
			29270,
			46570,
			42443,
			38312,
			34185,
			62830,
			58703,
			54572,
			50445,
			13538,
			9411,
			5280,
			1153,
			29798,
			25671,
			21540,
			17413,
			42971,
			47098,
			34713,
			38840,
			59231,
			63358,
			50973,
			55100,
			9939,
			14066,
			1681,
			5808,
			26199,
			30326,
			17941,
			22068,
			55628,
			51565,
			63758,
			59695,
			39368,
			35305,
			47498,
			43435,
			22596,
			18533,
			30726,
			26663,
			6336,
			2273,
			14466,
			10403,
			52093,
			56156,
			60223,
			64286,
			35833,
			39896,
			43963,
			48026,
			19061,
			23124,
			27191,
			31254,
			2801,
			6864,
			10931,
			14994,
			64814,
			60687,
			56684,
			52557,
			48554,
			44427,
			40424,
			36297,
			31782,
			27655,
			23652,
			19525,
			15522,
			11395,
			7392,
			3265,
			61215,
			65342,
			53085,
			57212,
			44955,
			49082,
			36825,
			40952,
			28183,
			32310,
			20053,
			24180,
			11923,
			16050,
			3793,
			7920
		};
	}
}
