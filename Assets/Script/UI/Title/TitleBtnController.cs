using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class TitleBtnController : MonoBehaviour
{
    private string HaveItemDataJsonPath;//���� ������ �����Ͱ� ����Ǵ� ���
    private string ObjStatusDataJsonPath;//������Ʈ ���°� �����Ͱ� ����Ǵ� ���

    private void Awake()
    {
        //�ּҰ� �ʱ�ȭ
        HaveItemDataJsonPath = Path.Combine(Application.streamingAssetsPath, "InGameData/HaveItemDataBase.json");
        ObjStatusDataJsonPath = Path.Combine(Application.streamingAssetsPath, "InGameData/ObjStatusDataBase.json");
}

    public void OnNewStartBtn(string SceneName)
    {
        SceneManager.LoadScene(SceneName);

        //���� ������ ����
        PlayerPrefs.SetString("LastSceneName", null);//�÷��̾� ������ ��ġ ����

        File.Delete(HaveItemDataJsonPath);//���������� ������ ����
        File.Delete(ObjStatusDataJsonPath);//������Ʈ ���°� ������ ����
    }

    public void OnContinueBtn()
    {
        string lastSceneName = PlayerPrefs.GetString("LastSceneName");//���������� �־��� ������ �̵�
        if (lastSceneName != null)
        {
            SceneManager.LoadScene(lastSceneName);
        }
    }
}
