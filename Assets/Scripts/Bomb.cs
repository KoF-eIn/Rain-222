using System.Collections;
using UnityEngine;

public class Bomb : PoolableObject
{
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private float _minLifetime = 2f;
    [SerializeField] private float _maxLifetime = 5f;

    private Renderer _renderer;

    private Material _material;

    private float _explosionTime;
    private float _currentAlpha = 1f;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
        SetMaterialTransparent();
    }

    public override void OnSpawn()
    {
        base.OnSpawn();

        _explosionTime = Random.Range(_minLifetime, _maxLifetime);
        _currentAlpha = 1f;
        UpdateAlpha();
        StartCoroutine(FadeAndExplode());
    }

    public override void OnDespawn()
    {
        base.OnDespawn();

        StopAllCoroutines();
    }

    private IEnumerator FadeAndExplode()
    {
        float elapsed = 0f;

        while (elapsed < _explosionTime)
        {
            elapsed += Time.deltaTime;
            _currentAlpha = Mathf.Lerp(1f, 0f, elapsed / _explosionTime);
            UpdateAlpha();

            yield return null;
        }

        Explode();

        SpawnerLocator.BombSpawner?.Despawn(this);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (var col in colliders)
        {
            if (col.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, 0f, ForceMode.Impulse);
            }
        }
    }

    private void UpdateAlpha()
    {
        Color color = _material.color;
        color.a = _currentAlpha;
        _material.color = color;
    }

    private void SetMaterialTransparent()
    {
        _material.SetFloat("_Mode", 2); // 2 = Fade
        _material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        _material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        _material.SetInt("_ZWrite", 0);
        _material.DisableKeyword("_ALPHATEST_ON");
        _material.EnableKeyword("_ALPHABLEND_ON");
        _material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        _material.renderQueue = 3000;
    }
}