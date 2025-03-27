using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 7.5f;
    [SerializeField] private float dashSpeed = 40f;
    [SerializeField] private float dashDuration = 0.1f;
    [SerializeField] private float dashCooldownTime = 5f;

    private enum DashState { Ready, Dashing, Cooldown }
    private DashState dashState = DashState.Ready;

    private float dashTimer = 0f;
    private float cooldownTimer = 0f;

    private PlayerShoot playerShoot;

    private Vector2 movement;

    private void Start()
    {
        playerShoot = GetComponent<PlayerShoot>();
    }

    void Update()
    {
        HandleMovement();
        HandleDash();
    }

    private void HandleMovement()
    {
        float xOffset = movement.x * movementSpeed * Time.deltaTime;
        float yOffset = movement.y * movementSpeed * Time.deltaTime;
        transform.localPosition += new Vector3(xOffset, yOffset, 0);
    }

    private void HandleDash()
    {
        if (dashState == DashState.Dashing)
        {
            dashTimer += Time.deltaTime;
            if (dashTimer >= dashDuration)
            {
                dashState = DashState.Cooldown;
                dashTimer = 0f;
                cooldownTimer = dashCooldownTime;
                movementSpeed = 7.5f;
                playerShoot.toggleOnFireAbility();
            }
        }
        else if (dashState == DashState.Cooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                dashState = DashState.Ready;
            }
        }
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

    public void OnDash()
    {
        if (dashState == DashState.Ready)
        {
            dashState = DashState.Dashing;
            movementSpeed = dashSpeed;
            playerShoot.toggleOffFireAbility(); 
        }
    }

    public float GetDashCooldown()
    {
        return dashState == DashState.Cooldown ? Mathf.Round(cooldownTimer * 10f) / 10f : 0f;
    }
}
