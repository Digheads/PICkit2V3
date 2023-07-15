using System;
using System.Text;

namespace PICkit2V3
{
	// Token: 0x02000002 RID: 2
	public class Utilities
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002100 File Offset: 0x00001100
		public static int Convert_Value_To_Int(string p_value)
		{
			uint[] array = new uint[]
			{
				0U,
				0U,
				2147483648U,
				1073741824U,
				536870912U,
				268435456U,
				134217728U,
				67108864U,
				33554432U,
				16777216U,
				8388608U,
				4194304U,
				2097152U,
				1048576U,
				524288U,
				262144U,
				131072U,
				65536U,
				32768U,
				16384U,
				8192U,
				4096U,
				2048U,
				1024U,
				512U,
				256U,
				128U,
				64U,
				32U,
				16U,
				8U,
				4U,
				2U,
				1U
			};
			uint[] array2 = new uint[]
			{
				0U,
				0U,
				268435456U,
				16777216U,
				1048576U,
				65536U,
				4096U,
				256U,
				16U,
				1U
			};
			int num = 0;
			if (p_value[0] == '\0')
			{
				num = 0;
			}
			else if (p_value[0] == 'Y' || p_value[0] == 'y')
			{
				num = 1;
			}
			else if (p_value[0] == 'N' || p_value[0] == 'n')
			{
				num = 0;
			}
			else if (p_value.Length > 1)
			{
				if ((p_value[0] == '0' && (p_value[1] == 'b' || p_value[1] == 'B')) || p_value[0] == 'b' || p_value[0] == 'B')
				{
					int num2 = p_value.Length - 1;
					int num3;
					if (p_value[0] == '0')
					{
						num3 = 2;
					}
					else
					{
						num3 = 1;
					}
					for (int i = num3; i <= num2; i++)
					{
						int num4;
						if (p_value[i] == '1')
						{
							num4 = 1;
						}
						else
						{
							num4 = 0;
						}
						num += (int)(array[i + 34 - p_value.Length] * (uint)num4);
					}
				}
				else if (p_value[0] == '0' && (p_value[1] == 'x' || p_value[1] == 'X'))
				{
					int num2 = p_value.Length - 1;
					int i = 2;
					while (i <= num2)
					{
						char c = p_value[i];
						int num4;
						switch (c)
						{
						case 'A':
							goto IL_197;
						case 'B':
							goto IL_19C;
						case 'C':
							goto IL_1A1;
						case 'D':
							goto IL_1A6;
						case 'E':
							goto IL_1AB;
						case 'F':
							goto IL_1B0;
						default:
							switch (c)
							{
							case 'a':
								goto IL_197;
							case 'b':
								goto IL_19C;
							case 'c':
								goto IL_1A1;
							case 'd':
								goto IL_1A6;
							case 'e':
								goto IL_1AB;
							case 'f':
								goto IL_1B0;
							default:
							{
								string s = p_value[i].ToString();
								if (!int.TryParse(s, out num4))
								{
									num4 = 0;
								}
								break;
							}
							}
							break;
						}
						IL_1D5:
						num += (int)(array2[i + 10 - p_value.Length] * (uint)num4);
						i++;
						continue;
						IL_197:
						num4 = 10;
						goto IL_1D5;
						IL_19C:
						num4 = 11;
						goto IL_1D5;
						IL_1A1:
						num4 = 12;
						goto IL_1D5;
						IL_1A6:
						num4 = 13;
						goto IL_1D5;
						IL_1AB:
						num4 = 14;
						goto IL_1D5;
						IL_1B0:
						num4 = 15;
						goto IL_1D5;
					}
				}
				else if (!int.TryParse(p_value, out num))
				{
					num = 0;
				}
			}
			else if (!int.TryParse(p_value, out num))
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002324 File Offset: 0x00001324
		public static string ConvertIntASCII(int toConvert, int numBytes)
		{
			byte[] array = new byte[numBytes];
			for (int i = numBytes; i > 0; i--)
			{
				array[i - 1] = (byte)toConvert;
				if (array[i - 1] < 32 || array[i - 1] > 126)
				{
					array[i - 1] = 46;
				}
				toConvert >>= 8;
			}
			return Encoding.ASCII.GetString(array);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002374 File Offset: 0x00001374
		public static string ConvertIntASCIIReverse(int toConvert, int numBytes)
		{
			numBytes += numBytes - 1;
			byte[] array = new byte[numBytes];
			for (int i = 0; i < numBytes; i++)
			{
				if (i % 2 == 1)
				{
					array[i] = 32;
				}
				else
				{
					array[i] = (byte)toConvert;
					if (array[i] < 32 || array[i] > 126)
					{
						array[i] = 46;
					}
					toConvert >>= 8;
				}
			}
			return Encoding.ASCII.GetString(array);
		}
	}
}
