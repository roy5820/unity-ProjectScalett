using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    private TextAsset HaveItemDataJsonFile; // ���� �� ������ JSON ������ �Ҵ��ؾ� �մϴ�.

    private void Awake()
    {
        //���� �޴��� �ν��Ͻ� �ʱ�ȭ
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        // JSON ������ �о�ͼ� �����ͺ��̽� �ʱ�ȭ
        //���� ������ �����ͺ��̽� �ʱ�ȭ
        if (HaveItemDataJsonFile != null)
        {
            HaveDataBase = JsonUtility.FromJson<PlayerDataBase>(HaveItemDataJsonFile.ToString());
        }
        else
        {
            Debug.LogError("������ �����ͺ��̽��� �ε��� �� �����ϴ�.");
        }

        //


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

                return AddStatus.Status;
            }
        }

        return -1; //�����͸� ���� ������ ��� -1�� ����
    }
}
