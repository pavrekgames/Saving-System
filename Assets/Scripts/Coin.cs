using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ISaveLoadData
{
    [SerializeField] private string coinID;
    [SerializeField] private bool hasCollected = false;

    public static event Action OnCoinCollected;

    void Update() => transform.Rotate(0, 0, 1);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasCollected)
        {
            CollectCoin();
        }
    }

    private void CollectCoin()
    {
        hasCollected = true;
        GameManager.instance.coinsAmount++;
        OnCoinCollected?.Invoke();
        gameObject.SetActive(false);
    }

    #region SaveLoadData Interface
    public void SaveGame(GameData gameData)
    {
        if (gameData.coinsCollected.ContainsKey(coinID))
        {
            gameData.coinsCollected.Remove(coinID);
        }

        gameData.coinsCollected.Add(coinID, hasCollected);

    }

    public void LoadGame(GameData gameData)
    {
        gameData.coinsCollected.TryGetValue(coinID, out hasCollected);

        if (hasCollected)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
    #endregion

}
