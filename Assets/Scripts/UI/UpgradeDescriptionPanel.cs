using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeDescriptionPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI upgradeNameText;
    [SerializeField] TextMeshProUGUI upgradeDescriptionText;

    public void Set(UpgradeData upgradeData)
    {
        upgradeNameText.text = upgradeData.Name;
        upgradeDescriptionText.text = upgradeData.Description;
    }
}
