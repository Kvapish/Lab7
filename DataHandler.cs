using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.IO;

public class DataHandler
{
    private string fileName;

    public DataHandler(string fileName)
    {
        this.fileName = fileName;
    }

    public List<int> LoadUserGuesses()
    {
        List<int> userGuesses = new List<int>();
        if (File.Exists(fileName))
        {
            string json = File.ReadAllText(fileName);
            userGuesses = JsonConvert.DeserializeObject<List<int>>(json);
        }
        return userGuesses;
    }

    public void SaveUserGuesses(List<int> userGuesses)
    {
        string json = JsonConvert.SerializeObject(userGuesses);
        File.WriteAllText(fileName, json);
    }

    public List<int> FilterUserGuesses(List<int> guesses, Predicate<int> filter)
    {
        return guesses.FindAll(filter);
    }

    public void SortUserGuesses(List<int> guesses, Comparison<int> sort)
    {
        guesses.Sort(sort);
    }
}
