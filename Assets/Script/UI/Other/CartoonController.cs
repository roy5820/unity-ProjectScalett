using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CartoonController : MonoBehaviour
{
    //만화 한페이지에 대한 정보값
    [System.Serializable]
    public class CartoonPage
    {
        public GameObject PageObj;
        public List<GameObject> cartoonObjs;
    }

    public List<CartoonPage> Pages;
    bool isPlay;//카툰이 출력 상태값
    public float PlaySpeed = 10f;
    public float PlayDelay = 0.3f;

    int nowPageNum = 0;
    int nowImgNum = 0;

    public string nextScene = "";
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if(!isPlay)
                PlayCartoon();
        }
    }

    public void PlayCartoon()
    {
        if (nowImgNum < Pages[nowPageNum].cartoonObjs.Count)
        {
            StartCoroutine(MovementImg(Pages[nowPageNum].cartoonObjs[nowImgNum].GetComponent<RectTransform>()));
            nowImgNum++;
        }
        else
        {
            Pages[nowPageNum].PageObj.SetActive(false);
            nowPageNum++;
            nowImgNum = 0;
            if (nowPageNum < Pages.Count)
                Pages[nowPageNum].PageObj.SetActive(true);
            else
                SceneManager.LoadScene(nextScene);
        }
    }

    IEnumerator MovementImg(RectTransform MovementObj)
    {
        float thisTime = 0;
        isPlay = true;

        while (thisTime < PlayDelay)
        {
            thisTime += Time.deltaTime;

            MovementObj.anchoredPosition = Vector2.Lerp(MovementObj.anchoredPosition, new Vector2(0,0), PlaySpeed*Time.deltaTime);
            yield return null;
        }

        isPlay = false;
    }

}
