using System.Collections.Generic;
using System.IO;

public class BinaryDataHandler
{
    private string fileName;

    public BinaryDataHandler(string fileName)
    {
        this.fileName = fileName;
    }

    public List<int> LoadUserGuesses()
    {
        List<int> userGuesses = new List<int>();
        if (File.Exists(fileName))
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                    {
                        userGuesses.Add(binaryReader.ReadInt32());
                    }
                }
            }
        }
        return userGuesses;
    }

    public void SaveUserGuesses(List<int> userGuesses)
    {
        using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
        {
            using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
            {
                foreach (int guess in userGuesses)
                {
                    binaryWriter.Write(guess);
                }
            }
        }
    }
}