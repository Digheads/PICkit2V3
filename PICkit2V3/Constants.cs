using System;

namespace PICkit2V3
{
	// Token: 0x0200002C RID: 44
	public class Constants
	{
		// Token: 0x04000418 RID: 1048
		public const string AppVersion = "2.61.00";

		// Token: 0x04000419 RID: 1049
		public const byte DevFileCompatLevel = 6;

		// Token: 0x0400041A RID: 1050
		public const byte DevFileCompatLevelMin = 0;

		// Token: 0x0400041B RID: 1051
		public const string UserGuideFileName = "\\PICkit2 User Guide 51553E.pdf";

		// Token: 0x0400041C RID: 1052
		public const byte FWVerMajorReq = 2;

		// Token: 0x0400041D RID: 1053
		public const byte FWVerMinorReq = 32;

		// Token: 0x0400041E RID: 1054
		public const byte FWVerDotReq = 0;

		// Token: 0x0400041F RID: 1055
		public const string FWFileName = "PK2V023200.hex";

		// Token: 0x04000420 RID: 1056
		public const uint PACKET_SIZE = 65U;

		// Token: 0x04000421 RID: 1057
		public const uint USB_REPORTLENGTH = 64U;

		// Token: 0x04000422 RID: 1058
		public const byte BIT_MASK_0 = 1;

		// Token: 0x04000423 RID: 1059
		public const byte BIT_MASK_1 = 2;

		// Token: 0x04000424 RID: 1060
		public const byte BIT_MASK_2 = 4;

		// Token: 0x04000425 RID: 1061
		public const byte BIT_MASK_3 = 8;

		// Token: 0x04000426 RID: 1062
		public const byte BIT_MASK_4 = 16;

		// Token: 0x04000427 RID: 1063
		public const byte BIT_MASK_5 = 32;

		// Token: 0x04000428 RID: 1064
		public const byte BIT_MASK_6 = 64;

		// Token: 0x04000429 RID: 1065
		public const byte BIT_MASK_7 = 128;

		// Token: 0x0400042A RID: 1066
		public const ushort MChipVendorID = 1240;

		// Token: 0x0400042B RID: 1067
		public const ushort Pk2DeviceID = 51;

		// Token: 0x0400042C RID: 1068
		public const ushort ConfigRows = 2;

		// Token: 0x0400042D RID: 1069
		public const ushort ConfigColumns = 4;

		// Token: 0x0400042E RID: 1070
		public const ushort MaxReadCfgMasks = 8;

		// Token: 0x0400042F RID: 1071
		public const ushort NumConfigMasks = 9;

		// Token: 0x04000430 RID: 1072
		public const float VddThresholdForSelfPoweredTarget = 2.3f;

		// Token: 0x04000431 RID: 1073
		public const bool NoMessage = false;

		// Token: 0x04000432 RID: 1074
		public const bool ShowMessage = true;

		// Token: 0x04000433 RID: 1075
		public const bool UpdateMemoryDisplays = true;

		// Token: 0x04000434 RID: 1076
		public const bool DontUpdateMemDisplays = false;

		// Token: 0x04000435 RID: 1077
		public const bool EraseEE = true;

		// Token: 0x04000436 RID: 1078
		public const bool WriteEE = false;

		// Token: 0x04000437 RID: 1079
		public const int UploadBufferSize = 128;

		// Token: 0x04000438 RID: 1080
		public const int DownLoadBufferSize = 256;

		// Token: 0x04000439 RID: 1081
		public const byte READFWFLASH = 1;

		// Token: 0x0400043A RID: 1082
		public const byte WRITEFWFLASH = 2;

		// Token: 0x0400043B RID: 1083
		public const byte ERASEFWFLASH = 3;

		// Token: 0x0400043C RID: 1084
		public const byte READFWEEDATA = 4;

		// Token: 0x0400043D RID: 1085
		public const byte WRITEFWEEDATA = 5;

		// Token: 0x0400043E RID: 1086
		public const byte RESETFWDEVICE = 255;

		// Token: 0x0400043F RID: 1087
		public const byte ENTER_BOOTLOADER = 66;

