using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicTower : MonoBehaviour
{

    public Transform rotationPoint;
    public List<Star> trackingStars;
    private Star target;

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
    }

    private void FindNextStar()
    {
        if (trackingStars.Count() <= 0)
        {
            target = null;
        }
        else
        {
            target = trackingStars[0];
        }
        return;
    }

    private void RotateGun()
    {
        if (target != null)
        {
            float angle = MathF.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            rotationPoint.rotation = targetRotation;
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
