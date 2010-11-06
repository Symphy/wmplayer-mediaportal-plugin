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
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Timers;
using MediaPortal.GUI.Library;
using AxWMPLib;
using WMPLib;

namespace MediaPortal.Player.WMPlayer
{
	public class WMPlayer : IExternalPlayer, IDisposable
	{
		#region Static Interface

		public static g_Player.ShowFullScreenWindowHandler FullScreenHandler = new g_Player.ShowFullScreenWindowHandler(ActivateFullScreen);
		private static g_Player.ShowFullScreenWindowHandler _SavedFullScreenHandler;

		private static bool ActivateFullScreen()
		{
			if (g_Player.Player != null && g_Player.Player.GetType().FullName == "MediaPortal.Player.WMPlayer.WMPlayer")
			{
				if (GUIWindowManager.ActiveWindow != (int)GUIWindow.Window.WINDOW_FULLSCREEN_MUSIC)
				{
					GUIWindowManager.ActivateWindow((int)GUIWindow.Window.WINDOW_FULLSCREEN_MUSIC);
					GUIGraphicsContext.IsFullScreenVideo = true;
				}
				return true;
			}
			else
				return _SavedFullScreenHandler.Invoke();
		}

		#endregion

		#region Fields

		private AxWindowsMediaPlayer _AxWMP = null;
		private int _BufferTime = 0;
		string _CurrentFile = "";
		private bool _Initialized = false;
		bool _NotifyPlaying = true;
		PlayState _PlayState = PlayState.Init;
		private ConfigProfile _Profile = null;
		private string[] _SupportedExtensions = null;
		Timer _Timer = new Timer(150);
		private bool _TimerFlag = false;
		WaitCursor _WaitCursor = null;
		private bool _WMPPlayCalled = false;

		#endregion

		#region IExternalPlayer Interface

		public override string AuthorName
		{
			get
			{
				// Get all Company attributes on this assembly
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
				// If there aren't any Company attributes, return an empty string
				if (attributes.Length == 0)
					return "";
				// If there is a Company attribute, return its value
				return ((AssemblyCompanyAttribute)attributes[0]).Company;
			}
		}

		public override string Description()
		{
			// Get all Description attributes on this assembly
			object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
			// If there aren't any Description attributes, return an empty string
			if (attributes.Length == 0)
				return "";
			// If there is a Description attribute, return its value
			return ((AssemblyDescriptionAttribute)attributes[0]).Description;
		}

		public override string[] GetAllSupportedExtensions()
		{
			LoadSettings();
			return _SupportedExtensions;
		}

		public override string PlayerName
		{
			get
			{
				// Get all Title attributes on this assembly
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				// If there is at least one Title attribute
				if (attributes.Length > 0)
				{
					// Select the first one
					AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
					// If it is not an empty string, return it
					if (titleAttribute.Title != "")
						return titleAttribute.Title;
				}
				// If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
				return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
			}
		}

		public override bool SupportsFile(string filename)
		{
			LoadSettings();

			FileType fileType = Utils.GetFileType(filename);
			bool supported = false;

			if (fileType.FileMainType == FileMainType.AudioFile)
			{
				string ext = Path.GetExtension(filename).ToLower();
				supported = SupportsExtension(ext);
			}
			else if (fileType.FileMainType == FileMainType.WebStream && fileType.FileSubType == FileSubType.None)
			{
				if (_Profile.UseForWebStream)
				{
					Uri uri = new Uri(filename);
					string ext = Path.GetExtension(uri.AbsolutePath).ToLower();
					supported = (ext == "" || SupportsExtension(ext));
				}
			}
			else if (fileType.FileMainType == FileMainType.CDTrack)
			{
				supported = _Profile.UseForCDDA;
			}
			else
			{
				supported = (fileType.FileMainType != FileMainType.Unknown);
			}

			Log.Debug("WMPlayer: SupportsFile(\"{0}\"): {1}", filename, supported ? "Supported" : "Not supported");

			return supported;
		}

		public override void ShowPlugin()
		{
			ConfigurationForm confForm = new ConfigurationForm();
			confForm.ShowDialog();
		}

		public override string VersionNumber
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
		}

		#endregion

		#region IPlayer Interface

		public override string CurrentFile
		{
			get { return _CurrentFile; }
		}

