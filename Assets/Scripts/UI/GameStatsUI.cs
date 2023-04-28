using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStatsUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Player player;

    [Header("General Stats")]
    [SerializeField] private TextMeshProUGUI coinsAmountText;

    [Header("Player Stats")]
    [SerializeField] private TextMeshProUGUI playerHealthText;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerLevelText;
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

    private void UpdateGeneralStats()
    {
        playerHealthText.text = player.health.ToString();
        playerNameText.text = player.playerName;
        playerLevelText.text = player.playerLevel.ToString();
    }

    private void UpdatePlayerPositionAndRotation()
    {
        playerPositionText.text = player.playerPosition.ToString();
        playerRotationText.text = player.playerRotation.ToString();
    }

    private void UpdateCoinsAmount() => coinsAmountText.text = gameManager.coinsAmount.ToString();

}
