using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetItem : MonoBehaviour
{
    
    public int GetItemId;
    public GameObject ChangeObj = null; //�̹����� ������ ������Ʈ
    public Sprite OpenChangeImg = null; //���� �� ������ �̹���
    public Sprite GetChangeImg = null; //������ ȹ�� �� ������ �̹���

    public int GetItemType = 0; //0: ��������, 1: ����
    public bool isLock = false; //��� ����
    public int KeyItemId = 101; //��ȣ�ۿ� �Ǵ� �������� ID

    public int GetMessageID;//������ ȹ�� �� �޼��� ID
    public int DuplicateMessageID;//�ߺ� �޽��� ID
    public int LockMessageID;//��� �޼��� ID

    public int isStatus = 0; //���� ������Ʈ ���� �� 0: ��ħ 1: ��������, 2: ����� 
    private string dataName = null; //�����ͺ��̽��� ����� �����͸�

    public void Start()
    {
        //�����͸��� ���� �� �̸� + ������Ʈ �̸����� �ʱ�ȭ
        dataName = SceneManager.GetActiveScene().name+ "_"+gameObject.name + "_GetItem";

        //������ ȹ�� ���θ� ���Ӹ޴������� ��������
        if (dataName != null)
        {
            int getObjStatus = GameManager.instance.ControllObjStatusData(0, dataName, 0);//���� �޴������� ������ ��������
            if(getObjStatus >= 0)
                isStatus = getObjStatus;
        }
    }

    public void Update()
    {
        //������Ʈ ���º� �̹��� ��ü
        if (isStatus == 1)
        {
            //������ �̹��� �� ������Ʈ�� ������ �� ����
            if (ChangeObj != null && GetChangeImg != null)
            {
                ChangeObj.GetComponent<SpriteRenderer>().sprite = OpenChangeImg;//�̹��� ����
            }
        }
        else if (isStatus == 2)
        {
            //������ �̹��� �� ������Ʈ�� ������ �� ����
            if (ChangeObj != null && GetChangeImg != null)
            {
                ChangeObj.GetComponent<SpriteRenderer>().sprite = GetChangeImg;//�̹��� ����
            }
        }
    }

    public void OnDestroy()
    {
        //�������ͽ��� ���� ���� ������ ���̽��� ����
        GameManager.instance.ControllObjStatusData(1, dataName, isStatus);
    }

    //������ ȹ�� ���� �Լ�
    public void DropItem()
    {
        
        //������ ȹ�� ���� + ��� ���ο� ���� �����۹ڽ� ����
        if (isStatus == 0 && ((isLock && GameManager.instance.SelectItemId == KeyItemId) || !isLock))
        {
            isStatus = 1; //�����۹ڽ� ���»��·� 1�� ����

            //Key�������� �κ��丮���� ã�� ����
            if (isLock)
            {
                //key�������� �κ��丮���� �����ϴ� �����Դϴ�.
                GameManager.instance.DelItem(0, KeyItemId);
            }
        }
        else if(isStatus == 1)
        {
            
            isStatus = 2; //������ ȹ�濩�� 2�� ����

            /*//������ ������ �̺�Ʈ ó��
            GameManager.instance.AddItem(GetItemType, GetItemId);

            //������ ȹ�� �� ������ �ν�����â ����
            PlayerUIController.instance.OpenItemInspectorWindow(GetItemType, GetItemId);*/
            //������ ȹ�� ��ȭâ ����
            PlayerUIController.instance.OpenChatWindow(GetMessageID, null);
        }
        else if (isStatus == 2)
        {
            //����� ���¿��� ��ȣ�ۿ�� �޽��� ���
            if(DuplicateMessageID > 0)
                PlayerUIController.instance.OpenChatWindow(DuplicateMessageID, null);
        }
        else if(isLock)
        {
            //����ְ� ���谡 �Ǵ� �������� ���� �� ������� �޽��� ���
            if (LockMessageID > 0)
                PlayerUIController.instance.OpenChatWindow(LockMessageID, null);
        }
    }
}
