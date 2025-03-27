using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private float movementSpeed = 1;

    Vector2 movement;

    void Update()
    {
        float xOffset = movement.x * movementSpeed * Time.deltaTime;
        float yOffset = movement.y * movementSpeed * Time.deltaTime;
        transform.localPosition = 
        new Vector2(
        transform.localPosition.x + xOffset, 
        transform.localPosition.y + yOffset);
    }

    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    public void OnSprint(InputValue value)
    {
        if(value.isPressed)
        {
            movementSpeed *= 1.5f;
        }
        else
        {
            movementSpeed /= 1.5f;
        }
        
    }
}
