using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeScreen : MonoBehaviour
{
    [SerializeField] 
    private Button startButton;

    public void OnStart()
    {
        SceneManager.LoadScene(0);
    }
}
