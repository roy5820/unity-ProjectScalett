using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapPageController : MonoBehaviour
{
    //�� �̹��� ���ϸ��� ����Ǵ� �迭
    [SerializeField]
    private string[] MapImgs;

    string MapImgPath = "Image/NoteInventory/";//�̹��� ��ġ
    int nowPage = 0;//���� ������

    public GameObject LeftBtn;
    public GameObject RightBtn;

    private void Start()
    {
        UpdateMap();
    }

    // Start is called before the first frame update
    void Update()
    {
        //���� ������ ���º� ��ư Ȱ��ȭ
        if (nowPage == 0)
            LeftBtn.SetActive(false);
        else
            LeftBtn.SetActive(true);

        if (nowPage+1 < MapImgs.Length)
            RightBtn.SetActive(true);
        else
            RightBtn.SetActive(false);

    }

    //�������� ��ȯ �̺�Ʈ
    public void OnTurnPageBtn(int Num)
    {
        nowPage += Num;
        UpdateMap();
    }

    //���� �������� ��ġ�� �̹����� ��ü
    public void UpdateMap()
    {
        Sprite MapImg = Resources.Load<Sprite>(MapImgPath + MapImgs[nowPage]);//�� �̹��� ��������
        //�ش��ο� �̹��� ���� �� �̹��� ����
        if (MapImg != null)
            this.GetComponent<Image>().sprite = MapImg;

    }
}
