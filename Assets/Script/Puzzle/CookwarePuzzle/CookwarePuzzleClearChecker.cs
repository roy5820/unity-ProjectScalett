using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookwarePuzzleClearChecker : MonoBehaviour
{
    int isPuzzleSatus = 0; //�ش� ���� �Ϸ� ���� 0: �̿Ϸ� 1: �Ϸ�

    [SerializeField]
    private string dataName = "puzzle_CookwarePuzzle"; //�����ͺ��̽��� ����� �����͸�

    [SerializeField]
    GameObject GetClearItemObj;//Ŭ����� Ȱ��ȭ�� Ŭ���� ������ ȹ�� ������Ʈ

    //�丮������ ��� �迭
    [SerializeField]
    private Transform[] CookwareList;

    //���� Ŭ���� ����
    [SerializeField]
    private Transform[] ClearCookwaresParents;

    void Start()
    {
        //�ش� ���� ���� ���� ������ ���̽����� ��������.
        if (dataName != null)
        {
            int getObjStatus = GameManager.instance.ControllObjStatusData(0, dataName, 0);//���� �޴������� ������ ��������
            if (getObjStatus >= 0)
                isPuzzleSatus = getObjStatus;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //���� �ϷῩ�� �ǽð� üũ
        //CookwareList���� �������� �θ��� ClearCookwaresParents�ȿ� ���ϰ� ���Ͽ� ������ Ŭ����
        if (isPuzzleSatus == 0)
        {
            //���� ������ ��ġ ������ ������ Ŭ���� ������ ������ ����, ���� �־�� �� ����
            if(CookwareList.Length == ClearCookwaresParents.Length && CookwareList.Length > 0 && ClearCookwaresParents.Length > 0)
            {
                int ClearCnt = 0;//Ŭ���� ���� ����
                //�θ��� ���ؼ� ������ ClearCnt++
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
        //�̹� Ŭ���� ���� ��� �ش� ������Ʈ ���� �� Ŭ���� ������ Ȱ��ȭ
        if(isPuzzleSatus == 1)
        {
            GetClearItemObj.SetActive(true);//Ŭ���� ������ ȹ�� ������Ʈ Ȱ��ȭ
            Destroy(this.gameObject);//�ش� ������Ʈ ����
        }
    }

    private void OnDestroy()
    {
        //�ش� ���� ���� ���� ������ ���̽��� ����
        GameManager.instance.ControllObjStatusData(1, dataName, isPuzzleSatus);
    }
}
