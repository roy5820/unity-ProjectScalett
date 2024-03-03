using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUIController : MonoBehaviour
{
    public static PlayerUIController instance = null;//인스턴스화

    //변경할 사용아이템 인벤토리창
    [SerializeField]
    public GameObject[] ItemBoxs;
    bool IsNullItemBoxs = true;

    //수첩시스템 관련 변수 선언
    public GameObject Note;

    string ImgPath = "Image/Item/"; //아이템 이미지 경로

    public GameObject ChatLogPre;//대화창 프리펩

    public GameObject ItemInspectorPre;//아이템 추가설명창 프리펩

    public GameObject InGameMenu;//인게임 메뉴창


    private void Awake()
    {
        //플레이어 UI컨트롤러 인스턴스 초기화
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //사용자 사용아이템 인벤토리의 아이템목록 리스트화
        List<DataIds> ItemIds = GameManager.instance.HaveDataBase.UseItem;
        //각 사용아이템 칸에 아이템 이미지 적용
        for (int i=0; i < ItemBoxs.Length; i++)
        {
            if(i < ItemIds.Count)
            {
                Sprite itemSprite = Resources.Load<Sprite>(ImgPath + ItemDataManager.instance.GetItemByID(ItemIds[i].ID, 0).spriteName);
                //이미지가 비어있지 않을 시 아이템 이미지 적용
                if (itemSprite != null)
                {
                    ItemBoxs[i].GetComponent<Image>().sprite = itemSprite;
                    ItemBoxs[i].SetActive(true);
                }

                //선택중인 아이템이 해당 아이템이면 아이템 박스를 키우기
                if (GameManager.instance.SelectItemId == ItemIds[i].ID)
                {
                    ItemBoxs[i].GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);
                    ItemBoxs[i].transform.parent.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1);
                }
                else
                {
                    ItemBoxs[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    ItemBoxs[i].transform.parent.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1);
                }
            }
            //빈 아이템칸은 기본값으로 설정 초기화
            else
            {
                ItemBoxs[i].GetComponent<Image>().sprite = null;
                ItemBoxs[i].SetActive(false);
                ItemBoxs[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                ItemBoxs[i].transform.parent.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1);
            }
        }
    }

    //플레이어 씬 이동 시 호출
    public void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    //플레이어 사용 아이템 선택 SelectNum: 선택한 아이템칸 번호
    public void SelectItem(int SelectNum)
    {
        //선택한 인벤토리칸 번호가 현재 인벤토리의 개수 보다 많지 않을 경우에 실행
        if(GameManager.instance.HaveDataBase.UseItem.Count > SelectNum)
        {
            DataIds thisItemId = GameManager.instance.HaveDataBase.UseItem[SelectNum];//현재 클릭한 아이템 가져오기
            
            //선택한 아이템 칸에 아이템이 있으면 SelectItemId를 해당 아이템ID로 변경
            if (thisItemId != null)
            {
                //선택한 아이템을 다시 선택하면 선택해제
                if (GameManager.instance.SelectItemId == thisItemId.ID)
                {
                    GameManager.instance.SelectItemId = 0;
                }
                else
                {
                    GameManager.instance.SelectItemId = thisItemId.ID;
                }
            }
        }
        else
        {
            GameManager.instance.SelectItemId = 0;//SelectItem 0으로 초기화
        }
    }

    //플레이어 수첩 열고 닫을 때 호출하는 함수
    public void OpenAndCloseNote()
    {
        bool NoteActive = Note.activeSelf;
        if(NoteActive)
            Note.SetActive(false);
        else if (!NoteActive)
            Note.SetActive(true);
    }

    //대화창 띄우는 함수CallObj_Status: 0:안읽음 1:읽음
    public void OpenChatWindow(int LogID, GameObject CallObj = null, int CallObj_Status = 0)
    {
        GameObject Chatlog = Instantiate(ChatLogPre, this.transform);//대화로그 창 생성 및 해당 캔버스로 부모 설정
        
        Chatlog.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0); // 위치값 초기화
        if (CallObj != null)
            Chatlog.GetComponent<ChatWindowController>().CallObj = CallObj; //호출 오브젝트 값 설정
        Chatlog.GetComponent<ChatWindowController>().LogID = LogID; //호출할 대화로그 ID 값 설정
        Chatlog.GetComponent<ChatWindowController>().CallObj_Status = CallObj_Status; //호출 오브젝트 상태값 설정
        
        Chatlog.SetActive(true);//대화창 프리펩 활성화
    }

    //ItemInspector창 띄우는 함수
    public void OpenItemInspectorWindow(int itemType, int itemID)
    {
        if(ItemInspectorPre != null)
        {
            GameObject Prefap = Instantiate(ItemInspectorPre, this.transform);//아이템 인스팩터창 생성

            Prefap.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0); // 위치값 초기화
            Prefap.GetComponent<ItemInspectorController>().ItemType = itemType;//보여줄 아이템 타입 설정
            Prefap.GetComponent<ItemInspectorController>().ItemID = itemID;//보여줄 아이템 아이디 설정
            Prefap.SetActive(true);//프리펩 활성화
        }
    }

    //CutScene창 띄우는 함수
    public void OpenCutSceneWindow(string CutSceneName, GameObject callBackObj = null, string callBackFuntion = null, string parameter = null)
    {
        string CutScenePath = "Prefap/UI/CutScene/"; //컷씬 프리펩 위치

        GameObject CutScenePre = Resources.Load<GameObject>(CutScenePath + CutSceneName);//컷씬 프리펩 가져오기

        //콜백 시 함수 호출 관련 처리
        if (callBackObj != null && callBackFuntion != null)
        {
            CutScenePre.GetComponent<CutSceneController>().CallBackObj = callBackObj;
            CutScenePre.GetComponent<CutSceneController>().CallBackFuntion = callBackFuntion;
            if (parameter != null)
                CutScenePre.GetComponent<CutSceneController>().CallBackParameter = parameter;
        }
        GameObject Prefap = Instantiate(CutScenePre, this.transform);//컷씬 프리펩 생성
    }

    //인게임 메뉴창 띄우기 버튼 이벤트 처리
    public void OnOpenInGameMenu()
    {
        InGameMenu.SetActive(true);//인게임 메뉴창 활성화
    }

    //인게임 메뉴 계속하기 버튼 이벤트 처리
    public void OnContinueBtn()
    {
        InGameMenu.SetActive(false);//인게임 메뉴창 비활성화
    }

    //타이틀 화면으로 나가기
    public void OnGoToTitle()
    {
        Destroy(GameManager.instance.gameObject);//인게임 메니저 삭제
        ChangeScene("MainTitle");//타이틀화면으로 나가기
    }
}
