using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private Gun equippedGun;
    private bool isFiring;
    private void Start()
    {
        isFiring = false;
    }

    private void Update()
    {
        if (isFiring)
        {
            equippedGun.AttemptFire();
        }
    }

    private void OnShoot(InputValue value)
    {

        isFiring = value.isPressed;
    }

    private void OnReload()
    {
        equippedGun.Reload();
    }
}
