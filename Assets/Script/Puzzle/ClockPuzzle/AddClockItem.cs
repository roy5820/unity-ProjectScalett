using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddClockItem : MonoBehaviour
{
    [SerializeField]
    private string dataName = "puzzle_ClockPuzzle_AddItem"; //데이터베이스에 저장될 데이터명

    //시계 분, 시 침 활성화 여부
    int AddItem1 = 0; //분침
    int AddItem2 = 0; //시침

    //활성화할 시, 분침 오브젝트
    public GameObject HourHourHand;
    public GameObject MinuteHand;

    void Start()
    {
        //해당 퍼즐 상태 값을 데이터 베이스에서 가져오기.
        if (dataName != null)
        {
            int getAddItem1Status = GameManager.instance.ControllObjStatusData(0, dataName+1, 0);//게임 메니저에서 데이터 가져오기
            if (getAddItem1Status >= 0)
                AddItem1 = getAddItem1Status;

            int getAddItem2Status = GameManager.instance.ControllObjStatusData(0, dataName + 2, 0);//게임 메니저에서 데이터 가져오기
            if (getAddItem2Status >= 0)
                AddItem1 = getAddItem2Status;
        }
    }

    private void Update()
    {
        if(AddItem1 == 1)
        {
            MinuteHand.SetActive(true);
        }
        else if(AddItem2 == 2)
        {
            HourHourHand.SetActive(true);
        }
    }

    //선택한 사용아이템에 따라 시,분침 활성화 함수
    public void OnAddItem()
    {
        int thisItem = GameManager.instance.SelectItemId;
        //분침 활성화
        if (thisItem == 102)
        {
            AddItem1 = 1;
        }
        //시침 활성화
        else if(thisItem == 104)
        {
            AddItem2 = 2;

        }
    }

    private void OnDestroy()
    {
        //해당 퍼즐 상태 값을 데이터 베이스에 저장
        GameManager.instance.ControllObjStatusData(1, dataName + 1, AddItem1);
        GameManager.instance.ControllObjStatusData(1, dataName + 2, AddItem2);
    }
}
