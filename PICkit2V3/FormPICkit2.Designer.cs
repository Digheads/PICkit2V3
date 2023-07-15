namespace PICkit2V3
{
	// Token: 0x02000010 RID: 16
	public partial class FormPICkit2 : global::System.Windows.Forms.Form
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x0002086A File Offset: 0x0001F86A
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0002088C File Offset: 0x0001F88C
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::PICkit2V3.FormPICkit2));
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new global::System.Windows.Forms.DataGridViewCellStyle();
			this.menuStrip1 = new global::System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.importFileToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.exportFileToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.hex1ToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.hex2ToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.hex3ToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.hex4ToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new global::System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.deviceToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.programmerToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.readDeviceToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.writeDeviceToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.verifyToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.eraseToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.blankCheckToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new global::System.Windows.Forms.ToolStripSeparator();
			this.verifyOnWriteToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemClearBuffersErase = new global::System.Windows.Forms.ToolStripMenuItem();
			this.MCLRtoolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSounds = new global::System.Windows.Forms.ToolStripMenuItem();
			this.writeOnPICkitButtonToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem8 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemManualSelect = new global::System.Windows.Forms.ToolStripMenuItem();
			this.pICkit2GoToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.enableCodeProtectToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.enableDataProtectStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.setOSCCALToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.calSetManuallyToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.calAutoRegenerateToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.targetPowerToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.autoDetectToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.forcePICkit2ToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.forceTargetToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDisplayUnimplConfigAs = new global::System.Windows.Forms.ToolStripMenuItem();
			this.as0BitValueToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.as1BitValueToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.asReadOrImportedToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.calibrateToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.VppFirstToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemLVPEnabled = new global::System.Windows.Forms.ToolStripMenuItem();
			this.fastProgrammingToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.UARTtoolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemLogicTool = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem6 = new global::System.Windows.Forms.ToolStripSeparator();
			this.checkCommunicationToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.troubleshhotToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemTestMemory = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new global::System.Windows.Forms.ToolStripSeparator();
			this.downloadPICkit2FirmwareToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemView = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSingleWindow = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemMultiWindow = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem7 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemShowProgramMemory = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemShowEEPROMData = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem9 = new global::System.Windows.Forms.ToolStripSeparator();
			this.mainWindowAlwaysInFrontToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.usersGuideToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemProgToGo = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemLogicToolUG = new global::System.Windows.Forms.ToolStripMenuItem();
			this.uG44pinToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.lpcUsersGuideToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.webPk2ToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.readMeToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new global::System.Windows.Forms.ToolStripSeparator();
			this.aboutToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.testToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.statusGroupBox = new global::System.Windows.Forms.GroupBox();
			this.labelOSSCALInvalid = new global::System.Windows.Forms.Label();
			this.checkBoxA2CS = new global::System.Windows.Forms.CheckBox();
			this.checkBoxA1CS = new global::System.Windows.Forms.CheckBox();
			this.checkBoxA0CS = new global::System.Windows.Forms.CheckBox();
			this.buttonShowIDMem = new global::System.Windows.Forms.Button();
			this.displayRev = new global::System.Windows.Forms.Label();
			this.labelCodeProtect = new global::System.Windows.Forms.Label();
			this.comboBoxSelectPart = new global::System.Windows.Forms.ComboBox();
			this.displayBandGap = new global::System.Windows.Forms.Label();
			this.labelBandGap = new global::System.Windows.Forms.Label();
			this.displayOSCCAL = new global::System.Windows.Forms.Label();
			this.labelOSCCAL = new global::System.Windows.Forms.Label();
			this.displayChecksum = new global::System.Windows.Forms.Label();
			this.displayUserIDs = new global::System.Windows.Forms.Label();
			this.displayDevice = new global::System.Windows.Forms.Label();
			this.labelConfig = new global::System.Windows.Forms.Label();
			this.dataGridConfigWords = new global::System.Windows.Forms.DataGridView();
			this.labelChecksum = new global::System.Windows.Forms.Label();
			this.labelUserIDs = new global::System.Windows.Forms.Label();
			this.labelDevice = new global::System.Windows.Forms.Label();
			this.pictureBoxLogo = new global::System.Windows.Forms.PictureBox();
			this.displayStatusWindow = new global::System.Windows.Forms.Label();
			this.buttonRead = new global::System.Windows.Forms.Button();
			this.progressBar1 = new global::System.Windows.Forms.ProgressBar();
			this.buttonWrite = new global::System.Windows.Forms.Button();
			this.buttonVerify = new global::System.Windows.Forms.Button();
			this.buttonErase = new global::System.Windows.Forms.Button();
			this.buttonBlankCheck = new global::System.Windows.Forms.Button();
			this.chkBoxVddOn = new global::System.Windows.Forms.CheckBox();
			this.numUpDnVDD = new global::System.Windows.Forms.NumericUpDown();
			this.groupBoxVDD = new global::System.Windows.Forms.GroupBox();
			this.checkBoxMCLR = new global::System.Windows.Forms.CheckBox();
			this.groupBoxProgMem = new global::System.Windows.Forms.GroupBox();
			this.dataGridProgramMemory = new global::System.Windows.Forms.DataGridView();
			this.contextMenuStrip1 = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemContextSelectAll = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemContextCopy = new global::System.Windows.Forms.ToolStripMenuItem();
			this.displayDataSource = new global::System.Windows.Forms.Label();
			this.labelDataSource = new global::System.Windows.Forms.Label();
			this.comboBoxProgMemView = new global::System.Windows.Forms.ComboBox();
			this.checkBoxProgMemEnabled = new global::System.Windows.Forms.CheckBox();
			this.openHexFileDialog = new global::System.Windows.Forms.OpenFileDialog();
			this.saveHexFileDialog = new global::System.Windows.Forms.SaveFileDialog();
			this.openFWFile = new global::System.Windows.Forms.OpenFileDialog();
			this.timerButton = new global::System.Windows.Forms.Timer(this.components);
			this.groupBoxEEMem = new global::System.Windows.Forms.GroupBox();
			this.displayEEProgInfo = new global::System.Windows.Forms.Label();
			this.dataGridViewEEPROM = new global::System.Windows.Forms.DataGridView();
			this.comboBoxEE = new global::System.Windows.Forms.ComboBox();
			this.checkBoxEEMem = new global::System.Windows.Forms.CheckBox();
			this.buttonExportHex = new global::System.Windows.Forms.Button();
			this.pictureBox1 = new global::System.Windows.Forms.PictureBox();
			this.timerDLFW = new global::System.Windows.Forms.Timer(this.components);
			this.checkBoxAutoImportWrite = new global::System.Windows.Forms.CheckBox();
			this.timerAutoImportWrite = new global::System.Windows.Forms.Timer(this.components);
			this.checkBoxProgMemEnabledAlt = new global::System.Windows.Forms.CheckBox();
			this.checkBoxEEDATAMemoryEnabledAlt = new global::System.Windows.Forms.CheckBox();
			this.timerInitalUpdate = new global::System.Windows.Forms.Timer(this.components);
			this.labelLVP = new global::System.Windows.Forms.Label();
			this.labelConfig9 = new global::System.Windows.Forms.Label();
			this.menuStrip1.SuspendLayout();
			this.statusGroupBox.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.dataGridConfigWords).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBoxLogo).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.numUpDnVDD).BeginInit();
			this.groupBoxVDD.SuspendLayout();
			this.groupBoxProgMem.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.dataGridProgramMemory).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.groupBoxEEMem.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.dataGridViewEEPROM).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.menuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.fileToolStripMenuItem,
				this.deviceToolStripMenuItem,
				this.programmerToolStripMenuItem,
				this.toolsToolStripMenuItem,
				this.toolStripMenuItemView,
				this.helpToolStripMenuItem,
				this.testToolStripMenuItem
			});
			this.menuStrip1.Location = new global::System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new global::System.Windows.Forms.Padding(7, 2, 0, 2);
			this.menuStrip1.Size = new global::System.Drawing.Size(538, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			this.fileToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
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
			this.fileToolStripMenuItem.Size = new global::System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "&File";
			this.importFileToolStripMenuItem.Enabled = false;
			this.importFileToolStripMenuItem.Name = "importFileToolStripMenuItem";
			this.importFileToolStripMenuItem.ShortcutKeys = (global::System.Windows.Forms.Keys)131145;
			this.importFileToolStripMenuItem.Size = new global::System.Drawing.Size(177, 22);
			this.importFileToolStripMenuItem.Text = "&Import Hex";
			this.importFileToolStripMenuItem.Click += new global::System.EventHandler(this.menuFileImportHex);
			this.exportFileToolStripMenuItem.Enabled = false;
			this.exportFileToolStripMenuItem.Name = "exportFileToolStripMenuItem";
			this.exportFileToolStripMenuItem.ShortcutKeys = (global::System.Windows.Forms.Keys)131141;
			this.exportFileToolStripMenuItem.Size = new global::System.Drawing.Size(177, 22);
			this.exportFileToolStripMenuItem.Text = "&Export Hex";
			this.exportFileToolStripMenuItem.Click += new global::System.EventHandler(this.menuFileExportHex);
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new global::System.Drawing.Size(174, 6);
			this.hex1ToolStripMenuItem.Name = "hex1ToolStripMenuItem";
			this.hex1ToolStripMenuItem.ShortcutKeys = (global::System.Windows.Forms.Keys.LButton | global::System.Windows.Forms.Keys.ShiftKey | global::System.Windows.Forms.Keys.Space | global::System.Windows.Forms.Keys.Control);
			this.hex1ToolStripMenuItem.Size = new global::System.Drawing.Size(177, 22);
			this.hex1ToolStripMenuItem.Text = "&1 ";
			this.hex1ToolStripMenuItem.Visible = false;
			this.hex1ToolStripMenuItem.Click += new global::System.EventHandler(this.hex1Click);
			this.hex2ToolStripMenuItem.Name = "hex2ToolStripMenuItem";
			this.hex2ToolStripMenuItem.ShortcutKeys = (global::System.Windows.Forms.Keys.RButton | global::System.Windows.Forms.Keys.ShiftKey | global::System.Windows.Forms.Keys.Space | global::System.Windows.Forms.Keys.Control);
			this.hex2ToolStripMenuItem.Size = new global::System.Drawing.Size(177, 22);
			this.hex2ToolStripMenuItem.Text = "&2 ";
			this.hex2ToolStripMenuItem.Visible = false;
			this.hex2ToolStripMenuItem.Click += new global::System.EventHandler(this.hex2Click);
			this.hex3ToolStripMenuItem.Name = "hex3ToolStripMenuItem";
			this.hex3ToolStripMenuItem.ShortcutKeys = (global::System.Windows.Forms.Keys.LButton | global::System.Windows.Forms.Keys.RButton | global::System.Windows.Forms.Keys.ShiftKey | global::System.Windows.Forms.Keys.Space | global::System.Windows.Forms.Keys.Control);
			this.hex3ToolStripMenuItem.Size = new global::System.Drawing.Size(177, 22);
			this.hex3ToolStripMenuItem.Text = "&3";
			this.hex3ToolStripMenuItem.Visible = false;
			this.hex3ToolStripMenuItem.Click += new global::System.EventHandler(this.hex3Click);
			this.hex4ToolStripMenuItem.Name = "hex4ToolStripMenuItem";
			this.hex4ToolStripMenuItem.ShortcutKeys = (global::System.Windows.Forms.Keys.MButton | global::System.Windows.Forms.Keys.ShiftKey | global::System.Windows.Forms.Keys.Space | global::System.Windows.Forms.Keys.Control);
			this.hex4ToolStripMenuItem.Size = new global::System.Drawing.Size(177, 22);
			this.hex4ToolStripMenuItem.Text = "&4";
			this.hex4ToolStripMenuItem.Visible = false;
			this.hex4ToolStripMenuItem.Click += new global::System.EventHandler(this.hex4Click);
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new global::System.Drawing.Size(174, 6);
			this.toolStripMenuItem5.Visible = false;
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.ShortcutKeys = (global::System.Windows.Forms.Keys)131153;
			this.exitToolStripMenuItem.Size = new global::System.Drawing.Size(177, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new global::System.EventHandler(this.fileMenuExit);
			this.deviceToolStripMenuItem.Enabled = false;
			this.deviceToolStripMenuItem.Name = "deviceToolStripMenuItem";
			this.deviceToolStripMenuItem.Size = new global::System.Drawing.Size(84, 20);
			this.deviceToolStripMenuItem.Text = "&Device Family";
			this.deviceToolStripMenuItem.DropDownItemClicked += new global::System.Windows.Forms.ToolStripItemClickedEventHandler(this.deviceFamilyClick);
			this.programmerToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
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
				this.pICkit2GoToolStripMenuItem
			});
			this.programmerToolStripMenuItem.Name = "programmerToolStripMenuItem";
			this.programmerToolStripMenuItem.Size = new global::System.Drawing.Size(77, 20);
			this.programmerToolStripMenuItem.Text = "&Programmer";
			this.readDeviceToolStripMenuItem.Enabled = false;
			this.readDeviceToolStripMenuItem.Name = "readDeviceToolStripMenuItem";
			this.readDeviceToolStripMenuItem.ShortcutKeys = (global::System.Windows.Forms.Keys)131154;
			this.readDeviceToolStripMenuItem.Size = new global::System.Drawing.Size(234, 22);
			this.readDeviceToolStripMenuItem.Text = "&Read Device";
			this.readDeviceToolStripMenuItem.Click += new global::System.EventHandler(this.readDevice);
			this.writeDeviceToolStripMenuItem.Enabled = false;
			this.writeDeviceToolStripMenuItem.Name = "writeDeviceToolStripMenuItem";
			this.writeDeviceToolStripMenuItem.ShortcutKeys = (global::System.Windows.Forms.Keys)131159;
			this.writeDeviceToolStripMenuItem.Size = new global::System.Drawing.Size(234, 22);
			this.writeDeviceToolStripMenuItem.Text = "&Write Device";
			this.writeDeviceToolStripMenuItem.Click += new global::System.EventHandler(this.writeDevice);
			this.verifyToolStripMenuItem.Enabled = false;
			this.verifyToolStripMenuItem.Name = "verifyToolStripMenuItem";
			this.verifyToolStripMenuItem.ShortcutKeys = (global::System.Windows.Forms.Keys)131161;
			this.verifyToolStripMenuItem.Size = new global::System.Drawing.Size(234, 22);
			this.verifyToolStripMenuItem.Text = "&Verify";
			this.verifyToolStripMenuItem.Click += new global::System.EventHandler(this.verifyDevice);
			this.eraseToolStripMenuItem.Enabled = false;
			this.eraseToolStripMenuItem.Name = "eraseToolStripMenuItem";
			this.eraseToolStripMenuItem.Size = new global::System.Drawing.Size(234, 22);
			this.eraseToolStripMenuItem.Text = "&Erase";
			this.eraseToolStripMenuItem.Click += new global::System.EventHandler(this.eraseDevice);
			this.blankCheckToolStripMenuItem.Enabled = false;
			this.blankCheckToolStripMenuItem.Name = "blankCheckToolStripMenuItem";
			this.blankCheckToolStripMenuItem.Size = new global::System.Drawing.Size(234, 22);
			this.blankCheckToolStripMenuItem.Text = "&Blank Check";
			this.blankCheckToolStripMenuItem.Click += new global::System.EventHandler(this.blankCheck);
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new global::System.Drawing.Size(231, 6);
			this.verifyOnWriteToolStripMenuItem.Checked = true;
			this.verifyOnWriteToolStripMenuItem.CheckOnClick = true;
			this.verifyOnWriteToolStripMenuItem.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.verifyOnWriteToolStripMenuItem.Name = "verifyOnWriteToolStripMenuItem";
			this.verifyOnWriteToolStripMenuItem.Size = new global::System.Drawing.Size(234, 22);
			this.verifyOnWriteToolStripMenuItem.Text = "Verify on Write";
			this.toolStripMenuItemClearBuffersErase.Checked = true;
			this.toolStripMenuItemClearBuffersErase.CheckOnClick = true;
			this.toolStripMenuItemClearBuffersErase.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.toolStripMenuItemClearBuffersErase.Name = "toolStripMenuItemClearBuffersErase";
			this.toolStripMenuItemClearBuffersErase.Size = new global::System.Drawing.Size(234, 22);
			this.toolStripMenuItemClearBuffersErase.Text = "Clear Memory Buffers on Erase";
			this.MCLRtoolStripMenuItem.Enabled = false;
			this.MCLRtoolStripMenuItem.Name = "MCLRtoolStripMenuItem";
			this.MCLRtoolStripMenuItem.Size = new global::System.Drawing.Size(234, 22);
			this.MCLRtoolStripMenuItem.Text = "Hold Device in Reset";
			this.MCLRtoolStripMenuItem.Click += new global::System.EventHandler(this.MCLRtoolStripMenuItem_Click);
			this.toolStripMenuItemSounds.Name = "toolStripMenuItemSounds";
			this.toolStripMenuItemSounds.Size = new global::System.Drawing.Size(234, 22);
			this.toolStripMenuItemSounds.Text = "Alert Sounds...";
			this.toolStripMenuItemSounds.Click += new global::System.EventHandler(this.toolStripMenuItemSounds_Click);
			this.writeOnPICkitButtonToolStripMenuItem.CheckOnClick = true;
			this.writeOnPICkitButtonToolStripMenuItem.Enabled = false;
			this.writeOnPICkitButtonToolStripMenuItem.Name = "writeOnPICkitButtonToolStripMenuItem";
			this.writeOnPICkitButtonToolStripMenuItem.Size = new global::System.Drawing.Size(234, 22);
			this.writeOnPICkitButtonToolStripMenuItem.Text = "Write on PICkit Button";
			this.writeOnPICkitButtonToolStripMenuItem.Click += new global::System.EventHandler(this.writeOnButton);
			this.toolStripMenuItem8.Name = "toolStripMenuItem8";
			this.toolStripMenuItem8.Size = new global::System.Drawing.Size(231, 6);
			this.toolStripMenuItemManualSelect.CheckOnClick = true;
			this.toolStripMenuItemManualSelect.Name = "toolStripMenuItemManualSelect";
			this.toolStripMenuItemManualSelect.Size = new global::System.Drawing.Size(234, 22);
			this.toolStripMenuItemManualSelect.Text = "Manual Device Select";
			this.toolStripMenuItemManualSelect.Click += new global::System.EventHandler(this.toolStripMenuItemManualSelect_Click);
			this.pICkit2GoToolStripMenuItem.Enabled = false;
			this.pICkit2GoToolStripMenuItem.Name = "pICkit2GoToolStripMenuItem";
			this.pICkit2GoToolStripMenuItem.Size = new global::System.Drawing.Size(234, 22);
			this.pICkit2GoToolStripMenuItem.Text = "PICkit 2 Programmer-To-Go...";
			this.pICkit2GoToolStripMenuItem.Click += new global::System.EventHandler(this.pICkit2GoToolStripMenuItem_Click);
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
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
			this.toolsToolStripMenuItem.Size = new global::System.Drawing.Size(44, 20);
			this.toolsToolStripMenuItem.Text = "&Tools";
			this.enableCodeProtectToolStripMenuItem.CheckOnClick = true;
			this.enableCodeProtectToolStripMenuItem.Name = "enableCodeProtectToolStripMenuItem";
			this.enableCodeProtectToolStripMenuItem.ShortcutKeys = (global::System.Windows.Forms.Keys)131152;
			this.enableCodeProtectToolStripMenuItem.Size = new global::System.Drawing.Size(261, 22);
			this.enableCodeProtectToolStripMenuItem.Text = "Enable &Code Protect";
			this.enableCodeProtectToolStripMenuItem.Click += new global::System.EventHandler(this.codeProtect);
			this.enableDataProtectStripMenuItem.CheckOnClick = true;
			this.enableDataProtectStripMenuItem.Enabled = false;
			this.enableDataProtectStripMenuItem.Name = "enableDataProtectStripMenuItem";
			this.enableDataProtectStripMenuItem.ShortcutKeys = (global::System.Windows.Forms.Keys)131140;
			this.enableDataProtectStripMenuItem.Size = new global::System.Drawing.Size(261, 22);
			this.enableDataProtectStripMenuItem.Text = "Enable &Data Protect";
			this.enableDataProtectStripMenuItem.Click += new global::System.EventHandler(this.dataProtect);
			this.setOSCCALToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.calSetManuallyToolStripMenuItem,
				this.calAutoRegenerateToolStripMenuItem
			});
			this.setOSCCALToolStripMenuItem.Name = "setOSCCALToolStripMenuItem";
			this.setOSCCALToolStripMenuItem.Size = new global::System.Drawing.Size(261, 22);
			this.setOSCCALToolStripMenuItem.Text = "&OSCCAL";
			this.calSetManuallyToolStripMenuItem.Name = "calSetManuallyToolStripMenuItem";
			this.calSetManuallyToolStripMenuItem.Size = new global::System.Drawing.Size(168, 22);
			this.calSetManuallyToolStripMenuItem.Text = "Set Manually";
			this.calSetManuallyToolStripMenuItem.Click += new global::System.EventHandler(this.setOSCCAL);
			this.calAutoRegenerateToolStripMenuItem.Name = "calAutoRegenerateToolStripMenuItem";
			this.calAutoRegenerateToolStripMenuItem.Size = new global::System.Drawing.Size(168, 22);
			this.calAutoRegenerateToolStripMenuItem.Text = "Auto Regenerate";
			this.calAutoRegenerateToolStripMenuItem.Click += new global::System.EventHandler(this.calAutoRegenerateToolStripMenuItem_Click);
			this.targetPowerToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.autoDetectToolStripMenuItem,
				this.forcePICkit2ToolStripMenuItem,
				this.forceTargetToolStripMenuItem
			});
			this.targetPowerToolStripMenuItem.Name = "targetPowerToolStripMenuItem";
			this.targetPowerToolStripMenuItem.Size = new global::System.Drawing.Size(261, 22);
			this.targetPowerToolStripMenuItem.Text = "Target &VDD Source";
			this.autoDetectToolStripMenuItem.Checked = true;
			this.autoDetectToolStripMenuItem.CheckOnClick = true;
			this.autoDetectToolStripMenuItem.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.autoDetectToolStripMenuItem.Name = "autoDetectToolStripMenuItem";
			this.autoDetectToolStripMenuItem.Size = new global::System.Drawing.Size(152, 22);
			this.autoDetectToolStripMenuItem.Text = "&Auto-Detect";
			this.autoDetectToolStripMenuItem.Click += new global::System.EventHandler(this.menuVDDAuto);
			this.forcePICkit2ToolStripMenuItem.CheckOnClick = true;
			this.forcePICkit2ToolStripMenuItem.Name = "forcePICkit2ToolStripMenuItem";
			this.forcePICkit2ToolStripMenuItem.Size = new global::System.Drawing.Size(152, 22);
			this.forcePICkit2ToolStripMenuItem.Text = "Force &PICkit 2";
			this.forcePICkit2ToolStripMenuItem.Click += new global::System.EventHandler(this.menuVDDPk2);
			this.forceTargetToolStripMenuItem.CheckOnClick = true;
			this.forceTargetToolStripMenuItem.Name = "forceTargetToolStripMenuItem";
			this.forceTargetToolStripMenuItem.Size = new global::System.Drawing.Size(152, 22);
			this.forceTargetToolStripMenuItem.Text = "Force &Target";
			this.forceTargetToolStripMenuItem.Click += new global::System.EventHandler(this.menuVDDTarget);
			this.toolStripMenuItemDisplayUnimplConfigAs.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.as0BitValueToolStripMenuItem,
				this.as1BitValueToolStripMenuItem,
				this.asReadOrImportedToolStripMenuItem
			});
			this.toolStripMenuItemDisplayUnimplConfigAs.Name = "toolStripMenuItemDisplayUnimplConfigAs";
			this.toolStripMenuItemDisplayUnimplConfigAs.Size = new global::System.Drawing.Size(261, 22);
			this.toolStripMenuItemDisplayUnimplConfigAs.Text = "Display Unimplemented Config Bits";
			this.as0BitValueToolStripMenuItem.Checked = true;
			this.as0BitValueToolStripMenuItem.CheckOnClick = true;
			this.as0BitValueToolStripMenuItem.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.as0BitValueToolStripMenuItem.Name = "as0BitValueToolStripMenuItem";
			this.as0BitValueToolStripMenuItem.Size = new global::System.Drawing.Size(180, 22);
			this.as0BitValueToolStripMenuItem.Text = "As '0' bit value";
			this.as0BitValueToolStripMenuItem.Click += new global::System.EventHandler(this.as0BitValueToolStripMenuItem_Click);
			this.as1BitValueToolStripMenuItem.CheckOnClick = true;
			this.as1BitValueToolStripMenuItem.Name = "as1BitValueToolStripMenuItem";
			this.as1BitValueToolStripMenuItem.Size = new global::System.Drawing.Size(180, 22);
			this.as1BitValueToolStripMenuItem.Text = "As '1' bit value";
			this.as1BitValueToolStripMenuItem.Click += new global::System.EventHandler(this.as1BitValueToolStripMenuItem_Click);
			this.asReadOrImportedToolStripMenuItem.CheckOnClick = true;
			this.asReadOrImportedToolStripMenuItem.Name = "asReadOrImportedToolStripMenuItem";
			this.asReadOrImportedToolStripMenuItem.Size = new global::System.Drawing.Size(180, 22);
			this.asReadOrImportedToolStripMenuItem.Text = "As read or imported";
			this.asReadOrImportedToolStripMenuItem.Click += new global::System.EventHandler(this.asReadOrImportedToolStripMenuItem_Click);
			this.calibrateToolStripMenuItem.Name = "calibrateToolStripMenuItem";
			this.calibrateToolStripMenuItem.Size = new global::System.Drawing.Size(261, 22);
			this.calibrateToolStripMenuItem.Text = "Calibrate VDD && Set Unit ID...";
			this.calibrateToolStripMenuItem.Click += new global::System.EventHandler(this.calibrateToolStripMenuItem_Click);
			this.VppFirstToolStripMenuItem.CheckOnClick = true;
			this.VppFirstToolStripMenuItem.Enabled = false;
			this.VppFirstToolStripMenuItem.Name = "VppFirstToolStripMenuItem";
			this.VppFirstToolStripMenuItem.Size = new global::System.Drawing.Size(261, 22);
			this.VppFirstToolStripMenuItem.Text = "&Use VPP First Program Entry";
			this.VppFirstToolStripMenuItem.CheckedChanged += new global::System.EventHandler(this.VppFirstToolStripMenuItem_Click);
			this.toolStripMenuItemLVPEnabled.CheckOnClick = true;
			this.toolStripMenuItemLVPEnabled.Enabled = false;
			this.toolStripMenuItemLVPEnabled.Name = "toolStripMenuItemLVPEnabled";
			this.toolStripMenuItemLVPEnabled.Size = new global::System.Drawing.Size(261, 22);
			this.toolStripMenuItemLVPEnabled.Text = "Use &LVP Program Entry";
			this.toolStripMenuItemLVPEnabled.CheckedChanged += new global::System.EventHandler(this.toolStripMenuItemLVPEnabled_CheckedChanged);
			this.fastProgrammingToolStripMenuItem.Checked = true;
			this.fastProgrammingToolStripMenuItem.CheckOnClick = true;
			this.fastProgrammingToolStripMenuItem.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.fastProgrammingToolStripMenuItem.Name = "fastProgrammingToolStripMenuItem";
			this.fastProgrammingToolStripMenuItem.Size = new global::System.Drawing.Size(261, 22);
			this.fastProgrammingToolStripMenuItem.Text = "&Fast Programming";
			this.fastProgrammingToolStripMenuItem.Click += new global::System.EventHandler(this.programmingSpeed);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(258, 6);
			this.UARTtoolStripMenuItem.Name = "UARTtoolStripMenuItem";
			this.UARTtoolStripMenuItem.Size = new global::System.Drawing.Size(261, 22);
			this.UARTtoolStripMenuItem.Text = "UART Tool...";
			this.UARTtoolStripMenuItem.Click += new global::System.EventHandler(this.UARTtoolStripMenuItem_Click);
			this.toolStripMenuItemLogicTool.Name = "toolStripMenuItemLogicTool";
			this.toolStripMenuItemLogicTool.Size = new global::System.Drawing.Size(261, 22);
			this.toolStripMenuItemLogicTool.Text = "Logic Tool...";
			this.toolStripMenuItemLogicTool.Click += new global::System.EventHandler(this.toolStripMenuItemLogicTool_Click);
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			this.toolStripMenuItem6.Size = new global::System.Drawing.Size(258, 6);
			this.checkCommunicationToolStripMenuItem.Name = "checkCommunicationToolStripMenuItem";
			this.checkCommunicationToolStripMenuItem.Size = new global::System.Drawing.Size(261, 22);
			this.checkCommunicationToolStripMenuItem.Text = "&Check Communication";
			this.checkCommunicationToolStripMenuItem.Click += new global::System.EventHandler(this.checkCommunication);
			this.troubleshhotToolStripMenuItem.Name = "troubleshhotToolStripMenuItem";
			this.troubleshhotToolStripMenuItem.Size = new global::System.Drawing.Size(261, 22);
			this.troubleshhotToolStripMenuItem.Text = "Troubleshoot...";
			this.troubleshhotToolStripMenuItem.Click += new global::System.EventHandler(this.troubleshhotToolStripMenuItem_Click);
			this.toolStripMenuItemTestMemory.Name = "toolStripMenuItemTestMemory";
			this.toolStripMenuItemTestMemory.ShortcutKeys = (global::System.Windows.Forms.Keys)131156;
			this.toolStripMenuItemTestMemory.Size = new global::System.Drawing.Size(261, 22);
			this.toolStripMenuItemTestMemory.Text = "Test Memory";
			this.toolStripMenuItemTestMemory.Visible = false;
			this.toolStripMenuItemTestMemory.Click += new global::System.EventHandler(this.toolStripMenuItemTestMemory_Click);
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new global::System.Drawing.Size(258, 6);
			this.downloadPICkit2FirmwareToolStripMenuItem.Enabled = false;
			this.downloadPICkit2FirmwareToolStripMenuItem.Name = "downloadPICkit2FirmwareToolStripMenuItem";
			this.downloadPICkit2FirmwareToolStripMenuItem.Size = new global::System.Drawing.Size(261, 22);
			this.downloadPICkit2FirmwareToolStripMenuItem.Text = "Download PICkit 2 Operating System";
			this.downloadPICkit2FirmwareToolStripMenuItem.Click += new global::System.EventHandler(this.downloadPk2Firmware);
			this.toolStripMenuItemView.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
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
			this.toolStripMenuItemView.Size = new global::System.Drawing.Size(41, 20);
			this.toolStripMenuItemView.Text = "View";
			this.toolStripMenuItemSingleWindow.Checked = true;
			this.toolStripMenuItemSingleWindow.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.toolStripMenuItemSingleWindow.Name = "toolStripMenuItemSingleWindow";
			this.toolStripMenuItemSingleWindow.Size = new global::System.Drawing.Size(261, 22);
			this.toolStripMenuItemSingleWindow.Text = "Single Window";
			this.toolStripMenuItemSingleWindow.Click += new global::System.EventHandler(this.toolStripMenuItemSingleWindow_Click);
			this.toolStripMenuItemMultiWindow.Name = "toolStripMenuItemMultiWindow";
			this.toolStripMenuItemMultiWindow.Size = new global::System.Drawing.Size(261, 22);
			this.toolStripMenuItemMultiWindow.Text = "Multi-Window";
			this.toolStripMenuItemMultiWindow.Click += new global::System.EventHandler(this.toolStripMenuItemMultiWindow_Click);
			this.toolStripMenuItem7.Name = "toolStripMenuItem7";
			this.toolStripMenuItem7.Size = new global::System.Drawing.Size(258, 6);
			this.toolStripMenuItemShowProgramMemory.Enabled = false;
			this.toolStripMenuItemShowProgramMemory.Name = "toolStripMenuItemShowProgramMemory";
			this.toolStripMenuItemShowProgramMemory.Size = new global::System.Drawing.Size(261, 22);
			this.toolStripMenuItemShowProgramMemory.Text = "Show Program Memory";
			this.toolStripMenuItemShowProgramMemory.Click += new global::System.EventHandler(this.toolStripMenuItemShowProgramMemory_Click);
			this.toolStripMenuItemShowEEPROMData.Enabled = false;
			this.toolStripMenuItemShowEEPROMData.Name = "toolStripMenuItemShowEEPROMData";
			this.toolStripMenuItemShowEEPROMData.Size = new global::System.Drawing.Size(261, 22);
			this.toolStripMenuItemShowEEPROMData.Text = "Show EEPROM Data";
			this.toolStripMenuItemShowEEPROMData.Click += new global::System.EventHandler(this.toolStripMenuItemShowEEPROMData_Click);
			this.toolStripMenuItem9.Name = "toolStripMenuItem9";
			this.toolStripMenuItem9.Size = new global::System.Drawing.Size(258, 6);
			this.mainWindowAlwaysInFrontToolStripMenuItem.CheckOnClick = true;
			this.mainWindowAlwaysInFrontToolStripMenuItem.Enabled = false;
			this.mainWindowAlwaysInFrontToolStripMenuItem.Name = "mainWindowAlwaysInFrontToolStripMenuItem";
			this.mainWindowAlwaysInFrontToolStripMenuItem.Size = new global::System.Drawing.Size(261, 22);
			this.mainWindowAlwaysInFrontToolStripMenuItem.Text = "Associate / Memory Displays in Front";
			this.mainWindowAlwaysInFrontToolStripMenuItem.Click += new global::System.EventHandler(this.mainWindowAlwaysInFrontToolStripMenuItem_Click);
			this.helpToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
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
			this.helpToolStripMenuItem.Size = new global::System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			this.usersGuideToolStripMenuItem.Name = "usersGuideToolStripMenuItem";
			this.usersGuideToolStripMenuItem.Size = new global::System.Drawing.Size(206, 22);
			this.usersGuideToolStripMenuItem.Text = "PICkit 2 &User's Guide";
			this.usersGuideToolStripMenuItem.Click += new global::System.EventHandler(this.launchUsersGuide);
			this.toolStripMenuItemProgToGo.Name = "toolStripMenuItemProgToGo";
			this.toolStripMenuItemProgToGo.Size = new global::System.Drawing.Size(206, 22);
			this.toolStripMenuItemProgToGo.Text = "Programmer-To-Go Guide";
			this.toolStripMenuItemProgToGo.Click += new global::System.EventHandler(this.toolStripMenuItemProgToGo_Click);
			this.toolStripMenuItemLogicToolUG.Name = "toolStripMenuItemLogicToolUG";
			this.toolStripMenuItemLogicToolUG.Size = new global::System.Drawing.Size(206, 22);
			this.toolStripMenuItemLogicToolUG.Text = "Logic Tool User Guide";
			this.toolStripMenuItemLogicToolUG.Click += new global::System.EventHandler(this.toolStripMenuItemLogicToolUG_Click);
			this.uG44pinToolStripMenuItem.Name = "uG44pinToolStripMenuItem";
			this.uG44pinToolStripMenuItem.Size = new global::System.Drawing.Size(206, 22);
			this.uG44pinToolStripMenuItem.Text = "44-Pin Demo Board Guide";
			this.uG44pinToolStripMenuItem.Click += new global::System.EventHandler(this.uG44pinToolStripMenuItem_Click);
			this.lpcUsersGuideToolStripMenuItem.Name = "lpcUsersGuideToolStripMenuItem";
			this.lpcUsersGuideToolStripMenuItem.Size = new global::System.Drawing.Size(206, 22);
			this.lpcUsersGuideToolStripMenuItem.Text = "LPC Demo Board Guide";
			this.lpcUsersGuideToolStripMenuItem.Click += new global::System.EventHandler(this.launchLPCDemoGuide);
			this.webPk2ToolStripMenuItem.Name = "webPk2ToolStripMenuItem";
			this.webPk2ToolStripMenuItem.Size = new global::System.Drawing.Size(206, 22);
			this.webPk2ToolStripMenuItem.Text = "PICkit 2 on the web";
			this.webPk2ToolStripMenuItem.Click += new global::System.EventHandler(this.pickit2OnTheWeb);
			this.readMeToolStripMenuItem.Name = "readMeToolStripMenuItem";
			this.readMeToolStripMenuItem.Size = new global::System.Drawing.Size(206, 22);
			this.readMeToolStripMenuItem.Text = "&ReadMe";
			this.readMeToolStripMenuItem.Click += new global::System.EventHandler(this.launchReadMe);
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new global::System.Drawing.Size(203, 6);
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new global::System.Drawing.Size(206, 22);
			this.aboutToolStripMenuItem.Text = "&About";
			this.aboutToolStripMenuItem.Click += new global::System.EventHandler(this.clickAbout);
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
			this.statusGroupBox.Cursor = global::System.Windows.Forms.Cursors.Default;
			this.statusGroupBox.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.statusGroupBox.Location = new global::System.Drawing.Point(12, 27);
			this.statusGroupBox.Margin = new global::System.Windows.Forms.Padding(2);
			this.statusGroupBox.Name = "statusGroupBox";
			this.statusGroupBox.Padding = new global::System.Windows.Forms.Padding(2);
			this.statusGroupBox.Size = new global::System.Drawing.Size(514, 102);
			this.statusGroupBox.TabIndex = 1;
			this.statusGroupBox.TabStop = false;
			this.statusGroupBox.Text = "Device Configuration";
			this.labelOSSCALInvalid.AutoSize = true;
			this.labelOSSCALInvalid.ForeColor = global::System.Drawing.Color.Red;
			this.labelOSSCALInvalid.Location = new global::System.Drawing.Point(284, 61);
			this.labelOSSCALInvalid.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelOSSCALInvalid.Name = "labelOSSCALInvalid";
			this.labelOSSCALInvalid.Size = new global::System.Drawing.Size(68, 13);
			this.labelOSSCALInvalid.TabIndex = 22;
			this.labelOSSCALInvalid.Text = "Invalid Value";
			this.labelOSSCALInvalid.Visible = false;
			this.checkBoxA2CS.AutoSize = true;
			this.checkBoxA2CS.Enabled = false;
			this.checkBoxA2CS.Location = new global::System.Drawing.Point(411, 47);
			this.checkBoxA2CS.Margin = new global::System.Windows.Forms.Padding(2);
			this.checkBoxA2CS.Name = "checkBoxA2CS";
			this.checkBoxA2CS.Size = new global::System.Drawing.Size(96, 17);
			this.checkBoxA2CS.TabIndex = 21;
			this.checkBoxA2CS.Text = "A2 Chip Select";
			this.checkBoxA2CS.UseVisualStyleBackColor = true;
			this.checkBoxA2CS.Visible = false;
			this.checkBoxA1CS.AutoSize = true;
			this.checkBoxA1CS.Enabled = false;
			this.checkBoxA1CS.Location = new global::System.Drawing.Point(411, 31);
			this.checkBoxA1CS.Margin = new global::System.Windows.Forms.Padding(2);
			this.checkBoxA1CS.Name = "checkBoxA1CS";
			this.checkBoxA1CS.Size = new global::System.Drawing.Size(96, 17);
			this.checkBoxA1CS.TabIndex = 20;
			this.checkBoxA1CS.Text = "A1 Chip Select";
			this.checkBoxA1CS.UseVisualStyleBackColor = true;
			this.checkBoxA1CS.Visible = false;
			this.checkBoxA0CS.AutoSize = true;
			this.checkBoxA0CS.Enabled = false;
			this.checkBoxA0CS.Location = new global::System.Drawing.Point(411, 15);
			this.checkBoxA0CS.Margin = new global::System.Windows.Forms.Padding(2);
			this.checkBoxA0CS.Name = "checkBoxA0CS";
			this.checkBoxA0CS.Size = new global::System.Drawing.Size(96, 17);
			this.checkBoxA0CS.TabIndex = 19;
			this.checkBoxA0CS.Text = "A0 Chip Select";
			this.checkBoxA0CS.UseVisualStyleBackColor = true;
			this.checkBoxA0CS.Visible = false;
			this.buttonShowIDMem.Location = new global::System.Drawing.Point(79, 46);
			this.buttonShowIDMem.Margin = new global::System.Windows.Forms.Padding(2);
			this.buttonShowIDMem.Name = "buttonShowIDMem";
			this.buttonShowIDMem.Size = new global::System.Drawing.Size(65, 22);
			this.buttonShowIDMem.TabIndex = 15;
			this.buttonShowIDMem.Text = "Display";
			this.buttonShowIDMem.UseVisualStyleBackColor = true;
			this.buttonShowIDMem.Visible = false;
			this.buttonShowIDMem.Click += new global::System.EventHandler(this.buttonShowIDMem_Click);
			this.displayRev.AutoSize = true;
			this.displayRev.Location = new global::System.Drawing.Point(135, 75);
			this.displayRev.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.displayRev.Name = "displayRev";
			this.displayRev.Size = new global::System.Drawing.Size(27, 13);
			this.displayRev.TabIndex = 14;
			this.displayRev.Text = "Rev";
			this.displayRev.Visible = false;
			this.labelCodeProtect.AutoSize = true;
			this.labelCodeProtect.ForeColor = global::System.Drawing.Color.Red;
			this.labelCodeProtect.Location = new global::System.Drawing.Point(242, 41);
			this.labelCodeProtect.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelCodeProtect.Name = "labelCodeProtect";
			this.labelCodeProtect.Size = new global::System.Drawing.Size(69, 13);
			this.labelCodeProtect.TabIndex = 13;
			this.labelCodeProtect.Text = "Code Protect";
			this.labelCodeProtect.Visible = false;
			this.comboBoxSelectPart.BackColor = global::System.Drawing.SystemColors.Info;
			this.comboBoxSelectPart.DropDownHeight = 212;
			this.comboBoxSelectPart.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSelectPart.FormattingEnabled = true;
			this.comboBoxSelectPart.IntegralHeight = false;
			this.comboBoxSelectPart.Items.AddRange(new object[]
			{
				"-Select Device-"
			});
			this.comboBoxSelectPart.Location = new global::System.Drawing.Point(79, 22);
			this.comboBoxSelectPart.Margin = new global::System.Windows.Forms.Padding(2);
			this.comboBoxSelectPart.Name = "comboBoxSelectPart";
			this.comboBoxSelectPart.Size = new global::System.Drawing.Size(148, 21);
			this.comboBoxSelectPart.Sorted = true;
			this.comboBoxSelectPart.TabIndex = 12;
			this.comboBoxSelectPart.Visible = false;
			this.comboBoxSelectPart.SelectionChangeCommitted += new global::System.EventHandler(this.selectPart);
			this.displayBandGap.AutoSize = true;
			this.displayBandGap.Location = new global::System.Drawing.Point(439, 75);
			this.displayBandGap.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.displayBandGap.Name = "displayBandGap";
			this.displayBandGap.Size = new global::System.Drawing.Size(31, 13);
			this.displayBandGap.TabIndex = 11;
			this.displayBandGap.Text = "0000";
			this.labelBandGap.AutoSize = true;
			this.labelBandGap.Location = new global::System.Drawing.Point(379, 75);
			this.labelBandGap.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelBandGap.Name = "labelBandGap";
			this.labelBandGap.Size = new global::System.Drawing.Size(55, 13);
			this.labelBandGap.TabIndex = 10;
			this.labelBandGap.Text = "BandGap:";
			this.displayOSCCAL.AutoSize = true;
			this.displayOSCCAL.Location = new global::System.Drawing.Point(300, 75);
			this.displayOSCCAL.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.displayOSCCAL.Name = "displayOSCCAL";
			this.displayOSCCAL.Size = new global::System.Drawing.Size(31, 13);
			this.displayOSCCAL.TabIndex = 9;
			this.displayOSCCAL.Text = "0000";
			this.labelOSCCAL.AutoSize = true;
			this.labelOSCCAL.Location = new global::System.Drawing.Point(242, 75);
			this.labelOSCCAL.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelOSCCAL.Name = "labelOSCCAL";
			this.labelOSCCAL.Size = new global::System.Drawing.Size(52, 13);
			this.labelOSCCAL.TabIndex = 8;
			this.labelOSCCAL.Text = "OSCCAL:";
			this.displayChecksum.AutoSize = true;
			this.displayChecksum.Location = new global::System.Drawing.Point(76, 75);
			this.displayChecksum.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.displayChecksum.Name = "displayChecksum";
			this.displayChecksum.Size = new global::System.Drawing.Size(31, 13);
			this.displayChecksum.TabIndex = 7;
			this.displayChecksum.Text = "0000";
			this.displayUserIDs.AutoSize = true;
			this.displayUserIDs.Location = new global::System.Drawing.Point(76, 50);
			this.displayUserIDs.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.displayUserIDs.Name = "displayUserIDs";
			this.displayUserIDs.Size = new global::System.Drawing.Size(64, 13);
			this.displayUserIDs.TabIndex = 6;
			this.displayUserIDs.Text = "00 00 00 00";
			this.displayDevice.AutoSize = true;
			this.displayDevice.Location = new global::System.Drawing.Point(79, 25);
			this.displayDevice.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.displayDevice.Name = "displayDevice";
			this.displayDevice.Size = new global::System.Drawing.Size(63, 13);
			this.displayDevice.TabIndex = 5;
			this.displayDevice.Text = "Not Present";
			this.labelConfig.AutoSize = true;
			this.labelConfig.Cursor = global::System.Windows.Forms.Cursors.Hand;
			this.labelConfig.Enabled = false;
			this.labelConfig.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Underline, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelConfig.ForeColor = global::System.Drawing.SystemColors.ActiveCaption;
			this.labelConfig.Location = new global::System.Drawing.Point(242, 25);
			this.labelConfig.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelConfig.Name = "labelConfig";
			this.labelConfig.Size = new global::System.Drawing.Size(72, 13);
			this.labelConfig.TabIndex = 4;
			this.labelConfig.Text = "Configuration:";
			this.labelConfig.Click += new global::System.EventHandler(this.labelConfig_Click);
			this.dataGridConfigWords.AllowUserToAddRows = false;
			this.dataGridConfigWords.AllowUserToDeleteRows = false;
			this.dataGridConfigWords.AllowUserToResizeColumns = false;
			this.dataGridConfigWords.AllowUserToResizeRows = false;
			this.dataGridConfigWords.BackgroundColor = global::System.Drawing.SystemColors.ControlDark;
			this.dataGridConfigWords.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
			this.dataGridConfigWords.CellBorderStyle = global::System.Windows.Forms.DataGridViewCellBorderStyle.None;
			dataGridViewCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = global::System.Drawing.SystemColors.Control;
			dataGridViewCellStyle.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle.ForeColor = global::System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridConfigWords.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dataGridConfigWords.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridConfigWords.ColumnHeadersVisible = false;
			dataGridViewCellStyle2.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = global::System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.ForeColor = global::System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = global::System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridConfigWords.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridConfigWords.Enabled = false;
			this.dataGridConfigWords.GridColor = global::System.Drawing.SystemColors.Control;
			this.dataGridConfigWords.Location = new global::System.Drawing.Point(320, 24);
			this.dataGridConfigWords.Margin = new global::System.Windows.Forms.Padding(2);
			this.dataGridConfigWords.MultiSelect = false;
			this.dataGridConfigWords.Name = "dataGridConfigWords";
			this.dataGridConfigWords.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = global::System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle3.ForeColor = global::System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridConfigWords.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridConfigWords.RowHeadersVisible = false;
			this.dataGridConfigWords.ScrollBars = global::System.Windows.Forms.ScrollBars.None;
			this.dataGridConfigWords.Size = new global::System.Drawing.Size(160, 34);
			this.dataGridConfigWords.TabIndex = 3;
			this.labelChecksum.AutoSize = true;
			this.labelChecksum.Location = new global::System.Drawing.Point(6, 75);
			this.labelChecksum.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelChecksum.Name = "labelChecksum";
			this.labelChecksum.Size = new global::System.Drawing.Size(60, 13);
			this.labelChecksum.TabIndex = 2;
			this.labelChecksum.Text = "Checksum:";
			this.labelUserIDs.AutoSize = true;
			this.labelUserIDs.Location = new global::System.Drawing.Point(6, 50);
			this.labelUserIDs.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelUserIDs.Name = "labelUserIDs";
			this.labelUserIDs.Size = new global::System.Drawing.Size(51, 13);
			this.labelUserIDs.TabIndex = 1;
			this.labelUserIDs.Text = "User IDs:";
			this.labelDevice.AutoSize = true;
			this.labelDevice.Location = new global::System.Drawing.Point(6, 25);
			this.labelDevice.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelDevice.Name = "labelDevice";
			this.labelDevice.Size = new global::System.Drawing.Size(44, 13);
			this.labelDevice.TabIndex = 0;
			this.labelDevice.Text = "Device:";
			this.pictureBoxLogo.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("pictureBoxLogo.Image");
			this.pictureBoxLogo.InitialImage = (global::System.Drawing.Image)componentResourceManager.GetObject("pictureBoxLogo.InitialImage");
			this.pictureBoxLogo.Location = new global::System.Drawing.Point(370, 134);
			this.pictureBoxLogo.Margin = new global::System.Windows.Forms.Padding(2);
			this.pictureBoxLogo.Name = "pictureBoxLogo";
			this.pictureBoxLogo.Size = new global::System.Drawing.Size(152, 42);
			this.pictureBoxLogo.TabIndex = 2;
			this.pictureBoxLogo.TabStop = false;
			this.displayStatusWindow.BackColor = global::System.Drawing.SystemColors.Info;
			this.displayStatusWindow.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.displayStatusWindow.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.displayStatusWindow.Location = new global::System.Drawing.Point(12, 137);
			this.displayStatusWindow.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.displayStatusWindow.Name = "displayStatusWindow";
			this.displayStatusWindow.Size = new global::System.Drawing.Size(342, 36);
			this.displayStatusWindow.TabIndex = 4;
			this.displayStatusWindow.Text = "Status\r\nWindow";
			this.buttonRead.Enabled = false;
			this.buttonRead.Location = new global::System.Drawing.Point(12, 204);
			this.buttonRead.Margin = new global::System.Windows.Forms.Padding(2);
			this.buttonRead.Name = "buttonRead";
			this.buttonRead.Size = new global::System.Drawing.Size(57, 26);
			this.buttonRead.TabIndex = 5;
			this.buttonRead.Text = "Read";
			this.buttonRead.UseVisualStyleBackColor = true;
			this.buttonRead.Click += new global::System.EventHandler(this.readDevice);
			this.progressBar1.BackColor = global::System.Drawing.SystemColors.Control;
			this.progressBar1.Location = new global::System.Drawing.Point(12, 182);
			this.progressBar1.Margin = new global::System.Windows.Forms.Padding(2);
			this.progressBar1.MarqueeAnimationSpeed = 50;
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new global::System.Drawing.Size(342, 15);
			this.progressBar1.Style = global::System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBar1.TabIndex = 6;
			this.buttonWrite.Enabled = false;
			this.buttonWrite.Location = new global::System.Drawing.Point(75, 204);
			this.buttonWrite.Margin = new global::System.Windows.Forms.Padding(2);
			this.buttonWrite.Name = "buttonWrite";
			this.buttonWrite.Size = new global::System.Drawing.Size(57, 26);
			this.buttonWrite.TabIndex = 7;
			this.buttonWrite.Text = "Write";
			this.buttonWrite.UseVisualStyleBackColor = true;
			this.buttonWrite.Click += new global::System.EventHandler(this.writeDevice);
			this.buttonVerify.Enabled = false;
			this.buttonVerify.Location = new global::System.Drawing.Point(138, 204);
			this.buttonVerify.Margin = new global::System.Windows.Forms.Padding(2);
			this.buttonVerify.Name = "buttonVerify";
			this.buttonVerify.Size = new global::System.Drawing.Size(57, 26);
			this.buttonVerify.TabIndex = 8;
			this.buttonVerify.Text = "Verify";
			this.buttonVerify.UseVisualStyleBackColor = true;
			this.buttonVerify.Click += new global::System.EventHandler(this.verifyDevice);
			this.buttonErase.Enabled = false;
			this.buttonErase.Location = new global::System.Drawing.Point(201, 204);
			this.buttonErase.Margin = new global::System.Windows.Forms.Padding(2);
			this.buttonErase.Name = "buttonErase";
			this.buttonErase.Size = new global::System.Drawing.Size(57, 26);
			this.buttonErase.TabIndex = 9;
			this.buttonErase.Text = "Erase";
			this.buttonErase.UseVisualStyleBackColor = true;
			this.buttonErase.Click += new global::System.EventHandler(this.eraseDevice);
			this.buttonBlankCheck.Enabled = false;
			this.buttonBlankCheck.Location = new global::System.Drawing.Point(264, 204);
			this.buttonBlankCheck.Margin = new global::System.Windows.Forms.Padding(2);
			this.buttonBlankCheck.Name = "buttonBlankCheck";
			this.buttonBlankCheck.Size = new global::System.Drawing.Size(91, 26);
			this.buttonBlankCheck.TabIndex = 10;
			this.buttonBlankCheck.Text = "Blank Check";
			this.buttonBlankCheck.UseVisualStyleBackColor = true;
			this.buttonBlankCheck.Click += new global::System.EventHandler(this.blankCheck);
			this.chkBoxVddOn.AutoSize = true;
			this.chkBoxVddOn.Enabled = false;
			this.chkBoxVddOn.Location = new global::System.Drawing.Point(15, 15);
			this.chkBoxVddOn.Margin = new global::System.Windows.Forms.Padding(2);
			this.chkBoxVddOn.Name = "chkBoxVddOn";
			this.chkBoxVddOn.Size = new global::System.Drawing.Size(40, 17);
			this.chkBoxVddOn.TabIndex = 11;
			this.chkBoxVddOn.Text = "On";
			this.chkBoxVddOn.UseVisualStyleBackColor = true;
			this.chkBoxVddOn.Click += new global::System.EventHandler(this.guiVddControl);
			this.numUpDnVDD.DecimalPlaces = 1;
			this.numUpDnVDD.Font = new global::System.Drawing.Font("Arial", 12f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.numUpDnVDD.Increment = new decimal(new int[]
			{
				1,
				0,
				0,
				65536
			});
			this.numUpDnVDD.Location = new global::System.Drawing.Point(97, 15);
			this.numUpDnVDD.Margin = new global::System.Windows.Forms.Padding(2);
			this.numUpDnVDD.Maximum = new decimal(new int[]
			{
				50,
				0,
				0,
				65536
			});
			this.numUpDnVDD.Minimum = new decimal(new int[]
			{
				25,
				0,
				0,
				65536
			});
			this.numUpDnVDD.Name = "numUpDnVDD";
			this.numUpDnVDD.Size = new global::System.Drawing.Size(53, 26);
			this.numUpDnVDD.TabIndex = 14;
			this.numUpDnVDD.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.numUpDnVDD.Value = new decimal(new int[]
			{
				25,
				0,
				0,
				65536
			});
			this.numUpDnVDD.ValueChanged += new global::System.EventHandler(this.guiChangeVDD);
			this.groupBoxVDD.Controls.Add(this.checkBoxMCLR);
			this.groupBoxVDD.Controls.Add(this.numUpDnVDD);
			this.groupBoxVDD.Controls.Add(this.chkBoxVddOn);
			this.groupBoxVDD.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.groupBoxVDD.Location = new global::System.Drawing.Point(370, 179);
			this.groupBoxVDD.Margin = new global::System.Windows.Forms.Padding(2);
			this.groupBoxVDD.Name = "groupBoxVDD";
			this.groupBoxVDD.Padding = new global::System.Windows.Forms.Padding(2);
			this.groupBoxVDD.Size = new global::System.Drawing.Size(156, 51);
			this.groupBoxVDD.TabIndex = 17;
			this.groupBoxVDD.TabStop = false;
			this.groupBoxVDD.Text = "VDD PICkit 2";
			this.checkBoxMCLR.AutoSize = true;
			this.checkBoxMCLR.Enabled = false;
			this.checkBoxMCLR.Location = new global::System.Drawing.Point(15, 32);
			this.checkBoxMCLR.Margin = new global::System.Windows.Forms.Padding(2);
			this.checkBoxMCLR.Name = "checkBoxMCLR";
			this.checkBoxMCLR.Size = new global::System.Drawing.Size(61, 17);
			this.checkBoxMCLR.TabIndex = 15;
			this.checkBoxMCLR.Text = "/MCLR";
			this.checkBoxMCLR.UseVisualStyleBackColor = true;
			this.checkBoxMCLR.Click += new global::System.EventHandler(this.MCLRtoolStripMenuItem_Click);
			this.groupBoxProgMem.Controls.Add(this.dataGridProgramMemory);
			this.groupBoxProgMem.Controls.Add(this.displayDataSource);
			this.groupBoxProgMem.Controls.Add(this.labelDataSource);
			this.groupBoxProgMem.Controls.Add(this.comboBoxProgMemView);
			this.groupBoxProgMem.Controls.Add(this.checkBoxProgMemEnabled);
			this.groupBoxProgMem.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.groupBoxProgMem.ForeColor = global::System.Drawing.SystemColors.InfoText;
			this.groupBoxProgMem.Location = new global::System.Drawing.Point(12, 236);
			this.groupBoxProgMem.Margin = new global::System.Windows.Forms.Padding(2);
			this.groupBoxProgMem.Name = "groupBoxProgMem";
			this.groupBoxProgMem.Padding = new global::System.Windows.Forms.Padding(2);
			this.groupBoxProgMem.Size = new global::System.Drawing.Size(514, 259);
			this.groupBoxProgMem.TabIndex = 18;
			this.groupBoxProgMem.TabStop = false;
			this.groupBoxProgMem.Text = "Program Memory";
			this.dataGridProgramMemory.AllowUserToAddRows = false;
			this.dataGridProgramMemory.AllowUserToDeleteRows = false;
			this.dataGridProgramMemory.AllowUserToResizeColumns = false;
			this.dataGridProgramMemory.AllowUserToResizeRows = false;
			this.dataGridProgramMemory.BackgroundColor = global::System.Drawing.SystemColors.Window;
			this.dataGridProgramMemory.CellBorderStyle = global::System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.dataGridProgramMemory.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridProgramMemory.ColumnHeadersVisible = false;
			this.dataGridProgramMemory.ContextMenuStrip = this.contextMenuStrip1;
			this.dataGridProgramMemory.Enabled = false;
			this.dataGridProgramMemory.Location = new global::System.Drawing.Point(6, 44);
			this.dataGridProgramMemory.Margin = new global::System.Windows.Forms.Padding(2);
			this.dataGridProgramMemory.Name = "dataGridProgramMemory";
			this.dataGridProgramMemory.RowHeadersVisible = false;
			this.dataGridProgramMemory.RowHeadersWidth = 75;
			this.dataGridProgramMemory.RowHeadersWidthSizeMode = global::System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dataGridViewCellStyle4.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.dataGridProgramMemory.RowsDefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridProgramMemory.RowTemplate.Height = 17;
			this.dataGridProgramMemory.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.dataGridProgramMemory.SelectionMode = global::System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.dataGridProgramMemory.Size = new global::System.Drawing.Size(502, 208);
			this.dataGridProgramMemory.TabIndex = 4;
			this.dataGridProgramMemory.CellMouseDown += new global::System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridProgramMemory_CellMouseDown);
			this.dataGridProgramMemory.CellEndEdit += new global::System.Windows.Forms.DataGridViewCellEventHandler(this.progMemEdit);
			this.contextMenuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.toolStripMenuItemContextSelectAll,
				this.toolStripMenuItemContextCopy
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new global::System.Drawing.Size(164, 48);
			this.contextMenuStrip1.ItemClicked += new global::System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
			this.toolStripMenuItemContextSelectAll.Name = "toolStripMenuItemContextSelectAll";
			this.toolStripMenuItemContextSelectAll.ShortcutKeyDisplayString = "Ctrl-A";
			this.toolStripMenuItemContextSelectAll.Size = new global::System.Drawing.Size(163, 22);
			this.toolStripMenuItemContextSelectAll.Text = "Select All";
			this.toolStripMenuItemContextCopy.Name = "toolStripMenuItemContextCopy";
			this.toolStripMenuItemContextCopy.ShortcutKeyDisplayString = "Ctrl-C";
			this.toolStripMenuItemContextCopy.Size = new global::System.Drawing.Size(163, 22);
			this.toolStripMenuItemContextCopy.Text = "Copy";
			this.displayDataSource.BorderStyle = global::System.Windows.Forms.BorderStyle.Fixed3D;
			this.displayDataSource.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.displayDataSource.Location = new global::System.Drawing.Point(229, 19);
			this.displayDataSource.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.displayDataSource.Name = "displayDataSource";
			this.displayDataSource.Size = new global::System.Drawing.Size(279, 16);
			this.displayDataSource.TabIndex = 3;
			this.displayDataSource.Text = "None (Empty/Erased)";
			this.displayDataSource.UseCompatibleTextRendering = true;
			this.labelDataSource.AutoSize = true;
			this.labelDataSource.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelDataSource.Location = new global::System.Drawing.Point(176, 20);
			this.labelDataSource.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelDataSource.Name = "labelDataSource";
			this.labelDataSource.Size = new global::System.Drawing.Size(51, 13);
			this.labelDataSource.TabIndex = 2;
			this.labelDataSource.Text = "Source:";
			this.comboBoxProgMemView.BackColor = global::System.Drawing.SystemColors.Info;
			this.comboBoxProgMemView.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxProgMemView.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.comboBoxProgMemView.FormattingEnabled = true;
			this.comboBoxProgMemView.Items.AddRange(new object[]
			{
				"Hex Only",
				"Word ASCII",
				"Byte ASCII"
			});
			this.comboBoxProgMemView.Location = new global::System.Drawing.Point(79, 17);
			this.comboBoxProgMemView.Margin = new global::System.Windows.Forms.Padding(2);
			this.comboBoxProgMemView.Name = "comboBoxProgMemView";
			this.comboBoxProgMemView.Size = new global::System.Drawing.Size(91, 21);
			this.comboBoxProgMemView.TabIndex = 1;
			this.comboBoxProgMemView.SelectionChangeCommitted += new global::System.EventHandler(this.progMemViewChanged);
			this.checkBoxProgMemEnabled.AutoSize = true;
			this.checkBoxProgMemEnabled.Checked = true;
			this.checkBoxProgMemEnabled.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.checkBoxProgMemEnabled.Enabled = false;
			this.checkBoxProgMemEnabled.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.checkBoxProgMemEnabled.Location = new global::System.Drawing.Point(6, 19);
			this.checkBoxProgMemEnabled.Margin = new global::System.Windows.Forms.Padding(2);
			this.checkBoxProgMemEnabled.Name = "checkBoxProgMemEnabled";
			this.checkBoxProgMemEnabled.Size = new global::System.Drawing.Size(65, 17);
			this.checkBoxProgMemEnabled.TabIndex = 0;
			this.checkBoxProgMemEnabled.Text = "Enabled";
			this.checkBoxProgMemEnabled.UseVisualStyleBackColor = true;
			this.checkBoxProgMemEnabled.Click += new global::System.EventHandler(this.memorySelectVerify);
			this.openHexFileDialog.DefaultExt = "hex";
			this.openHexFileDialog.Filter = "HEX files|*.hex|All files|*.*";
			this.openHexFileDialog.Title = "Import Hex File";
			this.openHexFileDialog.FileOk += new global::System.ComponentModel.CancelEventHandler(this.importHexFile);
			this.saveHexFileDialog.DefaultExt = "hex";
			this.saveHexFileDialog.Filter = "Hex files|*.hex|All files|*.*";
			this.saveHexFileDialog.Title = "Export Hex File";
			this.saveHexFileDialog.FileOk += new global::System.ComponentModel.CancelEventHandler(this.exportHexFile);
			this.openFWFile.DefaultExt = "hex";
			this.openFWFile.Filter = "PICkit 2 OS|pk*.hex|All files|*.*";
			this.openFWFile.InitialDirectory = "c:\\Program Files\\Microchip\\PICkit 2 v2";
			this.openFWFile.Title = "Open PICkit 2 Operating System File";
			this.timerButton.Interval = 250;
			this.timerButton.Tick += new global::System.EventHandler(this.timerGoesOff);
			this.groupBoxEEMem.Controls.Add(this.displayEEProgInfo);
			this.groupBoxEEMem.Controls.Add(this.dataGridViewEEPROM);
			this.groupBoxEEMem.Controls.Add(this.comboBoxEE);
			this.groupBoxEEMem.Controls.Add(this.checkBoxEEMem);
			this.groupBoxEEMem.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.groupBoxEEMem.ForeColor = global::System.Drawing.SystemColors.InfoText;
			this.groupBoxEEMem.Location = new global::System.Drawing.Point(12, 501);
			this.groupBoxEEMem.Margin = new global::System.Windows.Forms.Padding(2);
			this.groupBoxEEMem.Name = "groupBoxEEMem";
			this.groupBoxEEMem.Padding = new global::System.Windows.Forms.Padding(2);
			this.groupBoxEEMem.Size = new global::System.Drawing.Size(399, 123);
			this.groupBoxEEMem.TabIndex = 19;
			this.groupBoxEEMem.TabStop = false;
			this.groupBoxEEMem.Text = "EEPROM Data";
			this.displayEEProgInfo.AutoSize = true;
			this.displayEEProgInfo.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.displayEEProgInfo.ForeColor = global::System.Drawing.Color.Red;
			this.displayEEProgInfo.Location = new global::System.Drawing.Point(186, 20);
			this.displayEEProgInfo.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.displayEEProgInfo.Name = "displayEEProgInfo";
			this.displayEEProgInfo.Size = new global::System.Drawing.Size(206, 13);
			this.displayEEProgInfo.TabIndex = 7;
			this.displayEEProgInfo.Text = "Preserve EEPROM and User IDs on write.";
			this.displayEEProgInfo.Visible = false;
			this.dataGridViewEEPROM.AllowUserToAddRows = false;
			this.dataGridViewEEPROM.AllowUserToDeleteRows = false;
			this.dataGridViewEEPROM.AllowUserToResizeColumns = false;
			this.dataGridViewEEPROM.AllowUserToResizeRows = false;
			this.dataGridViewEEPROM.BackgroundColor = global::System.Drawing.SystemColors.Window;
			this.dataGridViewEEPROM.CellBorderStyle = global::System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.dataGridViewEEPROM.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dataGridViewEEPROM.ColumnHeadersVisible = false;
			this.dataGridViewEEPROM.ContextMenuStrip = this.contextMenuStrip1;
			this.dataGridViewEEPROM.Location = new global::System.Drawing.Point(6, 44);
			this.dataGridViewEEPROM.Margin = new global::System.Windows.Forms.Padding(2);
			this.dataGridViewEEPROM.Name = "dataGridViewEEPROM";
			this.dataGridViewEEPROM.RowHeadersVisible = false;
			this.dataGridViewEEPROM.RowHeadersWidthSizeMode = global::System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dataGridViewCellStyle5.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.dataGridViewEEPROM.RowsDefaultCellStyle = dataGridViewCellStyle5;
			this.dataGridViewEEPROM.RowTemplate.Height = 17;
			this.dataGridViewEEPROM.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.dataGridViewEEPROM.SelectionMode = global::System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.dataGridViewEEPROM.Size = new global::System.Drawing.Size(387, 72);
			this.dataGridViewEEPROM.TabIndex = 6;
			this.dataGridViewEEPROM.CellMouseDown += new global::System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewEEPROM_CellMouseDown);
			this.dataGridViewEEPROM.CellEndEdit += new global::System.Windows.Forms.DataGridViewCellEventHandler(this.eEpromEdit);
			this.comboBoxEE.BackColor = global::System.Drawing.SystemColors.Info;
			this.comboBoxEE.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxEE.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.comboBoxEE.FormattingEnabled = true;
			this.comboBoxEE.Items.AddRange(new object[]
			{
				"Hex Only",
				"Word ASCII",
				"Byte ASCII"
			});
			this.comboBoxEE.Location = new global::System.Drawing.Point(79, 17);
			this.comboBoxEE.Margin = new global::System.Windows.Forms.Padding(2);
			this.comboBoxEE.Name = "comboBoxEE";
			this.comboBoxEE.Size = new global::System.Drawing.Size(91, 21);
			this.comboBoxEE.TabIndex = 5;
			this.comboBoxEE.SelectionChangeCommitted += new global::System.EventHandler(this.progMemViewChanged);
			this.checkBoxEEMem.AutoSize = true;
			this.checkBoxEEMem.Checked = true;
			this.checkBoxEEMem.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.checkBoxEEMem.Enabled = false;
			this.checkBoxEEMem.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.checkBoxEEMem.Location = new global::System.Drawing.Point(6, 19);
			this.checkBoxEEMem.Margin = new global::System.Windows.Forms.Padding(2);
			this.checkBoxEEMem.Name = "checkBoxEEMem";
			this.checkBoxEEMem.Size = new global::System.Drawing.Size(65, 17);
			this.checkBoxEEMem.TabIndex = 0;
			this.checkBoxEEMem.Text = "Enabled";
			this.checkBoxEEMem.UseVisualStyleBackColor = true;
			this.checkBoxEEMem.Click += new global::System.EventHandler(this.memorySelectVerify);
			this.buttonExportHex.Location = new global::System.Drawing.Point(423, 545);
			this.buttonExportHex.Margin = new global::System.Windows.Forms.Padding(2);
			this.buttonExportHex.Name = "buttonExportHex";
			this.buttonExportHex.Size = new global::System.Drawing.Size(103, 35);
			this.buttonExportHex.TabIndex = 21;
			this.buttonExportHex.Text = "Read Device +\r\nExport Hex File";
			this.buttonExportHex.UseVisualStyleBackColor = true;
			this.buttonExportHex.Click += new global::System.EventHandler(this.buttonReadExport);
			this.pictureBox1.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("pictureBox1.Image");
			this.pictureBox1.Location = new global::System.Drawing.Point(423, 586);
			this.pictureBox1.Margin = new global::System.Windows.Forms.Padding(2);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new global::System.Drawing.Size(103, 38);
			this.pictureBox1.TabIndex = 22;
			this.pictureBox1.TabStop = false;
			this.timerDLFW.Tick += new global::System.EventHandler(this.autoDownloadFW);
			this.checkBoxAutoImportWrite.Appearance = global::System.Windows.Forms.Appearance.Button;
			this.checkBoxAutoImportWrite.Location = new global::System.Drawing.Point(423, 505);
			this.checkBoxAutoImportWrite.Margin = new global::System.Windows.Forms.Padding(2);
			this.checkBoxAutoImportWrite.Name = "checkBoxAutoImportWrite";
			this.checkBoxAutoImportWrite.Size = new global::System.Drawing.Size(103, 35);
			this.checkBoxAutoImportWrite.TabIndex = 23;
			this.checkBoxAutoImportWrite.Text = "Auto Import Hex\r\n+ Write Device";
			this.checkBoxAutoImportWrite.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBoxAutoImportWrite.UseVisualStyleBackColor = true;
			this.checkBoxAutoImportWrite.Click += new global::System.EventHandler(this.checkBoxAutoImportWrite_Click);
			this.checkBoxAutoImportWrite.CheckedChanged += new global::System.EventHandler(this.checkBoxAutoImportWrite_Changed);
			this.timerAutoImportWrite.Interval = 250;
			this.timerAutoImportWrite.Tick += new global::System.EventHandler(this.timerAutoImportWrite_Tick);
			this.checkBoxProgMemEnabledAlt.AutoSize = true;
			this.checkBoxProgMemEnabledAlt.Checked = true;
			this.checkBoxProgMemEnabledAlt.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.checkBoxProgMemEnabledAlt.Location = new global::System.Drawing.Point(16, 240);
			this.checkBoxProgMemEnabledAlt.Margin = new global::System.Windows.Forms.Padding(2);
			this.checkBoxProgMemEnabledAlt.Name = "checkBoxProgMemEnabledAlt";
			this.checkBoxProgMemEnabledAlt.Size = new global::System.Drawing.Size(147, 17);
			this.checkBoxProgMemEnabledAlt.TabIndex = 24;
			this.checkBoxProgMemEnabledAlt.Text = "Program Memory Enabled";
			this.checkBoxProgMemEnabledAlt.UseVisualStyleBackColor = true;
			this.checkBoxProgMemEnabledAlt.Visible = false;
			this.checkBoxProgMemEnabledAlt.Click += new global::System.EventHandler(this.memorySelectVerify);
			this.checkBoxEEDATAMemoryEnabledAlt.AutoSize = true;
			this.checkBoxEEDATAMemoryEnabledAlt.Checked = true;
			this.checkBoxEEDATAMemoryEnabledAlt.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.checkBoxEEDATAMemoryEnabledAlt.Location = new global::System.Drawing.Point(16, 259);
			this.checkBoxEEDATAMemoryEnabledAlt.Margin = new global::System.Windows.Forms.Padding(2);
			this.checkBoxEEDATAMemoryEnabledAlt.Name = "checkBoxEEDATAMemoryEnabledAlt";
			this.checkBoxEEDATAMemoryEnabledAlt.Size = new global::System.Drawing.Size(140, 17);
			this.checkBoxEEDATAMemoryEnabledAlt.TabIndex = 25;
			this.checkBoxEEDATAMemoryEnabledAlt.Text = "EEPROM Data Enabled";
			this.checkBoxEEDATAMemoryEnabledAlt.UseVisualStyleBackColor = true;
			this.checkBoxEEDATAMemoryEnabledAlt.Visible = false;
			this.checkBoxEEDATAMemoryEnabledAlt.Click += new global::System.EventHandler(this.memorySelectVerify);
			this.timerInitalUpdate.Interval = 1;
			this.timerInitalUpdate.Tick += new global::System.EventHandler(this.timerInitalUpdate_Tick);
			this.labelLVP.AutoSize = true;
			this.labelLVP.ForeColor = global::System.Drawing.Color.Red;
			this.labelLVP.Location = new global::System.Drawing.Point(200, 7);
			this.labelLVP.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelLVP.Name = "labelLVP";
			this.labelLVP.Size = new global::System.Drawing.Size(27, 13);
			this.labelLVP.TabIndex = 23;
			this.labelLVP.Text = "LVP";
			this.labelLVP.Visible = false;
			this.labelConfig9.AutoSize = true;
			this.labelConfig9.Location = new global::System.Drawing.Point(478, 43);
			this.labelConfig9.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.labelConfig9.Name = "labelConfig9";
			this.labelConfig9.Size = new global::System.Drawing.Size(31, 13);
			this.labelConfig9.TabIndex = 24;
			this.labelConfig9.Text = "FFFF";
			this.labelConfig9.Visible = false;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(96f, 96f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = global::System.Drawing.SystemColors.Control;
			base.ClientSize = new global::System.Drawing.Size(538, 636);
			base.Controls.Add(this.checkBoxEEDATAMemoryEnabledAlt);
			base.Controls.Add(this.checkBoxProgMemEnabledAlt);
			base.Controls.Add(this.checkBoxAutoImportWrite);
			base.Controls.Add(this.pictureBox1);
			base.Controls.Add(this.buttonExportHex);
			base.Controls.Add(this.groupBoxEEMem);
			base.Controls.Add(this.groupBoxProgMem);
			base.Controls.Add(this.groupBoxVDD);
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
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MainMenuStrip = this.menuStrip1;
			base.Margin = new global::System.Windows.Forms.Padding(2);
			base.MaximizeBox = false;
			this.MaximumSize = new global::System.Drawing.Size(544, 670);
			this.MinimumSize = new global::System.Drawing.Size(544, 320);
			base.Name = "FormPICkit2";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "PICkit 2 Programmer";
			base.Move += new global::System.EventHandler(this.FormPICkit2_Move);
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.pickitFormClosing);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusGroupBox.ResumeLayout(false);
			this.statusGroupBox.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.dataGridConfigWords).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBoxLogo).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.numUpDnVDD).EndInit();
			this.groupBoxVDD.ResumeLayout(false);
			this.groupBoxVDD.PerformLayout();
			this.groupBoxProgMem.ResumeLayout(false);
			this.groupBoxProgMem.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.dataGridProgramMemory).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.groupBoxEEMem.ResumeLayout(false);
			this.groupBoxEEMem.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.dataGridViewEEPROM).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400023D RID: 573
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400023E RID: 574
		private global::System.Windows.Forms.MenuStrip menuStrip1;

		// Token: 0x0400023F RID: 575
		private global::System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;

		// Token: 0x04000240 RID: 576
		private global::System.Windows.Forms.ToolStripMenuItem importFileToolStripMenuItem;

		// Token: 0x04000241 RID: 577
		private global::System.Windows.Forms.ToolStripMenuItem exportFileToolStripMenuItem;

		// Token: 0x04000242 RID: 578
		private global::System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;

		// Token: 0x04000243 RID: 579
		private global::System.Windows.Forms.ToolStripMenuItem deviceToolStripMenuItem;

		// Token: 0x04000244 RID: 580
		private global::System.Windows.Forms.ToolStripMenuItem programmerToolStripMenuItem;

		// Token: 0x04000245 RID: 581
		private global::System.Windows.Forms.ToolStripMenuItem readDeviceToolStripMenuItem;

		// Token: 0x04000246 RID: 582
		private global::System.Windows.Forms.ToolStripMenuItem writeDeviceToolStripMenuItem;

		// Token: 0x04000247 RID: 583
		private global::System.Windows.Forms.ToolStripMenuItem verifyToolStripMenuItem;

		// Token: 0x04000248 RID: 584
		private global::System.Windows.Forms.ToolStripMenuItem eraseToolStripMenuItem;

		// Token: 0x04000249 RID: 585
		private global::System.Windows.Forms.ToolStripMenuItem blankCheckToolStripMenuItem;

		// Token: 0x0400024A RID: 586
		private global::System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;

		// Token: 0x0400024B RID: 587
		private global::System.Windows.Forms.ToolStripMenuItem enableCodeProtectToolStripMenuItem;

		// Token: 0x0400024C RID: 588
		private global::System.Windows.Forms.ToolStripMenuItem targetPowerToolStripMenuItem;

		// Token: 0x0400024D RID: 589
		private global::System.Windows.Forms.ToolStripMenuItem fastProgrammingToolStripMenuItem;

		// Token: 0x0400024E RID: 590
		private global::System.Windows.Forms.ToolStripMenuItem checkCommunicationToolStripMenuItem;

		// Token: 0x0400024F RID: 591
		private global::System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;

		// Token: 0x04000250 RID: 592
		private global::System.Windows.Forms.ToolStripMenuItem downloadPICkit2FirmwareToolStripMenuItem;

		// Token: 0x04000251 RID: 593
		private global::System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;

		// Token: 0x04000252 RID: 594
		private global::System.Windows.Forms.ToolStripMenuItem usersGuideToolStripMenuItem;

		// Token: 0x04000253 RID: 595
		private global::System.Windows.Forms.ToolStripMenuItem readMeToolStripMenuItem;

		// Token: 0x04000254 RID: 596
		private global::System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;

		// Token: 0x04000255 RID: 597
		private global::System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;

		// Token: 0x04000256 RID: 598
		private global::System.Windows.Forms.GroupBox statusGroupBox;

		// Token: 0x04000257 RID: 599
		private global::System.Windows.Forms.PictureBox pictureBoxLogo;

		// Token: 0x04000258 RID: 600
		private global::System.Windows.Forms.Label labelDevice;

		// Token: 0x04000259 RID: 601
		private global::System.Windows.Forms.Label labelChecksum;

		// Token: 0x0400025A RID: 602
		private global::System.Windows.Forms.Label labelUserIDs;

		// Token: 0x0400025B RID: 603
		private global::System.Windows.Forms.DataGridView dataGridConfigWords;

		// Token: 0x0400025C RID: 604
		private global::System.Windows.Forms.Label displayUserIDs;

		// Token: 0x0400025D RID: 605
		private global::System.Windows.Forms.Label displayDevice;

		// Token: 0x0400025E RID: 606
		private global::System.Windows.Forms.Label displayChecksum;

		// Token: 0x0400025F RID: 607
		private global::System.Windows.Forms.Label labelOSCCAL;

		// Token: 0x04000260 RID: 608
		private global::System.Windows.Forms.Label displayBandGap;

		// Token: 0x04000261 RID: 609
		private global::System.Windows.Forms.Label labelBandGap;

		// Token: 0x04000262 RID: 610
		private global::System.Windows.Forms.Label displayOSCCAL;

		// Token: 0x04000263 RID: 611
		private global::System.Windows.Forms.Label displayStatusWindow;

		// Token: 0x04000264 RID: 612
		private global::System.Windows.Forms.Button buttonRead;

		// Token: 0x04000265 RID: 613
		private global::System.Windows.Forms.ProgressBar progressBar1;

		// Token: 0x04000266 RID: 614
		private global::System.Windows.Forms.Button buttonWrite;

		// Token: 0x04000267 RID: 615
		private global::System.Windows.Forms.Button buttonVerify;

		// Token: 0x04000268 RID: 616
		private global::System.Windows.Forms.Button buttonErase;

		// Token: 0x04000269 RID: 617
		private global::System.Windows.Forms.Button buttonBlankCheck;

		// Token: 0x0400026A RID: 618
		private global::System.Windows.Forms.CheckBox chkBoxVddOn;

		// Token: 0x0400026B RID: 619
		private global::System.Windows.Forms.NumericUpDown numUpDnVDD;

		// Token: 0x0400026C RID: 620
		private global::System.Windows.Forms.GroupBox groupBoxVDD;

		// Token: 0x0400026D RID: 621
		private global::System.Windows.Forms.GroupBox groupBoxProgMem;

		// Token: 0x0400026E RID: 622
		private global::System.Windows.Forms.CheckBox checkBoxProgMemEnabled;

		// Token: 0x0400026F RID: 623
		private global::System.Windows.Forms.ComboBox comboBoxProgMemView;

		// Token: 0x04000270 RID: 624
		private global::System.Windows.Forms.Label labelDataSource;

		// Token: 0x04000271 RID: 625
		private global::System.Windows.Forms.Label displayDataSource;

		// Token: 0x04000272 RID: 626
		private global::System.Windows.Forms.DataGridView dataGridProgramMemory;

		// Token: 0x04000273 RID: 627
		private global::System.Windows.Forms.OpenFileDialog openHexFileDialog;

		// Token: 0x04000274 RID: 628
		private global::System.Windows.Forms.SaveFileDialog saveHexFileDialog;

		// Token: 0x04000275 RID: 629
		private global::System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;

		// Token: 0x04000276 RID: 630
		private global::System.Windows.Forms.ToolStripMenuItem verifyOnWriteToolStripMenuItem;

		// Token: 0x04000277 RID: 631
		private global::System.Windows.Forms.OpenFileDialog openFWFile;

		// Token: 0x04000278 RID: 632
		private global::System.Windows.Forms.ToolStripMenuItem writeOnPICkitButtonToolStripMenuItem;

		// Token: 0x04000279 RID: 633
		private global::System.Windows.Forms.Timer timerButton;

		// Token: 0x0400027A RID: 634
		private global::System.Windows.Forms.GroupBox groupBoxEEMem;

		// Token: 0x0400027B RID: 635
		private global::System.Windows.Forms.Button buttonExportHex;

		// Token: 0x0400027C RID: 636
		private global::System.Windows.Forms.ComboBox comboBoxEE;

		// Token: 0x0400027D RID: 637
		private global::System.Windows.Forms.CheckBox checkBoxEEMem;

		// Token: 0x0400027E RID: 638
		private global::System.Windows.Forms.DataGridView dataGridViewEEPROM;

		// Token: 0x0400027F RID: 639
		private global::System.Windows.Forms.ToolStripMenuItem autoDetectToolStripMenuItem;

		// Token: 0x04000280 RID: 640
		private global::System.Windows.Forms.ToolStripMenuItem forcePICkit2ToolStripMenuItem;

		// Token: 0x04000281 RID: 641
		private global::System.Windows.Forms.ToolStripMenuItem forceTargetToolStripMenuItem;

		// Token: 0x04000282 RID: 642
		private global::System.Windows.Forms.PictureBox pictureBox1;

		// Token: 0x04000283 RID: 643
		private global::System.Windows.Forms.ComboBox comboBoxSelectPart;

		// Token: 0x04000284 RID: 644
		private global::System.Windows.Forms.Label labelCodeProtect;

		// Token: 0x04000285 RID: 645
		private global::System.Windows.Forms.Timer timerDLFW;

		// Token: 0x04000286 RID: 646
		private global::System.Windows.Forms.ToolStripMenuItem hex1ToolStripMenuItem;

		// Token: 0x04000287 RID: 647
		private global::System.Windows.Forms.ToolStripMenuItem hex2ToolStripMenuItem;

		// Token: 0x04000288 RID: 648
		private global::System.Windows.Forms.ToolStripMenuItem hex3ToolStripMenuItem;

		// Token: 0x04000289 RID: 649
		private global::System.Windows.Forms.ToolStripMenuItem hex4ToolStripMenuItem;

		// Token: 0x0400028A RID: 650
		private global::System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;

		// Token: 0x0400028B RID: 651
		private global::System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;

		// Token: 0x0400028C RID: 652
		private global::System.Windows.Forms.ToolStripMenuItem enableDataProtectStripMenuItem;

		// Token: 0x0400028D RID: 653
		private global::System.Windows.Forms.ToolStripMenuItem lpcUsersGuideToolStripMenuItem;

		// Token: 0x0400028E RID: 654
		private global::System.Windows.Forms.Label displayEEProgInfo;

		// Token: 0x0400028F RID: 655
		private global::System.Windows.Forms.ToolStripMenuItem setOSCCALToolStripMenuItem;

		// Token: 0x04000290 RID: 656
		private global::System.Windows.Forms.ToolStripMenuItem webPk2ToolStripMenuItem;

		// Token: 0x04000291 RID: 657
		private global::System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;

		// Token: 0x04000292 RID: 658
		private global::System.Windows.Forms.ToolStripMenuItem troubleshhotToolStripMenuItem;

		// Token: 0x04000293 RID: 659
		private global::System.Windows.Forms.CheckBox checkBoxMCLR;

		// Token: 0x04000294 RID: 660
		private global::System.Windows.Forms.ToolStripMenuItem MCLRtoolStripMenuItem;

		// Token: 0x04000295 RID: 661
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTestMemory;

		// Token: 0x04000296 RID: 662
		private global::System.Windows.Forms.CheckBox checkBoxAutoImportWrite;

		// Token: 0x04000297 RID: 663
		private global::System.Windows.Forms.Timer timerAutoImportWrite;

		// Token: 0x04000298 RID: 664
		private global::System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;

		// Token: 0x04000299 RID: 665
		private global::System.Windows.Forms.Label displayRev;

		// Token: 0x0400029A RID: 666
		private global::System.Windows.Forms.ToolStripMenuItem uG44pinToolStripMenuItem;

		// Token: 0x0400029B RID: 667
		private global::System.Windows.Forms.Button buttonShowIDMem;

		// Token: 0x0400029C RID: 668
		private global::System.Windows.Forms.ToolStripMenuItem VppFirstToolStripMenuItem;

		// Token: 0x0400029D RID: 669
		private global::System.Windows.Forms.CheckBox checkBoxA1CS;

		// Token: 0x0400029E RID: 670
		private global::System.Windows.Forms.CheckBox checkBoxA0CS;

		// Token: 0x0400029F RID: 671
		private global::System.Windows.Forms.CheckBox checkBoxA2CS;

		// Token: 0x040002A0 RID: 672
		private global::System.Windows.Forms.ToolStripMenuItem calibrateToolStripMenuItem;

		// Token: 0x040002A1 RID: 673
		private global::System.Windows.Forms.Label labelOSSCALInvalid;

		// Token: 0x040002A2 RID: 674
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x040002A3 RID: 675
		private global::System.Windows.Forms.ToolStripMenuItem UARTtoolStripMenuItem;

		// Token: 0x040002A4 RID: 676
		private global::System.Windows.Forms.CheckBox checkBoxProgMemEnabledAlt;

		// Token: 0x040002A5 RID: 677
		private global::System.Windows.Forms.CheckBox checkBoxEEDATAMemoryEnabledAlt;

		// Token: 0x040002A6 RID: 678
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItemView;

		// Token: 0x040002A7 RID: 679
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSingleWindow;

		// Token: 0x040002A8 RID: 680
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMultiWindow;

		// Token: 0x040002A9 RID: 681
		private global::System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;

		// Token: 0x040002AA RID: 682
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItemShowProgramMemory;

		// Token: 0x040002AB RID: 683
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItemShowEEPROMData;

		// Token: 0x040002AC RID: 684
		private global::System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;

		// Token: 0x040002AD RID: 685
		private global::System.Windows.Forms.ToolStripMenuItem pICkit2GoToolStripMenuItem;

		// Token: 0x040002AE RID: 686
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItemManualSelect;

		// Token: 0x040002AF RID: 687
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItemProgToGo;

		// Token: 0x040002B0 RID: 688
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLogicTool;

		// Token: 0x040002B1 RID: 689
		private global::System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

		// Token: 0x040002B2 RID: 690
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItemContextSelectAll;

		// Token: 0x040002B3 RID: 691
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItemContextCopy;

		// Token: 0x040002B4 RID: 692
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLogicToolUG;

		// Token: 0x040002B5 RID: 693
		private global::System.Windows.Forms.ToolStripMenuItem calSetManuallyToolStripMenuItem;

		// Token: 0x040002B6 RID: 694
		private global::System.Windows.Forms.ToolStripMenuItem calAutoRegenerateToolStripMenuItem;

		// Token: 0x040002B7 RID: 695
		private global::System.Windows.Forms.Timer timerInitalUpdate;

		// Token: 0x040002B8 RID: 696
		private global::System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;

		// Token: 0x040002B9 RID: 697
		private global::System.Windows.Forms.ToolStripMenuItem mainWindowAlwaysInFrontToolStripMenuItem;

		// Token: 0x040002BA RID: 698
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItemClearBuffersErase;

		// Token: 0x040002BB RID: 699
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSounds;

		// Token: 0x040002BC RID: 700
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDisplayUnimplConfigAs;

		// Token: 0x040002BD RID: 701
		private global::System.Windows.Forms.ToolStripMenuItem as0BitValueToolStripMenuItem;

		// Token: 0x040002BE RID: 702
		private global::System.Windows.Forms.ToolStripMenuItem as1BitValueToolStripMenuItem;

		// Token: 0x040002BF RID: 703
		private global::System.Windows.Forms.ToolStripMenuItem asReadOrImportedToolStripMenuItem;

		// Token: 0x040002C0 RID: 704
		private global::System.Windows.Forms.Label labelConfig;

		// Token: 0x040002C1 RID: 705
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLVPEnabled;

		// Token: 0x040002C2 RID: 706
		private global::System.Windows.Forms.Label labelLVP;

		// Token: 0x040002C3 RID: 707
		private global::System.Windows.Forms.Label labelConfig9;
	}
}
