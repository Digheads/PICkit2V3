using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PICkit2V3
{
	public partial class FormPICkit2 : System.Windows.Forms.Form
	{
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new Container();
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(PICkit2V3.FormPICkit2));
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			this.menuStrip1 = new MenuStrip();
			this.fileToolStripMenuItem = new ToolStripMenuItem();
			this.importFileToolStripMenuItem = new ToolStripMenuItem();
			this.exportFileToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItem1 = new ToolStripSeparator();
			this.hex1ToolStripMenuItem = new ToolStripMenuItem();
			this.hex2ToolStripMenuItem = new ToolStripMenuItem();
			this.hex3ToolStripMenuItem = new ToolStripMenuItem();
			this.hex4ToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItem5 = new ToolStripSeparator();
			this.exitToolStripMenuItem = new ToolStripMenuItem();
			this.deviceToolStripMenuItem = new ToolStripMenuItem();
			this.programmerToolStripMenuItem = new ToolStripMenuItem();
			this.readDeviceToolStripMenuItem = new ToolStripMenuItem();
			this.writeDeviceToolStripMenuItem = new ToolStripMenuItem();
			this.verifyToolStripMenuItem = new ToolStripMenuItem();
			this.eraseToolStripMenuItem = new ToolStripMenuItem();
			this.blankCheckToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItem4 = new ToolStripSeparator();
			this.verifyOnWriteToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItemClearBuffersErase = new ToolStripMenuItem();
			this.MCLRtoolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItemSounds = new ToolStripMenuItem();
			this.writeOnPICkitButtonToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItem8 = new ToolStripSeparator();
			this.toolStripMenuItemManualSelect = new ToolStripMenuItem();
			this.picKit2GoToolStripMenuItem = new ToolStripMenuItem();
			this.toolsToolStripMenuItem = new ToolStripMenuItem();
			this.enableCodeProtectToolStripMenuItem = new ToolStripMenuItem();
			this.enableDataProtectStripMenuItem = new ToolStripMenuItem();
			this.setOSCCALToolStripMenuItem = new ToolStripMenuItem();
			this.calSetManuallyToolStripMenuItem = new ToolStripMenuItem();
			this.calAutoRegenerateToolStripMenuItem = new ToolStripMenuItem();
			this.targetPowerToolStripMenuItem = new ToolStripMenuItem();
			this.autoDetectToolStripMenuItem = new ToolStripMenuItem();
			this.forcePICkit2ToolStripMenuItem = new ToolStripMenuItem();
			this.forceTargetToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItemDisplayUnimplConfigAs = new ToolStripMenuItem();
			this.as0BitValueToolStripMenuItem = new ToolStripMenuItem();
			this.as1BitValueToolStripMenuItem = new ToolStripMenuItem();
			this.asReadOrImportedToolStripMenuItem = new ToolStripMenuItem();
			this.calibrateToolStripMenuItem = new ToolStripMenuItem();
			this.VppFirstToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItemLVPEnabled = new ToolStripMenuItem();
			this.fastProgrammingToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.UARTtoolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItemLogicTool = new ToolStripMenuItem();
			this.toolStripMenuItem6 = new ToolStripSeparator();
			this.checkCommunicationToolStripMenuItem = new ToolStripMenuItem();
			this.troubleshhotToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItemTestMemory = new ToolStripMenuItem();
			this.toolStripMenuItem2 = new ToolStripSeparator();
			this.downloadPICkit2FirmwareToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItemView = new ToolStripMenuItem();
			this.toolStripMenuItemSingleWindow = new ToolStripMenuItem();
			this.toolStripMenuItemMultiWindow = new ToolStripMenuItem();
			this.toolStripMenuItem7 = new ToolStripSeparator();
			this.toolStripMenuItemShowProgramMemory = new ToolStripMenuItem();
			this.toolStripMenuItemShowEEPROMData = new ToolStripMenuItem();
			this.toolStripMenuItem9 = new ToolStripSeparator();
			this.mainWindowAlwaysInFrontToolStripMenuItem = new ToolStripMenuItem();
			this.helpToolStripMenuItem = new ToolStripMenuItem();
			this.usersGuideToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItemProgToGo = new ToolStripMenuItem();
			this.toolStripMenuItemLogicToolUG = new ToolStripMenuItem();
			this.uG44pinToolStripMenuItem = new ToolStripMenuItem();
			this.lpcUsersGuideToolStripMenuItem = new ToolStripMenuItem();
			this.webPk2ToolStripMenuItem = new ToolStripMenuItem();
			this.readMeToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItem3 = new ToolStripSeparator();
			this.aboutToolStripMenuItem = new ToolStripMenuItem();
			this.testToolStripMenuItem = new ToolStripMenuItem();
			this.statusGroupBox = new GroupBox();
			this.labelOSSCALInvalid = new Label();
			this.checkBoxA2CS = new CheckBox();
			this.checkBoxA1CS = new CheckBox();
			this.checkBoxA0CS = new CheckBox();
			this.buttonShowIDMem = new Button();
			this.displayRev = new Label();
			this.labelCodeProtect = new Label();
			this.comboBoxSelectPart = new ComboBox();
			this.displayBandGap = new Label();
			this.labelBandGap = new Label();
			this.displayOSCCAL = new Label();
			this.labelOSCCAL = new Label();
			this.displayChecksum = new Label();
			this.displayUserIDs = new Label();
			this.displayDevice = new Label();
			this.labelConfig = new Label();
			this.dataGridConfigWords = new DataGridView();
			this.labelChecksum = new Label();
			this.labelUserIDs = new Label();
			this.labelDevice = new Label();
			this.pictureBoxLogo = new PictureBox();
			this.displayStatusWindow = new Label();
			this.buttonRead = new Button();
			this.progressBar1 = new ProgressBar();
			this.buttonWrite = new Button();
			this.buttonVerify = new Button();
			this.buttonErase = new Button();
			this.buttonBlankCheck = new Button();
			this.chkBoxVddOn = new CheckBox();
			this.numUpDnVdd = new NumericUpDown();
			this.groupBoxVdd = new GroupBox();
			this.checkBoxMCLR = new CheckBox();
			this.groupBoxProgMem = new GroupBox();
			this.dataGridProgramMemory = new DataGridView();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.toolStripMenuItemContextSelectAll = new ToolStripMenuItem();
			this.toolStripMenuItemContextCopy = new ToolStripMenuItem();
			this.displayDataSource = new Label();
			this.labelDataSource = new Label();
			this.comboBoxProgMemView = new ComboBox();
			this.checkBoxProgMemEnabled = new CheckBox();
			this.openHexFileDialog = new OpenFileDialog();
			this.saveHexFileDialog = new SaveFileDialog();
			this.openFWFile = new OpenFileDialog();
			this.timerButton = new Timer(this.components);
			this.groupBoxEEMem = new GroupBox();
			this.displayEEProgInfo = new Label();
			this.dataGridViewEEPROM = new DataGridView();
			this.comboBoxEE = new ComboBox();
			this.checkBoxEEMem = new CheckBox();
			this.buttonExportHex = new Button();
			this.pictureBox1 = new PictureBox();
			this.timerDLFW = new Timer(this.components);
			this.checkBoxAutoImportWrite = new CheckBox();
			this.timerAutoImportWrite = new Timer(this.components);
			this.checkBoxProgMemEnabledAlt = new CheckBox();
			this.checkBoxEEDATAMemoryEnabledAlt = new CheckBox();
			this.timerInitalUpdate = new Timer(this.components);
			this.labelLVP = new Label();
			this.labelConfig9 = new Label();
			this.menuStrip1.SuspendLayout();
			this.statusGroupBox.SuspendLayout();
			((ISupportInitialize)this.dataGridConfigWords).BeginInit();
			((ISupportInitialize)this.pictureBoxLogo).BeginInit();
			((ISupportInitialize)this.numUpDnVdd).BeginInit();
			this.groupBoxVdd.SuspendLayout();
			this.groupBoxProgMem.SuspendLayout();
			((ISupportInitialize)this.dataGridProgramMemory).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.groupBoxEEMem.SuspendLayout();
			((ISupportInitialize)this.dataGridViewEEPROM).BeginInit();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.menuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.fileToolStripMenuItem,
				this.deviceToolStripMenuItem,
				this.programmerToolStripMenuItem,
				this.toolsToolStripMenuItem,
				this.toolStripMenuItemView,
				this.helpToolStripMenuItem,
				this.testToolStripMenuItem
			});
			this.menuStrip1.Location = new Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new Padding(7, 2, 0, 2);
			this.menuStrip1.Size = new Size(538, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.importFileToolStripMenuItem,
				this.exportFileToolStripMenuItem,
				this.toolStripMenuItem1,
				this.hex1ToolStripMenuItem,
				this.hex2ToolStripMenuItem,
				this.hex3ToolStripMenuItem,
				this.hex4ToolStripMenuItem,
				this.toolStripMenuItem5,
				this.exitToolStripMenuItem
			});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new Size(35, 20);
			this.fileToolStripMenuItem.Text = "&File";
			this.importFileToolStripMenuItem.Enabled = false;
			this.importFileToolStripMenuItem.Name = "importFileToolStripMenuItem";
			this.importFileToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.I;
			this.importFileToolStripMenuItem.Size = new Size(177, 22);
			this.importFileToolStripMenuItem.Text = "&Import Hex";
			this.importFileToolStripMenuItem.Click += new EventHandler(this.MenuFileImportHex);
			this.exportFileToolStripMenuItem.Enabled = false;
			this.exportFileToolStripMenuItem.Name = "exportFileToolStripMenuItem";
			this.exportFileToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.E;
			this.exportFileToolStripMenuItem.Size = new Size(177, 22);
			this.exportFileToolStripMenuItem.Text = "&Export Hex";
			this.exportFileToolStripMenuItem.Click += new EventHandler(this.MenuFileExportHex);
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new Size(174, 6);
			this.hex1ToolStripMenuItem.Name = "hex1ToolStripMenuItem";
			this.hex1ToolStripMenuItem.ShortcutKeys = Keys.LButton | Keys.ShiftKey | Keys.Space | Keys.Control;
			this.hex1ToolStripMenuItem.Size = new Size(177, 22);
			this.hex1ToolStripMenuItem.Text = "&1 ";
			this.hex1ToolStripMenuItem.Visible = false;
			this.hex1ToolStripMenuItem.Click += new EventHandler(this.Hex1Click);
			this.hex2ToolStripMenuItem.Name = "hex2ToolStripMenuItem";
			this.hex2ToolStripMenuItem.ShortcutKeys = Keys.RButton | Keys.ShiftKey | Keys.Space | Keys.Control;
			this.hex2ToolStripMenuItem.Size = new Size(177, 22);
			this.hex2ToolStripMenuItem.Text = "&2 ";
			this.hex2ToolStripMenuItem.Visible = false;
			this.hex2ToolStripMenuItem.Click += new EventHandler(this.Hex2Click);
			this.hex3ToolStripMenuItem.Name = "hex3ToolStripMenuItem";
			this.hex3ToolStripMenuItem.ShortcutKeys = Keys.LButton | Keys.RButton | Keys.ShiftKey | Keys.Space | Keys.Control;
			this.hex3ToolStripMenuItem.Size = new Size(177, 22);
			this.hex3ToolStripMenuItem.Text = "&3";
			this.hex3ToolStripMenuItem.Visible = false;
			this.hex3ToolStripMenuItem.Click += new EventHandler(this.Hex3Click);
			this.hex4ToolStripMenuItem.Name = "hex4ToolStripMenuItem";
			this.hex4ToolStripMenuItem.ShortcutKeys = Keys.MButton | Keys.ShiftKey | Keys.Space | Keys.Control;
			this.hex4ToolStripMenuItem.Size = new Size(177, 22);
			this.hex4ToolStripMenuItem.Text = "&4";
			this.hex4ToolStripMenuItem.Visible = false;
			this.hex4ToolStripMenuItem.Click += new EventHandler(this.Hex4Click);
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new Size(174, 6);
			this.toolStripMenuItem5.Visible = false;
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Q;
			this.exitToolStripMenuItem.Size = new Size(177, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new EventHandler(this.FileMenuExit);
			this.deviceToolStripMenuItem.Enabled = false;
			this.deviceToolStripMenuItem.Name = "deviceToolStripMenuItem";
			this.deviceToolStripMenuItem.Size = new Size(84, 20);
			this.deviceToolStripMenuItem.Text = "&Device Family";
			this.deviceToolStripMenuItem.DropDownItemClicked += new ToolStripItemClickedEventHandler(this.DeviceFamilyClick);
			this.programmerToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.readDeviceToolStripMenuItem,
				this.writeDeviceToolStripMenuItem,
				this.verifyToolStripMenuItem,
				this.eraseToolStripMenuItem,
				this.blankCheckToolStripMenuItem,
				this.toolStripMenuItem4,
				this.verifyOnWriteToolStripMenuItem,
				this.toolStripMenuItemClearBuffersErase,
				this.MCLRtoolStripMenuItem,
				this.toolStripMenuItemSounds,
				this.writeOnPICkitButtonToolStripMenuItem,
				this.toolStripMenuItem8,
				this.toolStripMenuItemManualSelect,
				this.picKit2GoToolStripMenuItem
			});
			this.programmerToolStripMenuItem.Name = "programmerToolStripMenuItem";
			this.programmerToolStripMenuItem.Size = new Size(77, 20);
			this.programmerToolStripMenuItem.Text = "&Programmer";
			this.readDeviceToolStripMenuItem.Enabled = false;
			this.readDeviceToolStripMenuItem.Name = "readDeviceToolStripMenuItem";
			this.readDeviceToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.R;
			this.readDeviceToolStripMenuItem.Size = new Size(234, 22);
			this.readDeviceToolStripMenuItem.Text = "&Read Device";
			this.readDeviceToolStripMenuItem.Click += new EventHandler(this.ReadDevice);
			this.writeDeviceToolStripMenuItem.Enabled = false;
			this.writeDeviceToolStripMenuItem.Name = "writeDeviceToolStripMenuItem";
			this.writeDeviceToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.W;
			this.writeDeviceToolStripMenuItem.Size = new Size(234, 22);
			this.writeDeviceToolStripMenuItem.Text = "&Write Device";
			this.writeDeviceToolStripMenuItem.Click += new EventHandler(this.WriteDevice);
			this.verifyToolStripMenuItem.Enabled = false;
			this.verifyToolStripMenuItem.Name = "verifyToolStripMenuItem";
			this.verifyToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Y;
			this.verifyToolStripMenuItem.Size = new Size(234, 22);
			this.verifyToolStripMenuItem.Text = "&Verify";
			this.verifyToolStripMenuItem.Click += new EventHandler(this.VerifyDevice);
			this.eraseToolStripMenuItem.Enabled = false;
			this.eraseToolStripMenuItem.Name = "eraseToolStripMenuItem";
			this.eraseToolStripMenuItem.Size = new Size(234, 22);
			this.eraseToolStripMenuItem.Text = "&Erase";
			this.eraseToolStripMenuItem.Click += new EventHandler(this.EraseDevice);
			this.blankCheckToolStripMenuItem.Enabled = false;
			this.blankCheckToolStripMenuItem.Name = "blankCheckToolStripMenuItem";
			this.blankCheckToolStripMenuItem.Size = new Size(234, 22);
			this.blankCheckToolStripMenuItem.Text = "&Blank Check";
			this.blankCheckToolStripMenuItem.Click += new EventHandler(this.BlankCheck);
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new Size(231, 6);
			this.verifyOnWriteToolStripMenuItem.Checked = true;
			this.verifyOnWriteToolStripMenuItem.CheckOnClick = true;
			this.verifyOnWriteToolStripMenuItem.CheckState = CheckState.Checked;
			this.verifyOnWriteToolStripMenuItem.Name = "verifyOnWriteToolStripMenuItem";
			this.verifyOnWriteToolStripMenuItem.Size = new Size(234, 22);
			this.verifyOnWriteToolStripMenuItem.Text = "Verify on Write";
			this.toolStripMenuItemClearBuffersErase.Checked = true;
			this.toolStripMenuItemClearBuffersErase.CheckOnClick = true;
			this.toolStripMenuItemClearBuffersErase.CheckState = CheckState.Checked;
			this.toolStripMenuItemClearBuffersErase.Name = "toolStripMenuItemClearBuffersErase";
			this.toolStripMenuItemClearBuffersErase.Size = new Size(234, 22);
			this.toolStripMenuItemClearBuffersErase.Text = "Clear Memory Buffers on Erase";
			this.MCLRtoolStripMenuItem.Enabled = false;
			this.MCLRtoolStripMenuItem.Name = "MCLRtoolStripMenuItem";
			this.MCLRtoolStripMenuItem.Size = new Size(234, 22);
			this.MCLRtoolStripMenuItem.Text = "Hold Device in Reset";
			this.MCLRtoolStripMenuItem.Click += new EventHandler(this.MCLRtoolStripMenuItem_Click);
			this.toolStripMenuItemSounds.Name = "toolStripMenuItemSounds";
			this.toolStripMenuItemSounds.Size = new Size(234, 22);
			this.toolStripMenuItemSounds.Text = "Alert Sounds...";
			this.toolStripMenuItemSounds.Click += new EventHandler(this.ToolStripMenuItemSounds_Click);
			this.writeOnPICkitButtonToolStripMenuItem.CheckOnClick = true;
			this.writeOnPICkitButtonToolStripMenuItem.Enabled = false;
			this.writeOnPICkitButtonToolStripMenuItem.Name = "writeOnPICkitButtonToolStripMenuItem";
			this.writeOnPICkitButtonToolStripMenuItem.Size = new Size(234, 22);
			this.writeOnPICkitButtonToolStripMenuItem.Text = "Write on PICkit Button";
			this.writeOnPICkitButtonToolStripMenuItem.Click += new EventHandler(this.WriteOnButton);
			this.toolStripMenuItem8.Name = "toolStripMenuItem8";
			this.toolStripMenuItem8.Size = new Size(231, 6);
			this.toolStripMenuItemManualSelect.CheckOnClick = true;
			this.toolStripMenuItemManualSelect.Name = "toolStripMenuItemManualSelect";
			this.toolStripMenuItemManualSelect.Size = new Size(234, 22);
			this.toolStripMenuItemManualSelect.Text = "Manual Device Select";
			this.toolStripMenuItemManualSelect.Click += new EventHandler(this.ToolStripMenuItemManualSelect_Click);
			this.picKit2GoToolStripMenuItem.Enabled = false;
			this.picKit2GoToolStripMenuItem.Name = "pICkit2GoToolStripMenuItem";
			this.picKit2GoToolStripMenuItem.Size = new Size(234, 22);
			this.picKit2GoToolStripMenuItem.Text = "PICkit 2 Programmer-To-Go...";
			this.picKit2GoToolStripMenuItem.Click += new EventHandler(this.PICkit2GoToolStripMenuItem_Click);
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.enableCodeProtectToolStripMenuItem,
				this.enableDataProtectStripMenuItem,
				this.setOSCCALToolStripMenuItem,
				this.targetPowerToolStripMenuItem,
				this.toolStripMenuItemDisplayUnimplConfigAs,
				this.calibrateToolStripMenuItem,
				this.VppFirstToolStripMenuItem,
				this.toolStripMenuItemLVPEnabled,
				this.fastProgrammingToolStripMenuItem,
				this.toolStripSeparator1,
				this.UARTtoolStripMenuItem,
				this.toolStripMenuItemLogicTool,
				this.toolStripMenuItem6,
				this.checkCommunicationToolStripMenuItem,
				this.troubleshhotToolStripMenuItem,
				this.toolStripMenuItemTestMemory,
				this.toolStripMenuItem2,
				this.downloadPICkit2FirmwareToolStripMenuItem
			});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new Size(44, 20);
			this.toolsToolStripMenuItem.Text = "&Tools";
			this.enableCodeProtectToolStripMenuItem.CheckOnClick = true;
			this.enableCodeProtectToolStripMenuItem.Name = "enableCodeProtectToolStripMenuItem";
			this.enableCodeProtectToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.P;
			this.enableCodeProtectToolStripMenuItem.Size = new Size(261, 22);
			this.enableCodeProtectToolStripMenuItem.Text = "Enable &Code Protect";
			this.enableCodeProtectToolStripMenuItem.Click += new EventHandler(this.CodeProtect);
			this.enableDataProtectStripMenuItem.CheckOnClick = true;
			this.enableDataProtectStripMenuItem.Enabled = false;
			this.enableDataProtectStripMenuItem.Name = "enableDataProtectStripMenuItem";
			this.enableDataProtectStripMenuItem.ShortcutKeys = Keys.Control | Keys.D;
			this.enableDataProtectStripMenuItem.Size = new Size(261, 22);
			this.enableDataProtectStripMenuItem.Text = "Enable &Data Protect";
			this.enableDataProtectStripMenuItem.Click += new EventHandler(this.DataProtect);
			this.setOSCCALToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.calSetManuallyToolStripMenuItem,
				this.calAutoRegenerateToolStripMenuItem
			});
			this.setOSCCALToolStripMenuItem.Name = "setOSCCALToolStripMenuItem";
			this.setOSCCALToolStripMenuItem.Size = new Size(261, 22);
			this.setOSCCALToolStripMenuItem.Text = "&OSCCAL";
			this.calSetManuallyToolStripMenuItem.Name = "calSetManuallyToolStripMenuItem";
			this.calSetManuallyToolStripMenuItem.Size = new Size(168, 22);
			this.calSetManuallyToolStripMenuItem.Text = "Set Manually";
			this.calSetManuallyToolStripMenuItem.Click += new EventHandler(this.SetOSCCAL);
			this.calAutoRegenerateToolStripMenuItem.Name = "calAutoRegenerateToolStripMenuItem";
			this.calAutoRegenerateToolStripMenuItem.Size = new Size(168, 22);
			this.calAutoRegenerateToolStripMenuItem.Text = "Auto Regenerate";
			this.calAutoRegenerateToolStripMenuItem.Click += new EventHandler(this.CalAutoRegenerateToolStripMenuItem_Click);
			this.targetPowerToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.autoDetectToolStripMenuItem,
				this.forcePICkit2ToolStripMenuItem,
				this.forceTargetToolStripMenuItem
			});
			this.targetPowerToolStripMenuItem.Name = "targetPowerToolStripMenuItem";
			this.targetPowerToolStripMenuItem.Size = new Size(261, 22);
			this.targetPowerToolStripMenuItem.Text = "Target &VDD Source";
			this.autoDetectToolStripMenuItem.Checked = true;
			this.autoDetectToolStripMenuItem.CheckOnClick = true;
			this.autoDetectToolStripMenuItem.CheckState = CheckState.Checked;
			this.autoDetectToolStripMenuItem.Name = "autoDetectToolStripMenuItem";
			this.autoDetectToolStripMenuItem.Size = new Size(152, 22);
			this.autoDetectToolStripMenuItem.Text = "&Auto-Detect";
			this.autoDetectToolStripMenuItem.Click += new EventHandler(this.MenuVddAuto);
			this.forcePICkit2ToolStripMenuItem.CheckOnClick = true;
			this.forcePICkit2ToolStripMenuItem.Name = "forcePICkit2ToolStripMenuItem";
			this.forcePICkit2ToolStripMenuItem.Size = new Size(152, 22);
			this.forcePICkit2ToolStripMenuItem.Text = "Force &PICkit 2";
			this.forcePICkit2ToolStripMenuItem.Click += new EventHandler(this.MenuVddPk2);
			this.forceTargetToolStripMenuItem.CheckOnClick = true;
			this.forceTargetToolStripMenuItem.Name = "forceTargetToolStripMenuItem";
			this.forceTargetToolStripMenuItem.Size = new Size(152, 22);
			this.forceTargetToolStripMenuItem.Text = "Force &Target";
			this.forceTargetToolStripMenuItem.Click += new EventHandler(this.MenuVddTarget);
			this.toolStripMenuItemDisplayUnimplConfigAs.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.as0BitValueToolStripMenuItem,
				this.as1BitValueToolStripMenuItem,
				this.asReadOrImportedToolStripMenuItem
			});
			this.toolStripMenuItemDisplayUnimplConfigAs.Name = "toolStripMenuItemDisplayUnimplConfigAs";
			this.toolStripMenuItemDisplayUnimplConfigAs.Size = new Size(261, 22);
			this.toolStripMenuItemDisplayUnimplConfigAs.Text = "Display Unimplemented Config Bits";
			this.as0BitValueToolStripMenuItem.Checked = true;
			this.as0BitValueToolStripMenuItem.CheckOnClick = true;
			this.as0BitValueToolStripMenuItem.CheckState = CheckState.Checked;
			this.as0BitValueToolStripMenuItem.Name = "as0BitValueToolStripMenuItem";
			this.as0BitValueToolStripMenuItem.Size = new Size(180, 22);
			this.as0BitValueToolStripMenuItem.Text = "As '0' bit value";
			this.as0BitValueToolStripMenuItem.Click += new EventHandler(this.As0BitValueToolStripMenuItem_Click);
			this.as1BitValueToolStripMenuItem.CheckOnClick = true;
			this.as1BitValueToolStripMenuItem.Name = "as1BitValueToolStripMenuItem";
			this.as1BitValueToolStripMenuItem.Size = new Size(180, 22);
			this.as1BitValueToolStripMenuItem.Text = "As '1' bit value";
			this.as1BitValueToolStripMenuItem.Click += new EventHandler(this.As1BitValueToolStripMenuItem_Click);
			this.asReadOrImportedToolStripMenuItem.CheckOnClick = true;
			this.asReadOrImportedToolStripMenuItem.Name = "asReadOrImportedToolStripMenuItem";
			this.asReadOrImportedToolStripMenuItem.Size = new Size(180, 22);
			this.asReadOrImportedToolStripMenuItem.Text = "As read or imported";
			this.asReadOrImportedToolStripMenuItem.Click += new EventHandler(this.AsReadOrImportedToolStripMenuItem_Click);
			this.calibrateToolStripMenuItem.Name = "calibrateToolStripMenuItem";
			this.calibrateToolStripMenuItem.Size = new Size(261, 22);
			this.calibrateToolStripMenuItem.Text = "Calibrate VDD && Set Unit ID...";
			this.calibrateToolStripMenuItem.Click += new EventHandler(this.CalibrateToolStripMenuItem_Click);
			this.VppFirstToolStripMenuItem.CheckOnClick = true;
			this.VppFirstToolStripMenuItem.Enabled = false;
			this.VppFirstToolStripMenuItem.Name = "VppFirstToolStripMenuItem";
			this.VppFirstToolStripMenuItem.Size = new Size(261, 22);
			this.VppFirstToolStripMenuItem.Text = "&Use VPP First Program Entry";
			this.VppFirstToolStripMenuItem.CheckedChanged += new EventHandler(this.VppFirstToolStripMenuItem_Click);
			this.toolStripMenuItemLVPEnabled.CheckOnClick = true;
			this.toolStripMenuItemLVPEnabled.Enabled = false;
			this.toolStripMenuItemLVPEnabled.Name = "toolStripMenuItemLVPEnabled";
			this.toolStripMenuItemLVPEnabled.Size = new Size(261, 22);
			this.toolStripMenuItemLVPEnabled.Text = "Use &LVP Program Entry";
			this.toolStripMenuItemLVPEnabled.CheckedChanged += new EventHandler(this.ToolStripMenuItemLVPEnabled_CheckedChanged);
			this.fastProgrammingToolStripMenuItem.Checked = true;
			this.fastProgrammingToolStripMenuItem.CheckOnClick = true;
			this.fastProgrammingToolStripMenuItem.CheckState = CheckState.Checked;
			this.fastProgrammingToolStripMenuItem.Name = "fastProgrammingToolStripMenuItem";
			this.fastProgrammingToolStripMenuItem.Size = new Size(261, 22);
			this.fastProgrammingToolStripMenuItem.Text = "&Fast Programming";
			this.fastProgrammingToolStripMenuItem.Click += new EventHandler(this.ProgrammingSpeed);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(258, 6);
			this.UARTtoolStripMenuItem.Name = "UARTtoolStripMenuItem";
			this.UARTtoolStripMenuItem.Size = new Size(261, 22);
			this.UARTtoolStripMenuItem.Text = "UART Tool...";
			this.UARTtoolStripMenuItem.Click += new EventHandler(this.UARTtoolStripMenuItem_Click);
			this.toolStripMenuItemLogicTool.Name = "toolStripMenuItemLogicTool";
			this.toolStripMenuItemLogicTool.Size = new Size(261, 22);
			this.toolStripMenuItemLogicTool.Text = "Logic Tool...";
			this.toolStripMenuItemLogicTool.Click += new EventHandler(this.ToolStripMenuItemLogicTool_Click);
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			this.toolStripMenuItem6.Size = new Size(258, 6);
			this.checkCommunicationToolStripMenuItem.Name = "checkCommunicationToolStripMenuItem";
			this.checkCommunicationToolStripMenuItem.Size = new Size(261, 22);
			this.checkCommunicationToolStripMenuItem.Text = "&Check Communication";
			this.checkCommunicationToolStripMenuItem.Click += new EventHandler(this.CheckCommunication);
			this.troubleshhotToolStripMenuItem.Name = "troubleshhotToolStripMenuItem";
			this.troubleshhotToolStripMenuItem.Size = new Size(261, 22);
			this.troubleshhotToolStripMenuItem.Text = "Troubleshoot...";
			this.troubleshhotToolStripMenuItem.Click += new EventHandler(this.TroubleshhotToolStripMenuItem_Click);
			this.toolStripMenuItemTestMemory.Name = "toolStripMenuItemTestMemory";
			this.toolStripMenuItemTestMemory.ShortcutKeys = Keys.Control | Keys.T;
			this.toolStripMenuItemTestMemory.Size = new Size(261, 22);
			this.toolStripMenuItemTestMemory.Text = "Test Memory";
			this.toolStripMenuItemTestMemory.Visible = false;
			this.toolStripMenuItemTestMemory.Click += new EventHandler(this.ToolStripMenuItemTestMemory_Click);
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new Size(258, 6);
			this.downloadPICkit2FirmwareToolStripMenuItem.Enabled = false;
			this.downloadPICkit2FirmwareToolStripMenuItem.Name = "downloadPICkit2FirmwareToolStripMenuItem";
			this.downloadPICkit2FirmwareToolStripMenuItem.Size = new Size(261, 22);
			this.downloadPICkit2FirmwareToolStripMenuItem.Text = "Download PICkit 2 Operating System";
			this.downloadPICkit2FirmwareToolStripMenuItem.Click += new EventHandler(this.DownloadPk2Firmware);
			this.toolStripMenuItemView.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.toolStripMenuItemSingleWindow,
				this.toolStripMenuItemMultiWindow,
				this.toolStripMenuItem7,
				this.toolStripMenuItemShowProgramMemory,
				this.toolStripMenuItemShowEEPROMData,
				this.toolStripMenuItem9,
				this.mainWindowAlwaysInFrontToolStripMenuItem
			});
			this.toolStripMenuItemView.Name = "toolStripMenuItemView";
			this.toolStripMenuItemView.Size = new Size(41, 20);
			this.toolStripMenuItemView.Text = "View";
			this.toolStripMenuItemSingleWindow.Checked = true;
			this.toolStripMenuItemSingleWindow.CheckState = CheckState.Checked;
			this.toolStripMenuItemSingleWindow.Name = "toolStripMenuItemSingleWindow";
			this.toolStripMenuItemSingleWindow.Size = new Size(261, 22);
			this.toolStripMenuItemSingleWindow.Text = "Single Window";
			this.toolStripMenuItemSingleWindow.Click += new EventHandler(this.ToolStripMenuItemSingleWindow_Click);
			this.toolStripMenuItemMultiWindow.Name = "toolStripMenuItemMultiWindow";
			this.toolStripMenuItemMultiWindow.Size = new Size(261, 22);
			this.toolStripMenuItemMultiWindow.Text = "Multi-Window";
			this.toolStripMenuItemMultiWindow.Click += new EventHandler(this.ToolStripMenuItemMultiWindow_Click);
			this.toolStripMenuItem7.Name = "toolStripMenuItem7";
			this.toolStripMenuItem7.Size = new Size(258, 6);
			this.toolStripMenuItemShowProgramMemory.Enabled = false;
			this.toolStripMenuItemShowProgramMemory.Name = "toolStripMenuItemShowProgramMemory";
			this.toolStripMenuItemShowProgramMemory.Size = new Size(261, 22);
			this.toolStripMenuItemShowProgramMemory.Text = "Show Program Memory";
			this.toolStripMenuItemShowProgramMemory.Click += new EventHandler(this.ToolStripMenuItemShowProgramMemory_Click);
			this.toolStripMenuItemShowEEPROMData.Enabled = false;
			this.toolStripMenuItemShowEEPROMData.Name = "toolStripMenuItemShowEEPROMData";
			this.toolStripMenuItemShowEEPROMData.Size = new Size(261, 22);
			this.toolStripMenuItemShowEEPROMData.Text = "Show EEPROM Data";
			this.toolStripMenuItemShowEEPROMData.Click += new EventHandler(this.ToolStripMenuItemShowEEPROMData_Click);
			this.toolStripMenuItem9.Name = "toolStripMenuItem9";
			this.toolStripMenuItem9.Size = new Size(258, 6);
			this.mainWindowAlwaysInFrontToolStripMenuItem.CheckOnClick = true;
			this.mainWindowAlwaysInFrontToolStripMenuItem.Enabled = false;
			this.mainWindowAlwaysInFrontToolStripMenuItem.Name = "mainWindowAlwaysInFrontToolStripMenuItem";
			this.mainWindowAlwaysInFrontToolStripMenuItem.Size = new Size(261, 22);
			this.mainWindowAlwaysInFrontToolStripMenuItem.Text = "Associate / Memory Displays in Front";
			this.mainWindowAlwaysInFrontToolStripMenuItem.Click += new EventHandler(this.MainWindowAlwaysInFrontToolStripMenuItem_Click);
			this.helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.usersGuideToolStripMenuItem,
				this.toolStripMenuItemProgToGo,
				this.toolStripMenuItemLogicToolUG,
				this.uG44pinToolStripMenuItem,
				this.lpcUsersGuideToolStripMenuItem,
				this.webPk2ToolStripMenuItem,
				this.readMeToolStripMenuItem,
				this.toolStripMenuItem3,
				this.aboutToolStripMenuItem
			});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new Size(40, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			this.usersGuideToolStripMenuItem.Name = "usersGuideToolStripMenuItem";
			this.usersGuideToolStripMenuItem.Size = new Size(206, 22);
			this.usersGuideToolStripMenuItem.Text = "PICkit 2 &User's Guide";
			this.usersGuideToolStripMenuItem.Click += new EventHandler(this.LaunchUsersGuide);
			this.toolStripMenuItemProgToGo.Name = "toolStripMenuItemProgToGo";
			this.toolStripMenuItemProgToGo.Size = new Size(206, 22);
			this.toolStripMenuItemProgToGo.Text = "Programmer-To-Go Guide";
			this.toolStripMenuItemProgToGo.Click += new EventHandler(this.ToolStripMenuItemProgToGo_Click);
			this.toolStripMenuItemLogicToolUG.Name = "toolStripMenuItemLogicToolUG";
			this.toolStripMenuItemLogicToolUG.Size = new Size(206, 22);
			this.toolStripMenuItemLogicToolUG.Text = "Logic Tool User Guide";
			this.toolStripMenuItemLogicToolUG.Click += new EventHandler(this.ToolStripMenuItemLogicToolUG_Click);
			this.uG44pinToolStripMenuItem.Name = "uG44pinToolStripMenuItem";
			this.uG44pinToolStripMenuItem.Size = new Size(206, 22);
			this.uG44pinToolStripMenuItem.Text = "44-Pin Demo Board Guide";
			this.uG44pinToolStripMenuItem.Click += new EventHandler(this.UG44pinToolStripMenuItem_Click);
			this.lpcUsersGuideToolStripMenuItem.Name = "lpcUsersGuideToolStripMenuItem";
			this.lpcUsersGuideToolStripMenuItem.Size = new Size(206, 22);
			this.lpcUsersGuideToolStripMenuItem.Text = "LPC Demo Board Guide";
			this.lpcUsersGuideToolStripMenuItem.Click += new EventHandler(this.LaunchLPCDemoGuide);
			this.webPk2ToolStripMenuItem.Name = "webPk2ToolStripMenuItem";
			this.webPk2ToolStripMenuItem.Size = new Size(206, 22);
			this.webPk2ToolStripMenuItem.Text = "PICkit 2 on the web";
			this.webPk2ToolStripMenuItem.Click += new EventHandler(this.PICkit2OnTheWeb);
			this.readMeToolStripMenuItem.Name = "readMeToolStripMenuItem";
			this.readMeToolStripMenuItem.Size = new Size(206, 22);
			this.readMeToolStripMenuItem.Text = "&ReadMe";
			this.readMeToolStripMenuItem.Click += new EventHandler(this.LaunchReadMe);
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new Size(203, 6);
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new Size(206, 22);
			this.aboutToolStripMenuItem.Text = "&About";
			this.aboutToolStripMenuItem.Click += new EventHandler(this.ClickAbout);
			this.statusGroupBox.Controls.Add(this.labelConfig9);
			this.statusGroupBox.Controls.Add(this.labelLVP);
			this.statusGroupBox.Controls.Add(this.labelOSSCALInvalid);
			this.statusGroupBox.Controls.Add(this.checkBoxA2CS);
			this.statusGroupBox.Controls.Add(this.checkBoxA1CS);
			this.statusGroupBox.Controls.Add(this.checkBoxA0CS);
			this.statusGroupBox.Controls.Add(this.buttonShowIDMem);
			this.statusGroupBox.Controls.Add(this.displayRev);
			this.statusGroupBox.Controls.Add(this.labelCodeProtect);
			this.statusGroupBox.Controls.Add(this.comboBoxSelectPart);
			this.statusGroupBox.Controls.Add(this.displayBandGap);
			this.statusGroupBox.Controls.Add(this.labelBandGap);
			this.statusGroupBox.Controls.Add(this.displayOSCCAL);
			this.statusGroupBox.Controls.Add(this.labelOSCCAL);
			this.statusGroupBox.Controls.Add(this.displayChecksum);
			this.statusGroupBox.Controls.Add(this.displayUserIDs);
			this.statusGroupBox.Controls.Add(this.displayDevice);
			this.statusGroupBox.Controls.Add(this.labelConfig);
			this.statusGroupBox.Controls.Add(this.dataGridConfigWords);
			this.statusGroupBox.Controls.Add(this.labelChecksum);
			this.statusGroupBox.Controls.Add(this.labelUserIDs);
			this.statusGroupBox.Controls.Add(this.labelDevice);
			this.statusGroupBox.Cursor = Cursors.Default;
			this.statusGroupBox.ForeColor = SystemColors.ControlText;
			this.statusGroupBox.Location = new Point(12, 27);
			this.statusGroupBox.Margin = new Padding(2);
			this.statusGroupBox.Name = "statusGroupBox";
			this.statusGroupBox.Padding = new Padding(2);
			this.statusGroupBox.Size = new Size(514, 102);
			this.statusGroupBox.TabIndex = 1;
			this.statusGroupBox.TabStop = false;
			this.statusGroupBox.Text = "Device Configuration";
			this.labelOSSCALInvalid.AutoSize = true;
			this.labelOSSCALInvalid.ForeColor = Color.Red;
			this.labelOSSCALInvalid.Location = new Point(284, 61);
			this.labelOSSCALInvalid.Margin = new Padding(2, 0, 2, 0);
			this.labelOSSCALInvalid.Name = "labelOSSCALInvalid";
			this.labelOSSCALInvalid.Size = new Size(68, 13);
			this.labelOSSCALInvalid.TabIndex = 22;
			this.labelOSSCALInvalid.Text = "Invalid Value";
			this.labelOSSCALInvalid.Visible = false;
			this.checkBoxA2CS.AutoSize = true;
			this.checkBoxA2CS.Enabled = false;
			this.checkBoxA2CS.Location = new Point(411, 47);
			this.checkBoxA2CS.Margin = new Padding(2);
			this.checkBoxA2CS.Name = "checkBoxA2CS";
			this.checkBoxA2CS.Size = new Size(96, 17);
			this.checkBoxA2CS.TabIndex = 21;
			this.checkBoxA2CS.Text = "A2 Chip Select";
			this.checkBoxA2CS.UseVisualStyleBackColor = true;
			this.checkBoxA2CS.Visible = false;
			this.checkBoxA1CS.AutoSize = true;
			this.checkBoxA1CS.Enabled = false;
			this.checkBoxA1CS.Location = new Point(411, 31);
			this.checkBoxA1CS.Margin = new Padding(2);
			this.checkBoxA1CS.Name = "checkBoxA1CS";
			this.checkBoxA1CS.Size = new Size(96, 17);
			this.checkBoxA1CS.TabIndex = 20;
			this.checkBoxA1CS.Text = "A1 Chip Select";
			this.checkBoxA1CS.UseVisualStyleBackColor = true;
			this.checkBoxA1CS.Visible = false;
			this.checkBoxA0CS.AutoSize = true;
			this.checkBoxA0CS.Enabled = false;
			this.checkBoxA0CS.Location = new Point(411, 15);
			this.checkBoxA0CS.Margin = new Padding(2);
			this.checkBoxA0CS.Name = "checkBoxA0CS";
			this.checkBoxA0CS.Size = new Size(96, 17);
			this.checkBoxA0CS.TabIndex = 19;
			this.checkBoxA0CS.Text = "A0 Chip Select";
			this.checkBoxA0CS.UseVisualStyleBackColor = true;
			this.checkBoxA0CS.Visible = false;
			this.buttonShowIDMem.Location = new Point(79, 46);
			this.buttonShowIDMem.Margin = new Padding(2);
			this.buttonShowIDMem.Name = "buttonShowIDMem";
			this.buttonShowIDMem.Size = new Size(65, 22);
			this.buttonShowIDMem.TabIndex = 15;
			this.buttonShowIDMem.Text = "Display";
			this.buttonShowIDMem.UseVisualStyleBackColor = true;
			this.buttonShowIDMem.Visible = false;
			this.buttonShowIDMem.Click += new EventHandler(this.ButtonShowIDMem_Click);
			this.displayRev.AutoSize = true;
			this.displayRev.Location = new Point(135, 75);
			this.displayRev.Margin = new Padding(2, 0, 2, 0);
			this.displayRev.Name = "displayRev";
			this.displayRev.Size = new Size(27, 13);
			this.displayRev.TabIndex = 14;
			this.displayRev.Text = "Rev";
			this.displayRev.Visible = false;
			this.labelCodeProtect.AutoSize = true;
			this.labelCodeProtect.ForeColor = Color.Red;
			this.labelCodeProtect.Location = new Point(242, 41);
			this.labelCodeProtect.Margin = new Padding(2, 0, 2, 0);
			this.labelCodeProtect.Name = "labelCodeProtect";
			this.labelCodeProtect.Size = new Size(69, 13);
			this.labelCodeProtect.TabIndex = 13;
			this.labelCodeProtect.Text = "Code Protect";
			this.labelCodeProtect.Visible = false;
			this.comboBoxSelectPart.BackColor = SystemColors.Info;
			this.comboBoxSelectPart.DropDownHeight = 212;
			this.comboBoxSelectPart.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxSelectPart.FormattingEnabled = true;
			this.comboBoxSelectPart.IntegralHeight = false;
			this.comboBoxSelectPart.Items.AddRange(new object[]
			{
				"-Select Device-"
			});
			this.comboBoxSelectPart.Location = new Point(79, 22);
			this.comboBoxSelectPart.Margin = new Padding(2);
			this.comboBoxSelectPart.Name = "comboBoxSelectPart";
			this.comboBoxSelectPart.Size = new Size(148, 21);
			this.comboBoxSelectPart.Sorted = true;
			this.comboBoxSelectPart.TabIndex = 12;
			this.comboBoxSelectPart.Visible = false;
			this.comboBoxSelectPart.SelectionChangeCommitted += new EventHandler(this.SelectPart);
			this.displayBandGap.AutoSize = true;
			this.displayBandGap.Location = new Point(439, 75);
			this.displayBandGap.Margin = new Padding(2, 0, 2, 0);
			this.displayBandGap.Name = "displayBandGap";
			this.displayBandGap.Size = new Size(31, 13);
			this.displayBandGap.TabIndex = 11;
			this.displayBandGap.Text = "0000";
			this.labelBandGap.AutoSize = true;
			this.labelBandGap.Location = new Point(379, 75);
			this.labelBandGap.Margin = new Padding(2, 0, 2, 0);
			this.labelBandGap.Name = "labelBandGap";
			this.labelBandGap.Size = new Size(55, 13);
			this.labelBandGap.TabIndex = 10;
			this.labelBandGap.Text = "BandGap:";
			this.displayOSCCAL.AutoSize = true;
			this.displayOSCCAL.Location = new Point(300, 75);
			this.displayOSCCAL.Margin = new Padding(2, 0, 2, 0);
			this.displayOSCCAL.Name = "displayOSCCAL";
			this.displayOSCCAL.Size = new Size(31, 13);
			this.displayOSCCAL.TabIndex = 9;
			this.displayOSCCAL.Text = "0000";
			this.labelOSCCAL.AutoSize = true;
			this.labelOSCCAL.Location = new Point(242, 75);
			this.labelOSCCAL.Margin = new Padding(2, 0, 2, 0);
			this.labelOSCCAL.Name = "labelOSCCAL";
			this.labelOSCCAL.Size = new Size(52, 13);
			this.labelOSCCAL.TabIndex = 8;
			this.labelOSCCAL.Text = "OSCCAL:";
			this.displayChecksum.AutoSize = true;
			this.displayChecksum.Location = new Point(76, 75);
			this.displayChecksum.Margin = new Padding(2, 0, 2, 0);
			this.displayChecksum.Name = "displayChecksum";
			this.displayChecksum.Size = new Size(31, 13);
			this.displayChecksum.TabIndex = 7;
			this.displayChecksum.Text = "0000";
			this.displayUserIDs.AutoSize = true;
			this.displayUserIDs.Location = new Point(76, 50);
			this.displayUserIDs.Margin = new Padding(2, 0, 2, 0);
			this.displayUserIDs.Name = "displayUserIDs";
			this.displayUserIDs.Size = new Size(64, 13);
			this.displayUserIDs.TabIndex = 6;
			this.displayUserIDs.Text = "00 00 00 00";
			this.displayDevice.AutoSize = true;
			this.displayDevice.Location = new Point(79, 25);
			this.displayDevice.Margin = new Padding(2, 0, 2, 0);
			this.displayDevice.Name = "displayDevice";
			this.displayDevice.Size = new Size(63, 13);
			this.displayDevice.TabIndex = 5;
			this.displayDevice.Text = "Not Present";
			this.labelConfig.AutoSize = true;
			this.labelConfig.Cursor = Cursors.Hand;
			this.labelConfig.Enabled = false;
			this.labelConfig.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Underline, GraphicsUnit.Point, 0);
			this.labelConfig.ForeColor = SystemColors.ActiveCaption;
			this.labelConfig.Location = new Point(242, 25);
			this.labelConfig.Margin = new Padding(2, 0, 2, 0);
			this.labelConfig.Name = "labelConfig";
			this.labelConfig.Size = new Size(72, 13);
			this.labelConfig.TabIndex = 4;
			this.labelConfig.Text = "Configuration:";
			this.labelConfig.Click += new EventHandler(this.LabelConfig_Click);
			this.dataGridConfigWords.AllowUserToAddRows = false;
			this.dataGridConfigWords.AllowUserToDeleteRows = false;
			this.dataGridConfigWords.AllowUserToResizeColumns = false;
			this.dataGridConfigWords.AllowUserToResizeRows = false;
			this.dataGridConfigWords.BackgroundColor = SystemColors.ControlDark;
			this.dataGridConfigWords.BorderStyle = BorderStyle.None;
			this.dataGridConfigWords.CellBorderStyle = DataGridViewCellBorderStyle.None;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = SystemColors.Control;
			dataGridViewCellStyle.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			dataGridViewCellStyle.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dataGridConfigWords.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dataGridConfigWords.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridConfigWords.ColumnHeadersVisible = false;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = SystemColors.Window;
			dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
			this.dataGridConfigWords.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridConfigWords.Enabled = false;
			this.dataGridConfigWords.GridColor = SystemColors.Control;
			this.dataGridConfigWords.Location = new Point(320, 24);
			this.dataGridConfigWords.Margin = new Padding(2);
			this.dataGridConfigWords.MultiSelect = false;
			this.dataGridConfigWords.Name = "dataGridConfigWords";
			this.dataGridConfigWords.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = SystemColors.Control;
			dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
			this.dataGridConfigWords.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridConfigWords.RowHeadersVisible = false;
			this.dataGridConfigWords.ScrollBars = ScrollBars.None;
			this.dataGridConfigWords.Size = new Size(160, 34);
			this.dataGridConfigWords.TabIndex = 3;
			this.labelChecksum.AutoSize = true;
			this.labelChecksum.Location = new Point(6, 75);
			this.labelChecksum.Margin = new Padding(2, 0, 2, 0);
			this.labelChecksum.Name = "labelChecksum";
			this.labelChecksum.Size = new Size(60, 13);
			this.labelChecksum.TabIndex = 2;
			this.labelChecksum.Text = "Checksum:";
			this.labelUserIDs.AutoSize = true;
			this.labelUserIDs.Location = new Point(6, 50);
			this.labelUserIDs.Margin = new Padding(2, 0, 2, 0);
			this.labelUserIDs.Name = "labelUserIDs";
			this.labelUserIDs.Size = new Size(51, 13);
			this.labelUserIDs.TabIndex = 1;
			this.labelUserIDs.Text = "User IDs:";
			this.labelDevice.AutoSize = true;
			this.labelDevice.Location = new Point(6, 25);
			this.labelDevice.Margin = new Padding(2, 0, 2, 0);
			this.labelDevice.Name = "labelDevice";
			this.labelDevice.Size = new Size(44, 13);
			this.labelDevice.TabIndex = 0;
			this.labelDevice.Text = "Device:";
			this.pictureBoxLogo.Image = (Image)resources.GetObject("pictureBoxLogo.Image");
			this.pictureBoxLogo.InitialImage = (Image)resources.GetObject("pictureBoxLogo.InitialImage");
			this.pictureBoxLogo.Location = new Point(370, 134);
			this.pictureBoxLogo.Margin = new Padding(2);
			this.pictureBoxLogo.Name = "pictureBoxLogo";
			this.pictureBoxLogo.Size = new Size(152, 42);
			this.pictureBoxLogo.TabIndex = 2;
			this.pictureBoxLogo.TabStop = false;
			this.displayStatusWindow.BackColor = SystemColors.Info;
			this.displayStatusWindow.BorderStyle = BorderStyle.Fixed3D;
			this.displayStatusWindow.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.displayStatusWindow.Location = new Point(12, 137);
			this.displayStatusWindow.Margin = new Padding(2, 0, 2, 0);
			this.displayStatusWindow.Name = "displayStatusWindow";
			this.displayStatusWindow.Size = new Size(342, 36);
			this.displayStatusWindow.TabIndex = 4;
			this.displayStatusWindow.Text = "Status\r\nWindow";
			this.buttonRead.Enabled = false;
			this.buttonRead.Location = new Point(12, 204);
			this.buttonRead.Margin = new Padding(2);
			this.buttonRead.Name = "buttonRead";
			this.buttonRead.Size = new Size(57, 26);
			this.buttonRead.TabIndex = 5;
			this.buttonRead.Text = "Read";
			this.buttonRead.UseVisualStyleBackColor = true;
			this.buttonRead.Click += new EventHandler(this.ReadDevice);
			this.progressBar1.BackColor = SystemColors.Control;
			this.progressBar1.Location = new Point(12, 182);
			this.progressBar1.Margin = new Padding(2);
			this.progressBar1.MarqueeAnimationSpeed = 50;
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new Size(342, 15);
			this.progressBar1.Style = ProgressBarStyle.Continuous;
			this.progressBar1.TabIndex = 6;
			this.buttonWrite.Enabled = false;
			this.buttonWrite.Location = new Point(75, 204);
			this.buttonWrite.Margin = new Padding(2);
			this.buttonWrite.Name = "buttonWrite";
			this.buttonWrite.Size = new Size(57, 26);
			this.buttonWrite.TabIndex = 7;
			this.buttonWrite.Text = "Write";
			this.buttonWrite.UseVisualStyleBackColor = true;
			this.buttonWrite.Click += new EventHandler(this.WriteDevice);
			this.buttonVerify.Enabled = false;
			this.buttonVerify.Location = new Point(138, 204);
			this.buttonVerify.Margin = new Padding(2);
			this.buttonVerify.Name = "buttonVerify";
			this.buttonVerify.Size = new Size(57, 26);
			this.buttonVerify.TabIndex = 8;
			this.buttonVerify.Text = "Verify";
			this.buttonVerify.UseVisualStyleBackColor = true;
			this.buttonVerify.Click += new EventHandler(this.VerifyDevice);
			this.buttonErase.Enabled = false;
			this.buttonErase.Location = new Point(201, 204);
			this.buttonErase.Margin = new Padding(2);
			this.buttonErase.Name = "buttonErase";
			this.buttonErase.Size = new Size(57, 26);
			this.buttonErase.TabIndex = 9;
			this.buttonErase.Text = "Erase";
			this.buttonErase.UseVisualStyleBackColor = true;
			this.buttonErase.Click += new EventHandler(this.EraseDevice);
			this.buttonBlankCheck.Enabled = false;
			this.buttonBlankCheck.Location = new Point(264, 204);
			this.buttonBlankCheck.Margin = new Padding(2);
			this.buttonBlankCheck.Name = "buttonBlankCheck";
			this.buttonBlankCheck.Size = new Size(91, 26);
			this.buttonBlankCheck.TabIndex = 10;
			this.buttonBlankCheck.Text = "Blank Check";
			this.buttonBlankCheck.UseVisualStyleBackColor = true;
			this.buttonBlankCheck.Click += new EventHandler(this.BlankCheck);
			this.chkBoxVddOn.AutoSize = true;
			this.chkBoxVddOn.Enabled = false;
			this.chkBoxVddOn.Location = new Point(15, 15);
			this.chkBoxVddOn.Margin = new Padding(2);
			this.chkBoxVddOn.Name = "chkBoxVddOn";
			this.chkBoxVddOn.Size = new Size(40, 17);
			this.chkBoxVddOn.TabIndex = 11;
			this.chkBoxVddOn.Text = "On";
			this.chkBoxVddOn.UseVisualStyleBackColor = true;
			this.chkBoxVddOn.Click += new EventHandler(this.GUIVddControl);
			this.numUpDnVdd.DecimalPlaces = 1;
			this.numUpDnVdd.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.numUpDnVdd.Increment = new decimal(new int[]
			{
				1,
				0,
				0,
				65536
			});
			this.numUpDnVdd.Location = new Point(97, 15);
			this.numUpDnVdd.Margin = new Padding(2);
			this.numUpDnVdd.Maximum = new decimal(new int[]
			{
				50,
				0,
				0,
				65536
			});
			this.numUpDnVdd.Minimum = new decimal(new int[]
			{
				25,
				0,
				0,
				65536
			});
			this.numUpDnVdd.Name = "numUpDnVDD";
			this.numUpDnVdd.Size = new Size(53, 26);
			this.numUpDnVdd.TabIndex = 14;
			this.numUpDnVdd.TextAlign = HorizontalAlignment.Center;
			this.numUpDnVdd.Value = new decimal(new int[]
			{
				25,
				0,
				0,
				65536
			});
			this.numUpDnVdd.ValueChanged += new EventHandler(this.GUIChangeVdd);
			this.groupBoxVdd.Controls.Add(this.checkBoxMCLR);
			this.groupBoxVdd.Controls.Add(this.numUpDnVdd);
			this.groupBoxVdd.Controls.Add(this.chkBoxVddOn);
			this.groupBoxVdd.ForeColor = SystemColors.ControlText;
			this.groupBoxVdd.Location = new Point(370, 179);
			this.groupBoxVdd.Margin = new Padding(2);
			this.groupBoxVdd.Name = "groupBoxVDD";
			this.groupBoxVdd.Padding = new Padding(2);
			this.groupBoxVdd.Size = new Size(156, 51);
			this.groupBoxVdd.TabIndex = 17;
			this.groupBoxVdd.TabStop = false;
			this.groupBoxVdd.Text = "VDD PICkit 2";
			this.checkBoxMCLR.AutoSize = true;
			this.checkBoxMCLR.Enabled = false;
			this.checkBoxMCLR.Location = new Point(15, 32);
			this.checkBoxMCLR.Margin = new Padding(2);
			this.checkBoxMCLR.Name = "checkBoxMCLR";
			this.checkBoxMCLR.Size = new Size(61, 17);
			this.checkBoxMCLR.TabIndex = 15;
			this.checkBoxMCLR.Text = "/MCLR";
			this.checkBoxMCLR.UseVisualStyleBackColor = true;
			this.checkBoxMCLR.Click += new EventHandler(this.MCLRtoolStripMenuItem_Click);
			this.groupBoxProgMem.Controls.Add(this.dataGridProgramMemory);
			this.groupBoxProgMem.Controls.Add(this.displayDataSource);
			this.groupBoxProgMem.Controls.Add(this.labelDataSource);
			this.groupBoxProgMem.Controls.Add(this.comboBoxProgMemView);
			this.groupBoxProgMem.Controls.Add(this.checkBoxProgMemEnabled);
			this.groupBoxProgMem.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.groupBoxProgMem.ForeColor = SystemColors.InfoText;
			this.groupBoxProgMem.Location = new Point(12, 236);
			this.groupBoxProgMem.Margin = new Padding(2);
			this.groupBoxProgMem.Name = "groupBoxProgMem";
			this.groupBoxProgMem.Padding = new Padding(2);
			this.groupBoxProgMem.Size = new Size(514, 259);
			this.groupBoxProgMem.TabIndex = 18;
			this.groupBoxProgMem.TabStop = false;
			this.groupBoxProgMem.Text = "Program Memory";
			this.dataGridProgramMemory.AllowUserToAddRows = false;
			this.dataGridProgramMemory.AllowUserToDeleteRows = false;
			this.dataGridProgramMemory.AllowUserToResizeColumns = false;
			this.dataGridProgramMemory.AllowUserToResizeRows = false;
			this.dataGridProgramMemory.BackgroundColor = SystemColors.Window;
			this.dataGridProgramMemory.CellBorderStyle = DataGridViewCellBorderStyle.None;
			this.dataGridProgramMemory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridProgramMemory.ColumnHeadersVisible = false;
			this.dataGridProgramMemory.ContextMenuStrip = this.contextMenuStrip1;
			this.dataGridProgramMemory.Enabled = false;
			this.dataGridProgramMemory.Location = new Point(6, 44);
			this.dataGridProgramMemory.Margin = new Padding(2);
			this.dataGridProgramMemory.Name = "dataGridProgramMemory";
			this.dataGridProgramMemory.RowHeadersVisible = false;
			this.dataGridProgramMemory.RowHeadersWidth = 75;
			this.dataGridProgramMemory.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dataGridProgramMemory.RowsDefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridProgramMemory.RowTemplate.Height = 17;
			this.dataGridProgramMemory.ScrollBars = ScrollBars.Vertical;
			this.dataGridProgramMemory.SelectionMode = DataGridViewSelectionMode.CellSelect;
			this.dataGridProgramMemory.Size = new Size(502, 208);
			this.dataGridProgramMemory.TabIndex = 4;
			this.dataGridProgramMemory.CellMouseDown += new DataGridViewCellMouseEventHandler(this.DataGridProgramMemory_CellMouseDown);
			this.dataGridProgramMemory.CellEndEdit += new DataGridViewCellEventHandler(this.ProgMemEdit);
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripMenuItemContextSelectAll,
				this.toolStripMenuItemContextCopy
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new Size(164, 48);
			this.contextMenuStrip1.ItemClicked += new ToolStripItemClickedEventHandler(this.ContextMenuStrip1_ItemClicked);
			this.toolStripMenuItemContextSelectAll.Name = "toolStripMenuItemContextSelectAll";
			this.toolStripMenuItemContextSelectAll.ShortcutKeyDisplayString = "Ctrl-A";
			this.toolStripMenuItemContextSelectAll.Size = new Size(163, 22);
			this.toolStripMenuItemContextSelectAll.Text = "Select All";
			this.toolStripMenuItemContextCopy.Name = "toolStripMenuItemContextCopy";
			this.toolStripMenuItemContextCopy.ShortcutKeyDisplayString = "Ctrl-C";
			this.toolStripMenuItemContextCopy.Size = new Size(163, 22);
			this.toolStripMenuItemContextCopy.Text = "Copy";
			this.displayDataSource.BorderStyle = BorderStyle.Fixed3D;
			this.displayDataSource.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.displayDataSource.Location = new Point(229, 19);
			this.displayDataSource.Margin = new Padding(2, 0, 2, 0);
			this.displayDataSource.Name = "displayDataSource";
			this.displayDataSource.Size = new Size(279, 16);
			this.displayDataSource.TabIndex = 3;
			this.displayDataSource.Text = "None (Empty/Erased)";
			this.displayDataSource.UseCompatibleTextRendering = true;
			this.labelDataSource.AutoSize = true;
			this.labelDataSource.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.labelDataSource.Location = new Point(176, 20);
			this.labelDataSource.Margin = new Padding(2, 0, 2, 0);
			this.labelDataSource.Name = "labelDataSource";
			this.labelDataSource.Size = new Size(51, 13);
			this.labelDataSource.TabIndex = 2;
			this.labelDataSource.Text = "Source:";
			this.comboBoxProgMemView.BackColor = SystemColors.Info;
			this.comboBoxProgMemView.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxProgMemView.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.comboBoxProgMemView.FormattingEnabled = true;
			this.comboBoxProgMemView.Items.AddRange(new object[]
			{
				"Hex Only",
				"Word ASCII",
				"Byte ASCII"
			});
			this.comboBoxProgMemView.Location = new Point(79, 17);
			this.comboBoxProgMemView.Margin = new Padding(2);
			this.comboBoxProgMemView.Name = "comboBoxProgMemView";
			this.comboBoxProgMemView.Size = new Size(91, 21);
			this.comboBoxProgMemView.TabIndex = 1;
			this.comboBoxProgMemView.SelectionChangeCommitted += new EventHandler(this.ProgMemViewChanged);
			this.checkBoxProgMemEnabled.AutoSize = true;
			this.checkBoxProgMemEnabled.Checked = true;
			this.checkBoxProgMemEnabled.CheckState = CheckState.Checked;
			this.checkBoxProgMemEnabled.Enabled = false;
			this.checkBoxProgMemEnabled.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.checkBoxProgMemEnabled.Location = new Point(6, 19);
			this.checkBoxProgMemEnabled.Margin = new Padding(2);
			this.checkBoxProgMemEnabled.Name = "checkBoxProgMemEnabled";
			this.checkBoxProgMemEnabled.Size = new Size(65, 17);
			this.checkBoxProgMemEnabled.TabIndex = 0;
			this.checkBoxProgMemEnabled.Text = "Enabled";
			this.checkBoxProgMemEnabled.UseVisualStyleBackColor = true;
			this.checkBoxProgMemEnabled.Click += new EventHandler(this.MemorySelectVerify);
			this.openHexFileDialog.DefaultExt = "hex";
			this.openHexFileDialog.Filter = "HEX files|*.hex|All files|*.*";
			this.openHexFileDialog.Title = "Import Hex File";
			this.openHexFileDialog.FileOk += new CancelEventHandler(this.ImportHexFile);
			this.saveHexFileDialog.DefaultExt = "hex";
			this.saveHexFileDialog.Filter = "Hex files|*.hex|All files|*.*";
			this.saveHexFileDialog.Title = "Export Hex File";
			this.saveHexFileDialog.FileOk += new CancelEventHandler(this.ExportHexFile);
			this.openFWFile.DefaultExt = "hex";
			this.openFWFile.Filter = "PICkit 2 OS|pk*.hex|All files|*.*";
			this.openFWFile.InitialDirectory = "c:\\Program Files\\Microchip\\PICkit 2 v2";
			this.openFWFile.Title = "Open PICkit 2 Operating System File";
			this.timerButton.Interval = 250;
			this.timerButton.Tick += new EventHandler(this.TimerGoesOff);
			this.groupBoxEEMem.Controls.Add(this.displayEEProgInfo);
			this.groupBoxEEMem.Controls.Add(this.dataGridViewEEPROM);
			this.groupBoxEEMem.Controls.Add(this.comboBoxEE);
			this.groupBoxEEMem.Controls.Add(this.checkBoxEEMem);
			this.groupBoxEEMem.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.groupBoxEEMem.ForeColor = SystemColors.InfoText;
			this.groupBoxEEMem.Location = new Point(12, 501);
			this.groupBoxEEMem.Margin = new Padding(2);
			this.groupBoxEEMem.Name = "groupBoxEEMem";
			this.groupBoxEEMem.Padding = new Padding(2);
			this.groupBoxEEMem.Size = new Size(399, 123);
			this.groupBoxEEMem.TabIndex = 19;
			this.groupBoxEEMem.TabStop = false;
			this.groupBoxEEMem.Text = "EEPROM Data";
			this.displayEEProgInfo.AutoSize = true;
			this.displayEEProgInfo.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.displayEEProgInfo.ForeColor = Color.Red;
			this.displayEEProgInfo.Location = new Point(186, 20);
			this.displayEEProgInfo.Margin = new Padding(2, 0, 2, 0);
			this.displayEEProgInfo.Name = "displayEEProgInfo";
			this.displayEEProgInfo.Size = new Size(206, 13);
			this.displayEEProgInfo.TabIndex = 7;
			this.displayEEProgInfo.Text = "Preserve EEPROM and User IDs on write.";
			this.displayEEProgInfo.Visible = false;
			this.dataGridViewEEPROM.AllowUserToAddRows = false;
			this.dataGridViewEEPROM.AllowUserToDeleteRows = false;
			this.dataGridViewEEPROM.AllowUserToResizeColumns = false;
			this.dataGridViewEEPROM.AllowUserToResizeRows = false;
			this.dataGridViewEEPROM.BackgroundColor = SystemColors.Window;
			this.dataGridViewEEPROM.CellBorderStyle = DataGridViewCellBorderStyle.None;
			this.dataGridViewEEPROM.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridViewEEPROM.ColumnHeadersVisible = false;
			this.dataGridViewEEPROM.ContextMenuStrip = this.contextMenuStrip1;
			this.dataGridViewEEPROM.Location = new Point(6, 44);
			this.dataGridViewEEPROM.Margin = new Padding(2);
			this.dataGridViewEEPROM.Name = "dataGridViewEEPROM";
			this.dataGridViewEEPROM.RowHeadersVisible = false;
			this.dataGridViewEEPROM.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dataGridViewEEPROM.RowsDefaultCellStyle = dataGridViewCellStyle5;
			this.dataGridViewEEPROM.RowTemplate.Height = 17;
			this.dataGridViewEEPROM.ScrollBars = ScrollBars.Vertical;
			this.dataGridViewEEPROM.SelectionMode = DataGridViewSelectionMode.CellSelect;
			this.dataGridViewEEPROM.Size = new Size(387, 72);
			this.dataGridViewEEPROM.TabIndex = 6;
			this.dataGridViewEEPROM.CellMouseDown += new DataGridViewCellMouseEventHandler(this.DataGridViewEEPROM_CellMouseDown);
			this.dataGridViewEEPROM.CellEndEdit += new DataGridViewCellEventHandler(this.EEPROMEdit);
			this.comboBoxEE.BackColor = SystemColors.Info;
			this.comboBoxEE.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxEE.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.comboBoxEE.FormattingEnabled = true;
			this.comboBoxEE.Items.AddRange(new object[]
			{
				"Hex Only",
				"Word ASCII",
				"Byte ASCII"
			});
			this.comboBoxEE.Location = new Point(79, 17);
			this.comboBoxEE.Margin = new Padding(2);
			this.comboBoxEE.Name = "comboBoxEE";
			this.comboBoxEE.Size = new Size(91, 21);
			this.comboBoxEE.TabIndex = 5;
			this.comboBoxEE.SelectionChangeCommitted += new EventHandler(this.ProgMemViewChanged);
			this.checkBoxEEMem.AutoSize = true;
			this.checkBoxEEMem.Checked = true;
			this.checkBoxEEMem.CheckState = CheckState.Checked;
			this.checkBoxEEMem.Enabled = false;
			this.checkBoxEEMem.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.checkBoxEEMem.Location = new Point(6, 19);
			this.checkBoxEEMem.Margin = new Padding(2);
			this.checkBoxEEMem.Name = "checkBoxEEMem";
			this.checkBoxEEMem.Size = new Size(65, 17);
			this.checkBoxEEMem.TabIndex = 0;
			this.checkBoxEEMem.Text = "Enabled";
			this.checkBoxEEMem.UseVisualStyleBackColor = true;
			this.checkBoxEEMem.Click += new EventHandler(this.MemorySelectVerify);
			this.buttonExportHex.Location = new Point(423, 545);
			this.buttonExportHex.Margin = new Padding(2);
			this.buttonExportHex.Name = "buttonExportHex";
			this.buttonExportHex.Size = new Size(103, 35);
			this.buttonExportHex.TabIndex = 21;
			this.buttonExportHex.Text = "Read Device +\r\nExport Hex File";
			this.buttonExportHex.UseVisualStyleBackColor = true;
			this.buttonExportHex.Click += new EventHandler(this.ButtonReadExport);
			this.pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
			this.pictureBox1.Location = new Point(423, 586);
			this.pictureBox1.Margin = new Padding(2);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Size(103, 38);
			this.pictureBox1.TabIndex = 22;
			this.pictureBox1.TabStop = false;
			this.timerDLFW.Tick += new EventHandler(this.AutoDownloadFW);
			this.checkBoxAutoImportWrite.Appearance = Appearance.Button;
			this.checkBoxAutoImportWrite.Location = new Point(423, 505);
			this.checkBoxAutoImportWrite.Margin = new Padding(2);
			this.checkBoxAutoImportWrite.Name = "checkBoxAutoImportWrite";
			this.checkBoxAutoImportWrite.Size = new Size(103, 35);
			this.checkBoxAutoImportWrite.TabIndex = 23;
			this.checkBoxAutoImportWrite.Text = "Auto Import Hex\r\n+ Write Device";
			this.checkBoxAutoImportWrite.TextAlign = ContentAlignment.MiddleCenter;
			this.checkBoxAutoImportWrite.UseVisualStyleBackColor = true;
			this.checkBoxAutoImportWrite.Click += new EventHandler(this.CheckBoxAutoImportWrite_Click);
			this.checkBoxAutoImportWrite.CheckedChanged += new EventHandler(this.CheckBoxAutoImportWrite_Changed);
			this.timerAutoImportWrite.Interval = 250;
			this.timerAutoImportWrite.Tick += new EventHandler(this.TimerAutoImportWrite_Tick);
			this.checkBoxProgMemEnabledAlt.AutoSize = true;
			this.checkBoxProgMemEnabledAlt.Checked = true;
			this.checkBoxProgMemEnabledAlt.CheckState = CheckState.Checked;
			this.checkBoxProgMemEnabledAlt.Location = new Point(16, 240);
			this.checkBoxProgMemEnabledAlt.Margin = new Padding(2);
			this.checkBoxProgMemEnabledAlt.Name = "checkBoxProgMemEnabledAlt";
			this.checkBoxProgMemEnabledAlt.Size = new Size(147, 17);
			this.checkBoxProgMemEnabledAlt.TabIndex = 24;
			this.checkBoxProgMemEnabledAlt.Text = "Program Memory Enabled";
			this.checkBoxProgMemEnabledAlt.UseVisualStyleBackColor = true;
			this.checkBoxProgMemEnabledAlt.Visible = false;
			this.checkBoxProgMemEnabledAlt.Click += new EventHandler(this.MemorySelectVerify);
			this.checkBoxEEDATAMemoryEnabledAlt.AutoSize = true;
			this.checkBoxEEDATAMemoryEnabledAlt.Checked = true;
			this.checkBoxEEDATAMemoryEnabledAlt.CheckState = CheckState.Checked;
			this.checkBoxEEDATAMemoryEnabledAlt.Location = new Point(16, 259);
			this.checkBoxEEDATAMemoryEnabledAlt.Margin = new Padding(2);
			this.checkBoxEEDATAMemoryEnabledAlt.Name = "checkBoxEEDATAMemoryEnabledAlt";
			this.checkBoxEEDATAMemoryEnabledAlt.Size = new Size(140, 17);
			this.checkBoxEEDATAMemoryEnabledAlt.TabIndex = 25;
			this.checkBoxEEDATAMemoryEnabledAlt.Text = "EEPROM Data Enabled";
			this.checkBoxEEDATAMemoryEnabledAlt.UseVisualStyleBackColor = true;
			this.checkBoxEEDATAMemoryEnabledAlt.Visible = false;
			this.checkBoxEEDATAMemoryEnabledAlt.Click += new EventHandler(this.MemorySelectVerify);
			this.timerInitalUpdate.Interval = 1;
			this.timerInitalUpdate.Tick += new EventHandler(this.TimerInitalUpdate_Tick);
			this.labelLVP.AutoSize = true;
			this.labelLVP.ForeColor = Color.Red;
			this.labelLVP.Location = new Point(200, 7);
			this.labelLVP.Margin = new Padding(2, 0, 2, 0);
			this.labelLVP.Name = "labelLVP";
			this.labelLVP.Size = new Size(27, 13);
			this.labelLVP.TabIndex = 23;
			this.labelLVP.Text = "LVP";
			this.labelLVP.Visible = false;
			this.labelConfig9.AutoSize = true;
			this.labelConfig9.Location = new Point(478, 43);
			this.labelConfig9.Margin = new Padding(2, 0, 2, 0);
			this.labelConfig9.Name = "labelConfig9";
			this.labelConfig9.Size = new Size(31, 13);
			this.labelConfig9.TabIndex = 24;
			this.labelConfig9.Text = "FFFF";
			this.labelConfig9.Visible = false;
			base.AutoScaleDimensions = new SizeF(96f, 96f);
			base.AutoScaleMode = AutoScaleMode.Dpi;
			this.BackColor = SystemColors.Control;
			base.ClientSize = new Size(538, 636);
			base.Controls.Add(this.checkBoxEEDATAMemoryEnabledAlt);
			base.Controls.Add(this.checkBoxProgMemEnabledAlt);
			base.Controls.Add(this.checkBoxAutoImportWrite);
			base.Controls.Add(this.pictureBox1);
			base.Controls.Add(this.buttonExportHex);
			base.Controls.Add(this.groupBoxEEMem);
			base.Controls.Add(this.groupBoxProgMem);
			base.Controls.Add(this.groupBoxVdd);
			base.Controls.Add(this.buttonBlankCheck);
			base.Controls.Add(this.buttonErase);
			base.Controls.Add(this.buttonVerify);
			base.Controls.Add(this.buttonWrite);
			base.Controls.Add(this.progressBar1);
			base.Controls.Add(this.buttonRead);
			base.Controls.Add(this.displayStatusWindow);
			base.Controls.Add(this.pictureBoxLogo);
			base.Controls.Add(this.statusGroupBox);
			base.Controls.Add(this.menuStrip1);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.Icon = (Icon)resources.GetObject("$this.Icon");
			base.MainMenuStrip = this.menuStrip1;
			base.Margin = new Padding(2);
			base.MaximizeBox = false;
			this.MaximumSize = new Size(544, 670);
			this.MinimumSize = new Size(544, 320);
			base.Name = "FormPICkit2";
			base.StartPosition = FormStartPosition.Manual;
			this.Text = "PICkit 2 Programmer";
			base.Move += new EventHandler(this.FormPICkit2_Move);
			base.FormClosing += new FormClosingEventHandler(this.PICkitFormClosing);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusGroupBox.ResumeLayout(false);
			this.statusGroupBox.PerformLayout();
			((ISupportInitialize)this.dataGridConfigWords).EndInit();
			((ISupportInitialize)this.pictureBoxLogo).EndInit();
			((ISupportInitialize)this.numUpDnVdd).EndInit();
			this.groupBoxVdd.ResumeLayout(false);
			this.groupBoxVdd.PerformLayout();
			this.groupBoxProgMem.ResumeLayout(false);
			this.groupBoxProgMem.PerformLayout();
			((ISupportInitialize)this.dataGridProgramMemory).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.groupBoxEEMem.ResumeLayout(false);
			this.groupBoxEEMem.PerformLayout();
			((ISupportInitialize)this.dataGridViewEEPROM).EndInit();
			((ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

        private IContainer components;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem importFileToolStripMenuItem;
        private ToolStripMenuItem exportFileToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem deviceToolStripMenuItem;
        private ToolStripMenuItem programmerToolStripMenuItem;
        private ToolStripMenuItem readDeviceToolStripMenuItem;
        private ToolStripMenuItem writeDeviceToolStripMenuItem;
        private ToolStripMenuItem verifyToolStripMenuItem;
        private ToolStripMenuItem eraseToolStripMenuItem;
        private ToolStripMenuItem blankCheckToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem enableCodeProtectToolStripMenuItem;
        private ToolStripMenuItem targetPowerToolStripMenuItem;
        private ToolStripMenuItem fastProgrammingToolStripMenuItem;
        private ToolStripMenuItem checkCommunicationToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem downloadPICkit2FirmwareToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem usersGuideToolStripMenuItem;
        private ToolStripMenuItem readMeToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private GroupBox statusGroupBox;
        private PictureBox pictureBoxLogo;
        private Label labelDevice;
        private Label labelChecksum;
        private Label labelUserIDs;
        private DataGridView dataGridConfigWords;
        private Label displayUserIDs;
        private Label displayDevice;
        private Label displayChecksum;
        private Label labelOSCCAL;
        private Label displayBandGap;
        private Label labelBandGap;
        private Label displayOSCCAL;
        private Label displayStatusWindow;
        private Button buttonRead;
        private ProgressBar progressBar1;
        private Button buttonWrite;
        private Button buttonVerify;
        private Button buttonErase;
        private Button buttonBlankCheck;
        private CheckBox chkBoxVddOn;
        private NumericUpDown numUpDnVdd;
        private GroupBox groupBoxVdd;
        private GroupBox groupBoxProgMem;
        private CheckBox checkBoxProgMemEnabled;
        private ComboBox comboBoxProgMemView;
        private Label labelDataSource;
        private Label displayDataSource;
        private DataGridView dataGridProgramMemory;
        private OpenFileDialog openHexFileDialog;
        private SaveFileDialog saveHexFileDialog;
        private ToolStripSeparator toolStripMenuItem4;
        private ToolStripMenuItem verifyOnWriteToolStripMenuItem;
        private OpenFileDialog openFWFile;
        private ToolStripMenuItem writeOnPICkitButtonToolStripMenuItem;
        private Timer timerButton;
        private GroupBox groupBoxEEMem;
        private Button buttonExportHex;
        private ComboBox comboBoxEE;
        private CheckBox checkBoxEEMem;
        private DataGridView dataGridViewEEPROM;
        private ToolStripMenuItem autoDetectToolStripMenuItem;
        private ToolStripMenuItem forcePICkit2ToolStripMenuItem;
        private ToolStripMenuItem forceTargetToolStripMenuItem;
        private PictureBox pictureBox1;
        private ComboBox comboBoxSelectPart;
        private Label labelCodeProtect;
        private Timer timerDLFW;
        private ToolStripMenuItem hex1ToolStripMenuItem;
        private ToolStripMenuItem hex2ToolStripMenuItem;
        private ToolStripMenuItem hex3ToolStripMenuItem;
        private ToolStripMenuItem hex4ToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem5;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem enableDataProtectStripMenuItem;
        private ToolStripMenuItem lpcUsersGuideToolStripMenuItem;
        private Label displayEEProgInfo;
        private ToolStripMenuItem setOSCCALToolStripMenuItem;
        private ToolStripMenuItem webPk2ToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem6;
        private ToolStripMenuItem troubleshhotToolStripMenuItem;
        private CheckBox checkBoxMCLR;
        private ToolStripMenuItem MCLRtoolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItemTestMemory;
        private CheckBox checkBoxAutoImportWrite;
        private Timer timerAutoImportWrite;
        private ToolStripMenuItem testToolStripMenuItem;
        private Label displayRev;
        private ToolStripMenuItem uG44pinToolStripMenuItem;
        private Button buttonShowIDMem;
        private ToolStripMenuItem VppFirstToolStripMenuItem;
        private CheckBox checkBoxA1CS;
        private CheckBox checkBoxA0CS;
        private CheckBox checkBoxA2CS;
        private ToolStripMenuItem calibrateToolStripMenuItem;
        private Label labelOSSCALInvalid;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem UARTtoolStripMenuItem;
        private CheckBox checkBoxProgMemEnabledAlt;
        private CheckBox checkBoxEEDATAMemoryEnabledAlt;
        private ToolStripMenuItem toolStripMenuItemView;
        private ToolStripMenuItem toolStripMenuItemSingleWindow;
        private ToolStripMenuItem toolStripMenuItemMultiWindow;
        private ToolStripSeparator toolStripMenuItem7;
        private ToolStripMenuItem toolStripMenuItemShowProgramMemory;
        private ToolStripMenuItem toolStripMenuItemShowEEPROMData;
        private ToolStripSeparator toolStripMenuItem8;
        private ToolStripMenuItem picKit2GoToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItemManualSelect;
        private ToolStripMenuItem toolStripMenuItemProgToGo;
        private ToolStripMenuItem toolStripMenuItemLogicTool;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem toolStripMenuItemContextSelectAll;
        private ToolStripMenuItem toolStripMenuItemContextCopy;
        private ToolStripMenuItem toolStripMenuItemLogicToolUG;
        private ToolStripMenuItem calSetManuallyToolStripMenuItem;
        private ToolStripMenuItem calAutoRegenerateToolStripMenuItem;
        private Timer timerInitalUpdate;
        private ToolStripSeparator toolStripMenuItem9;
        private ToolStripMenuItem mainWindowAlwaysInFrontToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItemClearBuffersErase;
        private ToolStripMenuItem toolStripMenuItemSounds;
        private ToolStripMenuItem toolStripMenuItemDisplayUnimplConfigAs;
        private ToolStripMenuItem as0BitValueToolStripMenuItem;
        private ToolStripMenuItem as1BitValueToolStripMenuItem;
        private ToolStripMenuItem asReadOrImportedToolStripMenuItem;
        private Label labelConfig;
        private ToolStripMenuItem toolStripMenuItemLVPEnabled;
        private Label labelLVP;
        private Label labelConfig9;
    }
}
