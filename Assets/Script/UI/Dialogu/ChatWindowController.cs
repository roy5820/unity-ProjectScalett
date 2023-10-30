using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class ChatWindowController : MonoBehaviour
{
    public GameObject CallObj;//해당 오브젝트를 호출한 오브젝트
    public int LogID = 0;//대화 로그아이디
    public int CallObj_Status;//호출한 오브젝트의 상태값

    public Line[] TalkLines = null; //대화로그가 저장될 배열변수 - 해당 프리펩을 생성하는 곳에서 TalkLines값을 설정해줘야함
    public int ThisLineNum; //현제 볼 Line 배열의 번호
    string ImgPath = "Image/ConversationImg/"; //캐릭터 이미지 경로

    public GameObject TalkerImgBox;//말하는 캐릭터 이미지가 드러갈 이미지
    public GameObject TalkerNameBox;//말하는 캐릭터의 이름이 드러갈 이름
    public GameObject ChatTextBox;//말하는 내용이 드라갈 텍스트 박스

    private bool isPrinting = false; //글자 출력 상태 여부
    private bool isGetItem = false; //아이템 획득 여부

    private void OnEnable()
    {
        TalkLines = TalkLogDataManager.instance.GetTalkLogByID(LogID); //로그 아이디로 대화내용 가져오기

        ThisLineNum = 0; //라인 번호 0으로 초기화
        UpdateChatLog(ThisLineNum);//대화로그 화면에 뿌리기
    }

    private void Update()
    {
        //프린팅 상태가 아니면서 아이템 획득 상태가 아니면서, CallObj상태가 처음 읽음일 경우에만 아이템 획득 이벤트 상태
        if (!isPrinting && !isGetItem && CallObj_Status == 0)
        {
            isGetItem = true;//아이템 획득여부 true로 설정
            //현재 라인에 getItemID 가져오기
            Line getLine = TalkLines[ThisLineNum];

            //getItemID가 있으면 아이템 획득 이벤트 구현
            if(getLine.getItemID > 0)
            {
                //아이템 종류별 이벤트 처리
                GameManager.instance.AddItem(getLine.getItemType, getLine.getItemID);

                //아이템 획득 시 아이템 인스펙터창 띄우기
                PlayerUIController.instance.OpenItemInspectorWindow(getLine.getItemType, getLine.getItemID);
            }
        }
    }

    public void UpdateChatLog(int NowLineNum)
    {
        //null체크
        if (TalkLines != null && TalkLines.Length > NowLineNum)
        {
            //이미지를 가져와 있으면 적용
            Sprite TalkerSprite = Resources.Load<Sprite>(ImgPath + TalkLines[NowLineNum].spriteName);//이미지 가져오기
            if (TalkerSprite != null)//null 체크
                TalkerImgBox.GetComponent<Image>().sprite = TalkerSprite;//이미지 적용
            else
                TalkerImgBox.GetComponent<Image>().sprite = null;

            //말하는 사람 이름 데이터가 있으면 적용
            string TalkerName = ItemDataManager.instance.GetCharcterByID(TalkLines[NowLineNum].speakerId).name;
            if (TalkerName != null)//null 체크
                TalkerNameBox.GetComponent<Text>().text = TalkerName;
            else
                TalkerNameBox.GetComponent<Text>().text = null;

            //대화 로그를 가져와 데이터가 있으면 적용
            string TalkLog = TalkLines[NowLineNum].log;
            if (TalkLog != null)//null 체크
                StartCoroutine(PrintText(TalkLog, 0.025f));
            else
                ChatTextBox.GetComponent<Text>().text = null;
        }
        //데이터가 없으면 자동으로 삭제
        else
        {
            Destroy(this.gameObject);
        }
    }

    //글자를 하나씩 출력하는 코루틴
    IEnumerator PrintText(string Text, float PrintingOneTextSpeed)
    {
        isPrinting = true;
        int TextArraayCnt = 0;//텍스트 배열에 현재 삽입된 글자 계수
        string TextArraay = null; //텍스트 배열
        while (TextArraayCnt < Text.Length)
        {
            //프린팅 상태 체크해서 프린팅 상태가 아니면 모든 텍스트 한번에 출력 및 종류
            if (!isPrinting)
            {
                if (Text != null)//null 체크
                    TextArraay = Text;
                TextArraayCnt = Text.Length;
            }
            else//프린팅 상태일 경우 Text안에 글자 하나를 TextArray안에 추가
            {
                TextArraay += Text[TextArraayCnt];
            }

            ChatTextBox.GetComponent<Text>().text = TextArraay;//텍스트 배열 안에 텍스트를 홤면에 출력
            TextArraayCnt++;
            yield return new WaitForSeconds(PrintingOneTextSpeed);
        }
        isPrinting = false;
    }

    //클릭시 호출되는 대화로그 전환 함수
    public void OnChangeChatLog()
    {
        //글자 추력중에는 출력중인 텍스트 모두 한번에 출력
        if (isPrinting)
        {
            isPrinting = false;
            return;
        }

        //다음 페이지 존제 시 라인번호 1증가 시키고 로그 전환
        if (ThisLineNum < TalkLines.Length - 1)
        {
            isGetItem = false;//아이템 획득여부 false로 설정
            ThisLineNum++;
            UpdateChatLog(ThisLineNum);
        }
        //다음페이지 존재하지 않을 시 삭제
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void OnDestroy()
    {
        if (CallObj != null)
        {
            //오브젝트 삭제 시 대화 선택창 다시 키우기
            CallObj.SetActive(true);
        }
    }
}