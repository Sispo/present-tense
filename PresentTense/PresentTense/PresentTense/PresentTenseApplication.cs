using System;
using System.Collections.Generic;
using System.Linq;
using PushdownAutomaton;
using Extensions;

namespace PresentTense
{
    public class PresentTenseApplication
    {

        public Dictionary<PresentTenseWordType, IEnumerable<string>> words { get; private set; }
        public PDA pda { get; private set; }
        private PTAStackAlphabet stackAlphabet = new PTAStackAlphabet();
        public PresentTenseApplication()
        {
            InitWords();
            InitPDA();
        }

        private void InitWords()
        {
            words = new Dictionary<PresentTenseWordType, IEnumerable<string>>();

            //Words
            InitWordType("And", stackAlphabet.AND, "and");
            InitWordType("The", stackAlphabet.THE, "the");
            InitWordType("Enough", stackAlphabet.ENOUGH, "enough");
            InitWordType("Very", stackAlphabet.VERY, "very");
            InitWordType("Every", stackAlphabet.EVERY, "every");
            InitWordType("Too", stackAlphabet.TOO, "too");
            InitWordType("I", stackAlphabet.I, "I");
            InitWordType("You", stackAlphabet.YOU, "you");
            InitWordType("We", stackAlphabet.WE, "we");
            InitWordType("They", stackAlphabet.THEY, "they");
            InitWordType("He", stackAlphabet.HE, "he");
            InitWordType("She", stackAlphabet.SHE, "she");
            InitWordType("It", stackAlphabet.IT, "it");

            //Verbs
            InitWordType("Base Verbs", stackAlphabet.VP);
            InitWordType("Base Verbs -s", stackAlphabet.VS);

            //Adjectives
            InitWordType("Adjectives", stackAlphabet.ADJCTV);

            //Adverbs
            InitWordType("Adverbs of manner", stackAlphabet.ADVOFM);
            InitWordType("Adverbs of frequency", stackAlphabet.ADVOFF);
            InitWordType("Adverbs of a degree", stackAlphabet.ADVOFD);
            InitWordType("Adverbs of place", stackAlphabet.ADVOFP);
            InitWordType("Adverbs of place+", stackAlphabet.ADVOFPP);

            //Nouns
            InitWordType("Nouns", stackAlphabet.MNS);
            InitWordType("Nouns plural", stackAlphabet.MNP);
            InitWordType("Time nouns", stackAlphabet.TNS);
            InitWordType("Time nouns plural", stackAlphabet.TNP);
            //--Places
            InitWordType("Architectural features place nouns", stackAlphabet.ARFPNS);
            InitWordType("Architectural features place nouns plural", stackAlphabet.ARFPNP);
            InitWordType("Building and facilities nouns", stackAlphabet.BFNS);
            InitWordType("Building and facilities nouns plural", stackAlphabet.BFNP);
            InitWordType("By water place nouns", stackAlphabet.BWPNS);
            InitWordType("By water place nouns plural", stackAlphabet.BWPNP);
            InitWordType("Living place nouns", stackAlphabet.LPNS);
            InitWordType("Living place nouns plural", stackAlphabet.LPNP);
            InitWordType("Outdoor place nouns", stackAlphabet.OPNS);
            InitWordType("Outdoor place nouns plural", stackAlphabet.OPNP);
            InitWordType("Place nouns", stackAlphabet.PNS);
            InitWordType("Place nouns plural", stackAlphabet.PNP);
            InitWordType("Under the ground place nouns", stackAlphabet.UGPNS);
            InitWordType("Under the ground place nouns plural", stackAlphabet.UGPNP);

            //Names
            InitWordType("Boy names", stackAlphabet.NMB);
            InitWordType("Girl names", stackAlphabet.NMG);
            //Cities
            InitWordType("Most populated cities", stackAlphabet.MPC);
            InitWordType("UK Cities", stackAlphabet.UKC);
            InitWordType("USA Cities", stackAlphabet.USAC);
        }

        private void InitWordType(string name, string stackID)
        {
            var type = new PresentTenseWordType(name, stackID);
            AddWords(type, type.name);
        }

        private void InitWordType(string name, string stackID, params string[] words)
        {
            var type = new PresentTenseWordType(name, stackID);
            AddWords(type, words);
        }

