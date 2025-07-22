using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    private float projectileSpeed = 10f;

    //camera
    [SerializeField] private Camera mainCam;
    //clamp rotation
    private float minRotation = -90f;
    private float maxRotation = 90f;

    //bullet lifetime
    [SerializeField] private float lifeTime = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //destroy projectile after time
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        //get mouse position in world space
        Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        //get direction/angle
        Vector3 dir = mouseWorldPos - transform.position;
        //calc angle in degrees
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f; //points up instead of right (-90f)

        //clamp rotation
        float clampedAngle = Mathf.Clamp(angle, minRotation, maxRotation);

        //apply rotation
        transform.rotation = Quaternion.Euler(0, 0, clampedAngle);

        //mouseclick shooting
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 direction = (mouseWorldPos - firePoint.position).normalized;

            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.freezeRotation = true;
            rb.linearVelocity = direction * projectileSpeed;

        }
    }
}
