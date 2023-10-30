using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class DialogusSelectionController : MonoBehaviour
{
    //��ȭ��ư�� ���� ������ ���� ��ü
    [System.Serializable]
    public class TalkButtonData
    {
        public int ButtonIndex;//��ư�ε��� ��ȣ
        public GameObject Button;//��ư ���ӿ�����Ʈ
        public int KeyID;//Ű ������ ID
        public int LogID;//�ҷ��� ��ȭ LogID
        public int GetTestimonyID;//ȹ���� ���� ID
        public int isStatus = 0;//��ư ���°� 0:������ 1: ���� 2: ���
    }

    //��ȭ��ư �������� ���� ����Ʈ ��ü
    [System.Serializable]
    public class TalkButtonDatas
    {
        public List<TalkButtonData> TalkButtonData;
    }

    public TalkButtonDatas ButtonDatas;//��ȭ��ư �����͸�
    private string dataName = null; //�����ͺ��̽��� ����� �����͸�

    private string ImgPath = "Image/UI/Button/";//��ư �̹��� ������ ���

    private void Start()
    {
        //�����͸��� ���� �� �̸� + ������Ʈ �̸����� �ʱ�ȭ
        dataName = SceneManager.GetActiveScene().name + "_" + gameObject.name + "_DialogusSelectionController_";

        //���ٽ��� ����� ��ư �̺�Ʈ ������ �ο�
        foreach (TalkButtonData ButtonData in ButtonDatas.TalkButtonData)
        {
            Button thisButton = ButtonData.Button.GetComponent<Button>();
            thisButton.onClick.AddListener(delegate { OnChatWindowOpen(ButtonData); });//��ư 
        }

        LoardButtonStatus();
    }

    private void OnEnable()
    {
        LoardButtonStatus();
    }

    //��ư Ŭ���� ��ȭâ�� ���� �Լ�
    //BtnIndex: ��ư �ε��� ��ȣ, LogID: �ҷ��� ��ȭ�α��� ID��
    public void OnChatWindowOpen(TalkButtonData BtnData)
    {
        //�� ���º� �̺�Ʈ ó��
        if (BtnData.isStatus == 0)
        {
            GameManager.instance.ControllObjStatusData(1, dataName + BtnData.ButtonIndex, 1);//��ư ���¸� 1(����)���� ����
            //ȹ���� ������ ������ ��� �κ��丮�� �߰�
            if (BtnData.GetTestimonyID > 0)
                GameManager.instance.AddItem(3, BtnData.GetTestimonyID);
        }
        else if (BtnData.isStatus == 2)
        {
                return;//������� �� �Լ� ����
        }

        PlayerUIController.instance.OpenChatWindow(BtnData.LogID, this.gameObject, BtnData.isStatus);//��ȭâ ����
        this.gameObject.SetActive(false);//ȭ�� ��Ȱ��ȭ
    }

    //��ư�� ���°� �ʱ�ȭ �ϴ� �Լ�
    public void LoardButtonStatus()
    {
        //��ȭâ�� Ȱ��ȭ �ɶ� ���� ��ư���� �̹��� �� ���°� �ʱ�ȭ�ϴ� �κ�
        foreach (TalkButtonData ButtonData in ButtonDatas.TalkButtonData)
        {
            //��ư���� ���°��� �����ͺ��̽����� ������ �ʱ�ȭ �ϴ� �κ� 
            int getStatus = GameManager.instance.ControllObjStatusData(0, dataName + ButtonData.ButtonIndex, 0);//������ �� �ڿ� ��ư�� �ε�����ȣ �ٿ��� �����Ͱ�������
            if (getStatus >= 0)
            {
                //Ű �������� ������ ���� �� ���(2) ������ ��ȭ��ư ������(0) ���·� ����
                if (getStatus == 2 && GameManager.instance.ChkItem(1, ButtonData.KeyID)) //key Item�� �κ��丮�� �ִ� �� üũ
                {
                    GameManager.instance.ControllObjStatusData(1, dataName + ButtonData.ButtonIndex, 1);//��ư ���¸� 1(����)���� ����
                    getStatus = 0;
                }

                ButtonData.isStatus = getStatus;
            }



            //��ư�� ���º� �̹��� ����
            if (ButtonData.isStatus > 0)
            {
                Sprite ButtonImg = null;

                if (ButtonData.isStatus == 1)
                    ButtonImg = Resources.Load<Sprite>(ImgPath + "ChatSelectButton_read");//��ư �̹��� ��������
                else if (ButtonData.isStatus == 2)
                    ButtonImg = Resources.Load<Sprite>(ImgPath + "ChatSelectButton_lock");//��ư �̹��� ��������
                if (ButtonImg != null)
                    ButtonData.Button.GetComponent<Image>().sprite = ButtonImg; //��ư �̹��� ����
            }

        }
    }

    //��ȭ����â �ݱ� �̺�Ʈ
    public void OnSelectWindowClose()
    {
        Destroy(this.gameObject);
    }
}