        private void InitPDA()
        {
            var inputAlphabet = from type in words
                                from word in type.Value
                                select word;

            var states = new HashSet<int> { 0, 1, 2 };

            var transitions = new List<PDATransition>();

            var a = stackAlphabet;

            transitions.Add(new PDATransition(0,1,"",PDA.initialStackSymbol,a.S,PDA.initialStackSymbol));

            void AddProductionRule(string popElement, params string[] pushElement)
            {
                transitions.Add(new PDATransition(1, 1, "", popElement, pushElement));
            }

            //S
            AddProductionRule(a.S, a.SB);
            AddProductionRule(a.S, a.B);
            AddProductionRule(a.S, a.DMPF, a.B);
            AddProductionRule(a.S, a.B, a.DMPF);

            //SB
            AddProductionRule(a.SB, a.SBWPP);
            AddProductionRule(a.SB, a.SBWPS);

            //IYWT
            AddProductionRule(a.IYWT, a.I);
            AddProductionRule(a.IYWT, a.YWT);
            AddProductionRule(a.YWT, a.YOU);
            AddProductionRule(a.YWT, a.WE);
            AddProductionRule(a.YWT, a.THEY);

            //HSI
            AddProductionRule(a.HSI, a.HE);
            AddProductionRule(a.HSI, a.SHE);
            AddProductionRule(a.HSI, a.IT);

            //SBWPP
            AddProductionRule(a.SBWPP, a.IYWT);
            AddProductionRule(a.SBWPP, a.SBP);

            //SBWPS
            AddProductionRule(a.SBWPS, a.HSI);
            AddProductionRule(a.SBWPS, a.SBS);

            //B
            AddProductionRule(a.B, a.BS);
            AddProductionRule(a.B, a.BP);

            //BS
            AddProductionRule(a.BS, a.SBWPS, a.VS);
            AddProductionRule(a.BS, a.SBADVMVS);
            AddProductionRule(a.BS, a.SBADVFVS);

            //BP
            AddProductionRule(a.BP, a.SBWPP, a.VP);
            AddProductionRule(a.BP, a.SBADVMVP);
            AddProductionRule(a.BP, a.SBADVFVP);

            //SBP
            AddProductionRule(a.SBP, a.THE, a.ADJCTV, a.NP);
            AddProductionRule(a.SBP, a.THE, a.NP);
            AddProductionRule(a.SBP, a.ADJCTV, a.NP);
            AddProductionRule(a.SBP, a.NP);
            AddProductionRule(a.SBP, a.SBS, a.AND, a.SBS);

            //SBS
            AddProductionRule(a.SBS, a.NM);
            AddProductionRule(a.SBS, a.THE, a.ADJCTV, a.NS);
            AddProductionRule(a.SBS, a.THE, a.NS);

            //SBADVMV
            AddProductionRule(a.SBADVMVS, a.SBWPS, a.ADVOFM, a.VS);
            AddProductionRule(a.SBADVMVP, a.SBWPP, a.ADVOFM, a.VP);

            //SBADVFV
            AddProductionRule(a.SBADVFVS, a.SBWPS, a.ADVOFF, a.VS);
            AddProductionRule(a.SBADVFVP, a.SBWPP, a.ADVOFF, a.VP);

            //NM
            AddProductionRule(a.NM, a.NMB);
            AddProductionRule(a.NM, a.NMG);

            //NS
            AddProductionRule(a.NS, a.MNS);
            AddProductionRule(a.NS, a.PLACENS);
            AddProductionRule(a.NS, a.TNS);

            //NP
            AddProductionRule(a.NP, a.MNP);
            AddProductionRule(a.NP, a.PLACENP);
            AddProductionRule(a.NP, a.TNP);

            //PLACEN
            AddProductionRule(a.PLACEN, a.PLACENS);
            AddProductionRule(a.PLACEN, a.PLACENP);

            //PLACENS
            AddProductionRule(a.PLACENS, a.ARFPNS);
            AddProductionRule(a.PLACENS, a.BFNS);
            AddProductionRule(a.PLACENS, a.BWPNS);
            AddProductionRule(a.PLACENS, a.LPNS);
            AddProductionRule(a.PLACENS, a.OPNS);
            AddProductionRule(a.PLACENS, a.PNS);
            AddProductionRule(a.PLACENS, a.UGPNS);

            //PLACENP
            AddProductionRule(a.PLACENP, a.ARFPNP);
            AddProductionRule(a.PLACENP, a.BFNP);
            AddProductionRule(a.PLACENP, a.BWPNP);
            AddProductionRule(a.PLACENP, a.LPNP);
            AddProductionRule(a.PLACENP, a.OPNP);
            AddProductionRule(a.PLACENP, a.PNP);
            AddProductionRule(a.PLACENP, a.UGPNP);

            //C
            AddProductionRule(a.C, a.MPC);
            AddProductionRule(a.C, a.UKC);
            AddProductionRule(a.C, a.USAC);

            //VERYTOO
            AddProductionRule(a.VERYTOO, a.VERY);
            AddProductionRule(a.VERYTOO, a.TOO);

            //DMPF
            AddProductionRule(a.DMPF, "");
            AddProductionRule(a.DMPF, a.ADVD);
            AddProductionRule(a.DMPF, a.ADVM);
            AddProductionRule(a.DMPF, a.ADVPB);
            AddProductionRule(a.DMPF, a.ADVF);

            //ADVDB
            AddProductionRule(a.ADVD, "");
            AddProductionRule(a.ADVD, a.ADVOFM, a.ENOUGH);
            AddProductionRule(a.VERYTOO, a.ADVOFM);
            AddProductionRule(a.ADVOFD, a.ADJCTV);

            //ADVMB
            AddProductionRule(a.ADVM, "");
            AddProductionRule(a.ADVM, a.ADVOFM);

            //ADVPB
            AddProductionRule(a.ADVPB, "");
            AddProductionRule(a.ADVPB, a.ADVP);
            AddProductionRule(a.ADVPB, a.ADVOFP, a.ADVP);
            AddProductionRule(a.ADVP, "");
            AddProductionRule(a.ADVP, a.ADVOFPP, a.THE, a.PLACEN);
            AddProductionRule(a.ADVP, a.ADVOFPP, a.C);

            //ADVF
            AddProductionRule(a.ADVF, "");
            AddProductionRule(a.ADVF, a.ADVOFF);
            AddProductionRule(a.ADVF, a.EVERY, a.TNS);
            AddProductionRule(a.ADVF, a.ADVOFF, a.EVERY, a.TNS);

            foreach (var wordType in words)
            {
                transitions.AddRange(GetTransitions(wordType.Key.stackID));
            }

            transitions.Add(new PDATransition(1, 2, "", PDA.initialStackSymbol, ""));

            pda = new PDA(inputAlphabet.ToHashSet(), stackAlphabet.allValues, states, 0, transitions);
        }

