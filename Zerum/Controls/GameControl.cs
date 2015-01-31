//-----------------------------------------------------------------------
// <copyright file="${FILENAME}.cs" company="none">
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
// <date>31/01/2015</date>
//-----------------------------------------------------------------------
using System;
using System.Drawing;
using Zerum.View;

namespace Zerum.Controls
{
    /// <summary>
    /// Description of GameControl.
    /// </summary>
    public abstract class GameControl
    {
        protected GameControl(SceneControl info)
        {
            LocationX = info.LocationX;
            LocationY = info.LocationY;
            Width = info.Width;
            Height = info.Height;
        }
        
        public int LocationX {
            get;
            set;
        }
        
        public int LocationY {
            get;
            set;
        }
        
        public int Width {
            get;
            set;
        }
        
        public int Height {
            get;
            set;
        }
        
        public void Paint(Graphics graphics)
        {
            var container = graphics.BeginContainer();
            graphics.SetClip(new Rectangle(LocationX, LocationY, Width, Height));
            graphics.TranslateTransform(LocationX, LocationY);
            
            PaintComponent(graphics);
            
            graphics.EndContainer(container);
        }
        
        protected abstract void PaintComponent(Graphics graphic);
    }
}
