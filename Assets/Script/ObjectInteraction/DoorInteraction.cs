using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    public GameObject ChangeObj = null; //�̹����� ������ ������Ʈ
    public Sprite ChangeImg = null; //������ �̹���
    public bool isLock = false; //��� ����
    public int KeyItemId = 101; //��ȣ�ۿ� �Ǵ� �������� ID

    public int LockMessageID;//��� �޼��� ID
    public int UnLockMessageID;//��� ���� �޼��� ID
    public string UnLockCutScenePreName = null;//��� ���� �ƾ� ������ �̸�

    private int isStatus = 0; //���� ������Ʈ ���� �� 0: ��ȣ�ۿ�X ,1: ��ȣ�ۿ�O
    private string dataName = null; //�����ͺ��̽��� ����� �����͸�
    public void Start()
    {
        //�����͸��� ���� �� �̸� + ������Ʈ �̸����� �ʱ�ȭ
        dataName = SceneManager.GetActiveScene().name + "_" + gameObject.name + "_DoorInteraction";

        //������ ȹ�� ���θ� ���Ӹ޴������� ��������
        if (dataName != null)
        {
            int getObjStatus = GameManager.instance.ControllObjStatusData(0, dataName, 0);//���� �޴������� ������ ��������
            if (getObjStatus >= 0)
                isStatus = getObjStatus;
        }

        //Ȱ��ȭ ���ο� ���� Lock���� ����
        if (isLock)
        {
            if (isStatus == 1)
                isLock = false;
        }
    }

    public void Update()
    {
        //������Ʈ ���º� �̹��� ��ü
        if (isStatus == 1)
        {
            //������ �̹��� �� ������Ʈ�� ������ �� ����
            if (ChangeObj != null && ChangeImg != null)
            {
                ChangeObj.GetComponent<SpriteRenderer>().sprite = ChangeImg;//�̹��� ����
            }
        }
    }

    public void OnDestroy()
    {
        //�������ͽ��� ���� ���� ������ ���̽��� ����
        GameManager.instance.ControllObjStatusData(1, dataName, isStatus);
    }
    //�� �̵� ���� �Լ�
    public void OpenDoor(string SceneName)
    {
        //���ǿ� ���� �̺�Ʈ ����
        //���� ���������� �׳� �̵�
        if (!isLock)
        {
            if(!isLock)
                SceneManager.LoadScene(SceneName);
        }
        //���� ��� �ְ� ���谡 ������ ��� ����
        else if (isLock && (KeyItemId == 0 || (KeyItemId == GameManager.instance.SelectItemId)))
        {
            isLock = false;//��� ���� ����
            isStatus = 1;//��ȣ�ۿ� ���� 1�� ����
            //�ƾ� ���
            if (UnLockCutScenePreName != null)
                PlayerUIController.instance.OpenCutSceneWindow(UnLockCutScenePreName, this.gameObject, "OpenDoor", SceneName);
            //�޼��� ���
            else if (UnLockMessageID > 0)
                PlayerUIController.instance.OpenChatWindow(UnLockMessageID);

            //������ �̹��� �� ������Ʈ�� ������ �� ����
            if (ChangeObj != null && ChangeImg != null)
            {
                ChangeObj.GetComponent<SpriteRenderer>().sprite = ChangeImg;//�̹��� ����
            }

            //key�������� �κ��丮���� �����ϴ� �����Դϴ�.
            GameManager.instance.DelItem(0, KeyItemId);
        }
        else if (isLock) //������� �� �޽��� ���� ��
        {
            //����ְ� ���谡 �Ǵ� �������� ���� �� ������� �޽��� ���
            if (LockMessageID > 0)
                PlayerUIController.instance.OpenChatWindow(LockMessageID, null);
        }
    }
}
