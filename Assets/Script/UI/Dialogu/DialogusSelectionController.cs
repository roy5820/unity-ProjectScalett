using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class DialogusSelectionController : MonoBehaviour
{
    //대화버튼에 대한 정보를 갖는 객체
    [System.Serializable]
    public class TalkButtonData
    {
        public int ButtonIndex;//버튼인덱스 번호
        public GameObject Button;//버튼 게임오브젝트
        public int KeyID;//키 아이템 ID
        public int LogID;//불러올 대화 LogID
        public int GetTestimonyID;//획득할 증언 ID
        public int isStatus = 0;//버튼 상태값 0:안읽음 1: 읽음 2: 잠김
    }

    //대화버튼 정보들을 갖는 리스트 객체
    [System.Serializable]
    public class TalkButtonDatas
    {
        public List<TalkButtonData> TalkButtonData;
    }

    public TalkButtonDatas ButtonDatas;//대화버튼 데이터를
    private string dataName = null; //데이터베이스에 저장될 데이터명

    private string ImgPath = "Image/UI/Button/";//버튼 이미지 가져올 경로

    private void Start()
    {
        //데이터명을 현제 씬 이름 + 오브젝트 이름으로 초기화
        dataName = SceneManager.GetActiveScene().name + "_" + gameObject.name + "_DialogusSelectionController_";

        //람다식을 사용한 버튼 이벤트 리스너 부여
        foreach (TalkButtonData ButtonData in ButtonDatas.TalkButtonData)
        {
            Button thisButton = ButtonData.Button.GetComponent<Button>();
            thisButton.onClick.AddListener(delegate { OnChatWindowOpen(ButtonData); });//버튼 
        }

        LoardButtonStatus();
    }

    private void OnEnable()
    {
        LoardButtonStatus();
    }

    //버튼 클릭시 대화창을 띄우는 함수
    //BtnIndex: 버튼 인덱스 번호, LogID: 불러올 대화로그의 ID값
    public void OnChatWindowOpen(TalkButtonData BtnData)
    {
        //각 상태별 이벤트 처리
        if (BtnData.isStatus == 0)
        {
            GameManager.instance.ControllObjStatusData(1, dataName + BtnData.ButtonIndex, 1);//버튼 상태를 1(읽음)으로 설정
            //획득할 증언이 존재할 경우 인벤토리에 추가
            if (BtnData.GetTestimonyID > 0)
                GameManager.instance.AddItem(3, BtnData.GetTestimonyID);
        }
        else if (BtnData.isStatus == 2)
        {
                return;//잠겨있을 시 함수 종료
        }

        PlayerUIController.instance.OpenChatWindow(BtnData.LogID, this.gameObject, BtnData.isStatus);//대화창 오픈
        this.gameObject.SetActive(false);//화면 비활성화
    }

    //버튼의 상태값 초기화 하는 함수
    public void LoardButtonStatus()
    {
        //대화창이 활성화 될때 마다 버튼들의 이미지 및 상태값 초기화하는 부분
        foreach (TalkButtonData ButtonData in ButtonDatas.TalkButtonData)
        {
            //버튼들의 상태값을 데이터베이스에서 가져와 초기화 하는 부분 
            int getStatus = GameManager.instance.ControllObjStatusData(0, dataName + ButtonData.ButtonIndex, 0);//데이터 명 뒤에 버튼의 인덱스번호 붙여서 데이터가져오기
            if (getStatus >= 0)
            {
                //키 아이템을 가지고 있을 시 잠김(2) 상태의 대화버튼 안읽음(0) 상태로 설정
                if (getStatus == 2 && GameManager.instance.ChkItem(1, ButtonData.KeyID)) //key Item이 인벤토리에 있는 지 체크
                {
                    GameManager.instance.ControllObjStatusData(1, dataName + ButtonData.ButtonIndex, 1);//버튼 상태를 1(읽음)으로 설정
                    getStatus = 0;
                }

                ButtonData.isStatus = getStatus;
            }



            //버튼들 상태별 이미지 적용
            if (ButtonData.isStatus > 0)
            {
                Sprite ButtonImg = null;

                if (ButtonData.isStatus == 1)
                    ButtonImg = Resources.Load<Sprite>(ImgPath + "ChatSelectButton_read");//버튼 이미지 가져오기
                else if (ButtonData.isStatus == 2)
                    ButtonImg = Resources.Load<Sprite>(ImgPath + "ChatSelectButton_lock");//버튼 이미지 가져오기
                if (ButtonImg != null)
                    ButtonData.Button.GetComponent<Image>().sprite = ButtonImg; //버튼 이미지 적용
            }

        }
    }

    //대화선택창 닫기 이벤트
    public void OnSelectWindowClose()
    {
        Destroy(this.gameObject);
    }
}
