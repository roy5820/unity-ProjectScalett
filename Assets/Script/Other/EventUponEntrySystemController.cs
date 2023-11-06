using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventUponEntrySystemController : MonoBehaviour
{
    public int EventType; //0: ��ȭ�α�, 1: �ƾ�

    public string CutScenePre_Name; //�ƾ� ������ �̸�
    public int ChatLogID;//��ȭ �α�

    private int isStatus = 0;//������Ʈ ���� �� 0:�̻�� 1: ���
    private string dataName = null; //�����ͺ��̽��� ����� �����͸�

    private void Start()
    {
        //�����͸��� ���� �� �̸� + ������Ʈ �̸����� �ʱ�ȭ
        dataName = SceneManager.GetActiveScene().name + "_" + gameObject.name + "_Event";

        //�̺�Ʈ ��뿩�� ��������
        if (dataName != null)
        {
            int getObjStatus = GameManager.instance.ControllObjStatusData(0, dataName, 0);//���� �޴������� ������ ��������
            if (getObjStatus >= 0)
                isStatus = getObjStatus;
        }

        //ó�� ���� �� �̺�Ʈ Ÿ�Կ� ���� �̺�Ʈ ó��
        if (isStatus == 0)
        {
            isStatus = 1;//���°� ����
            //�������ͽ��� ���� ���� ������ ���̽��� ����
            GameManager.instance.ControllObjStatusData(1, dataName, isStatus);

            if (EventType == 0)
            {
                PlayerUIController.instance.OpenChatWindow(ChatLogID, null);//��ȭâ ����
            }
            else if (EventType == 1)
            {
                PlayerUIController.instance.OpenCutSceneWindow(CutScenePre_Name);//�ƾ� ����
            }
        }
    }
}
