using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TalkAndMovementEvent : MonoBehaviour
{
    public string CutScenePreName;//
    public string SceneName;//
    public void OnOpenCutSceneAndMovement()
    {
        PlayerUIController.instance.OpenCutSceneWindow(CutScenePreName, this.gameObject, "MovementScene", SceneName);
    }

    public void MovementScene()
    {
        Destroy(GameManager.instance.gameObject);
        
        SceneManager.LoadScene(SceneName);

    }
}
