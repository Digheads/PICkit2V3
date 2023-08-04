using System;
using System.Threading;

namespace PICkit2V3
{
	public class PIC32MXFunctions
	{
		public static void EnterSerialExecution()
		{
			int num = 0;
			byte[] array = new byte[29];
			array[num++] = 166;
			array[num++] = 27;
			array[num++] = 187;
			array[num++] = 4;
			array[num++] = 187;
			array[num++] = 7;
			array[num++] = 186;
			array[num++] = 0;
			array[num++] = 187;
			array[num++] = 4;
			array[num++] = 187;
			array[num++] = 7;
			array[num++] = 186;
			array[num++] = 209;
			array[num++] = 187;
			array[num++] = 5;
			array[num++] = 188;
			array[num++] = 6;
			array[num++] = 31;
			array[num++] = 187;
			array[num++] = 12;
			array[num++] = 187;
			array[num++] = 4;
			array[num++] = 187;
			array[num++] = 7;
			array[num++] = 186;
			array[num++] = 208;
			array[num++] = 186;
			array[num++] = 254;
			PICkitFunctions.WriteUSB(array);
		}

		public static bool DownloadPE()
		{
			int i = 0;
			byte[] array = new byte[64];
			array[i++] = 167;
			array[i++] = 168;
			array[i++] = 28;
			i = AddInstruction(array, 1006944136, i);
			i = AddInstruction(array, 881074176, i);
			i = AddInstruction(array, 1006960671, i);
			i = AddInstruction(array, 883228736, i);
			i = AddInstruction(array, 2894397440, i);
			i = AddInstruction(array, 872744960, i);
			i = AddInstruction(array, 2894397456, i);
			array[i++] = 166;
			array[i++] = 12;
			array[i++] = 187;
			array[i++] = 5;
			array[i++] = 188;
			array[i++] = 6;
			array[i++] = 31;
			array[i++] = 182;
			array[i++] = 182;
			array[i++] = 182;
			array[i++] = 182;
			array[i++] = 182;
			array[i++] = 182;
			array[i++] = 182;
			while (i < 64)
			{
				array[i] = 173;
				i++;
			}
			PICkitFunctions.WriteUSB(array);
			if (PICkitFunctions.BusErrorCheck())
			{
				return false;
			}
			i = 0;
			array[i++] = 167;
			array[i++] = 168;
			array[i++] = 20;
			i = AddInstruction(array, 872775680, i);
			i = AddInstruction(array, 2894397472, i);
			i = AddInstruction(array, 2894397488, i);
			i = AddInstruction(array, 1006936064, i);
			i = AddInstruction(array, 881068032, i);
			array[i++] = 166;
			array[i++] = 5;
			array[i++] = 182;
			array[i++] = 182;
			array[i++] = 182;
			array[i++] = 182;
			array[i++] = 182;
			while (i < 64)
			{
				array[i] = 173;
				i++;
			}
			PICkitFunctions.WriteUSB(array);
			if (PICkitFunctions.BusErrorCheck())
				return false;

			for (int j = 0; j < pe_Loader.Length; j += 2)
			{
				i = 0;
				array[i++] = 167;
				array[i++] = 168;
				array[i++] = 16;
				i = AddInstruction(array, 1007026176 | pe_Loader[j], i);
				i = AddInstruction(array, 885391360 | pe_Loader[j + 1], i);
				i = AddInstruction(array, 2894462976, i);
				i = AddInstruction(array, 612630532, i);
				array[i++] = 166;
				array[i++] = 4;
				array[i++] = 182;
				array[i++] = 182;
				array[i++] = 182;
				array[i++] = 182;
				while (i < 64)
				{
					array[i] = 173;
					i++;
				}
				PICkitFunctions.WriteUSB(array);
				if (PICkitFunctions.BusErrorCheck())
					return false;
			}
			i = 0;
			array[i++] = 167;
			array[i++] = 168;
			array[i++] = 16;
			i = AddInstruction(array, 1008312320, i);
			i = AddInstruction(array, 926484480, i);
			i = AddInstruction(array, 52428808, i);
			i = AddInstruction(array, 0, i);
			array[i++] = 166;
			array[i++] = 21;
			array[i++] = 182;
			array[i++] = 182;
			array[i++] = 182;
			array[i++] = 182;
			array[i++] = 187;
			array[i++] = 5;
			array[i++] = 188;
			array[i++] = 6;
			array[i++] = 31;
			array[i++] = 187;
			array[i++] = 14;
			array[i++] = 184;
			array[i++] = 0;
			array[i++] = 9;
			array[i++] = 0;
			array[i++] = 160;
			array[i++] = 184;
			array[i++] = (byte)(PIC32_PE.Length & 255);
			array[i++] = (byte)(PIC32_PE.Length >> 8 & 255);
			array[i++] = 0;
			array[i++] = 0;
			while (i < 64)
			{
				array[i] = 173;
				i++;
			}
			PICkitFunctions.WriteUSB(array);
			if (PICkitFunctions.BusErrorCheck())
				return false;

			int num = PIC32_PE.Length / 10;
			for (int k = 0; k < num; k++)
			{
				i = 0;
				array[i++] = 167;
				array[i++] = 168;
				array[i++] = 40;
				int num2 = k * 10;
				i = AddInstruction(array, PIC32_PE[num2], i);
				i = AddInstruction(array, PIC32_PE[num2 + 1], i);
				i = AddInstruction(array, PIC32_PE[num2 + 2], i);
				i = AddInstruction(array, PIC32_PE[num2 + 3], i);
				i = AddInstruction(array, PIC32_PE[num2 + 4], i);
				i = AddInstruction(array, PIC32_PE[num2 + 5], i);
				i = AddInstruction(array, PIC32_PE[num2 + 6], i);
				i = AddInstruction(array, PIC32_PE[num2 + 7], i);
				i = AddInstruction(array, PIC32_PE[num2 + 8], i);
				i = AddInstruction(array, PIC32_PE[num2 + 9], i);
				array[i++] = 166;
				array[i++] = 10;
				array[i++] = 183;
				array[i++] = 183;
				array[i++] = 183;
				array[i++] = 183;
				array[i++] = 183;
				array[i++] = 183;
				array[i++] = 183;
				array[i++] = 183;
				array[i++] = 183;
				array[i++] = 183;
				while (i < 64)
				{
					array[i] = 173;
					i++;
				}
				PICkitFunctions.WriteUSB(array);
				if (PICkitFunctions.BusErrorCheck())
					return false;
			}
			Thread.Sleep(100);
			int num3 = num * 10;
			num = PIC32_PE.Length % 10;
			if (num > 0)
			{
				i = 0;
				array[i++] = 167;
				array[i++] = 168;
				array[i++] = (byte)(4 * num);
				for (int l = 0; l < num; l++)
					i = AddInstruction(array, PIC32_PE[l + num3], i);

				array[i++] = 166;
				array[i++] = (byte)num;
				for (int m = 0; m < num; m++)
					array[i++] = 183;

				while (i < 64)
				{
					array[i] = 173;
					i++;
				}
				PICkitFunctions.WriteUSB(array);
				if (PICkitFunctions.BusErrorCheck())
					return false;
			}
			i = 0;
			array[i++] = 167;
			array[i++] = 168;
			array[i++] = 8;
			i = AddInstruction(array, 0, i);
			i = AddInstruction(array, 3735879680, i);
			array[i++] = 166;
			array[i++] = 2;
			array[i++] = 183;
			array[i++] = 183;
			while (i < 64)
			{
				array[i] = 173;
				i++;
			}
			PICkitFunctions.WriteUSB(array);
			if (PICkitFunctions.BusErrorCheck())
				return false;

			Thread.Sleep(100);
			return true;
		}

