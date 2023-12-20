using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingCradit : MonoBehaviour
{
    public GameObject Cradit;
    public float PlaySpeed = 10f;
    public float PlayDelay = 0.3f;
    public string moveSceneName = null;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MovementImg(Cradit.GetComponent<RectTransform>()));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(moveSceneName);
        }
    }

    IEnumerator MovementImg(RectTransform MovementObj)
    {
        float thisTime = 0;

        while (thisTime < PlayDelay)
        {
            thisTime += Time.deltaTime;

            MovementObj.anchoredPosition = Vector2.Lerp(MovementObj.anchoredPosition, new Vector2(0, 0), PlaySpeed * Time.deltaTime);
            yield return null;
        }
    }
}
