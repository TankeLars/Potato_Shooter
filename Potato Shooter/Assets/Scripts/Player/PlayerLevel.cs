using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public static PlayerLevel Instance { get; private set; }

    public int level;
    public int currentXP;
    public int xpToNextLevel;

    [SerializeField] private int baseXP = 100;
    [SerializeField] private float xpMultiplier = 1.5f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    void Start()
    {
        level = 1;
        currentXP = 0;
        xpToNextLevel = CalculateXPForNextLevel(level);
    }


    public void AddXP(int amount)
    {
        currentXP += amount;
        while (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        GameManager.Instance.ShowUpgradeScreen();
        xpToNextLevel = CalculateXPForNextLevel(level);
        gameObject.GetComponent<ZombieSpawner>().IncreaseDifficulty();
    }

    private int CalculateXPForNextLevel(int currentLevel)
    {
        return Mathf.FloorToInt(baseXP * Mathf.Pow(xpMultiplier, currentLevel - 1));
    }
    public float GetPercentageToNextLevel()
    {
        // currentXP is 10
        //  xpTpNextLevel is 100
        // percentage should be 0.10, but it is not working

        float percentage = (float)currentXP / xpToNextLevel;
        //Debug.Log(percentage);
        return percentage;
    }

    public int GetLevel() => level;
    public int GetCurrentXP() => currentXP;
    public int GetXPToNextLevel() => xpToNextLevel;
}
