using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatWindowController : MonoBehaviour
{
    public GameObject CallObj;//�ش� ������Ʈ�� ȣ���� ������Ʈ
    public int LogID = 0;//��ȭ �α׾��̵�

    public Line[] TalkLines = null; //��ȭ�αװ� ����� �迭���� - �ش� �������� �����ϴ� ������ TalkLines���� �����������
    public int ThisLineNum; //���� �� Line �迭�� ��ȣ
    string ImgPath = "Image/ConversationImg/"; //ĳ���� �̹��� ���

    public GameObject TalkerImgBox;//���ϴ� ĳ���� �̹����� �巯�� �̹���
    public GameObject TalkerNameBox;//���ϴ� ĳ������ �̸��� �巯�� �̸�
    public GameObject ChatTextBox;//���ϴ� ������ ��� �ؽ�Ʈ �ڽ�

    private bool isPrinting = false; //���� ��� ���� ����

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
            //�̹����� ������ ������ ����
            Sprite TalkerSprite = Resources.Load<Sprite>(ImgPath + TalkLines[NowLineNum].spriteName);//�̹��� ��������
            if (TalkerSprite != null)//null üũ
                TalkerImgBox.GetComponent<Image>().sprite = TalkerSprite;//�̹��� ����
            else
                TalkerImgBox.GetComponent<Image>().sprite = null;

            //���ϴ� ��� �̸� �����Ͱ� ������ ����
            string TalkerName = ItemDataManager.instance.GetCharcterByID(TalkLines[NowLineNum].speakerId).name;
            if (TalkerName != null)//null üũ
                TalkerNameBox.GetComponent<Text>().text = TalkerName;
            else
                TalkerNameBox.GetComponent<Text>().text = null;

            //��ȭ �α׸� ������ �����Ͱ� ������ ����
            string TalkLog = TalkLines[NowLineNum].log;
            if (TalkLog != null)//null üũ
                StartCoroutine(PrintText(TalkLog, 0.025f));
            else
                ChatTextBox.GetComponent<Text>().text = null;
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

        //���� ������ ���� �� ���ι�ȣ 1���� ��Ű�� �α� ��ȯ
        if (ThisLineNum < TalkLines.Length - 1)
        {
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
            GameObject.Find(CallObj.name).GetComponent<RectTransform>().localScale = new Vector2(1, 1);
        }
    }
}