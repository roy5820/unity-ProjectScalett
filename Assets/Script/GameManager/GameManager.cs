using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

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

    private string InGameDataJsonPath;//인게임 데이터가 저장되는 장소
    private string HaveItemDataJsonFile; // 보유아이템 데이터가 저장되는 JSON파일
    private string ObjStatusDataJsonFile; // 오브젝트 상태값이 데이터가 저장되는 JSON파일

    private string HaveItemDataPath;//보유 아이템 JSON파일 경로
    private string ObjStatusDataPath;//보유 아이템 JSON파일 경로

    private void Awake()
    {
        //게임 메니저 인스턴스 초기화
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
            

        DontDestroyOnLoad(gameObject);

        HaveItemDataPath = Path.Combine(Application.streamingAssetsPath, "HaveItemDataBase.json");//보유 아이템 JSON파일 경로 초기화
        ObjStatusDataPath = Path.Combine(Application.streamingAssetsPath, "ObjStatusDataBase.json");//오브젝트 상태값 JSON파일 경로 초기화

    //JSON파일 읽어 오기
    OnLoadJsonFile();
    }

    //플레이어 아이템 데이터베이스에 특정 아이템이 있는지 확인하는 함수
    public bool ChkItem(int ItemType, int ItemId)
    {
        if (ItemType < 0 && ItemId <= 0)//매개변수 빈값 체크
            return false;

        List<DataIds> thisIDs  = null;//아이템 타입별 리스트가 드러갈 변수 선언

        //타입별 리스트 설정
        switch (ItemType)
        {
            case 0:
                thisIDs = HaveDataBase.UseItem;
                break;
            case 1:
                thisIDs = HaveDataBase.EvidenceItem;
                break;
            case 2:
                thisIDs = HaveDataBase.CharacterInformation;
                break;
            case 3:
                thisIDs = HaveDataBase.CharacterTestimony;
                break;
            default:
                return false;//맞는 아이템 타입이 없으면 종료
        }
        //유저 인벤토리에서 해당 아이템 검색 후 있으면 true 리턴
        foreach (DataIds id in thisIDs)
        {
            if (id.ID == ItemId)
            {
                return true;
            }
        }

        return false;
    }

    //플레이어 아이템 데이터베이스에 아이템 추가하는 함수
    public void AddItem(int ItemType, int ItemId, int ItemRelatedID = 0)
    {
        List<DataIds> GetDataBase = null;//데이터 베이스에서 데이터를 받아 넣을 변수

        //아이템 중복 체크, 이미 있을 경우 함수 종료
        if (ChkItem(ItemType, ItemId)) return;

        //아이템 타입별로 List 설정
        switch (ItemType)
        {
            case 0:
                GetDataBase = HaveDataBase.UseItem;
                break;
            case 1:
                GetDataBase = HaveDataBase.EvidenceItem;
                break;
            case 2:
                GetDataBase = HaveDataBase.CharacterInformation;
                break;
            case 3:
                GetDataBase = HaveDataBase.CharacterTestimony;
                break;
        }

        //데이터 삽입 하는 부분
        //데이터 삽입전 데이터 베이스에 맞는 데이터로 가공
        DataIds ComparativeData = new DataIds();
        ComparativeData.ID = ItemId;//아이템 아이디 설정
        ComparativeData.RelatedID = ItemRelatedID;//아이템 관련 아이디 설정

        int DataBaseCnt = GetDataBase.Count;//현제 리스트의 크기
        //리스트의 크기가 0보다 클시 삽입정렬을 통한 데이터 삽입 진행 아닐 시 그냥 추가
        if (DataBaseCnt > 0)
        {
            for (int i = 0; i < DataBaseCnt; i++)
            {
                //비교할 데이터가 현제 아이디 보다 작을 시 현제 위치에 데이터 삽입
                if (ComparativeData.ID < GetDataBase[i].ID)
                {
                    //비교중인 데이터를 현제 위치에 삽입
                    GetDataBase.Insert(i, ComparativeData);
                    return;
                }

                //현제 위치가 데이터의 끝일경우 NowData의 값을 리스트 끝에 추가
                if (i == DataBaseCnt - 1)
                    GetDataBase.Add(ComparativeData);
            }
        }
        else
        {
            GetDataBase.Add(ComparativeData);//아이템 추가
        }

        OnSave();
    }
    //데이터베이스에서 아이템을 제거하는 함수
    public void DelItem(int ItemType, int ItemId)
    {
        //플레이어 데이터베이스 가져오기
        List<DataIds> GetDataBase = null;//데이터 베이스에서 데이터를 받아 넣을 변수

        //아이템 타입별로 List 설정
        switch (ItemType)
        {
            case 0:
                GetDataBase = HaveDataBase.UseItem;
                break;
            case 1:
                GetDataBase = HaveDataBase.EvidenceItem;
                break;
            case 2:
                GetDataBase = HaveDataBase.CharacterInformation;
                break;
            case 3:
                GetDataBase = HaveDataBase.CharacterTestimony;
                break;
        }

        //ID로 유저 인벤토리에서 데이터 삭제
        foreach (DataIds dataid in GetDataBase)
        {
            if (dataid.ID == ItemId)
            {
                GetDataBase.Remove(dataid);
                break;
            }
        }

        OnSave();//삭제 후 저장
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

                OnSave();

                return AddStatus.Status;
            }
        }

        return -1; //데이터를 읽지 못했을 경우 -1로 리턴
    }

    //인게임 데이터가 저장된 JSON을 읽어오는 함수
    private void OnLoadJsonFile()
    {
        // JSON 파일을 읽어와서 데이터베이스 초기화
        //보유 아이템 데이터베이스 초기화 부분
        //경로에 파일 유무에 따른 실행여부 결정
        if (File.Exists(HaveItemDataPath))
        {
            HaveItemDataJsonFile = File.ReadAllText(HaveItemDataPath);//json파일 가져오기
            HaveDataBase = JsonUtility.FromJson<PlayerDataBase>(HaveItemDataJsonFile);//가져온 json파일 데이터베이스에 삽입
        }


        //오브젝트 상태값 저장되는 데이터베이스 초기화 부분
        //경로에 파일 유무에 따른 실행여부 결정
        if (File.Exists(ObjStatusDataPath))
        {
            ObjStatusDataJsonFile = File.ReadAllText(ObjStatusDataPath);//json파일 가져오기
            ObjStatusDataBase = JsonUtility.FromJson<StatusDataBase>(ObjStatusDataJsonFile);//가져온 json파일 데이터베이스에 삽입
        }
    }

    //저장기능 구현 함수
    private void OnSave()
    {
        //게임 종료 또는 메뉴로 나갈 시 게임 데이터 json 파일로 저장
        //보유 아이템 인벤토리를 json파일로 저장하는 부분
        string haveItemDataBaseJson = JsonConvert.SerializeObject(HaveDataBase); //데이터 베이스 내용을 json문자열로 변환
        File.WriteAllText(HaveItemDataPath, haveItemDataBaseJson);//Json 문자열을 파일에 쓰기
        
        //오브젝트 상태값이 json파일로 저장하는 부분
        string objStatusDatatBasejson = JsonConvert.SerializeObject(ObjStatusDataBase); //데이터 베이스 내용을 json문자열로 변환
        File.WriteAllText(ObjStatusDataPath, objStatusDatatBasejson);//Json 문자열을 파일에 쓰기
    }

    private void OnDestroy()
    {
        //마지막 있던 씬 이름을 PlayerPrefs에 저장
        PlayerPrefs.SetString("LastSceneName", SceneManager.GetActiveScene().name);
    }
}
