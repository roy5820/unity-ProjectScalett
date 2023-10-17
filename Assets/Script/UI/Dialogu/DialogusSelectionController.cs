using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//��ȭ��ư�� ���� ������ ���� ��ü
[System.Serializable]
public class TalkButtonData
{
    public int ButtonIndex;//��ư�ε��� ��ȣ
    public GameObject Button;//��ư ���ӿ�����Ʈ
    public int KeyID;//Ű ������ ID
    public int LogID;//�ҷ��� ��ȭ LogID
    public int isStatus = 0;//��ư ���°� 0:������ 1: ����
}

//��ȭ��ư �������� ���� ����Ʈ ��ü
[System.Serializable]
public class TalkButtonDatas
{
    public List<TalkButtonData> TalkButtonData;
}

public class DialogusSelectionController : MonoBehaviour
{
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
    }

    private void OnEnable()
    {
        //��ȭâ�� Ȱ��ȭ �ɶ� ���� ��ư���� �̹��� �� ���°� �ʱ�ȭ�ϴ� �κ�
        foreach (TalkButtonData ButtonData in ButtonDatas.TalkButtonData)
        {
            //��ư���� ���°��� �����ͺ��̽����� ������ �ʱ�ȭ �ϴ� �κ� 
            int getStatus = GameManager.instance.ControllObjStatusData(0, dataName + ButtonData.ButtonIndex, 0);//������ �� �ڿ� ��ư�� �ε�����ȣ �ٿ��� �����Ͱ�������
            if (getStatus >= 0)
            {
                ButtonData.isStatus = getStatus;
            }

            //��ư�� ���º� �̹��� ����
            if(ButtonData.isStatus > 0)
            {
                Sprite ButtonImg = null;

                if (ButtonData.isStatus == 1)
                    ButtonImg = Resources.Load<Sprite>(ImgPath + "ChatSelectButton_lock");//��ư �̹��� ��������
                else if (ButtonData.isStatus == 2)
                    ButtonImg = Resources.Load<Sprite>(ImgPath + "ChatSelectButton_nomal");//��ư �̹��� ��������
                if(ButtonImg != null)
                    ButtonData.Button.GetComponent<Image>().sprite = ButtonImg; //��ư �̹��� ����
            }
            
        }
    }

    private void Update()
    {
        
    }

    //��ư Ŭ���� ��ȭâ�� ���� �Լ�
    //BtnIndex: ��ư �ε��� ��ȣ, LogID: �ҷ��� ��ȭ�α��� ID��
    public void OnChatWindowOpen(TalkButtonData BtnData)
    {
        //�� ���º� �̺�Ʈ ó��
        if (BtnData.isStatus == 0)
        {

        }
        else if (BtnData.isStatus == 2)
        {
        }
    }

  

    //��ȭ����â �ݱ� �̺�Ʈ
    public void OnSelectWindowClose()
    {
        Destroy(this.gameObject);
    }
}
