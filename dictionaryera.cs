using System;
using System.Collections.Generic;
using System.Text;
using NetSpell;
using System.Collections;
using System.IO;
using CoreUtilities;
using Layout;
using RichBoxLinks;

namespace NamingCentral
{
    public class dictionaryera: erabase
    {
        private const string NOMATCH = "no matching word found";
        private  NetSpell.SpellChecker.Spelling speller = null;
        private Hashtable randomwordhash = null;
      
       
        /// <summary>
        /// Random generator
        /// </summary>
        Random _Random
        {
            get { return new Random(); }
        }

        /// <summary>
        /// Assumes a multipart name coming through
        /// </summary>
        /// <param name="sWord"></param>
        /// <returns></returns>
        public override string GetDefinition(string sWord)
        {
            string sDefinition = "";


            string sFile = DictionaryDefinitionClass.FileName(sWord, path);

            // only refresh if definition not defined OR we have changed the
            // letter file 

            DictionaryDefinitionClass definitions = (DictionaryDefinitionClass)
                FileUtils.DeSerialize(sFile, typeof(DictionaryDefinitionClass));

            definitions.CurrentFile = sFile;
            DictionaryDefinitionClassIndividual entry = definitions.GetEntry(sWord);



            if (entry == null)
            {
                // remove a trailing s
                if (sWord.EndsWith("s") == true)
                {
                    sWord = sWord.Substring(0, sWord.Length - 1);
                    entry = definitions.GetEntry(sWord);
                }
            }

            if (entry == null)
            {
                // now try lowercase
                entry = definitions.GetEntry(sWord.ToLower());
            }

            if (null != entry)
            {
                sDefinition = entry.Description;
            }

            definitions.Dispose();
            entry = null;
            return sDefinition;

        }
        /// <summary>
        /// August 2012 - subdividing to use this bit elsewhere
        /// </summary>
        /// <param name="sWord"></param>
        /// <returns></returns>
        public static string GetBaseCodeFromWord(string sWord)
        {
            int nIndex_ = sWord.IndexOf('_');
            int _nIndex = sWord.LastIndexOf('_');

            string code = "";

            // now grab number
            if (-1 != nIndex_ && -1 != _nIndex)
            {
                // there are _xxx_ present
                 code = sWord.Substring(nIndex_ + 1, _nIndex - nIndex_ - 1);

               
            }
            return code;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sWord"></param>
        /// <returns></returns>
		public static RichBoxLinks.PartsOfSpeech GetCodeFromWord(string sWord)
        {
            RichBoxLinks.PartsOfSpeech partsofspeech = PartsOfSpeech.Adjective;
            string code = GetBaseCodeFromWord(sWord);
            if ("" != code)
            {
                partsofspeech = (RichBoxLinks.PartsOfSpeech)Int64.Parse(code);
            }
            return partsofspeech;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nTypes"></param>
        /// <param name="StartsWith">* is any, otherwise tries and matches the right letter</param>
        /// <returns></returns>
        private string GetRandomWordFromDictionary(RichBoxLinks.PartsOfSpeech nTypes, string StartsWith)
        {
            int RandomNum = 0;// new Random().Next(random.Count - 1);
            // build a list of all male names
            ArrayList list = new ArrayList();

            string sType = "_" + ((int)nTypes).ToString() + "_";
            foreach (DictionaryEntry word in randomwordhash)
            {

                //(int i = 0; i < random.Count; i++)

                //object word = (object) random[i];
                NetSpell.SpellChecker.Dictionary.Word asWord =
                    (NetSpell.SpellChecker.Dictionary.Word)word.Value;



                // here's where we have to do work; we need to actually extra the 'key' (see code in fmain.cs, make a routein) and then do a proper bit test

                PartsOfSpeech speech = GetCodeFromWord(asWord.AffixKeys);

                //if (asWord.AffixKeys.IndexOf(sType) > -1)
                if ( (speech & nTypes) == nTypes)
                {
                    if ("*" == StartsWith || asWord.Text[0] == StartsWith[0])
                    {
                        // 1 in the dictionary Affix means 'male'
                        list.Add(asWord.Text);
                    }
                }
            }

            if (list == null )
            {
                throw new Exception(" name did not generate from dictionary correctly.");
            }
            if (list.Count == 0)
            {
                list.Add(NOMATCH);
            }

            RandomNum = _Random.Next(list.Count);
            string sValue = list[RandomNum].ToString();


            list = null;
            return sValue;
        }

        public override string GetName(string sRegion, string sType, string sStartsWith)
        {
            // region has no effect on this type of list
            return RandomWord(sType, sStartsWith, sRegion);
        }


        /// <summary>
        /// creats a valid spellcheck object
        /// </summary>
        private  NetSpell.SpellChecker.Spelling SpellCheckObject(string sDicName, string sFolder, string sUserFile)
        {

            if (sDicName == null)
            {
                throw new Exception("doSpellCheck - dicname was null.");
            }
            if (sFolder == null)
            {
                throw new Exception("doSpellCheck - sFolder was null.");
            }
            if (sUserFile == null)
            {
                throw new Exception("doSpellCheck - userfile was null.");
            }
            if (File.Exists(sFolder + "\\" + sDicName) == false)
            {
                throw new Exception(String.Format("doSpellCheck - Dictionoary {0} does not exist", sFolder + "\\" + sDicName
                    ));
            }
            NetSpell.SpellChecker.Spelling SpellChecker2;
            SpellChecker2 = new NetSpell.SpellChecker.Spelling();

            SpellChecker2.ShowDialog = true;
            NetSpell.SpellChecker.Dictionary.WordDictionary dictionary = new NetSpell.SpellChecker.Dictionary.WordDictionary();
            dictionary.DictionaryFile = sDicName;
            dictionary.DictionaryFolder = sFolder;
            dictionary.UserFile = sUserFile;
            SpellChecker2.Dictionary = dictionary;

            return SpellChecker2;

        }

        /// <summary>
        /// Returns a random word
        /// 
        /// Replacing the old Keeper functionality byusing the new Netspell dictionary system
        /// </summary>
        /// <param name="sType"></param>
        /// <returns></returns>
        public string RandomWord(string sType, string sStartsWith, string sRegion)
        {
            sType = sType.ToLower();

            // Only initialize the dictionary once

            if (speller == null)
            {
                throw new Exception("Speller not setup");
            }

            PartsOfSpeech region = PartsOfSpeech.Proper; // default to ALL REGIONS

            if ("*" != sRegion)
            {
                region = (PartsOfSpeech)Enum.Parse(typeof(PartsOfSpeech), sRegion, true);
            }
       


            string sValue = "";
            string sLastName = GetRandomWordFromDictionary(region |PartsOfSpeech.Noun | PartsOfSpeech.Proper | PartsOfSpeech.Lastname, "*");

            if (NOMATCH == sLastName)
            {
                sLastName = "";
            }


            if (sType == "female")
            {
                // table = Program.AppMainForm.data.femaleWords;
                sValue = GetRandomWordFromDictionary(region | PartsOfSpeech.Female | PartsOfSpeech.Noun | PartsOfSpeech.Proper, sStartsWith) + " " +
                    sLastName;
            }
            else if (sType == "male")
            {
                sValue = GetRandomWordFromDictionary(region | PartsOfSpeech.Male | PartsOfSpeech.Noun | PartsOfSpeech.Proper, sStartsWith) + " " +
                    sLastName; ;

                // table = Program.AppMainForm.data.maleWords;

            }
            else
            if (sType == "lastname")
            {
                // table = Program.AppMainForm.data.lastWords;
                sValue = GetRandomWordFromDictionary(region |PartsOfSpeech.Noun | PartsOfSpeech.Proper | PartsOfSpeech.Lastname, sStartsWith);
            }


        sValue = sValue.Trim();
            return sValue;

        }
        /// <summary>
        /// some routines need to do setup. We pass the path in case they need it
        /// </summary>
        /// <param name="_path"></param>
        public override void Setup(string _path, NamingFileClass namingFile)
        {
            path = _path;
            speller = SpellCheckObject("en-US.dic", _path, Path.Combine(_path, "UserDic.dic"));
            try
            {
                speller.Dictionary.Initialize();
            }
            catch (Exception ex)
            {
                NewMessage.Show(ex.ToString());
            }
            try
            {
                randomwordhash = speller.Dictionary.BaseWords;
            }
            catch(Exception ex)
            {
                NewMessage.Show(ex.ToString());
            }



            // namingFile is ignored here used in erarulelanguage
        }
    }
}
