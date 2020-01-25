using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class FadeIn : MonoBehaviour
{
    public FadeIn NextFadeIn;
    public float FadeDuration = 2.0f;
    public bool FadeInOnStart = false;

    private CanvasRenderer canvasRenderer;

    private void Start()
    {
        canvasRenderer = GetComponent<CanvasRenderer>();
        canvasRenderer.SetAlpha(0);
        if (FadeInOnStart)
            DoFadeIn();
    }

    public void DoFadeIn()
    {
        StartCoroutine(Fade());
    }

    public IEnumerator Fade()
    {
        Debug.Log("Fading");
        float counter = 0;

        while (counter < FadeDuration)
        {
            counter += Time.deltaTime;
            canvasRenderer.SetAlpha(Mathf.Lerp(canvasRenderer.GetAlpha(), 1, counter / FadeDuration));

            yield return null;
        }

        NextFadeIn?.DoFadeIn();
    }
}
