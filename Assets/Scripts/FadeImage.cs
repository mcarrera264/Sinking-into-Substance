using System;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    public Image targetImage;
    public float fadeDuration = 1.0f;

    public bool fadeInAwake = false;
    public bool invertLogic = false;

    public bool isFading = false;

    [System.Serializable]
    public class FadeEvent : UnityEvent { }

    public FadeEvent OnFadeInComplete;
    public FadeEvent OnFadeOutComplete;

    private void Start()
    {
        if (fadeInAwake)
            FadeIn();

    }

    public void FadeIn()
    {
        if (isFading)
            return;

        float startFloat = 1.0f;
        float targetFloat = 0.0f;

        if (invertLogic)
        {
            startFloat = 0.0f;
            targetFloat = 1.0f;

        }

        StartCoroutine(FadeImageCoroutine(startFloat, targetFloat, () =>
        {
            isFading = false;
            OnFadeInComplete?.Invoke();
        }));
    }

    public void FadeOut()
    {
        if (isFading)
            return;

        float startFloat = 0.0f;
        float targetFloat = 1.0f;

        if (invertLogic)
        {
            startFloat = 1.0f;
            targetFloat = 0.0f;

        }

        StartCoroutine(FadeImageCoroutine(startFloat, targetFloat, () =>
        {
            isFading = false;
            OnFadeOutComplete?.Invoke();
        }));
    }

    private IEnumerator FadeImageCoroutine(float startAlpha, float targetAlpha, Action onComplete)
    {
        isFading = true;

        float elapsedTime = 0.0f;
        Color imageColor = targetImage.color;

        while (elapsedTime < fadeDuration)
        {
            float normalizedTime = elapsedTime / fadeDuration;
            imageColor.a = Mathf.Lerp(startAlpha, targetAlpha, normalizedTime);
            targetImage.color = imageColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        imageColor.a = targetAlpha;
        targetImage.color = imageColor;

        onComplete?.Invoke();
    }
}
