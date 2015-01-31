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
        readonly int lineGap;
		string text;

        public NftrLabel(LabelInfo info)
            : base(info)
        {
			font = new NftrFont(info.Fontpath.FixPath());
			lineGap = font.Blocks.GetByType<Finf>(0).LineGap + 5;
            
            Text = info.DefaultText;
        }
        
        public string Text {
			get { return text; }
			set { text = value.ApplyTable("replace", true); }
        }
        
        protected override void PaintComponent(Graphics graphic)
        {
            int x = 0, y = 0;
            foreach (char ch in Text) {
                if (ch == '\n') {
                    x = 0;
                    y += lineGap;
                    continue;
                }
                
                if (ch == '\r')
                    continue;
                
                var glyph = font.SearchGlyphByChar(ch);
                
                if (x + glyph.Width.Width > Width) {
                    x = 0;
                    y += lineGap;
                }
                
                x += glyph.Width.BearingX;
                graphic.DrawImageUnscaled(glyph.ToImage(1, true), x, y);
				x += glyph.Width.Advance - glyph.Width.BearingX;
            }
        }
    }
}