		public override double CurrentPosition
		{
			get
			{
				if (_Initialized && _PlayState != PlayState.Init)
					return _AxWMP.Ctlcontrols.currentPosition;
				else
					return 0.0d;
			}
		}

		public override double Duration
		{
			get
			{
				if (_Initialized && _PlayState != PlayState.Init)
					return _AxWMP.currentMedia.duration;
				else
					return 0.0d;
			}
		}

		public override bool Ended
		{
			get { return _PlayState == PlayState.Ended; }
		}

		public override bool HasVideo
		{
			get { return false; }
		}

		public override int PlaybackType
		{
			get
			{
				return -1;
			}
		}

		public override bool Playing
		{
			get
			{
				return (_PlayState == PlayState.Playing || _PlayState == PlayState.Paused);
			}
		}

		public override bool Paused
		{
			get
			{
				return (_PlayState == PlayState.Paused);
			}
		}

		public override bool Stopped
		{
			get
			{
				return (_PlayState == PlayState.Init);
			}
		}

		public override bool Play(string filePath)
		{
			Log.Debug("WMPlayer: Play(\"{0}\") called...", filePath);

			bool result = true;
			try
			{
				_PlayState = PlayState.Init;
				_CurrentFile = filePath;

				result = Initialize();
				if (result)
				{
					IWMPMedia media = GetCDTrackMedia(filePath);
					if (media == null)
						media = _AxWMP.newMedia(filePath);

					_AxWMP.currentMedia = media;

					while (_AxWMP.playState != WMPPlayState.wmppsReady)
						System.Threading.Thread.Sleep(50);

					// For some unknown reasons the play() call sometimes gets cancelled,
					// especially on slower systems. To overcome this, we call play() again
					// as soon as the playState changed to wmppsReady. See Process().

					// Remarkable: The change to wmppsReady or wmppsPlaying does not occur before 
					// g_Player.OnStarted() has been fired...

					_WMPPlayCalled = true;
					_AxWMP.Ctlcontrols.play();

					GUIMessage msgPb = new GUIMessage(GUIMessage.MessageType.GUI_MSG_PLAYBACK_STARTED, 0, 0, 0, 0, 0, null);
					msgPb.Label = filePath;
					GUIWindowManager.SendThreadMessage(msgPb);

					_NotifyPlaying = true;
					_PlayState = PlayState.Playing;
					_Timer.Enabled = true;
				}
			}
			catch (Exception ex)
			{
				result = false;
				Log.Error("WMPlayer: Play() caused an exception: {0}.", ex);
			}
			return result;
		}

		public override void Pause()
		{
			if (_Initialized)
			{
				if (_PlayState == PlayState.Paused)
				{
					_AxWMP.Ctlcontrols.play();
					_PlayState = PlayState.Playing;
				}
				else if (_PlayState == PlayState.Playing)
				{
					_AxWMP.Ctlcontrols.pause();
					_PlayState = PlayState.Paused;
				}
			}
		}

		public override void Stop()
		{
			if (_Initialized && _PlayState != PlayState.Init)
			{
				_AxWMP.Ctlcontrols.stop();
				_PlayState = PlayState.Init;
			}
		}

		public override void Process()
		{
			if (_Initialized && _TimerFlag)
			{
				_TimerFlag = false;

				if (_WMPPlayCalled && _AxWMP.playState == WMPPlayState.wmppsReady)
					_AxWMP.Ctlcontrols.play();

				if (Playing)
				{
					if (GUIGraphicsContext.BlankScreen)
					{
						if (_AxWMP.Visible)
							_AxWMP.Visible = false;
					}
					else
					{
						// Fix:
						// g_Player sets GUIGraphicsContext.IsFullScreenVideo to false on Stop().
						GUIGraphicsContext.IsFullScreenVideo =
							GUIGraphicsContext.IsFullScreenVideo ||
							(GUIWindowManager.ActiveWindow == (int)GUIWindow.Window.WINDOW_FULLSCREEN_MUSIC);

						if (GUIGraphicsContext.IsFullScreenVideo)
						{
							Point location = new Point(GUIGraphicsContext.OverScanLeft, GUIGraphicsContext.OverScanTop);
							Size size = new Size(GUIGraphicsContext.OverScanWidth, GUIGraphicsContext.OverScanHeight + 60);
							Region region = new Region(new Rectangle(
								new Point(0, 0),
								new Size(GUIGraphicsContext.OverScanWidth, GUIGraphicsContext.OverScanHeight)));

							if (location != _AxWMP.Location)
								_AxWMP.Location = location;

							if (size != _AxWMP.Size)
								_AxWMP.Size = size;

							if (region != _AxWMP.Region)
								_AxWMP.Region = region;

							if (!_AxWMP.Visible)
								_AxWMP.Visible = true;
						}
						else
						{
							if (_AxWMP.Visible)
								_AxWMP.Visible = false;
						}
					}
					if (_NotifyPlaying && CurrentPosition >= 10.0)
					{
						_NotifyPlaying = false;
						GUIMessage msg = new GUIMessage(GUIMessage.MessageType.GUI_MSG_PLAYING_10SEC, 0, 0, 0, 0, 0, null);
						msg.Label = CurrentFile;
						GUIWindowManager.SendThreadMessage(msg);
					}
				}
				else
				{
					if (_AxWMP != null && _AxWMP.Visible)
						_AxWMP.Visible = false;
				}
			}
		}

