using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenDialogueSelectBox : MonoBehaviour
{
    public GameObject DialogueSelectPre;//������ ��ȭ ����â ������

    //��ȭ ����â�� �����ϴ� �Լ�
    public void OpenDialogueSelectBoxPre()
    {
        GameObject thisPre = Instantiate(DialogueSelectPre, this.transform.parent.parent);

        thisPre.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }
}
