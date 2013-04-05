// erabase.cs
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
    /// base class for retrieving names. Will be generic, some names will be pulled from a dictionary, others from
    /// a custom list system (to be developed) and others may be generated
    /// </summary>
    public abstract class erabase
    {

        protected string path = ""; // this will be the path to the dictionary folder
        /// <summary>
        /// Returns a name
        /// 
        /// Regions = United States
        /// String Types = Male, Female, Lastname
        /// </summary>
        /// <param name="sRegion">The region to search by</param>
        /// <param name="sType">This is a string because of an enum because data files can create new types</param>
        /// <returns></returns>
        public virtual string GetName(string sRegion, string sType, string sStartsWith)
        {
            return "boo";
        }

        /// <summary>
        /// some routines need to do setup. We pass the path in case they need it
        /// </summary>
        /// <param name="_path"></param>
        public virtual void Setup(string _path, NamingFileClass namingFile)
        {

        }
        public virtual void Setup(string _path)
        {

        }

        /// <summary>
        /// returns the definition of the word analyzed
        /// </summary>
        /// <param name="sWord"></param>
        /// <returns></returns>
        public virtual string GetDefinition(string sWord)
        {
            return "no definition system setup";
        }
    }
}
