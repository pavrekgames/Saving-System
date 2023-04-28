using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ISaveLoadData
{

    [SerializeField] private Transform player;

    public float health = 100;
    [HideInInspector] public Vector3 playerPosition;
    [HideInInspector] public Quaternion playerRotation;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 3f;

    public static event Action OnPlayerStatsUpdated;
   
    void Start()
    {
        
    }

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
        Vector2 inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = +1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = +1;
        }

        inputVector = inputVector.normalized;

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        player.transform.position += moveDir * moveSpeed * Time.deltaTime;

        player.transform.forward = Vector3.Slerp(player.transform.forward, moveDir, rotationSpeed * Time.deltaTime);

    }

    private void UpdatePlayerStats()
    {
        playerPosition = player.transform.position;
        playerRotation = player.transform.rotation;
        Debug.Log(player.transform.position);
        Debug.Log(player.transform.rotation);
        OnPlayerStatsUpdated?.Invoke();
    }

    #region SaveLoadData Interface
    public void SaveGame(GameData gameData)
    {
        playerPosition = player.transform.position;
        playerRotation = player.transform.rotation;
        gameData.playerHealth = health;
        gameData.playerPosition = playerPosition;
        gameData.playerRotation = playerRotation;
    }

    public void LoadGame(GameData gameData)
    {
        health = gameData.playerHealth;
        playerPosition = gameData.playerPosition;
        playerRotation = gameData.playerRotation;
        player.transform.position = playerPosition;
        player.transform.rotation = playerRotation;
        Debug.Log("Loaded");
    }

    #endregion
}
