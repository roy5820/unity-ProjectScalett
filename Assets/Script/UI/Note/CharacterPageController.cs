using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPageController : MonoBehaviour
{
    //���� �������� �̹��� + ���� + ��ư ��������
    public GameObject LeftPageCharacterImage; // ���������� ĳ���� �̹���
    public GameObject LeftPageCharacterName; //���������� ĳ���� �̸�
    public GameObject LeftPageCharacterAge; //���������� ĳ���� ����
    public GameObject LeftPageCharacterJob; //���������� ĳ���� ����
    public GameObject LeftPageTestimony; //���������� ����
    public GameObject LeftTurnPageBtn;//���������� �ѱ�� ��ư

    public GameObject RightPageCharacterImage;//������ ������ ĳ���� �̹���
    public GameObject RightPageCharacterName; //������������ ĳ���� �̸�
    public GameObject RightPageCharacterAge; //������������ ĳ���� ����
    public GameObject RightPageCharacterJob; //������������ ĳ���� ����
    public GameObject RightPageTestimony; //������������ ����
    public GameObject RightTurnPageBtn;//������������ �ѱ�� ��ư

    //���� ������
    public int NowPage = 0;

    string ImgPath = "Image/Character/"; //�ι� �̹��� ���

    // Update is called once per frame
    void Update()
    {
        //�κ��丮 ����
        PlayerDataBase HaveItems = GameManager.instance.HaveDataBase;
        //�κ��丮�� �ι����� ���� �������� �����ֱ�
        if (NowPage < HaveItems.CharacterInformation.Count)
        {
            Character CharacterData = ItemDataManager.instance.GetCharcterByID(HaveItems.CharacterInformation[NowPage].ID);// �ι� ���� ��������

            Sprite CharacterSprite = Resources.Load<Sprite>(ImgPath + CharacterData.spriteName);//��������Ʈ �̹��� ��������

            LeftPageCharacterImage.GetComponent<Image>().sprite = CharacterSprite;//ĳ���� �̹��� ����
            LeftPageCharacterName.GetComponent<Text>().text = CharacterData.name;//ĳ���� �̸�
            LeftPageCharacterAge.GetComponent<Text>().text = CharacterData.age;//ĳ���� ����
            LeftPageCharacterJob.GetComponent<Text>().text = CharacterData.job;//ĳ���� ����

            //ĳ���� ID�� ���� ������ ������ ȭ�鿡 �Ѹ��� ģ��
            LeftPageTestimony.GetComponent<Text>().text = null; //���� ���� �� �ʱ�ȭ
            foreach (DataIds TestimonyID in HaveItems.CharacterTestimony)
            {
                if(TestimonyID.RelatedID == CharacterData.id)
                {
                    Testimony TestimonyData = ItemDataManager.instance.GetTestimonyByID(TestimonyID.ID);// ���� ��������
                    
                    if (TestimonyData != null)
                    {
                        if(LeftPageTestimony.GetComponent<Text>().text == null)
                            LeftPageTestimony.GetComponent<Text>().text += TestimonyData.additionalExplanation;//���� ������ ȭ�鿡 �߰�
                        else
                            LeftPageTestimony.GetComponent<Text>().text += "\n" + TestimonyData.additionalExplanation;//���� ������ ȭ�鿡 �߰�
                    }
                }
            }
        }
        //������ �������� �ʱ�ȭ
        else
        {
            
        }

        //�κ��丮�� �ι����� ������ �������� �����ֱ�
        if (NowPage + 1 < HaveItems.CharacterInformation.Count)
        {
            Character CharacterData = ItemDataManager.instance.GetCharcterByID(HaveItems.CharacterInformation[NowPage+1].ID);// �ι� ���� ��������

            Sprite CharacterSprite = Resources.Load<Sprite>(ImgPath + CharacterData.spriteName);//��������Ʈ �̹��� ��������

            RightPageCharacterImage.GetComponent<Image>().sprite = CharacterSprite;//ĳ���� �̹��� ����
            RightPageCharacterName.GetComponent<Text>().text = CharacterData.name;//ĳ���� �̸�
            RightPageCharacterAge.GetComponent<Text>().text = CharacterData.age;//ĳ���� ����
            RightPageCharacterJob.GetComponent<Text>().text = CharacterData.job;//ĳ���� ����


            //ĳ���� ID�� ���� ������ ������ ȭ�鿡 �Ѹ��� ģ��
            RightPageTestimony.GetComponent<Text>().text = null; //���� ���� �� �ʱ�ȭ
            foreach (DataIds TestimonyID in HaveItems.CharacterTestimony)
            {
                if (TestimonyID.RelatedID == CharacterData.id)
                {
                    Testimony TestimonyData = ItemDataManager.instance.GetTestimonyByID(TestimonyID.ID);// ���� ��������

                    if (TestimonyData != null)
                    {
                        if (RightPageTestimony.GetComponent<Text>().text == null)
                            RightPageTestimony.GetComponent<Text>().text += TestimonyData.additionalExplanation;//���� ������ ȭ�鿡 �߰�
                        else
                            RightPageTestimony.GetComponent<Text>().text += "\n" + TestimonyData.additionalExplanation;//���� ������ ȭ�鿡 �߰�
                    }
                }
            }
        }
        //������ �������� �ʱ�ȭ
        else
        {
            
        }

        //�κ��丮�� ���� ������ ���� �������� ���� TrunPage��ư Ȱ��ȭ/��Ȱ��ȭ
        if (NowPage - 1 < 0)
            LeftTurnPageBtn.SetActive(false);
        else
            LeftTurnPageBtn.SetActive(true);

        if (NowPage + 2 < HaveItems.CharacterInformation.Count)
            RightTurnPageBtn.SetActive(true);
        else
            RightTurnPageBtn.SetActive(false);
    }

    //���� ������ �ѱ�� ���� �Լ�
    public void TurnPage(int TurnAroow)
    {
        NowPage += TurnAroow * 2;
    }
}
