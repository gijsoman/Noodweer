using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndscreenManager : MonoBehaviour
{
    public RawImage HeadlineImage;
    public Text HeadlineTitle;
    public Text HeadlineText;
    public Text CommentsTitle;
    public Text Comment1;
    public Text Comment2;
    public Text Comment3;
    public Text Comment4;

    private void Start()
    {
        HeadlineImage.canvasRenderer.SetAlpha(0);
        HeadlineImage.CrossFadeAlpha(1, 2f, false);     
    }
    //IEnumerator DoFadeInImage(float duration, RawImage image, float startValeu, float endValue)
    //{
    //    float counter = 0;

    //    while (counter < duration)
    //    {
    //        counter += Time.deltaTime;
    //    }
    //    yield return new WaitForSeconds(0);
    //}

    //IEnumerator DoFadeInText(float duration, Text text, float startValue, float endValue)
    //{
    //    float counter = 0;

    //    while (counter < duration)
    //    {
    //        counter += Time.deltaTime;
    //        text.fade = Mathf.Lerp()
    //    }
    //    yield return new WaitForSeconds(0);
    //}
}