		// Token: 0x04000440 RID: 1088
		public const byte NO_OPERATION = 90;

		// Token: 0x04000441 RID: 1089
		public const byte FIRMWARE_VERSION = 118;

		// Token: 0x04000442 RID: 1090
		public const byte SETVDD = 160;

		// Token: 0x04000443 RID: 1091
		public const byte SETVPP = 161;

		// Token: 0x04000444 RID: 1092
		public const byte READ_STATUS = 162;

		// Token: 0x04000445 RID: 1093
		public const byte READ_VOLTAGES = 163;

		// Token: 0x04000446 RID: 1094
		public const byte DOWNLOAD_SCRIPT = 164;

		// Token: 0x04000447 RID: 1095
		public const byte RUN_SCRIPT = 165;

		// Token: 0x04000448 RID: 1096
		public const byte EXECUTE_SCRIPT = 166;

		// Token: 0x04000449 RID: 1097
		public const byte CLR_DOWNLOAD_BUFFER = 167;

		// Token: 0x0400044A RID: 1098
		public const byte DOWNLOAD_DATA = 168;

		// Token: 0x0400044B RID: 1099
		public const byte CLR_UPLOAD_BUFFER = 169;

		// Token: 0x0400044C RID: 1100
		public const byte UPLOAD_DATA = 170;

		// Token: 0x0400044D RID: 1101
		public const byte CLR_SCRIPT_BUFFER = 171;

		// Token: 0x0400044E RID: 1102
		public const byte UPLOAD_DATA_NOLEN = 172;

		// Token: 0x0400044F RID: 1103
		public const byte END_OF_BUFFER = 173;

		// Token: 0x04000450 RID: 1104
		public const byte RESET = 174;

		// Token: 0x04000451 RID: 1105
		public const byte SCRIPT_BUFFER_CHKSUM = 175;

		// Token: 0x04000452 RID: 1106
		public const byte SET_VOLTAGE_CALS = 176;

		// Token: 0x04000453 RID: 1107
		public const byte WR_INTERNAL_EE = 177;

		// Token: 0x04000454 RID: 1108
		public const byte RD_INTERNAL_EE = 178;

		// Token: 0x04000455 RID: 1109
		public const byte ENTER_UART_MODE = 179;

		// Token: 0x04000456 RID: 1110
		public const byte EXIT_UART_MODE = 180;

		// Token: 0x04000457 RID: 1111
		public const byte ENTER_LEARN_MODE = 181;

		// Token: 0x04000458 RID: 1112
		public const byte EXIT_LEARN_MODE = 182;

		// Token: 0x04000459 RID: 1113
		public const byte ENABLE_PK2GO_MODE = 183;

		// Token: 0x0400045A RID: 1114
		public const byte LOGIC_ANALYZER_GO = 184;

		// Token: 0x0400045B RID: 1115
		public const byte COPY_RAM_UPLOAD = 185;

		// Token: 0x0400045C RID: 1116
		public const byte MC_READ_OSCCAL = 128;

		// Token: 0x0400045D RID: 1117
		public const byte MC_WRITE_OSCCAL = 129;

		// Token: 0x0400045E RID: 1118
		public const byte MC_START_CHECKSUM = 130;

		// Token: 0x0400045F RID: 1119
		public const byte MC_VERIFY_CHECKSUM = 131;

		// Token: 0x04000460 RID: 1120
		public const byte MC_CHECK_DEVICE_ID = 132;

		// Token: 0x04000461 RID: 1121
		public const byte MC_READ_BANDGAP = 133;

		// Token: 0x04000462 RID: 1122
		public const byte MC_WRITE_CFG_BANDGAP = 134;

		// Token: 0x04000463 RID: 1123
		public const byte MC_CHANGE_CHKSM_FRMT = 135;

		// Token: 0x04000464 RID: 1124
		public const byte _VDD_ON = 255;

		// Token: 0x04000465 RID: 1125
		public const byte _VDD_OFF = 254;

		// Token: 0x04000466 RID: 1126
		public const byte _VDD_GND_ON = 253;

		// Token: 0x04000467 RID: 1127
		public const byte _VDD_GND_OFF = 252;

