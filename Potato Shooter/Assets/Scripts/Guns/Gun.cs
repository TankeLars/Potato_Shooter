using UnityEngine;

public class Gun : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 mousePosition;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public bool canFire = true;
    public bool isReloading = false;

    public int maxAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 2f;
    private float reloadTimer = 0f;
    private float shotTimer = 0f;
    public float timeBetweenShots = 0.2f;

    public int pierceAmount;
    public bool singleShot;
    public int bulletAmount;

    public float spreadAngle = 10f;  
    public float potatoDamage; 
    public float bulletForce = 10f; 

    public AudioClip shootSound;
    public AudioClip reloadSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentAmmo = maxAmmo;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePosition - transform.position;

        float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        // Reload logic
        if (currentAmmo == maxAmmo)
        {
            isReloading = false;
        }

        if (isReloading)
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer >= reloadTime)
            {
                currentAmmo = maxAmmo;
                isReloading = false;
                reloadTimer = 0f;
            }
        }

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
            return false;
        }

        if (canFire && !isReloading)
        {
            Fire();
            currentAmmo--;
            canFire = false;
            return true;
        }
        return false;
    }

    private void Fire()
    {
        if (bulletAmount == 1)
        {
            InstantiateBullet(0f);
        }
        else
        {

            float angleStep = spreadAngle / (bulletAmount - 1);

            for (int i = 0; i < bulletAmount; i++)
            {
                float angleOffset = (i - (bulletAmount / 2)) * angleStep; 
                InstantiateBullet(angleOffset);
            }
        }
        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    private void InstantiateBullet(float angleOffset)
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        // Get the Bullet_Script attached to the instantiated bullet
        Bullet_Script bulletScript = bullet.GetComponent<Bullet_Script>();
        
        if (bulletScript != null)
        {
            bulletScript.pierceAmount = pierceAmount;
            bulletScript.angle = angleOffset;       
            bulletScript.potatoDamage = potatoDamage;     
            bulletScript.force = bulletForce;       
        }
    }

    public void Reload()
    {
        if (!isReloading && currentAmmo < maxAmmo)
        {
            isReloading = true;
            HUDManager.Instance.Reload(reloadTime);
            if (reloadSound != null)
            {
                audioSource.PlayOneShot(reloadSound);
            }
        }
    }
}