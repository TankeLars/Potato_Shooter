using UnityEngine;

public class Gun : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 mousePosition;
    public GameObject bulletPrefab;  // Bullet prefab to instantiate when firing
    public Transform bulletSpawn;    // Position from where the bullets are spawned
    public bool canFire = true;      // Can the gun currently fire?
    private bool isReloading = false; // Is the gun currently reloading?

    public int maxAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 2f;
    private float reloadTimer = 0f;
    private float shotTimer = 0f;
    public float timeBetweenShots = 0.2f;
    public GameObject gunModelPrefab;

    void Start()
    {
        currentAmmo = maxAmmo;  // Initialize ammo at the start
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePosition - transform.position;

        float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;


        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        // Reload logic
        if(currentAmmo == maxAmmo)
        {
            isReloading = false;
        }
        if (isReloading)
        {
            Debug.Log("currently reloading");
            reloadTimer += Time.deltaTime;
            if (reloadTimer >= reloadTime)
            {
                currentAmmo = maxAmmo;
                isReloading = false;
                reloadTimer = 0f;
            }
        }

        // Fire cooldown logic
        if (!canFire)
        {
            shotTimer += Time.deltaTime;
            if (shotTimer >= timeBetweenShots)
            {
                canFire = true;
                shotTimer = 0f;
            }
        }
    }

    public bool AttemptFire()
    {
        if (currentAmmo == 0)
        {
            Debug.Log("no ammo");
            Reload();  
            return false;
        }
        Debug.Log(isReloading);
        if (canFire && !isReloading)
        {
            Debug.Log("weeeee");
            Fire();
            currentAmmo--;
            canFire = false;
            return true;
        }
        Debug.Log("nothing happened");
        return false;
    }

    private void Fire()
    {
        Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
    }

    public void Reload()
    {
        if (!isReloading && currentAmmo < maxAmmo)
        {
            isReloading = true;  // Start the reload process
        }
    }
}
