using System;
using System.IO;
using System.Text;

namespace PICkit2V3
{
	// Token: 0x02000013 RID: 19
	public class PICkitFunctions
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00035C71 File Offset: 0x00034C71
		public static void TestingMethod()
		{
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00035C74 File Offset: 0x00034C74
		public static bool CheckComm()
		{
			if (PICkitFunctions.writeUSB(new byte[]
			{
				167,
				168,
				8,
				1,
				2,
				3,
				4,
				5,
				6,
				7,
				8,
				185,
				0,
				1,
				170,
				167,
				169
			}) && PICkitFunctions.readUSB() && PICkitFunctions.Usb_read_array[1] == 63)
			{
				for (int i = 1; i < 9; i++)
				{
					if ((int)PICkitFunctions.Usb_read_array[1 + i] != i)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00035D28 File Offset: 0x00034D28
		public static bool EnterLearnMode(byte memsize)
		{
			if (PICkitFunctions.writeUSB(new byte[]
			{
				181,
				80,
				75,
				50,
				memsize
			}))
			{
				PICkitFunctions.LearnMode = true;
				float num = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].Vpp;
				if (num < 1f || (PICkitFunctions.lvpEnabled && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript > 0))
				{
					if (PICkitFunctions.lvpEnabled && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript > 0)
					{
						string text = PICkitFunctions.DevFile.Scripts[(int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript - 1)].ScriptName;
						text = text.Substring(text.Length - 2);
						if (text == "HV")
						{
							num = (float)PICkitFunctions.DevFile.Scripts[(int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript - 1)].Script[1] / 10f;
							PICkitFunctions.SetVppVoltage(num, 0.7f);
						}
						else
						{
							PICkitFunctions.SetVppVoltage(PICkitFunctions.vddLastSet, 0.7f);
						}
					}
					else
					{
						PICkitFunctions.SetVppVoltage(PICkitFunctions.vddLastSet, 0.7f);
					}
				}
				else
				{
					PICkitFunctions.SetVppVoltage(num, 0.7f);
				}
				PICkitFunctions.downloadPartScripts(PICkitFunctions.GetActiveFamily());
				return true;
			}
			return false;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00035EAC File Offset: 0x00034EAC
		public static bool ExitLearnMode()
		{
			PICkitFunctions.LearnMode = false;
			return PICkitFunctions.writeUSB(new byte[]
			{
				182
			});
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00035ED4 File Offset: 0x00034ED4
		public static bool EnablePK2GoMode(byte memsize)
		{
			PICkitFunctions.LearnMode = false;
			return PICkitFunctions.writeUSB(new byte[]
			{
				183,
				80,
				75,
				50,
				memsize
			});
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00035F10 File Offset: 0x00034F10
		public static bool MetaCmd_CHECK_DEVICE_ID()
		{
			int num = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].DeviceIDMask;
			int num2 = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DeviceID;
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift != 0)
			{
				num <<= 1;
				num2 <<= 1;
			}
			return PICkitFunctions.writeUSB(new byte[]
			{
				132,
				(byte)(num & 255),
				(byte)(num >> 8 & 255),
				(byte)(num2 & 255),
				(byte)(num2 >> 8 & 255)
			});
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00035FBC File Offset: 0x00034FBC
		public static bool MetaCmd_READ_BANDGAP()
		{
			return PICkitFunctions.writeUSB(new byte[]
			{
				133
			});
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00035FE0 File Offset: 0x00034FE0
		public static bool MetaCmd_WRITE_CFG_BANDGAP()
		{
			return PICkitFunctions.writeUSB(new byte[]
			{
				134
			});
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00036004 File Offset: 0x00035004
		public static bool MetaCmd_READ_OSCCAL()
		{
			int num = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem - 1U);
			return PICkitFunctions.writeUSB(new byte[]
			{
				128,
				(byte)(num & 255),
				(byte)(num >> 8 & 255)
			});
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0003605C File Offset: 0x0003505C
		public static bool MetaCmd_WRITE_OSCCAL()
		{
			int num = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem - 1U);
			return PICkitFunctions.writeUSB(new byte[]
			{
				129,
				(byte)(num & 255),
				(byte)(num >> 8 & 255)
			});
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000360B4 File Offset: 0x000350B4
		public static bool MetaCmd_START_CHECKSUM()
		{
			return PICkitFunctions.writeUSB(new byte[]
			{
				130,
				PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift,
				0
			});
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000360F8 File Offset: 0x000350F8
		public static bool MetaCmd_CHANGE_CHKSM_FRMT(byte format)
		{
			return PICkitFunctions.writeUSB(new byte[]
			{
				135,
				format,
				0
			});
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00036124 File Offset: 0x00035124
		public static bool MetaCmd_VERIFY_CHECKSUM(uint checksum)
		{
			checksum = ~checksum;
			return PICkitFunctions.writeUSB(new byte[]
			{
				131,
				(byte)(checksum & 255U),
				(byte)(checksum >> 8 & 255U)
			});
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00036162 File Offset: 0x00035162
		public static void ResetPk2Number()
		{
			PICkitFunctions.lastPk2number = 255;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00036170 File Offset: 0x00035170
		public static float MeasurePGDPulse()
		{
			if (PICkitFunctions.writeUSB(new byte[]
			{
				169,
				166,
				9,
				243,
				2,
				232,
				20,
				243,
				6,
				191,
				243,
				3,
				170
			}) && PICkitFunctions.readUSB() && PICkitFunctions.Usb_read_array[1] == 2)
			{
				float num = (float)((int)PICkitFunctions.Usb_read_array[2] + (int)PICkitFunctions.Usb_read_array[3] * 256);
				return num * 0.021333f;
			}
			return 0f;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0003621C File Offset: 0x0003521C
		public static bool EnterUARTMode(uint baudValue)
		{
			return PICkitFunctions.writeUSB(new byte[]
			{
				167,
				169,
				179,
				(byte)(baudValue & 255U),
				(byte)(baudValue >> 8 & 255U)
			});
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00036268 File Offset: 0x00035268
		public static bool ExitUARTMode()
		{
			return PICkitFunctions.writeUSB(new byte[]
			{
				180,
				167,
				169
			});
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0003629C File Offset: 0x0003529C
		public static bool ValidateOSSCAL()
		{
			uint num = PICkitFunctions.DeviceBuffers.OSCCAL;
			num &= 65280U;
			return num != 0U && num == (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[7];
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000362E0 File Offset: 0x000352E0
		public static bool isCalibrated()
		{
			if (PICkitFunctions.writeUSB(new byte[]
			{
				178,
				0,
				4
			}) && PICkitFunctions.readUSB())
			{
				int num = (int)PICkitFunctions.Usb_read_array[1] + (int)PICkitFunctions.Usb_read_array[2] * 256;
				if (num <= 320 && num >= 192)
				{
					return PICkitFunctions.Usb_read_array[1] != 0 || PICkitFunctions.Usb_read_array[2] != 1 || PICkitFunctions.Usb_read_array[3] != 0 || PICkitFunctions.Usb_read_array[4] != 128;
				}
			}
			return false;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00036368 File Offset: 0x00035368
		public static string UnitIDRead()
		{
			string result = "";
			if (PICkitFunctions.writeUSB(new byte[]
			{
				178,
				240,
				16
			}) && PICkitFunctions.readUSB() && PICkitFunctions.Usb_read_array[1] == 35)
			{
				int num = 0;
				while (num < 15 && PICkitFunctions.Usb_read_array[2 + num] != 0)
				{
					num++;
				}
				byte[] array = new byte[num];
				Array.Copy(PICkitFunctions.Usb_read_array, 2, array, 0, num);
				char[] array2 = new char[Encoding.ASCII.GetCharCount(array, 0, array.Length)];
				Encoding.ASCII.GetChars(array, 0, array.Length, array2, 0);
				string text = new string(array2);
				result = text;
			}
			return result;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00036414 File Offset: 0x00035414
		public static bool UnitIDWrite(string unitID)
		{
			int num = unitID.Length;
			if (num > 15)
			{
				num = 15;
			}
			byte[] array = new byte[19];
			array[0] = 177;
			array[1] = 240;
			array[2] = 16;
			byte[] bytes = Encoding.Unicode.GetBytes(unitID);
			byte[] array2 = Encoding.Convert(Encoding.Unicode, Encoding.ASCII, bytes);
			if (num > 0)
			{
				array[3] = 35;
			}
			else
			{
				array[3] = byte.MaxValue;
			}
			for (int i = 0; i < 15; i++)
			{
				if (i < num)
				{
					array[4 + i] = array2[i];
				}
				else
				{
					array[4 + i] = 0;
				}
			}
			return PICkitFunctions.writeUSB(array);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000364AC File Offset: 0x000354AC
		public static bool SetVoltageCals(ushort adcCal, byte vddOffset, byte VddCal)
		{
			return PICkitFunctions.writeUSB(new byte[]
			{
				176,
				(byte)adcCal,
				(byte)(adcCal >> 8),
				vddOffset,
				VddCal
			});
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000364E4 File Offset: 0x000354E4
		public static bool HCS360_361_VppSpecial()
		{
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DeviceID != 4294967094U)
			{
				return true;
			}
			byte[] array = new byte[12];
			array[0] = 166;
			array[1] = 10;
			if ((PICkitFunctions.DeviceBuffers.ProgramMemory[0] & 1U) == 0U)
			{
				array[2] = 243;
				array[3] = 4;
				array[4] = 247;
				array[5] = 250;
				array[6] = 232;
				array[7] = 5;
				array[8] = 243;
				array[9] = 4;
				array[10] = 243;
				array[11] = 0;
			}
			else
			{
				array[2] = 243;
				array[3] = 4;
				array[4] = 246;
				array[5] = 251;
				array[6] = 232;
				array[7] = 5;
				array[8] = 243;
				array[9] = 12;
				array[10] = 243;
				array[11] = 8;
			}
			return PICkitFunctions.writeUSB(array);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000365C8 File Offset: 0x000355C8
		public static bool FamilyIsEEPROM()
		{
			int num = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].FamilyName.Length;
			if (num > 6)
			{
				num = 6;
			}
			return PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].FamilyName.Substring(0, num) == "EEPROM";
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00036624 File Offset: 0x00035624
		public static bool FamilyIsKeeloq()
		{
			return PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].FamilyName == "KEELOQ® HCS";
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0003664C File Offset: 0x0003564C
		public static bool FamilyIsMCP()
		{
			int num = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].FamilyName.Length;
			if (num > 3)
			{
				num = 3;
			}
			return PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].FamilyName.Substring(0, num) == "MCP";
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000366A8 File Offset: 0x000356A8
		public static bool FamilyIsPIC32()
		{
			int num = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].FamilyName.Length;
			if (num > 5)
			{
				num = 5;
			}
			return PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].FamilyName.Substring(0, num) == "PIC32";
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00036704 File Offset: 0x00035704
		public static bool FamilyIsdsPIC30()
		{
			int num = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].FamilyName.Length;
			if (num > 7)
			{
				num = 7;
			}
			return PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].FamilyName.Substring(0, num) == "dsPIC30";
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00036760 File Offset: 0x00035760
		public static bool FamilyIsdsPIC30SMPS()
		{
			int num = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].FamilyName.Length;
			if (num > 9)
			{
				num = 9;
			}
			return PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].FamilyName.Substring(0, num) == "dsPIC30 S";
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000367C0 File Offset: 0x000357C0
		public static bool FamilyIsPIC18J()
		{
			int num = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].FamilyName.Length;
			if (num > 9)
			{
				num = 9;
			}
			return PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].FamilyName.Substring(0, num) == "PIC18F_J_";
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00036820 File Offset: 0x00035820
		public static bool FamilyIsPIC24FJ()
		{
			int num = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].PartName.Length;
			if (num > 7)
			{
				num = 7;
			}
			return PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].PartName.Substring(0, num) == "PIC24FJ";
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0003687C File Offset: 0x0003587C
		public static bool FamilyIsPIC24H()
		{
			int num = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].PartName.Length;
			if (num > 6)
			{
				num = 6;
			}
			return PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].PartName.Substring(0, num) == "PIC24H";
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000368D8 File Offset: 0x000358D8
		public static bool FamilyIsdsPIC33F()
		{
			int num = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].PartName.Length;
			if (num > 8)
			{
				num = 8;
			}
			return PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].PartName.Substring(0, num) == "dsPIC33F";
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00036934 File Offset: 0x00035934
		public static void SetVPPFirstProgramEntry()
		{
			PICkitFunctions.vppFirstEnabled = true;
			PICkitFunctions.scriptBufferChecksum = ~PICkitFunctions.scriptBufferChecksum;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00036947 File Offset: 0x00035947
		public static void ClearVppFirstProgramEntry()
		{
			PICkitFunctions.vppFirstEnabled = false;
			PICkitFunctions.scriptBufferChecksum = ~PICkitFunctions.scriptBufferChecksum;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0003695A File Offset: 0x0003595A
		public static void SetLVPProgramEntry()
		{
			PICkitFunctions.lvpEnabled = true;
			PICkitFunctions.scriptBufferChecksum = ~PICkitFunctions.scriptBufferChecksum;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0003696D File Offset: 0x0003596D
		public static void ClearLVPProgramEntry()
		{
			PICkitFunctions.lvpEnabled = false;
			PICkitFunctions.scriptBufferChecksum = ~PICkitFunctions.scriptBufferChecksum;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00036980 File Offset: 0x00035980
		public static void RowEraseDevice()
		{
			int num = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem / (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DebugRowEraseSize);
			PICkitFunctions.RunScript(0, 1);
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrPrepScript != 0)
			{
				PICkitFunctions.DownloadAddress3(0);
				PICkitFunctions.RunScript(6, 1);
			}
			do
			{
				if (num >= 256)
				{
					PICkitFunctions.RunScript(26, 0);
					num -= 256;
				}
				else
				{
					PICkitFunctions.RunScript(26, num);
					num = 0;
				}
			}
			while (num > 0);
			PICkitFunctions.RunScript(1, 1);
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERowEraseScript > 0)
			{
				int num2 = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem / PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERowEraseWords);
				PICkitFunctions.RunScript(0, 1);
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERdPrepScript != 0)
				{
					PICkitFunctions.DownloadAddress3((int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEAddr / (uint)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemBytesPerWord));
					PICkitFunctions.RunScript(8, 1);
				}
				do
				{
					if (num2 >= 256)
					{
						PICkitFunctions.RunScript(28, 0);
						num2 -= 256;
					}
					else
					{
						PICkitFunctions.RunScript(28, num2);
						num2 = 0;
					}
				}
				while (num2 > 0);
				PICkitFunctions.RunScript(1, 1);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMemEraseScript > 0)
			{
				PICkitFunctions.RunScript(0, 1);
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrPrepScript != 0)
				{
					PICkitFunctions.DownloadAddress3((int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDAddr);
					PICkitFunctions.RunScript(6, 1);
				}
				PICkitFunctions.ExecuteScript((int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMemEraseScript);
				PICkitFunctions.RunScript(1, 1);
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00036B9C File Offset: 0x00035B9C
		public static bool ExecuteScript(int scriptArrayIndex)
		{
			if (scriptArrayIndex == 0)
			{
				return false;
			}
			int scriptLength = (int)PICkitFunctions.DevFile.Scripts[--scriptArrayIndex].ScriptLength;
			byte[] array = new byte[3 + scriptLength];
			array[0] = 169;
			array[1] = 166;
			array[2] = (byte)scriptLength;
			for (int i = 0; i < scriptLength; i++)
			{
				array[3 + i] = (byte)PICkitFunctions.DevFile.Scripts[scriptArrayIndex].Script[i];
			}
			return PICkitFunctions.writeUSB(array);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00036C16 File Offset: 0x00035C16
		public static bool GetVDDState()
		{
			return PICkitFunctions.vddOn;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00036C20 File Offset: 0x00035C20
		public static bool SetMCLRTemp(bool nMCLR)
		{
			byte[] array = new byte[1];
			if (nMCLR)
			{
				array[0] = 247;
			}
			else
			{
				array[0] = 246;
			}
			return PICkitFunctions.SendScript(array);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00036C50 File Offset: 0x00035C50
		public static bool HoldMCLR(bool nMCLR)
		{
			PICkitFunctions.assertMCLR = nMCLR;
			byte[] array = new byte[1];
			if (nMCLR)
			{
				array[0] = 247;
			}
			else
			{
				array[0] = 246;
			}
			return PICkitFunctions.SendScript(array);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00036C85 File Offset: 0x00035C85
		public static void SetFastProgramming(bool fast)
		{
			PICkitFunctions.fastProgramming = fast;
			PICkitFunctions.scriptBufferChecksum = ~PICkitFunctions.scriptBufferChecksum;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00036C98 File Offset: 0x00035C98
		public static void ForcePICkitPowered()
		{
			PICkitFunctions.targetSelfPowered = false;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00036CA0 File Offset: 0x00035CA0
		public static void ForceTargetPowered()
		{
			PICkitFunctions.targetSelfPowered = true;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00036CA8 File Offset: 0x00035CA8
		public static void ReadConfigOutsideProgMem()
		{
			PICkitFunctions.RunScript(0, 1);
			PICkitFunctions.RunScript(13, 1);
			PICkitFunctions.UploadData();
			PICkitFunctions.RunScript(1, 1);
			int configWords = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords;
			int num = 2;
			for (int i = 0; i < configWords; i++)
			{
				uint num2 = (uint)PICkitFunctions.Usb_read_array[num++];
				num2 |= (uint)((uint)PICkitFunctions.Usb_read_array[num++] << 8);
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
				{
					num2 = (num2 >> 1 & PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue);
				}
				PICkitFunctions.DeviceBuffers.ConfigWords[i] = num2;
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00036D60 File Offset: 0x00035D60
		public static void ReadBandGap()
		{
			PICkitFunctions.RunScript(0, 1);
			PICkitFunctions.RunScript(13, 1);
			PICkitFunctions.UploadData();
			PICkitFunctions.RunScript(1, 1);
			byte configWords = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords;
			uint num = (uint)PICkitFunctions.Usb_read_array[2];
			num |= (uint)((uint)PICkitFunctions.Usb_read_array[3] << 8);
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
			{
				num = (num >> 1 & PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue);
			}
			PICkitFunctions.DeviceBuffers.BandGap = (num & PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BandGapMask);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00036E1C File Offset: 0x00035E1C
		public static uint WriteConfigOutsideProgMem(bool codeProtect, bool dataProtect)
		{
			int configWords = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords;
			uint num = 0U;
			byte[] array = new byte[configWords * 2];
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BandGapMask > 0U)
			{
				PICkitFunctions.DeviceBuffers.ConfigWords[0] &= ~PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BandGapMask;
				if (!PICkitFunctions.LearnMode)
				{
					PICkitFunctions.DeviceBuffers.ConfigWords[0] |= PICkitFunctions.DeviceBuffers.BandGap;
				}
			}
			if (PICkitFunctions.FamilyIsMCP())
			{
				PICkitFunctions.DeviceBuffers.ConfigWords[0] |= 16376U;
			}
			PICkitFunctions.RunScript(0, 1);
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWrPrepScript > 0)
			{
				PICkitFunctions.DownloadAddress3(0);
				PICkitFunctions.RunScript(14, 1);
			}
			int i = 0;
			int num2 = 0;
			while (i < configWords)
			{
				uint num3 = PICkitFunctions.DeviceBuffers.ConfigWords[i] & (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[i];
				if (i == (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPConfig - 1))
				{
					if (codeProtect)
					{
						num3 &= (uint)(~(uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPMask);
					}
					if (dataProtect)
					{
						num3 &= (uint)(~(uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DPMask);
					}
				}
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
				{
					num3 |= ((uint)(~(uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[i]) & ~PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BandGapMask);
					if (!PICkitFunctions.FamilyIsMCP())
					{
						num3 &= PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
					}
					num3 <<= 1;
				}
				array[num2++] = (byte)(num3 & 255U);
				array[num2++] = (byte)(num3 >> 8 & 255U);
				num += (uint)((byte)(num3 & 255U));
				num += (uint)((byte)(num3 >> 8 & 255U));
				i++;
			}
			PICkitFunctions.DataClrAndDownload(array, 0);
			if (PICkitFunctions.LearnMode && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BandGapMask > 0U)
			{
				PICkitFunctions.MetaCmd_WRITE_CFG_BANDGAP();
			}
			else
			{
				PICkitFunctions.RunScript(15, 1);
			}
			PICkitFunctions.RunScript(1, 1);
			return num;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000370D4 File Offset: 0x000360D4
		public static bool ReadOSSCAL()
		{
			if (PICkitFunctions.RunScript(0, 1) && PICkitFunctions.DownloadAddress3((int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem - 1U)) && PICkitFunctions.RunScript(20, 1) && PICkitFunctions.UploadData() && PICkitFunctions.RunScript(1, 1))
			{
				PICkitFunctions.DeviceBuffers.OSCCAL = (uint)PICkitFunctions.Usb_read_array[2] + (uint)PICkitFunctions.Usb_read_array[3] * 256U;
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
				{
					PICkitFunctions.DeviceBuffers.OSCCAL >>= 1;
				}
				PICkitFunctions.DeviceBuffers.OSCCAL &= PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
				return true;
			}
			return false;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000371AC File Offset: 0x000361AC
		public static bool WriteOSSCAL()
		{
			if (PICkitFunctions.RunScript(0, 1))
			{
				uint num = PICkitFunctions.DeviceBuffers.OSCCAL;
				uint num2 = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem - 1U;
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
				{
					num <<= 1;
				}
				PICkitFunctions.DataClrAndDownload(new byte[]
				{
					(byte)(num2 & 255U),
					(byte)(num2 >> 8 & 255U),
					(byte)(num2 >> 16 & 255U),
					(byte)(num & 255U),
					(byte)(num >> 8 & 255U)
				}, 0);
				if (PICkitFunctions.RunScript(21, 1) && PICkitFunctions.RunScript(1, 1))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00037270 File Offset: 0x00036270
		public static Constants.PICkit2PWR CheckTargetPower(ref float vdd, ref float vpp)
		{
			if (PICkitFunctions.vddOn)
			{
				return Constants.PICkit2PWR.vdd_on;
			}
			if (!PICkitFunctions.ReadPICkitVoltages(ref vdd, ref vpp))
			{
				PICkitFunctions.targetSelfPowered = false;
				return Constants.PICkit2PWR.no_response;
			}
			if (vdd > 2.3f)
			{
				PICkitFunctions.targetSelfPowered = true;
				PICkitFunctions.SetVDDVoltage(vdd, 0.85f);
				return Constants.PICkit2PWR.selfpowered;
			}
			PICkitFunctions.targetSelfPowered = false;
			return Constants.PICkit2PWR.unpowered;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000372BC File Offset: 0x000362BC
		public static int GetActiveFamily()
		{
			return (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].Family;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000372D7 File Offset: 0x000362D7
		public static void SetActiveFamily(int family)
		{
			PICkitFunctions.ActivePart = 0;
			PICkitFunctions.lastFoundPart = 0;
			PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].Family = (ushort)family;
			PICkitFunctions.ResetBuffers();
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00037308 File Offset: 0x00036308
		public static bool SetVDDVoltage(float voltage, float threshold)
		{
			if (voltage < 2.5f)
			{
				voltage = 2.5f;
			}
			PICkitFunctions.vddLastSet = voltage;
			ushort num = PICkitFunctions.CalculateVddCPP(voltage);
			byte b = (byte)(threshold * voltage / 5f * 255f);
			if (b > 210)
			{
				b = 210;
			}
			return PICkitFunctions.writeUSB(new byte[]
			{
				160,
				(byte)(num & 255),
				(byte)(num / 256),
				b
			});
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00037380 File Offset: 0x00036380
		public static ushort CalculateVddCPP(float voltage)
		{
			ushort num = (ushort)(voltage * 32f + 10.5f);
			return (ushort)(num << 6);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000373A4 File Offset: 0x000363A4
		public static bool VddOn()
		{
			byte[] array = new byte[4];
			array[0] = 166;
			array[1] = 2;
			array[2] = 252;
			if (PICkitFunctions.targetSelfPowered)
			{
				array[3] = 254;
			}
			else
			{
				array[3] = byte.MaxValue;
			}
			bool flag = PICkitFunctions.writeUSB(array);
			if (flag)
			{
				PICkitFunctions.vddOn = true;
				return true;
			}
			return flag;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000373F8 File Offset: 0x000363F8
		public static bool VddOff()
		{
			byte[] array = new byte[4];
			array[0] = 166;
			array[1] = 2;
			array[2] = 254;
			if (PICkitFunctions.targetSelfPowered)
			{
				array[3] = 252;
			}
			else
			{
				array[3] = 253;
			}
			bool flag = PICkitFunctions.writeUSB(array);
			if (flag)
			{
				PICkitFunctions.vddOn = false;
				return true;
			}
			return flag;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0003744C File Offset: 0x0003644C
		public static bool SetProgrammingSpeed(byte speed)
		{
			PICkitFunctions.LastICSPSpeed = speed;
			return PICkitFunctions.writeUSB(new byte[]
			{
				166,
				2,
				234,
				speed
			});
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00037484 File Offset: 0x00036484
		public static bool ResetPICkit2()
		{
			return PICkitFunctions.writeUSB(new byte[]
			{
				174
			});
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000374A8 File Offset: 0x000364A8
		public static bool EnterBootloader()
		{
			return PICkitFunctions.writeUSB(new byte[]
			{
				66
			});
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000374C8 File Offset: 0x000364C8
		public static bool VerifyBootloaderMode()
		{
			return PICkitFunctions.writeUSB(new byte[]
			{
				118
			}) && PICkitFunctions.readUSB() && PICkitFunctions.Usb_read_array[1] == 118;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00037504 File Offset: 0x00036504
		public static bool BL_EraseFlash()
		{
			byte[] array = new byte[]
			{
				3,
				192,
				0,
				32,
				0
			};
			if (PICkitFunctions.writeUSB(array))
			{
				array[3] = 80;
				return PICkitFunctions.writeUSB(array);
			}
			return false;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00037548 File Offset: 0x00036548
		public static bool BL_WriteFlash(byte[] payload)
		{
			byte[] array = new byte[37];
			array[0] = 2;
			array[1] = 32;
			for (int i = 0; i < 35; i++)
			{
				array[2 + i] = payload[i];
			}
			return PICkitFunctions.writeUSB(array);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00037584 File Offset: 0x00036584
		public static bool BL_WriteFWLoadedKey()
		{
			byte[] array = new byte[35];
			array[0] = 224;
			array[1] = 127;
			array[2] = 0;
			for (int i = 3; i < array.Length; i++)
			{
				array[i] = byte.MaxValue;
			}
			array[array.Length - 2] = 85;
			array[array.Length - 1] = 85;
			return PICkitFunctions.BL_WriteFlash(array);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000375D8 File Offset: 0x000365D8
		public static bool BL_ReadFlash16(int address)
		{
			return PICkitFunctions.writeUSB(new byte[]
			{
				1,
				16,
				(byte)(address & 255),
				(byte)(address >> 8 & 255),
				0
			}) && PICkitFunctions.readUSB();
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00037620 File Offset: 0x00036620
		public static bool BL_Reset()
		{
			return PICkitFunctions.writeUSB(new byte[]
			{
				byte.MaxValue
			});
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00037644 File Offset: 0x00036644
		public static bool ButtonPressed()
		{
			ushort num = PICkitFunctions.readPkStatus();
			return (num & 64) == 64;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00037664 File Offset: 0x00036664
		public static bool BusErrorCheck()
		{
			ushort num = PICkitFunctions.readPkStatus();
			if ((num & 1024) == 1024)
			{
				return true;
			}
			PICkitFunctions.writeUSB(new byte[]
			{
				166,
				1,
				245
			});
			return false;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000376AC File Offset: 0x000366AC
		public static Constants.PICkit2PWR PowerStatus()
		{
			ushort num = PICkitFunctions.readPkStatus();
			if (num == 65535)
			{
				return Constants.PICkit2PWR.no_response;
			}
			if ((num & 48) == 48)
			{
				PICkitFunctions.vddOn = false;
				return Constants.PICkit2PWR.vddvpperrors;
			}
			if ((num & 32) == 32)
			{
				PICkitFunctions.vddOn = false;
				return Constants.PICkit2PWR.vpperror;
			}
			if ((num & 16) == 16)
			{
				PICkitFunctions.vddOn = false;
				return Constants.PICkit2PWR.vdderror;
			}
			if ((num & 2) == 2)
			{
				PICkitFunctions.vddOn = true;
				return Constants.PICkit2PWR.vdd_on;
			}
			PICkitFunctions.vddOn = false;
			return Constants.PICkit2PWR.vdd_off;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00037710 File Offset: 0x00036710
		public static void DisconnectPICkit2Unit()
		{
			if (PICkitFunctions.usbWriteHandle != IntPtr.Zero)
			{
				USB.CloseHandle(PICkitFunctions.usbWriteHandle);
			}
			if (PICkitFunctions.usbReadHandle != IntPtr.Zero)
			{
				USB.CloseHandle(PICkitFunctions.usbReadHandle);
			}
			PICkitFunctions.usbReadHandle = IntPtr.Zero;
			PICkitFunctions.usbWriteHandle = IntPtr.Zero;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00037769 File Offset: 0x00036769
		public static string GetSerialUnitID()
		{
			return USB.UnitID;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00037770 File Offset: 0x00036770
		public static Constants.PICkit2USB DetectPICkit2Device(ushort pk2ID, bool readFW)
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			PICkitFunctions.DisconnectPICkit2Unit();
			bool flag = USB.Find_This_Device(1240, 51, pk2ID, ref zero, ref zero2);
			PICkitFunctions.lastPk2number = pk2ID;
			PICkitFunctions.usbReadHandle = zero;
			PICkitFunctions.usbWriteHandle = zero2;
			if (flag && !readFW)
			{
				return Constants.PICkit2USB.found;
			}
			if (!flag)
			{
				return Constants.PICkit2USB.notFound;
			}
			flag = PICkitFunctions.writeUSB(new byte[]
			{
				118
			});
			if (!flag)
			{
				return Constants.PICkit2USB.writeError;
			}
			if (!PICkitFunctions.readUSB())
			{
				return Constants.PICkit2USB.readError;
			}
			PICkitFunctions.FirmwareVersion = string.Format("{0:D1}.{1:D2}.{2:D2}", PICkitFunctions.Usb_read_array[1], PICkitFunctions.Usb_read_array[2], PICkitFunctions.Usb_read_array[3]);
			if (PICkitFunctions.Usb_read_array[1] == 2 && ((PICkitFunctions.Usb_read_array[2] == 32 && PICkitFunctions.Usb_read_array[3] >= 0) || PICkitFunctions.Usb_read_array[2] > 32))
			{
				return Constants.PICkit2USB.found;
			}
			if (PICkitFunctions.Usb_read_array[1] == 118)
			{
				PICkitFunctions.FirmwareVersion = string.Format("BL {0:D1}.{1:D1}", PICkitFunctions.Usb_read_array[7], PICkitFunctions.Usb_read_array[8]);
				return Constants.PICkit2USB.bootloader;
			}
			return Constants.PICkit2USB.firmwareInvalid;
		}

		public static bool ReadDeviceFile(string DeviceFileName)
		{
			DeviceFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DeviceFileName);
			bool flag = File.Exists(DeviceFileName);
			if (flag)
			{
				try
				{
					FileStream fileStream = File.OpenRead(DeviceFileName);
					using (BinaryReader binaryReader = new BinaryReader(fileStream))
					{
						DevFile.Info.VersionMajor = binaryReader.ReadInt32();
						DevFile.Info.VersionMinor = binaryReader.ReadInt32();
						DevFile.Info.VersionDot = binaryReader.ReadInt32();
						DevFile.Info.VersionNotes = binaryReader.ReadString();
						DevFile.Info.NumberFamilies = binaryReader.ReadInt32();
						DevFile.Info.NumberParts = binaryReader.ReadInt32();
						DevFile.Info.NumberScripts = binaryReader.ReadInt32();
						DevFile.Info.Compatibility = binaryReader.ReadByte();
						DevFile.Info.UNUSED1A = binaryReader.ReadByte();
						DevFile.Info.UNUSED1B = binaryReader.ReadUInt16();
						DevFile.Info.UNUSED2 = binaryReader.ReadUInt32();
						DeviceFileVersion = string.Format("{0:D1}.{1:D2}.{2:D2}", DevFile.Info.VersionMajor, DevFile.Info.VersionMinor, DevFile.Info.VersionDot);
						DevFile.Families = new DeviceFile.DeviceFamilyParams[DevFile.Info.NumberFamilies];
						DevFile.PartsList = new DeviceFile.DevicePartParams[DevFile.Info.NumberParts];
						DevFile.Scripts = new DeviceFile.DeviceScripts[DevFile.Info.NumberScripts];
						for (int i = 0; i < DevFile.Info.NumberFamilies; i++)
						{
							PICkitFunctions.DevFile.Families[i].FamilyID = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.Families[i].FamilyType = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.Families[i].SearchPriority = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.Families[i].FamilyName = binaryReader.ReadString();
							PICkitFunctions.DevFile.Families[i].ProgEntryScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.Families[i].ProgExitScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.Families[i].ReadDevIDScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.Families[i].DeviceIDMask = binaryReader.ReadUInt32();
							PICkitFunctions.DevFile.Families[i].BlankValue = binaryReader.ReadUInt32();
							PICkitFunctions.DevFile.Families[i].BytesPerLocation = binaryReader.ReadByte();
							PICkitFunctions.DevFile.Families[i].AddressIncrement = binaryReader.ReadByte();
							PICkitFunctions.DevFile.Families[i].PartDetect = binaryReader.ReadBoolean();
							PICkitFunctions.DevFile.Families[i].ProgEntryVPPScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.Families[i].UNUSED1 = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.Families[i].EEMemBytesPerWord = binaryReader.ReadByte();
							PICkitFunctions.DevFile.Families[i].EEMemAddressIncrement = binaryReader.ReadByte();
							PICkitFunctions.DevFile.Families[i].UserIDHexBytes = binaryReader.ReadByte();
							PICkitFunctions.DevFile.Families[i].UserIDBytes = binaryReader.ReadByte();
							PICkitFunctions.DevFile.Families[i].ProgMemHexBytes = binaryReader.ReadByte();
							PICkitFunctions.DevFile.Families[i].EEMemHexBytes = binaryReader.ReadByte();
							PICkitFunctions.DevFile.Families[i].ProgMemShift = binaryReader.ReadByte();
							PICkitFunctions.DevFile.Families[i].TestMemoryStart = binaryReader.ReadUInt32();
							PICkitFunctions.DevFile.Families[i].TestMemoryLength = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.Families[i].Vpp = binaryReader.ReadSingle();
						}
						PICkitFunctions.familySearchTable = new int[PICkitFunctions.DevFile.Info.NumberFamilies];
						for (int j = 0; j < PICkitFunctions.DevFile.Info.NumberFamilies; j++)
						{
							PICkitFunctions.familySearchTable[(int)PICkitFunctions.DevFile.Families[j].SearchPriority] = j;
						}
						for (int k = 0; k < PICkitFunctions.DevFile.Info.NumberParts; k++)
						{
							PICkitFunctions.DevFile.PartsList[k].PartName = binaryReader.ReadString();
							PICkitFunctions.DevFile.PartsList[k].Family = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].DeviceID = binaryReader.ReadUInt32();
							PICkitFunctions.DevFile.PartsList[k].ProgramMem = binaryReader.ReadUInt32();
							PICkitFunctions.DevFile.PartsList[k].EEMem = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].EEAddr = binaryReader.ReadUInt32();
							PICkitFunctions.DevFile.PartsList[k].ConfigWords = binaryReader.ReadByte();
							PICkitFunctions.DevFile.PartsList[k].ConfigAddr = binaryReader.ReadUInt32();
							PICkitFunctions.DevFile.PartsList[k].UserIDWords = binaryReader.ReadByte();
							PICkitFunctions.DevFile.PartsList[k].UserIDAddr = binaryReader.ReadUInt32();
							PICkitFunctions.DevFile.PartsList[k].BandGapMask = binaryReader.ReadUInt32();
							PICkitFunctions.DevFile.PartsList[k].ConfigMasks = new ushort[9];
							PICkitFunctions.DevFile.PartsList[k].ConfigBlank = new ushort[9];
							for (int l = 0; l < 8; l++)
							{
								PICkitFunctions.DevFile.PartsList[k].ConfigMasks[l] = binaryReader.ReadUInt16();
							}
							for (int m = 0; m < 8; m++)
							{
								PICkitFunctions.DevFile.PartsList[k].ConfigBlank[m] = binaryReader.ReadUInt16();
							}
							PICkitFunctions.DevFile.PartsList[k].CPMask = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].CPConfig = binaryReader.ReadByte();
							PICkitFunctions.DevFile.PartsList[k].OSSCALSave = binaryReader.ReadBoolean();
							PICkitFunctions.DevFile.PartsList[k].IgnoreAddress = binaryReader.ReadUInt32();
							PICkitFunctions.DevFile.PartsList[k].VddMin = binaryReader.ReadSingle();
							PICkitFunctions.DevFile.PartsList[k].VddMax = binaryReader.ReadSingle();
							PICkitFunctions.DevFile.PartsList[k].VddErase = binaryReader.ReadSingle();
							PICkitFunctions.DevFile.PartsList[k].CalibrationWords = binaryReader.ReadByte();
							PICkitFunctions.DevFile.PartsList[k].ChipEraseScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].ProgMemAddrSetScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].ProgMemAddrBytes = binaryReader.ReadByte();
							PICkitFunctions.DevFile.PartsList[k].ProgMemRdScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].ProgMemRdWords = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].EERdPrepScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].EERdScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].EERdLocations = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].UserIDRdPrepScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].UserIDRdScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].ConfigRdPrepScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].ConfigRdScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].ProgMemWrPrepScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].ProgMemWrScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].ProgMemWrWords = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].ProgMemPanelBufs = binaryReader.ReadByte();
							PICkitFunctions.DevFile.PartsList[k].ProgMemPanelOffset = binaryReader.ReadUInt32();
							PICkitFunctions.DevFile.PartsList[k].EEWrPrepScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].EEWrScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].EEWrLocations = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].UserIDWrPrepScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].UserIDWrScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].ConfigWrPrepScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].ConfigWrScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].OSCCALRdScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].OSCCALWrScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].DPMask = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].WriteCfgOnErase = binaryReader.ReadBoolean();
							PICkitFunctions.DevFile.PartsList[k].BlankCheckSkipUsrIDs = binaryReader.ReadBoolean();
							PICkitFunctions.DevFile.PartsList[k].IgnoreBytes = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].ChipErasePrepScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].BootFlash = binaryReader.ReadUInt32();
							PICkitFunctions.DevFile.PartsList[k].Config9Mask = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].ConfigMasks[8] = PICkitFunctions.DevFile.PartsList[k].Config9Mask;
							PICkitFunctions.DevFile.PartsList[k].Config9Blank = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].ConfigBlank[8] = PICkitFunctions.DevFile.PartsList[k].Config9Blank;
							PICkitFunctions.DevFile.PartsList[k].ProgMemEraseScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].EEMemEraseScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].ConfigMemEraseScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].reserved1EraseScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].reserved2EraseScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].TestMemoryRdScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].TestMemoryRdWords = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].EERowEraseScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].EERowEraseWords = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].ExportToMPLAB = binaryReader.ReadBoolean();
							PICkitFunctions.DevFile.PartsList[k].DebugHaltScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].DebugRunScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].DebugStatusScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].DebugReadExecVerScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].DebugSingleStepScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].DebugBulkWrDataScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].DebugBulkRdDataScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].DebugWriteVectorScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].DebugReadVectorScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].DebugRowEraseScript = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].DebugRowEraseSize = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].DebugReserved5Script = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].DebugReserved6Script = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].DebugReserved7Script = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].DebugReserved8Script = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.PartsList[k].LVPScript = binaryReader.ReadUInt16();
						}
						for (int n = 0; n < PICkitFunctions.DevFile.Info.NumberScripts; n++)
						{
							PICkitFunctions.DevFile.Scripts[n].ScriptNumber = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.Scripts[n].ScriptName = binaryReader.ReadString();
							PICkitFunctions.DevFile.Scripts[n].ScriptVersion = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.Scripts[n].UNUSED1 = binaryReader.ReadUInt32();
							PICkitFunctions.DevFile.Scripts[n].ScriptLength = binaryReader.ReadUInt16();
							PICkitFunctions.DevFile.Scripts[n].Script = new ushort[(int)PICkitFunctions.DevFile.Scripts[n].ScriptLength];
							for (int num = 0; num < (int)PICkitFunctions.DevFile.Scripts[n].ScriptLength; num++)
							{
								PICkitFunctions.DevFile.Scripts[n].Script[num] = binaryReader.ReadUInt16();
							}
							PICkitFunctions.DevFile.Scripts[n].Comment = binaryReader.ReadString();
						}
						binaryReader.Close();
					}
					fileStream.Close();
				}
				catch
				{
					return false;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00038888 File Offset: 0x00037888
		public static bool DetectDevice(int familyIndex, bool resetOnNotFound, bool keepVddOn)
		{
			if (familyIndex == 16777215)
			{
				if (!PICkitFunctions.targetSelfPowered)
				{
					PICkitFunctions.SetVDDVoltage(3.3f, 0.85f);
				}
				for (int i = 0; i < PICkitFunctions.DevFile.Families.Length; i++)
				{
					if (PICkitFunctions.DevFile.Families[PICkitFunctions.familySearchTable[i]].PartDetect && PICkitFunctions.searchDevice(PICkitFunctions.familySearchTable[i], true, keepVddOn))
					{
						return true;
					}
				}
				return false;
			}
			PICkitFunctions.SetVDDVoltage(PICkitFunctions.vddLastSet, 0.85f);
			return !PICkitFunctions.DevFile.Families[familyIndex].PartDetect || PICkitFunctions.searchDevice(familyIndex, resetOnNotFound, keepVddOn);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00038934 File Offset: 0x00037934
		public static int FindLastUsedInBuffer(uint[] bufferToSearch, uint blankValue, int startIndex)
		{
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].FamilyName != "KEELOQ® HCS")
			{
				for (int i = startIndex; i > 0; i--)
				{
					if (bufferToSearch[i] != blankValue)
					{
						return i;
					}
				}
				return 0;
			}
			return bufferToSearch.Length - 1;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00038984 File Offset: 0x00037984
		public static bool RunScriptUploadNoLen(int script, int repetitions)
		{
			bool flag = PICkitFunctions.writeUSB(new byte[]
			{
				169,
				165,
				PICkitFunctions.scriptRedirectTable[script].redirectToScriptLocation,
				(byte)repetitions,
				172
			});
			if (flag)
			{
				flag = PICkitFunctions.readUSB();
			}
			return flag;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000389D9 File Offset: 0x000379D9
		public static bool GetUpload()
		{
			return PICkitFunctions.readUSB();
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000389E0 File Offset: 0x000379E0
		public static bool UploadData()
		{
			bool flag = PICkitFunctions.writeUSB(new byte[]
			{
				170
			});
			if (flag)
			{
				flag = PICkitFunctions.readUSB();
			}
			return flag;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00038A10 File Offset: 0x00037A10
		public static bool UploadDataNoLen()
		{
			bool flag = PICkitFunctions.writeUSB(new byte[]
			{
				172
			});
			if (flag)
			{
				flag = PICkitFunctions.readUSB();
			}
			return flag;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00038A40 File Offset: 0x00037A40
		public static bool RunScript(int script, int repetitions)
		{
			return PICkitFunctions.writeUSB(new byte[]
			{
				169,
				165,
				PICkitFunctions.scriptRedirectTable[script].redirectToScriptLocation,
				(byte)repetitions
			}) && (script != 1 || PICkitFunctions.assertMCLR || PICkitFunctions.HoldMCLR(false));
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00038A9C File Offset: 0x00037A9C
		public static int DataClrAndDownload(byte[] dataArray, int startIndex)
		{
			if (startIndex >= dataArray.Length)
			{
				return 0;
			}
			int num = dataArray.Length - startIndex;
			if (num > 61)
			{
				num = 61;
			}
			byte[] array = new byte[3 + num];
			array[0] = 167;
			array[1] = 168;
			array[2] = (byte)(num & 255);
			for (int i = 0; i < num; i++)
			{
				array[3 + i] = dataArray[startIndex + i];
			}
			if (PICkitFunctions.writeUSB(array))
			{
				return startIndex + num;
			}
			return 0;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00038B08 File Offset: 0x00037B08
		public static int DataDownload(byte[] dataArray, int startIndex, int lastIndex)
		{
			if (startIndex >= lastIndex)
			{
				return 0;
			}
			int num = lastIndex - startIndex;
			if (num > 62)
			{
				num = 62;
			}
			byte[] array = new byte[2 + num];
			array[0] = 168;
			array[1] = (byte)(num & 255);
			for (int i = 0; i < num; i++)
			{
				array[2 + i] = dataArray[startIndex + i];
			}
			if (PICkitFunctions.writeUSB(array))
			{
				return startIndex + num;
			}
			return 0;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00038B68 File Offset: 0x00037B68
		public static bool DownloadAddress3(int address)
		{
			return PICkitFunctions.writeUSB(new byte[]
			{
				167,
				168,
				3,
				(byte)(address & 255),
				(byte)(255 & address >> 8),
				(byte)(255 & address >> 16)
			});
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00038BBC File Offset: 0x00037BBC
		public static bool DownloadAddress3MSBFirst(int address)
		{
			return PICkitFunctions.writeUSB(new byte[]
			{
				167,
				168,
				3,
				(byte)(255 & address >> 16),
				(byte)(255 & address >> 8),
				(byte)(address & 255)
			});
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00038C10 File Offset: 0x00037C10
		public static bool Download3Multiples(int downloadBytes, int multiples, int increment)
		{
			byte b = 167;
			for (;;)
			{
				int num = multiples;
				if (multiples > 20)
				{
					num = 20;
					multiples -= 20;
				}
				else
				{
					multiples = 0;
				}
				byte[] array = new byte[3 * num + 3];
				array[0] = b;
				array[1] = 168;
				array[2] = (byte)(3 * num);
				for (int i = 0; i < num; i++)
				{
					array[3 + 3 * i] = (byte)(downloadBytes >> 16);
					array[4 + 3 * i] = (byte)(downloadBytes >> 8);
					array[5 + 3 * i] = (byte)downloadBytes;
					downloadBytes += increment;
				}
				if (!PICkitFunctions.writeUSB(array))
				{
					break;
				}
				b = 90;
				if (multiples <= 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00038C9C File Offset: 0x00037C9C
		public static uint ComputeChecksum(bool codeProtectOn, bool dataProtectOn)
		{
			uint num = 0U;
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue < 65535U)
			{
				int num2 = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem;
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].OSSCALSave)
				{
					num2--;
				}
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords > 0 && (((uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPMask & PICkitFunctions.DeviceBuffers.ConfigWords[(int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPConfig - 1)]) != (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPMask || codeProtectOn))
				{
					if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue < 16383U)
					{
						num2 = 64;
					}
					else
					{
						num2 = 0;
					}
				}
				for (int i = 0; i < num2; i++)
				{
					num += PICkitFunctions.DeviceBuffers.ProgramMemory[i];
				}
				if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords > 0)
				{
					if (((uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPMask & PICkitFunctions.DeviceBuffers.ConfigWords[(int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPConfig - 1)]) != (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPMask || codeProtectOn)
					{
						for (int j = 0; j < (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords; j++)
						{
							int num3 = 1;
							for (int k = 0; k < j; k++)
							{
								num3 <<= 4;
							}
							num += (uint)((ulong)(15U & PICkitFunctions.DeviceBuffers.UserIDs[(int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords - j - 1]) * (ulong)((long)num3));
						}
					}
					for (int l = 0; l < (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords; l++)
					{
						if (l == (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPConfig - 1))
						{
							uint num4 = PICkitFunctions.DeviceBuffers.ConfigWords[l] & (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[l];
							if (codeProtectOn)
							{
								num4 &= (uint)(~(uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPMask);
							}
							if (dataProtectOn)
							{
								num4 &= (uint)(~(uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DPMask);
							}
							num += num4;
						}
						else
						{
							num += (PICkitFunctions.DeviceBuffers.ConfigWords[l] & (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[l]);
						}
					}
				}
				return num & 65535U;
			}
			int num5 = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr / (uint)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemHexBytes);
			if ((long)num5 > (long)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem))
			{
				num5 = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem;
			}
			for (int m = 0; m < num5; m++)
			{
				uint num6 = PICkitFunctions.DeviceBuffers.ProgramMemory[m];
				num += (num6 & 255U);
				for (int n = 1; n < (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BytesPerLocation; n++)
				{
					num6 >>= 8;
					num += (num6 & 255U);
				}
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords > 0)
			{
				if (((uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPMask & PICkitFunctions.DeviceBuffers.ConfigWords[(int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPConfig - 1)]) != (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].CPMask || codeProtectOn)
				{
					num = 0U;
					for (int num7 = 0; num7 < (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords; num7++)
					{
						uint num8 = PICkitFunctions.DeviceBuffers.UserIDs[num7];
						num += (num8 & 255U);
						num += (num8 >> 8 & 255U);
					}
				}
				for (int num9 = 0; num9 < (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords; num9++)
				{
					uint num10 = PICkitFunctions.DeviceBuffers.ConfigWords[num9] & (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[num9];
					num += (num10 & 255U);
					num += (num10 >> 8 & 255U);
				}
			}
			return num & 65535U;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000391C4 File Offset: 0x000381C4
		public static void ResetBuffers()
		{
			PICkitFunctions.DeviceBuffers = new DeviceData(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem, PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem, PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords, PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords, PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue, (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemAddressIncrement, (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].UserIDBytes, PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank, (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[7]);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000392C0 File Offset: 0x000382C0
		public static DeviceData CloneBuffers(DeviceData copyFrom)
		{
			DeviceData deviceData = new DeviceData(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem, PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem, PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords, PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords, PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue, (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemAddressIncrement, (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].UserIDBytes, PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank, (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[7]);
			for (int i = 0; i < copyFrom.ProgramMemory.Length; i++)
			{
				deviceData.ProgramMemory[i] = copyFrom.ProgramMemory[i];
			}
			for (int j = 0; j < copyFrom.EEPromMemory.Length; j++)
			{
				deviceData.EEPromMemory[j] = copyFrom.EEPromMemory[j];
			}
			for (int k = 0; k < copyFrom.ConfigWords.Length; k++)
			{
				deviceData.ConfigWords[k] = copyFrom.ConfigWords[k];
			}
			for (int l = 0; l < copyFrom.UserIDs.Length; l++)
			{
				deviceData.UserIDs[l] = copyFrom.UserIDs[l];
			}
			deviceData.OSCCAL = copyFrom.OSCCAL;
			deviceData.OSCCAL = copyFrom.BandGap;
			return deviceData;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00039464 File Offset: 0x00038464
		public static void PrepNewPart(bool resetBuffers)
		{
			if (resetBuffers)
			{
				PICkitFunctions.ResetBuffers();
			}
			float num = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].Vpp;
			if (num < 1f || (PICkitFunctions.lvpEnabled && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript > 0))
			{
				if (PICkitFunctions.lvpEnabled && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript > 0)
				{
					string text = PICkitFunctions.DevFile.Scripts[(int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript - 1)].ScriptName;
					text = text.Substring(text.Length - 2);
					if (text == "HV")
					{
						num = (float)PICkitFunctions.DevFile.Scripts[(int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript - 1)].Script[1] / 10f;
						PICkitFunctions.SetVppVoltage(num, 0.7f);
					}
					else
					{
						PICkitFunctions.SetVppVoltage(PICkitFunctions.vddLastSet, 0.7f);
					}
				}
				else
				{
					PICkitFunctions.SetVppVoltage(PICkitFunctions.vddLastSet, 0.7f);
				}
			}
			else
			{
				PICkitFunctions.SetVppVoltage(num, 0.7f);
			}
			PICkitFunctions.downloadPartScripts(PICkitFunctions.GetActiveFamily());
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000395BC File Offset: 0x000385BC
		public static uint ReadDebugVector()
		{
			PICkitFunctions.RunScript(0, 1);
			PICkitFunctions.ExecuteScript((int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DebugReadVectorScript);
			PICkitFunctions.UploadData();
			PICkitFunctions.RunScript(1, 1);
			int num = 2;
			int num2 = 2;
			uint num3 = 0U;
			for (int i = 0; i < num; i++)
			{
				uint num4 = (uint)PICkitFunctions.Usb_read_array[num2++];
				num4 |= (uint)((uint)PICkitFunctions.Usb_read_array[num2++] << 8);
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
				{
					num4 = (num4 >> 1 & PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue);
				}
				if (i == 0)
				{
					num3 = num4;
				}
				else
				{
					num3 += num4 << 16;
				}
			}
			return num3;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0003967C File Offset: 0x0003867C
		public static void WriteDebugVector(uint debugWords)
		{
			int num = 2;
			byte[] array = new byte[4];
			PICkitFunctions.RunScript(0, 1);
			int i = 0;
			int num2 = 0;
			while (i < num)
			{
				uint num3;
				if (i == 0)
				{
					num3 = (debugWords & 65535U);
				}
				else
				{
					num3 = debugWords >> 16;
				}
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift > 0)
				{
					num3 <<= 1;
				}
				array[num2++] = (byte)(num3 & 255U);
				array[num2++] = (byte)(num3 >> 8 & 255U);
				i++;
			}
			PICkitFunctions.DataClrAndDownload(array, 0);
			PICkitFunctions.ExecuteScript((int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DebugWriteVectorScript);
			PICkitFunctions.RunScript(1, 1);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00039734 File Offset: 0x00038734
		public static bool ReadPICkitVoltages(ref float vdd, ref float vpp)
		{
			if (PICkitFunctions.writeUSB(new byte[]
			{
				163
			}) && PICkitFunctions.readUSB())
			{
				float num = (float)((int)PICkitFunctions.Usb_read_array[2] * 256 + (int)PICkitFunctions.Usb_read_array[1]);
				vdd = num / 65536f * 5f;
				num = (float)((int)PICkitFunctions.Usb_read_array[4] * 256 + (int)PICkitFunctions.Usb_read_array[3]);
				vpp = num / 65536f * 13.7f;
				return true;
			}
			return false;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000397B0 File Offset: 0x000387B0
		public static bool SetVppVoltage(float voltage, float threshold)
		{
			byte b = 64;
			byte b2 = (byte)(voltage * 18.61f);
			byte b3 = (byte)(threshold * voltage * 18.61f);
			return PICkitFunctions.writeUSB(new byte[]
			{
				161,
				b,
				b2,
				b3
			});
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000397F8 File Offset: 0x000387F8
		public static bool SendScript(byte[] script)
		{
			int num = script.Length;
			byte[] array = new byte[2 + num];
			array[0] = 166;
			array[1] = (byte)num;
			for (int i = 0; i < num; i++)
			{
				array[2 + i] = script[i];
			}
			return PICkitFunctions.writeUSB(array);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0003983C File Offset: 0x0003883C
		private static ushort readPkStatus()
		{
			if (!PICkitFunctions.writeUSB(new byte[]
			{
				162
			}))
			{
				return ushort.MaxValue;
			}
			if (PICkitFunctions.readUSB())
			{
				return (ushort)((int)PICkitFunctions.Usb_read_array[2] * 256 + (int)PICkitFunctions.Usb_read_array[1]);
			}
			return ushort.MaxValue;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0003988C File Offset: 0x0003888C
		public static bool writeUSB(byte[] commandList)
		{
			int num = 0;
			PICkitFunctions.Usb_write_array[0] = 0;
			for (int i = 1; i < PICkitFunctions.Usb_write_array.Length; i++)
			{
				PICkitFunctions.Usb_write_array[i] = 173;
			}
			Array.Copy(commandList, 0, PICkitFunctions.Usb_write_array, 1, commandList.Length);
			bool flag = USB.WriteFile(PICkitFunctions.usbWriteHandle, PICkitFunctions.Usb_write_array, PICkitFunctions.Usb_write_array.Length, ref num, 0);
			return num == PICkitFunctions.Usb_write_array.Length && flag;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000398F8 File Offset: 0x000388F8
		public static bool readUSB()
		{
			int num = 0;
			if (PICkitFunctions.LearnMode)
			{
				return true;
			}
			bool flag = USB.ReadFile(PICkitFunctions.usbReadHandle, PICkitFunctions.Usb_read_array, PICkitFunctions.Usb_read_array.Length, ref num, 0);
			return num == PICkitFunctions.Usb_read_array.Length && flag;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00039938 File Offset: 0x00038938
		public static bool VerifyDeviceID(bool resetOnNoDevice, bool keepVddOn)
		{
			float num = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].Vpp;
			if (num < 1f || (PICkitFunctions.lvpEnabled && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript > 0))
			{
				if (PICkitFunctions.lvpEnabled && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript > 0)
				{
					string text = PICkitFunctions.DevFile.Scripts[(int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript - 1)].ScriptName;
					text = text.Substring(text.Length - 2);
					if (text == "HV")
					{
						num = (float)PICkitFunctions.DevFile.Scripts[(int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript - 1)].Script[1] / 10f;
						PICkitFunctions.SetVppVoltage(num, 0.7f);
					}
					else
					{
						PICkitFunctions.SetVppVoltage(PICkitFunctions.vddLastSet, 0.7f);
					}
				}
				else
				{
					PICkitFunctions.SetVppVoltage(PICkitFunctions.vddLastSet, 0.7f);
				}
			}
			else
			{
				PICkitFunctions.SetVppVoltage(num, 0.7f);
			}
			PICkitFunctions.SetMCLRTemp(true);
			PICkitFunctions.VddOn();
			if (PICkitFunctions.lvpEnabled && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript > 0)
			{
				PICkitFunctions.ExecuteScript((int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript);
			}
			else if (PICkitFunctions.vppFirstEnabled && PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgEntryVPPScript > 0)
			{
				PICkitFunctions.ExecuteScript((int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgEntryVPPScript);
			}
			else
			{
				PICkitFunctions.ExecuteScript((int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgEntryScript);
			}
			PICkitFunctions.ExecuteScript((int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ReadDevIDScript);
			PICkitFunctions.UploadData();
			PICkitFunctions.ExecuteScript((int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgExitScript);
			if (!keepVddOn)
			{
				PICkitFunctions.VddOff();
			}
			if (!PICkitFunctions.assertMCLR)
			{
				PICkitFunctions.HoldMCLR(false);
			}
			uint num2 = (uint)PICkitFunctions.Usb_read_array[5] * 16777216U + (uint)PICkitFunctions.Usb_read_array[4] * 65536U + (uint)PICkitFunctions.Usb_read_array[3] * 256U + (uint)PICkitFunctions.Usb_read_array[2];
			for (int i = 0; i < (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemShift; i++)
			{
				num2 >>= 1;
			}
			if (PICkitFunctions.Usb_read_array[1] == 4)
			{
				PICkitFunctions.LastDeviceRev = (int)PICkitFunctions.Usb_read_array[5] * 256 + (int)PICkitFunctions.Usb_read_array[4];
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue == 4294967295U)
				{
					PICkitFunctions.LastDeviceRev >>= 4;
				}
			}
			else
			{
				PICkitFunctions.LastDeviceRev = (int)(num2 & ~(int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].DeviceIDMask);
			}
			PICkitFunctions.LastDeviceRev &= 65535;
			PICkitFunctions.LastDeviceRev &= (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
			num2 &= PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].DeviceIDMask;
			PICkitFunctions.LastDeviceID = num2;
			if (num2 != PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DeviceID)
			{
				return false;
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].OSSCALSave)
			{
				PICkitFunctions.VddOn();
				PICkitFunctions.ReadOSSCAL();
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BandGapMask > 0U)
			{
				PICkitFunctions.VddOn();
				PICkitFunctions.ReadBandGap();
			}
			if (!keepVddOn)
			{
				PICkitFunctions.VddOff();
			}
			return true;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00039D20 File Offset: 0x00038D20
		private static bool searchDevice(int familyIndex, bool resetOnNoDevice, bool keepVddOn)
		{
			int activePart = PICkitFunctions.ActivePart;
			if (PICkitFunctions.ActivePart != 0)
			{
				PICkitFunctions.lastFoundPart = PICkitFunctions.ActivePart;
			}
			float num = PICkitFunctions.DevFile.Families[familyIndex].Vpp;
			if (num < 1f || (PICkitFunctions.lvpEnabled && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript > 0))
			{
				if (PICkitFunctions.lvpEnabled && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript > 0)
				{
					string text = PICkitFunctions.DevFile.Scripts[(int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript - 1)].ScriptName;
					text = text.Substring(text.Length - 2);
					if (text == "HV")
					{
						num = (float)PICkitFunctions.DevFile.Scripts[(int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript - 1)].Script[1] / 10f;
						PICkitFunctions.SetVppVoltage(num, 0.7f);
					}
					else
					{
						PICkitFunctions.SetVppVoltage(PICkitFunctions.vddLastSet, 0.7f);
					}
				}
				else
				{
					PICkitFunctions.SetVppVoltage(PICkitFunctions.vddLastSet, 0.7f);
				}
			}
			else
			{
				PICkitFunctions.SetVppVoltage(num, 0.7f);
			}
			PICkitFunctions.SetMCLRTemp(true);
			PICkitFunctions.VddOn();
			if (PICkitFunctions.lvpEnabled && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript > 0)
			{
				PICkitFunctions.ExecuteScript((int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript);
			}
			else if (PICkitFunctions.vppFirstEnabled && PICkitFunctions.DevFile.Families[familyIndex].ProgEntryVPPScript > 0)
			{
				PICkitFunctions.ExecuteScript((int)PICkitFunctions.DevFile.Families[familyIndex].ProgEntryVPPScript);
			}
			else
			{
				PICkitFunctions.ExecuteScript((int)PICkitFunctions.DevFile.Families[familyIndex].ProgEntryScript);
			}
			PICkitFunctions.ExecuteScript((int)PICkitFunctions.DevFile.Families[familyIndex].ReadDevIDScript);
			PICkitFunctions.UploadData();
			PICkitFunctions.ExecuteScript((int)PICkitFunctions.DevFile.Families[familyIndex].ProgExitScript);
			if (!keepVddOn)
			{
				PICkitFunctions.VddOff();
			}
			if (!PICkitFunctions.assertMCLR)
			{
				PICkitFunctions.HoldMCLR(false);
			}
			uint num2 = (uint)PICkitFunctions.Usb_read_array[5] * 16777216U + (uint)PICkitFunctions.Usb_read_array[4] * 65536U + (uint)PICkitFunctions.Usb_read_array[3] * 256U + (uint)PICkitFunctions.Usb_read_array[2];
			for (int i = 0; i < (int)PICkitFunctions.DevFile.Families[familyIndex].ProgMemShift; i++)
			{
				num2 >>= 1;
			}
			if (PICkitFunctions.Usb_read_array[1] == 4)
			{
				PICkitFunctions.LastDeviceRev = (int)PICkitFunctions.Usb_read_array[5] * 256 + (int)PICkitFunctions.Usb_read_array[4];
				if (PICkitFunctions.DevFile.Families[familyIndex].BlankValue == 4294967295U)
				{
					PICkitFunctions.LastDeviceRev >>= 4;
				}
			}
			else
			{
				PICkitFunctions.LastDeviceRev = (int)(num2 & ~(int)PICkitFunctions.DevFile.Families[familyIndex].DeviceIDMask);
			}
			PICkitFunctions.LastDeviceRev &= 65535;
			PICkitFunctions.LastDeviceRev &= (int)PICkitFunctions.DevFile.Families[familyIndex].BlankValue;
			num2 &= PICkitFunctions.DevFile.Families[familyIndex].DeviceIDMask;
			PICkitFunctions.LastDeviceID = num2;
			PICkitFunctions.ActivePart = 0;
			for (int j = 0; j < PICkitFunctions.DevFile.PartsList.Length; j++)
			{
				if ((int)PICkitFunctions.DevFile.PartsList[j].Family == familyIndex && PICkitFunctions.DevFile.PartsList[j].DeviceID == num2)
				{
					PICkitFunctions.ActivePart = j;
					break;
				}
			}
			if (PICkitFunctions.ActivePart == 0)
			{
				if (activePart != 0)
				{
					PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart] = PICkitFunctions.DevFile.PartsList[activePart];
					PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DeviceID = 0U;
					PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].PartName = "Unsupported Part";
				}
				if (resetOnNoDevice)
				{
					PICkitFunctions.ResetBuffers();
				}
				return false;
			}
			if (PICkitFunctions.ActivePart == PICkitFunctions.lastFoundPart && PICkitFunctions.scriptBufferChecksum != 0U && PICkitFunctions.scriptBufferChecksum == PICkitFunctions.getScriptBufferChecksum())
			{
				return true;
			}
			PICkitFunctions.downloadPartScripts(familyIndex);
			if (PICkitFunctions.ActivePart != PICkitFunctions.lastFoundPart)
			{
				PICkitFunctions.ResetBuffers();
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].OSSCALSave)
			{
				PICkitFunctions.VddOn();
				PICkitFunctions.ReadOSSCAL();
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BandGapMask > 0U)
			{
				PICkitFunctions.VddOn();
				PICkitFunctions.ReadBandGap();
			}
			if (!keepVddOn)
			{
				PICkitFunctions.VddOff();
			}
			return true;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0003A1E8 File Offset: 0x000391E8
		private static void downloadPartScripts(int familyIndex)
		{
			PICkitFunctions.writeUSB(new byte[]
			{
				171
			});
			for (int i = 0; i < PICkitFunctions.scriptRedirectTable.Length; i++)
			{
				PICkitFunctions.scriptRedirectTable[i].redirectToScriptLocation = 0;
				PICkitFunctions.scriptRedirectTable[i].deviceFileScriptNumber = 0;
			}
			if (PICkitFunctions.DevFile.Families[familyIndex].ProgEntryScript != 0)
			{
				if (PICkitFunctions.lvpEnabled && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript > 0)
				{
					PICkitFunctions.downloadScript(0, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].LVPScript);
				}
				else if (PICkitFunctions.vppFirstEnabled && PICkitFunctions.DevFile.Families[familyIndex].ProgEntryVPPScript != 0)
				{
					PICkitFunctions.downloadScript(0, (int)PICkitFunctions.DevFile.Families[familyIndex].ProgEntryVPPScript);
				}
				else
				{
					PICkitFunctions.downloadScript(0, (int)PICkitFunctions.DevFile.Families[familyIndex].ProgEntryScript);
				}
			}
			if (PICkitFunctions.DevFile.Families[familyIndex].ProgExitScript != 0)
			{
				PICkitFunctions.downloadScript(1, (int)PICkitFunctions.DevFile.Families[familyIndex].ProgExitScript);
			}
			if (PICkitFunctions.DevFile.Families[familyIndex].ReadDevIDScript != 0)
			{
				PICkitFunctions.downloadScript(2, (int)PICkitFunctions.DevFile.Families[familyIndex].ReadDevIDScript);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemRdScript != 0)
			{
				PICkitFunctions.downloadScript(3, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemRdScript);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ChipErasePrepScript != 0)
			{
				PICkitFunctions.downloadScript(4, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ChipErasePrepScript);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemAddrSetScript != 0)
			{
				PICkitFunctions.downloadScript(5, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemAddrSetScript);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrPrepScript != 0)
			{
				PICkitFunctions.downloadScript(6, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrPrepScript);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrScript != 0)
			{
				PICkitFunctions.downloadScript(7, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemWrScript);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERdPrepScript != 0)
			{
				PICkitFunctions.downloadScript(8, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERdPrepScript);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERdScript != 0)
			{
				PICkitFunctions.downloadScript(9, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERdScript);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEWrPrepScript != 0)
			{
				PICkitFunctions.downloadScript(10, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEWrPrepScript);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEWrScript != 0)
			{
				PICkitFunctions.downloadScript(11, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEWrScript);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigRdPrepScript != 0)
			{
				PICkitFunctions.downloadScript(12, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigRdPrepScript);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigRdScript != 0)
			{
				PICkitFunctions.downloadScript(13, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigRdScript);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWrPrepScript != 0)
			{
				PICkitFunctions.downloadScript(14, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWrPrepScript);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWrScript != 0)
			{
				PICkitFunctions.downloadScript(15, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWrScript);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDRdPrepScript != 0)
			{
				PICkitFunctions.downloadScript(16, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDRdPrepScript);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDRdScript != 0)
			{
				PICkitFunctions.downloadScript(17, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDRdScript);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWrPrepScript != 0)
			{
				PICkitFunctions.downloadScript(18, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWrPrepScript);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWrScript != 0)
			{
				PICkitFunctions.downloadScript(19, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWrScript);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].OSCCALRdScript != 0)
			{
				PICkitFunctions.downloadScript(20, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].OSCCALRdScript);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].OSCCALWrScript != 0)
			{
				PICkitFunctions.downloadScript(21, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].OSCCALWrScript);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ChipEraseScript != 0)
			{
				PICkitFunctions.downloadScript(22, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ChipEraseScript);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemEraseScript != 0)
			{
				PICkitFunctions.downloadScript(23, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgMemEraseScript);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMemEraseScript != 0)
			{
				PICkitFunctions.downloadScript(24, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMemEraseScript);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DebugRowEraseScript != 0)
			{
				PICkitFunctions.downloadScript(26, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].DebugRowEraseScript);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].TestMemoryRdScript != 0)
			{
				PICkitFunctions.downloadScript(27, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].TestMemoryRdScript);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERowEraseScript != 0)
			{
				PICkitFunctions.downloadScript(28, (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EERowEraseScript);
			}
			PICkitFunctions.scriptBufferChecksum = PICkitFunctions.getScriptBufferChecksum();
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0003A93C File Offset: 0x0003993C
		private static uint getScriptBufferChecksum()
		{
			if (PICkitFunctions.LearnMode)
			{
				return 0U;
			}
			if (!PICkitFunctions.writeUSB(new byte[]
			{
				175
			}))
			{
				return 0U;
			}
			if (PICkitFunctions.readUSB())
			{
				uint num = (uint)PICkitFunctions.Usb_read_array[4];
				num += (uint)((uint)PICkitFunctions.Usb_read_array[3] << 8);
				num += (uint)((uint)PICkitFunctions.Usb_read_array[2] << 16);
				return num + (uint)((uint)PICkitFunctions.Usb_read_array[1] << 24);
			}
			return 0U;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0003A9A4 File Offset: 0x000399A4
		private static bool downloadScript(byte scriptBufferLocation, int scriptArrayIndex)
		{
			byte b = scriptBufferLocation;
			byte b2 = 0;
			while ((int)b2 < PICkitFunctions.scriptRedirectTable.Length)
			{
				if (scriptArrayIndex == PICkitFunctions.scriptRedirectTable[(int)b2].deviceFileScriptNumber)
				{
					b = b2;
					break;
				}
				b2 += 1;
			}
			PICkitFunctions.scriptRedirectTable[(int)scriptBufferLocation].redirectToScriptLocation = b;
			PICkitFunctions.scriptRedirectTable[(int)scriptBufferLocation].deviceFileScriptNumber = scriptArrayIndex;
			if (scriptBufferLocation != b)
			{
				return true;
			}
			int scriptLength = (int)PICkitFunctions.DevFile.Scripts[--scriptArrayIndex].ScriptLength;
			byte[] array = new byte[3 + scriptLength];
			array[0] = 164;
			array[1] = scriptBufferLocation;
			array[2] = (byte)scriptLength;
			for (int i = 0; i < scriptLength; i++)
			{
				ushort num = PICkitFunctions.DevFile.Scripts[scriptArrayIndex].Script[i];
				if (PICkitFunctions.fastProgramming)
				{
					array[3 + i] = (byte)num;
				}
				else if (num == 43751)
				{
					ushort num2 = (ushort)(PICkitFunctions.DevFile.Scripts[scriptArrayIndex].Script[i + 1] & 255);
					if (num2 < 170 && num2 != 0)
					{
						array[3 + i++] = (byte)num;
						byte b3 = (byte)PICkitFunctions.DevFile.Scripts[scriptArrayIndex].Script[i];
						array[3 + i] = (byte)(b3 + b3 / 2);
					}
					else
					{
						array[3 + i++] = 232;
						array[3 + i] = 2;
					}
				}
				else if (num == 43752)
				{
					ushort num3 = (ushort)(PICkitFunctions.DevFile.Scripts[scriptArrayIndex].Script[i + 1] & 255);
					if (num3 < 171 && num3 != 0)
					{
						array[3 + i++] = (byte)num;
						byte b4 = (byte)PICkitFunctions.DevFile.Scripts[scriptArrayIndex].Script[i];
						array[3 + i] = (byte)(b4 + b4 / 2);
					}
					else
					{
						array[3 + i++] = 232;
						array[3 + i] = 0;
					}
				}
				else
				{
					array[3 + i] = (byte)num;
				}
			}
			return PICkitFunctions.writeUSB(array);
		}

		// Token: 0x0400031E RID: 798
		public static string FirmwareVersion = "NA";

		// Token: 0x0400031F RID: 799
		public static string DeviceFileVersion = "NA";

		// Token: 0x04000320 RID: 800
		public static DeviceFile DevFile = new DeviceFile();

		// Token: 0x04000321 RID: 801
		public static DeviceData DeviceBuffers;

		// Token: 0x04000322 RID: 802
		public static byte[] Usb_write_array = new byte[65];

		// Token: 0x04000323 RID: 803
		public static byte[] Usb_read_array = new byte[65];

		// Token: 0x04000324 RID: 804
		public static int ActivePart = 0;

		// Token: 0x04000325 RID: 805
		public static uint LastDeviceID = 0U;

		// Token: 0x04000326 RID: 806
		public static int LastDeviceRev = 0;

		// Token: 0x04000327 RID: 807
		public static bool LearnMode = false;

		// Token: 0x04000328 RID: 808
		public static byte LastICSPSpeed = 0;

		// Token: 0x04000329 RID: 809
		private static IntPtr usbReadHandle = IntPtr.Zero;

		// Token: 0x0400032A RID: 810
		private static IntPtr usbWriteHandle = IntPtr.Zero;

		// Token: 0x0400032B RID: 811
		private static ushort lastPk2number = 255;

		// Token: 0x0400032C RID: 812
		private static int[] familySearchTable;

		// Token: 0x0400032D RID: 813
		private static bool vddOn = false;

		// Token: 0x0400032E RID: 814
		private static float vddLastSet = 3.3f;

		// Token: 0x0400032F RID: 815
		private static bool targetSelfPowered = false;

		// Token: 0x04000330 RID: 816
		private static bool fastProgramming = true;

		// Token: 0x04000331 RID: 817
		private static bool assertMCLR = false;

		// Token: 0x04000332 RID: 818
		private static bool vppFirstEnabled = false;

		// Token: 0x04000333 RID: 819
		private static bool lvpEnabled = false;

		// Token: 0x04000334 RID: 820
		private static uint scriptBufferChecksum = 0U;

		// Token: 0x04000335 RID: 821
		private static int lastFoundPart = 0;

		// Token: 0x04000336 RID: 822
		private static PICkitFunctions.scriptRedirect[] scriptRedirectTable = new PICkitFunctions.scriptRedirect[32];

		// Token: 0x02000014 RID: 20
		private struct scriptRedirect
		{
			// Token: 0x04000337 RID: 823
			public byte redirectToScriptLocation;

			// Token: 0x04000338 RID: 824
			public int deviceFileScriptNumber;
		}
	}
}
