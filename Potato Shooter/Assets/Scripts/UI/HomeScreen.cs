using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class HomeScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject start;
    [SerializeField]
    private GameObject chooseWeapons;
    

    void Start()
    {
        ResetUI();
        GameData.Instance.Reset();
    }

    public void OnStart()
    {
        start.SetActive(false);
        chooseWeapons.SetActive(true);
    }

    public void OnWeaponSelection(Gun selectedGun)
    {
        GameData.Instance.selectedGun = selectedGun;
        SceneManager.LoadScene(0);
    }

    public void ResetUI()
    {
        start.SetActive(true);
        chooseWeapons.SetActive(false);
        StopAllCoroutines();
    }

}
