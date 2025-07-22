using UnityEngine;

public class BulletScript : MonoBehaviour
{
    //bullet lifetime
    //[SerializeField] private float lifeTime = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //destroy projectile after time
        //Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    /*private void OnDestroy()
    {
        // Inform tower to allow another shot
        PlayerScript player = FindObjectOfType<PlayerScript>();
        if (player != null)
        {
            player.OnBulletDestroyed();
        }
    }*/
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
