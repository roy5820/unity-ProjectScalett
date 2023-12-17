using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneController : MonoBehaviour
{
    //�ƾ� �������� ���� ����
    [System.Serializable]
    public class CutScene
    {
        public int ChatLogID;//��ȭ�α�
        public bool isOpenLog = false;//��ȭ�α� ���� ����
        public string CutSceneImgName; //�ƾ��̹��� �̸�
    }

    //ȭ�鿡 ��� �ƾ����� ȭ����� ������ ����Ǵ� ����
    [SerializeField]
    private List<CutScene> CutScenes;
    int nowCutSceneNum = 0;//���� �ƾ� ��ȣ

    [SerializeField]
    private GameObject CutSceneImgObj;//�ƾ� �̹����� �ݿ��� ������Ʈ
    string CutSceneImgPath = "Image/Illustration/";

    //�ݹ� �� �Լ� ȣ��
    public GameObject CallBackObj = null;
    public string CallBackFuntion = null;
    public string CallBackParameter = null;

    private void Start()
    {
        //null üũ
        if(CutScenes.Count > 0)
        {
            UpdateCutSceneImg();
        }
    }

    public void OnChatLog()
    {
        //��ȭ�α� �������� ���ο� ���� ���� �ƾ����� �ѱ��� �α׸� ����� ����
        if (!CutScenes[nowCutSceneNum].isOpenLog)
        {
            CutScenes[nowCutSceneNum].isOpenLog = true;//��ȭ�α� �������� ���� true�� ����
            //�� üũ
            if (CutScenes[nowCutSceneNum].ChatLogID > 0)
            {
                PlayerUIController.instance.OpenChatWindow(CutScenes[nowCutSceneNum].ChatLogID, this.gameObject);//��ȭâ ����
            }
            else
            {
                //���� �ƾ����� �ѱ�� �̹��� �ݿ�
                nowCutSceneNum++;
                UpdateCutSceneImg();
            }
        }
        else
        {
            //���� �ƾ����� �ѱ�� �̹��� �ݿ�
            nowCutSceneNum++;
            if(nowCutSceneNum < CutScenes.Count)
                UpdateCutSceneImg();
            else
                Destroy(gameObject);
        }
    }

    //nowCutSceneNum ��ġ�� �����ϴ� �̹����� �ƾ� �̹��� ��ü
    public void UpdateCutSceneImg()
    {
        //�ƾ� �̹��� ��������
        Sprite CutSceneImg = Resources.Load<Sprite>(CutSceneImgPath + CutScenes[nowCutSceneNum].CutSceneImgName);
        
        //�ƾ� �̹��� ����
        CutSceneImgObj.GetComponent<Image>().sprite = CutSceneImg;
    }

    private void OnDestroy()
    {
        if (CallBackObj != null && CallBackFuntion != null)
        {
            if(CallBackParameter != null)
                CallBackObj.SendMessage(CallBackFuntion, CallBackParameter);
            else
                CallBackObj.SendMessage(CallBackFuntion);
        }
            
    }
}
