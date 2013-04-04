using System;
using System.Collections.Generic;
using System.Text;
using RulesMess;

namespace NamingCentral
{
    /// <summary>
    /// This class will process rules
    /// </summary>
    class erarulelanguage : erabase
    {
        Random newRandom;
        NamingFileClass _namingFile;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sWord"></param>
        /// <returns></returns>
        public override string GetDefinition(string sWord)
        {
            string sDefinition = "none";


            return sDefinition;

        }

        /// <summary>
        /// wrapper for actual word generation
        /// </summary>
        /// <returns></returns>
        private string CreateAWord(string sRegion, string sType, string sStartsWith)
        {
            string sWords = "";

            KnowledgeBase kb = new KnowledgeBase();
            // ON every creation of a word we will REBUILD Knowledgebase because we create temporary rules(the gender parsing)
         //   kb.Add(new Relationship("1", "is", "A", "", 50, 0, false));
         //   kb.Add(new Relationship("1", "is", "B", "", 50, 0, false));
         //   kb.Add(new Relationship("1", "is", "C", "", 50, 0, false));
         //   kb.Add(new Relationship("1", "is", "Z", "", 10, 0, false));
      /*      kb.Add(new Relationship("*", "is", "e", "", 70, 0, false));
            kb.Add(new Relationship("*", "is", "u", "", 70, 0, false));
            kb.Add(new Relationship("*", "is", "-", "", 70, 0, false));
            kb.Add(new Relationship("*", "is", "c", "", 70, 0, false));
            kb.Add(new Relationship("*", "is", "v", "", 99, 0, false));
            kb.Add(new Relationship("*", "is", "s", "", 15, 0, false));
            kb.Add(new Relationship("*", "is", "d", "", 15, 0, false));
            kb.Add(new Relationship("*", "is", "f", "", 15, 0, false));


            kb.Add(new Relationship("2", "is", "y", "", 70, 0, false));
            kb.Add(new Relationship("2", "is", "rrrrrrrrr", "", 45, 0, false));
            kb.Add(new Relationship("2", "is", "e", "", 60, 0, false));

            // set up vowels
            kb.Add(new Relationship("vowel", "is", "a", "", 90, 0, false));
            kb.Add(new Relationship("vowel", "is", "e", "", 90, 0, false));
            kb.Add(new Relationship("vowel", "is", "i", "", 90, 0, false));
            kb.Add(new Relationship("vowel", "is", "o", "", 90, 0, false));
            kb.Add(new Relationship("vowel", "is", "u", "", 90, 0, false));

            // followers
            kb.Add(new Relationship("A", "is", "bba", "", 90, 0, false));
            kb.Add(new Relationship("B", "is", "e", "", 90, 0, false));
            kb.Add(new Relationship("B", "is", "i", "", 90, 0, false));
            kb.Add(new Relationship("B", "is", "u", "", 30, 0, false));

            // more complicated rules
            kb.Add(new Relationship("A", "is", "@*", "", 20, 0, false)); // this means we will pick any letter to follow B. Useful when we want B to often have other letters to follow it but ALSO to be able to have any letter
                                                                        // uses the GROUPING system @* means to use from group *
            kb.Add(new Relationship("3", "is", "@vowel", "", 20, 0, false)); // 3rd position ALWAYS a vowel

            */
            //gender/place -- can't use groups
            // special gender/region rules are comma seperated, no spaces
         //   kb.Add(new Relationship(, "is", "tt", "", 90, 0, false)); // double t's occur frequently in male names; use higher #s to drown out 'generic' rules
           // kb.Add(new Relationship("", "is", "f", "", 90, 0, false));
           // kb.Add(new Relationship("gender:female:*", "is", "g", "", 90, 0, false));
           // kb.Add(new Relationship("gender:female:1", "is", "P", "", 90, 0, false));  // all female names start with P
           // kb.Add(new Relationship("gender:female:v", "is", "oom", "", 90, 0, false)); // anytmie a female name has a v we add oom
           // kb.Add(new Relationship("gender:lastname:1", "is", "Vlad", "", 90, 0, false)); // last names have vlad

         /*   string[] rules = new string[8];
            rules[0] = "gender=maleregion=*:*:tt:190";
            rules[1] = "gender=femaleregion=*:*:f:190";
            rules[2] = "gender=femaleregion=*:1:P:190";
            rules[3] = "gender=femaleregion=*:v:oom:190";
            rules[4] = "gender=lastnameregion=*:1:Vlad:1900";

            rules[5] = "gender=lastnameregion=upper:1:Boris:225900";
            rules[6] = "gender=maleregion=upper:c:hicken:221900";
            rules[7] = "gender=femaleregion=upper:C:hicken:221900";
            */


            // the other way of doing this is to BUILD custom rules so that gender:male:* just adds a new rule

           // PROCESS RULES
            // special rules like gender and region need to be transformed based on the user's choice of gender and region
            // into standard rules

            int RULE = 1;
            int RULE_RESULT = 2;
            int RULE_WEIGHT = 3;
            bool bAddRule = false;

            foreach (string rule in _namingFile.Rules)
            {
                bAddRule = true;

                // this is a special gender rule
                if (rule.IndexOf("gender") > -1)
                {
                    bAddRule = false; // once we get to a gender test we have to do a TEST
                    string sRuleTest = "gender=" + sType;
                    sRuleTest = sRuleTest + "region=" + sRegion; // now add region

                    /// we always test ANY region too, so that core gender rules sitll apply to region specific
                    if (rule.IndexOf(sRuleTest) > -1 || rule.IndexOf("gender=" + sType + "region=*") > -1)
                    {
                        bAddRule = true;
                       
                    }
                }



                if (true == bAddRule)
                {
                    string[] ruleparts = rule.Split(new char[1] { ':' });

                    // we found a rule matching the chosen sType, now add it
                    kb.Add(new Relationship(ruleparts[RULE], "is", ruleparts[RULE_RESULT], "", Double.Parse(ruleparts[RULE_WEIGHT]), 0, false));
                }
            }

            // pass 2 we make more complicated with REGION

           // kb.Add(new Relationship("gender:male", "is", "mmm", "", 90, 0, false));

            // we build a string based on what is their, gender:male,region:Upper
            // we then just do loops; get BASIC GENDER working first then try regions


           // kb.Add(new Relationship("(region:Upper)_1", "is", "Kab-", "", 90, 0, false)); // all Upper regions start with Kab-
           // kb.Add(new Relationship("(region:Upper)_*", "is", "prime", "", 90, 0, false)); // often 'prime'
           // kb.Add(new Relationship("(region:Upper)_v", "is", "rock", "", 90, 0, false)); // often rock after v with Upper

           // kb.Add(new Relationship("(region:Lower)_2", "is", "mmm", "", 90, 0, false));
           // kb.Add(new Relationship("(*,Lower,2)", "is", "mmm", "", 90, 0, false));
          

            // uses the GROUPING system @* means to use from group *

            // kb.Add(new Relationship("C", "is", "vowel", "", 90, 0, false));

            /*Limitations; without deduction I can't do grouping but I do have a tremendous amount of power over specifics */

            // kb.Deduction(); //No deductions, we don't want them, they'll do confusing things

            int nStart = 1;
            if ("*" != sStartsWith)
            {
                nStart = 2;
                sWords = sStartsWith; // start with a specific letter
            }

            for (int i = nStart; i <= _namingFile.MinLettersWord + newRandom.Next(_namingFile.MaxLettersWord - _namingFile.MinLettersWord); i++)
            {



                string sTest = "1";


                if (i > 1)
                {
                    // always look for follower characters
                    sTest = sWords[i - 2].ToString();
                }

               
                

                // grab position (i.e., 1) or a specific letter (i.e., what follows A)
                Relationship bestRelationship = kb.GetRandomWeightedFact(sTest, newRandom);
                if (null == bestRelationship)
                {
                    sTest = i.ToString();
                    bestRelationship = kb.GetRandomWeightedFact(sTest, newRandom);
                }
                
                if (null == bestRelationship)
                {
                    sTest = "*";
                    // we did not find a letter for this position, grab any letter
                    bestRelationship = kb.GetRandomWeightedFact(sTest,newRandom);
                }


                // Add the word

                if (null != bestRelationship)
                {
                    string sAddWord = bestRelationship.B;
                    //GROUP SYSTEM
                    if (sAddWord.IndexOf("@") > -1 && sAddWord.Length > 1)
                    {
                        // we have a group, go pick from that group please
                        string sgroupname = sAddWord.Replace("@", " ").Trim();
                        bestRelationship = kb.GetRandomWeightedFact(sgroupname, newRandom);
                        if (null != bestRelationship)
                        {
                            sAddWord = bestRelationship.B;
                        }

                    }
                    sWords = sWords + sAddWord;
                }
            }
            return sWords;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sRegion"></param>
        /// <param name="sType"></param>
        /// <param name="sStartsWith"></param>
        /// <returns></returns>
        public override string GetName(string sRegion, string sType, string sStartsWith)
        {
            string sWords = "";
            sType = sType.ToLower();
            sRegion = sRegion.ToLower();
            

            /*for (int i = 1 ; i <= _namingFile.MinLettersWord + newRandom.Next(_namingFile.MaxLettersWord - _namingFile.MinLettersWord); i++)
            {
                sWords = sWords +      ((char) ((short)'a' + newRandom.Next(26)));
            }*/


           
           


                if (sType == "lastname")
                {
                    sWords = CreateAWord(sRegion, sType, sStartsWith);
                }
                else
                {
                    for (int i = 1; i <= _namingFile.FirstNamesPerName; i++)
                    {
                        sWords = sWords + CreateAWord(sRegion, sType, sStartsWith) + " ";
                        sStartsWith = "*"; // second and subsquent words don't have to start with the same letter
                    }
                    for (int i = 1; i <= _namingFile.LastNamesPerName; i++)
                    {
                        sWords = sWords + CreateAWord(sRegion, "lastname", "*") + " ";
                    }
                }

            return sWords.Trim();
        }


        /// <summary>
        /// some routines need to do setup. We pass the path in case they need it
        /// </summary>
        /// <param name="_path"></param>
        public override void Setup(string _path, NamingFileClass namingFile)
        {
            path = _path;
            _namingFile = namingFile;

            newRandom = new Random(DateTime.Now.Millisecond);
          
        }
    }
}
