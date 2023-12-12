using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClockPuzzleMainController : MonoBehaviour
{
    int isPuzzleSatus = 0; //�ش� ���� �Ϸ� ���� 0: �̿Ϸ� 1: �Ϸ�

    [SerializeField]
    private string dataName = "puzzle_ClockPuzzle"; //�����ͺ��̽��� ����� �����͸�

    [SerializeField]
    string ClearSceneName = "";//Ŭ���� �� �̵��� �� �̸�

    [SerializeField]
    GameObject[] clockHands;//�ð� ħ

    [SerializeField]
    float[] clearLotateZ;//�ð� ħ�� Ŭ���� ȸ����

    // Start is called before the first frame update
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
            //�� �ð� ħ ���� ������ Ŭ���� ħ ������ ���Ͽ� ��� ������ isPuzzleSatus�� true�� ��ȯ
            //nullüũ
            if (clockHands.Length > 0 && clearLotateZ.Length > 0 && clockHands.Length == clearLotateZ.Length)
            {
                int clearCnt = 0;
                for(int i = 0; i < clockHands.Length; i++)
                {
                    //�ð� ħ�� ������ Ŭ���� ������ ������ 
                    if (clockHands[i].transform.rotation.eulerAngles.z == clearLotateZ[i])
                        clearCnt++;
                }
                //��� ��ħ�� ������ ����Ű�� ����
                if (clearCnt == clearLotateZ.Length)
                    isPuzzleSatus = 1;
            }
        }

        //�̹� Ŭ���� ���� ��� �ش� ������Ʈ ���� �� Ŭ���� ������ Ȱ��ȭ
        if (isPuzzleSatus == 1)
        {
            SceneManager.LoadScene(ClearSceneName);
        }
    }

    private void OnDestroy()
    {
        //�ش� ���� ���� ���� ������ ���̽��� ����
        GameManager.instance.ControllObjStatusData(1, dataName, isPuzzleSatus);
    }
}
