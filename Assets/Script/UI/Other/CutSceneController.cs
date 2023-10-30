using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneController : MonoBehaviour
{
    public int ChatLogID; //��ȭ�α�

    private bool isEndTalk = false;//��ȭ�� ������ ���� ���� ���� üũ

    public void OnChatLog()
    {
        //��ȭ�α׸� �̹� ����� �� â ����
        if (isEndTalk)
        {
            Destroy(gameObject);
        }
        else
        {
            PlayerUIController.instance.OpenChatWindow(ChatLogID, this.gameObject);//��ȭâ ����
            isEndTalk = true;
        }
    }
}
