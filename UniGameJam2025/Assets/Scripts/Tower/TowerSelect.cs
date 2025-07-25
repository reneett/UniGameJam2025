using UnityEngine;

public class TowerSelect : MonoBehaviour
{

    [SerializeField] public BasicTower tower;

    //all this does is tell the tower it was clicked. i hate colliders
    void OnMouseDown()
    {
        Debug.Log("clicked!");
        tower.Clicked();
    }
}
