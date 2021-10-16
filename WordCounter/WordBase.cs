using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WordCounter
{
    class WordBase
    {
        private Dictionary<string, int> dictionary;
        private SortedDictionary<int, SortedSet<string>> sortedDictionary;

        private void AddWord(string word)
        {
            if (!dictionary.ContainsKey(word))
            {
                dictionary.Add(word, 1);
            }
            else
            {
                ++dictionary[word];
            }
        }

        private void ConvertDictionary()
        {
            foreach (KeyValuePair<string, int> pair in dictionary)
            {
                if (!sortedDictionary.ContainsKey(pair.Value))
                {
                    sortedDictionary.Add(pair.Value, new SortedSet<string>());
                }
                sortedDictionary[pair.Value].Add(pair.Key);
            }
        }

        public WordBase()
        {
            dictionary = new Dictionary<string, int>();
            sortedDictionary = new SortedDictionary<int, SortedSet<string>>(new ReverseComparer());
        }

        public void ReadFile(string path)
        {
            StringBuilder sb = new StringBuilder();
            StreamReader sr = new StreamReader(path);
            while (!sr.EndOfStream)
            {
                char c = (char)sr.Read();
                if (Char.IsLetter(c))
                {
                    sb.Append(Char.ToLower(c));
                }
                else if ((c == '-' || c == '\'' || Char.IsNumber(c)) && sb.Length > 0)
                {
                    sb.Append(c);
                }
                else if (sb.Length > 0)
                {
                    AddWord(sb.ToString());
                    sb.Clear();
                }
            }
            if (sb.Length > 0)
            {
                AddWord(sb.ToString());
                sb.Clear();
            }
            sr.Close();
            ConvertDictionary();
        }

        public void Print()
        {
            if (sortedDictionary.Count > 0)
            {
                foreach (KeyValuePair<int, SortedSet<string>> pair in sortedDictionary)
                {
                    foreach (string word in pair.Value)
                    {
                        Console.WriteLine("{0}: {1}", word, pair.Key);
                    }                    
                }
            }
        }

        public void WriteInFile(string path)
        {
            StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8);
            if (sortedDictionary.Count > 0)
            {
                foreach (KeyValuePair<int, SortedSet<string>> pair in sortedDictionary)
                {
                    foreach (string word in pair.Value)
                    {
                        sw.WriteLine("{0}: {1}", word, pair.Key);
                    }
                }
            }
            sw.Close();
        }
    }
}