		public override void Dispose()
		{
      // This method is called on each Stop(), allthough the player object 
      // is kept alive in the player factory.
      // This means we cannot use this as a real Dispose.
      
      _Timer.Enabled = false;
		}

		#endregion

		#region Public Instance Interface

    public void RealDispose()
    {
      // See Dispose()
      
      if (_AxWMP != null)
        _AxWMP.Dispose();
    }

		#endregion

		#region Private Instance Interface

		private bool SupportsExtension(string ext)
		{
			bool supported = false;
			for (int i = 0; i < _SupportedExtensions.Length; i++)
			{
				if (_SupportedExtensions[i].Equals(ext))
				{
					supported = true;
					break;
				}
			}
			return supported;
		}

		private bool Initialize()
		{
			bool result = true;
			if (!_Initialized)
			{
				Log.Info("WMPlayer: Initializing player ...");

				LoadSettings();

				_AxWMP = new AxWindowsMediaPlayer();
				GUIGraphicsContext.form.SuspendLayout();
				GUIGraphicsContext.form.Controls.Add(_AxWMP);
				GUIGraphicsContext.form.ResumeLayout(false);

				_AxWMP.BeginInit();

				_AxWMP.Name = "AxWindowsMediaPlayer";
				_AxWMP.Visible = false;
				_AxWMP.TabIndex = 0;
				_AxWMP.TabStop = false;
				_AxWMP.Location = new Point(0, 0);
				_AxWMP.Size = new Size(1, 1);
				// Only in "full" mode advanced Visualizations are supported.
				// The controls are hidden by positioning them outside the window.
				// See also Process().
				_AxWMP.uiMode = "full";
				_AxWMP.settings.autoStart = false;
				_AxWMP.settings.enableErrorDialogs = false;
				_AxWMP.settings.volume = 100;
				_AxWMP.network.bufferingTime = _BufferTime;
				_AxWMP.windowlessVideo = false;
				_AxWMP.enableContextMenu = false;
				_AxWMP.Ctlenabled = false;
				_AxWMP.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(OnPlayStateChange);
				_AxWMP.Buffering += new AxWMPLib._WMPOCXEvents_BufferingEventHandler(OnBuffering);
				_AxWMP.EndInit();

				_SavedFullScreenHandler = g_Player.ShowFullScreenWindowVideo;
				g_Player.ShowFullScreenWindowVideo = FullScreenHandler;

				GUIGraphicsContext.OnNewAction += new OnActionHandler(OnNewAction);
        GUIGraphicsContext.form.Disposed += new EventHandler(OnAppFormDisposed);
        _Timer.Elapsed += new System.Timers.ElapsedEventHandler(_Timer_Elapsed);

				if (result)
				{
					Log.Info("WMPlayer: Initializing complete.");
					_Initialized = true;
				}
			}
			return result;
		}

		private void LoadSettings()
		{
			if (_Profile == null)
			{
				_Profile = new ConfigProfile();
				_Profile.LoadSettings();
				_SupportedExtensions = _Profile.Extensions.Split(new string[] { "," }, StringSplitOptions.None);
			}
		}

		private void Rewind()
		{
			Seek(-_Profile.SeekIncrement);
		}

		private void Forward()
		{
			Seek(_Profile.SeekIncrement);
		}

