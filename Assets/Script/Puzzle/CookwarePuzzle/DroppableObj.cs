using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DroppableObj : MonoBehaviour, IDropHandler
{
    private RectTransform rectTransform;

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().position = rectTransform.position;
        }
    }
}
