//
//  NftrLabel.cs
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
using System.Drawing;
using Libgame;
using Nftr;
using Nftr.Structure;
using Zerum.Info;

namespace Zerum.Controls
{
    public partial class NftrLabel : SceneControl
    {
        readonly NftrFont font;
        readonly int extraGap;
		string text;

        public NftrLabel(LabelInfo info)
            : base(info)
        {
			font = new NftrFont(info.Fontpath.FixPath());
            extraGap = 2;   // Constant present (at least in Ninokuni game).
            extraGap += 3;  // Furigana font box height (Hard coded for Spanish trans).

            Text = info.DefaultText;
            Text = Text.Replace("{!SP}", " ");
        }
        
        public string Text {
			get { return text; }
			set { text = value.ApplyTable("replace", true); }
        }
        
        protected override void PaintComponent(Graphics graphic)
        {
            font.Painter.DrawString(Text, graphic, 0, 0, Width, extraGap);
        }
    }
}