		private void Seek(int seconds)
		{
			if (_Initialized && _AxWMP.playState == WMPLib.WMPPlayState.wmppsPlaying)
			{
				double newPos = _AxWMP.Ctlcontrols.currentPosition + seconds;
				if (newPos < 0)
					newPos = 0;
				else if (newPos > Duration)
					newPos = Duration;

				int volume = _AxWMP.settings.volume;
				_AxWMP.settings.volume = 0;
				_AxWMP.Ctlcontrols.currentPosition = newPos;
				_AxWMP.settings.volume = volume;
			}
		}

		private IWMPMedia GetCDTrackMedia(string filePath)
		{
			IWMPMedia media = null;

			bool result = false;
			int drive = 0;
			int track = 0;

			if (filePath.StartsWith("cdda:"))
			{
				drive = 0;
				track = Convert.ToInt32(filePath.Substring(5));
				result = true;
			}
			else if (Path.GetExtension(filePath) == ".cda")
			{
				int pos = filePath.IndexOf(".cda");
				if (pos >= 0)
				{
					string strTrack = "";
					pos--;
					while (Char.IsDigit(filePath[pos]) && pos > 0)
					{
						strTrack = filePath[pos] + strTrack;
						pos--;
					}
					track = Convert.ToInt32(strTrack);

					string root = Path.GetPathRoot(filePath);
					if (!String.IsNullOrEmpty(root))
					{
						string strDrive = root.Substring(0, 2);

						if (_AxWMP.cdromCollection.count > 0)
						{
							drive = 0;
							while ((_AxWMP.cdromCollection.Item(drive).driveSpecifier != strDrive) && (drive < _AxWMP.cdromCollection.count))
							{
								drive++;
							}
							result = true;
						}
					}
				}
			}
			if (result)
			{
				if (_AxWMP.cdromCollection.count > 0)
				{
					IWMPPlaylist playList = _AxWMP.cdromCollection.Item(drive).Playlist;
					if (playList != null)
					{
						if (track <= playList.count)
						{
							media = playList.get_Item(track - 1);
							Log.Info("WMPlayer: CD track:{0}/{1}", track, playList.count);
						}
					}
				}
			}
			return media;
		}

		#endregion

		#region Event Handlers

		private void OnPlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
		{
			Log.Debug("WMPlayer: _AxWMP.playStateChange: " + _AxWMP.playState.ToString());
			switch (_AxWMP.playState)
			{
				case WMPLib.WMPPlayState.wmppsBuffering:
					if (_WaitCursor == null)
						_WaitCursor = new WaitCursor();
					break;

				case WMPLib.WMPPlayState.wmppsPlaying:
					_WMPPlayCalled = false;
					if (_WaitCursor != null)
					{
						_WaitCursor.Dispose();
						_WaitCursor = null;
					}
					break;

				case WMPLib.WMPPlayState.wmppsStopped:
					_AxWMP.Visible = false;
					_PlayState = PlayState.Ended;
					break;
			}
		}

		private void OnBuffering(object sender, _WMPOCXEvents_BufferingEvent e)
		{
			if (!e.start)
			{
				Log.Debug("WMPlayer: bandWidth: {0}", _AxWMP.network.bandWidth);
				Log.Debug("WMPlayer: bitRate: {0}", _AxWMP.network.bitRate);
				Log.Debug("WMPlayer: receivedPackets: {0}", _AxWMP.network.receivedPackets);
				Log.Debug("WMPlayer: receptionQuality: {0}", _AxWMP.network.receptionQuality);
			}
		}

		private void OnNewAction(Action action)
		{
			//Log.Debug(
			// "BASS: Action: {0}",
			// Enum.GetName(typeof(Action.ActionType), action.wID));

			switch (action.wID)
			{
				case Action.ActionType.ACTION_FORWARD:
				case Action.ActionType.ACTION_MUSIC_FORWARD:
					Forward();
					break;

				case Action.ActionType.ACTION_REWIND:
				case Action.ActionType.ACTION_MUSIC_REWIND:
					Rewind();
					break;

				case Action.ActionType.ACTION_TOGGLE_MUSIC_GAP:
					break;
			}
		}

    void OnAppFormDisposed(object sender, EventArgs e)
    {
      RealDispose();
    }

    private void _Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			_TimerFlag = true;
		}

		#endregion
	}
}