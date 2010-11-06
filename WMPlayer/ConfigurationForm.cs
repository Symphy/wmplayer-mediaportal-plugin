#region Copyright (C) 2005-2010 Team MediaPortal

// Copyright (C) 2005-2010 Team MediaPortal
// http://www.team-mediaportal.com
// 
// MediaPortal is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
// 
// MediaPortal is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with MediaPortal. If not, see <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Drawing;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace MediaPortal.Player.WMPlayer
{
  /// <summary>
  /// Summary description for Configuration.
  /// </summary>
  public class ConfigurationForm : System.Windows.Forms.Form
  {
    private WMPlayer _player = new WMPlayer();
    private ConfigProfile _Profile = new ConfigProfile();

    private MediaPortal.UserInterface.Controls.MPButton btnOk;
    private MediaPortal.UserInterface.Controls.MPButton btnCancel;
    private TreeView tvMenu;
    private TabControl tcConfig;
    private TabPage tabPageAbout;
    private TabPage tabPageGeneral;
    private TabPage tabPageExtensions;
    private GroupBox gbAbout;
    private GroupBox gbExtensions;
    private TextBox tbExtension;
    private MediaPortal.UserInterface.Controls.MPButton mpButtonAddExt;
    private MediaPortal.UserInterface.Controls.MPButton mpButtonDeleteExt;
    private MediaPortal.UserInterface.Controls.MPLabel lblLabel1;
    private MediaPortal.UserInterface.Controls.MPLabel lblLabel2;
    private MediaPortal.UserInterface.Controls.MPLabel lblLabel4;
    private MediaPortal.UserInterface.Controls.MPLabel lblLabel3;
    private MediaPortal.UserInterface.Controls.MPLabel lblLabel5;
    private MediaPortal.UserInterface.Controls.MPLabel mpLabelPlayerName;
    private MediaPortal.UserInterface.Controls.MPLabel mpLabelAuthorName;
    private MediaPortal.UserInterface.Controls.MPLabel mpLabelVersion;
    private MediaPortal.UserInterface.Controls.MPLabel mpLabelDescription;
    private MediaPortal.UserInterface.Controls.MPButton mpButtonDefault;
    private ListView listViewExtensions;
    private MediaPortal.UserInterface.Controls.MPGradientLabel ucHeader;
    private MediaPortal.UserInterface.Controls.MPBeveledLine beveledLine;
    private GroupBox gbGeneral;
    private LinkLabel lnkSoundProperties;
    private LinkLabel lnkWMP;
    private GroupBox groupBox1;
    private CheckBox chkUseForCDDA;
    private CheckBox chkUseForWebStream;
    private LinkLabel lnkForum;
    private LinkLabel lnkDownload;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    public ConfigurationForm()
    {
      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (components != null)
        {
          components.Dispose();
        }
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Extensions");
      System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("General Settings", new System.Windows.Forms.TreeNode[] {
            treeNode1});
      System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("WMPlayer Plugin", new System.Windows.Forms.TreeNode[] {
            treeNode2});
      this.btnOk = new MediaPortal.UserInterface.Controls.MPButton();
      this.btnCancel = new MediaPortal.UserInterface.Controls.MPButton();
      this.tcConfig = new System.Windows.Forms.TabControl();
      this.tabPageAbout = new System.Windows.Forms.TabPage();
      this.gbAbout = new System.Windows.Forms.GroupBox();
      this.lnkForum = new System.Windows.Forms.LinkLabel();
      this.lnkDownload = new System.Windows.Forms.LinkLabel();
      this.lblLabel1 = new MediaPortal.UserInterface.Controls.MPLabel();
      this.mpLabelPlayerName = new MediaPortal.UserInterface.Controls.MPLabel();
      this.lblLabel2 = new MediaPortal.UserInterface.Controls.MPLabel();
      this.mpLabelAuthorName = new MediaPortal.UserInterface.Controls.MPLabel();
      this.lblLabel3 = new MediaPortal.UserInterface.Controls.MPLabel();
      this.mpLabelVersion = new MediaPortal.UserInterface.Controls.MPLabel();
      this.lblLabel4 = new MediaPortal.UserInterface.Controls.MPLabel();
      this.mpLabelDescription = new MediaPortal.UserInterface.Controls.MPLabel();
      this.tabPageGeneral = new System.Windows.Forms.TabPage();
      this.gbGeneral = new System.Windows.Forms.GroupBox();
      this.lnkSoundProperties = new System.Windows.Forms.LinkLabel();
      this.lnkWMP = new System.Windows.Forms.LinkLabel();
      this.tabPageExtensions = new System.Windows.Forms.TabPage();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.chkUseForWebStream = new System.Windows.Forms.CheckBox();
      this.chkUseForCDDA = new System.Windows.Forms.CheckBox();
      this.gbExtensions = new System.Windows.Forms.GroupBox();
      this.lblLabel5 = new MediaPortal.UserInterface.Controls.MPLabel();
      this.listViewExtensions = new System.Windows.Forms.ListView();
      this.tbExtension = new System.Windows.Forms.TextBox();
      this.mpButtonDefault = new MediaPortal.UserInterface.Controls.MPButton();
      this.mpButtonDeleteExt = new MediaPortal.UserInterface.Controls.MPButton();
      this.mpButtonAddExt = new MediaPortal.UserInterface.Controls.MPButton();
      this.tvMenu = new System.Windows.Forms.TreeView();
      this.ucHeader = new MediaPortal.UserInterface.Controls.MPGradientLabel();
      this.beveledLine = new MediaPortal.UserInterface.Controls.MPBeveledLine();
      this.tcConfig.SuspendLayout();
      this.tabPageAbout.SuspendLayout();
      this.gbAbout.SuspendLayout();
      this.tabPageGeneral.SuspendLayout();
      this.gbGeneral.SuspendLayout();
      this.tabPageExtensions.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.gbExtensions.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(559, 371);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 23);
      this.btnOk.TabIndex = 3;
      this.btnOk.Text = "Ok";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new System.EventHandler(this.mpButtonOk_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(640, 371);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 4;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // tcConfig
      // 
      this.tcConfig.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
      this.tcConfig.Controls.Add(this.tabPageAbout);
      this.tcConfig.Controls.Add(this.tabPageGeneral);
      this.tcConfig.Controls.Add(this.tabPageExtensions);
      this.tcConfig.Location = new System.Drawing.Point(204, 13);
      this.tcConfig.Name = "tcConfig";
      this.tcConfig.SelectedIndex = 0;
      this.tcConfig.Size = new System.Drawing.Size(511, 348);
      this.tcConfig.TabIndex = 2;
      // 
      // tabPageAbout
      // 
      this.tabPageAbout.Controls.Add(this.gbAbout);
      this.tabPageAbout.Location = new System.Drawing.Point(4, 25);
      this.tabPageAbout.Name = "tabPageAbout";
      this.tabPageAbout.Size = new System.Drawing.Size(503, 319);
      this.tabPageAbout.TabIndex = 4;
      this.tabPageAbout.Text = "About";
      this.tabPageAbout.UseVisualStyleBackColor = true;
      // 
      // gbAbout
      // 
      this.gbAbout.Controls.Add(this.lnkForum);
      this.gbAbout.Controls.Add(this.lnkDownload);
      this.gbAbout.Controls.Add(this.lblLabel1);
      this.gbAbout.Controls.Add(this.mpLabelPlayerName);
      this.gbAbout.Controls.Add(this.lblLabel2);
      this.gbAbout.Controls.Add(this.mpLabelAuthorName);
      this.gbAbout.Controls.Add(this.lblLabel3);
      this.gbAbout.Controls.Add(this.mpLabelVersion);
      this.gbAbout.Controls.Add(this.lblLabel4);
      this.gbAbout.Controls.Add(this.mpLabelDescription);
      this.gbAbout.Location = new System.Drawing.Point(3, 3);
      this.gbAbout.Name = "gbAbout";
      this.gbAbout.Size = new System.Drawing.Size(497, 308);
      this.gbAbout.TabIndex = 19;
      this.gbAbout.TabStop = false;
      this.gbAbout.Text = "About this plugin";
      // 
      // lnkForum
      // 
      this.lnkForum.AutoSize = true;
      this.lnkForum.Location = new System.Drawing.Point(22, 149);
      this.lnkForum.Name = "lnkForum";
      this.lnkForum.Size = new System.Drawing.Size(230, 13);
      this.lnkForum.TabIndex = 20;
      this.lnkForum.TabStop = true;
      this.lnkForum.Text = "Visit forumthread at www.team-mediaportal.com";
      this.lnkForum.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkForum_LinkClicked);
      // 
      // lnkDownload
      // 
      this.lnkDownload.AutoSize = true;
      this.lnkDownload.Location = new System.Drawing.Point(22, 122);
      this.lnkDownload.Name = "lnkDownload";
      this.lnkDownload.Size = new System.Drawing.Size(176, 13);
      this.lnkDownload.TabIndex = 19;
      this.lnkDownload.TabStop = true;
      this.lnkDownload.Text = "Visit homepage at code.google.com";
      this.lnkDownload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDownload_LinkClicked);
      // 
      // lblLabel1
      // 
      this.lblLabel1.Location = new System.Drawing.Point(22, 30);
      this.lblLabel1.Name = "lblLabel1";
      this.lblLabel1.Size = new System.Drawing.Size(100, 23);
      this.lblLabel1.TabIndex = 13;
      this.lblLabel1.Text = "Name:";
      // 
      // mpLabelPlayerName
      // 
      this.mpLabelPlayerName.Location = new System.Drawing.Point(128, 30);
      this.mpLabelPlayerName.Name = "mpLabelPlayerName";
      this.mpLabelPlayerName.Size = new System.Drawing.Size(232, 23);
      this.mpLabelPlayerName.TabIndex = 18;
      this.mpLabelPlayerName.Text = "Name:";
      // 
      // lblLabel2
      // 
      this.lblLabel2.Location = new System.Drawing.Point(22, 53);
      this.lblLabel2.Name = "lblLabel2";
      this.lblLabel2.Size = new System.Drawing.Size(100, 23);
      this.lblLabel2.TabIndex = 10;
      this.lblLabel2.Text = "Description:";
      // 
      // mpLabelAuthorName
      // 
      this.mpLabelAuthorName.Location = new System.Drawing.Point(128, 99);
      this.mpLabelAuthorName.Name = "mpLabelAuthorName";
      this.mpLabelAuthorName.Size = new System.Drawing.Size(232, 23);
      this.mpLabelAuthorName.TabIndex = 17;
      this.mpLabelAuthorName.Text = "Author:";
      // 
      // lblLabel3
      // 
      this.lblLabel3.Location = new System.Drawing.Point(22, 76);
      this.lblLabel3.Name = "lblLabel3";
      this.lblLabel3.Size = new System.Drawing.Size(100, 23);
      this.lblLabel3.TabIndex = 11;
      this.lblLabel3.Text = "Version:";
      // 
      // mpLabelVersion
      // 
      this.mpLabelVersion.Location = new System.Drawing.Point(128, 76);
      this.mpLabelVersion.Name = "mpLabelVersion";
      this.mpLabelVersion.Size = new System.Drawing.Size(232, 23);
      this.mpLabelVersion.TabIndex = 16;
      this.mpLabelVersion.Text = "Version:";
      // 
      // lblLabel4
      // 
      this.lblLabel4.Location = new System.Drawing.Point(22, 99);
      this.lblLabel4.Name = "lblLabel4";
      this.lblLabel4.Size = new System.Drawing.Size(100, 23);
      this.lblLabel4.TabIndex = 12;
      this.lblLabel4.Text = "Author:";
      // 
      // mpLabelDescription
      // 
      this.mpLabelDescription.Location = new System.Drawing.Point(128, 53);
      this.mpLabelDescription.Name = "mpLabelDescription";
      this.mpLabelDescription.Size = new System.Drawing.Size(232, 23);
      this.mpLabelDescription.TabIndex = 15;
      this.mpLabelDescription.Text = "Description:";
      // 
      // tabPageGeneral
      // 
      this.tabPageGeneral.Controls.Add(this.gbGeneral);
      this.tabPageGeneral.Location = new System.Drawing.Point(4, 25);
      this.tabPageGeneral.Name = "tabPageGeneral";
      this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageGeneral.Size = new System.Drawing.Size(503, 319);
      this.tabPageGeneral.TabIndex = 0;
      this.tabPageGeneral.Text = "General";
      this.tabPageGeneral.UseVisualStyleBackColor = true;
      // 
      // gbGeneral
      // 
      this.gbGeneral.Controls.Add(this.lnkSoundProperties);
      this.gbGeneral.Controls.Add(this.lnkWMP);
      this.gbGeneral.Location = new System.Drawing.Point(3, 3);
      this.gbGeneral.Name = "gbGeneral";
      this.gbGeneral.Size = new System.Drawing.Size(497, 305);
      this.gbGeneral.TabIndex = 0;
      this.gbGeneral.TabStop = false;
      this.gbGeneral.Text = "Algemene instellingen";
      // 
      // lnkSoundProperties
      // 
      this.lnkSoundProperties.AutoSize = true;
      this.lnkSoundProperties.Location = new System.Drawing.Point(24, 76);
      this.lnkSoundProperties.Name = "lnkSoundProperties";
      this.lnkSoundProperties.Size = new System.Drawing.Size(369, 13);
      this.lnkSoundProperties.TabIndex = 38;
      this.lnkSoundProperties.TabStop = true;
      this.lnkSoundProperties.Text = "Launch Windows Audio Devices Properties to select the audiodevice to use.";
      this.lnkSoundProperties.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSoundProperties_LinkClicked);
      // 
      // lnkWMP
      // 
      this.lnkWMP.AutoSize = true;
      this.lnkWMP.Location = new System.Drawing.Point(24, 42);
      this.lnkWMP.Name = "lnkWMP";
      this.lnkWMP.Size = new System.Drawing.Size(233, 13);
      this.lnkWMP.TabIndex = 37;
      this.lnkWMP.TabStop = true;
      this.lnkWMP.Text = "Launch Windows MediaPlayer for configuration.\r\n";
      this.lnkWMP.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkWMP_LinkClicked);
      // 
      // tabPageExtensions
      // 
      this.tabPageExtensions.Controls.Add(this.groupBox1);
      this.tabPageExtensions.Controls.Add(this.gbExtensions);
      this.tabPageExtensions.Location = new System.Drawing.Point(4, 25);
      this.tabPageExtensions.Name = "tabPageExtensions";
      this.tabPageExtensions.Padding = new System.Windows.Forms.Padding(3);
      this.tabPageExtensions.Size = new System.Drawing.Size(503, 319);
      this.tabPageExtensions.TabIndex = 2;
      this.tabPageExtensions.Text = "Extensions";
      this.tabPageExtensions.UseVisualStyleBackColor = true;
      this.tabPageExtensions.Validating += new System.ComponentModel.CancelEventHandler(this.tabPageExtensions_Validating);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.chkUseForWebStream);
      this.groupBox1.Controls.Add(this.chkUseForCDDA);
      this.groupBox1.Location = new System.Drawing.Point(3, 225);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(497, 86);
      this.groupBox1.TabIndex = 2;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Options";
      // 
      // chkUseForWebStream
      // 
      this.chkUseForWebStream.AutoSize = true;
      this.chkUseForWebStream.Location = new System.Drawing.Point(15, 42);
      this.chkUseForWebStream.Name = "chkUseForWebStream";
      this.chkUseForWebStream.Size = new System.Drawing.Size(150, 17);
      this.chkUseForWebStream.TabIndex = 1;
      this.chkUseForWebStream.Text = "Use player for webstreams";
      this.chkUseForWebStream.UseVisualStyleBackColor = true;
      // 
      // chkUseForCDDA
      // 
      this.chkUseForCDDA.AutoSize = true;
      this.chkUseForCDDA.Location = new System.Drawing.Point(15, 19);
      this.chkUseForCDDA.Name = "chkUseForCDDA";
      this.chkUseForCDDA.Size = new System.Drawing.Size(155, 17);
      this.chkUseForCDDA.TabIndex = 0;
      this.chkUseForCDDA.Text = "Use player for CD playback";
      this.chkUseForCDDA.UseVisualStyleBackColor = true;
      // 
      // gbExtensions
      // 
      this.gbExtensions.Controls.Add(this.lblLabel5);
      this.gbExtensions.Controls.Add(this.listViewExtensions);
      this.gbExtensions.Controls.Add(this.tbExtension);
      this.gbExtensions.Controls.Add(this.mpButtonDefault);
      this.gbExtensions.Controls.Add(this.mpButtonDeleteExt);
      this.gbExtensions.Controls.Add(this.mpButtonAddExt);
      this.gbExtensions.Location = new System.Drawing.Point(3, 3);
      this.gbExtensions.Name = "gbExtensions";
      this.gbExtensions.Size = new System.Drawing.Size(497, 215);
      this.gbExtensions.TabIndex = 1;
      this.gbExtensions.TabStop = false;
      this.gbExtensions.Text = "Extensions this player will be used for";
      // 
      // lblLabel5
      // 
      this.lblLabel5.Location = new System.Drawing.Point(110, 21);
      this.lblLabel5.Name = "lblLabel5";
      this.lblLabel5.Size = new System.Drawing.Size(123, 18);
      this.lblLabel5.TabIndex = 35;
      this.lblLabel5.Text = "New extension to add:";
      // 
      // listViewExtensions
      // 
      this.listViewExtensions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
      this.listViewExtensions.HideSelection = false;
      this.listViewExtensions.Location = new System.Drawing.Point(6, 45);
      this.listViewExtensions.Name = "listViewExtensions";
      this.listViewExtensions.Size = new System.Drawing.Size(308, 163);
      this.listViewExtensions.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.listViewExtensions.TabIndex = 3;
      this.listViewExtensions.UseCompatibleStateImageBehavior = false;
      this.listViewExtensions.View = System.Windows.Forms.View.List;
      // 
      // tbExtension
      // 
      this.tbExtension.Location = new System.Drawing.Point(239, 19);
      this.tbExtension.Name = "tbExtension";
      this.tbExtension.Size = new System.Drawing.Size(75, 20);
      this.tbExtension.TabIndex = 1;
      // 
      // mpButtonDefault
      // 
      this.mpButtonDefault.Location = new System.Drawing.Point(320, 74);
      this.mpButtonDefault.Name = "mpButtonDefault";
      this.mpButtonDefault.Size = new System.Drawing.Size(75, 23);
      this.mpButtonDefault.TabIndex = 5;
      this.mpButtonDefault.Text = "Default";
      this.mpButtonDefault.UseVisualStyleBackColor = true;
      this.mpButtonDefault.Click += new System.EventHandler(this.mpButtonDefault_Click);
      // 
      // mpButtonDeleteExt
      // 
      this.mpButtonDeleteExt.Location = new System.Drawing.Point(320, 45);
      this.mpButtonDeleteExt.Name = "mpButtonDeleteExt";
      this.mpButtonDeleteExt.Size = new System.Drawing.Size(75, 23);
      this.mpButtonDeleteExt.TabIndex = 4;
      this.mpButtonDeleteExt.Text = "Delete";
      this.mpButtonDeleteExt.UseVisualStyleBackColor = true;
      this.mpButtonDeleteExt.Click += new System.EventHandler(this.mpButtonDeleteExt_Click);
      // 
      // mpButtonAddExt
      // 
      this.mpButtonAddExt.Location = new System.Drawing.Point(320, 16);
      this.mpButtonAddExt.Name = "mpButtonAddExt";
      this.mpButtonAddExt.Size = new System.Drawing.Size(75, 23);
      this.mpButtonAddExt.TabIndex = 2;
      this.mpButtonAddExt.Text = "Add";
      this.mpButtonAddExt.UseVisualStyleBackColor = true;
      this.mpButtonAddExt.Click += new System.EventHandler(this.mpButtonAddExt_Click);
      // 
      // tvMenu
      // 
      this.tvMenu.HideSelection = false;
      this.tvMenu.Location = new System.Drawing.Point(13, 13);
      this.tvMenu.Name = "tvMenu";
      treeNode1.Name = "NodeExtensions";
      treeNode1.Text = "Extensions";
      treeNode2.Name = "NodeGeneral";
      treeNode2.Text = "General Settings";
      treeNode3.Name = "NodeRoot";
      treeNode3.Text = "WMPlayer Plugin";
      this.tvMenu.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
      this.tvMenu.Size = new System.Drawing.Size(185, 336);
      this.tvMenu.TabIndex = 1;
      this.tvMenu.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewMenu_AfterSelect);
      // 
      // ucHeader
      // 
      this.ucHeader.Caption = "";
      this.ucHeader.FirstColor = System.Drawing.SystemColors.InactiveCaption;
      this.ucHeader.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ucHeader.LastColor = System.Drawing.Color.WhiteSmoke;
      this.ucHeader.Location = new System.Drawing.Point(204, 13);
      this.ucHeader.Name = "ucHeader";
      this.ucHeader.PaddingLeft = 2;
      this.ucHeader.Size = new System.Drawing.Size(511, 24);
      this.ucHeader.TabIndex = 5;
      this.ucHeader.TabStop = false;
      this.ucHeader.TextColor = System.Drawing.Color.WhiteSmoke;
      this.ucHeader.TextFont = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      // 
      // beveledLine
      // 
      this.beveledLine.Location = new System.Drawing.Point(13, 363);
      this.beveledLine.Name = "beveledLine";
      this.beveledLine.Size = new System.Drawing.Size(701, 2);
      this.beveledLine.TabIndex = 6;
      this.beveledLine.TabStop = false;
      // 
      // ConfigurationForm
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(727, 404);
      this.Controls.Add(this.beveledLine);
      this.Controls.Add(this.ucHeader);
      this.Controls.Add(this.tvMenu);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.tcConfig);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ConfigurationForm";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "WMPlayer Plugin Configuration";
      this.Load += new System.EventHandler(this.ConfigurationForm_Load);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConfigurationForm_FormClosed);
      this.tcConfig.ResumeLayout(false);
      this.tabPageAbout.ResumeLayout(false);
      this.gbAbout.ResumeLayout(false);
      this.gbAbout.PerformLayout();
      this.tabPageGeneral.ResumeLayout(false);
      this.gbGeneral.ResumeLayout(false);
      this.gbGeneral.PerformLayout();
      this.tabPageExtensions.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.gbExtensions.ResumeLayout(false);
      this.gbExtensions.PerformLayout();
      this.ResumeLayout(false);

    }
    #endregion

    private void ConfigurationForm_Load(object sender, System.EventArgs e)
    {
      _Profile.LoadSettings();

      tcConfig.Region =
        new Region(
        new RectangleF(
        tabPageAbout.Left,
        tabPageAbout.Top,
        tabPageAbout.Width,
        tabPageAbout.Height));

      tvMenu.ExpandAll();

      DisplayExtList();

      mpLabelPlayerName.Text = _player.PlayerName;
      mpLabelDescription.Text = _player.Description();
      mpLabelVersion.Text = _player.VersionNumber;
      mpLabelAuthorName.Text = _player.AuthorName;

      chkUseForCDDA.DataBindings.Add("Checked", _Profile, "UseForCDDA");
      chkUseForWebStream.DataBindings.Add("Checked", _Profile, "UseForWebStream");
    }

    private void treeViewMenu_AfterSelect(object sender, TreeViewEventArgs e)
    {
      ucHeader.Caption = tvMenu.SelectedNode.Text;
      string nodeName = tvMenu.SelectedNode.Name;
      switch (nodeName)
      {
        case "NodeRoot":
          tcConfig.SelectedTab = tabPageAbout;
          break;
        case "NodeGeneral":
          tcConfig.SelectedTab = tabPageGeneral;
          break;
        case "NodeExtensions":
          tcConfig.SelectedTab = tabPageExtensions;
          break;
      }
    }

    private void linkLabelDownLoad_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start(
        "http://www.team-mediaportal.com/files/Download/MediaPortalInstaller(MPI)/AudioorRadio/ASIOMusicPlayerPlugin/");
    }

    private void linkLabelForum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start(
        "http://forum.team-mediaportal.com/asio_music_player-f245.html");
    }

    private void mpButtonMMEControlPanel_Click(object sender, EventArgs e)
    {
      Process.Start("mmsys.cpl");
    }

    private void mpButtonAddExt_Click(object sender, EventArgs e)
    {
      string ext = tbExtension.Text.Trim();
      if (ext != String.Empty)
      {
        if (!ext.StartsWith("."))
          ext = "." + ext;

        ListViewItem item = listViewExtensions.Items.Add(ext);
        listViewExtensions.SelectedItems.Clear();
        item.Selected = true;
        tbExtension.Text = String.Empty;
      }
    }

    private void mpButtonDeleteExt_Click(object sender, EventArgs e)
    {
      ListView.SelectedIndexCollection selected = listViewExtensions.SelectedIndices;
      int index = 0;
      while (index < listViewExtensions.Items.Count)
      {
        if (selected.Contains(index))
          listViewExtensions.Items.RemoveAt(index);
        else
          index++;
      }
    }

    private void mpButtonDefault_Click(object sender, EventArgs e)
    {
      DialogResult result = MessageBox.Show(this, "Do you want to restore the default extensionlist?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
      if (result == DialogResult.Yes)
      {
        _Profile.Extensions = ConfigProfile.Defaults.Extensions;
        DisplayExtList();
      }
    }

    private void tabPageExtensions_Validating(object sender, CancelEventArgs e)
    {
      string ext = String.Empty;
      foreach (ListViewItem item in listViewExtensions.Items)
      {
        if (ext != String.Empty)
          ext += ",";
        ext += item.Text;
      }
      _Profile.Extensions = ext;
    }

    private void mpButtonOk_Click(object sender, System.EventArgs e)
    {
      _Profile.SaveSettings();
      Close();
    }

    private void ConfigurationForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      _player.Dispose();
    }

    private void DisplayExtList()
    {
      string[] ext = _Profile.Extensions.Split(new string[] { "," }, StringSplitOptions.None);

      listViewExtensions.Items.Clear();
      for (int i = 0; i < ext.Length; i++)
      {
        listViewExtensions.Items.Add(ext[i]);
      }
    }

    private void lnkWMP_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("wmplayer");
    }

    private void lnkSoundProperties_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("mmsys.cpl");
    }

    private void lnkDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start(
        "http://code.google.com/p/wmplayer-mediaportal-plugin");
    }

    private void lnkForum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start(
        "http://forum.team-mediaportal.com/asio-music-player-245/");
    }
  }
}
