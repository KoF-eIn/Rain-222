using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : PoolableObject
{
    [SerializeField] protected T _prefab;

    [SerializeField] protected Transform _parentForObjects;

    protected ObjectPool<T> _pool;

    public int TotalCreated => _pool.TotalCreated;
    public int SpawnCount => _pool.SpawnCount;
    public int ActiveCount => _pool.ActiveCount;

    protected virtual void Awake()
    {
        _pool = new ObjectPool<T>(_prefab, _parentForObjects != null ? _parentForObjects : transform);
    }

    public virtual T Spawn(Vector3 position, Quaternion rotation)
    {
        return _pool.Get(position, rotation);
    }

    public virtual void Despawn(T obj)
    {
        _pool.Return(obj);
    }
}