using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventUponEntrySystemController : MonoBehaviour
{
    public int EventType; //0: 대화로그, 1: 컷씬

    public string CutScenePre_Name; //컷씬 프리펩 이름
    public int ChatLogID;//대화 로그

    private int isStatus = 0;//오브젝트 상태 값 0:미사용 1: 사용
    private string dataName = null; //데이터베이스에 저장될 데이터명

    private void Start()
    {
        //데이터명을 현제 씬 이름 + 오브젝트 이름으로 초기화
        dataName = SceneManager.GetActiveScene().name + "_" + gameObject.name + "_Event";

        //이벤트 사용여부 가져오기
        if (dataName != null)
        {
            int getObjStatus = GameManager.instance.ControllObjStatusData(0, dataName, 0);//게임 메니저에서 데이터 가져오기
            if (getObjStatus >= 0)
                isStatus = getObjStatus;
        }

        //처음 입장 시 이벤트 타입에 따라 이벤트 처리
        if (isStatus == 0)
        {
            isStatus = 1;//상태값 변경
            //스테이터스를 상태 값을 데이터 베이스에 저장
            GameManager.instance.ControllObjStatusData(1, dataName, isStatus);

            if (EventType == 0)
            {
                PlayerUIController.instance.OpenChatWindow(ChatLogID, null);//대화창 띄우기
            }
            else if (EventType == 1)
            {
                PlayerUIController.instance.OpenCutSceneWindow(CutScenePre_Name);//컷씬 띄우기
            }
        }
    }
}
