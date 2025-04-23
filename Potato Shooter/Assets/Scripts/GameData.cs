using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    public Gun selectedGun;
    public bool isDead;
    public bool hasCompleted;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Reset();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Reset()
    {
        
        {
            isDead = false;
            hasCompleted = false;
        }
    }
}
