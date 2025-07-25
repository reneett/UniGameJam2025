using UnityEngine;

[CreateAssetMenu(fileName = "StarWaves", menuName = "Wave/Star Wave Data")]
public class StarWave : ScriptableObject
{
    public int numStars;
    public float spawnDelay;
    public float speed;
}
