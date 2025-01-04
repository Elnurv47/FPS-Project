using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageEffect : MonoBehaviour
{
    private const float INITIAL_ALPHA = 0f;
    private const float DAMAGE_ALPHA = 0.7f;
    private const float FADE_DURATION = 0.5f;

    [SerializeField] private Image damageOverlay;

    private Color overlayColor;
    private Coroutine fadeCoroutine;

    void Start()
    {
        if (damageOverlay != null)
        {
            overlayColor = damageOverlay.color;
            SetOverlayAlpha(INITIAL_ALPHA);
        }
    }

    public void ShowDamageEffect()
    {
        if (damageOverlay != null)
        {
            SetOverlayAlpha(DAMAGE_ALPHA);

            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);

            fadeCoroutine = StartCoroutine(FadeOutOverlay());
        }
    }

    private void SetOverlayAlpha(float alpha)
    {
        overlayColor.a = alpha;
        damageOverlay.color = overlayColor;
    }

    private IEnumerator FadeOutOverlay()
    {
        float elapsedTime = 0f;

        while (elapsedTime < FADE_DURATION)
        {
            elapsedTime += Time.deltaTime;
            SetOverlayAlpha(Mathf.Lerp(DAMAGE_ALPHA, INITIAL_ALPHA, elapsedTime / FADE_DURATION));
            yield return null;
        }

        SetOverlayAlpha(INITIAL_ALPHA);
    }
}
