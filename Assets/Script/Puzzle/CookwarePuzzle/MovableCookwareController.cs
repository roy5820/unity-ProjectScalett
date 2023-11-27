using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovableCookwareController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    //오브젝트 이동제한 값
    [SerializeField]
    private float maxX = 0;
    [SerializeField]
    private float minX = 0;
    [SerializeField]
    private float maxY = 0;
    [SerializeField]
    private float minY = 0;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.delta / GetComponentInParent<Canvas>().scaleFactor;
        Vector2 position = rectTransform.anchoredPosition + delta;
        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);
        rectTransform.anchoredPosition = position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}