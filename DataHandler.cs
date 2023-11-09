using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
/// <summary>
/// Класс DataHandler создан для управления операциями сохранения и восстановления данных в формате JSON.
/// </summary>
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
}
