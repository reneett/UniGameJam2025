using UnityEngine;

public class GhostTower : MonoBehaviour
{

    [SerializeField] public LayerMask towerLayer;
    [SerializeField] private BoxCollider2D baseCollider;
    [SerializeField] private SpriteRenderer radius;
    public int towerCost = 100;
    public Color yesPlace;
    public Color noPlace;

    private bool valid;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        valid = false;
        radius.color = noPlace;
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D overlap = Physics2D.OverlapBox(baseCollider.bounds.center, baseCollider.bounds.size, 0f, towerLayer);

        if (overlap != null)
        {
            radius.color = noPlace;
            valid = false;
        }
        else
        {
            radius.color = yesPlace;
            valid = true;
        }
    }

    public bool IsValid()
    {
        return valid;
    }

}
