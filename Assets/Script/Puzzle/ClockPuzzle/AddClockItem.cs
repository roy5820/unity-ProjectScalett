using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddClockItem : MonoBehaviour
{
    [SerializeField]
    private string dataName = "puzzle_ClockPuzzle_AddItem"; //�����ͺ��̽��� ����� �����͸�

    //�ð� ��, �� ħ Ȱ��ȭ ����
    int AddItem1 = 0; //��ħ
    int AddItem2 = 0; //��ħ

    //Ȱ��ȭ�� ��, ��ħ ������Ʈ
    public GameObject HourHourHand;
    public GameObject MinuteHand;

    void Start()
    {
        //�ش� ���� ���� ���� ������ ���̽����� ��������.
        if (dataName != null)
        {
            int getAddItem1Status = GameManager.instance.ControllObjStatusData(0, dataName+1, 0);//���� �޴������� ������ ��������
            if (getAddItem1Status >= 0)
                AddItem1 = getAddItem1Status;

            int getAddItem2Status = GameManager.instance.ControllObjStatusData(0, dataName + 2, 0);//���� �޴������� ������ ��������
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

    //������ �������ۿ� ���� ��,��ħ Ȱ��ȭ �Լ�
    public void OnAddItem()
    {
        int thisItem = GameManager.instance.SelectItemId;
        //��ħ Ȱ��ȭ
        if (thisItem == 102)
        {
            AddItem1 = 1;
        }
        //��ħ Ȱ��ȭ
        else if(thisItem == 104)
        {
            AddItem2 = 2;

        }
    }

    private void OnDestroy()
    {
        //�ش� ���� ���� ���� ������ ���̽��� ����
        GameManager.instance.ControllObjStatusData(1, dataName + 1, AddItem1);
        GameManager.instance.ControllObjStatusData(1, dataName + 2, AddItem2);
    }
}