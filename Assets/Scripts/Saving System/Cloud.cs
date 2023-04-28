using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    Dictionary<string, GameData> profilesData;

    private void Start() => profilesData = new Dictionary<string, GameData>();

    public void SaveToCloud(GameData gameData, string profileId)
    {
        if (profilesData.ContainsKey(profileId))
        {
            profilesData[profileId] = gameData;
        }
        else
        {
            profilesData.Add(profileId, gameData);
        }
    }

    public GameData LoadFromCloud(string profileId)
    {
        profilesData = SaveLoadDataManager.instance.GetAllProfilesData();

        if (profilesData.ContainsKey(profileId))
        {
            return profilesData[profileId];
        }
        else
        {
            return null;
        }
    }
}