		// Token: 0x04000468 RID: 1128
		public const byte _VPP_ON = 251;

		// Token: 0x04000469 RID: 1129
		public const byte _VPP_OFF = 250;

		// Token: 0x0400046A RID: 1130
		public const byte _VPP_PWM_ON = 249;

		// Token: 0x0400046B RID: 1131
		public const byte _VPP_PWM_OFF = 248;

		// Token: 0x0400046C RID: 1132
		public const byte _MCLR_GND_ON = 247;

		// Token: 0x0400046D RID: 1133
		public const byte _MCLR_GND_OFF = 246;

		// Token: 0x0400046E RID: 1134
		public const byte _BUSY_LED_ON = 245;

		// Token: 0x0400046F RID: 1135
		public const byte _BUSY_LED_OFF = 244;

		// Token: 0x04000470 RID: 1136
		public const byte _SET_ICSP_PINS = 243;

		// Token: 0x04000471 RID: 1137
		public const byte _WRITE_BYTE_LITERAL = 242;

		// Token: 0x04000472 RID: 1138
		public const byte _WRITE_BYTE_BUFFER = 241;

		// Token: 0x04000473 RID: 1139
		public const byte _READ_BYTE_BUFFER = 240;

		// Token: 0x04000474 RID: 1140
		public const byte _READ_BYTE = 239;

		// Token: 0x04000475 RID: 1141
		public const byte _WRITE_BITS_LITERAL = 238;

		// Token: 0x04000476 RID: 1142
		public const byte _WRITE_BITS_BUFFER = 237;

		// Token: 0x04000477 RID: 1143
		public const byte _READ_BITS_BUFFER = 236;

		// Token: 0x04000478 RID: 1144
		public const byte _READ_BITS = 235;

		// Token: 0x04000479 RID: 1145
		public const byte _SET_ICSP_SPEED = 234;

		// Token: 0x0400047A RID: 1146
		public const byte _LOOP = 233;

		// Token: 0x0400047B RID: 1147
		public const byte _DELAY_LONG = 232;

		// Token: 0x0400047C RID: 1148
		public const byte _DELAY_SHORT = 231;

		// Token: 0x0400047D RID: 1149
		public const byte _IF_EQ_GOTO = 230;

		// Token: 0x0400047E RID: 1150
		public const byte _IF_GT_GOTO = 229;

		// Token: 0x0400047F RID: 1151
		public const byte _GOTO_INDEX = 228;

		// Token: 0x04000480 RID: 1152
		public const byte _EXIT_SCRIPT = 227;

		// Token: 0x04000481 RID: 1153
		public const byte _PEEK_SFR = 226;

		// Token: 0x04000482 RID: 1154
		public const byte _POKE_SFR = 225;

		// Token: 0x04000483 RID: 1155
		public const byte _ICDSLAVE_RX = 224;

		// Token: 0x04000484 RID: 1156
		public const byte _ICDSLAVE_TX_LIT = 223;

		// Token: 0x04000485 RID: 1157
		public const byte _ICDSLAVE_TX_BUF = 222;

		// Token: 0x04000486 RID: 1158
		public const byte _LOOPBUFFER = 221;

		// Token: 0x04000487 RID: 1159
		public const byte _ICSP_STATES_BUFFER = 220;

		// Token: 0x04000488 RID: 1160
		public const byte _POP_DOWNLOAD = 219;

		// Token: 0x04000489 RID: 1161
		public const byte _COREINST18 = 218;

		// Token: 0x0400048A RID: 1162
		public const byte _COREINST24 = 217;

		// Token: 0x0400048B RID: 1163
		public const byte _NOP24 = 216;

		// Token: 0x0400048C RID: 1164
		public const byte _VISI24 = 215;

		// Token: 0x0400048D RID: 1165
		public const byte _RD2_BYTE_BUFFER = 214;

		// Token: 0x0400048E RID: 1166
		public const byte _RD2_BITS_BUFFER = 213;

		// Token: 0x0400048F RID: 1167
		public const byte _WRITE_BUFWORD_W = 212;

