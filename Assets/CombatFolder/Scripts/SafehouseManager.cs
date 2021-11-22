using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafehouseManager : MonoBehaviour
{
    private float timer;
    public bool isSafehouse;
    public float hideTime;
    public float fadeTime;
    private CanvasGroup cg;
    private bool isFading;
    private bool checkBoolChange;

    void Start()
    {
        isSafehouse = true;
        checkBoolChange = isSafehouse;
        cg = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        //Test change bool
        if (Input.GetKeyDown(KeyCode.B) && isSafehouse)
        {
            isSafehouse = false;
        }
        else if (Input.GetKeyDown(KeyCode.B) && !isSafehouse)
        {
            isSafehouse = true;
        }
        //when fading, but try to change state
        if(isSafehouse != checkBoolChange && isFading)
        {
            isSafehouse = checkBoolChange;
        }
        //check if fading or not
        if(timer >= fadeTime)
        {
            isFading = false;
        }
        else
        {
            timer += Time.deltaTime;
        }
        //when bool is changed, do once
        if(isSafehouse != checkBoolChange && isSafehouse)
        {
            StartCoroutine(FadeCanvas(cg, 1f, fadeTime));
            checkBoolChange = isSafehouse;
        }
        else if(isSafehouse != checkBoolChange && !isSafehouse)
        {
            StartCoroutine(FadeCanvas(cg, 0f, fadeTime));
            checkBoolChange = isSafehouse;
        }
    }

    IEnumerator FadeCanvas(CanvasGroup cg, float endValue, float duration)
    {
        timer = 0;
        isFading = true;
        float elapsedTime = 0;
        float startValue = cg.alpha;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            yield return null;
        }
    }
}
