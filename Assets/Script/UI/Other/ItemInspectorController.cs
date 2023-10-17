using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInspectorController : MonoBehaviour
{
    public GameObject ItemImgObj;//아이템 이미지 보여주는 오브젝트
    public GameObject AdditionalExplanationObj;//아이템 설명 보여주는 오브젝트
    public int ItemType; //0: 사용아이템 1: 증거
    public int ItemID; // 아이템 아이디

    private string ImgPath = "Image/Item/";//아이템 이미지 경로

    private void OnEnable()
    {
        LoadItem(ItemType, ItemID);
    }

    //아이템 Type과 ID로 아이템 이미지 부과설명 출력
    private void LoadItem(int itemType, int itemID)
    {
        if(itemType >= 0 && itemID > 0)//빈값 체크
        {
            Item ShowItem = ItemDataManager.instance.GetItemByID(itemID, itemType);//아이템 정보 가져오기
            
            if(ShowItem != null)
            {
                Sprite ItemSprite = Resources.Load<Sprite>(ImgPath + ShowItem.spriteName); //아이템 이미지 가져오기

                ItemImgObj.GetComponent<Image>().sprite = ItemSprite; //아이템 이미지 적용
                AdditionalExplanationObj.GetComponent<Text>().text = ShowItem.additionalExplanation; //아이템 부가 설명 적용
            }
        }
    }
    //아이템 인스펙터창 닫기
    public void OnCloseItemInspector()
    {
        Destroy(this.gameObject);
    }
}
