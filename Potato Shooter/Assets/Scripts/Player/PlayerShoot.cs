using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] public int equippedGun; 
    private bool isFiring;
    private bool isReloading;
    private bool canFire;
    [SerializeField]
    private int potatoes;
    public int Potatoes
    {
        get {return potatoes;}
        set {potatoes = value;}   
    }
    public Gun[] guns; 


    private void Start()
    {
        isFiring = false;
        isReloading = false;
        canFire = true;
        guns = new Gun[transform.childCount];


        for (int i = 0; i < transform.childCount; i++)
        {
            guns[i] = transform.GetChild(i).GetComponent<Gun>();
        }

        EquipGun();
    }

    private void Update()
    {
        if (isFiring && guns[equippedGun] != null && canFire)
        {
            guns[equippedGun].AttemptFire(); 
        }
        if (isReloading && guns[equippedGun].isReloading == false)
        {
            if(potatoes > 0)
            {
                guns[equippedGun].Reload();
                isReloading = false; 
                potatoes--;
            }
            else
            {
                //no ammo thing
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

    private void OnSwitchWeapon()
    {
        SwitchWeapon(true);
    }
    void OnScrollWeapon(InputValue value)
    {
        float direction = value.Get<float>();
        if(direction == 1)
        {
            SwitchWeapon(true);
        }
        else if (direction == -1)
        {
            SwitchWeapon(false);
        }
    }
    private void SwitchWeapon(bool direction)
    {
        int previousGun = equippedGun;
        if(direction)
        {
            if (equippedGun >= guns.Length - 1)
            {
                equippedGun = 0;
            }
            else
            {
                equippedGun++;
            }
        }
        else
        {
            if (equippedGun <= 0)
            {
                equippedGun = guns.Length - 1;
            }
            else 
            {
                equippedGun--;
            }
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

    public void PotatoPickup(int amount)
    {
        potatoes += amount;
    }
}
