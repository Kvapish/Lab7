using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class Program
{
    static void Main()
    {
        string jsonFileName = "userGuesses.json";
        DataHandler dataHandler = new DataHandler(jsonFileName);

        string binaryFileName = "userGuesses.bin";
        BinaryDataHandler binaryDataHandler = new BinaryDataHandler(binaryFileName);

        List<int> userGuesses = dataHandler.LoadUserGuesses();

        Random random = new Random();
        int secretNumber = random.Next(1, 101);
        int attempts = 0;

        while (true)
        {
            Console.Write("Угадайте число (от 1 до 100): ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int userGuess))
            {
                userGuesses.Add(userGuess);
                attempts++;

                if (userGuess == secretNumber)
                {
                    Console.WriteLine($"Поздравляем! Вы угадали число {secretNumber} с {attempts} попыток.");
                    dataHandler.SaveUserGuesses(userGuesses);
                    binaryDataHandler.SaveUserGuesses(userGuesses);
                    // Фильтрация данных
                    List<int> evenGuesses = dataHandler.FilterUserGuesses(userGuesses, guess => guess % 2 == 0);
                    Console.Write("Отфильтрованные данные (четные числа): ");
                    foreach (var guess in evenGuesses)
                    {
                        Console.Write(guess + " ");
                    }
                    Console.WriteLine(); // Перейти на следующую строку

                    // Сортировка данных
                    dataHandler.SortUserGuesses(userGuesses, (guess1, guess2) => guess1.CompareTo(guess2));
                    Console.Write("Отсортированные данные: ");
                    foreach (var guess in userGuesses)
                    {
                        Console.Write(guess + " ");
                    }
                    Console.WriteLine(); // Перейти на следующую строку
                    Console.WriteLine("Для завершения программы, нажмите любую клавишу...");
                    Console.ReadKey();
                    break;
                }
                else if (userGuess < secretNumber)
                {
                    Console.WriteLine("Загаданное число больше.");
                }
                else
                {
                    Console.WriteLine("Загаданное число меньше.");
                }
            }
            else
            {
                Console.WriteLine("Пожалуйста, введите корректное число.");
            }
        }

       
    }
}













