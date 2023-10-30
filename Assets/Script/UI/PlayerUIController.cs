using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUIController : MonoBehaviour
{
    public static PlayerUIController instance = null;//�ν��Ͻ�ȭ

    //������ �������� �κ��丮â
    [SerializeField]
    public GameObject[] ItemBoxs;
    bool IsNullItemBoxs = true;

    //��ø�ý��� ���� ���� ����
    public GameObject Note;

    string ImgPath = "Image/Item/"; //������ �̹��� ���

    public GameObject ChatLogPre;//��ȭâ ������

    public GameObject ItemInspectorPre;//������ �߰�����â ������
    private void Awake()
    {
        //�÷��̾� UI��Ʈ�ѷ� �ν��Ͻ� �ʱ�ȭ
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //�������κ��丮�� �������۵��� �����ͼ� foreach�� �ݺ�������
        int loofCnt = 0;

        //�������� �κ��丮�� �Ⱥ�� ������ �κ��丮�� ������ ȭ�鿡 ����
        if (GameManager.instance.HaveDataBase.UseItem.Count > 0)
        {
            foreach (DataIds dataid in GameManager.instance.HaveDataBase.UseItem)
            {
                //������ ������ ���̽��� �ִ� �̹����� �κ��丮�� �̹��� ����
                if (ItemBoxs.Length > loofCnt)
                {
                    Sprite itemSprite = Resources.Load<Sprite>(ImgPath + ItemDataManager.instance.GetItemByID(dataid.ID, 0).spriteName);
                    if (itemSprite != null)
                        ItemBoxs[loofCnt].GetComponent<Image>().sprite = itemSprite;
                    //�������� �������� �ش� �������̸� ������ �ڽ��� Ű���
                    if (GameManager.instance.SelectItemId == dataid.ID)
                    {
                        ItemBoxs[loofCnt].GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1);
                    }
                    else
                    {
                        ItemBoxs[loofCnt].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    }
                }
                loofCnt++;
            }
        }
        else
        {
            //�κ��丮�� �������� ������ ù��°�� ����
            ItemBoxs[0].GetComponent<Image>().sprite = null;
            ItemBoxs[0].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            GameManager.instance.SelectItemId = 0;//���õ� ������ �ڽ� ��ȣ �ʱ�ȭ
        }
    }

    //�÷��̾� �� �̵� �� ȣ��
    public void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    //�÷��̾� ��� ������ ���� SelectNum: ������ ������ĭ ��ȣ
    public void SelectItem(int SelectNum)
    {
        //������ �κ��丮ĭ ��ȣ�� ���� �κ��丮�� ���� ���� ���� ���� ��쿡 ����
        if(GameManager.instance.HaveDataBase.UseItem.Count > SelectNum)
        {
            DataIds thisItemId = GameManager.instance.HaveDataBase.UseItem[SelectNum];//���� Ŭ���� ������ ��������
            
            //������ ������ ĭ�� �������� ������ SelectItemId�� �ش� ������ID�� ����
            if (thisItemId != null)
            {
                //������ �������� �ٽ� �����ϸ� ��������
                if (GameManager.instance.SelectItemId == thisItemId.ID)
                {
                    GameManager.instance.SelectItemId = 0;
                }
                else
                {
                    GameManager.instance.SelectItemId = thisItemId.ID;
                }
            }
        }
        else
        {
            GameManager.instance.SelectItemId = 0;//SelectItem 0���� �ʱ�ȭ
        }
    }

    //�÷��̾� ��ø ���� ���� �� ȣ���ϴ� �Լ�
    public void OpenAndCloseNote()
    {
        bool NoteActive = Note.activeSelf;
        if(NoteActive)
            Note.SetActive(false);
        else if (!NoteActive)
            Note.SetActive(true);
    }

    //��ȭâ ���� �Լ�CallObj_Status: 0:������ 1:����
    public void OpenChatWindow(int LogID, GameObject CallObj = null, int CallObj_Status = 0)
    {
        GameObject Chatlog = Instantiate(ChatLogPre, this.transform);//��ȭ�α� â ���� �� �ش� ĵ������ �θ� ����

        Chatlog.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0); // ��ġ�� �ʱ�ȭ
        if (CallObj != null)
            Chatlog.GetComponent<ChatWindowController>().CallObj = CallObj; //ȣ�� ������Ʈ �� ����
        Chatlog.GetComponent<ChatWindowController>().LogID = LogID; //ȣ���� ��ȭ�α� ID �� ����
        Chatlog.GetComponent<ChatWindowController>().CallObj_Status = CallObj_Status; //ȣ�� ������Ʈ ���°� ����

        Chatlog.SetActive(true);//��ȭâ ������ Ȱ��ȭ
    }

    //ItemInspectorâ ���� �Լ�
    public void OpenItemInspectorWindow(int itemType, int itemID)
    {
        if(ItemInspectorPre != null)
        {
            GameObject Prefap = Instantiate(ItemInspectorPre, this.transform);//������ �ν�����â ����

            Prefap.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0); // ��ġ�� �ʱ�ȭ
            Prefap.GetComponent<ItemInspectorController>().ItemType = itemType;//������ ������ Ÿ�� ����
            Prefap.GetComponent<ItemInspectorController>().ItemID = itemID;//������ ������ ���̵� ����
            Prefap.SetActive(true);//������ Ȱ��ȭ
        }
    }

    //CutSceneâ ���� �Լ�
    public void OpenCutSceneWindow(string CutSceneName)
    {
        string CutScenePath = "Prefap/UI/CutScene/"; //�ƾ� ������ ��ġ

        GameObject CutScenePre = Resources.Load<GameObject>(CutScenePath + CutSceneName);//�ƾ� ������ ��������

        GameObject Prefap = Instantiate(CutScenePre, this.transform);//�ƾ� ������ ����
    }
}
