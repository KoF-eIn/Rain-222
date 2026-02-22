using UnityEngine;
using System.Collections;

public class Bomb : PoolableObject
{
    [SerializeField] private float _minLifetime = 2f;
    [SerializeField] private float _maxLifetime = 5f;

    private FadeController _fadeController;
    private Exploder _exploder;

    private void Awake()
    {
        _fadeController = GetComponent<FadeController>();

        if (_fadeController == null)
            _fadeController = gameObject.AddComponent<FadeController>();

        _exploder = GetComponent<Exploder>();

        if (_exploder == null)
            _exploder = gameObject.AddComponent<Exploder>();
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
        float lifetime = Random.Range(_minLifetime, _maxLifetime);
        _fadeController.ResetAlpha();
        _fadeController.StartFade(lifetime, OnFadeComplete);
    }

    private void OnFadeComplete()
    {
        _exploder.Explode(transform.position);
        SpawnerLocator.BombSpawner?.Despawn(this);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        StopAllCoroutines();
    }
}