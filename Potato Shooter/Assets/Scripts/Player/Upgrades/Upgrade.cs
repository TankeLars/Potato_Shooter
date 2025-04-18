using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/Upgrade")]
public class Upgrade : ScriptableObject
{
    public StatType statType;
    public float value;

    public string title;

    [TextArea]
    public string description;
}
