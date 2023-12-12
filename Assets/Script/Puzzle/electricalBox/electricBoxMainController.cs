using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class electricBoxMainController : MonoBehaviour
{
    int isElectricBoxSatus = 0; //해당 퍼즐 완료 여부 0: 미완료 1: 완료
    [SerializeField]
    GameObject GetClearItemObj;//클리어시 활성화할 클리어 아이템 획득 오브젝트
    //전구들을 관리하기위한 LightBulb 객체 선언
    [System.Serializable]
    public class LightBulb
    {
        public GameObject bulbObj;//전구 오브젝트
        public GameObject btnObj;//버튼 오브젝트
        public int bulbStatus;//전구 상태 1: 켜짐 -1: 꺼짐
    }
    [SerializeField]
    List<LightBulb> lightBulbs = new List<LightBulb>();//전구를 관리하기 위한 리스트 변수 초기화

    [SerializeField]
    private string dataName = "puzzle_electricalPuzzle"; //데이터베이스에 저장될 데이터명

    //상태별 전환할 전구와 버튼들의 이미지
    public Sprite turnOnBulbImg;//켜진 전구 이미지
    public Sprite turnOffBulbImg;//꺼진 전구 이미지
    public Sprite turnOnBtnImg;//켜진 버튼 이미지
    public Sprite turnOffBtnImg;//꺼진 버튼 이미지



    // Start is called before the first frame update
    void Start()
    {
        //해당 퍼즐 상태 값을 데이터 베이스에서 가져오기.
        if (dataName != null)
        {
            int getObjStatus = GameManager.instance.ControllObjStatusData(0, dataName, 0);//게임 메니저에서 데이터 가져오기
            if (getObjStatus >= 0)
                isElectricBoxSatus = getObjStatus;
        }

        //스테이스 별로 상태 변경
        ChageForSatus();
    }

    //lightBulbs객체안에 있는 Status 값으로 전구들 이미지 교체
    public void ChageForSatus()
    {
        int turnOnBulbCnt = 0;//켜진 전구 카운트
        //퍼즐 미완료 상태일 경우
        if (isElectricBoxSatus == 0)
        {
            //전구 상태별로 전구 이미지 교체
            foreach (LightBulb bulb in lightBulbs)
            {
                //켜진 상태
                if (bulb.bulbStatus == 1)
                {
                    turnOnBulbCnt++;
                    bulb.bulbObj.GetComponent<Image>().sprite = turnOnBulbImg;
                    bulb.btnObj.GetComponent<Image>().sprite = turnOnBtnImg;
                }
                //꺼진 상태
                else
                {
                    bulb.bulbObj.GetComponent<Image>().sprite = turnOffBulbImg;
                    bulb.btnObj.GetComponent<Image>().sprite = turnOffBtnImg;
                }
            }

            //전구가 전부 켜진경 퍼즐 완료 상대로 전환
            if (turnOnBulbCnt == lightBulbs.Count)
                isElectricBoxSatus = 1;
        }
        //퍼즐 완료 상태일 경우
        if(isElectricBoxSatus == 1)
        {
            GetClearItemObj.SetActive(true);//클리어 아이템 획득 오브젝트 활성화
            Destroy(this.gameObject);//해당 오브젝트 삭제
        }
    }

    //
    public void OnChageBulb(int bulbIndex)
    {
        if(lightBulbs[bulbIndex] != null)
        {
            lightBulbs[bulbIndex].bulbStatus *= -1;
        }
    }

    private void OnDestroy()
    {
        //해당 퍼즐 상태 값을 데이터 베이스에 저장
        GameManager.instance.ControllObjStatusData(1, dataName, isElectricBoxSatus);
    }
}
