using System;

namespace WordCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            WordBase wb = new WordBase();
            Console.Write("Введите путь к обрабатываемому файлу: ");
            wb.ReadFile(Console.ReadLine());
            Console.Write("Вывести результат в консоли? (y/n): ");
            if (Char.ToLower(Console.ReadLine()[0]) == 'y')
            {
                wb.Print();
            }
            Console.Write("Вывести результат в файл? (y/n): ");
            if (Char.ToLower(Console.ReadLine()[0]) == 'y')
            {
                Console.Write("Введите путь к файлу с файлу с результатом: ");
                wb.WriteInFile(Console.ReadLine());
            }
            Console.Write("Нажмите Enter для выхода: ");
            Console.ReadLine();
        }
    }
}
