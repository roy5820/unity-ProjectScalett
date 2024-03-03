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

    public GameObject InGameMenu;//�ΰ��� �޴�â


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
        //����� �������� �κ��丮�� �����۸�� ����Ʈȭ
        List<DataIds> ItemIds = GameManager.instance.HaveDataBase.UseItem;
        //�� �������� ĭ�� ������ �̹��� ����
        for (int i=0; i < ItemBoxs.Length; i++)
        {
            if(i < ItemIds.Count)
            {
                Sprite itemSprite = Resources.Load<Sprite>(ImgPath + ItemDataManager.instance.GetItemByID(ItemIds[i].ID, 0).spriteName);
                //�̹����� ������� ���� �� ������ �̹��� ����
                if (itemSprite != null)
                {
                    ItemBoxs[i].GetComponent<Image>().sprite = itemSprite;
                    ItemBoxs[i].SetActive(true);
                }

                //�������� �������� �ش� �������̸� ������ �ڽ��� Ű���
                if (GameManager.instance.SelectItemId == ItemIds[i].ID)
                {
                    ItemBoxs[i].GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);
                    ItemBoxs[i].transform.parent.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1);
                }
                else
                {
                    ItemBoxs[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    ItemBoxs[i].transform.parent.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1);
                }
            }
            //�� ������ĭ�� �⺻������ ���� �ʱ�ȭ
            else
            {
                ItemBoxs[i].GetComponent<Image>().sprite = null;
                ItemBoxs[i].SetActive(false);
                ItemBoxs[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                ItemBoxs[i].transform.parent.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1);
            }
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
    public void OpenCutSceneWindow(string CutSceneName, GameObject callBackObj = null, string callBackFuntion = null, string parameter = null)
    {
        string CutScenePath = "Prefap/UI/CutScene/"; //�ƾ� ������ ��ġ

        GameObject CutScenePre = Resources.Load<GameObject>(CutScenePath + CutSceneName);//�ƾ� ������ ��������

        //�ݹ� �� �Լ� ȣ�� ���� ó��
        if (callBackObj != null && callBackFuntion != null)
        {
            CutScenePre.GetComponent<CutSceneController>().CallBackObj = callBackObj;
            CutScenePre.GetComponent<CutSceneController>().CallBackFuntion = callBackFuntion;
            if (parameter != null)
                CutScenePre.GetComponent<CutSceneController>().CallBackParameter = parameter;
        }
        GameObject Prefap = Instantiate(CutScenePre, this.transform);//�ƾ� ������ ����
    }

    //�ΰ��� �޴�â ���� ��ư �̺�Ʈ ó��
    public void OnOpenInGameMenu()
    {
        InGameMenu.SetActive(true);//�ΰ��� �޴�â Ȱ��ȭ
    }

    //�ΰ��� �޴� ����ϱ� ��ư �̺�Ʈ ó��
    public void OnContinueBtn()
    {
        InGameMenu.SetActive(false);//�ΰ��� �޴�â ��Ȱ��ȭ
    }

    //Ÿ��Ʋ ȭ������ ������
    public void OnGoToTitle()
    {
        Destroy(GameManager.instance.gameObject);//�ΰ��� �޴��� ����
        ChangeScene("MainTitle");//Ÿ��Ʋȭ������ ������
    }
}
