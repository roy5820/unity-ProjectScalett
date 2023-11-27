using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovableCookwareController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private RectTransform rectTransform; //���������Ʈ�� ��ƮƮ������
    private CanvasGroup canvasGroup; //���� ������Ʈ�� �׷�ĵ����

    private RectTransform parentsTransform;//�θ� ������Ʈ�� ��Ʈ Ʈ������

    //������Ʈ �̵����� ��
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

        //�θ� �ٲ���� �� �θ� �ٲٱ�
        if (parentsTransform == rectTransform.parent.GetComponent<RectTransform>())
        {
            //�θ� �ٲٱ�
            parentsTransform = rectTransform.parent.GetComponent<RectTransform>();
        }
        //��ġ 0���� �ʱ�ȭ
        rectTransform.anchoredPosition = new Vector2(0, 0);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Transform getParent = eventData.pointerDrag.transform.parent;
            //���� �巡�� ���� ������Ʈ�� �θ� �ڽ��� �θ�� �ٲٱ�
            eventData.pointerDrag.transform.SetParent(transform.parent);

            //�巡�� ���� ������Ʈ�� �θ� �ڽ��� �θ�� �ٲ�
            transform.SetParent(getParent);
            //��ġ 0���� �ʱ�ȭ
            rectTransform.anchoredPosition = new Vector2(0, 0);
        }
    }
}