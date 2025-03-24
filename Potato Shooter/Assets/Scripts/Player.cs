using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float movementSpeed = 1;
    void Start()
    {
        
    }

    void Update()
    {
        float xPosition = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        float yPosition = Input.GetAxis("Vertical") * Time.deltaTime *movementSpeed;
        transform.Translate(xPosition, yPosition, 0);
    }
}
