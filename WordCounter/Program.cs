using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WordCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите путь к обрабатываемому файлу: ");
            string text = null;

            try
            {
                text = File.ReadAllText(Console.ReadLine());
            }
            catch (IOException e)
            {
                Console.WriteLine("Ошибка обработки файла!\nЧтобы выйти, нажмите Enter");
                Console.ReadLine();
                Environment.Exit(0);
            }

            Dictionary<string, int> dictionary = WordCounter.GetWordCount(text);
            Console.Write("Вывести результат в консоль? (y/n): ");
            if (Console.ReadLine().ToLower().StartsWith("y"))
            {
                foreach (KeyValuePair<string, int> pair in dictionary)
                {
                    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
                }
            }

            Console.Write("Вывести результат в файл? (y/n): ");
            if (Console.ReadLine().ToLower().StartsWith("y"))
            {
                Console.Write("Введите название результирующего файла: ");
                StreamWriter sw = null;

                try
                {
                    sw = new StreamWriter(new FileStream(Console.ReadLine(), FileMode.Create), Encoding.UTF8);
                }
                catch (IOException e)
                {
                    sw.Close();
                    Console.WriteLine("Ошибка обработки файла!\nЧтобы выйти, нажмите Enter");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                
                foreach (KeyValuePair<string, int> pair in dictionary)
                {
                    sw.WriteLine("{0}: {1}", pair.Key, pair.Value);
                }
                sw.Close();
            }

            Console.WriteLine("Чтобы выйти, нажмите Enter");
            Console.ReadLine();
        }
    }
}
