
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class placeThingsController : MonoBehaviour
{
    int isPlaceTingsSatus = 0; //물건 놓기 여부 0: 미완료 1: 완료

    [SerializeField]
    GameObject PlaceObj;//놓을 물건 오브젝트

    [SerializeField]
    int PlaceItemID = 101;//놓을 물건 오브젝트

    [SerializeField]
    private string dataName = "placeNewsPaper"; //데이터베이스에 저장될 데이터명

    // Start is called before the first frame update
    void Start()
    {
        //해당 퍼즐 상태 값을 데이터 베이스에서 가져오기.
        if (dataName != null)
        {
            int getObjStatus = GameManager.instance.ControllObjStatusData(0, dataName, 0);//게임 메니저에서 데이터 가져오기
            if (getObjStatus >= 0)
                isPlaceTingsSatus = getObjStatus;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //물건이 놓여있는지에 따른 이벤트 처리
        if (isPlaceTingsSatus == 1)
        {
            GameManager.instance.DelItem(0, PlaceItemID);
            PlaceObj.SetActive(true);
            Destroy(this.gameObject);
        }
    }

    //클릭시 사용아이템 칸에 놓을 아이템이 있을 시 PlaceObj 활성화
    public void OnPlaceObj()
    {
        if(GameManager.instance.SelectItemId == PlaceItemID)
        {
            isPlaceTingsSatus = 1;
        }
    }

    private void OnDestroy()
    {
        GameManager.instance.ControllObjStatusData(1, dataName, isPlaceTingsSatus);
    }
}
