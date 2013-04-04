using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CoreUtilities;
using System.IO;
using System.Reflection;
using Layout;
namespace NamingCentral
{
    /// <summary>
    /// This is replacing the Naming Central Form so that I can use this as part of StoryFlesh
    /// </summary>
    public partial class NamingCentralUserControl : UserControl
    {

        private string _path;
        private string _eraclasstouse = "";
        erabase era = null;

        public string sResult = "";

        public NamingCentralUserControl()
        {
            InitializeComponent();
			SetupPath();
        }
        /// <summary>
        /// needs to be called before the user control can be used
        /// 
        /// Path should be to the dictionary folder
        /// </summary>
        /// <param name="path"></param>
        public void SetupPath()
        {
			_path = Path.Combine (LayoutDetails.Instance.Path, "dictionary");
            if (_path == null || Directory.Exists(_path) == false)
            {
                throw new Exception(_path + " must exist");
            }


            // we need to initialize a path for UserInfo (in case it hasn't been done already)
            // to do this we'll need to revesre engineer _path 
            // FOR TESTING I just used _path as is
            string pathtosettingsfolder = ""; // this is always assumed to be at same route level of the 
            //dictionary path passed in and is called
            //settings

            pathtosettingsfolder = Path.Combine(new DirectoryInfo(_path).Parent.FullName, "settings");
         //   UserInfoBase.path = pathtosettingsfolder;

            /*
            // tem
            NamingFileClass filetemp = new NamingFileClass();
            filetemp.Regions = new string[2]{"United States", "Canada"};
            filetemp.Types = new string[3] {"Male", "Female", "Lastname" };

            string sFile = Path.Combine(_path, "dictonary.era");
            General.Serialize(filetemp, sFile);
*/

            UpdateListOfFiles();
            comboBoxStartWordWith.SelectedIndex = 0;
        }


        /// <summary>
        /// fills the combo box with a list of files
        /// </summary>
        private void UpdateListOfFiles()
        {

            string[] files = Directory.GetFiles(_path, "*.era");
            for (int i = 0; i < files.Length; i++)
            {
                files[i] = new FileInfo(files[i]).Name.Replace(".era", "").Trim();
            }
            comboBoxListOfFiles.Items.Clear();
            comboBoxListOfFiles.Items.AddRange(files);
            if (comboBoxListOfFiles.Items.Count > 0)
            {
                comboBoxListOfFiles.SelectedIndex = 0;
            }
        }

        private void comboBoxListOfFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            // once a file has been selected, we populate the other boxes
            string sFile = (comboBoxListOfFiles.Items[comboBoxListOfFiles.SelectedIndex]).ToString();
            sFile = Path.Combine(_path, sFile + ".era");
            if (File.Exists(sFile))
            {
                NamingFileClass naming = (NamingFileClass)FileUtils.DeSerialize(sFile, typeof(NamingFileClass));
                if (naming != null)
                {
                    comboBoxRegions.Items.Clear();
                    comboBoxRegions.Items.AddRange(naming.Regions);
                    comboBoxRegions.SelectedIndex = 0;

                    comboBoxTypes.Items.Clear();
                    comboBoxTypes.Items.AddRange(naming.Types);
                    comboBoxTypes.SelectedIndex = 0;

                    comboBoxStartWordWith.SelectedIndex = 0;

                    _eraclasstouse = naming.ClassToUse;
                    // based on the class we are using, we 
                    // instantiate

                    string typename = String.Format("NamingCentral.{0}", _eraclasstouse);
                    Type type = Type.GetType(typename);
                    era = (erabase)Activator.CreateInstance(type);
                    era.Setup(_path, naming);
                }
            }
        }

        /// <summary>
        ///  generate a list fifty names
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            listOfNames.Items.Clear();

            for (int i = 1; i <= 10; i++)
            {
                string sName = GenerateName(comboBoxStartWordWith.SelectedItem.ToString());
                if ("" != sName)
                {
                    listOfNames.Items.Add(sName);
                }
            }
        }

        /// <summary>
        /// generates a name using the current setting selected in the interface
        /// </summary>
        /// <returns></returns>
        private string GenerateName(string sStartsWith)
        {
            if (era == null)
            {
                // throw new Exception("Era was never set to a proper object");
                NewMessage.Show(Loc.Instance.GetString ("Please select an appropriate Name Era first"));
                return "";
            }

            if (comboBoxRegions.SelectedItem == null || comboBoxTypes.SelectedItem == null)
            {
                NewMessage.Show(Loc.Instance.GetString ("Please select a valid region and type"));
                return "";
            }

            string sRegion = comboBoxRegions.SelectedItem.ToString();
            string sType = comboBoxTypes.SelectedItem.ToString();
            if (sRegion == "" || sType == "")
            {
                NewMessage.Show(Loc.Instance.GetString ("Please select a valid region and type"));
                return "";
            }


            return era.GetName(sRegion, sType, sStartsWith);

        }

        /// <summary>
        /// show name and lookup description
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listOfNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBoxCurrentName.Text = (listOfNames.SelectedItem.ToString());

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sResult = groupBoxCurrentName.Text;
        }

        /// <summary>
        /// goes to a babynames page for definition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkLabelLink.Tag != null)
            {
                string sName = linkLabelLink.Tag.ToString();
                //string sUrl = String.Format(@"http://babynamesworld.parentsconnect.com/meaning_of_{0}.html", sName);
				//Todo: finish properly
                string sUrl = String.Format("A website!", sName);
                General.OpenDocument(sUrl, "");
            }
        }
        /// <summary>
        /// copies name to scratch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            OnGrabNameChosen(groupBoxCurrentName.Text);
        }

        /////////////////////////
        // Delegates
        //////////////////////////////

        public delegate void DisplayDefinition(string sDefinition);
        public event DisplayDefinition displayDefinition;

        /// <summary>
                /// </summary>
        /// <returns></returns>
        public void OnDisplayDefinition(string sDefinition)
        {
            
            if (displayDefinition != null)
            {
                displayDefinition(sDefinition);
            }
            

        }


        public delegate void GrabNameChosen(string sName);
        public event GrabNameChosen grabNameChosen;

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public void OnGrabNameChosen(string sName)
        {

            if (grabNameChosen != null)
            {
                grabNameChosen(sName);
            }
            

        }

        /// <summary>
        /// moved to a separate button because it is slow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDefinition_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;


            // need to pick out just the first name
            string[] names = groupBoxCurrentName.Text.Split(new char[1] { ' ' });
            string sFirstName = names[0];

            string sLastName = "";

            if (names.Length > 1)
            {
                sLastName = names[1];
            }
            sFirstName = sFirstName.Trim();
            linkLabelLink.Tag = sFirstName;

            if ("no" != sFirstName.ToLower())
            {
                string sText = era.GetDefinition(sFirstName);
                if ("" != sLastName)
                {
                    sLastName = sLastName.Trim();
                    string sdef = era.GetDefinition(sLastName);
                    if ("" != sdef && " " != sdef && "none" != sdef)
                    {
                        sText = String.Format("{0}\r\n({1}) {2}", sText, sLastName, sdef);
                    }
                }
                OnDisplayDefinition(sText);
            }
            else
            {
                OnDisplayDefinition("");
            }
            this.Cursor = Cursors.Default;
        }
     
    }
}
