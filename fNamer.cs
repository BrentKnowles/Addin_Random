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


