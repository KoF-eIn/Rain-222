using UnityEngine;
using System.Collections;

public class FadeController : MonoBehaviour
{

    private void Awake()
    {
        if (_targetRenderer == null)
            _targetRenderer = GetComponent<Renderer>();

        _material = _targetRenderer.material;
        SetMaterialToFadeMode();
    }

    private static class ShaderProperties
    {
        public const string Mode = "_Mode";
        public const string SrcBlend = "_SrcBlend";
        public const string DstBlend = "_DstBlend";
        public const string ZWrite = "_ZWrite";
        public const string Color = "_Color";
    }

    private static class RenderingModes
    {
        public const float Opaque = 0f;
        public const float Cutout = 1f;
        public const float Fade = 2f;
        public const float Transparent = 3f;
    }

    [SerializeField] private Renderer _targetRenderer;

    private Material _material;
    private Coroutine _fadeRoutine;

    private void SetMaterialToFadeMode()
    {
        _material.SetFloat(ShaderProperties.Mode, RenderingModes.Fade);
        _material.SetInt(ShaderProperties.SrcBlend, (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        _material.SetInt(ShaderProperties.DstBlend, (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        _material.SetInt(ShaderProperties.ZWrite, 0);
        _material.EnableKeyword("_ALPHABLEND_ON");
        _material.DisableKeyword("_ALPHATEST_ON");
        _material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        _material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }

    public void StartFade(float duration, System.Action onComplete = null)
    {
        if (_fadeRoutine != null)
            StopCoroutine(_fadeRoutine);

        _fadeRoutine = StartCoroutine(FadeRoutine(duration, onComplete));
    }

    private IEnumerator FadeRoutine(float duration, System.Action onComplete)
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