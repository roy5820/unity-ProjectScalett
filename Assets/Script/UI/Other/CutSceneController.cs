using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneController : MonoBehaviour
{
    public int ChatLogID; //대화로그

    private bool isEndTalk = false;//대화가 끝났는 지에 따른 여부 체크

    public void OnChatLog()
    {
        //대화로그를 이미 띄었을 시 창 끄기
        if (isEndTalk)
        {
            Destroy(gameObject);
        }
        else
        {
            PlayerUIController.instance.OpenChatWindow(ChatLogID, this.gameObject);//대화창 띄우기
            isEndTalk = true;
        }
    }
}
