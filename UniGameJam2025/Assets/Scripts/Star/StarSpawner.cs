using UnityEngine;
using UnityEngine.Splines;
using System.Collections;

public class StarSpawner : MonoBehaviour
{
    public GameObject starPrefab;
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
        GameObject star = Instantiate(starPrefab, transform.position, Quaternion.identity);
        Debug.Log("Star was instantiated");

        Star script = star.GetComponent<Star>();
        if (script != null && path != null)
        {
            script.splineContainer = path;
        }
    }
}

