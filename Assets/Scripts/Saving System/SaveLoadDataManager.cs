using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveLoadDataManager : MonoBehaviour
{
    public static SaveLoadDataManager instance { get; private set; }

    [SerializeField] private Cloud cloud;

    [Header("Saving options")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption = false;
    [SerializeField] private bool loadFromCloud = false;

    private GameData gameData;
    private FileDataHandler fileDataHandler;
    private List<ISaveLoadData> saveLoadDataObjects;
    private string selectedProfileId = "1";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        NewGame();
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        saveLoadDataObjects = FindAllSaveLoadDataObjecs();
    }

    public void ChangeSelectedProfileId(string newProfileId)
    {
        selectedProfileId = newProfileId;
    }

    public void NewGame() => gameData = new GameData();

    public void SaveGame()
    {
        foreach (ISaveLoadData saveLoadDataObject in saveLoadDataObjects)
        {
            saveLoadDataObject.SaveGame(gameData);
        }

        fileDataHandler.Save(gameData, selectedProfileId);
        cloud.SaveToCloud(gameData, selectedProfileId);
    }

    public void LoadGame()
    {
        gameData = fileDataHandler.Load(selectedProfileId);

        if (loadFromCloud) { gameData = cloud.LoadFromCloud(selectedProfileId); }

        if (gameData == null) { NewGame(); }

        foreach (ISaveLoadData saveLoadDataObject in saveLoadDataObjects)
        {
            saveLoadDataObject.LoadGame(gameData);
        }
    }

    private List<ISaveLoadData> FindAllSaveLoadDataObjecs()
    {
        IEnumerable<ISaveLoadData> saveLoadDataObjects = FindObjectsOfType<MonoBehaviour>().OfType<ISaveLoadData>();

        return new List<ISaveLoadData>(saveLoadDataObjects);
    }

    public Dictionary<string, GameData> GetAllProfilesData()
    {
        return fileDataHandler.LoadAllProfiles();
    }

}
