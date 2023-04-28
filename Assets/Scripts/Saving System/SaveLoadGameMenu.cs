using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveLoadGameMenu : MonoBehaviour
{

    [SerializeField] private Canvas loadGameCanvas;
    [SerializeField] private SaveSlot[] saveSlots;
    [SerializeField] private TextMeshProUGUI saveLoadStateText;

    public enum SaveLoadState
    {
        SaveGame,
        LoadGame
    }

    public SaveLoadState saveLoadState;

    public void ActivateMenu(SaveLoadState saveLoadState)
    {
        loadGameCanvas.enabled = true;
        this.saveLoadState = saveLoadState;
        SetSaveLoadStateText();

        Dictionary<string, GameData> profilesData = SaveLoadDataManager.instance.GetAllProfilesData();

        foreach(SaveSlot saveSlot in saveSlots)
        {
            GameData profileData = null;
            profilesData.TryGetValue(saveSlot.GetProfileId(), out profileData);
            saveSlot.SetData(profileData);
        }
    }

    public void ChooseSaveSlot(SaveSlot saveSlot)
    {
        SaveLoadDataManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());

        if (saveLoadState == SaveLoadState.SaveGame)
        {
            SaveLoadDataManager.instance.SaveGame();
        }
        else
        {
            SaveLoadDataManager.instance.LoadGame();
        }

        loadGameCanvas.enabled = false;
    }

    private void SetSaveLoadStateText()
    {
        if (saveLoadState == SaveLoadState.SaveGame)
        {
            saveLoadStateText.text = "Save game";
        }
        else
        {
            saveLoadStateText.text = "Load game";
        }
    }

    public void BackButton()
    {
        loadGameCanvas.enabled = false;
    }

}
