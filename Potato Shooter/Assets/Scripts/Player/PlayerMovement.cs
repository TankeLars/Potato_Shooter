using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 7.5f;
    [SerializeField] private float dashSpeed = 40f;
    [SerializeField] private float dashDuration = 0.1f;
    [SerializeField] private float dashCooldownTime = 5f;
    [SerializeField] public GameObject cinemachineCamera;

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
        CheckForTeleport();
    }

    private void HandleMovement()
    {
        float xOffset = movement.x * movementSpeed * Time.deltaTime;
        float yOffset = movement.y * movementSpeed * Time.deltaTime;
        transform.localPosition += new Vector3(xOffset, yOffset, 0);
    }

    private void CheckForTeleport()
    {         
        if (transform.position.x > 50)
        {
            transform.position = new Vector3(-48, transform.position.y, 0);
/*            cinemachineCamera.GetComponent<CinemachineCamera>().enabled = false;
            cinemachineCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -5.43f);
            cinemachineCamera.GetComponent<CinemachineCamera>().enabled = true;*/

        }
        else if (transform.position.x < -48)
        {
            transform.position = new Vector3(50, transform.localPosition.y, 0);
/*            cinemachineCamera.GetComponent<CinemachineFollow>().enabled = false;
            cinemachineCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -5.43f);
            cinemachineCamera.GetComponent<CinemachineCamera>().enabled = true;*/

        }

        if (transform.position.y > 50)
        {
            transform.position = new Vector3(transform.position.x, -26, 0);
/*            cinemachineCamera.GetComponent<CinemachineCamera>().enabled = false;
            cinemachineCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -5.43f);
            cinemachineCamera.GetComponent<CinemachineCamera>().enabled = true;*/

        }
        else if (transform.position.y < -26)
        {
            transform.position = new Vector3(transform.localPosition.x, 50, 0);
/*            cinemachineCamera.GetComponent<CinemachineFollow>().enabled = false;
            cinemachineCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -5.43f);
            cinemachineCamera.GetComponent<CinemachineCamera>().enabled = true;*/

        }
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
