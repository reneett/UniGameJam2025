using System.Collections;
using UnityEngine;

public class Ice : Star
{
    public GameObject frostWavePrefab;
    public Collider2D detectionCollider;
    public int radius;
    public float cooldown = 5f;
    public bool canFire = true;
    public float frostMod = 2f;
    public float frostDuration = 5f;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.CompareTag("Tower"))
        {
            Debug.Log("Proc Frost");
            BasicTower tower = collision.GetComponent<BasicTower>();
            if (tower != null && canFire)
            {
                tower.StackFrost(frostDuration, frostMod);
                GameObject wave = Instantiate(frostWavePrefab, transform.position, Quaternion.identity);
                FrostWave waveScript = wave.GetComponent<FrostWave>();
                waveScript.maxRadius = 1;
                StartCoroutine(BeginCooldown());
            }
        }
    }

    IEnumerator BeginCooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(cooldown);
        Debug.Log("Came off cooldown");
        canFire = true;        
    }
}
