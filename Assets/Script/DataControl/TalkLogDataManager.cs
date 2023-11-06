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
    public TextAsset jsonFile; // 이제 이 변수에 JSON 파일을 할당해야 합니다.

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        // JSON 파일을 읽어와서 아이템 데이터베이스 초기화
        if (jsonFile != null)
        {
            string jsonText = jsonFile.text;

            TalkDataList = JsonUtility.FromJson<TalkDataList>(jsonText);
        }
        else
        {
            Debug.LogError("대화 로그 데이터를 로드할 수 없습니다.");
        }
    }

    // ID를 기반으로 아이템을 가져오는 함수 GetDatabaseType 0: UseItem(사용아이템) 1: EvidenceItem(증거아이템)
    public Line[] GetTalkLogByID(int id)
    {
        //리턴할 로그배열 선언
        Line[] OutputLines = null;
        //특정 ID의 대화로그를 TalkLogLine형식에 배열에 저장하는 구간
        foreach (TalkData talkData in TalkDataList.talkDataList)
        {
            if (talkData.talkID == id)
            {
                //배열 크기를 로그 수로 설정
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
