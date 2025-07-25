using UnityEngine;
using UnityEngine.Splines;
using System.Collections;
using System.Collections.Generic;

public class StarSpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public List<Star> enemies;
    public SplineContainer path;
    public StarWave baseWave;
    public int waveIndex = 1;
    public bool isWaitingForPlayerReady;
    private HashSet<Star> liveStars = new HashSet<Star>();

    private void Start()
    {
        StartCoroutine(SpawnStars());
    }

    private IEnumerator SpawnStars()
    {
        int numStars = baseWave.numStars;
        float spawnDelay = baseWave.spawnDelay;
        for (int i = 0; i < numStars * waveIndex; ++i)
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
            enemies.Add(script);
        }
    }

    private void OnEnable()
    {
        Star.OnExplode += HandleStarExplode;
    }

    private void OnDisable()
    {
        Star.OnExplode -= HandleStarExplode;
    }
    private void HandleStarExplode(Star star)
    {
        liveStars.Remove(star);
        if (liveStars.Count == 0)
        {
            Debug.Log("Wave Won!");
            waveIndex++;

            // UI Manager Call here?
        }
    }
}

