using System.Globalization;
using System.IO;
using System.Threading;

namespace PICkit2V3
{
	internal class Pk2BootLoader
	{
		public static bool ReadHexAndDownload(string fileName, ref ushort pk2num)
		{
			bool result;
			try
			{
				FileInfo fileInfo = new FileInfo(fileName);
				TextReader textReader = fileInfo.OpenText();
				byte[] array = new byte[35];
				string text = textReader.ReadLine();
				if (text != null)
				{
					PICkitFunctions.EnterBootloader();
					Thread.Sleep(3000);
					pk2num = 0;
					int i;
					for (i = 0; i < 10; i++)
					{
						if (PICkitFunctions.DetectPICkit2Device(pk2num, true) == Constants.PICkit2USB.bootloader)
						{
							if (PICkitFunctions.VerifyBootloaderMode())
								break;
						}
						else
							pk2num += 1;
						Thread.Sleep(500);
					}
					if (i == 10)
					{
						textReader.Close();
						return false;
					}
				}
				PICkitFunctions.BL_EraseFlash();
				bool flag = false;
				while (text != null)
				{
					if (text[0] == ':' && text.Length >= 11)
					{
						int num = int.Parse(text.Substring(1, 2), NumberStyles.HexNumber);
						int num2 = int.Parse(text.Substring(3, 4), NumberStyles.HexNumber);
						int num3 = int.Parse(text.Substring(7, 2), NumberStyles.HexNumber);
						if (flag && (num2 & 16) == 0)
						{
							PICkitFunctions.BL_WriteFlash(array);
							for (int j = 0; j < array.Length; j++)
							{
								array[j] = byte.MaxValue;
							}
						}
						flag = (num2 & 16) == 16;
						if (num3 == 0 && num2 >= 8192 && num2 < 32736)
						{
							if (!flag)
							{
								int num4 = num2 & 65504;
								array[0] = (byte)(num4 & 255);
								array[1] = (byte)(num4 >> 8 & 255);
								array[2] = 0;
							}
							if (text.Length >= 11 + 2 * num)
							{
								int num5 = num2 & 15;
								int num6 = num5 + num;
								int num7 = 3;
								if (flag)
								{
									num7 = 19;
								}
								for (int k = 0; k < 16; k++)
								{
									if (k >= num5 && k < num6)
									{
										uint num8 = uint.Parse(text.Substring(9 + 2 * (k - num5), 2), NumberStyles.HexNumber);
										array[num7 + k] = (byte)(num8 & 255U);
									}
								}
							}
						}
						if (num3 == 1)
						{
							break;
						}
					}
					text = textReader.ReadLine();
				}
				PICkitFunctions.BL_WriteFlash(array);
				textReader.Close();
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool ReadHexAndVerify(string fileName)
		{
			bool result;
			try
			{
				FileInfo fileInfo = new FileInfo(fileName);
				TextReader textReader = fileInfo.OpenText();
				string text = textReader.ReadLine();
				bool flag = true;
				int num = 0;
				while (text != null)
				{
					if (text[0] == ':' && text.Length >= 11)
					{
						int num2 = int.Parse(text.Substring(1, 2), NumberStyles.HexNumber);
						int num3 = int.Parse(text.Substring(3, 4), NumberStyles.HexNumber);
						int num4 = int.Parse(text.Substring(7, 2), NumberStyles.HexNumber);
						if (num4 == 0 && num3 >= 8192 && num3 < 32736)
						{
							int num5 = num3 & 15;
							int num6 = num3 & 65520;
							if (num != num6)
							{
								PICkitFunctions.BL_ReadFlash16(num6);
							}
							if (text.Length >= 11 + 2 * num2)
							{
								for (int i = 0; i < num2; i++)
								{
									uint num7 = uint.Parse(text.Substring(9 + 2 * i, 2), NumberStyles.HexNumber);
									if (PICkitFunctions.Usb_read_array[6 + num5 + i] != (byte)(num7 & 255U))
									{
										flag = false;
										num4 = 1;
										break;
									}
								}
							}
							num = num6;
						}
						if (num4 == 1)
						{
							break;
						}
					}
					text = textReader.ReadLine();
				}
				textReader.Close();
				result = flag;
			}
			catch
			{
				result = false;
			}
			return result;
		}
	}
}
