using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneController : MonoBehaviour
{
    //컷씬 정보들을 가질 변수
    [System.Serializable]
    public class CutScene
    {
        public int ChatLogID;//대화로그
        public bool isOpenLog = false;//대화로그 오픈 여부
        public string CutSceneImgName; //컷씬이미지 이름
    }

    //화면에 띄울 컷씬들을 화면들의 정보가 저장되는 변수
    [SerializeField]
    private List<CutScene> CutScenes;
    int nowCutSceneNum = 0;//현재 컷씬 번호

    [SerializeField]
    private GameObject CutSceneImgObj;//컷씬 이미지가 반영될 오브젝트
    string CutSceneImgPath = "Image/Illustration/";

    //콜백 시 함수 호출
    public GameObject CallBackObj = null;
    public string CallBackFuntion = null;
    public string CallBackParameter = null;

    private void Start()
    {
        //null 체크
        if(CutScenes.Count > 0)
        {
            UpdateCutSceneImg();
        }
    }

    public void OnChatLog()
    {
        //대화로그 열었는지 여부에 따라 다음 컷씨으로 넘길지 로그를 띄울지 결정
        if (!CutScenes[nowCutSceneNum].isOpenLog)
        {
            CutScenes[nowCutSceneNum].isOpenLog = true;//대화로그 열었는지 여부 true로 변경
            //빈값 체크
            if (CutScenes[nowCutSceneNum].ChatLogID > 0)
            {
                PlayerUIController.instance.OpenChatWindow(CutScenes[nowCutSceneNum].ChatLogID, this.gameObject);//대화창 띄우기
            }
            else
            {
                //다음 컷씬으로 넘기고 이미지 반영
                nowCutSceneNum++;
                UpdateCutSceneImg();
            }
        }
        else
        {
            //다음 컷씬으로 넘기고 이미지 반영
            nowCutSceneNum++;
            if(nowCutSceneNum < CutScenes.Count)
                UpdateCutSceneImg();
            else
                Destroy(gameObject);
        }
    }

    //nowCutSceneNum 위치에 존재하는 이미지로 컷씬 이미지 교체
    public void UpdateCutSceneImg()
    {
        //컷씬 이미지 가져오기
        Sprite CutSceneImg = Resources.Load<Sprite>(CutSceneImgPath + CutScenes[nowCutSceneNum].CutSceneImgName);
        
        //컷씬 이미지 적용
        CutSceneImgObj.GetComponent<Image>().sprite = CutSceneImg;
    }

    private void OnDestroy()
    {
        if (CallBackObj != null && CallBackFuntion != null)
        {
            if(CallBackParameter != null)
                CallBackObj.SendMessage(CallBackFuntion, CallBackParameter);
            else
                CallBackObj.SendMessage(CallBackFuntion);
        }
            
    }
}
