using UnityEngine;

public class Bullet_Script : MonoBehaviour
{
    public LayerMask collisionLayer;
    private Vector3 mousePosition;
    private Camera mainCamera;
    private Rigidbody2D rb;
    public float force;  
    private float timePassed = 0;
    public float maxTime = 1;

    public float potatoDamage; 
    public float angle;  

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mousePosition - transform.position;
        direction = Quaternion.Euler(0, 0, angle) * direction; 
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * force;  
        
        float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90); 
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > maxTime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collisionLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.DoDamage(potatoDamage);
            }

            Destroy(gameObject);
        }
    }

}
