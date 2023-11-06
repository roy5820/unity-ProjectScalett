using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//
[System.Serializable]
public class Line
{
    public int lineNum;
    public string log;
    public int speakerId;
    public string spriteName;
    public int getItemID;
    public int getItemType;
}

//
[System.Serializable]
public class TalkData
{
    public int talkID;
    public string sceneName;
    public string talkName;
    public List<Line> lines;
}

[System.Serializable]
public class TalkDataList
{
    public List<TalkData> talkDataList;
}

public class TalkLogDataManager : MonoBehaviour
{
    public static TalkLogDataManager instance;
    public TalkDataList TalkDataList;
    public TextAsset jsonFile; // ���� �� ������ JSON ������ �Ҵ��ؾ� �մϴ�.

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        // JSON ������ �о�ͼ� ������ �����ͺ��̽� �ʱ�ȭ
        if (jsonFile != null)
        {
            string jsonText = jsonFile.text;

            TalkDataList = JsonUtility.FromJson<TalkDataList>(jsonText);
        }
        else
        {
            Debug.LogError("��ȭ �α� �����͸� �ε��� �� �����ϴ�.");
        }
    }

    // ID�� ������� �������� �������� �Լ� GetDatabaseType 0: UseItem(��������) 1: EvidenceItem(���ž�����)
    public Line[] GetTalkLogByID(int id)
    {
        //������ �α׹迭 ����
        Line[] OutputLines = null;
        //Ư�� ID�� ��ȭ�α׸� TalkLogLine���Ŀ� �迭�� �����ϴ� ����
        foreach (TalkData talkData in TalkDataList.talkDataList)
        {
            if (talkData.talkID == id)
            {
                //�迭 ũ�⸦ �α� ���� ����
                OutputLines = new Line[talkData.lines.Count];
                foreach (Line line in talkData.lines)
                {
                    OutputLines[line.lineNum - 1] = line;
                }
            }
        }

        return OutputLines;
    }
}
