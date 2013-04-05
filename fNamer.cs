// fNamer.cs
//
// Copyright (c) 2013 Brent Knowles (http://www.brentknowles.com)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
// Review documentation at http://www.yourothermind.com for updated implementation notes, license updates
// or other general information/
// 
// Author information available at http://www.brentknowles.com or http://www.amazon.com/Brent-Knowles/e/B0035WW7OW
// Full source code: https://github.com/BrentKnowles/YourOtherMind
//###
using System;
using System.Windows.Forms;
using NamingCentral;
using Layout;
using CoreUtilities;

namespace Addin_Random
{
	public class fNamer : Form
	{

		#region gui
		NamingCentralUserControl Namer= null;
		TextBox Scratch = null;
		#endregion

		public fNamer ()
		{
			Namer = new NamingCentralUserControl();
			Namer.Dock = DockStyle.Fill;
			this.StartPosition = FormStartPosition.CenterParent;
			this.Width = 500;
			this.Height = 625;
			Namer.grabNameChosen+= HandlegrabNameChosen;
			this.Icon = LayoutDetails.Instance.MainFormIcon;
			FormUtils.SizeFormsForAccessibility(this, LayoutDetails.Instance.MainFormFontSize);


			 Scratch = new TextBox();
			Scratch.Multiline = true;
			Scratch.Height = 100;
			Scratch.Dock = DockStyle.Bottom;


			Panel bottomPanel = new Panel();
			bottomPanel.Dock = DockStyle.Bottom;
			bottomPanel.Height = LayoutDetails.ButtonHeight;
			Button ok = new Button();
			ok.Text = Loc.Instance.GetString ("OK");
			ok.Dock = DockStyle.Left;
			ok.DialogResult = DialogResult.OK;

			bottomPanel.Controls.Add (ok);

			this.Controls.Add (Namer);
			this.Controls.Add (Scratch);
			this.Controls.Add (bottomPanel);
		}

		void HandlegrabNameChosen (string sName)
		{
			Scratch.Text = Scratch.Text + Environment.NewLine + sName;
		}
	}
}


