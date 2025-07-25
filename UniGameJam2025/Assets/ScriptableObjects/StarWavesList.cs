using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWaveList", menuName = "Wave/Wave List")]
public class StarWavesList : ScriptableObject
{
    public List<StarWave> waves;
}
