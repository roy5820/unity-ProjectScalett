using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

//������ ID����
[System.Serializable]
public class DataIds
{
    public int ID;
    public int RelatedID;
}

//�÷��̾� ���������� ������ ������ ������
[System.Serializable]
public class PlayerDataBase
{
    public List<DataIds> UseItem;
    public List<DataIds> EvidenceItem;
    public List<DataIds> CharacterInformation;
    public List<DataIds> CharacterTestimony;
}

[System.Serializable]
//����ü�� Status�����ϴ� �����ͺ��̽� ��ü ����
public class ObjStatus
{
    public string DataName;
    public int Status;
}

[System.Serializable]
public class StatusDataBase
{
    public List<ObjStatus> ObjStatus;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;//���Ӹ޴��� �ν��Ͻ�ȭ�� ���� ���� �ʱ�ȭ
    public PlayerDataBase HaveDataBase; // �������� ������ ������ ���̽�
    public StatusDataBase ObjStatusDataBase;//������Ʈ ���� �����ϴ� ������ ���̽�

    public int SelectItemId = 0; //���� ������ ��� ������

    private string InGameDataJsonPath;//�ΰ��� �����Ͱ� ����Ǵ� ���
    private string HaveItemDataJsonFile; // ���������� �����Ͱ� ����Ǵ� JSON����
    private string ObjStatusDataJsonFile; // ������Ʈ ���°��� �����Ͱ� ����Ǵ� JSON����

    private string HaveItemDataPath;//���� ������ JSON���� ���
    private string ObjStatusDataPath;//���� ������ JSON���� ���

    private void Awake()
    {
        //���� �޴��� �ν��Ͻ� �ʱ�ȭ
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
            

        DontDestroyOnLoad(gameObject);

        HaveItemDataPath = Path.Combine(Application.streamingAssetsPath, "HaveItemDataBase.json");//���� ������ JSON���� ��� �ʱ�ȭ
        ObjStatusDataPath = Path.Combine(Application.streamingAssetsPath, "ObjStatusDataBase.json");//������Ʈ ���°� JSON���� ��� �ʱ�ȭ

    //JSON���� �о� ����
    OnLoadJsonFile();
    }

    //�÷��̾� ������ �����ͺ��̽��� Ư�� �������� �ִ��� Ȯ���ϴ� �Լ�
    public bool ChkItem(int ItemType, int ItemId)
    {
        if (ItemType < 0 && ItemId <= 0)//�Ű����� �� üũ
            return false;

        List<DataIds> thisIDs  = null;//������ Ÿ�Ժ� ����Ʈ�� �巯�� ���� ����

        //Ÿ�Ժ� ����Ʈ ����
        switch (ItemType)
        {
            case 0:
                thisIDs = HaveDataBase.UseItem;
                break;
            case 1:
                thisIDs = HaveDataBase.EvidenceItem;
                break;
            case 2:
                thisIDs = HaveDataBase.CharacterInformation;
                break;
            case 3:
                thisIDs = HaveDataBase.CharacterTestimony;
                break;
            default:
                return false;//�´� ������ Ÿ���� ������ ����
        }
        //���� �κ��丮���� �ش� ������ �˻� �� ������ true ����
        foreach (DataIds id in thisIDs)
        {
            if (id.ID == ItemId)
            {
                return true;
            }
        }

        return false;
    }

    //�÷��̾� ������ �����ͺ��̽��� ������ �߰��ϴ� �Լ�
    public void AddItem(int ItemType, int ItemId, int ItemRelatedID = 0)
    {
        List<DataIds> GetDataBase = null;//������ ���̽����� �����͸� �޾� ���� ����

        //������ �ߺ� üũ, �̹� ���� ��� �Լ� ����
        if (ChkItem(ItemType, ItemId)) return;

        //������ Ÿ�Ժ��� List ����
        switch (ItemType)
        {
            case 0:
                GetDataBase = HaveDataBase.UseItem;
                break;
            case 1:
                GetDataBase = HaveDataBase.EvidenceItem;
                break;
            case 2:
                GetDataBase = HaveDataBase.CharacterInformation;
                break;
            case 3:
                GetDataBase = HaveDataBase.CharacterTestimony;
                break;
        }

        //������ ���� �ϴ� �κ�
        //������ ������ ������ ���̽��� �´� �����ͷ� ����
        DataIds ComparativeData = new DataIds();
        ComparativeData.ID = ItemId;//������ ���̵� ����
        ComparativeData.RelatedID = ItemRelatedID;//������ ���� ���̵� ����

        int DataBaseCnt = GetDataBase.Count;//���� ����Ʈ�� ũ��
        //����Ʈ�� ũ�Ⱑ 0���� Ŭ�� ���������� ���� ������ ���� ���� �ƴ� �� �׳� �߰�
        if (DataBaseCnt > 0)
        {
            for (int i = 0; i < DataBaseCnt; i++)
            {
                //���� �����Ͱ� ���� ���̵� ���� ���� �� ���� ��ġ�� ������ ����
                if (ComparativeData.ID < GetDataBase[i].ID)
                {
                    //������ �����͸� ���� ��ġ�� ����
                    GetDataBase.Insert(i, ComparativeData);
                    return;
                }

                //���� ��ġ�� �������� ���ϰ�� NowData�� ���� ����Ʈ ���� �߰�
                if (i == DataBaseCnt - 1)
                    GetDataBase.Add(ComparativeData);
            }
        }
        else
        {
            GetDataBase.Add(ComparativeData);//������ �߰�
        }

        OnSave();
    }
    //�����ͺ��̽����� �������� �����ϴ� �Լ�
    public void DelItem(int ItemType, int ItemId)
    {
        //�÷��̾� �����ͺ��̽� ��������
        List<DataIds> GetDataBase = null;//������ ���̽����� �����͸� �޾� ���� ����

        //������ Ÿ�Ժ��� List ����
        switch (ItemType)
        {
            case 0:
                GetDataBase = HaveDataBase.UseItem;
                break;
            case 1:
                GetDataBase = HaveDataBase.EvidenceItem;
                break;
            case 2:
                GetDataBase = HaveDataBase.CharacterInformation;
                break;
            case 3:
                GetDataBase = HaveDataBase.CharacterTestimony;
                break;
        }

        //ID�� ���� �κ��丮���� ������ ����
        foreach (DataIds dataid in GetDataBase)
        {
            if (dataid.ID == ItemId)
            {
                GetDataBase.Remove(dataid);
                break;
            }
        }

        OnSave();//���� �� ����
    }

