using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    public GameObject ChangeObj = null; //이미지를 변경할 오브젝트
    public Sprite ChangeImg = null; //변경할 이미지
    public bool isLock = false; //잠김 여부
    public int KeyItemId = 101; //상호작용 되는 아이템의 ID

    public int LockMessageID;//잠김 메세지 ID
    public int UnLockMessageID;//잠김 해제 메세지 ID
    public string UnLockCutScenePreName = null;//잠김 해제 컷씬 프리펩 이름

    private int isStatus = 0; //현제 오브젝트 상태 값 0: 상호작용X ,1: 상호작용O
    private string dataName = null; //데이터베이스에 저장될 데이터명
    public void Start()
    {
        //데이터명을 현제 씬 이름 + 오브젝트 이름으로 초기화
        dataName = SceneManager.GetActiveScene().name + "_" + gameObject.name + "_DoorInteraction";

        //아이템 획득 여부를 게임메니저에서 가져오기
        if (dataName != null)
        {
            int getObjStatus = GameManager.instance.ControllObjStatusData(0, dataName, 0);//게임 메니저에서 데이터 가져오기
            if (getObjStatus >= 0)
                isStatus = getObjStatus;
        }

        //활성화 여부에 따라 Lock상태 변경
        if (isLock)
        {
            if (isStatus == 1)
                isLock = false;
        }
    }

    public void Update()
    {
        //오브젝트 상태별 이미지 교체
        if (isStatus == 1)
        {
            //변경할 이미지 및 오브젝트가 존재할 시 변경
            if (ChangeObj != null && ChangeImg != null)
            {
                ChangeObj.GetComponent<SpriteRenderer>().sprite = ChangeImg;//이미지 변경
            }
        }
    }

    public void OnDestroy()
    {
        //스테이터스를 상태 값을 데이터 베이스에 저장
        GameManager.instance.ControllObjStatusData(1, dataName, isStatus);
    }
    //문 이동 구현 함수
    public void OpenDoor(string SceneName)
    {
        //조건에 따른 이벤트 구현
        //문이 열러있으면 그냥 이동
        if (!isLock)
        {
            if(!isLock)
                SceneManager.LoadScene(SceneName);
        }
        //문이 잠겨 있고 열쇠가 있으면 잠금 해제
        else if (isLock && (KeyItemId == 0 || (KeyItemId == GameManager.instance.SelectItemId)))
        {
            isLock = false;//잠금 상태 해제
            isStatus = 1;//상호작용 상태 1로 변경
            //컷씬 출력
            if (UnLockCutScenePreName != null)
                PlayerUIController.instance.OpenCutSceneWindow(UnLockCutScenePreName, this.gameObject, "OpenDoor", SceneName);
            //메세지 출력
            else if (UnLockMessageID > 0)
                PlayerUIController.instance.OpenChatWindow(UnLockMessageID);

            //변경할 이미지 및 오브젝트가 존재할 시 변경
            if (ChangeObj != null && ChangeImg != null)
            {
                ChangeObj.GetComponent<SpriteRenderer>().sprite = ChangeImg;//이미지 변경
            }

            //key아이템을 인벤토리에서 제거하는 문장입니다.
            GameManager.instance.DelItem(0, KeyItemId);
        }
        else if (isLock) //잠겨있을 시 메시지 띄우고 끝
        {
            //잠겨있고 열쇠가 되는 아이템이 없을 시 잠겨있음 메시지 출력
            if (LockMessageID > 0)
                PlayerUIController.instance.OpenChatWindow(LockMessageID, null);
        }
    }
}
