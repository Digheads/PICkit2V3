using System;
using System.Globalization;
using System.IO;

namespace PICkit2V3
{
	// Token: 0x02000024 RID: 36
	internal class ImportExportHex
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x0003AC80 File Offset: 0x00039C80
		public static Constants.FileRead ImportHexFile(string filePath, bool progMem, bool eeMem)
		{
			if (filePath.Length > 4 && filePath.Substring(filePath.Length - 4).ToUpper() == ".BIN" && PICkitFunctions.FamilyIsEEPROM())
			{
				return ImportExportHex.ImportBINFile(filePath);
			}
			Constants.FileRead result;
			try
			{
				FileInfo fileInfo = new FileInfo(filePath);
				ImportExportHex.LastWriteTime = fileInfo.LastWriteTime;
				TextReader textReader = fileInfo.OpenText();
				int progMemHexBytes = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemHexBytes;
				int eememHexBytes = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemHexBytes;
				uint eeaddr = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEAddr;
				int num = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem * (uint)progMemHexBytes);
				int num2 = 0;
				bool flag = false;
				bool flag2 = true;
				bool flag3 = false;
				int userIDWords = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords;
				uint num3 = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDAddr;
				if (num3 == 0U)
				{
					num3 = uint.MaxValue;
				}
				int userIDHexBytes = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].UserIDHexBytes;
				int configWords = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords;
				bool[] array = new bool[configWords];
				for (int i = 0; i < configWords; i++)
				{
					PICkitFunctions.DeviceBuffers.ConfigWords[i] = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue;
					if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[i] == 0)
					{
						array[i] = true;
					}
					else
					{
						array[i] = false;
					}
				}
				int num4 = progMemHexBytes;
				uint num5 = 0U;
				uint num6 = 0U;
				uint bootFlash = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BootFlash;
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 16777215U)
				{
					num5 = 486539264U;
					num6 = 532676608U;
					num -= (int)(bootFlash * (uint)progMemHexBytes);
					num += (int)num5;
					num4 = 2;
				}
				uint num7 = num6 + bootFlash * (uint)progMemHexBytes;
				int num8 = (int)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem - bootFlash);
				for (string text = textReader.ReadLine(); text != null; text = textReader.ReadLine())
				{
					if (text[0] == ':' && text.Length >= 11)
					{
						int num9 = int.Parse(text.Substring(1, 2), NumberStyles.HexNumber);
						int num10 = num2 + int.Parse(text.Substring(3, 4), NumberStyles.HexNumber);
						int num11 = int.Parse(text.Substring(7, 2), NumberStyles.HexNumber);
						if (num11 == 0)
						{
							if (text.Length >= 11 + 2 * num9)
							{
								for (int j = 0; j < num9; j++)
								{
									int num12 = num10 + j;
									int num13 = (num12 - (int)num5) / progMemHexBytes;
									int num14 = num12 % progMemHexBytes;
									uint num15 = 4294967040U | uint.Parse(text.Substring(9 + 2 * j, 2), NumberStyles.HexNumber);
									for (int k = 0; k < num14; k++)
									{
										num15 <<= 8;
										num15 |= 255U;
									}
									flag2 = true;
									if ((long)num12 >= (long)((ulong)num5) && num12 < num)
									{
										if (progMem)
										{
											PICkitFunctions.DeviceBuffers.ProgramMemory[num13] &= num15;
										}
										flag2 = false;
									}
									if (bootFlash > 0U && (long)num12 >= (long)((ulong)num6) && (long)num12 < (long)((ulong)num7))
									{
										num13 = (int)((long)num8 + ((long)num12 - (long)((ulong)num6)) / (long)progMemHexBytes);
										if (progMem)
										{
											PICkitFunctions.DeviceBuffers.ProgramMemory[num13] &= num15;
										}
										flag2 = false;
									}
									if ((long)num12 >= (long)((ulong)eeaddr) && eeaddr > 0U && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem > 0)
									{
										int num16 = (int)((long)num12 - (long)((ulong)eeaddr)) / eememHexBytes;
										if (num16 < (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem)
										{
											flag2 = false;
											if (eeMem)
											{
												if (eememHexBytes == progMemHexBytes)
												{
													PICkitFunctions.DeviceBuffers.EEPromMemory[num16] &= num15;
												}
												else
												{
													int num17 = num14 / eememHexBytes * eememHexBytes;
													for (int l = 0; l < num17; l++)
													{
														num15 >>= 8;
													}
													PICkitFunctions.DeviceBuffers.EEPromMemory[num16] &= num15;
												}
											}
										}
									}
									else if ((long)num12 >= (long)((ulong)eeaddr) && eeaddr > 0U && PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem == 0)
									{
										flag2 = false;
									}
									if ((long)num12 >= (long)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr) && configWords > 0)
									{
										int num18 = (num12 - (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr) / num4;
										if (num4 != progMemHexBytes && num14 > 1)
										{
											num15 = (num15 >> 16 & PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue);
										}
										if (num18 < (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords)
										{
											flag2 = false;
											flag = true;
											array[num18] = true;
											if (progMem)
											{
												PICkitFunctions.DeviceBuffers.ConfigWords[num18] &= num15;
												if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue == 4095U)
												{
													PICkitFunctions.DeviceBuffers.ConfigWords[num18] |= (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[5];
												}
												if (num12 < num)
												{
													uint num19;
													if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue == 65535U)
													{
														num19 = 61440U;
													}
													else
													{
														num19 = (16711680U | (uint)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank[num18] & ~(uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[num18]));
													}
													PICkitFunctions.DeviceBuffers.ProgramMemory[num13] &= (num15 & (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank[num18]);
													PICkitFunctions.DeviceBuffers.ProgramMemory[num13] |= num19;
												}
											}
										}
									}
									if (userIDWords > 0 && (long)num12 >= (long)((ulong)num3))
									{
										int num20 = (int)((long)num12 - (long)((ulong)num3)) / userIDHexBytes;
										if (num20 < userIDWords)
										{
											flag2 = false;
											if (progMem)
											{
												if (userIDHexBytes == progMemHexBytes)
												{
													PICkitFunctions.DeviceBuffers.UserIDs[num20] &= num15;
												}
												else
												{
													int num21 = num14 / userIDHexBytes * userIDHexBytes;
													for (int m = 0; m < num21; m++)
													{
														num15 >>= 8;
													}
													PICkitFunctions.DeviceBuffers.UserIDs[num20] &= num15;
												}
											}
										}
									}
									if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].IgnoreBytes > 0 && (long)num12 >= (long)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].IgnoreAddress) && (long)num12 < (long)((ulong)(PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].IgnoreAddress + (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].IgnoreBytes)))
									{
										flag2 = false;
									}
									if (FormPICkit2.TestMemoryEnabled && FormPICkit2.TestMemoryOpen && FormPICkit2.formTestMem.HexImportExportTM() && (long)num12 >= (long)((ulong)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart) && PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart > 0U && FormPICkit2.TestMemoryWords > 0)
									{
										int num22 = (int)((long)num12 - (long)((ulong)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart)) / progMemHexBytes;
										if (num22 < FormPICkit2.TestMemoryWords)
										{
											flag2 = false;
											FormTestMemory.TestMemory[num22] &= num15;
										}
									}
								}
							}
							if (flag2)
							{
								flag3 = true;
							}
						}
						if (num11 == 2 || num11 == 4)
						{
							if (text.Length >= 11 + 2 * num9)
							{
								num2 = int.Parse(text.Substring(9, 4), NumberStyles.HexNumber);
							}
							if (num11 == 2)
							{
								num2 <<= 4;
							}
							else
							{
								num2 <<= 16;
							}
						}
						if (num11 == 1 || fileInfo.Extension.ToUpper() == ".NUM")
						{
							break;
						}
					}
				}
				textReader.Close();
				if (configWords > 0)
				{
					if (!flag)
					{
						return Constants.FileRead.noconfig;
					}
					for (int n = 0; n < configWords; n++)
					{
						if (!array[n])
						{
							if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue == 16777215U && configWords > 7)
							{
								PICkitFunctions.DeviceBuffers.ConfigWords[7] &= (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[7];
							}
							return Constants.FileRead.partialcfg;
						}
					}
				}
				if (flag3)
				{
					result = Constants.FileRead.largemem;
				}
				else
				{
					result = Constants.FileRead.success;
				}
			}
			catch
			{
				result = Constants.FileRead.failed;
			}
			return result;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0003B610 File Offset: 0x0003A610
		public static Constants.FileRead ImportBINFile(string filePath)
		{
			try
			{
				FileInfo fileInfo = new FileInfo(filePath);
				ImportExportHex.LastWriteTime = fileInfo.LastWriteTime;
				FileStream fileStream = File.OpenRead(filePath);
				int progMemHexBytes = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemHexBytes;
				int num = 0;
				int num2 = 0;
				byte[] array = new byte[1];
				while (fileStream.Read(array, 0, 1) > 0)
				{
					if ((long)num >= (long)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem))
					{
						return Constants.FileRead.largemem;
					}
					uint num3 = 4294967040U | (uint)array[0];
					for (int i = 0; i < num2; i++)
					{
						num3 <<= 8;
						num3 |= 255U;
					}
					PICkitFunctions.DeviceBuffers.ProgramMemory[num] &= num3;
					if (++num2 >= progMemHexBytes)
					{
						num++;
						num2 = 0;
					}
				}
				fileStream.Close();
			}
			catch
			{
				return Constants.FileRead.failed;
			}
			return Constants.FileRead.success;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0003B710 File Offset: 0x0003A710
		public static bool ExportHexFile(string filePath, bool progMem, bool eeMem)
		{
			if (filePath.Length > 4 && filePath.Substring(filePath.Length - 4).ToUpper() == ".BIN" && PICkitFunctions.FamilyIsEEPROM())
			{
				return ImportExportHex.ExportBINFile(filePath);
			}
			StreamWriter streamWriter = new StreamWriter(filePath);
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 16777215U)
			{
				streamWriter.WriteLine(":020000041D00DD");
			}
			else
			{
				streamWriter.WriteLine(":020000040000FA");
			}
			int num = 0;
			int num2 = 0;
			int num3 = PICkitFunctions.DeviceBuffers.ProgramMemory.Length;
			if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 16777215U)
			{
				num = 7424;
				num2 = 0;
				num3 -= (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BootFlash;
			}
			int num4 = 0;
			int progMemHexBytes = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemHexBytes;
			int num5 = 16 / progMemHexBytes;
			if (progMem)
			{
				do
				{
					string text = string.Format(":10{0:X4}00", num2);
					for (int i = 0; i < num5; i++)
					{
						string text2 = "00000000";
						if (num4 + i < PICkitFunctions.DeviceBuffers.ProgramMemory.Length)
						{
							text2 = string.Format("{0:X8}", PICkitFunctions.DeviceBuffers.ProgramMemory[num4 + i]);
						}
						for (int j = 0; j < progMemHexBytes; j++)
						{
							text += text2.Substring(6 - 2 * j, 2);
						}
					}
					text += string.Format("{0:X2}", ImportExportHex.computeChecksum(text));
					streamWriter.WriteLine(text);
					num2 += 16;
					num4 += num5;
					if (num2 > 65535 && num4 < PICkitFunctions.DeviceBuffers.ProgramMemory.Length)
					{
						num += num2 >> 16;
						num2 &= 65535;
						string text3 = string.Format(":02000004{0:X4}", num);
						text3 += string.Format("{0:X2}", ImportExportHex.computeChecksum(text3));
						streamWriter.WriteLine(text3);
					}
				}
				while (num4 < num3);
			}
			if (PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].BootFlash > 0U && PICkitFunctions.FamilyIsPIC32())
			{
				streamWriter.WriteLine(":020000041FC01B");
				num4 = num3;
				num3 = PICkitFunctions.DeviceBuffers.ProgramMemory.Length;
				num = 8128;
				num2 = 0;
				if (progMem)
				{
					do
					{
						string text4 = string.Format(":10{0:X4}00", num2);
						for (int k = 0; k < num5; k++)
						{
							string text5 = string.Format("{0:X8}", PICkitFunctions.DeviceBuffers.ProgramMemory[num4 + k]);
							for (int l = 0; l < progMemHexBytes; l++)
							{
								text4 += text5.Substring(6 - 2 * l, 2);
							}
						}
						text4 += string.Format("{0:X2}", ImportExportHex.computeChecksum(text4));
						streamWriter.WriteLine(text4);
						num2 += 16;
						num4 += num5;
						if (num2 > 65535 && num4 < PICkitFunctions.DeviceBuffers.ProgramMemory.Length)
						{
							num += num2 >> 16;
							num2 &= 65535;
							string text6 = string.Format(":02000004{0:X4}", num);
							text6 += string.Format("{0:X2}", ImportExportHex.computeChecksum(text6));
							streamWriter.WriteLine(text6);
						}
					}
					while (num4 < num3);
				}
			}
			if (eeMem)
			{
				int eemem = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEMem;
				num4 = 0;
				if (eemem > 0)
				{
					uint eeaddr = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].EEAddr;
					if ((eeaddr & 4294901760U) > 0U)
					{
						string text7 = string.Format(":02000004{0:X4}", eeaddr >> 16);
						text7 += string.Format("{0:X2}", ImportExportHex.computeChecksum(text7));
						streamWriter.WriteLine(text7);
					}
					num2 = (int)(eeaddr & 65535U);
					int eememHexBytes = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].EEMemHexBytes;
					num5 = 16 / eememHexBytes;
					do
					{
						string text8 = string.Format(":10{0:X4}00", num2);
						for (int m = 0; m < num5; m++)
						{
							string text9 = string.Format("{0:X8}", PICkitFunctions.DeviceBuffers.EEPromMemory[num4 + m]);
							for (int n = 0; n < eememHexBytes; n++)
							{
								text8 += text9.Substring(6 - 2 * n, 2);
							}
						}
						text8 += string.Format("{0:X2}", ImportExportHex.computeChecksum(text8));
						streamWriter.WriteLine(text8);
						num2 += 16;
						num4 += num5;
					}
					while (num4 < PICkitFunctions.DeviceBuffers.EEPromMemory.Length);
				}
			}
			if (progMem)
			{
				int num6 = progMemHexBytes;
				if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 16777215U)
				{
					num6 = 2;
				}
				int configWords = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigWords;
				if (configWords > 0 && (ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr > (ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem * (ulong)((long)progMemHexBytes))
				{
					uint configAddr = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigAddr;
					if ((configAddr & 4294901760U) > 0U)
					{
						string text10 = string.Format(":02000004{0:X4}", configAddr >> 16);
						text10 += string.Format("{0:X2}", ImportExportHex.computeChecksum(text10));
						streamWriter.WriteLine(text10);
					}
					num2 = (int)(configAddr & 65535U);
					int num7 = 0;
					for (int num8 = 0; num8 < (configWords * num6 - 1) / 16 + 1; num8++)
					{
						int num9 = configWords - num7;
						if (num9 >= 16 / num6)
						{
							num9 = 16 / num6;
						}
						string text11 = string.Format(":{0:X2}{1:X4}00", num9 * num6, num2);
						num2 += num9 * num6;
						for (int num10 = 0; num10 < num9; num10++)
						{
							uint num11 = PICkitFunctions.DeviceBuffers.ConfigWords[num7 + num10];
							if (PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].BlankValue > 16777215U)
							{
								num11 |= (uint)(~(uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigMasks[num7 + num10]);
								num11 &= (uint)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ConfigBlank[num7 + num10];
							}
							string text12 = string.Format("{0:X8}", num11);
							for (int num12 = 0; num12 < num6; num12++)
							{
								text11 += text12.Substring(8 - (num12 + 1) * 2, 2);
							}
						}
						text11 += string.Format("{0:X2}", ImportExportHex.computeChecksum(text11));
						streamWriter.WriteLine(text11);
						num7 += num9;
					}
				}
			}
			if (progMem)
			{
				int userIDWords = (int)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDWords;
				num4 = 0;
				if (userIDWords > 0)
				{
					uint userIDAddr = PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].UserIDAddr;
					if ((userIDAddr & 4294901760U) > 0U)
					{
						string text13 = string.Format(":02000004{0:X4}", userIDAddr >> 16);
						text13 += string.Format("{0:X2}", ImportExportHex.computeChecksum(text13));
						streamWriter.WriteLine(text13);
					}
					num2 = (int)(userIDAddr & 65535U);
					int userIDHexBytes = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].UserIDHexBytes;
					num5 = 16 / userIDHexBytes;
					do
					{
						int num13 = (userIDWords - num4) * userIDHexBytes;
						string text14;
						if (num13 < 16)
						{
							text14 = string.Format(":{0:X2}{1:X4}00", num13, num2);
							num5 = userIDWords - num4;
						}
						else
						{
							text14 = string.Format(":10{0:X4}00", num2);
						}
						for (int num14 = 0; num14 < num5; num14++)
						{
							string text15 = string.Format("{0:X8}", PICkitFunctions.DeviceBuffers.UserIDs[num4 + num14]);
							for (int num15 = 0; num15 < userIDHexBytes; num15++)
							{
								text14 += text15.Substring(6 - 2 * num15, 2);
							}
						}
						text14 += string.Format("{0:X2}", ImportExportHex.computeChecksum(text14));
						streamWriter.WriteLine(text14);
						num2 += 16;
						num4 += num5;
					}
					while (num4 < PICkitFunctions.DeviceBuffers.UserIDs.Length);
				}
			}
			if (FormPICkit2.TestMemoryEnabled && FormPICkit2.TestMemoryOpen && FormPICkit2.formTestMem.HexImportExportTM())
			{
				int testMemoryWords = FormPICkit2.TestMemoryWords;
				num4 = 0;
				if (testMemoryWords > 0)
				{
					uint testMemoryStart = PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].TestMemoryStart;
					if ((testMemoryStart & 4294901760U) > 0U)
					{
						string text16 = string.Format(":02000004{0:X4}", testMemoryStart >> 16);
						text16 += string.Format("{0:X2}", ImportExportHex.computeChecksum(text16));
						streamWriter.WriteLine(text16);
					}
					num2 = (int)(testMemoryStart & 65535U);
					int progMemHexBytes2 = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemHexBytes;
					num5 = 16 / progMemHexBytes2;
					do
					{
						string text17 = string.Format(":10{0:X4}00", num2);
						for (int num16 = 0; num16 < num5; num16++)
						{
							string text18 = string.Format("{0:X8}", FormTestMemory.TestMemory[num4 + num16]);
							for (int num17 = 0; num17 < progMemHexBytes2; num17++)
							{
								text17 += text18.Substring(6 - 2 * num17, 2);
							}
						}
						text17 += string.Format("{0:X2}", ImportExportHex.computeChecksum(text17));
						if (num2 != (int)(testMemoryStart & 65535U) || PICkitFunctions.GetActiveFamily() != 3)
						{
							streamWriter.WriteLine(text17);
						}
						num2 += 16;
						num4 += num5;
					}
					while (num4 < FormPICkit2.TestMemoryWords);
				}
			}
			streamWriter.WriteLine(":00000001FF");
			streamWriter.Close();
			return true;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0003C17C File Offset: 0x0003B17C
		public static bool ExportBINFile(string filePath)
		{
			try
			{
				FileStream fileStream = File.Open(filePath, FileMode.Create);
				int progMemHexBytes = (int)PICkitFunctions.DevFile.Families[PICkitFunctions.GetActiveFamily()].ProgMemHexBytes;
				int num = 0;
				while ((long)num < (long)((ulong)PICkitFunctions.DevFile.PartsList[PICkitFunctions.ActivePart].ProgramMem))
				{
					for (int i = 0; i < progMemHexBytes; i++)
					{
						byte value = (byte)(PICkitFunctions.DeviceBuffers.ProgramMemory[num] >> 8 * i & 255U);
						fileStream.WriteByte(value);
					}
					num++;
				}
				fileStream.Close();
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0003C224 File Offset: 0x0003B224
		private static byte computeChecksum(string fileLine)
		{
			int num = int.Parse(fileLine.Substring(1, 2), NumberStyles.HexNumber);
			if (fileLine.Length >= 9 + 2 * num)
			{
				int num2 = num;
				for (int i = 0; i < 3 + num; i++)
				{
					num2 += int.Parse(fileLine.Substring(3 + 2 * i, 2), NumberStyles.HexNumber);
				}
				num2 = -num2;
				return (byte)(num2 & 255);
			}
			return 0;
		}

		// Token: 0x04000339 RID: 825
		public static DateTime LastWriteTime = default(DateTime);
	}
}
