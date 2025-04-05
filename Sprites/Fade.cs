using UnityEngine;

public class LogoFade : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 2f;
    public float delayBeforeFade = 1f;

    void Start()
    {
        StartCoroutine(FadeOut());
    }

    System.Collections.IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(delayBeforeFade);

        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = 1 - (t / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0;
        gameObject.SetActive(false); // optional: disable logo after fading
    }
}
