using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//데이터 ID변수
[System.Serializable]
public class DataIds
{
    public int ID;
    public int RelatedID;
}

//플레이어 보유아이템 저장할 데이터 변수형
[System.Serializable]
public class PlayerDataBase
{
    public List<DataIds> UseItem;
    public List<DataIds> EvidenceItem;
    public List<DataIds> CharacterInformation;
    public List<DataIds> CharacterTestimony;
}

[System.Serializable]
//각객체별 Status저장하는 데이터베이스 객체 선언
public class ObjStatus
{
    public string DataName;
    public int Status;
}

[System.Serializable]
public class StatusDataBase
{
    public List<ObjStatus> ObjStatus;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;//게임메니저 인스턴스화를 위한 변수 초기화
    public PlayerDataBase HaveDataBase; // 보유중인 아이템 데이터 베이스
    public StatusDataBase ObjStatusDataBase;//오브젝트 상태 저장하는 데이터 베이스

    public int SelectItemId = 0; //현제 선택한 사용 아이템

    private void Awake()
    {
        //게임 메니저 인스턴스 초기화
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    //플레이어 아이템 데이터베이스에 특정 아이템이 있는지 확인하는 함수
    public void GetItem(int ItemType, int ItemId)
    {

    }

    //플레이어 아이템 데이터베이스에 아이템 추가하는 함수
    public void AddItem(int ItemType, int ItemId)
    {
        DataIds dataid = new DataIds();//데이터 베이스에서 데이터를 받아 넣을 변수
        bool DuplicateChk = false; //데이터 중복 체크
        //아이템 타입별로 데이터 가져오기
        switch (ItemType)
        {
            case 0:
            case 1:
                Item GetItem = ItemDataManager.instance.GetItemByID(ItemId, ItemType);//아이템 Id로 아이템 정보 가져오기
                //PlayerDataBase에 맞게 데이터 변환
                
                dataid.ID = GetItem.id;
                //타입별로 플레이어 인벤토리에 ID추가
                if (ItemType == 0)
                {
                    //이미 보유중인지 중복 체크
                    foreach (DataIds data in GameManager.instance.HaveDataBase.UseItem)
                    {
                        if (data.ID == ItemId)
                        {
                            DuplicateChk = true;
                        }
                    }
                    //해당 데이터가 이미 인벤토리에 있는 지 중복 체크
                    if (!DuplicateChk)
                    {
                        if (GetItem != null)
                            GameManager.instance.HaveDataBase.UseItem.Add(dataid);//아이템 인벤토리에 ID 추가
                    }
                }
                else if (ItemType == 1)
                {
                    //이미 보유중인지 중복 체크
                    foreach (DataIds data in GameManager.instance.HaveDataBase.EvidenceItem)
                    {
                        if (data.ID == ItemId)
                        {
                            DuplicateChk = true;
                        }
                    }
                    //해당 데이터가 이미 인벤토리에 있는 지 중복 체크
                    if (!DuplicateChk)
                    {
                        if (GetItem != null)
                            GameManager.instance.HaveDataBase.EvidenceItem.Add(dataid);//아이템 인벤토리에 ID 추가
                    }
                }

                break;
            case 2:
                //이미 보유중인지 중복 체크
                foreach (DataIds data in GameManager.instance.HaveDataBase.CharacterInformation)
                {
                    if (data.ID == ItemId)
                    {
                        DuplicateChk = true;
                    }
                }
                //해당 데이터가 이미 인벤토리에 있는 지 중복 체크
                if (!DuplicateChk)
                {
                    Character GetCharacter = ItemDataManager.instance.GetCharcterByID(ItemId);

                    dataid.ID = GetCharacter.id;
                    if (GetCharacter != null)
                        GameManager.instance.HaveDataBase.CharacterInformation.Add(dataid);//아이템 인벤토리에 ID 추가
                }

                break;
            case 3:
                //이미 보유중인지 중복 체크
                foreach (DataIds data in GameManager.instance.HaveDataBase.CharacterTestimony)
                {
                    if (data.ID == ItemId)
                    {
                        DuplicateChk = true;
                    } 
                }
                //해당 데이터가 이미 인벤토리에 있는 지 중복 체크
                if (!DuplicateChk)
                {
                    //아이템 데이터 베이스에 해당 아이템이 있는 지 체크
                    Testimony GetTestimony = ItemDataManager.instance.GetTestimonyByID(ItemId);

                    dataid.ID = GetTestimony.id;
                    dataid.RelatedID = GetTestimony.id / 100;
                    if (GetTestimony != null)
                        GameManager.instance.HaveDataBase.CharacterTestimony.Add(dataid);//아이템 인벤토리에 ID 추가
                }
                
                break;
        }
    }
    //데이터베이스에서 아이템을 제거하는 함수
    public void DelItem(int ItemType, int ItemId)
    {
        //플레이어 데이터베이스 가져오기
        PlayerDataBase haveItemData = GameManager.instance.HaveDataBase;

        //아이템 종류별 이벤트 처리
        if (ItemType == 0)
        {
            //ItemId가 일치하는 아이템을 인벤토리에서 제거하는 문장입니다.
            List<DataIds> UseItemList = GameManager.instance.HaveDataBase.UseItem; //배열 삭제를 위한 아이템 데이터베이스 리스트화
            foreach (DataIds dataid in haveItemData.UseItem)
            {
                if (dataid.ID == ItemId)
                {
                    UseItemList.Remove(dataid);
                    break;
                }
            }
        }
        else if (ItemType == 1)
        {
            //ItemId가 일치하는 아이템을 인벤토리에서 제거하는 문장입니다.
            List<DataIds> UseItemList = GameManager.instance.HaveDataBase.EvidenceItem; //배열 삭제를 위한 아이템 데이터베이스 리스트화
            foreach (DataIds dataid in haveItemData.EvidenceItem)
            {
                if (dataid.ID == ItemId)
                {
                    UseItemList.Remove(dataid);
                    break;
                }
            }
        }
        else if (ItemType == 2)
        {
            //ItemId가 일치하는 아이템을 인벤토리에서 제거하는 문장입니다.
            List<DataIds> UseItemList = GameManager.instance.HaveDataBase.CharacterInformation; //배열 삭제를 위한 아이템 데이터베이스 리스트화
            foreach (DataIds dataid in haveItemData.CharacterInformation)
            {
                if (dataid.ID == ItemId)
                {
                    UseItemList.Remove(dataid);
                    break;
                }
            }
        }
        else if (ItemType == 3)
        {
            //ItemId가 일치하는 아이템을 인벤토리에서 제거하는 문장입니다.
            List<DataIds> UseItemList = GameManager.instance.HaveDataBase.CharacterTestimony; //배열 삭제를 위한 아이템 데이터베이스 리스트화
            foreach (DataIds dataid in haveItemData.CharacterTestimony)
            {
                if (dataid.ID == ItemId)
                {
                    UseItemList.Remove(dataid);
                    break;
                }
            }
        }
    }

    //ObjStatusDataBase를 컨트롤 하는 함수 Mode: 0:읽기 1:쓰기 
    public int ControllObjStatusData(int Mode,string DataName, int inputStatus)
    {
        //null체크
        if (DataName != null && inputStatus >= 0)
        {
            //데이터 베이스에서 오브젝트 이름으로 검색하여 정보 가져온뒤 Mode에 따라 읽기 또는 쓰기 실행
            foreach (ObjStatus thisStatus in ObjStatusDataBase.ObjStatus)
            {
                //ObjName과 일치하는 데이터 있는지 검색
                if (thisStatus.DataName == DataName)
                {
                    //읽기 모드일 경우 상태값만 리턴하고 종료
                    //쓰기 모드일 경우 상태값 수정
                    if (Mode == 1)
                    {
                        thisStatus.Status = inputStatus;
                    }
                    return thisStatus.Status;
                }
            }

            //데이터 베이스에 해당 데이터가 없을 경우 데이터 추가
            if (Mode == 1)
            {
                //추가할 ObjStatus객체 생성 및 값 설정
                ObjStatus AddStatus = new ObjStatus();
                AddStatus.DataName = DataName;
                AddStatus.Status = inputStatus;

                ObjStatusDataBase.ObjStatus.Add(AddStatus);//리스트에 데이터 추가

                return AddStatus.Status;
            }
        }

        return -1; //데이터를 읽지 못했을 경우 -1로 리턴
    }
}
