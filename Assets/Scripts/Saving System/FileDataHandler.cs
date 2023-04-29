using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dirPath = "";
    private string fileName = "";

    private bool useEncryption = false;

    private readonly string encryptionCodeWord = "Secret";

    public FileDataHandler(string dirPath, string fileName, bool useEncryption)
    {
        this.dirPath = dirPath;
        this.fileName = fileName;
        this.useEncryption = useEncryption;
    }

    // Edit this function to load game from other format
    public GameData Load(string profileId)
    {
        string fullPath = Path.Combine(dirPath, profileId, fileName);
        GameData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (useEncryption)
                {
                    dataToLoad = EncryptDecryptData(dataToLoad);
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);

            }
            catch (Exception e)
            {
                Debug.LogError("Error during saving data to file: " + fullPath + "\n" + e);
            }
        }

        return loadedData;

    }

    // Edit this function to save game to other format
    public void Save(GameData data, string profileId)
    {
        string fullPath = Path.Combine(dirPath, profileId, fileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToSave = JsonUtility.ToJson(data, true);

            if (useEncryption)
            {
                dataToSave = EncryptDecryptData(dataToSave);
            }

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToSave);
                }
            }

        }
        catch (Exception e)
        {
            Debug.LogError("Error during saving data to file: " + fullPath + "\n" + e);
        }
    }

    public Dictionary<string, GameData> LoadAllProfiles()
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();

        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dirPath).EnumerateDirectories();

        foreach (DirectoryInfo dirInfo in dirInfos)
        {
            string profileId = dirInfo.Name;
            string fullPath = Path.Combine(dirPath, profileId, fileName);

            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("Directory has been skipped because it does not contain data: " + profileId);
                continue;
            }

            GameData profileData = Load(profileId);

            if (profileData != null)
            {
                profileDictionary.Add(profileId, profileData);
            }
            else
            {
                Debug.LogError("Something went wrong during looading profileId: " + profileId);
            }
        }

        return profileDictionary;
    }

    private string EncryptDecryptData(string data)
    {
        string modifiedData = "";

        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }

        return modifiedData;
    }

}
