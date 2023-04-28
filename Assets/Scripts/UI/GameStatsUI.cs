using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStatsUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Player player;

    [Header("General Stats")]
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerLevelText;
    [SerializeField] private TextMeshProUGUI coinsAmountText;

    [Header("Player Stats")]
    [SerializeField] private TextMeshProUGUI playerHealthText;
    [SerializeField] private TextMeshProUGUI playerPositionText;
    [SerializeField] private TextMeshProUGUI playerRotationText;

    void Start()
    {
        GameManager.OnStatsUpdated += UpdateGeneralStats;
        GameManager.OnStatsUpdated += UpdatePlayerPositionAndRotation;
        GameManager.OnStatsUpdated += UpdateCoinsAmount;
        Player.OnPlayerStatsUpdated += UpdatePlayerPositionAndRotation;
        Coin.OnCoinCollected += UpdateCoinsAmount;
    }

    
    void Update()
    {
        
    }

    private void UpdateGeneralStats()
    {
        playerNameText.text = gameManager.playerName;
        playerLevelText.text = gameManager.playerLevel.ToString();
    }

    private void UpdatePlayerPositionAndRotation()
    {
        playerHealthText.text = player.health.ToString();
        playerPositionText.text = player.playerPosition.ToString();
        playerRotationText.text = player.playerRotation.ToString();
    }

    private void UpdateCoinsAmount()
    {
        coinsAmountText.text = gameManager.coinsAmount.ToString();
    }

}
