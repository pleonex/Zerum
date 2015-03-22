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

		TableLayoutPanel panel;
		ListBox sceneList;

		public MainForm()
		{
			this.AutoScaleMode = AutoScaleMode.Font;
			this.AutoSize = true;
			this.Size = new Size(756, 425);
			this.Text = "Zerum ~~ by pleonex ~~";
			CreateComponents();

			manager = ScenesManager.Instance;
			foreach (var name in manager.GetScenesName())
				sceneList.Items.Add(name);

			sceneList.SelectedIndexChanged += HandleSelectedIndexChanged;
			sceneList.SelectedIndex = 0;
		}

		void CreateComponents()
		{
			this.SuspendLayout();

			panel = new TableLayoutPanel();
			panel.Dock = DockStyle.Fill;
			panel.RowCount = 1;
			panel.ColumnCount = 2;
			panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
			panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200));
			this.Controls.Add(panel);

			// Second cell: Scene list
			TableLayoutPanel listPanel = new TableLayoutPanel();
			listPanel.Dock = DockStyle.Fill;
			listPanel.ColumnCount = 1;
			listPanel.RowCount = 2;
			listPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
			listPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
			panel.Controls.Add(listPanel, 1, 0);

			Label sceneListLabel = new Label();
			sceneListLabel.AutoSize = true;
			sceneListLabel.Text = "Scenes:";
			listPanel.Controls.Add(sceneListLabel, 0, 0);

			sceneList = new ListBox();
			sceneList.Dock = DockStyle.Fill;
			sceneList.IntegralHeight = false;
			listPanel.Controls.Add(sceneList, 0, 1);

			this.ResumeLayout(false);
		}

		void HandleSelectedIndexChanged(object sender, EventArgs e)
		{
			if (panel.Controls.ContainsKey(SceneView.ControlName)) {
				panel.Controls[SceneView.ControlName].Dispose();
				panel.Controls.RemoveByKey(SceneView.ControlName);
			}

			string sceneName = (string)sceneList.SelectedItem;
			SceneView sceneView = new SceneView(manager.LoadScene(sceneName));
			panel.Controls.Add(sceneView, 0, 0);
		}
	}
}
