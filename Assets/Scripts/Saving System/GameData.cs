using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //player
    public string playerName;
    public int playerLevel;
    public float playerHealth;
    public Vector3 playerPosition;
    public Quaternion playerRotation;

    //general
    public int coinsAmount;

    //objects
    public SerializableDictionary<string, bool> coinsCollected;

    public GameData()
    {
        //general
        coinsAmount = 0;

        // Player
        this.playerName = "NonName";
        this.playerLevel = 1;
        this.playerHealth = 100f;
        this.playerPosition = new Vector3(0, 0, 0);
        this.playerRotation = new Quaternion(0, 0, 0, 0);

        //Objects
        coinsCollected = new SerializableDictionary<string, bool>();

    }
  
}
