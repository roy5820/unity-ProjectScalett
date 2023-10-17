using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteController : MonoBehaviour
{
    public GameObject EvidencePage; //���� ������
    public GameObject CharacterPage; //�ι� ������

    //Ư�� �������� �����ϴ� �Լ� PageType: 0 ����, 1 �ι�, 2 ����
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
