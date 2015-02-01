//
//  MainForm.cs
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
using System.Drawing;
using System.Windows.Forms;
using Zerum.Controls;
using Zerum.Info;

namespace Zerum
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		ScenesManager manager;
		SceneView sceneView;

		Label sceneListLabel;
		ListBox sceneList;

		public MainForm()
		{
			this.AutoScaleMode = AutoScaleMode.Font;
			this.AutoSize = true;
			this.Text = "Zerum ~~ by pleonex ~~";

			sceneListLabel = new Label();
			sceneListLabel.AutoSize = true;
			sceneListLabel.Text = "Scenes:";
			Controls.Add(sceneListLabel);

			sceneList = new ListBox();
			sceneList.Width = 200;
			manager = ScenesManager.Instance;
			foreach (var name in manager.GetScenesName())
				sceneList.Items.Add(name);

			Controls.Add(sceneList);
			sceneList.SelectedIndexChanged += HandleSelectedIndexChanged;
			sceneList.SelectedIndex = 0;
		}

		void HandleSelectedIndexChanged(object sender, EventArgs e)
		{
			if (sceneView != null) {
				Controls.Remove(sceneView);
				sceneView.Dispose();
			}

			UpdateSceneView();
		}

		void UpdateSceneView()
		{
			sceneView = new SceneView(manager.LoadScene((string)sceneList.SelectedItem));
			sceneView.Location = new Point(5, 0);
			Controls.Add(sceneView);

			UpdateExternalControlsLocation();
		}

		void UpdateExternalControlsLocation()
		{
			int xBase = sceneView.Location.X + sceneView.Width + 10;
			sceneListLabel.Location = new Point(xBase, 10);

			sceneList.Location = new Point(xBase, 25);
			sceneList.Height = sceneView.Height - 20;
		}
	}
}
