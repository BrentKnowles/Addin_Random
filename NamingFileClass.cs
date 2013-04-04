using System;
using System.Collections.Generic;
using System.Text;



namespace NamingCentral
{
    /// <summary>
    /// The title of the file is what shows up in the NamingCentral 
    /// dropdown list
    /// 
    /// We then can also pull out an array of regions
    /// and an array of string types
    /// 
    /// Regions = United States
    /// String Types = Male, Female, Lastname
    /// </summary>
    public class NamingFileClass
    {

        /// <summary>
        ///  default constructor
        /// </summary>
        public NamingFileClass()
        {
        }

        private string[] region;
        private string[] types;

        public string[] Regions
        {
            get { return region; }
            set { region = value; }
        }

        /// <summary>
        /// genders
        /// </summary>
        public string[] Types
        {
            get { return types; }
            set { types = value ; }
        }

        private string[] rules;
        /// <summary>
        /// the list of rules for the erarulelanguage.cs
        /// </summary>
        public string[] Rules
        {
            get { return rules; }
            set { rules = value; }
        }

        private string classtouse;
        /// <summary>
        /// an actual class that will be initiated through reflection
        /// </summary>
        public string ClassToUse
        {
            get { return classtouse; }
            set { classtouse = value; }
        }



        private int _MinLettersWords;
        /// <summary>
        /// For data languages, the min letters in a word
        /// </summary>
        public int MinLettersWord
        {
            get { return _MinLettersWords; }
            set { _MinLettersWords = value; }
        }


        private int _MaxLettersWords;
        /// <summary>
        /// For data languages, the min letters in a word
        /// </summary>
        public int MaxLettersWord
        {
            get { return _MaxLettersWords; }
            set { _MaxLettersWords = value; }
        }

        private int _firstnames;
        /// <summary>
        /// For data languages, the min letters in a word
        /// </summary>
        public int FirstNamesPerName
        {
            get { return _firstnames; }
            set { _firstnames = value; }
        }


        private int _lastnames;
        /// <summary>
        /// For data languages, the min letters in a word
        /// </summary>
        public int LastNamesPerName
        {
            get { return _lastnames; }
            set { _lastnames = value; }
        }

    }
}
