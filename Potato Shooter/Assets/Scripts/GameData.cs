using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    public Gun selectedGun;
    public bool hasCompleted;
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
}
