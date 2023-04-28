using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveSlot : MonoBehaviour
{
    [SerializeField] private string profileId = "";
    [SerializeField] private TextMeshProUGUI slotNameText;

    public void SetData(GameData data)
    {
        if(data == null)
        {
            slotNameText.text = "EMPTY";
        }
        else
        {
            slotNameText.text = profileId;
        }
    }

    public string GetProfileId()
    {
        return profileId;
    }

}
