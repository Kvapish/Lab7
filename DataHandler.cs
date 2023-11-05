using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
/// </summary>
/// <summary>
/// Класс DataHandler создан для управления операциями сохранения и восстановления данных.
/// </summary>
public class DataHandler
{
    public List<int> LoadUserGuesses(string fileName)
    {
        List<int> userGuesses = new List<int>();
        if (File.Exists(fileName))
        {
            string json = File.ReadAllText(fileName);
            userGuesses = JsonConvert.DeserializeObject<List<int>>(json);
        }
        return userGuesses;
    }

    public void SaveUserGuesses(string fileName, List<int> userGuesses)
    {
        string json = JsonConvert.SerializeObject(userGuesses);
        File.WriteAllText(fileName, json);
    }
}
