using System;
using System.Collections.Generic;
/// <summary>
/// Вариант 8
/// Создал список для сохранения попыток пользователя, в этом списке будут храниться введенные пользователем числа,сгенерировал случайное число, которое пользователь должен угадать,
/// пользователь будет вводить числа и они будут добавляться в список userGuesses. List был выбран, так как он обеспечивает удобное добавление и хранение элементов в последовательном порядке. 
/// </summary>
class Program
{
    static void Main()
    {
        List<int> userGuesses = new List<int>(); 
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