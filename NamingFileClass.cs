// NamingFileClass.cs
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
