//
//  SceneView.cs
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
using System.Windows.Forms;
using System.Drawing;
using Zerum.Info;

namespace Zerum.Controls
{
	public class SceneView : TableLayoutPanel
	{
		SceneInfo scene;

		Panel scenePanel;
		List<SceneControl> controls;
		Dictionary<TextBox, NftrLabel> textControls;

		public SceneView(SceneInfo scene)
		{
			this.scene = scene;
			CreateComponents();
		}

		public static string ControlName {
			get { return "ScenePanel"; }
		}

		void CreateComponents()
		{
			this.SuspendLayout();

			// Create tablelayout
			this.Name = SceneView.ControlName;
			this.Dock = DockStyle.Fill;
			this.RowCount = 1;
			this.ColumnCount = 2;
			this.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

			// Panel for SceneView
			scenePanel = new Panel();
			scenePanel.Size = new Size(scene.Width, scene.Height);
			scenePanel.Paint += OnScenePaint;
			this.Controls.Add(scenePanel, 0, 0);

			// Create scene controls
			controls = new List<SceneControl>();
			textControls = new Dictionary<TextBox, NftrLabel>();
			foreach (var control in scene.Controls)
				controls.Add(AddComponent(control));

			this.ResumeLayout(false);
		}

		SceneControl AddComponent(SceneElement control)
		{
			if (control is LabelInfo) {
				var labelInfo = (LabelInfo)control;
				if (labelInfo.IsEditable)
					return AddTextEntry(labelInfo);
				else
					return new NftrLabel(labelInfo);
			} else if (control is ImageInfo)
			    return new ExternalImage((ImageInfo)control);
			
			return null;
		}

		NftrLabel AddTextEntry(LabelInfo labelInfo)
		{
			// Scene panel must fill all the rows of the first column
			this.RowCount += 2;
			this.RowStyles.Add(new RowStyle());
			this.RowStyles.Add(new RowStyle());
			this.SetRowSpan(scenePanel, this.RowCount);

			this.RowStyles[this.RowCount - 3] = new RowStyle(SizeType.AutoSize);
			var textBoxLabel = new Label();
			textBoxLabel.AutoSize = true;
			textBoxLabel.Text = labelInfo.Name;
			this.Controls.Add(textBoxLabel, 1, this.RowCount - 3);

			this.RowStyles[this.RowCount - 2] = new RowStyle(SizeType.Absolute, 70);
			var textBox = new TextBox();
			textBox.Dock = DockStyle.Fill;
			textBox.ScrollBars = ScrollBars.Vertical;
			textBox.Multiline = true;
			textBox.Text = labelInfo.DefaultText;
			textBox.TextChanged += HandleTextChanged;
			this.Controls.Add(textBox, 1, this.RowCount - 2);

			var label = new NftrLabel(labelInfo);
			textControls.Add(textBox, label);
			return label;
		}
		
        void OnScenePaint(object sender, PaintEventArgs e)
        {
            foreach (var control in controls) {
            	control.Paint(e.Graphics);
            }
        }

		void HandleTextChanged(object sender, EventArgs e)
		{
			var textBox = (TextBox)sender;
			var label = textControls[textBox];
			label.Text = textBox.Text;
			scenePanel.Invalidate();
		}
	}
}

