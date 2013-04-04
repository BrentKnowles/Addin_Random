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
