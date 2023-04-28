using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ISaveLoadData
{
    [SerializeField] private Transform player;

    public float health = 100;
    public string playerName;
    public int playerLevel = 1;

    [HideInInspector] public Vector3 playerPosition;
    [HideInInspector] public Quaternion playerRotation;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 3f;

    public static event Action OnPlayerStatsUpdated;

    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.U))
        {
            UpdatePlayerStats();
        }
    }

    private void Movement()
    {
        float moveForwardBack = Input.GetAxisRaw("Vertical");
        float moveLeftRight = Input.GetAxisRaw("Horizontal");

        Vector3 moveDir = new Vector3(moveLeftRight, 0f, moveForwardBack).normalized;
        player.transform.position += moveDir * moveSpeed * Time.deltaTime;
        player.transform.forward = Vector3.Slerp(player.transform.forward, moveDir, rotationSpeed * Time.deltaTime);

    }

    private void UpdatePlayerStats()
    {
        playerPosition = player.transform.position;
        playerRotation = player.transform.rotation;
        OnPlayerStatsUpdated?.Invoke();
    }

    #region SaveLoadData Interface
    public void SaveGame(GameData gameData)
    {
        playerPosition = player.transform.position;
        playerRotation = player.transform.rotation;

        gameData.playerHealth = health;
        gameData.playerName = playerName;
        gameData.playerLevel = playerLevel;

        gameData.playerPosition = playerPosition;
        gameData.playerRotation = playerRotation;
    }

    public void LoadGame(GameData gameData)
    {
        health = gameData.playerHealth;
        playerName = gameData.playerName;
        playerLevel = gameData.playerLevel;

        playerPosition = gameData.playerPosition;
        playerRotation = gameData.playerRotation;

        player.transform.position = playerPosition;
        player.transform.rotation = playerRotation;
    }
    #endregion
}
