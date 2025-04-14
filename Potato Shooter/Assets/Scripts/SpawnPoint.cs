using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool HasPotato { get; private set; }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Potato"))
        {
            HasPotato = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Potato"))
        {
            HasPotato = false;
        }
    }

}