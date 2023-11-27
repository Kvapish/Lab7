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

    public List<int> FilterUserGuesses(List<int> guesses, FilterDelegate filter)
    {
        List<int> filteredGuesses = new List<int>();
        foreach (var guess in guesses)
        {
            if (filter(guess))
            {
                filteredGuesses.Add(guess);
            }
        }
        return filteredGuesses;
    }

    public void SortUserGuesses(List<int> guesses, ComparisonDelegate sort)
    {
        for (int i = 0; i < guesses.Count - 1; i++)
        {
            for (int j = i + 1; j < guesses.Count; j++)
            {
                if (sort(guesses[i], guesses[j]) > 0)
                {
                    int temp = guesses[i];
                    guesses[i] = guesses[j];
                    guesses[j] = temp;
                }
            }
        }
    }
}
