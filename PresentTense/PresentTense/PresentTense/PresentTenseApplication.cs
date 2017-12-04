using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            //S
            AddTransition(transitions, 0, 1, "", PDA.startStackElement, a.S, PDA.startStackElement);
            AddTransition(transitions, 1, 1, "", a.S, a.SB);
            AddTransition(transitions, 1, 1, "", a.S, a.B);
            AddTransition(transitions, 1, 1, "", a.S, a.DMPF, a.B);
            AddTransition(transitions, 1, 1, "", a.S, a.B, a.DMPF);

            //SB
            AddTransition(transitions, 1, 1, "", a.SB, a.SBWPP);
            AddTransition(transitions, 1, 1, "", a.SB, a.SBWPS);

            //IYWT
            AddTransition(transitions, 1, 1, "", a.IYWT, a.I);
            AddTransition(transitions, 1, 1, "", a.IYWT, a.YWT);
            AddTransition(transitions, 1, 1, "", a.YWT, a.YOU);
            AddTransition(transitions, 1, 1, "", a.YWT, a.WE);
            AddTransition(transitions, 1, 1, "", a.YWT, a.THEY);

            //HSI
            AddTransition(transitions, 1, 1, "", a.HSI, a.HE);
            AddTransition(transitions, 1, 1, "", a.HSI, a.SHE);
            AddTransition(transitions, 1, 1, "", a.HSI, a.IT);

            //SBWPP
            AddTransition(transitions, 1, 1, "", a.SBWPP, a.IYWT);
            AddTransition(transitions, 1, 1, "", a.SBWPP, a.SBP);

            //SBWPS
            AddTransition(transitions, 1, 1, "", a.SBWPS, a.HSI);
            AddTransition(transitions, 1, 1, "", a.SBWPS, a.SBS);

            //B
            AddTransition(transitions, 1, 1, "", a.B, a.BS);
            AddTransition(transitions, 1, 1, "", a.B, a.BP);

            //BS
            AddTransition(transitions, 1, 1, "", a.BS, a.SBWPS, a.VS);
            AddTransition(transitions, 1, 1, "", a.BS, a.SBADVMVS);
            AddTransition(transitions, 1, 1, "", a.BS, a.SBADVFVS);

            //BP
            AddTransition(transitions, 1, 1, "", a.BP, a.SBWPP, a.VP);
            AddTransition(transitions, 1, 1, "", a.BP, a.SBADVMVP);
            AddTransition(transitions, 1, 1, "", a.BP, a.SBADVFVP);

            //SBP
            AddTransition(transitions, 1, 1, "", a.SBP, a.THE, a.ADJCTV, a.NP);
            AddTransition(transitions, 1, 1, "", a.SBP, a.THE, a.NP);
            AddTransition(transitions, 1, 1, "", a.SBP, a.ADJCTV, a.NP);
            AddTransition(transitions, 1, 1, "", a.SBP, a.NP);
            AddTransition(transitions, 1, 1, "", a.SBP, a.SBS, a.AND, a.SBS);

            //SBS
            AddTransition(transitions, 1, 1, "", a.SBS, a.NM);
            AddTransition(transitions, 1, 1, "", a.SBS, a.THE, a.ADJCTV, a.NS);
            AddTransition(transitions, 1, 1, "", a.SBS, a.THE, a.NS);

            //SBADVMV
            AddTransition(transitions, 1, 1, "", a.SBADVMVS, a.SBWPS, a.ADVOFM, a.VS);
            AddTransition(transitions, 1, 1, "", a.SBADVMVP, a.SBWPP, a.ADVOFM, a.VP);

            //SBADVFV
            AddTransition(transitions, 1, 1, "", a.SBADVFVS, a.SBWPS, a.ADVOFF, a.VS);
            AddTransition(transitions, 1, 1, "", a.SBADVFVP, a.SBWPP, a.ADVOFF, a.VP);

            //NM
            AddTransition(transitions, 1, 1, "", a.NM, a.NMB);
            AddTransition(transitions, 1, 1, "", a.NM, a.NMG);

            //NS
            AddTransition(transitions, 1, 1, "", a.NS, a.MNS);
            AddTransition(transitions, 1, 1, "", a.NS, a.PLACENS);
            AddTransition(transitions, 1, 1, "", a.NS, a.TNS);

            //NP
            AddTransition(transitions, 1, 1, "", a.NP, a.MNP);
            AddTransition(transitions, 1, 1, "", a.NP, a.PLACENP);
            AddTransition(transitions, 1, 1, "", a.NP, a.TNP);

            //PLACEN
            AddTransition(transitions, 1, 1, "", a.PLACEN, a.PLACENS);
            AddTransition(transitions, 1, 1, "", a.PLACEN, a.PLACENP);

            //PLACENS
            AddTransition(transitions, 1, 1, "", a.PLACENS, a.ARFPNS);
            AddTransition(transitions, 1, 1, "", a.PLACENS, a.BFNS);
            AddTransition(transitions, 1, 1, "", a.PLACENS, a.BWPNS);
            AddTransition(transitions, 1, 1, "", a.PLACENS, a.LPNS);
            AddTransition(transitions, 1, 1, "", a.PLACENS, a.OPNS);
            AddTransition(transitions, 1, 1, "", a.PLACENS, a.PNS);
            AddTransition(transitions, 1, 1, "", a.PLACENS, a.UGPNS);

            //PLACENP
            AddTransition(transitions, 1, 1, "", a.PLACENP, a.ARFPNP);
            AddTransition(transitions, 1, 1, "", a.PLACENP, a.BFNP);
            AddTransition(transitions, 1, 1, "", a.PLACENP, a.BWPNP);
            AddTransition(transitions, 1, 1, "", a.PLACENP, a.LPNP);
            AddTransition(transitions, 1, 1, "", a.PLACENP, a.OPNP);
            AddTransition(transitions, 1, 1, "", a.PLACENP, a.PNP);
            AddTransition(transitions, 1, 1, "", a.PLACENP, a.UGPNP);

            //C
            AddTransition(transitions, 1, 1, "", a.C, a.MPC);
            AddTransition(transitions, 1, 1, "", a.C, a.UKC);
            AddTransition(transitions, 1, 1, "", a.C, a.USAC);

            //VERYTOO
            AddTransition(transitions, 1, 1, "", a.VERYTOO, a.VERY);
            AddTransition(transitions, 1, 1, "", a.VERYTOO, a.TOO);

            //DMPF
            AddTransition(transitions, 1, 1, "", a.DMPF, "");
            AddTransition(transitions, 1, 1, "", a.DMPF, a.ADVD);
            AddTransition(transitions, 1, 1, "", a.DMPF, a.ADVM);
            AddTransition(transitions, 1, 1, "", a.DMPF, a.ADVPB);
            AddTransition(transitions, 1, 1, "", a.DMPF, a.ADVF);

            //ADVDB
            AddTransition(transitions, 1, 1, "", a.ADVD, "");
            AddTransition(transitions, 1, 1, "", a.ADVD, a.ADVOFM, a.ENOUGH);
            AddTransition(transitions, 1, 1, "", a.VERYTOO, a.ADVOFM);
            AddTransition(transitions, 1, 1, "", a.ADVOFD, a.ADJCTV);

            //ADVMB
            AddTransition(transitions, 1, 1, "", a.ADVM, "");
            AddTransition(transitions, 1, 1, "", a.ADVM, a.ADVOFM);

            //ADVPB
            AddTransition(transitions, 1, 1, "", a.ADVPB, "");
            AddTransition(transitions, 1, 1, "", a.ADVPB, a.ADVP);
            AddTransition(transitions, 1, 1, "", a.ADVPB, a.ADVOFP, a.ADVP);
            AddTransition(transitions, 1, 1, "", a.ADVP, "");
            AddTransition(transitions, 1, 1, "", a.ADVP, a.ADVOFPP, a.THE, a.PLACEN);
            AddTransition(transitions, 1, 1, "", a.ADVP, a.ADVOFPP, a.C);

            //ADVF
            AddTransition(transitions, 1, 1, "", a.ADVF, "");
            AddTransition(transitions, 1, 1, "", a.ADVF, a.ADVOFF);
            AddTransition(transitions, 1, 1, "", a.ADVF, a.EVERY, a.TNS);
            AddTransition(transitions, 1, 1, "", a.ADVF, a.ADVOFF, a.EVERY, a.TNS);

            foreach (var wordType in words)
            {
                transitions.AddRange(GetTransitions(wordType.Key.stackID));
            }

            transitions.Add(new PDATransition(1, 2, "", PDA.startStackElement, ""));

            pda = new PDA(inputAlphabet.ToHashSet(), stackAlphabet.allValues, states, 0, transitions);
        }

        private void AddTransition(List<PDATransition> transitions, int fromState, int toState, string readFromInput, string popElement, params string[] pushElement)
        {
            transitions.Add(new PDATransition(fromState, toState, readFromInput, popElement, pushElement));
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
            return GetSentenseString(pda.Generate(1));
        }

        public string GetSentenseString(List<string> words)
        {
            if (words.Count > 0)
            {
                words[0] = words[0].First().ToString().ToUpper() + words[0].Substring(1);
                return String.Join(" ", words) + ".";
            }
            return "ε";
        } 

    }
}
