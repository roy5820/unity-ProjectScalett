using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInspectorController : MonoBehaviour
{
    public GameObject ItemImgObj;//������ �̹��� �����ִ� ������Ʈ
    public GameObject AdditionalExplanationObj;//������ ���� �����ִ� ������Ʈ
    public int ItemType; //0: �������� 1: ����
    public int ItemID; // ������ ���̵�

    private string ImgPath = "Image/Item/";//������ �̹��� ���

    private void OnEnable()
    {
        LoadItem(ItemType, ItemID);
    }

    //������ Type�� ID�� ������ �̹��� �ΰ����� ���
    private void LoadItem(int itemType, int itemID)
    {
        if(itemType >= 0 && itemID > 0)//�� üũ
        {
            Item ShowItem = ItemDataManager.instance.GetItemByID(itemID, itemType);//������ ���� ��������
            
            if(ShowItem != null)
            {
                Sprite ItemSprite = Resources.Load<Sprite>(ImgPath + ShowItem.spriteName); //������ �̹��� ��������

                ItemImgObj.GetComponent<Image>().sprite = ItemSprite; //������ �̹��� ����
                AdditionalExplanationObj.GetComponent<Text>().text = ShowItem.additionalExplanation; //������ �ΰ� ���� ����
            }
        }
    }
    //������ �ν�����â �ݱ�
    public void OnCloseItemInspector()
    {
        Destroy(this.gameObject);
    }
}
