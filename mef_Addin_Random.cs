// mef_Addin_Random.cs
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
using MefAddIns;

using MefAddIns.Extensibility;
using System.ComponentModel.Composition;

using System.Windows.Forms;
using CoreUtilities;
using System.IO;
using System.Collections.Generic;

using System.Data;
using System.Collections;
using System.Drawing;
using Layout;
namespace Addin_Random
{
	[Export(typeof(mef_IBase))]
	public class mef_Addin_Random : PlugInBase, mef_IBase
	{
		public mef_Addin_Random ()
		{
			guid = "RandomNames";
		}


		
		public string Author
		{
			get { return @"Brent Knowles"; }
		}
		public string Version
		{
			get { return @"1.0.0.0"; }
		}
		public string Description
		{
			get { return "Utility to generate random names."; }
		}
		public string Name
		{
			get { return @"RandomNamer"; }
		}
		public override bool DeregisterType ()
		{
			
		//do not actually remove the addin type since we force shutdown
			
			//NewMessage.Show ("need to remove from list");
			return true;
		}
		public override void AssignHotkeys (ref List<HotKeys.KeyData> Hotkeys, ref mef_IBase addin, Action<mef_IBase> Runner)
		{
			
			base.AssignHotkeys (ref Hotkeys, ref addin, Runner);
		}
		public void HotkeyAction(bool b)
		{
//			if (myRunnForHotKeys != null && myAddInOnMainFormForHotKeys != null)
//				myRunnForHotKeys(myAddInOnMainFormForHotKeys);
			
		}
		public override void RegisterType()
		{
		}
		fNamer NamingForm ;
		public void RespondToMenuOrHotkey<T>(T form) where T: System.Windows.Forms.Form, MEF_Interfaces.iAccess 
		{
			SetupFiles();
			NamingForm = new fNamer();
		//	NamingForm.FormClosing+= HandleFormClosing;

			NamingForm.ShowDialog();

		}
		public void ActionWithParamForNoteTextActions (object param)
		{
		}
		public override object ActiveForm ()
		{
			return NamingForm;
		}

//		void HandleFormClosing (object sender, FormClosingEventArgs e)
//		{
//
//			NewMessage.Show ("Closing");
//			// assign this to the close event of any form we may create
//			RemoveQuickLinks();
//		}
		public override string dependencyguid {
			get {

				return "NetSpell";
			}
		}
		public void SetupFiles ()
		{
			string sDirectory = Path.Combine (LayoutDetails.Instance.Path, "dictionary");
			if (Directory.Exists (sDirectory) == false) {
				Directory.CreateDirectory (sDirectory);
			}
			string defaultfile = "dictionary.era";
			defaultfile = Path.Combine (sDirectory, defaultfile);
			if (File.Exists (defaultfile) == false) {
				System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly ();
				if (null != _assembly)
				{
					FileUtils.PreparePullResource (_assembly, "dictionary.era", defaultfile);
				}
			}
		}
		public PlugInAction CalledFrom { 
			get
			{
				PlugInAction action = new PlugInAction();
				//	action.HotkeyNumber = -1;
				action.MyMenuName = Loc.Instance.GetString ("Random Names");
				
				action.ParentMenuName = "ToolsMenu";
				action.IsOnContextStrip = false;
				action.IsOnAMenu = true;
				
				
				action.IsANote = false;
				action.IsNoteAction = false;
				action.QuickLinkShows = false;
				//action.NoteActionMenuOverride = Loc.Instance.GetString ("Proofread by Color");
				action.ToolTip =Loc.Instance.GetString("Random naming tools.");
				//action.Image = FileUtils.GetImage_ForDLL("camera_add.png");
				action.GUID = GUID;
				return action;
			} 
		}
	}
}

