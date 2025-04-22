using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class UpgradeManager : MonoBehaviour
{

    public static UpgradeManager Instance { get; private set; }
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerShoot playerShoot;
    [SerializeField] private StatUpgradeList statUpgradeList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ApplyUpgrade(Upgrade upgrade)
    {
        switch (upgrade.statType)
        {
            case StatType.DamageMultiplier:
                playerShoot.equippedGun.potatoDamage *= upgrade.value;
                break;
            case StatType.MovementSpeedMultiplier:
                playerMovement.ApplySpeedMultiplier(upgrade.value);
                break;
            case StatType.DashCooldownReduction:
                playerMovement.ApplyDashCooldownReduction(upgrade.value);
                break;
            case StatType.FireRateReduction:
                playerShoot.equippedGun.timeBetweenShots *= upgrade.value;
                break;
            case StatType.BonusHealth:
                playerHealth.AddBonusHealth((int)upgrade.value);
                break;
            case StatType.BonusAmmo:
                playerShoot.equippedGun.maxAmmo += (int)upgrade.value;
                playerShoot.equippedGun.currentAmmo += (int)upgrade.value;
                break;
            case StatType.BonusBullets:
                playerShoot.equippedGun.bulletAmount += (int)upgrade.value;
                break;
            case StatType.BonusPotatoPickup:
                playerShoot.bonusPotatoes += (int)upgrade.value;
                break;
        }

        Debug.Log($"Applied upgrade: {upgrade.name} - {upgrade.description}");
    }

    public List<Upgrade> Get3RandomUpgrades()
    {
        List<Upgrade> unshuffledUpgrades = new List<Upgrade>(statUpgradeList.upgrades);
        
        for (int i = unshuffledUpgrades.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Upgrade temp = unshuffledUpgrades[i];
            unshuffledUpgrades[i] = unshuffledUpgrades[randomIndex];
            unshuffledUpgrades[randomIndex] = temp;
        }
        return unshuffledUpgrades.Take(3).ToList();
    }
}
