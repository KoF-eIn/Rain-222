using System.Collections;
using UnityEngine;

public class Cube : PoolableObject
{
    [SerializeField] private float _lifeTime = 5f;
    private Coroutine _lifeRoutine;

    public override void OnSpawn()
    {
        base.OnSpawn();
        _lifeRoutine = StartCoroutine(LifeRoutine());
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        if (_lifeRoutine != null)
            StopCoroutine(_lifeRoutine);
    }

    private IEnumerator LifeRoutine()
    {
        yield return new WaitForSeconds(_lifeTime);

        if (SpawnerLocator.BombSpawner != null)
            SpawnerLocator.BombSpawner.Spawn(transform.position, Quaternion.identity);

        SpawnerLocator.CubeSpawner?.Despawn(this);
    }
}