		// Token: 0x04000490 RID: 1168
		public const byte _WRITE_BUFBYTE_W = 211;

		// Token: 0x04000491 RID: 1169
		public const byte _CONST_WRITE_DL = 210;

		// Token: 0x04000492 RID: 1170
		public const byte _WRITE_BITS_LIT_HLD = 209;

		// Token: 0x04000493 RID: 1171
		public const byte _WRITE_BITS_BUF_HLD = 208;

		// Token: 0x04000494 RID: 1172
		public const byte _SET_AUX = 207;

		// Token: 0x04000495 RID: 1173
		public const byte _AUX_STATE_BUFFER = 206;

		// Token: 0x04000496 RID: 1174
		public const byte _I2C_START = 205;

		// Token: 0x04000497 RID: 1175
		public const byte _I2C_STOP = 204;

		// Token: 0x04000498 RID: 1176
		public const byte _I2C_WR_BYTE_LIT = 203;

		// Token: 0x04000499 RID: 1177
		public const byte _I2C_WR_BYTE_BUF = 202;

		// Token: 0x0400049A RID: 1178
		public const byte _I2C_RD_BYTE_ACK = 201;

		// Token: 0x0400049B RID: 1179
		public const byte _I2C_RD_BYTE_NACK = 200;

		// Token: 0x0400049C RID: 1180
		public const byte _SPI_WR_BYTE_LIT = 199;

		// Token: 0x0400049D RID: 1181
		public const byte _SPI_WR_BYTE_BUF = 198;

		// Token: 0x0400049E RID: 1182
		public const byte _SPI_RD_BYTE_BUF = 197;

		// Token: 0x0400049F RID: 1183
		public const byte _SPI_RDWR_BYTE_LIT = 196;

		// Token: 0x040004A0 RID: 1184
		public const byte _SPI_RDWR_BYTE_BUF = 195;

		// Token: 0x040004A1 RID: 1185
		public const byte _ICDSLAVE_RX_BL = 194;

		// Token: 0x040004A2 RID: 1186
		public const byte _ICDSLAVE_TX_LIT_BL = 193;

		// Token: 0x040004A3 RID: 1187
		public const byte _ICDSLAVE_TX_BUF_BL = 192;

		// Token: 0x040004A4 RID: 1188
		public const byte _MEASURE_PULSE = 191;

		// Token: 0x040004A5 RID: 1189
		public const byte _UNIO_TX = 190;

		// Token: 0x040004A6 RID: 1190
		public const byte _UNIO_TX_RX = 189;

		// Token: 0x040004A7 RID: 1191
		public const byte _JT2_SETMODE = 188;

		// Token: 0x040004A8 RID: 1192
		public const byte _JT2_SENDCMD = 187;

		// Token: 0x040004A9 RID: 1193
		public const byte _JT2_XFERDATA8_LIT = 186;

		// Token: 0x040004AA RID: 1194
		public const byte _JT2_XFERDATA32_LIT = 185;

		// Token: 0x040004AB RID: 1195
		public const byte _JT2_XFRFASTDAT_LIT = 184;

		// Token: 0x040004AC RID: 1196
		public const byte _JT2_XFRFASTDAT_BUF = 183;

		// Token: 0x040004AD RID: 1197
		public const byte _JT2_XFERINST_BUF = 182;

		// Token: 0x040004AE RID: 1198
		public const byte _JT2_GET_PE_RESP = 181;

		// Token: 0x040004AF RID: 1199
		public const byte _JT2_WAIT_PE_RESP = 180;

		// Token: 0x040004B0 RID: 1200
		public const int SEARCH_ALL_FAMILIES = 16777215;

		// Token: 0x040004B1 RID: 1201
		public const byte PROG_ENTRY = 0;

		// Token: 0x040004B2 RID: 1202
		public const byte PROG_EXIT = 1;

		// Token: 0x040004B3 RID: 1203
		public const byte RD_DEVID = 2;

		// Token: 0x040004B4 RID: 1204
		public const byte PROGMEM_RD = 3;

		// Token: 0x040004B5 RID: 1205
		public const byte ERASE_CHIP_PREP = 4;

