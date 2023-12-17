using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLockPopup : MonoBehaviour
{
    int isPasswordAwardSatus = 0; //Lock 퍼즐 완료 여부 0: 미완료 1: 완료
    [SerializeField]
    private string dataName = "puzzle_passwordPuzzle"; //데이터베이스에 저장될 데이터명
    [SerializeField]
    private int EmptyMessage = 5003;//Lock퍼즐을 이미 클리어 했을 시 출력 되는 메시지
    [SerializeField]
    private GameObject LockPuzzleObj;//Lock퍼즐 오브젝트
    private void Start()
    {
        LoadData();
    }

    public void LoadData()
    {
        //해당 퍼즐 상태 값을 데이터 베이스에서 가져오기.
        if (dataName != null)
        {
            int getObjStatus = GameManager.instance.ControllObjStatusData(0, dataName, 0);//게임 메니저에서 데이터 가져오기
            if (getObjStatus >= 0)
                isPasswordAwardSatus = getObjStatus;
        }
    }

    //Lock퍼즐 호출하는 함수
    public void OnOpenLockPuzzlePopup()
    {
        LoadData();
        if (isPasswordAwardSatus > 0)
            PlayerUIController.instance.OpenChatWindow(EmptyMessage);
        else
            LockPuzzleObj.SetActive(true);
            
    }
}
