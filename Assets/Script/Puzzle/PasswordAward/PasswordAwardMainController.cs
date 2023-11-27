using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PasswordAwardMainController : MonoBehaviour
{
    int isPasswordAwardSatus = 0; //해당 퍼즐 완료 여부 0: 미완료 1: 완료
    [SerializeField]
    string clearPassword = "2523";//클리어 비밀번호

    [SerializeField]
    GameObject GetClearItemObj;//클리어시 활성화할 클리어 아이템 획득 오브젝트
    //전구들을 관리하기위한 LightBulb 객체 선언
    [System.Serializable]
    public class NumberBoard
    {
        public GameObject numObj;
        public int isNum;//현재 입력된 숫자
    }

    [SerializeField]
    List<NumberBoard> numBoards = new List<NumberBoard>();//숫자 판을 관리하기 위한 리스트 변수 초기화

    [SerializeField]
    private string dataName = "puzzle_passwordPuzzle"; //데이터베이스에 저장될 데이터명

    // Start is called before the first frame update
    void Start()
    {
        //해당 퍼즐 상태 값을 데이터 베이스에서 가져오기.
        if (dataName != null)
        {
            int getObjStatus = GameManager.instance.ControllObjStatusData(0, dataName, 0);//게임 메니저에서 데이터 가져오기
            if (getObjStatus >= 0)
                isPasswordAwardSatus = getObjStatus;
        }

        //numBoards안에 isNum값을 UI에 반영 하는 함수 호출
        ChageForNum();
    }

    //numBoards안에 isNum값을 UI에 반영 하는 함수
    public void ChageForNum()
    {
        //현재 입력된 비밀번호
        string inputPassword = "";
        //퍼즐 미완료 상태일 경우
        if (isPasswordAwardSatus == 0)
        {
            //numBoards안에 숫자값 UI에 반영
            foreach (NumberBoard thisBoard in numBoards)
            {
                //비밀번호 일치 여부를 확인을 위한 현제 입력된 번호가 저장되는 변수
                inputPassword += thisBoard.isNum.ToString();

                thisBoard.numObj.GetComponent<Text>().text = thisBoard.isNum.ToString();
            }

            //전구가 전부 켜진경 퍼즐 완료 상대로 전환
            if (inputPassword == clearPassword)
                isPasswordAwardSatus = 1;
        }
        //퍼즐 완료 상태일 경우
        if(isPasswordAwardSatus == 1)
        {
            GetClearItemObj.SetActive(true);//클리어 아이템 획득 오브젝트 활성화
            Destroy(this.gameObject);//해당 오브젝트 삭제
        }
    }

    //숫자 Up 버튼 이벤트 처리
    public void OnUpNum(int numIndex)
    {
        if(numBoards[numIndex] != null)
        {
            //숫자가 9일 경우 0으로 초기화
            if (numBoards[numIndex].isNum == 9)
                numBoards[numIndex].isNum = 0;
            else
                numBoards[numIndex].isNum++;
        }
        //numBoards안에 isNum값을 UI에 반영 하는 함수 호출
        ChageForNum();
    }

    //숫자 Down 버튼 이벤트 처리
    public void OnDownNum(int numIndex)
    {
        if (numBoards[numIndex] != null)
        {
            //숫자가 9일 경우 0으로 초기화
            if (numBoards[numIndex].isNum == 0)
                numBoards[numIndex].isNum = 0;
            else
                numBoards[numIndex].isNum--;
        }
        //numBoards안에 isNum값을 UI에 반영 하는 함수 호출
        ChageForNum();
    }

    private void OnDestroy()
    {
        //해당 퍼즐 상태 값을 데이터 베이스에 저장
        GameManager.instance.ControllObjStatusData(1, dataName, isPasswordAwardSatus);
    }
}
