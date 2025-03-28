using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas1;
    [SerializeField] private Canvas canvas2;

    void Start()
    {

        EnsureCanvasGroup(canvas1);
        EnsureCanvasGroup(canvas2);
        ShowCanvas1(); 
    }

    public void ShowCanvas1()
    {
        canvas1.gameObject.SetActive(true);
        canvas2.gameObject.SetActive(false);
    }

    public void ShowCanvas2()
    {
        canvas1.gameObject.SetActive(false);
        canvas2.gameObject.SetActive(true);
    }

    private void EnsureCanvasGroup(Canvas canvas)
    {
        if (canvas.GetComponent<CanvasGroup>() == null)
        {
            canvas.gameObject.AddComponent<CanvasGroup>();
        }
    }
}

