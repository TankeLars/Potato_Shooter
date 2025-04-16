using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set; }

    public TextMeshProUGUI weaponsInfo;
    public TextMeshProUGUI dashCooldownInfo;
    public TextMeshProUGUI healthInfo;
    public TextMeshProUGUI levelInfo;
    public TextMeshProUGUI timer;
    public Image redCircle;
    public Image xpBar;

    private PlayerShoot playerShoot;
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;
    private PlayerLevel playerLevel;

    void Awake()
    {
        // Set up Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerShoot = player.GetComponent<PlayerShoot>();
            playerMovement = player.GetComponent<PlayerMovement>();
            playerHealth = player.GetComponent<PlayerHealth>();
            playerLevel = player.GetComponent<PlayerLevel>();
        }

        if (redCircle != null)
        {
            redCircle.gameObject.SetActive(false);
            redCircle.fillAmount = 0f;
        }
    }

    void Update()
    {
        if (playerShoot != null && playerShoot.equippedGun >= 0 && playerShoot.equippedGun < playerShoot.guns.Length)
        {
            Gun gun = playerShoot.guns[playerShoot.equippedGun];
            weaponsInfo.text = $"{gun.name}\nPotatoes: {gun.currentAmmo}/{gun.maxAmmo} \n{playerShoot.Potatoes} Magazines";
        }
        else
        {
            weaponsInfo.text = "No Weapon Equipped";
        }


        float dashCooldown = playerMovement.GetDashCooldown();
        dashCooldownInfo.text = dashCooldown == 0f ? "Dash \nReady" : $"Dash\n {dashCooldown}";

        float currentHealth = playerHealth.getHealth();
        healthInfo.text = $"{currentHealth} HP";
        int level = playerLevel.GetLevel();
        levelInfo.text = $"Level {level}";

        float xpPercentage = playerLevel.GetPercentageToNextLevel();
        Debug.Log(xpPercentage);
        xpBar.fillAmount = xpPercentage; 

        var (mins, secs) = GameManager.Instance.GetCurrentTime();
        timer.text = $"{mins}:{secs}";
    }

    public void Reload(float duration)
    {
        if (redCircle != null)
        {
            StopAllCoroutines();
            StartCoroutine(PlayRedCircleAnimation(duration));
        }
    }


    private IEnumerator PlayRedCircleAnimation(float duration)
    {
        redCircle.gameObject.SetActive(true);
        redCircle.fillAmount = 0f;

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            redCircle.fillAmount = Mathf.Clamp01(timer / duration);
            yield return null;
        }

        redCircle.gameObject.SetActive(false);
    }
}

