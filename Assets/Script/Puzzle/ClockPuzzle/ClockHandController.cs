using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ClockHandController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos = eventData.position;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 direction = worldPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(transform.GetComponent<RectTransform>().rotation.z * 180 / 6);
        transform.rotation = Quaternion.Euler(0, 0, (int)(transform.GetComponent<RectTransform>().rotation.z*180/6) * 30);
    }
}
