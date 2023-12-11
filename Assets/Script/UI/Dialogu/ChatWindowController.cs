using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatWindowController : MonoBehaviour
{
    public GameObject CallObj;//�ش� ������Ʈ�� ȣ���� ������Ʈ
    public int LogID = 0;//��ȭ �α׾��̵�
    public int CallObj_Status;//ȣ���� ������Ʈ�� ���°�

    public Line[] TalkLines = null; //��ȭ�αװ� ����� �迭���� - �ش� �������� �����ϴ� ������ TalkLines���� �����������
    public int ThisLineNum; //���� �� Line �迭�� ��ȣ
    string ImgPath = "Image/Character/"; //ĳ���� �̹��� ���
    string SFXPath = "Sound/SFX/";//ȿ���� ���

    public GameObject TalkerImgBox;//���ϴ� ĳ���� �̹����� �巯�� �̹���
    public GameObject TalkerNameBox;//���ϴ� ĳ������ �̸��� �巯�� �̸�
    public GameObject ChatTextBox;//���ϴ� ������ ��� �ؽ�Ʈ �ڽ�

    private bool isPrinting = false; //���� ��� ���� ����
    private bool isGetItem = false; //������ ȹ�� ����

    private void OnEnable()
    {
        TalkLines = TalkLogDataManager.instance.GetTalkLogByID(LogID); //�α� ���̵�� ��ȭ���� ��������

        ThisLineNum = 0; //���� ��ȣ 0���� �ʱ�ȭ
        UpdateChatLog(ThisLineNum);//��ȭ�α� ȭ�鿡 �Ѹ���
    }

    public void UpdateChatLog(int NowLineNum)
    {
        //nullüũ
        if (TalkLines != null && TalkLines.Length > NowLineNum)
        {
            //nullüũ
            if(TalkLines[NowLineNum].spriteName != null)
            {
                //�̹����� ������ ������ ����
                Sprite TalkerSprite = Resources.Load<Sprite>(ImgPath + TalkLines[NowLineNum].spriteName);//�̹��� ��������
                if (TalkerSprite != null)//null üũ
                {
                    TalkerImgBox.GetComponent<Image>().sprite = TalkerSprite;//�̹��� ����
                    TalkerImgBox.SetActive(true);
                }
                else
                {
                    TalkerImgBox.GetComponent<Image>().sprite = null;
                    TalkerImgBox.SetActive(false);
                }
                    
            }
            else
            {
                TalkerImgBox.GetComponent<Image>().sprite = null;
                TalkerImgBox.SetActive(false);
            }

            //���ϴ� ��� �̸� �����Ͱ� ������ ����
            if (TalkLines[NowLineNum].speakerId > 0)
            {
                string TalkerName = ItemDataManager.instance.GetCharcterByID(TalkLines[NowLineNum].speakerId).name;
                if (TalkerName != null)//null üũ
                    TalkerNameBox.GetComponent<Text>().text = TalkerName;
                else
                    TalkerNameBox.GetComponent<Text>().text = null;
            }
            else
            {
                TalkerNameBox.GetComponent<Text>().text = null;
            }
            

            //��ȭ �α׸� ������ �����Ͱ� ������ ����
            string TalkLog = TalkLines[NowLineNum].log;
            if (TalkLog != null)//null üũ
                StartCoroutine(PrintText(TalkLog, 0.025f));
            else
                ChatTextBox.GetComponent<Text>().text = null;

            //ȿ���� ������ �����Ͱ� ������ ����
            string SFXSound = TalkLines[NowLineNum].soundName;
            if(SFXSound != null)
            {
                AudioClip getSFXSound = Resources.Load<AudioClip>(SFXPath+SFXSound);
                SoundManager.instance.SfxSoundPlay(getSFXSound);
            }
            else
                SoundManager.instance.SfxSoundPlay(null);
                
        }
        //�����Ͱ� ������ �ڵ����� ����
        else
        {
            Destroy(this.gameObject);
        }
    }

    //���ڸ� �ϳ��� ����ϴ� �ڷ�ƾ
    IEnumerator PrintText(string Text, float PrintingOneTextSpeed)
    {
        isPrinting = true;
        int TextArraayCnt = 0;//�ؽ�Ʈ �迭�� ���� ���Ե� ���� ���
        string TextArraay = null; //�ؽ�Ʈ �迭
        while (TextArraayCnt < Text.Length)
        {
            //������ ���� üũ�ؼ� ������ ���°� �ƴϸ� ��� �ؽ�Ʈ �ѹ��� ��� �� ����
            if (!isPrinting)
            {
                if (Text != null)//null üũ
                    TextArraay = Text;
                TextArraayCnt = Text.Length;
            }
            else//������ ������ ��� Text�ȿ� ���� �ϳ��� TextArray�ȿ� �߰�
            {
                TextArraay += Text[TextArraayCnt];
            }

            ChatTextBox.GetComponent<Text>().text = TextArraay;//�ؽ�Ʈ �迭 �ȿ� �ؽ�Ʈ�� �c�鿡 ���
            TextArraayCnt++;
            yield return new WaitForSeconds(PrintingOneTextSpeed);
        }
        isPrinting = false;
    }

    //Ŭ���� ȣ��Ǵ� ��ȭ�α� ��ȯ �Լ�
    public void OnChangeChatLog()
    {
        //���� �߷��߿��� ������� �ؽ�Ʈ ��� �ѹ��� ���
        if (isPrinting)
        {
            isPrinting = false;
            return;
        }
        //������ ���°� �ƴϸ鼭 ������ ȹ�� ���°� �ƴϸ鼭, CallObj���°� ó�� ������ ��쿡�� ������ ȹ�� �̺�Ʈ ����
        else if (!isPrinting && !isGetItem && CallObj_Status == 0)
        {
            isGetItem = true;//������ ȹ�濩�� true�� ����
            //���� ���ο� getItemID ��������
            Line getLine = TalkLines[ThisLineNum];

            //getItemID�� ������ ������ ȹ�� �̺�Ʈ ����
            if (getLine.getItemID > 0)
            {
                int charactorID = 0;

                //ȹ�� �������� �����̸� ���� ���̵� ĳ���� id �߰�
                if (getLine.getItemType == 3)
                    charactorID = getLine.speakerId;
                //������ ������ �̺�Ʈ ó��
                GameManager.instance.AddItem(getLine.getItemType, getLine.getItemID, charactorID);

                //������ ȹ�� �� ������ �ν�����â ����
                if (getLine.getItemType < 2)
                {
                    PlayerUIController.instance.OpenItemInspectorWindow(getLine.getItemType, getLine.getItemID);
                    return;
                }
            }
        }

        //���� ������ ���� �� ���ι�ȣ 1���� ��Ű�� �α� ��ȯ
        if (ThisLineNum < TalkLines.Length - 1)
        {
            isGetItem = false;//������ ȹ�濩�� false�� ����
            ThisLineNum++;
            UpdateChatLog(ThisLineNum);
        }
        //���������� �������� ���� �� ����
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void OnDestroy()
    {
        if (CallObj != null)
        {
            //������Ʈ ���� �� ��ȭ ����â �ٽ� Ű���
            CallObj.SetActive(true);
        }
    }
}