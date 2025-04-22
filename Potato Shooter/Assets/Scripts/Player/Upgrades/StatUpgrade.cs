using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatUpgrades", menuName = "Upgrades/StatUpgradeList")]
public class StatUpgradeList : ScriptableObject
{
    public List<Upgrade> upgrades;

}
