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
    private AudioSource audioSource;
    

    void Start()
    {
        ResetUI();
        audioSource = GetComponent<AudioSource>();
    }

    public void OnStart()
    {
        start.SetActive(false);
        chooseWeapons.SetActive(true);
        audioSource.Play();
    }

    public void OnWeaponSelection(Gun selectedGun)
    {
        GameData.Instance.selectedGun = selectedGun;
        SceneManager.LoadScene(0);
        audioSource.Play();

    }

    public void ResetUI()
    {
        start.SetActive(true);
        chooseWeapons.SetActive(false);
        StopAllCoroutines();

    }

}
