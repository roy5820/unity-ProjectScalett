using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvidencePageController : MonoBehaviour
{
    //���� �������� �̹��� + ���� + ��ư ��������
    public GameObject LeftPageImage; // ���������� �̹���
    public GameObject LeftPageExplanation; //���������� �����
    public GameObject LeftTurnPageBtn;//���������� �ѱ�� ��ư
    public GameObject RightPageImage;//������ ������ �̹���
    public GameObject RightPageExplanation;//������ ������ �����
    public GameObject RightTurnPageBtn;//������ �������� �ѱ�� ��ư

    //���� ������
    public int NowPage = 0;

    string ImgPath = "Image/Item/"; //������ �̹��� ���

    // Update is called once per frame
    void Update()
    {
        //�κ��丮 ����
        PlayerDataBase HaveItems = GameManager.instance.HaveDataBase;
        
        //�κ��丮�� ���� ���� �������� �����ֱ�
        if(NowPage < HaveItems.EvidenceItem.Count)
        {
            Item LeftEvidenceData = ItemDataManager.instance.GetItemByID(HaveItems.EvidenceItem[NowPage].ID, 1);// ���� ��������
            
            Sprite EvidenceSprite = Resources.Load<Sprite>(ImgPath + LeftEvidenceData.spriteName);//��������Ʈ �̹��� ��������

            LeftPageImage.GetComponent<Image>().sprite = EvidenceSprite;//�̹��� ����
            LeftPageImage.SetActive(true);//�̹��� ��ü Ȱ��ȭ
            LeftPageExplanation.GetComponent<Text>().text = LeftEvidenceData.additionalExplanation;//���� ���� ����
        }
        //������ �������� �ʱ�ȭ
        else
        {
            LeftPageImage.GetComponent<Image>().sprite = null;//�̹��� �ʱ�ȭ
            LeftPageImage.SetActive(false);//�̹��� ��ü ��Ȱ��ȭ
            LeftPageExplanation.GetComponent<Text>().text = null;//���ż��� �ʱ�ȭ
        }

        //�κ��丮�� ���� ������ �������� �����ֱ�
        if (NowPage+1 < HaveItems.EvidenceItem.Count)
        {
            Item RightEvidenceData = ItemDataManager.instance.GetItemByID(HaveItems.EvidenceItem[NowPage+1].ID, 1);// ���� ��������

            Sprite EvidenceSprite = Resources.Load<Sprite>(ImgPath + RightEvidenceData.spriteName);//��������Ʈ �̹��� ��������

            RightPageImage.GetComponent<Image>().sprite = EvidenceSprite;//�̹��� ����
            RightPageImage.SetActive(true); // �̹��� ��ü Ȱ��ȭ
            RightPageExplanation.GetComponent<Text>().text = RightEvidenceData.additionalExplanation;//���� ���� ����
        }
        //������ �������� �ʱ�ȭ
        else
        {
            RightPageImage.GetComponent<Image>().sprite = null;//�̹��� �ʱ�ȭ
            RightPageImage.SetActive(false); // �̹��� ��ü ��Ȱ��ȭ
            RightPageExplanation.GetComponent<Text>().text = null;//���ż��� �ʱ�ȭ
        }

        //�κ��丮�� ���� ������ ���� �������� ���� TrunPage��ư Ȱ��ȭ/��Ȱ��ȭ
        if (NowPage - 1 < 0)
            LeftTurnPageBtn.SetActive(false);
        else
            LeftTurnPageBtn.SetActive(true);

        if (NowPage + 2 < HaveItems.EvidenceItem.Count)
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
