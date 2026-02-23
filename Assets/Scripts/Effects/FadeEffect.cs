using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class FadeEffect : MonoBehaviour
{
    private Renderer _renderer;
    private Material _material;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
    }

    public void StartFade(float duration, Action onComplete = null)
    {
        StartCoroutine(FadeCoroutine(duration, onComplete));
    }

    private IEnumerator FadeCoroutine(float duration, Action onComplete)
    {
        float elapsed = 0f;
        Color startColor = _material.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            _material.color = Color.Lerp(startColor, targetColor, t);

            yield return null;
        }

        _material.color = targetColor;
        onComplete?.Invoke();
    }

    public void ResetAlpha()
    {
        Color c = _material.color;
        c.a = 1f;
        _material.color = c;
    }
}