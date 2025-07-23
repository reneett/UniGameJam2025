using UnityEngine;

[CreateAssetMenu(fileName = "StarPath", menuName = "Scriptable Objects/StarPath")]
public class StarPath : ScriptableObject
{
  public Vector2[] waypoints;
}
