using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : PoolableObject
{
    [SerializeField] private float _lifeTime = 5f;

    public event Action<Cube> Expired;

    private Coroutine _lifeRoutine;

    public override void OnSpawn()
    {
        base.OnSpawn();

        _lifeRoutine = StartCoroutine(RunLifeCycle());
    }

    public override void OnDespawn()
    {
        base.OnDespawn();

        if (_lifeRoutine != null)
            StopCoroutine(_lifeRoutine);
    }

    private IEnumerator RunLifeCycle()
    {
        yield return new WaitForSeconds(_lifeTime);

        Expired?.Invoke(this);
    }
}