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
using MediaPortal.Configuration;

namespace MediaPortal.Player.WMPlayer
{
	public class ConfigProfile
	{
		public const string ConfigSection = "WMPlayer";
		public static class PropNames
		{
			public const string Extensions = "Extensions";
			public const string SeekIncrement = "SeekIncrement";
			public const string UseForCDDA = "UseForCDDA";
			public const string UseForWebStream = "UseForWebStream";
		}

		public static class Defaults
		{
			public const int SeekIncrement = 20; // sec
			public const bool UseForCDDA = true;
			public const bool UseForWebStream = true;
			public const string Extensions = 
				".wav,.mp3,.wma,.ac3,.dts,.asx";
		}

		private string _Extensions = Defaults.Extensions;
		private int _SeekIncrement = Defaults.SeekIncrement; //sec
		private bool _UseForCDDA = Defaults.UseForCDDA;
		private bool _UseForWebStream = Defaults.UseForWebStream;

		public string Extensions
		{
			get
			{
				return _Extensions;
			}
			set
			{
				_Extensions = value;
			}
		}
		public int SeekIncrement
		{
			get
			{
				return _SeekIncrement;
			}
			set
			{
				_SeekIncrement = value;
			}
		}
		public bool UseForCDDA
		{
			get
			{
				return _UseForCDDA;
			}
			set
			{
				_UseForCDDA = value;
			}
		}
		public bool UseForWebStream
		{
			get
			{
				return _UseForWebStream;
			}
			set
			{
				_UseForWebStream = value;
			}
		}

		public void LoadSettings()
		{
			using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
			{
				string section = ConfigSection;

				Extensions =
					xmlreader.GetValueAsString(section, PropNames.Extensions, Defaults.Extensions);

				SeekIncrement =
					xmlreader.GetValueAsInt(section, PropNames.SeekIncrement, Defaults.SeekIncrement);

				UseForCDDA =
					xmlreader.GetValueAsBool(section, PropNames.UseForCDDA, Defaults.UseForCDDA);

				UseForWebStream =
					xmlreader.GetValueAsBool(section, PropNames.UseForWebStream, Defaults.UseForWebStream);
			}
		}

		public void SaveSettings()
		{
			using (MediaPortal.Profile.Settings xmlWriter = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
			{
				string section = ConfigSection;

				xmlWriter.SetValue(section, PropNames.Extensions, Extensions);
				xmlWriter.SetValue(section, PropNames.SeekIncrement, SeekIncrement);
				xmlWriter.SetValueAsBool(section, PropNames.UseForCDDA, UseForCDDA);
				xmlWriter.SetValueAsBool(section, PropNames.UseForWebStream, UseForWebStream);
			}
		}
	}
}

