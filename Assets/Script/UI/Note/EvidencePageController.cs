using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvidencePageController : MonoBehaviour
{
    //증거 페이지의 이미지 + 설명 + 버튼 가져오기
    public GameObject LeftPageImage; // 왼쪽페이지 이미지
    public GameObject LeftPageExplanation; //왼쪽페이지 설명글
    public GameObject LeftTurnPageBtn;//왼쪽페이지 넘기기 버튼
    public GameObject RightPageImage;//오른쪽 페이지 이미지
    public GameObject RightPageExplanation;//오른쪽 페이지 설명글
    public GameObject RightTurnPageBtn;//오른쪽 페이지로 넘기기 버튼

    //현제 페이지
    public int NowPage = 0;

    string ImgPath = "Image/Item/"; //아이템 이미지 경로

    // Update is called once per frame
    void Update()
    {
        //인벤토리 연결
        PlayerDataBase HaveItems = GameManager.instance.HaveDataBase;
        
        //인벤토리의 증거 왼쪽 페이지에 보여주기
        if(NowPage < HaveItems.EvidenceItem.Count)
        {
            Item LeftEvidenceData = ItemDataManager.instance.GetItemByID(HaveItems.EvidenceItem[NowPage].ID, 1);// 증거 가져오기
            
            Sprite EvidenceSprite = Resources.Load<Sprite>(ImgPath + LeftEvidenceData.spriteName);//스프라이트 이미지 가져오기

            LeftPageImage.GetComponent<Image>().sprite = EvidenceSprite;//이미지 적용
            LeftPageImage.SetActive(true);//이미지 객체 활성화
            LeftPageExplanation.GetComponent<Text>().text = LeftEvidenceData.additionalExplanation;//증거 설명 적용
        }
        //없으면 공백으로 초기화
        else
        {
            LeftPageImage.GetComponent<Image>().sprite = null;//이미지 초기화
            LeftPageImage.SetActive(false);//이미지 객체 비활성화
            LeftPageExplanation.GetComponent<Text>().text = null;//증거설명 초기화
        }

        //인벤토리의 증거 오른쪽 페이지에 보여주기
        if (NowPage+1 < HaveItems.EvidenceItem.Count)
        {
            Item RightEvidenceData = ItemDataManager.instance.GetItemByID(HaveItems.EvidenceItem[NowPage+1].ID, 1);// 증거 가져오기

            Sprite EvidenceSprite = Resources.Load<Sprite>(ImgPath + RightEvidenceData.spriteName);//스프라이트 이미지 가져오기

            RightPageImage.GetComponent<Image>().sprite = EvidenceSprite;//이미지 적용
            RightPageImage.SetActive(true); // 이미지 객체 활성화
            RightPageExplanation.GetComponent<Text>().text = RightEvidenceData.additionalExplanation;//증거 설명 적용
        }
        //없으면 공백으로 초기화
        else
        {
            RightPageImage.GetComponent<Image>().sprite = null;//이미지 초기화
            RightPageImage.SetActive(false); // 이미지 객체 비활성화
            RightPageExplanation.GetComponent<Text>().text = null;//증거설명 초기화
        }

        //인벤토리의 증거 개수와 현제 페이지에 따른 TrunPage버튼 활성화/비활성화
        if (NowPage - 1 < 0)
            LeftTurnPageBtn.SetActive(false);
        else
            LeftTurnPageBtn.SetActive(true);

        if (NowPage + 2 < HaveItems.EvidenceItem.Count)
            RightTurnPageBtn.SetActive(true);
        else
            RightTurnPageBtn.SetActive(false);
    }

    //증거 페이지 넘기기 구현 함수
    public void TurnPage(int TurnAroow)
    {
        NowPage += TurnAroow * 2;
    }
}
