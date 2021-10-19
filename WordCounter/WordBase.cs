using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace WordCounter
{
    class WordBase
    {
        private Dictionary<string, int> dictionary;

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

        public WordBase()
        {
            dictionary = new Dictionary<string, int>();
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
            dictionary = dictionary.OrderBy(pair => pair.Value).Reverse().ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public void Print()
        {
            foreach (KeyValuePair<string, int> pair in dictionary)
            {
                Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            }
        }

        public void WriteInFile(string path)
        {
            StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8);
            foreach (KeyValuePair<string, int> pair in dictionary)
            {
                Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            }
            sw.Close();
        }
    }
}