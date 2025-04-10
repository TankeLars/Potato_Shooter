using UnityEngine;

public class CanvasSwitcher : MonoBehaviour
{
    [SerializeField] private Canvas inGameHUD;
    [SerializeField] private Canvas deathScreen;
    [SerializeField] private Canvas winScreen;



    void Start()
    {
        EnsureCanvasGroup(inGameHUD);
        EnsureCanvasGroup(deathScreen);
        EnsureCanvasGroup(winScreen);
        ShowinGameHUD();
    }

    public void ShowinGameHUD()
    {
        inGameHUD.gameObject.SetActive(true);
        deathScreen.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(false);
    }

    public void ShowDeathScreen()
    {
        inGameHUD.gameObject.SetActive(false);
        deathScreen.gameObject.SetActive(true);
        winScreen.gameObject.SetActive(false);
    }

        public void ShowWinScreen()
    {
        inGameHUD.gameObject.SetActive(false);
        deathScreen.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(true);
    }

    private void EnsureCanvasGroup(Canvas canvas)
    {
        if (canvas.GetComponent<CanvasGroup>() == null)
        {
            canvas.gameObject.AddComponent<CanvasGroup>();
        }
    }
}
