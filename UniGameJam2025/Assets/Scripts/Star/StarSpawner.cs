using UnityEngine;
using UnityEngine.Splines;
using System.Collections;
using System.Collections.Generic;

public class StarSpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public SplineContainer path;
    public int numStars = 5;

    [Header("Spawn Delay Timer")]
    public float spawnDelay = 1.0f;

    private void Start()
    {
        StartCoroutine(SpawnStars());
    }

    private IEnumerator SpawnStars()
    {
        for (int i = 0; i < numStars; ++i)
        {
            SpawnStar();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnStar()
    {
        int rand = Random.Range(0, enemyPrefabs.Count);

        GameObject star = Instantiate(enemyPrefabs[rand], transform.position, Quaternion.identity);
        Debug.Log("Star was instantiated");

        Star script = star.GetComponent<Star>();
        if (script != null && path != null)
        {
            script.splineContainer = path;
        }
    }
}

