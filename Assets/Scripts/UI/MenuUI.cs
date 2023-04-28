using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private SaveLoadGameMenu saveLoadGameMenu;

    public void NewGameButton() => SaveLoadDataManager.instance.NewGame();

    public void LoadGameButton() => saveLoadGameMenu.ActivateMenu(SaveLoadGameMenu.SaveLoadState.LoadGame);

    public void SaveGameButton() => saveLoadGameMenu.ActivateMenu(SaveLoadGameMenu.SaveLoadState.SaveGame);

}
