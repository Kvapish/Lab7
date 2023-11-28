using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public delegate bool FilterDelegate(int guess);
public delegate int ComparisonDelegate(int guess1, int guess2);

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
                    List<int> evenGuesses = dataHandler.FilterUserGuesses(userGuesses, guess => guess % 2 == 0);
                    Console.Write("Отфильтрованные данные (четные числа): ");
                    foreach (var guess in evenGuesses)
                    {
                        Console.Write(guess + " ");
                    }
                    Console.WriteLine(); 
                    dataHandler.SortUserGuesses(userGuesses, (guess1, guess2) => guess1.CompareTo(guess2));
                    Console.Write("Отсортированные данные: ");
                    foreach (var guess in userGuesses)
                    {
                        Console.Write(guess + " ");
                    }
                    Console.WriteLine(); 

                    //Заполнение массива в 4 потока случайными данными
                    int arraySize = 1000000;
                    int[] dataArray = new int[arraySize];
                    Parallel.For(0, 4, i =>
                    {
                        int startIndex = i * (arraySize / 4);
                        int endIndex = (i + 1) * (arraySize / 4);

                        for (int j = startIndex; j < endIndex; j++)
                        {
                            dataArray[j] = random.Next(1, 101);
                        }
                    });

                    //Параллельная сортировка массива
                    Console.WriteLine("Параллельная сортировка массива:");           
                    int[] dataArrayCopy = (int[])dataArray.Clone();
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    Array.Sort(dataArrayCopy);
                    watch.Stop();
                    Console.WriteLine($"Время выполнения сортировки без параллелизма: {watch.ElapsedMilliseconds} мс");
                    for (int threadCount = 2; threadCount <= 8; threadCount *= 2)
                    {
                        dataArrayCopy = (int[])dataArray.Clone(); 
                        watch = System.Diagnostics.Stopwatch.StartNew();
                        ParallelSort(dataArrayCopy, threadCount);
                        watch.Stop();
                        Console.WriteLine($"Время выполнения параллельной сортировки ({threadCount} потоков): {watch.ElapsedMilliseconds} мс");
                    }
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
    // Метод для параллельной сортировки массива
    static void ParallelSort(int[] array, int threadCount)
    {
        List<int[]> subArrays = new List<int[]>();
        for (int i = 0; i < threadCount; i++)
        {
            int startIndex = i * (array.Length / threadCount);
            int endIndex = (i + 1) * (array.Length / threadCount);
            int[] subArray = new int[endIndex - startIndex];
            Array.Copy(array, startIndex, subArray, 0, endIndex - startIndex);
            subArrays.Add(subArray);
        }
        Parallel.For(0, threadCount, i =>
        {
            Array.Sort(subArrays[i]);
        });
        for (int i = 1; i < threadCount; i++)
        {
            subArrays[0] = MergeArrays(subArrays[0], subArrays[i]);
        }
        Array.Copy(subArrays[0], array, array.Length);
    }

    // Метод для объединения двух отсортированных массивов
    static int[] MergeArrays(int[] array1, int[] array2)
    {
        int[] mergedArray = new int[array1.Length + array2.Length];
        int i = 0, j = 0, k = 0;

        while (i < array1.Length && j < array2.Length)
        {
            if (array1[i] < array2[j])
            {
                mergedArray[k++] = array1[i++];
            }
            else
            {
                mergedArray[k++] = array2[j++];
            }
        }

        while (i < array1.Length)
        {
            mergedArray[k++] = array1[i++];
        }

        while (j < array2.Length)
        {
            mergedArray[k++] = array2[j++];
        }

        return mergedArray;
    }
}






