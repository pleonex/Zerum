//
//  ScenesManager.cs
//
//  Author:
//       Benito Palacios Sánchez <benito356@gmail.com>
//
//  Copyright (c) 2015 Benito Palacios Sánchez
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using YAXLib;

namespace Zerum.Info
{
	public class ScenesManager
	{
		static readonly ScenesManager instance = new ScenesManager();
		readonly string Filename = "scenes.xml";
		readonly string executablePath;
		readonly string filepath;
		Dictionary<string, SceneInfo> scenes;

		private ScenesManager()
		{
			executablePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			filepath = Path.Combine(executablePath, Filename);
			Read();
		}

		public static ScenesManager Instance {
			get { return instance; }
		}

		public IEnumerable<string> GetScenesName()
		{
			return scenes.Keys;
		}

		public SceneInfo LoadScene(string name)
		{
			if (!scenes.ContainsKey(name))
				return null;

			Environment.CurrentDirectory = executablePath;
			Environment.CurrentDirectory = scenes[name].WorkDir;
			Libgame.Configuration.Initialize(XDocument.Load(scenes[name].ConfigFile));

			return scenes[name];
		}

		public SceneInfo this[string name] {
			get { return LoadScene(name); }
		}

		private void Read()
		{
			var deserializer = new YAXSerializer(typeof(SceneInfo));
			var document = XDocument.Load(filepath);
			scenes = new Dictionary<string, SceneInfo>();

			foreach (var sceneXml in document.Root.Elements()) {
				var scene = (SceneInfo)deserializer.Deserialize(sceneXml);
				scenes.Add(scene.Name, scene);
			}
		}
	}
}

