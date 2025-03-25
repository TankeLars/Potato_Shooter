using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 mousePosition;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    private bool isReloading;

    private float reloadTimer;
    private float shotTimer;
    [SerializeField]
    private GameObject bulletSpawn;
    [SerializeField]
    private float timeBetweenFiring;
    [SerializeField]
    private float reloadTime;
    public float damage;
    public int maxAmmo;
    public int currentAmmo;
    void Start()
    {
        currentAmmo = maxAmmo;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePosition - transform.position;

        float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        if (!canFire)
        {
            shotTimer += Time.deltaTime;
            if (shotTimer > timeBetweenFiring)
            {
                canFire = true;
                shotTimer = 0;
            }
        }
        if (isReloading)
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer > reloadTime)
            {
                currentAmmo = maxAmmo;
                isReloading = false;
                reloadTimer = 0;
            }
        }

    }

    public bool AttemptFire()
    {
        if (currentAmmo == 0)
        {
            Reload();
            return false;
        }
        if (canFire && currentAmmo > 0 && !isReloading)
        {
            Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
            currentAmmo--;
            canFire = false;
            return true;
        }
        return false;
    }

    public void Reload()
    {
        if(!isReloading && currentAmmo != maxAmmo){
            isReloading = true;
        }
    }
}
