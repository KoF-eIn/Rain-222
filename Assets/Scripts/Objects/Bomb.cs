using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(FadeEffect))]
[RequireComponent(typeof(Exploder))]
public class Bomb : PoolableObject
{
    [SerializeField] private float _minLifetime = 2f;
    [SerializeField] private float _maxLifetime = 5f;

    public event Action<Bomb> Exploded;

    private FadeEffect _fade;
    private Exploder _exploder;

    private void Awake()
    {
        _fade = GetComponent<FadeEffect>();
        _exploder = GetComponent<Exploder>();
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
        float lifetime = UnityEngine.Random.Range(_minLifetime, _maxLifetime);
        _fade.StartFade(lifetime, OnFadeComplete);
    }

    private void OnFadeComplete()
    {
        _exploder.Explode(transform.position);
        Exploded?.Invoke(this);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        StopAllCoroutines();
    }
}