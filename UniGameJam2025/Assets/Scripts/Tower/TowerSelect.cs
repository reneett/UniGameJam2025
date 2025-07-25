using UnityEngine;

public class TowerSelect : MonoBehaviour
{

    [SerializeField] public BasicTower tower;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnMouseDown()
    {
        tower.Clicked();
    }
}