		// Token: 0x040004B6 RID: 1206
		public const byte PROGMEM_ADDRSET = 5;

		// Token: 0x040004B7 RID: 1207
		public const byte PROGMEM_WR_PREP = 6;

		// Token: 0x040004B8 RID: 1208
		public const byte PROGMEM_WR = 7;

		// Token: 0x040004B9 RID: 1209
		public const byte EE_RD_PREP = 8;

		// Token: 0x040004BA RID: 1210
		public const byte EE_RD = 9;

		// Token: 0x040004BB RID: 1211
		public const byte EE_WR_PREP = 10;

		// Token: 0x040004BC RID: 1212
		public const byte EE_WR = 11;

		// Token: 0x040004BD RID: 1213
		public const byte CONFIG_RD_PREP = 12;

		// Token: 0x040004BE RID: 1214
		public const byte CONFIG_RD = 13;

		// Token: 0x040004BF RID: 1215
		public const byte CONFIG_WR_PREP = 14;

		// Token: 0x040004C0 RID: 1216
		public const byte CONFIG_WR = 15;

		// Token: 0x040004C1 RID: 1217
		public const byte USERID_RD_PREP = 16;

		// Token: 0x040004C2 RID: 1218
		public const byte USERID_RD = 17;

		// Token: 0x040004C3 RID: 1219
		public const byte USERID_WR_PREP = 18;

		// Token: 0x040004C4 RID: 1220
		public const byte USERID_WR = 19;

		// Token: 0x040004C5 RID: 1221
		public const byte OSSCAL_RD = 20;

		// Token: 0x040004C6 RID: 1222
		public const byte OSSCAL_WR = 21;

		// Token: 0x040004C7 RID: 1223
		public const byte ERASE_CHIP = 22;

		// Token: 0x040004C8 RID: 1224
		public const byte ERASE_PROGMEM = 23;

		// Token: 0x040004C9 RID: 1225
		public const byte ERASE_EE = 24;

		// Token: 0x040004CA RID: 1226
		public const byte ROW_ERASE = 26;

		// Token: 0x040004CB RID: 1227
		public const byte TESTMEM_RD = 27;

		// Token: 0x040004CC RID: 1228
		public const byte EEROW_ERASE = 28;

		// Token: 0x040004CD RID: 1229
		public const int OSCCAL_MASK = 7;

		// Token: 0x040004CE RID: 1230
		public const int PROTOCOL_CFG = 0;

		// Token: 0x040004CF RID: 1231
		public const int ADR_MASK_CFG = 1;

		// Token: 0x040004D0 RID: 1232
		public const int ADR_BITS_CFG = 2;

		// Token: 0x040004D1 RID: 1233
		public const int CS_PINS_CFG = 3;

		// Token: 0x040004D2 RID: 1234
		public const int I2C_BUS = 1;

		// Token: 0x040004D3 RID: 1235
		public const int SPI_BUS = 2;

		// Token: 0x040004D4 RID: 1236
		public const int MICROWIRE_BUS = 3;

		// Token: 0x040004D5 RID: 1237
		public const int UNIO_BUS = 4;

		// Token: 0x040004D6 RID: 1238
		public const bool READ_BIT = true;

		// Token: 0x040004D7 RID: 1239
		public const bool WRITE_BIT = false;

		// Token: 0x040004D8 RID: 1240
		public const uint FLASHW_STOP = 0U;

		// Token: 0x040004D9 RID: 1241
		public const uint FLASHW_CAPTION = 1U;

		// Token: 0x040004DA RID: 1242
		public const uint FLASHW_TRAY = 2U;

		// Token: 0x040004DB RID: 1243
		public const uint FLASHW_ALL = 3U;

		// Token: 0x040004DC RID: 1244
		public const uint FLASHW_TIMER = 4U;

		// Token: 0x040004DD RID: 1245
		public const uint FLASHW_TIMERNOFG = 12U;

		// Token: 0x040004DE RID: 1246
		public const byte ADC_CAL_L = 0;

		// Token: 0x040004DF RID: 1247
		public const byte ADC_CAL_H = 1;

