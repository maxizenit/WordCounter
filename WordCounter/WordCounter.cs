using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WordCounter
{
    class WordCounter
    {
        private struct WordCounterInfo
        {
            public ConcurrentDictionary<string, int> ConcurrentDictionary;
            public string Text;
            public int StartPosition;
            public int EndPosition;
        }

        private static int s_processorCount = Environment.ProcessorCount;

        private static int[] GetPartPositions(string text)
        {
            int[] partPositions = new int[s_processorCount];
            for (int i = 0; i < s_processorCount; ++i)
            {
                partPositions[i] = (int)Math.Round((double)i / s_processorCount * text.Length);
                if (partPositions[i] >= text.Length)
                {
                    partPositions[i] = text.Length;
                    continue;
                }

                while (!Char.IsWhiteSpace(text[partPositions[i]]))
                {
                    ++partPositions[i];
                }

                if (partPositions[i] > text.Length)
                {
                    partPositions[i] = text.Length;
                }
            }

            return partPositions;
        }

        private static void CountWordsOnPart(object wordCounterInfo)
        {
            WordCounterInfo info = (WordCounterInfo)wordCounterInfo;
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = info.StartPosition; i < info.EndPosition; ++i)
            {
                char c = info.Text[i];
                if (Char.IsLetter(c))
                {
                    stringBuilder.Append(Char.ToLower(c));
                }
                else if ((c == '-' || c == '\'' || Char.IsNumber(c)) && stringBuilder.Length > 0)
                {
                    stringBuilder.Append(c);
                }
                else if (stringBuilder.Length > 0)
                {
                    info.ConcurrentDictionary.AddOrUpdate(stringBuilder.ToString(), 1, (key, oldValue) => oldValue + 1);
                    stringBuilder.Clear();
                }
            }

            if (stringBuilder.Length > 0)
            {
                info.ConcurrentDictionary.AddOrUpdate(stringBuilder.ToString(), 1, (key, oldValue) => oldValue + 1);
                stringBuilder.Clear();
            }
        }

        public static Dictionary<string, int> GetWordCount(string text)
        {
            ConcurrentDictionary<string, int> concurrentDictionary = new ConcurrentDictionary<string, int>(s_processorCount, 0);
            Thread[] threads = new Thread[s_processorCount];
            int[] partPositions = GetPartPositions(text);

            for (int i = 0; i < s_processorCount; ++i)
            {
                WordCounterInfo info = new WordCounterInfo();
                info.ConcurrentDictionary = concurrentDictionary;
                info.Text = text;
                info.StartPosition = partPositions[i];
                info.EndPosition = (i < s_processorCount - 1) ? partPositions[i + 1] : text.Length;

                threads[i] = new Thread(CountWordsOnPart);
                threads[i].Start(info);
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            return concurrentDictionary.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}
