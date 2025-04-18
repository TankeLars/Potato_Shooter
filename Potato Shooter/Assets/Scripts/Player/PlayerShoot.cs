using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] public Gun equippedGun; // Now, it's a Gun reference, not an index
    private bool isFiring;
    private bool isReloading;
    private bool canFire;
    public int bonusPotatoes;
    [SerializeField]
    private int potatoes;
    public int Potatoes
    {
        get { return potatoes; }
        set { potatoes = value; }   
    }



    private void Start()
    {
        isFiring = false;
        isReloading = false;
        canFire = true;


        // Instantiate the gun stored in GameData.Instance.selectedGun if it's not null
        if (GameData.Instance.selectedGun != null)
        {
            equippedGun = Instantiate(GameData.Instance.selectedGun, transform);  // Instantiate the selected gun under the player
            EquipGun();  // Equip the instantiated gun
        }
        else
        {
            Debug.LogError("No selected gun in GameData.");
        }
    }

    private void Update()
    {
        if (isFiring && equippedGun != null && canFire)
        {
            equippedGun.AttemptFire(); 
        }
        if (isReloading && equippedGun != null && equippedGun.isReloading == false)
        {
            if (potatoes > 0)
            {
                equippedGun.Reload();
                isReloading = false; 
                potatoes--;
            }
            else
            {
                // Handle no ammo case here
            }
        }
    }

    public void toggleOnFireAbility()
    {
        canFire = true;
    }

    public void toggleOffFireAbility()
    {
        canFire = false;
    }

    private void OnShoot(InputValue value)
    {
        isFiring = value.isPressed;
    }

    private void OnReload(InputValue value)
    {
        isReloading = value.isPressed;
    }

    // Removed the OnSwitchWeapon method as we no longer switch guns
    // Removed OnScrollWeapon and SwitchWeapon methods as they are related to switching guns

    private void EquipGun()
    {

        // Activate the currently equipped gun if it's valid
        if (equippedGun != null)
        {
            equippedGun.gameObject.SetActive(true);
        }
    }

    public void PotatoPickup(int amount)
    {
        amount += bonusPotatoes;
        potatoes += amount;
    }
}
