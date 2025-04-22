using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private CanvasSwitcher canvasSwitcher;
    [SerializeField]
    private int minutes;
    [SerializeField]
    private float seconds;
    
    private float totalTimeInSeconds;
    private float currentTime;
    private bool isTimerRunning = false;
    private bool isPlayerDead = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //Debug.Log(GameData.Instance.selectedGun.name);
        canvasSwitcher = FindFirstObjectByType<CanvasSwitcher>();
        StartTimer();
    }

    private void Update()
    {
        if (isTimerRunning && !isPlayerDead)
        {
            currentTime -= Time.deltaTime;
            
            if (currentTime <= 0)
            {
                currentTime = 0;
                isTimerRunning = false;
                ShowWinScreen();
            }
            
            // Update minutes and seconds for display purposes
            minutes = Mathf.FloorToInt(currentTime / 60);
            seconds = currentTime % 60;
        }
    }
    public (int minutes, int seconds) GetCurrentTime()
    {
        var (mins, secs) =  (Mathf.FloorToInt(currentTime / 60), currentTime % 60);
        int roundedSecs = (int)Mathf.Round(secs);
        return (mins, roundedSecs);
    }

    public void StartTimer()
    {
        totalTimeInSeconds = minutes * 60 + seconds;
        currentTime = totalTimeInSeconds;
        isTimerRunning = true;
        isPlayerDead = false; // Reset player state when timer starts
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    public void ShowDeathScreen()
    {
        isPlayerDead = true;
        StopTimer();
        if (canvasSwitcher != null)
        {
            canvasSwitcher.ShowDeathScreen();
        }
        else
        {
            Debug.LogWarning("CanvasSwitcher instance not found!");
        }
    }

    public void ShowWinScreen()
    {
        if (isPlayerDead) return; // Don't show win screen if player is dead
        
        StopTimer();
        if (canvasSwitcher != null)
        {
            Time.timeScale = 0;
            canvasSwitcher.ShowWinScreen();
        }
        else
        {
            Debug.LogWarning("CanvasSwitcher instance not found!");
        }
    }
    public void ShowUpgradeScreen()
    {
        Time.timeScale = 0;
        canvasSwitcher.ShowUpgradeScreen();
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        canvasSwitcher.ShowinGameHUD();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitGame()
    {
        SceneManager.LoadScene(1);
    }

    // Helper method to check if timer is running
    public bool IsTimerRunning()
    {
        return isTimerRunning && !isPlayerDead;
    }
}