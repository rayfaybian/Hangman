using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangman
{
    class Word
    {
        public string word { get; private set; }

        public string visibleWord { get; set; }

        private string[] wordfile = System.IO.File.ReadAllLines("C:\\Coding\\C#\\Hangman\\Hangman\\words.txt");

        List<string> wordlist = new List<string>();

        public Word()
        {
            SelectNewWord();
        }

        public void SelectNewWord()
        {
            foreach (string element in wordfile)
            {
                wordlist.Add(element.Split(";").FirstOrDefault());
            }

            Random random = new Random();
            int i = random.Next(wordlist.Count());
            word = wordlist.ElementAt(i);

            char[] letterCount = word.ToCharArray();
            char[] placeholder = new char[word.Length];
            var counter = 0;
            foreach (var element in letterCount)
            {
                placeholder[counter] = '.';
                counter++;
            }
            visibleWord = new string(placeholder);
        }
    }
}
