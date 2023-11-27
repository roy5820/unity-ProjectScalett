using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovableCookwareController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private RectTransform rectTransform; //현재오브젝트의 렉트트렌스폼
    private CanvasGroup canvasGroup; //현재 오브젝트의 그룹캔버스

    private RectTransform parentsTransform;//부모 오브젝트의 렉트 트랜스폼

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
        parentsTransform = rectTransform.parent.GetComponent<RectTransform>();
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
        rectTransform.anchoredPosition = position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        //부모가 바뀌었을 시 부모 바꾸기
        if (parentsTransform == rectTransform.parent.GetComponent<RectTransform>())
        {
            //부모값 바꾸기
            parentsTransform = rectTransform.parent.GetComponent<RectTransform>();
        }
        //위치 0으로 초기화
        rectTransform.anchoredPosition = new Vector2(0, 0);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Transform getParent = eventData.pointerDrag.transform.parent;
            //현재 드래그 중인 오브젝트의 부모를 자신의 부모로 바꾸기
            eventData.pointerDrag.transform.SetParent(transform.parent);

            //드래그 중인 오브젝트의 부모를 자신의 부모로 바꿈
            transform.SetParent(getParent);
            //위치 0으로 초기화
            rectTransform.anchoredPosition = new Vector2(0, 0);
        }
    }
}