		public static int ReadPEVersion()
		{
			byte[] array = new byte[11];
			int num = 0;
			array[num++] = 169;
			array[num++] = 166;
			array[num++] = 8;
			array[num++] = 187;
			array[num++] = 14;
			array[num++] = 184;
			array[num++] = 0;
			array[num++] = 0;
			array[num++] = 7;
			array[num++] = 0;
			array[num++] = 181;
			PICkitFunctions.WriteUSB(array);
			if (PICkitFunctions.BusErrorCheck())
				return 0;

			if (!PICkitFunctions.UploadData())
				return 0;

			int num2 = PICkitFunctions.Usb_read_array[4] + PICkitFunctions.Usb_read_array[5] * 256;
			if (num2 != 7)
				return 0;

			return PICkitFunctions.Usb_read_array[2] + PICkitFunctions.Usb_read_array[3] * 256;
		}

		public static bool PEBlankCheck(uint startAddress, uint lengthBytes)
		{
			byte[] array = new byte[21];
			int num = 0;
			array[num++] = 169;
			array[num++] = 166;
			array[num++] = 18;
			array[num++] = 187;
			array[num++] = 14;
			array[num++] = 184;
			array[num++] = 0;
			array[num++] = 0;
			array[num++] = 6;
			array[num++] = 0;
			array[num++] = 184;
			array[num++] = (byte)(startAddress & 255U);
			array[num++] = (byte)(startAddress >> 8 & 255U);
			array[num++] = (byte)(startAddress >> 16 & 255U);
			array[num++] = (byte)(startAddress >> 24 & 255U);
			array[num++] = 184;
			array[num++] = (byte)(lengthBytes & 255U);
			array[num++] = (byte)(lengthBytes >> 8 & 255U);
			array[num++] = (byte)(lengthBytes >> 16 & 255U);
			array[num++] = (byte)(lengthBytes >> 24 & 255U);
			array[num++] = 181;
			PICkitFunctions.WriteUSB(array);
			return !PICkitFunctions.BusErrorCheck() && PICkitFunctions.UploadData() && PICkitFunctions.Usb_read_array[4] == 6 && PICkitFunctions.Usb_read_array[2] == 0;
		}

		public static int PEGetCRC(uint startAddress, uint lengthBytes)
		{
			byte[] array = new byte[20];
			int num = 0;
			array[num++] = 169;
			array[num++] = 166;
			array[num++] = 17;
			array[num++] = 187;
			array[num++] = 14;
			array[num++] = 184;
			array[num++] = 0;
			array[num++] = 0;
			array[num++] = 8;
			array[num++] = 0;
			array[num++] = 184;
			array[num++] = (byte)(startAddress & 255U);
			array[num++] = (byte)(startAddress >> 8 & 255U);
			array[num++] = (byte)(startAddress >> 16 & 255U);
			array[num++] = (byte)(startAddress >> 24 & 255U);
			array[num++] = 184;
			array[num++] = (byte)(lengthBytes & 255U);
			array[num++] = (byte)(lengthBytes >> 8 & 255U);
			array[num++] = (byte)(lengthBytes >> 16 & 255U);
			array[num++] = (byte)(lengthBytes >> 24 & 255U);
			PICkitFunctions.WriteUSB(array);
			byte[] array2 = new byte[5];
			num = 0;
			array2[num++] = 169;
			array2[num++] = 166;
			array2[num++] = 2;
			array2[num++] = 181;
			array2[num++] = 181;
			PICkitFunctions.WriteUSB(array2);
			if (PICkitFunctions.BusErrorCheck())
				return 0;

			if (!PICkitFunctions.UploadData())
				return 0;

			if (PICkitFunctions.Usb_read_array[4] != 8 || PICkitFunctions.Usb_read_array[2] != 0)
				return 0;

			return PICkitFunctions.Usb_read_array[6] + (PICkitFunctions.Usb_read_array[7] << 8);
		}

		private static int AddInstruction(byte[] commandarray, uint instruction, int offset)
		{
			commandarray[offset++] = (byte)(instruction & 255U);
			commandarray[offset++] = (byte)(instruction >> 8 & 255U);
			commandarray[offset++] = (byte)(instruction >> 16 & 255U);
			commandarray[offset++] = (byte)(instruction >> 24 & 255U);
			return offset;
		}

		public static bool PE_DownloadAndConnect()
		{
			UpdateStatusWinText("Downloading Programming Executive...");
			PICkitFunctions.RunScript(0, 1);
			PICkitFunctions.UploadData();
			if ((PICkitFunctions.Usb_read_array[2] & 128) == 0)
			{
				UpdateStatusWinText("Device is Code Protected and must be Erased first.");
				PICkitFunctions.RunScript(1, 1);
				return false;
			}
			EnterSerialExecution();
			DownloadPE();
			int num = ReadPEVersion();
			if (num != 265)
			{
				UpdateStatusWinText("Downloading Programming Executive...FAILED!");
				PICkitFunctions.RunScript(1, 1);
				return false;
			}
			return true;
		}