		// Token: 0x040004E0 RID: 1248
		public const byte CPP_OFFSET = 2;

		// Token: 0x040004E1 RID: 1249
		public const byte CPP_CAL = 3;

		// Token: 0x040004E2 RID: 1250
		public const byte UNIT_ID = 240;

		// Token: 0x040004E3 RID: 1251
		public const uint P32_PROGRAM_FLASH_START_ADDR = 486539264U;

		// Token: 0x040004E4 RID: 1252
		public const uint P32_BOOT_FLASH_START_ADDR = 532676608U;

		// Token: 0x040004E5 RID: 1253
		public static uint[] BASELINE_CAL = new uint[]
		{
			3072U,
			37U,
			103U,
			104U,
			105U,
			102U,
			3326U,
			6U,
			1574U,
			2568U,
			1830U,
			2570U,
			112U,
			3202U,
			49U,
			752U,
			2575U,
			753U,
			2575U,
			3321U,
			48U,
			3272U,
			49U,
			1286U,
			0U,
			0U,
			0U,
			0U,
			0U,
			752U,
			2584U,
			0U,
			3321U,
			48U,
			0U,
			0U,
			0U,
			753U,
			2584U,
			1030U,
			2568U
		};

		// Token: 0x040004E6 RID: 1254
		public static uint[] MR16F676FAM_CAL = new uint[]
		{
			12288U,
			10245U,
			0U,
			0U,
			9U,
			5763U,
			144U,
			401U,
			415U,
			12542U,
			133U,
			4739U,
			12295U,
			153U,
			389U,
			6277U,
			10255U,
			7301U,
			10257U,
			416U,
			12418U,
			161U,
			2976U,
			10262U,
			2977U,
			10262U,
			12537U,
			160U,
			12488U,
			161U,
			5125U,
			0U,
			0U,
			0U,
			0U,
			0U,
			2976U,
			10271U,
			0U,
			12537U,
			160U,
			0U,
			0U,
			0U,
			2977U,
			10271U,
			4101U,
			10255U
		};

		// Token: 0x0200002D RID: 45
		public enum PICkit2USB
		{
			// Token: 0x040004E8 RID: 1256
			found,
			// Token: 0x040004E9 RID: 1257
			notFound,
			// Token: 0x040004EA RID: 1258
			writeError,
			// Token: 0x040004EB RID: 1259
			readError,
			// Token: 0x040004EC RID: 1260
			firmwareInvalid,
			// Token: 0x040004ED RID: 1261
			bootloader
		}

		// Token: 0x0200002E RID: 46
		public enum PICkit2PWR
		{
			// Token: 0x040004EF RID: 1263
			no_response,
			// Token: 0x040004F0 RID: 1264
			vdd_on,
			// Token: 0x040004F1 RID: 1265
			vdd_off,
			// Token: 0x040004F2 RID: 1266
			vdderror,
			// Token: 0x040004F3 RID: 1267
			vpperror,
			// Token: 0x040004F4 RID: 1268
			vddvpperrors,
			// Token: 0x040004F5 RID: 1269
			selfpowered,
			// Token: 0x040004F6 RID: 1270
			unpowered
		}

		// Token: 0x0200002F RID: 47
		public enum FileRead
		{
			// Token: 0x040004F8 RID: 1272
			success,
			// Token: 0x040004F9 RID: 1273
			failed,
			// Token: 0x040004FA RID: 1274
			noconfig,
			// Token: 0x040004FB RID: 1275
			partialcfg,
			// Token: 0x040004FC RID: 1276
			largemem
		}

		// Token: 0x02000030 RID: 48
		public enum StatusColor
		{
			// Token: 0x040004FE RID: 1278
			normal,
			// Token: 0x040004FF RID: 1279
			green,
			// Token: 0x04000500 RID: 1280
			yellow,
			// Token: 0x04000501 RID: 1281
			red
		}

		// Token: 0x02000031 RID: 49
		public enum VddTargetSelect
		{
			// Token: 0x04000503 RID: 1283
			auto,
			// Token: 0x04000504 RID: 1284
			pickit2,
			// Token: 0x04000505 RID: 1285
			target
		}
	}
}
