using UnityEngine;
using UnityEngine.EventSystems;

public class TowerHandler : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    [SerializeField] public Canvas canvas;
    [SerializeField] public CanvasGroup canvasGroup;
    private Transform towerTransform;
    private Transform originalPosition;


    private void Awake()
    {
        towerTransform = GetComponent<Transform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = towerTransform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .7f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        towerTransform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0f) / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("clicking on tower placer");
    }
}
