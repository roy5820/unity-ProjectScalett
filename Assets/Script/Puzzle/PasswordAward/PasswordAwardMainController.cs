using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PasswordAwardMainController : MonoBehaviour
{
    int isPasswordAwardSatus = 0; //�ش� ���� �Ϸ� ���� 0: �̿Ϸ� 1: �Ϸ�
    [SerializeField]
    string clearPassword = "2523";//Ŭ���� ��й�ȣ

    [SerializeField]
    int GetClearItemID;//Ŭ����� ȹ�� ������ ID

    [SerializeField]
    int GetClearItemType;//Ŭ����� ȹ�� ������ Ÿ��

    //�������� �����ϱ����� LightBulb ��ü ����
    [System.Serializable]
    public class NumberBoard
    {
        public GameObject numObj;
        public int isNum;//���� �Էµ� ����
    }

    [SerializeField]
    List<NumberBoard> numBoards = new List<NumberBoard>();//���� ���� �����ϱ� ���� ����Ʈ ���� �ʱ�ȭ

    [SerializeField]
    private string dataName = "puzzle_passwordPuzzle"; //�����ͺ��̽��� ����� �����͸�
    // Start is called before the first frame update
    void Start()
    {
        //�ش� ���� ���� ���� ������ ���̽����� ��������.
        if (dataName != null)
        {
            int getObjStatus = GameManager.instance.ControllObjStatusData(0, dataName, 0);//���� �޴������� ������ ��������
            if (getObjStatus >= 0)
                isPasswordAwardSatus = getObjStatus;
        }

        //numBoards�ȿ� isNum���� UI�� �ݿ� �ϴ� �Լ� ȣ��
        ChageForNum();
    }

    //numBoards�ȿ� isNum���� UI�� �ݿ� �ϴ� �Լ�
    public void ChageForNum()
    {
        //���� �Էµ� ��й�ȣ
        string inputPassword = "";
        //���� �̿Ϸ� ������ ���
        if (isPasswordAwardSatus == 0)
        {
            //numBoards�ȿ� ���ڰ� UI�� �ݿ�
            foreach (NumberBoard thisBoard in numBoards)
            {
                //��й�ȣ ��ġ ���θ� Ȯ���� ���� ���� �Էµ� ��ȣ�� ����Ǵ� ����
                inputPassword += thisBoard.isNum.ToString();

                thisBoard.numObj.GetComponent<Text>().text = thisBoard.isNum.ToString();
            }

            //������ ���� ������ ���� �Ϸ� ���� ��ȯ
            if (inputPassword == clearPassword)
                isPasswordAwardSatus = 1;
        }
        //���� �Ϸ� ������ ���
        if(isPasswordAwardSatus == 1)
        {
            //�ش� ���� ���� ���� ������ ���̽��� ����
            GameManager.instance.ControllObjStatusData(1, dataName, isPasswordAwardSatus);
            GameManager.instance.AddItem(GetClearItemType, GetClearItemID);//������ ȹ��
            PlayerUIController.instance.OpenItemInspectorWindow(GetClearItemType, GetClearItemID);
            this.gameObject.SetActive(false);
        }
    }

    //���� Up ��ư �̺�Ʈ ó��
    public void OnUpNum(int numIndex)
    {
        if(numBoards[numIndex] != null)
        {
            //���ڰ� 9�� ��� 0���� �ʱ�ȭ
            if (numBoards[numIndex].isNum == 9)
                numBoards[numIndex].isNum = 0;
            else
                numBoards[numIndex].isNum++;
        }
        //numBoards�ȿ� isNum���� UI�� �ݿ� �ϴ� �Լ� ȣ��
        ChageForNum();
    }

    //���� Down ��ư �̺�Ʈ ó��
    public void OnDownNum(int numIndex)
    {
        if (numBoards[numIndex] != null)
        {
            //���ڰ� 9�� ��� 0���� �ʱ�ȭ
            if (numBoards[numIndex].isNum == 0)
                numBoards[numIndex].isNum = 9;
            else
                numBoards[numIndex].isNum--;
        }
        //numBoards�ȿ� isNum���� UI�� �ݿ� �ϴ� �Լ� ȣ��
        ChageForNum();
    }

    //�ش� �˾�â�� �ݴ� �Լ�
    public void CloseThisPopup()
    {
        this.gameObject.SetActive(false);
    }
}
