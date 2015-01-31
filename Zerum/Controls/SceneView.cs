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
	public class SceneView : GroupBox
	{
		Dictionary<TextBox, NftrLabel> textControls;
		List<SceneControl> controls;
		GroupBox textEntryBox;
		SceneInfo scene;
		
		public SceneView(SceneInfo scene)
		{
			this.scene = scene;
			CreateComponents();
		}

		void CreateComponents()
		{
			textEntryBox = new GroupBox();
			textEntryBox.Text = "Text";
			textEntryBox.Width  = 100;
			textEntryBox.Height = scene.Height;
			textEntryBox.Location = new Point(scene.Width, 0);
			Controls.Add(textEntryBox);

			Text   = scene.Name;
			Width  = scene.Width + textEntryBox.Width;
			Height = scene.Height;

			controls = new List<SceneControl>();
			textControls = new Dictionary<TextBox, NftrLabel>();
			foreach (var control in scene.Controls)
			    controls.Add(AddComponent(control));
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
			var label = new NftrLabel(labelInfo);

			int yBase = textControls.Count * 40;

			var textBoxLabel = new Label();
			textBoxLabel.Text = labelInfo.Name;
			textBoxLabel.Location = new Point(0, yBase);

			var textBox = new TextBox();
			textBox.Text = label.Text;
			textBox.Multiline = true;
			textBox.Width  = 80;
			textBox.Height = 30;
			textBox.Location = new Point(0, yBase + 10);
			textBox.TextChanged += HandleTextChanged;

			textEntryBox.Controls.Add(textBoxLabel);
			textEntryBox.Controls.Add(textBox);
			textControls.Add(textBox, label);
			return label;
		}
		
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            foreach (var control in controls) {
                if (control != null)
                    control.Paint(e.Graphics);
            }
        }

		void HandleTextChanged(object sender, EventArgs e)
		{
			var textBox = (TextBox)sender;
			var label = textControls[textBox];
			label.Text = textBox.Text;
			Invalidate();
		}
	}
}

