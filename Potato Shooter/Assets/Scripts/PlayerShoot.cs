using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] public int equippedGun;  // Current equipped gun index
    private bool isFiring;
    private bool isReloading;

    public Gun[] guns;  // Array to hold references to gun scripts

    private void Start()
    {
        isFiring = false;
        isReloading = false;

        // Assuming each child has a Gun component attached to it
        guns = new Gun[transform.childCount];

        // Assign the gun components to the array
        for (int i = 0; i < transform.childCount; i++)
        {
            guns[i] = transform.GetChild(i).GetComponent<Gun>();
        }

        EquipGun();  // Equip the starting gun
    }

    private void Update()
    {
        if (isFiring && guns[equippedGun] != null)
        {
            Debug.Log("Pulling trigger");
            guns[equippedGun].AttemptFire();  // Call fire on the equipped gun
        }
        if (isReloading && guns[equippedGun] != null)
        {
            guns[equippedGun].Reload();  // Call reload on the equipped gun
        }
    }

    private void OnShoot(InputValue value)
    {
        isFiring = value.isPressed;
    }

    private void OnReload(InputValue value)
    {
        isReloading = value.isPressed;
    }

    private void OnSwitchWeapon()
    {
        int previousGun = equippedGun;

        if (equippedGun >= guns.Length - 1)
        {
            equippedGun = 0;
        }
        else
        {
            equippedGun++;
        }

        if (previousGun != equippedGun)
        {
            EquipGun();  // Equip the new selected gun
        }
    }

    private void EquipGun()
    {
        // Deactivate all guns first
        foreach (var gun in guns)
        {
            gun.gameObject.SetActive(false);
        }

        // Activate the currently equipped gun
        if (equippedGun >= 0 && equippedGun < guns.Length)
        {
            guns[equippedGun].gameObject.SetActive(true);
        }
    }
}
