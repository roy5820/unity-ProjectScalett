using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//데이터베이스 안 Item의 속성값을 가진 변수 선언
[System.Serializable]
public class Item
{
    public int id;
    public string name;
    public string spriteName;
    public string additionalExplanation;
}
//데이터베이스 안 Charcter의 속성값을 가진 변수 선언
[System.Serializable]
public class Character
{
    public int id;
    public string name;
    public string age;
    public string job;
    public string spriteName;
}
//데이터베이스 안 Testimony의 속성값을 가진 변수 선언
[System.Serializable]
public class Testimony
{
    public int id;
    public int characterId;
    public string additionalExplanation;
}

//게임 내 아이템, 캐릭터 등의 데이터를 갖는 데이터 베이스
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
    private TextAsset jsonFile; // 이제 이 변수에 JSON 파일을 할당해야 합니다.

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        // JSON 파일을 읽어와서 아이템 데이터베이스 초기화
        if (jsonFile != null)
        {
            itemDatabase = JsonUtility.FromJson<ItemDatabase>(jsonFile.ToString());
        }
        else
        {
            Debug.LogError("아이템 데이터베이스를 로드할 수 없습니다.");
        }
    }

    // ID를 기반으로 아이템을 가져오는 함수 GetDatabaseType 0: UseItem(사용아이템) 1: EvidenceItem(증거아이템)
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
    //ID로 캐릭터 정보 찾기
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

    //ID로 증언 정보 찾기
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
