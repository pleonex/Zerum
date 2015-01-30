//-----------------------------------------------------------------------
// <copyright file="MainForm.cs" company="none">
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
// <author>pleoNeX</author>
// <email>benito356@gmail.com</email>
// <date>30/01/2015</date>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Zerum.Controls;

namespace Zerum
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			var label = new NftrLabel("font_b11.NFTR");
			label.Text = "Hello World";
			this.Controls.Add(label);
		}
	}
}
