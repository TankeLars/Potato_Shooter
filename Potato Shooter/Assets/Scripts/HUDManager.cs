using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI weaponsInfo;
    public TextMeshProUGUI dashCooldownInfo;
    private PlayerShoot playerShoot;
    private PlayerMovement playerMovement;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerShoot = player.GetComponent<PlayerShoot>();
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    void Update()
    {
        if (playerShoot != null && playerShoot.equippedGun >= 0 && playerShoot.equippedGun < playerShoot.guns.Length)
        {
            Gun gun = playerShoot.guns[playerShoot.equippedGun]; // Access the equipped gun
            weaponsInfo.text = $"{gun.name}\nAmmo: {gun.currentAmmo}/{gun.maxAmmo}";
        }
        else
        {
            weaponsInfo.text = "No Weapon Equipped";
        }
        float dashCooldown = playerMovement.GetDashCooldown();
        if(dashCooldown == 0f)
        {
            dashCooldownInfo.text = "Dash \nReady";
        }
        else
        {
            dashCooldownInfo.text = $"Dash\n {dashCooldown}";
        }
    }
}
