using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookwarePuzzleClearChecker : MonoBehaviour
{
    int isPuzzleSatus = 0; //해당 퍼즐 완료 여부 0: 미완료 1: 완료

    [SerializeField]
    private string dataName = "puzzle_CookwarePuzzle"; //데이터베이스에 저장될 데이터명

    [SerializeField]
    GameObject GetClearItemObj;//클리어시 활성화할 클리어 아이템 획득 오브젝트

    //요리도구가 담길 배열
    [SerializeField]
    private Transform[] CookwareList;

    //퍼즐 클리어 조건
    [SerializeField]
    private Transform[] ClearCookwaresParents;

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
            //현재 도구함 위치 정보값 개수와 클리어 정보값 개수가 같고, 값이 있어야 비교 진행
            if(CookwareList.Length == ClearCookwaresParents.Length && CookwareList.Length > 0 && ClearCookwaresParents.Length > 0)
            {
                int ClearCnt = 0;//클리어 개수 저장
                //부모값을 비교해서 같으면 ClearCnt++
                for(int i=0; i < CookwareList.Length; i++)
                {
                    if (CookwareList[i].transform.parent.name == ClearCookwaresParents[i].name)
                    {
                        ClearCnt++;
                    }
                        
                }
                if (ClearCnt == ClearCookwaresParents.Length)
                    isPuzzleSatus = 1;
            }
        }
        //이미 클리어 했을 경우 해당 오브젝트 제거 및 클리어 아이템 활성화
        if(isPuzzleSatus == 1)
        {
            GetClearItemObj.SetActive(true);//클리어 아이템 획득 오브젝트 활성화
            Destroy(this.gameObject);//해당 오브젝트 삭제
        }
    }

    private void OnDestroy()
    {
        //해당 퍼즐 상태 값을 데이터 베이스에 저장
        GameManager.instance.ControllObjStatusData(1, dataName, isPuzzleSatus);
    }
}
