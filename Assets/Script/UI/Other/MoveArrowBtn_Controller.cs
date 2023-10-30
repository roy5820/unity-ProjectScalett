using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveArrowBtn_Controller : MonoBehaviour
{
    public string SceneName; //이동할 씬 이름

    public int isStatus = 0;//0:이동 가능 1: 잠김
    private string dataName = null; //데이터베이스에 저장될 데이터명

    public int KeyItemID;//키 아이템 아이디
    public int KeyItemType;//키 아이템 타입
    public int LockMessageID;//잠김 메세지

    void Start()
    {
        //데이터명을 현제 씬 이름 + 오브젝트 이름으로 초기화
        dataName = SceneManager.GetActiveScene().name + "_" + gameObject.name + "_MoveArrowBtn";

        //아이템 획득 여부를 게임메니저에서 가져오기
        if (dataName != null)
        {
            int getObjStatus = GameManager.instance.ControllObjStatusData(0, dataName, 0);//게임 메니저에서 데이터 가져오기
            if (getObjStatus >= 0)
                isStatus = getObjStatus;
        }

        //잠김 상태일 경우 키 아이템이 있으면 이동 가능 상태로 전환
        if (isStatus == 1 && GameManager.instance.ChkItem(KeyItemType, KeyItemID))
        {
            isStatus = 0;
        }
    }

    private void Update()
    {
        
    }

    public void OnMoveScene()
    {
        //상태값에 따른 이벤트 처리
        if(isStatus == 0)
        {
            PlayerUIController.instance.ChangeScene(SceneName);//씬이동
        }
        if(isStatus == 1)
        {
            PlayerUIController.instance.OpenChatWindow(LockMessageID);//잠김 메시지 띄우기
        }
    }

    private void OnDestroy()
    {
        //스테이터스를 상태 값을 데이터 베이스에 저장
        GameManager.instance.ControllObjStatusData(1, dataName, isStatus);
    }
}
