using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

/// <summary>
/// Вариант 8
/// Игра "Угадай число" (List): Создайте игру, в которой программа случайным образом выбирает число, а пользователь должен угадать его. Используйте список для сохранения попыток пользователя.
/// Соблюдая Microsoft C# code conventions и средства языка C# для задания из предыдущей работы реализовать сохранение и восстановление данных данных массивов объектов:
/// используя сохранение/восстановление в бинарном файле;
/// используя сериализацию/десериализацию в JSON или XML (на выбор); дополнительно оценивается реализация вышеуказанных операций при помощи отдельного класса.
/// Код использует библиотеку Newtonsoft.Json для сериализации и десериализации данных в формат JSON. Данные о попытках пользователя сохраняются как JSON в файл userGuesses.json и восстанавливаются из него при запуске программы.
/// </summary>
class Program
{
    static void Main()
    {
        // Задайте имя файла для сохранения и загрузки данных в формате JSON
        string jsonFileName = "userGuesses.json";
        DataHandler dataHandler = new DataHandler(jsonFileName);

        // Задайте имя файла для сохранения и загрузки данных в бинарном формате
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



