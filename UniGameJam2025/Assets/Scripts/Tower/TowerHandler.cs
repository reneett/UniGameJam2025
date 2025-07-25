using UnityEngine;
using UnityEngine.EventSystems;

public class TowerHandler : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    [SerializeField] public Canvas canvas;
    [SerializeField] public CanvasGroup canvasGroup;
    [SerializeField] private TMPro.TextMeshProUGUI radiusText;
    [SerializeField] private TMPro.TextMeshProUGUI speedText;
    [SerializeField] private TMPro.TextMeshProUGUI damageText;
    [SerializeField] public GameObject ghostTower;
    [SerializeField] public GameObject BasicTowerPrefab;
    [SerializeField] private UIManager uiManager;
    private GameObject ghost;
    private RectTransform towerTransform;
    private Vector2 originalPosition;

    private BasicTower currentActive;


    private void Awake()
    {
        towerTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = towerTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .7f;
        canvasGroup.blocksRaycasts = false;

        ghost = Instantiate(ghostTower);
    }

    public void OnDrag(PointerEventData eventData)
    {
        towerTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(towerTransform.position);
        worldPos.z = 0f;
        ghost.transform.position = worldPos;
    }

    //returns icon to original position when let go
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        towerTransform.anchoredPosition = originalPosition;

        //place a tower, depending on validity of position
        var ghostScript = ghost.GetComponent<GhostTower>();

        if (ghostScript.IsValid())
        {
            if (uiManager.currMoney >= 100)
            {
                //spawn basic tower, and subtract cost
                uiManager.changeMoney(-100);
                GameObject newTower = Instantiate(BasicTowerPrefab, ghost.transform.position, Quaternion.identity);
                newTower.GetComponent<BasicTower>().uiManager = uiManager;
                newTower.GetComponent<BasicTower>().towerHandler = this;
            }
        }
        else
        {
            Debug.Log("Invalid position, cancelling placement");
        }
        Destroy(ghost);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("clicking on tower placer");
    }

    public void SwapTower(BasicTower newTower)
    {
        if (currentActive != null)
        {
            currentActive.CloseUpgrader();
        }
        currentActive = newTower;

        radiusText.text = currentActive.upgradeLevels[0].ToString();
        speedText.text = currentActive.upgradeLevels[1].ToString();
        damageText.text = currentActive.upgradeLevels[2].ToString();
    }
}
