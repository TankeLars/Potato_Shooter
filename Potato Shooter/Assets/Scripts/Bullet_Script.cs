using UnityEngine;

public class Bullet_Script : MonoBehaviour
{
    private Vector3 mousePosition;
    private Camera mainCamera;
    private Rigidbody2D rb;
    public float force;
    private float timePassed = 0;
    public float maxTime = 1;
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        Vector3 rotation = transform.position - mousePosition;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > maxTime)
        {
            Destroy(gameObject);
        }
        
    }
}
