using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] public int equippedGun; 
    private bool isFiring;
    private bool isReloading;

    public Gun[] guns; 

    private void Start()
    {
        isFiring = false;
        isReloading = false;


        guns = new Gun[transform.childCount];


        for (int i = 0; i < transform.childCount; i++)
        {
            guns[i] = transform.GetChild(i).GetComponent<Gun>();
        }

        EquipGun();
    }

    private void Update()
    {
        if (isFiring && guns[equippedGun] != null)
        {
            guns[equippedGun].AttemptFire(); 
        }
        if (isReloading && guns[equippedGun] != null)
        {
            guns[equippedGun].Reload();
            isReloading = false; 
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
            EquipGun(); 
        }
    }

    private void EquipGun()
    {

        foreach (var gun in guns)
        {
            gun.gameObject.SetActive(false);
        }


        if (equippedGun >= 0 && equippedGun < guns.Length)
        {
            guns[equippedGun].gameObject.SetActive(true);
        }
    }
}
