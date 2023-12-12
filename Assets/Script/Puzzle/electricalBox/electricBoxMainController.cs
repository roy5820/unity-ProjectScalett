using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class electricBoxMainController : MonoBehaviour
{
    int isElectricBoxSatus = 0; //�ش� ���� �Ϸ� ���� 0: �̿Ϸ� 1: �Ϸ�
    [SerializeField]
    GameObject GetClearItemObj;//Ŭ����� Ȱ��ȭ�� Ŭ���� ������ ȹ�� ������Ʈ
    //�������� �����ϱ����� LightBulb ��ü ����
    [System.Serializable]
    public class LightBulb
    {
        public GameObject bulbObj;//���� ������Ʈ
        public GameObject btnObj;//��ư ������Ʈ
        public int bulbStatus;//���� ���� 1: ���� -1: ����
    }
    [SerializeField]
    List<LightBulb> lightBulbs = new List<LightBulb>();//������ �����ϱ� ���� ����Ʈ ���� �ʱ�ȭ

    [SerializeField]
    private string dataName = "puzzle_electricalPuzzle"; //�����ͺ��̽��� ����� �����͸�

    //���º� ��ȯ�� ������ ��ư���� �̹���
    public Sprite turnOnBulbImg;//���� ���� �̹���
    public Sprite turnOffBulbImg;//���� ���� �̹���
    public Sprite turnOnBtnImg;//���� ��ư �̹���
    public Sprite turnOffBtnImg;//���� ��ư �̹���



    // Start is called before the first frame update
    void Start()
    {
        //�ش� ���� ���� ���� ������ ���̽����� ��������.
        if (dataName != null)
        {
            int getObjStatus = GameManager.instance.ControllObjStatusData(0, dataName, 0);//���� �޴������� ������ ��������
            if (getObjStatus >= 0)
                isElectricBoxSatus = getObjStatus;
        }

        //�����̽� ���� ���� ����
        ChageForSatus();
    }

    //lightBulbs��ü�ȿ� �ִ� Status ������ ������ �̹��� ��ü
    public void ChageForSatus()
    {
        int turnOnBulbCnt = 0;//���� ���� ī��Ʈ
        //���� �̿Ϸ� ������ ���
        if (isElectricBoxSatus == 0)
        {
            //���� ���º��� ���� �̹��� ��ü
            foreach (LightBulb bulb in lightBulbs)
            {
                //���� ����
                if (bulb.bulbStatus == 1)
                {
                    turnOnBulbCnt++;
                    bulb.bulbObj.GetComponent<Image>().sprite = turnOnBulbImg;
                    bulb.btnObj.GetComponent<Image>().sprite = turnOnBtnImg;
                }
                //���� ����
                else
                {
                    bulb.bulbObj.GetComponent<Image>().sprite = turnOffBulbImg;
                    bulb.btnObj.GetComponent<Image>().sprite = turnOffBtnImg;
                }
            }

            //������ ���� ������ ���� �Ϸ� ���� ��ȯ
            if (turnOnBulbCnt == lightBulbs.Count)
                isElectricBoxSatus = 1;
        }
        //���� �Ϸ� ������ ���
        if(isElectricBoxSatus == 1)
        {
            GetClearItemObj.SetActive(true);//Ŭ���� ������ ȹ�� ������Ʈ Ȱ��ȭ
            Destroy(this.gameObject);//�ش� ������Ʈ ����
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
        //�ش� ���� ���� ���� ������ ���̽��� ����
        GameManager.instance.ControllObjStatusData(1, dataName, isElectricBoxSatus);
    }
}
