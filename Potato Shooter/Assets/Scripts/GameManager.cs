using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private CanvasSwitcher canvasSwitcher;

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
        canvasSwitcher = FindFirstObjectByType<CanvasSwitcher>();
    }

    public void ShowDeathScreen()
    {
        if (canvasSwitcher != null)
        {
            canvasSwitcher.ShowCanvas2();
        }
        else
        {
            Debug.LogWarning("CanvasSwitcher instance not found!");
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
