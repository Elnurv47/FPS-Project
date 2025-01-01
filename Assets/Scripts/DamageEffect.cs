using UnityEngine;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour
{
    private Color overlayColor;

    [SerializeField] private Image damageOverlay;
    [SerializeField] private float fadeDuration = 0.5f;

    void Start()
    {
        if (damageOverlay != null)
        {
            overlayColor = damageOverlay.color;
            overlayColor.a = 0;
            damageOverlay.color = overlayColor;
        }
    }

    public void ShowDamageEffect()
    {
        if (damageOverlay != null)
        {
            overlayColor.a = 1f;
            damageOverlay.color = overlayColor;

            StartCoroutine(FadeOutOverlay());
        }
    }

    private System.Collections.IEnumerator FadeOutOverlay()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            overlayColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            damageOverlay.color = overlayColor;

            yield return null;
        }

        overlayColor.a = 0f;
        damageOverlay.color = overlayColor;
    }
}
