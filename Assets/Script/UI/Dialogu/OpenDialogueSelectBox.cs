using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenDialogueSelectBox : MonoBehaviour
{
    public GameObject DialogueSelectPre;//생성할 대화 선택창 프리펩

    //대화 선택창을 생성하는 함수
    public void OpenDialogueSelectBoxPre()
    {
        GameObject thisPre = Instantiate(DialogueSelectPre, this.transform.parent.parent);

        thisPre.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }
}
