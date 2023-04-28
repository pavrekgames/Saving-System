using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, ISaveLoadData
{

    public static GameManager instance;

    public string playerName;
    public int playerLevel = 1;
    public int coinsAmount = 0;

    public static event Action OnStatsUpdated;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UpdateStats();
        }
    }

    private void UpdateStats()
    {
        OnStatsUpdated?.Invoke();
    }

    #region SaveLoadData Interface

    public void LoadGame(GameData gameData)
    {
        playerName = gameData.playerName;
        playerLevel = gameData.playerLevel;
        coinsAmount = gameData.coinsAmount;

        OnStatsUpdated?.Invoke();

    }

    public void SaveGame(GameData gameData)
    {
        gameData.playerName = playerName;
        gameData.playerLevel = playerLevel;
        gameData.coinsAmount = coinsAmount;
    }

    #endregion

}
