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
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                wb.Print();
            }
            Console.Write("\nВывести результат в файл? (y/n): ");
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                Console.Write("\nВведите путь к файлу с файлу с результатом: ");
                wb.WriteInFile(Console.ReadLine());
            }
            Console.Write("\nНажмите любую клавишу: ");
            Console.ReadKey();
        }
    }
}
