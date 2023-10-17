using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteController : MonoBehaviour
{
    public GameObject EvidencePage; //증언 페이지
    public GameObject CharacterPage; //인물 페이지

    //특정 페이지를 오픈하는 함수 PageType: 0 증거, 1 인물, 2 구조
    public void OpenPage(int PageType)
    {
        if (PageType == 0)
        {
            EvidencePage.SetActive(true);
            CharacterPage.SetActive(false);
        }
        else if (PageType == 1)
        {
            EvidencePage.SetActive(false);
            CharacterPage.SetActive(true);
        }
    }
}
