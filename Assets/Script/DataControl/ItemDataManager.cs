using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//�����ͺ��̽� �� Item�� �Ӽ����� ���� ���� ����
[System.Serializable]
public class Item
{
    public int id;
    public string name;
    public string spriteName;
    public string additionalExplanation;
}
//�����ͺ��̽� �� Charcter�� �Ӽ����� ���� ���� ����
[System.Serializable]
public class Character
{
    public int id;
    public string name;
    public string age;
    public string job;
    public string spriteName;
}
//�����ͺ��̽� �� Testimony�� �Ӽ����� ���� ���� ����
[System.Serializable]
public class Testimony
{
    public int id;
    public int characterId;
    public string additionalExplanation;
}

//���� �� ������, ĳ���� ���� �����͸� ���� ������ ���̽�
[System.Serializable]
public class ItemDatabase
{
    public List<Item> UseItems;
    public List<Item> EvidenceItems;
    public List<Character> CharcterInformation;
    public List<Testimony> CharacterTestimony;
}

public class ItemDataManager : MonoBehaviour
{
    public static ItemDataManager instance;
    public ItemDatabase itemDatabase;
    private TextAsset jsonFile; // ���� �� ������ JSON ������ �Ҵ��ؾ� �մϴ�.

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        // JSON ������ �о�ͼ� ������ �����ͺ��̽� �ʱ�ȭ
        if (jsonFile != null)
        {
            itemDatabase = JsonUtility.FromJson<ItemDatabase>(jsonFile.ToString());
        }
        else
        {
            Debug.LogError("������ �����ͺ��̽��� �ε��� �� �����ϴ�.");
        }
    }

    // ID�� ������� �������� �������� �Լ� GetDatabaseType 0: UseItem(��������) 1: EvidenceItem(���ž�����)
    public Item GetItemByID(int id, int GetDatabaseType)
    {
        if (GetDatabaseType == 0)
        {
            foreach (Item item in itemDatabase.UseItems)
            {
                if (item.id == id)
                {
                    return item;
                }
            }
        }
        else if (GetDatabaseType == 1)
        {
            foreach (Item item in itemDatabase.EvidenceItems)
            {
                if (item.id == id)
                {
                    return item;
                }
            }
        }
        
        return null;
    }
    //ID�� ĳ���� ���� ã��
    public Character GetCharcterByID(int id)
    {
        foreach (Character character in itemDatabase.CharcterInformation)
        {
            if (character.id == id)
            {
                return character;
            }
        }

        return null;
    }

    //ID�� ���� ���� ã��
    public Testimony GetTestimonyByID(int id)
    {
        foreach (Testimony testimony in itemDatabase.CharacterTestimony)
        {
            if (testimony.id == id)
            {
                return testimony;
            }
        }

        return null;
    }
}
