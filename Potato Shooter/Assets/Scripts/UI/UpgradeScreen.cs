using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;  // Include this for Button

public class UpgradeScreen : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI title1;
    public TextMeshProUGUI desc1;
    public Button button1; 
    public TextMeshProUGUI title2;
    public TextMeshProUGUI desc2;
    public Button button2; 
    public TextMeshProUGUI title3;
    public TextMeshProUGUI desc3;
    public Button button3; 

    private List<Upgrade> upgradeChoices;

    void OnEnable()
    {
        upgradeChoices = UpgradeManager.Instance.Get3RandomUpgrades();

        if (upgradeChoices.Count >= 3)
        {
            title1.text = upgradeChoices[0].title;
            desc1.text = upgradeChoices[0].description;
            button1.onClick.RemoveAllListeners();
            button1.onClick.AddListener(() => Choice(upgradeChoices[0]));  // Add button listener

            title2.text = upgradeChoices[1].title;
            desc2.text = upgradeChoices[1].description;
            button2.onClick.RemoveAllListeners();
            button2.onClick.AddListener(() => Choice(upgradeChoices[1]));  // Add button listener

            title3.text = upgradeChoices[2].title;
            desc3.text = upgradeChoices[2].description;
            button3.onClick.RemoveAllListeners();
            button3.onClick.AddListener(() => Choice(upgradeChoices[2]));  // Add button listener
        }
        else
        {
            Debug.LogWarning("Not enough upgrades to display.");
        }
    }

    public void Choice(Upgrade choice)
    {
        UpgradeManager.Instance.ApplyUpgrade(choice);
        GameManager.Instance.ContinueGame();
    }
}
