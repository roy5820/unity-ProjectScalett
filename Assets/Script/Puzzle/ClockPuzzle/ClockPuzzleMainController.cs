using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClockPuzzleMainController : MonoBehaviour
{
    int isPuzzleSatus = 0; //해당 퍼즐 완료 여부 0: 미완료 1: 완료

    [SerializeField]
    private string dataName = "puzzle_ClockPuzzle"; //데이터베이스에 저장될 데이터명

    [SerializeField]
    string ClearSceneName = "";//클리어 시 이동할 씬 이름

    [SerializeField]
    GameObject[] clockHands;//시계 침

    [SerializeField]
    float[] clearLotateZ;//시계 침별 클리어 회전값

    // Start is called before the first frame update
    void Start()
    {
        //해당 퍼즐 상태 값을 데이터 베이스에서 가져오기.
        if (dataName != null)
        {
            int getObjStatus = GameManager.instance.ControllObjStatusData(0, dataName, 0);//게임 메니저에서 데이터 가져오기
            if (getObjStatus >= 0)
                isPuzzleSatus = getObjStatus;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //퍼즐 완료여부 실시간 체크
        //CookwareList안의 도구들의 부모값을 ClearCookwaresParents안에 값하고 비교하여 맞으면 클리어
        if (isPuzzleSatus == 0)
        {
            //각 시계 침 들의 각도를 클리어 침 각도와 비교하여 모두 맞으면 isPuzzleSatus를 true로 전환
            //null체크
            if (clockHands.Length > 0 && clearLotateZ.Length > 0 && clockHands.Length == clearLotateZ.Length)
            {
                int clearCnt = 0;
                for(int i = 0; i < clockHands.Length; i++)
                {
                    //시계 침의 각도와 클리어 각도가 같으면 
                    if (clockHands[i].transform.rotation.eulerAngles.z == clearLotateZ[i])
                        clearCnt++;
                }
                //모든 시침이 정답을 가르키면 정답
                if (clearCnt == clearLotateZ.Length)
                    isPuzzleSatus = 1;
            }
        }

        //이미 클리어 했을 경우 해당 오브젝트 제거 및 클리어 아이템 활성화
        if (isPuzzleSatus == 1)
        {
            SceneManager.LoadScene(ClearSceneName);
        }
    }

    private void OnDestroy()
    {
        //해당 퍼즐 상태 값을 데이터 베이스에 저장
        GameManager.instance.ControllObjStatusData(1, dataName, isPuzzleSatus);
    }
}
