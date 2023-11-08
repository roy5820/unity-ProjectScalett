using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetItem : MonoBehaviour
{
    
    public int GetItemId;
    public GameObject ChangeObj = null; //이미지를 변경할 오브젝트
    public Sprite OpenChangeImg = null; //오픈 시 변경할 이미지
    public Sprite GetChangeImg = null; //아이템 획득 시 변경할 이미지

    public int GetItemType = 0; //0: 사용아이템, 1: 증거
    public bool isLock = false; //잠김 여부
    public int KeyItemId = 101; //상호작용 되는 아이템의 ID

    public int GetMessageID;//아이템 획득 시 메세지 ID
    public int DuplicateMessageID;//중복 메시지 ID
    public int LockMessageID;//잠김 메세지 ID

    public int isStatus = 0; //현제 오브젝트 상태 값 0: 닫침 1: 열린상태, 2: 빈상태 
    private string dataName = null; //데이터베이스에 저장될 데이터명

    public void Start()
    {
        //데이터명을 현제 씬 이름 + 오브젝트 이름으로 초기화
        dataName = SceneManager.GetActiveScene().name+ "_"+gameObject.name + "_GetItem";

        //아이템 획득 여부를 게임메니저에서 가져오기
        if (dataName != null)
        {
            int getObjStatus = GameManager.instance.ControllObjStatusData(0, dataName, 0);//게임 메니저에서 데이터 가져오기
            if(getObjStatus >= 0)
                isStatus = getObjStatus;
        }
    }

    public void Update()
    {
        //오브젝트 상태별 이미지 교체
        if (isStatus == 1)
        {
            //변경할 이미지 및 오브젝트가 존재할 시 변경
            if (ChangeObj != null && GetChangeImg != null)
            {
                ChangeObj.GetComponent<SpriteRenderer>().sprite = OpenChangeImg;//이미지 변경
            }
        }
        else if (isStatus == 2)
        {
            //변경할 이미지 및 오브젝트가 존재할 시 변경
            if (ChangeObj != null && GetChangeImg != null)
            {
                ChangeObj.GetComponent<SpriteRenderer>().sprite = GetChangeImg;//이미지 변경
            }
        }
    }

    public void OnDestroy()
    {
        //스테이터스를 상태 값을 데이터 베이스에 저장
        GameManager.instance.ControllObjStatusData(1, dataName, isStatus);
    }

    //아이템 획득 구현 함수
    public void DropItem()
    {
        
        //아이템 획득 여부 + 잠김 여부에 따른 아이템박스 오픈
        if (isStatus == 0 && ((isLock && GameManager.instance.SelectItemId == KeyItemId) || !isLock))
        {
            isStatus = 1; //아이템박스 오픈상태로 1로 설정

            //Key아이템을 인벤토리에서 찾아 제거
            if (isLock)
            {
                //key아이템을 인벤토리에서 제거하는 문장입니다.
                GameManager.instance.DelItem(0, KeyItemId);
            }
        }
        else if(isStatus == 1)
        {
            
            isStatus = 2; //아이템 획득여부 2로 설정

            /*//아이템 종류별 이벤트 처리
            GameManager.instance.AddItem(GetItemType, GetItemId);

            //아이템 획득 시 아이템 인스펙터창 띄우기
            PlayerUIController.instance.OpenItemInspectorWindow(GetItemType, GetItemId);*/
            //아이템 획득 대화창 띄우기
            PlayerUIController.instance.OpenChatWindow(GetMessageID, null);
        }
        else if (isStatus == 2)
        {
            //빈상태 상태에서 상호작용시 메시지 출력
            if(DuplicateMessageID > 0)
                PlayerUIController.instance.OpenChatWindow(DuplicateMessageID, null);
        }
        else if(isLock)
        {
            //잠겨있고 열쇠가 되는 아이템이 없을 시 잠겨있음 메시지 출력
            if (LockMessageID > 0)
                PlayerUIController.instance.OpenChatWindow(LockMessageID, null);
        }
    }
}
