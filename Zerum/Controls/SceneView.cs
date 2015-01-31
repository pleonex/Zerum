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
using TextTuple = System.Tuple<System.Windows.Forms.TextBox, Zerum.Controls.NftrLabel>;

namespace Zerum.Controls
{
	public class SceneView : GroupBox
	{
		List<TextTuple> textControls;
		List<SceneControl> controls;
		
		public SceneView(SceneInfo scene)
		{
			CreateComponents(scene);
		}

		void CreateComponents(SceneInfo scene)
		{
			Text = scene.Name;
			controls = new List<SceneControl>();
			foreach (var control in scene.Controls)
			    controls.Add(AddComponent(control));
		}

		SceneControl AddComponent(SceneElement control)
		{
			if (control is LabelInfo)
			    return new NftrLabel((LabelInfo)control);
			else if (control is ImageInfo)
			    return new ExternalImage((ImageInfo)control);
			
			return null;
		}
		
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            foreach (var control in controls) {
                if (control != null)
                    control.Paint(e.Graphics);
            }
        }
	}
}

