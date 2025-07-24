using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicTower : MonoBehaviour
{

    public Transform rotationPoint;

    //bullet vars stolen from Lia
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] public float projectileSpeed = 10f;
    [SerializeField] public float fireCooldown = 1f; //seconds between shots
    private float lastFireTime = -Mathf.Infinity;     //time the last shot was fired

    [SerializeField] private CircleCollider2D detectionCollider;
    [SerializeField] public float radius = 5f; //tower circle collider size
    [SerializeField] public float speed = 1f; //speed modifier, which decreases cooldown
    [SerializeField] public float damage = 20f; //damage of each bullet

    public List<Star> trackingStars; //list of currently tracked stars
    private Transform target; //current star being tracked


    void Start()
    {
        target = null;
        detectionCollider = GetComponent<CircleCollider2D>();
        
        if (detectionCollider != null)
        {
            detectionCollider.radius = radius;
        }
        else
        {
            Debug.LogError("CircleCollider not found on this GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        FindNextStar();
        RotateGun();

        if (target != null && Time.time >= lastFireTime + fireCooldown)
        {
            float angle = MathF.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            GameObject bullet = Instantiate(projectilePrefab, rotationPoint.position, targetRotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.freezeRotation = true;
            rb.linearVelocity = target.transform.position * projectileSpeed;
            lastFireTime = Time.time;
        }
    }

    //finds the closest star within range
    private void FindNextStar()
    {
        if (trackingStars.Count() <= 0)
        {
            target = null;
            trackingStars.Clear();
        }
        else
        {
            target = trackingStars[0].transform;
        }
        return;
    }

    //rotates tower to be facing star
    private void RotateGun()
    {
        if (target != null)
        {
            float angle = MathF.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            rotationPoint.rotation = Quaternion.RotateTowards(rotationPoint.rotation, targetRotation, 200f*Time.deltaTime);
        }
    }

    //when a star enters the trigger collider, add it to the list of stars being tracked
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Star"))
        {
            Star star = collision.GetComponent<Star>();
            trackingStars.Add(star);
        }
    }

    //when a star exists the trigger collider, remove it from the list of stars being tracked
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Star"))
        {
            Star star = collision.GetComponent<Star>();
            if (trackingStars.Contains(star))
            {
                trackingStars.Remove(star);
            }
        }
    }
}
