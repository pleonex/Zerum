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
		readonly string Filepath;
		Dictionary<string, SceneInfo> scenes;

		private ScenesManager()
		{
			string execPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			Filepath = Path.Combine(execPath, Filename);
			Read();
		}

		public static ScenesManager Instance {
			get { return instance; }
		}

		public string[] GetScenesName()
		{
			var names = new string[scenes.Count];
			scenes.Keys.CopyTo(names, 0);
			return names;
		}

		public SceneInfo GetScene(string name)
		{
			if (!scenes.ContainsKey(name))
				return null;

			return scenes[name];
		}

		public SceneInfo this[string name] {
			get { return GetScene(name); }
		}

		private void Read()
		{
			var document = XDocument.Load(Filepath);
			scenes = new Dictionary<string, SceneInfo>();

			foreach (var sceneXml in document.Root.Elements()) {
				var scene = ReadScene(sceneXml);
				scenes.Add(scene.Name, scene);
			}
		}

		private SceneInfo ReadScene(XElement sceneXml)
		{
			string workdir = sceneXml.Element("WorkDir").Value;
			Environment.CurrentDirectory = workdir;

			SceneInfo scene = new SceneInfo();
			scene.Name = sceneXml.Element("Name").Value;
			scene.Width  = Convert.ToInt32(sceneXml.Element("Width").Value);
			scene.Height = Convert.ToInt32(sceneXml.Element("Height").Value);
			scene.Controls = new List<SceneElement>();

			foreach (var controlXml in sceneXml.Element("Controls").Elements()) {
				var controlType = Type.GetType("Zerum.Info." + controlXml.Name.LocalName);
				var deserializer = new YAXSerializer(controlType);

				var control = (SceneElement)deserializer.Deserialize(controlXml);
				scene.Controls.Add(control);
			}

			return scene;
		}
	}
}

