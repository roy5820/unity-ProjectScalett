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

    private void Awake()
    {
        //���� �޴��� �ν��Ͻ� �ʱ�ȭ
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    //�÷��̾� ������ �����ͺ��̽��� Ư�� �������� �ִ��� Ȯ���ϴ� �Լ�
    public void GetItem(int ItemType, int ItemId)
    {

    }

    //�÷��̾� ������ �����ͺ��̽��� ������ �߰��ϴ� �Լ�
    public void AddItem(int ItemType, int ItemId)
    {
        DataIds dataid = new DataIds();//������ ���̽����� �����͸� �޾� ���� ����
        bool DuplicateChk = false; //������ �ߺ� üũ
        //������ Ÿ�Ժ��� ������ ��������
        switch (ItemType)
        {
            case 0:
            case 1:
                Item GetItem = ItemDataManager.instance.GetItemByID(ItemId, ItemType);//������ Id�� ������ ���� ��������
                //PlayerDataBase�� �°� ������ ��ȯ
                
                dataid.ID = GetItem.id;
                //Ÿ�Ժ��� �÷��̾� �κ��丮�� ID�߰�
                if (ItemType == 0)
                {
                    //�̹� ���������� �ߺ� üũ
                    foreach (DataIds data in GameManager.instance.HaveDataBase.UseItem)
                    {
                        if (data.ID == ItemId)
                        {
                            DuplicateChk = true;
                        }
                    }
                    //�ش� �����Ͱ� �̹� �κ��丮�� �ִ� �� �ߺ� üũ
                    if (!DuplicateChk)
                    {
                        if (GetItem != null)
                            GameManager.instance.HaveDataBase.UseItem.Add(dataid);//������ �κ��丮�� ID �߰�
                    }
                }
                else if (ItemType == 1)
                {
                    //�̹� ���������� �ߺ� üũ
                    foreach (DataIds data in GameManager.instance.HaveDataBase.EvidenceItem)
                    {
                        if (data.ID == ItemId)
                        {
                            DuplicateChk = true;
                        }
                    }
                    //�ش� �����Ͱ� �̹� �κ��丮�� �ִ� �� �ߺ� üũ
                    if (!DuplicateChk)
                    {
                        if (GetItem != null)
                            GameManager.instance.HaveDataBase.EvidenceItem.Add(dataid);//������ �κ��丮�� ID �߰�
                    }
                }

                break;
            case 2:
                //�̹� ���������� �ߺ� üũ
                foreach (DataIds data in GameManager.instance.HaveDataBase.CharacterInformation)
                {
                    if (data.ID == ItemId)
                    {
                        DuplicateChk = true;
                    }
                }
                //�ش� �����Ͱ� �̹� �κ��丮�� �ִ� �� �ߺ� üũ
                if (!DuplicateChk)
                {
                    Character GetCharacter = ItemDataManager.instance.GetCharcterByID(ItemId);

                    dataid.ID = GetCharacter.id;
                    if (GetCharacter != null)
                        GameManager.instance.HaveDataBase.CharacterInformation.Add(dataid);//������ �κ��丮�� ID �߰�
                }

                break;
            case 3:
                //�̹� ���������� �ߺ� üũ
                foreach (DataIds data in GameManager.instance.HaveDataBase.CharacterTestimony)
                {
                    if (data.ID == ItemId)
                    {
                        DuplicateChk = true;
                    } 
                }
                //�ش� �����Ͱ� �̹� �κ��丮�� �ִ� �� �ߺ� üũ
                if (!DuplicateChk)
                {
                    //������ ������ ���̽��� �ش� �������� �ִ� �� üũ
                    Testimony GetTestimony = ItemDataManager.instance.GetTestimonyByID(ItemId);

                    dataid.ID = GetTestimony.id;
                    dataid.RelatedID = GetTestimony.id / 100;
                    if (GetTestimony != null)
                        GameManager.instance.HaveDataBase.CharacterTestimony.Add(dataid);//������ �κ��丮�� ID �߰�
                }
                
                break;
        }
    }
    //�����ͺ��̽����� �������� �����ϴ� �Լ�
    public void DelItem(int ItemType, int ItemId)
    {
        //�÷��̾� �����ͺ��̽� ��������
        PlayerDataBase haveItemData = GameManager.instance.HaveDataBase;

        //������ ������ �̺�Ʈ ó��
        if (ItemType == 0)
        {
            //ItemId�� ��ġ�ϴ� �������� �κ��丮���� �����ϴ� �����Դϴ�.
            List<DataIds> UseItemList = GameManager.instance.HaveDataBase.UseItem; //�迭 ������ ���� ������ �����ͺ��̽� ����Ʈȭ
            foreach (DataIds dataid in haveItemData.UseItem)
            {
                if (dataid.ID == ItemId)
                {
                    UseItemList.Remove(dataid);
                    break;
                }
            }
        }
        else if (ItemType == 1)
        {
            //ItemId�� ��ġ�ϴ� �������� �κ��丮���� �����ϴ� �����Դϴ�.
            List<DataIds> UseItemList = GameManager.instance.HaveDataBase.EvidenceItem; //�迭 ������ ���� ������ �����ͺ��̽� ����Ʈȭ
            foreach (DataIds dataid in haveItemData.EvidenceItem)
            {
                if (dataid.ID == ItemId)
                {
                    UseItemList.Remove(dataid);
                    break;
                }
            }
        }
        else if (ItemType == 2)
        {
            //ItemId�� ��ġ�ϴ� �������� �κ��丮���� �����ϴ� �����Դϴ�.
            List<DataIds> UseItemList = GameManager.instance.HaveDataBase.CharacterInformation; //�迭 ������ ���� ������ �����ͺ��̽� ����Ʈȭ
            foreach (DataIds dataid in haveItemData.CharacterInformation)
            {
                if (dataid.ID == ItemId)
                {
                    UseItemList.Remove(dataid);
                    break;
                }
            }
        }
        else if (ItemType == 3)
        {
            //ItemId�� ��ġ�ϴ� �������� �κ��丮���� �����ϴ� �����Դϴ�.
            List<DataIds> UseItemList = GameManager.instance.HaveDataBase.CharacterTestimony; //�迭 ������ ���� ������ �����ͺ��̽� ����Ʈȭ
            foreach (DataIds dataid in haveItemData.CharacterTestimony)
            {
                if (dataid.ID == ItemId)
                {
                    UseItemList.Remove(dataid);
                    break;
                }
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
