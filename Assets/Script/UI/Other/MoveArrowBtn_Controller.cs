using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveArrowBtn_Controller : MonoBehaviour
{
    public string SceneName; //�̵��� �� �̸�

    public int isStatus = 0;//0:�̵� ���� 1: ���
    private string dataName = null; //�����ͺ��̽��� ����� �����͸�

    public int KeyItemID;//Ű ������ ���̵�
    public int KeyItemType;//Ű ������ Ÿ��
    public int LockMessageID;//��� �޼���

    void Start()
    {
        //�����͸��� ���� �� �̸� + ������Ʈ �̸����� �ʱ�ȭ
        dataName = SceneManager.GetActiveScene().name + "_" + gameObject.name + "_MoveArrowBtn";

        //������ ȹ�� ���θ� ���Ӹ޴������� ��������
        if (dataName != null)
        {
            int getObjStatus = GameManager.instance.ControllObjStatusData(0, dataName, 0);//���� �޴������� ������ ��������
            if (getObjStatus >= 0)
                isStatus = getObjStatus;
        }

        //��� ������ ��� Ű �������� ������ �̵� ���� ���·� ��ȯ
        if (isStatus == 1 && GameManager.instance.ChkItem(KeyItemType, KeyItemID))
        {
            isStatus = 0;
        }
    }

    private void Update()
    {
        
    }

    public void OnMoveScene()
    {
        //���°��� ���� �̺�Ʈ ó��
        if(isStatus == 0)
        {
            PlayerUIController.instance.ChangeScene(SceneName);//���̵�
        }
        if(isStatus == 1)
        {
            PlayerUIController.instance.OpenChatWindow(LockMessageID);//��� �޽��� ����
        }
    }

    private void OnDestroy()
    {
        //�������ͽ��� ���� ���� ������ ���̽��� ����
        GameManager.instance.ControllObjStatusData(1, dataName, isStatus);
    }
}
