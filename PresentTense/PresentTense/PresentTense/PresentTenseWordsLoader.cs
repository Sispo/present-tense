using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PresentTense
{
    public class PresentTenseWordsLoader
    {
        public static IEnumerable<string> LoadWords(string fileName)
        {
            var words = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader("../../../../Words/" + fileName + ".txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        words.Add(sr.ReadLine());
                    }
                }

            } catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return words.AsEnumerable();
        }
    }
}
