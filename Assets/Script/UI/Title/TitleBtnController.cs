using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class TitleBtnController : MonoBehaviour
{
    private string HaveItemDataJsonPath;//보유 아이템 데이터가 저장되는 장소
    private string ObjStatusDataJsonPath;//오브젝트 상태값 데이터가 저장되는 장소

    public GameObject OptionMenu;
    private void Awake()
    {
        //주소값 초기화
        HaveItemDataJsonPath = Path.Combine(Application.streamingAssetsPath, "HaveItemDataBase.json");
        ObjStatusDataJsonPath = Path.Combine(Application.streamingAssetsPath, "ObjStatusDataBase.json");
}

    public void OnNewStartBtn(string SceneName)
    {
        SceneManager.LoadScene(SceneName);

        //기존 데이터 삭제
        PlayerPrefs.SetString("LastSceneName", null);//플레이어 마지막 위치 삭제

        File.Delete(HaveItemDataJsonPath);//보유아이템 데이터 삭제
        File.Delete(ObjStatusDataJsonPath);//오브젝트 상태값 데이터 삭제
    }

    public void OnContinueBtn()
    {
        string lastSceneName = PlayerPrefs.GetString("LastSceneName");//마지막으로 있었던 씬으로 이동
        if (lastSceneName != null)
        {
            SceneManager.LoadScene(lastSceneName);
        }
    }

    public void OnOpenOption()
    {
        OptionMenu.SetActive(true);
    }

    public void OnCloseOption()
    {
        OptionMenu.SetActive(false);
    }
}
