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

        public PresentTenseApplication()
        {
            InitWords();
            InitPDA();
        }

        private void InitWords()
        {
            words = new Dictionary<PresentTenseWordType, IEnumerable<string>>();
            var typeBaseVerb = new PresentTenseWordType("Base Verb", "<VP>", "<verbPlural>");
            AddWords(typeBaseVerb, "Base Verbs");
            var typeBaseVerbS = new PresentTenseWordType("Base Verb -s", "<VS>", "<verbSingular>");
            AddWords(typeBaseVerbS, "Base Verbs -s");
            var typeIYouWeThey = new PresentTenseWordType("I You We They", "<IYWT>", "<IYouWeThey>");
            AddWords(typeIYouWeThey, "I You We They");
            var typeHeSheIt = new PresentTenseWordType("He She It", "<HSI>", "<HeSheIt>");
            AddWords(typeHeSheIt, "He She It");
            var typeAdd = new PresentTenseWordType("And", "<AND>", "<And>");
            AddWords(typeAdd, new List<string>() { "and" });
            var typeThe = new PresentTenseWordType("The", "<THE>", "<The>");
            AddWords(typeThe, new List<string>() { "the" });
            var typeAdjective = new PresentTenseWordType("Adjective", "<ADJCTV>", "<Adjective>");
            AddWords(typeAdjective, "Adjectives");
        }

        enum StackElemet { S, SB, B, SBP, SBS, BS, BP, IYWT, HSI, VS, VP, And }
        private void InitPDA()
        {
            var inputAlphabet = from type in words
                                from word in type.Value
                                select word;

            var stackWordTypeAlphabet = from type in words
                                select type.Key.stackID;

            string[] stackAlphas = { "<S>", "<SB>", "<B>", "<SBP>", "<SBS>", "<BS>", "<BP>", "<IYWT>", "<HSI>", "<VS>", "<VP>", "<And>" };

            var stackAlphabet = stackAlphas.ToList();
            stackAlphabet.AddRange(stackWordTypeAlphabet);

            var states = new HashSet<int> { 0, 1, 2 };

            var transitions = new List<PDATransition>();

            string[] stackZ0S = { stackAlphas[(int)StackElemet.S], PDA.startStackElement };
            transitions.Add(new PDATransition(0, 1, "", PDA.startStackElement, stackZ0S));
            transitions.Add(new PDATransition(1, 1, "", stackAlphas[(int)StackElemet.S], stackAlphas[(int)StackElemet.SB]));
            transitions.Add(new PDATransition(1, 1, "", stackAlphas[(int)StackElemet.S], stackAlphas[(int)StackElemet.B]));
            transitions.Add(new PDATransition(1, 1, "", stackAlphas[(int)StackElemet.SB], stackAlphas[(int)StackElemet.SBP]));
            transitions.Add(new PDATransition(1, 1, "", stackAlphas[(int)StackElemet.SB], stackAlphas[(int)StackElemet.SBS]));
            transitions.Add(new PDATransition(1, 1, "", stackAlphas[(int)StackElemet.B], stackAlphas[(int)StackElemet.BS]));
            transitions.Add(new PDATransition(1, 1, "", stackAlphas[(int)StackElemet.B], stackAlphas[(int)StackElemet.BP]));

            string[] stackSBSVS = { stackAlphas[(int)StackElemet.SBS], stackAlphas[(int)StackElemet.VS] };
            transitions.Add(new PDATransition(1, 1, "", stackAlphas[(int)StackElemet.BS], stackSBSVS));

            string[] stackSBPVP = { stackAlphas[(int)StackElemet.SBP], stackAlphas[(int)StackElemet.VP] };
            transitions.Add(new PDATransition(1, 1, "", stackAlphas[(int)StackElemet.BP], stackSBPVP));

            transitions.Add(new PDATransition(1, 1, "", stackAlphas[(int)StackElemet.SBP], stackAlphas[(int)StackElemet.IYWT]));
            transitions.Add(new PDATransition(1, 1, "", stackAlphas[(int)StackElemet.SBS], stackAlphas[(int)StackElemet.HSI]));

            // (1,e,<And>)->(1,and)
            transitions.Add(new PDATransition(1, 1, "", stackAlphas[(int)StackElemet.And], "and"));
            // (1,and,and)->(1,e)
            transitions.Add(new PDATransition(1, 1, "and", "and", ""));

            foreach(var wordType in stackWordTypeAlphabet)
            {
                transitions.AddRange(GetTransitions(wordType));
            }

            transitions.Add(new PDATransition(1, 2, "", PDA.startStackElement, ""));

            pda = new PDA(inputAlphabet.ToHashSet(), stackAlphabet, states, 0, transitions);
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
            words[0] = words[0].First().ToString().ToUpper() + words[0].Substring(1);
            return String.Join(" ", words) + ".";
        } 

    }
}
