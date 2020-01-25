using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public FadeIn NextFadeIn;
    public float FadeDuration = 2.0f;
    public bool FadeInOnStart = false;

    private CanvasRenderer canvasRenderer;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasRenderer = GetComponent<CanvasRenderer>();

        if (canvasRenderer == null)
        {
            //if there is no canvasrenderer we expect a canvasgroup
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                Debug.LogWarning("You need to have at least a canvasrender or a canvasgroup attached to this gameobject");
                return;
            }
            canvasGroup.alpha = 0;           
        }
        else
        {
            canvasRenderer.SetAlpha(0);
        }

        if (FadeInOnStart)
            DoFadeIn();
    }

    public void DoFadeIn()
    {
        if (canvasRenderer != null || canvasGroup != null)
            StartCoroutine(Fade());
        else
            Debug.LogWarning("No canvasRenderer or canvasGroup attatched.");
    }

    public IEnumerator Fade()
    {
        float counter = 0;

        while (counter < FadeDuration)
        {
            counter += Time.deltaTime;
            if (canvasRenderer != null)
                canvasRenderer.SetAlpha(Mathf.Lerp(canvasRenderer.GetAlpha(), 1, counter / FadeDuration));
            else if (canvasGroup != null)
                canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 1, counter / FadeDuration);

            yield return null;
        }

        NextFadeIn?.DoFadeIn();
    }
}
