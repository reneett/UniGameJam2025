using UnityEngine;

public class RangeDetection : MonoBehaviour
{
    [SerializeField] private BasicTower tower;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    //when a star enters the trigger collider, add it to the list of stars being tracked
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Star"))
        {
            Star star = collision.GetComponent<Star>();
            tower.trackingStars.Add(star);
        }
    }

    //when a star exists the trigger collider, remove it from the list of stars being tracked
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Star"))
        {
            Star star = collision.GetComponent<Star>();
            if (tower.trackingStars.Contains(star))
            {
                tower.trackingStars.Remove(star);
            }
        }
    }
}
