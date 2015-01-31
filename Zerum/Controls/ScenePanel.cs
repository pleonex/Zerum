//
//  ScenePanel.cs
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
using TextTuple = System.Tuple<System.Windows.Forms.TextBox, Zerum.Controls.NftrLabel>;
using System.Drawing;
using Zerum.View;

namespace Zerum.Controls
{
	public class ScenePanel : GroupBox
	{
		List<TextTuple> textControls;

		public ScenePanel(Scene scene)
		{
		    AutoSize = true;
			CreateComponents(scene);
		}

		void CreateComponents(Scene scene)
		{
			Text = scene.Name;
			foreach (var control in scene.Controls)
				AddComponent(control);
		}

		void AddComponent(SceneControl control)
		{
			if (control is SceneLabel)
				AddLabel((SceneLabel)control);
			else if (control is SceneImage)
			    AddImage((SceneImage)control);
		}

		void AddLabel(SceneLabel labelInfo)
		{
			var label = new NftrLabel(labelInfo.Fontpath);
			SetDefaultInfo(label, labelInfo);
			label.Text = labelInfo.DefaultText;
			Controls.Add(label);
		}
		
		void AddImage(SceneImage imgInfo)
		{
		    var picBox = new PictureBox();
		    SetDefaultInfo(picBox, imgInfo);
		    picBox.SizeMode = PictureBoxSizeMode.Normal;
		    picBox.Image = Image.FromFile(imgInfo.ImagePath);
		    Controls.Add(picBox);
		}
		
		void SetDefaultInfo(Control control, SceneControl info)
		{
		    control.Width    = info.Width;
		    control.Height   = info.Height;
		    control.Location = new Point(info.LocationX, info.LocationY);
		}
	}
}

