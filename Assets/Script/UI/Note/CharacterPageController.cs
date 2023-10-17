using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPageController : MonoBehaviour
{
    //증거 페이지의 이미지 + 설명 + 버튼 가져오기
    public GameObject LeftPageCharacterImage; // 왼쪽페이지 캐릭터 이미지
    public GameObject LeftPageCharacterName; //왼쪽페이지 캐릭터 이름
    public GameObject LeftPageCharacterAge; //왼쪽페이지 캐릭터 나이
    public GameObject LeftPageCharacterJob; //왼쪽페이지 캐릭터 직업
    public GameObject LeftPageTestimony; //왼쪽페이지 증언
    public GameObject LeftTurnPageBtn;//왼쪽페이지 넘기기 버튼

    public GameObject RightPageCharacterImage;//오른쪽 페이지 캐릭터 이미지
    public GameObject RightPageCharacterName; //오른쪽페이지 캐릭터 이름
    public GameObject RightPageCharacterAge; //오른쪽페이지 캐릭터 나이
    public GameObject RightPageCharacterJob; //오른쪽페이지 캐릭터 직업
    public GameObject RightPageTestimony; //오른쪽페이지 증언
    public GameObject RightTurnPageBtn;//오른쪽페이지 넘기기 버튼

    //현제 페이지
    public int NowPage = 0;

    string ImgPath = "Image/Character/"; //인물 이미지 경로

    // Update is called once per frame
    void Update()
    {
        //인벤토리 연결
        PlayerDataBase HaveItems = GameManager.instance.HaveDataBase;
        //인벤토리의 인물정보 왼쪽 페이지에 보여주기
        if (NowPage < HaveItems.CharacterInformation.Count)
        {
            Character CharacterData = ItemDataManager.instance.GetCharcterByID(HaveItems.CharacterInformation[NowPage].ID);// 인물 정보 가져오기

            Sprite CharacterSprite = Resources.Load<Sprite>(ImgPath + CharacterData.spriteName);//스프라이트 이미지 가져오기

            LeftPageCharacterImage.GetComponent<Image>().sprite = CharacterSprite;//캐릭터 이미지 적용
            LeftPageCharacterName.GetComponent<Text>().text = CharacterData.name;//캐릭터 이름
            LeftPageCharacterAge.GetComponent<Text>().text = CharacterData.age;//캐릭터 나이
            LeftPageCharacterJob.GetComponent<Text>().text = CharacterData.job;//캐릭터 직업

            //캐릭터 ID로 증언 정보를 가져와 화면에 뿌리는 친구
            LeftPageTestimony.GetComponent<Text>().text = null; //정보 갱신 전 초기화
            foreach (DataIds TestimonyID in HaveItems.CharacterTestimony)
            {
                if(TestimonyID.RelatedID == CharacterData.id)
                {
                    Testimony TestimonyData = ItemDataManager.instance.GetTestimonyByID(TestimonyID.ID);// 증언 가져오기
                    
                    if (TestimonyData != null)
                    {
                        if(LeftPageTestimony.GetComponent<Text>().text == null)
                            LeftPageTestimony.GetComponent<Text>().text += TestimonyData.additionalExplanation;//증언 내용을 화면에 추가
                        else
                            LeftPageTestimony.GetComponent<Text>().text += "\n" + TestimonyData.additionalExplanation;//증언 내용을 화면에 추가
                    }
                }
            }
        }
        //없으면 공백으로 초기화
        else
        {
            
        }

        //인벤토리의 인물정보 오른쪽 페이지에 보여주기
        if (NowPage + 1 < HaveItems.CharacterInformation.Count)
        {
            Character CharacterData = ItemDataManager.instance.GetCharcterByID(HaveItems.CharacterInformation[NowPage+1].ID);// 인물 정보 가져오기

            Sprite CharacterSprite = Resources.Load<Sprite>(ImgPath + CharacterData.spriteName);//스프라이트 이미지 가져오기

            RightPageCharacterImage.GetComponent<Image>().sprite = CharacterSprite;//캐릭터 이미지 적용
            RightPageCharacterName.GetComponent<Text>().text = CharacterData.name;//캐릭터 이름
            RightPageCharacterAge.GetComponent<Text>().text = CharacterData.age;//캐릭터 나이
            RightPageCharacterJob.GetComponent<Text>().text = CharacterData.job;//캐릭터 직업


            //캐릭터 ID로 증언 정보를 가져와 화면에 뿌리는 친구
            RightPageTestimony.GetComponent<Text>().text = null; //정보 갱신 전 초기화
            foreach (DataIds TestimonyID in HaveItems.CharacterTestimony)
            {
                if (TestimonyID.RelatedID == CharacterData.id)
                {
                    Testimony TestimonyData = ItemDataManager.instance.GetTestimonyByID(TestimonyID.ID);// 증언 가져오기

                    if (TestimonyData != null)
                    {
                        if (RightPageTestimony.GetComponent<Text>().text == null)
                            RightPageTestimony.GetComponent<Text>().text += TestimonyData.additionalExplanation;//증언 내용을 화면에 추가
                        else
                            RightPageTestimony.GetComponent<Text>().text += "\n" + TestimonyData.additionalExplanation;//증언 내용을 화면에 추가
                    }
                }
            }
        }
        //없으면 공백으로 초기화
        else
        {
            
        }

        //인벤토리의 증거 개수와 현제 페이지에 따른 TrunPage버튼 활성화/비활성화
        if (NowPage - 1 < 0)
            LeftTurnPageBtn.SetActive(false);
        else
            LeftTurnPageBtn.SetActive(true);

        if (NowPage + 2 < HaveItems.CharacterInformation.Count)
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
