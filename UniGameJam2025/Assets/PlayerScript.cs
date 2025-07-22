using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Camera mainCam;

    //clamp rotation
    private float minRotation = -90f;
    private float maxRotation = 90f;
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
    }
}
