//-----------------------------------------------------------------------
// <copyright file="NftrLabel.cs" company="none">
// Copyright (C) 2015
//
//   This program is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by 
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//
//   This program is distributed in the hope that it will be useful, 
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details. 
//
//   You should have received a copy of the GNU General Public License
//   along with this program.  If not, see "http://www.gnu.org/licenses/". 
// </copyright>
// <author>pleonex</author>
// <email>benito356@gmail.com</email>
// <date>30/01/2015</date>
//-----------------------------------------------------------------------
using System;
using System.Drawing;
using Nftr;
using Nftr.Structure;
using Zerum.View;

namespace Zerum.Controls
{
    /// <summary>
    /// Description of NftrLabel.
    /// </summary>
    public partial class NftrLabel : GameControl
    {
        readonly NftrFont font;
        readonly int lineGap;
        
        public NftrLabel(SceneLabel info)
            : base(info)
        {            
            font = new NftrFont(info.Fontpath);
			lineGap = font.Blocks.GetByType<Finf>(0).LineGap;
            
            Text = info.DefaultText;
        }
        
        public string Text {
            get;
            set;
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
                x += glyph.Width.Advance;
            }
        }
    }
}