		public static bool PIC32Read()
		{
			PICkitFunctions.SetMCLRTemp(true);
			PICkitFunctions.VddOn();
			if (!PE_DownloadAndConnect())
				return false;

			string text = "Reading device:\n";
			UpdateStatusWinText(text);
			byte[] array = new byte[128];
			int num = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem;
			int bootFlash = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BootFlash;
			num -= bootFlash;
			text += "Program Flash... ";
			UpdateStatusWinText(text);
			int bytesPerLocation = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
			int num2 = 128 / (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemRdWords * bytesPerLocation);
			int num3 = num2 * PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemRdWords;
			int num4 = 0;
			ResetStatusBar(num / num3);
			int num7;
			do
			{
				int num5 = (num - num4) / num3;
				if (num5 > 15)
					num5 = 15;

				uint num6 = (uint)(num4 * bytesPerLocation + 486539264);
				byte[] array2 = new byte[3 + num5 * 4];
				int offset = 0;
				array2[offset++] = 167;
				array2[offset++] = 168;
				array2[offset++] = (byte)(num5 * 4);
				for (int i = 0; i < num5; i++)
					offset = AddInstruction(array2, num6 + (uint)(i * PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemRdWords * bytesPerLocation), offset);

				PICkitFunctions.WriteUSB(array2);
				for (int j = 0; j < num5; j++)
				{
					PICkitFunctions.RunScriptUploadNoLen(3, num2);
					Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 0, 64);
					PICkitFunctions.UploadDataNoLen();
					Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 64, 64);
					num7 = 0;
					for (int k = 0; k < num3; k++)
					{
						int num8 = 0;
						uint num9 = array[num7 + num8++];
						if (num8 < bytesPerLocation)
							num9 |= (uint)array[num7 + num8++] << 8;
						if (num8 < bytesPerLocation)
							num9 |= (uint)array[num7 + num8++] << 16;
						if (num8 < bytesPerLocation)
							num9 |= (uint)array[num7 + num8++] << 24;
						num7 += num8;
						PICkitFunctions.DeviceBuffers.ProgramMemory[num4++] = num9;
						if (num4 == num)
						{
							j = num5;
							break;
						}
					}
					StepStatusBar();
				}
			}
			while (num4 < num);
			text += "Boot... ";
			UpdateStatusWinText(text);
			num4 = 0;
			ResetStatusBar(bootFlash / num3);
			do
			{
				uint instruction = (uint)(num4 * bytesPerLocation + 532676608);
				byte[] array3 = new byte[3 + num2 * 4];
				int offset2 = 0;
				array3[offset2++] = 167;
				array3[offset2++] = 168;
				array3[offset2++] = (byte)(num2 * 4);
				for (int l = 0; l < num2; l++)
					offset2 = AddInstruction(array3, instruction, offset2);

				PICkitFunctions.WriteUSB(array3);
				PICkitFunctions.RunScriptUploadNoLen(3, num2);
				Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 0, 64);
				PICkitFunctions.UploadDataNoLen();
				Array.Copy(PICkitFunctions.Usb_read_array, 1, array, 64, 64);
				num7 = 0;
				for (int m = 0; m < num3; m++)
				{
					int num10 = 0;
					uint num11 = array[num7 + num10++];
					if (num10 < bytesPerLocation)
						num11 |= (uint)array[num7 + num10++] << 8;
					if (num10 < bytesPerLocation)
						num11 |= (uint)array[num7 + num10++] << 16;
					if (num10 < bytesPerLocation)
						num11 |= (uint)array[num7 + num10++] << 24;
					num7 += num10;
					PICkitFunctions.DeviceBuffers.ProgramMemory[num + num4++] = num11;
					if (num4 == bootFlash)
						break;
				}
				StepStatusBar();
			}
			while (num4 < bootFlash);
			text += "UserIDs... ";
			UpdateStatusWinText(text);
			PICkitFunctions.DeviceBuffers.UserIDs[0] = array[num7];
			PICkitFunctions.DeviceBuffers.UserIDs[1] = array[num7 + 1];
			num7 += bytesPerLocation;
			text += "Config... ";
			UpdateStatusWinText(text);
			for (int n = 0; n < PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords; n++)
			{
				PICkitFunctions.DeviceBuffers.ConfigWords[n] = array[num7++];
				PICkitFunctions.DeviceBuffers.ConfigWords[n] |= (uint)array[num7++] << 8;
			}
			text += "Done.";
			UpdateStatusWinText(text);
			PICkitFunctions.RunScript(1, 1);
			return true;
		}

		public static bool PIC32BlankCheck()
		{
			PICkitFunctions.SetMCLRTemp(true);
			PICkitFunctions.VddOn();
			if (!PE_DownloadAndConnect())
				return false;

			string text = "Checking if Device is blank:\n";
			UpdateStatusWinText(text);
			int num = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem;
			int bootFlash = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BootFlash;
			num -= bootFlash;
			int bytesPerLocation = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
			text += "Program Flash... ";
			UpdateStatusWinText(text);
			if (!PEBlankCheck(486539264, (uint)(num * bytesPerLocation)))
			{
				text = "Program Flash is not blank";
				UpdateStatusWinText(text);
				PICkitFunctions.RunScript(1, 1);
				return false;
			}
			text += "Boot Flash... ";
			UpdateStatusWinText(text);
			if (!PEBlankCheck(532676608, (uint)(bootFlash * bytesPerLocation)))
			{
				text = "Boot Flash is not blank";
				UpdateStatusWinText(text);
				PICkitFunctions.RunScript(1, 1);
				return false;
			}
			text += "UserID & Config... ";
			UpdateStatusWinText(text);
			if (!PEBlankCheck((uint)(532676608 + bootFlash * bytesPerLocation), 16))
			{
				text = "ID / Config Memory is not blank";
				UpdateStatusWinText(text);
				PICkitFunctions.RunScript(1, 1);
				return false;
			}
			PICkitFunctions.RunScript(1, 1);
			text = "Device is Blank.";
			UpdateStatusWinText(text);
			return true;
		}

		public static bool P32Write(bool verifyWrite, bool codeProtect)
		{
			PICkitFunctions.SetMCLRTemp(true);
			PICkitFunctions.VddOn();
			PICkitFunctions.RunScript(0, 1);
			PICkitFunctions.RunScript(22, 1);
			if (!PE_DownloadAndConnect())
				return false;

			PICkitFunctions.RunScript(22, 1);
			string text = "Writing device:\n";
			UpdateStatusWinText(text);
			text += "Program Flash... ";
			UpdateStatusWinText(text);
			int num = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem;
			int bootFlash = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BootFlash;
			num -= bootFlash;
			int num2 = 128;
			int num3 = PICkitFunctions.FindLastUsedInBuffer(PICkitFunctions.DeviceBuffers.ProgramMemory, PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue, num - 1);
			int num4 = (num3 + 1) / num2;
			if ((num3 + 1) % num2 > 0)
				num4++;
			if (num4 < 2)
				num4 = 2;
			ResetStatusBar(num3 / num2);
			PEProgramHeader(486539264U, (uint)(num4 * 512));
			int num5 = 0;
			PEProgramSendBlock(num5, false);
			num4--;
			StepStatusBar();
			do
			{
				num5 += num2;
				PEProgramSendBlock(num5, true);
				StepStatusBar();
			}
			while (--num4 > 0);
			byte[] array = new byte[4];
			int num6 = 0;
			array[num6++] = 169;
			array[num6++] = 166;
			array[num6++] = 1;
			array[num6++] = 181;
			PICkitFunctions.WriteUSB(array);
			text += "Boot Flash... ";
			UpdateStatusWinText(text);
			num2 = 128;
			num3 = PICkitFunctions.FindLastUsedInBuffer(PICkitFunctions.DeviceBuffers.ProgramMemory, PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue, PICkitFunctions.DeviceBuffers.ProgramMemory.Length - 1);
			if (num3 < num)
				num3 = 1;
			else
				num3 -= num;

			num4 = (num3 + 1) / num2;
			if ((num3 + 1) % num2 > 0)
				num4++;

			if (num4 < 2)
				num4 = 2;

			ResetStatusBar(num3 / num2);
			PEProgramHeader(532676608U, (uint)(num4 * 512));
			num5 = num;
			PEProgramSendBlock(num5, false);
			num4--;
			StepStatusBar();
			do
			{
				num5 += num2;
				PEProgramSendBlock(num5, true);
				StepStatusBar();
			}
			while (--num4 > 0);
			PICkitFunctions.WriteUSB(array);
			text += "UserID & Config... ";
			UpdateStatusWinText(text);
			uint[] array2 = new uint[4];
			array2[0] = PICkitFunctions.DeviceBuffers.UserIDs[0] & 255U;
			array2[0] |= (PICkitFunctions.DeviceBuffers.UserIDs[1] & 255U) << 8;
			array2[0] |= 4294901760U;
			array2[1] = (PICkitFunctions.DeviceBuffers.ConfigWords[0] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0]) | (PICkitFunctions.DeviceBuffers.ConfigWords[1] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[1]) << 16;
			array2[1] |= (uint)((~PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank[0]) | (~PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[1] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank[1]) << 16);
			array2[2] = (PICkitFunctions.DeviceBuffers.ConfigWords[2] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[2]) | (PICkitFunctions.DeviceBuffers.ConfigWords[3] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[3]) << 16;
			array2[2] |= (uint)((~PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[2] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank[2]) | (~PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[3] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank[3]) << 16);
			array2[3] = (PICkitFunctions.DeviceBuffers.ConfigWords[4] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[4]) | (PICkitFunctions.DeviceBuffers.ConfigWords[5] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[5]) << 16;
			array2[3] |= (uint)((~PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[4] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank[4]) | (~PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[5] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank[5]) << 16);
			if (codeProtect)
				array2[3] &= ~((uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPMask << 16);

			uint num7 = (uint)(532676608 + bootFlash * 4);
			byte[] array3 = new byte[39];
			num6 = 0;
			array3[num6++] = 169;
			array3[num6++] = 166;
			array3[num6++] = 36;
			array3[num6++] = 187;
			array3[num6++] = 14;
			array3[num6++] = 184;
			array3[num6++] = 0;
			array3[num6++] = 0;
			array3[num6++] = 3;
			array3[num6++] = 0;
			array3[num6++] = 184;
			array3[num6++] = (byte)(num7 & 255U);
			array3[num6++] = (byte)(num7 >> 8 & 255U);
			array3[num6++] = (byte)(num7 >> 16 & 255U);
			array3[num6++] = (byte)(num7 >> 24 & 255U);
			array3[num6++] = 184;
			array3[num6++] = (byte)(array2[0] & 255U);
			array3[num6++] = (byte)(array2[0] >> 8 & 255U);
			array3[num6++] = (byte)(array2[0] >> 16 & 255U);
			array3[num6++] = (byte)(array2[0] >> 24 & 255U);
			array3[num6++] = 180;
			num7 += 4U;
			array3[num6++] = 187;
			array3[num6++] = 14;
			array3[num6++] = 184;
			array3[num6++] = 0;
			array3[num6++] = 0;
			array3[num6++] = 3;
			array3[num6++] = 0;
			array3[num6++] = 184;
			array3[num6++] = (byte)(num7 & 255U);
			array3[num6++] = (byte)(num7 >> 8 & 255U);
			array3[num6++] = (byte)(num7 >> 16 & 255U);
			array3[num6++] = (byte)(num7 >> 24 & 255U);
			array3[num6++] = 184;
			array3[num6++] = (byte)(array2[1] & 255U);
			array3[num6++] = (byte)(array2[1] >> 8 & 255U);
			array3[num6++] = (byte)(array2[1] >> 16 & 255U);
			array3[num6++] = (byte)(array2[1] >> 24 & 255U);
			array3[num6++] = 180;
			num7 += 4U;
			PICkitFunctions.WriteUSB(array3);
			num6 = 0;
			array3[num6++] = 169;
			array3[num6++] = 166;
			array3[num6++] = 36;
			array3[num6++] = 187;
			array3[num6++] = 14;
			array3[num6++] = 184;
			array3[num6++] = 0;
			array3[num6++] = 0;
			array3[num6++] = 3;
			array3[num6++] = 0;
			array3[num6++] = 184;
			array3[num6++] = (byte)(num7 & 255U);
			array3[num6++] = (byte)(num7 >> 8 & 255U);
			array3[num6++] = (byte)(num7 >> 16 & 255U);
			array3[num6++] = (byte)(num7 >> 24 & 255U);
			array3[num6++] = 184;
			array3[num6++] = (byte)(array2[2] & 255U);
			array3[num6++] = (byte)(array2[2] >> 8 & 255U);
			array3[num6++] = (byte)(array2[2] >> 16 & 255U);
			array3[num6++] = (byte)(array2[2] >> 24 & 255U);
			array3[num6++] = 180;
			num7 += 4U;
			array3[num6++] = 187;
			array3[num6++] = 14;
			array3[num6++] = 184;
			array3[num6++] = 0;
			array3[num6++] = 0;
			array3[num6++] = 3;
			array3[num6++] = 0;
			array3[num6++] = 184;
			array3[num6++] = (byte)(num7 & 255U);
			array3[num6++] = (byte)(num7 >> 8 & 255U);
			array3[num6++] = (byte)(num7 >> 16 & 255U);
			array3[num6++] = (byte)(num7 >> 24 & 255U);
			array3[num6++] = 184;
			array3[num6++] = (byte)(array2[3] & 255U);
			array3[num6++] = (byte)(array2[3] >> 8 & 255U);
			array3[num6++] = (byte)(array2[3] >> 16 & 255U);
			array3[num6++] = (byte)(array2[3] >> 24 & 255U);
			array3[num6++] = 180;
            PICkitFunctions.WriteUSB(array3);
			if (verifyWrite)
				return P32Verify(true, codeProtect);

			PICkitFunctions.RunScript(1, 1);
			return true;
		}

		private static void PEProgramHeader(uint startAddress, uint lengthBytes)
		{
			byte[] array = new byte[20];
			int num = 0;
			array[num++] = 169;
			array[num++] = 166;
			array[num++] = 17;
			array[num++] = 187;
			array[num++] = 14;
			array[num++] = 184;
			array[num++] = 0;
			array[num++] = 0;
			array[num++] = 2;
			array[num++] = 0;
			array[num++] = 184;
			array[num++] = (byte)(startAddress & 255U);
			array[num++] = (byte)(startAddress >> 8 & 255U);
			array[num++] = (byte)(startAddress >> 16 & 255U);
			array[num++] = (byte)(startAddress >> 24 & 255U);
			array[num++] = 184;
			array[num++] = (byte)(lengthBytes & 255U);
			array[num++] = (byte)(lengthBytes >> 8 & 255U);
			array[num++] = (byte)(lengthBytes >> 16 & 255U);
			array[num++] = (byte)(lengthBytes >> 24 & 255U);
			PICkitFunctions.WriteUSB(array);
		}

		private static void PEProgramSendBlock(int index, bool peResp)
		{
			byte[] array = new byte[256];
			int num = 0;
			int num2 = PICkitFunctions.DeviceBuffers.ProgramMemory.Length;
			for (int i = 0; i < 64; i++)
			{
				uint num3;
				if (index < num2)
					num3 = PICkitFunctions.DeviceBuffers.ProgramMemory[index++];
				else
					num3 = uint.MaxValue;

				array[num++] = (byte)(num3 & 255U);
				array[num++] = (byte)(num3 >> 8 & 255U);
				array[num++] = (byte)(num3 >> 16 & 255U);
				array[num++] = (byte)(num3 >> 24 & 255U);
			}
			int num4 = PICkitFunctions.DataClrAndDownload(array, 0);
			while (num - num4 > 62)
				num4 = PICkitFunctions.DataDownload(array, num4, array.Length);

			int num5 = num - num4;
			byte[] array2 = new byte[5 + num5];
			int num6 = 0;
			array2[num6++] = 168;
			array2[num6++] = (byte)(num5 & 255);
			for (int j = 0; j < num5; j++)
				array2[num6++] = array[num4 + j];

			array2[num6++] = 165;
			array2[num6++] = 6;
			array2[num6++] = 1;
			PICkitFunctions.WriteUSB(array2);
			num = 0;
			for (int k = 0; k < 64; k++)
			{
				uint num3;
				if (index < num2)
					num3 = PICkitFunctions.DeviceBuffers.ProgramMemory[index++];
				else
					num3 = uint.MaxValue;

				array[num++] = (byte)(num3 & 255U);
				array[num++] = (byte)(num3 >> 8 & 255U);
				array[num++] = (byte)(num3 >> 16 & 255U);
				array[num++] = (byte)(num3 >> 24 & 255U);
			}
			num4 = PICkitFunctions.DataClrAndDownload(array, 0);
			while (num - num4 > 62)
				num4 = PICkitFunctions.DataDownload(array, num4, array.Length);

			num5 = num - num4;
			num6 = 0;
			array2[num6++] = 168;
			array2[num6++] = (byte)(num5 & 255);
			for (int l = 0; l < num5; l++)
				array2[num6++] = array[num4 + l];

			array2[num6++] = 165;
			if (peResp)
				array2[num6++] = 7;
			else
				array2[num6++] = 6;

			array2[num6++] = 1;
			PICkitFunctions.WriteUSB(array2);
		}

		public static bool P32Verify(bool writeVerify, bool codeProtect)
		{
			if (!writeVerify)
			{
				PICkitFunctions.SetMCLRTemp(true);
				PICkitFunctions.VddOn();
				if (!PE_DownloadAndConnect())
					return false;
			}
			string text = "Verifying Device:\n";
			UpdateStatusWinText(text);
			int num = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem;
			int bootFlash = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BootFlash;
			num -= bootFlash;
			int bytesPerLocation = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
			text += "Program Flash... ";
			UpdateStatusWinText(text);
			int num2 = P32CRC_buf(PICkitFunctions.DeviceBuffers.ProgramMemory, 0, (uint)num);
			int num3 = PEGetCRC(486539264U, (uint)(num * bytesPerLocation));
			if (num2 != num3)
			{
				if (writeVerify)
				{
					text = "Programming Program Flash Failed.";
					UpdateStatusWinText(text);
				}
				else
				{
					text = "Verify of Program Flash Failed.";
					UpdateStatusWinText(text);
				}
				PICkitFunctions.RunScript(1, 1);
				return false;
			}
			text += "Boot Flash... ";
			UpdateStatusWinText(text);
			num2 = P32CRC_buf(PICkitFunctions.DeviceBuffers.ProgramMemory, (uint)num, (uint)bootFlash);
			num3 = PEGetCRC(532676608U, (uint)(bootFlash * bytesPerLocation));
			if (num2 != num3)
			{
				if (writeVerify)
				{
					text = "Programming Boot Flash Failed.";
					UpdateStatusWinText(text);
				}
				else
				{
					text = "Verify of Boot Flash Failed.";
					UpdateStatusWinText(text);
				}
				PICkitFunctions.RunScript(1, 1);
				return false;
			}
			text += "ID/Config Flash... ";
			UpdateStatusWinText(text);
			uint[] array = new uint[4];
			array[0] = PICkitFunctions.DeviceBuffers.UserIDs[0] & 255U;
			array[0] |= (PICkitFunctions.DeviceBuffers.UserIDs[1] & 255U) << 8;
			array[0] |= 4294901760;
			array[1] = (PICkitFunctions.DeviceBuffers.ConfigWords[0] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0]) | (PICkitFunctions.DeviceBuffers.ConfigWords[1] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[1]) << 16;
			array[1] |= (uint)((~PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[0] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank[0]) | (~PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[1] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank[1]) << 16);
			array[2] = (PICkitFunctions.DeviceBuffers.ConfigWords[2] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[2]) | (PICkitFunctions.DeviceBuffers.ConfigWords[3] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[3]) << 16;
			array[2] |= (uint)((~PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[2] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank[2]) | (~PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[3] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank[3]) << 16);
			array[3] = (PICkitFunctions.DeviceBuffers.ConfigWords[4] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[4]) | (PICkitFunctions.DeviceBuffers.ConfigWords[5] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[5]) << 16;
			array[3] |= (uint)((~PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[4] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank[4]) | (~PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[5] & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank[5]) << 16);
			if (codeProtect)
				array[3] &= ~((uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPMask << 16);

			num2 = P32CRC_buf(array, 0, 4);
			num3 = PEGetCRC((uint)(532676608 + bootFlash * bytesPerLocation), (uint)(4 * bytesPerLocation));
			if (num2 != num3)
			{
				if (writeVerify)
				{
					text = "Programming ID/Config Flash Failed.";
					UpdateStatusWinText(text);
				}
				else
				{
					text = "Verify of ID/Config Flash Failed.";
					UpdateStatusWinText(text);
				}
				PICkitFunctions.RunScript(1, 1);
				return false;
			}
			if (!writeVerify)
			{
				text = "Verification Successful.\n";
				UpdateStatusWinText(text);
			}
			else
			{
				text = "Programming Successful.\n";
				UpdateStatusWinText(text);
			}
			PICkitFunctions.RunScript(1, 1);
			return true;
		}

		private static int P32CRC_buf(uint[] buffer, uint startIdx, uint len)
		{
			uint num = 69665;
			uint num2 = 65535;
			uint num3 = num2;
			uint bytesPerLocation = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation;
			for (uint num5 = startIdx; num5 < startIdx + len; num5 += 1)
			{
				uint num6 = buffer[(int)num5];
				for (uint num7 = 0U; num7 < bytesPerLocation; num7 += 1)
				{
					uint num8 = (num6 & 255U) << 8;
					num6 >>= 8;
					for (uint num9 = 0; num9 < 8; num9 += 1)
					{
						uint num10 = (num3 ^ num8) & 32768U;
						num3 <<= 1;
						num8 <<= 1;
						if (num10 > 0)
						{
							num3 ^= num;
						}
					}
				}
			}
			return (int)(num3 & 65535U);
		}

        public static DelegateStatusWin UpdateStatusWinText;
		public static DelegateResetStatusBar ResetStatusBar;
		public static DelegateStepStatusBar StepStatusBar;
		private static readonly uint[] pe_Loader = new uint[]
		{
			15367U,
			57005U,
			15366U,
			65312U,
			15365U,
			65312U,
			36036U,
			0U,
			36035U,
			0U,
			4199U,
			11U,
			0U,
			0U,
			4192U,
			65531U,
			0U,
			0U,
			36002U,
			0U,
			9315U,
			65535U,
			44162U,
			0U,
			9348U,
			4U,
			5216U,
			65531U,
			0U,
			0U,
			4096U,
			65523U,
			0U,
			0U,
			15362U,
			40960U,
			13378U,
			2304U,
			64U,
			8U,
			0U,
			0U
		};

		private static readonly uint[] PIC32_PE = new uint[]
		{
			1008508928U,
			664567792U,
			1008574464U,
			935135484U,
			1007198208U,
			621285532U,
			16777224U,
			0U,
			1007075208U,
			2429051232U,
			1007009672U,
			889651264U,
			2697421152U,
			2426691904U,
			604176319U,
			8525860U,
			2695061824U,
			65011720U,
			0U,
			1007075208U,
			2429051232U,
			604372927U,
			17119268U,
			2697421152U,
			1006878600U,
			2422497600U,
			8720420U,
			2690801984U,
			65011720U,
			0U,
			1007075208U,
			2429051233U,
			1007009672U,
			889651201U,
			2697421153U,
			2426691905U,
			604176382U,
			8525860U,
			2695061825U,
			65011720U,
			0U,
			1007075208U,
			2429051233U,
			604372990U,
			17119268U,
			2697421153U,
			1006878600U,
			2422497601U,
			8720420U,
			2690801985U,
			65011720U,
			0U,
			338050U,
			278921243U,
			14369U,
			1007034304U,
			1006878656U,
			1006796799U,
			885600252U,
			879243260U,
			877395967U,
			268435462U,
			604700671U,
			2358837248U,
			388694030U,
			0U,
			281018382U,
			612630532U,
			9027622U,
			8943654U,
			789381121U,
			770572289U,
			619118593U,
			28205093U,
			360775668U,
			15020075U,
			2357329920U,
			273350644U,
			0U,
			65011720U,
			604110849U,
			65011720U,
			4129U,
			1006960416U,
			2357395456U,
			744620034U,
			272629763U,
			604241921U,
			2944630804U,
			8225U,
			65011720U,
			8392737U,
			278921223U,
			1006829344U,
			876806156U,
			2357592064U,
			614858751U,
			2892365824U,
			346095612U,
			612630532U,
			65011720U,
			0U,
			134219008U,
			0U,
			666763216U,
			2947874852U,
			2947809312U,
			2947678232U,
			2948530220U,
			2947940392U,
			2947743772U,
			2947612692U,
			2947547152U,
			10526753U,
			8425505U,
			278921252U,
			1008074528U,
			1006772223U,
			878116863U,
			713228417U,
			605225088U,
			42178571U,
			1255466U,
			278921224U,
			1007067136U,
			616825360U,
			39852065U,
			2393309184U,
			610533375U,
			2894528512U,
			341901308U,
			612630532U,
			604242048U,
			1382285341U,
			1007001600U,
			278921229U,
			34849U,
			1007263744U,
			623903248U,
			2382692352U,
			37756961U,
			201327872U,
			640745473U,
			638582788U,
			36902954U,
			339738631U,
			642908164U,
			1415643128U,
			2382692352U,
			43229219U,
			377552865U,
			713228417U,
			4129U,
			2411659308U,
			2411069480U,
			2411003940U,
			2410938400U,
			2410872860U,
			2410807320U,
			2410741780U,
			2410676240U,
			65011720U,
			666697776U,
			614662672U,
			73400323U,
			46344228U,
			1007173632U,
			6826017U,
			201327880U,
			37756961U,
			339804142U,
			43229219U,
			377552842U,
			642908672U,
			268500970U,
			4129U,
			1006804992U,
			608633360U,
			1007091488U,
			14690337U,
			604176511U,
			2361917440U,
			610533375U,
			2896691200U,
			73531388U,
			614793220U,
			1007042560U,
			14686241U,
			81854468U,
			15083553U,
			1006968831U,
			883425279U,
			6760484U,
			134219016U,
			0U,
			666763232U,
			2947678232U,
			2947612692U,
			2947547152U,
			2948530204U,
			10522657U,
			8421409U,
			346030087U,
			34849U,
			2411659292U,
			2410807320U,
			2410741780U,
			2410676240U,
			65011720U,
			666697760U,
			33562657U,
			201327888U,
			640745473U,
			638586880U,
			272695285U,
			36837419U,
			341901306U,
			33562657U,
			2411659292U,
			2410807320U,
			2410741780U,
			2410676240U,
			65011720U,
			666697760U,
			134219030U,
			0U,
			2407694356U,
			666763192U,
			2948005948U,
			2947940408U,
			2947547168U,
			2948530240U,
			2947874868U,
			2947809328U,
			2947743788U,
			2947678248U,
			2947612708U,
			8421409U,
			10532897U,
			12628001U,
			2745171992U,
			272629825U,
			2745171993U,
			363010U,
			1006960641U,
			604638976U,
			1007402888U,
			1007271816U,
			872973519U,
			1007075208U,
			883167265U,
			1006878600U,
			2909417524U,
			2905223224U,
			2898800704U,
			2892247120U,
			438304790U,
			853934335U,
			1007108095U,
			888537087U,
			1007984640U,
			1007927296U,
			605159427U,
			45115435U,
			34889765U,
			34809889U,
			604438530U,
			26028042U,
			46145569U,
			604373248U,
			201328002U,
			2947678224U,
			4208673U,
			640811007U,
			339738683U,
			638583040U,
			505479155U,
			45115435U,
			853934335U,
			281018382U,
			1007828991U,
			911343615U,
			1007656960U,
			36728875U,
			297795587U,
			34547749U,
			1007927296U,
			34809889U,
			604438530U,
			46145569U,
			605028355U,
			201328002U,
			2947547152U,
			4208673U,
			283115571U,
			604110849U,
			2411659328U,
			2411135036U,
			2411069496U,
			2411003956U,
			2410938416U,
			2410872876U,
			2410807336U,
			2410741796U,
			2410676256U,
			65011720U,
			666697800U,
			201327956U,
			872742911U,
			1008680959U,
			939130879U,
			1008246784U,
			53520427U,
			312475651U,
			35135525U,
			1006813184U,
			33693729U,
			201327979U,
			48244769U,
			201328000U,
			0U,
			2931949568U,
			2411659328U,
			2411135036U,
			2411069496U,
			2411003956U,
			2410938416U,
			2410872876U,
			2410807336U,
			2410741796U,
			2410676256U,
			4129U,
			65011720U,
			666697800U,
			2411659328U,
			2411135036U,
			2411069496U,
			2411003956U,
			2410938416U,
			2410872876U,
			2410807336U,
			2410741796U,
			2410676256U,
			604110849U,
			65011720U,
			666697800U,
			46145569U,
			665124888U,
			605290499U,
			604372994U,
			604438530U,
			201328002U,
			2947809296U,
			2411659328U,
			2411135036U,
			2411069496U,
			2411003956U,
			2410938416U,
			2410872876U,
			2410807336U,
			2410741796U,
			2410676256U,
			135211U,
			65011720U,
			666697800U,
			666763208U,
			2948005932U,
			2947874852U,
			2947743772U,
			2947678232U,
			2947612692U,
			2947547152U,
			2948530228U,
			2948464688U,
			2947940392U,
			2947809312U,
			10520609U,
			8425505U,
			43041U,
			605487105U,
			1007746848U,
			278921273U,
			38945U,
			1006804992U,
			1006837759U,
			609616400U,
			880738303U,
			46145569U,
			604176511U,
			2382692352U,
			610533375U,
			2894397440U,
			73531388U,
			612630532U,
			316670022U,
			0U,
			47137U,
			642055680U,
			9803787U,
			1449132041U,
			640810880U,
			1007173632U,
			64368676U,
			18229281U,
			717684736U,
			13051915U,
			201327896U,
			37756961U,
			640810880U,
			707330176U,
			354418714U,
			642908672U,
			1007394816U,
			627711032U,
			2371092480U,
			604176511U,
			41951265U,
			2383151104U,
			610533375U,
			2894856192U,
			73531388U,
			612630532U,
			201327916U,
			0U,
			642645504U,
			27432971U,
			375390216U,
			4237345U,
			1008222208U,
			714014720U,
			64253988U,
			51652641U,
			30353419U,
			201327896U,
			37756961U,
			640810880U,
			642908672U,
			371261390U,
			46145569U,
			201327916U,
			1286187U,
			165931U,
			36995109U,
			301989913U,
			1008205600U,
			1008140064U,
			918814732U,
			605356034U,
			2895446016U,
			1382023202U,
			643038720U,
			2895314944U,
			2411659316U,
			2411593776U,
			2411135020U,
			2411069480U,
			2411003940U,
			2410938400U,
			2410872860U,
			2410807320U,
			2410741780U,
			2410676240U,
			4129U,
			65011720U,
			666697784U,
			201327916U,
			0U,
			268500921U,
			4237345U,
			921829388U,
			2923429888U,
			2411659316U,
			2411593776U,
			2411135020U,
			2411069480U,
			2411003940U,
			2410938400U,
			2410872860U,
			2410807320U,
			2410741780U,
			2410676240U,
			4129U,
			65011720U,
			666697784U,
			2895314944U,
			268500959U,
			2411659316U,
			666763200U,
			1006813064U,
			604245955U,
			2948530232U,
			2947612724U,
			2947547184U,
			2890149936U,
			2353213488U,
			274989232U,
			604307457U,
			2944434196U,
			1007271808U,
			891875872U,
			2366046208U,
			1007067136U,
			2096184064U,
			617618420U,
			1007550240U,
			2376859648U,
			604700673U,
			799746U,
			830865407U,
			751435792U,
			2945155088U,
			2946891792U,
			289406982U,
			2946826260U,
			442496U,
			51410977U,
			2381185024U,
			29360136U,
			0U,
			604307459U,
			606011407U,
			816119807U,
			283050017U,
			1006829583U,
			604176391U,
			281215102U,
			604438538U,
			281477252U,
			604700674U,
			281804774U,
			424960U,
			905510914U,
			1008271136U,
			52787211U,
			923140108U,
			2899247104U,
			2409824272U,
			748945409U,
			946733057U,
			768409601U,
			27549732U,
			360710269U,
			945946632U,
			742588417U,
			6617124U,
			1407254487U,
			1007550240U,
			2409889832U,
			2898526208U,
			268500947U,
			1007550240U,
			2407890964U,
			606011407U,
			816119807U,
			350224353U,
			1006829583U,
			1006829344U,
			8593445U,
			877002764U,
			2898526208U,
			268500936U,
			1007550240U,
			201327251U,
			0U,
			2410020880U,
			268500946U,
			4204577U,
			268500944U,
			10273U,
			1007550240U,
			2376335360U,
			2946760728U,
			2376859648U,
			809090U,
			23078945U,
			201327272U,
			2947219476U,
			2410020880U,
			268500933U,
			4204577U,
			2946498600U,
			1008336672U,
			2401501184U,
			665190440U,
			2946760728U,
			2402811904U,
			50341921U,
			201327394U,
			2948071444U,
			2410020880U,
			268500921U,
			4204577U,
			268500919U,
			604307721U,
			1007681312U,
			2380529664U,
			2946760728U,
			2381185024U,
			29370401U,
			201327220U,
			2947416084U,
			2410020880U,
			268500909U,
			4204577U,
			1006829344U,
			2353397760U,
			12591137U,
			201327363U,
			2946891800U,
			2410020880U,
			268500901U,
			4204577U,
			201327392U,
			0U,
			2410020880U,
			268500896U,
			4204577U,
			1008729888U,
			2414084096U,
			2946760728U,
			2415460352U,
			52439073U,
			201327270U,
			2948136988U,
			2410020880U,
			268500886U,
			4204577U,
			1007353632U,
			2370043904U,
			2946760728U,
			2370371584U,
			606338U,
			16787489U,
			201327533U,
			2947022868U,
			2410020880U,
			268500875U,
			4204577U,
			1007025952U,
			2359754752U,
			604307458U,
			469003U,
			268500869U,
			2946957336U,
			1006960416U,
			2357395456U,
			6299681U,
			201327344U,
			2946695192U,
			2410020880U,
			268500861U,
			4204577U,
			816119807U,
			1006829575U,
			1006829344U,
			8593445U,
			877002764U,
			2898526208U,
			268500838U,
			1007550240U,
			1007288330U,
			1007288096U,
			36323365U,
			891617292U,
			2896691200U,
			268500831U,
			1007550240U,
			2409889816U,
			201327260U,
			2409955348U,
			268500826U,
			1007550240U,
			268500817U,
			2944761876U,
			65011720U,
			4129U,
			881082368U,
			1007009665U,
			2896491520U,
			1007206272U,
			889779200U,
			1007266457U,
			891905621U,
			1007310182U,
			894081450U,
			1007353856U,
			896237568U,
			2903048208U,
			2903113744U,
			2903179272U,
			2359555072U,
			811761664U,
			339804157U,
			0U,
			0U,
			0U,
			0U,
			0U,
			604520448U,
			1007140737U,
			2900947972U,
			2359751680U,
			65011720U,
			818028544U,
			8400929U,
			1006813057U,
			1006878593U,
			604241921U,
			2890331168U,
			2892362800U,
			134218980U,
			0U,
			8400929U,
			1006813057U,
			1006878593U,
			604241923U,
			2890331168U,
			2892362816U,
			134218980U,
			0U,
			8394785U,
			1006813057U,
			604241924U,
			2890134560U,
			134218980U,
			0U,
			134218980U,
			604241934U,
			1007075201U,
			2898588704U,
			1006878593U,
			604258307U,
			1006813057U,
			2892362816U,
			2890200064U,
			1007206272U,
			889779200U,
			1007266457U,
			891905621U,
			1007310182U,
			894081450U,
			1007353856U,
			896237568U,
			2903048208U,
			2903113744U,
			2903179272U,
			65011720U,
			4129U,
			1006944129U,
			2357457920U,
			811761664U,
			339804157U,
			0U,
			0U,
			0U,
			0U,
			0U,
			604454912U,
			1007075201U,
			2898785284U,
			2357588992U,
			65011720U,
			815931392U,
			1006804992U,
			608764432U,
			16417U,
			532992U,
			813957119U,
			10273U,
			604372999U,
			352320U,
			10704934U,
			965152801U,
			2081117728U,
			88080386U,
			820379647U,
			830865407U,
			223296U,
			617021439U,
			79822838U,
			832831487U,
			621281281U,
			688062720U,
			2904883200U,
			341901293U,
			623443972U,
			65011720U,
			0U,
			666763240U,
			2948530192U,
			2810478620U,
			201327931U,
			2944434200U,
			2411659280U,
			604110849U,
			666697752U,
			65011720U,
			2944565272U,
			2542174236U,
			814416127U,
			2093758976U,
			23875622U,
			1007263744U,
			669824U,
			623378960U,
			15212577U,
			2357526528U,
			397824U,
			4528166U,
			65011720U,
			2810413084U,
			666763232U,
			2947612692U,
			2947547152U,
			2948530204U,
			2947678232U,
			8423457U,
			278921224U,
			615579647U,
			605224959U,
			2451832832U,
			638648319U,
			201327966U,
			640745473U,
			1444085756U,
			2451832832U,
			2411659292U,
			2410807320U,
			2410741780U,
			2410676240U,
			65011720U,
			666697760U,
			65011720U,
			2541912092U,
			604504256U,
			1894303746U,
			666763240U,
			2948530192U,
			1008705536U,
			2414418996U,
			1007468424U,
			604635264U,
			2911514676U,
			53035041U,
			2434269184U,
			14702625U,
			2131689920U,
			604569664U,
			604901376U,
			1007533960U,
			872644608U,
			1006813064U,
			770113537U,
			2903048216U,
			2913873924U,
			2890084360U,
			283115523U,
			0U,
			2903113736U,
			2903048216U,
			2433155072U,
			2095710656U,
			356581373U,
			2411200552U,
			604962872U,
			604962815U,
			604831747U,
			2904031232U,
			1006813064U,
			2903441428U,
			895680704U,
			2903375908U,
			2911711284U,
			1007435776U,
			2890084408U,
			77660164U,
			11276321U,
			1008279551U,
			925499391U,
			11079716U,
			1006977024U,
			2902589488U,
			75563012U,
			8720417U,
			1007296511U,
			895483903U,
			10424356U,
			2902589504U,
			2902851728U,
			2366046352U,
			23076897U,
			2902917216U,
			2365980768U,
			604307458U,
			2902851664U,
			201328068U,
			12321U,
			2411659280U,
			65011720U,
			666697752U,
			604569792U,
			1888040962U,
			1007329280U,
			2370313268U,
			604176512U,
			15212577U,
			604111103U,
			2894200868U,
			20513U,
			2894266376U,
			423979U,
			2894266392U,
			278921273U,
			0U,
			604700673U,
			279642134U,
			604176385U,
			2357985360U,
			293601282U,
			604504320U,
			2357723216U,
			2358050912U,
			295698434U,
			604438784U,
			2357657696U,
			2358116496U,
			297795588U,
			604176640U,
			2357395600U,
			268435458U,
			17240107U,
			17240107U,
			16922634U,
			14927905U,
			658046975U,
			50528283U,
			6291956U,
			6162U,
			610861055U,
			604897281U,
			604831748U,
			604766336U,
			604766207U,
			2357395488U,
			813170691U,
			2087256192U,
			318767110U,
			2087125184U,
			2086928384U,
			276824084U,
			604635138U,
			268435474U,
			604635137U,
			350224400U,
			0U,
			287309832U,
			0U,
			279838732U,
			0U,
			1358954506U,
			604635139U,
			621346815U,
			2894921764U,
			2894856216U,
			299958250U,
			0U,
			617021439U,
			348913639U,
			0U,
			604635141U,
			65011720U,
			20975649U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			17419691U,
			2684359440U,
			2684359416U,
			2684359372U,
			2684359332U,
			2684359312U,
			2684359280U,
			2684359240U,
			2684359232U,
			2684359184U,
			2684359140U,
			2684359132U,
			2684359112U,
			2684358940U,
			2684358940U,
			2684358940U,
			2684359068U,
			3213373536U,
			2684354576U
		};
	}
}