    //ObjStatusDataBase�� ��Ʈ�� �ϴ� �Լ� Mode: 0:�б� 1:���� 
    public int ControllObjStatusData(int Mode,string DataName, int inputStatus)
    {
        //nullüũ
        if (DataName != null && inputStatus >= 0)
        {
            //������ ���̽����� ������Ʈ �̸����� �˻��Ͽ� ���� �����µ� Mode�� ���� �б� �Ǵ� ���� ����
            foreach (ObjStatus thisStatus in ObjStatusDataBase.ObjStatus)
            {
                //ObjName�� ��ġ�ϴ� ������ �ִ��� �˻�
                if (thisStatus.DataName == DataName)
                {
                    //�б� ����� ��� ���°��� �����ϰ� ����
                    //���� ����� ��� ���°� ����
                    if (Mode == 1)
                    {
                        thisStatus.Status = inputStatus;
                    }
                    return thisStatus.Status;
                }
            }

            //������ ���̽��� �ش� �����Ͱ� ���� ��� ������ �߰�
            if (Mode == 1)
            {
                //�߰��� ObjStatus��ü ���� �� �� ����
                ObjStatus AddStatus = new ObjStatus();
                AddStatus.DataName = DataName;
                AddStatus.Status = inputStatus;

                ObjStatusDataBase.ObjStatus.Add(AddStatus);//����Ʈ�� ������ �߰�

                OnSave();

                return AddStatus.Status;
            }
        }

        return -1; //�����͸� ���� ������ ��� -1�� ����
    }

    //�ΰ��� �����Ͱ� ����� JSON�� �о���� �Լ�
    private void OnLoadJsonFile()
    {
        // JSON ������ �о�ͼ� �����ͺ��̽� �ʱ�ȭ
        //���� ������ �����ͺ��̽� �ʱ�ȭ �κ�
        //��ο� ���� ������ ���� ���࿩�� ����
        if (File.Exists(HaveItemDataPath))
        {
            HaveItemDataJsonFile = File.ReadAllText(HaveItemDataPath);//json���� ��������
            HaveDataBase = JsonUtility.FromJson<PlayerDataBase>(HaveItemDataJsonFile);//������ json���� �����ͺ��̽��� ����
        }


        //������Ʈ ���°� ����Ǵ� �����ͺ��̽� �ʱ�ȭ �κ�
        //��ο� ���� ������ ���� ���࿩�� ����
        if (File.Exists(ObjStatusDataPath))
        {
            ObjStatusDataJsonFile = File.ReadAllText(ObjStatusDataPath);//json���� ��������
            ObjStatusDataBase = JsonUtility.FromJson<StatusDataBase>(ObjStatusDataJsonFile);//������ json���� �����ͺ��̽��� ����
        }
    }

    //������ ���� �Լ�
    private void OnSave()
    {
        //���� ���� �Ǵ� �޴��� ���� �� ���� ������ json ���Ϸ� ����
        //���� ������ �κ��丮�� json���Ϸ� �����ϴ� �κ�
        string haveItemDataBaseJson = JsonConvert.SerializeObject(HaveDataBase); //������ ���̽� ������ json���ڿ��� ��ȯ
        File.WriteAllText(HaveItemDataPath, haveItemDataBaseJson);//Json ���ڿ��� ���Ͽ� ����
        
        //������Ʈ ���°��� json���Ϸ� �����ϴ� �κ�
        string objStatusDatatBasejson = JsonConvert.SerializeObject(ObjStatusDataBase); //������ ���̽� ������ json���ڿ��� ��ȯ
        File.WriteAllText(ObjStatusDataPath, objStatusDatatBasejson);//Json ���ڿ��� ���Ͽ� ����
    }

    private void OnDestroy()
    {
        //������ �ִ� �� �̸��� PlayerPrefs�� ����
        PlayerPrefs.SetString("LastSceneName", SceneManager.GetActiveScene().name);
    }
}
