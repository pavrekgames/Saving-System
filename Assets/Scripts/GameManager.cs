using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, ISaveLoadData
{
    public static GameManager instance;

    public int coinsAmount = 0;

    public static event Action OnStatsUpdated;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UpdateStats();
        }
    }

    private void UpdateStats() => OnStatsUpdated?.Invoke();

    #region SaveLoadData Interface
    public void SaveGame(GameData gameData)
    {
        gameData.coinsAmount = coinsAmount;
    }

    public void LoadGame(GameData gameData)
    {
        coinsAmount = gameData.coinsAmount;
        OnStatsUpdated?.Invoke();
    }
    #endregion
}
