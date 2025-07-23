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
    [SerializeField] private float projectileSpeed = 10f;

    [SerializeField] private float fireCooldown = 1f; //seconds between shots
    private float lastFireTime = -Mathf.Infinity;     //time the last shot was fired

    public List<Star> trackingStars;
    private Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = null;
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


    private void RotateGun()
    {
        if (target != null)
        {
            float angle = MathF.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            rotationPoint.rotation = Quaternion.RotateTowards(rotationPoint.rotation, targetRotation, 200f*Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Star"))
        {
            Star star = collision.GetComponent<Star>();
            trackingStars.Add(star);
        }
    }

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