        private List<PDATransition> GetTransitions(string wordTypeStackElement)
        {
            var transitions = new List<PDATransition>();

            var typeWords = from type in words
                          where type.Key.stackID == wordTypeStackElement
                          from word in type.Value
                          select word;

            foreach (string word in typeWords)
            {
                transitions.Add(new PDATransition(1, 1, "", wordTypeStackElement, word));
            }

            foreach (string word in typeWords)
            {
                transitions.Add(new PDATransition(1, 1, word, word, ""));
            }

            return transitions;
        }

        public void AddWords(PresentTenseWordType type, IEnumerable<string> words)
        {
            if (this.words.ContainsKey(type))
            {
                var currentWords = this.words[type].ToList();
                currentWords.AddRange(words);
                this.words[type] = currentWords.AsEnumerable();
            }
            else
            {
                this.words[type] = words;
            }
        }

        public void AddWords(PresentTenseWordType type, string fileName)
        {
            AddWords(type, PresentTenseWordsLoader.LoadWords(fileName));
        }

        public PDARecognitionResult Recognize(string input)
        {
            return pda.Recognize(input.Split(' '));
        }

        public string Generate()
        {
            return GetSentenseString(pda.Generate());
        }

        public string GetSentenseString(string[] words)
        {
            if (words.Length > 0)
            {
                words[0] = words[0].First().ToString().ToUpper() + words[0].Substring(1);
                return String.Join(" ", words) + ".";
            }
            return "ε";
        } 

    }
}
