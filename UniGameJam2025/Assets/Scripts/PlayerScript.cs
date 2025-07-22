using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Camera mainCam;

    //clamp rotation
    private float minRotation = -90f;
    private float maxRotation = 90f;

    //bullet vars
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 10f;

    [SerializeField] private float fireCooldown = 1f; //seconds between shots
    private float lastFireTime = -Mathf.Infinity;     //time the last shot was fired

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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

        //bullet shoot
        if (Input.GetMouseButtonDown(0) && Time.time >= lastFireTime + fireCooldown)
        {
            Vector3 direction = (mouseWorldPos - firePoint.position).normalized;

            //bullet angle
            float bulletAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion bulletRotation = Quaternion.Euler(0, 0, bulletAngle);

            GameObject bullet = Instantiate(projectilePrefab, firePoint.position, bulletRotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.freezeRotation = true;
            rb.linearVelocity = direction * projectileSpeed;
        }
    }

}