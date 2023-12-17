using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapPageController : MonoBehaviour
{
    //맵 이미지 파일명이 저장되는 배열
    [SerializeField]
    private string[] MapImgs;

    string MapImgPath = "Image/NoteInventory/";//이미지 위치
    int nowPage = 0;//현재 페이지

    public GameObject LeftBtn;
    public GameObject RightBtn;

    private void Start()
    {
        UpdateMap();
    }

    // Start is called before the first frame update
    void Update()
    {
        //현제 페이지 상태별 버튼 활성화
        if (nowPage == 0)
            LeftBtn.SetActive(false);
        else
            LeftBtn.SetActive(true);

        if (nowPage+1 < MapImgs.Length)
            RightBtn.SetActive(true);
        else
            RightBtn.SetActive(false);

    }

    //맵페이지 전환 이벤트
    public void OnTurnPageBtn(int Num)
    {
        nowPage += Num;
        UpdateMap();
    }

    //현재 페이지에 위치한 이미지로 교체
    public void UpdateMap()
    {
        Sprite MapImg = Resources.Load<Sprite>(MapImgPath + MapImgs[nowPage]);//맵 이미지 가져오기
        //해당경로에 이미지 있을 시 이미지 적용
        if (MapImg != null)
            this.GetComponent<Image>().sprite = MapImg;

    }
}
