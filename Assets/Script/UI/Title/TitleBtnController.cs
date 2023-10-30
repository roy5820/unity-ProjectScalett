using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleBtnController : MonoBehaviour
{
    public void OnNewStartBtn(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
